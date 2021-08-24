using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime;
    [SerializeField] private float waitTime;

    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _movingRight = true;
    private float _moveCount;
    private float _waitCount;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        leftPoint.parent = null;
        rightPoint.parent = null;
        _moveCount = moveTime;
    }

    void Update() {
        if (_moveCount > 0) {
            Move();
        } else if (_waitCount > 0) {
            Wait();
        }
    }

    private void Wait() {
        _waitCount -= Time.deltaTime;
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        if (_waitCount <= 0) _moveCount = Random.Range(moveTime * .75f, waitTime * .75f);
        _animator.SetBool("isMoving", false);
    }

    private void Move() {
        _moveCount -= Time.deltaTime;
        if (_movingRight) {
            _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);
            spriteRenderer.flipX = true;
            if (transform.position.x > rightPoint.position.x) _movingRight = false;
        } else {
            _rb.velocity = new Vector2(-moveSpeed, _rb.velocity.y);
            spriteRenderer.flipX = false;
            if (transform.position.x < leftPoint.position.x) _movingRight = true;
        }

        if (_moveCount <= 0) _waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
        _animator.SetBool("isMoving", true);
    }
}