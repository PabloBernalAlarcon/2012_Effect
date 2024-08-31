using UnityEngine;

public class A_ParalaxEffect : MonoBehaviour
{
    private float length, starpos;
    public GameObject cam;
    public float parallaxEfect;
    void Start()
    {
        starpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        float speed = parallaxEfect;

    }

    
    void FixedUpdate()
    {   
        float temp = (cam.transform.position.x * (1 - parallaxEfect));
        float dist = (cam.transform.position.x * parallaxEfect);
        transform.position = new Vector3(starpos + dist, transform.position.y, transform.position.z);
        if (temp > starpos + length)starpos += length;
        else if(temp < starpos - length) starpos -= length;
    }
}
