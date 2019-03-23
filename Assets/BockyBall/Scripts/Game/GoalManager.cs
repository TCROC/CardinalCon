using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour {

    public int teamNum;

    public GameObject explosion;
    GameObject explosionClone;

    public GameObject wall;
    public GameObject rim;

    float wallRimDistance;

    private void Start()
    {
        wallRimDistance = Vector3.Distance(wall.transform.position, rim.transform.position); 
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Ball" && col.transform.position.y < transform.position.y)
        {
            float wallBallDist = Vector3.Distance(wall.transform.position, col.transform.position);
            if (wallBallDist < wallRimDistance)
            {
                Vector3 spawnPos = col.transform.position;
                // spawnPos.y += 3;
                explosionClone = Instantiate(explosion, spawnPos, Quaternion.identity);
                col.gameObject.SetActive(false);
                GameStateManager.S.CallSetUp(3);
                GameStateManager.S.Score(teamNum);
                GameStateManager.S.pauseTimer = true;
            }
			//SceneManager.LoadSceneAsync("Scene_Game");
		}
    }
}
