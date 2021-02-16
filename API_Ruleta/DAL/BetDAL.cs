using API_Ruleta.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Ruleta.DAL
{
    public class BetDAL
    {
        private readonly string _conexion = RouletteConst.DATA_SOURCE;
        public int CreateBet(Bet bet)
        {
            int LastRowID = 0;
            using (SqliteConnection conexion = new SqliteConnection(this._conexion))
            {
                DateTime dateNow = DateTime.Now;
                SqliteCommand command = new SqliteCommand("INSERT INTO Bet(IdRoulette,Number,Color,Value,BetType,DateTimeBet) VALUES(@IdRoulette, @Number, @Color, @Value, @BetType, @DateTimeBet)", conexion);
                conexion.Open();
                try
                {
                    command.Parameters.AddWithValue("IdRoulette", bet.IdRoulette);
                    command.Parameters.AddWithValue("Number", bet.Number);
                    command.Parameters.AddWithValue("Color", bet.Color);
                    command.Parameters.AddWithValue("Value", bet.Value);
                    command.Parameters.AddWithValue("BetType", bet.BetType);
                    command.Parameters.AddWithValue("DateTimeBet", dateNow);

                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        command.CommandText = "select last_insert_rowid()";
                        Int64 LastRowID64 = (Int64)command.ExecuteScalar();
                        LastRowID = (int)LastRowID64;
                    }

                    return LastRowID;
                }
                catch (Exception ex)
                {
                    return LastRowID;
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        public List<Bet> GetBetsByIdRouletteAndDateTime(Roulette roulette)
        {
            List<Bet> betList = new List<Bet>();
            using (SqliteConnection conexion = new SqliteConnection(this._conexion))
            {
                try
                {
                    conexion.Open();
                    SqliteCommand command = new SqliteCommand("SELECT * FROM Bet WHERE IdRoulette=@IdRoulette AND DateTimeBet BETWEEN @DateTimeOpening AND @DateTimeClosing", conexion);
                    command.Parameters.AddWithValue("IdRoulette", roulette.Id);
                    command.Parameters.AddWithValue("DateTimeOpening", roulette.DateTimeOpening);
                    command.Parameters.AddWithValue("DateTimeClosing", roulette.DateTimeClosing);
                    using (IDataReader rd = command.ExecuteReader())
                    {
                        if (((SqliteDataReader)rd).HasRows)
                        {
                            while (rd.Read())
                            {
                                Bet bet = new Bet();
                                bet.Id = int.Parse(rd["Id"].ToString());
                                bet.IdRoulette = int.Parse(rd["IdRoulette"].ToString());
                                bet.Number = int.Parse(rd["Number"].ToString());
                                bet.Color = (rd["Color"].ToString());
                                bet.Value = int.Parse(rd["Value"].ToString());
                                bet.BetType = int.Parse(rd["BetType"].ToString());
                                if (rd["DateTimeBet"] != DBNull.Value)
                                    bet.DateTimeBet = DateTime.Parse(rd["DateTimeBet"].ToString());
                                betList.Add(bet);
                            }
                        }
                        return betList;
                    }
                }
                catch (Exception ex)
                {
                    return betList;
                }
                finally
                {
                    conexion.Close();
                }
            }
        }
    }
}
