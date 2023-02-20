using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("References")]
    public GameObject MainMenuUI;
    public GameObject InGameUI;
    public GameObject CompleteUI;
    public GameObject FailUI;
    public GameObject LoadingUI;
    public GameObject OverTIme;

    private void Start()
    {
    
    }

    public void Loading()
    {
        StartCoroutine(C_Loading());
    }

    private IEnumerator C_Loading()
    {
        LoadingUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LoadingUI.SetActive(false);
    }

    public void Show_MainMenuUI()
    {
        MainMenuUI.SetActive(true);
        InGameUI.SetActive(false);
        CompleteUI.SetActive(false);
        FailUI.SetActive(false);
    }

    public void Show_InGameUI()
    {
        MainMenuUI.SetActive(false);
        InGameUI.SetActive(true);
        CompleteUI.SetActive(false);
        FailUI.SetActive(false);
    }

    public void Show_CompleteUI()
    {
        MainMenuUI.SetActive(false);
        InGameUI.SetActive(false);
        CompleteUI.SetActive(true);
        FailUI.SetActive(false);
    }

    public void Show_FailUI()
    {
        MainMenuUI.SetActive(false);
        InGameUI.SetActive(false);
        CompleteUI.SetActive(false);
        FailUI.SetActive(true);
    }
}
