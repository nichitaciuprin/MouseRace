public struct Rect
{
    public Vector2 p0;
    public Vector2 p1;
    public Rect(Vector2 p0, Vector2 p1)
    {
        this.p0 = p0;
        this.p1 = p1;
    }
    public float MinX => MathF.Min(p0.x,p1.x);
    public float MaxX => MathF.Max(p0.x,p1.x);
    public float MinY => MathF.Min(p0.y,p1.y);
    public float MaxY => MathF.Max(p0.y,p1.y);
    public Vector2 Center => (p0+p1)/2;
    public float Width => MaxX-MinX;
    public float Height => MaxY-MinY;
    public Rect Move(Vector2 offset)
    {
        return new Rect(p0+offset,p1+offset);
    }
    public Vector2 RandPointInside()
    {
        var x = Helper.Range(MinX,MaxX);
        var y = Helper.Range(MinY,MaxY);
        return new Vector2(x,y);
    }
    public void Draw(GameWindow gameWindow, Raylib_cs.Color color)
    {
        gameWindow.DrawRect((int)(Center.x-Width/2),(int)(Center.y-Height/2),(int)Width,(int)Height,color);
    }
}