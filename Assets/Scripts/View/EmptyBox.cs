using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBox : MonoBehaviour
{

    private static readonly float MOVE_TIME = 1.8f;
    private static readonly float MOVE_SPEED = 4;

    private bool isMoving = false;
    private float timer;

    private Rigidbody rigidBody;
    private Animation anim;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
    }


    public void Empty() {

        if (isMoving) {
            return;
        }

        timer = 0;
        isMoving = true;
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {

        if (isMoving) {

            timer += Time.fixedDeltaTime;

            if (timer <= MOVE_TIME) {
                
                rigidBody.MovePosition(transform.position + (new Vector3(1f, 0, -0.7f) * MOVE_SPEED * Time.fixedDeltaTime));
            } else {
                // Empty completed, get box back...

                anim.Play("CrateAnim");

                isMoving = false;
            }

        }

    }


    public bool CanTakePaper() {
        return !isMoving;
    }

}
