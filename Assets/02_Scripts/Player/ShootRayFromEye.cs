using UnityEngine;

public class ShootRayFromEye : MonoBehaviour
{
    [SerializeField] Transform eyePos;
    [SerializeField] LayerMask interactableLayerMask;

    InteractableBase currentInteractable;

    bool shouldShootRay = true;

    private void OnEnable()
    {
        InputManager.OnInteractInput += InputManager_OnInteractInput;
    }

    private void OnDisable()
    {
        InputManager.OnInteractInput -= InputManager_OnInteractInput;
    }

    private void Update()
    {
        RaycastHit hit;
        if (shouldShootRay && Physics.Raycast(eyePos.position, eyePos.forward, out hit, Mathf.Infinity, interactableLayerMask))
        {
            hit.collider.TryGetComponent(out currentInteractable);
        }
        else
        {
            currentInteractable = null;
        }
    }

    private void InputManager_OnInteractInput()
    {
        if (!currentInteractable) return;

        currentInteractable.BeginInteract();
    }

    private void OnDrawGizmos()
    {
        if (!enabled) return;

        Color color = currentInteractable ? Color.green : Color.red;
        Gizmos.color = color;
        Gizmos.DrawRay(eyePos.position, eyePos.forward * 100f);
    }

}
