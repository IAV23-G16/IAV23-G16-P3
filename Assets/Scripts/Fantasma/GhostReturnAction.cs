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


 //Acción que hace que el agente vaya a la sala de musica, cuando llega devuelve Success
 

public class GhostReturnAction : Action
{
    NavMeshAgent agent;
    GameObject musicRoom;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        musicRoom = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().musicRoom;
    }

    public override TaskStatus OnUpdate()
    {
        //El agente se dirige hacia la sala de música
        if (agent.enabled)
            agent.SetDestination(musicRoom.transform.position);
        if (Vector3.SqrMagnitude(transform.position - musicRoom.transform.position) < 1.5f)
        {
            agent.SetDestination(transform.position);
            return TaskStatus.Success;
        }
        else return TaskStatus.Running;
    }
}