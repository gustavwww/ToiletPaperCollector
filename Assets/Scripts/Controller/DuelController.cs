using System;
using Model;
using TMPro;
using UnityEngine;
using View;

namespace Controller {
    
    public class DuelController : MonoBehaviour, DuelCommandListener {

        public DuelCommandHandler duelCommandHandler;

        public Canvas mainMenuCanvas;
        public Camera duelCamera;
        
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
        
        private int amount;
        private int opponentAmount;

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
            duelCamera.gameObject.SetActive(false);
            mainMenuCanvas.gameObject.SetActive(true);
            rdyBtn.SetActive(true);
            gameOverPanel.SetActive(false);
        }

        public void instantiateDuel(string name, string opponentName) {
            amount = 0;
            opponentAmount = 0;
            
            this.name.text = name;
            nameRdy.text = name;
            this.opponentName.text = opponentName;
            opponentNameRdy.text = opponentName;
            winner.text = "";
            refreshScore();
        }

        private void refreshScore() {
            score.text = amount.ToString();
            opponentScore.text = opponentAmount.ToString();
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
                opponentAmount++;
                skinManager.spawnBody(opponentBox);
            } else {
                amount++;
            }
            refreshScore();
        }

        public void startTimerChanged(int count) {
            startTimer.gameObject.SetActive(true);
            startTimer.text = count.ToString();
        }

        public void gameTimerChanged(int count) {
            gameTimer.text = count.ToString();
        }
    }
    
}