using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justClose : MonoBehaviour
{
    public GameObject TargetGameObject;
    // Start is called before the first frame update
    public void Close()
    {
        TargetGameObject.SetActive(false);
    }
}
