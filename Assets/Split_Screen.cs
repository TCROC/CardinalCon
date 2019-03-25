using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split_Screen : MonoBehaviour
{
    public Transform mainCamera;

    void cameraFollow()
    {
        float charPosX = transform.position.x;
        float charPosZ = transform.position.z;
        float cameraOffset = 18.0f;

        mainCamera.transform.position = new Vector3(charPosX, cameraOffset, charPosX);
        mainCamera.rotation = Quaternion.Euler(0f, 0f, 0f);


    }

    private void Update()
    {
        cameraFollow();
    }


}
