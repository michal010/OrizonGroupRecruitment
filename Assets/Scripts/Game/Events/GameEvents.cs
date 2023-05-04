using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameEvents
{
    public void OnPlayerHit();
    public void OnRestartGame();
}

public class GameEvents : IGameEvents
{
    private IGameManager gameManager;

    public GameEvents(IGameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPlayerHit()
    {
        //Debug.Log("Reset game or check if player has hp etc.");
        //(GameManager)gameManager.Game.
        //gameManager.Game.
        gameManager.GetGame().player.Animator.Play("Stumble Backwards");
        gameManager.GetGame().player.Transform.gameObject.GetComponent<Player>().enabled = false;
        gameManager.GetGame().uiManager.ShowGameOverPanel();
        gameManager.GetGame().EndGame();
        //Debug.Log(p.Transform.position);
    }
}

//public interface IConcreteGame
//{
//    void StartGame();
//    void EndGame();
//}

//public class ConcreteGame : IConcreteGame
//{
//    public void EndGame() { }
//    public void StartGame() { }
//}

//public class GameModeA : ConcreteGame
//{
//    // Some data I'd like to access
//    int PlayerHealth;
//}

//public interface IGM
//{
//    IConcreteGame Game { get; }
//}

//public interface IGEvents
//{
//    public void DoSth();
//}

//public class GEvents : IGEvents
//{
//    IGM gM;
//    public GEvents(IGM gM)
//    {
//        this.gM = gM;
//    }

//    public void DoSth()
//    {
//        // access data via gM
//        gM.Game.PlayerHealth; // Error, how to fix?
//    }
//}

//public class GM: MonoBehaviour, IGM
//{
//    public IConcreteGame Game { get; private set; }
//    public IGEvents GameEvents { get; private set; }
//    void Awake()
//    {
//        Game = new GameModeA();
//    }
//}