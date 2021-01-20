using System.Collections.Generic;

namespace Model {
    
    public enum GameLevel {
        LEVEL1 = 0,
        LEVEL2 = 1
    }
    
    public class GameModel {

        private static readonly int MAX_BOX = 40;

        private GameLevel level = GameLevel.LEVEL1;
        
        private int amount = 0;
        private int boxes;
        
        private readonly IList<ModelListener> listeners = new List<ModelListener>();

        public GameModel() {
            boxes = 0;
            checkGameLevel();
        }
        
        public GameModel(int boxes) {
            this.boxes = boxes;
            checkGameLevel();
        }

        public void addListener(ModelListener listener) {
            listeners.Add(listener);
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
                informObserversBoxFull();
                checkGameLevel();
            }

        }

        private void checkGameLevel() {
            // TODO: Implement
        }

        public GameLevel getLevel() {
            return level;
        }
        
        public int getBoxes() {
            return boxes;
        }

    }
    
}
