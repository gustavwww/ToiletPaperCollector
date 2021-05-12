using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View {
    public class SkinManager : MonoBehaviour {

        public List<GameObject> skins;
        private GameObject selectedSkin;

        private Transform reference;

        private void Start() {
            selectedSkin = skins[0];
            reference = gameObject.transform;
        }

        public void spawnBody(GameObject box) {
            GameObject o = Instantiate(selectedSkin, getRandomSpawnPos(box), reference.rotation, reference);

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
        
        public GameObject getSkin() {
            return selectedSkin;
        }

        public void setSkin(int index) {
            selectedSkin = skins[index];
        }
        
    }
}