﻿/*    
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


//Acción que hace que el agente siga a la cantante, cuando la alcanza devuelve Success
public class GhostChaseAction : Action
{
    NavMeshAgent agent;
    GameObject singer;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        singer = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().singer;
    }

    public override TaskStatus OnUpdate()
    {
        //El agente se dirige hacia la cantante
        agent.SetDestination(singer.transform.position);
        if (Vector3.SqrMagnitude(singer.transform.position - this.transform.position) < 1.5f)
        {
            //La captura
            singer.GetComponent<Cantante>().setCapturada(true, true);
            return TaskStatus.Success;
        }
        else return TaskStatus.Running;
    }
}
