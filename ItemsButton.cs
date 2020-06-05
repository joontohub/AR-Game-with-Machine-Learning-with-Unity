using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsButton : MonoBehaviour
{
    public Button button;
    public Text text;
    public Image coin;
    public GameObject popup;
    public Text characterPointtext;
    //public ColorBlock buttoncolor;

    public void OnClickCheck () {
        if(DataVariables.characterPoint>= 200){
            if(text.text == "Apply"){
                text.text = "UnApply";
            }
            else if(text.text == "UnApply"){
                text.text = "Apply";
            }
        }
        else{
            popup.SetActive(true);
        }
    }
}