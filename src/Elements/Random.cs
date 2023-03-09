public class Random : Element
{
    private Vector2 direction;
    public Random(Game game) : base(game)
    {
        ChangeDirection();
    }
    public override void Update(float deltaTime, Game game)
    {
        if (IsMouseIntersect(game))
        {
            OnHitTarget(game);
            return;
        }

        position += direction*speed*deltaTime;
        var offset = Physics.InsideBoundOffset(game.bound,GetShape());
        if (Vector2.Equals(offset,Vector2.Zero)) return;
        position += offset;
        ChangeDirection();
        // reduse shaking
        position += offset.Normalized()*5;
    }
    public override void Render(GameWindow gameWindow)
    {
        GetShape().Draw(gameWindow,Raylib_cs.Color.YELLOW);
    }
    protected override bool IsMouseIntersect(Game game)
    {
        return Physics.Collision(game.mousePosition,GetShape());
    }
    protected override void OnHitTarget(Game game)
    {
        game.EndGame();
    }
    private void ChangeDirection()
    {
        direction = Vector2.RandDirection();
    }
    private Cyrcle GetShape()
    {
        var center = new Vector2(position.x,position.y);
        return new Cyrcle(center,size);
    }
}