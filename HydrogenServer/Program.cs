using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HydrogenServer
{
	class Program
	{
		static Socket socket;

		static void Main(string[] args)
		{
			IPHostEntry hosts = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ip = hosts.AddressList.First(address => address.AddressFamily == AddressFamily.InterNetwork);
			int port = 17000;
			IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
			int backlogSize = 100;

			try
			{
				Program.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				Program.socket.Bind(endPoint);
				Program.socket.Listen(backlogSize);
				Program.socket.BeginAccept(AcceptCallback, null);
				Console.WriteLine($"Main, Begin accepting, end point = {endPoint.ToString()}");
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
					case "exit":
						loop = false;
						break;
				}
			}
		}

		static void AcceptCallback(IAsyncResult result)
		{
			Socket socket = Program.socket.EndAccept(result);
			Console.WriteLine($"AcceptCallback, socket = {socket.ToString()}");
		}
	}
}
