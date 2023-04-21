using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    pause,
    play,
    end
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static PlayerController Instance;
    public static ObjectPool poolinstance;
    public System.Action OnGameStart, OnGameEnd, ScoreIncrement,Spawn,WriteDistance;
    public int Score;
    public GameState gamestate;
    public Material[] mat;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        OnGameStart += StartGame;
        OnGameEnd += EndGame;
        Score = 0;
    }


    void StartGame()
    {
        gamestate = GameState.play;
        Time.timeScale = 1;
        Instance.anim.SetTrigger("Run");
    }

    void EndGame()
    {
        gamestate = GameState.end;
        Time.timeScale = 0;
        Instance.anim.SetTrigger("Die");
    }
}
