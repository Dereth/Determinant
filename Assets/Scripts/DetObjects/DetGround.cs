using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetGround : DetObject
{
    public const float size = 10000F;

    public static DetGround Instance { get; private set; }

    public DetGround(DetGroundProps props) : base(props)
    {
        Instance = this;

        Mesh mesh = new Mesh();
        float d = size / 2;
        mesh.vertices = new Vector3[]
        {
            new Vector3(-d, 0, -d),
            new Vector3(-d, 0, d),
            new Vector3(d, 0, d),
            new Vector3(d, 0, -d)
        };
        mesh.triangles = new int[]
        {
            0, 1, 3,
            2, 3, 1
        };
        mesh.Optimize();
        gameObj.AddComponent<MeshFilter>().mesh = mesh;
    }

    public override void resetValues()
    {
        base.resetValues();
    }

    public override void preTick()
    {
        base.preTick();
    }

    public override void tick(float dt)
    {
        base.tick(dt);
    }

    public override float getMoI(Vector3 vel)
    {
        return 0;
    }

    public override float getCollisionRadius()
    {
        return 0;
    }

    public override DetCollision getCollision(DetObject obj)
    {
        if (obj is DetRect)
        {
            DetRect rect = (DetRect) obj;

            bool collided = false;
            float maxDepth = float.MaxValue;
            Vector3 sumPoints = new Vector3(0, 0, 0);
            int numPoints = 0;

            foreach (Vector3 vertex in rect.rotated)
            {
                Vector3 point = vertex + rect.pos;
                if (point.y <= 0)
                {
                    collided = true;
                    if (point.y <= maxDepth + 0.001F && point.y >= maxDepth - 0.001F)
                    {
                        sumPoints += point;
                        numPoints++;
                    }
                    else if (point.y < maxDepth)
                    {
                        sumPoints = point;
                        numPoints = 1;
                        maxDepth = point.y;
                    }
                }
            }

            if (collided)
            {
                return new DetCollision(-maxDepth, rect, this, (sumPoints / numPoints) - rect.pos, Vector3.zero, new Vector3(0, 1, 0));
            }
        }
        else if (obj is DetSphere)
        {
            DetSphere sphere = (DetSphere) obj;

            if (sphere.pos.y <= sphere.r)
            {
                float depth = sphere.r - sphere.pos.y;
                Vector3 point1 = new Vector3(0, -sphere.r, 0);
                Vector3 point2 = Vector3.zero;
                Vector3 direction = new Vector3(0, 1, 0);
                return new DetCollision(depth, sphere, this, point1, point2, direction);
            }
        }
        return null;
    }
}

public class DetGroundProps : DetProps
{

    public static Material groundMat = Resources.Load("Plastic_Gray_Mat", typeof(Material)) as Material;

    public DetGroundProps() : base()
    {
        unstoppable = true;
    }

    public override DetObject createObject()
    {
        return new DetGround(this);
    }

    public override string createName()
    {
        return "Ground";
    }

    public override Material getRenderMaterial()
    {
        return groundMat;
    }

}