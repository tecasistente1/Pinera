using UnityEngine;

public class PuntoSnapPina : MonoBehaviour
{
    private SnapPinas snapActual = null;

    public void ResetSlot(SnapPinas snap)
    {
        if (snapActual == snap)
            snapActual = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (snapActual != null) return;
        SnapPinas snap = other.GetComponentInParent<SnapPinas>();
        if (snap != null)
        {
            Debug.Log($"ENTER: {other.name} | SnapActual: {(snapActual != null ? snapActual.name : "null")}");
            snap.EntrarZonaSnapCamion(this.transform);
            snapActual = snap; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SnapPinas snap = other.GetComponentInParent<SnapPinas>();
        if (snap != null && snap == snapActual)
        {
            Debug.Log($"EXIT: {other.name} | SnapActual: {(snapActual != null ? snapActual.name : "null")}");
            snapActual = null;
            snap.SalirZonaSnapCamion();
        }
    }
}