using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject devilPrefab;
    public GameObject rocket;
    public List<GameObject> rockInRadar = new List<GameObject>();
    public List<LittleDevil> devilList = new List<LittleDevil>();
    private BoxCollider2D boxCollider;
    public Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        FitToScreen();
    }
    void FitToScreen()
    {
        if (mainCamera == null) return;

        // 1. Tính ranh giới màn hình thế giới 2D
        Vector3 minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // 2. Tính Chiều Rộng và Chiều Cao thực tế của Camera
        float screenWidth = maxBounds.x - minBounds.x;
        float screenHeight = maxBounds.y - minBounds.y;

        // 3. Đưa Radar về đúng tâm màn hình (Center Point)
        transform.position = new Vector3((minBounds.x + maxBounds.x) / 2f, (minBounds.y + maxBounds.y) / 2f, 0f);

        // 4. Đổi trực tiếp Local Scale của Transform theo kích thước màn hình
        transform.localScale = new Vector3(screenWidth, screenHeight, 1f);

        // 5. Chuẩn hóa BoxCollider2D về Size (1, 1) để khớp 100% với Transform Scale mới
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(1f, 1f);
            boxCollider.isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Rock"))
        {
            if (!rockInRadar.Contains(collider.gameObject))
            {
                rockInRadar.Add(collider.gameObject);
                rockInRadar.RemoveAll(x => x == null); //xóa đá bị phá bởi skill khác 
            }
            if (devilList.Count > 0)
            {
                devilList.RemoveAll(x => x == null);
                LittleDevil devilToLaunch = devilList[0];
                devilList.RemoveAt(0);
                // Gán đá cho viên đạn đó lao tới
                devilToLaunch.AssignTarget(collider.transform);

                // Chia lại góc cho những viên đạn còn lại đang xoay
                ReorderDevils();
            }
        }

    }
    private void OnTriggeExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Rock") && rockInRadar.Contains(collider.gameObject))
        {
            rockInRadar.Remove(collider.gameObject);
            rockInRadar.RemoveAll(x => x == null);
        }
    }
    public int DestroyRockInRadar()
    {
        int count = rockInRadar.Count;
        foreach (GameObject rock in rockInRadar)
        {
            Destroy(rock);
        }
        rockInRadar.Clear();
        Debug.Log("Đã phá "+ count + "Đá");
        return count;
    }
    public void SpawnDevil(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject dev = Instantiate(devilPrefab, rocket.transform.position, Quaternion.identity);
            LittleDevil devilScript = dev.GetComponent<LittleDevil>();
            devilList.Add(devilScript);
            ReorderDevils(); //chia khoảng cách lại cho đẹp
        }

    }
    public void ReorderDevils()
    {
        devilList.RemoveAll(x => x == null);
        int count = devilList.Count;
        if (count == 0) return;
        float angleStep = 360f / count;
        for (int i = 0; i < count; i++)
        {
            devilList[i].orbitAngle = i * angleStep;
        }
    }
}
