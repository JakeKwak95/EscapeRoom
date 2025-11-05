using UnityEngine;

public class SimpleInteractable : InteractableBase
{
    public override void BeginInteract(Transform interactor = null)
    {
        if (!isInteractable) return;
        base.BeginInteract(interactor);
        isInteractable = false;
        Invoke(nameof(EnableInteraction), GlobalData.InteractionCoolDown);
        onInteract.Invoke();
    }

    protected override void EnableInteraction()
    {
        base.EnableInteraction();

        EndInteract();
    }
}