using UnityEngine;

public class Rock : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float fallSpeed = 10f;
    public bool isFall;
    // Update is called once per frame
    void Update()
    {
        if(isFall)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * fallSpeed * Time.deltaTime);
        }
        if (transform.position.y < -10f || transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }
}
