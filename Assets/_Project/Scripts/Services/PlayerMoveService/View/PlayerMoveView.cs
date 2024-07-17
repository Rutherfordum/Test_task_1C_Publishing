using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PlayerMoveView : MonoBehaviour, IPlayerMoveView
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    public Transform PlayerTransform => transform;

    public void SetMoveAnimation(bool value)
    {
        _animator.SetBool("Move",value);
    }

    public void SetFlipX(float value)
    {
        if (value<0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (value > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}

public interface IPlayerMoveView
{
    public Transform PlayerTransform { get; }
    public void SetMoveAnimation(bool value);
    public void SetFlipX(float value);
}
