using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMoveModeIndicator : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Sprite walkModeImage;
    [SerializeField] private Sprite flightModeImage;
    [Header("Components")]
    [SerializeField] private PlayerMovement playerMovement;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = walkModeImage;

        playerMovement.OnMoveModeChanged += ChangeMode;
    }
    public void ChangeMode(bool isFlight)
    {
        Sprite newSprite = isFlight ? flightModeImage : walkModeImage;

        image.sprite = newSprite;
    }
}
