using UnityEngine;
using Zenject;

public class ScreenSizeService : IInitializable, IScreenSizeService
{
    private Camera _mainCamera;
    private ScreenSizeConfigurations _screenSizeConfig;
    private Vector2 _screenSize;

    public ScreenSizeService(
        Camera mainCamera,
        ScreenSizeConfigurations screenSizeConfigurations)
    {
        _mainCamera = mainCamera;
        _screenSizeConfig = screenSizeConfigurations;
    }

    public Vector2 ScreenSizeMax => _screenSize;

    public Vector2 ScreenSizeMin => new Vector2(-_screenSize.x, -_screenSize.y);

    public Vector3 FinishLinePosition => _screenSizeConfig.FinishLinePosition + _screenSizeConfig.OffsetScreenSize;

    public void Initialize()
    {
        CalculateScreenSizeWithOffset();
    }

    private void CalculateScreenSizeWithOffset()
    {
        _screenSize.x = Vector2.Distance(_mainCamera.ScreenToWorldPoint(new Vector2(0, 0)), _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        _screenSize.y = Vector2.Distance(_mainCamera.ScreenToWorldPoint(new Vector2(0, 0)), _mainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        _screenSize.x += _screenSizeConfig.OffsetScreenSize.x;
        _screenSize.y += _screenSizeConfig.OffsetScreenSize.y;
    }
}

public interface IScreenSizeService
{
    public Vector2 ScreenSizeMax { get; }
    public Vector2 ScreenSizeMin { get; }
    public Vector3 FinishLinePosition { get; }
}
