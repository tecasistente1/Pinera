using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantingPineapplePlantGrid2 : MonoBehaviour
{
    public GameObject plantPrefab1;
    public GameObject plantPrefab2;
    public Terrain terrain;
    public Transform jugadorOCamara;
    public float distanciaActivacionC = 30f;

    private int contPlants = 0;
    public int OpcionPlantacion = 1; 

    private void InstanciarPinaConContenedor(Vector3 position, GameObject prefab, GameObject contendorVisual)
    {

        GameObject contenedor = new GameObject("Contenedor_" + prefab.name);
        contenedor.transform.position = position;

        GameObject visual = Instantiate(prefab, position, Quaternion.identity, contenedor.transform);

        DestroyImmediate(visual.GetComponent<Rigidbody>());
        DestroyImmediate(visual.GetComponent<Collider>());

        contenedor.transform.SetParent(contendorVisual.transform);
        contPlants++;
    }

    public void GenerarPinasZona(float inicioZ, float finZ, float pasoZ, float inicioX, float finX, float pasoX)
    {
        GameObject superContenedor = new GameObject(name + "_ContenedorPinas");
        GameObject contendorVisual = new GameObject(name + "_ContenedorVisual");
        for (float ileraZ = inicioZ; ileraZ < finZ; ileraZ += pasoZ)
        {
            for (float ileraX = inicioX; ileraX < finX; ileraX += pasoX)
            {
                float ileraY = terrain.SampleHeight(new Vector3(ileraX, 0, ileraZ));
                Vector3 posicionVisual = new Vector3(ileraX, ileraY - 0.01f, ileraZ);

                GameObject prefab = (Random.Range(0f, 1f) > 0.5f) ? plantPrefab1 : plantPrefab2;
                InstanciarPinaConContenedor(posicionVisual, prefab, contendorVisual);
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
        if (OpcionPlantacion !=1)
        {
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 5.5f, 17.5f, 0.8f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 5.5f, 17.5f, 0.8f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 5.5f, 17.5f, 0.8f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 5.5f, 17.5f, 0.8f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 17.5f, 29.5f, 0.8f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 17.5f, 29.5f, 0.8f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 17.5f, 29.5f, 0.8f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 17.5f, 29.5f, 0.8f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 38.2f, 50.2f, 0.8f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 38.2f, 50.2f, 0.8f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 38.2f, 50.2f, 0.8f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 38.2f, 50.2f, 0.8f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 50.2f, 62.2f, 0.8f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 50.2f, 62.2f, 0.8f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 50.2f, 62.2f, 0.8f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 50.2f, 62.2f, 0.8f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 71.1f, 83.1f, 0.8f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 71.1f, 83.1f, 0.8f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 71.1f, 83.1f, 0.8f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 71.1f, 83.1f, 0.8f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 83.1f, 95.1f, 0.8f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 83.1f, 95.1f, 0.8f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 83.1f, 95.1f, 0.8f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 83.1f, 95.1f, 0.8f);


            GenerarPinasZona(54f, 64.175f, 3.7f, 5.5f, 17.5f, 0.8f);
            GenerarPinasZona(65.1f, 74.35f, 3.7f, 5.5f, 17.5f, 0.8f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 5.5f, 17.5f, 0.8f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 5.5f, 17.5f, 0.8f);
            GenerarPinasZona(54f, 64.175f, 3.7f, 17.5f, 29.5f, 0.8f);
            GenerarPinasZona(65.1f, 74.35f, 3.7f, 17.5f, 29.5f, 0.8f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 17.5f, 29.5f, 0.8f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 17.5f, 29.5f, 0.8f);

            GenerarPinasZona(72.5f, 74.35f, 3.7f, 38.2f, 50.2f, 0.8f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 38.2f, 50.2f, 0.8f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 38.2f, 50.2f, 0.8f);

            GenerarPinasZona(72.5f, 74.35f, 3.7f, 50.2f, 62.2f, 0.8f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 50.2f, 62.2f, 0.8f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 50.2f, 62.2f, 0.8f);

            GenerarPinasZona(54f, 64.175f, 3.7f, 71.1f, 83.1f, 0.8f);
            GenerarPinasZona(65.1f, 74.35f, 3.7f, 71.1f, 83.1f, 0.8f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 71.1f, 83.1f, 0.8f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 71.1f, 83.1f, 0.8f);
            GenerarPinasZona(54f, 64.175f, 3.7f, 83.1f, 95.1f, 0.8f);
            GenerarPinasZona(65.1f, 74.35f, 3.7f, 83.1f, 95.1f, 0.8f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 83.1f, 95.1f, 0.8f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 83.1f, 95.1f, 0.8f);
        }
        else
        {
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 5.5f, 17.5f, 1f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 5.5f, 17.5f, 1f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 5.5f, 17.5f, 1f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 5.5f, 17.5f, 1f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 17.5f, 29.5f, 1f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 17.5f, 29.5f, 1f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 17.5f, 29.5f, 1f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 17.5f, 29.5f, 1f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 38.2f, 50.2f, 1f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 38.2f, 50.2f, 1f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 38.2f, 50.2f, 1f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 38.2f, 50.2f, 1f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 50.2f, 62.2f, 1f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 50.2f, 62.2f, 1f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 50.2f, 62.2f, 1f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 50.2f, 62.2f, 1f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 71.1f, 83.1f, 1f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 71.1f, 83.1f, 1f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 71.1f, 83.1f, 1f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 71.1f, 83.1f, 1f);
            GenerarPinasZona(4.7f, 15.25f, 3.7f, 83.1f, 95.1f, 1f);
            GenerarPinasZona(15.8f, 25.8f, 3.7f, 83.1f, 95.1f, 1f);
            GenerarPinasZona(26.9f, 36.35f, 3.7f, 83.1f, 95.1f, 1f);
            GenerarPinasZona(38f, 46.9f, 3.7f, 83.1f, 95.1f, 1f);


            GenerarPinasZona(54f, 64.175f, 3.7f, 5.5f, 17.5f, 1f);
            GenerarPinasZona(65.1f, 74.35f, 3.7f, 5.5f, 17.5f, 1f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 5.5f, 17.5f, 1f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 5.5f, 17.5f, 1f);
            GenerarPinasZona(54f, 64.175f, 3.7f, 17.5f, 29.5f, 1f);
            GenerarPinasZona(65.1f, 74.35f, 3.7f, 17.5f, 29.5f, 1f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 17.5f, 29.5f, 1f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 17.5f, 29.5f, 1f);

            GenerarPinasZona(72.5f, 74.35f, 3.7f, 38.2f, 50.2f, 1f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 38.2f, 50.2f, 1f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 38.2f, 50.2f, 1f);

            GenerarPinasZona(72.5f, 74.35f, 3.7f, 50.2f, 62.2f, 1f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 50.2f, 62.2f, 1f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 50.2f, 62.2f, 1f);

            GenerarPinasZona(54f, 64.175f, 3.7f, 71.1f, 83.1f, 1f);
            GenerarPinasZona(65.1f, 74.35f, 3.7f, 71.1f, 83.1f, 1f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 71.1f, 83.1f, 1f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 71.1f, 83.1f, 1f);
            GenerarPinasZona(54f, 64.175f, 3.7f, 83.1f, 95.1f, 1f);
            GenerarPinasZona(65.1f, 74.35f, 3.7f, 83.1f, 95.1f, 1f);
            GenerarPinasZona(76.2f, 84.525f, 3.7f, 83.1f, 95.1f, 1f);
            GenerarPinasZona(87.3f, 94.7f, 3.7f, 83.1f, 95.1f, 1f);
        }

        Debug.Log("Total de plantas de piña colocadas: " + contPlants);
    }
}