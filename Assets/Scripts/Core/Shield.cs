using UnityEngine;

public class Shield : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.Play("Long");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Control.Instance.currentSkill == CardSkillManager.SkillName.Cancer)
        {
            if (collision.CompareTag("Rock"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
