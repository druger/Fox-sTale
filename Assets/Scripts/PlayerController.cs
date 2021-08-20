using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private const float SpeedXMultiplier = 50f;

    [SerializeField] private float speedX = 5.0f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float _knockBackLength = .25f;
    [SerializeField] private float _knockBackForce = 5f;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private float _horizontalInput;
    private float _knockBackCounter;
    private bool _isJump;
    private bool _canDoubleJump;
    private bool _onGround;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (_knockBackCounter <= 0) {
            _horizontalInput = Input.GetAxis("Horizontal");

            _onGround = Physics2D.OverlapCircle(groundPoint.position, .2f, groundMask);

            if (Input.GetButtonDown("Jump")) {
                if (_onGround) {
                    _isJump = true;
                    _canDoubleJump = true;
                } else if (_canDoubleJump) {
                    _canDoubleJump = false;
                    _isJump = true;
                }
            }

            Flip();
        } else {
            _knockBackCounter -= Time.deltaTime;
        }

        SetupAnimations();
    }

    public void KnockBack() {
        _knockBackCounter = _knockBackLength;
        _rb.velocity = new Vector2(0f, _knockBackForce);
        _animator.SetTrigger("hurt");
    }

    private void Flip() {
        if (_horizontalInput < 0 && !_spriteRenderer.flipX) _spriteRenderer.flipX = true;
        else if (_horizontalInput > 0 && _spriteRenderer.flipX) _spriteRenderer.flipX = false;
    }

    private void SetupAnimations() {
        _animator.SetBool("onGround", _onGround);
        _animator.SetFloat("moveSpeed", Math.Abs(_rb.velocity.x));
    }

    private void FixedUpdate() {
        if (_knockBackCounter <= 0) {
            _rb.velocity = new Vector2(speedX * SpeedXMultiplier * _horizontalInput * Time.fixedDeltaTime,
                _rb.velocity.y);
            if (_isJump) {
                _isJump = false;
                Jump();
            }
        } else {
            if (_spriteRenderer.flipX) _rb.velocity = new Vector2(_knockBackForce, _rb.velocity.y);
            else _rb.velocity = new Vector2(-_knockBackForce, _rb.velocity.y);
        }
    }

    private void Jump() {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}