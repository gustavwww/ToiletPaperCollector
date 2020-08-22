using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Navigation {
    MENU, GAME
}
public class MenuController : MonoBehaviour, MenuCameraListener {

    public Camera gameCamera;
    public Camera menuCamera;
    
    public Canvas menuCanvas;
    public Canvas gameCanvas;

    private MenuCameraScript menuCameraScript;
    
    private Navigation currentNavigation = Navigation.MENU;

    private void Start() {
        this.menuCameraScript = menuCamera.GetComponent<MenuCameraScript>();
        menuCameraScript.addObserver(this);
        
        gameCamera.gameObject.SetActive(false);
        enableMenu();
    }
    
    public void playPressed() {
        currentNavigation = Navigation.GAME;
        menuCameraScript.moveCamera(Navigation.GAME);
        disableMenu();
    }

    public void storePressed() {
        
        

    }

    public void aboutPressed() {


    }

    public void cameraReached(Navigation nav) {
        Debug.Log("Camera Reached");
        switch (nav) {
            
            case Navigation.GAME:
                menuCamera.gameObject.SetActive(false);
                gameCamera.gameObject.SetActive(true);
                break;

        }
        
    }
    
    private void enableMenu() {
        menuCanvas.gameObject.SetActive(true);
    }
    
    private void disableMenu() {
        menuCanvas.gameObject.SetActive(false);
    }

}
