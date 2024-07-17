using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerDistanceWatcherConfigurations", menuName = "TestTask/Configurations/PlayerDistanceWatcherConfigurations")]
public class PlayerDistanceWatcherConfigurations : ScriptableObject
{
    [SerializeField] private float _distanceWatcher;
    public float DistanceWatcher => _distanceWatcher;
}