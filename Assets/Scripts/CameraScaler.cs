using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    // Tỷ lệ màn hình chuẩn khi thiết kế (Ví dụ 16:9 = 1.777f, hoặc 9:16 = 0.5625f)
    public float targetAspectRatio = 9f / 16f; 
    public float targetOrthographicSize = 5f;

    void Awake()
    {
        Camera cam = Camera.main;
        float currentAspectRatio = (float)Screen.width / Screen.height;

        // Tự động tính toán lại kích thước Camera để không bị mất góc nhìn chiều rộng
        if (currentAspectRatio < targetAspectRatio)
        {
            cam.orthographicSize = targetOrthographicSize * (targetAspectRatio / currentAspectRatio);
        }
        else
        {
            cam.orthographicSize = targetOrthographicSize;
        }
    }
}