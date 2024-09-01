using UnityEngine;

public class A_Loopbackground1 : MonoBehaviour
{
    [Range(-10f,10f)]
    public float scrollSpeed = 0.5f;
    private float offset;
    private Material mat;

   
    public Vector2 tiling = new Vector2(1, 1);  // Adjust these values as needed
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetTextureScale("_MainTex", tiling);  // Explicitly set the tiling

        offset += (Time.deltaTime * scrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
