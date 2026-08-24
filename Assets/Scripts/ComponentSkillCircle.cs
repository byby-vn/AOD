using UnityEngine;

public class ComponentSkillCircle : MonoBehaviour
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
        if (Control.Instance.currentSkill == CardSkillManager.SkillName.Aries)
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
        if (Control.Instance.currentSkill == CardSkillManager.SkillName.Cancer)
        {
            if (collision.CompareTag("Rock"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
