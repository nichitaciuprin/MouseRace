public class Chase : Element
{
    public Chase(Game game) : base(game) {}
    public override void Update(float deltaTime, Game game)
    {
        if (IsMouseIntersect(game))
        {
            OnHitTarget(game);
            return;
        }
        position = Vector2.MoveTowards(position,game.mousePosition,speed*deltaTime);
    }
    public override void Render(GameWindow gameWindow)
    {
        GetShape().Draw(gameWindow,Raylib_cs.Color.RED);
    }
    protected override bool IsMouseIntersect(Game game)
    {
        return Physics.Collision(game.mousePosition,GetShape());
    }
    protected override void OnHitTarget(Game game)
    {
        game.EndGame();
    }
    private Rect GetShape()
    {
        var p0 = position;
        var p1 = position;
        p0.x += size*2;
        p0.y += size;
        p1.x -= size*2;
        p1.y -= size;
        return new Rect(p0,p1);
    }
}