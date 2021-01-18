using System;
using JetBrains.Annotations;
using Model;
using Protocol;
using Services;
using Services.Protocol;
using UnityEngine;

namespace Controller {
    
    public class ServerController : TCPListener {

        private static readonly string HOST = "localhost";
        private static readonly int PORT = 26000;

        private readonly MenuController menuController;
        private readonly GameController gameController;

        private readonly TCPClient tcpClient;
        private readonly IServerProtocol protocol;

        private bool loggedIn = false;
        
        public ServerController(MenuController menuController, GameController gameController) {
            this.menuController = menuController;
            this.gameController = gameController;
            tcpClient = new TCPClient(HOST, PORT);
            tcpClient.addListener(this);
            protocol = ProtocolFactory.getServerProtocol();

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
                case "logged":
                    int amount = int.Parse(cmd.getArgs()[0]);
                    loggedIn = true;
                    UnityMainThread.instance.addJob(() => {
                        menuController.menuView.showMainMenu();
                        gameController.setGameModel(new GameModel(amount));
                    });
                    break;

                case "error":
                    string err = cmd.getArgs()[0];
                    if (err.StartsWith("usercouldnotbefound")) {
                        UnityMainThread.instance.addJob(() => {
                            menuController.menuView.setLoading(false);
                            menuController.menuView.showNickPanel();
                        });
                    } else {
                        UnityMainThread.instance.addJob(() => {
                            menuController.menuView.setLoading(false);
                            menuController.menuView.displayError(err);
                        });
                    }
                    break;
            }
        }

        public void exceptionOccurred(Exception e) {
            Debug.Log("Error occurred: " + e.Message);
            loggedIn = false;
            UnityMainThread.instance.addJob(() => {
                menuController.menuView.setLoading(false);
                menuController.menuView.displayError(e.Message);
            });
        }

        public bool isLoggedIn() {
            return loggedIn;
        }

    }
    
    
}