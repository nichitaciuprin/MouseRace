public struct Vector2
{
    public float x;
    public float y;
    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public static Vector2 Zero = new Vector2(0,0);
    public static bool Equals(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;
    public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x+b.x,a.y+b.y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x-b.x,a.y-b.y);
    public static Vector2 operator /(Vector2 a, float value) => new Vector2(a.x/value,a.y/value);
    public static Vector2 operator *(Vector2 a, float value) => new Vector2(a.x*value,a.y*value);
    public static Vector2 MoveTowards(Vector2 fromVec, Vector2 toVec, float delta)
    {
        if (Vector2.Equals(fromVec,toVec)) return fromVec;
        var diff = toVec-fromVec;
        var dist = diff.Length();
        if (dist <= delta) return toVec;
        var dir = diff.Normalized();
        var moveVec = dir*delta;
        return fromVec+moveVec;
    }
    public static float Distance(Vector2 v1, Vector2 v2)
    {
        var diff = v1-v2;
        return diff.Length();
    }
    public float Length()
    {
        var distSquared = x*x + y*y;
        var dist = MathF.Sqrt(distSquared);
        return dist;
    }
    public float LengthSquared()
    {
        var distSquared = x*x + y*y;
        return distSquared;
    }
    public Vector2 Normalized()
    {
        var dist = Length();
        var t = 1.0f / dist;
        return new Vector2(x*t,y*t);
    }
    public static Vector2 RandDirection()
    {
        var x = Helper.Range(-1,1);
        var y = Helper.Range(-1,1);
        var vec = new Vector2(x,y);
        return vec.Normalized();
    }
}