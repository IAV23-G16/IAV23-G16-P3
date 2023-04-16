using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cantante : MonoBehaviour
{
    // Segundos que estara cantando
    public double tiempoDeCanto;
    // Segundo en el que comezo a cantar
    private double tiempoComienzoCanto;
    // Segundos que esta descanasando
    public double tiempoDeDescanso;
    // Segundo en el que comezo a descansar
    private double tiempoComienzoDescanso;
    // Si esta capturada
    public bool capturada = false;

    [Range(0, 180)]
    // Angulo de vision en horizontal
    public double anguloVistaHorizontal;
    // Distancia maxima de vision
    public double distanciaVista;
    // Objetivo al que ver"
    public Transform objetivo;

    // Segundos que puede estar merodeando
    public double tiempoDeMerodeo;
    // Segundo en el que comezo a merodear
    public double tiempoComienzoMerodeo = 0;
    // Distancia de merodeo
    public int distanciaDeMerodeo = 16;
    // Si canta o no
    public bool cantando = false;
    // Si descansa o no
    public bool descansando = false;
    // Si esta encerrada o no
    public bool encerrada = false;
    //
    public bool capturadaPorFant = false;

    // Componente cacheado NavMeshAgent
    private NavMeshAgent agente;

    // Objetivos de su itinerario
    public Transform Escenario;
    public Transform Bambalinas;

    // La blackboard
    public GameBlackboard bb;

    //para seguir al fantasma o al vizconde
    public GameObject fantasma;
    public GameObject player;

    public void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        agente.updateRotation = false;
    }

    public void LateUpdate()
    {
        if (capturada)
        {
            transform.position = objetivo.position - objetivo.forward.normalized;
        }
        if (agente.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agente.velocity.normalized);
        }
    }

    // Comienza a cantar, reseteando el temporizador
    public void Cantar()
    {
        tiempoComienzoCanto = 0;
        cantando = true;
        descansando = false;
    }

    // Comprueba si tiene que dejar de cantar
    public bool TerminaCantar()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);
        if ((1 << NavMesh.GetAreaFromName("Escenario") & navHit.mask) != 0)
        {
            // Cuenta el tiempo mientras está en el escenario
            tiempoComienzoCanto += Time.deltaTime;
            if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
        }
        return tiempoComienzoCanto >= tiempoDeCanto;
    }

    // Comienza a descansar, reseteando el temporizador
    public void Descansar()
    {
        tiempoComienzoDescanso = 0;
        descansando = true;
        cantando = false;
    }

    // Comprueba si tiene que dejar de descansar
    public bool TerminaDescansar()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);

        // Cuenta el tiempo si está en las Bambalinas
        if ((1 << NavMesh.GetAreaFromName("Bambalinas") & navHit.mask) != 0)
            tiempoComienzoDescanso += Time.deltaTime;

        return tiempoComienzoDescanso >= tiempoDeDescanso;
    }

    // Comprueba si se encuentra en la celda
    public bool EstaEnCelda()
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);
        return (1 << NavMesh.GetAreaFromName("Celda") & navHit.mask) != 0;
    }

    // Comprueba si esta en un sitio desde el cual sabe llegar al escenario
    public bool ConozcoEsteSitio()
    {
        NavMeshPath path = new NavMeshPath();
        agente.CalculatePath(Escenario.position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    // Genera una posicion aleatoria a cierta distancia dentro de las areas permitidas
    private Vector3 RandomNavSphere(float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += transform.position;

        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, distance, 1))
            finalPosition = hit.position;

        return finalPosition;
    }

    // Genera un nuevo punto de merodeo cada vez que agota su tiempo de merodeo actual
    public void IntentaMerodear()
    {
        if (tiempoComienzoMerodeo + tiempoDeMerodeo < Time.time)
        {
            tiempoComienzoMerodeo = Time.time;
            if (agente.enabled)
                agente.SetDestination(RandomNavSphere(distanciaDeMerodeo));
        }
    }
    public bool GetCapturada()
    {
        return capturada;
    }

    public bool GetCapturadaPorFantasma()
    {
        return capturadaPorFant;
    }

    public void setCapturada(bool cap, bool porFantasma)
    {
        capturadaPorFant = porFantasma;
        capturada = cap;

        if (capturada)
        {
            cantando = false;
            if (capturadaPorFant)
                sigueFantasma();
            else
                sigueVizconde();
        }
        else
            capturada = false;
              
    }

    public void sigueFantasma()
    {
        agente.enabled = false;
        objetivo = fantasma.transform;
    }

    public void sigueVizconde()
    {
        if (encerrada) encerrada = false;
        agente.enabled = false;
        objetivo = player.transform;
    }

    private void nuevoObjetivo(GameObject obj)
    {
        agente.SetDestination(obj.transform.position);
    }

    public bool GetDescansando()
    {
        return descansando;
    }

    public bool GetCantando()
    {
        return cantando;
    }
}
