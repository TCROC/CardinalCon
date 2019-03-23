using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Beat : MonoBehaviour
{
    public static float beatSpeed = 2;

    public int explosionCount;
    public GameObject explodedCubePrefab;

    private void Update()
    {
        Collider col = Physics.OverlapBox(transform.position, transform.lossyScale / 2)?.FirstOrDefault(i => i.tag == "Player");
        if (col != null)
        {
            if (col.gameObject.name == "Player1")
            {
                BeatGameManager.instance.P1Score++;
            }
            else if (col.gameObject.name == "Player2")
            {
                BeatGameManager.instance.P2Score++;
            }

            for (int i = 0; i < explosionCount; i++)
            {
                GameObject go = Instantiate(explodedCubePrefab, transform.position, Quaternion.identity);
                Destroy(go, 2);
            }
            Destroy(gameObject);
        }

        transform.Translate(Vector3.back * beatSpeed * Time.smoothDeltaTime);
    }
}