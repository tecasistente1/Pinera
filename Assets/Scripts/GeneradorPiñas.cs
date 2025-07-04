using Oculus.Interaction;
using UnityEngine;

public class PlantingPineappleGrid : MonoBehaviour
{
    public GameObject pineapple1;
    public GameObject pineapple2;
    public Terrain terrain;
    public Transform jugadorOCamara;
    public Camera mainCamera;
    public float distanciaActivacionC = 20f;
    public int opcionSiembra = 1;
    public bool usarBajaResolucion = false;


    void Start()
    {
        int contPlants = 0;

        float[] zonasX = { 5.5f, 38.2f, 71.1f };
        float zMin1 = 4.7f, zMax1 = 46.9f;
        float zMin2 = 54f, zMax2 = 94.7f;

        foreach (float startX in zonasX)
        {
            float endX = startX + 24f;
            colocarPiñas(startX, endX, zMin1, zMax1, ref contPlants);
            colocarPiñas(startX, endX, zMin2, zMax2, ref contPlants);
        }

        Debug.Log("Total de piñas colocadas: " + contPlants);
    }

    void colocarPiñas(float startX, float endX, float startZ, float endZ, ref int contador)
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
                    Vector3 posicion = new Vector3(ileraX, y , ileraZ + 0.35f);
                    Quaternion rotacion = Quaternion.Euler(-90f, 0f, 0f);


                    GameObject contenedor = new GameObject("Contenedor_Piña");
                    contenedor.transform.position = posicion;

                    GameObject visual;
                    if (usarBajaResolucion)
                    {
                        visual = Instantiate(pineapple2, posicion, rotacion);
                    }
                    else
                    {
                        visual = Instantiate(pineapple1, posicion, rotacion);
                    }
                    float randonomScale = Random.Range(0.17f, 0.09f);
                    visual.transform.localScale = Vector3.one * randonomScale;
                    visual.transform.SetParent(contenedor.transform);

                    contenedor.AddComponent<PiñasMakeChildrenGrabbable>();
                    contenedor.AddComponent<SnapInteractor>();

                    var snap = contenedor.AddComponent<SnapPiñas>();
                    snap.objetoVisual = visual;
                    snap.posicionOrigen = posicion;
                    snap.rotacionOrigen = rotacion;



                    var script = contenedor.AddComponent<ActivadorPorProximidad2>();
                    script.referencia = jugadorOCamara;
                    script.camaraJugador = mainCamera;
                    script.distanciaActivacion = distanciaActivacionC;
                    script.chequearCadaFrame = true;
                    script.objetoVisual = visual;


                    contador++;
                }
            }
            else if (opcionSiembra == 2)
            {
                for (float ileraX = startX; ileraX < endX; ileraX += 0.7f)
                {
                
                    if (ileraX >= 38.1f && ileraX <= 61.2f && ileraZ >= 54f && ileraZ <= 71.1f)
                        continue;

                    float y = terrain.SampleHeight(new Vector3(ileraX, 0, ileraZ));
                    Vector3 posicion = new Vector3(ileraX , y, ileraZ + 0.35f);
                    Quaternion rotacion = Quaternion.Euler(-90f, 0f, 0f);


                    GameObject contenedor = new GameObject("Contenedor_Piña");
                    contenedor.transform.position = posicion;

                    GameObject visual;
                    if (usarBajaResolucion)
                    {
                        visual = Instantiate(pineapple2, posicion, rotacion);
                    }
                    else
                    {
                        visual = Instantiate(pineapple1, posicion, rotacion);
                    }
                    float randonomScale = Random.Range(1.3f, 0.8f);
                    visual.transform.localScale = Vector3.one * randonomScale;
                    visual.transform.SetParent(contenedor.transform);

                    contenedor.AddComponent<PiñasMakeChildrenGrabbable>();
                    contenedor.AddComponent<SnapInteractor>();

                    var snap = contenedor.AddComponent<SnapPiñas>();
                    snap.objetoVisual = visual;
                    snap.posicionOrigen = posicion;
                    snap.rotacionOrigen = rotacion;



                    var script = contenedor.AddComponent<ActivadorPorProximidad2>();
                    script.referencia = jugadorOCamara;
                    script.camaraJugador = mainCamera;
                    script.distanciaActivacion = distanciaActivacionC;
                    script.chequearCadaFrame = true;
                    script.objetoVisual = visual;


                    contador++;
                }
            }
            
        }
    }

}