using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    
    public class GameModel : MonoBehaviour {

        private Level currentLevel = LevelData.LEVEL1;

        private string nickname;
        
        private int amount = 0;
        private int boxes = 0;
        private int totalBoxes = 0;
        
        private readonly IList<ModelListener> listeners = new List<ModelListener>();

        public void addListener(ModelListener listener) {
            listeners.Add(listener);
        }

        private void informObserversBoxFull() {
            foreach (ModelListener listener in listeners) {
                listener.boxFull();
            }
        }
        
        private void informObserversLevelUpdated(Level newLevel) {
            foreach (ModelListener listener in listeners) {
                listener.levelUpdated(newLevel);
            }
        }

        public void incrementAmount() {
            amount++;
            if (amount >= currentLevel.getCapacity()) {

                amount = 0;
                boxes += currentLevel.getBoxIncrement();
                totalBoxes += currentLevel.getBoxIncrement();
                informObserversBoxFull();
                checkGameLevel();
            }

        }
        
        public void setAmount(int amount, int totalAmount) {
            boxes = amount;
            totalBoxes = totalAmount;
            checkGameLevel();
        }

        public void setNickName(string nickname) {
            this.nickname = nickname;
        }

        private void checkGameLevel() {
            if (boxes >= 4 && currentLevel.getId() == 0) {
                currentLevel = LevelData.LEVEL2;
                informObserversLevelUpdated(currentLevel);
            }
        }

        public Level getLevel() {
            return currentLevel;
        }

        public string getNickName() {
            return nickname;
        }
        
        public int getBoxes() {
            return boxes;
        }

        public int getTotalBoxes() {
            return totalBoxes;
        }

    }
    
}
