using UnityEngine;

public class SuperSnapZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        var snap = other.GetComponentInParent<SnapPiñas>();
        if (snap != null)
        {
            snap.EntrarZonaSnap();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var snap = other.GetComponentInParent<SnapPiñas>();
        if (snap != null)
        {
            snap.SalirZonaSnap();
        }
    }
}
