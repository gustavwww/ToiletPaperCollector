using System;
using Controller.CommandHandlers;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.Duel {
    
    public class DuelRequestController : MonoBehaviour, DuelCommandListener, ServerErrorListener {

        public DuelCommandHandler duelCommandHandler;
        public ServerErrorHandler serverErrorHandler;
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
        public GameObject backBtn;

        private string requestName = "";
        
        private void Start() {
            duelCommandHandler.addListener(this);
            serverErrorHandler.addListener(this);
        }

        public void sendPressed() {
            requestName = nickInput.text;
            if (string.IsNullOrEmpty(requestName)) { return; }
            
            setWaitState(true);
            duelCommandHandler.sendRequest(requestName);
        }

        public void backPressed() {
            closeWindow();
        }

        private void setWaitState(bool set) {
            indicator.SetActive(set);
            error.gameObject.SetActive(!set);
            requestLabel.SetActive(!set);
            waitingLabel.SetActive(set);
            nickInput.gameObject.SetActive(!set);
            sendButton.SetActive(!set);
            backBtn.SetActive(!set);
        }

        private void joinDuel() {
            duelController.joinDuel(gameModel.getNickName(), requestName);
            closeWindow();
        }

        private void closeWindow() {
            setWaitState(false);
            error.gameObject.SetActive(false);
            
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

                joinDuel();
            }
        }
        
        public void gotRequest(string from) {
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

        public void onException(Exception e) {
        }

        public void onError(string message) {
            setWaitState(false);
            error.text = message;
        }
        
    }
    
}