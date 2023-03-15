using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Rigidbody rigid;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        FlagController.Instance.HideItem(this);
    }

    public void Active(Vector3 _pos)
    {
        transform.position = _pos;
        gameObject.SetActive(true);
    }

    public void ActiveWithPhysics(Vector3 _pos)
    {
        _pos.y = 2.0f;
        transform.position = _pos;
        gameObject.SetActive(true);
        rigid.velocity = Vector3.zero;
        Vector3 force = Vector3.zero;
        force.y = Random.Range(1.2f,1.5f) * 500.0f;
        force.x = RandomDirection() * 200.0f;
        force.z = RandomDirection() * 200.0f;
        rigid.AddForce(force);
    }

    [System.Obsolete]
    public void Hide()
    {
        EffectManager.Instance.Item(transform.position, Color.white);
        gameObject.SetActive(false);
    }

    private float RandomDirection()
    {
        return (Random.value > 0.5f) ? 1.0f : -1.0f;
    }
}
