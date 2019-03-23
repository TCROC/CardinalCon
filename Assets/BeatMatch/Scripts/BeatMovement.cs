using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatMovement : MonoBehaviour
{
    public Transform endPoint;

    public float timeToComplete;

    public AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0, 1));

    public UnityEvent onAnimationComplete;

    private void Start()
    {
        StartCoroutine(LerpToPoint(endPoint));
    }

    IEnumerator LerpToPoint(Transform point)
    {
        float startTime = Time.time;

        float percentageComplete = 0;
        Vector3 startPos = transform.position;

        while (percentageComplete < 1)
        {
            transform.position = Vector3.LerpUnclamped(startPos, point.position, percentageComplete);
            percentageComplete = animationCurve.Evaluate(Time.time - startTime);
            yield return null;
        }

        onAnimationComplete?.Invoke();
    }
}
