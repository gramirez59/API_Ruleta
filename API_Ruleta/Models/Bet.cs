using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Ruleta.Models
{
    public class Bet
    {
        public int Id { get; set; }
        public int IdRoulette { get; set; }
        public int Number { get; set; }
        public string Color { get; set; }
        public int Value { get; set; }
        public int BetType { get; set; }//1-Number, 2-Color
        public DateTime DateTimeBet { get; set; }
    }
}
