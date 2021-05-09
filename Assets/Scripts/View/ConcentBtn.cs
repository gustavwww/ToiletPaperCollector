using UnityEngine;

public class ConcentBtn : MonoBehaviour {
    
    private void Start() {
        if (PlayerPrefs.GetInt("concent") == 1) {
            gameObject.SetActive(false);
        }
    }

    public void acceptPressed() {
        PlayerPrefs.SetInt("concent", 1);
        gameObject.SetActive(false);
    }
    
}
