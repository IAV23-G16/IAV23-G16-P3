using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;


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
