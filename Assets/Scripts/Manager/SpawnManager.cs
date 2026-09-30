using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Obstacles; // Mảng chứa các Prefab chướng ngại vật (0, 1: Xương rồng; 2: Chim)
    [SerializeField] private Transform highPos;    // Vị trí xuất hiện trên cao
    [SerializeField] private Transform lowPos;     // Vị trí xuất hiện dưới thấp
    [SerializeField] private float spawnRate = 2f; // Khoảng thời gian giữa các lần tạo vật thể (giây)
    private float timer = 0;                       // Biến tích lũy thời gian

    private void Update()
    {
        // Cộng dồn thời gian trôi qua của từng khung hình
        timer += Time.deltaTime;

        // Khi đủ thời gian quy định thì tiến hành tạo vật thể
        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0; // Reset lại bộ đếm
        }
    }

    // Hàm tạo ngẫu nhiên chướng ngại vật
    private void SpawnObstacle()
    {
        // Kiểm tra an toàn: nếu mảng rỗng thì không chạy tiếp
        if (Obstacles == null || Obstacles.Length == 0) return;

        // Lấy chỉ số ngẫu nhiên từ 0 đến Obstacles.Length - 1
        int index = Random.Range(0, Obstacles.Length);

        // Trường hợp vật thể 0 hoặc 1 -> Sinh ra ở vị trí thấp (lowPos)
        if (index == 0 || index == 1)
        {
            GameObject Obstacle = Instantiate(Obstacles[index], lowPos.position, Quaternion.identity);
        }
        // Trường hợp vật thể 2 -> Sinh ra ở vị trí cao (highPos)
        else if (index == 2)
        {
            GameObject Obstacle = Instantiate(Obstacles[index], highPos.position, Quaternion.identity);
        }
    }
}