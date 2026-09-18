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
        ActiveAnimation();
    }

    void ActiveAnimation()
    {
        if (Control.Instance.currentSkill == CardSkillManager.SkillName.Aries)
        {
            animator.Play("FadeIn");
        }
        else if (Control.Instance.currentSkill == CardSkillManager.SkillName.Taurus)
        {
            animator.Play("Larger");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Rock")) return;

        if (Control.Instance.currentSkill == CardSkillManager.SkillName.Aries)
        {
            Rock rock = collision.GetComponent<Rock>();
            if (rock != null)
            {
                // Chuẩn hóa Vector hướng đẩy (normalized)
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                float pushForce = 20f; // Độ mạnh cú hất

                rock.ApplyAriesPush(pushDirection, pushForce);
            }
        }
        else if (Control.Instance.currentSkill == CardSkillManager.SkillName.Taurus)
        {
            Destroy(collision.gameObject);
        }
    }
}