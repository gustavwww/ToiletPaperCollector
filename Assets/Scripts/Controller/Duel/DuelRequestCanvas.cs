using Controller.CommandHandlers;
using Model;
using TMPro;
using UnityEngine;

namespace Controller.Duel {
    public class DuelRequestCanvas : MonoBehaviour, DuelCommandListener {

        public DuelCommandHandler duelCommandHandler;
        public DuelController duelController;
        public GameModel gameModel;

        public Canvas mainMenuCanvas;
        public Canvas duelCanvas;
        public Camera duelCamera;
    
        public TMP_Text sender;

        public void acceptPressed() {
            joinDuel();
        }

        public void declinePressed() {
            gameObject.SetActive(false);
        }

        private void joinDuel() {
            duelController.instantiateDuel(gameModel.getNickName(), sender.text);
            gameObject.SetActive(false);
            mainMenuCanvas.gameObject.SetActive(false);
            duelCanvas.gameObject.SetActive(true);
            duelCamera.gameObject.SetActive(true);
        }

        private void Start() {
            duelCommandHandler.addListener(this);
        }
    
        public void gotRequest(string from) {
            sender.text = from;
            gameObject.SetActive(true);
        }

        public void gotResponse(DuelResponseType type) {
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
