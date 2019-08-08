using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HydrogenClient
{
	class Program
	{
		static Socket socket;

		static void Main(string[] args)
		{
			try
			{
				Program.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Main, {ex.ToString()}, {ex.Message}");
			}

			bool loop = true;
			while (loop)
			{
				string command = Console.ReadLine();
				switch (command)
				{
					case "connect":
						try
						{
							IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 18000);
							Program.socket.BeginConnect(endPoint, ConnectCallback, null);
						}
						catch (Exception ex)
						{
							Console.WriteLine($"Main, {ex.ToString()}, {ex.Message}");
						}
						break;

					case "exit":
						loop = false;
						break;
				}
			}
		}

		static void ConnectCallback(IAsyncResult result)
		{
			try
			{
				Program.socket.EndConnect(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"ConnectCallback, {ex.ToString()}, {ex.Message}");
			}
		}
	}
}
