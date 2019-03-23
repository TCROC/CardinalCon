using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatPlayerMovement : MonoBehaviour
{
    public KeyCode inputKey;
    public float speed = 1;
    public bool moveLeft;

    public float minXPos;
    public float maxXPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(inputKey))
        {
            moveLeft = false;
        } else
        {
            moveLeft = true;
        }

        if (moveLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        Vector3 tempPos = transform.position;
        tempPos.x = Mathf.Clamp(tempPos.x, minXPos, maxXPos);
        transform.position = tempPos;
    }
}
