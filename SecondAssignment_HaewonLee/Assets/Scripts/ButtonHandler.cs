using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerDownHandler
{
    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<Selectable>().OnPointerExit(null);
    }

    public void OnPointerDown(PointerEventData eventData) // Point down a selected button
    {
        if (eventData.selectedObject.GetComponent<Button>() != null) // only for buttons
        {
            GetComponent<Button>().onClick.Invoke(); // call the button that is currently selected
        }
    }
    public void OnPointerEnter(PointerEventData eventData) // A button is changed when the cursor places on a button  
    {
        GetComponent<Selectable>().Select();
    }


}
