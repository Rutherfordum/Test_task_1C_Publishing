using UnityEngine;

[CreateAssetMenu(fileName = "new ScreenSizeConfigurations", menuName = "TestTask/Configurations/ScreenSizeConfigurations")]
public class ScreenSizeConfigurations : ScriptableObject
{
    [SerializeField] private Vector2 _offsetScreenSize;
    [SerializeField] private Vector2 _finishLinePosition;

    public Vector2 OffsetScreenSize => _offsetScreenSize;
    public Vector2 FinishLinePosition => _finishLinePosition;
}
