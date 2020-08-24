namespace Utilities {
    
    public enum ServerCommandType {
        GET_ID,
        GET_NICKNAME,
        RESPONSE_LOGGED,
        RESPONSE_AMOUNT,
        INVALID
    }
    
    public class ServerCommand {

        private readonly string msg;
        private readonly ServerCommandType type;

        public ServerCommand(string msg, ServerCommandType type) {
            this.msg = msg;
            this.type = type;
        }

        public string getMsg() {
            return msg;
        }

        public ServerCommandType getType() {
            return type;
        }

    }
    
}