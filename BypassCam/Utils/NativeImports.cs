using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Point = OpenCvSharp.Point;

namespace NosGame.Utils;

public class NativeImports
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString,
        int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SetWindowText(IntPtr hWnd, string strText);

    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource,
        int nXSrc, int nYSrc, uint dwRop);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern bool DeleteDC(IntPtr hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    public static extern bool DeleteObject(IntPtr hObject);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    private const int SRCCOPY = 0x00CC0020;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
        {
        }

        public int X
        {
            get { return Left; }
            set
            {
                Right -= (Left - value);
                Left = value;
            }
        }

        public int Y
        {
            get { return Top; }
            set
            {
                Bottom -= (Top - value);
                Top = value;
            }
        }

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = value + Top; }
        }

        public int Width
        {
            get { return Right - Left; }
            set { Right = value + Left; }
        }

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(Left, Top); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(Width, Height); }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public static implicit operator System.Drawing.Rectangle(RECT r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(System.Drawing.Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
                return Equals((RECT) obj);
            else if (obj is System.Drawing.Rectangle)
                return Equals(new RECT((System.Drawing.Rectangle) obj));
            return false;
        }

        public override int GetHashCode()
        {
            return ((System.Drawing.Rectangle) this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture,
                "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }

    // Get the screenshot of the window with the specified handle
    public static Bitmap GetWindowScreenshot(IntPtr hwnd)
    {
        // get te hDC of the target window
        IntPtr hdcSrc = GetWindowDC(hwnd);
        // get the size
        RECT windowRect = new RECT();
        GetWindowRect(hwnd, out windowRect);
        int width = windowRect.Right - windowRect.Left;
        int height = windowRect.Bottom - windowRect.Top - 25;
        // create a device context we can copy to
        IntPtr hdcDest = CreateCompatibleDC(hdcSrc);
        // create a bitmap we can copy it to,
        // using GetDeviceCaps to get the width/height
        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
        // select the bitmap object
        IntPtr hOld = SelectObject(hdcDest, hBitmap);
        // bitblt over
        BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 25, SRCCOPY);
        // restore selection
        SelectObject(hdcDest, hOld);
        // clean up 
        DeleteDC(hdcDest);
        ReleaseDC(hwnd, hdcSrc);
        // get a .NET image object for it
        Image img = Image.FromHbitmap(hBitmap);
        // free up the Bitmap object
        DeleteObject(hBitmap);
        return new Bitmap(img);
    }

    private static uint WM_LBUTTONDOWN = 0x0201;
    private static uint WM_LBUTTONUP = 0x0202;
    private static uint WM_MOUSEMOVE = 0x0200;
    private static uint WM_RBUTTONDOWN = 0x0204;
    private static uint WM_RBUTTONUP = 0x0205;
    private static uint WM_KEYDOWN = 0x0100;
    private static uint WM_KEYUP = 0x0101;
    private static uint MK_LBUTTON = 0x0001;
    private static uint MK_RBUTTON = 0x0002;

    public static void ClickAt(IntPtr handle, Point point)
    {
        SendMessage(handle, WM_LBUTTONDOWN, MK_LBUTTON, (point.Y << 16) | point.X);
        SendMessage(handle, WM_LBUTTONUP, 0, (point.Y << 16) | point.X);
    }
    
    public static void MoveTo(IntPtr handle, Point point)
    {
        SendMessage(handle, WM_MOUSEMOVE, 0, (point.Y << 16) | point.X);
    }
    
    public static void ClickLeftArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x25, 0);
        SendMessage(handle, WM_KEYUP, 0x25, 0);
    }
    
    public static void ClickRightArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x27, 0);
        SendMessage(handle, WM_KEYUP, 0x27, 0);
    }
    
    public static void ClickUpArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x26, 0);
        SendMessage(handle, WM_KEYUP, 0x26, 0);
    }
    
    public static void ClickDownArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x28, 0);
        SendMessage(handle, WM_KEYUP, 0x28, 0);
    }

    public static void KeyDownLeftArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x25, 0);
    }
    
    public static void KeyDownRightArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x27, 0);
    }
    
    public static void KeyDownUpArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x26, 0);
    }
    
    public static void KeyDownDownArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x28, 0);
    }
    
    public static void KeyUpLeftArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYUP, 0x25, 0);
    }
    
    public static void KeyUpRightArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYUP, 0x27, 0);
    }
    
    public static void KeyUpDownArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYUP, 0x26, 0);
    }
    
    public static void KeyUpUpArrow(IntPtr handle)
    {
        SendMessage(handle, WM_KEYUP, 0x28, 0);
    }

    public static void ClickReturn(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x0D, 0);
        SendMessage(handle, WM_KEYUP, 0x0D, 0);
    }

    public static void ClickZero(IntPtr handle)
    {
        SendMessage(handle, WM_KEYDOWN, 0x30, 0);
        SendMessage(handle, WM_KEYUP, 0x30, 0);
    }
}