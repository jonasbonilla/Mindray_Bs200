using Npgsql;
using NpgsqlTypes;
using System.Data; 

namespace Data.Comandos
{
    public static class PgCommands
    {
        private static readonly PgConnection PgConexion = new PgConnection();
        public static void CloseConnections()
        {
            PgConexion.CloseNpgsqlConnection();
        }
        public static string GetIdAutoInc(string name)
        {
            string code = null;
            try
            {
                using (var sql = new NpgsqlCommand("select fx_contador_auto('" + name + "')"))
                {
                    sql.Connection = PgConexion.OpenNpgsqlConnection();
                    var data = sql.ExecuteReader();
                    if (data.Read()) code = data[0].ToString();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return code;
        }
        public static NpgsqlDataReader SeleccionarDatosReader(NpgsqlCommand sql)
        {
            NpgsqlDataReader data = null;
            try
            {
                using (sql)
                {
                    sql.Connection = PgConexion.OpenNpgsqlConnection();
                    data = sql.ExecuteReader();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return data;
        } 
        public static DataTable SeleccionarDatosTable(NpgsqlCommand sql)
        {
            var tb = new DataTable();
            try
            {
                using (sql)
                {
                    sql.Connection = PgConexion.OpenNpgsqlConnection();
                    using (var adapter = new NpgsqlDataAdapter(sql))
                    {
                        adapter.Fill(tb);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tb;
        }
        public static List<object> SeleccionarDatosColumna(NpgsqlCommand sql)
        {
            var data = new List<object>();
            try
            {
                using (sql)
                {
                    sql.Connection = PgConexion.OpenNpgsqlConnection();
                    using (var reader = sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(reader.GetValue(0));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return data;
        }
        public static NpgsqlCommand UpdateContador(string table)
        {
            NpgsqlCommand command = null;
            try
            {
                command = new NpgsqlCommand("update tb_contador set contador_cont=contador_cont+1 where tabla_cont=@1;");
                command.Parameters.Add("@1", NpgsqlDbType.Varchar).Value = table;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return command;
        }
        public static string GetNowServidor()
        {
            string dato = null;
            const string sentence = "SELECT SUBSTRING((NOW()||'') FROM 1 FOR 19)";
            try
            {
                using (var cmd = new NpgsqlCommand(sentence, PgConexion.OpenNpgsqlConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.GetValue(0) != DBNull.Value)
                            {
                                dato = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return dato;
        }
        public static string GetFechaServidor()
        {
            string dato = null;
            const string sentence = "SELECT SUBSTRING((NOW()||'') FROM 1 FOR 10)";
            try
            {
                using (var cmd = new NpgsqlCommand(sentence, PgConexion.OpenNpgsqlConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.GetValue(0) != DBNull.Value)
                            {
                                dato = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return dato;
        }
        public static string GetHoraServidor()
        {
            string dato = null;
            const string sentence = "SELECT SUBSTRING((NOW()||'') FROM 12 FOR 8)";
            try
            {
                using (var cmd = new NpgsqlCommand(sentence, PgConexion.OpenNpgsqlConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.GetValue(0) != DBNull.Value)
                            {
                                dato = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return dato;
        }
        public static string GetTimeStampServidor()
        {
            string dato = null;
            const string sentence = "SELECT SUBSTRING((NOW()||'') FROM 1 FOR 19)";
            try
            {
                using (var cmd = new NpgsqlCommand(sentence, PgConexion.OpenNpgsqlConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            if (reader.GetValue(0) != DBNull.Value)
                                dato = reader.GetString(0);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return dato;
        }
        public static object[] EjecutarTransaccion(IEnumerable<NpgsqlCommand> sqlList, string audit, bool printQuerys)
        {
            var result = new object[2];
            NpgsqlTransaction trans = null;
            try
            {
                var con = PgConexion.OpenNpgsqlConnection();
                trans = con.BeginTransaction();

                foreach (var sqlcmd in sqlList)
                {
                    if (printQuerys) Console.WriteLine(string.Concat("EjecutarTransaccion: ", sqlcmd.CommandText));
                    sqlcmd.Connection = con;
                    sqlcmd.Transaction = trans;
                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.Dispose();
                }

                result[0] = 0;
                result[1] = "Transacción procesada correctamente.";

                trans.Commit();
            }
            catch (Exception ex)
            {
                result[0] = 1;
                result[1] = "Error al procesar la transacción ex: " + ex.Message;

                if (trans == null) return result;
                try
                {
                    trans.Rollback();
                }
                catch (Exception rb)
                {
                    result[0] = 1;
                    result[1] = "Error al procesar la transacción rb: " + rb.Message;
                }
            }
            return result;
        }
    }
}
