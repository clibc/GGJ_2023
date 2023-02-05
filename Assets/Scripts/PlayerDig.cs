using UnityEngine;

public class PlayerDig : MonoBehaviour
{
    [SerializeField] GameObject CursorPrefab;
    [SerializeField] float MaxRadius = 2.0f;
    [SerializeField] LayerMask GroundLayer;
    Transform Player;
    Camera MainCamera;

    GameObject Cursor;

    Block CurrentlyHoveringTile;
    Collider2D ColliderOld;

    void Start()
    {
        MainCamera = Camera.main;

        Cursor = Instantiate(CursorPrefab, transform);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        {
            Vector3 PlayerPos = Player.position;
            Vector3 MousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            MousePos.z = PlayerPos.z;
            Vector3 Diff = MousePos - Player.position;
            MousePos = PlayerPos + Diff.normalized * Mathf.Clamp(Diff.magnitude, 0, MaxRadius);
            Cursor.transform.position = MousePos;
        }

        {
            Collider2D Col = Physics2D.OverlapCircle(Cursor.transform.position, 0.1f, GroundLayer);
            if(Col != null && Col != ColliderOld)
            {
                if(ColliderOld != null)
                    OnCursorGroundColliderExit(ColliderOld);
                OnCursorGroundColliderEnter(Col);
                ColliderOld = Col;
            }
            else if(Col == null && ColliderOld != null)
            {
                OnCursorGroundColliderExit(ColliderOld);
                ColliderOld = null;
            }
        }

        if(CurrentlyHoveringTile != null)
            Debug.Log(CurrentlyHoveringTile.gameObject.name);

        if(Input.GetMouseButtonDown(0) && CurrentlyHoveringTile != null)
        {
            CurrentlyHoveringTile.Hit();
        }
    }

    void OnCursorGroundColliderEnter(Collider2D Other)
    {
        CurrentlyHoveringTile = Other.transform.parent.GetComponent<Block>();
        CurrentlyHoveringTile.Highligt(true);
    }

    void OnCursorGroundColliderExit(Collider2D Other)
    {
        Other.transform.parent.GetComponent<Block>().Highligt(false);
        CurrentlyHoveringTile = null;
    }
}
