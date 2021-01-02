using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Services {
	
	public class TCPClient {

		private readonly string host;
		private readonly int port;

		private TcpClient socket;
		private Thread readThread;

		private bool isListening = false;
		
		private readonly IList<TCPListener> listeners = new List<TCPListener>();

		public TCPClient(string host, int port) {
			this.host = host;
			this.port = port;
		}
		
		public void addListener(TCPListener listener) {
			listeners.Add(listener);
		}

		private void informObserversNewMessage(string msg) {
			foreach (TCPListener listener in listeners) {
				listener.messageReceived(msg);
			}
		}

		private void informObserversException(Exception e) {
			foreach (TCPListener listener in listeners) {
				listener.exceptionOccurred(e);
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

				while (isListening) {

					socket = new TcpClient(host, port);
					Byte[] bytes = new Byte[1024];

					using (NetworkStream stream = socket.GetStream()) {

						int length;
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {
							
							byte[] incomingData = new byte[length];
							Array.Copy(bytes, 0, incomingData, 0, length);
					 						
							string serverMessage = Encoding.ASCII.GetString(incomingData).Trim();
							informObserversNewMessage(serverMessage);
						}
						
					}
				}

			} catch (SocketException e) {
				informObserversException(e);
			}

		}

		public void sendMessage(string message) {
			Debug.Log("Sending: " + message);
			if (socket == null) { return; }

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
