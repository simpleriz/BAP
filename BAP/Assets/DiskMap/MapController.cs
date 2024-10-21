using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField] FightPlace fightPlace;
    [SerializeField] GridLayoutGroup filesGrid;
    MapFile[] mapFiles;
    
    [SerializeField] GameObject buttonFilePrefab;
    [SerializeField] TextMeshProUGUI descriptionText;
    public MapFile selectedFile;

    [SerializeField] Button scanButton;

    [ContextMenu("Wave")]
    public void VirusWave(int round){
        
        DeleteAllFiles();

        int f1Strenght = Random.Range(1,Mathf.CeilToInt(round/2)+1);
        int f2Strenght = Random.Range(1,Mathf.CeilToInt(round/2)+1);
        int f3Strenght = Random.Range(1,Mathf.CeilToInt(round/2)+1);

        var f2Chance = Random.Range(8,11);
        var f3Chance = Random.Range(0,6);

        newFile(true,f1Strenght,"100");
        newFile(Random.Range(1,11)<=f2Chance,f2Strenght,((f2Chance)*10).ToString());
        newFile(Random.Range(1,11)<=f3Chance,f3Strenght,(f3Chance*10).ToString());

        scanButton.interactable = true;
    }

    void newFile(bool isInfected, int strength, string chance){
        GameObject inst = Instantiate(buttonFilePrefab,filesGrid.transform);
        MapFile mapFileInst = inst.GetComponent<MapFile>();
        mapFileInst.descriptionText = descriptionText;
        mapFileInst.mapController = this;
        mapFileInst.isInfected = isInfected;
        mapFileInst.displayChance = chance;
        mapFileInst.virusStrength = strength;
    }

    void Start()
    {

    }

    public void ScanFile()
    {   
        if(selectedFile != null)
        {
            selectedFile.Scan(fightPlace);
            scanButton.interactable = false;
        }
        
    }

    public void DeleteFile(){
        if(selectedFile == null) return;
        selectedFile.Delete();
    }

    public void DeleteAllFiles(){
        foreach(MapFile file in filesGrid.gameObject.GetComponentsInChildren<MapFile>()){
            if(file == null) continue;
            file.Delete();
        }
    }
}