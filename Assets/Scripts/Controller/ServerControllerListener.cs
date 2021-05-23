using System;

namespace Controller {
    
    public interface ServerControllerListener {
        public void onConnected();
        public void onLoggedIn(string name, int coins, int amount, int totalAmount);
        
    }
    
    
}