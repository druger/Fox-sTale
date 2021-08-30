using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayer : MonoBehaviour {
    [SerializeField] private MapPoint currentPoint;
    [SerializeField] private float moveSpeed = 10f;
    
    void Update() {
        var oldPosition = transform.position;
        transform.position = Vector3.MoveTowards(oldPosition, currentPoint.transform.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentPoint.transform.position) < .1f) {
            MoveToRight();
            MoveToLeft();
            MoveToUp();
            MoveToDown();
        }
    }

    private void MoveToRight() {
        if (Input.GetAxisRaw("Horizontal") > .5f) {
            if (currentPoint.Right != null) {
                SetNextPoint(currentPoint.Right);
            }
        }
    }
    
    private void MoveToLeft() {
        if (Input.GetAxisRaw("Horizontal") < -.5f) {
            if (currentPoint.Left != null) {
                SetNextPoint(currentPoint.Left);
            }
        }
    }
    
    private void MoveToUp() {
        if (Input.GetAxisRaw("Vertical") > .5f) {
            if (currentPoint.Up != null) {
                SetNextPoint(currentPoint.Up);
            }
        }
    }
    
    private void MoveToDown() {
        if (Input.GetAxisRaw("Vertical") < -.5f) {
            if (currentPoint.Down != null) {
                SetNextPoint(currentPoint.Down);
            }
        }
    }

    private void SetNextPoint(MapPoint next) {
        currentPoint = next;
    }
}