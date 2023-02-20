using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    private void Update()
    {
        for (int i = 0; i < GameManager.Instance.player.Length; i++)
        {
            GameManager.Instance.player[i].canvas.anchoredPosition = Vector2.up * 0.0f;
        }

        Human t = GetTarget();
        if (t == null) return;
        t.canvas.anchoredPosition = Vector2.up * 1.0f;
        transform.position = t.transform.position;
    }

    private Human GetTarget()
    {
        if (BXHController.Instance.listNameBXH.Count == 0) return null;
        string _nameTop1 = BXHController.Instance.listNameBXH[0];

        for (int i = 0; i < GameManager.Instance.player.Length; i++)
        {
            if (GameManager.Instance.player[i].nameTMP.text == _nameTop1) return GameManager.Instance.player[i];
        }

        return null;
    }
}
