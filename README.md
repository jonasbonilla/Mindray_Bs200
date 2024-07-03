# **OBTENER RESULTADOS DE ANÁLISIS - Mindray Bs200**

![Mindray Bs200](https://medtestdx.com/wp-content/uploads/2019/05/BS200-Software.jpg)

***Esta app se hizo para obtener resultados de los análisis de laboratorio realizados por un equipo Mindray Bs200 que luego serán usadas en otro sistema.***
**Nota:** Es mi primer aporte en GitHub.

* Para su realización usamos la librería TcpServer, que proporciona un componente de servidor TCP fácil de usar para el entorno .NET, ya que la comunicación con el equipo se obtiene a través de LIS, que es un sistema responsable del seguimiento de los pedidos de muestra y los resultados.


Ejemplo de obtención de datos:

```c#
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
```

## **FUNCIONAMIENTO**
Básicamente, funciona como un servidor, se conecta al equipo seleccionado Bs200, obtiene los resultados del análisis especificado y almacena los datos en una BD para ser tomados por otro sistema. 
Es un intermediario en el servidor, puesto que el sistema de destino es una aplicación web.
