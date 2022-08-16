//ArmorBarManager.cs
/* Manages a armor bar.
 * 
 * Scripted by Simone Siragusa 306067 @ PoliTO | Game Design & Gamification Exam
 */

// Ref: https://www.youtube.com/watch?v=BLfNP4Sc_iA utile per ampliare e mettere il gradiente
using UnityEngine;
using UnityEngine.UI;

public class ArmorBarManager : MonoBehaviour, IBarManageable
{
    [SerializeField] private Slider slider;

    public void SetValue(float value)
    {
        slider.value = value;
    }

    private void Awake()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
    }

    public void SetMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }
}
