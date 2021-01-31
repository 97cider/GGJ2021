using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    // Start is called before the first frame update
    public Text toolTipText;

    private static Tooltip instance;
    public Camera mainCamera;
    public GameObject uiMana;
    public RectTransform background;
    void Start()
    {
    }
    private void Awake() {
        instance = this;
        this.gameObject.SetActive(false);
    }
    private void show(string messageBody){
        gameObject.SetActive(true);
        toolTipText.text = messageBody;
        float textOffSet = 4f;
        Vector2 bg_size = new Vector2(toolTipText.preferredWidth + textOffSet * 2, toolTipText.preferredHeight + textOffSet * 2);
        background.sizeDelta = bg_size;
    }
    private void hide(){
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(uiMana != null){
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, mainCamera, out localPoint);
            transform.localPosition = localPoint;
        }
    }
    public static void ShowToolTip(GameObject ui, string tooltipstr){
        instance.uiMana = ui;
        instance.show(tooltipstr);
    }
    public static void HideToolTip(){
        instance.hide();
    }
}
