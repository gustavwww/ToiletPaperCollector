using UnityEngine;

namespace View {

    public enum GameLevel {
        LEVEL1 = 1,
        LEVEL2 = 2
    }
    
    public class WorldSpawner : MonoBehaviour {
        
        private GameLevel level = GameLevel.LEVEL1;

        public GameObject spawnObject;
        public GameObject rigidBody;
        public GameObject[] box;
        
        public void setLevel(GameLevel level) {
            this.level = level;
        }
        
        public void spawnBody() {
            GameObject o = Instantiate(rigidBody, getRandomSpawnPos(box[(int) level]), spawnObject.transform.rotation, spawnObject.transform);

            // Add rotation
            Rigidbody rgBody = o.GetComponent<Rigidbody>();
            rgBody.AddTorque(new Vector3(10000f, 10000f, 1000000f));
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