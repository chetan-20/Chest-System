using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateChest : MonoBehaviour
{
    [SerializeField] private Button generateChestButton;
    [SerializeField] private Transform chestParentTransform;
    [SerializeField] private GameObject tempChest;
    private void Start()
    {
        generateChestButton.onClick.AddListener(SpawnChest);
    }
    private void SpawnChest()
    {
        GameObject chest = tempChest;
        Instantiate(chest,chestParentTransform);        
    }   
}
