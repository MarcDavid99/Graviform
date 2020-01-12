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

    public static event Action<string> OnChangeCamera;
    public static void ChangeCamera(string direction) => OnChangeCamera?.Invoke(direction);

    public static event Func<bool> OnRequestAllowRotation;
    public static bool RequestAllowRotation() => OnRequestAllowRotation?.Invoke() ?? true;

    public static event Func<bool> OnFacingRight;
    public static bool FacingRight() => OnFacingRight?.Invoke() ?? true;

    public static event Action OnRespawn;
    public static void Respawn() => OnRespawn?.Invoke();

    public static event Action<float[]> OnPickUpCoin;
    public static void ChangeCoinCounter(float[] coords) => OnPickUpCoin?.Invoke(coords);

    public static event Action OnWin;
    public static void DoSomething() => OnWin?.Invoke();

    public static event Action OnResetCounter;
    public static void ResetCoinCounter() => OnResetCounter?.Invoke();

    public static event Action<int> OnChangeDrone;
    public static void ChangeDrone(int direction) => OnChangeDrone?.Invoke(direction);


}
