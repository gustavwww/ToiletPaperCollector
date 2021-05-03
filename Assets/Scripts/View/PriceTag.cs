using Services.IAP;
using TMPro;
using UnityEngine;

namespace View {
    public class PriceTag : MonoBehaviour {

        public Purchaser purchaser;
        public TMP_Text label;
        
        void Start() {
            string price = purchaser.getProductPrice(purchaser.goldenPaperID);
            if (price != null && !price.Equals("0")) {
                label.text = price;
            }

            label.text = "3.99 $";
        }
    }
}

