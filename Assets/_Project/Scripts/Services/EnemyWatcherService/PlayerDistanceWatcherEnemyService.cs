using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerDistanceWatcherEnemyService : ITickable, IDistanceWatcherEnemyService
{
    private readonly IPlayerDistanceWatcher _playerDistanceWatcher;
    private readonly IEnemySpawnerService _enemySpawnerService;
    private readonly PlayerDistanceWatcherConfigurations _distanceWatcherConfig;
    private List<EnemyPattern> _enemyPatterns;
    private bool _isActive;

    public bool SeeEnemy { get; private set; }

    public PlayerDistanceWatcherEnemyService(
        IPlayerDistanceWatcher playerDistanceWatcher,
        IEnemySpawnerService enemySpawnerService,
        PlayerDistanceWatcherConfigurations distanceWatcherConfig)
    {
        _playerDistanceWatcher = playerDistanceWatcher;
        _enemySpawnerService = enemySpawnerService;
        _distanceWatcherConfig = distanceWatcherConfig;
    }

    public void StartWatch()
    {
        _isActive = true;
    }

    public void StopWatch()
    {
        _isActive = false;
    }

    public void Tick()
    {
        if (!_isActive)
            return;

        _enemyPatterns = new List<EnemyPattern>();
        _enemyPatterns = _enemySpawnerService.Enemies.Where(item => item != null).ToList();

        if (_enemyPatterns.Count > 0)
            _enemyPatterns.ForEach(pattern =>
            {
                var distance = Vector2.Distance(pattern.transform.position, _playerDistanceWatcher.Position);

                if (distance > _distanceWatcherConfig.DistanceWatcher)
                {
                    SeeEnemy = false;
                }
                else
                {
                    SeeEnemy = true;
                }
            });
    }
}

public interface IDistanceWatcherEnemyService
{
    public void StartWatch();
    public void StopWatch();

    public bool SeeEnemy { get; }
}