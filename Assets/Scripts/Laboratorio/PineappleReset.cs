using UnityEngine;

public class PineappleReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Rigidbody rb;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void ResetPineapple()
    {
        gameObject.SetActive(true);
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
