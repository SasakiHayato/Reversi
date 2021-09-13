using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    static GameManager instance = new GameManager();
    public static GameManager getInstance() => instance;
    private GameManager() { }

    public bool IsPlay { get; private set; }
    public bool SetIsPlay(bool set) => IsPlay = set;
}
