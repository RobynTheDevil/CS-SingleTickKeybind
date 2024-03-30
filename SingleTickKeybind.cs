using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using SecretHistories;
using SecretHistories.Infrastructure;
using SecretHistories.UI;

public class SingleTickKeybind : MonoBehaviour
{
    public static bool started = false;
    public static KeybindTracker keyTick;

    public static void Initialise()
    {
        new GameObject().AddComponent<SingleTickKeybind>();
        NoonUtility.Log("SingleTickKeybind: Initialised");
	}

    public void Start() => SceneManager.sceneLoaded += Load;

    public void OnDestroy() => SceneManager.sceneLoaded -= Load;

    public void Load(Scene scene, LoadSceneMode mode)
    {
        try
        {
            if (!started) {
                keyTick = new KeybindTracker("KeySingleTick");
                started = true;
            } else {
                keyTick.Subscribe();
            }
        }
        catch (Exception ex)
        {
          NoonUtility.LogException(ex);
        }
        NoonUtility.Log("SingleTickKeybind: Trackers Started");
    }


    public void Update()
    {
        if (Watchman.Get<LocalNexus>() == null || Watchman.Get<LocalNexus>().PlayerInputDisabled())
            return;
        if (keyTick.wasPressedThisFrame())
        {
            NoonUtility.Log("SingleTickKeybind: Skip");
            Watchman.Get<Heart>().Beat(0.015625f, 0.5f);
        }
    }

}

