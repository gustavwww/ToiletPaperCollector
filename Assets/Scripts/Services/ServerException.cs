using System;

namespace Services {

    public class ServerException : Exception {

        public ServerException(string message) : base(message) { }

    }
    
}
