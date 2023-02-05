using UnityEngine;

public class GameRef : MonoBehaviour
{
    public static GameRef Instance = null;

    public Door Door;
    public PlayerCollect PlayerCollect;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
