using BowlingGame.Models;

namespace BowlingGame.Services.Contract
{
    public interface IRandomRollService
    {
        void RollTheBall(UserProfile userProfile);
    }
}
