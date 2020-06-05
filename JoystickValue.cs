using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickValue : MonoBehaviour
{
    public static JoystickValue instance;
    public static float horizontal;
    public static float vertical;

    public JoystickController landJoystick;
    public static  Vector3 dir = Vector3.zero;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
    }
   
    public void GetJoyValue()
    {
        horizontal = landJoystick.GetHorizontalValue();
        vertical = landJoystick.GetVerticalValue();  

        dir.x = horizontal;
        dir.z = vertical;  
    }
}
