using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _radiusDamage;

    public int Damage => _damage;
    public void Disable() => gameObject.SetActive(false);
    public void Enable() => gameObject.SetActive(true);

    public void Initialize(int damage, float speed, float radiusDamage)
    {
        _damage = damage;
        _speed = speed;
        _radiusDamage = radiusDamage;

        transform.localScale = new Vector3(radiusDamage, radiusDamage, radiusDamage);
    }

    private void FixedUpdate()
    {
        transform.position += Time.fixedDeltaTime * _speed * Vector3.up;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
public interface IBullet
{
    public int Damage { get; }

    public void Disable();
    public void Enable();
}