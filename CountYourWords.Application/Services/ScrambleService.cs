using CountYourWords.Application.Interfaces;

namespace CountYourWords.Application.Services;

public class ScrambleService : IScrambleService
{
    public string Scramble(string stringContent)
    {
        return string.Join(string.Empty,
            stringContent.Reverse().Select((c, i) => i % 2 == 0 ? char.ToUpper(c) : char.ToLower(c)));
    }
}