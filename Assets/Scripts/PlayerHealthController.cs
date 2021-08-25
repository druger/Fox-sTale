using UnityEngine;

public class PlayerHealthController : MonoBehaviour {
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject deathEffect;

    public int maxHealth = 6;

    private SpriteRenderer _spriteRenderer;
    private PlayerController _playerController;

    private int _currentHealth;
    private float _invinsibleCounter;
    private float _invinsibleLength = 1;

    public int CurrentHealth {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
        _currentHealth = maxHealth;
    }

    private void Update() {
        if (_invinsibleCounter > 0) {
            _invinsibleCounter -= Time.deltaTime;
            if (_invinsibleCounter <= 0) ChangePlayerTransparency(1f);
        }
    }

    public void DealDamage() {
        if (_invinsibleCounter <= 0) {
            _currentHealth--;
            if (_currentHealth <= 0) {
                _currentHealth = 0;
                Instantiate(deathEffect, transform.position, transform.rotation);
                levelManager.RespawnPlayer();
            } else {
                _invinsibleCounter = _invinsibleLength;
                ChangePlayerTransparency(.5f);
                _playerController.KnockBack();
                audioManager.PlaySFX(9);
            }

            uiController.ReduceHealth();
            audioManager.PlaySFX(9);
        }
    }

    public void HealPlayer() {
        _currentHealth++;
        if (_currentHealth > maxHealth) _currentHealth = maxHealth;
        uiController.IncreaseHealth();
    }

    private void ChangePlayerTransparency(float alfa) {
        var color = _spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, alfa);
        _spriteRenderer.color = color;
    }
}