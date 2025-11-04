using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menuObject;

    private void Awake()
    {
        InputManager.OnEscapeInput += InputManager_OnEscapeInput;
    }
    private void OnDestroy()
    {
        InputManager.OnEscapeInput -= InputManager_OnEscapeInput;
    }
    private void InputManager_OnEscapeInput()
    {
        if(GameManager.Instance.CurrentGameState.state == GameState.Moving ||
           GameManager.Instance.CurrentGameState.state == GameState.Paused)
            menuObject.SetActive(!menuObject.activeSelf);
    }

    public void ChangeState(int state)
    {
        GameManager.Instance.ChangeGameState((GameState)state);
        menuObject.SetActive(false);
    }
}
