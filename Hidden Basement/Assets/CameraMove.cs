using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform camGFX;

    bool startNextRotation = true;
    public bool rotRight;

    public float yaw;
    public float pitch;

    public float secondsToRot;

    public float rotSwitchTime;

    // Start is called before the first frame update
    void Start()
    {
        camGFX = transform.GetChild(0);
        camGFX.localRotation = Quaternion.AngleAxis(pitch, Vector3.right);

        SetUpStartRotation();
    }

    void Update()
    {
        if(startNextRotation && rotRight)
        {
            StartCoroutine(Rotate(yaw, secondsToRot));
        }
        else if(startNextRotation && !rotRight)
        {
            StartCoroutine(Rotate(-yaw, secondsToRot));
        }
    }

    // Update is called once per frame
    IEnumerator Rotate(float yaw, float duration)
    {
        startNextRotation = false;

        Quaternion initialRotation = transform.rotation;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = initialRotation * Quaternion.AngleAxis(timer / duration * yaw, Vector3.up);
            yield return null;
        }

        yield return new WaitForSeconds(rotSwitchTime);

        startNextRotation = true;
        rotRight = !rotRight;
    }

    void SetUpStartRotation()
    {
        if(rotRight)
        {
            transform.localRotation = Quaternion.AngleAxis(-yaw / 2, Vector3.up);
        }
        else
        {
            transform.localRotation = Quaternion.AngleAxis(yaw / 2, Vector3.up);
        }
    }
}
