namespace Controller.CommandHandlers {
    
    public interface DuelCommandListener {
        void gotRequest(string from);
        void gotResponse(DuelResponseType type);
        void duelStarted();
        void duelEnded(string winner);
        void userLeft(string nickname);
        void userReadyUp(string nickname);
        void countSent(string sender, int count);
        void startTimerChanged(int count);
        void gameTimerChanged(int count);
    }
    
}