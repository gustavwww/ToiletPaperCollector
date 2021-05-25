using System;
using Model;
using UnityEngine;

namespace View.Camera {
    
    public class GameCameraScript : MonoBehaviour, ModelListener {

        public GameModel gameModel;

        private Animator animator;
        private Level currentPos = LevelData.LEVEL1;
        
        private void Start() {
            gameModel.addListener(this);
            animator = gameObject.GetComponent<Animator>();
        }

        private void checkCameraPosition(Level level) {
            if (currentPos == level) {
                return;
            }
            if (level.getId() == 1) {
                animator.SetTrigger("ToLevel2");
            }

            currentPos = level;
        }
        
        public void boxFull() {
        }

        public void levelUpdated(Level level) {
            checkCameraPosition(level);
        }
        
    }
}