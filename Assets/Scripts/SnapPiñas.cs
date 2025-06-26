using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapPiñas : MonoBehaviour
{
    public GameObject objetoVisual; // El hijo visual (piña)
    public Vector3 posicionOrigen;  // Posición exacta de inserción
    public Quaternion rotacionOrigen; // Rotación exacta de inserción
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
            Debug.LogWarning("SnapPiñas: No se encontró objetoVisual en " + gameObject.name);

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
            Debug.Log("SnapPiñas: Listener agregado a " + objetoVisual.name);
        }
        else
        {
            Debug.LogWarning("SnapPiñas: No se encontró XRGrabInteractable en " + (objetoVisual != null ? objetoVisual.name : "N/A"));
        }

        tiempoUltimaManipulacion = Time.time;
        Debug.Log("SnapPiñas: Inicio completado para " + objetoVisual?.name);
    }

    void Update()
    {
        // Si está dentro del área snap, nada de esto se ejecuta
        if (enZonaSnap)
            return;

        if (objetoVisual != null && objetoVisual.transform.position.y < 0)
        {
            Debug.Log("SnapPiñas: Snap por caída fuera del rango [" + objetoVisual.name + "]");
            Regresar();
            return;
        }

        if (!EstaSiendoManipulada() && (Time.time - tiempoUltimaManipulacion > tiempoRegreso))
        {
            Debug.Log("SnapPiñas: Snap por tiempo sin manipulación [" + objetoVisual.name + "]");
            Regresar();
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("SnapPiñas: ¡Piña agarrada! [" + objetoVisual.name + "]");
        tiempoUltimaManipulacion = Time.time;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("SnapPiñas: ¡Piña soltada! [" + objetoVisual.name + "]");
        tiempoUltimaManipulacion = Time.time;
    }

    private bool EstaSiendoManipulada()
    {
        if (grabInteractable == null)
        {
            Debug.LogWarning("SnapPiñas: No hay XRGrabInteractable asignado para [" + objetoVisual?.name + "]");
            return false;
        }
        return grabInteractable.isSelected;
    }

    private void Regresar()
    {
        if (objetoVisual != null)
        {
            Debug.Log("SnapPiñas: Regresando piña a posición original [" + objetoVisual.name + "] Pos: " + posicionOrigen);

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                Debug.Log("SnapPiñas: Rigidbody reseteado para [" + objetoVisual.name + "]");
            }

            objetoVisual.transform.position = posicionOrigen;
            objetoVisual.transform.rotation = rotacionOrigen;

            if (rb != null)
                StartCoroutine(ReactivarFisica(rb));
        }
        tiempoUltimaManipulacion = Time.time;
    }

    // Corrutina para reactivar física tras mover
    private System.Collections.IEnumerator ReactivarFisica(Rigidbody rb)
    {
        yield return null;
        if (!enZonaSnap) // Solo reactivar física si está fuera de la zona snap
            rb.isKinematic = false;
    }

    // ----------- NUEVO: métodos para usar desde el SuperContenedor -----------
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
        Debug.Log("SnapPiñas: Entró a la zona snap [" + objetoVisual.name + "]");
    }

    public void SalirZonaSnap()
    {
        enZonaSnap = false;
        if (rb != null)
            rb.isKinematic = false;
        Debug.Log("SnapPiñas: Salió de la zona snap [" + objetoVisual.name + "]");
    }
}
