using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Tooltip("Índice del video a reproducir (0,1,2,...)")]
    public int videoToPlay = 0;

    // Llama esto desde el OnClick() de un botón UI
    public void Go()
    {
        VideoManager.SetVideoIndex(videoToPlay);
        SceneManager.LoadScene("VideoPlayer", LoadSceneMode.Single);
    }

    // Si es un portal 3D con trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Go();
        }
    }
}