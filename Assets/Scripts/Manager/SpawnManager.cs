using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Transform lowPos;   // Vị trí thấp (Dành cho Bẫy, Thân cây, Chim tầm thấp)
    [SerializeField] private Transform midPos;   // Vị trí tầm trung (Chim tầm trung)
    [SerializeField] private Transform highPos;  // Vị trí tầm cao (Chim tầm cao)
    [SerializeField] private float lowBirdYOffset = -0.8f;

    // Bộ đếm thời gian
    private float timer = 0f;
    [SerializeField] private float nextSpawnInterval = 1.5f;

    private void Update()
    {
        timer += Time.deltaTime;

        // Khi đủ thời gian thì sinh ra chướng ngại vật mới
        if (timer >= nextSpawnInterval)
        {
            SpawnObstacle();
            timer = 0f; // Reset bộ đếm
        }
    }

    // Hàm xử lý logic sinh chướng ngại vật theo giai đoạn điểm số
    private void SpawnObstacle()
    {
        // Kiểm tra an toàn: GameManager phải tồn tại và mảng obstacles phải đủ 3 phần tử
        if (GameManager.instance == null || obstacles.Length < 3) return;

        float currentScore = GameManager.instance.GetScore();

        // Biến cấu hình nhịp độ game theo từng giai đoạn
        float minTime = 2.5f, maxTime = 4.0f;
        int birdChance = 0;              // Tỉ lệ xuất hiện Chim (%)
        bool allowTreeTrunk = false;     // Cho phép xuất hiện Thân cây gãy

        //Cấu hình độ khó game 
        if (currentScore < 100) // GIAI ĐOẠN 1: Mới vào game (Dễ)
        {
            minTime = 2.5f; maxTime = 4.0f;
            birdChance = 0;
            allowTreeTrunk = false; // Chỉ xuất hiện Bẫy đơn giản
        }
        else if (currentScore < 300) // GIAI ĐOẠN 2: Tăng tốc nhẹ
        {
            minTime = 1.8f; maxTime = 3.0f;
            birdChance = 10;
            allowTreeTrunk = true;  // Bắt đầu xuất hiện Thân cây gãy
        }
        else if (currentScore < 600) // GIAI ĐOẠN 3: Tốc độ cao
        {
            minTime = 1.2f; maxTime = 2.2f;
            birdChance = 25;
            allowTreeTrunk = true;
        }
        else // GIAI ĐOẠN 4: Thử thách cực đại (600+ điểm)
        {
            minTime = 0.8f; maxTime = 1.5f;
            birdChance = 40;
            allowTreeTrunk = true;
        }

        // Tính toán khoảng thời gian ngẫu nhiên cho lần spawn kế tiếp
        nextSpawnInterval = Random.Range(minTime, maxTime);
        int randomRate = Random.Range(0, 100);
        if (randomRate < birdChance) 
        {
            Transform chosenPos = lowPos;
            int posRand = Random.Range(0, 3);

            if (posRand == 1 && midPos != null) chosenPos = midPos;
            else if (posRand == 2 && highPos != null) chosenPos = highPos;

            // Lấy tọa độ gốc
            Vector3 spawnPosition = chosenPos.position;

            // Nếu chim xuất hiện ở vị trí lowPos thì hạ thấp Y xuống theo lowBirdYOffset
            if (chosenPos == lowPos)
            {
                spawnPosition.y += lowBirdYOffset;
            }

            Instantiate(obstacles[2], spawnPosition, Quaternion.identity);
        }
        else 
        {
            int groundIndex = 0;
            if (allowTreeTrunk)
            {
                groundIndex = Random.Range(0, 2); 
            }

            Instantiate(obstacles[groundIndex], lowPos.position, Quaternion.identity);
        }
    }
}