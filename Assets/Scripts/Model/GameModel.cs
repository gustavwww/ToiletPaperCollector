using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : ServerListener {

    private static readonly int MAX_PAPER_BOX = 40;

    private readonly ServerManager server;

    private int toiletPapers = 0;
    private int toiletPaperBoxes = 0;

    public GameModel() {
        server = new ServerManager();
        server.addObserver(this);
    }

    public void Start() {

        server.connect();
    }

    public void incToiletPaper() {

        toiletPapers++;

        if (toiletPapers >= MAX_PAPER_BOX) {

            toiletPapers = 0;
            toiletPaperBoxes++;
            informObserversBoxFull();
            sendCountPacket();
        }

    }

    private void sendCountPacket() {
        server.sendMessage(ServerProtocol.writeCount());
    }



    public int getBoxes() {
        return toiletPaperBoxes;
    }

    public void commandReceived(ServerCommand cmd) {

        switch (cmd) {

            case ServerCommand.GET_ID:
                server.sendMessage(ServerProtocol.writeId(SystemInfo.deviceUniqueIdentifier));
                break;

            case ServerCommand.GET_NICKNAME:
                server.sendMessage(ServerProtocol.writeNick("SOME NICK"));
                break;

        }

    }

    public void exceptionOccurred(Exception e) {

        if (e.GetType() == typeof(ServerException)) {
            // Server Error
            // TODO: Enum for ServerErrors
        }


    }

    // Observers ------------------
    private IList<ModelListener> observers = new List<ModelListener>();
    public void addObserver(ModelListener listener) {
        observers.Add(listener);
    }

    private void informObserversBoxFull() {

        foreach (ModelListener observer in observers) {

            observer.boxFullOfPaper();
        }

    }

}
