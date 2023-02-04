using UnityEngine;

public class PlayerDig : MonoBehaviour
{
    [SerializeField] Transform TopCheckSpot;
    [SerializeField] Transform BotCheckSpot;
    [SerializeField] Transform RightCheckSpot;
    [SerializeField] Transform LeftCheckSpot;
    [SerializeField] LayerMask GroundLayer;

    [SerializeField] SpriteAnimator PlayerAnimator;
    [SerializeField] CharacterControllerBase CharacterController;
    [SerializeField] Animator Animator;

    void Update()
    {
        bool IsDigging = false;
        if(Input.GetKey(KeyCode.A) && Physics.CheckSphere(LeftCheckSpot.position, 0.1f, GroundLayer))
        {
            Debug.Log("Digging Left");
            IsDigging = true;
        }
        if (Input.GetKey(KeyCode.D) && Physics.CheckSphere(RightCheckSpot.position, 0.1f, GroundLayer))
        {
            Debug.Log("Digging Right");
            IsDigging = true;
        }
        if (Input.GetKey(KeyCode.W) && Physics.CheckSphere(TopCheckSpot.position, 0.1f, GroundLayer))
        {
            Debug.Log("Digging Top");
            IsDigging = true;
        }
        if (Input.GetKey(KeyCode.S) && Physics.CheckSphere(BotCheckSpot.position, 0.1f, GroundLayer))
        {
            Debug.Log("Digging Bottom");
            IsDigging = true;
        }

        PlayerAnimator.enabled = !IsDigging;
        if (IsDigging)
        {
            Animator.Play("ClassicClimb");
        }
        else
        {
            if(PlayerAnimator.m_CurrentAnimationName != "")
            {
                Animator.Play("Classic" + PlayerAnimator.m_CurrentAnimationName);
            }
        }
    }
}
