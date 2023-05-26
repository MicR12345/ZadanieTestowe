using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastSelection : MonoBehaviour
{
    [Header("Referencje do wyswietlaczy tekstu")]
    [SerializeField]
    TMPro.TextMeshProUGUI NameTextComp;
    [SerializeField]
    TMPro.TextMeshProUGUI HealthTextComp;
    Displayable thing = null;
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Displayable previousThing = thing;
                bool isDisplayable = hit.collider.gameObject.TryGetComponent<Displayable>(out thing);
                //Ponowne klikniecie odznacza
                if (previousThing !=null && previousThing.ToString()!="null" && previousThing == thing)
                {
                    thing = null;
                }
            }
            else
            {
                thing = null;
            }
        }
        if (thing !=null && thing.ToString()!="null")
        {
            NameTextComp.text = "Nazwa: " + thing.GetName();
            HealthTextComp.text = "HP: " + thing.GetHealth();
            transform.position = thing.GetPosition();
        }
        else
        {
            NameTextComp.text = "Nazwa: --";
            HealthTextComp.text = "HP: --";
            transform.position = new Vector3(1000, 1000, 1000);
        }
    }
}
