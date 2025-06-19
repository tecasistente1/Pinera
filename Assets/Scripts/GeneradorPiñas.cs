using UnityEngine;

public class PlantingPineappleGrid : MonoBehaviour
{
    public GameObject pineapple;
    public Terrain terrain;
    public Transform jugadorOCamara;
    public Camera mainCamera; 
    public float distanciaActivacionC = 20f;


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
        for (float ileraX = startX; ileraX < endX; ileraX++)
        {
            for (float ileraZ = startZ; ileraZ < endZ; ileraZ += 3.7f)
            {
                // Zona central ignorada totalmente
                if (ileraX >= 38.1f && ileraX <= 61.2f && ileraZ >= 54f && ileraZ <= 71.1f)
                    continue;

                float y = terrain.SampleHeight(new Vector3(ileraX, 0, ileraZ));
                Vector3 posicion = new Vector3(ileraX + 0.3f, y + 0.05f, ileraZ + 0.1f);
                Quaternion rotacion = Quaternion.Euler(-90f, 0f, 0f);

                // Solo en este rango se agregan los snaps
                if (ileraX >= 38.1f && ileraX <= 61.2f && ileraZ >= 43f && ileraZ <= 47f)
                {
                    // --------- Piña grabable CON snap ---------
                    GameObject superContenedor = new GameObject("SuperContenedorSnap_" + contador);
                    superContenedor.transform.position = posicion;

                    BoxCollider box = superContenedor.AddComponent<BoxCollider>();
                    box.isTrigger = true;
                    box.size = new Vector3(0.5f, 0.5f, 0.5f);
                    box.center = Vector3.zero;
                    superContenedor.AddComponent<SuperSnapZone>();

                    GameObject contenedor = new GameObject("Contenedor_Piña");
                    contenedor.transform.position = posicion;
                    contenedor.transform.SetParent(superContenedor.transform);

                    GameObject visual = Instantiate(pineapple, posicion, rotacion);
                    float randonomScale = Random.Range(0.18f, 0.115f);
                    visual.transform.localScale = Vector3.one * randonomScale;
                    visual.transform.SetParent(contenedor.transform);

                    contenedor.AddComponent<PiñasMakeChildrenGrabbable>();

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

                    Debug.Log("Piña con snap creada en: " + posicion + " | Contador: " + contador);
                }
                else
                {
                    // --------- Piña grabable SIN snap ---------
                    GameObject contenedor = new GameObject("Contenedor_Piña");
                    contenedor.transform.position = posicion;

                    GameObject visual = Instantiate(pineapple, posicion, rotacion);
                    float randonomScale = Random.Range(0.18f, 0.115f);
                    visual.transform.localScale = Vector3.one * randonomScale;
                    visual.transform.SetParent(contenedor.transform);

                    contenedor.AddComponent<PiñasMakeChildrenGrabbable>();

                    var script = contenedor.AddComponent<ActivadorPorProximidad2>();
                    script.referencia = jugadorOCamara;
                    script.camaraJugador = mainCamera;
                    script.distanciaActivacion = distanciaActivacionC;
                    script.chequearCadaFrame = true;
                    script.objetoVisual = visual;
                }

                contador++;
            }
        }
    }

}
