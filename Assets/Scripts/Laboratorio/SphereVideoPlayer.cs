using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SkyboxVideoPlayer : MonoBehaviour
{
    [Header("VideoPlayer de la escena")]
    public VideoPlayer videoPlayer;

    [Header("Clips disponibles (índices 0..N)")]
    public VideoClip[] clips;

    [Header("Material del Skybox para el video 360")]
    public Material videoSkybox; // Material con Skybox/Panoramic y RenderTexture asignado

    private Material defaultSkybox; // Guarda el skybox original

    [Header("Opcional")]
    public bool loop = false;

    private void Start()
    {
        // Guardar el skybox original
        defaultSkybox = RenderSettings.skybox;

        int index = VideoManager.GetVideoIndexOrDefault(0);

        if (clips == null || clips.Length == 0)
        {
            Debug.LogError("[SkyboxVideoPlayer] No hay clips asignados.");
            return;
        }
        if (index < 0 || index >= clips.Length)
        {
            Debug.LogWarning($"[SkyboxVideoPlayer] Índice {index} fuera de rango. Usando 0.");
            index = 0;
        }

        StartCoroutine(PlaySelected(index));

        // Escuchar cuando el video termine
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private IEnumerator PlaySelected(int index)
    {
        // Cambiar skybox al material de video
        if (videoSkybox != null)
            RenderSettings.skybox = videoSkybox;

        videoPlayer.Stop();
        videoPlayer.isLooping = loop;
        videoPlayer.clip = clips[index];

        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Restaurar skybox original
        if (defaultSkybox != null)
            RenderSettings.skybox = defaultSkybox;

        // Regresar a la escena anterior
        int prevIndex = SceneManager.GetActiveScene().buildIndex - 1;

        if (prevIndex >= 0)
        {
            SceneManager.LoadScene(prevIndex);
        }
        else
        {
            Debug.LogWarning("No hay escena anterior en Build Settings.");
        }
    }
}
