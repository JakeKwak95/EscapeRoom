using System;
using UnityEngine;

public class WorldRay : InteractionRayBase
{
    [SerializeField] float interactDistance = 5f;
    [SerializeField] Transform rayOriginTransform;

    protected override void OnEnable()
    {
        base.OnEnable();
        
        InputManager.OnMouseRClickInput += InputManager_OnMouseRClickInput;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        InputManager.OnMouseRClickInput -= InputManager_OnMouseRClickInput;
    }

    private void InputManager_OnMouseRClickInput()
    {
        if (currentInteractable)
            currentInteractable.EndInteract();

        currentInteractable = null;
    }

    protected override void InputManager_OnMouseLClickInput()
    {
        if (!currentInteractable) return;

        currentInteractable.BeginInteract(rayOriginTransform);
    }

    protected override void InputManager_OnInteractInput()
    {
        if (!currentInteractable) return;

        currentInteractable.BeginInteract(rayOriginTransform);
    }

    private void Update()
    {
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out RaycastHit hit, interactDistance, interactableLayerMask))
        {
            hit.collider.TryGetComponent(out currentInteractable);
        }
        else
        {
            currentInteractable = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (!enabled) return;

        Color color = currentInteractable ? Color.green : Color.red;
        Gizmos.color = color;
        Gizmos.DrawRay(rayOrigin.position, rayOrigin.forward * interactDistance);
    }

}
