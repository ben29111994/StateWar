using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using ProjectDawn.Navigation;

[RequireMatchingQueriesForUpdate]
public partial class UnitDeathSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, Transform transform, in UnitDead unitDead) =>
        {
            //transform.gameObject.SetActive(false);
            GameObject.Destroy(transform.gameObject);
            EntityManager.DestroyEntity(entity);
        }).WithStructuralChanges().WithoutBurst().Run();
    }
}
