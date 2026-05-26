using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace OSExpertSystemWinUI.UI;

public static class UiBrushes
{
    public static SolidColorBrush Brush(byte a, byte r, byte g, byte b)
        => new(Color.FromArgb(a, r, g, b));

    public static SolidColorBrush Brush(byte r, byte g, byte b)
        => new(Color.FromArgb(255, r, g, b));

    public static SolidColorBrush Ink => Brush(17, 24, 39);
    public static SolidColorBrush Muted => Brush(71, 85, 105);
    public static SolidColorBrush Card => Brush(255, 255, 255, 255);
    public static SolidColorBrush CardText => Brush(17, 24, 39);
    public static SolidColorBrush CardMuted => Brush(71, 85, 105);
    public static SolidColorBrush Blue => Brush(37, 99, 235);
    public static SolidColorBrush Cyan => Brush(14, 165, 233);
    public static SolidColorBrush Green => Brush(5, 150, 105);
    public static SolidColorBrush Violet => Brush(124, 58, 237);
    public static SolidColorBrush White => new(Colors.White);

    public static SolidColorBrush SectionHeader => Brush(255, 23, 32, 51);
    public static SolidColorBrush SectionHeaderBorder => Brush(255, 51, 65, 85);
    public static SolidColorBrush ScorePanel => Brush(255, 255, 247, 237);
    public static SolidColorBrush ScoreBorder => Brush(255, 249, 115, 22);
    public static SolidColorBrush ScoreText => Brush(15, 23, 42);
    public static SolidColorBrush ScoreMuted => Brush(124, 45, 18);
    public static SolidColorBrush RuleHitBackground => Brush(255, 248, 250, 252);
}
