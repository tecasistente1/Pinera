using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class PlantingGrassGrid2: MonoBehaviour
{
    public GameObject GrassObjectPrefab1; 
    public GameObject GrassObjectPrefab2;
    public Terrain terrain;         
    public Transform jugadorOCamara;
    public float distanciaActivacionC = 20f;
    private int contPlants = 0; 
    public bool altaDensidad = false; 
    public bool usarObjetosExtras = true;
    public int plantaPrincipal = 1;


    private void InstanciarPlantaConContenedor(Vector3 position, GameObject prefab, GameObject contendorVisual)
    {
  
        GameObject contenedor = new GameObject("Contenedor_" + prefab.name);
        contenedor.transform.position = position;

        GameObject visual = Instantiate(prefab, position, Quaternion.identity, contenedor.transform);

        DestroyImmediate(visual.GetComponent<Rigidbody>());
        DestroyImmediate(visual.GetComponent<Collider>());

        contenedor.transform.SetParent(contendorVisual.transform);

        //var script = contenedor.AddComponent<ActivadorPorProximidad2>();
        //script.referencia = jugadorOCamara;
        //script.camaraJugador = mainCamera;
        //script.distanciaActivacion = distanciaActivacionC;
        //script.chequearCadaFrame = true;
        //script.objetoVisual = visual;

        contPlants++; 
    }


    public void GenerarPlantasZona(float inicioZ, float finZ, float pasoZ, float inicioX, float finX, float pasoX)
    {
        GameObject superContenedor = new GameObject(name + "_ContenedorPlantas");
        GameObject contendorVisual = new GameObject(name + "_ContenedorVisual");
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

                Vector3 basePosition = new Vector3(worldX, worldY + 0.001f, worldZ);
                if (plantaPrincipal == 1)
                    InstanciarPlantaConContenedor(basePosition, GrassObjectPrefab1, contendorVisual);
                else
                    InstanciarPlantaConContenedor(basePosition, GrassObjectPrefab2, contendorVisual);

                if (usarObjetosExtras)
                {
                    int cantidadExtras = (randomValue < 1f) ? 1 :
                                     (randomValue < 2f) ? 2 :
                                     (randomValue < 3f) ? 3 : 0;
                    for (int i = 0; i < cantidadExtras; i++)
                    {
                        float extraX = Random.Range(inicioX, finX);
                        worldY = terrain.SampleHeight(new Vector3(extraX, 0, worldZ));
                        Vector3 extraPos = new Vector3(extraX, worldY + 0.1234f, worldZ);
                        InstanciarPlantaConContenedor(extraPos, GrassObjectPrefab2, contendorVisual);
                    }
                }
            }
        }
        var script = superContenedor.AddComponent<ActivadorPorProximidad3>();
        script.referencia = jugadorOCamara;
        script.distanciaActivacion = distanciaActivacionC;
        script.chequearCadaFrame = true;
        script.objetoVisual = contendorVisual;
        script.xInicio = (int)inicioX;
        script.xFin = (int)finX;
        script.zInicio = (int)inicioZ;
        script.zFin = (int)finZ;
        contendorVisual.transform.SetParent(superContenedor.transform);


    }


    void Start()
    {
        if (altaDensidad)
        {
            GenerarPlantasZona(3.5f, 10f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(10.1f, 20f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(20.1f, 30f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(30.1f, 40f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(40.1f, 50f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(50.1f, 60f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(60.1f, 70f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(70.1f, 80f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(80.1f, 90f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(90.1f, 96.5f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(3.5f, 10f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(10.1f, 20f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(20.1f, 30f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(30.1f, 40f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(40.1f, 50f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(50.1f, 60f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(60.1f, 70f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(70.1f, 80f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(80.1f, 90f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(90.1f, 96.5f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(3.5f, 10f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(10.1f, 20f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(20.1f, 30f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(30.1f, 40f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(40.1f, 50f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(50.1f, 60f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(60.1f, 70f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(70.1f, 80f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(80.1f, 90f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(90.1f, 96.5f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(3.5f, 10f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(10.1f, 20f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(20.1f, 30f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(30.1f, 40f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(40.1f, 50f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(50.1f, 60f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(60.1f, 70f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(70.1f, 80f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(80.1f, 90f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(90.1f, 96.5f, 1f, 96.5f, 100f, 1f);
            GenerarPlantasZona(1f, 4f, 1f, 0f, 10f, 1f);
            GenerarPlantasZona(1f, 4f,1f, 10f, 20f, 1f);
            GenerarPlantasZona(1f, 4f,1f, 20f, 30f, 1f);
            GenerarPlantasZona(1f, 4f,1f, 30f, 40f, 1f);
            GenerarPlantasZona(1f, 4f,1f, 40f, 50f, 1f);
            GenerarPlantasZona(1f, 4f,1f, 50f, 60f, 1f);
            GenerarPlantasZona(1f, 4f,1f, 60f, 70f, 1f);
            GenerarPlantasZona(1f, 4f,1f, 70f, 80f, 1f);
            GenerarPlantasZona(1f, 4f,1f, 80f, 90f, 1f);
            GenerarPlantasZona(1f, 4f, 1f, 90f, 100f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 3f, 9.75f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 9.75f, 16.5f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 16.5f, 23.25f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 23.25f, 30f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 36f, 42.875f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 42.875f, 49.75f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 49.75f, 56.625f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 56.625f, 63.5f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 69.3f, 76.225f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 76.225f, 83.15f, 1f);
            GenerarPlantasZona(47.3f, 53f,1f, 83.15f, 90.075f, 1f);
            GenerarPlantasZona(47.3f, 53f, 1f, 90.075f, 97f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 3f, 10f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 10f, 20f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 20f, 30f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 30f, 40f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 40f, 50f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 50f, 60f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 60f, 70f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 70f, 80f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 80f, 90f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 90f, 100f, 1f);
            GenerarPlantasZona(53f, 61f, 1f, 36f, 41.5f, 1f);
            GenerarPlantasZona(53f, 61f, 1f, 41.5f, 47f, 1f);
            GenerarPlantasZona(53f, 61f, 1f, 47f, 52.5f, 1f);
            GenerarPlantasZona(53f, 61f, 1f, 52.5f, 58f, 1f);
            GenerarPlantasZona(53f, 61f, 1f, 58f, 63.5f, 1f);
            GenerarPlantasZona(61f, 69f, 1f, 36f, 41.5f, 1f);
            GenerarPlantasZona(61f, 69f, 1f, 41.5f, 47f, 1f);
            GenerarPlantasZona(61f, 69f, 1f, 47f, 52.5f, 1f);
            GenerarPlantasZona(61f, 69f, 1f, 52.5f, 58f, 1f);
            GenerarPlantasZona(61f, 69f, 1f, 58f, 63.5f, 1f);

        }
        else
        {   //z
            GenerarPlantasZona(3.5f, 10f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(10.1f, 20f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(20.1f, 30f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(30.1f, 40f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(40.1f, 50f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(50.1f, 60f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(60.1f, 70f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(70.1f, 80f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(80.1f, 90f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(90.1f, 96.5f, 1.5f, 0f, 3f, 1.5f);
            GenerarPlantasZona(3.5f, 10f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(10.1f, 20f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(20.1f, 30f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(30.1f, 40f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(40.1f, 50f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(50.1f, 60f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(60.1f, 70f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(70.1f, 80f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(80.1f, 90f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(90.1f, 96.5f, 1.5f, 30.5f, 36f, 1.5f);
            GenerarPlantasZona(3.5f, 10f, 1.5f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(10.1f, 20f, 1.5f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(20.1f, 30f, 1.5f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(30.1f, 40f, 1.5f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(40.1f, 50f, 1.5f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(50.1f, 60f, 1.5f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(60.1f, 70f, 1.5f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(70.1f, 80f, 1.5f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(80.1f, 90f, 1.5f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(90.1f, 96.5f, 1f, 63.5f, 69.2f, 1.5f);
            GenerarPlantasZona(3.5f, 10f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(10.1f, 20f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(20.1f, 30f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(30.1f, 40f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(40.1f, 50f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(50.1f, 60f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(60.1f, 70f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(70.1f, 80f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(80.1f, 90f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(90.1f, 96.5f, 1.5f, 96.5f, 100f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 0f, 10f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 10f, 20f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 20f, 30f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 30f, 40f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 40f, 50f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 50f, 60f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 60f, 70f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 70f, 80f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 80f, 90f, 1.5f);
            GenerarPlantasZona(1f, 4f, 1.5f, 90f, 100f, 1f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 3f, 9.75f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 9.75f, 16.5f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 16.5f, 23.25f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 23.25f, 30f, 1.5F);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 36f, 42.875f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 42.875f, 49.75f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 49.75f, 56.625f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 56.625f, 63.5f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 69.3f, 76.225f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 76.225f, 83.15f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 83.15f, 90.075f, 1.5f);
            GenerarPlantasZona(47.3f, 53f, 1.5f, 90.075f, 97f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 3f, 10f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 10f, 20f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 20f, 30f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 30f, 40f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 40f, 50f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 50f, 60f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 60f, 70f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 70f, 80f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 80f, 90f, 1.5f);
            GenerarPlantasZona(97f, 100f, 1.5f, 90f, 100f, 1.5f);
            GenerarPlantasZona(53f, 61f, 1.5f, 36f, 41.5f, 1.5f);
            GenerarPlantasZona(53f, 61f, 1.5f, 41.5f, 47f, 1.5f);
            GenerarPlantasZona(53f, 61f, 1.5f, 47f, 52.5f, 1.5f);
            GenerarPlantasZona(53f, 61f, 1.5f, 52.5f, 58f, 1.5f);
            GenerarPlantasZona(53f, 61f, 1.5f, 58f, 63.5f, 1.5f);
            GenerarPlantasZona(61f, 69f, 1.5f, 36f, 41.5f, 1.5f);
            GenerarPlantasZona(61f, 69f, 1.5f, 41.5f, 47f, 1.5f);
            GenerarPlantasZona(61f, 69f, 1.5f, 47f, 52.5f, 1.5f);
            GenerarPlantasZona(61f, 69f, 1.5f, 52.5f, 58f, 1.5f);
            GenerarPlantasZona(61f, 69f, 1.5f, 58f, 63.5f, 1.5f);
        }

        Debug.Log("Total de plantas de hierba colocadas: " + contPlants);
    }
}