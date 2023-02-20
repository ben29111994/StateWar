using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using ProjectDawn.Navigation;

namespace ProjectDawn.Navigation.Sample.Zerg
{
    [RequireMatchingQueriesForUpdate]
    public partial class UnitDeathSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity, Transform transform, in UnitDead unitDead) =>
            {
                //GameObject.Destroy(transform.gameObject);
                transform.gameObject.SetActive(false);
                //transform.GetComponent<UnitAuthoring>().SpawnPoint.GetComponent<>()
                //EntityManager.DestroyEntity(entity);
            }).WithStructuralChanges().WithoutBurst().Run();
        }
    }
}
