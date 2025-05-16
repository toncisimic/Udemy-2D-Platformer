using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NeScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text buttonText;
    public string hoverText = "DA";
    private string originalText;
    
    // Start is called before the first frame update
    void Start()
    {
        if (buttonText == null)
            buttonText = GetComponentInChildren<Text>();

        originalText = buttonText.text;
        Debug.Log("NeScript started with text: " + originalText);
    }

    // Implementacija IPointerEnterHandler sučelja
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered");
        buttonText.text = hoverText;
    }

    // Implementacija IPointerExitHandler sučelja
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited");
        buttonText.text = originalText;
    }
}
