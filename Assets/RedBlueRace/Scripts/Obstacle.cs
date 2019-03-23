using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public string teamName;
    public float forceStrength = 10;

    private void Start()
    {
        RacerController racerController = FindObjectsOfType<RacerController>().First(i => i.teamName == teamName);

        Collider myCollider = GetComponent<Collider>();

        Physics.IgnoreCollision(myCollider, racerController.GetComponent<Collider>());
    }

    public void OnCollisionEnter(Collision collision)
    {
        Rigidbody collidedRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        collidedRigidbody.AddForce(Vector3.left * forceStrength, ForceMode.Impulse);
    }
}
