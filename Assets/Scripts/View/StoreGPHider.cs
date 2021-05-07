using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View {
    public class StoreGPHider : MonoBehaviour {

        public Button purchaseBtn;
        public TMP_Text btnText;
        
        void Start() {
            if (PlayerPrefs.GetInt("golden_paper") == 1) {
                btnText.text = "Purchased";
               btnText.color = new Color(0.33f, 1f, 0.35f);
                purchaseBtn.interactable = false;

            }
        }
        
    }
}

