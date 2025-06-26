using UnityEngine;

public class PineappleManager : MonoBehaviour
{
    private PineappleReset[] pineapples;

    void Start()
    {
        pineapples = FindObjectsOfType<PineappleReset>();
    }

    public void ResetAllPineapples()
    {
        foreach (var pineapple in pineapples)
        {
            if (pineapple != null)
                pineapple.ResetPineapple();
        }
    }
}
