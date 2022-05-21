using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void FixedUpdate()
    {
        if (target != null)
        {
            var targetPos = target.position + offset;

            transform.position = targetPos;
        }
    }
}
