using System;

namespace Services {
    
    public interface ServerListener {
        void commandReceived();
        void exceptionOccurred(Exception e);
    }
    
}
