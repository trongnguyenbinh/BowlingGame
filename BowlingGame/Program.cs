using System;
using BowlingGame.Services;
using BowlingGame.Services.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartUp();
        }

        private static void StartUp()
        {
            var collection = new ServiceCollection()
                .AddScoped<IRandomRollService, RandomRollService>()
                .AddTransient<IScoreCalculationService,ScoreCalculationService>()
                .AddSingleton<IBowlingGameService,BowlingGameService>();

            var serviceProvider = collection.BuildServiceProvider();

            var bowlingGameService = serviceProvider.GetService<IBowlingGameService>();
            bowlingGameService?.PlayGame();
            
            Console.ReadLine();
        }

    }
}
