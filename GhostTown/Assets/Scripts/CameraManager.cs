using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform FollowTarget;
    public Vector3 TargetOffset;
    public float MoveSpeed = 3f;

    private Transform _myTransform;
    public bool doScreenShake;
    Vector3 shakeOffset;

    private void Start()
    {
        _myTransform = transform;
    }

    public void SetTarget(Transform aTransform)
    {
        FollowTarget = aTransform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(Shake(.2f, .05f));
        }
        _myTransform.position += shakeOffset;

    }

    private void LateUpdate()
    {
        if (FollowTarget != null)
        {
            _myTransform.position = Vector3.Lerp(_myTransform.position, FollowTarget.position + TargetOffset , MoveSpeed * Time.deltaTime);
        }

    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        if (doScreenShake)
        {
            Vector3 originalPos = transform.localPosition;

            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
                shakeOffset = new Vector3(x, y, 0f);

                elapsed += Time.deltaTime;
                yield return null;

            }

            shakeOffset = Vector3.zero;
        }
    }
}
