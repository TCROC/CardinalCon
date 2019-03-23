using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Beat : MonoBehaviour
{
    public int explosionCount;
    public GameObject explodedCubePrefab;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.lossyScale / 2);
        if (colliders?.Length > 0 && colliders.Any(i => i.tag == "Player"))
        {
            for (int i = 0; i < explosionCount; i++)
            {
                GameObject go = Instantiate(explodedCubePrefab, transform.position, Quaternion.identity);
                Destroy(go, 2);
            }
            Destroy(gameObject);
        }
    }
}
