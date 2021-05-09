using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreBtn : MonoBehaviour {
    void Start() {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer) {
            gameObject.SetActive(false);
        }
    }
}
