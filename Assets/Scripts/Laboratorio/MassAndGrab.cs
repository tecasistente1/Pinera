using UnityEngine;
using TMPro;


public class MassAndGrab : MonoBehaviour
{
    private readonly float[] masasPosibles = { 0.5f, 1f, 1.5f, 2f, 2.5f, 3f };

    public Font fontTMP;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            // 1. Collider no trigger
            Collider col = child.GetComponent<Collider>();
            if (col == null)
                col = child.gameObject.AddComponent<BoxCollider>();

            col.isTrigger = false;

            // 2. Rigidbody
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb == null)
                rb = child.gameObject.AddComponent<Rigidbody>();

            rb.isKinematic = false;
            rb.useGravity = true;

            // 3. Masa aleatoria y escala
            float masa = masasPosibles[Random.Range(0, masasPosibles.Length)];
            rb.mass = masa;

            float escala = Mathf.Lerp(0.5f, 1.0f, (masa - 0.5f) / 2.5f);
            child.localScale = Vector3.one * escala;

            // 4. XRGrabInteractable (Near only)
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = child.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grab == null)
                grab = child.gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

            grab.attachTransform = null; // Usar el contacto como punto de anclaje
            grab.movementType = UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable.MovementType.Instantaneous;
            grab.trackPosition = true;
            grab.trackRotation = true;

            // 🔥 Clave: limitar la interacción a manos cercanas (opcional)
            // Podés omitir esto si no tenés capas configuradas
            // grab.interactionLayers = InteractionLayerMask.GetMask("DirectTouch");

            // (Opcional) Mostrar texto con la masa
            /*
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
            */
        }
    }
}
