using UnityEngine;

public class BoneSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform skeletonParent;

    private Transform[] boneTemplates;

    void Start()
    {
        if (skeletonParent == null || spawnPoint == null)
        {
            Debug.LogWarning("BoneSpawner: 'skeletonParent' or 'spawnPoint' is not assigned.");
            return;
        }

        // Get all bones under the skeleton
        boneTemplates = skeletonParent.GetComponentsInChildren<Transform>(true);
        MoveRandomBone();
    }

    public void MoveRandomBone()
    {
        if (boneTemplates == null || boneTemplates.Length <= 1)
        {
            Debug.LogWarning("BoneSpawner: No bones found under the skeleton.");
            return;
        }

        // Select a random bone (skip index 0 = root)
        Transform selectedBone = null;
        while (selectedBone == null || selectedBone == skeletonParent)
        {
            selectedBone = boneTemplates[Random.Range(1, boneTemplates.Length)];
        }

        // Make sure it's active and visible
        selectedBone.gameObject.SetActive(true);

        // If it has a Rigidbody, disable physics before teleporting
        Rigidbody rb = selectedBone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            bool wasKinematic = rb.isKinematic;
            rb.isKinematic = true; // Force disable physics

            // Temporarily unparent to avoid transform offset issues
            Transform originalParent = selectedBone.parent;
            selectedBone.SetParent(null);

            selectedBone.position = spawnPoint.position;
            selectedBone.rotation = spawnPoint.rotation;

            // Restore parent
            selectedBone.SetParent(originalParent);

            rb.isKinematic = wasKinematic; // Restore original setting
        }
        else
        {
            // No Rigidbody, just move directly
            Transform originalParent = selectedBone.parent;
            selectedBone.SetParent(null);

            selectedBone.position = spawnPoint.position;
            selectedBone.rotation = spawnPoint.rotation;

            selectedBone.SetParent(originalParent);
        }

        Debug.Log("BoneSpawner: Forced teleport of bone: " + selectedBone.name);
    }
}
