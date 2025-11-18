using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class InteractableBase : MonoBehaviour
{
    public static InteractableBase CurrentInteractable { get; protected set; }

    [SerializeField] protected UnityEvent onInteract;

    protected bool isInteractable = true;

    public virtual void BeginInteract(Transform interactor = null)
    {
        CurrentInteractable = this;
    }
    public virtual void EndInteract()
    {
        CurrentInteractable = null;
    }

    protected virtual void EnableInteraction()
    {
        isInteractable = true;

    }
}
