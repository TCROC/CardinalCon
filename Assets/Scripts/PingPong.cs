using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    public Transform point1;
    public Transform point2;

    public float timeToComplete = 1;

    public AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0,0), new Keyframe(1,1));

    private void Start()
    {
        StartCoroutine(LerpToPoint(point1));
    }

    IEnumerator LerpToPoint(Transform point)
    {
        float startTime = Time.time;

        float percentageComplete = 0;
        Vector3 startPos = transform.position;

        while (percentageComplete < 1)
        {
            transform.position = Vector3.LerpUnclamped(startPos, point.position, percentageComplete);
            percentageComplete = animationCurve.Evaluate((Time.time - startTime) / timeToComplete);
            yield return null;
        }

        if (point == point1)
        {
            StartCoroutine(LerpToPoint(point2));
        }
        else
        {
            StartCoroutine(LerpToPoint(point1));
        }
    }
}
