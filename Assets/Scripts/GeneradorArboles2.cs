using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantingTreeGrid2 : MonoBehaviour
{
    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;
    public GameObject tree4;
    public Terrain terrain;
    public Transform jugadorOCamara;
    public float distanciaActivacionC = 3000f;
    public bool bajaResolucion = false;

    private int contPlants = 0;
    public int OpcionPlantacion = 1;

    //void Start()
    //{
    //    float[] zonasX = { 9.4f, 42.1f, 75f };
    //    float zMin1 = 10.2f, zMax1 = 40f;
    //    float zMin2 = 59.5f, zMax2 = 89f;

    //    foreach (float startX in zonasX)
    //    {
    //        float endX = startX + 20f;
    //        PlantarBloques(startX, endX, zMin1, zMax1);
    //        PlantarBloques(startX, endX, zMin2, zMax2);
    //    }

    //    Debug.Log("Total de plantas de arboles colocados: " + contPlants);
    //}

    //void PlantarBloques(float startX, float endX, float startZ, float endZ)
    //{
    //    for (float ileraZ = startZ; ileraZ < endZ; ileraZ += 7.4f)
    //    {
    //        if (OpcionPlantacion == 1)
    //        {
    //            for (float ileraX = startX; ileraX < endX; ileraX += 8f)
    //            {
    //                float randomX = Random.Range(-1.5f, 1.5f);
    //                PlantarUnaPlanta(ileraX + randomX, ileraZ);
    //            }
    //        }
    //        else if (OpcionPlantacion == 2)
    //        {
    //            for (float ileraX = startX; ileraX < endX; ileraX += 6f)
    //            {
    //                float randomX = Random.Range(-1.5f, 1.5f);
    //                PlantarUnaPlanta(ileraX + randomX, ileraZ);
    //            }
    //        }
    //    }
    //}
    //void PlantarUnaPlanta(float x, float z)
    //{

    //    if (x >= 35f && x <= 64f && z >= 54f && z <= 62f)
    //        return;
    //    float y = terrain.SampleHeight(new Vector3(x, 0, z));


    //    GameObject contenedorArbol = new GameObject("Arbol_" + x.ToString("F2") + "_" + z.ToString("F2"));
    //    contenedorArbol.transform.position = new Vector3(x, y, z);

    //    GameObject prefab;
    //    if (!bajaResolucion)
    //    { 
    //        int randomPlant = Random.Range(0, 3);
    //        if (randomPlant == 1)
    //        {
    //            prefab = tree1;
    //        }
    //        else if (randomPlant == 2)
    //        {
    //            prefab = tree2;
    //        }
    //        else
    //        {
    //            prefab = tree3;
    //        }
    //    }else
    //    {
    //        prefab = tree4;
    //    }

    //    Vector3 posicionVisual = new Vector3(x, y - 0.01f, z);
    //    GameObject plantaVisual = Instantiate(prefab, posicionVisual, Quaternion.identity, contenedorArbol.transform);


    //    DestroyImmediate(plantaVisual.GetComponent<Rigidbody>());
    //    DestroyImmediate(plantaVisual.GetComponent<Collider>());


    //    var script = contenedorArbol.AddComponent<ActivadorPorProximidad>();
    //    script.referencia = jugadorOCamara;
    //    script.distanciaActivacion = distanciaActivacionC;
    //    script.chequearCadaFrame = true;
    //    script.objetoVisual = plantaVisual;

    //    contPlants++;
    //}

    private void InstanciarArbolConContenedor(Vector3 position, GameObject prefab, GameObject contenedorVisual)
    {
        GameObject contenedor = new GameObject("Contenedor_" + prefab.name);
        contenedor.transform.position = position;

        GameObject visual = Instantiate(prefab, position, Quaternion.identity, contenedor.transform);

        DestroyImmediate(visual.GetComponent<Rigidbody>());
        DestroyImmediate(visual.GetComponent<Collider>());

        contenedor.transform.SetParent(contenedorVisual.transform);
        contPlants++;
    }
    public void GenerarArbolesZona(float inicioZ, float finZ, float pasoZ, float inicioX, float finX, float pasoX)
    {
        GameObject superContenedor = new GameObject(name + "_ContenedorArboles");
        GameObject contenedorVisual = new GameObject(name + "_ContenedorVisual");

        for (float ileraZ = inicioZ; ileraZ < finZ; ileraZ += pasoZ)
        {
            for (float ileraX = inicioX; ileraX < finX; ileraX += pasoX)
            {
                float worldY = terrain.SampleHeight(new Vector3(ileraX, 0, ileraZ));
                Vector3 posicionVisual = new Vector3(ileraX, worldY - 0.01f, ileraZ);

                // Instanciar alguno de los 4 arbgoles de forma aleatoria
                GameObject prefab = null;
                if (!bajaResolucion)
                {
                    int randomPlant = Random.Range(0, 3);
                    switch (randomPlant)
                    {
                        case 0: prefab = tree1; break;
                        case 1: prefab = tree2; break;
                        case 2: prefab = tree3; break;
                    }
                }
                else
                {
                    prefab = tree4; // Solo baja resolucion
                }
                InstanciarArbolConContenedor(posicionVisual, prefab, contenedorVisual);

            }
        }

        var script = superContenedor.AddComponent<ActivadorPorProximidad3>();
        script.referencia = jugadorOCamara;
        script.distanciaActivacion = distanciaActivacionC;
        script.chequearCadaFrame = true;
        script.objetoVisual = contenedorVisual;
        script.xInicio = (int)inicioX;
        script.xFin = (int)finX;
        script.zInicio = (int)inicioZ;
        script.zFin = (int)finZ;

        contenedorVisual.transform.SetParent(superContenedor.transform);
    }

    void Start()
    {
        int pasoX = 0;
        if (OpcionPlantacion == 1)
        {
            pasoX = 8;
        }else if (OpcionPlantacion == 2)
        {
            pasoX = 6;
        }

        GenerarArbolesZona(10.2f, 11f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX); 
        GenerarArbolesZona(10.2f,11f,7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(17.6f, 18f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX);
        GenerarArbolesZona(17.6f, 18f, 7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(25f, 25.5f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX);
        GenerarArbolesZona(25f, 25.5f, 7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(32.4f, 33f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX);
        GenerarArbolesZona(32.4f, 33f, 7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(39.8f, 40.5f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX);
        GenerarArbolesZona(39.8f, 40.5f, 7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);

        GenerarArbolesZona(10.2f, 11f, 7.4f, 42.1f, 42.1f + pasoX + 1, pasoX);
        GenerarArbolesZona(10.2f, 11f, 7.4f, 42.1f + pasoX * 2, 42.1f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(17.6f, 18f, 7.4f, 42.1f, 42.1f + pasoX + 1, pasoX);
        GenerarArbolesZona(17.6f, 18f, 7.4f, 42.1f + pasoX * 2, 42.1f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(25f, 25.5f, 7.4f, 42.1f, 42.1f + pasoX + 1, pasoX);
        GenerarArbolesZona(25f, 25.5f, 7.4f, 42.1f + pasoX * 2, 42.1f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(32.4f, 33f, 7.4f, 42.1f, 42.1f + pasoX + 1, pasoX);
        GenerarArbolesZona(32.4f, 33f, 7.4f, 42.1f + pasoX * 2, 42.1f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(39.8f, 40.5f, 7.4f, 42.1f, 42.1f + pasoX + 1, pasoX);
        GenerarArbolesZona(39.8f, 40.5f, 7.4f, 42.1f + pasoX * 2, 42.1f + pasoX * 2 + 1, pasoX);

        GenerarArbolesZona(10.2f, 11f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(10.2f, 11f, 7.4f, 75f + pasoX * 2, 75f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(17.6f, 18f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(17.6f, 18f, 7.4f, 75f + pasoX * 2, 75f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(25f, 25.5f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(25f, 25.5f, 7.4f, 75f + pasoX * 2, 75f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(32.4f, 33f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(39.8f, 40.5f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(32.4f, 33f, 7.4f, 75f + pasoX * 2, 75f + pasoX * 2 + 1, pasoX);



        GenerarArbolesZona(59.5f, 60f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX);
        GenerarArbolesZona(59.5f, 60f, 7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(66.9f, 70f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX);
        GenerarArbolesZona(66.9f, 70f, 7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(74.3f, 75f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX);
        GenerarArbolesZona(74.3f, 75f, 7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(81.7f, 82f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX);
        GenerarArbolesZona(81.7f, 82f, 7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(89.1f, 90f, 7.4f, 9.4f, 9.4f + pasoX + 1, pasoX);
        GenerarArbolesZona(89.1f, 90f, 7.4f, 9.4f + pasoX * 2, 9.4f + pasoX * 2 + 1, pasoX);

        GenerarArbolesZona(66.9f, 70f, 7.4f, 42.1f, 42.1f + pasoX + 1, pasoX);
        GenerarArbolesZona(66.9f, 70f, 7.4f, 42.1f + pasoX * 2, 42.1f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(74.3f, 75f, 7.4f, 42.1f, 42.1f + pasoX + 1, pasoX);
        GenerarArbolesZona(74.3f, 75f, 7.4f, 42.1f + pasoX * 2, 42.1f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(81.7f, 82f, 7.4f, 42.1f, 42.1f + pasoX + 1, pasoX);
        GenerarArbolesZona(81.7f, 82f, 7.4f, 42.1f + pasoX * 2, 42.1f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(89.1f, 90f, 7.4f, 42.1f, 42.1f + pasoX + 1, pasoX);
        GenerarArbolesZona(89.1f, 90f, 7.4f, 42.1f + pasoX * 2, 42.1f + pasoX * 2 + 1, pasoX);

        GenerarArbolesZona(59.5f, 60f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(59.5f, 60f, 7.4f, 75f + pasoX * 2, 75f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(66.9f, 70f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(66.9f, 70f, 7.4f, 75f + pasoX * 2, 75f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(74.3f, 75f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(74.3f, 75f, 7.4f, 75f + pasoX * 2, 75f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(81.7f, 82f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(81.7f, 82f, 7.4f, 75f + pasoX * 2, 75f + pasoX * 2 + 1, pasoX);
        GenerarArbolesZona(89.1f, 90f, 7.4f, 75f, 75f + pasoX + 1, pasoX);
        GenerarArbolesZona(89.1f, 90f, 7.4f, 75f + pasoX * 2, 75f + pasoX * 2 + 1, pasoX);
    }
}

