using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Destruye cualquier objeto que entre al basurero
        Destroy(other.gameObject);
        Debug.Log($"Objeto destruido: {other.name}");
    }
}
