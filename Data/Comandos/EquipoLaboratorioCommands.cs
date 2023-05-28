using Modelos.Laboratorio;
using Npgsql;
using NpgsqlTypes;

namespace Data.Comandos
{
    public class EquipoLaboratorioCommands
    {
        public static MEquipoLaboratorio GetEquiposLaboratorioById(string id)
        {
            MEquipoLaboratorio data = null;
            try
            {
                using (var comando = new NpgsqlCommand("select * from tb_equipo_laboratorio where id_equipo=@1;"))
                {
                    comando.Parameters.Add("@1", NpgsqlDbType.Varchar).Value = id;
                    using (var reader = PgCommands.SeleccionarDatosReader(comando))
                    {
                        if (reader.Read())
                        {
                            data = new MEquipoLaboratorio
                            {
                                IdEquipo = reader["id_equipo"].ToString(),
                                Nombre = reader["nombre"].ToString(),
                                Estado = Convert.ToBoolean(reader["estado"]),
                                Puerto = Convert.ToInt32(reader["puerto"])
                            };
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
        public static IEnumerable<MEquipoLaboratorio> GetEquiposLaboratorio()
        {
            var data = new List<MEquipoLaboratorio>();
            try
            {
                using (var comando = new NpgsqlCommand("select * from tb_equipo_laboratorio order by 1;"))
                {
                    using (var reader = PgCommands.SeleccionarDatosReader(comando))
                    {
                        while (reader.Read())
                        {
                            data.Add(new MEquipoLaboratorio
                            {
                                IdEquipo = reader["id_equipo"].ToString(),
                                Nombre = reader["nombre"].ToString(),
                                Estado = Convert.ToBoolean(reader["estado"]),
                                Puerto = Convert.ToInt32(reader["puerto"])
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
        public static object[] SaveEquipoLaboratorio(MEquipoLaboratorio unidad, IEnumerable<MDetalleEquipoLaboratorio> codigos, bool isNew)
        {
            var data = new object[2];
            try
            {
                if (isNew)
                {
                    using (var read = PgCommands.SeleccionarDatosReader(new NpgsqlCommand("select fx_contador('tb_equipo_laboratorio');")))
                    {
                        if (read.Read()) unidad.IdEquipo = read[0].ToString();
                    }
                }

                var lista = new List<NpgsqlCommand> { SaveOrUpdateEquipoLaboratorio(unidad, isNew) };
                if (isNew) lista.Add(PgCommands.UpdateContador("tb_equipo_laboratorio"));

                foreach (var codigo in codigos)
                {
                    if (codigo.IdDetalle.Equals("AUTO"))
                    {
                        codigo.IdDetalle = PgCommands.GetIdAutoInc("tb_detalle_equipo_laboratorio");
                        lista.Add(SaveOrUpdateDetalleEquipoLaboratorio(codigo, true));
                    }
                    else lista.Add(SaveOrUpdateDetalleEquipoLaboratorio(codigo, false));
                }

                data = PgCommands.EjecutarTransaccion(lista, null, false);
            }
            catch (Exception ex)
            {
                data[0] = 1;
                data[1] = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return data;
        }
        private static NpgsqlCommand SaveOrUpdateEquipoLaboratorio(MEquipoLaboratorio unidad, bool isNew)
        {
            NpgsqlCommand command = null;
            try
            {
                command = new NpgsqlCommand
                {
                    CommandText = isNew ? "INSERT INTO tb_equipo_laboratorio (id_equipo, nombre, estado, puerto) VALUES (@1, @2, @3, @4);" :
                    "UPDATE tb_equipo_laboratorio set nombre=@2, estado=@3, puerto=@4 where id_equipo=@1;"
                };
                command.Parameters.Add("@1", NpgsqlDbType.Varchar).Value = unidad.IdEquipo;
                command.Parameters.Add("@2", NpgsqlDbType.Varchar).Value = unidad.Nombre;
                command.Parameters.Add("@3", NpgsqlDbType.Boolean).Value = unidad.Estado;
                command.Parameters.Add("@4", NpgsqlDbType.Integer).Value = unidad.Puerto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return command;
        }
        public static NpgsqlCommand SaveOrUpdateDetalleEquipoLaboratorio(MDetalleEquipoLaboratorio examen, bool isNew)
        {
            NpgsqlCommand command = null;
            try
            {
                command = new NpgsqlCommand
                {
                    CommandText = isNew ? "insert into tb_detalle_equipo_laboratorio values (@id_equipo, @codigo_lis, @id_detalle);" :
                    "update tb_detalle_equipo_laboratorio set id_equipo=@id_equipo, codigo_lis=@codigo_lis where id_detalle=@id_detalle;"
                };

                command.Parameters.Add("@id_detalle", NpgsqlDbType.Varchar).Value = examen.IdDetalle;
                command.Parameters.Add("@id_equipo", NpgsqlDbType.Varchar).Value = examen.Equipo.IdEquipo;
                command.Parameters.Add("@codigo_lis", NpgsqlDbType.Varchar).Value = examen.CodigoLis;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return command;
        }
    }
}
