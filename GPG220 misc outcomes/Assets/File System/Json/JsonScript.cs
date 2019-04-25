using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class JsonScript : MonoBehaviour
{
    
    JsonClass _jsonClass = new JsonClass();
    public Text textString;
    public Text textInt;
    public Text textFloat;
    private string json;

    public Text Output;

    public void Save()
    {
        if (textString.text != null)_jsonClass._string = textString.text;
        if (textInt.text != null)_jsonClass._int = int.Parse(textInt.text);
        if (textFloat.text != null)_jsonClass._float = float.Parse(textFloat.text);

        json = JsonUtility.ToJson(_jsonClass);
    }

    public void Load()
    {
        if (json != null)_jsonClass = JsonUtility.FromJson<JsonClass>(json);
        Output.text = "File Output: \nString: \n" + _jsonClass._string + "\nInt: \n" + _jsonClass._int + "\nFloat: \n" + _jsonClass._float;
    }
}
