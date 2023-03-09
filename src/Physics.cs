public static class Physics
{
    public static Vector2 InsideBoundOffset(Rect bound, Cyrcle cyrcle)
    {
        var rect = cyrcle.OuterRect();
        return InsideBoundOffset(bound,rect);
    }
    public static Vector2 InsideBoundOffset(Rect bound, Rect rect)
    {
        var xOffset = 0f;
        var yOffset = 0f;
        if      (bound.MaxX < rect.MaxX) xOffset = bound.MaxX - rect.MaxX;
        else if (bound.MinX > rect.MinX) xOffset = bound.MinX - rect.MinX;
        if      (bound.MaxY < rect.MaxY) yOffset = bound.MaxY - rect.MaxY;
        else if (bound.MinY > rect.MinY) yOffset = bound.MinY - rect.MinY;
        return new Vector2(xOffset,yOffset);
    }
    public static bool InsideBound(Rect bound, Cyrcle cyrcle)
    {
        var rect = cyrcle.OuterRect();
        return InsideBound(bound,rect);
    }
    public static bool InsideBound(Rect bound, Rect rect)
    {
        if      (bound.MaxX < rect.MaxX) return false;
        else if (bound.MinX > rect.MinX) return false;
        if      (bound.MaxY < rect.MaxY) return false;
        else if (bound.MinY > rect.MinY) return false;
        return true;
    }
    public static bool Collision(Vector2 point, Cyrcle sphere)
    {
        return Vector2.Distance(point, sphere.center) < sphere.radius;
    }
    public static bool Collision(Vector2 point, Rect rect)
    {
        if (point.x < rect.MinX) return false;
        if (point.x > rect.MaxX) return false;
        if (point.y < rect.MinY) return false;
        if (point.y > rect.MaxY) return false;
        return true;
    }
    public static bool Collision(Cyrcle a, Cyrcle b)
    {
        var dist1 = Vector2.Distance(a.center, a.center);
        var dist2 = a.radius + b.radius;
        return dist1 < dist2;
    }
}
