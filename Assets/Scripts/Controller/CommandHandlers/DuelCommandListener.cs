using System;

namespace Controller {
    
    public interface DuelCommandListener {
        void gotRequest(String from);
        void gotResponse(DuelResponseType type);
        void duelStarted();
        void duelEnded(String winner);
        void userReadyUp(String nickname);
        void countSent(String sender, int count);
        void startTimerChanged(int count);
        void gameTimerChanged(int count);
    }
    
}