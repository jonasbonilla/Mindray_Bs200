using Modelos.Laboratorio;
using Npgsql;
using NpgsqlTypes;

namespace Data.Comandos
{
    public class DetalleEquipoLaboratorioCommands
    {
        public static IEnumerable<MDetalleEquipoLaboratorio> GetDetalleEquipoLaboratorio()
        {
            var data = new List<MDetalleEquipoLaboratorio>();
            try
            {
                using (var comando = new NpgsqlCommand("select * from tb_detalle_equipo_laboratorio order by 1, 2;"))
                {
                    using (var reader = PgCommands.SeleccionarDatosReader(comando))
                    {
                        while (reader.Read())
                        {
                            data.Add(new MDetalleEquipoLaboratorio
                            {
                                Equipo = EquipoLaboratorioCommands.GetEquiposLaboratorioById(reader["id_equipo"].ToString()),
                                IdDetalle = reader["id_detalle"].ToString(),
                                CodigoLis = reader["codigo_lis"].ToString()
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
        public static IEnumerable<MDetalleEquipoLaboratorio> GetDetalleEquipoLaboratorioByIdEquipo(string idp)
        {
            var data = new List<MDetalleEquipoLaboratorio>();
            try
            {
                using (var comando = new NpgsqlCommand("select * from tb_detalle_equipo_laboratorio where id_equipo = @1;"))
                {
                    comando.Parameters.Add("@1", NpgsqlDbType.Varchar).Value = idp;
                    using (var reader = PgCommands.SeleccionarDatosReader(comando))
                    {
                        while (reader.Read())
                        {
                            data.Add(new MDetalleEquipoLaboratorio
                            {
                                Equipo = EquipoLaboratorioCommands.GetEquiposLaboratorioById(reader["id_equipo"].ToString()),
                                IdDetalle = reader["id_detalle"].ToString(),
                                CodigoLis = reader["codigo_lis"].ToString()
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

        public static string GetDetalleEquipoLaboratorioByIdDeta(string idp)
        {
            string data = null;
            try
            {
                using (
                    var comando = new NpgsqlCommand("select codigo_lis from tb_detalle_equipo_laboratorio where id_detalle = @1 limit 1;")
                    )
                {
                    comando.Parameters.Add("@1", NpgsqlDbType.Varchar).Value = idp;
                    using (var reader = PgCommands.SeleccionarDatosReader(comando))
                    {
                        if (reader.Read())
                        {
                            data = reader["codigo_lis"]?.ToString();
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

        public static object[] EliminarDetalleEquipoLaboratoiro(MDetalleEquipoLaboratorio examen)
        {
            var data = new object[2];
            try
            {
                data = PgCommands.EjecutarTransaccion(new List<NpgsqlCommand> { DeleteDetalleEquipoLaboratoiro(examen) }, null, false);
            }
            catch (Exception ex)
            {
                data[0] = 1;
                data[1] = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return data;
        }
        public static NpgsqlCommand DeleteDetalleEquipoLaboratoiro(MDetalleEquipoLaboratorio examen)
        {
            NpgsqlCommand command = null;
            try
            {
                command = new NpgsqlCommand
                {
                    CommandText = "delete from tb_detalle_equipo_laboratorio where id_detalle = @ide;"
                };
                command.Parameters.Add("@ide", NpgsqlDbType.Varchar).Value = examen.IdDetalle;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return command;
        }
    }
}
