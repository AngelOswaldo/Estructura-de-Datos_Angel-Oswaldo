using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{
    public List<Text> totalTexts;

    public VerticalLayoutGroup eventGroup;
    public Text textPrefab;

    public int counter = 9;

    private void Start()
    {
        UpdateEvents("Inicio del juego.");
        UpdateEvents("Escribe tu nombre...");
    }

    public void UpdateEvents(string EventText)
    {
        Text myText = Instantiate(textPrefab, eventGroup.transform);
        myText.text = EventText;

        if(counter!=0)
        {
            counter--;

            GameObject newDeleted = totalTexts[counter-1].gameObject;
            Destroy(newDeleted);

            totalTexts.RemoveAt(counter-1);
        }
        else if(counter==0)
        {
            GameObject newDeleted = totalTexts[0].gameObject;
            Destroy(newDeleted);

            totalTexts.RemoveAt(0);
        }

        totalTexts.Add(myText);

    }

}
