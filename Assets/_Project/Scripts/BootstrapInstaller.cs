using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [Space(10)]
    [SerializeField] private StartGameCanvasView _startGameCanvas;
    [SerializeField] private DefeatGameCanvasView _defeatGameCanvas;
    [SerializeField] private VictoryGameCanvasView _victoryGameCanvas;
    [SerializeField] private GamePlayCanvasView _gamePlayCanvas;

    [Space(10)]
    [SerializeField] private Camera _camera;
    [SerializeField] private ScreenSizeConfigurations _screenSizeConfig;
    
    [Space(10)]
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private EnemySpawnerConfigurations _enemySpawnerConfig;
    [SerializeField] private EnemyConfigurations _enemyConfig;
    
    [Space(10)]
    [SerializeField] private PlayerMoveView _playerMoveView;
    [SerializeField] private PlayerMoveConfigurations _playerMoveConfig;

    [Space(10)]
    [SerializeField] private AutoShootView _autoShootView;
    [SerializeField] private AutoShootConfigurations _autoShootConfig;
    [SerializeField] private BulletConfigurations _bulletConfig;

    [Space(10)]
    [SerializeField] private Tower _towerView;

    [Space(10)]
    [SerializeField] private PlayerDistanceWatcher _playerDistanceWatcher;
    [SerializeField] private PlayerDistanceWatcherConfigurations _distanceWatcherConfig;

    public override void InstallBindings()
    {
        Container.Bind<DefeatService>().AsSingle();
        Container.Bind<VictoryService>().AsSingle();

        Container.Bind<StartGameCanvasView>().FromInstance(_startGameCanvas).AsSingle();
        Container.Bind<DefeatGameCanvasView>().FromInstance(_defeatGameCanvas).AsSingle();
        Container.Bind<VictoryGameCanvasView>().FromInstance(_victoryGameCanvas).AsSingle();
        Container.Bind<GamePlayCanvasView>().FromInstance(_gamePlayCanvas).AsSingle();

        Container.BindInterfacesAndSelfTo<UserInputService>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyWatcherService>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerDistanceWatcherEnemyService>().AsSingle()
            .WithArguments(_playerDistanceWatcher, _distanceWatcherConfig);

        Container.BindInterfacesAndSelfTo<EnemySpawnerService>().AsSingle()
            .WithArguments(_spawnPoints, _enemySpawnerConfig, _enemyConfig, new MagEnemyFactory());

        Container.BindInterfacesAndSelfTo<ScreenSizeService>().AsSingle()
            .WithArguments(_camera, _screenSizeConfig);

        Container.BindInterfacesAndSelfTo<PlayerMoveService>().AsSingle()
            .WithArguments(_playerMoveView, _playerMoveConfig);

        Container.BindInterfacesAndSelfTo<AutoShootService>().AsSingle()
            .WithArguments(_autoShootView, _autoShootConfig, _bulletConfig);

        Container.BindInterfacesAndSelfTo<GameViewMediator>().AsSingle();

        Container.Bind<TowerService>().AsSingle().WithArguments(_towerView);

    }
}