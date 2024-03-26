using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using SecretHistories;
using SecretHistories.Infrastructure;
using SecretHistories.UI;

public class SingleTickKeybind : MonoBehaviour
{
    public static KeybindTracker keyTick;

    public static void Initialise()
    {
        new GameObject().AddComponent<SingleTickKeybind>();
        NoonUtility.Log("SingleTickKeybind: Initialised");
	}

    public void Start() => SceneManager.sceneLoaded += Awake;

    public void OnDestroy() => SceneManager.sceneLoaded -= Awake;

    public void Update()
    {
        bool inputEnabled = !Watchman.Get<LocalNexus>().PlayerInputDisabled();
        if (inputEnabled) {
            if (SingleTickKeybind.keyTick.wasPressedThisFrame)
            {
                NoonUtility.Log("SingleTickKeybind: Skip");
                Watchman.Get<Heart>().Beat(0.015625f, 0.5f);
            }
        }
    }

    private void Awake(Scene scene, LoadSceneMode mode)
    {
        if (!(scene.name == "S3Menu"))
          return;
        try
        {
            SingleTickKeybind.keyTick = new KeybindTracker("KeySingleTick");
        }
        catch (Exception ex)
        {
          NoonUtility.LogException(ex);
        }
        NoonUtility.Log("SingleTickKeybind: Trackers Started");
    }

}

