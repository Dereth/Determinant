using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    public static bool paused = true;
    public static bool canEdit = true;
    public static DetObject selected = null;

    public static EventController Instance { get; private set; }

    public GameObject Canvas;
    public GameObject NoneDisplay;
    public GameObject GlobalDisplay;
    public GameObject SphereDisplay;
    public GameObject RectDisplay;


    // Button Values
    public bool pausePressed = false;
    public bool resetPressed = false;
    public bool menuPressed = false;


    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

        }
    }

    void Update()
    {
        if (!pausePressed & updatePauseButton())
        {
            if (paused)
            {
                Determinant.Instance.unpause();
                paused = false;
                canEdit = false;
            }
            else
            {
                Determinant.Instance.pause();
                paused = true;
            }
        }

        if (!resetPressed & updateResetButton())
        {
            Determinant.resetObjects();
            if (paused)
            {
                canEdit = true;
            }
        }

        if (!menuPressed & updateMenuButton())
        {

        }
    }

    bool updatePauseButton()
    {
        pausePressed = false;
        return pausePressed;
    }

    bool updateResetButton()
    {
        resetPressed = false;
        return resetPressed;
    }

    bool updateMenuButton()
    {
        menuPressed = false;
        return menuPressed;
    }

    public void select(DetObject obj)
    {
        selected = obj;
        NoneDisplay.SetActive(false);
        GlobalDisplay.SetActive(obj == null);
        SphereDisplay.SetActive(obj is DetSphere);
        RectDisplay.SetActive(obj is DetRect);
    }

    public void deselect()
    {
        selected = null;
        NoneDisplay.SetActive(true);
        GlobalDisplay.SetActive(false);
        SphereDisplay.SetActive(false);
        RectDisplay.SetActive(false);
    }

    /*
     * 
     * UI Functions
     * 
     */

    public void deleteSelected()
    {
        if (selected != null)
        {
            deselect();
        }
    }

}