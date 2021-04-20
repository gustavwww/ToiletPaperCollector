using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.IAP {

    public class Purchaser : MonoBehaviour, IStoreListener {


        private static IStoreController storeController;
        private static IExtensionProvider extensionProvider;
        
        private string goldenPaperID = "golden_paper";
        
        
        void Start() {
            if (storeController == null) {
                initPurchaser();
            }
        }

        private void initPurchaser() {

            if (isInitialized) { return; }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.addProduct(goldenPaperID, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }


        private bool isInitialized() {
            return storeController != null && extensionProvider != null;
        }

        private void buyProduct(string productId) {

            if (!isInitialized()) {
                return;
            }

            Product product = storeController.products.WithID(productId);

            if (product == null || !product.availableToPurchase) {
                return;
            }

            
            storeController.InitiatePurchase(product);
        }


        public void OnInitialized(IStoreController controller, IExtensionProvider extension) {
            storeController = controller;
            extensionProvider = extension;
            Debug.Log("Purchaser initialized");
        }

        public void OnInitializeFailed(InitializationFailureReason error) {
            Debug.Log("Purchaser initialize failed: " + error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
            
            if(string.Equals(args.purchasedProduct.definition.id, goldenPaperID)) {
                //TODO: Activate paper.
                Debug.Log("Paper purchased");
            }
            
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
            Debug.Log("Purchase failed: " + failureReason);
        }

    }

}
