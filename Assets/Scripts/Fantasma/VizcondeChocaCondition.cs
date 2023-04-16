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

/*
 * Devuelve Success cuando la cantante es sobre el palco
 */


public class VizcondeChocaCondition : Conditional
{
    NavMeshAgent agent;
    GameBlackboard blackboard;
    GameObject vizconde;
    GameObject cantante;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        vizconde = blackboard.player;
        cantante = blackboard.singer;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other == vizconde.GetComponent<Player>().areaAtaque.GetComponent<BoxCollider>())
        {
            blackboard.hit = true;
            cantante.GetComponent<Cantante>().setCapturada(false, false);
            cantante.GetComponent<NavMeshAgent>().enabled = true;
            //cantante.GetComponent<Cantante>().setCapturada(false, false);
        }
        else
            blackboard.hit = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (blackboard.hit)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;

    }

}
