namespace Services.Protocol {
    
    public class ProtocolFactory {

        public static IServerProtocol getServerProtocol() {
            return ProtocolFacade.getInstance();
        }
        
    }
    
    
}