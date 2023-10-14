using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImgEvent : MonoBehaviour
{
    Outline outline;
    Button button;

    private void Start() {
        GetComponent();
    }

    void GetComponent() {
        outline = GetComponent<Outline>();
        button = GetComponent<Button>();

    }

    public void EnableOutline(bool enabled) {
        if (outline != null) {
            if (button.interactable) {
                outline.enabled = enabled;
            }
        }
        else GetComponent();
    }

    public void ButtonInteractable(bool interactable) {
        outline.enabled = false;
        button.interactable = interactable;
    }


}
