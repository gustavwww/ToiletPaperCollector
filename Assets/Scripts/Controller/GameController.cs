using System;
using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;
using View;

namespace Controller {
    
    public class GameController : MonoBehaviour, ModelListener {

        public ServerController serverController;
        public SkinManager skinManager;
        public GameModel gameModel;

        public List<GameObject> boxes;

        public TMP_Text score;

        void Start() {
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
