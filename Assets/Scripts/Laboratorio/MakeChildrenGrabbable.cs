using UnityEngine;


public class MakeChildrenGrabbable : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            // Asegúrate de que el hijo tenga un collider
            if (child.GetComponent<Collider>() == null)
            {
                child.gameObject.AddComponent<BoxCollider>();
            }

            // Asegúrate de que el hijo tenga un rigidbody
            if (child.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
                rb.mass = 1f;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
            }

            // Añade el XRGrabInteractable si no existe
            if (child.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>() == null)
            {
                child.gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            }
        }
    }
}
