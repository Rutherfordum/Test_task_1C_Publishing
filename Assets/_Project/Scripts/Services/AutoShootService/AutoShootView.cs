using UnityEngine;

public class AutoShootView : MonoBehaviour, IAutoShootView
{
    public Transform BulletSpawnPosition => transform;
}

public interface IAutoShootView
{
    public Transform BulletSpawnPosition { get; }
}