using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Ruleta.BL;
using API_Ruleta.DTO;
using API_Ruleta.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Ruleta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        [HttpPost("CreateRoulette")]
        public int CreateRoulette()
        {
            RouletteBL rouletteBL = new RouletteBL();
            return rouletteBL.CreateRoulette();
        }

        [HttpGet("OpenRoulette/{id}", Name = "OpenRoulette")]
        public String OpenRoulette(int id)
        {
            RouletteBL rouletteBL = new RouletteBL();
            return rouletteBL.OpenRoulette(id);
            
        }

        [HttpPost("ToBet")]
        public string ToBet([FromHeader] int id, [FromBody] Bet bet)
        {
            RouletteBL rouletteBL = new RouletteBL();
            return rouletteBL.ToBet(id, bet);
        }

        [HttpGet("CloseBet/{id}", Name = "CloseBet")]
        public BetResultsResponse CloseBet(int id)
        {
            RouletteBL rouletteBL = new RouletteBL();
            return rouletteBL.CloseBet(id);
        }

        [HttpGet("GetRoulettes", Name = "GetRoulettes")]
        public List<Roulette> GetRoulettes()
        {
            RouletteBL rouletteBL = new RouletteBL();
            return rouletteBL.GetRoulettes();
        }
    }
}
