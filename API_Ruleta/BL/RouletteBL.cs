using API_Ruleta.DAL;
using API_Ruleta.DTO;
using API_Ruleta.Enum;
using API_Ruleta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Ruleta.BL
{
    public class RouletteBL
    {
        public int CreateRoulette()
        {
            RouletteDAL rouleteDAL = new RouletteDAL();
            return rouleteDAL.CreateRoulette();
        }

        public string OpenRoulette(int id)
        {
            RouletteDAL rouleteDAL = new RouletteDAL();
            Roulette roulette = rouleteDAL.GetRouletteById(id);
            if (roulette != null && roulette.OpenRoulette.Equals(RouletteConst.OPEN))
                return RouletteConst.ROULETTE_OPEN1 + id + RouletteConst.ROULETTE_OPEN2;
            bool open = rouleteDAL.OpenRoulette(id);
            if (open)
                return RouletteConst.ROULETTE_OPEN_SUCCESSFUL1 + id + RouletteConst.ROULETTE_OPEN_SUCCESSFUL2;
            else
                return RouletteConst.ROULETTE_OPEN_ERROR1 + id + RouletteConst.ROULETTE_OPEN_ERROR2;
        }

        public List<Roulette> GetRoulettes()
        {
            RouletteDAL rouleteDAL = new RouletteDAL();
            return rouleteDAL.GetRoulettes();
        }

        public string ToBet(int id, Bet bet)
        {
            string response = string.Empty;
            string resultValidate = ValidateBet(bet);
            if (!String.IsNullOrEmpty(resultValidate))
                response = resultValidate;
            else
            {
                BetDAL betDAL = new BetDAL();
                int idApuesta = betDAL.CreateBet(bet);
                if (idApuesta > 0)
                    response = RouletteConst.BET_SUCCESSFUL + idApuesta;
            }
            return response;
        }

        public BetResultsResponse CloseBet(int id)
        {
            BetResultsResponse betResultsResponse = new BetResultsResponse();
            List<BetResults> betResultsLits = new List<BetResults>();
            if (!ValidateCloseRoulette(id))
            {
                betResultsResponse.Bets = betResultsLits;
                betResultsResponse.Message = RouletteConst.ROULETTE_CLOSED_OR_NO_EXIST;
                return betResultsResponse;
            }                
            RouletteDAL rouletteDAL = new RouletteDAL();
            ResultDAL resultDAL = new ResultDAL();
            BetDAL betDAL = new BetDAL();
            rouletteDAL.CloseRoulette(id);
            Result result = new Result();
            result.IdRoulette = id;
            result.Number = GetWinningNumber();
            result.DateTimeResult = DateTime.Now;
            resultDAL.CreateResult(result);
            List<Bet> betList = betDAL.GetBetsByIdRouletteAndDateTime(rouletteDAL.GetRouletteById(id));
            
            foreach (var bet in betList)
            {
                BetResults betResults = new BetResults();
                betResults.Id = bet.Id;
                betResults.Number = bet.Number;
                betResults.Color = bet.Color;
                betResults.Value = bet.Value;
                betResults.BetType = bet.BetType;
                betResults.DateTimeBet = bet.DateTimeBet;
                betResults.WinningNumber = result.Number;
                if (bet.BetType == (int)BetType.Number && bet.Number == result.Number)
                {
                    betResults.AmountEarned = bet.Value * 5;
                    betResults.State = RouletteConst.WON;
                }
                if (bet.BetType == (int)BetType.Number && bet.Number != result.Number)
                {
                    betResults.AmountEarned = 0;
                    betResults.State = RouletteConst.LOST;
                }
                if(bet.BetType == (int)BetType.Color && bet.Color.Equals(RouletteConst.RED_COLOR) && (result.Number % 2) == 0)
                {
                    betResults.AmountEarned = (int)(bet.Value * 1.8F);
                    betResults.State = RouletteConst.WON;
                }
                if (bet.BetType == (int)BetType.Color && bet.Color.Equals(RouletteConst.RED_COLOR) && (result.Number % 2) != 0)
                {
                    betResults.AmountEarned = 0;
                    betResults.State = RouletteConst.LOST;
                }
                if (bet.BetType == (int)BetType.Color && bet.Color.Equals(RouletteConst.BLACK_COLOR) && (result.Number % 2) != 0)
                {
                    betResults.AmountEarned = (int)(bet.Value * 1.8F);
                    betResults.State = RouletteConst.WON;
                }
                if (bet.BetType == (int)BetType.Color && bet.Color.Equals(RouletteConst.BLACK_COLOR) && (result.Number % 2) == 0)
                {
                    betResults.AmountEarned = 0;
                    betResults.State = RouletteConst.LOST;
                }
                betResultsLits.Add(betResults);
            }
            betResultsResponse.Bets = betResultsLits;
            betResultsResponse.Message = RouletteConst.BET_CLOSED_SUCCESSFUL;

            return betResultsResponse;
        }

        private int GetWinningNumber()
        {
            Random random = new Random();
            return random.Next(RouletteConst.BET_MIN_NUMBERT, RouletteConst.BET_MAX_NUMBERT);
        }

        private bool ValidateCloseRoulette(int id)
        {
            bool validate = true;
            RouletteDAL rouletteDAL = new RouletteDAL();
            Roulette roulette = rouletteDAL.GetRouletteById(id);
            if (roulette == null)
                return false;
            if (roulette.OpenRoulette.Equals(RouletteConst.CLOSED))
                return false;
            return validate;
        }

        private string ValidateBet(Bet bet)
        {
            string response = string.Empty;
            RouletteDAL rouletteDAL = new RouletteDAL();
            if (bet.BetType != (int)BetType.Number && bet.BetType != (int)BetType.Color)
                return RouletteConst.BET_TYPE_INVALID;
            if ((bet.BetType == (int)BetType.Number) && (bet.Number < RouletteConst.BET_MIN_NUMBERT || bet.Number > RouletteConst.BET_MAX_NUMBERT))
                return RouletteConst.BET_NUMBER_INVALID;
            if ((bet.BetType == (int)BetType.Color) && (!bet.Color.Equals(RouletteConst.BLACK_COLOR) && !bet.Color.Equals(RouletteConst.RED_COLOR)))
                return RouletteConst.BET_COLOR_INVALID;
            if (bet.Value < RouletteConst.BET_MIN_VALUE || bet.Value > RouletteConst.BET_MAX_VALUE)
                return RouletteConst.BET_VALUE_INVALID;
            Roulette roulette = rouletteDAL.GetRouletteById(bet.IdRoulette);
            if (roulette == null)
                return RouletteConst.ROULETTE_NO_EXIST;
            if (roulette.OpenRoulette.Equals(RouletteConst.CLOSED))
                return RouletteConst.ROULETTE_CLOSED;
            return response;
        }
    }
}
