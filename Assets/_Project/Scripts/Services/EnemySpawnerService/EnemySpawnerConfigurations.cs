using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyAutoSpawnerConfigurations", menuName = "TestTask/Configurations/EnemyAutoSpawnerConfigurations")]
public class EnemySpawnerConfigurations: ScriptableObject
{
    public float SpawnTime => Random.Range(_spawnTimeMin, _spawnTimeMax);
    public int EnemyCount => Random.Range(_enemyCountMin, _enemyCountMax);

    [SerializeField] float _spawnTimeMax;
    [SerializeField] float _spawnTimeMin;

    [SerializeField] int _enemyCountMax;
    [SerializeField] int _enemyCountMin;
}