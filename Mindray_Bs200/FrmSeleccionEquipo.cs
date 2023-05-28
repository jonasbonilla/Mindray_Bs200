using Controller.Negocio;
using Mindray_Bs200.Formularios;
using Modelos.Laboratorio;

namespace Mindray_Bs200
{
    public partial class FrmSeleccionEquipo : Form
    {
        public FrmSeleccionEquipo()
        {
            InitializeComponent();
            ListarEquipos();
        }

        private void _btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListarEquipos();
        }

        private void ListarEquipos()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var equipos = NEquipoLaboratorio.GetEquiposLaboratorio();
                foreach (var equipo in equipos) dataGridView1.Rows.Add(equipo.IdEquipo, equipo.Nombre, equipo.Puerto);
                dataGridView1.AutoResizeRows();
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ocurrió un problema al cargar los equipos registrados: " + ex.Message, @"ERROR DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Iniciar();
        }

        private void _btnChangePort_Click(object sender, EventArgs e)
        {
            Iniciar();
        }

        private void Iniciar()
        {
            if (dataGridView1.RowCount == 0 || dataGridView1.CurrentRow == null) return;
            var idEquipo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString().Substring(3, dataGridView1.CurrentRow.Cells[0].Value.ToString().Length - 3));
            var port = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
            new FrmMain(idEquipo, port).Show();
            Hide();
        }
    }
}