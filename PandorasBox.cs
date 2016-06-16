/* Pandoras Box Automation - pbauto-csharp v0.3.12086 @2016-06-16 <support@coolux.de> */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;


namespace PandorasBox
{
    /// <summary>
    /// The main class used to communicate with Pandoras Box
    /// </summary>
    public class PBAuto
    {
        private Connector c;

        public PBAuto(Connector connector)
        {
            c = connector;
        }

        public static PBAuto ConnectTcp(string ip, int domain = 0)
        {
            return new PBAuto(new TCP(ip, domain));
        }

        public struct PBAutoResult {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
        }


        public PBAutoResult SetParamInt(int siteId, int deviceId, string parameterName, int parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.writeShort(1);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b.writeInt(parameterValue);
            b.writeBool(doSilent);
            b.writeBool(doDirect);
            b = c.Send(b, false);return new PBAutoResult(){ code = 1,error = 0 };
        }

        public PBAutoResult SetParamDouble(int siteId, int deviceId, string parameterName, double parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.writeShort(84);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b.writeDouble(parameterValue);
            b.writeBool(doSilent);
            b.writeBool(doDirect);
            b = c.Send(b, false);return new PBAutoResult(){ code = 84,error = 0 };
        }

        public PBAutoResult SetParamByteTuples(int siteId, int deviceId, string parameterName, int tupleDimension, byte[] tupleData, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.writeShort(115);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b.writeInt(tupleDimension);
            b.writeByteBuffer(tupleData);
            b.writeBool(doSilent);
            b.writeBool(doDirect);
            b = c.Send(b, false);return new PBAutoResult(){ code = 115,error = 0 };
        }

