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
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

//Condición que comprueba si las lámparas han caído
public class PublicoCondition : Conditional
{
    GameBlackboard blackboard;
    ControlPalanca palancaIzda;
    ControlPalanca palancaDcha;

    bool izda;
    bool dcha;

    public override void OnAwake()
    {
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        palancaIzda = blackboard.eastLever.GetComponentInChildren<ControlPalanca>();
        palancaDcha = blackboard.westLever.GetComponentInChildren<ControlPalanca>();
    }

    public override TaskStatus OnUpdate()
    {
        izda = palancaIzda.caido;
        dcha = palancaDcha.caido;

        if (!izda && !dcha)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
