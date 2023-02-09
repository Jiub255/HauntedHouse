using UnityEngine;

public class MasterSingleton : MonoBehaviour
{
    public static MasterSingleton Instance { get; private set; }


    public AudioManager AudioManager { get; private set; }
    //public InputManager InputManager { get; private set; }
    public ObjectPool ObjectPool { get; private set; }

    public GameObject Canvas { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Found another MasterSingleton instance, destroying it");
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        AudioManager = GetComponentInChildren<AudioManager>();
        Canvas = GetComponentInChildren<Canvas>().gameObject;
       // InputManager = GetComponentInChildren<InputManager>();
        ObjectPool = GetComponentInChildren<ObjectPool>();

        DontDestroyOnLoad(gameObject);
    }
}