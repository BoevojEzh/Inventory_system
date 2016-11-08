using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemoveCarpet : MonoBehaviour {

    void Start()
    {

    }

    public void SetBgrd()
    {
        this.transform.parent.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item Icons/Inventory_bgrd");
        GameObject.Find("Remove carpet").SetActive(false);
    }
}
