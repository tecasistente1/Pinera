using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SnapPiñaEnCamion : MonoBehaviour
{
    public Transform attachPoint;

    private void OnTriggerEnter(Collider other)
    {

        SnapPiñas snap = other.GetComponentInParent<SnapPiñas>();
        if (snap != null)
        {
            snap.EntrarZonaSnapCamion(attachPoint);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SnapPiñas snap = other.GetComponentInParent<SnapPiñas>();
        if (snap != null)
        {
            snap.SalirZonaSnapCamion();
        }
    }
}