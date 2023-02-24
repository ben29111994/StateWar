using UnityEngine;
using ProjectDawn.Navigation.Hybrid;
using System.Collections.Generic;

namespace ProjectDawn.Navigation.Sample.Scenarios
{
    [RequireComponent(typeof(AgentAuthoring))]
    [DisallowMultipleComponent]
    public class AgentDestinationAuthoring : MonoBehaviour
    {
        public List<Transform> listTarget = new List<Transform>();
        public float Radius;

        private void Start()
        {
            var agent = transform.GetComponent<AgentAuthoring>();
            var body = agent.EntityBody;
            var pickTarget = listTarget[Random.Range(0, listTarget.Count - 1)];
            body.Destination = pickTarget.position;
            body.IsStopped = false;
            agent.EntityBody = body;
        }
    }
}
