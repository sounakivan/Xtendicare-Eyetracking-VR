using System.Collections;
using System.Collections.Generic;
using Tobii.G2OM;
using UnityEngine;
using UnityEngine.UI;

public class NextOnGaze : MonoBehaviour, IGazeFocusable
{
    public bool isLookingAtNext;
    public Slider nextSlider;
    private float nextTime = 0;

    public GameObject[] texts;
    private int index = 0;
    public bool win;
    
    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            isLookingAtNext = true;
        }
        else
        {
            isLookingAtNext = false;
            nextTime = 0;
        }
    }

    private void Start()
    {
        texts[0].SetActive(true);
        win = false;
    }

    private void Update() 
    {
        if (isLookingAtNext)
        {
            nextTime += Time.deltaTime;
        }

        nextSlider.value = nextTime;

        if(nextSlider.value >= 1 && index <= texts.Length)
        {
            changeText();
        }
        texts[index].SetActive(true);

        if (index == texts.Length-1)
        {
            win = true;
        }
    }

    private void changeText()
    {
        texts[index].SetActive(false);
        index += 1;
        nextTime = 0;
    }
}
