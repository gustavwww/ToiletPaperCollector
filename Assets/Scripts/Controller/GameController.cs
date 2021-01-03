using Model;
using TMPro;
using UnityEngine;

namespace Controller {
    
    public class GameController : MonoBehaviour, ModelListener {

        public GameObject spawnObject;
        public GameObject rigidPaper;
        public GameObject box;
        
        public TMP_Text gamePaperLabel;

        private GameModel gameModel;
        private EmptyBox emptyBoxManager;

        private ServerController server;

        public void setServerController(ServerController server) {
            this.server = server;
        }
        
        void Start() {
            gameModel = new GameModel();
            gameModel.addListener(this);
        
            emptyBoxManager = box.GetComponent<EmptyBox>();
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
            GameObject paper = Instantiate(rigidPaper, getRandomSpawnPos(box), spawnObject.transform.rotation, spawnObject.transform);

            // Add rotation
            Rigidbody rgBody = paper.GetComponent<Rigidbody>();
            rgBody.AddTorque(new Vector3(10000f, 10000f, 1000000f));

            gamePaperLabel.text = gameModel.getBoxes().ToString();
        }

    
        private Vector3 getRandomSpawnPos(GameObject box) {

            Renderer boxRender = box.GetComponent<Renderer>();

            Vector3 origin = box.transform.position;
            float rangeX = boxRender.bounds.size.x / 4;
            float rangeZ = boxRender.bounds.size.z / 4;

            Vector3 randomRange = new Vector3(Random.Range(-rangeX, rangeX), 6, Random.Range(-rangeZ, rangeZ));

            return origin + randomRange;
        }

    }
    
}
