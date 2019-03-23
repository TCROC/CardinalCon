using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

	public static GameStateManager S;

	public GameObject ball;
    Vector3 ballSpawnPoint;

    public List<GameObject> players;
    List<Vector3> spawnPoints = new List<Vector3>();

    public int gameLength;
    [HideInInspector] public int timeLeft { get; private set; }

	int team1Score, team2Score;
    public Text team1ScoreDisplay, team2ScoreDisplay, timer;

    public TextMeshProUGUI tmpEndGame;

    public bool pauseTimer;

	// Use this for initialization
	void Start () {
		S = this;

		team1Score = team2Score = 0;
        timeLeft = gameLength;
        timer.text = timeLeft.ToString();
        ballSpawnPoint = ball.transform.position;
        pauseTimer = false;
        foreach (GameObject p in players) {
            spawnPoints.Add(p.transform.position); 
        }

        StartCoroutine(Timer()); 
	}

    IEnumerator Timer () {
		while (!pauseTimer && timeLeft > 0)
        {
            timeLeft--;
			pauseTimer = timeLeft <= 0;

			var span = new TimeSpan(0, 0, timeLeft); //Or TimeSpan.FromSeconds(seconds); (see Jakob C´s answer)
			var yourStr = string.Format("{0}:{1:00}",
										(int)span.TotalMinutes,
										span.Seconds);

			int seconds = timeLeft % 60;
			int minutes = timeLeft / 60;
            string time = minutes.ToString()  + ":" + seconds.ToString();

            timer.text = yourStr;
            yield return new WaitForSeconds(1);
        }
	}

    public void CallSetUp(float waitTime)
    {
        Invoke("SetUp", waitTime);
    }

    void SetUp () {
        int i = 0;
        foreach (GameObject p in players) {
            p.transform.position = spawnPoints[i];
            p.GetComponent<Rigidbody> ().velocity = Vector3.zero;
            p.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
            i++;
        }
        ball.transform.position = ballSpawnPoint;
        ball.transform.GetComponent<Rigidbody> ().velocity = Vector3.zero;
        ball.transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.SetActive(true);
        pauseTimer = false;
        if (timeLeft > 0)
            StartCoroutine(Timer());
    }

    public void Score(int teamScoredOn)
    {
        if (teamScoredOn == 1)
        {
            team2Score++;
            team2ScoreDisplay.text = team2Score.ToString();
        }
		if (teamScoredOn == 2)
		{
            team1Score++;
            team1ScoreDisplay.text = team1Score.ToString();
		}
    }

    public void EndGame()
    {
        StartCoroutine(ReturnToMenuCo());

        tmpEndGame.gameObject.SetActive(true);

        if (team1Score > team2Score)
        {
            tmpEndGame.text = "Blue Player Wins!";
        }
        else if (team2Score > team1Score)
        {
            tmpEndGame.text = "Red Player Wins!";
        }
        else if (team1Score == team2Score)
        {
            tmpEndGame.text = "There was a tie... you are both a dissapointment.";
        }
        else
        {
            Debug.Log("I have absolutely on idea how you could have possible gotten here.");
        }
    }

    IEnumerator ReturnToMenuCo()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
