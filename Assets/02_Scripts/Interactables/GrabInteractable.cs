using System;
using UnityEngine;

public class GrabInteractable : InteractableBase
{
    Rigidbody rb;
    public Action onDrop;

    private void Awake()
    {
        TryGetComponent(out rb);
    }


    public override void BeginInteract(Transform interactor = null)
    {
        base.BeginInteract(interactor);

        if (interactor != null)
        {
            transform.SetParent(interactor);
            rb.isKinematic = true;
        }
    }

    public override void EndInteract()
    {
        base.EndInteract();

        transform.SetParent(null);
        rb.isKinematic = false;

        onDrop?.Invoke();
    }
}
