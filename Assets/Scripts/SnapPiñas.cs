using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapPiñas : MonoBehaviour
{
    public GameObject objetoVisual;
    public Vector3 posicionOrigen;
    public Quaternion rotacionOrigen;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    private bool yaFueCosechada = false;
    private bool enSnapCamion = false;
    private PuntoSnapPiña slotActual;

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
        rb = objetoVisual != null ? GetComponentInChildren<Rigidbody>() : null;



        if (rb != null)
            rb.isKinematic = true;

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }


    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!yaFueCosechada && rb != null)
        {
            rb.isKinematic = false;
            yaFueCosechada = true;
        }
        enSnapCamion = false;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (rb != null && !enSnapCamion)
        {
            rb.isKinematic = false;
        
            if (slotActual != null)
            {
                slotActual.ResetSlot(this);
                slotActual = null;
            }
        }
    }

    public void EntrarZonaSnapCamion(Transform puntoSnap)
    {
        if (objetoVisual != null && rb != null)
        {
            if (grabInteractable != null && grabInteractable.isSelected)
                grabInteractable.interactionManager.SelectExit(grabInteractable.firstInteractorSelecting, grabInteractable);

            objetoVisual.transform.position = puntoSnap.position;
            objetoVisual.transform.rotation = puntoSnap.rotation;

            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            enSnapCamion = true;

            slotActual = puntoSnap.GetComponent<PuntoSnapPiña>();

            Debug.Log("Piña colocada en el snap del camión: " + gameObject.name);
        }
        Debug.Log("Entrando en zona snap camión: " + gameObject.name);
    }

    public void SalirZonaSnapCamion()
    {
        if (enSnapCamion && rb != null)
        {
            rb.isKinematic = false;
            enSnapCamion = false;

            if (slotActual != null)
            {
                slotActual.ResetSlot(this);
                slotActual = null;
            }
        }
    }
}