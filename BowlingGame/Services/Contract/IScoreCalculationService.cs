using BowlingGame.Models;

namespace BowlingGame.Services.Contract
{
    public interface IScoreCalculationService
    {
        void EndGame(UserProfile userProfile);
    }
}
