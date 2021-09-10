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
    [SerializeField] private GameObject mine;
    [SerializeField] private Transform minePoint;
    [SerializeField] private float timeBetweenMines;
    [SerializeField] private float mineCounter;

    [Header("Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenShots;
    private float _shotCounter;

    [Header("Hurt")] 
    [SerializeField] private GameObject hitBox;
    [SerializeField] private float hurtTime;
    private float _hurtCounter;

    [Header("Health")]
    [SerializeField] private int health = 5;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float shotSpeedUp = 1.2f;
    [SerializeField] private float mineSpeedUp = 1.2f;
    private bool _isDefeated;

    void Start() {
        currentState = States.Shooting;
    }

    void Update() {
        switch (currentState) {
            case States.Shooting:
                _shotCounter -= Time.deltaTime;
                if (_shotCounter <= 0) {
                    _shotCounter = timeBetweenShots;
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = boss.localScale;
                }
                break;
            case States.Hurt:
                if (_hurtCounter > 0) {
                    _hurtCounter -= Time.deltaTime;

                    if (_hurtCounter <= 0) {
                        currentState = States.Moving;
                        mineCounter = 0f;
                        if (_isDefeated) {
                            boss.gameObject.SetActive(false);
                            Instantiate(explosion, boss.transform.position, boss.transform.rotation);
                            currentState = States.Defeated;
                        }
                    }
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

                mineCounter -= Time.deltaTime;
                if (mineCounter <= 0) {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }
                break;
        }
    }

    private void EndMovement() {
        currentState = States.Shooting;
        _shotCounter = 0f;
        hitBox.SetActive(true);
        animator.SetTrigger("stopMoving");
    }

    public void TakeHit() {
        currentState = States.Hurt;
        _hurtCounter = hurtTime;
        animator.SetTrigger("hit");
        ExplodeAllMInes();
        health--;
        if (health <= 0) {
            _isDefeated = true;
        } else {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    }

    private void ExplodeAllMInes() {
        var mines = FindObjectsOfType<TankMine>();
        if (mines.Length > 0) {
            foreach (var foundMine in mines) {
                foundMine.Explode();
            }
        }
    }

    private enum States {
        Shooting,
        Hurt,
        Moving,
        Defeated
    }
}