using UnityEngine;

public class Chibaku_tensei : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("Rock"))
        {
            Rock rock = collider.GetComponent<Rock>();
            rock.isFall = true; //cho đá rơi sau khi wave biến mất
        }
    }
}
