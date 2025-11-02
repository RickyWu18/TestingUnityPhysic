using UnityEngine;

// This class is used to transform coordinate system between NED, FRD to Unity's left-hand system
public class Coordinate
{
    // NED to Unity
    public static Vector3 NED2Unity(Vector3 ned)
    {
        return new Vector3(ned.y, -ned.z, ned.x);
    }

    // Unity to NED
    public static Vector3 Unity2NED(Vector3 unity)
    {
        return new Vector3(unity.z, unity.x, -unity.y);
    }

    // FRD to Unity
    public static Vector3 FRD2Unity(Vector3 frd)
    {
        return new Vector3(frd.x, -frd.z, -frd.y);
    }

    // Unity to FRD
    public static Vector3 Unity2FRD(Vector3 unity)
    {
        return new Vector3(unity.x, -unity.z, -unity.y);
    }
}
