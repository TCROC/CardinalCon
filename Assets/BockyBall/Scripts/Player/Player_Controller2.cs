using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller2 : MonoBehaviour
{

    int jumps;

    bool jumped;
    bool touchingGround;

    // Use this for initialization
    void Start()
    {
        jumps = 0;
        jumped = false;
        touchingGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        /*
		if (Input.GetKey (KeyCode.A) && jumps < 1) {
			transform.GetComponent<Rigidbody> ().AddForce (Vector3.left * 30);
		}
		if (Input.GetKey (KeyCode.D) && jumps < 1) {
			transform.GetComponent<Rigidbody> ().AddForce (Vector3.right * 30);
		}
		*/

        if (Input.GetKey(KeyCode.LeftArrow))
        { //&& jumps >= 1) {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.x = -10;
            transform.GetComponent<Rigidbody>().velocity = tempVel;
            Vector3 tempAng = transform.GetComponent<Rigidbody>().angularVelocity;
            tempAng.z = 10;
            transform.GetComponent<Rigidbody>().angularVelocity = tempAng;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        { // && jumps >= 1) {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.x = 10;
            transform.GetComponent<Rigidbody>().velocity = tempVel;
            Vector3 tempAng = transform.GetComponent<Rigidbody>().angularVelocity;
            tempAng.z = -10;
            transform.GetComponent<Rigidbody>().angularVelocity = tempAng;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && jumps == 0)
        {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.y = 10;
            transform.GetComponent<Rigidbody>().velocity = tempVel;
            jumps++;
            jumped = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && jumps < 2)
        {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            if (tempVel.y >= 0)
            {
                tempVel.y += 10;
            }
            else
            {
                tempVel.y = 10;
            }
            transform.GetComponent<Rigidbody>().velocity = tempVel;
            jumps++;
            jumped = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.y = -10;
            transform.GetComponent<Rigidbody>().velocity = tempVel;
        }

    }

    void OnCollisionStay(Collision col)
    {
        if (col.transform.tag == "Ground" && jumped && !touchingGround)
        {
            jumps = 0;
            jumped = false;
            touchingGround = true;
        }
        if (col.transform.tag == "Wall")
        {
            jumps = 0;
            //jumped = false;
            //touchingGround = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.transform.tag == "Ground")
        {
            touchingGround = false;
        }
    }
}
