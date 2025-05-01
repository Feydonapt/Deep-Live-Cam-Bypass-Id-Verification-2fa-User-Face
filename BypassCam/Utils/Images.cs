using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;

namespace NosGame.Utils;

public class Images
{
    private static Mat GetImage(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream("NosGame.images." + name);
        var bitmap = new Bitmap(stream);
        var mat = bitmap.ToMat();
        return mat;
    }

    public static readonly Mat ArrowDown = GetImage("arrow_down.jpg");
    public static readonly Mat ArrowUp = GetImage("arrow_up.jpg");
    public static readonly Mat ArrowLeft = GetImage("arrow_left.jpg");
    public static readonly Mat ArrowRight = GetImage("arrow_right.jpg");

    public static readonly Mat CouponCheck = GetImage("coupon_check.jpg");
    public static readonly Mat NotEnoughPoints = GetImage("not_enough_points.png");

    public static readonly Mat OpenGame = GetImage("open_game.jpg");
    public static readonly Mat StartingGame = GetImage("starting_game.png");
    public static readonly Mat StartGame = GetImage("start_game.png");
    
    public static readonly Mat Digits = GetImage("score_digits.png");
    
    public static readonly Mat Result = GetImage("result.png");
    
    public static readonly Mat LevelButton = GetImage("level_button.png");

    public struct TemplateResult
    {
        public Point Location;
        public readonly double Value;
        
        public TemplateResult(Point location, double value)
        {
            Location = location;
            Value = value;
        }
        
        public override string ToString()
        {
            return $"Location: {Location}, Value: {Value}";
        }
    }

    public static TemplateResult FindTemplate(Mat source, Mat template)
    {
        var result = new Mat(source.Rows - template.Rows + 1, source.Cols - template.Cols + 1, MatType.CV_32FC1);
        source = source.CvtColor(ColorConversionCodes.RGB2GRAY);
        template = template.CvtColor(ColorConversionCodes.RGB2GRAY);
        Cv2.MatchTemplate(source, template, result, TemplateMatchModes.CCoeffNormed);
        Cv2.MinMaxLoc(result, out _, out var maxval, out _, out var maxLoc);

        return new TemplateResult(maxLoc, maxval);
    }
    
    public static TemplateResult FindTemplateNoConversion(Mat source, Mat template)
    {
        var result = new Mat(source.Rows - template.Rows + 1, source.Cols - template.Cols + 1, MatType.CV_32FC1);
        Cv2.MatchTemplate(source, template, result, TemplateMatchModes.CCoeffNormed);
        Cv2.MinMaxLoc(result, out _, out var maxval, out _, out var maxLoc);

        return new TemplateResult(maxLoc, maxval);
    }

    public static bool PixelTolerance(Vec3b source, Vec3b pixelToCheck, int tolerance)
    {
        return Math.Abs(source.Item0 - pixelToCheck.Item0) <= tolerance &&
               Math.Abs(source.Item1 - pixelToCheck.Item1) <= tolerance &&
               Math.Abs(source.Item2 - pixelToCheck.Item2) <= tolerance;
    }
}