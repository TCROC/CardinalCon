using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    int jumps;
    bool jumped;
    bool touchingGround;

    public float moveSpeed = 10;
    public float accelerationSpeed = 1;

    float? prevXVel;

    // Use this for initialization
    void Start()
    {
        jumps = 0;
        jumped = false;
        touchingGround = true;

        prevXVel = null;
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

        if (Input.GetKey(KeyCode.A))
        { //&& jumps >= 1) {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.x = Mathf.MoveTowards(tempVel.x, -moveSpeed, accelerationSpeed);
            if (prevXVel != null && tempVel.x > prevXVel)
                tempVel.x = prevXVel.Value;

            transform.GetComponent<Rigidbody>().velocity = tempVel;
            Vector3 tempAng = transform.GetComponent<Rigidbody>().angularVelocity;
            tempAng.z = Mathf.MoveTowards(tempAng.z, moveSpeed, accelerationSpeed);
            transform.GetComponent<Rigidbody>().angularVelocity = tempAng;
            prevXVel = tempVel.x;
        }

        if (Input.GetKey(KeyCode.D))
        { // && jumps >= 1) {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.x = Mathf.MoveTowards(tempVel.x, moveSpeed, accelerationSpeed);
            if (prevXVel != null && tempVel.x < prevXVel)
                tempVel.x = prevXVel.Value;
            transform.GetComponent<Rigidbody>().velocity = tempVel;
            Vector3 tempAng = transform.GetComponent<Rigidbody>().angularVelocity;
            tempAng.z = Mathf.MoveTowards(tempAng.z, -moveSpeed, accelerationSpeed);
            transform.GetComponent<Rigidbody>().angularVelocity = tempAng;
            prevXVel = tempVel.x;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            prevXVel = null;

        if (Input.GetKeyDown(KeyCode.W) && jumps == 0)
        {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.y = 10;
            transform.GetComponent<Rigidbody>().velocity = tempVel;
            jumped = true;
            jumps++;
        }
        else if (Input.GetKeyDown(KeyCode.W) && jumps < 2)
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
            jumped = true;
            jumps++;
        }

        if (Input.GetKeyDown(KeyCode.S))
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
