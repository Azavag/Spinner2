using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwampControl : MonoBehaviour
{
    NavMeshAgent[] agents;
    //float agentsSpeedArr;
    float slowPercent = 30;
    Dictionary<NavMeshAgent, float> agentsSpeedDict = new Dictionary<NavMeshAgent, float>();
    // Start is called before the first frame update
    void Start()
    {
        agents = FindObjectsOfType<NavMeshAgent>();
        foreach (NavMeshAgent agent in agents) 
        {
            agentsSpeedDict.Add(agent, agent.speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out NavMeshAgent tempAgent))
        {
            return;
        }
        tempAgent.speed -= tempAgent.speed * (slowPercent / 100);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out NavMeshAgent tempAgent))
        {
            return;
        }
        tempAgent.speed = agentsSpeedDict[tempAgent];
    }



}
