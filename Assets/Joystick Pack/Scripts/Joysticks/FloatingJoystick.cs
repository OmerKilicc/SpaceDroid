using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    { // Joystick Boyutlandırma
        if (transform.CompareTag("MoveStick"))
        {
            var Rect = gameObject.GetComponent<RectTransform>();
            Rect.position = new Vector3(Screen.width / 4, Screen.height / 2, 0);
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 2);
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
        }
        else if (transform.CompareTag("AimStick"))
        {
            var Rect = gameObject.GetComponent<RectTransform>();
            Rect.position = new Vector3(Screen.width - (Screen.width / 4), Screen.height / 2, 0);
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 2);
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
        }
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}