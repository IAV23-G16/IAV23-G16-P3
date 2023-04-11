using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CantanteEncerradaAction : Action
{
    NavMeshAgent agent;
    Cantante cantante;
    // Start is called before the first frame update


    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        cantante = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().singer.GetComponent<Cantante>();
    }

    public override TaskStatus OnUpdate()
    {
        if (cantante.encerrada)
            return TaskStatus.Failure;
        else
            return TaskStatus.Success;
    }
}
