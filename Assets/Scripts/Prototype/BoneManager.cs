using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BoneManager : MonoBehaviour
{
    public Transform[] boneTransforms;

    [ContextMenu("Setup Bones (Editor)")]
    public void SetupBones()
    {
        // Get all child transforms of the SKELETON
        boneTransforms = GetComponentsInChildren<Transform>();

        foreach (Transform bone in boneTransforms)
        {
            if (bone == this.transform) continue;

            // Add Rigidbody if missing
            if (bone.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = bone.gameObject.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            // Add BoxCollider if no collider present
            if (bone.GetComponent<Collider>() == null)
            {
                bone.gameObject.AddComponent<BoxCollider>();
            }

            // Add BoneInfo if missing
            if (bone.GetComponent<BoneInfo>() == null)
            {
                BoneInfo info = bone.gameObject.AddComponent<BoneInfo>();
                info.id = bone.name;
            }

            // Add BoneSelector if missing
            if (bone.GetComponent<BoneSelector>() == null)
            {
                bone.gameObject.AddComponent<BoneSelector>();
            }

            // Add XRGrabInteractable if missing
            if (bone.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>() == null)
            {
                bone.gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            }
        }

        Debug.Log("BoneManager: Setup complete (in Editor).");
    }
}
