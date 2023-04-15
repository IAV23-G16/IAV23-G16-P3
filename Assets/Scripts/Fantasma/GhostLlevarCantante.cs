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

/*
 * Accion de seguir a la cantante, cuando la alcanza devuelve Success
 */

public class GhostLlevarCantante : Action
{
    NavMeshAgent agent;
    GameObject singer;

    GameObject sotanoNorte;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        sotanoNorte = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().basement;
        singer = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().singer;
    }

    public override TaskStatus OnUpdate()
    {
        if (agent.enabled)
        {
            agent.SetDestination(sotanoNorte.transform.position);
        }
        if (Vector3.SqrMagnitude(transform.position - sotanoNorte.transform.position) < 1.2f)
        {
            return TaskStatus.Success;
        }
        else return TaskStatus.Running;
        return TaskStatus.Running;

    }
}
