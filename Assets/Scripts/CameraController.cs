using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform farBg;
    [SerializeField] private Transform middleBg;

    private Vector2 _lastPos;
    private bool _stopFollow;

    public bool StopFollow {
        set => _stopFollow = value;
    }

    private void Start() {
        _lastPos = transform.position;
    }

    void Update() {
        if (!_stopFollow) {
            Vector2 amountToMove = new Vector2(
                transform.position.x - _lastPos.x,
                transform.position.y - _lastPos.y);

            farBg.position = farBg.position + new Vector3(amountToMove.x, amountToMove.y);
            middleBg.position += new Vector3(amountToMove.x, amountToMove.y) * .5f;

            _lastPos = transform.position;
        }
    }
}