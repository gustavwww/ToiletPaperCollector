using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MenuCameraScript : MonoBehaviour {

    public Camera gameCamera;

    private Animator animator;
    
    void Start() {
        this.animator = GetComponent<Animator>();
    }
    
    public void moveCamera(Navigation to) {
        switch (to) {
            
            case Navigation.GAME:
                StartCoroutine(moveToGame());
                break;
            
            case Navigation.MENU:
                enableAnimator();
                break;
            
        }
    }
    
    private IEnumerator moveToGame() {
        disableAnimator();
        
        float duration = 2.0f;
        for (float t = .0f; t < duration; t += Time.deltaTime) {
            transform.position = Vector3.Lerp(transform.position, gameCamera.transform.position, (Time.deltaTime * 3));
            transform.rotation = Quaternion.Lerp(transform.rotation, gameCamera.transform.rotation, (Time.deltaTime * 3));
            yield return null;
        }

        Debug.Log("Done transforming...");
        informCameraReached(Navigation.GAME);
    }
    
    void Update() {
        
    }
    

    private void disableAnimator() {
        animator.enabled = false;
    }

    private void enableAnimator() {
        animator.enabled = true;
    }


public List<MenuCameraListener> observers = new List<MenuCameraListener>();

    public void addObserver(MenuCameraListener listener) {
        observers.Add(listener);
    }

    private void informCameraReached(Navigation nav) {
        foreach (MenuCameraListener observer in observers) {
            observer.cameraReached(nav);
        }
    }
    
}
