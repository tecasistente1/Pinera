using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapPiñas : MonoBehaviour
{
    public GameObject objetoVisual;
    public Vector3 posicionOrigen;
    public Quaternion rotacionOrigen;
    private float tiempoUltimaManipulacion;
    public float tiempoRegreso = 15f;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    private bool enSnapCamion = false;
    private Rigidbody lastRigidBody;

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
        }
        tiempoUltimaManipulacion = Time.time;
    }

    public void EntrarZonaSnapCamion(Transform puntoSnap)
    {
        if (objetoVisual != null)
        {
            // Quita selección si alguien la estaba agarrando
            if (grabInteractable != null && grabInteractable.isSelected)
                grabInteractable.interactionManager.SelectExit(grabInteractable.firstInteractorSelecting, grabInteractable);

            Rigidbody rbActual = objetoVisual.GetComponent<Rigidbody>();
            if (rbActual != null)
            {
                rbActual.isKinematic = true;
                rbActual.velocity = Vector3.zero;
                rbActual.angularVelocity = Vector3.zero;
                lastRigidBody = rbActual;
            }

            // Coloca la piña exactamente en el snap y con la rotación del attachPoint
            objetoVisual.transform.position = puntoSnap.position;
            objetoVisual.transform.rotation = puntoSnap.rotation;

            enSnapCamion = true;
        }
    }

    public void SalirZonaSnapCamion()
    {
        enSnapCamion = false;
        if (lastRigidBody != null)
            lastRigidBody.isKinematic = false;
    }

    void Update()
    {
        // Si está en el snap del camión, no hacer nada
        if (enSnapCamion) return;
        // Aquí puedes poner la lógica de regreso o lo que quieras para fuera del camión
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        tiempoUltimaManipulacion = Time.time;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        tiempoUltimaManipulacion = Time.time;
        if (rb != null) rb.isKinematic = false; // Por si sale del snap agarrada
    }
}
