using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class PlantingGrassGrid: MonoBehaviour
{
    public GameObject GrassObjectPrefab1; // Cubo o lo que uses para visualizar
    public GameObject GrassObjectPrefab2;
    public Terrain terrain;             // El terreno sobre el cual se va a plantar
    public Transform jugadorOCamara;
    public Camera mainCamera;
    public float distanciaActivacionC = 20f; // Distancia a la que se activa el objeto
    private int contPlants = 0; // Contador de plantas para verificar el total plantado
    public bool altaDensidad = false; // Si se quiere plantar con más densidad


    private void InstanciarPlantaConContenedor(Vector3 position, GameObject prefab)
    {
        // Crear el contenedor vacío
        GameObject contenedor = new GameObject("Contenedor_" + prefab.name);
        contenedor.transform.position = position;

        GameObject visual = Instantiate(prefab, position, Quaternion.identity, contenedor.transform);

        DestroyImmediate(visual.GetComponent<Rigidbody>());
        DestroyImmediate(visual.GetComponent<Collider>());

        var script = contenedor.AddComponent<ActivadorPorProximidad2>();
        script.referencia = jugadorOCamara;       
        script.camaraJugador = mainCamera; 
        script.distanciaActivacion = distanciaActivacionC;
        script.chequearCadaFrame = true;
        script.objetoVisual = visual;

        contPlants++; 
    }


    public void GenerarPlantasZona(float inicioZ, float finZ, float pasoZ, float inicioX, float finX, float pasoX)
    {
        for (float ileraZ = inicioZ; ileraZ < finZ; ileraZ += pasoZ)
        {
            for (float ileraX = inicioX; ileraX < finX; ileraX += pasoX)
            {
                float randomValue = Random.Range(0, 3.5f);
                float randomX = Random.Range(-0.5f, 0.5f);
                float randomZ = Random.Range(-0.5f, 0.5f);

                float worldX = ileraX + randomX;
                float worldZ = ileraZ + randomZ;
                float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ));

                Vector3 basePosition = new Vector3(worldX, worldY + 0.1234f, worldZ);
                InstanciarPlantaConContenedor(basePosition, GrassObjectPrefab1);

                // Plantas adicionales
                int cantidadExtras = (randomValue < 1f) ? 1 :
                                     (randomValue < 2f) ? 2 :
                                     (randomValue < 3f) ? 3 : 0;

                for (int i = 0; i < cantidadExtras; i++)
                {
                    float extraX = Random.Range(inicioX, finX);
                    worldY = terrain.SampleHeight(new Vector3(extraX, 0, worldZ)); // Recalcular Y para la nueva posición
                    Vector3 extraPos = new Vector3(extraX, worldY + 0.1234f, worldZ);
                    InstanciarPlantaConContenedor(extraPos, GrassObjectPrefab2);
                }
            }
        }
    }


    void Start()
    {
        if (altaDensidad)
        {
            GenerarPlantasZona(0f, 100f, 0.7f, 0f, 3f, 0.7f);
            GenerarPlantasZona(0f, 100f, 0.7f, 30.5f, 36f, 0.7f);
            GenerarPlantasZona(0f, 100f, 0.7f, 63.5f, 69.2f, 0.7f);
            GenerarPlantasZona(0f, 100f, 0.7f, 96.5f, 100f, 0.7f);

            GenerarPlantasZona(1f, 4f, 0.7f, 0f, 100f, 0.7f);        //(float inicioZ, float finZ, float pasoZ, float inicioX, float finX, float pasoX)
            GenerarPlantasZona(47.3f, 53f, 0.7f, 0f, 100f, 0.7f);
            GenerarPlantasZona(97f, 100f, 0.7f, 0f, 100f, 0.7f);
        }
        else
        {
            GenerarPlantasZona(0f, 100f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(0f, 100f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(0f, 100f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(0f, 100f, 1f, 96.5f, 100f, 1f);

            GenerarPlantasZona(1f, 4f, 1f, 0f, 100f, 1f);        //(float inicioZ, float finZ, float pasoZ, float inicioX, float finX, float pasoX)
            GenerarPlantasZona(47.3f, 53f, 1f, 0f, 100f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 0f, 100f, 1f);
        }

        Debug.Log("Total de plantas de hierba colocadas: " + contPlants);
    }
}