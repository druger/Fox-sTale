using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayer : MonoBehaviour {
    [SerializeField] private LSManager lsManager;
    [SerializeField] private MapPoint currentPoint;
    [SerializeField] private float moveSpeed = 10f;

    private bool _levelLoading;
    
    public MapPoint CurrentPoint { set => currentPoint = value; }

    void Update() {
        var oldPosition = transform.position;
        transform.position = Vector3.MoveTowards(oldPosition, currentPoint.transform.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentPoint.transform.position) < .1f && !_levelLoading) {
            MoveToRight();
            MoveToLeft();
            MoveToUp();
            MoveToDown();
            SelectLevel();
        }
    }

    private void SelectLevel() {
        if (IsLevel() && IsLevelExists() && IsLevelNotLocked() && Input.GetButtonDown("Jump")) {
            _levelLoading = true;
            lsManager.LoadLevel(currentPoint.LevelToLoad);
        }
    }

    private bool IsLevelNotLocked() {
        return !currentPoint.IsLocked;
    }

    private bool IsLevelExists() {
        return currentPoint.LevelToLoad != null;
    }

    private bool IsLevel() {
        return currentPoint.IsLevel;
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