using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DetRect : DetObject
{
	private const int XAXIS = 0;
	private const int YAXIS = 1;
	private const int ZAXIS = 2;

	private static (int i1, int i2)[] edges =
	{
		(0, 1),
		(0, 3),
		(0, 7),
		(1, 2),
		(1, 6),
		(2, 3),
		(2, 5),
		(3, 4),
		(4, 5),
		(4, 7),
		(5, 6),
		(6, 7)
	};

	private static int[] triangles = new int[] {
            0, 2, 1,
            0, 3, 2,
            2, 3, 4,
            2, 4, 5,
            1, 2, 5,
            1, 5, 6,
            0, 7, 4,
            0, 4, 3,
            5, 4, 7,
            5, 7, 6,
            0, 6, 7,
            0, 1, 6
        };

    public Vector3[] vertices { get; private set; }
	public Vector3[] rotated { get; private set; }

	private float l;
	private float w;
	private float h;
	private float collisionRadius;

	private float l2;
	private float w2;
	private float h2;

	public DetRect(DetRectProps props) : base(props)
	{
		l = props.length;
		w = props.width;
		h = props.height;

		calcDimensions();

		Mesh mesh = createMesh();
		gameObj.AddComponent<MeshFilter>().mesh = mesh;
	}

	public void modifyLength(float length)
    {
        if (length <= 0)
        {
            throw new Exception("length must be greater than 0");
        }
        DetRectProps rectProps = (DetRectProps) props;
        rectProps.length = length;
		l = rectProps.length;
		calcDimensions();
        gameObj.GetComponent<MeshFilter>().mesh = createMesh();
    }

    public void modifyWidth(float width)
    {
        if (width <= 0)
        {
            throw new Exception("width must be greater than 0");
        }
        DetRectProps rectProps = (DetRectProps) props;
        rectProps.width = width;
        w = rectProps.width;
        calcDimensions();
        gameObj.GetComponent<MeshFilter>().mesh = createMesh();
    }

    public void modifyHeight(float height)
    {
        if (height <= 0)
        {
            throw new Exception("height must be greater than 0");
        }
        DetRectProps rectProps = (DetRectProps) props;
        rectProps.height = height;
        h = rectProps.height;
        calcDimensions();
        gameObj.GetComponent<MeshFilter>().mesh = createMesh();
    }

	private void calcDimensions()
	{
        float x2 = l / 2;
		float x1 = -x2;
        float y2 = h / 2;
        float y1 = -y2;
        float z2 = w / 2;
        float z1 = -z2;

        collisionRadius = Mathf.Sqrt(x2 * x2 + y2 * y2 + z2 * z2);

		l2 = l * l;
		w2 = w * w;
		h2 = h * h;

        vertices = new Vector3[] {
            new Vector3(x1, y1, z1), // 0
			new Vector3(x2, y1, z1), // 1
			new Vector3(x2, y2, z1), // 2
			new Vector3(x1, y2, z1), // 3
			new Vector3(x1, y2, z2), // 4
			new Vector3(x2, y2, z2), // 5
			new Vector3(x2, y1, z2), // 6
			new Vector3(x1, y1, z2), // 7
		};
    }

	private Mesh createMesh()
	{
		Mesh mesh = new Mesh();
		mesh.vertices = this.vertices;
		mesh.triangles = triangles;
		mesh.Optimize();
		return mesh;
    }

    public void calcHitbox()
    {
        rotated = new Vector3[]
        {
            disalignVect(vertices[0]),
            disalignVect(vertices[1]),
            disalignVect(vertices[2]),
            disalignVect(vertices[3]),
            disalignVect(vertices[4]),
            disalignVect(vertices[5]),
            disalignVect(vertices[6]),
            disalignVect(vertices[7])
        };
    }

    public override void resetValues()
    {
        DetRectProps rectProps = (DetRectProps)props;
        l = rectProps.length;
        w = rectProps.width;
        h = rectProps.height;
        calcDimensions();
        gameObj.GetComponent<MeshFilter>().mesh = createMesh();
        base.resetValues();
    }

    public override void preTick()
    {
        base.preTick();
        calcHitbox();
    }

    public override void tick(float dt)
    {
        base.tick(dt);
    }

    public override bool tryClick(Vector3 dir, float dt)
    {
        if (!props.unstoppable && canClick())
        {
            int bestAxis = 0;
            int bestInvert = 0;
            float bestDist = float.MaxValue;
            Vector3 alignedDir = alignVect(dir);

            for (int axis = 0; axis < 3; axis++)
            {
                int invert = alignedDir[axis] < 0 ? -1 : 1;
                Vector3 forceDiff = alignedDir;
                forceDiff[axis] = 0;
                float dist = forceDiff.sqrMagnitude;
                if (dist < bestDist)
                {
                    bestDist = dist;
                    bestAxis = axis;
                    bestInvert = invert;
                }
            }

            Vector3 closest = Vector3.zero;
            closest[bestAxis] = bestInvert;
            closest = disalignVect(closest);

            Vector3 rotAxis = Vector3.Cross(closest, dir);
            if (rotAxis.magnitude != 0)
            {
                float angle = Mathf.Asin(rotAxis.magnitude);
                rotAxis = rotAxis.normalized;
                Vector3 needed = rotAxis * angle / dt;
                if ((ang - needed).magnitude < 0.05F)
                {
                    Debug.Log("Clicked");
                    angleMom = Vector3.zero;
                    ang = Vector3.zero;
                    moi = 0;
                    rot = Quaternion.AngleAxis(angle * DetMath.RAD_TO_DEG, rotAxis) * rot;
                    cacheTrig();
                    calcHitbox();
                    jitterCount = 0;
                    return true;
                }
            }

        }
        return false;

    }

    public override void addForce(Vector3 force, Vector3 location, float dt, bool couldClick)
    {
        base.addForce(force, location, dt, couldClick);


    }

    public override float getMoI(Vector3 vel)
    {
        if (vel.x == 0 && vel.y == 0 && vel.z == 0)
        {
            return 0;
        }
        Vector3 rotVel = alignVect(vel);
        float x2 = rotVel.x * rotVel.x;
        float y2 = rotVel.y * rotVel.y;
        float z2 = rotVel.z * rotVel.z;
        return props.mass * (z2 * h2 + y2 * w2 + z2 * l2 + x2 * w2 + x2 * h2 + y2 * l2) / (12 * (x2 + y2 + z2));
    }

    public override float getCollisionRadius()
	{
		return collisionRadius;
    }


    public override DetCollision getCollision(DetObject obj)
    {
        if (obj is DetRect)
        {
            DetRect rect = (DetRect) obj;

            RectCollision c1 = rect.getRectCollision(this);
            RectCollision c2 = this.getRectCollision(rect);
            RectCollision coll;
            DetObject obj1;
            DetObject obj2;
            Vector3 point1;
            Vector3 point2;
            Vector3 direction;

            if (c1 == null && c2 == null)
            {
                return null;
            }

            if (c2 == null || (c1 != null && c1.dist < c2.dist))
            {
                coll = c1;
                obj1 = this;
                obj2 = rect;
            }
            else
            {
                coll = c2;
                obj1 = rect;
                obj2 = this;
            }

            point1 = coll.point;
            point1[coll.axis] = coll.invert * getDimension(coll.axis) / 2;
            point1 = obj1.disalignVect(point1);
            point2 = coll.point;
            point2 = obj1.disalignVect(point2);
            Debug.Log("Point before: " + coll.point + "point after: " + point2);
            point2 = (point2 + obj1.pos) - obj2.pos;
            direction = new Vector3(0, 0, 0);
            direction[coll.axis] = coll.invert * -1;
            direction = obj1.disalignVect(direction);

            return new DetCollision(coll.dist, obj1, obj2, point1, point2, direction);

        }
        else if (obj is DetSphere)
        {
            DetSphere sphere = (DetSphere) obj;
            return sphere.getCollision(this);
        }

        return null;
    }

    private Line truncPoints(Vector3 point1, Vector3 point2)
	{
		Line line = new Line(point1, point2);

		for (int i = 0; i < 3; i++)
		{
			if (truncLine(line, i) == null)
			{
				return null;
			}
		}

		return line;
	}

	private Line truncLine(Line line, int axis)
    {
        float border = getDimension(axis) / 2;
		float v1;
		float v2;
		Vector3 trunc1;
		Vector3 trunc2;
		Vector3 slope = line.point2 - line.point1;

		if (line.point1[axis] >= 0)
		{
			v1 = line.point1[axis];
			v2 = line.point2[axis];
		}
		else
        {
            v1 = -line.point1[axis];
            v2 = -line.point2[axis];
        }
        if (v1 > border)
        {
            if (v2 > border)
            {
                return null;
            }
			trunc1 = (line.point1 + slope * (v1 - border) / (v1 - v2));
        }
        else
        {
			trunc1 = line.point1;
        }

        if (line.point2[axis] >= 0)
        {
            v1 = line.point1[axis];
            v2 = line.point2[axis];
        }
        else
        {
            v1 = -line.point1[axis];
            v2 = -line.point2[axis];
        }
        if (v2 > border)
        {
            if (v1 > border)
            {
                return null;
            }
            trunc2 = (line.point2 - slope * (v2 - border) / (v2 - v1));
        }
        else
        {
            trunc2 = line.point2;
        }

		line.point1 = trunc1;
		line.point2 = trunc2;
		return line;
	}

	public float getDimension(int axis)
	{
		switch (axis)
		{
			case XAXIS:
				return l;
			case YAXIS:
				return h;
			case ZAXIS:
				return w;
			default:
				return 0;
		}
	}

	private RectCollision getRectCollision(DetRect rect)
    {
        Vector3 diff = pos - rect.pos;
		bool collided = false;

		Vector3[] aligned = new Vector3[]
		{
				rect.alignVect(diff + rotated[0]),
				rect.alignVect(diff + rotated[1]),
				rect.alignVect(diff + rotated[2]),
				rect.alignVect(diff + rotated[3]),
				rect.alignVect(diff + rotated[4]),
				rect.alignVect(diff + rotated[5]),
				rect.alignVect(diff + rotated[6]),
				rect.alignVect(diff + rotated[7])
		};

		(
			Vector3 points, 
			float dist, 
			float len,
			int num,
			int axis,
			int invert
		) [] sides = {
			(new Vector3(0, 1, 0), 0, int.MinValue, 0, XAXIS, 1),
			(new Vector3(0, 2, 0), 0, int.MinValue, 0, XAXIS, -1),
			(new Vector3(0, 3, 0), 0, int.MinValue, 0, YAXIS, 1),
			(new Vector3(0, 4, 0), 0, int.MinValue, 0, YAXIS, -1),
			(new Vector3(0, 5, 0), 0, int.MinValue, 0, ZAXIS, 1),
			(new Vector3(0, 6, 0), 0, int.MinValue, 0, ZAXIS, -1)
		};

		foreach ((int index1, int index2) in edges)
		{
			Vector3 point1 = aligned[index1];
			Vector3 point2 = aligned[index2];

			Line line = rect.truncPoints(point1, point2);

			if (line != null)
            {
				collided = true;
				Vector3 trunc1 = line.point1;
				Vector3 trunc2 = line.point2;

                for (int i = 0; i < sides.Length; i++)
				{
					var side = sides[i];
					float border = rect.getDimension(side.axis) / 2;

					float len1 = border - (point1[side.axis] * side.invert);
					float len2 = border - (point2[side.axis] * side.invert);

					if (len1 > side.len)
					{
						side.points = trunc1;
						side.dist = border - (trunc1[side.axis] * side.invert);
						side.len = len1;
						side.num = 1;
                    }
					else if (len1 == side.len)
					{
						side.points = side.points + trunc1;
						side.num = side.num + 1;
                    }

                    if (len2 > side.len)
                    {
                        side.points = trunc2;
                        side.dist = border - (trunc2[side.axis] * side.invert);
                        side.len = len2;
                        side.num = 1;
                    }
                    else if (len2 == side.len)
                    {
                        side.points = side.points + trunc2;
                        side.num = side.num + 1;
                    }

                    sides[i] = side;

                }

            }


		}

		if (!collided)
		{
			return null;
		}

		RectCollision minCol = new RectCollision(new Vector3(0, 0, 0), 0, 0, 0);

		float minLen = int.MaxValue;

		foreach (var side in sides)
		{
			if (side.num > 0 && side.len < minLen)
			{
				minCol.point = side.points / side.num;
                minCol.dist = side.dist;
                minCol.axis = side.axis;
                minCol.invert = side.invert;
				minLen = side.len;
			}
		}

		return minCol;

    }

	private class RectCollision
	{
		public Vector3 point;
		public float dist;
		public int axis;
		public int invert;

		public RectCollision(Vector3 point, float dist, int axis, int invert)
		{
			this.point = point;
			this.dist = dist;
			this.axis = axis;
			this.invert = invert;
		}
	}

}

public class DetRectProps : DetProps
{
	private static int index = 0;
    public float length;
    public float width;
    public float height;

    public DetRectProps() : base()
    {
        length = 1;
        width = 1;
        height = 1;
    }

    public override DetObject createObject()
    {
        return new DetRect(this);
    }

    public override string createName()
	{
		string name = "RectangularPrism" + index;
		index++;
		return name;
	}

}