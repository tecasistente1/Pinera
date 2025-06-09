using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float velocidadMovimiento = 10f;
    public float velocidadRotacion = 50f;
    public float multiplicadorSprint = 2f;
    public bool usarShiftParaCorrer = true;

    void Update()
    {
        // Movimiento
        float velocidadActual = velocidadMovimiento;
        if (usarShiftParaCorrer && Input.GetKey(KeyCode.LeftShift))
        {
            velocidadActual *= multiplicadorSprint;
        }

        Vector3 direccion = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) direccion += transform.forward;
        if (Input.GetKey(KeyCode.S)) direccion -= transform.forward;
        if (Input.GetKey(KeyCode.A)) direccion -= transform.right;
        if (Input.GetKey(KeyCode.D)) direccion += transform.right;
        if (Input.GetKey(KeyCode.E)) direccion += transform.up;
        if (Input.GetKey(KeyCode.Q)) direccion -= transform.up;

        transform.position += direccion * velocidadActual * Time.deltaTime;

        // Rotación con flechas
        float rotacionHorizontal = 0f;
        float rotacionVertical = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) rotacionHorizontal = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) rotacionHorizontal = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) rotacionVertical = -1f;
        if (Input.GetKey(KeyCode.DownArrow)) rotacionVertical = 1f;

        transform.Rotate(Vector3.up, rotacionHorizontal * velocidadRotacion * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, rotacionVertical * velocidadRotacion * Time.deltaTime, Space.Self);
    }
}
