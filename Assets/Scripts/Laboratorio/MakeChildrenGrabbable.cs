using UnityEngine;


public class MakeChildrenGrabbable : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Collider>() == null)
            {
                child.gameObject.AddComponent<BoxCollider>();
            }

            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = child.gameObject.AddComponent<Rigidbody>();
            }
            rb.mass = 1f;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = child.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grabInteractable == null)
            {
                grabInteractable = child.gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            }

            grabInteractable.movementType = UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable.MovementType.VelocityTracking;
            grabInteractable.trackPosition = true;
            grabInteractable.trackRotation = true;
            grabInteractable.throwOnDetach = true;
            grabInteractable.attachTransform = null;

            // Crear attachTransform en tiempo real en el punto de contacto
            grabInteractable.selectEntered.AddListener((args) =>
            {
                // Crear un punto de agarre donde se toca el objeto
                Transform interactorAttach = args.interactorObject.GetAttachTransform(args.interactableObject);
                if (interactorAttach != null)
                {
                    grabInteractable.attachTransform = interactorAttach;
                }
            });

            // Restaurar attachTransform al soltarlo
            grabInteractable.selectExited.AddListener((args) =>
            {
                grabInteractable.attachTransform = null;
            });
        }
    }
}
