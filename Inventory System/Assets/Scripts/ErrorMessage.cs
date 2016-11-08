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
        Cursor.SetCursor(Resources.Load<Texture2D>("Item icons/point_cursor"), Vector2.zero, CursorMode.ForceSoftware);
        errorMessage.SetActive(true);
        errorMessage.transform.GetChild(0).GetComponent<Text>().text = errorText;
    }

    public void HideError()
    {
        Cursor.SetCursor(Resources.Load<Texture2D>("Item icons/drop_cursor"), Vector2.zero, CursorMode.ForceSoftware);
        errorMessage.SetActive(false);

    }

	
	
}
