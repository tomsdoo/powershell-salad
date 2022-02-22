namespace TCP
{
	public class SocketCreatorClass
	{
		System.Net.Sockets.Socket m_soc;
		public SocketCreatorClass()
		{
			m_soc = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
		}
		public System.Net.Sockets.Socket GetServerSocket(int provided_port, int provided_maxconn)
		{
			System.Net.IPAddress hostip = null;
			foreach(System.Net.IPAddress tempip in (System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList))
			{
				if(tempip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					hostip = tempip;
				}
			}
			System.Net.IPEndPoint endpoint = new System.Net.IPEndPoint(hostip, provided_port);
			m_soc.Bind(endpoint);
			m_soc.Listen(provided_maxconn);
			return m_soc;
		}
		public System.Net.Sockets.Socket GetClientSocket(string provided_server, int provided_port)
		{
			System.Net.IPAddress ipaddr = null;
			foreach(System.Net.IPAddress tempip in (System.Net.Dns.GetHostEntry(provided_server).AddressList))
			{
				if(tempip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					
					ipaddr = tempip;
				}
			}
			System.Net.IPEndPoint endpoint = new System.Net.IPEndPoint(ipaddr, provided_port);
			m_soc.Connect(endpoint);
			return m_soc;
		}
	}
	public class AsyncStateObject
	{
		public System.Net.Sockets.Socket Socket;
		public byte[] ReceiveBuffer;
		public System.IO.MemoryStream ReceivedData;
		public AsyncStateObject(System.Net.Sockets.Socket soc)
		{
			Socket = soc;
			ReceiveBuffer = new byte[1024];
			ReceivedData = new System.IO.MemoryStream();
		}
	}
	public delegate void TCPSocketClosedEventHandler(TCPBaseClass provided_tcpbase);
	public delegate void TCPBeginningReceiveErrorOccuredEventHandler(TCPBaseClass provided_tcpbase);
	public class TCPBaseClass
	{
		public event TCPSocketClosedEventHandler SocketClosed;
		public event TCPBeginningReceiveErrorOccuredEventHandler BeginningReceiveErrorOccured;
		public TCPBaseClass(){}
		public void StartReceive(System.Net.Sockets.Socket soc)
		{
			AsyncStateObject so = new AsyncStateObject(soc);
			soc.BeginReceive(so.ReceiveBuffer, 0, so.ReceiveBuffer.Length, System.Net.Sockets.SocketFlags.None, new System.AsyncCallback(ReceiveDataCallback), so);
		}
		public void ReceiveDataCallback(System.IAsyncResult ar)
		{
			AsyncStateObject so = (AsyncStateObject)ar.AsyncState;
			int len = 0;
			try
			{
				len = so.Socket.EndReceive(ar);
			}
			catch
			{
				return;
			}
			if(0 >= len)
			{
				so.Socket.Close();
				if(null != SocketClosed)
				{
					SocketClosed(this);
				}
				return;
			}
			so.ReceivedData.Write(so.ReceiveBuffer, 0, len);
			bool bContinue = true;
			if(0 == so.Socket.Available)
			{
				string str = System.Text.Encoding.UTF8.GetString(so.ReceivedData.ToArray());
				bContinue = ReceivedAndAct(str, so.Socket);
				so.ReceivedData = new System.IO.MemoryStream();
			}
			if(bContinue)
			{
				try
				{
					so.Socket.BeginReceive(so.ReceiveBuffer, 0, so.ReceiveBuffer.Length, System.Net.Sockets.SocketFlags.None, new System.AsyncCallback(ReceiveDataCallback), so);
				}
				catch
				{
					if(null != BeginningReceiveErrorOccured)
					{
						BeginningReceiveErrorOccured(this);
					}
				}
			}
		}
		protected virtual bool ReceivedAndAct(string provided_message, System.Net.Sockets.Socket provided_socket)
		{
			return true;
		}
		public void SendData(System.Net.Sockets.Socket provided_socket, string provided_str)
		{
			provided_socket.Send(System.Text.Encoding.UTF8.GetBytes(provided_str));
			return;
		}
	}
	public delegate void TCPServerBaseAcceptStartedEventHandler(TCPServerBaseClass provided_serverbase);
	public delegate void TCPServerBaseListenStartedEventHandler(TCPServerBaseClass provided_serverbase);
	public delegate void TCPServerBaseListenEndedEventHandler(TCPServerBaseClass provided_serverbase);
	public delegate void TCPServerBaseConnectionAcceptedEventHandler(TCPServerBaseClass provided_serverbase);
	public delegate void TCPServerBaseMessageReceivedEventHandler(TCPServerBaseClass provided_serverbase, string provided_data);
	public class TCPServerBaseClass : TCPBaseClass
	{
		public event TCPServerBaseAcceptStartedEventHandler AcceptStarted;
		public event TCPServerBaseListenStartedEventHandler ListenStarted;
		public event TCPServerBaseListenEndedEventHandler ListenEnded;
		public event TCPServerBaseConnectionAcceptedEventHandler BaseConnectionAccepted;
		public event TCPServerBaseMessageReceivedEventHandler BaseMessageReceived;
		protected int m_port;
		public int port
		{
			get
			{
				return m_port;
			}
		}
		protected int m_maxconnection;
		public int maxconnection
		{
			get
			{
				return m_maxconnection;
			}
		}
		protected System.Net.Sockets.Socket m_serversocket;
		public System.Net.Sockets.Socket serversocket
		{
			get
			{
				return m_serversocket;
			}
		}
		public TCPServerBaseClass(int provided_port, int provided_maxconnection) : base()
		{
			m_port = provided_port;
			m_maxconnection = provided_maxconnection;
			m_serversocket = null;
		}
		public void StartListen()
		{
			SocketCreatorClass c = new SocketCreatorClass();
			m_serversocket = c.GetServerSocket(m_port, m_maxconnection);
			if(null != ListenStarted)
			{
				ListenStarted(this);
			}
		}
		public virtual void EndListen()
		{
			m_serversocket.Close();
			m_serversocket = null;
			if(null != ListenEnded)
			{
				ListenEnded(this);
			}
		}
		public void StartAccept()
		{
			m_serversocket.BeginAccept(new System.AsyncCallback(AcceptCallback), m_serversocket);
			if(null != AcceptStarted)
			{
				AcceptStarted(this);
			}
		}
		public void AcceptCallback(System.IAsyncResult ar)
		{
			System.Net.Sockets.Socket server = (System.Net.Sockets.Socket)ar.AsyncState;
			System.Net.Sockets.Socket client = null;
			try
			{
				client = server.EndAccept(ar);
			}
			catch
			{
				return;
			}
			StartReceive(client);
			AcceptedAndAct(client);
			server.BeginAccept(new System.AsyncCallback(AcceptCallback), server);
		}
		protected virtual void AcceptedAndAct(System.Net.Sockets.Socket provided_fromsoc)
		{
			if(null != BaseConnectionAccepted)
			{
				BaseConnectionAccepted(this);
			}
			return;
		}
		protected override bool ReceivedAndAct(string provided_data, System.Net.Sockets.Socket provided_socket)
		{
			if(null != BaseMessageReceived)
			{
				BaseMessageReceived(this, provided_data);
			}
			return true;
		}
	}
	public class TCPClientBaseClass : TCPBaseClass
	{
		protected string m_server;
		public string server
		{
			get
			{
				return m_server;
			}
		}
		protected int m_port;
		public int port
		{
			get
			{
				return m_port;
			}
		}
		protected System.Net.Sockets.Socket m_soc;
		public System.Net.Sockets.Socket soc
		{
			get
			{
				return m_soc;
			}
		}
		public TCPClientBaseClass(string provided_server, int provided_port) : base()
		{
			m_server = provided_server;
			m_port = provided_port;
		}
		public void Connect()
		{
			SocketCreatorClass tempc = new SocketCreatorClass();
			m_soc = tempc.GetClientSocket(m_server, m_port);
			StartReceive(m_soc);
		}
		public void SendData(string provided_message)
		{
			m_soc.Send(System.Text.Encoding.UTF8.GetBytes(provided_message));
		}
		public virtual void Close()
		{
			try
			{
				m_soc.Shutdown(System.Net.Sockets.SocketShutdown.Both);
				m_soc.Close();
			}
			catch
			{
				System.Console.WriteLine("closing failed");
			}
		}
		protected override bool ReceivedAndAct(string provided_data, System.Net.Sockets.Socket provided_socket)
		{
			return true;
		}
	}
}
