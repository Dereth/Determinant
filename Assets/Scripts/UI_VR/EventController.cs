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
    public GameObject laserL;
    public GameObject laserR;
    public GameObject pausedMessage;
    public InvalidInputMessage invalidInputMessage;

    public GameObject noneDisplay;
    public GameObject globalDisplay;
    public GameObject sphereDisplay;
    public GameObject rectDisplay;

    public ObjectSelector objectSelector;
    public NumpadController numpadController;

    public GameObject VRMenu;

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
            VRMenu = GameObject.FindGameObjectWithTag("VR_menu");
        }
    }

    void Update()
    {
        if (!pausePressed & updatePauseButton())
        {
            if (!Determinant.running)
            {
                pausedMessage.SetActive(false);
                Determinant.Instance.unpause();
                canEdit = false;
            }
            else
            {
                pausedMessage.SetActive(true);
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
            canvas.transform.position = VRMenu.transform.position;
            canvas.transform.rotation = VRMenu.transform.rotation;
            canvas.SetActive(menuOpen);
            laserL.SetActive(menuOpen);
            laserR.SetActive(menuOpen);
        }
    }

    // Inputs for button controls

    bool updatePauseButton()
    {
        //VR Controller Left
        bool value;
        if (Determinant.leftDevices.Count > 0 && Determinant.leftDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out value) && value)
        {
            pausePressed = true;
            return pausePressed;
        }

        //VR Controller Right
        bool value2;
        if (Determinant.rightDevices.Count > 0 && Determinant.rightDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out value2) && value2)
        {
            pausePressed = true;
            return pausePressed;
        }

        //Keyboard
        pausePressed = Input.GetKey(KeyCode.Space);
        return pausePressed;
    }

    bool updateResetButton()
    {
        //VR Controller Left
        bool value;
        if (Determinant.leftDevices.Count > 0 && Determinant.leftDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out value) && value)
        {
            resetPressed = true;
            return resetPressed;
        }

        //VR Controller Right
        bool value2;
        if (Determinant.rightDevices.Count > 0 && Determinant.rightDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out value2) && value2)
        {
            resetPressed = true;
            return resetPressed;
        }

        //Keyboard
        resetPressed = Input.GetKey(KeyCode.R);
        return resetPressed;
    }

    bool updateMenuButton()
    {
        //VR Controller Left (Menu button only exists on left controller, Right controller's Oculus button will not be used)
        bool value;
        if (Determinant.leftDevices.Count > 0 && Determinant.leftDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out value) && value)
        {
            menuPressed = true;
            return menuPressed;
        }

        //Keyboard
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
        NumpadButton.resetInput();
    }

    public void deselect()
    {
        selected = null;
        noneDisplay.SetActive(true);
        globalDisplay.SetActive(false);
        sphereDisplay.SetActive(false);
        rectDisplay.SetActive(false);
        NumpadButton.resetInput();
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
            DetObject toDelete = selected;
            deselect();
            if (toDelete is DetObjectHoldable)
            {
                DetObject.removeObject((DetObjectHoldable) toDelete);
                objectSelector.refreshOptions();
            }
        }
    }

}