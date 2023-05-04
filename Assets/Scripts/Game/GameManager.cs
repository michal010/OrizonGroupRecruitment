using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameType { Endless, Story }

public interface IGameManager
{
    BaseGame GetGame();
    IGameEvents GameEvents { get; }
    UnityEvent OnGameManagerTickEvent { get; }
}

public abstract class BaseGameManager : MonoBehaviour
{

}

public class GameManager : BaseGameManager , IGameManager
{
    public GameType gameType = GameType.Endless;

    public UnityEvent OnGameManagerTickEvent { get; private set; }

    private BaseGame Game;

    // Singleton solution for now.
    public static GameManager Instance { get; private set; }

    public IGameEvents GameEvents { get; private set; }
    //public IGame Game { get; private set; }

    public BaseGame GetGame()
    {
        return Game;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        GameEvents = new GameEvents(this);
        OnGameManagerTickEvent = new UnityEvent();
        // Initialize all systems
        switch (gameType)
        {
            case GameType.Endless:
                Game = new EndlessGame(this);
                break;
            case GameType.Story:
                break;
        }
        // Start game
        Game.StartGame();
    }
    void Update()
    {
        OnGameManagerTickEvent?.Invoke();
    }

}
