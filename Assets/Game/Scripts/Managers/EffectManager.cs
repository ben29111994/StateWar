using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    protected static EffectManager instance;
    public static EffectManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    
    public virtual void Item(Vector3 _pos)
    {
        GameObject _go = PoolManager.Instance.GetObject(PoolManager.NameObject.Item) as GameObject;
        _go.transform.position = new Vector3(_pos.x, 0.01f, _pos.z);
        StartCoroutine(C_Active(_go, 1.0f));
    }

    private IEnumerator C_Active(GameObject _go,float _time)
    {
        _go.SetActive(true);
        yield return new WaitForSeconds(_time);
        _go.SetActive(false);
    }
}
