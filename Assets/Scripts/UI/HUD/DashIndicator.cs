using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Slider dashCooldownSlider;

    private void Start()
    {
        dashCooldownSlider.minValue = 0;
        dashCooldownSlider.maxValue = 1;
        dashCooldownSlider.value = 0;
    }

    private void Update()
    {
        UpdateCooldownSlider();
    }

    private void UpdateCooldownSlider()
    {
        float timeSinceLastDash = Time.time - playerMovement.lastDashTime;

        if (timeSinceLastDash < playerMovement.dashCooldown)
        {
            dashCooldownSlider.value = 1 - (timeSinceLastDash / playerMovement.dashCooldown);
        }
        else
        {
            dashCooldownSlider.value = 0;
        }
    }
}
