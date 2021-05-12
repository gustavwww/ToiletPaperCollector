using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Controller {
    public class LoginPanelController : MonoBehaviour, ServerControllerListener {

        public ServerController serverController;

        public GameModel gameModel;
        
        public GameObject indicator;
        public Text error;
        public GameObject retryBtn;

        public GameObject nickPanel;
        public GameObject mainMenuPanel;

        private void Start() {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            serverController.addListener(this);
            serverController.connect();
        }

        public void retryPressed() {
            
            
        }

        public void onConnected() {
            serverController.login(SystemInfo.deviceUniqueIdentifier, null);
        }

        public void onLoggedIn(string name, int amount, int totalAmount) {
            gameModel.setNickName(name);
            gameModel.setAmount(amount, totalAmount);
            gameObject.SetActive(false);
            mainMenuPanel.SetActive(true);
        }

        public void onException(Exception e) {
            indicator.SetActive(false);
            retryBtn.SetActive(true);
            error.gameObject.SetActive(true);
        }

        public void onError(string message) {
            gameObject.SetActive(false);
            nickPanel.SetActive(true);
        }
        
    }
}