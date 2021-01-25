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
            spawner.setLevel(gameModel.getLevel());
            menuController.menuView.setGameAmount(gameModel.getBoxes());
        }
        
        void Start() {
            gameModel = new GameModel();
            gameModel.addListener(this);
        }
        
        public void spawnButtonPressed() {
            if (spawner.isEmptying()) {
                return;
            }
            gameModel.incrementAmount();
            menuController.menuView.setGameAmount(gameModel.getBoxes());
            menuController.menuView.setMainStats(gameModel.getTotalBoxes(), gameModel.getBoxes());
            spawner.spawnBody();
        }
        
        public void boxFull() {
            server.sendTCP("count");
            spawner.emptyBox();
        }

        public void levelUpdated(GameLevel level) {
            // TODO: Move Camera etc.
            
            spawner.setLevel(level);
        }
    }
    
}
