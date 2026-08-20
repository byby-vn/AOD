using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class Control : MonoBehaviour
{
    [Header("Cấu hình nhảy")]
    public float up = 7f; //lực nhảy
    public float down = -1f;
    public float left = -7f;
    public float right = 1f;
    public float timeSkill = 3f; 
    private Rigidbody2D rb;
    private Camera mainCamera;
    private float objectWidth;
    private float objectHeight;
    [Header("Độ mượt (Càng cao càng nhanh trở về trạng thái trôi)")]
    public float smoothSpeed = 3f;
    [Header("Text")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI timeText;
    [Header("Skill")]
    public SpriteRenderer cardFront;
    public SpriteRenderer cardBack;
    public Animator cardAnimator;
    private float timer;
    private bool isUsingSkill;
    private bool isHaveSkill;
    private CardSkillManager.SkillName currentSkill = CardSkillManager.SkillName.None;
    private bool isLose = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        mainCamera = Camera.main;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        gameOverPanel.SetActive(false);
        if(spriteRenderer!=null) //lấy kích thước tên lửa
        {
            objectWidth = spriteRenderer.bounds.extents.x;
            objectHeight= spriteRenderer.bounds.extents.y;
        }
        cardFront.gameObject.SetActive(false);
        cardBack.gameObject.SetActive(false); 
        cardAnimator.gameObject.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;
        int min = Mathf.FloorToInt(timer/60f);
        int sec = Mathf.FloorToInt(timer%60f);
        int frac = Mathf.FloorToInt((timer * 100f) % 100f);
        timeText.text = string.Format("{0:00}m {1:00}s {2:00}'", min, sec, frac);
        if(isLose)
        {
            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if(Keyboard.current.sKey.wasPressedThisFrame && isUsingSkill == false && isHaveSkill == true)
        {
            isHaveSkill = false;
           StartCoroutine(ActiveSkill());
        }
        float current_X = rb.linearVelocity.x;
        float current_Y = rb.linearVelocity.y;
        if(Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            current_Y = up; //bay lên
            transform.rotation = Quaternion.Euler(0, 0, 0f);
        }
        else
        {
            current_Y = Mathf.Lerp(current_Y, down, Time.deltaTime * smoothSpeed);
        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            current_X = left; // Ép ngay lập tức đi sang trái
            transform.rotation = Quaternion.Euler(0, 0, 90f);
        }
        if(Keyboard.current.upArrowKey.wasPressedThisFrame && Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            transform.rotation = Quaternion.Euler(0,0,45f);
        }
        else
        {
            current_X = Mathf.Lerp(current_X, right, Time.deltaTime * smoothSpeed);
        }
        rb.linearVelocity = new Vector2(current_X,current_Y);
        ClampPositionToScreen();
    }
    void ClampPositionToScreen()
    {
        // Lấy tọa độ mép màn hình theo thế giới 2D
        Vector3 minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(0,0.22f,mainCamera.nearClipPlane));
        Vector3 maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(0.99f, 1, mainCamera.nearClipPlane));
        // Vị trí giới hạn
        float minX = minScreenBounds.x + objectWidth;
        float maxX = maxScreenBounds.x - objectWidth;
        float minY = minScreenBounds.y + objectHeight;
        float maxY = maxScreenBounds.y - objectHeight;
        // Lấy vị trí hiện tại
        Vector3 clampedPosition = transform.position;
        // Khóa tọa độ X và Y không cho vượt quá mép màn hình
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        // Áp dụng vị trí mới
        transform.position = clampedPosition; 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Rock"))
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            isLose = true;
        } //ăn đá là thua
        if(collision.gameObject.CompareTag("Card"))
        {
            Skill card = collision.gameObject.GetComponent<Skill>();
            isHaveSkill = true;
            currentSkill = card.skillType; // Lưu lại chòm sao lá bài vừa ăn
            Debug.Log("<color=yellow>Player đã ăn lá bài:</color> " + currentSkill);
        } //ăn skill nhận skill
    }
    private IEnumerator ActiveSkill()
    {
        isUsingSkill = true;
        cardAnimator.gameObject.SetActive(true);
        CardSkillManager.Instance.ActiveSkill(currentSkill);
        cardAnimator.Play("Flip");
        Debug.Log("TimeSkill là: " + timeSkill);
        yield return new WaitForSeconds(timeSkill);
        cardAnimator.Play("FlipBack");
        yield return new WaitForSeconds(0.2f);
        CardSkillManager.Instance.EndSkill(currentSkill);
        currentSkill = CardSkillManager.SkillName.None;
        yield return new WaitForSeconds(0.3f);
        // Ẩn lá bài đi và mở lại khả năng bấm S
        cardAnimator.gameObject.SetActive(false);
        isUsingSkill = false;
    }
}
