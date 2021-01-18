using System.Collections.Generic;

namespace Model {
    
    public enum GameLevel {
        LEVEL1 = 0,
        LEVEL2 = 1
    }
    
    public class GameModel {

        public static GameLevel LEVEL = GameLevel.LEVEL1;
        
        private static readonly int MAX_BOX = 40;

        private int amount = 0;
        private int boxes;
        
        private readonly IList<ModelListener> listeners = new List<ModelListener>();

        public GameModel() {
            boxes = 0;
        }
        
        public GameModel(int boxes) {
            this.boxes = boxes;
        }
        
        public void addListener(ModelListener listener) {
            listeners.Add(listener);
        }

        private void informObserversBoxFull() {
            foreach (ModelListener listener in listeners) {
                listener.boxFull();
            }
        }

        public void incrementAmount() {
            amount++;
            if (amount >= MAX_BOX) {

                amount = 0;
                boxes++;
                informObserversBoxFull();
            }

        }
        
        public int getBoxes() {
            return boxes;
        }

    }
    
}
