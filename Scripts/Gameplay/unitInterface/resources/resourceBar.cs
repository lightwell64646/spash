using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceBar : MonoBehaviour {

    public int maxVal = 10;
    public int currentVal;
    public int valPerPip = 5;
    public Text myText = null;
    
    private float regenCarry = 0;
    public SpriteRenderer sprite;

    // Update is called once per frame
    void FixedUpdate () {
        float x = (float)currentVal / valPerPip / 2;
        Vector2 size = new Vector2(x, 0.2f);
        GetComponent<SpriteRenderer>().size = size;

        if (x != 0) x = 2f/x;
        Vector3 scale = new Vector3(x * currentVal/maxVal, 1, 1);
        transform.localScale = scale;

        Vector3 pos = transform.localPosition;
        pos.x = (float)currentVal / maxVal - 1;
        transform.localPosition = pos;

        if (myText != null)
        {
            myText.text = currentVal.ToString();
        }
    }

    public bool use(int x)
    {
        if (currentVal >= x)
        {
            currentVal -= x;
            return true;
        }
        return false;
    }

    public bool regen(float x)
    {
        float xCarry = x + regenCarry;
        int regenThisFrame = (int)Mathf.Floor(xCarry);
        currentVal += regenThisFrame;
        regenCarry = xCarry - regenThisFrame;

        if (currentVal > maxVal)
        {
            currentVal = maxVal;
            return true;
        }
        return false;
    }
}
