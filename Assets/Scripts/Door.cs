using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] Sprite ActivatedBoneSprite;
    [SerializeField] SpriteRenderer[] Bones;

    public static int SceneCount = 0;
    
    public void EnterDoor()
    {
        if(GameRef.Instance.PlayerCollect.BoneCount == 4)
        {
            SceneCount += 1;
            SceneManager.LoadScene(SceneCount % 2);
        }
    }

    public void UpdateBones()
    {
        for(int I = 0; I < GameRef.Instance.PlayerCollect.BoneCount; ++I)
        {
            Bones[I].sprite = ActivatedBoneSprite;
        }
		if(GameRef.Instance.PlayerCollect.BoneCount == 4)
		{
			Anim.Play("DoorOpen");
		}
    }
}