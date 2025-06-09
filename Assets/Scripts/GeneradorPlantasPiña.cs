using UnityEngine;

public class PlantingPineapplePlantGrid : MonoBehaviour
{
    public GameObject testObjectPrefab;
    public Terrain terrain;
    public Transform jugadorOCamara;
    public float distanciaActivacionC = 20f;

    private int contPlants = 0;

    void Start()
    {
        float[] zonasX = { 22f, 119f, 216f, 313f, 410f };
        float zMin1 = 21.2f, zMax1 = 237f;
        float zMin2 = 263.9f, zMax2 = 480f;

        foreach (float startX in zonasX)
        {
            float endX = startX + 69f;
            PlantarBloques(startX, endX, zMin1, zMax1);
            PlantarBloques(startX, endX, zMin2, zMax2);
        }

        Debug.Log("Total de plantas de piñas colocadas: " + contPlants);
    }

    void PlantarBloques(float startX, float endX, float startZ, float endZ)
    {
        for (float ileraX = startX; ileraX < endX; ileraX++)
        {
            for (float ileraZ = startZ; ileraZ < endZ; ileraZ += 4.3f)
            {
                PlantarUnaPlanta(ileraX, ileraZ);
            }
        }
    }

    void PlantarUnaPlanta(float x, float z)
    {
        float y = terrain.SampleHeight(new Vector3(x, 0, z));

        // Crear contenedor vacío para la planta
        GameObject contenedor = new GameObject("Planta_" + x.ToString("F2") + "_" + z.ToString("F2"));
        contenedor.transform.position = new Vector3(x, y, z);

        // Instanciar la planta como hijo del contenedor
        Vector3 posicionVisual = new Vector3(x, y + 0.1234f, z);
        GameObject plantaVisual = Instantiate(testObjectPrefab, posicionVisual, Quaternion.identity, contenedor.transform);

        // Quitar físicas si las tuviera
        DestroyImmediate(plantaVisual.GetComponent<Rigidbody>());
        DestroyImmediate(plantaVisual.GetComponent<Collider>());

        // Añadir el script de activación al contenedor (¡no al visual!)
        var script = contenedor.AddComponent<ActivadorPorProximidad>();
        script.referencia = jugadorOCamara;
        script.distanciaActivacion = distanciaActivacionC;
        script.chequearCadaFrame = true;
        script.objetoVisual = plantaVisual; // Se asume que el script lo usa para activar/desactivar

        contPlants++;
    }

}
