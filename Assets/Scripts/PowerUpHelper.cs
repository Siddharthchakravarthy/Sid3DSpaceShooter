using System;

public static class PowerUpHelper
{
    public static float SpawnTime;
    public static float PowerUpUseTime = 5.0f;
    public static bool CanUsePowerUp = false;
    public static float GetSpawnTime() {
        Random rnd = new Random();
        SpawnTime = rnd.Next(1, 20);
        return SpawnTime;
    }
}
