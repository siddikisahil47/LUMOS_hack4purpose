using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameInfo", menuName = "Game/Game Info")]
public class GameItemSo : ScriptableObject
{
    public string gameName;
    public long stepsToUnlock;
    public bool unlocked;
    public string url;
}
