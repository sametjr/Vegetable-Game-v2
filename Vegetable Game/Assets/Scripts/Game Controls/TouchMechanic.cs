using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchMechanic : MonoBehaviour
{
    private bool onDrag = false;
    private Vector3 offset;
    private float previousZPos;

    private PanIngredients pan;

    #region DoubleClickVariables
        private int clickCount = 0;
        private float thresholdValue = .3f;
        private double lastClick;
    #endregion

    private void Start() {
        pan = FindObjectOfType<PanIngredients>();
    }
    private void OnMouseDown() {

        if(GameManager.Instance.CanPlayerInteract == false) return;

        onDrag = true;

        if(ControlDoubleClick())
        {
            SoundManager.Instance.PlayRolloverSound();
            pan.SendObjectToPan(this.gameObject);
        }

        previousZPos = transform.position.z;
        offset = transform.position - GameManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool ControlDoubleClick()
    {
        if(clickCount == 0)
        {
            lastClick = Time.time;
            clickCount++;
        }

        else if(clickCount == 1 && Time.time - lastClick <= thresholdValue)
        {
            clickCount = 0;
            return true;
        }

        else
        {
            clickCount = 1;
            lastClick = Time.time;
            return false;
        }

        return false;

    }

    private void OnMouseUp() {
        onDrag = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, previousZPos);
    }

    private void FixedUpdate() {
        if(onDrag)
        {
            transform.position = GameManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(transform.position.x, transform.position.y, -4);
        }
    }
}
