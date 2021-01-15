using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBox : MonoBehaviour {

    private static readonly float INIT_DELAY = 1.0f;
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

    public void empty() {

        if (isMoving) {
            return;
        }

        timer = 0;
        isMoving = true;
    }

    private void FixedUpdate() {

        if (isMoving) {

            timer += Time.fixedDeltaTime;

            if (timer < INIT_DELAY) { return; }

            if (timer <= MOVE_TIME + INIT_DELAY) {
                
                rigidBody.MovePosition(transform.position + (new Vector3(1f, 0, -0.7f) * (MOVE_SPEED * Time.fixedDeltaTime)));
            } else {
                // Empty completed, get box back...

                anim.Play("NewCrateAnim");

                isMoving = false;
            }

        }

    }


    public bool isEmptying() {
        return isMoving;
    }

}
