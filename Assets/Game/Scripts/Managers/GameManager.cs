using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Status Game")]
    public bool isStart;
    public bool isComplete;
    public bool isVibration;
    public bool isShakeCamera;
    public Human[] player;
    public GameObject winPanel;
    public int playerCounter;
    public int enemyCounter;
    public Text playerCountertxt;
    public Text enemyCountertxt;
    public Transform spawnPointManager;
    int totalSpawnPoint = 0;
    int playerCapture = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
        InitPlugin();
    }

    private void Start()
    {
        OnStartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            LevelUp();
            //OnStartGame();
        }

        UpdateStartGame();
    }

    private void UpdateStartGame()
    {
        if (Input.GetMouseButtonDown(0) && isStart == false)
        {
            isStart = true;
        }
    }

    public void InitPlugin()
    {
#if UNITY_IOS
        MMVibrationManager.iOSInitializeHaptics();
#else

#endif
    }

    public void OnStartGame()
    {
        OnRefresh();
        //GenerateMap.Instance.LoadDataFromResource();
        //UIManager.Instance.Show_MainMenuUI();
        //UIManager.Instance.Loading();
    }

    protected virtual void OnRefresh()
    {
        isStart = false;
        isComplete = false;

        //BXHController.Instance.Refresh();
        PoolManager.Instance.RefreshAll();
        totalSpawnPoint = spawnPointManager.transform.childCount;
        playerCapture = 0;
        foreach (Transform child in spawnPointManager.transform)
        {
            if (child.GetComponent<Conquest>().currentTeam.name == "Player's Spawn")
            {
                playerCapture++;
            }
        }
    }

    public void Capture(int value)
    {
        playerCapture += value;
        if(playerCapture >= totalSpawnPoint)
        {
            Complete();
        }
        else if(playerCapture <= 0)
        {
            Complete();
        }
    }

    //private void StartAI()
    //{
    //    for(int i = 1; i < GetBotAmount(); i++)
    //    {
    //        player[i].GetComponent<Bot>().StartAI();
    //    }
    //}

    public void LevelUp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void Complete()
    {
        if (isComplete) return;

        isComplete = true;
        StartCoroutine(C_Complete());
    }

    private IEnumerator C_Complete()
    {
        PoolManager.Instance.RefreshAll();
        yield return new WaitForSeconds(0.5f);
        //Win
        winPanel.SetActive(true);
        float percent = 0;
        playerCounter = int.Parse(playerCountertxt.ToString());
        enemyCounter = int.Parse(enemyCountertxt.ToString());
        if (playerCounter > enemyCounter)
        {
            percent = (playerCounter * 100) / (enemyCounter + playerCounter);
            winPanel.GetComponentInChildren<TextMeshPro>().text = "YOU TAKE OVER " + percent + " OF THE LAND";
        }
        else
        {
            percent = (enemyCounter * 100) / (enemyCounter + playerCounter);
            winPanel.GetComponentInChildren<TextMeshPro>().text = "ENEMY TAKE OVER " + percent + " OF THE LAND";
        }
    }

    public void Fail()
    {
        if (isComplete) return;

        isComplete = true;
        StartCoroutine(C_Fail());
    }

    private IEnumerator C_Fail()
    {
        yield return new WaitForSeconds(0.5f);
        winPanel.SetActive(true);
    }

    public void Vibration()
    {
        if (isVibration) return;

        StartCoroutine(C_Vibarion());
    }

    private IEnumerator C_Vibarion()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);

        isVibration = true;
        yield return new WaitForSecondsRealtime(0.3f);
        isVibration = false;
    }

    private int GetBotAmount()
    {
        if(DataManager.Instance.LevelGame <= 1)
        {
            return 2;
        }
        else if (DataManager.Instance.LevelGame <= 3)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
}