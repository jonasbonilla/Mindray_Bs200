using Modelos.Laboratorio;
using Npgsql;
using NpgsqlTypes;

namespace Data.Comandos
{
    public class ResultadoEquipoCommands
    {
        public static IEnumerable<MResultadoEquipo> GetResultadosObtenidosByFecha(DateTime fecha)
        {
            var data = new List<MResultadoEquipo>();
            try
            {
                var ini = fecha.AddDays(-30);
                var fin = fecha;
                using (var comando = new NpgsqlCommand("select r.id_orden, (select max(c.fecha_hora) from tb_resultados_equipo c where c.estado ='A' and c.id_orden=r.id_orden) fecha_hora " +
                    "from tb_resultados_equipo r where r.estado = 'A' and length(r.id_orden) > 1 and r.fecha_hora >= @desde::timestamp and r.fecha_hora <= @hasta::timestamp group by 1 order by 1;"))
                {
                    comando.Parameters.Add("@desde", NpgsqlDbType.Varchar).Value = ini.ToShortDateString() + " 00:00:00";
                    comando.Parameters.Add("@hasta", NpgsqlDbType.Varchar).Value = fin.ToShortDateString() + " 23:59:59";

                    using (var reader = PgCommands.SeleccionarDatosReader(comando))
                    {
                        while (reader.Read())
                        {
                            data.Add(new MResultadoEquipo
                            {
                                FechaHora = Convert.ToDateTime(reader["fecha_hora"].ToString()),
                                OrdenTurno = reader["id_orden"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return data;
        }
        public static IEnumerable<MResultadoEquipo> GetResultadosOrdenByNumOrden(string numOrden)
        {
            var data = new List<MResultadoEquipo>();
            try
            {
                using (var comando = new NpgsqlCommand("select * from tb_resultados_equipo where id_orden=@id and estado='A';"))
                {
                    comando.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = numOrden;

                    using (var reader = PgCommands.SeleccionarDatosReader(comando))
                    {
                        while (reader.Read())
                        {
                            data.Add(new MResultadoEquipo
                            {
                                Secuencial = reader["secuencial"].ToString(),
                                IdEquipo = reader["id_equipo"].ToString(),
                                FechaHora = Convert.ToDateTime(reader["fecha_hora"].ToString()),
                                OrdenTurno = reader["id_orden"].ToString(),
                                Abreviatura = reader["abreviatura"].ToString(),
                                Estado = reader["estado"].ToString(),
                                Resultado = reader["resultado"].ToString(),
                                Error = reader["error"].ToString(),
                                Convertido = reader["convertido"].ToString(),
                                Registro = Convert.ToDateTime(reader["registro"].ToString()),
                                Data = reader["data"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return data;
        }
        public static string SaveResultadoEquipo(IEnumerable<MResultadoEquipo> resultados)
        {
            string data;
            var lista = new List<NpgsqlCommand>();
            try
            {
                lista.AddRange(resultados.Select(SaveOrUpdateResultadoEquipo));
                data = PgCommands.EjecutarTransaccion(lista, null, false)[1].ToString();
            }
            catch (Exception ex)
            {
                data = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return data;
        }
        private static NpgsqlCommand SaveOrUpdateResultadoEquipo(MResultadoEquipo resultado)
        {
            NpgsqlCommand command = null;
            try
            {
                command = new NpgsqlCommand
                {
                    CommandText = "INSERT INTO tb_resultados_equipo (id_equipo,fecha_hora,id_orden,abreviatura,estado,resultado,error,convertido) " +
                                  "VALUES (@id_equipo,@fecha_hora,@id_orden,@abreviatura,@estado,@resultado,@error,@convertido);"
                };
                command.Parameters.Add("@id_equipo", NpgsqlDbType.Varchar).Value = resultado.IdEquipo;
                command.Parameters.Add("@fecha_hora", NpgsqlDbType.Timestamp).Value = resultado.FechaHora;
                command.Parameters.Add("@id_orden", NpgsqlDbType.Varchar).Value = resultado.OrdenTurno;
                command.Parameters.Add("@abreviatura", NpgsqlDbType.Varchar).Value = resultado.Abreviatura;
                command.Parameters.Add("@estado", NpgsqlDbType.Varchar).Value = resultado.Estado;
                command.Parameters.Add("@resultado", NpgsqlDbType.Varchar).Value = resultado.Resultado;
                command.Parameters.Add("@error", NpgsqlDbType.Varchar).Value = resultado.Error;
                command.Parameters.Add("@convertido", NpgsqlDbType.Varchar).Value = resultado.Convertido;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return command;
        }
    }
}
