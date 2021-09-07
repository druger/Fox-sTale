using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour {
    [SerializeField] private Transform[] points;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float distanceToAttackPlayer;

    private Vector3 _attackTarget;
    private int _currentPoint;
    private bool _hasAttacked;
    private float _waitAfterAttack = 1f;
    private float _attackCounter;

    private void Start() {
        foreach (var point in points) {
            point.parent = null;
        }
    }

    void Update() {
        if (_attackCounter > 0) {
            _attackCounter -= Time.deltaTime;
        } else {
            if (Vector3.Distance(transform.position, playerController.transform.position) > distanceToAttackPlayer) {
                Move();
                Flip();
            } else {
                Attack();
            }
        }
    }

    private void Attack() {
        if (_attackTarget == Vector3.zero) {
            _attackTarget = playerController.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, _attackTarget, chaseSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _attackTarget) <= .1f) {
            _hasAttacked = true;
            _attackCounter = _waitAfterAttack;
            _attackTarget = Vector3.zero;
        }
    }

    private void Flip() {
        if (transform.position.x < points[_currentPoint].position.x) {
            spriteRenderer.flipX = true;
        } else if (transform.position.x > points[_currentPoint].position.x) {
            spriteRenderer.flipX = false;
        }
    }

    private void Move() {
        _attackTarget = Vector3.zero;
        transform.position = Vector3.MoveTowards(
            transform.position,
            points[_currentPoint].position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, points[_currentPoint].position) < .5f) {
            _currentPoint++;
        }

        if (_currentPoint >= points.Length) _currentPoint = 0;
    }
}