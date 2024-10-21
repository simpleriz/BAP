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

    public void onClick()
    {
        activitiesController.SelectActivity(this);
    }

    void Update(){
        animator.SetBool("isVisible", action.IsVisible());

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
