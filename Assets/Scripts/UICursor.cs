using UnityEngine;
using UnityEngine.UI;

public class UICursor : MonoBehaviour
{
    [SerializeField] private Material material;

    private void Awake()
    {
        GetComponent<Image>().material = material;
    }
}
