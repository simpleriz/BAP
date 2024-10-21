using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{

    int roundNumber;

    const int waveFrequency = 4;

    [SerializeField] MapController mapController;
    [SerializeField] CodeController codeController;
    [SerializeField] ActivitiesController activitiesController;
    // Start is called before the first frame update
    void Awake()
    {
        mapController.VirusWave(roundNumber);
        codeController.activitiesController = activitiesController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Round()
    {
        roundNumber++;

        activitiesController.Act();
        //fight
        codeController.Act();

        mapController.DeleteAllFiles();
        if(roundNumber%waveFrequency == 0){
            mapController.VirusWave(roundNumber);
        }
    }
}
