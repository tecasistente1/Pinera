using UnityEngine;
using System.Collections;

public class CamionKinematic : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null && !rb.isKinematic)
        {
            StartCoroutine(ActivarKinematicDespues(rb, 2f));
            Debug.Log("El Rigidbody del camión se activará como isKinematic después de 2 segundos");
        }
    }

    private IEnumerator ActivarKinematicDespues(Rigidbody rb, float segundos)
    {
        yield return new WaitForSeconds(segundos);
        rb.isKinematic = true;
        Debug.Log("isKinematic activado en el camión: " + gameObject.name);
    }
}
