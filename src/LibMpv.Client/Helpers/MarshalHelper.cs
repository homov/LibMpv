using System.Runtime.InteropServices;
using System.Text;

namespace LibMpv.Client;

public class MarshalHelper : IDisposable
{
    bool disposed = false;
    public static TStruct PtrToStructure<TStruct>(IntPtr ptr) where TStruct : struct
    {
        if (ptr == IntPtr.Zero)
            throw new ArgumentException("Invalid pointer.");

        return (TStruct)Marshal.PtrToStructure(ptr, typeof(TStruct));
    }


    public static string PtrToStringUTF8OrEmpty(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero) return String.Empty;
        return UTF8Marshaler.FromNative(Encoding.UTF8, ptr);
    }

    public static string? PtrToStringUTF8OrNull(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero) return null;
        return UTF8Marshaler.FromNative(Encoding.UTF8, ptr);
    }


    struct AllocBlock
    {
        public bool isHGlobal;
        public IntPtr intPtr;
        public AllocBlock(IntPtr intPtr, bool isHGlobal=true)
        {
            this.intPtr = intPtr;
            this.isHGlobal = isHGlobal;
        }
    }

    private List<AllocBlock> toBeFree = new List<AllocBlock>();

    public nint CStringFromManagedUTF8String(string @string)
    {
        @string += '\0';

        var stringBytes = Encoding.UTF8.GetBytes(@string);
        var stringBytesCount = stringBytes.Length;

        var stringPtr = Marshal.AllocCoTaskMem(stringBytesCount);
        Marshal.Copy(stringBytes, 0, stringPtr, stringBytesCount);

        toBeFree.Add(new AllocBlock(stringPtr, false));
        return stringPtr;
    }

    public nint CStringArrayForManagedUTF8StringArray(string[] inArray)
    {
        var numberOfStrings = inArray.Length + 1;

        nint[] outArray = new IntPtr[numberOfStrings];

        var rootPointer = Marshal.AllocCoTaskMem(IntPtr.Size * numberOfStrings);
        toBeFree.Add(new AllocBlock(rootPointer, false));

        for (var index = 0; index < inArray.Length; index++)
        {
            var currentString = inArray[index];
            var currentStringPtr = CStringFromManagedUTF8String(currentString);

            outArray[index] = currentStringPtr;
        }

        Marshal.Copy(outArray, 0, rootPointer, numberOfStrings);

        return rootPointer;
    }

    public nint StringToHGlobalAnsi(string value)
    {
        var ptr = Marshal.StringToHGlobalAnsi(value);
        toBeFree.Add(new AllocBlock(ptr));
        return ptr;
    }

    public nint AllocHGlobal<T>()
    {
        return this.AllocHGlobal(Marshal.SizeOf<T>());
    }

    public nint AllocHGlobal<T>(T instance) where T : struct
    {
        var ptr = this.AllocHGlobal(Marshal.SizeOf<T>());
        Marshal.StructureToPtr(instance, ptr, false);
        return ptr;
    }

    public nint AllocHGlobalValue(int value)
    {
        var ptr = this.AllocHGlobal(sizeof(int));
        Marshal.WriteInt32(ptr, value);
        return ptr;
    }


    public nint AllocHGlobal(int cb)
    {
        var ptr = Marshal.AllocHGlobal(cb);
        toBeFree.Add(new AllocBlock(ptr));
        return ptr;
    }
    public void Dispose()
    {
        if (disposed) return;
        foreach(var item in toBeFree)
        {
            if (item.isHGlobal)
                Marshal.FreeHGlobal(item.intPtr);
            else
                Marshal.FreeCoTaskMem(item.intPtr);
        }
        disposed = true;
    }
}
