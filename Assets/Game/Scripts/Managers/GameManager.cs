using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Status Game")]
    public bool isStart;
    public bool isComplete;
    public bool isVibration;
    public bool isShakeCamera;

    public Human[] player;


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
            OnStartGame();
        }

        UpdateStartGame();
    }

    private void UpdateStartGame()
    {
        if (Input.GetMouseButtonDown(0) && isStart == false)
        {
            isStart = true;
            UIManager.Instance.Show_InGameUI();
            TimerController.Instance.StartTimer();
            FlagController.Instance.StartGenerate();
            RedController.Instance.Active();
            StartAI();
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
        GenerateMap.Instance.LoadDataFromResource();
        UIManager.Instance.Show_MainMenuUI();
        UIManager.Instance.Loading();
    }

    protected virtual void OnRefresh()
    {
        isStart = false;
        isComplete = false;

        BXHController.Instance.Refresh();
        PoolManager.Instance.RefreshAll();
        for (int i = 0; i < player.Length; i++) player[i].Hide();
        for (int i = 0; i < GetBotAmount(); i++) player[i].Active();

        player[0].GetComponent<Player>().joyStick.gameObject.SetActive(true);

        FlagController.Instance.StopGenerate();
        TimerController.Instance.ResetTimer();
        RedController.Instance.Refresh();
    }

    private void StartAI()
    {
        for(int i = 1; i < GetBotAmount(); i++)
        {
            player[i].GetComponent<Bot>().StartAI();
        }
    }

    private void LevelUp()
    {
        DataManager.Instance.LevelGame++;
    }

    public void Complete()
    {
        if (isComplete) return;

        isComplete = true;
        StartCoroutine(C_Complete());
    }

    private IEnumerator C_Complete()
    {
        for (int i = 0; i < player.Length; i++) player[i].Complete();
        RedController.Instance.Refresh();
        LevelUp();
        UIManager.Instance.OverTIme.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        UIManager.Instance.OverTIme.SetActive(false);

        UIManager.Instance.Show_CompleteUI();
    }

    public void Fail()
    {
        if (isComplete) return;

        isComplete = true;
        StartCoroutine(C_Fail());
    }

    private IEnumerator C_Fail()
    {
        yield return null;
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