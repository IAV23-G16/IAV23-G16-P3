﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Se encarga de controlar si el público debería huir o quedarse en el patio de butacas
 */

public class Publico : MonoBehaviour
{
    GameBlackboard blackboard;
    NavMeshAgent agent;
    GameObject hall;
    GameObject luzAsociada;

    Vector3 initialPositon;

    bool miLuzEncendida;
    bool sentado = true;
    public bool mitadDerecha;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        //lucesEncendidas = 2;
        sentado = true;
        initialPositon = transform.position;
        hall = blackboard.hall;
    }

    public void LateUpdate()
    {
        //para que rote hacia donde se mueve
        if (GetComponent<NavMeshAgent>().velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<NavMeshAgent>().velocity.normalized);
        }
        else if(miLuzEncendida)  //para que al llegar a su butaca miren hacia delante(el escenario)
            transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public bool getLuces()
    {
        return sentado;
    }

    public void apagaLuz()
    {
        miLuzEncendida = false;
        sentado = false;
        agent.SetDestination(hall.transform.position);
        //lucesEncendidas--;
        //sentado = lucesEncendidas == 2;
    }
    //se llama cuando el fantasma o el vizconde desactivan o activan las luces
    public void enciendeLuz()
    {
        miLuzEncendida = true;
        sentado = true;
        agent.SetDestination(initialPositon);
        // lucesEncendidas++;
        //sentado = lucesEncendidas == 2;
    }

}
