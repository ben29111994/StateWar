using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommand : MonoBehaviour
{
    public SpawnPoint spawnPoint;
    public UnitAuthoring unitCommand;

    private void Awake()
    {
        unitCommand = GetComponent<UnitAuthoring>();
    }

    private void OnEnable()
    {
        //Adjust Move Speed
        unitCommand.MoveAnimationSpeed = 0.05f + spawnPoint.currentSpawn * 0.05f;
    }

    [System.Obsolete]
    private void OnDisable()
    {
        spawnPoint.OnUnitDead();
        spawnPoint.GetComponent<EffectManager>().Item(transform.position, spawnPoint.unitColor);
    }
}
