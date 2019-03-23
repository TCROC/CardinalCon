using System;
using System.Collections;
using UnityEngine;

public class Player1SingleButton : MonoBehaviour {

    public bool goingLeft;

    public float doublePressTime = .1f;

    public int pressCount;
    
    public float pressTime;

    public Action singlePress;
    public Action doublePress;

    Coroutine singlePressRoutine;

    public float moveSpeed = 10;
    public float accelerationSpeed = 1;

    float? prevXVel;

    public bool isJumping;

    public KeyCode inputKey;

    private void Start()
    {
        singlePress += SwitchDirection;
        doublePress += Jump;
    }

    private void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            pressCount++;

            if (Time.time - pressTime < doublePressTime && pressCount >= 2)
            {
                doublePress.Invoke();
                StopCoroutine(singlePressRoutine);
                pressCount = 0;
                pressTime = 0;
            } else
            {
                pressCount = 1;
                pressTime = Time.time;
                singlePressRoutine = StartCoroutine(SinglePress());
            }
        }

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

    public IEnumerator SinglePress()
    {
        yield return new WaitForSeconds(doublePressTime);
        if (pressCount == 1)
        {
            pressCount = 0;
            singlePress.Invoke();
        }
    }

    public void SwitchDirection()
    {
        Debug.Log("Sitch Direction");
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
