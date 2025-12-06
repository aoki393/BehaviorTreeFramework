using UnityEngine;

public class MonoBehaviorSingleton : MonoBehaviour
{
    public static MonoBehaviorSingleton Instance;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
