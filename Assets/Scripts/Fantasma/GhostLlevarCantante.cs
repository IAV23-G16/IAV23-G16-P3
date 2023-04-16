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

//Acción que hace que el agente lleve a la cantante al sótano norte, cuando llega devuelve Success
public class GhostLlevarCantante : Action
{
    NavMeshAgent agent;

    GameObject sotanoNorte;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        sotanoNorte = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().basement;
    }

    public override TaskStatus OnUpdate()
    {
        if (agent.enabled)
        {
            //El agente se dirige hacia el sótano norte
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
