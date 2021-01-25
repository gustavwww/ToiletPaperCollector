using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class MenuCameraScript : MonoBehaviour {

    public Camera gameCamera;

    private Animator animator;
    
    public List<MenuCameraListener> observers = new List<MenuCameraListener>();

    public void addObserver(MenuCameraListener listener) {
        observers.Add(listener);
    }

    private void informCameraReached(Navigation nav) {
        foreach (MenuCameraListener observer in observers) {
            observer.cameraReached(nav);
        }
    }
    
    void Start() {
        animator = GetComponent<Animator>();
    }
    
    public void moveCamera(Navigation to) {
        switch (to) {
            
            case Navigation.GAME:
                disableAnimator();
                StartCoroutine(moveToLocation(gameCamera.gameObject.transform, to));
                break;
            
            case Navigation.MENU:
                enableAnimator();
                informCameraReached(to);
                break;
            
        }
    }
    
    private IEnumerator moveToLocation(Transform location, Navigation navigation) {

        float duration = 2.0f;
        for (float t = .0f; t < duration; t += Time.deltaTime) {
            transform.position = Vector3.Lerp(transform.position, location.position, (Time.deltaTime * 3));
            transform.rotation = Quaternion.Lerp(transform.rotation, location.rotation, (Time.deltaTime * 3));
            yield return null;
        }
        
        informCameraReached(navigation);
    }
    
    public void disableAnimator() {
        animator.enabled = false;
    }

    public void enableAnimator() {
        animator.enabled = true;
    }

}
