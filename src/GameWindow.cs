using Raylib_cs;

// cant create multiple windows in Raylib
public class GameWindow
{
    public uint width;
    public uint heigth;
    public GameWindow(Game game)
    {
        this.width = (uint)game.bound.Width;
        this.heigth = (uint)game.bound.Height;
        Raylib.InitWindow((int)width, (int)heigth, "MouseRace");
    }
    public bool ShouldClose => Raylib.WindowShouldClose();
    public void Render(Game game)
    {
        game.mousePosition = GetMousePosition();

        Raylib.ClearBackground(Raylib_cs.Color.BLACK);
        Raylib.BeginDrawing();
        switch (game.gameState)
        {
            case Game.GameState.Start:
            {
                RenderButton(game.bound.Center,game.StartGame);
                break;
            }
            case Game.GameState.Play:
            {
                foreach (var element in game.elements)
                    element.Render(this);

                Raylib.DrawText(((int)game.score).ToString(),0,0,30,Color.WHITE);
                break;
            }
            case Game.GameState.GameOver:
            {
                Raylib.DrawText(((int)game.score).ToString(),0,0,30,Color.WHITE);
                Raylib.DrawText("GameOver",(int)game.bound.Center.x-70,(int)game.bound.Center.y-13,30,Color.WHITE);
                break;
            }
        }
        Raylib.EndDrawing();
    }
    public void DrawRect(int posX, int posY, int width, int height, Color color) => Raylib.DrawRectangle(posX, posY, width, height, color);
    public void DrawCircle(int centerX, int centerY, float radius, Color color) => Raylib.DrawCircle(centerX, centerY, radius, color);
    private void RenderButton(Vector2 position, Action onClick)
    {
        var rect = GetButtonRect(position);
        var center = rect.Center;
        var color = IsMouseHower(position) ? Color.RED : Color.WHITE;
        Raylib.DrawText("START",(int)center.x-54,(int)center.y-13,30,color);
        if (!IsMouseHower(position)) return;
        if (!IsMousePressed()) return;
        onClick();
    }
    private Rect GetButtonRect(Vector2 position)
    {
        var width2 = 58;
        var height2 = 15;
        var center = position;
        var p0 = center;
        var p1 = center;
        p0.x -= width2;
        p1.x += width2;
        p0.y -= height2;
        p1.y += height2;
        return new Rect(p0,p1);
    }
    private Vector2 GetMousePosition()
    {
        var vec = Raylib.GetMousePosition();
        return new Vector2(vec.X,vec.Y);
    }
    private bool IsMouseHower(Vector2 position) => Physics.Collision(GetMousePosition(),GetButtonRect(position));
    private bool IsMousePressed() => Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON);
}
