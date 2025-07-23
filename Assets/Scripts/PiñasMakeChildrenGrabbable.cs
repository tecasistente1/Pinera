using UnityEngine;


public class PinasMakeChildrenGrabbable : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            Collider col = child.GetComponent<Collider>();
            if (col == null)
            {
                col = child.gameObject.AddComponent<BoxCollider>();
            }

            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = child.gameObject.AddComponent<Rigidbody>();
            }

            var grabInteractable = child.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grabInteractable == null)
            {
                grabInteractable = child.gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            }

            grabInteractable.farAttachMode = UnityEngine.XR.Interaction.Toolkit.Attachment.InteractableFarAttachMode.Near;
            rb.mass = 1f;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            grabInteractable.movementType = UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable.MovementType.VelocityTracking;
            grabInteractable.trackPosition = true;
            grabInteractable.trackRotation = true;
            grabInteractable.throwOnDetach = true;
            grabInteractable.attachTransform = null;

            grabInteractable.selectEntered.AddListener((args) =>
            {
                Transform interactorAttach = args.interactorObject.GetAttachTransform(args.interactableObject);
                if (interactorAttach != null)
                {
                    grabInteractable.attachTransform = interactorAttach;
                }
            });

            grabInteractable.selectExited.AddListener((args) =>
            {
                grabInteractable.attachTransform = null;
            });
        }
    }
}