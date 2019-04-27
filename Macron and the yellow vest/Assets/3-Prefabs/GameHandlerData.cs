using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MoneyMessage : UnityEvent<System.Object>
{
}


[System.Serializable]
public class MoneyButtonMessage : UnityEvent<System.Object>
{
    public GameObject button;
}

public static class GameHandlerData
{
    public static string GoToMainMenuHandler = "00";
    public static string GoToLevelPickerSoloHandler = "01";
    public static string GoToLevelPickerMultiplayerHandler = "02";
    public static string GoToLevelOptionsHandler = "03";
    public static string LevelObjectSpawnFinishedHandler = "04";
    public static string SceneLoadedHandler = "05";
    public static string SceneLoadingHandler = "06";
    public static string SceneStartLoadingHandler = "07";
    public static string WaveStartingHandler = "08";
    public static string GameStartingHandler = "09";
    public static string PauseButtonPressedHandler = "10";
    public static string GameDataLoaded = "11";
    public static string PlayerMoneyUpdated = "12";
    public static string PlayerMoneyBeingUpdated = "13";
    public static string NewLevelUnlocked = "14";
    public static string MoneyButtonInstantiated = "15";
    public static string LangChangedHandler = "16";

}