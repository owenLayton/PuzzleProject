using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_ActivateByState : IDataDictionaryListener
{
    public override void OnVariableUpdated(object value)
    {
        if (value is not bool)
            Debug.Log($"{value.GetType()} not implemented in DD_ActivateByState");

        gameObject.transform.GetChild(0).gameObject.SetActive((bool)value);
    }
}
