using System.Text;

namespace Services.Protocol {
    
    class ProtocolWriter {

        public string writeMessage(Command cmd) {
            
            StringBuilder sb = new StringBuilder();

            sb.Append(cmd.getCmd()).Append(":");
            foreach (string arg in cmd.getArgs()) {
                sb.Append(arg).Append(",");
            }
            if (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == ':') {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }
        
    }
    
}