using UnityEngine;
using UnityEngine.Video;

public class PlayVideoController : MonoBehaviour
{
    [Header("References")]
    public GameObject videoSphere;
    public VideoPlayer videoPlayer;
    public GameObject XRController;
    public GameObject teleportationEnvironment;

    [Header("Player Settings")]
    public string playerTag = "Player";   // Tag del Player
    public float yOffset = -0.5f;         // Ajuste vertical para centrar al jugador en la esfera

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ActivateVideo();

            // Colocar al player en el centro de la esfera
            if (videoSphere != null)
            {
                Vector3 sphereCenter = videoSphere.transform.position;

                Vector3 newPos = new Vector3(
                    sphereCenter.x,
                    sphereCenter.y + yOffset, // un poco más abajo si quieres
                    sphereCenter.z
                );

                other.transform.position = newPos;
                other.transform.rotation = Quaternion.identity;
            }
        }
    }

    public void ActivateVideo()
    {
        if (videoSphere != null)
            videoSphere.SetActive(true);

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }

        if (teleportationEnvironment != null)
            teleportationEnvironment.SetActive(false);

        if (XRController != null)
            XRController.SetActive(true);
    }
}
