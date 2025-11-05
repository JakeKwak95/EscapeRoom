using UnityEngine;

public class WorldRay : InteractionRayBase
{
    [SerializeField] float interactDistance = 3f;

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
