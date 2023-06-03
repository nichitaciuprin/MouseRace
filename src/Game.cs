public class Game
{
public GameState gameState { get; private set; }
    public float score { get; private set; }
    public Element[] elements { get; private set; }
    public readonly Rect bound;
    public Vector2 mousePosition;

    public Game(Rect bound)
    {
        this.bound = bound;
        this.elements = Array.Empty<Element>();
    }
    public void Update(float deltaTime)
    {
        switch (gameState)
        {
            case Game.GameState.Play:
            {
                score += deltaTime;
                foreach (var element in elements)
                    element.Update(deltaTime,this);
                return;
            }
        }
    }
    public void StartGame()
    {
        if (gameState != GameState.Start) return;
        gameState = GameState.Play;
        elements = new Element[]
        {
            new Chase(this),
            new Chase(this),
            new Chase(this),
            new Chase(this),
            new Random(this),
            new Random(this),
            new Random(this),
            new Random(this),
            new Escape(this),
            new Escape(this),
            new Escape(this),
            new Escape(this),
        };
    }
    public void EndGame()
    {
        gameState = GameState.GameOver;
    }
    public void AddExtraScore(float value)
    {
        if (value <= 0) return;
        score += value;
    }
    public enum GameState
    {
        Start,
        Play,
        GameOver
    }
}