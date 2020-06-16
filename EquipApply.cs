using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipApply : MonoBehaviour
{
    public Button Buybuttons;
    public Button EquipButtons;
    public GameObject Pet;
    public Text equiptext;
    
    public void BuyItem()
    {
        DataVariables.characterPoint -= 200;
    }
    public void EquipItem()
    {
        
    }
    public void OffItem()
    {
        
    }
}