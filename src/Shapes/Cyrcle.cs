public struct Cyrcle
{
    public Vector2 center;
    public float radius;
    public Cyrcle(Vector2 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }
    public Rect OuterRect()
    {
        var p0 = center;
        var p1 = center;
        p0.x -= radius;
        p0.y -= radius;
        p1.x += radius;
        p1.y += radius;
        return new Rect(p0,p1);
    }
    public void Draw(GameWindow gameWindow, Raylib_cs.Color color)
    {
        gameWindow.DrawCircle((int)center.x,(int)center.y,radius,color);
    }
}