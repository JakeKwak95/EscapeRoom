using UnityEngine;

public class FunctionTester : MonoBehaviour
{
    public void ChangeColor(Renderer renderer)
    {
        renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
