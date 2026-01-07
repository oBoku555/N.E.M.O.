using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class justOpen : MonoBehaviour
{
    public GameObject TargetGameObject;
    // Start is called before the first frame update
    public void Open(){
        TargetGameObject.SetActive(!TargetGameObject.activeSelf);
    }
}
