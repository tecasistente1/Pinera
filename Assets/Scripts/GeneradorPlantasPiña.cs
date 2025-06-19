using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantingPineapplePlantGrid : MonoBehaviour
{
    public GameObject plantPrefab1;
    public GameObject plantPrefab2;
    public Terrain terrain;
    public Transform jugadorOCamara;
    public Camera mainCamera; 
    public float distanciaActivacionC = 3000f;

    private int contPlants = 0;

    void Start()
    {
        float[] zonasX = { 5.5f, 38.2f, 71.1f};
        float zMin1 = 4.7f, zMax1 = 46.9f;
        float zMin2 = 54f, zMax2 = 94.7f; 

        foreach (float startX in zonasX)
        {
            float endX = startX + 24f;
            PlantarBloques(startX, endX, zMin1, zMax1);
            PlantarBloques(startX, endX, zMin2, zMax2);
        }

        Debug.Log("Total de plantas de piñas colocadas: " + contPlants);
    }

    void PlantarBloques(float startX, float endX, float startZ, float endZ)
    {
        for (float ileraX = startX; ileraX < endX; ileraX++)
        {
            for (float ileraZ = startZ; ileraZ < endZ; ileraZ += 3.7f)
            {
             PlantarUnaPlanta(ileraX, ileraZ);
              
            }
        }
    }

    void PlantarUnaPlanta(float x, float z)
    {

        if (x >= 38.1f && x <= 61.2f && z >= 54f && z <= 71.1f)
            return;
        float y = terrain.SampleHeight(new Vector3(x, 0, z));

        // Crear contenedor vacío para la planta
        GameObject contenedor = new GameObject("Planta_" + x.ToString("F2") + "_" + z.ToString("F2"));
        contenedor.transform.position = new Vector3(x, y, z);

        // Instanciar la planta como hijo del contenedor (50% de probabilidad para cada tipo)
        GameObject prefab = (Random.Range(0f, 1f) > 0.5f) ? plantPrefab1 : plantPrefab2;
        Vector3 posicionVisual = new Vector3(x, y - 0.01f, z);
        GameObject plantaVisual = Instantiate(prefab, posicionVisual, Quaternion.identity, contenedor.transform);

        // Quitar físicas si las tuviera
        DestroyImmediate(plantaVisual.GetComponent<Rigidbody>());
        DestroyImmediate(plantaVisual.GetComponent<Collider>());

        //// Añadir el script de activación al contenedor (¡no al visual!)
        //var script = contenedor.AddComponent<ActivadorPorProximidad>();
        //script.referencia = jugadorOCamara;
        //script.distanciaActivacion = distanciaActivacionC;
        //script.chequearCadaFrame = true;
        //script.objetoVisual = plantaVisual; // Se asume que el script lo usa para activar/desactivar

        var script = contenedor.AddComponent<ActivadorPorProximidad2>();
        script.referencia = jugadorOCamara;           // (Por si usas la posición para distancia)
        script.camaraJugador = mainCamera; // <-- AQUÍ debes pasar la Main Camera
        script.distanciaActivacion = distanciaActivacionC;
        script.chequearCadaFrame = true;
        script.objetoVisual = plantaVisual;


        contPlants++;
    }

}
