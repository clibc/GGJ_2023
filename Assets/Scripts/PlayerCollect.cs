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
        }
        else if(Col.gameObject.CompareTag("Door"))
        {
            Col.GetComponent<Door>().OpenDoor();
        }
        else if(Col.gameObject.CompareTag("Zombie"))
        {
            SceneManager.LoadScene(0);
        }
        else if (Col.gameObject.CompareTag("DeadBlock"))
        {
            Debug.Log("DeadBlock");
            SceneManager.LoadScene(0);
        }
    }
}