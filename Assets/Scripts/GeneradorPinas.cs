using Oculus.Interaction;
using UnityEngine;

public class PlantingPineappleGrid : MonoBehaviour
{
    public GameObject pineapple;
    public Terrain terrain;
    public Transform jugadorOCamara;
    public Camera mainCamera;
    public float distanciaActivacionC = 20f;
    public int opcionSiembra = 1;


    void Start()
    {
        int contPlants = 0;

        //float[] zonasX = { 5.5f, 38.2f, 71.1f };
        //float zMin1 = 4.7f, zMax1 = 46.9f;
        //float zMin2 = 54f, zMax2 = 94.7f;

        float[] zonasX = {40.2f};
        float zMin1 = 45.4f, zMax1 = 46.9f;
       

        foreach (float startX in zonasX)
        {
            float endX = startX + 21f;
            colocarPinas(startX, endX, zMin1, zMax1, ref contPlants);
            //colocarPinas(startX, endX, zMin2, zMax2, ref contPlants);
        }

        Debug.Log("Total de pinas colocadas: " + contPlants);
    }

    void colocarPinas(float startX, float endX, float startZ, float endZ, ref int contador)
    {
        for (float ileraZ = startZ; ileraZ < endZ; ileraZ += 3.7f)
        {
            if (opcionSiembra == 1)
            {
                for (float ileraX = startX; ileraX < endX; ileraX++)
                {
                   
                    if (ileraX >= 35f && ileraX <= 64f && ileraZ >= 54f && ileraZ <= 71.1f)
                        continue;

                    float y = terrain.SampleHeight(new Vector3(ileraX, 0, ileraZ));
                    Vector3 posicion = new Vector3(ileraX - 0.0f, y + 0.45F, ileraZ);
                    Quaternion rotacion = Quaternion.Euler(-90f, 0f, 0f);


                    GameObject contenedor = new GameObject("Contenedor_Pina");
                    contenedor.transform.position = posicion;

                    GameObject visual = Instantiate(pineapple, posicion, rotacion);
                    float randonomScale = Random.Range(1.3f, 0.8f);
                    visual.transform.localScale = Vector3.one * randonomScale;
                    visual.transform.SetParent(contenedor.transform);

                    contenedor.AddComponent<PinasMakeChildrenGrabbable>();
                    contenedor.AddComponent<SnapInteractor>();

                    var snap = contenedor.AddComponent<SnapPinas>();
                    snap.objetoVisual = visual;
                    snap.posicionOrigen = posicion;
                    snap.rotacionOrigen = rotacion;



                    var script = contenedor.AddComponent<ActivadorPorProximidadPina>();
                    script.referencia = jugadorOCamara;
                    script.camaraJugador = mainCamera;
                    script.distanciaActivacion = distanciaActivacionC;
                    script.chequearCadaFrame = true;


                    contador++;
                }
            }
            else if (opcionSiembra == 2)
            {
                for (float ileraX = startX; ileraX < endX; ileraX += 0.8f)
                {
                
                    if (ileraX >= 38.1f && ileraX <= 61.2f && ileraZ >= 54f && ileraZ <= 71.1f)
                        continue;

                    float y = terrain.SampleHeight(new Vector3(ileraX, 0, ileraZ));
                    Vector3 posicion = new Vector3(ileraX - 0.0f, y + 0.45F, ileraZ );
                    Quaternion rotacion = Quaternion.Euler(-90f, 0f, 0f);


                    GameObject contenedor = new GameObject("Contenedor_Pina");
                    contenedor.transform.position = posicion;

                    GameObject visual = Instantiate(pineapple, posicion, rotacion);
                    float randonomScale = Random.Range(1.3f, 0.8f);
                    visual.transform.localScale = Vector3.one * randonomScale;
                    visual.transform.SetParent(contenedor.transform);

                    contenedor.AddComponent<PinasMakeChildrenGrabbable>();
                    contenedor.AddComponent<SnapInteractor>();

                    var snap = contenedor.AddComponent<SnapPinas>();
                    snap.objetoVisual = visual;
                    snap.posicionOrigen = posicion;
                    snap.rotacionOrigen = rotacion;



                    var script = contenedor.AddComponent<ActivadorPorProximidadPina>();
                    script.referencia = jugadorOCamara;
                    script.camaraJugador = mainCamera;
                    script.distanciaActivacion = distanciaActivacionC;
                    script.chequearCadaFrame = true;


                    contador++;
                }
            }
            
        }
    }

}