using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour {

    [SerializeField] private Transform boss;
    [SerializeField] private Animator animator;

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
    }

    void Update() {
    }
}