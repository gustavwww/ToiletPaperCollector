namespace Services.Protocol {

    public class Command {

        private readonly string cmd;
        private readonly string[] args;

        public Command(string cmd, params string[] args) {
            this.cmd = cmd;
            this.args = args;
        }

        public string getCmd() {
            return cmd;
        }

        public string[] getArgs() {
            return args;
        }

    }

}