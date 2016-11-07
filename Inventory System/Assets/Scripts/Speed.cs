using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speed : MonoBehaviour {

    public int speed;

    void Start()
    {
        GameObject.Find("Speed").transform.GetChild(0).GetComponent<Text>().text = speed.ToString();

    }
}
