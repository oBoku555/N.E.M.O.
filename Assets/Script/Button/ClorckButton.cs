using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClorckButton : MonoBehaviour
{
      // Start is called before the first frame update
    public Button t;
    public GameObject claendar;
    // Update is called once per frame
    
    public void onTclick()
    {
        claendar.SetActive(!claendar.activeSelf);
    }
}
