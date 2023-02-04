using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float JumpPower = 1f;
    [SerializeField] float WallSlideSpeed = 1f;
    [SerializeField] LayerMask GroundLayer;

    [SerializeField] Transform GroundCheckSpot;
    [SerializeField] Transform RightCheckSpot;
    [SerializeField] Transform LeftCheckSpot;

    Rigidbody2D RB;
    Vector3 MoveDir;
    public bool IsGrounded = false;
    public bool IsTouchingWall = false;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float InputX = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.A))
        {
            InputX = -1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            InputX = 1;
        }
        else
        {
            InputX = 0;
        }

        IsGrounded = Physics2D.OverlapCircle(GroundCheckSpot.position, 0.1f, GroundLayer) != null;
        Collider2D RightWall = Physics2D.OverlapCircle(RightCheckSpot.position, 0.1f, GroundLayer);
        Collider2D LeftWall = Physics2D.OverlapCircle(LeftCheckSpot.position, 0.1f, GroundLayer);
        IsTouchingWall = RightWall != null || LeftWall != null;

        MoveDir = new Vector3(InputX, 0, 0.0f).normalized;
        Vector2 Velocity = MoveSpeed * Time.fixedDeltaTime * MoveDir;
        Velocity.y = RB.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Velocity += Vector2.up * JumpPower;
        }
        if(IsTouchingWall && !IsGrounded)
        {
            if(RightWall != null)
            {

            }
            else if(LeftWall != null)
            {

            }
            Velocity = Vector2.down * WallSlideSpeed;
        }

        RB.velocity = Velocity;

        Debug.DrawRay(transform.position + new Vector3(0.5f, -0.5f), Vector3.down, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(-0.5f, -0.5f), Vector3.down, Color.blue);
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("Ground"))
        {
            Other.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.CompareTag("Ground"))
        {
            Other.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}