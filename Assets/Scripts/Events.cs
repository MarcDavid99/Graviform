using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
    public static event Action<int> OnChangeGravity;
    public static void ChangeGravity(int direction) => OnChangeGravity?.Invoke(direction);

    public static event Func<string> OnRequestGravityDirection;
    public static string RequestGravityDirection() => OnRequestGravityDirection?.Invoke() ?? "down";

    public static event Action<int> OnChangeCamera;
    public static void ChangeCamera(int direction) => OnChangeCamera?.Invoke(direction);

    public static event Func<bool> OnRequestAllowRotation;
    public static bool RequestAllowRotation() => OnRequestAllowRotation?.Invoke() ?? true;
}
