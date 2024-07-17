using UnityEngine;

public abstract class EnemyAbstractFactory
{
    protected abstract T CreateEnemy<T>(T enemyType, Vector3 startPosition, EnemyConfigurations enemyConfig) where T: EnemyPattern;
}