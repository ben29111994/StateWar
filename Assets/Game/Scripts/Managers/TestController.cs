using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour
{
    public static TestController Instance;
   

    public int TypeDeFaultCube
    {
        get
        {
            return PlayerPrefs.GetInt("TypeDeFaultCube");
        }
        set
        {
            PlayerPrefs.SetInt("TypeDeFaultCube", value);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void OnChangeTypeDefaultCube()
    {
        TypeDeFaultCube++;
        if (TypeDeFaultCube == 2) TypeDeFaultCube = 0;

        SceneManager.LoadScene(0);
    }
}
