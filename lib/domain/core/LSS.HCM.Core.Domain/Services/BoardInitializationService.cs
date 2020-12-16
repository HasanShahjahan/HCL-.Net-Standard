using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Utiles;
using LSS.HCM.Core.DataObjects.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSS.HCM.Core.Domain.Services
{
    public sealed class BoardInitializationService
    {
        public static Dictionary<string, string> ExecuteCommand(string commandType, List<byte> bufferResponse)
        {
            byte length = bufferResponse.ElementAt(3);
            List<byte> Data = bufferResponse.GetRange(5, length - 1);

            var compiledData = new Dictionary<string, string>();
            switch (commandType)
            {
                case CommandType.DoorOpen:
                    DoorOpenCommand(Data, compiledData);
                    break;

                case CommandType.DoorStatus:
                    DoorStatusCommand(Data, compiledData);
                    break;

                case CommandType.ItemDetection:
                    ItemDetectionCommand(Data, compiledData);
                    break;

                default:
                    break;
            }
            return compiledData;
        }
        public static List<byte> GenerateCommand(string commandName, List<byte> commandData, AppSettings lockerConfiguration)
        {
            List<byte> commandHeader = Utiles.ToByteList(lockerConfiguration.Microcontroller.CommandHeader);
            var commandByte = new List<byte>();
            switch (commandName)
            {
                case CommandType.DoorOpen:
                    commandByte = Utiles.GetByteCommand(lockerConfiguration.Microcontroller.Commands.OpenDoor.Length, lockerConfiguration.Microcontroller.Commands.OpenDoor.Code, commandHeader, commandData);
                    break;

                case CommandType.DoorStatus:
                    commandByte = Utiles.GetByteCommand(lockerConfiguration.Microcontroller.Commands.DoorStatus.Length, lockerConfiguration.Microcontroller.Commands.DoorStatus.Code, commandHeader, commandData);
                    break;

                case CommandType.ItemDetection:
                    commandByte = Utiles.GetByteCommand(lockerConfiguration.Microcontroller.Commands.DetectItem.Length, lockerConfiguration.Microcontroller.Commands.DetectItem.Code, commandHeader, commandData);
                    break;
            }
            
            var commandChecksum = Utiles.Crc16CheckSum(commandByte);
            commandByte.AddRange(commandChecksum);
            
            return commandByte;
        }
        private static void DoorStatusCommand(List<byte> Data, Dictionary<string, string> compiledData)
        {
            char[] statusArray1_8 = Convert.ToString(Data.ElementAt(2), 2).PadLeft(8, '0').ToCharArray();
            char[] statusArray9_16 = Convert.ToString(Data.ElementAt(3), 2).PadLeft(8, '0').ToCharArray();
            char[] statusArray17_24 = Convert.ToString(Data.ElementAt(4), 2).PadLeft(8, '0').ToCharArray();
            
            Array.Reverse(statusArray1_8);
            Array.Reverse(statusArray9_16);
            Array.Reverse(statusArray17_24);

            string statusStr1_8 = new string(statusArray1_8);
            string statusStr9_16 = new string(statusArray9_16);
            string statusStr17_24 = new string(statusArray17_24);

            string statusAry = statusStr1_8 + statusStr9_16 + statusStr17_24;
            compiledData.Add("statusAry", statusAry);
        }
        private static void DoorOpenCommand(List<byte> Data, Dictionary<string, string> compiledData)
        {
            byte moduleNo = Data.ElementAt(0);
            byte doorNo = Data.ElementAt(1);

            string openingStatus;
            if (Data.ElementAt(2) == 0xFF) openingStatus = true.ToString();
            else openingStatus = false.ToString();
            
            compiledData.Add("moduleNo", moduleNo.ToString());
            compiledData.Add("doorNo", doorNo.ToString());
            compiledData.Add("DoorOpen", openingStatus);
        }
        private static void ItemDetectionCommand(List<byte> Data, Dictionary<string, string> compiledData)
        {
            string detectionArray1_8 = Convert.ToString(Data.ElementAt(3), 2).PadLeft(8, '0');
            string detectionArray9_16 = Convert.ToString(Data.ElementAt(4), 2).PadLeft(8, '0');
            string detectionArray17_24 = Convert.ToString(Data.ElementAt(5), 2).PadLeft(8, '0');
            string detectionArray25_32 = Convert.ToString(Data.ElementAt(6), 2).PadLeft(8, '0');

            string detectionAry = detectionArray1_8 + detectionArray9_16 + detectionArray17_24 + detectionArray25_32;
            compiledData.Add("detectionAry", detectionAry);
        }
    }
}
