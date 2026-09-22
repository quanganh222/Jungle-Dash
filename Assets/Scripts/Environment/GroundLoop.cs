using UnityEngine;

public class GroundLoop : MonoBehaviour
{
    [SerializeField] private Transform ground1;
    [SerializeField] private Transform ground2;
    [SerializeField] private float moveSpeed = 5f;

    private float groundWidth;

    private void Start()
    {
        groundWidth = Mathf.Abs(ground2.position.x - ground1.position.x);
    }

    private void Update()
    {
        // để mặt dất di chuyển sang bên trái 
        ground1.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        ground2.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (ground1.position.x <= -groundWidth)
        {
            // đưa nó sang phía bên phải 
            ground1.position = new Vector3(
                ground2.position.x + groundWidth,
                ground1.position.y,
                ground1.position.z
            );
        }
         if (ground2.position.x <= -groundWidth)
        {
            // đưa nó sang phía bên phải 
            ground2.position = new Vector3(
                ground1.position.x + groundWidth,
                ground2.position.y,
                ground2.position.z
            );
        }
    }
}
