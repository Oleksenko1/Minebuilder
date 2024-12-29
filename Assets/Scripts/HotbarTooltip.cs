using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
public class HotbarTooltip : MonoBehaviour
{
    [SerializeField] private RectTransform backgroundRect;
    [SerializeField] private RectTransform textRect;
    [Header("Time lengths")]
    [SerializeField] private float startTime = 0.2f;
    [SerializeField] private float pauseTime = 1f;
    [SerializeField] private float endTime = 1f;

    private Image background;
    private TextMeshProUGUI text;

    private Sequence backgroundSequence;
    private Sequence textSequence;

    // Black 25% transparent color
    private Color backgroundColor = new Color(0, 0, 0, 0.75f);
    private void Awake()
    {
        background = backgroundRect.GetComponent<Image>();
        text = textRect.GetComponent<TextMeshProUGUI>();

        background.color = Color.clear;

        text.color = Color.clear;
        text.autoSizeTextContainer = true;
    }
    public void UpdateTooltip(string newText)
    {
        // Kills existing tweens
        backgroundSequence.Kill();
        textSequence.Kill();

        // Updating text
        text.SetText(newText);

        // Force a layout update to get the correct text bounds
        Canvas.ForceUpdateCanvases();

        // Updating size of the background to a text width
        backgroundRect.sizeDelta = new Vector2(text.textBounds.size.x, text.textBounds.size.y);

        // Handling background animation
        backgroundSequence = DOTween.Sequence();
        backgroundSequence.Append(background.DOColor(backgroundColor, startTime));
        backgroundSequence.AppendInterval(pauseTime);
        backgroundSequence.Append(background.DOColor(Color.clear, endTime));

        // Handling text color animation
        textSequence = DOTween.Sequence();
        textSequence.Append(text.DOColor(Color.white, startTime));
        textSequence.AppendInterval(pauseTime);
        textSequence.Append(text.DOColor(Color.clear, endTime));
    }
}
