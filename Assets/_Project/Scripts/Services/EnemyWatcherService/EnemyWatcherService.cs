using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemyWatcherService : ITickable
{
    private IEnemySpawnerService _enemySpawnerService;
    private VictoryService _victoryService;
    private List<EnemyPattern> _enemyPatterns;
    private bool _isActive;

    public EnemyWatcherService(IEnemySpawnerService enemySpawnerService, VictoryService victoryService)
    {
        _enemySpawnerService = enemySpawnerService;
        _victoryService = victoryService;
    }

    public void StartWatch()
    {
        _isActive = true;
    }

    public void StopWatch()
    {
        _isActive = false;
        _enemyPatterns.Clear();
    }

    public void Tick()
    {
        if (!_isActive)
            return;

        _enemyPatterns = new List<EnemyPattern>();
        _enemyPatterns = _enemySpawnerService.Enemies.Where(item => item != null).ToList();

        if (_enemyPatterns.Count <= 0)
        {
            _victoryService.Victory.Invoke();
            _isActive = false;
        }
    }
}