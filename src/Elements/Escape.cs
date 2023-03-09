public class Escape : Element
{
    public Escape(Game game) : base(game) {}
    public override void Update(float deltaTime, Game game)
    {
        if (IsMouseIntersect(game))
        {
            OnHitTarget(game);
            return;
        }
        var dir = (position-game.mousePosition).Normalized();
        position += dir*speed*deltaTime;
        position += Physics.InsideBoundOffset(game.bound,GetShape());
    }
    public override void Render(GameWindow gameWindow)
    {
        GetShape().Draw(gameWindow,Raylib_cs.Color.GREEN);
    }
    protected override bool IsMouseIntersect(Game game)
    {
        return Physics.Collision(game.mousePosition,GetShape());
    }
    protected override void OnHitTarget(Game game)
    {
        game.AddExtraScore(5f);
        position = game.bound.RandPointInside();
    }
    private Rect GetShape()
    {
        var p0 = position;
        var p1 = position;
        p0.x += size;
        p0.y += size;
        p1.x -= size;
        p1.y -= size;
        return new Rect(p0,p1);
    }
}