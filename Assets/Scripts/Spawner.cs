using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject RockPrefab;
    public float TimeSpawnARock = 1f;
    private float timer;
    private Camera mainCamera;
    public float waveDuration = 3f;
    Vector3 maxBounds, minBounds;
    void Start()
    {
        mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
    
    }
    void Update()
    {
        bool spawnOnTop = Random.value > 0.5f; //lớn hơn 0.5 là spawn trên top
        if(spawnOnTop)
        {
            float waveTimer = waveDuration;
            if(waveTimer>0)
            {
                timer += Time.deltaTime;
                if(timer>=TimeSpawnARock)
                {
                    SpawnRockTop();
                    waveTimer-=timer;
                    timer = 0f;
                }
            }

        }
        else
        {
            float waveTimer = waveDuration;
            if(waveTimer>0)
            {
                timer += Time.deltaTime;
                if(timer>=TimeSpawnARock)
                {
                    SpawnRockLeft();
                    waveTimer-=timer;
                    timer = 0f;
                }
            }
        }
        
    }
    void SpawnRockTop()
    {
        if(RockPrefab == null) return;
        
        // Chọn vị trí X ngẫu nhiên từ mép trái tới mép phải
        float randomX = Random.Range(minBounds.x + 0.5f, maxBounds.x - 0.5f);
        float spawnY = maxBounds.y + 1f;
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
        GameObject newRock = Instantiate(RockPrefab, spawnPosition, Quaternion.identity /*góc quay*/);
        Rock rockScript = newRock.GetComponent<Rock>();
        rockScript.isFall = true;

        // Vị trí Y xuất hiện ở phía trên đỉnh màn hình một chút
    }
    void SpawnRockLeft()
    {
        if(RockPrefab == null) return;
        float randomY = Random.Range(minBounds.y + 0.5f, maxBounds.y - 0.5f);
        float spawnX = minBounds.x -1f;
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);
        GameObject newRock = Instantiate(RockPrefab, spawnPosition, Quaternion.identity /*góc quay*/);
        Rock rockScript = newRock.GetComponent<Rock>();
        Rigidbody2D rb = newRock.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rockScript.isFall = false;
    }
}
