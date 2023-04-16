/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com
   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).
   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;


//Acción que hace que el agente cierre la puerta de la celda, yendo hacia la palanca, cuando la alcanza devuelve Success


public class GhostCloseDoorAction : Action
{
    NavMeshAgent agent;
    GameBlackboard blackboard;
    GameObject puerta;
    [SerializeField] PalancaPuerta palanca;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        puerta = blackboard.puerta;
        palanca = blackboard.palancaCelda;
    }

    public override TaskStatus OnUpdate()
    {
        //El agente se dirige hacia la puerta
        agent.SetDestination(puerta.transform.position);
        if (Vector3.SqrMagnitude(transform.position - puerta.transform.position) < 1.5f)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}