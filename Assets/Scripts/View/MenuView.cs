using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View {
    
    public class MenuView : MonoBehaviour {

        public Canvas menuCanvas;
        public GameObject menuPanel;
        public GameObject menuIndicator;
        public Text menuError;

        public GameObject loginPanel;
        public GameObject loginIndicator;
        public Text loginError;
        
        public GameObject nickPanel;
        public GameObject nickIndicator;
        public TMP_InputField nickInput;
        public Text nickError;

        public GameObject LBPanel;

        public Canvas gameCanvas;
        public TMP_Text gameLabel;
        
        private CanvasGroup menuCanvasGroup;
        
        private void Start() {
            menuCanvasGroup = menuCanvas.GetComponent<CanvasGroup>();
            showMainMenu();
        }

        public void showGameMenu() {
            gameCanvas.gameObject.SetActive(true);
            menuCanvas.gameObject.SetActive(false);
        }

        public void showMenu(bool show) {
            menuCanvas.gameObject.SetActive(show);
        }
        
        public void showMainMenu() {
            gameCanvas.gameObject.SetActive(false);
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
        
        public void setLoading(bool isLoading) {
            displayError(!isLoading);
            menuIndicator.SetActive(isLoading);
            nickIndicator.SetActive(isLoading);
            loginIndicator.SetActive(isLoading);
            menuCanvasGroup.interactable = !isLoading;
        }
        
        public void displayError(string error) {
            nickError.gameObject.SetActive(true);
            nickError.text = error;
            menuError.gameObject.SetActive(true);
            menuError.text = error;
            loginError.gameObject.SetActive(true);
            loginError.text = error;
        }

        public void displayError(bool display) {
            nickError.gameObject.SetActive(display);
            nickError.text = "Invalid nickname, try again.";
            menuError.gameObject.SetActive(display);
            menuError.text = "Could not connect to server, try again.";
            loginError.gameObject.SetActive(display);
            loginError.text = "Could not connect to server, try again.";
        }

        public void setGameAmount(int amount) {
            gameLabel.text = amount.ToString();
        }


    }
    
    
}