using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    
    public enum GameLevel {
        LEVEL1 = 0,
        LEVEL2 = 1
    }
    
    public class GameModel : MonoBehaviour {

        private static readonly int MAX_BOX = 40;

        private GameLevel level = GameLevel.LEVEL1;

        private string nickname;
        
        private int amount = 0;
        private int boxes;
        private int totalBoxes;
        
        private readonly IList<ModelListener> listeners = new List<ModelListener>();

        public void addListener(ModelListener listener) {
            listeners.Add(listener);
        }

        private void Start() {
            boxes = 0;
            totalBoxes = 0;
        }
        
        private void informObserversBoxFull() {
            foreach (ModelListener listener in listeners) {
                listener.boxFull();
            }
        }
        
        private void informObserversLevelUpdated(GameLevel newLevel) {
            foreach (ModelListener listener in listeners) {
                listener.levelUpdated(newLevel);
            }
        }

        public void incrementAmount() {
            amount++;
            if (amount >= MAX_BOX) {

                amount = 0;
                boxes++;
                totalBoxes++;
                informObserversBoxFull();
            }

        }

        private void checkGameLevel() {
        }

        public GameLevel getLevel() {
            return level;
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
        
        public void setAmount(int amount, int totalAmount) {
            this.boxes = amount;
            this.totalBoxes = totalAmount;
        }

        public void setNickName(string nickname) {
            this.nickname = nickname;
        }

    }
    
}
