using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;

namespace Services.Protocol {
    
    class ProtocolReader {
        
        public Command parseMessage(string msg) {
            string trimmed = msg.Trim().ToLower();
            StringBuilder sb = new StringBuilder();

            string command = null;
            List<string> argsList = new List<string>();

            // Loops through every character. Sums them up add adds them either as a command or argument
            // depending on following characters, ":" or ",".
            foreach (char c in trimmed) {
                if (c == ' ') {
                    continue;
                }

                if (c == ':' && command == null) {
                    command = sb.ToString();
                    sb.Clear();
                    continue;
                }

                if (c == ',') {
                    argsList.Add(sb.ToString());
                    sb.Clear();
                    continue;
                }

                sb.Append(c);
            }

            // If command missing arguments and ":", add the remaining characters as command,
            // else add the remaining characters as an argument.
            if (command == null) {
                command = sb.ToString();
            } else {
                argsList.Add(sb.ToString());
            }

            string[] args = new string[argsList.Count];
            for (int i = 0; i < argsList.Count; i++) {
                args[i] = argsList.ElementAt(i);
            }

            return new Command(command, args);
        }


    }

}