using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetProps
{

    public const int nMats = 3;
    public static float[,] rCoefs = new float[nMats, nMats];
    public static float[,] fCoefs = new float[nMats, nMats];
    
    private static Material mat0 = Resources.Load("Ground_Mat", typeof(Material)) as Material;
    private static Material mat1 = Resources.Load("Stud_Mat", typeof(Material)) as Material;
    private static Material mat2 = Resources.Load("Strap_Mat", typeof(Material)) as Material;
    private static Material nullMat = Resources.Load("Mat_Null", typeof(Material)) as Material;

    public int material;
    public float mass;
    public bool unstoppable;
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 rotation;
    public Vector3 angleVel;

    public DetProps()
    {
        this.material = 0;
        this.mass = 1;
        this.unstoppable = false;
        this.position = new Vector3(0, 0.5F, 0);
        this.velocity = new Vector3(0, 0, 0);
        this.rotation = new Vector3(0, 0, 0);
        this.angleVel = new Vector3(0, 0, 0);
    }

    public virtual Material getRenderMaterial()
    {
        switch (material)
        {
            case 0:
                return mat0;
            case 1:
                return mat1;
            case 2:
                return mat2;
            default:
                return nullMat;
        }
    }

    public abstract DetObject createObject();

    public abstract string createName();

    public float getRcoef(DetProps props)
    {
        return rCoefs[material, props.material];
    }

    public float getFcoef(DetProps props)
    {
        return fCoefs[material, props.material];
    }

}