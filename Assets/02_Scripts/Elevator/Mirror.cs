using UnityEngine;

[ExecuteAlways]
public class Mirror : MonoBehaviour
{
    [SerializeField] Material mirrorMaterial;
    [SerializeField] Transform player;

    private void Update()
    {
        if (mirrorMaterial && player)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            mirrorMaterial.SetFloat("_Distance", distance);
        }
    }
}
