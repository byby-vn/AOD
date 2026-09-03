using Unity.VisualScripting;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float fallSpeed = 10f;
    private float nowSpeed;
    private Rigidbody2D rigidbody2D;
    public bool isFall;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isFall)
        {
            if (Control.Instance.currentSkill == CardSkillManager.SkillName.Libra && Control.Instance.isUsingSkill == true)
            {
                rigidbody2D.gravityScale = -1;
                nowSpeed = -fallSpeed;
            }
            else
            {
                rigidbody2D.gravityScale = 1;
                nowSpeed = fallSpeed;
            }
            transform.Translate(Vector3.down * nowSpeed * Time.deltaTime);
        }
        else
        {
            rigidbody2D.gravityScale = 0;
            if (Control.Instance.currentSkill == CardSkillManager.SkillName.Libra && Control.Instance.isUsingSkill == true)
            {
                nowSpeed = -fallSpeed;
            }
            else
            {
                nowSpeed = fallSpeed;
            }
            transform.Translate(Vector3.right * nowSpeed * Time.deltaTime);
        }
        if (transform.position.y < -15f || transform.position.x > 15f || transform.position.y > 15f || transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
