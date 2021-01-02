using Protocol;

namespace Services.Protocol {
    
    public interface IServerProtocol {
        Command parseMessage(string msg);
        string writeMessage(Command cmd);
    }
    
}