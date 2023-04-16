using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

//Acción que hace que el agente se diriga hacia las palancas, cuando las alcanza devuelve Success
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

        //Booleanos que indican si las lámparas están en su sitio
        izda = palancaIzda.caido;
        dcha = palancaDcha.caido;

        //Si están en su sitio, se dirige a la más cercana
        if (!izda && !dcha)
        {
            palanca = blackboard.nearestLever(this.gameObject);
            agent.SetDestination(palanca.transform.position);
        }
        //Si una de ellas se ha caído, se dirige a la otra
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
