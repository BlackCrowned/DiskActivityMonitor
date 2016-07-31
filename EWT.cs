using System;

namespace DiskUsageAnalizer
{
    using System.Runtime.InteropServices;

//    [StructLayout(LayoutKind.Sequential)]
//    public struct GUID
//    {
//        public uint a;
//        public short b;
//        public short c;
//        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
//        public byte[] d;
//    }

//    [StructLayout(LayoutKind.Explicit)]
//    public struct WnodeHeader
//    {
//        [FieldOffset(0)]
//        public uint BufferSize;
//        [FieldOffset(4)]
//        public uint ProviderId;
//            [FieldOffset(8)]
//        public ulong HistoricalContext;
//            [FieldOffset(8)]
//        public uint Version;
//            [FieldOffset(12)]
//        public uint Linkage;
        
//            [FieldOffset(16)]
//        public IntPtr KernelHandle;
//            [FieldOffset(16)]
//        public long TimeStamp;
//        [FieldOffset(24)]
//        public GUID Guid;
//        [FieldOffset(40)]
//        public uint ClientContext;
//        [FieldOffset(44)]
//        public uint Flags;
//    }

//    [StructLayout(LayoutKind.Sequential)]
//    public struct EventTraceProperties
//{
//        public WnodeHeader Wnode;
//        public uint BufferSize;
//        public uint MinimumBuffers;
//        public uint MaximumBuffers;
//        public uint MaximumFileSize;
//        public uint LogFileMode;
//        public uint FlushTimer;
//        public uint EnableFlags;
//        public int AgeLimit;
//        public uint NumberOfBuffers;
//        public uint FreeBuffers;
//        public uint EventsLost;
//        public uint BuffersWritten;
//        public uint LogBuffersLost;
//        public uint RealTimeBuffersLost;
//        public IntPtr LoggerThreadId;
//        public uint LogFileNameOffset;
//        public uint LoggerNameOffset;
//    }

    public static class Ewt
    {
        //public static readonly string KERNEL_LOGGER_NAME = "NT Kernel Logger";

        //public static readonly GUID SystemTraceControlGuid = new GUID() { a = 0x9e814aad, b = 0x3204, c = 0x11d2, d = new byte[]{ 0x9a, 0x82, 0x00, 0x60, 0x08, 0xa8, 0x69, 0x39 } };

        //[DllImport("sechost.dll")]
        //public static extern ulong StartStrace(out IntPtr sessionHandle, [In] string sessionName, ref EventTraceProperties properties);

        [DllImport("RealTimeEWTListener.dll", EntryPoint="genRTL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void genRTL(out IntPtr handle);

        [DllImport("RealTimeEWTListener.dll", EntryPoint = "deleteRTL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void deleteRTL(ref IntPtr handle);

        [DllImport("RealTimeEWTListener.dll", EntryPoint = "rtlStartTrace", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint rtlStartTrace([In] IntPtr handle);

        [DllImport("RealTimeEWTListener.dll", EntryPoint = "rtlOpenTrace", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint rtlOpenTrace([In] IntPtr handle);

        [DllImport("RealTimeEWTListener.dll", EntryPoint = "rtlStartConsumption", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint rtlStartConsumption([In] IntPtr handle);

        [DllImport("RealTimeEWTListener.dll", EntryPoint = "rtlStopConsumption", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint rtlStopConsumption([In] IntPtr handle);
    }
}
