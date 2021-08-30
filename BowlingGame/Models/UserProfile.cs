using System.Collections.Generic;

namespace BowlingGame.Models
{
    public class UserProfile
    {
        public string Name { get; set; }
        public List<int> TenFramesScore { get; set; }
        public List<int[]> TenFramesLog { get; set; }
    }
}