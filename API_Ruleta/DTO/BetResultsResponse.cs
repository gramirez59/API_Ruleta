using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Ruleta.DTO
{
    public class BetResultsResponse
    {
        public List<BetResults> Bets { get; set; }
        public string Message { get; set; }
    }
}
