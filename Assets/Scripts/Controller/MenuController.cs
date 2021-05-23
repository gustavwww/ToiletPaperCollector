using System.Collections;
using System.Threading;
using Model;
using Services.IAP;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;

namespace Controller {
    
    public enum Navigation {
        MENU, GAME
    }
    
    public class MenuController : MonoBehaviour, MenuCameraListener, ModelListener {

        public GameModel gameModel;
        
        public Canvas menuCanvas;
        
        public Camera gameCamera;
        public Camera menuCamera;
        private MenuCameraScript menuCameraScript;

        public Canvas gameCanvas;
        public Canvas storeCanvas;
        public GameObject leaderBoardPanel;
        public GameObject duelPanel;

        public TMP_Text name;
        public TMP_Text weeklyScore;
        public TMP_Text totalScore;

        private void Start() {
            menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
            menuCameraScript.addObserver(this);
            
            gameModel.addListener(this);

            name.text = gameModel.getNickName();
            weeklyScore.text = gameModel.getBoxes().ToString();
            totalScore.text = gameModel.getBoxes().ToString();
        }

        public void playPressed() {
            menuCanvas.gameObject.SetActive(false);
            navigate(Navigation.GAME);
        }

        public void backToMainMenuPressed() {
            navigate(Navigation.MENU);
        }

        public void leaderBoardPressed() {
            leaderBoardPanel.SetActive(true);
            leaderBoardPanel.GetComponent<LeaderBoardPanelController>().loadLeaderBoard();
            gameObject.SetActive(false);
        }

        public void duelPressed() {
            duelPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void storePressed() {
            menuCanvas.gameObject.SetActive(false);
            storeCanvas.gameObject.SetActive(true);
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
                    gameCanvas.gameObject.SetActive(true);
                    menuCameraScript.enableAnimator();
                    break;
                
                case Navigation.MENU:
                    gameCanvas.gameObject.SetActive(false);
                    menuCanvas.gameObject.SetActive(true);
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

        public void boxFull() {
            weeklyScore.text = gameModel.getBoxes().ToString();
            totalScore.text = gameModel.getTotalBoxes().ToString();
        }

        public void levelUpdated(GameLevel level) {
        }
    }

}