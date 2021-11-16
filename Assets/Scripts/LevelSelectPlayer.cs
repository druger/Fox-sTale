using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class LevelSelectPlayer : MonoBehaviour {
    [SerializeField] private LSManager lsManager;
    [SerializeField] private MapPoint currentPoint;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private float moveSpeed = 10f;

    private MapPoint _selectedPoint;
    private bool _levelLoading;
    private bool _isSelectPoint;

    public MapPoint CurrentPoint {
        set => currentPoint = value;
    }

    void Update() {
        if (_isSelectPoint) MovePlayer();
        ChangePosition();
        OnMovement();
        OnTouch();
        OnMouseClick();
        OnPlayClick();
    }

    private void ChangePosition() {
        var oldPosition = transform.position;
        transform.position = Vector3.MoveTowards(
            oldPosition,
            currentPoint.transform.position,
            moveSpeed * Time.deltaTime
        );
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

    public void OnMovement() {
        if (CanMove()) {
            if (Input.GetAxisRaw("Horizontal")  > .5f) {
                MoveToRight();
            } else if (Input.GetAxisRaw("Horizontal")  < -.5f) {
                MoveToLeft();
            } else if (Input.GetAxisRaw("Vertical") > .5f) {
                MoveToUp();
            } else if (Input.GetAxisRaw("Vertical") < -.5f) {
                MoveToDown();
            }
        }
    }

    private Boolean CanMove() {
        return Vector3.Distance(transform.position, currentPoint.transform.position) < .1f && !_levelLoading;
    }

    private void OnPlayClick() {
        if (CanMove() && IsLevel() && IsLevelExists() && IsLevelNotLocked() && Input.GetKeyDown(KeyCode.Space)) {
            _levelLoading = true;
            lsManager.LoadLevel(currentPoint.LevelToLoad);
        }
    }

    private void OnTouch() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            SetSelectedPoint(Input.touches[0].position);
        }
    }

    public void OnMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
            SetSelectedPoint(Mouse.current.position.ReadValue());
        }
    }

    private void SetSelectedPoint(Vector3 position) {
        Vector2 origin = Camera.main.ScreenToWorldPoint(position);
        var hit = Physics2D.Raycast(origin, Vector2.zero);
        if (hit) {
            _selectedPoint = hit.transform.gameObject.GetComponent<MapPoint>();
            _isSelectPoint = true;
        }
    }

    private void MovePlayer() {
        Debug.Log("start moving");
        if (_selectedPoint == currentPoint) {
            _isSelectPoint = false;
            Debug.Log("end moving");
            return;
        }
        if (CanMove()) SetNextPoint(_selectedPoint.ID > currentPoint.ID ? currentPoint.Next : currentPoint.Prev);
    }


    private void MoveToRight() {
        if (currentPoint.Right != null) {
            SetNextPoint(currentPoint.Right);
        }
    }

    private void MoveToLeft() {
        if (currentPoint.Left != null) {
            SetNextPoint(currentPoint.Left);
        }
    }

    private void MoveToUp() {
        if (currentPoint.Up != null) {
            SetNextPoint(currentPoint.Up);
        }
    }

    private void MoveToDown() {
        if (currentPoint.Down != null) {
            SetNextPoint(currentPoint.Down);
        }
    }

    private void SetNextPoint(MapPoint next) {
        currentPoint = next;
        audioManager.PlaySFX(5);
    }
}