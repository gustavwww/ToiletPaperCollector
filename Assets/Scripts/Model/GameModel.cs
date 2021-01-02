using System.Collections.Generic;
using Utilities;

namespace Model {
    
    public class GameModel {

        private static readonly int MAX_PAPER_BOX = 40;

        private int toiletPapers = 0;
        private int toiletPaperBoxes = 0;

        public GameModel() {
        }

        public void incToiletPaper() {

            toiletPapers++;

            if (toiletPapers >= MAX_PAPER_BOX) {

                toiletPapers = 0;
                toiletPaperBoxes++;
                informObserversBoxFull();
            }

        }



        public int getBoxes() {
            return toiletPaperBoxes;
        }

        // Observers -----------------------------------------------
        private IList<ModelListener> observers = new List<ModelListener>();
        public void addObserver(ModelListener listener) {
            observers.Add(listener);
        }

        private void informObserversBoxFull() {

            foreach (ModelListener observer in observers) {

                observer.boxFull();
            }

        }

    }
    
}
