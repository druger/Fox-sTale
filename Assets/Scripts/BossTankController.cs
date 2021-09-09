using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour {
    [SerializeField] private Transform boss;
    [SerializeField] private Animator animator;
    [SerializeField] private States currentState;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private bool _moveRight;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;

    [Header("Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenShots;
    private float _shotCounter;
    
    [Header("Hurt")]
    [SerializeField] private float hurtTime;
    private float _hurtCounter;

    void Start() {
        currentState = States.Shooting;
    }

    void Update() {
        switch (currentState) {
            case States.Shooting:
                break;
            case States.Hurt:
                if (_hurtCounter > 0) {
                    _hurtCounter -= Time.deltaTime;

                    if (_hurtCounter <= 0) currentState = States.Moving;
                }

                break;
            case States.Moving:
                if (_moveRight) {
                    boss.position += new Vector3(moveSpeed * Time.deltaTime, 0f);
                    if (boss.position.x > rightPoint.position.x) {
                        _moveRight = false;
                        boss.localScale = Vector3.one;
                        EndMovement();
                    }
                } else {
                    boss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f);
                    if (boss.position.x < leftPoint.position.x) {
                        _moveRight = true;
                        boss.localScale = new Vector3(-1f, 1f, 1f);
                        EndMovement();
                    }
                }
                break;
        }
    }

    private void EndMovement() {
        currentState = States.Shooting;
        _shotCounter = timeBetweenShots;
        animator.SetTrigger("stopMoving");
    }

    private void TakeHit() {
        currentState = States.Hurt;
        _hurtCounter = hurtTime;
        animator.SetTrigger("hit");
    }

    private enum States {
        Shooting,
        Hurt,
        Moving
    }
}