using UnityEngine;

public class GroundLoop : MonoBehaviour
{
    private Material material;
    [SerializeField] private float parallaxFactor = 0.01f;
    private float offset;
    public float gameSpeed = 5f;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        ParallaxScroll();
    }

    private void ParallaxScroll()
    {
        float speed=gameSpeed*parallaxFactor;
        offset += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", Vector2.left*offset);
    }
}
