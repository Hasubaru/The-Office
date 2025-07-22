using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public Slider staminaSlider;
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player == null)
            Debug.LogError("❌ Không tìm thấy PlayerController trong scene");

        if (staminaSlider == null)
            Debug.LogError("❌ Slider chưa được gán vào StaminaUI");
    }

    void Update()
    {
        if (player != null && staminaSlider != null)
        {
            staminaSlider.maxValue = player.maxStamina;
            staminaSlider.value = player.currentStamina;
        }
        Image fillImage = staminaSlider.fillRect.GetComponent<Image>();
        if (player.currentStamina < player.maxStamina * 0.3f)
            fillImage.color = Color.red;
        else if (player.currentStamina < player.maxStamina * 0.6f)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.green;
    }
}
