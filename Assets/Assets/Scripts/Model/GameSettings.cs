using System.Collections.Generic;

public class GameSettings
{
    private readonly List<string> clickableObjectTags = new List<string>() { "InputArea", "Dog", "Cow", "Paddlock", "Bonus" };
    private const float unitMoveSpeed = 200f;
    private const int dogsCount = 3;
    private const int cowsCount = 25;
    private const int winCount = 25;
    private const int paddlocksCount = 1;
    private const float bonusLifeTime = 10f;
    private const int bonusCowsCount = 5;

    public float cowGenerateRadius = 50f;
    public float dogGenerateRadius = 80f;
    public float paddlocGeneratekRadius = 300f;

    public float UnitMoveSpeed
    {
        get { return unitMoveSpeed; }
    }

    public int DogsCount
    {
        get { return dogsCount;}
    }

    public int CowsCount
    {
        get { return cowsCount; }
    }

    public int PaddlocksCount
    {
        get { return paddlocksCount; }
    }

    public int WinCount
    {
        get { return winCount; }
    }

    public List<string> ClickableObjectTags
    {
        get
        {
            return clickableObjectTags;
        }
    }

    public float BonusLifeTime
    {
        get { return bonusLifeTime; }
    }

    public int BonusCowsCount
    {
        get { return bonusCowsCount; }
    }
}
