using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CantanteMerodeoAction : Action
{
    NavMeshAgent agent;
    Cantante cantante_script;
    // Start is called before the first frame update


    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        cantante_script = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().singer.GetComponent<Cantante>();
    }

    public override TaskStatus OnUpdate()
    {
        //Si esta perdida devuelve false
        if (!cantante_script.cantando && !cantante_script.descansando)
            return TaskStatus.Failure;
        else
            return TaskStatus.Success;
    }
}
