using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMoving : MonoBehaviour
{
    [Header("Input")]
    public float timeDelay;
    public NameMovingType nameMovingType;

    public enum NameMovingType
    {
        Moving_0,
        Moving_1,
        Moving_2,
        Moving_3,
        Moving_4,
        Moving_5
    }
    private Animator animtor;

    private void Awake()
    {
        TryGetComponent();
    }

    private void OnEnable()
    {
        animtor.enabled = false;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
        Invoke("SetMovingAnimation",1.0f + timeDelay);
    }

    private void TryGetComponent()
    {
        animtor = transform.GetComponent<Animator>();
    }
    
    private void SetMovingAnimation()
    {
        float maxCount = System.Enum.GetValues(typeof(NameMovingType)).Length - 1;
        float blend = (int)nameMovingType / maxCount;

        animtor.enabled = true;
        animtor.SetFloat("Blend", blend);
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(true);
    }
}
