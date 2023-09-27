using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace LibMpv.WPF;

internal class VideoHwndHost: HwndHost
{
    public VideoHwndHost()
    {
        base.VerticalAlignment = VerticalAlignment.Stretch;
        base.HorizontalAlignment = HorizontalAlignment.Stretch;
    }

    protected override HandleRef BuildWindowCore(HandleRef hWndParent)
    {
        CustomWindProcDelegate = CustomWndProc;
        var windClass = new WNDCLASS();
        windClass.lpszClassName = CustomClassName;
        windClass.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(CustomWindProcDelegate);
        windClass.hbrBackground = CreateSolidBrush(0);

        var classAtom = RegisterClass(ref windClass);
        var lastError = Marshal.GetLastWin32Error();

        if (classAtom == 0 && lastError != ERROR_CLASS_ALREADY_EXISTS)
        {
            throw new Exception("Could not register window class");
        }

        IntPtr intPtr = CreateWindowEx(
            0,
            windClass.lpszClassName, 
            string.Empty, 
            WindowStyles.WS_CHILD | WindowStyles.WS_VISIBLE, 
            0, 0, 0, 0, 
            hWndParent.Handle, 
            IntPtr.Zero, 
            IntPtr.Zero, 
            IntPtr.Zero);

        lastError = Marshal.GetLastWin32Error();
        if (lastError != 0)
        {
            throw new Exception("Could not create window");
        }
        return new HandleRef(this, intPtr);
    }

    protected override IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == 132)
        {
            handled = true;
            return new IntPtr(-1);
        }
        handled = false;
        return IntPtr.Zero;
    }

    protected override void DestroyWindowCore(HandleRef hWnd)
    {
        DestroyWindow(hWnd.Handle);
    }

    private static IntPtr CustomWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        return DefWindowProc(hWnd, msg, wParam, lParam);
    }

    internal void RegisterWindow()
    {
        CustomWindProcDelegate = CustomWndProc;
        var windClass = new WNDCLASS();
        windClass.lpszClassName = CustomClassName;
        windClass.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(CustomWindProcDelegate);
        windClass.hbrBackground = CreateSolidBrush(0);

        var classAtom = RegisterClass(ref windClass);
        var lastError = Marshal.GetLastWin32Error();

        if (classAtom == 0 && lastError != ERROR_CLASS_ALREADY_EXISTS)
        {
            throw new Exception("Could not register window class");
        }
    }

    internal delegate IntPtr CustomWndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    internal CustomWndProcDelegate CustomWindProcDelegate;
    

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern IntPtr CreateWindowEx(
        ExtendedWindowStyles dwExStyle, 
        [MarshalAs(UnmanagedType.LPStr)] string lpClassName, 
        [MarshalAs(UnmanagedType.LPStr)] string lpWindowName, 
        WindowStyles dwStyle, 
        int x, int y, int nWidth, int nHeight, 
        IntPtr hWndParent, 
        IntPtr hMenu, 
        IntPtr hInstance, 
        IntPtr lpParam);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern bool DestroyWindow(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    internal static extern UInt16 RegisterClass([In] ref WNDCLASS lpWndClass);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr CreateSolidBrush(uint theColor);

    internal const string CustomClassName = "mpvcustom";

    internal const int ERROR_CLASS_ALREADY_EXISTS = 1410;

    [Flags]
    internal enum ExtendedWindowStyles : uint
    {
        WS_EX_TRANSPARENT = 0x00000020
    }

    [Flags]
    internal enum WindowStyles : uint
    {
        WS_CHILD = 0x40000000,
        WS_VISIBLE = 0x10000000
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct WNDCLASS
    {
        public readonly uint style;

        public IntPtr lpfnWndProc;

        public readonly int cbClsExtra;

        public readonly int cbWndExtra;

        public readonly IntPtr hInstance;

        public readonly IntPtr hIcon;

        public readonly IntPtr hCursor;

        public IntPtr hbrBackground;

        [MarshalAs(UnmanagedType.LPStr)]
        public readonly string lpszMenuName;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpszClassName;
    }
}