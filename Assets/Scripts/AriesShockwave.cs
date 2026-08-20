using UnityEngine;

public class AriesShockwave : MonoBehaviour
{
    public Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        animator.Play("FadeIn");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 pushDirection = collision.transform.position - transform.position; //lấy hướng từ cục đá tới tâm vòng tròn
            float pushForce = 15f; //lực đẩy
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }
}
