
public static class Helper
{
    public static float Range(float min, float max) => min + ((max - min) * System.Random.Shared.NextSingle());
}