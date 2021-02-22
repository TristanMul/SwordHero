using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform FollowTarget;
    public Vector3 TargetOffset;
    public float MoveSpeed = 3f;

    private Transform _myTransform;

    private void Start()
    {
        _myTransform = transform;
    }

    public void SetTarget(Transform aTransform)
    {
        FollowTarget = aTransform;
    }

    private void LateUpdate()
    {
        if (FollowTarget != null)
            _myTransform.position = Vector3.Lerp(_myTransform.position, FollowTarget.position + TargetOffset, MoveSpeed * Time.deltaTime);
    }
}
