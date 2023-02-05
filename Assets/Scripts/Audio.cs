using UnityEngine;

public enum audio_clip
{
    Fail = 0,
    Dig,
    Collect,
    Jump,
}

public class Audio : MonoBehaviour
{
    static Audio Instance = null;
    static AudioSource Source;

    [SerializeField] AudioClip[] Clips;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        Source = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }

    static public void Play(audio_clip Clip)
    {
        Source.PlayOneShot(Instance.Clips[(int)Clip]);
    }
}
