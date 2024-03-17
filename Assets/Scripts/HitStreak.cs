using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitStreak : MonoBehaviour
{
    [SerializeField] float comboIncreasePerHit = .25f;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] float baseMultiplier = 1f;
    int combo = 0;

    public void BulletDestruction(bool wasHit)
    {
        if(wasHit)
        {
            combo++;
        }
        else
        {
            combo = 0;
        }
        UpdateComboText();
    }

    public float GetMultiplier()
    {
        return baseMultiplier + comboIncreasePerHit*combo;
    }

    void UpdateComboText()
    {
        comboText.text = "Combo: " + combo;
    }
}
