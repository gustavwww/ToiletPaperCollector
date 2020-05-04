using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel {


    private int toiletPapers = 0;
    private static readonly int MAX_PAPER_BOX = 50;

    private IList observers = new List<ModelListener>();

    public GameModel() {
        // Connect to server.

    }

    public void IncToiletPaper() {

        toiletPapers++;
        SendPaperPacket();

        if (toiletPapers >= MAX_PAPER_BOX) {

            toiletPapers = 0;
            InformObserversBoxFull();
        }


    }

    private void SendPaperPacket() {
        // Send paper packet to server.

    }

    public void AddObserver(ModelListener listener) {

        observers.Add(listener);
    }

    private void InformObserversBoxFull() {

        foreach(ModelListener observer in observers) {

            observer.BoxFullOfPaper();
        }

    }
}
