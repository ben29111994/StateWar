using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimerController : MonoBehaviour
{
    protected static TimerController instance;
    public static TimerController Instance => instance;

    public Text timerText;
    private const float maxTime = 46.0f;

    private float currentTime;
    private float CurrentTime
    {
        get
        {
            return currentTime;
        }
        set
        {
            currentTime = value;
            timerText.text = "0:" + CurrentTime;
            if (currentTime < 10) timerText.text = "0:0" + currentTime;
            if (currentTime == -1) timerText.text = "";
            timerText.transform.DOScale(Vector3.one * 1.05f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }
    }


    private void Awake()
    {
        instance = this;
    }

    public void ResetTimer()
    {
        StopTimer();
        CurrentTime = maxTime;
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    public void StartTimer()
    {
        StartCoroutine(C_StartTimer());
    }

    private IEnumerator C_StartTimer()
    {
        while(CurrentTime >= 0)
        {
            yield return new WaitForSeconds(1.0f);

            CurrentTime--;
        }

        GameManager.Instance.Complete();
    }
}
