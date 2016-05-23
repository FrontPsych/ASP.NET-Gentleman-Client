using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Aggregates
{
    public class GameStatistic
    {
        public Game Game { get; set; }
        public int NumberOfPlayers { get; set; } 
        public int Position { get; set; } //User Game ROw
        public int Score { get; set; } // User Game Row
        public TimeSpan DurationTimeSpan { get; set; } //Game
    }
}
