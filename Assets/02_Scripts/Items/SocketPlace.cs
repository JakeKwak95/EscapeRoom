using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SocketPlace : MonoBehaviour
{
    [SerializeField] int socketID;

    public bool IsCorrectSocket(int id)
    {
        if(id == socketID)
        {
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }
}
