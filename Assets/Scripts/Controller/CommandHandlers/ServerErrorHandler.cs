using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.CommandHandlers {
    
    public class ServerErrorHandler : MonoBehaviour {

        private readonly List<ServerErrorListener> listeners = new List<ServerErrorListener>();

        public void addListener(ServerErrorListener listener) {
            listeners.Add(listener);
        }

        public void removeListener(ServerErrorListener listener) {
            listeners.Remove(listener);
        }
        
        public void handleServerError(string message) {
            foreach (ServerErrorListener l in listeners) {
                l.onError(message);
            }
        }

        public void handleServerException(Exception e) {
            foreach (ServerErrorListener l in listeners) {
                l.onException(e);
            }
        }
        
    }
    
}