using System;

namespace Controller.CommandHandlers {
    
    public interface ServerErrorListener {
        void onException(Exception e);
        void onError(string message);
    }
    
    
}