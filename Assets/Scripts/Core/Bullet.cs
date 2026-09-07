using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 dir;
    public float flySpeed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float objectWidth;
    public float objectHeight;
    public Camera mainCamera;
    public GameObject wave;
    private float timer = 0;
    private bool isAnchored = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        if (spriteRenderer != null)
        {
            objectWidth = spriteRenderer.bounds.extents.x;
            objectHeight = spriteRenderer.bounds.extents.y;
        }

        if (wave != null)
        {
            wave.SetActive(false);
        }
    }

    void Update()
    {
        if (isAnchored) return;

        // 1. Tự động di chuyển chuẩn xác theo MỌI hướng (Up, Left, 45 độ)
        // Không dùng if/else so sánh Vector2 để tránh trượt số thực
        transform.Translate((Vector3)dir.normalized * flySpeed * Time.deltaTime);

        // 2. Tính ranh giới hiển thị (dùng mốc 0 đến 1 Viewport chuẩn)
        // Đổi (0, 0) thành (0, 0.22f) cho đồng bộ với Tên lửa
        Vector3 minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.23f, mainCamera.nearClipPlane));
        Vector3 maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        float minX = minScreenBounds.x + objectWidth;
        float maxX = maxScreenBounds.x - objectWidth;
        float minY = minScreenBounds.y + objectHeight;
        float maxY = maxScreenBounds.y - objectHeight;
        // Lấy vị trí hiện tại
        Vector3 clampedPosition = transform.position;
        // Khóa tọa độ X và Y không cho vượt quá mép màn hình
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        // Áp dụng vị trí mới
        transform.position = clampedPosition;
        timer += Time.deltaTime;
        // hết 1 giây bay thì khóa 
        if (timer >= 1.5)
        {
            AnchorBullet(transform.position.x, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAnchored && collision.CompareTag("Rock"))
        {
            AnchorBullet(transform.position.x, transform.position.y);
        }
    }

    private void AnchorBullet(float clampX, float clampY)
    {
        isAnchored = true;

        transform.position = new Vector3(clampX, clampY, transform.position.z);

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (wave != null)
        {
            wave.SetActive(true);
        }
    }
}