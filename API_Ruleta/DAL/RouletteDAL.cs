using API_Ruleta.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Ruleta.DAL
{
    public class RouletteDAL
    {
        private readonly string _conexion = RouletteConst.DATA_SOURCE;
        public int CreateRoulette()
        {
            int LastRowID = 0;
            using (SqliteConnection conexion = new SqliteConnection(this._conexion))
            {
                SqliteCommand command = new SqliteCommand("INSERT INTO Roulette(Id,OpenRoulette) VALUES(NULL,0)", conexion);
                conexion.Open();
                try
                {
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

        public bool OpenRoulette(int id)
        {
            Boolean openRoulette = false;
            using (SqliteConnection conexion = new SqliteConnection(this._conexion))
            {
                DateTime dateNow = DateTime.Now;
                SqliteCommand command = new SqliteCommand("UPDATE Roulette SET OpenRoulette = 1, DateTimeOpening = @DateTimeOpening WHERE Id=" + id, conexion);
                conexion.Open();
                try
                {
                    command.Parameters.AddWithValue("DateTimeOpening", dateNow);
                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        openRoulette = true;
                    }

                    return openRoulette;
                }
                catch (Exception ex)
                {
                    return openRoulette;
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        public bool CloseRoulette(int id)
        {
            Boolean openRoulette = false;
            using (SqliteConnection conexion = new SqliteConnection(this._conexion))
            {
                DateTime dateNow = DateTime.Now;
                SqliteCommand command = new SqliteCommand("UPDATE Roulette SET OpenRoulette = 0, DateTimeClosing = @DateTimeClosing WHERE Id=" + id, conexion);
                conexion.Open();
                try
                {
                    command.Parameters.AddWithValue("DateTimeClosing", dateNow);
                    int result = command.ExecuteNonQuery();
                    if (result == 1)
                    {
                        openRoulette = true;
                    }

                    return openRoulette;
                }
                catch (Exception ex)
                {
                    return openRoulette;
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        public List<Roulette> GetRoulettes()
        {
            List<Roulette> rouletteList = new List<Roulette>();
            using (SqliteConnection conexion = new SqliteConnection(this._conexion))
            {
                string query = "SELECT * FROM Roulette";

                try
                {
                    conexion.Open();
                    using (IDataReader rd = new SqliteCommand(query, conexion).ExecuteReader())
                    {
                        // Consulta la cantidad de registros afectados en la consulta.
                        if (((SqliteDataReader)rd).HasRows)
                        {
                            while (rd.Read())
                            {
                                Roulette roulette = new Roulette();
                                roulette.Id = int.Parse(rd["Id"].ToString());
                                int rouletteState = int.Parse(rd["OpenRoulette"].ToString());
                                if (rouletteState == 1)
                                    roulette.OpenRoulette = RouletteConst.OPEN;
                                else
                                    roulette.OpenRoulette = RouletteConst.CLOSED;
                                if (rd["DateTimeOpening"] != DBNull.Value)
                                    roulette.DateTimeOpening = DateTime.Parse(rd["DateTimeOpening"].ToString());
                                if (rd["DateTimeClosing"] != DBNull.Value)
                                    roulette.DateTimeClosing = DateTime.Parse(rd["DateTimeClosing"].ToString());
                                rouletteList.Add(roulette);
                            }
                        }
                        return rouletteList;
                    }
                }
                catch (Exception ex)
                {
                    return rouletteList;
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        public Roulette GetRouletteById(int id)
        {
            Roulette roulette = null;
            using (SqliteConnection conexion = new SqliteConnection(this._conexion))
            {
                string query = "SELECT * FROM Roulette WHERE Id=" + id;
                try
                {
                    conexion.Open();
                    using (IDataReader rd = new SqliteCommand(query, conexion).ExecuteReader())
                    {
                        // Consulta la cantidad de registros afectados en la consulta.
                        if (((SqliteDataReader)rd).HasRows)
                        {
                            while (rd.Read())
                            {
                                roulette = new Roulette();
                                roulette.Id = int.Parse(rd["Id"].ToString());
                                int rouletteState = int.Parse(rd["OpenRoulette"].ToString());
                                if (rouletteState == 1)
                                    roulette.OpenRoulette = RouletteConst.OPEN;
                                else
                                    roulette.OpenRoulette = RouletteConst.CLOSED;
                                if (rd["DateTimeOpening"] != DBNull.Value)
                                    roulette.DateTimeOpening = DateTime.Parse(rd["DateTimeOpening"].ToString());
                                if (rd["DateTimeClosing"] != DBNull.Value)
                                    roulette.DateTimeClosing = DateTime.Parse(rd["DateTimeClosing"].ToString());
                            }
                        }
                        return roulette;
                    }
                }
                catch (Exception ex)
                {
                    return roulette;
                }
                finally
                {
                    conexion.Close();
                }
            }
        }
    }
}
