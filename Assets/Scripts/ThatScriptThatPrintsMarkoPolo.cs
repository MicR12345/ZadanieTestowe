using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThatScriptThatPrintsMarkoPolo : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI textBoxToSpam;
    [SerializeField]
    TMPro.TextMeshProUGUI textBoxToSpam2;
    public void PrintNumbersOrWierdText()
    {
        string text = "";
        for (int i = 1; i <= 44; i++)
        {
            bool divisible = false;
            if (i % 3 == 0)
            {
                text += "Marko";
                divisible = true;
            }
            if (i % 5 == 0)
            {
                text += "Polo";
                divisible = true;
            }
            if (divisible) text += "\n";
            else text += i + "\n";
        }
        textBoxToSpam.text = text;
        string text2 = "";
        for (int i = 45; i <= 100; i++)
        {
            bool divisible = false;
            if (i % 3 == 0)
            {
                text2 += "Marko";
                divisible = true;
            }
            if (i % 5 == 0)
            {
                text2 += "Polo";
                divisible = true;
            }
            if (divisible) text2 += "\n";
            else text2 += i + "\n";
        }
        textBoxToSpam2.text = text2;
    }
}
