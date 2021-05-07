using UnityEngine;

namespace View {
    public class Fader : MonoBehaviour {

        public float timeTillFade;
        
        private Renderer meshRenderer;

        private bool fade = false;
        private float timeToFade = 3.0f;
        private Color nonAlphaColor;
        private Color alphaColor;
        void Start() {
            meshRenderer = gameObject.GetComponent<Renderer>();
            alphaColor = meshRenderer.material.color;
            alphaColor.a = 0;
            nonAlphaColor = meshRenderer.material.color;
        }
        
        void Update() {

            timeTillFade -= Time.deltaTime;
            if (timeTillFade <= 0.0f) {
                fade = true;
            }
            
            if (fade) {
                meshRenderer.material.color = Color.Lerp(nonAlphaColor, alphaColor, timeToFade * Time.deltaTime);
                if (meshRenderer.material.color.Equals(alphaColor)) {
                    Destroy(gameObject);
                }
            }
        }
    }
}