        public struct GetParamResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public double parameterValue;
        }
        public GetParamResult GetParam(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(79);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, true);
            var r = new GetParamResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 79)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.parameterValue = b.readDouble();
            }
            return r;
        }

        public struct GetParamByteTuplesResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int tupleDimension;
            public byte[] tupleData;
        }
        public GetParamByteTuplesResult GetParamByteTuples(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(132);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringWide(parameterName);
            b = c.Send(b, true);
            var r = new GetParamByteTuplesResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 132)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.tupleDimension = b.readInt();
                r.tupleData = b.readByteBuffer();
            }
            return r;
        }

        public PBAutoResult SetParamOfKind(int siteId, int deviceId, int parameterKindId, int parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.writeShort(39);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(parameterKindId);
            b.writeInt(parameterValue);
            b.writeBool(doSilent);
            b.writeBool(doDirect);
            b = c.Send(b, false);return new PBAutoResult(){ code = 39,error = 0 };
        }

        public PBAutoResult SetParamOfKindDouble(int siteId, int deviceId, int parameterKindId, double parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.writeShort(85);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(parameterKindId);
            b.writeDouble(parameterValue);
            b.writeBool(doSilent);
            b.writeBool(doDirect);
            b = c.Send(b, false);return new PBAutoResult(){ code = 85,error = 0 };
        }

        public struct GetParamOfKindResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public double parameterValue;
        }
        public GetParamOfKindResult GetParamOfKind(int siteId, int deviceId, int parameterKindId)
        {
            var b = new ByteUtil();
            b.writeShort(80);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(parameterKindId);
            b = c.Send(b, true);
            var r = new GetParamOfKindResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 80)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.parameterValue = b.readDouble();
            }
            return r;
        }

        public PBAutoResult SetParamInSelection(string parameterName, int parameterValue)
        {
            var b = new ByteUtil();
            b.writeShort(58);
            b.writeStringNarrow(parameterName);
            b.writeInt(parameterValue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 58,error = 0 };
        }

        public PBAutoResult SetParamInSelectionDouble(string parameterName, double parameterValue)
        {
            var b = new ByteUtil();
            b.writeShort(99);
            b.writeStringNarrow(parameterName);
            b.writeDouble(parameterValue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 99,error = 0 };
        }

        public PBAutoResult SetParamOfKindInSelection(int parameterKindId, int parameterValue)
        {
            var b = new ByteUtil();
            b.writeShort(59);
            b.writeInt(parameterKindId);
            b.writeInt(parameterValue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 59,error = 0 };
        }

        public PBAutoResult SetParamOfKindInSelectionDouble(int parameterKindId, double parameterValue)
        {
            var b = new ByteUtil();
            b.writeShort(100);
            b.writeInt(parameterKindId);
            b.writeDouble(parameterValue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 100,error = 0 };
        }

        public PBAutoResult SetParamLerpTime(int siteId, int deviceId, string parameterName, int smoothingTime)
        {
            var b = new ByteUtil();
            b.writeShort(232);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b.writeInt(smoothingTime);
            b = c.Send(b, false);return new PBAutoResult(){ code = 232,error = 0 };
        }

        public struct GetIsDeviceSelectedResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public byte isSelected;
        }
        public GetIsDeviceSelectedResult GetIsDeviceSelected(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(74);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, true);
            var r = new GetIsDeviceSelectedResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 74)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isSelected = b.readByte();
            }
            return r;
        }

        public struct GetSelectedDeviceCountResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int selectedDevicesCount;
        }
        public GetSelectedDeviceCountResult GetSelectedDeviceCount()
        {
            var b = new ByteUtil();
            b.writeShort(81);
            b = c.Send(b, true);
            var r = new GetSelectedDeviceCountResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 81)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.selectedDevicesCount = b.readInt();
            }
            return r;
        }

        public struct GetSelectedDeviceResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int siteId;
            public int deviceId;
        }
        public GetSelectedDeviceResult GetSelectedDevice(int selectionIndex)
        {
            var b = new ByteUtil();
            b.writeShort(75);
            b.writeInt(selectionIndex);
            b = c.Send(b, true);
            var r = new GetSelectedDeviceResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 75)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.siteId = b.readInt();
                r.deviceId = b.readInt();
            }
            return r;
        }

        public PBAutoResult SetSequenceMediaAtTime(int siteId, int deviceId, int sequenceId, int hours, int minutes, int seconds, int frames, int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(56);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(sequenceId);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 56,error = 0 };
        }

        public PBAutoResult AssignResource(int siteId, int deviceId, int dmxFolderId, int dmxFileId, bool forMesh)
        {
            var b = new ByteUtil();
            b.writeShort(2);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(forMesh);
            b = c.Send(b, false);return new PBAutoResult(){ code = 2,error = 0 };
        }

        public PBAutoResult AssignResourceByName(int siteId, int deviceId, string resourcePath, string parameterName, bool forMesh)
        {
            var b = new ByteUtil();
            b.writeShort(129);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringWide(resourcePath);
            b.writeStringWide(parameterName);
            b.writeBool(forMesh);
            b = c.Send(b, false);return new PBAutoResult(){ code = 129,error = 0 };
        }

        public PBAutoResult AssignResourceToSelection(int dmxFolderId, int dmxFileId, bool forMesh)
        {
            var b = new ByteUtil();
            b.writeShort(61);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(forMesh);
            b = c.Send(b, false);return new PBAutoResult(){ code = 61,error = 0 };
        }

        public PBAutoResult MoveResourceToPath(string resourcePath, string projectPath)
        {
            var b = new ByteUtil();
            b.writeShort(144);
            b.writeStringWide(resourcePath);
            b.writeStringWide(projectPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 144,error = 0 };
        }

        public PBAutoResult MoveTreeItem(int itemIdFrom, int itemIdTo)
        {
            var b = new ByteUtil();
            b.writeShort(158);
            b.writeInt(itemIdFrom);
            b.writeInt(itemIdTo);
            b = c.Send(b, false);return new PBAutoResult(){ code = 158,error = 0 };
        }

        public PBAutoResult SetSequenceTransportMode(int sequenceId, int transportMode)
        {
            var b = new ByteUtil();
            b.writeShort(3);
            b.writeInt(sequenceId);
            b.writeInt(transportMode);
            b = c.Send(b, false);return new PBAutoResult(){ code = 3,error = 0 };
        }

        public struct GetSequenceTransportModeResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int transportMode;
        }
        public GetSequenceTransportModeResult GetSequenceTransportMode(int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(72);
            b.writeInt(sequenceId);
            b = c.Send(b, true);
            var r = new GetSequenceTransportModeResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 72)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.transportMode = b.readInt();
            }
            return r;
        }

        public PBAutoResult MoveSequenceToTime(int sequenceId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(5);
            b.writeInt(sequenceId);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 5,error = 0 };
        }

        public struct GetSequenceTimeResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int hours;
            public int minutes;
            public int seconds;
            public int frames;
        }
        public GetSequenceTimeResult GetSequenceTime(int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(73);
            b.writeInt(sequenceId);
            b = c.Send(b, true);
            var r = new GetSequenceTimeResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 73)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.hours = b.readInt();
                r.minutes = b.readInt();
                r.seconds = b.readInt();
                r.frames = b.readInt();
            }
            return r;
        }

        public PBAutoResult MoveSequenceToNextFrame(int sequenceId, byte isNext)
        {
            var b = new ByteUtil();
            b.writeShort(6);
            b.writeInt(sequenceId);
            b.writeByte(isNext);
            b = c.Send(b, false);return new PBAutoResult(){ code = 6,error = 0 };
        }

        public PBAutoResult MoveSequenceToCue(int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.writeShort(4);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 4,error = 0 };
        }

        public PBAutoResult MoveSequenceToNextCue(int sequenceId, byte isNext)
        {
            var b = new ByteUtil();
            b.writeShort(7);
            b.writeInt(sequenceId);
            b.writeByte(isNext);
            b = c.Send(b, false);return new PBAutoResult(){ code = 7,error = 0 };
        }

        public PBAutoResult SetSequenceTransparency(int sequenceId, int transparency)
        {
            var b = new ByteUtil();
            b.writeShort(8);
            b.writeInt(sequenceId);
            b.writeInt(transparency);
            b = c.Send(b, false);return new PBAutoResult(){ code = 8,error = 0 };
        }

        public struct GetSequenceTransparencyResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int transparency;
        }
        public GetSequenceTransparencyResult GetSequenceTransparency(int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(91);
            b.writeInt(sequenceId);
            b = c.Send(b, true);
            var r = new GetSequenceTransparencyResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 91)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.transparency = b.readInt();
            }
            return r;
        }

        public PBAutoResult SetSequenceSMTPETimeCodeMode(int sequenceId, int timeCodeMode)
        {
            var b = new ByteUtil();
            b.writeShort(41);
            b.writeInt(sequenceId);
            b.writeInt(timeCodeMode);
            b = c.Send(b, false);return new PBAutoResult(){ code = 41,error = 0 };
        }

        public PBAutoResult SetSequenceSMTPETimeCodeOffset(int sequenceId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(42);
            b.writeInt(sequenceId);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 42,error = 0 };
        }

        public PBAutoResult SetSequenceSMTPETimeCodeStopAction(int sequenceId, int stopAction)
        {
            var b = new ByteUtil();
            b.writeShort(43);
            b.writeInt(sequenceId);
            b.writeInt(stopAction);
            b = c.Send(b, false);return new PBAutoResult(){ code = 43,error = 0 };
        }

        public PBAutoResult ResetAll()
        {
            var b = new ByteUtil();
            b.writeShort(9);
            b = c.Send(b, false);return new PBAutoResult(){ code = 9,error = 0 };
        }

        public PBAutoResult ResetSite(int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(10);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 10,error = 0 };
        }

        public PBAutoResult ResetDevice(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(11);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 11,error = 0 };
        }

        public PBAutoResult ResetParam(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(12);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 12,error = 0 };
        }

        public PBAutoResult SetAllActive()
        {
            var b = new ByteUtil();
            b.writeShort(35);
            b = c.Send(b, false);return new PBAutoResult(){ code = 35,error = 0 };
        }

        public PBAutoResult SetSiteActive(int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(36);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 36,error = 0 };
        }

        public PBAutoResult SetDeviceActive(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(37);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 37,error = 0 };
        }

        public PBAutoResult SetParamActive(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(38);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 38,error = 0 };
        }

        public PBAutoResult ClearAllActive()
        {
            var b = new ByteUtil();
            b.writeShort(13);
            b = c.Send(b, false);return new PBAutoResult(){ code = 13,error = 0 };
        }

        public PBAutoResult ClearActiveSite(int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(14);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 14,error = 0 };
        }

        public PBAutoResult ClearActiveDevice(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(15);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 15,error = 0 };
        }

        public PBAutoResult ClearActiveParam(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(16);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 16,error = 0 };
        }

        public PBAutoResult ToggleFullscreen(int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(17);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 17,error = 0 };
        }

        public PBAutoResult SetParamRelativeDouble(int siteId, int deviceId, string parameterName, double parameterValue)
        {
            var b = new ByteUtil();
            b.writeShort(98);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b.writeDouble(parameterValue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 98,error = 0 };
        }

        public PBAutoResult SetParamRelativeExtended(int siteId, int deviceId, string parameterName, double parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.writeShort(149);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b.writeDouble(parameterValue);
            b.writeBool(doSilent);
            b.writeBool(doDirect);
            b = c.Send(b, false);return new PBAutoResult(){ code = 149,error = 0 };
        }

        public PBAutoResult SetParamRelativeInSelection(string parameterName, int parameterValue)
        {
            var b = new ByteUtil();
            b.writeShort(60);
            b.writeStringNarrow(parameterName);
            b.writeInt(parameterValue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 60,error = 0 };
        }

        public PBAutoResult SetParamRelativeInSelectionDouble(string parameterName, double parameterValue)
        {
            var b = new ByteUtil();
            b.writeShort(101);
            b.writeStringNarrow(parameterName);
            b.writeDouble(parameterValue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 101,error = 0 };
        }

        public PBAutoResult AddContentToPath(string filePath, int siteId, int dmxFolderId, int dmxFileId, string projectPath)
        {
            var b = new ByteUtil();
            b.writeShort(87);
            b.writeStringNarrow(filePath);
            b.writeInt(siteId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringNarrow(projectPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 87,error = 0 };
        }

        public PBAutoResult AddContentToTreeItem(string filePath, int siteId, int dmxFolderId, int dmxFileId, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(153);
            b.writeStringNarrow(filePath);
            b.writeInt(siteId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeInt(treeItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 153,error = 0 };
        }

        public PBAutoResult AddContentFromLocalNode(string filePath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(63);
            b.writeStringNarrow(filePath);
            b.writeShort(dmxFolderId);
            b.writeShort(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 63,error = 0 };
        }

        public PBAutoResult AddContentFromLocalNodeToPath(string filePath, string projectPath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(135);
            b.writeStringNarrow(filePath);
            b.writeStringNarrow(projectPath);
            b.writeShort(dmxFolderId);
            b.writeShort(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 135,error = 0 };
        }

        public PBAutoResult AddContentFromLocalNodeToTreeItem(string filePath, int treeItemIndex, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(154);
            b.writeStringNarrow(filePath);
            b.writeInt(treeItemIndex);
            b.writeShort(dmxFolderId);
            b.writeShort(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 154,error = 0 };
        }

        public PBAutoResult AddContentFromFolder(string folderPath, int siteId, int dmxFolderId, int dmxFileId, string projectPath)
        {
            var b = new ByteUtil();
            b.writeShort(124);
            b.writeStringWide(folderPath);
            b.writeInt(siteId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringWide(projectPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 124,error = 0 };
        }

        public PBAutoResult AddContentFromLocalNodeFolder(string folderPath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(133);
            b.writeStringWide(folderPath);
            b.writeShort(dmxFolderId);
            b.writeShort(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 133,error = 0 };
        }

        public PBAutoResult AddContentFromLocalNodeFolderToPath(string folderPath, string projectPath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(134);
            b.writeStringNarrow(folderPath);
            b.writeStringNarrow(projectPath);
            b.writeShort(dmxFolderId);
            b.writeShort(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 134,error = 0 };
        }

        public PBAutoResult AddContentFolderFromLocalNodeToTreeItem(string folderPath, int treeItemIndex, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(155);
            b.writeStringNarrow(folderPath);
            b.writeInt(treeItemIndex);
            b.writeShort(dmxFolderId);
            b.writeShort(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 155,error = 0 };
        }

        public PBAutoResult RemoveMediaById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(20);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 20,error = 0 };
        }

        public PBAutoResult RemoveMeshById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(21);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 21,error = 0 };
        }

        public PBAutoResult RemoveContentByName(string resourcePath, bool allEquallyNamed)
        {
            var b = new ByteUtil();
            b.writeShort(125);
            b.writeStringWide(resourcePath);
            b.writeBool(allEquallyNamed);
            b = c.Send(b, false);return new PBAutoResult(){ code = 125,error = 0 };
        }

        public PBAutoResult RemoveTreeItem(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(156);
            b.writeInt(treeItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 156,error = 0 };
        }

        public PBAutoResult RemoveAllResources(bool removeFolder)
        {
            var b = new ByteUtil();
            b.writeShort(126);
            b.writeBool(removeFolder);
            b = c.Send(b, false);return new PBAutoResult(){ code = 126,error = 0 };
        }

        public PBAutoResult SetContentId(string resourcePath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(234);
            b.writeStringWide(resourcePath);
            b.writeShort(dmxFolderId);
            b.writeShort(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 234,error = 0 };
        }

        public PBAutoResult SpreadAll()
        {
            var b = new ByteUtil();
            b.writeShort(22);
            b = c.Send(b, false);return new PBAutoResult(){ code = 22,error = 0 };
        }

        public PBAutoResult SpreadMediaById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(23);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 23,error = 0 };
        }

        public PBAutoResult SpreadMeshById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(24);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 24,error = 0 };
        }

        public PBAutoResult ReloadMediaById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(44);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 44,error = 0 };
        }

        public PBAutoResult ReloadMeshById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(45);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 45,error = 0 };
        }

        public PBAutoResult ReloadResource(string resourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(147);
            b.writeStringWide(resourcePath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 147,error = 0 };
        }

        public PBAutoResult SpreadResource(string resourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(148);
            b.writeStringWide(resourcePath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 148,error = 0 };
        }

        public PBAutoResult ReloadAndSpreadResourceByPath(string resourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(159);
            b.writeStringWide(resourcePath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 159,error = 0 };
        }

        public PBAutoResult ReloadAndSpreadResourceByTreeItem(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(160);
            b.writeInt(treeItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 160,error = 0 };
        }

        public PBAutoResult ReloadAndSpreadResourceByDmxId(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(161);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 161,error = 0 };
        }

        public PBAutoResult RemoveInconsistent()
        {
            var b = new ByteUtil();
            b.writeShort(34);
            b = c.Send(b, false);return new PBAutoResult(){ code = 34,error = 0 };
        }

        public PBAutoResult RemoveAssetOnSite(string resourcePath, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(170);
            b.writeStringWide(resourcePath);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 170,error = 0 };
        }

        public PBAutoResult RemoveAssetOnSiteById(int dmxFolderId, int dmxFileId, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(171);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 171,error = 0 };
        }

        public PBAutoResult RemoveAssetOnSiteByTreeItem(int treeItemIndex, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(172);
            b.writeInt(treeItemIndex);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 172,error = 0 };
        }

        public PBAutoResult AttachAssetOnSite(string filePath, string resourcePath, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(173);
            b.writeStringWide(filePath);
            b.writeStringWide(resourcePath);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 173,error = 0 };
        }

        public PBAutoResult AttachAssetOnSiteByDmxId(string filePath, int dmxFolderId, int dmxFileId, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(174);
            b.writeStringWide(filePath);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 174,error = 0 };
        }

        public PBAutoResult AttachAssetOnSiteByTreeItem(string filePath, int treeItemIndex, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(175);
            b.writeStringWide(filePath);
            b.writeInt(treeItemIndex);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 175,error = 0 };
        }

        public PBAutoResult StoreActive(int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(25);
            b.writeInt(sequenceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 25,error = 0 };
        }

        public PBAutoResult StoreActiveToTime(int sequenceId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(26);
            b.writeInt(sequenceId);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 26,error = 0 };
        }

        public PBAutoResult SetMediaFrameBlendingById(int dmxFolderId, int dmxFileId, bool frameBlended)
        {
            var b = new ByteUtil();
            b.writeShort(27);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(frameBlended);
            b = c.Send(b, false);return new PBAutoResult(){ code = 27,error = 0 };
        }

        public PBAutoResult SetMediaDeinterlacingById(int dmxFolderId, int dmxFileId, int deinterlacer)
        {
            var b = new ByteUtil();
            b.writeShort(28);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeInt(deinterlacer);
            b = c.Send(b, false);return new PBAutoResult(){ code = 28,error = 0 };
        }

        public PBAutoResult SetMediaAnisotropicFilteringById(int dmxFolderId, int dmxFileId, bool useFiltering)
        {
            var b = new ByteUtil();
            b.writeShort(29);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(useFiltering);
            b = c.Send(b, false);return new PBAutoResult(){ code = 29,error = 0 };
        }

        public PBAutoResult SetMediaUnderscanById(int dmxFolderId, int dmxFileId, bool useUnderscan)
        {
            var b = new ByteUtil();
            b.writeShort(30);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(useUnderscan);
            b = c.Send(b, false);return new PBAutoResult(){ code = 30,error = 0 };
        }

        public PBAutoResult SetMediaMpegColourSpaceById(int dmxFolderId, int dmxFileId, bool useMpegColorSpace)
        {
            var b = new ByteUtil();
            b.writeShort(31);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(useMpegColorSpace);
            b = c.Send(b, false);return new PBAutoResult(){ code = 31,error = 0 };
        }

        public PBAutoResult SetMediaAlphaChannelById(int dmxFolderId, int dmxFileId, bool useAlphaChannel)
        {
            var b = new ByteUtil();
            b.writeShort(32);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(useAlphaChannel);
            b = c.Send(b, false);return new PBAutoResult(){ code = 32,error = 0 };
        }

        public PBAutoResult CreateTextInput(int dmxFolderId, int dmxFileId, string text)
        {
            var b = new ByteUtil();
            b.writeShort(52);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringNarrow(text);
            b = c.Send(b, false);return new PBAutoResult(){ code = 52,error = 0 };
        }

        public PBAutoResult SetText(int dmxFolderId, int dmxFileId, string text)
        {
            var b = new ByteUtil();
            b.writeShort(33);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringNarrow(text);
            b = c.Send(b, false);return new PBAutoResult(){ code = 33,error = 0 };
        }

        public PBAutoResult LoadProject(string folderPathToProject, string projectXmlFileName, byte saveExisting)
        {
            var b = new ByteUtil();
            b.writeShort(46);
            b.writeStringNarrow(folderPathToProject);
            b.writeStringNarrow(projectXmlFileName);
            b.writeByte(saveExisting);
            b = c.Send(b, false);return new PBAutoResult(){ code = 46,error = 0 };
        }

        public PBAutoResult CloseProject(byte save)
        {
            var b = new ByteUtil();
            b.writeShort(47);
            b.writeByte(save);
            b = c.Send(b, false);return new PBAutoResult(){ code = 47,error = 0 };
        }

        public PBAutoResult ClearSelection()
        {
            var b = new ByteUtil();
            b.writeShort(48);
            b = c.Send(b, false);return new PBAutoResult(){ code = 48,error = 0 };
        }

        public PBAutoResult SetDeviceAcceptDmxById(int siteId, int deviceId, byte acceptDmx)
        {
            var b = new ByteUtil();
            b.writeShort(49);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeByte(acceptDmx);
            b = c.Send(b, false);return new PBAutoResult(){ code = 49,error = 0 };
        }

        public PBAutoResult SetSiteAcceptDmxById(int siteId, byte acceptDmx)
        {
            var b = new ByteUtil();
            b.writeShort(50);
            b.writeInt(siteId);
            b.writeByte(acceptDmx);
            b = c.Send(b, false);return new PBAutoResult(){ code = 50,error = 0 };
        }

        public PBAutoResult SetDeviceDmxAddressById(int siteId, int deviceId, int index, int id1, int id2)
        {
            var b = new ByteUtil();
            b.writeShort(51);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(index);
            b.writeInt(id1);
            b.writeInt(id2);
            b = c.Send(b, false);return new PBAutoResult(){ code = 51,error = 0 };
        }

        public PBAutoResult SetSiteDmxAddressById(int siteId, int index, int id1, int id2)
        {
            var b = new ByteUtil();
            b.writeShort(235);
            b.writeInt(siteId);
            b.writeInt(index);
            b.writeInt(id1);
            b.writeInt(id2);
            b = c.Send(b, false);return new PBAutoResult(){ code = 235,error = 0 };
        }

        public PBAutoResult SetCuePlayMode(int sequenceId, int cueId, int playMode)
        {
            var b = new ByteUtil();
            b.writeShort(53);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b.writeInt(playMode);
            b = c.Send(b, false);return new PBAutoResult(){ code = 53,error = 0 };
        }

        public PBAutoResult SetNextCuePlayMode(int sequenceId, int playMode)
        {
            var b = new ByteUtil();
            b.writeShort(54);
            b.writeInt(sequenceId);
            b.writeInt(playMode);
            b = c.Send(b, false);return new PBAutoResult(){ code = 54,error = 0 };
        }

        public PBAutoResult SetIgnoreNextCue(int sequenceId, byte doIgnore)
        {
            var b = new ByteUtil();
            b.writeShort(55);
            b.writeInt(sequenceId);
            b.writeByte(doIgnore);
            b = c.Send(b, false);return new PBAutoResult(){ code = 55,error = 0 };
        }

        public PBAutoResult SaveProject()
        {
            var b = new ByteUtil();
            b.writeShort(62);
            b = c.Send(b, false);return new PBAutoResult(){ code = 62,error = 0 };
        }

        public PBAutoResult SetIsSiteFullscreen(int siteId, byte isFullscreen)
        {
            var b = new ByteUtil();
            b.writeShort(64);
            b.writeInt(siteId);
            b.writeByte(isFullscreen);
            b = c.Send(b, false);return new PBAutoResult(){ code = 64,error = 0 };
        }

        public PBAutoResult SetIsSiteFullscreenByIp(string ipAddress, byte isFullscreen)
        {
            var b = new ByteUtil();
            b.writeShort(65);
            b.writeStringNarrow(ipAddress);
            b.writeByte(isFullscreen);
            b = c.Send(b, false);return new PBAutoResult(){ code = 65,error = 0 };
        }

        public PBAutoResult SetTextTextureSize(int dmxFolderId, int dmxFileId, int width, int height)
        {
            var b = new ByteUtil();
            b.writeShort(66);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeInt(width);
            b.writeInt(height);
            b = c.Send(b, false);return new PBAutoResult(){ code = 66,error = 0 };
        }

        public PBAutoResult SetTextProperties(int dmxFolderId, int dmxFileId, string fontFamily, int size, byte style, byte alignment, byte colorRed, byte colorGreen, byte colorBlue)
        {
            var b = new ByteUtil();
            b.writeShort(67);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringNarrow(fontFamily);
            b.writeInt(size);
            b.writeByte(style);
            b.writeByte(alignment);
            b.writeByte(colorRed);
            b.writeByte(colorGreen);
            b.writeByte(colorBlue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 67,error = 0 };
        }

        public PBAutoResult SetTextCenterOnTexture(int dmxFolderId, int dmxFileId, byte centerOnTexture)
        {
            var b = new ByteUtil();
            b.writeShort(68);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeByte(centerOnTexture);
            b = c.Send(b, false);return new PBAutoResult(){ code = 68,error = 0 };
        }

        public PBAutoResult CreateTextInputWide(int dmxFolderId, int dmxFileId, string text)
        {
            var b = new ByteUtil();
            b.writeShort(69);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringWide(text);
            b = c.Send(b, false);return new PBAutoResult(){ code = 69,error = 0 };
        }

        public PBAutoResult SetTextWide(int dmxFolderId, int dmxFileId, string text)
        {
            var b = new ByteUtil();
            b.writeShort(70);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringWide(text);
            b = c.Send(b, false);return new PBAutoResult(){ code = 70,error = 0 };
        }

        public PBAutoResult SetSiteIpById(int siteId, string ip)
        {
            var b = new ByteUtil();
            b.writeShort(71);
            b.writeInt(siteId);
            b.writeStringNarrow(ip);
            b = c.Send(b, false);return new PBAutoResult(){ code = 71,error = 0 };
        }

        public struct GetClipRemainingTimeResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int hours;
            public int minutes;
            public int seconds;
            public int frames;
        }
        public GetClipRemainingTimeResult GetClipRemainingTime(int siteId, int deviceId, int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(77);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(sequenceId);
            b = c.Send(b, true);
            var r = new GetClipRemainingTimeResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 77)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.hours = b.readInt();
                r.minutes = b.readInt();
                r.seconds = b.readInt();
                r.frames = b.readInt();
            }
            return r;
        }

        public struct GetRemainingTimeUntilNextCueResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int hours;
            public int minutes;
            public int seconds;
            public int frames;
        }
        public GetRemainingTimeUntilNextCueResult GetRemainingTimeUntilNextCue(int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(78);
            b.writeInt(sequenceId);
            b = c.Send(b, true);
            var r = new GetRemainingTimeUntilNextCueResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 78)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.hours = b.readInt();
                r.minutes = b.readInt();
                r.seconds = b.readInt();
                r.frames = b.readInt();
            }
            return r;
        }

        public struct GetResourceCountResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int mediaCount;
        }
        public GetResourceCountResult GetResourceCount()
        {
            var b = new ByteUtil();
            b.writeShort(82);
            b = c.Send(b, true);
            var r = new GetResourceCountResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 82)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.mediaCount = b.readInt();
            }
            return r;
        }

        public struct GetTreeItemCountResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int treeItemCount;
        }
        public GetTreeItemCountResult GetTreeItemCount()
        {
            var b = new ByteUtil();
            b.writeShort(150);
            b = c.Send(b, true);
            var r = new GetTreeItemCountResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 150)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.treeItemCount = b.readInt();
            }
            return r;
        }

        public PBAutoResult CreateProjectFolder(string folderName)
        {
            var b = new ByteUtil();
            b.writeShort(83);
            b.writeStringWide(folderName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 83,error = 0 };
        }

        public PBAutoResult CreateProjectFolderInPath(string folderName, string projectPath)
        {
            var b = new ByteUtil();
            b.writeShort(122);
            b.writeStringWide(folderName);
            b.writeStringWide(projectPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 122,error = 0 };
        }

        public PBAutoResult CreateProjectFolderInTreeItem(string folderName, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(157);
            b.writeStringWide(folderName);
            b.writeInt(treeItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 157,error = 0 };
        }

        public PBAutoResult RemoveFolderFromProject(string projectPath)
        {
            var b = new ByteUtil();
            b.writeShort(123);
            b.writeStringWide(projectPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 123,error = 0 };
        }

        public PBAutoResult SetDeviceSelection(int siteId, int deviceId, int selectionMode)
        {
            var b = new ByteUtil();
            b.writeShort(86);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(selectionMode);
            b = c.Send(b, false);return new PBAutoResult(){ code = 86,error = 0 };
        }

        public PBAutoResult SetClxControllerFaderMapping(int faderId, int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(90);
            b.writeInt(faderId);
            b.writeInt(sequenceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 90,error = 0 };
        }

        public PBAutoResult SetClxControllerCueMapping(int cueBtnId, int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.writeShort(92);
            b.writeInt(cueBtnId);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 92,error = 0 };
        }

        public PBAutoResult CreateCue(int sequenceId, int cueId, int hours, int minutes, int seconds, int frames, string cueName, int cueKindId)
        {
            var b = new ByteUtil();
            b.writeShort(93);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b.writeStringWide(cueName);
            b.writeInt(cueKindId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 93,error = 0 };
        }

        public PBAutoResult RemoveCueById(int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.writeShort(94);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 94,error = 0 };
        }

        public PBAutoResult RemoveAllCues(int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(95);
            b.writeInt(sequenceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 95,error = 0 };
        }

        public struct CreateVideoLayerGetIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int layerId;
        }
        public CreateVideoLayerGetIdResult CreateVideoLayerGetId(int siteId, bool isGraphicLayer)
        {
            var b = new ByteUtil();
            b.writeShort(110);
            b.writeInt(siteId);
            b.writeBool(isGraphicLayer);
            b = c.Send(b, true);
            var r = new CreateVideoLayerGetIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 110)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.layerId = b.readInt();
            }
            return r;
        }

        public PBAutoResult RemoveLayer(int siteId, int layerId, bool isGraphicLayer)
        {
            var b = new ByteUtil();
            b.writeShort(97);
            b.writeInt(siteId);
            b.writeInt(layerId);
            b.writeBool(isGraphicLayer);
            b = c.Send(b, false);return new PBAutoResult(){ code = 97,error = 0 };
        }

        public PBAutoResult SetIsBackup(bool enable)
        {
            var b = new ByteUtil();
            b.writeShort(102);
            b.writeBool(enable);
            b = c.Send(b, false);return new PBAutoResult(){ code = 102,error = 0 };
        }

        public PBAutoResult ApplyView(int viewId)
        {
            var b = new ByteUtil();
            b.writeShort(103);
            b.writeInt(viewId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 103,error = 0 };
        }

        public PBAutoResult SetSpareFromSpread(int siteId, bool spareFromSpread)
        {
            var b = new ByteUtil();
            b.writeShort(104);
            b.writeInt(siteId);
            b.writeBool(spareFromSpread);
            b = c.Send(b, false);return new PBAutoResult(){ code = 104,error = 0 };
        }

        public struct GetParamResourceResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int dmxFolderId;
            public int dmxFileId;
            public string filePath;
            public string resourcePath;
        }
        public GetParamResourceResult GetParamResource(int siteId, int deviceId, bool isMedia, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(105);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeBool(isMedia);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, true);
            var r = new GetParamResourceResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 105)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.dmxFolderId = b.readInt();
                r.dmxFileId = b.readInt();
                r.filePath = b.readStringNarrow();
                r.resourcePath = b.readStringNarrow();
            }
            return r;
        }

        public struct GetMediaTransportModeResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int transportMode;
        }
        public GetMediaTransportModeResult GetMediaTransportMode(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(108);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, true);
            var r = new GetMediaTransportModeResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 108)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.transportMode = b.readInt();
            }
            return r;
        }

        public struct GetIsSiteConnectedResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public bool isConnected;
        }
        public GetIsSiteConnectedResult GetIsSiteConnected(int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(109);
            b.writeInt(siteId);
            b = c.Send(b, true);
            var r = new GetIsSiteConnectedResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 109)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isConnected = b.readBool();
            }
            return r;
        }

        public PBAutoResult MoveLayerUp(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(111);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 111,error = 0 };
        }

        public PBAutoResult MoveLayerDown(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(112);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 112,error = 0 };
        }

        public PBAutoResult MoveLayerToFirstPosition(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(113);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 113,error = 0 };
        }

        public PBAutoResult MoveLayerToLastPosition(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(114);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 114,error = 0 };
        }

        public PBAutoResult SetEnableClxController(byte forJogShuttle, bool enable)
        {
            var b = new ByteUtil();
            b.writeShort(117);
            b.writeByte(forJogShuttle);
            b.writeBool(enable);
            b = c.Send(b, false);return new PBAutoResult(){ code = 117,error = 0 };
        }

        public struct GetEnableClxControllerResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public bool isEnabled;
        }
        public GetEnableClxControllerResult GetEnableClxController(byte forJogShuttle)
        {
            var b = new ByteUtil();
            b.writeShort(116);
            b.writeByte(forJogShuttle);
            b = c.Send(b, true);
            var r = new GetEnableClxControllerResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 116)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isEnabled = b.readBool();
            }
            return r;
        }

        public PBAutoResult SetSequenceCueWaitTime(int sequenceId, int cueId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(118);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 118,error = 0 };
        }

        public PBAutoResult SetSequenceCueJumpTargetTime(int sequenceId, int cueId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(119);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 119,error = 0 };
        }

        public PBAutoResult SetCueJumpCount(int sequenceId, int cueId, int jumpCount)
        {
            var b = new ByteUtil();
            b.writeShort(120);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b.writeInt(jumpCount);
            b = c.Send(b, false);return new PBAutoResult(){ code = 120,error = 0 };
        }

        public PBAutoResult ResetCueTriggerCount(int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.writeShort(121);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 121,error = 0 };
        }

        public struct GetIsContentConsistentResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int isContentInconsistent;
        }
        public GetIsContentConsistentResult GetIsContentConsistent(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(127);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, true);
            var r = new GetIsContentConsistentResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 127)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isContentInconsistent = b.readInt();
            }
            return r;
        }

        public struct GetIsContentConsistentByNameResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int isContentInconsistent;
        }
        public GetIsContentConsistentByNameResult GetIsContentConsistentByName(string resourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(128);
            b.writeStringWide(resourcePath);
            b = c.Send(b, true);
            var r = new GetIsContentConsistentByNameResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 128)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isContentInconsistent = b.readInt();
            }
            return r;
        }

        public struct CreateSequenceGetIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int sequenceId;
        }
        public CreateSequenceGetIdResult CreateSequenceGetId()
        {
            var b = new ByteUtil();
            b.writeShort(130);
            b = c.Send(b, true);
            var r = new CreateSequenceGetIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 130)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.sequenceId = b.readInt();
            }
            return r;
        }

        public PBAutoResult RemoveSequence(int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(131);
            b.writeInt(sequenceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 131,error = 0 };
        }

        public PBAutoResult SendMouseInput(int siteId, int deviceId, int mouseEventType, int screenPosX, int screenPosY, bool firstPass)
        {
            var b = new ByteUtil();
            b.writeShort(136);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(mouseEventType);
            b.writeInt(screenPosX);
            b.writeInt(screenPosY);
            b.writeBool(firstPass);
            b = c.Send(b, false);return new PBAutoResult(){ code = 136,error = 0 };
        }

        public PBAutoResult SendMouseScroll(int siteId, int deviceId, int scrollValue)
        {
            var b = new ByteUtil();
            b.writeShort(233);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(scrollValue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 233,error = 0 };
        }

        public PBAutoResult SendTouchInput(int siteId, int deviceId, int touchId, int touchType, int screenPosX, int screenPosY, bool firstPass)
        {
            var b = new ByteUtil();
            b.writeShort(146);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(touchId);
            b.writeInt(touchType);
            b.writeInt(screenPosX);
            b.writeInt(screenPosY);
            b.writeBool(firstPass);
            b = c.Send(b, false);return new PBAutoResult(){ code = 146,error = 0 };
        }

        public PBAutoResult SendKeyboardInput(int siteId, int keyboardEventType, int keyCode)
        {
            var b = new ByteUtil();
            b.writeShort(137);
            b.writeInt(siteId);
            b.writeInt(keyboardEventType);
            b.writeInt(keyCode);
            b = c.Send(b, false);return new PBAutoResult(){ code = 137,error = 0 };
        }

        public PBAutoResult SetShowCursorInFullscreen(int siteId, bool showCursor)
        {
            var b = new ByteUtil();
            b.writeShort(138);
            b.writeInt(siteId);
            b.writeBool(showCursor);
            b = c.Send(b, false);return new PBAutoResult(){ code = 138,error = 0 };
        }

        public PBAutoResult SetNodeOfSiteIsAudioClockMaster(int siteId, bool isMaster)
        {
            var b = new ByteUtil();
            b.writeShort(145);
            b.writeInt(siteId);
            b.writeBool(isMaster);
            b = c.Send(b, false);return new PBAutoResult(){ code = 145,error = 0 };
        }

        public struct AddEncryptionKeyGetIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public bool isKeyAdded;
        }
        public AddEncryptionKeyGetIdResult AddEncryptionKeyGetId(string encryptionKey)
        {
            var b = new ByteUtil();
            b.writeShort(164);
            b.writeStringWide(encryptionKey);
            b = c.Send(b, true);
            var r = new AddEncryptionKeyGetIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 164)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isKeyAdded = b.readBool();
            }
            return r;
        }

        public struct AddEncryptionPolicyGetIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public bool isKeyAdded;
        }
        public AddEncryptionPolicyGetIdResult AddEncryptionPolicyGetId(string encryptionPolicy)
        {
            var b = new ByteUtil();
            b.writeShort(165);
            b.writeStringWide(encryptionPolicy);
            b = c.Send(b, true);
            var r = new AddEncryptionPolicyGetIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 165)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isKeyAdded = b.readBool();
            }
            return r;
        }

        public PBAutoResult SetRouteInputToLayer(int siteId, bool enableInputRouting)
        {
            var b = new ByteUtil();
            b.writeShort(166);
            b.writeInt(siteId);
            b.writeBool(enableInputRouting);
            b = c.Send(b, false);return new PBAutoResult(){ code = 166,error = 0 };
        }

        public PBAutoResult SetRouteInputToAutomation(int siteId, bool enableInputAutomation)
        {
            var b = new ByteUtil();
            b.writeShort(167);
            b.writeInt(siteId);
            b.writeBool(enableInputAutomation);
            b = c.Send(b, false);return new PBAutoResult(){ code = 167,error = 0 };
        }

        public PBAutoResult SetEnableOutputForPicking(int siteId, int outputId, bool enableInputPicking)
        {
            var b = new ByteUtil();
            b.writeShort(168);
            b.writeInt(siteId);
            b.writeInt(outputId);
            b.writeBool(enableInputPicking);
            b = c.Send(b, false);return new PBAutoResult(){ code = 168,error = 0 };
        }

        public PBAutoResult SetASIOMasterVolume(int siteId, double asioVolume)
        {
            var b = new ByteUtil();
            b.writeShort(169);
            b.writeInt(siteId);
            b.writeDouble(asioVolume);
            b = c.Send(b, false);return new PBAutoResult(){ code = 169,error = 0 };
        }

        public struct GetThumbnailByPathResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int thumbnailWidth;
            public int thumbnailHeight;
            public byte[] thumbnailData;
        }
        public GetThumbnailByPathResult GetThumbnailByPath(string resourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(162);
            b.writeStringWide(resourcePath);
            b = c.Send(b, true);
            var r = new GetThumbnailByPathResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 162)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.thumbnailWidth = b.readInt();
                r.thumbnailHeight = b.readInt();
                r.thumbnailData = b.readByteBuffer();
            }
            return r;
        }

        public struct GetThumbnailByItemIndexResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int thumbnailWidth;
            public int thumbnailHeight;
            public byte[] thumbnailData;
        }
        public GetThumbnailByItemIndexResult GetThumbnailByItemIndex(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(163);
            b.writeInt(treeItemIndex);
            b = c.Send(b, true);
            var r = new GetThumbnailByItemIndexResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 163)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.thumbnailWidth = b.readInt();
                r.thumbnailHeight = b.readInt();
                r.thumbnailData = b.readByteBuffer();
            }
            return r;
        }

        public PBAutoResult CreatePlaylist(bool doSetDmxIds, int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(176);
            b.writeBool(doSetDmxIds);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 176,error = 0 };
        }

        public PBAutoResult CreatePlaylistInPath(string projectPath, bool doSetDmxIds, int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(177);
            b.writeStringNarrow(projectPath);
            b.writeBool(doSetDmxIds);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 177,error = 0 };
        }

        public PBAutoResult CreatePlaylistInItemId(int treeItemIndex, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(178);
            b.writeInt(treeItemIndex);
            b.writeBool(setdmxFileIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 178,error = 0 };
        }

        public PBAutoResult CreatePlaylistInPathFromFolder(string projectPath, string sourceProjectPath, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(179);
            b.writeStringNarrow(projectPath);
            b.writeStringNarrow(sourceProjectPath);
            b.writeBool(setdmxFileIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 179,error = 0 };
        }

        public PBAutoResult CreatePlaylistInTreeItemFromFolder(int treeItemIndex, int sourceFolderItemId, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(180);
            b.writeInt(treeItemIndex);
            b.writeInt(sourceFolderItemId);
            b.writeBool(setdmxFileIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 180,error = 0 };
        }

        public PBAutoResult PushBackPlaylistEntryByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int resourceDmxFolderId, int resourceDmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(181);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(resourceDmxFolderId);
            b.writeInt(resourceDmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 181,error = 0 };
        }

        public PBAutoResult PushBackPlaylistEntryByPath(string playlistPath, string resourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(182);
            b.writeStringNarrow(playlistPath);
            b.writeStringNarrow(resourcePath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 182,error = 0 };
        }

        public PBAutoResult PushBackPlaylistEntryByItemId(int playlistItemIndex, int resourceItemId)
        {
            var b = new ByteUtil();
            b.writeShort(183);
            b.writeInt(playlistItemIndex);
            b.writeInt(resourceItemId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 183,error = 0 };
        }

        public PBAutoResult InsertPlaylistEntryByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int resourceDmxFolderId, int resourceDmxFileId, int index)
        {
            var b = new ByteUtil();
            b.writeShort(184);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(resourceDmxFolderId);
            b.writeInt(resourceDmxFileId);
            b.writeInt(index);
            b = c.Send(b, false);return new PBAutoResult(){ code = 184,error = 0 };
        }

        public PBAutoResult InsertPlaylistEntryByPath(string playlistPath, string resourcePath, int index)
        {
            var b = new ByteUtil();
            b.writeShort(185);
            b.writeStringNarrow(playlistPath);
            b.writeStringNarrow(resourcePath);
            b.writeInt(index);
            b = c.Send(b, false);return new PBAutoResult(){ code = 185,error = 0 };
        }

        public PBAutoResult InsertPlaylistEntryByItemId(int playlistItemIndex, int resourceItemId, int index)
        {
            var b = new ByteUtil();
            b.writeShort(186);
            b.writeInt(playlistItemIndex);
            b.writeInt(resourceItemId);
            b.writeInt(index);
            b = c.Send(b, false);return new PBAutoResult(){ code = 186,error = 0 };
        }

        public PBAutoResult RemovePlaylistEntryByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index)
        {
            var b = new ByteUtil();
            b.writeShort(187);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(index);
            b = c.Send(b, false);return new PBAutoResult(){ code = 187,error = 0 };
        }

        public PBAutoResult RemovePlaylistEntryByPath(string playlistPath, int index)
        {
            var b = new ByteUtil();
            b.writeShort(188);
            b.writeStringNarrow(playlistPath);
            b.writeInt(index);
            b = c.Send(b, false);return new PBAutoResult(){ code = 188,error = 0 };
        }

        public PBAutoResult RemovePlaylistEntryByItemId(int playlistItemIndex, int index)
        {
            var b = new ByteUtil();
            b.writeShort(189);
            b.writeInt(playlistItemIndex);
            b.writeInt(index);
            b = c.Send(b, false);return new PBAutoResult(){ code = 189,error = 0 };
        }

        public struct GetPlaylistSizeByDmxIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int playlistSize;
        }
        public GetPlaylistSizeByDmxIdResult GetPlaylistSizeByDmxId(int playlistDmxFolderId, int playlistdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(190);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b = c.Send(b, true);
            var r = new GetPlaylistSizeByDmxIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 190)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.playlistSize = b.readInt();
            }
            return r;
        }

        public struct GetPlaylistSizeByPathResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int playlistSize;
        }
        public GetPlaylistSizeByPathResult GetPlaylistSizeByPath(string playlistPath)
        {
            var b = new ByteUtil();
            b.writeShort(191);
            b.writeStringNarrow(playlistPath);
            b = c.Send(b, true);
            var r = new GetPlaylistSizeByPathResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 191)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.playlistSize = b.readInt();
            }
            return r;
        }

        public struct GetPlaylistSizeByItemIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int playlistSize;
        }
        public GetPlaylistSizeByItemIdResult GetPlaylistSizeByItemId(int playlistItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(192);
            b.writeInt(playlistItemIndex);
            b = c.Send(b, true);
            var r = new GetPlaylistSizeByItemIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 192)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.playlistSize = b.readInt();
            }
            return r;
        }

        public PBAutoResult SetPlaylistEntryIndexByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int newIndex)
        {
            var b = new ByteUtil();
            b.writeShort(199);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(index);
            b.writeInt(newIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 199,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryIndexByPath(string playlistPath, int index, int newIndex)
        {
            var b = new ByteUtil();
            b.writeShort(200);
            b.writeStringNarrow(playlistPath);
            b.writeInt(index);
            b.writeInt(newIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 200,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryIndexByItemId(int playlistItemIndex, int index, int newIndex)
        {
            var b = new ByteUtil();
            b.writeShort(201);
            b.writeInt(playlistItemIndex);
            b.writeInt(index);
            b.writeInt(newIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 201,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryDurationByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(202);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 202,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryDurationByPath(string playlistPath, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(203);
            b.writeStringNarrow(playlistPath);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 203,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryDurationByItemId(int playlistItemIndex, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(204);
            b.writeInt(playlistItemIndex);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 204,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryFadeOutTimeByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(205);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 205,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryFadeOutTimeByPath(string playlistPath, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(206);
            b.writeStringNarrow(playlistPath);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 206,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryFadeOutTimeByItemId(int playlistItemIndex, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(207);
            b.writeInt(playlistItemIndex);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 207,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryInPointByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(208);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 208,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryInPointByPath(string playlistPath, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(210);
            b.writeStringNarrow(playlistPath);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 210,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryInPointByItemId(int playlistItemIndex, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(211);
            b.writeInt(playlistItemIndex);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 211,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryOutPointByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(212);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 212,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryOutPointByPath(string playlistPath, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(213);
            b.writeStringNarrow(playlistPath);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 213,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryOutPointByItemId(int playlistItemIndex, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(214);
            b.writeInt(playlistItemIndex);
            b.writeInt(index);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 214,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryTransitionByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int fadeFxId)
        {
            var b = new ByteUtil();
            b.writeShort(215);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(index);
            b.writeInt(fadeFxId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 215,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryTransitionByPath(string playlistPath, int index, int fadeFxId)
        {
            var b = new ByteUtil();
            b.writeShort(216);
            b.writeStringNarrow(playlistPath);
            b.writeInt(index);
            b.writeInt(fadeFxId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 216,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryTransitionByItemId(int playlistItemIndex, int index, int fadeFxId)
        {
            var b = new ByteUtil();
            b.writeShort(217);
            b.writeInt(playlistItemIndex);
            b.writeInt(index);
            b.writeInt(fadeFxId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 217,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryNoteByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, string pNote)
        {
            var b = new ByteUtil();
            b.writeShort(218);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(index);
            b.writeStringNarrow(pNote);
            b = c.Send(b, false);return new PBAutoResult(){ code = 218,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryNoteByPath(string playlistPath, int index, string pNote)
        {
            var b = new ByteUtil();
            b.writeShort(219);
            b.writeStringNarrow(playlistPath);
            b.writeInt(index);
            b.writeStringNarrow(pNote);
            b = c.Send(b, false);return new PBAutoResult(){ code = 219,error = 0 };
        }

        public PBAutoResult SetPlaylistEntryNoteByItemId(int playlistItemIndex, int index, string pNote)
        {
            var b = new ByteUtil();
            b.writeShort(220);
            b.writeInt(playlistItemIndex);
            b.writeInt(index);
            b.writeStringNarrow(pNote);
            b = c.Send(b, false);return new PBAutoResult(){ code = 220,error = 0 };
        }

        public PBAutoResult RecordLiveInputByDmxId(int folderID, int fileID, string pFilename, string encodingPresetName, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(222);
            b.writeInt(folderID);
            b.writeInt(fileID);
            b.writeStringNarrow(pFilename);
            b.writeStringNarrow(encodingPresetName);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 222,error = 0 };
        }

        public PBAutoResult RecordLiveInputStartByDmxId(int folderID, int fileID, string pFilename, string encodingPresetName)
        {
            var b = new ByteUtil();
            b.writeShort(223);
            b.writeInt(folderID);
            b.writeInt(fileID);
            b.writeStringNarrow(pFilename);
            b.writeStringNarrow(encodingPresetName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 223,error = 0 };
        }

        public PBAutoResult RecordLiveInputByName(string liveInputResourcePath, string pFilename, string encodingPresetName, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.writeShort(225);
            b.writeStringNarrow(liveInputResourcePath);
            b.writeStringNarrow(pFilename);
            b.writeStringNarrow(encodingPresetName);
            b.writeInt(hours);
            b.writeInt(minutes);
            b.writeInt(seconds);
            b.writeInt(frames);
            b = c.Send(b, false);return new PBAutoResult(){ code = 225,error = 0 };
        }

        public PBAutoResult RecordLiveInputStartByName(string liveInputResourcePath, string pFilename, string encodingPresetName)
        {
            var b = new ByteUtil();
            b.writeShort(226);
            b.writeStringNarrow(liveInputResourcePath);
            b.writeStringNarrow(pFilename);
            b.writeStringNarrow(encodingPresetName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 226,error = 0 };
        }

        public PBAutoResult ExportVideo(string pFilename, string encodingPresetName, int sequenceId, int startHour, int startMinute, int startSecond, int startFrame, int endHour, int endMinute, int endSecond, int endFrame)
        {
            var b = new ByteUtil();
            b.writeShort(227);
            b.writeStringNarrow(pFilename);
            b.writeStringNarrow(encodingPresetName);
            b.writeInt(sequenceId);
            b.writeInt(startHour);
            b.writeInt(startMinute);
            b.writeInt(startSecond);
            b.writeInt(startFrame);
            b.writeInt(endHour);
            b.writeInt(endMinute);
            b.writeInt(endSecond);
            b.writeInt(endFrame);
            b = c.Send(b, false);return new PBAutoResult(){ code = 227,error = 0 };
        }

        public PBAutoResult EncodeFileByName(string resourcePath, string encodingPreset)
        {
            var b = new ByteUtil();
            b.writeShort(228);
            b.writeStringNarrow(resourcePath);
            b.writeStringNarrow(encodingPreset);
            b = c.Send(b, false);return new PBAutoResult(){ code = 228,error = 0 };
        }

        public PBAutoResult EncodeFileByDmxId(int folderID, int fileID, string encodingPreset)
        {
            var b = new ByteUtil();
            b.writeShort(230);
            b.writeInt(folderID);
            b.writeInt(fileID);
            b.writeStringNarrow(encodingPreset);
            b = c.Send(b, false);return new PBAutoResult(){ code = 230,error = 0 };
        }

        public PBAutoResult EncodeFileToPath(string resourcePath, string projectPath, bool overwriteExisting, string encodingPreset)
        {
            var b = new ByteUtil();
            b.writeShort(229);
            b.writeStringNarrow(resourcePath);
            b.writeStringNarrow(projectPath);
            b.writeBool(overwriteExisting);
            b.writeStringNarrow(encodingPreset);
            b = c.Send(b, false);return new PBAutoResult(){ code = 229,error = 0 };
        }

        public PBAutoResult EncodeFileByDmxId(int folderID, int fileID, string projectPath, bool overwriteExisting, string encodingPreset)
        {
            var b = new ByteUtil();
            b.writeShort(231);
            b.writeInt(folderID);
            b.writeInt(fileID);
            b.writeStringNarrow(projectPath);
            b.writeBool(overwriteExisting);
            b.writeStringNarrow(encodingPreset);
            b = c.Send(b, false);return new PBAutoResult(){ code = 231,error = 0 };
        }

        public PBAutoResult SetCanvasResolutionByDmxId(int canvasDmxFolderId, int canvasDmxFileId, int width, int height)
        {
            var b = new ByteUtil();
            b.writeShort(239);
            b.writeInt(canvasDmxFolderId);
            b.writeInt(canvasDmxFileId);
            b.writeInt(width);
            b.writeInt(height);
            b = c.Send(b, false);return new PBAutoResult(){ code = 239,error = 0 };
        }

        public PBAutoResult SetCanvasResolutionByPath(string canvasResourcePath, int width, int height)
        {
            var b = new ByteUtil();
            b.writeShort(240);
            b.writeStringNarrow(canvasResourcePath);
            b.writeInt(width);
            b.writeInt(height);
            b = c.Send(b, false);return new PBAutoResult(){ code = 240,error = 0 };
        }

        public PBAutoResult SetCanvasResolutionByItemId(int canvasItemIndex, int width, int height)
        {
            var b = new ByteUtil();
            b.writeShort(241);
            b.writeInt(canvasItemIndex);
            b.writeInt(width);
            b.writeInt(height);
            b = c.Send(b, false);return new PBAutoResult(){ code = 241,error = 0 };
        }

        public PBAutoResult ClearCanvasByDmxId(int canvasDmxFolderId, int canvasDmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(242);
            b.writeInt(canvasDmxFolderId);
            b.writeInt(canvasDmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 242,error = 0 };
        }

        public PBAutoResult ClearCanvasByPath(string canvasResourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(243);
            b.writeStringNarrow(canvasResourcePath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 243,error = 0 };
        }

        public PBAutoResult ClearCanvasByItemId(int canvasItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(244);
            b.writeInt(canvasItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 244,error = 0 };
        }

        public PBAutoResult ExecuteCanvasCmdByDmxId(int canvasDmxFolderId, int canvasDmxFileId, string cmd, bool cmdContainsResourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(245);
            b.writeInt(canvasDmxFolderId);
            b.writeInt(canvasDmxFileId);
            b.writeStringNarrow(cmd);
            b.writeBool(cmdContainsResourcePath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 245,error = 0 };
        }

        public PBAutoResult ExecuteCanvasCmdByPath(string canvasResourcePath, string cmd, bool cmdContainsResourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(246);
            b.writeStringNarrow(canvasResourcePath);
            b.writeStringNarrow(cmd);
            b.writeBool(cmdContainsResourcePath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 246,error = 0 };
        }

        public PBAutoResult ExecuteCanvasCmdByItemId(int canvasItemIndex, string cmd, bool cmdContainsResourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(247);
            b.writeInt(canvasItemIndex);
            b.writeStringNarrow(cmd);
            b.writeBool(cmdContainsResourcePath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 247,error = 0 };
        }

        public struct GetCanvasDrawCommandsByDmxIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public string commands;
        }
        public GetCanvasDrawCommandsByDmxIdResult GetCanvasDrawCommandsByDmxId(int canvasDmxFolderId, int canvasDmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(248);
            b.writeInt(canvasDmxFolderId);
            b.writeInt(canvasDmxFileId);
            b = c.Send(b, true);
            var r = new GetCanvasDrawCommandsByDmxIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 248)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.commands = b.readStringNarrow();
            }
            return r;
        }

        public struct GetCanvasDrawCommandsByPathResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public string commands;
        }
        public GetCanvasDrawCommandsByPathResult GetCanvasDrawCommandsByPath(string canvasResourcePath)
        {
            var b = new ByteUtil();
            b.writeShort(249);
            b.writeStringNarrow(canvasResourcePath);
            b = c.Send(b, true);
            var r = new GetCanvasDrawCommandsByPathResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 249)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.commands = b.readStringNarrow();
            }
            return r;
        }

        public struct GetCanvasDrawCommandsByItemIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public string commands;
        }
        public GetCanvasDrawCommandsByItemIdResult GetCanvasDrawCommandsByItemId(int canvasItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(250);
            b.writeInt(canvasItemIndex);
            b = c.Send(b, true);
            var r = new GetCanvasDrawCommandsByItemIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 250)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.commands = b.readStringNarrow();
            }
            return r;
        }

        public struct GetMediaWidthByDmxIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int width;
        }
        public GetMediaWidthByDmxIdResult GetMediaWidthByDmxId(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(251);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, true);
            var r = new GetMediaWidthByDmxIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 251)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.width = b.readInt();
            }
            return r;
        }

        public struct GetMediaWidthByPathResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int width;
        }
        public GetMediaWidthByPathResult GetMediaWidthByPath(string folderPathToProject)
        {
            var b = new ByteUtil();
            b.writeShort(252);
            b.writeStringNarrow(folderPathToProject);
            b = c.Send(b, true);
            var r = new GetMediaWidthByPathResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 252)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.width = b.readInt();
            }
            return r;
        }

        public struct GetMediaWidthByItemIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int width;
        }
        public GetMediaWidthByItemIdResult GetMediaWidthByItemId(int itemId)
        {
            var b = new ByteUtil();
            b.writeShort(253);
            b.writeInt(itemId);
            b = c.Send(b, true);
            var r = new GetMediaWidthByItemIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 253)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.width = b.readInt();
            }
            return r;
        }

        public struct GetMediaHeightByDmxIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int height;
        }
        public GetMediaHeightByDmxIdResult GetMediaHeightByDmxId(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(254);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, true);
            var r = new GetMediaHeightByDmxIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 254)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.height = b.readInt();
            }
            return r;
        }

        public struct GetMediaHeightByPathResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int height;
        }
        public GetMediaHeightByPathResult GetMediaHeightByPath(string folderPathToProject)
        {
            var b = new ByteUtil();
            b.writeShort(255);
            b.writeStringNarrow(folderPathToProject);
            b = c.Send(b, true);
            var r = new GetMediaHeightByPathResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 255)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.height = b.readInt();
            }
            return r;
        }

        public struct GetMediaHeightByItemIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int height;
        }
        public GetMediaHeightByItemIdResult GetMediaHeightByItemId(int itemId)
        {
            var b = new ByteUtil();
            b.writeShort(256);
            b.writeInt(itemId);
            b = c.Send(b, true);
            var r = new GetMediaHeightByItemIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 256)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.height = b.readInt();
            }
            return r;
        }

        public struct GetProjectPathOnDiscResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public string commands;
        }
        public GetProjectPathOnDiscResult GetProjectPathOnDisc()
        {
            var b = new ByteUtil();
            b.writeShort(257);
            b = c.Send(b, true);
            var r = new GetProjectPathOnDiscResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 257)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.commands = b.readStringNarrow();
            }
            return r;
        }

        public PBAutoResult SaveProjectAs(string folderPathToProject, string projectXmlFileName)
        {
            var b = new ByteUtil();
            b.writeShort(258);
            b.writeStringNarrow(folderPathToProject);
            b.writeStringNarrow(projectXmlFileName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 258,error = 0 };
        }

        public PBAutoResult SaveProjectCopy(string folderPathToProject, string projectXmlFileName)
        {
            var b = new ByteUtil();
            b.writeShort(259);
            b.writeStringNarrow(folderPathToProject);
            b.writeStringNarrow(projectXmlFileName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 259,error = 0 };
        }

        public PBAutoResult BundleProject(string bundlePath, string bundleName)
        {
            var b = new ByteUtil();
            b.writeShort(260);
            b.writeStringNarrow(bundlePath);
            b.writeStringNarrow(bundleName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 260,error = 0 };
        }

        public PBAutoResult SetResourceNameByPath(string resourcePath, string newResourceName)
        {
            var b = new ByteUtil();
            b.writeShort(261);
            b.writeStringNarrow(resourcePath);
            b.writeStringNarrow(newResourceName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 261,error = 0 };
        }

        public PBAutoResult SetResourceNameByItemIndex(int treeItemIndex, string newResourceName)
        {
            var b = new ByteUtil();
            b.writeShort(263);
            b.writeInt(treeItemIndex);
            b.writeStringNarrow(newResourceName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 263,error = 0 };
        }

        public PBAutoResult SetResourceNameByDmxId(int dmxFolderId, int dmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.writeShort(262);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringNarrow(newResourceName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 262,error = 0 };
        }

        public PBAutoResult SendCanvasCmdsToStackByDmxId(int canvasDmxFolderId, int canvasDmxFileId, bool doAddToStack)
        {
            var b = new ByteUtil();
            b.writeShort(265);
            b.writeInt(canvasDmxFolderId);
            b.writeInt(canvasDmxFileId);
            b.writeBool(doAddToStack);
            b = c.Send(b, false);return new PBAutoResult(){ code = 265,error = 0 };
        }

        public PBAutoResult SetAddCanvasCmdsToStackByPath(string canvasResourcePath, bool doAddToStack)
        {
            var b = new ByteUtil();
            b.writeShort(266);
            b.writeStringNarrow(canvasResourcePath);
            b.writeBool(doAddToStack);
            b = c.Send(b, false);return new PBAutoResult(){ code = 266,error = 0 };
        }

        public PBAutoResult SetAddCanvasCmdsToStackByItemId(int canvasItemIndex, bool doAddToStack)
        {
            var b = new ByteUtil();
            b.writeShort(267);
            b.writeInt(canvasItemIndex);
            b.writeBool(doAddToStack);
            b = c.Send(b, false);return new PBAutoResult(){ code = 267,error = 0 };
        }

        public PBAutoResult ClearEmptyPlaylistEntriesByDmxId(int playlistDmxFolderId, int playlistdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(268);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 268,error = 0 };
        }

        public PBAutoResult ClearEmptyPlaylistEntriesByPath(string playlistPath)
        {
            var b = new ByteUtil();
            b.writeShort(269);
            b.writeStringNarrow(playlistPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 269,error = 0 };
        }

        public PBAutoResult ClearEmptyPlaylistEntriesByItemId(int playlistItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(270);
            b.writeInt(playlistItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 270,error = 0 };
        }

        public PBAutoResult ClearAllPlaylistEntriesByDmxId(int playlistDmxFolderId, int playlistdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(271);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 271,error = 0 };
        }

        public PBAutoResult ClearAllPlaylistEntriesByPath(string playlistPath)
        {
            var b = new ByteUtil();
            b.writeShort(272);
            b.writeStringNarrow(playlistPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 272,error = 0 };
        }

        public PBAutoResult ClearAllPlaylistEntriesByItemIndex(int playlistItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(273);
            b.writeInt(playlistItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 273,error = 0 };
        }

        public PBAutoResult SetSublayerParamOfKindDouble(int siteId, int deviceId, int sublayerId, int parameterKindId, double parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.writeShort(274);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(sublayerId);
            b.writeInt(parameterKindId);
            b.writeDouble(parameterValue);
            b.writeBool(doSilent);
            b.writeBool(doDirect);
            b = c.Send(b, false);return new PBAutoResult(){ code = 274,error = 0 };
        }

        public PBAutoResult HandleSublayer(int siteId, int deviceId, int action, int data)
        {
            var b = new ByteUtil();
            b.writeShort(275);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(action);
            b.writeInt(data);
            b = c.Send(b, false);return new PBAutoResult(){ code = 275,error = 0 };
        }

        public PBAutoResult SetCueName(int sequenceId, int cueId, string cueName)
        {
            var b = new ByteUtil();
            b.writeShort(276);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b.writeStringNarrow(cueName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 276,error = 0 };
        }

        public struct GetCueNameResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public string cueName;
        }
        public GetCueNameResult GetCueName(int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.writeShort(277);
            b.writeInt(sequenceId);
            b.writeInt(cueId);
            b = c.Send(b, true);
            var r = new GetCueNameResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 277)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.cueName = b.readStringNarrow();
            }
            return r;
        }

        public PBAutoResult StoreActiveSite(int sequenceId, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(278);
            b.writeInt(sequenceId);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 278,error = 0 };
        }

        public PBAutoResult StoreActiveDevice(int sequenceId, int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(279);
            b.writeInt(sequenceId);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 279,error = 0 };
        }

        public PBAutoResult StoreActiveParam(int sequenceId, int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(280);
            b.writeInt(sequenceId);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 280,error = 0 };
        }

        public PBAutoResult AssignDeviceByName(int siteId, int deviceId, int sourceDeviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(282);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(sourceDeviceId);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 282,error = 0 };
        }

        public PBAutoResult AssignResourceToParam(int siteId, int deviceId, int dmxFolderId, int dmxFileId, bool forMesh, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(283);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(forMesh);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 283,error = 0 };
        }

        public PBAutoResult AddImageSequence(string folderPath, int siteId, int dmxFolderId, int dmxFileId, int fps)
        {
            var b = new ByteUtil();
            b.writeShort(284);
            b.writeStringNarrow(folderPath);
            b.writeInt(siteId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeInt(fps);
            b = c.Send(b, false);return new PBAutoResult(){ code = 284,error = 0 };
        }

        public PBAutoResult AddImageSequenceToFolder(string folderPath, int siteId, int dmxFolderId, int dmxFileId, int fps, string projectPath)
        {
            var b = new ByteUtil();
            b.writeShort(285);
            b.writeStringNarrow(folderPath);
            b.writeInt(siteId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeInt(fps);
            b.writeStringNarrow(projectPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 285,error = 0 };
        }

        public PBAutoResult AddImageSequenceToTreeItem(string folderPath, int siteId, int dmxFolderId, int dmxFileId, int fps, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(286);
            b.writeStringNarrow(folderPath);
            b.writeInt(siteId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeInt(fps);
            b.writeInt(treeItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 286,error = 0 };
        }

        public PBAutoResult AddImageSequenceFromLocalNode(string folderPath, int fps)
        {
            var b = new ByteUtil();
            b.writeShort(287);
            b.writeStringNarrow(folderPath);
            b.writeInt(fps);
            b = c.Send(b, false);return new PBAutoResult(){ code = 287,error = 0 };
        }

        public PBAutoResult AddImageSequenceFromLocalNodeId(string folderPath, int fps, int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(288);
            b.writeStringNarrow(folderPath);
            b.writeInt(fps);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 288,error = 0 };
        }

        public PBAutoResult AddImageSequenceFromLocalNodeToFolder(string folderPath, int fps, string projectPath)
        {
            var b = new ByteUtil();
            b.writeShort(289);
            b.writeStringNarrow(folderPath);
            b.writeInt(fps);
            b.writeStringNarrow(projectPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 289,error = 0 };
        }

        public PBAutoResult AddImageSequenceFromLocalNodeToFolderId(string folderPath, int fps, int dmxFolderId, int dmxFileId, string projectPath)
        {
            var b = new ByteUtil();
            b.writeShort(290);
            b.writeStringNarrow(folderPath);
            b.writeInt(fps);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringNarrow(projectPath);
            b = c.Send(b, false);return new PBAutoResult(){ code = 290,error = 0 };
        }

        public PBAutoResult AddImageSequenceFromLocalNodeToTreeItem(string folderPath, int fps, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(291);
            b.writeStringNarrow(folderPath);
            b.writeInt(fps);
            b.writeInt(treeItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 291,error = 0 };
        }

        public PBAutoResult AddImageSequenceFromLocalNodeToTreeItemId(string folderPath, int fps, int dmxFolderId, int dmxFileId, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(292);
            b.writeStringNarrow(folderPath);
            b.writeInt(fps);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeInt(treeItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 292,error = 0 };
        }

        public PBAutoResult SetTextFormatted(int dmxFolderId, int dmxFileId, string text, bool isFormatted)
        {
            var b = new ByteUtil();
            b.writeShort(293);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringNarrow(text);
            b.writeBool(isFormatted);
            b = c.Send(b, false);return new PBAutoResult(){ code = 293,error = 0 };
        }

        public PBAutoResult SetTextFormattedWide(int dmxFolderId, int dmxFileId, string text, bool isFormatted)
        {
            var b = new ByteUtil();
            b.writeShort(294);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringWide(text);
            b.writeBool(isFormatted);
            b = c.Send(b, false);return new PBAutoResult(){ code = 294,error = 0 };
        }

        public struct GetCurrentTimeCueInfoResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int hours;
            public int minutes;
            public int seconds;
            public int frames;
            public int previousCueId;
            public string previousCueName;
            public int hoursPreviousCue;
            public int minutesPreviousCue;
            public int secondsPreviousCue;
            public int framesPreviousCue;
            public int previousCueMode;
            public int nextCueId;
            public string nextCueName;
            public int hoursNextCue;
            public int minutesNextCue;
            public int secondsNextCue;
            public int framesNextCue;
            public int nextCueMode;
        }
        public GetCurrentTimeCueInfoResult GetCurrentTimeCueInfo(int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(295);
            b.writeInt(sequenceId);
            b = c.Send(b, true);
            var r = new GetCurrentTimeCueInfoResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 295)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.hours = b.readInt();
                r.minutes = b.readInt();
                r.seconds = b.readInt();
                r.frames = b.readInt();
                r.previousCueId = b.readInt();
                r.previousCueName = b.readStringNarrow();
                r.hoursPreviousCue = b.readInt();
                r.minutesPreviousCue = b.readInt();
                r.secondsPreviousCue = b.readInt();
                r.framesPreviousCue = b.readInt();
                r.previousCueMode = b.readInt();
                r.nextCueId = b.readInt();
                r.nextCueName = b.readStringNarrow();
                r.hoursNextCue = b.readInt();
                r.minutesNextCue = b.readInt();
                r.secondsNextCue = b.readInt();
                r.framesNextCue = b.readInt();
                r.nextCueMode = b.readInt();
            }
            return r;
        }

        public PBAutoResult GetContentIsConsistentByTreeItem(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(296);
            b.writeInt(treeItemIndex);
            b = c.Send(b, false);return new PBAutoResult(){ code = 296,error = 0 };
        }

        public PBAutoResult SpreadToSite(string resourcePath, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(297);
            b.writeStringNarrow(resourcePath);
            b.writeInt(siteId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 297,error = 0 };
        }

        public PBAutoResult SetGroupSelection(int groupIndex, int selectionMode)
        {
            var b = new ByteUtil();
            b.writeShort(298);
            b.writeInt(groupIndex);
            b.writeInt(selectionMode);
            b = c.Send(b, false);return new PBAutoResult(){ code = 298,error = 0 };
        }

        public PBAutoResult SetSequenceSelection(int sequenceId)
        {
            var b = new ByteUtil();
            b.writeShort(299);
            b.writeInt(sequenceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 299,error = 0 };
        }

        public PBAutoResult CreatePlaylistWithName(bool doSetDmxIds, int dmxFolderId, int dmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.writeShort(300);
            b.writeBool(doSetDmxIds);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringNarrow(newResourceName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 300,error = 0 };
        }

        public PBAutoResult CreatePlaylistInPathWithName(string projectPath, bool doSetDmxIds, int dmxFolderId, int dmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.writeShort(301);
            b.writeStringNarrow(projectPath);
            b.writeBool(doSetDmxIds);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeStringNarrow(newResourceName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 301,error = 0 };
        }

        public PBAutoResult CreatePlaylistInItemIdWithName(int treeItemIndex, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.writeShort(302);
            b.writeInt(treeItemIndex);
            b.writeBool(setdmxFileIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b.writeStringNarrow(newResourceName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 302,error = 0 };
        }

        public PBAutoResult CreatePlaylistInPathFromFolderWithName(string projectPath, string sourceProjectPath, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.writeShort(303);
            b.writeStringNarrow(projectPath);
            b.writeStringNarrow(sourceProjectPath);
            b.writeBool(setdmxFileIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b.writeStringNarrow(newResourceName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 303,error = 0 };
        }

        public PBAutoResult CreatePlaylistInTreeItemFromFolderWithName(int treeItemIndex, int sourceFolderItemId, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.writeShort(304);
            b.writeInt(treeItemIndex);
            b.writeInt(sourceFolderItemId);
            b.writeBool(setdmxFileIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b.writeStringNarrow(newResourceName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 304,error = 0 };
        }

        public PBAutoResult SetWatchedFolderProperty(string projectPath, int watchFolderProperty, bool enable)
        {
            var b = new ByteUtil();
            b.writeShort(305);
            b.writeStringNarrow(projectPath);
            b.writeInt(watchFolderProperty);
            b.writeBool(enable);
            b = c.Send(b, false);return new PBAutoResult(){ code = 305,error = 0 };
        }

        public PBAutoResult SetWatchedFolderPropertyByItemId(int treeItemIndex, int watchFolderProperty, bool enable)
        {
            var b = new ByteUtil();
            b.writeShort(306);
            b.writeInt(treeItemIndex);
            b.writeInt(watchFolderProperty);
            b.writeBool(enable);
            b = c.Send(b, false);return new PBAutoResult(){ code = 306,error = 0 };
        }

        public PBAutoResult SetFolderSpreadToSite(string projectPath, int siteId, bool enable)
        {
            var b = new ByteUtil();
            b.writeShort(307);
            b.writeStringNarrow(projectPath);
            b.writeInt(siteId);
            b.writeBool(enable);
            b = c.Send(b, false);return new PBAutoResult(){ code = 307,error = 0 };
        }

        public PBAutoResult SetFolderSpreadToSiteByItemId(int treeItemIndex, int siteId, bool enable)
        {
            var b = new ByteUtil();
            b.writeShort(308);
            b.writeInt(treeItemIndex);
            b.writeInt(siteId);
            b.writeBool(enable);
            b = c.Send(b, false);return new PBAutoResult(){ code = 308,error = 0 };
        }

        public PBAutoResult ClearStreamingText(int dmxFolderId, int dmxFileId, bool pendingOnly)
        {
            var b = new ByteUtil();
            b.writeShort(309);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(pendingOnly);
            b = c.Send(b, false);return new PBAutoResult(){ code = 309,error = 0 };
        }

        public struct GetWatchedFolderPropertyResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public bool isEnabled;
        }
        public GetWatchedFolderPropertyResult GetWatchedFolderProperty(string projectPath, int watchFolderProperty)
        {
            var b = new ByteUtil();
            b.writeShort(310);
            b.writeStringNarrow(projectPath);
            b.writeInt(watchFolderProperty);
            b = c.Send(b, true);
            var r = new GetWatchedFolderPropertyResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 310)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isEnabled = b.readBool();
            }
            return r;
        }

        public struct GetWatchedFolderPropertyByItemIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public bool isEnabled;
        }
        public GetWatchedFolderPropertyByItemIdResult GetWatchedFolderPropertyByItemId(int treeItemIndex, int watchFolderProperty)
        {
            var b = new ByteUtil();
            b.writeShort(311);
            b.writeInt(treeItemIndex);
            b.writeInt(watchFolderProperty);
            b = c.Send(b, true);
            var r = new GetWatchedFolderPropertyByItemIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 311)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isEnabled = b.readBool();
            }
            return r;
        }

        public struct GetFolderSpreadToSiteResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public bool isEnabled;
        }
        public GetFolderSpreadToSiteResult GetFolderSpreadToSite(string projectPath, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(312);
            b.writeStringNarrow(projectPath);
            b.writeInt(siteId);
            b = c.Send(b, true);
            var r = new GetFolderSpreadToSiteResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 312)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isEnabled = b.readBool();
            }
            return r;
        }

        public struct GetFolderSpreadToSiteByItemIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public bool isEnabled;
        }
        public GetFolderSpreadToSiteByItemIdResult GetFolderSpreadToSiteByItemId(int treeItemIndex, int siteId)
        {
            var b = new ByteUtil();
            b.writeShort(313);
            b.writeInt(treeItemIndex);
            b.writeInt(siteId);
            b = c.Send(b, true);
            var r = new GetFolderSpreadToSiteByItemIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 313)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.isEnabled = b.readBool();
            }
            return r;
        }

        public PBAutoResult InsertPlaylistEntryWithParametersByDmxId()
        {
            var b = new ByteUtil();
            b.writeShort(314);
            b = c.Send(b, false);return new PBAutoResult(){ code = 314,error = 0 };
        }

        public PBAutoResult InsertPlaylistEntryWithParametersByPath()
        {
            var b = new ByteUtil();
            b.writeShort(315);
            b = c.Send(b, false);return new PBAutoResult(){ code = 315,error = 0 };
        }

        public PBAutoResult InsertPlaylistEntryWithParametersByTreeItem()
        {
            var b = new ByteUtil();
            b.writeShort(316);
            b = c.Send(b, false);return new PBAutoResult(){ code = 316,error = 0 };
        }

        public PBAutoResult SetParamRelative(int siteId, int deviceId, string parameterName, int parameterValue)
        {
            var b = new ByteUtil();
            b.writeShort(18);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b.writeInt(parameterValue);
            b = c.Send(b, false);return new PBAutoResult(){ code = 18,error = 0 };
        }

        public PBAutoResult AddContent(string filePath, int siteId, int dmxFolderId, int dmxFileId, bool autoIncrementDmxId)
        {
            var b = new ByteUtil();
            b.writeShort(19);
            b.writeStringNarrow(filePath);
            b.writeInt(siteId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(autoIncrementDmxId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 19,error = 0 };
        }

        public struct GetMediaInfoResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int dmxFolderId;
            public int dmxFileId;
            public string resourceName;
            public string resourcePath;
            public string projectPath;
            public int width;
            public int height;
            public int fps;
            public int hours;
            public int minutes;
            public int seconds;
            public int frames;
            public int options;
        }
        public GetMediaInfoResult GetMediaInfo(int treeItemsMediaIndex)
        {
            var b = new ByteUtil();
            b.writeShort(76);
            b.writeInt(treeItemsMediaIndex);
            b = c.Send(b, true);
            var r = new GetMediaInfoResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 76)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.dmxFolderId = b.readInt();
                r.dmxFileId = b.readInt();
                r.resourceName = b.readStringNarrow();
                r.resourcePath = b.readStringNarrow();
                r.projectPath = b.readStringNarrow();
                r.width = b.readInt();
                r.height = b.readInt();
                r.fps = b.readInt();
                r.hours = b.readInt();
                r.minutes = b.readInt();
                r.seconds = b.readInt();
                r.frames = b.readInt();
                r.options = b.readInt();
            }
            return r;
        }

        public PBAutoResult InsertPlaylistEntryWithParametersByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int resourceDmxFolderId, int resourceDmxFileId, int index, int durationHours, int durationMinutes, int durationSeconds, int durationFrames, int fadeOutHour, int fadeOutMinute, int fadeOutSecond, int fadeOutFrame, int startHour, int startMinute, int startSecond, int startFrame, int endHour, int endMinute, int endSecond, int endFrame, int fadeFxId)
        {
            var b = new ByteUtil();
            b.writeShort(314);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(resourceDmxFolderId);
            b.writeInt(resourceDmxFileId);
            b.writeInt(index);
            b.writeInt(durationHours);
            b.writeInt(durationMinutes);
            b.writeInt(durationSeconds);
            b.writeInt(durationFrames);
            b.writeInt(fadeOutHour);
            b.writeInt(fadeOutMinute);
            b.writeInt(fadeOutSecond);
            b.writeInt(fadeOutFrame);
            b.writeInt(startHour);
            b.writeInt(startMinute);
            b.writeInt(startSecond);
            b.writeInt(startFrame);
            b.writeInt(endHour);
            b.writeInt(endMinute);
            b.writeInt(endSecond);
            b.writeInt(endFrame);
            b.writeInt(fadeFxId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 314,error = 0 };
        }

        public PBAutoResult InsertPlaylistEntryWithParametersByPath(string playlistPath, string resourcePath, int index, int durationHours, int durationMinutes, int durationSeconds, int durationFrames, int fadeOutHour, int fadeOutMinute, int fadeOutSecond, int fadeOutFrame, int startHour, int startMinute, int startSecond, int startFrame, int endHour, int endMinute, int endSecond, int endFrame, int fadeFxId)
        {
            var b = new ByteUtil();
            b.writeShort(315);
            b.writeStringNarrow(playlistPath);
            b.writeStringNarrow(resourcePath);
            b.writeInt(index);
            b.writeInt(durationHours);
            b.writeInt(durationMinutes);
            b.writeInt(durationSeconds);
            b.writeInt(durationFrames);
            b.writeInt(fadeOutHour);
            b.writeInt(fadeOutMinute);
            b.writeInt(fadeOutSecond);
            b.writeInt(fadeOutFrame);
            b.writeInt(startHour);
            b.writeInt(startMinute);
            b.writeInt(startSecond);
            b.writeInt(startFrame);
            b.writeInt(endHour);
            b.writeInt(endMinute);
            b.writeInt(endSecond);
            b.writeInt(endFrame);
            b.writeInt(fadeFxId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 315,error = 0 };
        }

        public PBAutoResult InsertPlaylistEntryWithParametersByTreeItem(int playlistItemIndex, int resourceItemId, int index, int durationHours, int durationMinutes, int durationSeconds, int durationFrames, int fadeOutHour, int fadeOutMinute, int fadeOutSecond, int fadeOutFrame, int startHour, int startMinute, int startSecond, int startFrame, int endHour, int endMinute, int endSecond, int endFrame, int fadeFxId)
        {
            var b = new ByteUtil();
            b.writeShort(316);
            b.writeInt(playlistItemIndex);
            b.writeInt(resourceItemId);
            b.writeInt(index);
            b.writeInt(durationHours);
            b.writeInt(durationMinutes);
            b.writeInt(durationSeconds);
            b.writeInt(durationFrames);
            b.writeInt(fadeOutHour);
            b.writeInt(fadeOutMinute);
            b.writeInt(fadeOutSecond);
            b.writeInt(fadeOutFrame);
            b.writeInt(startHour);
            b.writeInt(startMinute);
            b.writeInt(startSecond);
            b.writeInt(startFrame);
            b.writeInt(endHour);
            b.writeInt(endMinute);
            b.writeInt(endSecond);
            b.writeInt(endFrame);
            b.writeInt(fadeFxId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 316,error = 0 };
        }

        public PBAutoResult SetYawPitchRoll(int siteId, int deviceId, bool inRadians, double yaw, double pitch, double roll, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.writeShort(323);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeBool(inRadians);
            b.writeDouble(yaw);
            b.writeDouble(pitch);
            b.writeDouble(roll);
            b.writeBool(doSilent);
            b.writeBool(doDirect);
            b = c.Send(b, false);return new PBAutoResult(){ code = 323,error = 0 };
        }

        public struct GetYawPitchRollResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public double yaw;
            public double pitch;
            public double roll;
        }
        public GetYawPitchRollResult GetYawPitchRoll(int siteId, int deviceId, bool inRadians)
        {
            var b = new ByteUtil();
            b.writeShort(324);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeBool(inRadians);
            b = c.Send(b, true);
            var r = new GetYawPitchRollResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 324)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.yaw = b.readDouble();
                r.pitch = b.readDouble();
                r.roll = b.readDouble();
            }
            return r;
        }

        public struct GetSiteIdsResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int[] siteIds;
        }
        public GetSiteIdsResult GetSiteIds()
        {
            var b = new ByteUtil();
            b.writeShort(317);
            b = c.Send(b, true);
            var r = new GetSiteIdsResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 317)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.siteIds = b.readIntBuffer();
            }
            return r;
        }

        public PBAutoResult SetCompositingPassRenderTargetSize(int siteId, int deviceId, int width, int height)
        {
            var b = new ByteUtil();
            b.writeShort(341);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(width);
            b.writeInt(height);
            b = c.Send(b, false);return new PBAutoResult(){ code = 341,error = 0 };
        }

        public PBAutoResult SetSoftedgeIsWarped(int siteId, int deviceId, bool isWarped)
        {
            var b = new ByteUtil();
            b.writeShort(342);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeBool(isWarped);
            b = c.Send(b, false);return new PBAutoResult(){ code = 342,error = 0 };
        }

        public PBAutoResult SetCanvasTextureFormatByDmxId(int canvasDmxFolderId, int canvasDmxFileId, int textureFormat)
        {
            var b = new ByteUtil();
            b.writeShort(338);
            b.writeInt(canvasDmxFolderId);
            b.writeInt(canvasDmxFileId);
            b.writeInt(textureFormat);
            b = c.Send(b, false);return new PBAutoResult(){ code = 338,error = 0 };
        }

        public PBAutoResult SetCanvasTextureFormatByPath(string canvasResourcePath, int textureFormat)
        {
            var b = new ByteUtil();
            b.writeShort(339);
            b.writeStringNarrow(canvasResourcePath);
            b.writeInt(textureFormat);
            b = c.Send(b, false);return new PBAutoResult(){ code = 339,error = 0 };
        }

        public PBAutoResult SetCanvasTextureFormatByItemId(int canvasItemIndex, int textureFormat)
        {
            var b = new ByteUtil();
            b.writeShort(340);
            b.writeInt(canvasItemIndex);
            b.writeInt(textureFormat);
            b = c.Send(b, false);return new PBAutoResult(){ code = 340,error = 0 };
        }

        public PBAutoResult ResetSockets()
        {
            var b = new ByteUtil();
            b.writeShort(354);
            b = c.Send(b, false);return new PBAutoResult(){ code = 354,error = 0 };
        }

        public PBAutoResult ResetSerialLink(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.writeShort(355);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 355,error = 0 };
        }

        public PBAutoResult AssignResourceToParamBlocked(int siteId, int deviceId, int dmxFolderId, int dmxFileId, bool forMesh, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(352);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(forMesh);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 352,error = 0 };
        }

        public PBAutoResult AssignResourceBlocked(int siteId, int deviceId, int dmxFolderId, int dmxFileId, bool forMesh)
        {
            var b = new ByteUtil();
            b.writeShort(353);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(dmxFolderId);
            b.writeInt(dmxFileId);
            b.writeBool(forMesh);
            b = c.Send(b, false);return new PBAutoResult(){ code = 353,error = 0 };
        }

        public PBAutoResult JumpToPlayListEntryByDmxId(bool forward, int playlistDmxFolderId, int playlistdmxFileId, int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(356);
            b.writeBool(forward);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 356,error = 0 };
        }

        public PBAutoResult JumpToPlayListEntryByPath(bool forward, string playlistPath, int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(357);
            b.writeBool(forward);
            b.writeStringNarrow(playlistPath);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 357,error = 0 };
        }

        public PBAutoResult JumpToPlayListEntryByItemId(bool forward, int playlistItemIndex, int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.writeShort(358);
            b.writeBool(forward);
            b.writeInt(playlistItemIndex);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeStringNarrow(parameterName);
            b = c.Send(b, false);return new PBAutoResult(){ code = 358,error = 0 };
        }

        public PBAutoResult SetMediaTransportMode(int siteId, int deviceId, int transportMode)
        {
            var b = new ByteUtil();
            b.writeShort(359);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(transportMode);
            b = c.Send(b, false);return new PBAutoResult(){ code = 359,error = 0 };
        }

        public PBAutoResult AssignDevice(int siteId, int deviceId, int sourceDeviceId)
        {
            var b = new ByteUtil();
            b.writeShort(281);
            b.writeInt(siteId);
            b.writeInt(deviceId);
            b.writeInt(sourceDeviceId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 281,error = 0 };
        }

        public PBAutoResult CreateCanvas(bool doSetDmxIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(236);
            b.writeBool(doSetDmxIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 236,error = 0 };
        }

        public PBAutoResult CreateCanvasByPath(string canvasResourcePath, bool doSetDmxIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(237);
            b.writeStringNarrow(canvasResourcePath);
            b.writeBool(doSetDmxIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 237,error = 0 };
        }

        public PBAutoResult CreateCanvasByItemId(int folderItemIndex, bool doSetDmxIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(238);
            b.writeInt(folderItemIndex);
            b.writeBool(doSetDmxIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 238,error = 0 };
        }

        public PBAutoResult RecordLiveInputStop()
        {
            var b = new ByteUtil();
            b.writeShort(224);
            b = c.Send(b, false);return new PBAutoResult(){ code = 224,error = 0 };
        }

        public PBAutoResult CreateCanvasByPathFromTemplate(string canvasResourcePath, string newResourceName, string cmd, bool setDims, int width, int height, bool doSetDmxIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(264);
            b.writeStringNarrow(canvasResourcePath);
            b.writeStringNarrow(newResourceName);
            b.writeStringNarrow(cmd);
            b.writeBool(setDims);
            b.writeInt(width);
            b.writeInt(height);
            b.writeBool(doSetDmxIds);
            b.writeInt(newDmxFolderId);
            b.writeInt(newdmxFileId);
            b = c.Send(b, false);return new PBAutoResult(){ code = 264,error = 0 };
        }

        public struct GetHostRevisionNumberResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int revision;
        }
        public GetHostRevisionNumberResult GetHostRevisionNumber()
        {
            var b = new ByteUtil();
            b.writeShort(334);
            b = c.Send(b, true);
            var r = new GetHostRevisionNumberResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 334)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.revision = b.readInt();
            }
            return r;
        }

        public struct GetTreeItemInfoResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int resourceType;
            public string resourcePath;
            public string folderPath;
        }
        public GetTreeItemInfoResult GetTreeItemInfo(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(151);
            b.writeInt(treeItemIndex);
            b = c.Send(b, true);
            var r = new GetTreeItemInfoResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 151)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.resourceType = b.readInt();
                r.resourcePath = b.readStringWide();
                r.folderPath = b.readStringWide();
            }
            return r;
        }

        public struct GetMediaInfoByTreeItemIndexResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int dmxFolderId;
            public int dmxFileId;
            public string resourceName;
            public string resourcePath;
            public string projectPath;
            public int width;
            public int height;
            public int fps;
            public int hours;
            public int minutes;
            public int seconds;
            public int frames;
            public int options;
        }
        public GetMediaInfoByTreeItemIndexResult GetMediaInfoByTreeItemIndex(int index)
        {
            var b = new ByteUtil();
            b.writeShort(152);
            b.writeInt(index);
            b = c.Send(b, true);
            var r = new GetMediaInfoByTreeItemIndexResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 152)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.dmxFolderId = b.readInt();
                r.dmxFileId = b.readInt();
                r.resourceName = b.readStringNarrow();
                r.resourcePath = b.readStringNarrow();
                r.projectPath = b.readStringNarrow();
                r.width = b.readInt();
                r.height = b.readInt();
                r.fps = b.readInt();
                r.hours = b.readInt();
                r.minutes = b.readInt();
                r.seconds = b.readInt();
                r.frames = b.readInt();
                r.options = b.readInt();
            }
            return r;
        }

        public struct GetPlaylistEntryByDmxIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int treeItemIndex;
            public string resourceName;
            public string resourcePath;
            public int durationHours;
            public int durationMinutes;
            public int durationSeconds;
            public int durationFrames;
            public int fadeOutHour;
            public int fadeOutMinute;
            public int fadeOutSecond;
            public int fadeOutFrame;
            public int startHour;
            public int startMinute;
            public int startSecond;
            public int startFrame;
            public int endHour;
            public int endMinute;
            public int endSecond;
            public int endFrame;
            public int fadeFxId;
        }
        public GetPlaylistEntryByDmxIdResult GetPlaylistEntryByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int playlistEntryIndex)
        {
            var b = new ByteUtil();
            b.writeShort(193);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b.writeInt(playlistEntryIndex);
            b = c.Send(b, true);
            var r = new GetPlaylistEntryByDmxIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 193)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.treeItemIndex = b.readInt();
                r.resourceName = b.readStringNarrow();
                r.resourcePath = b.readStringNarrow();
                r.durationHours = b.readInt();
                r.durationMinutes = b.readInt();
                r.durationSeconds = b.readInt();
                r.durationFrames = b.readInt();
                r.fadeOutHour = b.readInt();
                r.fadeOutMinute = b.readInt();
                r.fadeOutSecond = b.readInt();
                r.fadeOutFrame = b.readInt();
                r.startHour = b.readInt();
                r.startMinute = b.readInt();
                r.startSecond = b.readInt();
                r.startFrame = b.readInt();
                r.endHour = b.readInt();
                r.endMinute = b.readInt();
                r.endSecond = b.readInt();
                r.endFrame = b.readInt();
                r.fadeFxId = b.readInt();
            }
            return r;
        }

        public struct GetPlaylistEntryByPathResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int treeItemIndex;
            public string resourceName;
            public string resourcePath;
            public int durationHours;
            public int durationMinutes;
            public int durationSeconds;
            public int durationFrames;
            public int fadeOutHour;
            public int fadeOutMinute;
            public int fadeOutSecond;
            public int fadeOutFrame;
            public int startHour;
            public int startMinute;
            public int startSecond;
            public int startFrame;
            public int endHour;
            public int endMinute;
            public int endSecond;
            public int endFrame;
            public int fadeFxId;
        }
        public GetPlaylistEntryByPathResult GetPlaylistEntryByPath(string playlistPath, int playlistEntryIndex)
        {
            var b = new ByteUtil();
            b.writeShort(194);
            b.writeStringNarrow(playlistPath);
            b.writeInt(playlistEntryIndex);
            b = c.Send(b, true);
            var r = new GetPlaylistEntryByPathResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 194)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.treeItemIndex = b.readInt();
                r.resourceName = b.readStringNarrow();
                r.resourcePath = b.readStringNarrow();
                r.durationHours = b.readInt();
                r.durationMinutes = b.readInt();
                r.durationSeconds = b.readInt();
                r.durationFrames = b.readInt();
                r.fadeOutHour = b.readInt();
                r.fadeOutMinute = b.readInt();
                r.fadeOutSecond = b.readInt();
                r.fadeOutFrame = b.readInt();
                r.startHour = b.readInt();
                r.startMinute = b.readInt();
                r.startSecond = b.readInt();
                r.startFrame = b.readInt();
                r.endHour = b.readInt();
                r.endMinute = b.readInt();
                r.endSecond = b.readInt();
                r.endFrame = b.readInt();
                r.fadeFxId = b.readInt();
            }
            return r;
        }

        public struct GetPlaylistEntryByItemIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int treeItemIndex;
            public string resourceName;
            public string resourcePath;
            public int durationHours;
            public int durationMinutes;
            public int durationSeconds;
            public int durationFrames;
            public int fadeOutHour;
            public int fadeOutMinute;
            public int fadeOutSecond;
            public int fadeOutFrame;
            public int startHour;
            public int startMinute;
            public int startSecond;
            public int startFrame;
            public int endHour;
            public int endMinute;
            public int endSecond;
            public int endFrame;
            public int fadeFxId;
        }
        public GetPlaylistEntryByItemIdResult GetPlaylistEntryByItemId(int playlistItemIndex, int playlistEntryIndex)
        {
            var b = new ByteUtil();
            b.writeShort(195);
            b.writeInt(playlistItemIndex);
            b.writeInt(playlistEntryIndex);
            b = c.Send(b, true);
            var r = new GetPlaylistEntryByItemIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 195)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.treeItemIndex = b.readInt();
                r.resourceName = b.readStringNarrow();
                r.resourcePath = b.readStringNarrow();
                r.durationHours = b.readInt();
                r.durationMinutes = b.readInt();
                r.durationSeconds = b.readInt();
                r.durationFrames = b.readInt();
                r.fadeOutHour = b.readInt();
                r.fadeOutMinute = b.readInt();
                r.fadeOutSecond = b.readInt();
                r.fadeOutFrame = b.readInt();
                r.startHour = b.readInt();
                r.startMinute = b.readInt();
                r.startSecond = b.readInt();
                r.startFrame = b.readInt();
                r.endHour = b.readInt();
                r.endMinute = b.readInt();
                r.endSecond = b.readInt();
                r.endFrame = b.readInt();
                r.fadeFxId = b.readInt();
            }
            return r;
        }

        public struct GetPlaylistEntryIndicesByDmxIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int[] treeItemIds;
        }
        public GetPlaylistEntryIndicesByDmxIdResult GetPlaylistEntryIndicesByDmxId(int playlistDmxFolderId, int playlistdmxFileId)
        {
            var b = new ByteUtil();
            b.writeShort(196);
            b.writeInt(playlistDmxFolderId);
            b.writeInt(playlistdmxFileId);
            b = c.Send(b, true);
            var r = new GetPlaylistEntryIndicesByDmxIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 196)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.treeItemIds = b.readIntBuffer();
            }
            return r;
        }

        public struct GetPlaylistEntryIndicesByPathResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int[] treeItemIds;
        }
        public GetPlaylistEntryIndicesByPathResult GetPlaylistEntryIndicesByPath(string playlistPath)
        {
            var b = new ByteUtil();
            b.writeShort(197);
            b.writeStringNarrow(playlistPath);
            b = c.Send(b, true);
            var r = new GetPlaylistEntryIndicesByPathResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 197)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.treeItemIds = b.readIntBuffer();
            }
            return r;
        }

        public struct GetPlaylistEntryIndicesByItemIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int[] treeItemIds;
        }
        public GetPlaylistEntryIndicesByItemIdResult GetPlaylistEntryIndicesByItemId(int playlistItemIndex)
        {
            var b = new ByteUtil();
            b.writeShort(198);
            b.writeInt(playlistItemIndex);
            b = c.Send(b, true);
            var r = new GetPlaylistEntryIndicesByItemIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 198)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.treeItemIds = b.readIntBuffer();
            }
            return r;
        }

        public struct CreateGraphicLayerGetIdResult
        {
            public bool ok { get { return error == 0; } }
            public short code; public int error;
            public int layerId;
        }
        public CreateGraphicLayerGetIdResult CreateGraphicLayerGetId(int siteId, bool isGraphicLayer)
        {
            var b = new ByteUtil();
            b.writeShort(96);
            b.writeInt(siteId);
            b.writeBool(isGraphicLayer);
            b = c.Send(b, true);
            var r = new CreateGraphicLayerGetIdResult();
            r.code = b.readShort();
            if(r.code < 0)
            {
                r.error = b.readInt();
            }
            else if(r.code != 96)
            {
            	r.code = -1;
            	r.error = 7; // WrongMessageReturned
            	return r;
            }
            else
            {
                r.error = 0;
                r.layerId = b.readInt();
            }
            return r;
        }
    }

    /// <summary>
    /// Contains extension methods for conversion between native format and byte arrays
    /// </summary>
    public static class PBUtil
    {
        public static byte PBAutoChecksum(this byte[] message)
        {
            if (message.Length < 17) throw new ArgumentException("Byte array is not a PBAuto header! Length != 17");
            var checksum = 0;
            for(int i=4;i<16;i++)
            {
                checksum += message[i];
            }
            return (byte)(checksum % 255);
        }
        public static long GetInt64(this byte[] bytes, int offset = 0)
        {
            byte[] value_bytes = new byte[8];
            Array.Copy(bytes, offset, value_bytes, 0, 8);
            if (BitConverter.IsLittleEndian) { Array.Reverse(value_bytes); }
            return BitConverter.ToInt64(value_bytes, 0);
        }

        public static int GetInt32(this byte[] bytes, int offset = 0)
        {
            byte[] value_bytes = new byte[4];
            Array.Copy(bytes, offset, value_bytes, 0, 4);
            if (BitConverter.IsLittleEndian) { Array.Reverse(value_bytes); }
            return BitConverter.ToInt32(value_bytes, 0);
        }

        public static short GetInt16(this byte[] bytes, int offset = 0)
        {
            byte[] value_bytes = new byte[2];
            Array.Copy(bytes, offset, value_bytes, 0, 2);
            if (BitConverter.IsLittleEndian) { Array.Reverse(value_bytes); }
            return BitConverter.ToInt16(value_bytes, 0);
        }

        public static byte[] GetBytesNetworkOrder(this Int64 value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) { Array.Reverse(bytes); }
            return bytes;
        }
        public static byte[] GetBytesNetworkOrder(this int value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) { Array.Reverse(bytes); }
            return bytes;
        }

        public static byte[] GetBytesNetworkOrder(this short value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) { Array.Reverse(bytes); }
            return bytes;
        }
    }

    /// <summary>
    /// Utility class for byte conversion
    /// </summary>
    public class ByteUtil
    {
        // Holds the bytes
        private List<byte> list_bytes;
        private byte[] read_bytes;
        
        // Position for reading
        private int position = 0;

        // Constructors
        public ByteUtil()
        {
            list_bytes = new List<byte>();
        }
        public ByteUtil(byte[] data)
        {
            read_bytes = data;
        }

        public void CopyTo(byte[] bytes, int offset) { list_bytes.CopyTo(bytes, offset); }
        public int Length { get { return list_bytes.Count; } }

        // Writing
        public void writeBool(bool value) { list_bytes.Add((byte)(value ? 1 : 0)); }
        public void writeByte(byte value) { list_bytes.Add(value); }
        public void writeShort(short value) { list_bytes.AddRange(value.GetBytesNetworkOrder() ); }
        public void writeInt(int value) { list_bytes.AddRange(value.GetBytesNetworkOrder()); }
        public void writeInt64(long value) { list_bytes.AddRange(value.GetBytesNetworkOrder()); }
        public void writeDouble(double value) { list_bytes.AddRange(BitConverter.GetBytes(value)); }
        public void writeStringNarrow(string value) { writeShort((short)value.Length); list_bytes.AddRange(Encoding.UTF8.GetBytes(value)); }
        public void writeStringWide(string value) { writeShort((short)value.Length); list_bytes.AddRange(Encoding.BigEndianUnicode.GetBytes(value)); }
        public void writeByteBuffer(byte[] value) { writeInt(value.Length); list_bytes.AddRange(value); }
        public void writeIntBuffer(int[] value) { foreach (var i in value) { list_bytes.AddRange(i.GetBytesNetworkOrder()); } }

        // Reading
        private byte[] _readBlock(int length) { var ret = new byte[length]; Array.Copy(read_bytes, position, ret, 0, length);position += length;return ret; }
        public bool readBool() { var result = read_bytes[position];position++;return result == 1; }
        public byte readByte() { var result = read_bytes[position];position++;return result; }
        public short readShort() { return _readBlock(2).GetInt16(); }
        public int readInt() { return _readBlock(4).GetInt32(); }
        public long readInt64() { return _readBlock(8).GetInt64(); }
        public double readDouble() { return BitConverter.ToDouble(_readBlock(8), 0); }
        public string readStringNarrow() { int length = readShort(); return Encoding.UTF8.GetString(_readBlock(length)); }
        public string readStringWide() { int length = readShort(); return Encoding.BigEndianUnicode.GetString(_readBlock(length * 2)); }
        public byte[] readByteBuffer() { int length = readInt(); return _readBlock(length); }
        public int[] readIntBuffer() { int length = (read_bytes.Length - position) / 4; int[] result = new int[length]; for (int i = 0;i < length; i++) { result[i] = _readBlock(4).GetInt32(); }; return result; }
    }

    /// <summary>
    /// Interface that allows PBAuto to transmit messages
    /// </summary>
    public interface Connector
    {
        ByteUtil Send(ByteUtil data, bool has_reponse);
    }

    /// <summary>
    /// Implements the Connector interface using TCP as the underlying protocol
    /// </summary>
    public class TCP : Connector
    {
        private string ip;
        private int domain;
        private TcpClient client;
        private object sendLock = new object();
        private const int PORT = 6211;

        public TCP(string ip, int domain = 0)
        {
            this.ip = ip;
            this.domain = domain;
            client = new TcpClient();
            client.NoDelay = true;
            client.Connect(System.Net.IPAddress.Parse(ip), PORT);
        }

        public ByteUtil Send(ByteUtil data, bool has_response)
        {
            byte[] header = new byte[17] {
                (byte)'P', (byte)'B', (byte)'A', (byte)'U', //# header consists of magic "PBAU" sequence
                1,                                          //# + protocol version (byte, currently 1)
                0, 0, 0, 0,                                 //# + domain id (integer)
                0, 0,                                       //# + message size (short)
                0, 0, 0, 0,                                 //# + connection id (int, user definable, defaults to 0)
                0,                                          //# + protocol flag (byte, 0 for TCP)
                0,                                          //# + checksum
            };

            // write domain id to header
            domain.GetBytesNetworkOrder().CopyTo(header, 5);
            // write message length
            ((short)data.Length).GetBytesNetworkOrder().CopyTo(header, 9);
            // calculate checksum and write
            header[16] = header.PBAutoChecksum();

            var message = new byte[17 + data.Length];
            header.CopyTo(message, 0);
            data.CopyTo(message, 17);

            lock(sendLock)
            {
                var stream = client.GetStream();
                stream.Write(message, 0, message.Length);
                stream.Flush();

                if( !has_response )
                {
                    return null;
                }

                int bytesread = 0;
                while(bytesread < 17)
                {
                    bytesread += stream.Read(header, bytesread, 17 - bytesread);
                }

                if(header[0] != 0x50 || header[1] != 0x42 || header[2] != 0x41 || header[3] != 0x55 || header.PBAutoChecksum() != header[16])
                {
                    // Not a PB Header or checksum fail
                    return new ByteUtil(new byte[]{ 255, 255, 0, 0, 0, 7});
                }

                int message_length = header.GetInt16(9);
                message = new byte[message_length];

                bytesread = 0;
                while (bytesread < message_length)
                {
                    bytesread += stream.Read(message, bytesread, message_length - bytesread);
                }
            }

            return new ByteUtil(message);
        }
    }
}