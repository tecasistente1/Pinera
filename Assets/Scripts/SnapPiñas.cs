using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapPi�as : MonoBehaviour
{
    public GameObject objetoVisual; // El hijo visual (pi�a)
    public Vector3 posicionOrigen;  // Posici�n exacta de inserci�n
    public Quaternion rotacionOrigen; // Rotaci�n exacta de inserci�n
    private float tiempoUltimaManipulacion;
    public float tiempoRegreso = 15f;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private bool enZonaSnap = false; // <--- NUEVO

    void Start()
    {
        if (objetoVisual == null && transform.childCount > 0)
            objetoVisual = transform.GetChild(0).gameObject;

        if (objetoVisual == null)
            Debug.LogWarning("SnapPi�as: No se encontr� objetoVisual en " + gameObject.name);

        if (posicionOrigen == Vector3.zero)
            posicionOrigen = objetoVisual != null ? objetoVisual.transform.position : transform.position;
        if (rotacionOrigen == Quaternion.identity)
            rotacionOrigen = objetoVisual != null ? objetoVisual.transform.rotation : transform.rotation;

        grabInteractable = objetoVisual != null ? objetoVisual.GetComponent<XRGrabInteractable>() : null;
        rb = objetoVisual != null ? objetoVisual.GetComponent<Rigidbody>() : null;

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
            Debug.Log("SnapPi�as: Listener agregado a " + objetoVisual.name);
        }
        else
        {
            Debug.LogWarning("SnapPi�as: No se encontr� XRGrabInteractable en " + (objetoVisual != null ? objetoVisual.name : "N/A"));
        }

        tiempoUltimaManipulacion = Time.time;
        Debug.Log("SnapPi�as: Inicio completado para " + objetoVisual?.name);
    }

    void Update()
    {
        // Si est� dentro del �rea snap, nada de esto se ejecuta
        if (enZonaSnap)
            return;

        if (objetoVisual != null && objetoVisual.transform.position.y < 0)
        {
            Debug.Log("SnapPi�as: Snap por ca�da fuera del rango [" + objetoVisual.name + "]");
            Regresar();
            return;
        }

        if (!EstaSiendoManipulada() && (Time.time - tiempoUltimaManipulacion > tiempoRegreso))
        {
            Debug.Log("SnapPi�as: Snap por tiempo sin manipulaci�n [" + objetoVisual.name + "]");
            Regresar();
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("SnapPi�as: �Pi�a agarrada! [" + objetoVisual.name + "]");
        tiempoUltimaManipulacion = Time.time;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("SnapPi�as: �Pi�a soltada! [" + objetoVisual.name + "]");
        tiempoUltimaManipulacion = Time.time;
    }

    private bool EstaSiendoManipulada()
    {
        if (grabInteractable == null)
        {
            Debug.LogWarning("SnapPi�as: No hay XRGrabInteractable asignado para [" + objetoVisual?.name + "]");
            return false;
        }
        return grabInteractable.isSelected;
    }

    private void Regresar()
    {
        if (objetoVisual != null)
        {
            Debug.Log("SnapPi�as: Regresando pi�a a posici�n original [" + objetoVisual.name + "] Pos: " + posicionOrigen);

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                Debug.Log("SnapPi�as: Rigidbody reseteado para [" + objetoVisual.name + "]");
            }

            objetoVisual.transform.position = posicionOrigen;
            objetoVisual.transform.rotation = rotacionOrigen;

            if (rb != null)
                StartCoroutine(ReactivarFisica(rb));
        }
        tiempoUltimaManipulacion = Time.time;
    }

    // Corrutina para reactivar f�sica tras mover
    private System.Collections.IEnumerator ReactivarFisica(Rigidbody rb)
    {
        yield return null;
        if (!enZonaSnap) // Solo reactivar f�sica si est� fuera de la zona snap
            rb.isKinematic = false;
    }

    // ----------- NUEVO: m�todos para usar desde el SuperContenedor -----------
    public void EntrarZonaSnap()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        objetoVisual.transform.position = posicionOrigen;
        objetoVisual.transform.rotation = rotacionOrigen;
        enZonaSnap = true;
        Debug.Log("SnapPi�as: Entr� a la zona snap [" + objetoVisual.name + "]");
    }

    public void SalirZonaSnap()
    {
        enZonaSnap = false;
        if (rb != null)
            rb.isKinematic = false;
        Debug.Log("SnapPi�as: Sali� de la zona snap [" + objetoVisual.name + "]");
    }
}
