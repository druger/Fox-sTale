using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    private const float SpeedXMultiplier = 50f;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private float speedX = 5.0f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float bounceForce = 15f;
    [SerializeField] private float knockBackLength = .25f;
    [SerializeField] private float knockBackForce = 5f;
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
    private bool _stopInput;
    public bool StopInput { set => _stopInput = value; }

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (!pauseMenu.IsPaused && !_stopInput) {
            if (_knockBackCounter <= 0) {
                _onGround = Physics2D.OverlapCircle(groundPoint.position, .2f, groundMask);

                Flip();
            } else {
                _knockBackCounter -= Time.deltaTime;
            }
        }

        SetupAnimations();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            transform.parent = null;
        }
    }

    public void KnockBack() {
        _knockBackCounter = knockBackLength;
        _rb.velocity = new Vector2(0f, knockBackForce);
        _animator.SetTrigger("hurt");
    }

    public void Bounce() {
        _rb.velocity = new Vector2(_rb.velocity.x, bounceForce);
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
        if (!pauseMenu.IsPaused && !_stopInput) {
            if (_knockBackCounter <= 0) {
                _rb.velocity = new Vector2(speedX * SpeedXMultiplier * _horizontalInput * Time.fixedDeltaTime,
                    _rb.velocity.y);
                if (_isJump) {
                    _isJump = false;
                    Jump();
                }
            } else {
                if (_spriteRenderer.flipX) _rb.velocity = new Vector2(knockBackForce, _rb.velocity.y);
                else _rb.velocity = new Vector2(-knockBackForce, _rb.velocity.y);
            }
        }
    }

    private void Jump() {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        audioManager.PlaySFX(10);
    }

    public void OnMovement(InputAction.CallbackContext context) {
        var input = context.ReadValue<Vector2>();
        _horizontalInput = input.x;
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.started) {
            if (_onGround) {
                _isJump = true;
                _canDoubleJump = true;
            } else if (_canDoubleJump) {
                _canDoubleJump = false;
                _isJump = true;
            }
        }
    }

    public Rigidbody2D Rb {
        get => _rb;
    }
}