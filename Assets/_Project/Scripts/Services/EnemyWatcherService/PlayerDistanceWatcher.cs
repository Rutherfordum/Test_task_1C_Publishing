using System.Linq;
using UnityEngine;

public class PlayerDistanceWatcher : MonoBehaviour, IPlayerDistanceWatcher
{
    public Vector2 Position => transform.position;

#if UNITY_EDITOR

    private PlayerDistanceWatcherConfigurations _distanceWatcherConfig;

    private void OnValidate()
    {
        _distanceWatcherConfig = Resources.LoadAll<PlayerDistanceWatcherConfigurations>("").First();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceWatcherConfig.DistanceWatcher);
    }
#endif

}

public interface IPlayerDistanceWatcher
{
    public Vector2 Position { get; }
}

