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
        /*for (int i = 0; i < 100;)
        {
            var x = EnhancedRandom.Next(0, 100);
            if (x > 100 || x < 0)
            if(x == 99) { Debug.Log(i); i++; }
        }*/
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
