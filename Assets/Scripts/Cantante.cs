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

    // Componente cacheado NavMeshAgent
    private NavMeshAgent agente;

    // Objetivos de su itinerario
    public Transform Escenario;
    public Transform Bambalinas;

    // La blackboard
    public GameBlackboard bb;

    //para seguir al fantasma o al vizconde
    public GameObject fantasma;

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
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position, out navHit, 2f, NavMesh.AllAreas);

        // Comprueba para todos los lugares desde los que hay camino
        return (1 << NavMesh.GetAreaFromName("Escenario") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Bambalinas") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Palco Oeste") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Palco Este") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Butacas") & navHit.mask) != 0 ||
            (1 << NavMesh.GetAreaFromName("Pasillos Escenario") & navHit.mask) != 0;
    }

    //Mira si ve al vizconde con un angulo de vision y una distancia maxima
    public bool Scan()
    {
        double forward = Vector3.Angle(transform.forward, objetivo.position - transform.position);

        if (forward < anguloVistaHorizontal && Vector3.Magnitude(transform.position - objetivo.position) <= distanciaVista)
        {
            // Raycast de visión
            RaycastHit hit;
            if (Physics.Raycast(transform.position, objetivo.position - transform.position, out hit, Mathf.Infinity) && hit.collider.gameObject.GetComponent<Player>())
            {
                return true;
            };
        }
        return false;
    }

    // Genera una posicion aleatoria a cierta distancia dentro de las areas permitidas
    private Vector3 RandomNavSphere(float dist)
    {
        Vector3 randomDir;
        NavMeshHit navHit;
        do
        {
            randomDir = UnityEngine.Random.insideUnitSphere * dist;
            randomDir += gameObject.transform.position;
            NavMesh.SamplePosition(randomDir, out navHit, dist, NavMesh.AllAreas);

        } while ((1 << NavMesh.GetAreaFromName("Escenario") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Bambalinas") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Palco Oeste") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Palco Este") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Butacas") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Walkable") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Jump") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Vestíbulo") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Sótano Este") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Sótano Oeste") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Celda") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Sótano Norte") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Música") & navHit.mask) == 0 &&
            (1 << NavMesh.GetAreaFromName("Pasillos Escenario") & navHit.mask) == 0);

        return navHit.position;
    }

    // Genera un nuevo punto de merodeo cada vez que agota su tiempo de merodeo actual
    public void IntentaMerodear()
    {
        if ((transform.position - agente.destination).magnitude <= 0.1 + agente.stoppingDistance)
        {
            tiempoComienzoMerodeo -= Time.deltaTime;
            if (tiempoComienzoMerodeo <= 0)
            {
                tiempoComienzoMerodeo = tiempoDeMerodeo;
                agente.SetDestination(RandomNavSphere(distanciaDeMerodeo));
            }
        }
    }
    public bool GetCapturada()
    {
        // IMPLEMENTAR
        return true;
    }

    public void setCapturada(bool cap)
    {
        capturada = cap;
    }

    public GameObject sigueFantasma()
    {
        // IMPLEMENTAR
        return null;
    }

    public void sigueVizconde()
    {
        // IMPLEMENTAR
    }

    private void nuevoObjetivo(GameObject obj)
    {
        // IMPLEMENTAR
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
