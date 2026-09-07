using UnityEngine;

public class SagittariusRocket : MonoBehaviour
{
    float flySpeed = 10f;
    void Update()
    {
        transform.Translate(Vector3.up * flySpeed * Time.deltaTime);
        if (transform.position.y < -15f || transform.position.x > 15f || transform.position.y > 15f || transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject,0.1f);
        }
    }
}
