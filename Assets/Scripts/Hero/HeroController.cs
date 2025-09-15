using UnityEngine;
using System.Collections;
using GameCore;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroController : MonoBehaviour
{
    [Header("Stats")]
    public int MaxHealth = 100;
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private int _specialDamage = 30;
    [SerializeField] private float _specialCooldown = 12f;
    [SerializeField] private float _respawnTime = 5f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private LayerMask _enemyMask;

    private Rigidbody2D _rb;
    private int _currentHealth;
    private float _lastAttackTime;
    private float _lastSpecialTime;
    private bool _isDead = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHealth = MaxHealth;
    }

    private void Update()
    {
        if (_isDead || GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        HandleMovement();
        HandleAttacks();
    }

    private void HandleMovement()
    {
        Vector2 move = GameInputManager.Instance.GetMovement();
        _rb.linearVelocity = move * _moveSpeed;
    }

    private void HandleAttacks()
    {
        if (GameInputManager.Instance.IsPrimaryAttack() && Time.time > _lastAttackTime + _attackCooldown)
        {
            _lastAttackTime = Time.time;
            DoBasicAttack();
        }

        if (GameInputManager.Instance.IsSpecialAttack() && Time.time > _lastSpecialTime + _specialCooldown)
        {
            _lastSpecialTime = Time.time;
            DoSpecialAttack();
            UIManager.Instance.ShowSpecialCooldown(_specialCooldown);
        }
    }

    private void DoBasicAttack()
    {
        var hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyMask);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy) enemy.TakeDamage(_attackDamage);
        }
    }

    private void DoSpecialAttack()
    {
        var hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange * 2.2f, _enemyMask);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(_specialDamage);
                enemy.Push((enemy.transform.position - transform.position).normalized * 3f);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (_isDead) return;
        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        UIManager.Instance.UpdateHeroHealth(_currentHealth, MaxHealth);
        if (_currentHealth <= 0) StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        _isDead = true;
        UIManager.Instance.ShowHeroDead(true);
        ResourceManager.Instance.TrySpendGold(20); 
        _rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(_respawnTime);
        _currentHealth = MaxHealth;
        gameObject.SetActive(true);
        UIManager.Instance.ShowHeroDead(false);
        UIManager.Instance.UpdateHeroHealth(_currentHealth, MaxHealth);
        _isDead = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange * 2.2f);
    }
}