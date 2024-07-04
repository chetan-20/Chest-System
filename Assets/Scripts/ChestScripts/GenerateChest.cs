using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GenerateChest : MonoBehaviour
{
    [SerializeField] private Button generateChestButton;
    [SerializeField] private Transform[] chestParentTransform;
    [SerializeField] private GameObject tempChest;
    [HideInInspector] public int chestCount;
    private void Start()
    {
        generateChestButton.onClick.AddListener(SpawnChest);
        chestCount = 0;
    }
    private void SpawnChest()
    {
        if (chestCount < 4)
        {
            GameObject chest = tempChest;
            Instantiate(chest, chestParentTransform[chestCount]);
            chestCount++;
        }
        else
        {
            Debug.Log("Cant Spawn");
        }
    }
   
}
