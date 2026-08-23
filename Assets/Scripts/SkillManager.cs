using System.Collections;
using UnityEngine;

public class CardSkillManager : MonoBehaviour
{
    // Singleton pattern để Player/Spawner dễ dàng truy cập
    public static CardSkillManager Instance { get; private set; }
    public GameObject rocket;
    public GameObject shockwavePrefab;
    private GameObject shockwave;
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
    public void ActiveSkill(SkillName skillName)
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
                ExecuteCancerSkill();
                break;

            case SkillName.Leo:
                ExecuteLeoSkill();
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
                // EndTaurusSkill();
                break;

            case SkillName.Gemini:
                // EndGeminiSkill();
                break;

            case SkillName.Cancer:
                // EndCancerSkill();
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
        // Logic cho skill Kim Ngưu (Ví dụ: Giáp kiên cố, miễn nhiễm sát thương)
    }

    private void ExecuteGeminiSkill()
    {
        // Logic cho skill Song Tử (Ví dụ: Phân thân tạo thêm 1 tên lửa giả)
    }

    private void ExecuteCancerSkill()
    {
        // Logic cho skill Cự Giải (Ví dụ: Tạo khiên bảo vệ)
    }

    private void ExecuteLeoSkill()
    {
        // Logic cho skill Sư Tử (Ví dụ: Bắt sóng gầm làm chậm hoặc phá hủy đá xung quanh)
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
}