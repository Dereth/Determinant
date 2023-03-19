using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Determinant : MonoBehaviour
{
    public static Mesh sphereMesh;
    public static bool running = false;
    public static bool usingCamera = false;
    public static bool canModify = true;

    public static Determinant Instance { get; private set; }

    private GameObject world;
    private bool spacePressed = false;
    private bool rPressed = false;
    private bool rightClicking = false;

    public static float gravity = 9.81F;

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

            running = false;

            DetGroundProps pr = new DetGroundProps();
            pr.createObject();

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
        Paused.Instance.SetActive(true);
    }

    public void unpause()
    {
        world = new GameObject("DetWorld");
        world.AddComponent<DetWorld>();
        Paused.Instance.SetActive(false);
    }

    public static void resetObjects()
    {
        foreach (DetObject obj in DetObject.objects)
        {
            obj.resetValues();
        }
    }

    public void test()
    {

    }
}
