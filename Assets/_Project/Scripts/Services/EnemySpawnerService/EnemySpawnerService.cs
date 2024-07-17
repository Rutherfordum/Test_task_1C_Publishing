using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawnerService: IInitializable, IDisposable, IEnemySpawnerService
{
    private List<Transform> _spawnPoints;
    private EnemyConfigurations _enemyConfig;
    private EnemySpawnerConfigurations _enemyAutoSpawnerConfig;

    private MagEnemyFactory _magEnemyFactory;
    private List<MagEnemyPattern> _magEnemies;
    private CancellationTokenSource _cancellationTokenSource;
    private int _enemyCount;

    public EnemySpawnerService(
        IEnumerable<Transform> spawnPoints,
        EnemySpawnerConfigurations enemyAutoSpawnerConfig, 
        EnemyConfigurations enemyConfig,
        EnemyAbstractFactory enemyFactory)
    {
        _spawnPoints = spawnPoints.ToList();
        _enemyConfig = enemyConfig;
        _enemyAutoSpawnerConfig = enemyAutoSpawnerConfig;
        _magEnemyFactory = enemyFactory as MagEnemyFactory;
    }

    public bool isStarted { get; private set; }
    public List<EnemyPattern> Enemies => _magEnemies.OfType<EnemyPattern>().ToList();

    public void Initialize()
    {
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }

    public void StartAutoSpawn()
    {
        _enemyCount = _enemyAutoSpawnerConfig.EnemyCount;
        _magEnemies = PreLoadEnemies(_enemyCount).ToList();
        AutoSpawnWithRandomTime(_cancellationTokenSource.Token).Forget();
        isStarted = true;
    }

    public void StopAutoSpawn()
    {
        DestroyMagEnemies();
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private async UniTaskVoid AutoSpawnWithRandomTime(CancellationToken cancellationToken)
    {
        foreach (var enemy in _magEnemies)
        {
            var time = _enemyAutoSpawnerConfig.SpawnTime;
            await UniTask.WaitForSeconds(time, cancellationToken: cancellationToken);
            enemy.gameObject.SetActive(true);
        }
    }

    private IEnumerable<MagEnemyPattern> PreLoadEnemies(int enemyCount)
    {
        List<MagEnemyPattern> magEnemies = new List<MagEnemyPattern>();

        for (int i = 0; i < enemyCount; i++)
        {
            var startPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
            var redMag = _magEnemyFactory.CreateGreenMagEnemy(startPosition, _enemyConfig);
            magEnemies.Add(redMag);
        }

        return magEnemies;
    }

    private void DestroyMagEnemies()
    {
        if(_magEnemies == null)
            return;

        var _enemyPatterns = _magEnemies.Where(item => item != null).ToList();
        _enemyPatterns.ForEach(pattern => MonoBehaviour.Destroy(pattern.gameObject));

        _magEnemies.Clear();
        _magEnemies = null;
    }
}

public interface IEnemySpawnerService
{
    public bool isStarted { get; }
    public List<EnemyPattern> Enemies { get; }
    public void StartAutoSpawn();
    public void StopAutoSpawn();
}