//HealthBarManager.cs
/* Manages a health bar.
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 */

// Ref: https://www.youtube.com/watch?v=BLfNP4Sc_iA utile per ampliare e mettere il gradiente
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    private void Awake()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
