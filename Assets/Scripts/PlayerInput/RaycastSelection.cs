using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastSelection : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI NameTextComp;
    [SerializeField]
    TMPro.TextMeshProUGUI HealthTextComp;
    [Header("Pokazywany agent")]
    [SerializeReference]
    Displayable thing = null;
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                bool isDisplayable = hit.collider.gameObject.TryGetComponent<Displayable>(out thing);
            }
            else
            {
                thing = null;
            }
        }
        if (thing.ToString()!="null")
        {
            NameTextComp.text = "Nazwa: " + thing.GetName();
            HealthTextComp.text = "HP: " + thing.GetHealth();
        }
        else
        {
            NameTextComp.text = "Nazwa: --";
            HealthTextComp.text = "HP: --";
        }
    }
}
