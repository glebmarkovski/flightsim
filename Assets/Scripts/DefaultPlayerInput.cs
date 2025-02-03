using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayerInput : PlayerInput
{
    public override float GetAxis(string name)
    {
        return 0;
    }
    public override bool GetButton(string name)
    {
        return false;
    }
    public override bool GetButtonDown(string name)
    {
        return false;
    }
    public override float GetTrim(string name)
    {
        return 0;
    }
}
