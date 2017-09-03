using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestMAP
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Data
    {
        public UInt16 pacheader;		/* Title is a constant and equal 0xABCD */
        public byte type;			/* Type of message */
        public byte len;			/* size of buf; < MAX_BUF unsigned chars */
        public UInt16 checksum;
        public static Data FromBytes(byte[] bytes)
        {
            GCHandle gcHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var data = (Data)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(Data));
            gcHandle.Free();
            return data;
        }
        public static byte[] ObjectToByteArray(Object obj)
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //using (var ms = new MemoryStream())
            //{
            //    bf.Serialize(ms, obj);
            //    return ms.ToArray();
            //}

            var size = Marshal.SizeOf(obj);
            // Both managed and unmanaged buffers required.
            var bytes = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            // Copy object byte-to-byte to unmanaged memory.
            Marshal.StructureToPtr(obj, ptr, false);
            // Copy data from unmanaged memory to managed buffer.
            Marshal.Copy(ptr, bytes, 0, size);
            // Release unmanaged memory.
            Marshal.FreeHGlobal(ptr);

            return bytes;
        }
    }

    class Data1
    {
        public Data header = new Data();
        public UInt16 data1;
        public byte data2;

        public static Data1 FromBytes(byte[] bytes)
        {
            GCHandle gcHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var data = (Data1)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(Data1));
            gcHandle.Free();
            return data;
        }
        public static byte[] ObjectToByteArray(Object obj)
        {
            var size = Marshal.SizeOf(obj);
            // Both managed and unmanaged buffers required.
            var bytes = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            // Copy object byte-to-byte to unmanaged memory.
            Marshal.StructureToPtr(obj, ptr, false);
            // Copy data from unmanaged memory to managed buffer.
            Marshal.Copy(ptr, bytes, 0, size);
            // Release unmanaged memory.
            Marshal.FreeHGlobal(ptr);

            return bytes;
        }
    }
     [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ReceiveBytesData
    {
        public Header headerPacket  = new Header();
        public Packet_CA packet_CA = new Packet_CA();

        public ReceiveBytesData() { }
        public ReceiveBytesData(byte[] receivebyte)
        {
            var segmentHeader = new ArraySegment<byte>(receivebyte, 0, Marshal.SizeOf(typeof(Header)));

            switch (GetTypeData(segmentHeader))
            {
                case 0xCA:
                    packet_CA = new Packet_CA();
                    var segmentCA = new ArraySegment<byte>(receivebyte, Marshal.SizeOf(typeof(Header)), Marshal.SizeOf(typeof(Packet_CA)));
                    GetPacket<Packet_CA>(segmentCA, ref packet_CA);
                    break;
                default:
                    break;
            }
        }

        private void GetPacket<AnyPacket>(ArraySegment<byte> segmentPacket, ref AnyPacket obj)
        {
            if (headerPacket.len == segmentPacket.Count)
            {
                UInt16 ch = calc_cs(segmentPacket.ToArray(), headerPacket.len);
                if (ch == headerPacket.checksum)
                {
                    var packet = obj as Object;
                    ByteArrayToStructure(segmentPacket.ToArray(), ref packet);
                    obj = (AnyPacket)packet;
                }
            }
        }

        private byte GetTypeData(ArraySegment<byte> segmentHeader)
        {
            byte ret = 0;
            byte[] tmp = new byte[3];
            Object header = new Header();
            ByteArrayToStructure(segmentHeader.ToArray(), ref header);
            headerPacket = header as Header;
            if (headerPacket.pacheader == 0xABCD)
                ret = headerPacket.type;

            return ret;
        }

        private UInt16 calc_cs(byte[] buf, byte len)
        { //checksum function
            UInt16 res;
            UInt16 i;
            res = 0xABCD;
            for (i = 0; i < len; i+=2)
            {
                if (len - i > 1)
                {
                    res ^= BitConverter.ToUInt16(buf, i);
                }
                else
                    res ^= (UInt16)buf[i];
            }
            return res;
        }

        public static byte[] StructToByteArray(object structure)
        {
            int sizeInBytes = System.Runtime.InteropServices.Marshal.SizeOf(structure);
            byte[] outArray = new byte[sizeInBytes];

            IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeInBytes);
            System.Runtime.InteropServices.Marshal.StructureToPtr(structure, ptr, true);
            System.Runtime.InteropServices.Marshal.Copy(ptr, outArray, 0, sizeInBytes);
            System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr);

            return outArray;
        }


        public static void ByteArrayToStructure(byte[] bytes, ref object structure)
        {
            int sizeInBytes = System.Runtime.InteropServices.Marshal.SizeOf(structure);
            IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeInBytes);

            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, ptr, sizeInBytes);
            structure = System.Runtime.InteropServices.Marshal.PtrToStructure(ptr, structure.GetType());
            System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr);
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Header
    {
        public UInt16 pacheader;	/* Title is a constant and equal 0xABCD */
        public byte type;			/* Type of message */
        public byte len;			/* size of buf; < MAX_BUF unsigned chars */
        public UInt16 checksum;	/* calculate by checksum function "calc_cs"; calculating only by body of packet */
        public Header() { }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Packet_CA
    {
        public byte axis_X;
        public byte axis_Y;
        public byte axis_Z;
        public byte button1;
        public byte button2;
        public byte button3;
        public byte button4;
        public byte button5;
        public byte enable_auto;
        public byte control_mode;

        public Packet_CA() { }
    } 

}
