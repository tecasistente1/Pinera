using UnityEngine;

public class ActivadorPorProximidad : MonoBehaviour
{
    public Transform referencia; 
    public float distanciaActivacion = 100f;
    public bool chequearCadaFrame = false;
    public float intervaloChequeo = 0.5f;
    private float tiempoSiguienteChequeo = 0f;
    public GameObject objetoVisual; 

    void Start()
    {
        if (!chequearCadaFrame)
        {
            EvaluarProximidad();
        }
    }

    void Update()
    {
        if (chequearCadaFrame && Time.time >= tiempoSiguienteChequeo)
        {
            EvaluarProximidad();
            tiempoSiguienteChequeo = Time.time + intervaloChequeo;
        }
    }

    void EvaluarProximidad()
    {
        if (referencia == null || objetoVisual == null) return;

        float distancia = Vector3.Distance(referencia.position, transform.position);
        bool activo = distancia <= distanciaActivacion;

        if (objetoVisual.activeSelf != activo)
        {
            objetoVisual.SetActive(activo);
        }
    }
}
