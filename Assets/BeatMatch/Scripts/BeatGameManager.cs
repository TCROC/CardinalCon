using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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

    private int p1Score;
    private int p2Score;

    [SerializeField]
    private int timeLeft = 180;

    [SerializeField]
    private TextMeshProUGUI tmpP1Score;

    [SerializeField]
    private TextMeshProUGUI tmpP2Score;

    [SerializeField]
    private TextMeshProUGUI tmpTimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            timeLeft--;

            var span = new TimeSpan(0, 0, timeLeft);
            var formattedTime = string.Format("{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);

            int seconds = timeLeft % 60;
            int minutes = timeLeft / 60;
            string time = minutes.ToString() + ":" + seconds.ToString();

            tmpTimeLeft.text = formattedTime;
            yield return new WaitForSeconds(1);
        }
    }
}