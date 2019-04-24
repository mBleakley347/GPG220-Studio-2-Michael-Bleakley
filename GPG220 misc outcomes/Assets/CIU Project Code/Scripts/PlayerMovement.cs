using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float playerJump;
    public float playerRotation;
    public float playerSpeed;

	void Update () {

        //Jump
        if (Input.GetKeyDown("space"))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, playerJump, 0), ForceMode.Impulse);
        }

        // rotate player based on mouse movement
        float RotationHorizontal = Input.GetAxis("Mouse X") * playerRotation;
        transform.Rotate(0, RotationHorizontal, 0);

        // move player based on key inputs
        var Straifing = Input.GetAxis("Horizontal") * ((playerSpeed * transform.localScale.y)/4);
        float Vertical = Input.GetAxis("Vertical") * ((playerSpeed * transform.localScale.y)/2);
        transform.Translate(Straifing, 0, Vertical);
    }
}
