using UnityEngine;

public class MagEnemyFactory: EnemyAbstractFactory
{
    private const string _redMagEnemyPath = "Enemies/RedMagEnemy";
    private const string _greenMagEnemyPath = "Enemies/GreenMagEnemy";
    private const string _blueMagEnemyPath = "Enemies/BlueMagEnemy";

    private string _enemyPath;

    public RedMagEnemy CreateRedMagEnemy(Vector3 startPosition, EnemyConfigurations enemyConfig)
    {
        _enemyPath = _redMagEnemyPath;
        return CreateEnemy(new RedMagEnemy(), startPosition, enemyConfig);
    }

    public GreenMagEnemy CreateGreenMagEnemy(Vector3 startPosition, EnemyConfigurations enemyConfig)
    {
        _enemyPath = _greenMagEnemyPath;
        return CreateEnemy(new GreenMagEnemy(), startPosition, enemyConfig);
    }

    public BlueMagEnemy CreateBlueMagEnemy(Vector3 startPosition, EnemyConfigurations enemyConfig)
    {
        _enemyPath = _blueMagEnemyPath;
        return CreateEnemy(new BlueMagEnemy(), startPosition, enemyConfig);
    }

    protected override T CreateEnemy<T>(T enemyType, Vector3 startPosition, EnemyConfigurations enemyConfig)
    {
        var speed = enemyConfig.Speed;
        var prefab = Resources.Load<GameObject>(_enemyPath);
        prefab.SetActive(false);
        var go = GameObject.Instantiate(prefab);
        var enemy = go.GetComponent<T>();
        enemy.Initialize(enemyConfig.Health, speed, enemyConfig.Damage, startPosition);
        return enemy;
    }
}