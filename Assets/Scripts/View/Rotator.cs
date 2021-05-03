using UnityEngine;


namespace View {
    public class Rotator : MonoBehaviour {
        void Update() {
            transform.Rotate(Vector3.right * Time.deltaTime * 30, Space.World);
        }
    }
}

