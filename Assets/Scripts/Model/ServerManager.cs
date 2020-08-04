using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ServerManager : MonoBehaviour {

    private static readonly string host = "localhost";
    private static readonly int port = 26000;

    private TcpClient socket;
    private Thread readThread;

	public void connect() {
		try {
			readThread = new Thread(new ThreadStart(listen));
			readThread.IsBackground = true;
			readThread.Start();

		} catch (Exception e) {
			Debug.Log(e.StackTrace);
		}

	}

    private void listen() {

		try {

			while (true) {
						socket = new TcpClient(host, port);
						Byte[] bytes = new Byte[1024];

						using (NetworkStream stream = socket.GetStream()) {

							int length;
									
							while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {
								var incommingData = new byte[length];
								Array.Copy(bytes, 0, incommingData, 0, length);
					 						
								string serverMessage = Encoding.ASCII.GetString(incommingData);
								Debug.Log("Server message received as: " + serverMessage);
							}

						}
					}

		} catch (SocketException e) {
			Debug.Log(e.StackTrace);
		}

	}

	public void send(string msg) {



	}

}
