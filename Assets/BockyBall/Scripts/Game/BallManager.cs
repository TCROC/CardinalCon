using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallManager : MonoBehaviour {

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (GameStateManager.S.timeLeft <= 0) {
                GameStateManager.S.EndGame();
            }
        }
    }
}
