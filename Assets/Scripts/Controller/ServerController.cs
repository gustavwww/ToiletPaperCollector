using System;
using JetBrains.Annotations;
using Protocol;
using Services;
using Services.Protocol;
using UnityEngine;

namespace Controller {
    
    public class ServerController : TCPListener {

        private static readonly string HOST = "localhost";
        private static readonly int PORT = 26000;

        private readonly MenuController menuController;

        private readonly TCPClient tcpClient;
        private readonly IServerProtocol protocol;

        public ServerController(MenuController menuController) {
            this.menuController = menuController;
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
                    UnityMainThread.instance.addJob(() => {
                        menuController.navigate(Navigation.GAME);
                    });
                    break;

                case "error":
                    
                    if (cmd.getArgs()[0].StartsWith("usercouldnotbefound")) {
                        UnityMainThread.instance.addJob(() => {
                            menuController.turnOffIndicators();
                            menuController.showNickMenu();
                        });
                    } else {
                        UnityMainThread.instance.addJob(() => {
                            menuController.turnOffIndicators();
                            menuController.showMenuError(true);
                            menuController.showNickError(true);
                        });
                    }
                    break;
            }
        }

        public void exceptionOccurred(Exception e) {
            Debug.Log("Error occurred: " + e.Message);
            UnityMainThread.instance.addJob(() => {
                menuController.turnOffIndicators();
                menuController.showMenuError(true);
                menuController.showNickError(true);
            });
        }

    }
    
    
}