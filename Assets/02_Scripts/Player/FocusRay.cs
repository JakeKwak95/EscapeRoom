using UnityEngine;
using UnityEngine.InputSystem;

public class FocusRay : InteractionRayBase
{
    RaycastHit raycastHit;
    Ray ray;
    Camera focusCam;


    protected override void OnEnable()
    {
        base.OnEnable();
        focusCam = Camera.main;
        rayOrigin = focusCam.transform;
    }


    private void Update()
    {
        ray = focusCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out raycastHit, float.MaxValue, interactableLayerMask))
        {
            raycastHit.collider.TryGetComponent(out currentInteractable);
        }
        else
        {
            currentInteractable = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (!enabled || !raycastHit.collider) return;

        Color color = currentInteractable ? Color.green : Color.red;
        Gizmos.color = color;
        Gizmos.DrawRay(ray.origin, ray.origin + (ray.direction * 100f));
    }
}
