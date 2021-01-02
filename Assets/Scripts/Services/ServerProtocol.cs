using UnityEngine;

namespace Services {
    
    public static class ServerProtocol {

        public static ServerCommand processMessage(string msg) {
            Debug.Log("Processing message: " + msg);
            
            if (responseLogged(msg)) {
                return new ServerCommand(msg, ServerCommandType.RESPONSE_LOGGED);
            }

            if (responseAmount(msg)) {
                return new ServerCommand(msg, ServerCommandType.RESPONSE_AMOUNT);
            }
        
            if (wantId(msg)) {
                return new ServerCommand(msg, ServerCommandType.GET_ID);
            }

            if (wantNick(msg)) {
                return new ServerCommand(msg, ServerCommandType.GET_NICKNAME);
            }

            if (getError(msg) != null) {
                throw new ServerException(getError(msg));
            }

            return new ServerCommand(msg, ServerCommandType.INVALID);
        }

        public static string writeCount() {
            return "count";
        }

        public static string writeNick(string nickname) {
            return "nickname:" + nickname.Trim();
        }

        public static string writeId(string id) {
            return "id:" + id.Trim();
        }

        public static string wantAmount() {
            return "want:amount";
        }

        public static int parseAmount(string msg) {
            return int.Parse(msg.Substring(7));
        }
    
        private static string getError(string msg) {

            if (msg.StartsWith("error:")) {

                return msg.Substring(6);
            }
            return null;
        }

        private static bool wantId(string msg) {
            return msg.Equals("want:id");
        }

        private static bool wantNick(string msg) {
            return msg.Equals("want:nickname");
        }

        private static bool responseLogged(string msg) {
            return msg.Equals("logged");
        }

        private static bool responseAmount(string msg) {
            return msg.StartsWith("amount:");
        }

    }
    
}
