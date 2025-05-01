using System;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace NosGame.Utils;

public class ReadPoints
{
    private static readonly Mat[] DigitsArray = new Mat[10];

    public static void LoadArray()
    {
        for (var i = 0; i < 10; i++)
        {
            var x = i * 20 + 1;
            var y = 0 + 1;
            DigitsArray[i] = Images.Digits[new Rect(x, y, 20, 20)];
        }

        Console.WriteLine("Loaded array");
    }

    public static void GetPoints(Mat source, ref int points)
    {
        var resultPoints = new int[10];
        source = source.CvtColor(ColorConversionCodes.BGR2GRAY);
        for (var j = 0; j < 10; j++)
        {
            Mat threshold = new Mat();
            Cv2.Threshold(source[new Rect(j * 22, 1, source.Width / 10, source.Height - 1)], threshold, 0, 255,
                ThresholdTypes.BinaryInv | ThresholdTypes.Otsu);
            for (var i = 0; i < DigitsArray.Length; i++)
            {
                var template = DigitsArray[i].CvtColor(ColorConversionCodes.BGR2GRAY);
                var result = Images.FindTemplateNoConversion(threshold, template);
                if (!(result.Value > 0.9)) continue;
                resultPoints[j] = i;
                break;
            }
        }

        var resultPoint = int.Parse(string.Join("", resultPoints.Select(x => x.ToString()).ToArray()));
        points = resultPoint;
    }
}