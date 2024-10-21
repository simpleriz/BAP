using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapFile : MonoBehaviour
{
    string name = "File name";
    string content = "File......#..#/=...K.4..eErro.r......../#/......///.";
    public string displayChance = "100";
    public int virusStrength = 5;
    public bool isInfected;

    [SerializeField] TextMeshProUGUI buttonText;

    public TextMeshProUGUI descriptionText;
    public MapController mapController;

    public void Start(){
        name = GenerateRandomName();
        buttonText.text = name;
    }

    static string GenerateRandomName() { 
        string[] prefixes = { "Фото с", "Картинка на", "Снимок во время", "Изображение из", "Книжка о", "Сказка про", "История", "Памятка для", "Записка от", "Документ по" };
        string[] objects = { "мамой", "папой", "братом", "сестрой", "другом", "кошкой", "собакой", "машиной", "деревом", "солнцем", "морем", "горой", "городом", "парком", "кино", "концертом", "путешествием" };
        string[] suffixes = { "", "в детстве", "на природе", "в отпуске", "на праздник", "на работе", "в школе", "в университете", "в путешествии", "в городе", "в деревне" };

        int randomIndex = Random.Range(0, prefixes.Length);
        string fileName = $"{prefixes[randomIndex]} {objects[Random.Range(0, objects.Length)]} {suffixes[Random.Range(0, suffixes.Length)]}";

        return fileName;
    }

    public void Delete(){
        Destroy(gameObject);
        descriptionText.text = "";
    }

    public void Scan(FightPlace fightPlace)
    {   
        if(isInfected)
            fightPlace.GenerateNewVirusesByCost(virusStrength);
        Delete();
    }

    public void Select(){
        mapController.selectedFile = this;
        if(displayChance == "100")
            descriptionText.text = $"{name}\n\n{content}\n\n<color=red>Шанс найти угрозу: <size=50>{displayChance}%</b></color>\n</size><color=red>Сложность: <size=50>{virusStrength.ToString()}</b></color>\n\n";
        else
            descriptionText.text = $"{name}\n\n{content}\n\n<color=red>Шанс найти угрозу: <size=50>{displayChance}%</b></color>\n</size><color=red>Сложность: <size=50>???</b></color>\n\n";
    }
} 