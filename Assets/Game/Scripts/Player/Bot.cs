using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Human
{
    private StateBot stateBot;
    private float timeChangeState;

    private Transform safeBlock;

    private enum StateBot
    {
        SafeBlock,
        CollectFlag
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isComplete || GameManager.Instance.isStart == false) return;

        UpdateMoving(SwipeDirection());
        UpdateRotate(SwipeDirection());
        UpdateAnimation(SwipeDirection());
        UpdateSafeBlock();

      //  if (myID == 1) IsWarning(); 
    }

    public override void Hide()
    {
        StopAI();
        base.Hide();
    }

    protected override void Dead()
    {
        base.Dead();
    }

    protected override void Idle()
    {
        base.Idle();
    }

    protected override void CollectFlag(Item _flag)
    {
        base.CollectFlag(_flag);
    }

    private Vector3 SwipeDirection()
    {
        if (GetTarget() == null) return Vector3.zero;
        Vector3 dir = GetTarget().position - transform.position;
        dir.y = 0.0f;
        if (dir.sqrMagnitude < 0.02f) return Vector3.zero;
        return dir.normalized;
    }

    private void StopAI()
    {
        StopAllCoroutines();
    }

    public void StartAI()
    {
        StartCoroutine(C_UpdateAI());
        StartCoroutine(C_AutoGetSafeBlock());
    }

    private IEnumerator C_UpdateAI()
    {     
        while (true)
        {
            if(stateBot == StateBot.CollectFlag)
            {
                stateBot = StateBot.SafeBlock;
            }
            else if(stateBot == StateBot.SafeBlock)
            {
                stateBot = StateBot.CollectFlag;
            }

            if(stateBot == StateBot.CollectFlag)
            {
                timeChangeState = myID >= 2 ? Random.Range(6.0f, 10.0f) : Random.Range(2.0f, 4.0f);
            }
            else
            {
                timeChangeState = myID >= 2 ? Random.Range(2.0f, 3.0f) : Random.Range(4.0f, 6.0f);
            }

            yield return new WaitForSeconds(timeChangeState);
        }
    }

    private Transform GetTarget()
    {
        if(stateBot == StateBot.SafeBlock)
        {
            return safeBlock;
        }
        else if(stateBot == StateBot.CollectFlag)
        {
            Item flag = GetFlag();
            if (flag == null) return null;
            return GetFlag().transform;
        }

        return null;
    }
    private Item GetFlag()
    {
        int targetID = 0;
        float distanceA = 999.0f;
        for(int i = 0; i < FlagController.Instance.listItem.Count;i++)
        {
            Item _flag = FlagController.Instance.listItem[i];
            if(_flag != null)
            {
                float distanceB = Vector3.Distance(FlagController.Instance.listItem[i].transform.position, transform.position);
                if (distanceB < distanceA)
                {
                    distanceA = distanceB;
                    targetID = i;
                }
            }
        }

        if (FlagController.Instance.listItem.Count == 0) return null;
        Item flag = FlagController.Instance.listItem[targetID];
        return flag;
    }

    private void HandleTimeChangeState()
    {
        if(stateBot == StateBot.SafeBlock)
        {
            timeChangeState = Random.Range(2.0f, 6.0f);
        }
        else
        {
            timeChangeState = Random.Range(2.0f, 6.0f);
        }
    }

    private IEnumerator C_AutoGetSafeBlock()
    {
        float timeChangeSafeBlock = Random.Range(1.0f, 4.0f);

        while (true)
        {
            safeBlock = GenerateMap.Instance.listSafeCube[Random.Range(0, GenerateMap.Instance.listSafeCube.Count)].transform;
            yield return new WaitForSeconds(timeChangeSafeBlock);
            timeChangeSafeBlock = Random.Range(1.0f, 4.0f);
        }
    }

    public bool IsWarning()
    {
        float dotProduct = Vector3.Dot(model.forward, RedCubeDirection().normalized);
        return false;
    }

    public Vector3 RedCubeDirection()
    {
        Vector3 origin = Vector3.zero;
        List<Cube> listRedCube = GenerateMap.Instance.ListRedCube();
        for(int i = 0; i < listRedCube.Count; i++) origin += listRedCube[i].transform.position;
        origin /= listRedCube.Count;
        Vector3 dir = transform.position - origin;
        dir.y = 0.0f;
        return dir;
    }
}
