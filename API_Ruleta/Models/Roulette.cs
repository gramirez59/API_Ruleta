using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Ruleta.Models
{
    public class Roulette
    {
        public int Id { get; set; }
        public string OpenRoulette { get; set; }
        public DateTime DateTimeOpening { get; set; }
        public DateTime DateTimeClosing { get; set; }
    }
}
