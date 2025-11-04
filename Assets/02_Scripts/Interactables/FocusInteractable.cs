using Unity.Cinemachine;
using UnityEngine;

public class FocusInteractable : InteractableBase
{
    [SerializeField] CinemachineCamera focusCamera;

    public override void BeginInteract()
    {
        if (!isInteractable) return;

        base.BeginInteract();

        isInteractable = false;
        Invoke(nameof(EnableInteraction), GlobalData.InteractionCoolDown);

        GameManager.Instance.ChangeGameState(GameState.Focusing);

        if(!focusCamera)
            focusCamera = GetComponentInChildren<CinemachineCamera>();

        focusCamera?.gameObject.SetActive(true);

        onInteract.Invoke();
    }

    public override void EndInteract()
    {
        base.EndInteract();

        focusCamera?.gameObject.SetActive(false);
        Invoke(nameof(ToMovingState), GlobalData.CameraBlendTime);
    }

    private void ToMovingState()
    {
        GameManager.Instance.ChangeGameState(GameState.Moving);
    }
}

public abstract class QuizBase : MonoBehaviour
{
    public abstract void CheckAnswer();
}