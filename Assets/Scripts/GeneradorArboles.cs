using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantingTreeGrid : MonoBehaviour
{
    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;
    public GameObject tree4;
    public Terrain terrain;
    public Transform jugadorOCamara;
    public Camera mainCamera;
    public float distanciaActivacionC = 3000f;
    public bool bajaResolucion = false;

    private int contPlants = 0;
    public int OpcionPlantacion = 1;

    void Start()
    {
        float[] zonasX = { 9.4f, 42.1f, 75f };
        float zMin1 = 10.2f, zMax1 = 40f;
        float zMin2 = 59.5f, zMax2 = 89f;

        foreach (float startX in zonasX)
        {
            float endX = startX + 20f;
            PlantarBloques(startX, endX, zMin1, zMax1);
            PlantarBloques(startX, endX, zMin2, zMax2);
        }

        Debug.Log("Total de plantas de piñas colocadas: " + contPlants);
    }

    void PlantarBloques(float startX, float endX, float startZ, float endZ)
    {
        for (float ileraZ = startZ; ileraZ < endZ; ileraZ += 7.4f)
        {
            if (OpcionPlantacion == 1)
            {
                for (float ileraX = startX; ileraX < endX; ileraX += 8f)
                {
                    float randomX = Random.Range(-1.5f, 1.5f);
                    PlantarUnaPlanta(ileraX + randomX, ileraZ);
                }
            }
            else if (OpcionPlantacion == 2)
            {
                for (float ileraX = startX; ileraX < endX; ileraX += 6f)
                {
                    float randomX = Random.Range(-1.5f, 1.5f);
                    PlantarUnaPlanta(ileraX + randomX, ileraZ);
                }
            }
        }
    }
    void PlantarUnaPlanta(float x, float z)
    {

        if (x >= 35f && x <= 64f && z >= 54f && z <= 62f)
            return;
        float y = terrain.SampleHeight(new Vector3(x, 0, z));


        GameObject contenedorArbol = new GameObject("Arbol_" + x.ToString("F2") + "_" + z.ToString("F2"));
        contenedorArbol.transform.position = new Vector3(x, y, z);

        GameObject prefab;
        if (!bajaResolucion)
        { 
            int randomPlant = Random.Range(0, 3);
            if (randomPlant == 1)
            {
                prefab = tree1;
            }
            else if (randomPlant == 2)
            {
                prefab = tree2;
            }
            else
            {
                prefab = tree3;
            }
        }else
        {
            prefab = tree4;
        }

        Vector3 posicionVisual = new Vector3(x, y - 0.01f, z);
        GameObject plantaVisual = Instantiate(prefab, posicionVisual, Quaternion.identity, contenedorArbol.transform);


        DestroyImmediate(plantaVisual.GetComponent<Rigidbody>());
        DestroyImmediate(plantaVisual.GetComponent<Collider>());


        var script = contenedorArbol.AddComponent<ActivadorPorProximidad>();
        script.referencia = jugadorOCamara;
        script.distanciaActivacion = distanciaActivacionC;
        script.chequearCadaFrame = true;
        script.objetoVisual = plantaVisual;

        contPlants++;
    }
}

