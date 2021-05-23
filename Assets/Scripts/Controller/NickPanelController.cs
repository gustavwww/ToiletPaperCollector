using System;
using Controller.CommandHandlers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller {
    
    public class NickPanelController : MonoBehaviour, ServerControllerListener, ServerErrorListener {

        public ServerController serverController;
        public ServerErrorHandler serverErrorHandler;
        
        public TMP_InputField input;
        public GameObject indicator;
        public Text error;

        public GameObject mainMenuPanel;

        private void Start() {
            serverErrorHandler.addListener(this);
            serverController.addListener(this);
        }

        public void enterPressed() {
            string nickname = input.text;
            if (string.IsNullOrEmpty(nickname)) { return; }
            
            error.gameObject.SetActive(false);
            indicator.SetActive(true);
            serverController.login(SystemInfo.deviceUniqueIdentifier, nickname);
        }


        public void onConnected() {
        }

        public void onLoggedIn(string name, int coins, int amount, int totalAmount) {
            mainMenuPanel.SetActive(true);
            gameObject.SetActive(false);
            indicator.SetActive(false);
        }

        public void onException(Exception e) {
        }

        public void onError(string message) {
            error.gameObject.SetActive(true);
            error.text = message;
        }
    }
    
    
}