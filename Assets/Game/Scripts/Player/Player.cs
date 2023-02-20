using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    [Header("Input")]
    public float cameraSpeed;

    [Header("References")]
    public Transform offsetCamera;
    public DynamicJoystick joyStick;

    private void FixedUpdate()
    {
        if (GameManager.Instance.isComplete || GameManager.Instance.isStart == false) return;

        UpdateMoving(SwipeDirection());
        UpdateRotate(SwipeDirection());
        UpdateAnimation(SwipeDirection());
        UpdateSafeBlock();
        UpdateCamera();

        if (Input.GetKeyDown(KeyCode.F)) Fall();
    }

    public override void Refresh()
    {
        base.Refresh();
        offsetCamera.transform.position = transform.position;
    }

    private void UpdateCamera()
    {
        Vector3 _pos = model.position;
        _pos.y = offsetCamera.position.y;
        offsetCamera.position = Vector3.Lerp(offsetCamera.position, _pos, Time.deltaTime * cameraSpeed);
    }

    protected override void Dead()
    {
        base.Dead();
    }

    protected override void Idle()
    {
        base.Idle();
    }

    protected override void Fall()
    {
        base.Fall();
        GameManager.Instance.Vibration();
    }

    public override void Complete()
    {
        base.Complete();
        joyStick.ResetPos();
        joyStick.gameObject.SetActive(false);
    }

    protected override void CollectFlag(Item _flag)
    {
        base.CollectFlag(_flag);
        GameManager.Instance.Vibration();
    }

    private Vector3 SwipeDirection()
    {
        return new Vector3(joyStick.Direction.x, 0.0f, joyStick.Direction.y);
    }
}
