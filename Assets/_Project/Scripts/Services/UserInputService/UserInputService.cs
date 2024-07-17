using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class UserInputService : IInitializable, IDisposable, IUserInput, IUserInputMove
{
    private UserInputControls _userInputControls;

    public bool IsMove { get; private set; }
    public Vector2 MoveDirection { get; private set; }

    public void Initialize()
    {
        _userInputControls = new UserInputControls();
        _userInputControls.Player.Move.performed += OnMove;
        _userInputControls.Player.Move.canceled += OnMove;
    }

    public void Dispose()
    {
        _userInputControls.Player.Move.performed -= OnMove;
        _userInputControls.Player.Move.canceled -= OnMove;
        _userInputControls.Dispose();
    }

    public void Enable()
    {
        _userInputControls.Enable();
    }

    public void Disable()
    {
        _userInputControls.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
        IsMove = MoveDirection.x != 0 || MoveDirection.y != 0;
    }
}

public interface IUserInputMove
{
    public bool IsMove { get; }

    public Vector2 MoveDirection { get; }
}

public interface IUserInput
{
    public void Enable();
    public void Disable();
}
