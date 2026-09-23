using System;
using UnityEditor.Search;
using UnityEngine;

public class GroundLoop : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private float speed = 5f;

    private bool hasSpawnedGround = false;

    // Theo kích thước ground hiện tại
    private float groundWidth = 9f;

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Khi Ground chạy đến X <= 1 thì tạo mảnh tiếp theo
        if (transform.position.x <= 1f && !hasSpawnedGround)
        {
            Vector3 spawnPosition = new Vector3(
                transform.position.x + groundWidth,
                transform.position.y,
                transform.position.z
            );

            Instantiate(
                groundPrefab,
                spawnPosition,
                Quaternion.identity
            );

            hasSpawnedGround = true;
        }

        // Ra xa bên trái thì xóa
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}
