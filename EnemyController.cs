using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    private bool isDead;
    private float enemyScore;
 
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Jewel")
        {
            if(other.gameObject.name == "1_Jewel")
            {
                enemyScore += DataVariables.jewel_1_score;
                other.gameObject.SetActive(false);
                Debug.Log("1_set false");
                Debug.Log("EnmeyScore : " + DataVariables.enemyScore);
            }
            else if(other.gameObject.name == "2_Jewel")
            {
                enemyScore += DataVariables.jewel_2_score;
                other.gameObject.SetActive(false);
                Debug.Log("2_set false");
                Debug.Log("EnmeyScore : " + DataVariables.enemyScore);
            }
            else if(other.gameObject.name == "3_Jewel")
            {
                enemyScore += DataVariables.jewel_3_score;
                other.gameObject.SetActive(false);
                Debug.Log("3_set false");
                Debug.Log("EnmeyScore : " + DataVariables.enemyScore);
            }
        }
    }
}
