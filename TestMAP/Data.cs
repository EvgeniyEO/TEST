﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestMAP
{
    class PASHR_DATA
    {
        public double Heading;
        public double Roll;
        public double Pitch;
        public PASHR_DATA()
        {
            Heading = 0;
            Roll = 0;
            Pitch = 0;
        }
    }
    class ReceiveBytesDataMoxa
    {
        public PASHR_DATA pashr_data = new PASHR_DATA();
        static int MAX_BUF = 500;
        byte[] Buffer = new byte[MAX_BUF];
        UInt32 index = 0;
        UInt32 index_chk = 0;
        bool isDataReady = false;
        enum Status { Header, Body, CheckSum, Data };
        Status status = Status.Header;


        public ReceiveBytesDataMoxa()
        {

        }

        public bool StoreBytes(byte[] receivebyte)
        {
            bool result = false;

            if ((receivebyte.Length + index) > MAX_BUF)
            {
                Array.Clear(Buffer, 0, MAX_BUF);
                index = 0;
            }
            for (int i = 0; i < receivebyte.Length; i++)
            {
                switch (status)
                {
                    case Status.Header:
                        switch (index)
                        {
                            case 0:
                                index = (receivebyte[i] == 0x24) ? index + 1 : 0;
                                break;
                            case 1:
                                index = (receivebyte[i] == 0x50) ? index + 1 : 0;
                                break;
                            case 2:
                                index = (receivebyte[i] == 0x41) ? index + 1 : 0;
                                break;
                            case 3:
                                index = (receivebyte[i] == 0x53) ? index + 1 : 0;
                                break;
                            case 4:
                                index = (receivebyte[i] == 0x48) ? index + 1 : 0;
                                break;
                            case 5:
                                if ( receivebyte[i] == 0x52 )
                                {
                                    status = Status.Body;
                                    index++;
                                }
                                else
                                {
                                    index = 0;
                                }
                                break;
                            default:
                                index = 0;
                                break;
                        }
                        if ( index > 1 )
                        {
                            Buffer[index - 2] = receivebyte[i];
                            if (status == Status.Body)
                                index--;
                        }
                        break;
                    case Status.Body:
                        switch (receivebyte[i])
	                    {
                            case 0x24:
                                status = Status.Header;
                                index = 0;
                                break;
                            case 0x2a:
                                status = Status.CheckSum;
                                index_chk = 0;
                                break;
		                    default:
                                Buffer[index] = receivebyte[i];
                                index++;
                                if (index > MAX_BUF)
                                {
                                    index = 0;
                                    status = Status.Header;
                                }
                                break;
	                    }
                        break;
                    case Status.CheckSum:
                        switch (index_chk)
	                    {
                            case 0:
                                Buffer[index++] = receivebyte[i];
                                index_chk++;
                                break;
                            case 1:
                                Buffer[index++] = receivebyte[i];
                                if (compCheckSum(Buffer, index-2, Buffer[index - 2], Buffer[index - 1]))
                                {
                                    if (parsData(Buffer, index - 2))
                                    {
                                        result = true;
                                    }
                                }
                                status = Status.Header;
                                index = 0;
                                index_chk = 0;
                                break;
		                    default:
                                index_chk = 0;
                                status = Status.Header;
                                index = 0;
                                break;
	                    }
                        break;
                    default:
                        status = Status.Header;
                        index = 0;
                        break;
                }
            }
            return result;
        }

        bool parsData(byte[] Data, UInt32 len)
        {
            bool result = false;
            int count = 0;
            int start = 0;
            int end = 0;

            for (int i = 0; i < len; i++)
            {
                if (count == 2 && Data[i] == 0x2c)
                {
                    var segmentHeader = new ArraySegment<byte>(Data, start, i - start);
                    string tmp = Encoding.UTF8.GetString(segmentHeader.ToArray());
                    pashr_data.Heading = Convert.ToDouble(tmp.Replace('.',','));
                    
                }
                if (count == 4 && Data[i] == 0x2c)
                {
                    var segmentHeader = new ArraySegment<byte>(Data, start, i - start);
                    string tmp = Encoding.UTF8.GetString(segmentHeader.ToArray());
                    pashr_data.Roll = Convert.ToDouble(tmp.Replace('.', ','));
                }
                if (count == 5 && Data[i] == 0x2c)
                {
                    var segmentHeader = new ArraySegment<byte>(Data, start, i - start);
                    string tmp = Encoding.UTF8.GetString(segmentHeader.ToArray());
                    pashr_data.Pitch = Convert.ToDouble(tmp.Replace('.', ','));
                    result = true;
                    break;
                }

                if (Data[i] == 0x2c)
                {
                    count++;
                    start = i+1;
                }
                
            }

            return result;
        }

        bool compCheckSum(byte[] receivebyte, UInt32 len, byte high, byte low)
        {
            byte Chk = 0, Read_Chk = 0;
            UInt32 i = 0;

            for (i = 0; i < len; i++)
            {
                Chk ^= receivebyte[i];
            }

            if ((high - '0') <= 9)
                Read_Chk = (byte)((high - '0') << 4);
            else
                Read_Chk = (byte)((high - 'A' + 10) << 4);

            if ((low - '0') <= 9)
            {
                Read_Chk |= (byte)(low - '0');
            }
            else
            {
                Read_Chk |= (byte)(low - 'A' + 10);
            }

            return (Read_Chk == Chk) ? true : false;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ReceiveBytesData
    {
        public Header headerPacket = new Header();
        public Packet_CA packet_CA = new Packet_CA();
        public Packet_AB packet_AB = new Packet_AB();
        public Int32 sizeHeader = Marshal.SizeOf(typeof(Header));
        public Int32 sizePacket_CA = Marshal.SizeOf(typeof(Packet_CA));
        public Int32 sizePacket_AB = Marshal.SizeOf(typeof(Packet_AB));

        public Int32 getLenToSendPacketCA() { return sizeHeader + sizePacket_CA; }

        public ReceiveBytesData() { }
        public ReceiveBytesData(byte[] receivebyte)
        {
            Int32 minSizeReceiveData = sizePacket_CA + sizeHeader;

            if (receivebyte.Length >= minSizeReceiveData)
            {
                var segmentHeader = new ArraySegment<byte>(receivebyte, 0, sizeHeader);

                switch (GetTypeData(segmentHeader))
                {
                    case 0xCA:
                        packet_CA = new Packet_CA();
                        var segmentCA = new ArraySegment<byte>(receivebyte, sizeHeader, sizePacket_CA);
                        GetPacket<Packet_CA>(segmentCA, ref packet_CA);
                        break;

                    case 0xAB:
                        packet_AB = new Packet_AB();
                        var segmentAB = new ArraySegment<byte>(receivebyte, sizeHeader, sizePacket_AB);
                        GetPacket<Packet_AB>(segmentAB, ref packet_AB);
                        break;

                    default:
                        break;
                }
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

        public byte[] getBytesToSendPacketCA()
        {
            byte[] ret = new byte[getLenToSendPacketCA()];
            byte[] byteCA = StructToByteArray(packet_CA);
            Header headerCA = new Header(0xCA, (byte)sizePacket_CA, this.calc_cs(byteCA, (byte)sizePacket_CA));
            byte[] byteHeader = StructToByteArray(headerCA);
            ret = byteHeader.Concat(byteCA).ToArray();

            return ret;
        }

        public byte[] getBytesToSendPacketCA(Packet_CA packet)
        {
            byte[] ret = new byte[getLenToSendPacketCA()];
            byte[] byteCA = StructToByteArray(packet);
            Header headerCA = new Header(0xCA, (byte)sizePacket_CA, this.calc_cs(byteCA, (byte)sizePacket_CA));
            byte[] byteHeader = StructToByteArray(headerCA);
            ret = byteHeader.Concat(byteCA).ToArray();

            return ret;
        }

    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Header
    {
        public UInt16 pacheader;	/* Title is a constant and equal 0xABCD */
        public byte type;			/* Type of message */
        public byte len;			/* size of buf; < MAX_BUF unsigned chars */
        public UInt16 checksum;	/* calculate by checksum function "calc_cs"; calculating only by body of packet */
        public Header(byte type, byte len, UInt16 checksum)
        {
            this.pacheader = 0xABCD;
            this.type = type;
            this.len = len;
            this.checksum = checksum; 
        }
        public Header() { }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Packet_CA
    {
        public byte axis_X = 0x7F;
        public byte axis_Y = 0x7F;
        public byte axis_Z = 0x7F;
        public byte button1;
        public byte button2;
        public byte button3;
        public byte button4;
        public byte button5;
        public byte enable_auto;
        public byte control_mode;

        public Packet_CA() { }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class Packet_AB
    {
        //packet_header       header;
		//att_meas_status		ams;
        public UInt32 millisecond_day;
        public byte day;
        public byte month;
        public UInt16 year;

        public UInt16 heading;
        public UInt16 mag_heading;
        public UInt16 track_angle;
        public UInt16 patch;
        public UInt16 roll;
        public UInt16 hor_flow_angle;
        public UInt16 ver_flow_angle;
        public float O1;
        public float O2;
        public float O3;
        public float N1;
        public float N2;
        public float N3;
        public UInt16 rms_heading;
        public UInt16 rms_mag_heading;
        public UInt16 rms_track_angle;
        public UInt16 rms_patch;
        public UInt16 rms_roll;
        public UInt16 rms_hor_flow_angle;
        public UInt16 rms_ver_flow_angle;
        public UInt16 rms_O1;
        public UInt16 rms_O2;
        public UInt16 rms_O3;
        public UInt16 rms_N1;
        public UInt16 rms_N2;
        public UInt16 rms_N3;

        public Packet_AB() { }
    }

}
