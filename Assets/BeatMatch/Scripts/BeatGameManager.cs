using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class BeatGameManager : MonoBehaviour
{
    public static BeatGameManager instance;

    public int P1Score
    {
        get
        {
            return p1Score;
        }
        set
        {
            p1Score = value;
            tmpP1Score.text = p1Score.ToString();
        }
    }

    public int P2Score
    {
        get
        {
            return p2Score;
        }
        set
        {
            p2Score = value;
            tmpP2Score.text = p2Score.ToString();
        }
    }

    public float beatStartSpeed = 2;
    public float beatEndSpeed = 5;

    private int p1Score;
    private int p2Score;

    [SerializeField]
    private int timeLeft = 180;

    private int timeStarted;

    [SerializeField]
    private TextMeshProUGUI tmpP1Score;

    [SerializeField]
    private TextMeshProUGUI tmpP2Score;

    [SerializeField]
    private TextMeshProUGUI tmpTimeLeft;

    [SerializeField]
    private TextMeshProUGUI tmpEndGame;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(Timer());
        timeStarted = timeLeft;
    }

    IEnumerator Timer()
    {
        while (timeLeft > 0)
        {
            timeLeft--;

            var span = new TimeSpan(0, 0, timeLeft);
            var formattedTime = string.Format("{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);

            int seconds = timeLeft % 60;
            int minutes = timeLeft / 60;
            string time = minutes.ToString() + ":" + seconds.ToString();

            tmpTimeLeft.text = formattedTime;

            Beat.beatSpeed = Utils.Remap(timeLeft, timeStarted, 0, beatStartSpeed, beatEndSpeed);

            yield return new WaitForSeconds(1);
        }

        EndGame();
    }

    public void EndGame()
    {
        StartCoroutine(ReturnToMenuCo());

        tmpEndGame.gameObject.SetActive(true);

        if (p1Score > p2Score)
        {
            tmpEndGame.text = "Blue Player Wins!";
        }
        else if (p2Score > p1Score)
        {
            tmpEndGame.text = "Red Player Wins!";
        }
        else if (p1Score == p2Score)
        {
            tmpEndGame.text = "There was a tie... so dissapointed.";
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