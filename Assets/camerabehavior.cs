using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerabehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float zOffset = 0f;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(target.transform.position.x + xOffset,
                                                target.transform.position.y + yOffset,
                                                target.transform.position.z + zOffset);
        this.transform.rotation = Quaternion.LookRotation(target.position - transform.position, -Vector3.down);
    }
}
