using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class AgentDestinationAuthoring : MonoBehaviour
{
    public List<Transform> listTarget = new List<Transform>();
    public float Radius;
    public float orderInterval;
    public NavMeshAgent m_NavMeshAgent = null;
    WaitForSeconds intervalCache;

    private void OnEnable()
    {
        intervalCache = new WaitForSeconds(orderInterval);
        StartCoroutine(delayCommand());
    }

    IEnumerator delayCommand()
    {
        yield return intervalCache;
        var pickTarget = listTarget[Random.Range(0, listTarget.Count)];
        m_NavMeshAgent.SetDestination(pickTarget.position);
        //var agent = transform.GetComponent<AgentAuthoring>();
        //var body = agent.EntityBody;
        //if (listTarget.Count > 0)
        //{
        //    var pickTarget = listTarget[Random.Range(0, listTarget.Count)];
        //    body.Destination = pickTarget.position;
        //    body.IsStopped = false;
        //    agent.EntityBody = body;
        //}
        StartCoroutine(delayCommand());
    }
}
