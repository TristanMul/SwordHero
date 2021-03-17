using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
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

    private void LateUpdate()
    {
        if (FollowTarget != null)
            transform.position = Vector3.Lerp(transform.position, FollowTarget.position + TargetOffset, MoveSpeed * Time.deltaTime);
    }
}
