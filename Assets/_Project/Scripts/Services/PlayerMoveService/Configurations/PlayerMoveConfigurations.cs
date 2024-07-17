using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerMoveConfigurations", menuName = "TestTask/Configurations/PlayerMoveConfigurations")]
public class PlayerMoveConfigurations : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed => _speed;
}
