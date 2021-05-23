using System;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controller {
    
    public class DuelRequestController : MonoBehaviour, DuelCommandListener {

        public DuelCommandHandler duelCommandHandler;
        public GameModel gameModel;

        public Canvas mainMenuCanvas;
        public GameObject mainMenuPanel;

        public DuelController duelController;
        public Canvas duelCanvas;
        public Camera duelCamera;
        
        public GameObject requestLabel;
        public GameObject waitingLabel;

        public TMP_InputField nickInput;
        public GameObject indicator;
        public Text error;
        public GameObject sendButton;

        private string requestName = "";
        
        private void Start() {
            duelCommandHandler.addListener(this);
        }

        public void sendPressed() {
            requestName = nickInput.text;
            if (string.IsNullOrEmpty(requestName)) { return; }
            
            setWaitState(true);
            duelCommandHandler.sendRequest(requestName);
        }

        private void setWaitState(bool set) {
            indicator.SetActive(set);
            error.gameObject.SetActive(!set);
            requestLabel.SetActive(!set);
            waitingLabel.SetActive(set);
            nickInput.gameObject.SetActive(!set);
            sendButton.SetActive(!set);
        }

        private void joinDuel() {
            duelController.instantiateDuel(gameModel.getNickName(), requestName);
            mainMenuCanvas.gameObject.SetActive(false);
            duelCanvas.gameObject.SetActive(true);
            duelCamera.gameObject.SetActive(true);
            closeWindow();
        }

        private void closeWindow() {
            setWaitState(false);
            gameObject.SetActive(false);
            mainMenuPanel.SetActive(true);
        }

        public void gotResponse(DuelResponseType type) {
            if (type == DuelResponseType.NOTFOUND) {
                error.text = "Player could not be found. Try again.";
                setWaitState(false);
            } else if (type == DuelResponseType.CANCELLED) {
                error.text = "Player declined.";
                setWaitState(false);
            } else {
                setWaitState(false);
                error.gameObject.SetActive(false);
                
                joinDuel();
            }
        }
        
        public void gotRequest(string from) {
        }

        public void duelStarted() {
        }

        public void duelEnded(string winner) {
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