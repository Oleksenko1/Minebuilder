using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HotbarItem : MonoBehaviour
{
    [SerializeField] private GameObject selectIconSprite;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI hotkeyTxt;

    private void Awake()
    {
        DeselectItem();
    }
    public void SetIconSprite(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
    public void SetHotkeyText(string text)
    {
        hotkeyTxt.SetText(text);
    }
    public void SelectItem()
    {
        selectIconSprite.SetActive(true);
    }
    public void DeselectItem()
    {
        selectIconSprite.SetActive(false);
    }
}
