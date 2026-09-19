using UnityEngine;

public class Rock : MonoBehaviour
{
    public float fallSpeed = 10f;
    public float nowSpeed;

    [Header("Libra Settings")]
    public float libraGravityScale = -0.5f;
    public float maxPhysicsVelocity = 4f;

    [Header("Aries Settings")]
    public float ariesPushDuration = 0.5f; // Thời gian tắt Translate để đá văng tự do
    private bool isPushedByAries = false;
    private float ariesPushTimer = 0f;

    private Rigidbody2D rb;
    public bool isFall;
    private bool wasUsingLibra;
    private bool isLibra;
    private bool isAquarius;


    private readonly Vector2 gravityForce = new Vector2(0f, -9.81f);

    void Start()
    {
        Physics2D.gravity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (isFall)
            {
                rb.AddForce(gravityForce * rb.gravityScale, ForceMode2D.Force);
            }
            else
            {
                Vector2 horizontalGravity = new Vector2(9.81f, 0f);
                rb.AddForce(horizontalGravity * rb.gravityScale, ForceMode2D.Force);
            }
        }
    }

    void Update()
    {
        // 1. Đếm giờ xử lý trạng thái bị Aries đẩy văng
        if (isPushedByAries)
        {
            ariesPushTimer += Time.deltaTime;
            if (ariesPushTimer >= ariesPushDuration)
            {
                isPushedByAries = false;
                ariesPushTimer = 0f;
            }
        }
        isLibra = Control.Instance.currentSkill == CardSkillManager.SkillName.Libra && Control.Instance.isUsingSkill;
        isAquarius = Control.Instance.currentSkill == CardSkillManager.SkillName.Aquarius && Control.Instance.isUsingSkill;
        if (isAquarius)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            nowSpeed = 0f;
        }
        else if (isLibra)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = libraGravityScale;
            nowSpeed = -fallSpeed;

            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxPhysicsVelocity);
            wasUsingLibra = true;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;
            nowSpeed = fallSpeed;

            if (wasUsingLibra)
            {
                rb.linearVelocity = Vector2.zero;
                wasUsingLibra = false;
            }
        }

        // 2. Chỉ di chuyển bằng Translate khi KHÔNG bị Aries hất văng
        if (!isPushedByAries)
        {
            Vector3 moveDirection = isFall ? Vector3.down : Vector3.right;
            transform.Translate(moveDirection * nowSpeed * Time.deltaTime);
        }

        if (Mathf.Abs(transform.position.x) > 20f || Mathf.Abs(transform.position.y) > 20f)
        {
            Destroy(gameObject);
        }
    }

    // 3. Hàm nhận lực đẩy từ Aries
    public void ApplyAriesPush(Vector2 pushDirection, float pushForce)
    {
        isPushedByAries = true;
        ariesPushTimer = 0f;

        rb.bodyType = RigidbodyType2D.Dynamic; // Chuyển sang Dynamic nếu đang bị freeze bởi Aquarius
        rb.linearVelocity = pushDirection * pushForce; // Ép vận tốc hất văng tức thì
    }
}