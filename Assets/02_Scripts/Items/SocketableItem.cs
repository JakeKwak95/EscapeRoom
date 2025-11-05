using UnityEngine;

public class SocketableItem : MonoBehaviour
{
    [SerializeField] int socketID;
    SocketPlace socket;

    private void OnEnable()
    {
        if (TryGetComponent(out GrabInteractable interactable))
        {
            interactable.onDrop += OnDropped;
        }
    }

    private void OnDisable()
    {
        if (TryGetComponent(out GrabInteractable interactable))
        {
            interactable.onDrop -= OnDropped;
        }
    }

    private void OnDropped()
    {
        if (socket && socket.IsCorrectSocket(socketID))
        {
            print("Socket ID Matches, Snapping Item into Place");
            transform.SetPositionAndRotation(socket.transform.position, socket.transform.rotation);
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SocketPlace socketPlace))
        {
            socket = socketPlace;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out SocketPlace socketPlace))
        {
            if (socket == socketPlace)
                socket = null;
        }
    }


}
