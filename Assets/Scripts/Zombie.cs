using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] Transform GroundColliderPoint;
    [SerializeField] SpriteRenderer Renderer;

    Vector3 VelocityDir = Vector3.right;

    bool IsGrounded = false;

    public float MoveSpeed = 5;
    public float FallSpeed = 5;

    void Update()
    {
        Collider2D Col = Physics2D.OverlapCircle(GroundColliderPoint.position, 0.1f, GroundLayer);
        IsGrounded = Col != null;
        if(IsGrounded)
        {
            RB.velocity = VelocityDir * MoveSpeed;
        }
        else
        {
            RB.velocity = Vector3.down * FallSpeed;
        }

        Renderer.flipX = VelocityDir.x < 0;
    }

    void OnTriggerEnter(Collider Col)
    {
        Debug.Log("Collides");

        if (Col.gameObject.CompareTag("Ground"))
        {
            VelocityDir *= -1;
        }
    }
}
