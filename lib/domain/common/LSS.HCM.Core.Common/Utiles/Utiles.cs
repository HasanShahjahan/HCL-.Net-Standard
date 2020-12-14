using System;
using System.Collections.Generic;

namespace LSS.HCM.Core.Common.Utiles
{
    public class Utiles
    {
        public static List<byte> ToByteList(string strInput)
        {
            int chunkSize = 2;
            List<byte> hexByteArray = new List<byte>();
            int stringLength = strInput.Length;
            for (int i = 0; stringLength > 0; i += chunkSize)
            {
                string subStringInput = strInput.Substring(i, chunkSize);
                hexByteArray.Add((byte)Convert.ToInt16(subStringInput, 16));
                stringLength -= 2;
            }
            return hexByteArray;
        }

        // Caculate checksum CRC16 MODBUS
        public static List<byte> Crc16CheckSum(List<byte> byteCommand)
        {
            uint crc16 = 0xffff;
            uint temp;
            uint flag;

            for (int i = 0; i < byteCommand.Count; i++)
            {
                temp = byteCommand[i];
                temp &= 0x00ff; // MSB Marks
                crc16 ^= temp; //crc16 XOR with temp 
                for (uint c = 0; c < 8; c++)
                {
                    flag = crc16 & 0x01;
                    crc16 >>= 1;
                    if (flag != 0)
                        crc16 ^= 0x0a001;
                }
            }
            List<byte> crc16modbusArray = new List<byte>
            {
                ((byte)(crc16 >> 8)),
                (byte)(crc16 >> 0)
            };

            return crc16modbusArray;
        }

        public static string ToString(List<char> statusList)
        {
            return string.Join(" ", statusList);
        }

        public static List<byte> GetByteCommand(string length, string code, List<byte> header, List<byte> data)
        {
            List<byte> commandByte = new List<byte>();
            commandByte.AddRange(header);
            commandByte.Add(Convert.ToByte(length, 16));
            commandByte.Add(Convert.ToByte(code, 16));
            commandByte.AddRange(data);
            return commandByte;
        }

        public static Dictionary<string, byte> GetStatusList(string strInput)
        {
            int chunkSize = 1;
            var statusList = new Dictionary<string, byte>();
            int stringLength = strInput.Length;
            for (int i = 0; stringLength > 0; i += chunkSize)
            {
                string subStringInput = strInput.Substring(i, chunkSize);
                statusList[i.ToString("X2")] = (byte)Convert.ToInt16(subStringInput); // Convert int to hex string
                stringLength -= 2;
            }
            return statusList;
        }
    }
}
