using BowlingGame.Models;
using BowlingGame.Services.Contract;
using System;
using BowlingGame.Common.Constants;

namespace BowlingGame.Services
{
    public class BowlingGameService : IBowlingGameService
    {
        private readonly IRandomRollService _randomRollService;

        public BowlingGameService(IRandomRollService randomRollService)
        {
            _randomRollService = randomRollService;
        }

        public void PlayGame()
        {
            try
            {
                Console.Write("Player name: ");
                var userProfile = new UserProfile {Name = Console.ReadLine()};
                while (string.IsNullOrEmpty(userProfile.Name))
                {
                    Console.Write("Please enter player name: ");
                    userProfile.Name = Console.ReadLine();
                }

                Console.WriteLine($"Welcome {userProfile.Name}, wish you have a nice game!");
                ShowMenu();

                var option = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(option))
                {
                    Console.Write(@"Please type something: ");
                    option = Console.ReadLine();
                }

                while (!option.ToLower().Equals(BowlingConstant.Quit) && !option.ToLower().Equals(BowlingConstant.Start))
                {
                    Console.Write(@"Please choose correct option: ");
                    option = Console.ReadLine();
                }

                ChooseOption(option.ToLower(), userProfile);
                Console.Write($"'{BowlingConstant.Quit}' for quit, enter for making another game: ");
                var answer = Console.ReadLine();
                if (string.IsNullOrEmpty(answer))
                {
                    PlayGame();
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Something bad happened");
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine(@"Please choose options below: ");
            Console.WriteLine("1. 'Start' the game");
            Console.WriteLine("2. 'q' for Exit");
        }

        private void ChooseOption(string option, UserProfile userProfile)
        {
            switch (option)
            {
                case BowlingConstant.Start:
                    _randomRollService.RollTheBall(userProfile);
                    break;
                case BowlingConstant.Quit:
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}