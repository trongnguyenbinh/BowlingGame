using BowlingGame.Common.Constants;
using BowlingGame.Models;
using BowlingGame.Services.Contract;
using System;
using System.Linq;

namespace BowlingGame.Services
{
    public class ScoreCalculationService : IScoreCalculationService
    {
        public void EndGame(UserProfile userProfile)
        {
            for (var i = 0; i < userProfile.TenFramesLog.Count; i++)
            {
                var throw1 = userProfile.TenFramesLog[i][0];
                var throw2 = userProfile.TenFramesLog[i][1];
                var frameScore = userProfile.TenFramesScore[i];

                if (frameScore == BowlingConstant.MaximumPointPerFrame && i < 9)
                {
                    var nextFrameScore = userProfile.TenFramesScore[i + 1];
                    if (throw1 == BowlingConstant.MaximumPointPerFrame)
                    {
                        if (userProfile.TenFramesLog[i + 1][0] == BowlingConstant.MaximumPointPerFrame)
                        {
                            if (i < 8)
                            {
                                var strikeBonus = BowlingConstant.MaximumPointPerFrame +
                                                  userProfile.TenFramesLog[i + 2][0];
                                userProfile.TenFramesScore[i] += strikeBonus;
                                GenerateBonusInform(userProfile.Name, BowlingConstant.Strike, i, strikeBonus);
                            }
                            else
                            {
                                var strikeBonus = userProfile.TenFramesLog[i + 1][0] +
                                                  userProfile.TenFramesLog[i + 1][1];
                                userProfile.TenFramesScore[i] += strikeBonus;
                                GenerateBonusInform(userProfile.Name, BowlingConstant.Strike, i, strikeBonus);
                            }
                        }
                        else
                        {
                            userProfile.TenFramesScore[i] += nextFrameScore;
                            GenerateBonusInform(userProfile.Name, BowlingConstant.Strike, i, nextFrameScore);
                        }
                    }
                    else
                    {
                        userProfile.TenFramesScore[i] += userProfile.TenFramesLog[i + 1][0];
                        GenerateBonusInform(userProfile.Name, BowlingConstant.Spare, i,
                            userProfile.TenFramesLog[i + 1][0]);
                    }
                }
                else
                {
                    if (throw1 == BowlingConstant.MaximumPointPerFrame)
                    {
                        var throw3 = userProfile.TenFramesLog[i][2];
                        userProfile.TenFramesScore[i] += throw3;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(
                            $"{userProfile.Name} got STRIKE at Frame {i} with bonus {throw2 + throw3} points");
                        Console.ResetColor();
                    }
                    else if (frameScore == 10) // spare in the first two throws of the 10th frame
                    {
                        var throw3 = userProfile.TenFramesLog[i][2];
                        userProfile.TenFramesScore[i] += throw3;
                        GenerateBonusInform(userProfile.Name, BowlingConstant.Spare, i, throw3);
                    }
                }
            }

            var totalScore = userProfile.TenFramesScore.Sum();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Good game {userProfile.Name}! Your score : {totalScore}");
            Console.ResetColor();
        }

        private void GenerateBonusInform(string playerName, string bonus, int index, int points)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{playerName} got {bonus.ToUpper()} at Frame {index + 1} with bonus {points} points");
            Console.ResetColor();
        }
    }
}