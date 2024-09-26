using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] TextMeshProUGUI timeText;
    public void HpChange(float maxHp, float hp)
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = hp;
    }
    public void TimeUpdate(float sec, float min)
    {
        timeText.text = $"{min}:{sec.ToString("00")}";
    }
}
