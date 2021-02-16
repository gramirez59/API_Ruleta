using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Ruleta
{
    public class RouletteConst
    {
        public const string DATA_SOURCE = "Data Source=Data\\Roulette.sqlite;";
        public const int BET_MIN_NUMBERT = 0;
        public const int BET_MAX_NUMBERT = 36;
        public const int BET_MIN_VALUE = 0;
        public const int BET_MAX_VALUE = 10000;
        public const string BLACK_COLOR = "negro";
        public const string RED_COLOR = "rojo";
        public const string OPEN = "Open";
        public const string CLOSED = "Closed";
        public const string BET_TYPE_INVALID = "El tipo de apuesta no es valido";
        public const string BET_NUMBER_INVALID = "El número ingresado no es valido para apostar";
        public const string BET_COLOR_INVALID = "El color a apostar no es valido";
        public const string BET_VALUE_INVALID = "El valor a apostar no es valido";
        public const string ROULETTE_NO_EXIST = "La ruleta en la cual desea apostar no existe";
        public const string ROULETTE_CLOSED = "la ruleta en la cual desea apostar está cerrada";
        public const string ROULETTE_OPEN1 = "La Ruleta con Id ";
        public const string ROULETTE_OPEN2 = " ya está abierta";
        public const string ROULETTE_OPEN_SUCCESSFUL1 = "Ruleta ";
        public const string ROULETTE_OPEN_SUCCESSFUL2 = " abierta exitosamente";
        public const string ROULETTE_OPEN_ERROR1 = "La ruleta ";
        public const string ROULETTE_OPEN_ERROR2 = " no se pudo abrir";
        public const string BET_SUCCESSFUL = "Se creó exitosamente la apuesta con Id ";
        public const string ROULETTE_CLOSED_OR_NO_EXIST = "La ruleta ya está cerrada o no existe";
        public const string BET_CLOSED_SUCCESSFUL = "Cierre de apuestas exitoso";
        public const string WON = "Ganó";
        public const string LOST = "Perdió";
    }
}
