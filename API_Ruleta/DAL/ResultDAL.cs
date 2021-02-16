using API_Ruleta.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Ruleta.DAL
{
    public class ResultDAL
    {
        private readonly string _conexion = RouletteConst.DATA_SOURCE;
        public int CreateResult(Result result)
        {
            int LastRowID = 0;
            using (SqliteConnection conexion = new SqliteConnection(this._conexion))
            {
                SqliteCommand command = new SqliteCommand("INSERT INTO Result(IdRoulette,Number,DateTimeResult) VALUES(@IdRoulette, @Number, @DateTimeResult)", conexion);
                conexion.Open();
                try
                {
                    command.Parameters.AddWithValue("IdRoulette", result.IdRoulette);
                    command.Parameters.AddWithValue("Number", result.Number);
                    command.Parameters.AddWithValue("DateTimeResult", result.DateTimeResult);

                    int res = command.ExecuteNonQuery();
                    if (res == 1)
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
    }
}
