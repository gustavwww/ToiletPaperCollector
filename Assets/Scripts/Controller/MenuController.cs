using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Navigation {
    MENU, GAME
}
public class MenuController : MonoBehaviour {

    public Camera gameCamera;
    public Camera menuCamera;
    
    public Canvas menuCanvas;
    public Canvas gameCanvas;

    private Navigation userNavigation = Navigation.MENU;

    public void playPressed() {
        
        
        disableMenu();
        userNavigation = Navigation.GAME;

    }

    private void disableMenu() {
        gameCanvas.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(false);

        menuCamera.GetComponent<Animator>().enabled = false;
    }

    private void enableMenu() {
        gameCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);

        menuCamera.GetComponent<Animator>().enabled = true;
    }

    public void storePressed() {
        
        

    }

    public void aboutPressed() {


    }


    // Start is called before the first frame update
    private void Start() {
        gameCamera.gameObject.SetActive(false);
        enableMenu();
    }
    
    private void updateCameraToGame() {
        // TODO
        menuCamera.transform.position = Vector3.Lerp(menuCamera.transform.position, gameCamera.transform.position, (9 * Time.fixedDeltaTime));
        menuCamera.transform.rotation = Quaternion.Lerp(menuCamera.transform.rotation,
                                    gameCamera.transform.rotation, (9 * Time.fixedDeltaTime));


    }
    
    // Update is called once per frame
    private void Update() {
        
        switch (userNavigation) {
                    
                    case Navigation.GAME:
                        updateCameraToGame();
                        break;
                    
                }
    }
    
}
