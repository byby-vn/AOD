using UnityEngine;

public class LeoDash : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("DashTrail_Stretch");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
        {
            Destroy(collision.gameObject);
        }
    }
}
