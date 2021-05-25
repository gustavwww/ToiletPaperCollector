using System;
using Model;
using UnityEngine;

namespace View {
    public class DestroyBox : MonoBehaviour, ModelListener {

        public GameModel gameModel;
        
        public float timeToDestroy = 3.0f;
        private bool destroying = false;

        private void OnTriggerStay(Collider other) {
            if (other.CompareTag("Entity") && destroying) {
                Destroy(other.gameObject);
            }
        }

        private float timeLeft;

        private void Start() {
            gameModel.addListener(this);
            timeLeft = timeToDestroy;
        }

        private void Update() {
            if (destroying) {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0.0f) {
                    destroying = false;
                    timeLeft = timeToDestroy;
                }
            }
        }

        public void boxFull() {
            destroying = true;
        }

        public void levelUpdated(Level level) {
        }
    }
    
}

