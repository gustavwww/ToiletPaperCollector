using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller {
    
    public enum Navigation {
        MENU, GAME
    }
    
    public class MenuController : MonoBehaviour, MenuCameraListener {

        public GameController gameController;
        
        // Cameras
        public Camera gameCamera;
        public Camera menuCamera;
    
        private MenuCameraScript menuCameraScript;
    
        // Menu Canvas
        public Canvas menuCanvas;
        public GameObject menuPanel;
        public GameObject menuIndicator;
        public Text menuError;
        
        public GameObject nickPanel;
        public GameObject nickIndicator;
        public TMP_InputField nickInput;
        public Text nickError;
    
        // Game Canvas
        public Canvas gameCanvas;

        // Client
        private string clientId;

        private ServerController server;
        
        private void Start() {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            clientId = SystemInfo.deviceUniqueIdentifier;

            server = new ServerController(this);
            gameController.setServerController(server);
            
            menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
            menuCameraScript.addObserver(this);
            
            setMenuCameraActive();
            resetMenu();
        }
        
        private void OnApplicationQuit() {
            
        }
    
        // Actions ------------------
        public void playPressed() {
            hideErrors();
            turnOnIndicator(menuIndicator);
            
            server.login(clientId, null);
        }

        public void leaderBoardPressed() {
        
        

        }

        public void storePressed() {


        }

        public void enterNickPressed() {
            string nickname = nickInput.text;
            if (string.IsNullOrEmpty(nickname)) { return; }
            
            hideErrors();
            turnOnIndicator(nickIndicator);
            server.login(clientId, nickname);
        }
        // --------------------------

        public void navigate(Navigation to) {
            switch (to) {
                case Navigation.MENU:
                    resetMenu();
                    menuCameraScript.moveCamera(Navigation.MENU);
                    break;
                
                case Navigation.GAME:
                    hideMenuCanvas();
                    menuCameraScript.moveCamera(Navigation.GAME);
                    break;
                }
            
        }
        
        // Camera Handling ----------------
        public void cameraReached(Navigation nav) {
            switch (nav) {
                case Navigation.GAME:
                    setGameCameraActive();
                    showGameCanvas();
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
        public void hideMenuCanvas() {
            menuCanvas.gameObject.SetActive(false);
        }

        public void resetMenu() {
            gameCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            menuPanel.SetActive(true);
            nickPanel.SetActive(false);
            hideErrors();
            turnOffIndicators();
        }

        public void showNickMenu() {
            menuPanel.SetActive(false);
            nickPanel.SetActive(true);
            hideErrors();
            turnOffIndicators();
        }

        public void showNickError(bool show) {
            nickError.gameObject.SetActive(show);
        }

        public void showMenuError(bool show) {
            menuError.gameObject.SetActive(show);
        }

        public void hideErrors() {
            showNickError(false);
            showMenuError(false);
        }

        public void turnOffIndicators() {
            menuIndicator.SetActive(false);
            nickIndicator.SetActive(false);
            menuCanvas.GetComponent<CanvasGroup>().interactable = true;
        }

        public void turnOnIndicator(GameObject indicator) {
            indicator.SetActive(true);
            menuCanvas.GetComponent<CanvasGroup>().interactable = false;
        }

        private void showGameCanvas() {
            gameCanvas.gameObject.SetActive(true);
        }

    }

}