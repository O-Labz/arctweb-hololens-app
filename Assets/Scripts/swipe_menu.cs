using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipe_menu : MonoBehaviour
{

    public GameObject scrollbar;
    float scroll_Position = 0;
    float[] position;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = new float[transform.childCount];
        float distance = 1f / (position.Length-1f);
        for(int i = 0; i < position.Length; i++)
        {
            position[i] = distance * i;
        }
        if(Input.GetMouseButton (0))
        {
            scroll_Position = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < position.Length; i++)
            {
                if (scroll_Position < position[i] + (distance / 2) && scroll_Position > position[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp (scrollbar.GetComponent<Scrollbar>().value, position[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < position.Length; i++)
        {
            if (scroll_Position < position[i] + (distance / 2) && scroll_Position > position[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for(int t = 0; t < position.Length; t++)
                {
                    if(t != i)
                    {
                        transform.GetChild(t).localScale = Vector2.Lerp(transform.GetChild(t).localScale, new Vector2(0.8f,0.8f), 0.1f);
                    }
                }
            }
        }
    }
}
