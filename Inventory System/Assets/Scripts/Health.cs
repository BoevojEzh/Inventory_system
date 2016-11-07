using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int health;

    void Start()
    {
        GameObject.Find("Health").transform.GetChild(0).GetComponent<Text>().text = health.ToString();

    }
}
