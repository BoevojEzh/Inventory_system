using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gold : MonoBehaviour {
    public int gold;

    void Start()
    {
        this.transform.GetChild(0).GetComponent<Text>().text = gold.ToString();


    }
}
