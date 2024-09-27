using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI killCounterText;
    public void HpChange(float maxHp, float hp)
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = hp;
    }
    public void TimeUpdate(float sec, float min)
    {
        timeText.text = $"{min}:{sec.ToString("00")}";
    }
    public void KillCounterUpdate(int count)
    {
        killCounterText.text = $"{count}";
    }
    public void EXPUpdate(float maxEXP, float EXP)
    {
        expSlider.maxValue = maxEXP;
        expSlider.value = EXP;
    }
}
