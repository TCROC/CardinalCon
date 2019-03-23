using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInput : MonoBehaviour
{
    public KeyCode inputKey;

    [SerializeField]
    private float _doublePressTime = .2f;
    [SerializeField]
    private float _longPressTime = .5f;

    private int pressCount;
    private float pressTime;

    private Coroutine _singlePressRoutine;

    public Action onSinglePress;
    public Action onDoublePress;
    public Action onLongPress;

    private void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            pressCount++;

            if (Time.time - pressTime < _doublePressTime && pressCount >= 2)
            {
                onDoublePress?.Invoke();
                StopCoroutine(_singlePressRoutine);
                pressCount = 0;
                pressTime = 0;
            }
            else
            {
                pressCount = 1;
                pressTime = Time.time;
                _singlePressRoutine = StartCoroutine(SinglePress());
            }
        }

        if (Input.GetKey(inputKey))
        {
            if (Time.time - pressTime > _longPressTime)
            {
                onLongPress?.Invoke();
                StopCoroutine(_singlePressRoutine);
                pressCount = 0;
                pressTime = 0;
            }
        }
    }
    public IEnumerator SinglePress()
    {
        yield return new WaitForSeconds(_doublePressTime);
        if (pressCount == 1)
        {
            pressCount = 0;
            onSinglePress?.Invoke();
        }
    }
}
