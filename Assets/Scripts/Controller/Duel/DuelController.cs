using Controller.CommandHandlers;
using TMPro;
using UnityEngine;
using View;
using View.skin;

namespace Controller.Duel {
    
    public class DuelController : MonoBehaviour, DuelCommandListener {

        public DuelCommandHandler duelCommandHandler;

        public Canvas mainMenuCanvas;
        public Camera duelCamera;
        public Canvas duelCanvas;
        
        public TMP_Text name;
        public TMP_Text nameRdy;
        public TMP_Text score;

        public TMP_Text opponentName;
        public TMP_Text opponentNameRdy;
        public TMP_Text opponentScore;

        public TMP_Text gameTimer;
        public TMP_Text startTimer;

        public GameObject rdyBtn;

        public GameObject gameOverPanel;
        public TMP_Text winner;

        public SkinManager skinManager;
        public GameObject box;
        public GameObject opponentBox;

        private bool running = false;
        
        private void Start() {
            duelCommandHandler.addListener(this);
        }

        public void clicked() {
            if (!running) { return; }
            duelCommandHandler.sendCount();
            skinManager.spawnBody(box);
        }

        public void readyPressed() {
            duelCommandHandler.readyUp();
            rdyBtn.SetActive(false);
        }

        public void backPressed() {
            if (running) { return; }
            display(false);
        }

        public void joinDuel(string name, string opponentName) {
            this.name.text = name.ToLower();
            nameRdy.text = name.ToLower();
            this.opponentName.text = opponentName.ToLower();
            opponentNameRdy.text = opponentName.ToLower();
            display(true);
        }

        private void resetWindow() {
            running = false;
            score.text = "0";
            opponentScore.text = "0";
            gameTimer.text = "15 s";
            winner.text = "NAN";
            setReady(false);
            setOpponentReady(false);
            
            rdyBtn.SetActive(true);
            startTimer.gameObject.SetActive(false);
            gameTimer.gameObject.SetActive(true);
            gameOverPanel.SetActive(false);
        }

        private void display(bool display) {
            if (display)
                resetWindow();
            duelCamera.gameObject.SetActive(display);
            duelCanvas.gameObject.SetActive(display);
        }

        private void setReady(bool rdy) {
            name.gameObject.SetActive(!rdy);
            nameRdy.gameObject.SetActive(rdy);
        }
        
        private void setOpponentReady(bool rdy) {
            opponentName.gameObject.SetActive(!rdy);
            opponentNameRdy.gameObject.SetActive(rdy);
        }

        public void gotRequest(string from) {
        }

        public void gotResponse(DuelResponseType type) {
        }

        public void duelStarted() {
            startTimer.gameObject.SetActive(false);
            gameTimer.gameObject.SetActive(true);
            running = true;
        }

        public void duelEnded(string winner) {
            this.winner.text = winner;
            gameOverPanel.SetActive(true);
            running = false;
        }

        public void userLeft(string nickname) {
        }

        public void userReadyUp(string nickname) {
            if (nickname.Equals(opponentName.text)) {
                opponentNameRdy.gameObject.SetActive(true);
                opponentName.gameObject.SetActive(false);
            } else {
                nameRdy.gameObject.SetActive(true);
                name.gameObject.SetActive(false);
            }
        }

        public void countSent(string sender, int count) {
            if (sender.Equals(opponentName.text)) {
                opponentScore.text = count.ToString();
                skinManager.spawnBody(opponentBox);
                return;
            }

            score.text = count.ToString();
        }

        public void startTimerChanged(int count) {
            startTimer.gameObject.SetActive(true);
            startTimer.text = count.ToString();
        }

        public void gameTimerChanged(int count) {
            gameTimer.text = count + " s";
        }
    }
    
}