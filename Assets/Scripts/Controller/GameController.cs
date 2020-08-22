using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour, ModelListener {

    public GameObject spawnObject;
    public GameObject rigidPaper;
    public GameObject box;

    private GameModel gameModel;
    private EmptyBox emptyBoxManager;

    void Start() {
        gameModel = new GameModel();
        gameModel.addObserver(this);


        emptyBoxManager = box.GetComponent<EmptyBox>();
    }

    public void boxFullOfPaper() {

        // Empty Box
        emptyBoxManager.empty();
    }

    public void spawnButtonPressed() {
        if (!emptyBoxManager.canTakePaper()) {
            return;
        }

        gameModel.incToiletPaper();
        GameObject paper = Instantiate(rigidPaper, getRandomSpawnPos(box), spawnObject.transform.rotation, spawnObject.transform);

        // Add rotation
        Rigidbody rgBody = paper.GetComponent<Rigidbody>();
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

}
