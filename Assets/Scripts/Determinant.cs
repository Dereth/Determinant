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
        if (!running)
        {
            rightHand.updatePositioning();
            leftHand.updatePositioning();
        }
    }

    public void pause()
    {
        running = false;
        Destroy(world);
    }

    public void unpause()
    {
        running = true;
        world = new GameObject("DetWorld");
        world.AddComponent<DetWorld>();
    }

    public static void resetObjects()
    {
        foreach (DetObject obj in DetObject.objects)
        {
            obj.resetValues();
        }
    }

}
