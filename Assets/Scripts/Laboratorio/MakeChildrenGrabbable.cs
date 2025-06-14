using UnityEngine;


public class MakeChildrenGrabbable : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            // Asegurar que tenga collider
            if (child.GetComponent<Collider>() == null)
                child.gameObject.AddComponent<BoxCollider>();

            // Asegurar que tenga rigidbody
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb == null)
                rb = child.gameObject.AddComponent<Rigidbody>();

            rb.mass = 1f;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Asegurar que tenga XRGrabInteractable
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = child.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grab == null)
                grab = child.gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

            // Configuración correcta para agarre libre
            grab.movementType = UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable.MovementType.VelocityTracking;
            grab.trackPosition = true;
            grab.trackRotation = true;
            grab.throwOnDetach = true;

            // ❌ No usar attachTransform en lo absoluto
            grab.attachTransform = null;
        }
    }
}
