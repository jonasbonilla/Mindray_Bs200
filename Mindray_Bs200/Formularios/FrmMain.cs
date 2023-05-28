using Controller.Negocio;
using Mindray_Bs200.Properties;
using Modelos.Laboratorio;
using System.ComponentModel;
using System.Data;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace Mindray_Bs200.Formularios
{
    public partial class FrmMain : Form
    {
        public delegate void InvokeDelegate();

        private static readonly char END_OF_BLOCK = '\x001C';
        private static readonly char START_OF_BLOCK = '\v';

        private readonly DataTable _dtOrden = new DataTable();

        private readonly int _idEquipo;
        private readonly int _port;
        private readonly string _equipo;
        private readonly string _characterset;

        public FrmMain(int idEquipo, int port)
        {
            InitializeComponent();

            _idEquipo = idEquipo;
            _port = port;

            if (_idEquipo == 1)
            {
                pictureBox1.Image = Resources.BS_200e;
                Text = @"BS-200/E TCP - ANALIZADOR QUÍMICO";
                _equipo = "BS-200";
                _characterset = "ASCII";
            }
            if (_idEquipo == 2)
            {
                pictureBox1.Image = Resources.analizador;
                Text = @"BC-5300/E TCP - ANALIZADOR HEMATOLOGÍA";
                _equipo = "BC-5300";
                _characterset = "UNICODE";
            }

            ChangeTcpPort();
            timer1.Enabled = true;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void _btnChangePort_Click(object sender, EventArgs e)
        {
            ChangeTcpPort();
        }
        private void ChangeTcpPort()
        {
            try
            {
                OpenTcpPort();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(@"Puerto debe ser un número entero: " + ex.Message, @"Puerto no válido", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(@"Los números de puerto válidos van de 0 a 65535: " + ex.Message, @"Puerto no válido", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }
        private void OpenTcpPort()
        {
            tcpServer1.Close();
            tcpServer1.Port = Convert.ToInt32(_port);
            _txtPort.Text = tcpServer1.Port.ToString();
            tcpServer1.Open();
            DisplayTcpServerStatus();
        }
        private void DisplayTcpServerStatus()
        {
            if (tcpServer1.IsOpen)
            {
                _lblStatus.Text = @"CONECTADO";
                _lblStatus.BackColor = Color.ForestGreen;
            }
            else
            {
                _lblStatus.Text = @"NO CONECTADO";
                _lblStatus.BackColor = Color.Coral;
            }
        }
        private void _btnSend_Click(object sender, EventArgs e)
        {
            if (_txtText.Text.Trim().Length == 0) return;
            Send();
        }
        private void Send()
        {
            var str1 = _txtText.Lines.Aggregate("", (current, line) => current + line.Replace("\r", "").Replace("\n", "") + "\r\n");
            var str2 = str1.Substring(0, str1.Length - 2);
            tcpServer1.Send(str2);
            LogData(true, str2);
        }
        public void LogData(bool sent, string text)
        {
            var txtLog = _txtLog;
            txtLog.Text = txtLog.Text + "\r\n" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt") + (sent ? " ENVIADO:\r\n" : " RECIBIDO:\r\n");
            _txtLog.Text += text;
            _txtLog.Text += "\r\n";
            if (_txtLog.Lines.Length > 500)
            {
                var strArray = new string[500];
                Array.Copy(_txtLog.Lines, _txtLog.Lines.Length - 500, strArray, 0, 500);
                _txtLog.Lines = strArray;
            }
            _txtLog.SelectionStart = _txtLog.Text.Length;
            _txtLog.ScrollToCaret();
        }
        private void SendAck(string fecha, string messageType, string textInformation, string messageId, string errorCode)
        {
            var strHl7 = new StringBuilder();
            if (_equipo.Equals("BS-200"))
            {
                strHl7.Append(START_OF_BLOCK);
                strHl7.Append("MSH|^~\\&|||Mindray|" + _equipo + "|" + fecha + "||" + messageType + "|1|P|2.3.1||||0||" + _characterset + "|||");
                strHl7.Append('\r');
                strHl7.Append("MSA|AA|1|" + textInformation + "|||" + errorCode + "|");
                strHl7.Append('\r');
                strHl7.Append(END_OF_BLOCK);
                strHl7.Append('\r');
            }
            if (_equipo.Equals("BC-5300"))
            {
                strHl7.Append(START_OF_BLOCK);
                strHl7.Append("MSH|^~\\&|LIS||||" + fecha + "||" + messageType + "|1|P|2.3.1||||||" + _characterset);
                strHl7.Append('\r');
                strHl7.Append("MSA|AA|1");
                strHl7.Append('\r');
                strHl7.Append(END_OF_BLOCK);
                strHl7.Append('\r');
            }

            tcpServer1.Send(strHl7.ToString().Replace("\n", ""));
            Invoke((InvokeDelegate)(() => LogData(false, strHl7.ToString())));
        }
        private void SendAckQuery(string fecha, string messageType, string textInformation, string errorCode, string queryResponseStatus)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(START_OF_BLOCK);
            stringBuilder.Append("MSH|^~\\&|||Mindray|" + _equipo + "|" + fecha + "||" + messageType + "|1|P|2.3.1||||0||" + _characterset + "|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("MSA|AA|1|" + textInformation + "|||" + errorCode + "|");
            stringBuilder.Append('\r');
            stringBuilder.Append("ERR|0|");
            stringBuilder.Append('\r');
            stringBuilder.Append("QAK|SR|" + queryResponseStatus + "|");
            stringBuilder.Append('\r');
            stringBuilder.Append(END_OF_BLOCK);
            stringBuilder.Append('\r');
            var str = stringBuilder.ToString().Replace("\n", "");
            tcpServer1.Send(str);
            Invoke((InvokeDelegate)(() => LogData(false, str)));
        }
        private void ConsultarOrden(int equipoId, string turno)
        {
            _dtOrden.Rows.Clear();
            try
            {
                //using (var sqlConnection = new SqlConnection())
                //{
                //    sqlConnection.ConnectionString = Settings.Default.Conexion;
                //    sqlConnection.Open();
                //    if (sqlConnection.State != ConnectionState.Open) return;
                //    var command = sqlConnection.CreateCommand();
                //    command.CommandType = CommandType.StoredProcedure;
                //    command.CommandText = "LabPro.[dbo].[Lab_P_ConsultarOrdenEquipo]";
                //    command.Parameters.Clear();
                //    command.Parameters.Add(SetParameters("@PI_EquipoId", equipoId, DbType.Int32, ParameterDirection.Input));
                //    command.Parameters.Add(SetParameters("@PI_OrdTurno", turno, DbType.String, ParameterDirection.Input));
                //    new SqlDataAdapter(command).Fill(_dtOrden);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //private SqlParameter SetParameters(string parameterName, object parameterValue, DbType parameterType, ParameterDirection parameterDirection)
        //{
        //    var sqlParameter = new SqlParameter(parameterName, parameterType)
        //    {
        //        Value = parameterValue,
        //        Direction = parameterDirection
        //    };
        //    return sqlParameter;
        //}
        private void SendOrder(string fecha, string messageType, string textInformation, string errorCode, DataTable dtOrden)
        {
            const int num1 = 0;
            var stringBuilder = new StringBuilder();
            var str1 = dtOrden.Rows[0]["UsuarioId"].ToString();
            var str2 = dtOrden.Rows[0]["Apellido1"] + " " + dtOrden.Rows[0]["Nombre1"];
            var str3 = dtOrden.Rows[0]["FechaNacimiento"].ToString();
            var str4 = dtOrden.Rows[0]["Genero"].ToString();
            var str5 = dtOrden.Rows[0]["OrdTurno"].ToString();
            var str6 = dtOrden.Rows[0]["FechaConsulta"].ToString();
            var str7 = dtOrden.Rows[0]["Tipo"].ToString();
            if (str7 == "Emergency" || str7 == "Routine") str7 = "N";

            stringBuilder.Append(START_OF_BLOCK);
            stringBuilder.Append("MSH|^~\\&|||Mindray|" + _equipo + "|" + fecha + "||" + messageType + "|" + num1 + "|P|2.3.1||||0||" + _characterset + "|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("MSA|AA|1|" + textInformation + "|||" + errorCode + "|");
            stringBuilder.Append('\r');
            stringBuilder.Append("ERR|0|");
            stringBuilder.Append('\r');
            stringBuilder.Append("QAK|SR|OK|");
            stringBuilder.Append('\r');
            stringBuilder.Append("QRD|" + fecha + "|R|D|1|||RD|0019|OTH|||T|");
            stringBuilder.Append('\r');
            stringBuilder.Append("QRF|BS-400|" + fecha + "|" + fecha + "|||RCT|COR|ALL||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|1||" + str1 + "|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|2||0|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|3||" + str2 + "|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|4||" + str3 + "|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|5||" + str4 + "|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|6||O|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|7|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|8|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|9|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|10|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|11|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|12|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|13|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|14|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|15||outpatient|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|16|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|17||own|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|18|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|19|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|20|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|21||" + str5 + "|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|22|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|23||" + str6 + "|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|24||" + str7 + "|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|25|||||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|26||Serum|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|27||LIS|||");
            stringBuilder.Append('\r');
            stringBuilder.Append("DSP|28||LAB|||");
            stringBuilder.Append('\r');
            var num2 = 29;
            foreach (var str8 in from DataRow row in dtOrden.Rows select row["Abreviatura"].ToString())
            {
                stringBuilder.Append("DSP|" + num2 + "||" + str8 + "^^^|||");
                stringBuilder.Append('\r');
                ++num2;
            }
            stringBuilder.Append("DSC||");
            stringBuilder.Append('\r');
            stringBuilder.Append(END_OF_BLOCK);
            stringBuilder.Append('\r');
            var str = stringBuilder.ToString().Replace("\n", "");
            tcpServer1.Send(str);
            Invoke((InvokeDelegate)(() => LogData(false, str)));
        }
        private void GrabarResultado(string rxString)
        {
            Console.WriteLine(rxString);

            try
            {
                var listObj = new BindingList<MEntity>();
                var listObj2 = new List<MResultadoEquipo>();
                listObj.Clear();
                listObj2.Clear();

                var str1 = rxString;
                var ch = '\v';
                var str2 = ch.ToString();
                int num1;
                if (str1.Contains(str2))
                {
                    var str3 = rxString;
                    ch = '\x001C';
                    var str4 = ch.ToString();
                    num1 = !str3.Contains(str4) ? 1 : 0;
                }
                else
                    num1 = 1;
                if (num1 != 0) return;
                var startIndex = rxString.IndexOf('\v');
                var num2 = rxString.IndexOf('\x001C');
                rxString = rxString.Substring(startIndex, num2 - startIndex);
                var strArray1 = rxString.Split('|');
                var dateTime = DateTime.Now;
                var now = DateTime.Now;
                var str5 = string.Empty;
                for (var index = 0; index < strArray1.Length - 1; ++index)
                {
                    var clsEntity = new MEntity
                    {
                        ResEquipoId = _idEquipo == 1 ? 3 : 0 // id equipo sql server 3 - bs200.... 0 - bs3500
                    };
                    if (strArray1[index].Replace("\r", "").Contains("OBR"))
                    {
                        str5 = strArray1[index + 2].Replace("\r\n", "").Replace("\r\n", "");
                        var str3 = strArray1[index + 7].Replace("\r\n", "");
                        dateTime = Convert.ToDateTime(str3.Substring(0, 4) + "-" + str3.Substring(4, 2) + "-" + str3.Substring(6, 2));
                        now = DateTime.Now;
                    }
                    clsEntity.ResTurno = str5;
                    clsEntity.ResFecha = dateTime;
                    clsEntity.ResHora = now;
                    clsEntity.ResEstado = Resources.EstadoActivo;
                    clsEntity.ResAbrev = string.Empty;
                    clsEntity.ResResultado = string.Empty;

                    if (!strArray1[index].Replace("\r\n", "").Contains("OBX")) continue;

                    var strArray2 = strArray1[index + 3].Replace("\r\n", "").Split('^');
                    clsEntity.ResAbrev = strArray2[0];
                    clsEntity.ResResultado = strArray1[index + 5].Replace("\r\n", "");
                    clsEntity.ResError = string.Empty;

                    decimal result;
                    decimal.TryParse(clsEntity.ResResultado, out result);
                    clsEntity.ResConvertido = result;
                    listObj.Add(clsEntity);
                    listObj2.Add(new MResultadoEquipo
                    {
                        IdEquipo = string.Concat("EQL", _idEquipo),
                        FechaHora = Convert.ToDateTime(clsEntity.ResFecha.ToShortDateString() + " " + clsEntity.ResHora.ToShortTimeString()),
                        OrdenTurno = clsEntity.ResTurno,
                        Abreviatura = clsEntity.ResAbrev,
                        Estado = clsEntity.ResEstado,
                        Resultado = clsEntity.ResResultado,
                        Error = clsEntity.ResError,
                        Convertido = string.Concat(string.Empty, clsEntity.ResConvertido)
                    });
                }

                var strError2 = NResultadoEquipo.GuardarResultadoEquipo(listObj2);
                _dgvResultado.DataSource = listObj;
                if (!string.IsNullOrEmpty(strError2)) Invoke((InvokeDelegate)(() => LogData(false, strError2)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error al grabar resultado: " + ex.Message);
            }
        }
        private void _btnClose_Click(object sender, EventArgs e)
        {
            ClosePort();
            Application.Exit();
        }
        private void _txtIdleTime_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tcpServer1.IdleTime = Convert.ToInt32(_txtIdleTime.Text);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void _txtMaxThreads_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tcpServer1.MaxCallbackThreads = Convert.ToInt32(_txtMaxThreads.Text);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void _txtAttempts_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tcpServer1.MaxSendAttempts = Convert.ToInt32(_txtAttempts.Text);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void tcpServer1_OnConnect(tcpServer.TcpServerConnection connection)
        {
            Invoke((InvokeDelegate)(() => _lblConnected.Text = tcpServer1.Connections.Count.ToString()));
        }
        private void tcpServer1_OnDataAvailable(tcpServer.TcpServerConnection connection)
        {
            var bytes = ReadStream(connection.Socket);
            if (bytes == null) return;
            var dataStr = Encoding.ASCII.GetString(bytes);

            Invoke((InvokeDelegate)(() => LogData(false, dataStr)));

            if (!dataStr.Contains('\v'.ToString()) || !dataStr.Contains('\x001C'.ToString())) return;

            var strArray = dataStr.Split('|');
            var fecha = strArray[6];
            var str1 = strArray[8];
            var messageId = strArray[9];
            var str2 = strArray[15];

            switch (str1 + str2)
            {
                case "ORU^R01":
                    if (_equipo.Equals("BC-5300"))
                    {
                        Invoke((InvokeDelegate)(() => SendAck(fecha, "ACK^R01", "Message accepted", messageId, "0")));
                        Invoke((InvokeDelegate)(() => GrabarResultado(dataStr)));
                    }
                    break;
                case "ORU^R010":
                    Invoke((InvokeDelegate)(() => SendAck(fecha, "ACK^R01", "Message accepted", messageId, "0")));
                    Invoke((InvokeDelegate)(() => GrabarResultado(dataStr)));
                    break;
                case "ORU^R0101":
                    Invoke((InvokeDelegate)(() => SendAck(fecha, "ACK^R01", "Message accepted", messageId, "1")));
                    break;
                case "ORU^R0102":
                    Invoke((InvokeDelegate)(() => SendAck(fecha, "ACK^R01", "Message accepted", messageId, "2")));
                    break;
                case "QRY^Q02":
                    var turno = strArray[28];
                    Invoke((InvokeDelegate)(() => ConsultarOrden(_idEquipo, turno)));
                    if (_dtOrden.Rows.Count > 0)
                    {
                        Invoke((InvokeDelegate)(() => SendAckQuery(fecha, "QCK^Q02", "Message accepted", "0", "OK")));
                        Invoke((InvokeDelegate)(() => SendOrder(fecha, "DSR^Q03", "Message accepted", "0", _dtOrden)));
                        break;
                    }
                    Invoke((InvokeDelegate)(() => SendAckQuery(fecha, "QCK^Q02", "Message accepted", "0", "NF")));
                    break;
            }
        }
        protected byte[] ReadStream(TcpClient client)
        {
            var stream = client.GetStream();
            if (!stream.DataAvailable)
                return null;
            var buffer = new byte[client.Available];
            var length = 0;
            try
            {
                length = stream.Read(buffer, 0, buffer.Length);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (length >= buffer.Length) return buffer;
            var numArray = buffer;
            buffer = new byte[length];
            Array.ConstrainedCopy(numArray, 0, buffer, 0, length);
            return buffer;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DisplayTcpServerStatus();
            _lblConnected.Text = tcpServer1.Connections.Count.ToString();
        }
        private void _txtValidateInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tcpServer1.VerifyConnectionInterval = Convert.ToInt32(_txtValidateInterval.Text);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dataGridViewColumn = _dgvResultado.Columns["ResTurno"];
            if (dataGridViewColumn != null) dataGridViewColumn.DisplayIndex = 0;

            var gridViewColumn = _dgvResultado.Columns["ResAbrev"];
            if (gridViewColumn != null) gridViewColumn.DisplayIndex = 1;

            var viewColumn = _dgvResultado.Columns["ResResultado"];
            if (viewColumn != null) viewColumn.DisplayIndex = 2;

            var column = _dgvResultado.Columns["ResError"];
            if (column != null) column.DisplayIndex = 3;

            var dataGridViewColumn1 = _dgvResultado.Columns["ResError"];
            if (dataGridViewColumn1 != null) dataGridViewColumn1.Width = 125;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosePort();
        }
        private void ClosePort()
        {
            try
            {
                if (tcpServer1.IsOpen) tcpServer1.Close();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            timer1.Enabled = false;
        }
    }
}
