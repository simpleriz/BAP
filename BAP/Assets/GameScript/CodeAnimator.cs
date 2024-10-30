using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeAnimator : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] GameObject CompleteBackground;
    [SerializeField] GameObject UncompleteBackground;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] float stringHeight;

    bool isValue;
    float step;
    float value;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if(isValue)
        {
            value -= step;
            text.text = value.ToString();
            if(value <= 0)
            {
                text.text = "";
                isValue = false;
            }
        }
    }

    public void SetValue(float nValue, float nStep){
        value = nValue;
        step = nStep;
        isValue = true;
        text.text = value.ToString();
    }

    public void SetContent(string content)
    {
        text.text = content;
    }

    public void SetContent(float content)
    {
        text.text = content.ToString();
    }

    public void nextStep()
    {
        transform.position -= new Vector3(0,stringHeight,0);
        text.text = "";
        disableAdditionalBackground();
    }

    public void skipSteps(int stepsCount)
    {
        transform.position -= new Vector3(0,stringHeight*stepsCount,0);
    }

    public void Hide()
    {
        text.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }

    public void Show()
    {
        text.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
    }

    public void Restart()
    {
        transform.position = startPosition;
        Show();
    }

    public void SetBackgroundComplete()
    {
        CompleteBackground.SetActive(true);
    }

    public void SetBackgroundUncomplete()
    {
        UncompleteBackground.SetActive(true);
    }

    public void disableAdditionalBackground(){
        CompleteBackground.SetActive(false);
        UncompleteBackground.SetActive(false);
    }
}
