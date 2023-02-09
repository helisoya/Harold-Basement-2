using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderRoomSizeValueChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public void OnValueChanged(float newVal){
        text.text = ((int)(newVal)).ToString();
    }
}
