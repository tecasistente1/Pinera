using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnClick : MonoBehaviour
{
    public GameObject videoSphere;
    public VideoPlayer videoPlayer;
    public GameObject XRController; // Controlador XR para el poke

    // Nuevas referencias
    public GameObject teleportationEnvironment;

    public void ActivateVideo()
    {
        if (videoSphere != null)
            videoSphere.SetActive(true);

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }

        // Desactivar teleporte y poke
        if (teleportationEnvironment != null)
            teleportationEnvironment.SetActive(false);
        if (XRController != null)
            XRController.SetActive(true);
        
    }
}
