using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Human : MonoBehaviour
{
    [Header("References")]
    public RectTransform canvas;
    public Transform head;
    public SkinnedMeshRenderer smr;
    public Transform model;
    public Animator anitor;
    public Rigidbody rigid;
    public TextMeshProUGUI flagTMP;
    public TextMeshProUGUI nameTMP;
    public Image[] flagImage;
    public Material[] mArray;
    public Material[] mFallArray;
    public Color[] colorArray;
    public Vector3[] startPosArray;

    [Header("Status")]
    public bool isHit;
    public bool isSafeBlock;

    [Header("Variables")]
    public int myID;
    public float moveSpeed = 14.0f;
    public float rotateSpeed = 10.0f;
    public Color myColor;

    protected int flagAmount;
    public int FlagAmount
    {
        get
        {
            return flagAmount;
        }
        set
        {
            flagAmount = value;
            flagTMP.text = flagAmount.ToString();
        }
    }

    public HumanState humanState;
    public enum HumanState
    {
        Idle,
        Running,
        Fall,
        Dance,
        Dead
    }

    protected virtual void Dead()
    {
        humanState = HumanState.Dead;
    }

    protected virtual void Dance()
    {
        humanState = HumanState.Dance;
    }

    protected virtual void Idle()
    {
        humanState = HumanState.Idle;
    }

    protected virtual void Fall()
    {
        if (isHit) return;
        humanState = HumanState.Fall;
        StartCoroutine(C_Fall());
    }

    protected virtual void Running()
    {
        humanState = HumanState.Running;
    }

    public virtual void Hide()
    {
        Refresh();
        gameObject.SetActive(false);
    }

    public virtual void Active()
    {
        gameObject.SetActive(true);
    }

    public virtual void Refresh()
    {
        SetColor();
        Idle();
        rigid.velocity = Vector3.zero;
        model.transform.localPosition = Vector3.up * -0.5f;
        transform.localPosition = startPosArray[myID];
        FlagAmount = 0;
        nameTMP.text = BXHController.Instance.listName[myID];
    }

    protected virtual void SetColor()
    {
        smr.material = mArray[myID];
        myColor = colorArray[myID];
        for (int i = 0; i < flagImage.Length; i++) flagImage[i].color = myColor;
    }

    protected virtual void UpdateMoving(Vector3 direction)
    {
        if (humanState == HumanState.Dead || humanState == HumanState.Fall) return;
        if (humanState != HumanState.Running && direction != Vector3.zero) Running();
        Vector3 _dir = new Vector3(direction.x, 0.0f, direction.z);
        rigid.velocity = _dir * moveSpeed;
        UpdateLimitPosition();
    }

    protected virtual void UpdateLimitPosition()
    {
        float maxPosX = GenerateMap.Instance.scaleMap.x;
        float maxPosZ = GenerateMap.Instance.scaleMap.z;
        Vector3 pos = transform.position;
        if (pos.x >= maxPosX) pos.x = maxPosX;
        else if (pos.x <= -maxPosX) pos.x = -maxPosX;
        if (pos.z >= maxPosZ) pos.z = maxPosZ;
        else if (pos.z <= -maxPosZ) pos.z = -maxPosZ;
        transform.position = pos;
    }

    protected virtual void UpdateRotate(Vector3 direction)
    {
        if (humanState == HumanState.Dead || humanState == HumanState.Fall) return;
        if (direction == Vector3.zero) return;
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotateSpeed);
    }

    protected virtual void UpdateAnimation(Vector3 direction)
    {
        float _moveSpeed = (direction == Vector3.zero) ? 0.0f : 1.0f;
        anitor.SetFloat("MoveSpeed", _moveSpeed);
        if (humanState == HumanState.Fall) return;
        if (_moveSpeed == 0.0f && humanState != HumanState.Idle) Idle();
    }

    protected virtual void UpdateSafeBlock()
    {
      
    }

    protected virtual void CollectFlag(Item _flag)
    {
        FlagAmount++;
        _flag.Hide();
    }

    public virtual void Complete()
    {
        rigid.velocity = Vector3.zero;
        anitor.SetFloat("MoveSpeed", 0.0f);
        Idle();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            CollectFlag(other.GetComponent<Item>());
        }
        else if (other.CompareTag("RedCube"))
        {
            Fall();
        }
        else if (other.CompareTag("Player"))
        {
            Human colPlayer = other.GetComponent<Human>();
            if (colPlayer.FlagAmount > FlagAmount) Fall();
        }
    }

    private IEnumerator C_Fall()
    {
        rigid.velocity = Vector3.zero;

        int r = Random.Range(0, 3);
        string sFall = "Fall_" + r;
        anitor.SetTrigger(sFall);

        StartCoroutine(C_FallEffect());
        yield return new WaitForSeconds(1.5f);
        Idle();
    }

    private IEnumerator C_FallEffect()
    {
        isHit = true;
        int n = 0;

        while (n < 25)
        {
            Material m = (n % 2 == 0) ? mArray[myID] : mFallArray[myID];
            smr.material = m;
            n++;
            yield return new WaitForSeconds(0.1f);
        }

        smr.material = mArray[myID];
        isHit = false;
    }
}
