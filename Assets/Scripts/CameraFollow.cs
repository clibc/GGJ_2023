using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float Speed = 20;
    [SerializeField] float Size = 2;
    [SerializeField] float FocusTime = 1;

    bool IsFocusing = false;

    Coroutine FocusCoroutine = null;

    Camera Cam;
    Transform PlayerT;
    IEnumerator Start()
    {
        Cam = GetComponent<Camera>();

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
        if(IsFocusing)
            return;

        Vector3 Pos = PlayerT.position;
        Pos.z = Cam.transform.position.z;
        Cam.transform.position = Vector3.Lerp(Cam.transform.position, Pos, Speed * Time.deltaTime);
    }

    public void Focus(Transform FocusObj)
    {
        if(FocusCoroutine != null)
            return;
        StartCoroutine(FocusInternal(FocusObj));
    }

    IEnumerator FocusInternal(Transform FocusObj)
    {
        IsFocusing = true;

        const float FocusTimeMax = 1f;
        float FocusTime = FocusTimeMax;
        Vector3 FocusPos = FocusObj.transform.position;
        FocusPos.z = Cam.transform.position.z;

        while (FocusTime > 0)
        {
            FocusTime -= Time.deltaTime;
            Cam.transform.position = Vector3.Lerp(Cam.transform.position, FocusPos, 1.0f - FocusTime / FocusTimeMax);
            yield return new WaitForEndOfFrame();
        }

        IsFocusing = false;
        FocusCoroutine = null;
    }
}
