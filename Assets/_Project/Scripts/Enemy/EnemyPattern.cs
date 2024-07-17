using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class EnemyPattern : MonoBehaviour
{
    protected const float COEF_MOVE_DOWN = -10f;
    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _damage;
    [SerializeField] protected Vector3 _startPosition;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().simulated = true;
    }

    public void Initialize(int health, float moveSpeed, int damage, Vector3 startPosition)
    {
        _health = health;
        _moveSpeed = moveSpeed;
        _damage = damage;
        _startPosition = startPosition;
    }

    public abstract void MoveDown();

    public abstract void ApplyDamage(int damage);

    protected abstract void OnTriggerEnter2D(Collider2D collider);
}