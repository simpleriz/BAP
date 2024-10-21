using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivitiesController : MonoBehaviour
{
    public Activity selectActivity;
    [SerializeField] GameObject ActivityPrefab;
    List<GameObject> activities;
    void Start(){
        activities = new List<GameObject>();
        Activity.activitiesController = this;
    }
    public void AddActivity(Action action)
    {
        GameObject obj = Instantiate(ActivityPrefab,transform);
        obj.GetComponent<Activity>().action = action;
        activities.Add(obj);
    }

    public void Act()
    {
        foreach(GameObject activity in activities){
            Destroy(activity);
        }
        activities = new List<GameObject>();


        Action.Round();
    }

    public GameObject OnClickAnywhere(string target)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag(tag))
            {
                return result.gameObject;
            }
        }

        return null;
    }

    void FixedUpdate(){
        if(selectActivity != null)
        {
            selectActivity.animator.SetBool("isSelect",true);

            if(Input.GetMouseButtonDown(0))
            {
                SelectActivityUpdate();
            }

            if(Input.GetMouseButtonDown(1))
            {
                selectActivity.animator.SetBool("isSelect",false);
                selectActivity = null;
            }
        }            
    }

    void SelectActivityUpdate()
    {
        var obj = OnClickAnywhere("FightObject");
        if(obj == null) return;
        FightSystem system = obj.GetComponent<FightSystem>();

        if(system != null)
        {
            selectActivity.action.ActionOnSystem(system);
            selectActivity.animator.SetBool("isSelect",false);
            selectActivity = null;
            return;
        }
        
        FightVirus virus = obj.transform.parent.gameObject.GetComponent<FightVirus>();
        if(virus != null)
        {
            selectActivity.action.ActionOnVirus(virus);
            selectActivity.animator.SetBool("isSelect",false);
            selectActivity = null;
            return;
        }
    }

    public void SelectActivity(Activity activity)
    {
        if(selectActivity != null)
        selectActivity.animator.SetBool("isSelect",false);
        selectActivity = activity;
    }
}
