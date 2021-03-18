using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool doScreenShake;
    Vector3 shakeOffset;
    private Transform FollowTarget;
    [SerializeField] private Vector3 TargetOffset;
    [SerializeField] private float MoveSpeed = 3f;

    private void Start()
    {
        FollowTarget = GameManager.instance._player.transform;
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
        transform.position += shakeOffset;

    }

    private void LateUpdate()
    {
        if (FollowTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, FollowTarget.position + TargetOffset , MoveSpeed * Time.deltaTime);
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
