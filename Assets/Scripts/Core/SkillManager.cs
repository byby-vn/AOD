using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class CardSkillManager : MonoBehaviour
{
    // Singleton pattern để Player/Spawner dễ dàng truy cập
    public static CardSkillManager Instance { get; private set; }
    public GameObject rocket;
    public GameObject shockwavePrefab;
    public GameObject shieldPrefab;
    private GameObject shockwave;
    private GameObject shield;
    private GameObject dash;
    [Header("Leo Skill Setup")]
    public GameObject dashTrailPrefab;
    public float dashDistance = 5f;
    public enum SkillName
    {
        Aries,      // Bạch Dương
        Taurus,     // Kim Ngưu
        Gemini,     // Song Tử
        Cancer,     // Cự Giải
        Leo,        // Sư Tử
        Virgo,      // Xử Nữ
        Libra,      // Thiên Bình
        Scorpio,    // Bọ Cạp 
        Sagittarius,// Nhân Mã
        Capricorn,  // Ma Kết
        Aquarius,   // Bảo Bình
        Pisces,     // Song Ngư
        Ophiuchus,   // Xà Phu
        None
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Hàm Router điều hướng kích hoạt skill
    public void ActiveSkill(SkillName skillName, Vector2 dir = default)
    {
        Debug.Log("Kich hoat skill: " + skillName.ToString());

        switch (skillName)
        {
            case SkillName.Aries:
                ExecuteAriesSkill();
                break;

            case SkillName.Taurus:
                ExecuteTaurusSkill();
                break;

            case SkillName.Gemini:
                ExecuteGeminiSkill();
                break;

            case SkillName.Cancer:
                ExecuteCancerSkill(dir);
                break;

            case SkillName.Leo:
                ExecuteLeoSkill(dir);
                break;

            case SkillName.Virgo:
                ExecuteVirgoSkill();
                break;

            case SkillName.Libra:
                ExecuteLibraSkill();
                break;

            case SkillName.Scorpio:
                ExecuteScorpioSkill();
                break;

            case SkillName.Sagittarius:
                ExecuteSagittariusSkill();
                break;

            case SkillName.Capricorn:
                ExecuteCapricornSkill();
                break;

            case SkillName.Aquarius:
                ExecuteAquariusSkill();
                break;

            case SkillName.Pisces:
                ExecutePiscesSkill();
                break;

            case SkillName.Ophiuchus:
                ExecuteOphiuchusSkill();
                break;

            default:
                Debug.LogWarning("Khong tim thay skill phu hop!");
                break;
        }
    }
    public void EndSkill(SkillName skillName)
    {
        Debug.Log("Ket thuc skill: " + skillName.ToString());

        switch (skillName)
        {
            case SkillName.Aries:
                EndAriesSkill();
                break;

            case SkillName.Taurus:
                EndTaurusSkill();
                break;

            case SkillName.Gemini:
                EndGeminiSkill();
                break;

            case SkillName.Cancer:
                EndCancerSkill();
                break;

            case SkillName.Leo:
                // EndLeoSkill();
                break;

            case SkillName.Virgo:
                // EndVirgoSkill();
                break;

            case SkillName.Libra:
                // EndLibraSkill();
                break;

            case SkillName.Scorpio:
                // EndScorpioSkill();
                break;

            case SkillName.Sagittarius:
                // EndSagittariusSkill();
                break;

            case SkillName.Capricorn:
                // EndCapricornSkill();
                break;

            case SkillName.Aquarius:
                // EndAquariusSkill();
                break;

            case SkillName.Pisces:
                EndPiscesSkill();
                break;

            case SkillName.Ophiuchus:
                // EndOphiuchusSkill();
                break;

            default:
                Debug.LogWarning("Khong tim thay skill de ket thuc!");
                break;
        }
    }
    private void ExecuteAriesSkill()
    {
        shockwave = Instantiate(shockwavePrefab, rocket.transform);
        shockwave.transform.localPosition = Vector3.zero;
        Control.Instance.timeSkill = 5f;
    }

    private void ExecuteTaurusSkill()
    {
        shockwave = Instantiate(shockwavePrefab, rocket.transform);
        shockwave.transform.localPosition = Vector3.zero;
        Control.Instance.timeSkill = 1f;
    }

    private void ExecuteGeminiSkill()
    {
        Debug.Log("Đã kích hoạt skill Gemini");
        rocket.layer = LayerMask.NameToLayer("Invincible");
        Control.Instance.timeSkill = 1f;
        SpriteRenderer sr = rocket.GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0.4f;
        sr.color = c;
    }

    private void ExecuteCancerSkill(Vector2 dir)
    {
        if (dir == Vector2.up)
        {
            shield = Instantiate(shieldPrefab, new Vector3(0, 4, 0), Quaternion.identity);
        }
        if (dir == Vector2.left)
        {
            shield = Instantiate(shieldPrefab, new Vector3(-10, 3.5f, 0), Quaternion.Euler(0, 0, 90));
        }
        Control.Instance.timeSkill = 5f;
    }

    private void ExecuteLeoSkill(Vector2 dir)
    {
        // Đặt thời gian duy trì tổng của skill Leo (bao gồm thời gian chờ lướt lần 2)
        Control.Instance.timeSkill = 2f;

        StartCoroutine(LeoDashComboRoutine(dir));
    }

    private void ExecuteVirgoSkill()
    {
        // Logic cho skill Xử Nữ (Ví dụ: Hồi máu / Tăng điểm thưởng)
    }

    private void ExecuteLibraSkill()
    {
        // Logic cho skill Thiên Bình (Ví dụ: Cân bằng lại HP / Dọn dẹp map)
    }

    private void ExecuteScorpioSkill()
    {
        // Logic cho skill Bọ Cạp (Ví dụ: Bắn đạn độc gây sát thương liên tục)
    }

    private void ExecuteSagittariusSkill()
    {
        // Logic cho skill Nhân Mã (Ví dụ: Bắn mưa tên lửa/đạn tầm xa)
    }

    private void ExecuteCapricornSkill()
    {
        // Logic cho skill Ma Kết (Ví dụ: Đóng băng vật thể xung quanh)
    }

    private void ExecuteAquariusSkill()
    {
        // Logic cho skill Bảo Bình (Ví dụ: Tạo sóng nước đẩy lùi đá)
    }

    private void ExecutePiscesSkill()
    {
        Debug.Log("Đã kích hoạt skill Pisces");
        rocket.layer = LayerMask.NameToLayer("Invincible");
        Control.Instance.timeSkill = 5f;
        SpriteRenderer sr = rocket.GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0.4f;
        sr.color = c;
    }
    private void EndAriesSkill()
    {
        Animator shockwaveAnimator = shockwave.GetComponent<Animator>();
        shockwaveAnimator.Play("FadeOut");
        Destroy(shockwave, 0.3f);
    }
    private void EndTaurusSkill()
    {
        Animator shockwaveAnimator = shockwave.GetComponent<Animator>();
        Destroy(shockwave, 0.3f);
    }
    private void EndGeminiSkill()
    {
        SpriteRenderer sr = rocket.GetComponent<SpriteRenderer>();
        rocket.layer = LayerMask.NameToLayer("Rocket");
        Color c = sr.color;
        if (sr != null)
        {
            c = sr.color;
            c.a = 1f;
            sr.color = c;
        }
    }
    private void EndCancerSkill()
    {
        Animator shieldAnimator = shield.GetComponent<Animator>();
        shieldAnimator.Play("Short");
        Destroy(shield, 0.3f);
    }
    private void EndLeoSkill()
    {
        // Dọn dẹp trạng thái visual/effect của Leo nếu có
        Debug.Log("Kết thúc Skill Leo (The Strength)");
        Animator dashAnimator = dash.GetComponent<Animator>();
        dashAnimator.Play("DashTrail_Shrink");
        rocket.layer = LayerMask.NameToLayer("Rocket");//tắt bất tử
        Destroy(dash, 0.3f);
    }
    private void EndPiscesSkill()
    {
        SpriteRenderer sr = rocket.GetComponent<SpriteRenderer>();
        rocket.layer = LayerMask.NameToLayer("Rocket");
        Color c = sr.color;
        if (sr != null)
        {
            c = sr.color;
            c.a = 1f;
            sr.color = c;
        }
    }
    private void ExecuteOphiuchusSkill()
    {
        // Logic cho skill Xà Phu (Ví dụ: Skill ẩn cực mạnh/Xóa toàn bộ đá trên màn hình)
    }
    private IEnumerator LeoDashComboRoutine(Vector2 initialDir)
    {
        int n_dash = 2;
        Vector2 currentDir = initialDir;
        Animator rocketAnim = rocket.GetComponent<Animator>();

        while (n_dash > 0)
        {
            // 1. Tính toán vị trí đích CHO MỖI LẦN LƯỚT
            Vector3 startPos = rocket.transform.position;
            Vector3 targetPos = startPos + (Vector3)(currentDir * dashDistance);

            Camera cam = Control.Instance.mainCamera;
            Vector3 minBounds = cam.ViewportToWorldPoint(new Vector3(0, 0.22f, cam.nearClipPlane));
            Vector3 maxBounds = cam.ViewportToWorldPoint(new Vector3(0.99f, 1, cam.nearClipPlane));
            targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x + Control.Instance.objectWidth, maxBounds.x - Control.Instance.objectWidth);
            targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y + Control.Instance.objectHeight, maxBounds.y - Control.Instance.objectHeight);

            Control.Instance.pendingDashTargetPos = targetPos;

            // 2. Spawn Vệt Lướt & Play FadeOut trên Tên Lửa
            SpawnDashTrail(currentDir);
            rocket.layer = LayerMask.NameToLayer("Invincible"); //buff bất tử khi lướt tránh chết oan
            if (rocketAnim != null) rocketAnim.Play("Player_FadeOut");

            // 3. Chờ Frame 5 dịch chuyển và hiện lại Tên Lửa
            yield return new WaitForSeconds(0.25f);
            if (rocketAnim != null) rocketAnim.Play("Player_FadeIn");

            // 4. Thu dọn Trail của lượt lướt này
            ShrinkCurrentDashTrail();

            n_dash--;
            if (n_dash <= 0) break; // Lướt xong 2 lần -> Thoát lặp

            // === CỬA SỔ CHỜ BẤM LƯỚT LẦN 2 (1.0 Giây) ===
            float waitTimer = 0f;
            bool hasPressedSecond = false;

            while (waitTimer < 2.0f)
            {
                bool up = Keyboard.current.upArrowKey.wasPressedThisFrame;
                bool left = Keyboard.current.leftArrowKey.wasPressedThisFrame;

                if (up && left)
                {
                    currentDir = new Vector2(-1f, 1f).normalized;
                    hasPressedSecond = true; // SỬA BUG: Bắt buộc gán true
                    break;
                }
                else if (up)
                {
                    currentDir = Vector2.up;
                    hasPressedSecond = true; // SỬA BUG: Bắt buộc gán true
                    break;
                }
                else if (left)
                {
                    currentDir = Vector2.left;
                    hasPressedSecond = true; // SỬA BUG: Bắt buộc gán true
                    break;
                }

                waitTimer += Time.deltaTime;
                yield return null;
            }

            // Nếu người chơi bỏ qua không bấm lần 2 -> Dừng combo
            if (!hasPressedSecond) break;
        }
    }
    public void SpawnDashTrail(Vector2 dir)
    {
        // 1. Lấy vị trí hiện tại của Player (Rocket)
        Vector3 spawnPosition = rocket.transform.position;

        // 2. Tính góc xoay (độ) từ Vector2 dir
        // Mathf.Atan2 trả về radian -> Nhân Mathf.Rad2Deg để đổi sang độ
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 3. Quy đổi góc theo hệ tọa độ của game:
        // Mặc định: Vector2.right (1,0) = 0° | Vector2.up (0,1) = 90° | Vector2.left (-1,0) = 180°
        // Để khớp với yêu cầu của bạn (Up = 0°, Up+Left = 45°, Left = 90°), ta trừ đi 90 độ:
        float customAngle = angle - 90f;
        // 4. Instantiate Prefab tại vị trí Player với góc xoay Z đã tính
        dash = Instantiate(dashTrailPrefab, spawnPosition, Quaternion.Euler(0, 0, customAngle));
    }
    private void ShrinkCurrentDashTrail()
    {
        if (dash != null)
        {
            Animator dashAnimator = dash.GetComponent<Animator>();
            if (dashAnimator != null) dashAnimator.Play("DashTrail_Shrink");
            rocket.layer = LayerMask.NameToLayer("Rocket");
            Destroy(dash, 0.3f);
        }
    }
}