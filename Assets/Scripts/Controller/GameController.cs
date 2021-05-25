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
        public TMP_Text toNext;
        public TMP_Text nextLbl;
        public TMP_Text currentLevel;

        void Start() {
            gameModel.addListener(this);
            score.text = gameModel.getBoxes().ToString();
            updateLevelTags(gameModel.getLevel());
        }
        
        public void spawnButtonPressed() {
            gameModel.incrementAmount();
            spawnObjects(gameModel.getLevel().getSpawnAmount());
        }

        private void spawnObjects(int amount) {
            Debug.Log(gameModel.getLevel().getId());
            GameObject box = boxes[gameModel.getLevel().getId()];
            for (int i = 0; i < amount; i++) {
                skinManager.spawnBody(box);
            }
        }

        private void updateLevelTags(Level level) {
            currentLevel.text = "Level " + (level.getId()+1);
            if (level.getId() == 0) {
                toNext.text = LevelData.LEVEL2.getRequirement().ToString();
            } else {
                toNext.gameObject.SetActive(false);
                nextLbl.gameObject.SetActive(false);
            }
            
        }
        
        public void boxFull() {
            serverController.sendTCP("count");
            score.text = gameModel.getBoxes().ToString();
        }

        public void levelUpdated(Level level) {
            updateLevelTags(level);
        }
    }
    
}
