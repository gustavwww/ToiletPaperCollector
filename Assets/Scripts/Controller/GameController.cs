using Model;
using UnityEngine;
using View;

namespace Controller {
    
    public class GameController : MonoBehaviour, ModelListener {

        public MenuController menuController;
        public WorldSpawner spawner;

        private ServerController server;
        private GameModel gameModel;

        public void setServerController(ServerController server) {
            this.server = server;
        }

        public void setGameModel(GameModel gameModel) {
            this.gameModel = gameModel;
            gameModel.addListener(this);
            menuController.menuView.setGameAmount(gameModel.getBoxes());
        }
        
        void Start() {
            gameModel = new GameModel();
            gameModel.addListener(this);
        }
        
        public void boxFull() {
            server.sendTCP("count");
            spawner.emptyBox();
        }

        public void spawnButtonPressed() {
            if (spawner.isEmptying()) {
                return;
            }
            gameModel.incrementAmount();
            menuController.menuView.setGameAmount(gameModel.getBoxes());
            spawner.spawnBody();
        }

    }
    
}
