using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Ruleta.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int IdRoulette { get; set; }
        public int Number { get; set; }
        public DateTime DateTimeResult { get; set; }
    }
}
