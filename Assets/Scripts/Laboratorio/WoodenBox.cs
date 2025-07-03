using UnityEngine;

public class WoodenBox : MonoBehaviour
{
    private int pineappleCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        pineappleCount++;
        Debug.Log($"🍍 Piña insertada. Total: {pineappleCount}");
        Destroy(other.gameObject); // Elimina el objeto que entra
    }

    public int GetPineappleCount()
    {
        return pineappleCount;
    }
}
