using UnityEngine;

public abstract class InteractionRayBase : MonoBehaviour
{
    protected Transform rayOrigin;
    [SerializeField] protected LayerMask interactableLayerMask;
    protected InteractableBase currentInteractable;

    protected virtual void OnEnable()
    {
        InputManager.OnInteractInput += InputManager_OnInteractInput;
        InputManager.OnMouseLClickInput += InputManager_OnMouseLClickInput;

        rayOrigin = Camera.main.transform;
    }
    protected virtual void OnDisable()
    {
        InputManager.OnInteractInput -= InputManager_OnInteractInput;
        InputManager.OnMouseLClickInput -= InputManager_OnMouseLClickInput;
    }

    protected virtual void InputManager_OnInteractInput()
    {
        if (!currentInteractable) return;

        currentInteractable.BeginInteract();
    }
    protected virtual void InputManager_OnMouseLClickInput()
    {
        if (!currentInteractable) return;

        currentInteractable.BeginInteract();
    }

}
