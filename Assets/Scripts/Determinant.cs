using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Determinant : MonoBehaviour
{
    public static Mesh rectMesh;
    public static Mesh sphereMesh;

    public static bool running = false;
    public static bool usingCamera = false;
    public static bool canModify = true;

    public static DetHand rightHand;
    public static DetHand leftHand;
    public static DetGround ground;
    public static float gravity = 9.81F;

    public static Determinant Instance { get; private set; }

    private GameObject world;
    public bool spacePressed = false;
    public bool rPressed = false;
    public bool rightClicking = false;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphereMesh = sphere.GetComponent<MeshFilter>().mesh;
            Destroy(sphere);
            GameObject rect = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rectMesh = rect.GetComponent<MeshFilter>().mesh;
            Destroy(rect);

            running = false;

            DetProps rightHandProps = new DetRightHandProps();
            DetProps leftHandProps = new DetLeftHandProps();
            DetGroundProps groundProps = new DetGroundProps();

            rightHand = (DetHand) rightHandProps.createObject();
            leftHand = (DetHand) leftHandProps.createObject();
            ground = (DetGround) groundProps.createObject();

        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!spacePressed)
            {
                if (usingCamera)
                {
                    if (running)
                    {
                        pause();
                        running = false;
                    }
                    else
                    {
                        unpause();
                        running = true;
                        DetEvents.setCanModify(false);
                    }
                }
            }
            spacePressed = true;
        }
        else
        {
            spacePressed = false;
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (!rPressed)
            {
                if (usingCamera)
                {
                    if (running)
                    {
                        pause();
                        resetObjects();
                        unpause();
                    }
                    else
                    {
                        resetObjects();
                        DetEvents.setCanModify(true);
                    }
                }
            }
            rPressed = true;
        }
        else
        {
            rPressed = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!rightClicking)
            {
                usingCamera = !usingCamera;
            }
            rightClicking = true;
        }
        else
        {
            rightClicking = false;
        }
    }

    public void pause()
    {
        Destroy(world);
        Paused.Instance.SetActive(true); // Allowed to modify
    }

    public void unpause()
    {
        world = new GameObject("DetWorld");
        world.AddComponent<DetWorld>();
        Paused.Instance.SetActive(false); // Allowed to modify
    }

    public static void resetObjects()
    {
        foreach (DetObject obj in DetObject.objects)
        {
            obj.resetValues();
        }
    }

}
