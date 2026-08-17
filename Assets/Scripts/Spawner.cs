using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    public GameObject RockPrefab;
    public GameObject cardPrefab;
    public float TimeSpawnARock;
    public float TimeSpawnACard;
    public float circleSpawn;
    public float timeWait;

    private float timer;
    private int wave = 0;
    private bool isTimeSpawn = true;
    private float CardTimer;
    private Camera mainCamera;
    Vector3 maxBounds, minBounds;

    void Start()
    {
        mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
    }

    void Update()
    {
        timer += Time.deltaTime;
        CardTimer += Time.deltaTime;

        // Khi đủ thời gian thì mới kiểm tra vị trí và sinh Rock
        if (timer >= TimeSpawnARock && isTimeSpawn)
        {
            bool spawnOnTop = Random.value > 0.5f;
            wave++;
            if (wave <= circleSpawn)
            {
                if (spawnOnTop)
                {
                    SpawnRockTop();
                }
                else
                {
                    SpawnRockLeft();
                }
                timer = 0f; // Reset đếm thời gian spawn Rock
            }
            if (wave == circleSpawn)
            {
                isTimeSpawn = false;
            }
        }
        if (!isTimeSpawn)
        {
            if (timer >= timeWait)
            {
                wave = 0;
                isTimeSpawn = true;
                timer = 0;
            }
        }
        if (CardTimer >= TimeSpawnACard)
        {
            bool spawnCardOnTop = Random.value > 0.5f;
            if (spawnCardOnTop)
            {
                SpawnSkillTop();
            }
            else
            {
                SpawnSkillLeft();
            }
            CardTimer = 0f; // Reset đếm thời gian spawn Card
        }
    }

    void SpawnRockTop()
    {
        if (RockPrefab == null) return;

        float randomX = Random.Range(minBounds.x + 0.5f, maxBounds.x - 0.5f);
        float spawnY = maxBounds.y + 1f;
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        GameObject newRock = Instantiate(RockPrefab, spawnPosition, Quaternion.identity);
        Rock rockScript = newRock.GetComponent<Rock>();
        if (rockScript != null) rockScript.isFall = true;
    }
    void SpawnSkillTop()
    {
        float randomX = Random.Range(minBounds.x + 0.5f, maxBounds.x - 0.5f);
        float spawnY = maxBounds.y + 1f;
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
        SpawnSkillCard(spawnPosition, true);
    
    }
    void SpawnRockLeft()
    {
        if (RockPrefab == null) return;

        float randomY = Random.Range(minBounds.y + 0.5f, maxBounds.y - 0.5f);
        float spawnX = minBounds.x - 1f;
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);

        GameObject newRock = Instantiate(RockPrefab, spawnPosition, Quaternion.identity);
        Rock rockScript = newRock.GetComponent<Rock>();
        Rigidbody2D rb = newRock.GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = 0f;
        if (rockScript != null) rockScript.isFall = false;

    }
    void SpawnSkillLeft()
    {
        float randomY = Random.Range(minBounds.y + 0.5f, maxBounds.y - 0.5f);
        float spawnX = minBounds.x - 1f;
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);
        SpawnSkillCard(spawnPosition, false);
    }
    // Hàm phụ trách khởi tạo Card, random chòm sao và cập nhật màu sắc
    void SpawnSkillCard(Vector3 spawnPos, bool isFalling)
    {
        if (cardPrefab == null) return;

        GameObject newSkill = Instantiate(cardPrefab, spawnPos, Quaternion.identity);

        // Lấy 1 chòm sao ngẫu nhiên từ Enum trong CardSkillManager
        CardSkillManager.SkillName randomSkill = (CardSkillManager.SkillName)Random.Range(0, System.Enum.GetValues(typeof(CardSkillManager.SkillName)).Length);

        // Đổi tên object (ví dụ: "Card_Pisces")
        newSkill.name = "Card_" + randomSkill.ToString();

        // Gán thông số và kích hoạt hàm đổi màu theo tên
        Skill CardScript = newSkill.GetComponent<Skill>();
        if (CardScript != null)
        {
            CardScript.isFall = isFalling;
            CardScript.skillType = randomSkill;
            CardTimer = 0;
            CardScript.ApplyCardColor(); // Ép tự cập nhật màu sắc ngay sau khi đổi tên
        }
    }
}