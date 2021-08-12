using UnityEngine;

public class PlayerController : MonoBehaviour {
    private const float SpeedXMultiplier = 50f;

    [SerializeField] private float speedX = 5.0f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody2D _rb;
    private float _horizontalInput;
    private bool _isJump;
    private bool _canDoubleJump;
    private bool _onGround;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
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
    }

    private void FixedUpdate() {
        _rb.velocity = new Vector2(speedX * SpeedXMultiplier * _horizontalInput * Time.fixedDeltaTime, _rb.velocity.y);
        if (_isJump) {
            _isJump = false;
            Jump();
        }
    }

    private void Jump() {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}