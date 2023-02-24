using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommand : MonoBehaviour
{
    public SpawnPoint spawnPoint;

    private void OnDisable()
    {
        spawnPoint.OnUnitDead();
        spawnPoint.GetComponent<EffectManager>().Item(transform.position, spawnPoint.unitColor);
    }
}
