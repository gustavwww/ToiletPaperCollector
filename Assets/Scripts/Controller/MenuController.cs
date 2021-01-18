using UnityEngine;
using View;

namespace Controller {
    
    public enum Navigation {
        MENU, GAME
    }
    
    public class MenuController : MonoBehaviour, MenuCameraListener {

        public GameController gameController;
        
        public MenuView menuView;
        
        public Camera gameCamera;
        public Camera menuCamera;
    
        private MenuCameraScript menuCameraScript;

        public LeaderBoardView lbView;

        // Client
        private string clientId;

        // Server
        private ServerController server;
        
        private void Start() {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            clientId = SystemInfo.deviceUniqueIdentifier;

            server = new ServerController(this, gameController);
            gameController.setServerController(server);
            
            menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
            menuCameraScript.addObserver(this);
            
            menuView.showLoginPanel();
        }

        public void loginPressed() {
            menuView.setLoading(true);
            server.login(clientId, null);
        }
        
        public void playPressed() {
            if (!server.isLoggedIn()) { return; }
            menuView.showMenu(false);
            navigate(Navigation.GAME);
        }

        public void leaderBoardPressed() {
            menuView.showLeaderBoardPanel();
            lbView.loadLeaderBoard();
        }

        public void storePressed() {
            
        }

        public void enterNickPressed() {
            string nickname = menuView.nickInput.text;
            if (string.IsNullOrEmpty(nickname)) { return; }
            
            menuView.setLoading(true);
            server.login(clientId, nickname);
        }

        public void navigate(Navigation to) {
            switch (to) {
                case Navigation.MENU:
                    setMenuCameraActive();
                    menuCameraScript.moveCamera(Navigation.MENU);
                    break;
                
                case Navigation.GAME:
                    menuCameraScript.moveCamera(Navigation.GAME);
                    break;
                }
        }

        public void cameraReached(Navigation nav) {
            switch (nav) {
                case Navigation.GAME:
                    setGameCameraActive();
                    menuView.showGameMenu();
                    break;
                
                case Navigation.MENU:
                    menuView.showMainMenu();
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

    }

}