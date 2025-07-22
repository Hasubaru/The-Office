using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float moveSpeed = 3f;
    public float maxStamina = 100f;
    public float currentStamina;
    private PlayerStats stats;

    public bool isWorking = false;

    private Vector2 input;
    private Rigidbody2D rb;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (isWorking)
        {
            // tính toán theo thời gian thực tế hoặc thời gian trong game
            float efficiency = PlayerStats.Instance.GetWorkEfficiency(); // giả định mỗi phút làm tăng 1 điểm
        }
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void ReduceStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina <= 0)
        {
            Debug.Log("Bất tỉnh! Nghỉ 1 ngày...");
            currentStamina = 0;
            GameManager.Instance.AdvanceDay(); // đơn giản hóa giai đoạn đầu
        }
    }

    public void RestoreStamina() => currentStamina = maxStamina;
}
