using UnityEngine;

public class Rock : MonoBehaviour
{
    public float fallSpeed = 10f;
    public float nowSpeed;
    
    [Header("Libra Settings")]
    public float libraGravityScale = -0.5f;
    public float maxPhysicsVelocity = 4f;

    private Rigidbody2D rb;
    public bool isFall;
    private bool wasUsingLibra;

    // Lực trọng lực chuẩn Trái Đất
    private readonly Vector2 gravityForce = new Vector2(0f, -9.81f);

    void Start()
    {
        Physics2D.gravity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // sử dụng trọng lực cục bộ bằng addforce 
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (isFall)
            {
                // Đá rơi: Chịu lực kéo XUỐNG DƯỚI 
                rb.AddForce(gravityForce * rb.gravityScale, ForceMode2D.Force);
            }
            else
            {
                // Đá ngang: Chịu lực kéo SANG PHẢI 
                Vector2 horizontalGravity = new Vector2(9.81f, 0f);
                rb.AddForce(horizontalGravity * rb.gravityScale, ForceMode2D.Force);
            }
        }
    }

    void Update()
    {
        bool isLibra = Control.Instance.currentSkill == CardSkillManager.SkillName.Libra && Control.Instance.isUsingSkill;
        bool isAquarius = Control.Instance.currentSkill == CardSkillManager.SkillName.Aquarius && Control.Instance.isUsingSkill;

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

            // Kẹp trần vận tốc tích lũy
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxPhysicsVelocity);
            wasUsingLibra = true;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;
            nowSpeed = fallSpeed;
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxPhysicsVelocity);
            if (wasUsingLibra)
            {
                rb.linearVelocity = Vector2.zero;
                wasUsingLibra = false;
            }
        }

        // Động cơ di chuyển Translate
        Vector3 moveDirection = isFall ? Vector3.down : Vector3.right;
        transform.Translate(moveDirection * nowSpeed * Time.deltaTime);

        // Hủy đá khi out map
        if (Mathf.Abs(transform.position.x) > 20f || Mathf.Abs(transform.position.y) > 20f)
        {
            Destroy(gameObject);
        }
    }
}