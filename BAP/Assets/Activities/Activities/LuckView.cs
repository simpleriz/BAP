using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LuckView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;

    private void Update()
    {
        textMeshPro.text = $"Удача: {LuckAttribute.luck.ToString()}";
    }
}

public static class LuckAttribute
{
    public static int luck = 0;
}
