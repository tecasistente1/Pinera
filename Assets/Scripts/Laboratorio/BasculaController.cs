using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasculaController : MonoBehaviour
{
    public TextMeshProUGUI pesoText; // Asigna este campo desde el inspector
    private float pesoTotal = 0f;
    private List<Rigidbody> objetosEnBascula = new List<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && !objetosEnBascula.Contains(rb))
        {
            objetosEnBascula.Add(rb);
            pesoTotal += rb.mass;
            ActualizarUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && objetosEnBascula.Contains(rb))
        {
            objetosEnBascula.Remove(rb);
            pesoTotal -= rb.mass;
            ActualizarUI();
        }
    }

    void ActualizarUI()
    {
        pesoText.text = $"Peso: {pesoTotal:F2} kg";
    }
}
