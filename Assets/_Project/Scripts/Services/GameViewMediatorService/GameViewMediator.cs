using System;
using System.Collections.Generic;
using Zenject;

public class GameViewMediator: IInitializable, IDisposable
{
    private StartGameCanvasView _startGameCanvas;
    private DefeatGameCanvasView _defeatGameCanvas;
    private VictoryGameCanvasView _victoryGameCanvas;
    private GamePlayCanvasView _gamePlayCanvas;
    private readonly EnemyWatcherService _enemyWatcherService;
    private readonly EnemySpawnerService _enemySpawnerService;
    private readonly AutoShootService _autoShootService;
    private readonly UserInputService _userInputService;
    private readonly DefeatService _defeatService;
    private readonly VictoryService _victoryService;
    private readonly TowerService _towerService;
    private readonly IDistanceWatcherEnemyService _distanceWatcherEnemyService;

    private List<CanvasViewPattern> _canvasViewPatterns;

    public GameViewMediator(
        StartGameCanvasView startGameCanvas, 
        DefeatGameCanvasView defeatGameCanvas,
        VictoryGameCanvasView victoryGameCanvas, 
        GamePlayCanvasView gamePlayCanvas,
        EnemyWatcherService enemyWatcherService,
        EnemySpawnerService enemySpawnerService,
        AutoShootService autoShootService,
        UserInputService userInputService,
        DefeatService defeatService,
        VictoryService victoryService,
        TowerService towerService,
        IDistanceWatcherEnemyService distanceWatcherEnemyService)
    {
        _startGameCanvas = startGameCanvas;
        _defeatGameCanvas = defeatGameCanvas;
        _victoryGameCanvas = victoryGameCanvas;
        _gamePlayCanvas = gamePlayCanvas;
        _enemyWatcherService = enemyWatcherService;
        _enemySpawnerService = enemySpawnerService;
        _autoShootService = autoShootService;
        _userInputService = userInputService;
        _defeatService = defeatService;
        _victoryService = victoryService;
        _towerService = towerService;
        _distanceWatcherEnemyService = distanceWatcherEnemyService;
    }

    public void Initialize()
    {
        _canvasViewPatterns = new List<CanvasViewPattern>()
        {
            _startGameCanvas,
            _defeatGameCanvas,
            _victoryGameCanvas,
            _gamePlayCanvas
        };

        _defeatService.Defeat += Defeat;
        _victoryService.Victory += Victory;
        _startGameCanvas.Button.onClick.AddListener(OnClickRestartGame);
        _defeatGameCanvas.Button.onClick.AddListener(OnClickRestartGame);
        _victoryGameCanvas.Button.onClick.AddListener(OnClickRestartGame);
    }

    public void Dispose()
    {
        _defeatService.Defeat -= Defeat;
        _victoryService.Victory -= Victory;
        _startGameCanvas.Button.onClick.RemoveAllListeners();
        _defeatGameCanvas.Button.onClick.RemoveAllListeners();
        _victoryGameCanvas.Button.onClick.RemoveAllListeners();
    }

    private void Victory()
    {
        VictoryGame();
    }

    private void Defeat()
    {
        DefeatGame();
    }

    private void OnClickRestartGame()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        _towerService.ResetHealth();
        _canvasViewPatterns.ForEach(pattern => pattern.Close());
        _gamePlayCanvas.Open();
        _userInputService.Enable();
        _enemySpawnerService.StartAutoSpawn();
        _autoShootService.StartShoot();
        _enemyWatcherService.StartWatch();
        _distanceWatcherEnemyService.StartWatch();
    }

    private void StopGame()
    {
        _canvasViewPatterns.ForEach(pattern => pattern.Close());
        _userInputService.Disable();
        _enemySpawnerService.StopAutoSpawn();
        _autoShootService.StopShoot();
        _enemyWatcherService.StopWatch();
        _distanceWatcherEnemyService.StopWatch();
    }

    private void VictoryGame()
    {
        StopGame();
        _victoryGameCanvas.Open();
    }

    private void DefeatGame()
    {
        StopGame();
        _defeatGameCanvas.Open();
    }
}