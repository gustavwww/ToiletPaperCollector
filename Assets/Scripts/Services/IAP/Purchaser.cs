using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using View;

namespace Services.IAP {

    public class Purchaser : MonoBehaviour, IStoreListener {


        public WorldSpawner spawner;
        public GameObject goldenPaper;

        private static IStoreController storeController;
        private static IExtensionProvider extensionProvider;

        public readonly string goldenPaperID = "golden_paper";

        void Start() {
            if (storeController == null) {
                initPurchaser();
            }
        }

        private void initPurchaser() {

            if (isInitialized()) {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(goldenPaperID, ProductType.NonConsumable);

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

        public void buyGoldenPaper() {
            buyProduct(goldenPaperID);
        }


        public void OnInitialized(IStoreController controller, IExtensionProvider extension) {
            storeController = controller;
            extensionProvider = extension;
            setupPurchased();

            Debug.Log("Purchaser initialized");
        }

        public void OnInitializeFailed(InitializationFailureReason error) {
            Debug.Log("Purchaser initialize failed: " + error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {

            if (string.Equals(args.purchasedProduct.definition.id, goldenPaperID)) {
                spawner.rigidBody = goldenPaper;
                PlayerPrefs.SetInt("golden_paper", 1);
                Debug.Log("Paper purchased");
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
            Debug.Log("Purchase failed: " + failureReason);
        }

        private bool hasBoughtProduct(string id) {
            if (!isInitialized()) {
                return false;
            }

            Product p = storeController.products.WithID(id);
            return p != null && p.hasReceipt;
        }

        private void setupPurchased() {

            if (hasBoughtProduct(goldenPaperID) || PlayerPrefs.GetInt("golden_paper") == 1) {
                spawner.rigidBody = goldenPaper;
            }

        }

        public string getProductPrice(string productID) {
            if (storeController != null) {
                return storeController.products.WithID(productID).metadata.localizedPriceString;
            }

            return null;
        }

        public void restorePurchases() {
            
            if (!isInitialized()) {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer) {

                var apple = extensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result) => {
                    Debug.Log("Restoring purchase: " + result);
                });


            } else {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
            
        }

    }
}
