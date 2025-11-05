using EditorAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header(" - Game State Management - ")]

    [SerializeField] StateBehaviourSet[] stateBehaviourSets;
    List<MonoBehaviour> managedBehaviours = new List<MonoBehaviour>(10);
    public StateBehaviourSet CurrentGameState { get; private set; }

    public Action OnFocus;

    [Header(" - Debug - ")]
    [SerializeField, EnumButtons] GameState debugGameState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (var set in stateBehaviourSets)
        {
            if (set.enabledBehaviours.Length > 0)
            {
                managedBehaviours.AddRange(set.enabledBehaviours);
            }
        }

        if(Camera.main.gameObject.TryGetComponent(out CinemachineBrain brain))
        {
            brain.DefaultBlend.Time = GlobalData.CameraBlendTime;
        }
    }

    private void OnEnable()
    {
        InputManager.OnEscapeInput += InputManager_OnEscapeInput;
    }

    private void OnDisable()
    {
        InputManager.OnEscapeInput -= InputManager_OnEscapeInput;
    }

    private void Start()
    {
        ChangeGameState(debugGameState);
    }

    [Button]
    public void ChangeGameState(GameState newState)
    {
        CurrentGameState = stateBehaviourSets.Where(set => set.state == newState).FirstOrDefault();
        foreach (var behaviour in managedBehaviours)
        {
            if (!CurrentGameState.enabledBehaviours.Contains(behaviour))
                behaviour.enabled = false;
            else
                behaviour.enabled = true;
        }

        switch( newState)
        {
            case GameState.None:
                break;
            case GameState.World:
                SetCursor(CursorLockMode.Locked);
                break;
            case GameState.Focusing:
                OnFocus?.Invoke();
                SetCursor(CursorLockMode.None);
                break;
            case GameState.Paused:
                SetCursor(CursorLockMode.None);
                break;
            default:
                break;
        }
    }

    private static void SetCursor(CursorLockMode cursorLockMode)
    {
        Cursor.lockState = cursorLockMode;
        Cursor.visible = cursorLockMode != CursorLockMode.Locked;
    }

    private void InputManager_OnEscapeInput()
    {
        switch (CurrentGameState.state)
        {
            case GameState.None:
                break;
            case GameState.World:
                ChangeGameState(GameState.Paused);
                break;
            case GameState.Focusing:
                // None 을 중간에 아무작용 없는 상태로 사용
                ChangeGameState(GameState.None);
                InteractableBase.CurrentInteractable?.EndInteract();
                break;
            case GameState.Paused:
                ChangeGameState(GameState.World);
                break;
            default:
                break;
        }
    }
}

public enum GameState
{
    None = -1,

    World,
    Focusing,
    Paused,

    Max

}


[Serializable]
public struct StateBehaviourSet
{
   [EnumButtons] public GameState state;
    public MonoBehaviour[] enabledBehaviours;
}