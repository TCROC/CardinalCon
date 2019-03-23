using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallManager : MonoBehaviour {

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (GameStateManager.S.timeLeft <= 0) {
                SceneManager.LoadScene("Scene_Game");
            }
        }
    }
}
