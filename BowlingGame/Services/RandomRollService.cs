using BowlingGame.Common.Constants;
using BowlingGame.Models;
using BowlingGame.Services.Contract;
using System;
using System.Collections.Generic;

namespace BowlingGame.Services
{
    public class RandomRollService : IRandomRollService
    {
        private readonly IScoreCalculationService _scoreCalculationService;

        public RandomRollService(IScoreCalculationService scoreCalculationService)
        {
            _scoreCalculationService = scoreCalculationService;
        }

        public void RollTheBall(UserProfile userProfile)
        {
            try
            {
                Console.Write("Press enter to throw the ball ");
                Console.ReadLine();
                var frameIndex = 1;
                userProfile.TenFramesLog = new List<int[]>();
                userProfile.TenFramesScore = new List<int>();

                while (true)
                {
                    var frame = GetFrameScore(frameIndex);
                    var throw1 = frame[0];
                    var throw2 = frame[1];
                    var frameScore = throw1 + throw2;
                    userProfile.TenFramesScore.Add(frameScore);

                    userProfile.TenFramesLog.Add(frame.Length == 3
                        ? new[] {throw1, throw2, frame[2]}
                        : new[] {throw1, throw2});

                    Console.WriteLine(
                        $"Frame {frameIndex} {GetFrameMessage(throw1, frameScore)}, Your throw [{throw1},{throw2}] Your frame score: {frameScore}");
                    frameIndex++;

                    if (frameIndex > BowlingConstant.MaximumFrame)
                    {
                        break;
                    }

                    Console.Write("Continue throw the ball: ");
                    Console.ReadLine();
                }


                _scoreCalculationService.EndGame(userProfile);
            }
            catch (Exception)
            {
                Console.WriteLine("Something bad happened");
            }
        }

        private int[] GetFrameScore(int frameIndex)
        {
            var random = new Random();
            var firstThrowInFrame = random.Next(0, BowlingConstant.MaximumPointPerFrame + 1);
            var secondThrowInFrame = random.Next(0, 11 - firstThrowInFrame);
            if (frameIndex != BowlingConstant.MaximumFrame) return new[] {firstThrowInFrame, secondThrowInFrame};

            if (firstThrowInFrame.Equals(BowlingConstant.MaximumPointPerFrame))
            {
                var firstBonusThrow = random.Next(0, 11);
                var secondBonusThrow = random.Next(0, 11);
                if (firstBonusThrow == BowlingConstant.MaximumPointPerFrame)
                {
                    secondBonusThrow = random.Next(0, 11 - firstThrowInFrame);
                }

                return new[] {firstThrowInFrame, firstBonusThrow, secondBonusThrow};
            }

            if (firstThrowInFrame + secondThrowInFrame == BowlingConstant.MaximumPointPerFrame)
            {
                var bonusThrow = random.Next(0, 10);
                return new[] {firstThrowInFrame, secondThrowInFrame, bonusThrow};
            }

            return new[] {firstThrowInFrame, secondThrowInFrame};
        }

        private string GetFrameMessage(int throw1, int totalFrameScore)
        {
            if (throw1.Equals(10))
            {
                return BowlingConstant.Strike;
            }

            if (totalFrameScore.Equals(10))
            {
                return BowlingConstant.Spare;
            }

            return BowlingConstant.Complete;
        }
    }
}