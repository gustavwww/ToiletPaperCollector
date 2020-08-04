using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private IList observers = new List<ModelListener>();

    public void addObserver(ModelListener listener) {

        observers.Add(listener);
    }

    private void informObserversBoxFull() {

        foreach(ModelListener observer in observers) {

            observer.boxFullOfPaper();
        }

    }

    public int getBoxes() {
        return toiletPaperBoxes;
    }

}
