using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Activity : MonoBehaviour
{
    public static ActivitiesController activitiesController;
    //public GameScript gameScript;
    [SerializeField] TextMeshProUGUI labelText;
    [SerializeField] Image image;
    bool isVisible;
    public Action action;
    public Animator animator;

    void Start(){
        action.activity = this;
        animator = GetComponent<Animator>();
    }

    public void SelectActivity()
    {
        if(action.isAimless){
            action.ActionWithoutTarget();
        }
        else{
            //activitiesController.
        }
    }

    public void OnClick()
    {
        activitiesController.SelectActivity(this);
    }

    void Update(){
        if(isVisible == false & action.IsVisible())
        {
            animator.SetBool("isVisible", true);

            isVisible = true;

            
        }
        else if(isVisible == false)
        {
            transform.SetAsLastSibling();
        }
        labelText.text = action.GetLabel();

        image.color = action.GetColor();
    }

    public void Delete(){
        animator.SetBool("isVisible", false);
        Invoke("SelfDestroy",1.5f);
        this.enabled = false;
    }

    void SelfDestroy(){
        Destroy(gameObject);
    }
}
