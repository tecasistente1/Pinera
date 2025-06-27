using UnityEngine;

public class ActivadorPorProximidadPiñasCamión : MonoBehaviour
{
    public Transform referencia;
    public Camera camaraJugador;
    public float distanciaActivacion = 100f;
    public bool chequearCadaFrame = false;
    public float intervaloChequeo = 0.5f;
    private float tiempoSiguienteChequeo = 0f;
    public GameObject objetoVisual;

    void Start()
    {
        if (!chequearCadaFrame)
        {
            EvaluarProximidadYVision();
        }
    }

    void Update()
    {
        if (chequearCadaFrame && Time.time >= tiempoSiguienteChequeo)
        {
            EvaluarProximidadYVision();
            tiempoSiguienteChequeo = Time.time + intervaloChequeo;
        }
    }

    void EvaluarProximidadYVision()
    {
        if (referencia == null || objetoVisual == null || camaraJugador == null) return;

        float distancia = Vector3.Distance(referencia.position, transform.position);
        if (distancia > distanciaActivacion)
        {
            if (objetoVisual.activeSelf)
                objetoVisual.SetActive(false);
            return;
        }

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camaraJugador);
        Renderer rend = objetoVisual.GetComponent<Renderer>();
        if (rend == null)
            rend = objetoVisual.GetComponentInChildren<Renderer>();
        if (rend == null) return;

        bool enVision = GeometryUtility.TestPlanesAABB(planes, rend.bounds);
        if (objetoVisual.activeSelf != enVision)
        {
            objetoVisual.SetActive(enVision);
        }
    }
}