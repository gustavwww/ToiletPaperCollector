
namespace Services.Protocol {
    
    class ProtocolFacade : IServerProtocol {

        private static ProtocolFacade instance = null;

        public static ProtocolFacade getInstance() {
            if (instance == null) {
                instance = new ProtocolFacade();
            }

            return instance;
        }

        private readonly ProtocolReader protocolReader;
        private readonly ProtocolWriter protocolWriter;
        
        private ProtocolFacade() {
            protocolReader = new ProtocolReader();
            protocolWriter = new ProtocolWriter();
        }


        public Command parseMessage(string msg) {
            return protocolReader.parseMessage(msg);
        }

        public string writeMessage(Command cmd) {
            return protocolWriter.writeMessage(cmd);
        }

    }
    
}