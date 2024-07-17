using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyConfigurations", menuName = "TestTask/Configurations/EnemyConfigurations")]
public class EnemyConfigurations : ScriptableObject
{
    public float Speed => Random.Range(_maxSpeed, _minSpeed);
    public int Health => _health;
    public int Damage => _damage;

    [Range(5f,10f)]
    [SerializeField] float _maxSpeed;

    [Range(10f,20f)]
    [SerializeField] float _minSpeed;

    [Range(1, 100)]
    [SerializeField] int _health;

    [Range(1, 100)]
    [SerializeField] int _damage;
}
