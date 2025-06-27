using UnityEngine;

public class PuntoSnapPiña : MonoBehaviour
{
    private SnapPiñas snapActual = null; // La piña que está en este slot

    public void ResetSlot(SnapPiñas snap)
    {
        if (snapActual == snap)
            snapActual = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (snapActual != null) return;
        SnapPiñas snap = other.GetComponentInParent<SnapPiñas>();
        if (snap != null)
        {
            Debug.Log($"ENTER: {other.name} | SnapActual: {(snapActual != null ? snapActual.name : "null")}");
            snap.EntrarZonaSnapCamion(this.transform);
            snapActual = snap; // Guardamos cuál piña ocupa este slot
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SnapPiñas snap = other.GetComponentInParent<SnapPiñas>();
        // Solo liberamos si es la misma piña que ocupa el slot
        if (snap != null && snap == snapActual)
        {
            Debug.Log($"EXIT: {other.name} | SnapActual: {(snapActual != null ? snapActual.name : "null")}");
            snapActual = null;
            snap.SalirZonaSnapCamion();
        }
    }
}