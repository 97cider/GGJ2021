using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Tooltip;
public class TooltipRunner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    private bool mouse_over;
    public UiController ui;
    
    public Accessory accessory;
    public Weapon weapon;

    private bool isShowing;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(accessory == null){
            accessory = ui.accessory;
        }
        if (weapon == null){
            weapon = ui.weapon;
        }
        if (mouse_over){
            if (!isShowing){

                //Get the current item or accessory
                if (accessory != null){
                    // Hovering over an accessory panel
                    Tooltip.ShowToolTip(this.gameObject, accessory.description);
                }
                else {
                    // Hovering over a weapons panel
                    Tooltip.ShowToolTip(this.gameObject, weapon.description);
                }
                Tooltip.ShowToolTip(this.gameObject, "In");
                isShowing = true;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData) {
        mouse_over = true;
    }
    public void OnPointerExit(PointerEventData eventData) {
        mouse_over = false;
        Debug.Log("EXIT");
        Tooltip.HideToolTip();
        isShowing = false;
    }
}
