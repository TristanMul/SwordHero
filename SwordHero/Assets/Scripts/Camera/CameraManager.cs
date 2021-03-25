using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool doScreenShake;
    Vector3 shakeOffset;
    private Transform FollowTarget;
    private PlayerMovement playerMovement;
    [SerializeField] private Vector3 TargetOffset;
    [SerializeField] private float MoveSpeed = 3f;
    [SerializeField] private float moveDistanceMagnitude = 1f;
    private float moveDistanceMult = 1;
    [SerializeField] private float distanceLerpSpeed = 1f;
    private float distanceMult = 1f;

    private void Start()
    {
        FollowTarget = GameManager.instance._player.transform;
        playerMovement = FollowTarget.GetComponent<PlayerMovement>();
    }

    public void SetTarget(Transform aTransform)
    {
        FollowTarget = aTransform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeDistance(.5f);
        }
        transform.position += shakeOffset;

    }

    private void LateUpdate()
    {
        if (FollowTarget != null)
        {
            moveDistanceMult = 1f + (playerMovement.movement.magnitude * moveDistanceMagnitude);
            distanceMult = Mathf.Lerp(distanceMult, 1f, distanceLerpSpeed * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, FollowTarget.position + TargetOffset * moveDistanceMult * distanceMult, MoveSpeed * Time.deltaTime);
        }

    }

    /// <summary>
    /// Change the distance from the player
    /// </summary>
    /// <param name="magnitude">The scale of the distance, 1 is normal</param>
    public void ChangeDistance(float magnitude)
    {
        distanceMult = magnitude;
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
