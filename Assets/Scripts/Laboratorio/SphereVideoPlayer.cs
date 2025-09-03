using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class SphereVideoPlayer : MonoBehaviour
{
    [Header("Referencia al único VideoPlayer de la esfera")]
    public VideoPlayer videoPlayer;

    [Header("Clips disponibles (índices 0..N)")]
    public VideoClip[] clips;

    [Header("Opcional")]
    public bool loop = true;

    private void Start()
    {
        // Tomar el índice elegido en la escena anterior
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
    }

    private IEnumerator PlaySelected(int index)
    {
        videoPlayer.Stop();
        videoPlayer.isLooping = loop;
        videoPlayer.clip = clips[index];

        // Asegura preparación antes de reproducir (evita pantalla negra en algunos targets)
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
    }
}
