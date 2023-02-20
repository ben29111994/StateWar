using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedController : MonoBehaviour
{
    protected static RedController instance;
    public static RedController Instance => instance;

    private List<GameObject> listRedType = new List<GameObject>();

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < transform.childCount; i++) listRedType.Add(transform.GetChild(i).gameObject);
    }

    public void Refresh()
    {
        for (int i = 0; i < listRedType.Count; i++) listRedType[i].SetActive(false);
    }

    public void Active()
    {
        listRedType[GetAnimationIndex()].SetActive(true);
    }

    private int GetAnimationIndex()
    {
        if (DataManager.Instance.LevelGame <= 4) return DataManager.Instance.LevelGame;
        return Random.Range(0, 5);
    }
}
