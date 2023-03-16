using UnityEngine;
using ProjectDawn.Navigation.Hybrid;
using System.Collections.Generic;

namespace ProjectDawn.Navigation.Sample.Scenarios
{
    [RequireComponent(typeof(AgentAuthoring))]
    [DisallowMultipleComponent]
    public class AgentDestinationAuthoring : MonoBehaviour
    {
        public List<Vector3> listTarget = new List<Vector3>();
        public float Radius;

        private void OnEnable()
        {
            var agent = transform.GetComponent<AgentAuthoring>();
            var body = agent.EntityBody;
            if (listTarget.Count > 0)
            {
                var pickTarget = listTarget[Random.Range(0, listTarget.Count - 1)];
                body.Destination = pickTarget;
                body.IsStopped = false;
                agent.EntityBody = body;
            }
        }
    }
}
