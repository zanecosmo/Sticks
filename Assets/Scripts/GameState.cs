using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameState : MonoBehaviour
{
    private static GameState _instance;
    public static GameState Instance { get { return _instance; } }

    int _currentPlayer = 0;
    int _turn = 0;
    bool _gameOver = false;

    int? _winner = null;
    public static int[]? Hovered = null;
    int[]? _selected = null;

    static int[,] _players = new int[2, 2]
    {
        { 1, 1 },
        { 1, 1 }
    };

    public static int[,] Players { get { return _players; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        // Debug.Log(Hovered == null ? null : $"{Hovered[0]}, {Hovered[1]}");

        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        };
    }

    void Bump()
    {
        Debug.Log("Bumped");
    }

    void Transfer()
    {
        Debug.Log("Transferred");

        _players[Hovered[0], Hovered[1]]+= _players[_selected[0], _selected[1]];

        if (_players[Hovered[0], Hovered[1]] >= 5) _players[Hovered[0], Hovered[1]] -= 5;
    }

    void CheckForWin(out int winner)
    {
        Debug.Log("Checking for win");

        for (int player = 0; player < _players.GetLength(0); player++)
        {
            int score = 0;

            for (int hand = 0; hand < _players.GetLength(1); hand++)
            {
                if (_players[player, hand] > 0) score++;
            }

            if (score == 0)
            {
                winner = player;
                return;
            }
        }

        winner = -1;
    }

    void HandleMouseClick()
    {
        Debug.Log("Current Player: " + _currentPlayer);
        Debug.Log("Turn: " + _turn);
        Debug.Log(Hovered == null ? "Hovered: NULL" : $"Hovered: {Hovered[0]}, {Hovered[1]}");
        Debug.Log(_selected == null ? "Selected: NULL" : $"Selected: {_selected[0]}, {_selected[1]}");

        if (Hovered == null) return;

        if (_selected == null)
        {
            Debug.Log("----");
            Debug.Log(Hovered[0]);
            if (Hovered[0] != _currentPlayer) Debug.Log("You must choose one of your own hands first");
            else
            {
                if (_players[Hovered[0], Hovered[1]] == 0) {
                    Debug.Log("Cannot use a zero score");
                    return;
                }
                _selected = Hovered;
            }
            return;
        };

        if (Hovered[0] == _currentPlayer) Bump();
        else Transfer();

        CheckForWin(out int winner);

        if (winner > -1)
        {
            _winner = winner;
            _gameOver = true;
            Debug.Log(_winner);
            return;
        }

        _selected = null;
        _turn++; // handle all logic relating to changing of turns here, including graphical changes
        _currentPlayer = _currentPlayer == 0 ? 1 : 0;
    }
}

// when player is instantiated, rotate it and position it, and then seperately rotate and position the numbers on the hands