using UnityEngine;

public class GameRef : MonoBehaviour
{
    public static GameRef Instance = null;

    public Door Door;
    public PlayerCollect PlayerCollect;
    public CameraFollow Cam;

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

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LockCursor()
    {
        Cursor.visible = false;
        Application.targetFrameRate = 60;
    }
}
