using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class FocusInteractable : InteractableBase
{
    [Header(" - Focus Interactable Settings - "), Space(10)]
    [SerializeField] CinemachineCamera focusCamera;
    [SerializeField] UnityEvent afterZoom;

    public override void BeginInteract(Transform interactor = null)
    {
        if (!isInteractable) return;

        base.BeginInteract(interactor);

        isInteractable = false;
        Invoke(nameof(EnableInteraction), GlobalData.InteractionCoolDown);

        GameManager.Instance.ChangeGameState(GameState.Focusing);

        if (!focusCamera)
            focusCamera = GetComponentInChildren<CinemachineCamera>();

        focusCamera?.gameObject.SetActive(true);

        onInteract.Invoke();

        Invoke(nameof(OnAfterZoom), GlobalData.CameraBlendTime);
    }

    public override void EndInteract()
    {
        base.EndInteract();

        focusCamera?.gameObject.SetActive(false);
        Invoke(nameof(ToWorldState), GlobalData.CameraBlendTime);
    }

    void OnAfterZoom()
    {
        afterZoom?.Invoke();
    }

    private void ToWorldState()
    {
        GameManager.Instance.ChangeGameState(GameState.World);
    }
}
