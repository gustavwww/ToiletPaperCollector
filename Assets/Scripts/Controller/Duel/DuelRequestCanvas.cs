using System;
using Controller.CommandHandlers;
using Model;
using TMPro;
using UnityEngine;

namespace Controller.Duel {
    public class DuelRequestCanvas : MonoBehaviour, DuelCommandListener {

        public DuelCommandHandler duelCommandHandler;
        public DuelController duelController;
        public GameModel gameModel;

        private CanvasGroup duelCanvas;

        public TMP_Text sender;

        private void Start() {
            duelCanvas = gameObject.GetComponent<CanvasGroup>();
            duelCommandHandler.addListener(this);
        }
        
        public void acceptPressed() {
            duelCommandHandler.acceptRequest();
            duelController.joinDuel(gameModel.getUser().getNickname(), sender.text);
            showRequestCanvas(false);
        }

        public void declinePressed() {
            duelCommandHandler.rejectRequest();
            showRequestCanvas(false);
        }

        private void showRequestCanvas(bool show) {
            duelCanvas.alpha = show ? 1 : 0;
            duelCanvas.interactable = show;
            duelCanvas.blocksRaycasts = show;
        }
    
        public void gotRequest(string from) {
            sender.text = from;
            showRequestCanvas(true);
        }

        public void gotResponse(DuelResponseType type) {
            if (type == DuelResponseType.CANCELLED) {
                showRequestCanvas(false);
            }
        }

        public void duelStarted() {
        }

        public void duelEnded(string winner) {
        }

        public void userLeft(string nickname) {
        }

        public void userReadyUp(string nickname) {
        }

        public void countSent(string sender, int count) {
        }

        public void startTimerChanged(int count) {
        }

        public void gameTimerChanged(int count) {
        }
    
    }
}
