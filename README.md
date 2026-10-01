# 🌴 Jungle-Dash (2D Endless Runner)

**Jungle-Dash** là một tựa game 2D side-scrolling endless runner được phát triển trên **Unity 6**. Người chơi điều khiển nhân vật chạy qua khu rừng rậm, né tránh các chướng ngại vật ngẫu nhiên bằng cách nhảy hoặc cúi người, đồng thời tích lũy điểm số khi tốc độ tăng dần theo thời gian.

---

## 🚀 Tính Năng Nổi Bật (Features)

* **Hệ thống di chuyển & Hitbox linh hoạt:**
  * Hỗ trợ cơ chế **Jump** (Nhảy) và **Duck** (Cúi người).
  * Tự động chuyển đổi Hitbox (Collider) mượt mà giữa trạng thái đứng và cúi để né tránh chướng ngại vật tầm cao/thấp.
* **Tự động tăng tốc độ theo điểm (Dynamic Difficulty Scaling):**
  * Tốc độ game tăng dần mượt mà theo các mốc điểm bằng thuật toán `Mathf.Lerp`:
    * `< 100 điểm`: Tốc độ cơ bản (5.0)
    * `100 - 300 điểm`: Tăng tốc mượt từ 5.0 lên 8.0
    * `300 - 600 điểm`: Tăng tốc mượt từ 8.0 lên 12.0
    * `> 600 điểm`: Tốc độ tối đa (15.0)
* **Chướng ngại vật đa dạng & Tối ưu bộ nhớ:**
  * Các loại chướng ngại vật (Bẫy gai, Khúc cây, Chim bay) được sinh ra ngẫu nhiên từ Spawner ở các độ cao khác nhau (`HighPos`, `MidPos`, `LowPos`).
  * Sử dụng `Polygon Collider 2D` cho hitbox chuẩn xác theo hình dáng vật thể.
  * Tự động hủy (`Destroy`) vật thể khi vượt quá giới hạn màn hình (`leftBoundary`).
* **UI & Luồng trạng thái game (UI-Driven Architecture):**
  * Luồng chuyển giao trạng thái liền mạch: **Main Menu $\rightarrow$ Gameplay $\rightarrow$ Game Over**.
  * Bắt sự kiện nút UI bằng code (`onClick.AddListener`), đảm bảo tính ổn định và không lo bị đứt liên kết trong Inspector.
* **Âm thanh liền mạch (Seamless Audio):**
  * Nhạc nền (BGM) chạy liên tục từ màn hình Main Menu sang Gameplay mà không bị gián đoạn.
  * Tự động reset giai điệu khi người chơi chọn chơi lại (Restart).

---

## 🛠️ Điểm Sáng Kỹ Thuật (Technical Highlights)

* **Engine:** Unity 6 (Version `6000.5.6f1`).
* **Input System:** Unity New Input System (`UnityEngine.InputSystem`).
* **Design Pattern:** **Singleton Pattern** áp dụng cho `GameManager` để quản lý trạng thái toàn cục (`Score`, `GameSpeed`, `TimeScale`, `GameOver`).
* **Graphics & Rendering:** Material `Sprite-Unlit-Default` đảm bảo hình ảnh rõ nét, không bị ảnh hưởng bởi lỗi thiếu sáng 2D URP.

---

## 🎮 Hướng Dẫn Điều Khiển (Controls)

| Thao tác | Phím bấm (Keyboard) | Chức năng |
| :--- | :--- | :--- |
| **Nhảy (Jump)** | `Space` | Nhảy lên cao để né chướng ngại vật mặt đất |
| **Cúi (Duck)** | `Mũi tên xuống (Down Arrow)` | Cúi người xuống để thu nhỏ Hitbox né chướng ngại vật tầm cao |

---

## 📂 Cấu Trúc Thư Mục Dự Án (Project Structure)

```text
Assets/
├── Animations/       # Animation Controller & Clips (Walk, Duck, Jump)
├── Audio/            # Background Music & Sound Effects (.wav / .mp3)
├── Prefabs/          # Prefab chướng ngại vật (Gai, Chim, Khúc cây)
├── Scenes/           # Scene Gameplay chính
├── Scripts/
│   ├── Environment/  # GroundLoop.cs
│   ├── Manager/      # GameManager.cs, Spawner.cs, Obstacle.cs
│   ├── Player/       # PlayerController.cs
│   └── UI/           # MainMenuUI.cs
└── Sprites/          # Sprite 2D (Player, Tileset, Background)