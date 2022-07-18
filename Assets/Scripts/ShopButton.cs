using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    bool buttonSelected;

    void Start()
    {
        AssignButtonComponets();
    }

    void AssignButtonComponets()
    {
        this.gameObject.AddComponent<Button>();
        Button _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        if (!buttonSelected)
        {
            SwitchOffOtherButtons();
            buttonSelected = true;
        }
        else
        {
            Debug.Log("You made a purchase!");
        }
    }

    void SwitchOffOtherButtons()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).GetComponent<ShopButton>().buttonSelected = false;
        }
    }
}
