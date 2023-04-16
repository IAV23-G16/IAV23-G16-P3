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

//Condición que comprueba si la cantante ha sido capturada por el fantasma
public class CapturadaCondition : Conditional
{
    Cantante cantante;

    public override void OnAwake()
    {
        cantante = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().singer.GetComponent<Cantante>();
    }

    public override TaskStatus OnUpdate()
    {
        if (cantante.capturadaPorFant)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}