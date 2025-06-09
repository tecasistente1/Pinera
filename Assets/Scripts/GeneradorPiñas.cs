using UnityEngine;

public class PlantingPineappleGrid : MonoBehaviour
{
    public GameObject testObjectPrefab; // Prefab de la piña visual
    public Terrain terrain;
    public Transform jugadorOCamara;
    public float distanciaActivacionC = 20f;

    void Start()
    {
        int contPlants = 0;

        // Todos los bloques combinados en un solo ciclo organizado
        GenerarRango(22f,   91f, 2f,   21.2f, 237f, ref contPlants);
        GenerarRango(22f,   91f, 2f,  263.9f, 480f, ref contPlants);

        GenerarRango(119f, 188f, 2f,   21.2f, 237f, ref contPlants);
        GenerarRango(119f, 188f, 2f,  263.9f, 480f, ref contPlants);

        GenerarRango(216f, 285f, 2f,   21.2f, 237f, ref contPlants);
        GenerarRango(216f, 285f, 2f,  263.9f, 480f, ref contPlants);

        GenerarRango(313f, 388f, 2f,   21.2f, 237f, ref contPlants);
        GenerarRango(313f, 388f, 2f,  263.9f, 480f, ref contPlants);

        GenerarRango(410f, 479f, 2f,   21.2f, 237f, ref contPlants);
        GenerarRango(410f, 479f, 2f,  263.9f, 480f, ref contPlants);

        Debug.Log("Total de piñas colocadas: " + contPlants);
    }

    void GenerarRango(float inicioX, float finX, float alturaExtra, float inicioZ, float finZ, ref int contador)
    {
        for (float x = inicioX; x < finX; x++)
        {
            for (float z = inicioZ; z < finZ; z += 4.3f)
            {
                float y = terrain.SampleHeight(new Vector3(x, 0, z));
                Vector3 posicion = new Vector3(x, y + alturaExtra, z);

                // Crear visual y contenedor
                GameObject visual = Instantiate(testObjectPrefab, posicion, Quaternion.identity);
                GameObject contenedor = new GameObject("Contenedor_Piña");
                contenedor.transform.position = posicion;
                visual.transform.SetParent(contenedor.transform);

                DestroyImmediate(visual.GetComponent<Rigidbody>());
                DestroyImmediate(visual.GetComponent<Collider>());

                var activador = contenedor.AddComponent<ActivadorPorProximidad>();
                activador.referencia = jugadorOCamara;
                activador.objetoVisual = visual;
                activador.distanciaActivacion = distanciaActivacionC;
                activador.chequearCadaFrame = true;

                contador++;
            }
        }
    }
}
