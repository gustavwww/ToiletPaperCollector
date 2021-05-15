using UnityEngine;

namespace Controller {
    
    public class StoreController : MonoBehaviour {


        public Canvas storeCanvas;
        public Canvas menuCanvas;
        
        public void backPressed() {
            storeCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
        }
        
        
    }
}