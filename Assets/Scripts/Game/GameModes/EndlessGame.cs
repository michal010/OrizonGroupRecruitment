using UnityEngine;

public class EndlessGame : BaseGame
{
    ISegmentGenerator segmentGenerator;
    IObstacleGenerator obstacleGenerator;

    public EndlessGame(IGameManager gameManager) : base(gameManager)
    {
        levelDistanceScore = new LevelDistance(player);
        obstacleGenerator = new ObstacleGenerator();
        segmentGenerator = new SegmentGenerator(obstacleGenerator, player, new UnityEngine.GameObject("Map").transform);
        //endlessGameUIManager = MonoFactory.CreateGameObject<EndlessGameUIManager>("EndlessGameUIManager");
        uiManager = MonoFactory.CreateWithDepedency<EndlessGameUIManager,LevelDistance>("EndlessGameUIManager", levelDistanceScore);
        uiManager.transform.SetParent( GameObject.Find("UI_Container").transform, false) ;
    }

    public override void EndGame()
    {
        gameManager.OnGameManagerTickEvent.RemoveAllListeners();
    }

    public override void StartGame()
    {
        // Does nothing currently.
        base.StartGame();
        
        // Start new endless game...
        segmentGenerator.GenerateLevel();
        // Hook scoring system to tick system
        //gameManager.OnGameManagerTickEvent += levelDistanceScore.UpdateDistance;
        gameManager.OnGameManagerTickEvent.AddListener(levelDistanceScore.UpdateDistance);
        gameManager.OnGameManagerTickEvent.AddListener(segmentGenerator.CheckForSegmentGeneration);

    }

}
