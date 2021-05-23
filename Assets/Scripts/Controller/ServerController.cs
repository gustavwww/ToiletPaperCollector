using System;
using System.Collections.Generic;
using Controller.CommandHandlers;
using JetBrains.Annotations;
using Model;
using Services;
using Services.Protocol;
using UnityEngine;
using Object = System.Object;

namespace Controller {
    
    public class ServerController : MonoBehaviour, TCPListener {

        // 188.166.99.144
        private static readonly string HOST = "localhost";
        private static readonly int PORT = 26000;
        
        public DuelCommandHandler duelCommandHandler;
        public ServerErrorHandler serverErrorHandler;

        private TCPClient tcpClient;
        private IServerProtocol protocol;

        private bool connected = false;
        private bool loggedIn = false;

        private readonly List<ServerControllerListener> listeners = new List<ServerControllerListener>();

        public void addListener(ServerControllerListener listener) {
            listeners.Add(listener);
        }

        public void removeListener(ServerControllerListener listener) {
            listeners.Remove(listener);
        }

        private void Start() {
            tcpClient = new TCPClient(HOST, PORT);
            tcpClient.addListener(this);
            protocol = ProtocolFactory.getServerProtocol();
        }

        public void connect() {
            tcpClient.connect();
        }

        public void login(string deviceID, [CanBeNull] string nickname) {
            if (nickname != null) {
                sendTCP("login", deviceID, nickname);
            } else {
                sendTCP("login", deviceID);
            }
        }
        
        public void sendTCP(string cmd, params string[] args) {
            tcpClient.sendMessage(protocol.writeMessage(new Command(cmd, args)));
        }

        public void messageReceived(string msg) {
            Command cmd = protocol.parseMessage(msg);
            
            switch (cmd.getCmd()) {
                case "connected":
                    connected = true;
                    informListenersConnected();
                    break;

                case "logged":
                    string name = cmd.getArgs()[0];
                    int coins = int.Parse(cmd.getArgs()[1]);
                    int totalAmount = int.Parse(cmd.getArgs()[2]);
                    int amount = int.Parse(cmd.getArgs()[3]);
                    loggedIn = true;
                    informListenersLogged(name, coins, amount, totalAmount);
                    break;

                case "duel":
                    UnityMainThread.instance.addJob(() => {
                        duelCommandHandler.handleDuelCommand(cmd);
                    });
                    break;
                
                case "error":
                    string err = cmd.getArgs()[0];
                    serverErrorHandler.handleServerError(err);
                    break;
            }
        }
        
        // err.StartsWith("usercouldnotbefound")

        public void exceptionOccurred(Exception e) {
            Debug.Log("Error occurred: " + e.Message);
            connected = false;
            loggedIn = false;
            serverErrorHandler.handleServerException(e);
        }

        private void informListenersConnected() {
            UnityMainThread.instance.addJob(() => {
                foreach (ServerControllerListener l in listeners) {
                    l.onConnected();
                }
            });
        }
        
        private void informListenersLogged(string name, int coins, int amount, int totalAmount) {
            UnityMainThread.instance.addJob(() => {
                foreach (ServerControllerListener l in listeners) {
                    l.onLoggedIn(name, coins, amount, totalAmount);
                }
            });
        }

        public bool isLoggedIn() {
            return loggedIn;
        }

        public bool isConnected() {
            return connected;
        }

    }

}