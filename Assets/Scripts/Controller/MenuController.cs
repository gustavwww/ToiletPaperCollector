using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Controller {
    public enum Navigation {
        MENU, GAME
    }

    public class MenuController : MonoBehaviour, MenuCameraListener, ServerListener {

        // Cameras
        public Camera gameCamera;
        public Camera menuCamera;
    
        private MenuCameraScript menuCameraScript;
    
        // Menu Canvas
        public Canvas menuCanvas;
        public GameObject menuPanel;
        public GameObject menuIndicator;
        public GameObject nickPanel;
        public GameObject nickIndicator;
        public TMP_InputField nickInput;
        public Text nickError;
    
        // Game Canvas
        public Canvas gameCanvas;
        public TMP_Text gamePaperLabel;

        // Server
        private ServerManager server;

        // Client
        private string clientId;
        
        private void Start() {
            clientId = SystemInfo.deviceUniqueIdentifier;
            
            menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
            menuCameraScript.addObserver(this);
        
            server = ServerManager.getInstance();
            server.addObserver(this);
        
            setMenuCameraActive();
            resetMenu();
        }
        
        private void OnApplicationQuit() {
            ServerManager.getInstance().close();
        }
    
        // Actions ------------------
        public void playPressed() {
            turnOnIndicator(menuIndicator);
        
            server.connect();
        }

        public void storePressed() {
        
        

        }

        public void aboutPressed() {


        }

        public void enterNickPressed() {
            turnOnIndicator(nickIndicator);
            string nickname = nickInput.text;
        
            if (string.IsNullOrEmpty(nickname)) {
                return;
            }
        
            server.sendMessage(ServerProtocol.writeNick(nickname));
        }

        // --------------------------
    
        // Camera Handling ----------------
        public void cameraReached(Navigation nav) {
            switch (nav) {
                case Navigation.GAME:
                    setGameCameraActive();
                    break;

            }
        
        }

        private void setGameCameraActive() {
            gameCamera.gameObject.SetActive(true);
            menuCamera.gameObject.SetActive(false);
        }
    
        private void setMenuCameraActive() {
            gameCamera.gameObject.SetActive(false);
            menuCamera.gameObject.SetActive(true);
        }
    
        // -----------------------------------
    
        // Menu Handling -------------------

        private void hideMenu() {
            menuCanvas.gameObject.SetActive(false);
        }
        
        private void resetMenu() {
            menuCanvas.gameObject.SetActive(true);
            menuPanel.SetActive(true);
            nickPanel.SetActive(false);
            turnOffIndicators();
        }

        private void showNickMenu() {
            menuPanel.SetActive(false);
            nickPanel.SetActive(true);
            showNickError(false);
            turnOffIndicators();
        }

        private void showNickError(bool show) {
            nickError.gameObject.SetActive(show);
        }

        private void turnOffIndicators() {
            menuIndicator.SetActive(false);
            nickIndicator.SetActive(false);
            menuCanvas.GetComponent<CanvasGroup>().interactable = true;
        }

        private void turnOnIndicator(GameObject indicator) {
            indicator.SetActive(true);
            menuCanvas.GetComponent<CanvasGroup>().interactable = false;
        }

        // --------------------------
    
    
        // Server response -----------------
        public void commandReceived(ServerCommand cmd) {
            switch (cmd.getType()) {

                case ServerCommandType.GET_ID:
                    server.sendMessage(ServerProtocol.writeId(clientId));
                    break;

                case ServerCommandType.GET_NICKNAME:
                    UnityMainThread.instance.addJob(() => {
                        showNickMenu();
                        turnOffIndicators();
                    });
                    break;
            
                case ServerCommandType.RESPONSE_LOGGED:
                    server.sendMessage(ServerProtocol.wantAmount());
                    UnityMainThread.instance.addJob(() => {
                        menuCameraScript.moveCamera(Navigation.GAME);
                        resetMenu();
                        hideMenu();
                    });
                    break;

                case ServerCommandType.RESPONSE_AMOUNT:
                    UnityMainThread.instance.addJob(() => {
                        gamePaperLabel.text = ServerProtocol.parseAmount(cmd.getMsg()).ToString();
                    });
                    break;
            }
        }

        public void exceptionOccurred(Exception e) {
            Debug.Log("Error occurred: " + e.Message);
            UnityMainThread.instance.addJob(turnOffIndicators);
            
            if (e.GetType() == typeof(ServerException)) {
                UnityMainThread.instance.addJob(() => {
                    nickError.text = e.Message;
                    showNickError(true);
                });
                
            }
        }

        // ----------------------

    }

}