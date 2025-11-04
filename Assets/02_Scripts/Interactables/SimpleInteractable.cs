public class SimpleInteractable : InteractableBase
{
    public override void BeginInteract()
    {
        if (!isInteractable) return;
        base.BeginInteract();
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