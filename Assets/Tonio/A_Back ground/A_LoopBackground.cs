using UnityEngine;

public class A_LoopBackground : MonoBehaviour
{
    public float loopSpeed;
    public Renderer bgRenderer;

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset = new Vector2(loopSpeed * Time.deltaTime, 0f);
    }
}
