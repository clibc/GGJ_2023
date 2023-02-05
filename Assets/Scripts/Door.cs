using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] Sprite ActivatedBoneSprite;
    [SerializeField] SpriteRenderer[] Bones;
    
    public void OpenDoor()
    {
        if(GameRef.Instance.PlayerCollect.BoneCount == 4)
        {
            Anim.Play("DoorOpen");
        }
    }

    public void UpdateBones()
    {
        for(int I = 0; I < GameRef.Instance.PlayerCollect.BoneCount; ++I)
        {
            Bones[I].sprite = ActivatedBoneSprite;
        }
    }
}