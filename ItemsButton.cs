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
    public GameObject character;
    //public ColorBlock buttoncolor;
    public GameObject item;
    public string itemSwitch;
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
    public void minus200()
    {
        if(DataVariables.characterPoint >= 200)
        {
            DataVariables.characterPoint -= 200;
            character.GetComponent<Animator>().enabled = false;
        }
        else{
            return;
        }
    }
    public void itemOn()
    {
        //itemSwitch = item.name;
    }
    private void RotateItem()
    {
        //item.transform.Rotate(new Vector3(0f,0f,30f) * Time.deltaTime);
    }
    public void itemOff()
    {
        //item.SetActive(false);
    }
  
}