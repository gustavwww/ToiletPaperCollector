using System;
using System.Collections.Generic;
using Services.Protocol;
using UnityEngine;

namespace Controller {

    public enum DuelResponseType {
        NOTFOUND, CANCELLED, FINISHED
    }
    public class DuelCommandHandler : MonoBehaviour {

        public ServerController serverController;
        
        private readonly List<DuelCommandListener> listeners = new List<DuelCommandListener>();

        public void addListener(DuelCommandListener listener) {
            listeners.Add(listener);
        }

        public void removeListener(DuelCommandListener listener) {
            listeners.Remove(listener);
        }

        public void sendRequest(String nickname) {
            serverController.sendTCP("duel", "request", nickname);
        }

        public void acceptRequest() {
            serverController.sendTCP("duel", "response", "accept");
        }

        public void rejectRequest() {
            serverController.sendTCP("duel", "response", "decline");
        }

        public void readyUp() {
            serverController.sendTCP("duel", "ready");
        }

        public void sendCount() {
            serverController.sendTCP("duel", "count");
        }

        public void handleDuelCommand(Command cmd) {
            if (!cmd.getCmd().Equals("duel") || cmd.getArgs().Length < 1) {
                return;
            }
            String[] args = cmd.getArgs();
            String subCmd = args[0];

            switch (subCmd) {
                
                case "request":
                    String from = args[1];
                    informGotRequest(from);
                    break;
                
                case "response":
                    switch (args[1]) {
                        case "notfound":
                            informGotResponse(DuelResponseType.NOTFOUND);
                            break;
                        
                        case "cancelled":
                            informGotResponse(DuelResponseType.CANCELLED);
                            break;
                        
                        case "finished":
                            informGotResponse(DuelResponseType.FINISHED);
                            break;
                    }
                    break;
                
                case "started":
                    informDuelStarted();
                    break;
                
                case "ended":
                    String winner = args[1];
                    informDuelEnded(winner);
                    break;
                
                case "ready":
                    String rdy = args[1];
                    informUserReadyUp(rdy);
                    break;
                
                case "count":
                    String sender = args[1];
                    int amount = int.Parse(args[2]);
                    informCountSent(sender, amount);
                    break;
                
                case "starttimer":
                    int startcount = int.Parse(args[1]);
                    informStartTimerChanged(startcount);
                    break;
                
                case "gametimer":
                    int gamecount = int.Parse(args[1]);
                    informGameTimerChanged(gamecount);
                    break;
            }

        }

        private void informGotRequest(String from) {
            foreach (var l in listeners) {
                l.gotRequest(from);
            }
        }
        
        private void informGotResponse(DuelResponseType type) {
            foreach (var l in listeners) {
                l.gotResponse(type);
            }
        }
        
        private void informDuelStarted() {
            foreach (var l in listeners) {
                l.duelStarted();
            }
        }
        
        private void informDuelEnded(String winner) {
            foreach (var l in listeners) {
                l.duelEnded(winner);
            }
        }
        
        private void informUserReadyUp(String nickname) {
            foreach (var l in listeners) {
                l.userReadyUp(nickname);
            }
        }
        
        private void informCountSent(String sender, int count) {
            foreach (var l in listeners) {
                l.countSent(sender, count);
            }
        }
        
        private void informStartTimerChanged(int count) {
            foreach (var l in listeners) {
                l.startTimerChanged(count);
            }
        }
        
        private void informGameTimerChanged(int count) {
            foreach (var l in listeners) {
                l.gameTimerChanged(count);
            }
        }
        
    }
}