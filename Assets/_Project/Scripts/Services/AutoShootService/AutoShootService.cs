using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Utils.ObjectPool;
using Zenject;

public class AutoShootService : IInitializable, IDisposable, IAutoShootService
{
    private const int BULLET_COUNT = 10;

    private IAutoShootView _autoShootView;
    private AutoShootConfigurations _autoShootConfig;
    private BulletConfigurations _bulletConfig;
    private readonly IDistanceWatcherEnemyService _distanceWatcherEnemyService;
    private ObjectPoolMono<Bullet> _objectPoolBullets;
    private CancellationTokenSource _cancellationTokenSource;
    private bool _isActiveShoot;

    public AutoShootService(
        IAutoShootView autoShootView,
        AutoShootConfigurations playerAutoShootConfig,
        BulletConfigurations bulletConfig,
        IDistanceWatcherEnemyService distanceWatcherEnemyService)
    {
        _autoShootView = autoShootView;
        _autoShootConfig = playerAutoShootConfig;
        _bulletConfig = bulletConfig;
        _distanceWatcherEnemyService = distanceWatcherEnemyService;
    }

    public void Initialize()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _objectPoolBullets = new ObjectPoolMono<Bullet>(_autoShootConfig.BulletPrefab, BULLET_COUNT, true);

        _objectPoolBullets.GetPool.ForEach(bullet =>
        {
            bullet.Initialize(_bulletConfig.Damage, _bulletConfig.Speed, _bulletConfig.DamageRadius);
        });
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }

    public void StartShoot()
    {
        _isActiveShoot = true;
        AutoShoot(_cancellationTokenSource.Token).Forget();
    }

    public void StopShoot()
    {
        _isActiveShoot = false;
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public async UniTaskVoid AutoShoot(CancellationToken cancellationToken)
    {
        while (_isActiveShoot)
        {
            var bullet = _objectPoolBullets.GetFreeElement();
            bullet.transform.position = _autoShootView.BulletSpawnPosition.position;
            await UniTask.WaitForSeconds(_autoShootConfig.SpeedShoot, cancellationToken: cancellationToken);
        }
    }
}

public interface IAutoShootService
{
    public void StartShoot();
    public void StopShoot();
}