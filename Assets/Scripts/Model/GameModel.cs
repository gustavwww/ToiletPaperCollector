using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
    
    public class GameModel : MonoBehaviour {

        private User user;
        private Level currentLevel = LevelData.LEVEL1;
        private int tempCoins = 0;
        
        private readonly IList<ModelListener> listeners = new List<ModelListener>();

        public void addListener(ModelListener listener) {
            listeners.Add(listener);
        }

        public void setUser(User user) {
            this.user = user;
            checkGameLevel();
        }
        
        public void incrementAmount() {
            tempCoins++;
            if (tempCoins >= currentLevel.getCapacity()) {

                tempCoins = 0;
                user.addAmount(currentLevel.getAmountIncrement());
                informObserversBoxFull();
                checkGameLevel();
            }

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

        private void checkGameLevel() {
            if (user.getWeeklyAmount() >= 4 && currentLevel.getId() == 0) {
                currentLevel = LevelData.LEVEL2;
                informObserversLevelUpdated(currentLevel);
            }
        }

        public Level getLevel() {
            return currentLevel;
        }

        public User getUser() {
            return user;
        }

    }
    
}
