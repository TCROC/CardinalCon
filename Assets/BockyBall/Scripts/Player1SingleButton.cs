using System;
using System.Collections;
using UnityEngine;

public class Player1SingleButton : MonoBehaviour {

    public bool goingLeft;

    public float moveSpeed = 10;
    public float accelerationSpeed = 1;

    private float? prevXVel;

    private bool isJumping;

    private void Start()
    {
        SingleInput i = GetComponent<SingleInput>();
        i.onDoublePress += Jump;
        i.onSinglePress += SwitchDirection;
    }

    private void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (goingLeft)
        {
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
        else
        {
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
    }

    public void Jump()
    {
        if (!isJumping)
        {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.y = 10;
            transform.GetComponent<Rigidbody>().velocity = tempVel;
            isJumping = true;
        } else
        {
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.y = -10;
            transform.GetComponent<Rigidbody>().velocity = tempVel;
        }
    }

    public void SwitchDirection()
    {
        goingLeft = !goingLeft;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
