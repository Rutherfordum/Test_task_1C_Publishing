using UnityEngine;
using Zenject;

public class PlayerMoveService : IFixedTickable
{
    private IPlayerMoveView _playerView;
    private PlayerMoveConfigurations _moveConfig;
    private IUserInputMove _userInputService;
    private ScreenSizeService _screenSizeService;

    public PlayerMoveService(
        IPlayerMoveView playerMoveView,
        PlayerMoveConfigurations configurations,
        IUserInputMove userInputMove,
        ScreenSizeService screenSizeService)
    {
        _playerView = playerMoveView;
        _moveConfig = configurations;
        _userInputService = userInputMove;
        _screenSizeService = screenSizeService;
    }

    public void FixedTick()
    {
        Move();
    }

    private void Move()
    {
        _playerView.SetMoveAnimation(_userInputService.IsMove);
        _playerView.SetFlipX(_userInputService.MoveDirection.x);

        if (!_userInputService.IsMove)
            return;

        Vector3 futurePosition = _userInputService.MoveDirection * _moveConfig.Speed * Time.fixedDeltaTime;

        if (CheckPlayerInScreenZone(futurePosition + _playerView.PlayerTransform.position))
            _playerView.PlayerTransform.Translate(futurePosition);

        _playerView.PlayerTransform.Translate(futurePosition);
    }

    private bool CheckPlayerInScreenZone(Vector3 playerPosition)
    {
        if (playerPosition.x < _screenSizeService.ScreenSizeMin.x)
        {
            _playerView.PlayerTransform.position = new Vector3(_screenSizeService.ScreenSizeMin.x, _playerView.PlayerTransform.position.y, _playerView.PlayerTransform.position.z);
            return false;
        }

        if (playerPosition.x > _screenSizeService.ScreenSizeMax.x)
        {
            _playerView.PlayerTransform.position = new Vector3(_screenSizeService.ScreenSizeMax.x, _playerView.PlayerTransform.position.y, _playerView.PlayerTransform.position.z);
            return false;
        }

        if (playerPosition.y < _screenSizeService.ScreenSizeMin.y)
        {
            _playerView.PlayerTransform.position = new Vector3(_playerView.PlayerTransform.position.x, _screenSizeService.ScreenSizeMin.y, _playerView.PlayerTransform.position.z);
            return false;
        }

        if (playerPosition.y > _screenSizeService.FinishLinePosition.y)
        {
            _playerView.PlayerTransform.position = new Vector3(_playerView.PlayerTransform.position.x, _screenSizeService.FinishLinePosition.y, _playerView.PlayerTransform.position.z);
            return false;
        }

        return true;
    }
}