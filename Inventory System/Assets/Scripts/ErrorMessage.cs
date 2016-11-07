using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ErrorMessage : MonoBehaviour {

    private GameObject errorMessage;

	void Start ()
    {
        errorMessage = GameObject.Find("Error");
        errorMessage.SetActive(false);
    }

    public void ShowError(string errorText)
    {
        errorMessage.SetActive(true);
        errorMessage.transform.GetChild(0).GetComponent<Text>().text = errorText;
    }

    public void HideError()
    {
        errorMessage.SetActive(false);
    }

	
	
}
