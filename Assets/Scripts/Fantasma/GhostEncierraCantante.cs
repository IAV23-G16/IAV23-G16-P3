using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class GhostEncierraCantante : Action
{
    NavMeshAgent agent;
    GameBlackboard blackboard;
    GameObject prison;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        prison = blackboard.celda;
    }

    public override TaskStatus OnUpdate()
    {
        if (agent.enabled)
            agent.SetDestination(prison.transform.position);
        if (Vector3.SqrMagnitude(transform.position - prison.transform.position) < 1.5f)
        {
            agent.SetDestination(transform.position);
            blackboard.singer.transform.parent = null;
            blackboard.singer.GetComponent<Cantante>().setCapturada(false, false);
            blackboard.singer.GetComponent<Cantante>().encerrada = true;
            return TaskStatus.Success;
        }
        else return TaskStatus.Running;
    }
}
