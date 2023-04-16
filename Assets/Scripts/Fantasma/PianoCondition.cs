/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com
   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Inform�tica de la Universidad Complutense de Madrid (Espa�a).
   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Condici�n que comprueba si el piano est� roto
public class PianoCondition : Conditional
{
    ControlPiano piano;

    public override void OnAwake()
    {
        piano = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().piano.GetComponent<ControlPiano>();
    }

    public override TaskStatus OnUpdate()
    {
        if (piano.roto)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
