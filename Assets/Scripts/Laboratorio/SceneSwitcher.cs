using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneA = "Laboratory Scene";
    public string sceneB = "Piñera";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate que el jugador tenga este tag
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == sceneA)
            {
                SceneManager.LoadScene(sceneB);
            }
            else if (currentScene == sceneB)
            {
                SceneManager.LoadScene(sceneA);
            }
            else
            {
                Debug.LogWarning("Current scene is not recognized.");
            }
        }
    }
}