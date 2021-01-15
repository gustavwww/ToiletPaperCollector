using Model;
using UnityEngine;
using View;

namespace Controller {
    
    public class GameController : MonoBehaviour, ModelListener {

        public MenuController menuController;
        public WorldSpawner spawner;
        public EmptyBox emptyBoxManager;
        
        private GameModel gameModel;
        private ServerController server;

        public void setServerController(ServerController server) {
            this.server = server;
        }
        
        void Start() {
            gameModel = new GameModel();
            gameModel.addListener(this);
        }
        
        public void boxFull() {
            server.sendTCP("count");
            emptyBoxManager.empty();
        }

        public void spawnButtonPressed() {
            if (emptyBoxManager.isEmptying()) {
                return;
            }
            gameModel.incrementAmount();
            menuController.menuView.setGameAmount(gameModel.getBoxes());
            spawner.spawnBody();
        }

    }
    
}
