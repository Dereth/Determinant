using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EventController : MonoBehaviour
{
    public static bool menuOpen = false;
    public static bool canEdit = true;
    public static DetObject selected = null;

    public static EventController Instance { get; private set; }

    public GameObject canvas;
    public GameObject pausedMessage;
    public InvalidInputMessage invalidInputMessage;

    public GameObject noneDisplay;
    public GameObject globalDisplay;
    public GameObject sphereDisplay;
    public GameObject rectDisplay;

    public NewObjectSelector objectSelector;

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
            if (!Determinant.running)
            {
                Determinant.Instance.unpause();
                canEdit = false;
            }
            else
            {
                Determinant.Instance.pause();
            }
        }

        if (!resetPressed & updateResetButton())
        {
            Determinant.resetObjects();
            if (!Determinant.running)
            {
                canEdit = true;
            }
        }

        if (!menuPressed & updateMenuButton())
        {
            menuOpen = !menuOpen;
            canvas.SetActive(menuOpen);
        }
    }

    // Inputs for button controls

    bool updatePauseButton()
    {
        pausePressed = Input.GetKey(KeyCode.Space);
        return pausePressed;
    }

    bool updateResetButton()
    {
        resetPressed = Input.GetKey(KeyCode.R);
        return resetPressed;
    }

    bool updateMenuButton()
    {
        menuPressed = Input.GetKey(KeyCode.M);
        return menuPressed;
    }

    // Selection Functions

    public void select(DetObject obj)
    {
        selected = obj;
        noneDisplay.SetActive(false);
        globalDisplay.SetActive(obj == null);
        sphereDisplay.SetActive(obj is DetSphere);
        rectDisplay.SetActive(obj is DetRect);
    }

    public void deselect()
    {
        selected = null;
        noneDisplay.SetActive(true);
        globalDisplay.SetActive(false);
        sphereDisplay.SetActive(false);
        rectDisplay.SetActive(false);
    }

    public void displayError()
    {
        invalidInputMessage.activate(1.5F);
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