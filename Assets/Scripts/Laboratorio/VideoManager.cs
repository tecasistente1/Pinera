using UnityEngine;

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance;
    public int videoIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static void SetVideoIndex(int idx)
    {
        if (Instance == null)
        {
            var go = new GameObject("VideoManager(Auto)");
            Instance = go.AddComponent<VideoManager>();
            DontDestroyOnLoad(go);
        }
        Instance.videoIndex = idx;
        PlayerPrefs.SetInt("videoIndex", idx); // respaldo
    }

    public static int GetVideoIndexOrDefault(int def = 0)
    {
        if (Instance != null) return Instance.videoIndex;
        return PlayerPrefs.GetInt("videoIndex", def);
    }
}
