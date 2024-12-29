using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class PlayerBuildSystem : MonoBehaviour
{
    public event Action OnBlockPlaced;
    public event Action OnBlockBreak;

    [Header("Stats")]
    [SerializeField] private float maxDistance;
    [Header("Components")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private HotbarManager itemsHotbar;
    [Space(15)]
    [SerializeField] private Transform selectedCubeOutline;
    

    [Inject] private GlobalMap globalMap;

    private Ray ray;
    RaycastHit hit;
    private void Awake()
    {
        PlayerInput.OnPlacePressed += PlaceBlock;
        PlayerInput.OnBreakPressed += BreakBlock;
    }
    private void Update()
    {
        CalculateRay();
    }
    private void PlaceBlock()
    {
        // Returns if there are no blocks in hit zone
        if (hit.transform == null) return;

        BlockSO currentBlock = itemsHotbar.GetCurrentItem();

        //// Checks if ray collided with something
        //if (Physics.Raycast(ray, out hit, maxDistance) == false) return;

        //// Checks if hitted object is block
        //if (hit.transform.CompareTag("Block") == false) return;

        Vector3 placePosition = RoundToGrid(hit.transform.position + hit.normal);

        // Define the size of the box to match the block size
        Vector3 boxSize = new Vector3(0.5f, currentBlock.height / 2.0f, 0.5f);

        // Checks if there are no obstacles in block place area
        if (!Physics.CheckBox(placePosition, boxSize / 2, Quaternion.identity))
        {
            // Checks if block is in the bounds of map
            if(globalMap.CheckForBounds(placePosition) == false)
            {
                Debug.Log("Out of bounds!!!");
                return;
            }

            Quaternion blockRotation = currentBlock.prefab.transform.rotation;

            if(currentBlock.isRotateble) // Rotates block if it does matter
            {
                blockRotation = CalculateBlockRotation(blockRotation, hit.normal);
            }

            // Place the block 
            var newBlock = Instantiate(currentBlock.prefab, placePosition, blockRotation);

            // Adds block to global map
            globalMap.AddBlock(placePosition, newBlock.GetComponent<BlockScript>());

            OnBlockPlaced?.Invoke();
        }
    }
    private void BreakBlock()
    {
        RaycastHit hit;

        // Checks if ray collided with something
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Checks if hitted object is block
            if (hit.transform.CompareTag("Block"))
            {
                // Removes block from global map
                globalMap.RemoveBlock(hit.transform.position, hit.transform.GetComponent<BlockScript>());

                OnBlockBreak?.Invoke();
            }
        }
    }
    private void CalculateRay() // Calculates ray from the center of the screen
    {
        Vector3 cameraPosition = new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2);

        ray = playerCamera.ScreenPointToRay(cameraPosition);

        // Checks if ray collided with something
        if (Physics.Raycast(ray, out hit, maxDistance) == false)
        {
            if (selectedCubeOutline.gameObject.activeSelf) selectedCubeOutline.gameObject.SetActive(false);
            return;
        }
        // Checks if hitted object is block
        if (hit.transform.CompareTag("Block") == false)
        {
            if (selectedCubeOutline.gameObject.activeSelf) selectedCubeOutline.gameObject.SetActive(false);
            return;
        }

        if (selectedCubeOutline.gameObject.activeSelf == false) selectedCubeOutline.gameObject.SetActive(true);
        selectedCubeOutline.position = hit.transform.position;
    }

    private Quaternion CalculateBlockRotation(Quaternion startRotation, Vector3 normal) // Calcultes block rotation
    {
        Quaternion additionalRotation = Quaternion.identity;

        // Assign rotation based on which face was hit
        if (normal == Vector3.up || normal == Vector3.down)       // Top face
        {
            additionalRotation = Quaternion.Euler(0, 0, 0); // Default orientation
        }
        else if (normal == Vector3.forward || normal == Vector3.back) // Front & back face
        {
            additionalRotation = Quaternion.Euler(-90, 0, 0);
        }

        else if (normal == Vector3.right || normal == Vector3.left) // Right & left face
        {
            additionalRotation = Quaternion.Euler(-90, 90, 0);
        }

        return additionalRotation * startRotation;
    }
    public Vector3 RoundToGrid(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x),
            Mathf.Round(position.y),
            Mathf.Round(position.z)
        );
    }
}
