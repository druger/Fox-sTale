using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int currentPoint;

    [SerializeField] private Transform platform;

    void Update() {
        platform.position = Vector3.MoveTowards(
            platform.position,
            points[currentPoint].position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(platform.position, points[currentPoint].position) < .5f) {
            currentPoint++;
        }

        if (currentPoint >= points.Length) currentPoint = 0;
    }
}