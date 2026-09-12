using UnityEngine;

public class LittleDevil : MonoBehaviour
{
    [Header("Cấu hình Xoay Quanh Player")]
    public float orbitSpeed = 200f;     // Tốc độ xoay (độ/giây)
    public float orbitRadius = 1.5f;     // Khoảng cách đến Rocket
    public float orbitAngle = 0f;        // Góc xoay hiện tại (được Manager gán)
    
    [Header("Cấu hình Đuổi Theo Đá")]
    public float chaseSpeed = 15f;       // Tốc độ bay lao vào đá
    
    private Transform playerTransform;
    private Transform targetRock;
    private bool isHoming = false;       // Đã khóa mục tiêu chưa
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = Control.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHoming) //chưa có mục tiêu
        {
            //tăng góc xoay theo thời gian
            orbitAngle += orbitSpeed * Time.deltaTime;
            if (orbitAngle >= 360f) orbitAngle -= 360f; //reset khi devil xoay được 1 vòng
            // Tính vị trí mới trên đường tròn quanh Rocket
            float rad = orbitAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * orbitRadius;
            transform.position = playerTransform.position + offset;
        }
        else //khóa mục tiêu 
        {
            // Nếu đá bị nổ trước đó bởi skill khác -> Hủy viên đạn
            if (targetRock == null)
            {
                Destroy(gameObject);
                return;
            }

            // Bay hướng về phía viên Đá
            transform.position = Vector3.MoveTowards(transform.position, targetRock.position, chaseSpeed * Time.deltaTime);
        }
    }
    // Hàm gọi khi Radar phát hiện có đá
    public void AssignTarget(Transform rock)
    {
        targetRock = rock;
        isHoming = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Va chạm với Đá -> Phá hủy cả 2
        if (isHoming && collision.CompareTag("Rock") && collision.transform == targetRock)
        {
            Destroy(collision.gameObject); // Phá hủy Đá
            Destroy(gameObject,0.3f);           // Phá hủy Tiểu Quỷ
        }
    }
}
