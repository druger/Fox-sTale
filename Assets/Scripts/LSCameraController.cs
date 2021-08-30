using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour {
    [SerializeField] private Vector2 minPos;
    [SerializeField] private Vector2 maxPos;
    [SerializeField] private Transform target;

    private void LateUpdate() {
        var xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x);
        var yPos = Mathf.Clamp(target.position.y, minPos.y, maxPos.y);
        var zPos = transform.position.z;

        transform.position = new Vector3(xPos, yPos, zPos);
    }
}