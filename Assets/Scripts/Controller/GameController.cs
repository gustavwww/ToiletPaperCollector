using System;
using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;
using View;

namespace Controller {
    
    public class GameController : MonoBehaviour, ModelListener, ServerControllerListener {

        public ServerController serverController;
        public SkinManager skinManager;
        public GameModel gameModel;

        public List<GameObject> boxes;

        public TMP_Text score;
        
        public void onConnected() {
        }

        public void onLoggedIn(string name, int coins, int amount, int totalAmount) {
        }

        public void onException(Exception e) {
        }

        public void onError(string message) {
        }

        void Start() {
            serverController.addListener(this);
            gameModel.addListener(this);

            score.text = gameModel.getBoxes().ToString();
        }
        
        public void spawnButtonPressed() {
            gameModel.incrementAmount();
            skinManager.spawnBody(boxes[0]);
        }
        
        public void boxFull() {
            serverController.sendTCP("count");
            score.text = gameModel.getBoxes().ToString();
        }

        public void levelUpdated(GameLevel level) {
        }
    }
    
}
