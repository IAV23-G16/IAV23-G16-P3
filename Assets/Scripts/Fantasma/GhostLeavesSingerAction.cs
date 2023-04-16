using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

//Acción que hace que el agente deje a la cantante, cuando la deja devuelve Success
public class GhostLeavesSingerAction : Action
{
    GameObject cantante;
    public override void OnAwake()
    {
        cantante = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().singer;
    }

    public override TaskStatus OnUpdate()
    {
        cantante.transform.SetParent(null);
        cantante.GetComponent<Cantante>().setCapturada(false, false);
        return TaskStatus.Success;
    }
}
