using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerController : MonoBehaviour
{
    public string teamName;

    public KeyCode inputKey;

    public int jumpLimit = 2;

    private int jumpCount;

    public GameObject obstaclePrefab;

    public float jumpStrength = 10;

    Rigidbody myRigidBody;

    public float moveSpeed = 10;

    public bool isJumping;

    public float obstactleDropTime = 5;

    private float obstactleDropTimeStart;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        SingleInput si = GetComponent<SingleInput>();
        si.onSinglePress += Jump;
        si.onLongPress += DropObstacle;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (!isJumping)
        {
            myRigidBody.AddForce(Vector3.right * moveSpeed);
        }
    }

    public void Jump()
    {
        if (jumpCount < jumpLimit)
        {
            isJumping = true;
            Vector3 tempVel = transform.GetComponent<Rigidbody>().velocity;
            tempVel.y = jumpStrength;
            transform.GetComponent<Rigidbody>().velocity = tempVel;
            jumpCount++;
        }
    }

    public void DropObstacle()
    {
        if (Time.time - obstactleDropTimeStart > obstactleDropTime)
        {
            Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            obstactleDropTimeStart = Time.time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
            isJumping = false;
        }
    }
}
