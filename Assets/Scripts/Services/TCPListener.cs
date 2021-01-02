using System;

namespace Services {
    
    public interface TCPListener {
        void messageReceived(string msg);
        void exceptionOccurred(Exception e);
    }
    
}
