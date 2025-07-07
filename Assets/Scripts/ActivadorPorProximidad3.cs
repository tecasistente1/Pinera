using UnityEngine;

public class ActivadorPorProximidad3 : MonoBehaviour
{
    public Transform referencia; // Objeto que representa la posición del jugador o centro de chequeo
    public int xInicio = 0;
    public int xFin = 100;
    public int zInicio = 0;
    public int zFin = 100;
    public float distanciaActivacion = 100f;
    public bool chequearCadaFrame = true;
    public float intervaloChequeo = 0.5f;
    private float tiempoSiguienteChequeo = 0f;
    public GameObject objetoVisual;

    void Start()
    {
        if (objetoVisual == null)
        {
            Debug.LogWarning("El objeto visual no está asignado en el Inspector. Intentando asignar primer hijo.");
            if (transform.childCount > 0)
                objetoVisual = transform.GetChild(0).gameObject;
        }

        if (!chequearCadaFrame)
        {
            EvaluarProximidadAlArea();
        }
    }

    void Update()
    {
        if (chequearCadaFrame && Time.time >= tiempoSiguienteChequeo)
        {
            EvaluarProximidadAlArea();
            tiempoSiguienteChequeo = Time.time + intervaloChequeo;
        }
    }

    void EvaluarProximidadAlArea()
    {
        if (referencia == null)
        {
            Debug.LogError("Referencia no asignados.");
            return;
        }
        if (objetoVisual == null)
        {
            Debug.LogError("El objeto visual no está asignado en el Inspector.");
            return;
        }

        Vector3 posicionReferencia = referencia.position;
        Debug.Log($"Posición de referencia: {posicionReferencia}");
        if (posicionReferencia.x >= xInicio - distanciaActivacion && posicionReferencia.x <= xFin + distanciaActivacion &&
            posicionReferencia.z >= zInicio - distanciaActivacion && posicionReferencia.z <= zFin + distanciaActivacion)
        {
            if (!objetoVisual.activeSelf)
                objetoVisual.SetActive(true);
        }
        else
        {
            if (objetoVisual.activeSelf)
                objetoVisual.SetActive(false);
        }
    }
}