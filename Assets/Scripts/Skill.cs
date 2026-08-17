using UnityEngine;

public class Skill : MonoBehaviour
{
    [Header("Cấu hình lá bài")]
    public CardSkillManager.SkillName skillType;

    [Header("Tốc độ rơi")]
    public float fallSpeed = 1f;
    public bool isFall;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Tự kiểm tra tên của chính nó và đổi màu tương ứng
        ApplyCardColor();
    }
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
        if (transform.position.y < -11f || transform.position.x > 11f)
        {
            Destroy(gameObject);
        }
    }
    public void ApplyCardColor()
    {
        if (spriteRenderer == null) return;

        switch (skillType)
        {
            case CardSkillManager.SkillName.Aries:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#FF3333", out Color cAries) ? cAries : Color.red;
                break;
            case CardSkillManager.SkillName.Taurus:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#4CAF50", out Color cTaurus) ? cTaurus : Color.green;
                break;
            case CardSkillManager.SkillName.Gemini:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#FFEB3B", out Color cGemini) ? cGemini : Color.yellow;
                break;
            case CardSkillManager.SkillName.Cancer:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#2196F3", out Color cCancer) ? cCancer : Color.blue;
                break;
            case CardSkillManager.SkillName.Leo:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#FF9800", out Color cLeo) ? cLeo : Color.orange;
                break;
            case CardSkillManager.SkillName.Virgo:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#8BC34A", out Color cVirgo) ? cVirgo : Color.green;
                break;
            case CardSkillManager.SkillName.Libra:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#E91E63", out Color cLibra) ? cLibra : Color.magenta;
                break;
            case CardSkillManager.SkillName.Scorpio:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#9C27B0", out Color cScorpio) ? cScorpio : Color.purple;
                break;
            case CardSkillManager.SkillName.Sagittarius:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#FF5722", out Color cSag) ? cSag : Color.red;
                break;
            case CardSkillManager.SkillName.Capricorn:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#795548", out Color cCap) ? cCap : Color.gray;
                break;
            case CardSkillManager.SkillName.Aquarius:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#00BCD4", out Color cAqua) ? cAqua : Color.cyan;
                break;
            case CardSkillManager.SkillName.Pisces:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#03A9F4", out Color cPisces) ? cPisces : Color.cyan;
                break;
            case CardSkillManager.SkillName.Ophiuchus:
                spriteRenderer.color = ColorUtility.TryParseHtmlString("#00E676", out Color cOphi) ? cOphi : Color.green;
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Lá bài đã chạm vô người chơi");
            Destroy(gameObject);
        }
    }
}
