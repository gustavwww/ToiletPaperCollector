using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ModelListener
{
    // Start is called before the first frame update

    public GameObject SpawnObject;
    public GameObject RigidPaper;
    public GameObject box;

    private EmptyBox emptyBoxManager;

    private GameModel gameModel;

    void Start()
    {
        gameModel = new GameModel();
        gameModel.AddObserver(this);


        emptyBoxManager = box.GetComponent<EmptyBox>();
    }

    public void BoxFullOfPaper() {

        // Empty Box
        emptyBoxManager.Empty();
    }

    public void SpawnButtonPressed()
    {
        if (!emptyBoxManager.CanTakePaper()) {
            return;
        }

        gameModel.IncToiletPaper();
        GameObject paper = Instantiate(RigidPaper, getRandomSpawnPos(box), SpawnObject.transform.rotation, SpawnObject.transform);

        // Add rotation
        Rigidbody rgBody = paper.GetComponent<Rigidbody>();
        rgBody.AddTorque(new Vector3(10000f, 10000f, 1000000f));
    } 

    
    private Vector3 getRandomSpawnPos(GameObject box)
    {

        Renderer boxRender = box.GetComponent<Renderer>();

        Vector3 origin = box.transform.position;
        float rangeX = boxRender.bounds.size.x / 4;
        float rangeZ = boxRender.bounds.size.z / 4;

        Vector3 randomRange = new Vector3(Random.Range(-rangeX, rangeX), 4, Random.Range(-rangeZ, rangeZ));

        return origin + randomRange;
    }


    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            SpawnButtonPressed();
        }
        
    }
}
