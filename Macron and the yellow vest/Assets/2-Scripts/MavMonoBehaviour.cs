using System.Collections;
using System.Collections.Generic;
using MAV;
using UnityEngine;

public class MavMonoBehaviour : MonoBehaviour
{

    public bool ScriptReady = false;

    // Use this for initialization
    public virtual void Start()
    {
        EventHandlerRegister();
        ScriptReady = true;
    }

    public virtual void OnDisable()
    {
        EventHandlerUnRegister();
    }

    public virtual void EventHandlerRegister()
    {
        EventManager.StartListening(GameHandlerData.SceneLoadedHandler, OnSceneLoaded);
        EventManager.StartListening(GameHandlerData.GameStartingHandler, OnGameStarting);
        EventManager.StartListening(GameHandlerData.PauseButtonPressedHandler, OnGamePauseButtonPressed);
        EventManager.StartListening(GameHandlerData.WaveStartingHandler, OnWaveStarting);
    }

    public virtual void EventHandlerUnRegister()
    {
        EventManager.StopListening(GameHandlerData.SceneLoadedHandler, OnSceneLoaded);
        EventManager.StopListening(GameHandlerData.GameStartingHandler, OnGameStarting);
        EventManager.StopListening(GameHandlerData.PauseButtonPressedHandler, OnGamePauseButtonPressed);
        EventManager.StopListening(GameHandlerData.WaveStartingHandler, OnWaveStarting);
    }


    public virtual void OnSceneLoaded(object arg0)
    {
    }

    public virtual void OnGameStarting(object arg0)
    {
    }

    public virtual void OnGamePauseButtonPressed(object arg0)
    {
    }

    public virtual void OnWaveStarting(object arg0)
    {
    }
}
