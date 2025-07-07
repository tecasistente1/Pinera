using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class PlantingGrassGrid: MonoBehaviour
{
    public GameObject GrassObjectPrefab1; 
    public GameObject GrassObjectPrefab2;
    public GameObject GrassObjectPrefab3;
    public GameObject GrassObjectPrefab4; 
    public GameObject GrassObjectPrefab5;
    public Terrain terrain;         
    public Transform jugadorOCamara;
    public Camera mainCamera;
    public float distanciaActivacionC = 20f;
    private int contPlants = 0; 
    public bool altaDensidad = false; 
    public bool usarObjetosExtras = true;
    public int plantaPrincipal = 1;


    private void InstanciarPlantaConContenedor(Vector3 position, GameObject prefab)
    {
  
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
                if (plantaPrincipal == 1)
                    InstanciarPlantaConContenedor(basePosition, GrassObjectPrefab1);
                else if (plantaPrincipal == 2)
                    InstanciarPlantaConContenedor(basePosition, GrassObjectPrefab2);
                else if (plantaPrincipal == 3)
                    InstanciarPlantaConContenedor(basePosition, GrassObjectPrefab3);
                else if (plantaPrincipal == 4)
                    InstanciarPlantaConContenedor(basePosition, GrassObjectPrefab4);
                else if (plantaPrincipal == 5)
                    InstanciarPlantaConContenedor(basePosition, GrassObjectPrefab5);

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
                        InstanciarPlantaConContenedor(extraPos, GrassObjectPrefab2);
                    }
                }
            }
        }
    }


    void Start()
    {
        if (altaDensidad)
        {   //z
            GenerarPlantasZona(3.5f, 96.5f, 0.8f, 0f, 3f, 0.8f);
            GenerarPlantasZona(3.5f, 96.5f, 0.8f, 30.5f, 36f, 0.8f);
            GenerarPlantasZona(3.5f, 96.5f, 0.8f, 63.5f, 69.2f, 0.8f);
            GenerarPlantasZona(3.5f, 96.5f, 0.8f, 96.5f, 100f, 0.8f);

            //x
            GenerarPlantasZona(1f, 4f, 0.8f, 0f, 100f, 0.8f); 
            GenerarPlantasZona(47.3f, 53f, 0.8f, 3f, 30f, 0.8f);
            GenerarPlantasZona(47.3f, 53f, 0.8f, 36f, 63.5f, 0.8f);
            GenerarPlantasZona(47.3f, 53f, 0.8f, 69.3f, 97f, 0.8f);
            GenerarPlantasZona(97f, 100f, 0.8f, 0f, 100f, 0.8f);


            //espacio vacío 
            GenerarPlantasZona(53f, 69f, 0.8f, 36f, 63.5f, 0.8f);

        }
        else
        {   //z
            GenerarPlantasZona(3.5f, 96.5f, 1f, 0f, 3f, 1f);
            GenerarPlantasZona(3.5f, 96.5f, 1f, 30.5f, 36f, 1f);
            GenerarPlantasZona(3.5f, 96.5f, 1f, 63.5f, 69.2f, 1f);
            GenerarPlantasZona(3.5f, 96.5f, 1f, 96.5f, 100f, 1f);

            //x
            GenerarPlantasZona(1f, 4f, 1f, 3f, 100f, 1f);   
            GenerarPlantasZona(47.3f, 53f, 1f, 3f, 30f, 1f);
            GenerarPlantasZona(47.3f, 53f, 1f, 36f, 63.5f, 1f);
            GenerarPlantasZona(47.3f, 53f, 1f, 69.3f, 97f, 1f);
            GenerarPlantasZona(97f, 100f, 1f, 3f, 100f, 1f);

            //espacio vacío 
            GenerarPlantasZona(53f, 69f, 1f, 36f, 63.5f, 1f);
        }

        Debug.Log("Total de plantas de hierba colocadas: " + contPlants);
    }
}