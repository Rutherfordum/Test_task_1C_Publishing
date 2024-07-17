using DG.Tweening;
using UnityEngine;

public class MagEnemyPattern : EnemyPattern
{
    private Tween _tween;

    private void OnEnable()
    {
        transform.position = _startPosition;

        if (_tween == null)
            MoveDown();

        _tween.Restart();
    }

    private void OnDisable()
    {
        if (_tween != null)
            _tween.Pause();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out ITower tower))
        {
            tower.ApplyDamage(_damage);
            Destroy(gameObject);
        }

        if (collider.TryGetComponent(out IBullet bullet))
        {
            bullet.Disable();
            ApplyDamage(bullet.Damage);
        }
    }

    public override void MoveDown()
    {
        _tween = transform.DOMoveY(_startPosition.y + COEF_MOVE_DOWN, _moveSpeed).SetEase(Ease.Linear).SetLink(gameObject);
    }

    public override void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
    }
}