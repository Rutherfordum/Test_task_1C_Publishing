using UnityEngine;

[CreateAssetMenu(fileName = "new BulletConfigurations", menuName = "TestTask/Configurations/BulletConfigurations")]
public class BulletConfigurations : ScriptableObject
{
    [SerializeField] private float _damageRadius;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    public float DamageRadius => _damageRadius;
    public float Speed => _speed;
    public int Damage => _damage;
}