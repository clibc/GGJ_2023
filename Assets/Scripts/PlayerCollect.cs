using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour
{
    public int BoneCount = 0;

    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.CompareTag("Bone"))
        {
            BoneCount += 1;
            Destroy(Col.gameObject);
            GameRef.Instance.Door.UpdateBones();
            GameRef.Instance.Cam.Focus(GameRef.Instance.Door.transform);
            Audio.Play(audio_clip.Collect);
        }
        else if(Col.gameObject.CompareTag("Door"))
        {
            Col.GetComponent<Door>().EnterDoor();
        }
        else if(Col.gameObject.CompareTag("Zombie"))
        {
            SceneManager.LoadScene(Door.SceneCount % 2);
        }
        else if (Col.gameObject.CompareTag("DeadBlock"))
        {
            Debug.Log("DeadBlock");
            Audio.Play(audio_clip.Fail);
            SceneManager.LoadScene(Door.SceneCount % 2);
        }
    }
}