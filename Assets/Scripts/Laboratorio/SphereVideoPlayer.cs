using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SphereVideoPlayer : MonoBehaviour
{
    [Header("Referencia al único VideoPlayer de la esfera")]
    public VideoPlayer videoPlayer;

    [Header("Clips disponibles (índices 0..N)")]
    public VideoClip[] clips;

    [Header("Opcional")]
    public bool loop = false; // 👈 ahora mejor dejar en false si quieres que vuelva

    private void Start()
    {
        int index = VideoManager.GetVideoIndexOrDefault(0);

        if (clips == null || clips.Length == 0)
        {
            Debug.LogError("[SphereVideoPlayer] No hay clips asignados.");
            return;
        }
        if (index < 0 || index >= clips.Length)
        {
            Debug.LogWarning($"[SphereVideoPlayer] Índice {index} fuera de rango. Usando 0.");
            index = 0;
        }

        StartCoroutine(PlaySelected(index));

        // 🔹 Escuchar cuando el video termine
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private IEnumerator PlaySelected(int index)
    {
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
        // 🔹 Regresar a la escena anterior
        //   Usamos SceneManager.GetActiveScene().buildIndex - 1
        //   para volver a la escena de donde venías
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