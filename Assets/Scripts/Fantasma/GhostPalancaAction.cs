using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class GhostPalanca : Action
{
    NavMeshAgent agent;
    GameBlackboard blackboard;
    ControlPalanca palancaIzda;
    ControlPalanca palancaDcha;
    GameObject palanca;

    bool izda;
    bool dcha;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        palancaIzda = blackboard.eastLever.GetComponentInChildren<ControlPalanca>();
        palancaDcha = blackboard.westLever.GetComponentInChildren<ControlPalanca>();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (!agent.enabled)
            return TaskStatus.Running;


        izda = palancaIzda.caido;
        dcha = palancaDcha.caido;

        if (!izda && !dcha)
        {
            palanca = blackboard.nearestLever(this.gameObject);
            agent.SetDestination(palanca.transform.position);
        }
        else if (!izda)
        {
            agent.SetDestination(palancaIzda.transform.position);
        }
        else
            agent.SetDestination(palancaDcha.transform.position);

        if (izda && dcha)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;

    }
}
