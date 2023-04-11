using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public partial class SelectionSystem : MonoBehaviour
{
    //NativeList<Entity> m_SelectedEntities;
    SelectionRectangle m_SelectionRectangle;
    Gestures m_Gestures;

    protected void OnCreate()
    {
        //Debug.LogWarning("Create");
        //m_SelectedEntities = new NativeList<Entity>(Allocator.Persistent);
        //m_SelectionRectangle = GameObject.FindObjectOfType<SelectionRectangle>(true);
        //m_Gestures = GameObject.FindObjectOfType<Gestures>(true);

        //World.EntityManager.CreateSingleton(new Singleton
        //{
        //    SelectedEntities = m_SelectedEntities,
        //}, "Selection");
    }

    public struct Singleton
    {
        //public NativeList<Entity> SelectedEntities;
    }

    protected void OnDestroy()
    {
        //m_SelectedEntities.Dispose();
    }

    protected void OnUpdate()
    {
        ////Debug.LogError( m_SelectionRectangle + " " + m_Gestures);
        //if (m_SelectionRectangle == null)
        //    return;

        //if (m_Gestures == null)
        //    return;

        //var selection = SystemAPI.GetSingletonRW<Singleton>();
        //var selectectEntities = selection.ValueRW.SelectedEntities;

        //if (m_Gestures.Swipe(out Rect rect))
        //{
        //    m_SelectionRectangle.Show(rect);
        //    selectectEntities.Clear();
        //    Entities.ForEach((Entity entity, Transform transformUnit, in Unit unit, in LocalTransform transform) =>
        //    {
        //        Vector3 position = Camera.main.WorldToScreenPoint(transform.Position);
        //        if (rect.Contains(position) && unit.Owner == PlayerId.Red)
        //        {
        //            if (!Gestures.listSelected.Contains(transformUnit.gameObject))
        //            {
        //                Gestures.listSelected.Add(transformUnit.gameObject);
        //            }
        //            selectectEntities.Add(entity);
        //        }
        //    }).WithoutBurst().Run();
        //}

        //if (m_Gestures.SelectionExit(out rect))
        //{
        //    m_SelectionRectangle.Hide();
        //}
    }
}
