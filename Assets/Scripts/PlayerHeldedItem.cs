using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeldedItem : MonoBehaviour
{
    [SerializeField] private HotbarManager hotbarManager;
    [SerializeField] private PlayerBuildSystem buildSystem;
    [SerializeField] private Transform handParent;
    [SerializeField] private Animator handAnimator;

    private Transform itemTransform;
    private void Awake()
    {
        hotbarManager.OnItemChanged += SelectNewItem;

        buildSystem.OnBlockBreak += () => {
            handAnimator.SetTrigger("BreakBlock");
        };

        buildSystem.OnBlockPlaced += () => {
            handAnimator.SetTrigger("UseItem");
        };
    }

    public void SelectNewItem()
    {
        // Destroys previous item
        if(itemTransform != null)
        {
            Destroy(itemTransform.gameObject);
        }

        BlockSO currentItem;
        currentItem = hotbarManager.GetCurrentItem();

        itemTransform = Instantiate(currentItem.prefab.transform, handParent);
        itemTransform.localScale *= 0.1f;

        itemTransform.GetComponent<Collider>().enabled = false; // Turns off collisions of new item;
        itemTransform.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; // Turns off shadows
        itemTransform.gameObject.isStatic = false; // Turns off static property of item

        handAnimator.SetTrigger("ChangeItem");
    }
}
