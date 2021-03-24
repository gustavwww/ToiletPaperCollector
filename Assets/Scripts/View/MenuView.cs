using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View {
    
    public class MenuView : MonoBehaviour {

        public Canvas menuCanvas;
        public GameObject menuPanel;
        public TMP_Text name;
        public TMP_Text weeklyScore;
        public TMP_Text totalScore;

        public GameObject loginPanel;
        public GameObject loginIndicator;
        public Text loginError;
        public GameObject retryBtn;
        
        public GameObject nickPanel;
        public GameObject nickIndicator;
        public TMP_InputField nickInput;
        public Text nickError;

        public GameObject LBPanel;

        public Canvas gameCanvas;
        public TMP_Text gameLabel;

        public Canvas storeCanvas;
        
        private CanvasGroup menuCanvasGroup;
        
        private void Start() {
            menuCanvasGroup = menuCanvas.GetComponent<CanvasGroup>();
            showMainMenu();
        }

        public void showGameMenu() {
            gameCanvas.gameObject.SetActive(true);
            menuCanvas.gameObject.SetActive(false);
            storeCanvas.gameObject.SetActive(false);
        }

        public void showMenu(bool show) {
            menuCanvas.gameObject.SetActive(show);
        }
        
        public void showMainMenu() {
            gameCanvas.gameObject.SetActive(false);
            storeCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            menuPanel.SetActive(true);
            nickPanel.SetActive(false);
            LBPanel.SetActive(false);
            loginPanel.SetActive(false);
            setLoading(false);
            displayError(false);
        }

        public void showLoginPanel() {
            menuPanel.SetActive(false);
            LBPanel.SetActive(false);
            nickPanel.SetActive(false);
            loginPanel.SetActive(true);
            setLoading(false);
            displayError(false);
        }
        
        public void showNickPanel() {
            menuPanel.SetActive(false);
            LBPanel.SetActive(false);
            loginPanel.SetActive(false);
            nickPanel.SetActive(true);
            setLoading(false);
            displayError(false);
        }

        public void showLeaderBoardPanel() {
            menuPanel.SetActive(false);
            nickPanel.SetActive(false);
            loginPanel.SetActive(false);
            LBPanel.SetActive(true);
            setLoading(false);
            displayError(false);
        }

        public void showStoreCanvas() {
            gameCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(false);
            storeCanvas.gameObject.SetActive(true);
        }

        public void setMainStats(string name, int totalScore, int weeklyScore) {
            this.name.text = name;
            this.totalScore.text = totalScore.ToString();
            this.weeklyScore.text = weeklyScore.ToString();
        }
        
        public void setMainStats(int totalScore, int weeklyScore) {
            this.totalScore.text = totalScore.ToString();
            this.weeklyScore.text = weeklyScore.ToString();
        }
        
        public void setLoading(bool isLoading) {
            displayError(!isLoading);
            nickIndicator.SetActive(isLoading);
            loginIndicator.SetActive(isLoading);
            menuCanvasGroup.interactable = !isLoading;
        }
        
        public void displayError(string error) {
            nickError.gameObject.SetActive(true);
            nickError.text = error;
            loginError.gameObject.SetActive(true);
            loginError.text = error;
            retryBtn.SetActive(true);
        }

        public void displayError(bool display) {
            nickError.gameObject.SetActive(display);
            nickError.text = "Invalid nickname, try again.";
            loginError.gameObject.SetActive(display);
            loginError.text = "Could not connect to server, try again.";
            retryBtn.SetActive(display);
        }

        public void setGameAmount(int amount) {
            gameLabel.text = amount.ToString();
        }


    }
    
    
}