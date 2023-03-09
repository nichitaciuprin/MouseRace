public abstract class Element
{
    public Vector2 position;
    public float size;
    public float speed;
    public Element(Game game)
    {
        position = game.bound.RandPointInside();
        size = Helper.Range(10,20);
        speed = Helper.Range(10f,200f);
    }
    public abstract void Update(float deltaTime, Game game);
    public abstract void Render(GameWindow gameWindow);
    protected abstract bool IsMouseIntersect(Game game);
    protected abstract void OnHitTarget(Game game);
}
