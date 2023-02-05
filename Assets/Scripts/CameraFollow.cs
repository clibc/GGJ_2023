using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] float Speed = 20;
    [SerializeField] float Size = 2;
    [SerializeField] float FocusTime = 1;

    Transform PlayerT;
    IEnumerator Start()
    {
        float InitSize = Cam.orthographicSize;
        PlayerT = GameRef.Instance.PlayerCollect.transform;
        float TimeS = FocusTime;
        while(TimeS > 0)
        {
            TimeS -= Time.deltaTime;
            Cam.orthographicSize = Mathf.Lerp(Size, InitSize, TimeS / 2);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    void Update()
    {
        Vector3 Pos = PlayerT.position;
        Pos.z = Cam.transform.position.z;
        Cam.transform.position = Vector3.Lerp(Cam.transform.position, Pos, Speed * Time.deltaTime);
    }
}
