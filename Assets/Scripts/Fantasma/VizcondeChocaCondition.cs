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

//Condición que comprueba si el agente ha chocado con el vizconde
public class VizcondeChocaCondition : Conditional
{
    GameBlackboard blackboard;
    GameObject vizconde;
    GameObject cantante;

    public override void OnAwake()
    {
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
