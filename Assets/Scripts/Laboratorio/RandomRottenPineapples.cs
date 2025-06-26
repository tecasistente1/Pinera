using UnityEngine;

public class RandomRottenPineapples : MonoBehaviour
{
    [Tooltip("Porcentaje de piñas que se pondrán podridas (entre 0 y 1)")]
    [Range(0f, 1f)]
    public float rottenPercentage = 0.3f;

    [Tooltip("Material que representa la piña podrida")]
    public Material rottenMaterial;

    [Tooltip("Padre que contiene todas las piñas")]
    public Transform pineapplesParent;

    void Start()
    {
        if (pineapplesParent == null || rottenMaterial == null)
        {
            Debug.LogError("Faltan referencias en el script.");
            return;
        }

        int totalPineapples = pineapplesParent.childCount;
        if (totalPineapples == 0)
        {
            Debug.LogWarning("No se encontraron piñas hijas.");
            return;
        }

        Transform[] allPineapples = new Transform[totalPineapples];
        for (int i = 0; i < totalPineapples; i++)
        {
            allPineapples[i] = pineapplesParent.GetChild(i);
        }

        int totalToRotten = Mathf.Clamp(Mathf.FloorToInt(totalPineapples * rottenPercentage), 1, totalPineapples);
        Debug.Log($"Piñas totales: {totalPineapples}, Podridas aleatorias: {totalToRotten}");

        // Shuffle para selección aleatoria
        System.Random rng = new System.Random();
        for (int i = 0; i < allPineapples.Length; i++)
        {
            int swapIndex = rng.Next(i, allPineapples.Length);
            Transform temp = allPineapples[i];
            allPineapples[i] = allPineapples[swapIndex];
            allPineapples[swapIndex] = temp;
        }

        // Aplicar material podrido solo a algunas
        for (int i = 0; i < totalToRotten; i++)
        {
            Renderer rend = allPineapples[i].GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = rottenMaterial;
            }
        }
    }
}
