using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public Text levelText;


    public int LevelGame
    {
        get
        {
           return PlayerPrefs.GetInt("LevelGame");
        }
        set
        {
            PlayerPrefs.SetInt("LevelGame", value);
            levelText.text = "Lvl." + (value + 1);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LevelGame += 0;
    }
}
