using System;

namespace Utilities {
    
    public interface ServerListener {
        void commandReceived(ServerCommand cmd);
        void exceptionOccurred(Exception e);
    }
    
}
