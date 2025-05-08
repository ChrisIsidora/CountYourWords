using CountYourWords.Application.Interfaces;

namespace CountYourWords.Application.Services;

public class ScrambleService : IScrambleService
{
    public string Scramble(string stringContent)
    {
        return string.Join(string.Empty,
            stringContent.Reverse().Select((c, i) => i % 2 == 0 ? char.ToLower(c) : char.ToUpper(c)));
    }
}