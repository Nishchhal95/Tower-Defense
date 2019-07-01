using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    private Image fillerImage;

    private void Awake()
    {
        fillerImage = transform.Find("Canvas").Find("ConstructionBar").Find("BG").Find("Filler").GetComponent<Image>();
    }

    public void UpdateConstructionProgressBar(float fillAmount)
    {
        fillerImage.fillAmount = fillAmount;
    }
}
