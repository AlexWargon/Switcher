using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Switching : MonoBehaviour  {

    [Header("Text")]
    public string myText;
    public Text switchTextObj;

    [Header("Background color")]
    public Color ColorBg;

    [Header("Knob Padding")]
    public float leftSide;
    public float rightSide;

    [Header("Check")]
    public bool check;
    public bool uncheck;
    public Image knob;
    public Image switchBgImage;
    public Slider slide;
    private float slideValue;
    private RectTransform knobTransform;
    private bool btm;
    private float xV1;
    private float xV2;
    public float velocity;
    private float speed = 300;
    public GameObject reflect;
    private Animator anim;
    bool animEnd;

    void Start() {
        slide = GameObject.Find("SwitcherSlider").GetComponent<Slider>();
        knobTransform = knob.GetComponent<RectTransform>();
        switchTextObj = switchTextObj.GetComponent<Text>();
        switchBgImage.color = ColorBg;
        knob.color = ColorBg;
        btm = false;
        anim = GetComponent<Animator>();
        slide.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    void Update () {

        switchTextObj.text = myText;
        switchBgImage.color = ColorBg;
        knob.color = ColorBg;
        slideValue = slide.value;
        Scretch();
        KnobPadding();
        SmoothSwitch();
        Checking();
        RecflectAnim();

        if (Input.GetMouseButtonDown(0))
        {
            btm = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            btm = false;
        }
    }
    public void SmoothSwitch()
    {
        if(!btm)
        {
            if (slide.value > 50f && slide.value < 100f)
            {
                slide.value += speed * Time.deltaTime;
            }
            else
            if (slide.value <= 50f && slide.value > 0f)
            {
                slide.value -= speed * Time.deltaTime;
            }

        }
    }
    void Scretch()
    {
        xV1 = slide.value;
        Invoke("SecondValue", 0.01f);        
        velocity = (xV1 - xV2) / Time.deltaTime;
        float screcthValue = velocity/10;

        if (velocity > 0.2f && knobTransform.offsetMin.x < (knobTransform.offsetMin.x - 200f) / 100 * slide.value)
        {
            knobTransform.offsetMin = new Vector2(knobTransform.offsetMin.x - (screcthValue), knobTransform.offsetMin.y);
            knobTransform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        if(velocity < -0.2f && knobTransform.offsetMax.x > (knobTransform.offsetMax.x - 200f) / 100 * (100-slide.value))
        {                       
            knobTransform.offsetMax = new Vector2(knobTransform.offsetMax.x - (screcthValue), knobTransform.offsetMax.y);
            knobTransform.eulerAngles = new Vector3(0, 0, 0);
        }
                
        if(knobTransform.offsetMax.x > -192f)
        {
            knobTransform.offsetMax = new Vector2(knobTransform.offsetMax.x - 1576 * Time.deltaTime, knobTransform.offsetMax.y);
            knobTransform.eulerAngles = new Vector3(0, 0, 0);
        }
        if(knobTransform.offsetMin.x < -292f)
        {
            knobTransform.offsetMin = new Vector2(knobTransform.offsetMin.x + 1576 * Time.deltaTime, knobTransform.offsetMin.y);
            knobTransform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    void SecondValue()
    {
        xV2 = slide.value;
    }

    void Checking()
    {
        if (slide.value == 0f)
        {
            check = true;
            uncheck = false;
        }
        if (slide.value == 100f)
        {
            check = false;
            uncheck = true;
        }
    }
    void KnobPadding()
    {
        leftSide = slide.value;
        rightSide = 100 - slide.value;
    }
    void RecflectAnim()
    {
        
        if (btm && !animEnd || !btm && !animEnd || btm)
        {
            anim.enabled = true;
        }
        if(!btm && animEnd)
        {
            anim.enabled = false;
        }
    }
    void AnimEnd()
    {
        animEnd = true;
    }
    void AnimStart()
    {
        animEnd = false;
    }
    public void ValueChangeCheck()
    {
        Debug.Log(slide.value);
    }
}
