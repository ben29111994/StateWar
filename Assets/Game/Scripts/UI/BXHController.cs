using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BXHController : MonoBehaviour
{
    public static BXHController Instance;

    public Text[] bxhText;
    public List<string> listName = new List<string>();
    public string[] nameBot1;
    public string[] nameBot2;
    public string[] nameBot3;
    public List<string> listNameBXH = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void Refresh()
    {
        listName.Clear();
        listName.Add("You");
        for (int i = 0; i < 3; i++)
        {
            if(i == 0)
            {
                listName.Add(nameBot1[i]);
            }
            else if (i == 1)
            {
                listName.Add(nameBot2[i]);
            }
            else if (i == 2)
            {
                listName.Add(nameBot3[i]);
            }
        }
    }

    public void Update()
    {
        UpdateBXH();
    }

    public void UpdateBXH()
    {
        listNameBXH.Clear();
        int countPlayer = GameManager.Instance.player.Length;
        float[] percentArray = new float[countPlayer];

        float f = 0.4f;
        for (int i = 0; i < countPlayer; i++)
        {
            percentArray[i] = GameManager.Instance.player[i].FlagAmount + f;
            f -= 0.1f;
        }

        List<float> a = new List<float>();

 

        for (int i = 0; i < percentArray.Length; i++)
            a.Add(percentArray[i]);

        a.Sort();
        a.Reverse();

        for (int i = 0; i < bxhText.Length; i++)
        {
            Text _text = bxhText[i];
            string name = "";
            float percent = a[i];

            for (int k = 0; k < a.Count; k++)
            {
                if (a[i] == percentArray[k])
                {
                    //    name = nameBot[k];
                    name = GetName(k);
                    k = a.Count;
                }
            }

            int percentFixed = (int)percent;
            _text.text = (i + 1).ToString() + " - " + name + " (" + percentFixed.ToString() + ")";
            if (GameManager.Instance.player[i].gameObject.activeInHierarchy == false)
            {
                name = "Empty";
                _text.text = (i + 1).ToString() + " - " + "Empty";
            }
                

    
            listNameBXH.Add(name);
        }

    }

    private string GetName(int _i)
    {
        return listName[_i];
    }
}
