using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [Header("Video Controller")]
    public PlayVideoController videoController; // referencia al script que controla el video

    [Header("Player Settings")]
    public string playerTag = "Player";  // Tag del player
    public float yOffset = 0f;        // Ajuste vertical para que el player quede dentro de la esfera

    private void OnTriggerEnter(Collider other)
    {
        // Verificar que el objeto que entró es el Player
        if (other.CompareTag(playerTag) && videoController != null)
        {
            // Activar el video
            videoController.ActivateVideo();

            if (videoController.videoSphere != null)
            {
                // Tomar la posición del centro de la esfera
                Vector3 sphereCenter = videoController.videoSphere.transform.position;

                // Nueva posición con offset en Y
                Vector3 newPos = new Vector3(
                    sphereCenter.x,
                    sphereCenter.y + yOffset,
                    sphereCenter.z
                );

                // Teletransportar al player
                other.transform.position = newPos;

                // Opcional: resetear rotación del player
                other.transform.rotation = Quaternion.identity;
            }
        }
    }
}
