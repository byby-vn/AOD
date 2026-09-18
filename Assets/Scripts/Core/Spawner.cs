using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject RockPrefab;
    public GameObject cardPrefab;
    public GameObject warning_up;
    public GameObject warning_left;
    public GameObject rocketPrefab;

    [Header("Wave Settings")]
    public float TimeSpawnARock = 0.5f; // Tốc độ spawn đá (mấy giây/cục)
    public float TimeWarning = 2f;      // Thời gian hiện cảnh báo
    public float waveTime = 10f;        // Thời gian mưa đá kéo dài
    public float timeWait = 3f;         // Thời gian nghỉ giữa các wave

    [Header("Skill Settings")]
    public float TimeSpawnARocket = 0.5f;

    // Timers
    private float TimeSpawnACard;
    private float WarningTimer;
    private float timer;
    private float waveTimer;
    private float rocketTimer;
    private float CardTimer;

    // States
    private bool isTimeSpawn = false; // Mặc định nghỉ trước khi vào Wave 1
    private bool spawnOnTop;

    private Camera mainCamera;
    private Vector3 maxBounds, minBounds;

    void Start()
    {
        mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.23f, mainCamera.nearClipPlane));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        TimeSpawnACard = Random.Range(10, 20);
        ResetWarningUI();
        spawnOnTop = Random.value > 0.5f; // Random hướng rơi cho Wave 1
    }

    void Update()
    {
        HandleRockWave();
        HandleCardSpawn();
        HandleSagittariusSkill();
    }

    // --- QUẢN LÝ LUỒNG WAVE MƯA ĐÁ ---
    private void HandleRockWave()
    {
        if (isTimeSpawn)
        {
            WarningTimer += Time.deltaTime;

            // Phase 1: Cảnh báo
            if (WarningTimer <= TimeWarning)
            {
                ShowWarningUI();
            }
            // Phase 2: Mưa đá rơi
            else if (waveTimer < waveTime)
            {
                ResetWarningUI(); // Tắt cảnh báo khi đá bắt đầu rơi

                waveTimer += Time.deltaTime;
                timer += Time.deltaTime;

                if (timer >= TimeSpawnARock)
                {
                    if (spawnOnTop) SpawnRockTop();
                    else SpawnRockLeft();

                    timer = 0f;
                }
            }
            // Phase 3: Kết thúc Wave -> Chuyển sang thời gian nghỉ
            else
            {
                isTimeSpawn = false;
                timer = 0f; // Reset dùng làm đếm giờ nghỉ
            }
        }
        else // Trạng thái nghỉ (Wait Time)
        {
            timer += Time.deltaTime;
            if (timer >= timeWait)
            {
                ResetWaveState();
            }
        }
    }

    // --- QUẢN LÝ SPAWN THẺ SKILL ---
    private void HandleCardSpawn()
    {
        CardTimer += Time.deltaTime;
        if (CardTimer >= TimeSpawnACard)
        {
            bool spawnCardOnTop = Random.value > 0.5f;
            if (spawnCardOnTop) SpawnSkillTop();
            else SpawnSkillLeft();

            CardTimer = 0f;
            TimeSpawnACard = Random.Range(10, 20);
        }
    }

    // --- QUẢN LÝ SKILL SAGITTARIUS (TÊN LỬA) ---
    private void HandleSagittariusSkill()
    {
        if (Control.Instance.currentSkill == CardSkillManager.SkillName.Sagittarius && Control.Instance.isUsingSkill)
        {
            rocketTimer += Time.deltaTime;
            if (rocketTimer >= TimeSpawnARocket)
            {
                SpawnRocketDown();
                rocketTimer = 0f;
            }
        }
    }

    // --- HÀM BỔ TRỢ RESET STATE ---
    private void ResetWaveState()
    {
        waveTimer = 0f;
        timer = 0f;
        WarningTimer = 0f;
        isTimeSpawn = true;
        spawnOnTop = Random.value > 0.5f; // Random hướng mới cho Wave tiếp theo
    }

    private void ShowWarningUI()
    {
        if (spawnOnTop)
        {
            warning_up.SetActive(true);
            warning_up.GetComponent<Animator>().Play("Warning");
        }
        else
        {
            warning_left.SetActive(true);
            warning_left.GetComponent<Animator>().Play("Warning");
        }
    }

    private void ResetWarningUI()
    {
        if (warning_up.activeSelf) warning_up.SetActive(false);
        if (warning_left.activeSelf) warning_left.SetActive(false);
    }

    // --- CÁC HÀM SPAWN OBJECT ---
    private void SpawnRockTop()
    {
        if (RockPrefab == null) return;

        float randomX = Random.Range(minBounds.x + 0.5f, maxBounds.x - 0.5f);
        Vector3 spawnPosition = new Vector3(randomX, maxBounds.y + 1f, 0f);

        GameObject newRock = Instantiate(RockPrefab, spawnPosition, Quaternion.identity);
        Rock rockScript = newRock.GetComponent<Rock>();
        if (rockScript != null) rockScript.isFall = true;
    }

    private void SpawnRockLeft()
    {
        if (RockPrefab == null) return;

        float randomY = Random.Range(minBounds.y + 0.5f, maxBounds.y - 0.5f);
        Vector3 spawnPosition = new Vector3(minBounds.x - 1f, randomY, 0f);

        GameObject newRock = Instantiate(RockPrefab, spawnPosition, Quaternion.identity);
        Rock rockScript = newRock.GetComponent<Rock>();
        if (rockScript != null) rockScript.isFall = false;
    }

    private void SpawnRocketDown()
    {
        if (rocketPrefab == null) return;

        float randomX = Random.Range(minBounds.x + 0.5f, maxBounds.x - 0.5f);
        Vector3 spawnPosition = new Vector3(randomX, minBounds.y - 1f, 0f);
        Instantiate(rocketPrefab, spawnPosition, Quaternion.identity);
    }

    private void SpawnSkillTop()
    {
        float randomX = Random.Range(minBounds.x + 0.5f, maxBounds.x - 0.5f);
        SpawnSkillCard(new Vector3(randomX, maxBounds.y + 1f, 0f), true);
    }

    private void SpawnSkillLeft()
    {
        float randomY = Random.Range(minBounds.y + 0.5f, maxBounds.y - 0.5f);
        SpawnSkillCard(new Vector3(minBounds.x - 1f, randomY, 0f), false);
    }

    private void SpawnSkillCard(Vector3 spawnPos, bool isFalling)
    {
        if (cardPrefab == null) return;

        GameObject newSkill = Instantiate(cardPrefab, spawnPos, Quaternion.identity);
        CardSkillManager.SkillName randomSkill = (CardSkillManager.SkillName)Random.Range(0, System.Enum.GetValues(typeof(CardSkillManager.SkillName)).Length - 1);
        newSkill.name = "Card_" + randomSkill.ToString();

        Skill CardScript = newSkill.GetComponent<Skill>();
        if (CardScript != null)
        {
            CardScript.isFall = isFalling;
            CardScript.skillType = randomSkill;
            CardTimer = 0;
            CardScript.ApplyCardColor();
        }
    }
}