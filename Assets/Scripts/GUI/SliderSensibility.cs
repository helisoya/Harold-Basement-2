using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderSensibility : MonoBehaviour
{
    public void EditPlayerSensibility(){
        GameObject.Find("Player").GetComponent<PlayerMovement>().rotationspeed = (int) GetComponent<UnityEngine.UI.Slider>().value;
    }
}
