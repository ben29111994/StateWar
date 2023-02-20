using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Complete : MonoBehaviour
{
    public Text[] sttArray;
    public GameObject[] continueButton;

    public void OnEnable()
    {
        StartCoroutine(C_Active());
    }

    private IEnumerator C_Active()
    {
        List<string> listName = BXHController.Instance.listNameBXH;
        for(int i = 0; i < sttArray.Length; i++)
        {
            sttArray[i].text = listName[i];
        }

        continueButton[0].SetActive(false);
        continueButton[1].SetActive(false);
        yield return new WaitForSeconds(1.0f);
        continueButton[0].SetActive(true);
        continueButton[1].SetActive(true);
    }
}
