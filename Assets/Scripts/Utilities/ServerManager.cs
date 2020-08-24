using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Utilities {
	
	public class ServerManager {

		private static readonly string HOST = "localhost";
		private static readonly int PORT = 26000;

		private TcpClient socket;
		private Thread readThread;

		private bool isListening = false;
		
		private IList<ServerListener> observers = new List<ServerListener>();

		private ServerManager() {}

		private static ServerManager instance = null;
		public static ServerManager getInstance() {
			if (instance != null) {
				return instance;
			}
			return new ServerManager();
		}
	
		public void addObserver(ServerListener listener) {
			observers.Add(listener);
		}

		private void informObserversNewMessage(ServerCommand cmd) {
			foreach (ServerListener observer in observers) {
				observer.commandReceived(cmd);
			}
		}

		private void informObserversException(Exception e) {
			foreach (ServerListener observer in observers) {
				observer.exceptionOccurred(e);
			}
		}

		public void connect() {
			try {
				readThread = new Thread(new ThreadStart(listen));
				readThread.IsBackground = true;
				readThread.Start();

			} catch (Exception e) {
				informObserversException(e);
			}

		}

		private void listen() {
			isListening = true;
			
			try {

				while (true) {
					if (!isListening) { break; }
					
					socket = new TcpClient(HOST, PORT);
					Byte[] bytes = new Byte[1024];

					using (NetworkStream stream = socket.GetStream()) {

						int length;
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {
							if (!isListening) { break;}
							var incommingData = new byte[length];
							Array.Copy(bytes, 0, incommingData, 0, length);
					 						
							string serverMessage = Encoding.ASCII.GetString(incommingData).Trim();
							informObserversNewMessage(ServerProtocol.processMessage(serverMessage));
						}
						
					}
				}

			} catch (SocketException e) {
				informObserversException(e);
			}

		}

		public void sendMessage(string message) {
			Debug.Log("Sending: " + message);
			
			if (socket == null) {
				return;
			}

			try {
				NetworkStream stream = socket.GetStream();
				
				if (stream.CanWrite) {
					byte[] byteMsg = Encoding.ASCII.GetBytes(message + "\n");
					stream.Write(byteMsg, 0, byteMsg.Length);
				}

			} catch (SocketException e) {
				informObserversException(e);
			}

		}
		
		public void close() {

			try {
				socket.Close();
				isListening = false;
			} catch (Exception e) {
				Debug.Log(e.StackTrace);
			}
			
		}

	}
	
}
