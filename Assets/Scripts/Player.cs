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
using UnityEngine.AI;

/*
 * Clase que se encarga de controlar las acciones del jugador
 */

public class Player : MonoBehaviour
{
    public float tiempoEsperaAtaque = 1;
    public float tiempoEsperaAccion = 0.5f;
    public GameObject areaAtaque;
    public GameObject areaAccion;
    public GameObject areaCaptura;
    private float tiempoAtaqueActivo;
    private float ultimoAtaque = 0;
    private float tiempoAccionActiva;
    private float ultimaAccion = 0;
    private Animator anim;
    private Rigidbody rb;

    public GameObject cantantePruebas;
    void Start()
    {
        tiempoAtaqueActivo = tiempoEsperaAtaque / 2;
        anim = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //desactivamos la animacion para que solo la haga una vez
        if (anim.GetBool("Usable"))
            anim.SetBool("Usable", false);
        //desactivamos la animacion para que solo la haga una vez
        if (anim.GetBool("Attack"))
            anim.SetBool("Attack", false);
        //realiza la accion "usar" (para arreglas luces y tocar piano)
        if (Input.GetMouseButtonDown(1) && ultimaAccion <= 0)
        {
            ultimaAccion = tiempoEsperaAccion;
            areaAccion.SetActive(true);
            areaAtaque.SetActive(true);
            areaCaptura.SetActive(true);
            anim.SetBool("Usable", true);
        }
        else if (ultimaAccion > 0)
        {
            ultimaAccion -= Time.deltaTime;

        }
        if (ultimaAccion < tiempoAccionActiva && areaAccion.activeSelf)
        {
            areaAccion.SetActive(false);
            areaAtaque.SetActive(false);
            areaCaptura.SetActive(false);
            anim.SetBool("Usable", false);
        }
        anim.SetFloat("Speed", GetComponent<NavMeshAgent>().velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.P))
        {
            cantantePruebas.GetComponent<Cantante>().setCapturada(true, false);
        }
        //    if (Input.GetKeyDown(KeyCode.E) && ultimaAccion <=0)
        //{
        //    ultimaAccion = tiempoEsperaAccion;
        //    areaAccion.SetActive(true);
        //    anim.SetBool("Usable", true);
        //}
        //else if (ultimaAccion > 0)
        //{
        //    ultimaAccion -= Time.deltaTime;

        //}
        //if (ultimaAccion < tiempoAccionActiva && areaAccion.activeSelf)
        //{
        //    areaAccion.SetActive(false);
        //    anim.SetBool("Attack", false);
        //}
        ////realiza la accion "atacar" (para atacar al fantasma)
        //if (Input.GetKeyDown(KeyCode.Space) && ultimoAtaque <=0)
        //{
        //    ultimoAtaque = tiempoEsperaAtaque;
        //    areaAtaque.SetActive(true);
        //    anim.SetBool("Attack",true);
        //}//reduce el tiempo hasta que puede volver a atacar
        //else if (ultimoAtaque > 0)
        //{
        //    ultimoAtaque -= Time.deltaTime;

        //}//desactiva el area de ataque
        //if (ultimoAtaque < tiempoAtaqueActivo && areaAtaque.activeSelf)
        //{
        //    areaAtaque.SetActive(false);
        //    anim.SetBool("Attack", false);
        //}
        ////realiza la acción de captura
        //if (Input.GetKeyDown(KeyCode.Q) && ultimaAccion <= 0)
        //{
        //    ultimaAccion = tiempoEsperaAccion;
        //    areaCaptura.SetActive(true);
        //    anim.SetBool("Usable", true);
        //}
        //else if (ultimaAccion > 0)
        //{
        //    ultimaAccion -= Time.deltaTime;

        //}
        //if (ultimaAccion < tiempoAccionActiva && areaCaptura.activeSelf)
        //{
        //    areaCaptura.SetActive(false);
        //    anim.SetBool("Attack", false);
        //}
        //anim.SetFloat("Speed", GetComponent<NavMeshAgent>().velocity.magnitude);


    }
}
