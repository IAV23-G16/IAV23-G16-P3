using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorPublico : MonoBehaviour
{
    int countPublico = 0;
    [SerializeField] bool mitadDerecha;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Publico>() != null)
        {
            if (other.GetComponent<Publico>().mitadDerecha == mitadDerecha)
            {
                countPublico++;
                Debug.Log("Público ha entrado: Quedan " + countPublico);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Publico>() != null)
        {
            if (other.GetComponent<Publico>().mitadDerecha == mitadDerecha)
            {
                countPublico--;
                Debug.Log("Público ha salido: Quedan " + countPublico);
            }
        }
    }

    public int getCount()
    {
        return countPublico;
    }
}
