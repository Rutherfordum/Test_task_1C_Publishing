using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerAutoShootConfigurations", menuName = "TestTask/Configurations/PlayerAutoShootConfigurations")]
public class AutoShootConfigurations : ScriptableObject
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _speedShoot;

    public Bullet BulletPrefab =>_bulletPrefab;
    public float SpeedShoot => _speedShoot;
}