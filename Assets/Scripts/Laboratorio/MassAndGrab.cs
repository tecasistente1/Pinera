using UnityEngine;
using TMPro;


public class MassAndGrab : MonoBehaviour
{
    private readonly float[] masasPosibles = { 0.5f, 1f, 1.5f, 2f, 2.5f, 3f };

    public Font fontTMP; // Fuente opcional

    private void Start()
    {
        foreach (Transform child in transform)
        {
            // 1. Añadir Collider si no hay
            if (child.GetComponent<Collider>() == null)
                child.gameObject.AddComponent<BoxCollider>();

            // 2. Añadir Rigidbody si no hay
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb == null)
                rb = child.gameObject.AddComponent<Rigidbody>();

            rb.isKinematic = false;
            rb.useGravity = true;

            // 3. Asignar masa aleatoria (múltiplos de 0.5)
            float masa = masasPosibles[Random.Range(0, masasPosibles.Length)];
            rb.mass = masa;

            // 4. Escala proporcional directa (0.5 → 0.5, 3.0 → 1.0)
            float escala = Mathf.Lerp(0.5f, 1.0f, (masa - 0.5f) / 2.5f);
            child.localScale = Vector3.one * escala;

            // 5. Asegurar XR Grab Interactable
            if (child.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>() == null)
                child.gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

            // 6. Mostrar texto flotante con el peso
            GameObject textoPeso = new GameObject("PesoText");
            textoPeso.transform.SetParent(child);
            textoPeso.transform.localPosition = new Vector3(0, 0.3f, 0);

            TextMeshPro tmp = textoPeso.AddComponent<TextMeshPro>();
            tmp.text = $"{masa:0.0} kg";
            tmp.fontSize = 0.3f;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.color = Color.black;

            if (fontTMP != null)
                tmp.font = TMP_FontAsset.CreateFontAsset(fontTMP);
        }
    }
}
