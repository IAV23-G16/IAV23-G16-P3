using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCapturaVozconde : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Cantante>() != null)
        {
            other.GetComponent<Cantante>().setCapturada(true, false);
        }
    }
}
