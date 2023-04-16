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

//Acción que hace que el agente arregle el piano
public class GhostArreglaPianoAction : Action
{
    NavMeshAgent agent;
    ControlPiano pianoControl;
    GameObject piano;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        pianoControl = GameObject.FindGameObjectWithTag("Piano").GetComponent<ControlPiano>();
        piano = GameObject.FindGameObjectWithTag("Piano");
    }

    public override TaskStatus OnUpdate()
    {
        //El agente se dirige hacia el piano
        agent.SetDestination(piano.transform.position);
        //Si está roto y se acerca
        if (pianoControl.roto && transform.position.x - piano.transform.position.x < 2f && transform.position.z - piano.transform.position.z < 2f)
        {
            //Lo arregla
            pianoControl.ArreglaPiano();
            return TaskStatus.Success;
        }
        else return TaskStatus.Running;
    }
}
