using System;
using TMPro;
using UnityEngine;
using Zenject;

public class TowerService: IInitializable, IDisposable
{
    private readonly ITower _tower;
    private readonly VictoryService _victoryService;

    public TowerService(ITower tower, VictoryService victoryService)
    {
        _tower = tower;
        _victoryService = victoryService;
    }

    public void Initialize()
    {
        _tower.OnHealthIsZeroOrLess += OnHealthIsZeroOrLess;
    }
    public void Dispose()
    {
        _tower.OnHealthIsZeroOrLess -= OnHealthIsZeroOrLess;
    }

    private void OnHealthIsZeroOrLess()
    {
        _victoryService.Victory?.Invoke();
    }
  
    public void ResetHealth()
    {
        _tower.Initialize();
    }
}

[RequireComponent(typeof(Collider2D))]
public class Tower : MonoBehaviour, ITower
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private int _healthMax;
    private int _health;

    public Action OnHealthIsZeroOrLess { get; set; }

    public void Initialize()
    {
        _health = _healthMax;
        _healthText.text = _health.ToString();
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            OnHealthIsZeroOrLess?.Invoke();
        }

        _healthText.text = _health.ToString();
    }
}

public interface ITower
{
    public Action OnHealthIsZeroOrLess { get; set; }
    public void Initialize();
    public void ApplyDamage(int damage);
}
