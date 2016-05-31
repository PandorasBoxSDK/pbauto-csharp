/* Pandoras Box Automation TESTING - pbauto-csharp v0.0.13077 @2016-05-31 <support@coolux.de> */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;


namespace PandorasBox
{
    /// <summary>
    /// The main class used to communicate with Pandoras Box
    /// </summary>
    public class PbAuto
    {
        private IConnector connector;

        public PbAuto(IConnector connector)
        {
            this.connector = connector;
        }

        public static PbAuto ConnectTcp(string ip, int domain = 0)
        {
            return new PbAuto(new TcpConnector(ip, domain));
        }

        public bool IsConnected
        {
            get
            {
                return connector.IsConnected();
            }
        }

        public struct PbAutoResult {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
        }


        public PbAutoResult SetParamInt(int siteId, int deviceId, string parameterName, int parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.WriteShort(1);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteInt( (int)parameterValue);
            b.WriteBool( (bool)doSilent);
            b.WriteBool( (bool)doDirect);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamDouble(int siteId, int deviceId, string parameterName, double parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.WriteShort(84);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteDouble( (double)parameterValue);
            b.WriteBool( (bool)doSilent);
            b.WriteBool( (bool)doDirect);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamByteTuples(int siteId, int deviceId, string parameterName, int tupleDimension, byte[] tupleData, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.WriteShort(115);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteInt( (int)tupleDimension);
            b.WriteByteBuffer( (byte[])tupleData);
            b.WriteBool( (bool)doSilent);
            b.WriteBool( (bool)doDirect);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetParamResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public double ParameterValue;
        }
        public GetParamResult GetParam(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(79);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, true);
            var r = new GetParamResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.ParameterValue = b.ReadDouble();
            }
            return r;
        }

        public struct GetParamByteTuplesResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int TupleDimension;
            public byte[] TupleData;
        }
        public GetParamByteTuplesResult GetParamByteTuples(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(132);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringWide( (string)parameterName);
            b = connector.Send(b, true);
            var r = new GetParamByteTuplesResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TupleDimension = b.ReadInt();
                r.TupleData = b.ReadByteBuffer();
            }
            return r;
        }

        public PbAutoResult SetParamOfKind(int siteId, int deviceId, int parameterKindId, int parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.WriteShort(39);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)parameterKindId);
            b.WriteInt( (int)parameterValue);
            b.WriteBool( (bool)doSilent);
            b.WriteBool( (bool)doDirect);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamOfKindDouble(int siteId, int deviceId, int parameterKindId, double parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.WriteShort(85);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)parameterKindId);
            b.WriteDouble( (double)parameterValue);
            b.WriteBool( (bool)doSilent);
            b.WriteBool( (bool)doDirect);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetParamOfKindResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public double ParameterValue;
        }
        public GetParamOfKindResult GetParamOfKind(int siteId, int deviceId, int parameterKindId)
        {
            var b = new ByteUtil();
            b.WriteShort(80);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)parameterKindId);
            b = connector.Send(b, true);
            var r = new GetParamOfKindResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.ParameterValue = b.ReadDouble();
            }
            return r;
        }

        public PbAutoResult SetParamInSelection(string parameterName, int parameterValue)
        {
            var b = new ByteUtil();
            b.WriteShort(58);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteInt( (int)parameterValue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamInSelectionDouble(string parameterName, double parameterValue)
        {
            var b = new ByteUtil();
            b.WriteShort(99);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteDouble( (double)parameterValue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamOfKindInSelection(int parameterKindId, int parameterValue)
        {
            var b = new ByteUtil();
            b.WriteShort(59);
            b.WriteInt( (int)parameterKindId);
            b.WriteInt( (int)parameterValue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamOfKindInSelectionDouble(int parameterKindId, double parameterValue)
        {
            var b = new ByteUtil();
            b.WriteShort(100);
            b.WriteInt( (int)parameterKindId);
            b.WriteDouble( (double)parameterValue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamLerpTime(int siteId, int deviceId, string parameterName, int smoothingTime)
        {
            var b = new ByteUtil();
            b.WriteShort(232);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteInt( (int)smoothingTime);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetIsDeviceSelectedResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public byte IsSelected;
        }
        public GetIsDeviceSelectedResult GetIsDeviceSelected(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(74);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, true);
            var r = new GetIsDeviceSelectedResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsSelected = b.ReadByte();
            }
            return r;
        }

        public struct GetSelectedDeviceCountResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int SelectedDevicesCount;
        }
        public GetSelectedDeviceCountResult GetSelectedDeviceCount()
        {
            var b = new ByteUtil();
            b.WriteShort(81);
            b = connector.Send(b, true);
            var r = new GetSelectedDeviceCountResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.SelectedDevicesCount = b.ReadInt();
            }
            return r;
        }

        public struct GetSelectedDeviceResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int SiteId;
            public int DeviceId;
        }
        public GetSelectedDeviceResult GetSelectedDevice(int selectionIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(75);
            b.WriteInt( (int)selectionIndex);
            b = connector.Send(b, true);
            var r = new GetSelectedDeviceResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.SiteId = b.ReadInt();
                r.DeviceId = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult SetSequenceMediaAtTime(int siteId, int deviceId, int sequenceId, int hours, int minutes, int seconds, int frames, int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(56);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AssignResource(int siteId, int deviceId, int dmxFolderId, int dmxFileId, bool forMesh)
        {
            var b = new ByteUtil();
            b.WriteShort(2);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)forMesh);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AssignResourceByName(int siteId, int deviceId, string resourcePath, string parameterName, bool forMesh)
        {
            var b = new ByteUtil();
            b.WriteShort(129);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringWide( (string)resourcePath);
            b.WriteStringWide( (string)parameterName);
            b.WriteBool( (bool)forMesh);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AssignResourceToSelection(int dmxFolderId, int dmxFileId, bool forMesh)
        {
            var b = new ByteUtil();
            b.WriteShort(61);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)forMesh);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult MoveResourceToPath(string resourcePath, string projectPath)
        {
            var b = new ByteUtil();
            b.WriteShort(144);
            b.WriteStringWide( (string)resourcePath);
            b.WriteStringWide( (string)projectPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult MoveTreeItem(int itemIdFrom, int itemIdTo)
        {
            var b = new ByteUtil();
            b.WriteShort(158);
            b.WriteInt( (int)itemIdFrom);
            b.WriteInt( (int)itemIdTo);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSequenceTransportMode(int sequenceId, int transportMode)
        {
            var b = new ByteUtil();
            b.WriteShort(3);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)transportMode);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetSequenceTransportModeResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int TransportMode;
        }
        public GetSequenceTransportModeResult GetSequenceTransportMode(int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(72);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, true);
            var r = new GetSequenceTransportModeResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TransportMode = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult MoveSequenceToTime(int sequenceId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(5);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetSequenceTimeResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Hours;
            public int Minutes;
            public int Seconds;
            public int Frames;
        }
        public GetSequenceTimeResult GetSequenceTime(int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(73);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, true);
            var r = new GetSequenceTimeResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Hours = b.ReadInt();
                r.Minutes = b.ReadInt();
                r.Seconds = b.ReadInt();
                r.Frames = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult MoveSequenceToNextFrame(int sequenceId, byte isNext)
        {
            var b = new ByteUtil();
            b.WriteShort(6);
            b.WriteInt( (int)sequenceId);
            b.WriteByte( (byte)isNext);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult MoveSequenceToCue(int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.WriteShort(4);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult MoveSequenceToNextCue(int sequenceId, byte isNext)
        {
            var b = new ByteUtil();
            b.WriteShort(7);
            b.WriteInt( (int)sequenceId);
            b.WriteByte( (byte)isNext);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSequenceTransparency(int sequenceId, int transparency)
        {
            var b = new ByteUtil();
            b.WriteShort(8);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)transparency);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetSequenceTransparencyResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Transparency;
        }
        public GetSequenceTransparencyResult GetSequenceTransparency(int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(91);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, true);
            var r = new GetSequenceTransparencyResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Transparency = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult SetSequenceSMTPETimeCodeMode(int sequenceId, int timeCodeMode)
        {
            var b = new ByteUtil();
            b.WriteShort(41);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)timeCodeMode);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSequenceSMTPETimeCodeOffset(int sequenceId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(42);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSequenceSMTPETimeCodeStopAction(int sequenceId, int stopAction)
        {
            var b = new ByteUtil();
            b.WriteShort(43);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)stopAction);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ResetAll()
        {
            var b = new ByteUtil();
            b.WriteShort(9);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ResetSite(int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(10);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ResetDevice(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(11);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ResetParam(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(12);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetAllActive()
        {
            var b = new ByteUtil();
            b.WriteShort(35);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSiteActive(int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(36);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetDeviceActive(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(37);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamActive(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(38);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearAllActive()
        {
            var b = new ByteUtil();
            b.WriteShort(13);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearActiveSite(int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(14);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearActiveDevice(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(15);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearActiveParam(int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(16);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ToggleFullscreen(int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(17);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamRelativeDouble(int siteId, int deviceId, string parameterName, double parameterValue)
        {
            var b = new ByteUtil();
            b.WriteShort(98);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteDouble( (double)parameterValue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamRelativeExtended(int siteId, int deviceId, string parameterName, double parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.WriteShort(149);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteDouble( (double)parameterValue);
            b.WriteBool( (bool)doSilent);
            b.WriteBool( (bool)doDirect);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamRelativeInSelection(string parameterName, int parameterValue)
        {
            var b = new ByteUtil();
            b.WriteShort(60);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteInt( (int)parameterValue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamRelativeInSelectionDouble(string parameterName, double parameterValue)
        {
            var b = new ByteUtil();
            b.WriteShort(101);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteDouble( (double)parameterValue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContentToPath(string filePath, int siteId, int dmxFolderId, int dmxFileId, string projectPath)
        {
            var b = new ByteUtil();
            b.WriteShort(87);
            b.WriteStringNarrow( (string)filePath);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringNarrow( (string)projectPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContentToTreeItem(string filePath, int siteId, int dmxFolderId, int dmxFileId, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(153);
            b.WriteStringNarrow( (string)filePath);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContentFromLocalNode(string filePath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(63);
            b.WriteStringNarrow( (string)filePath);
            b.WriteShort( (short)dmxFolderId);
            b.WriteShort( (short)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContentFromLocalNodeToPath(string filePath, string projectPath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(135);
            b.WriteStringNarrow( (string)filePath);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteShort( (short)dmxFolderId);
            b.WriteShort( (short)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContentFromLocalNodeToTreeItem(string filePath, int treeItemIndex, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(154);
            b.WriteStringNarrow( (string)filePath);
            b.WriteInt( (int)treeItemIndex);
            b.WriteShort( (short)dmxFolderId);
            b.WriteShort( (short)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContentFromFolder(string folderPath, int siteId, int dmxFolderId, int dmxFileId, string projectPath)
        {
            var b = new ByteUtil();
            b.WriteShort(124);
            b.WriteStringWide( (string)folderPath);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringWide( (string)projectPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContentFromLocalNodeFolder(string folderPath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(133);
            b.WriteStringWide( (string)folderPath);
            b.WriteShort( (short)dmxFolderId);
            b.WriteShort( (short)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContentFromLocalNodeFolderToPath(string folderPath, string projectPath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(134);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteShort( (short)dmxFolderId);
            b.WriteShort( (short)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContentFolderFromLocalNodeToTreeItem(string folderPath, int treeItemIndex, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(155);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)treeItemIndex);
            b.WriteShort( (short)dmxFolderId);
            b.WriteShort( (short)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveMediaById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(20);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveMeshById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(21);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveContentByName(string resourcePath, bool allEquallyNamed)
        {
            var b = new ByteUtil();
            b.WriteShort(125);
            b.WriteStringWide( (string)resourcePath);
            b.WriteBool( (bool)allEquallyNamed);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveTreeItem(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(156);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveAllResources(bool removeFolder)
        {
            var b = new ByteUtil();
            b.WriteShort(126);
            b.WriteBool( (bool)removeFolder);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetContentId(string resourcePath, short dmxFolderId, short dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(234);
            b.WriteStringWide( (string)resourcePath);
            b.WriteShort( (short)dmxFolderId);
            b.WriteShort( (short)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SpreadAll()
        {
            var b = new ByteUtil();
            b.WriteShort(22);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SpreadMediaById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(23);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SpreadMeshById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(24);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ReloadMediaById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(44);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ReloadMeshById(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(45);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ReloadResource(string resourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(147);
            b.WriteStringWide( (string)resourcePath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SpreadResource(string resourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(148);
            b.WriteStringWide( (string)resourcePath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ReloadAndSpreadResourceByPath(string resourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(159);
            b.WriteStringWide( (string)resourcePath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ReloadAndSpreadResourceByTreeItem(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(160);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ReloadAndSpreadResourceByDmxId(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(161);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveInconsistent()
        {
            var b = new ByteUtil();
            b.WriteShort(34);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveAssetOnSite(string resourcePath, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(170);
            b.WriteStringWide( (string)resourcePath);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveAssetOnSiteById(int dmxFolderId, int dmxFileId, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(171);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveAssetOnSiteByTreeItem(int treeItemIndex, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(172);
            b.WriteInt( (int)treeItemIndex);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AttachAssetOnSite(string filePath, string resourcePath, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(173);
            b.WriteStringWide( (string)filePath);
            b.WriteStringWide( (string)resourcePath);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AttachAssetOnSiteByDmxId(string filePath, int dmxFolderId, int dmxFileId, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(174);
            b.WriteStringWide( (string)filePath);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AttachAssetOnSiteByTreeItem(string filePath, int treeItemIndex, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(175);
            b.WriteStringWide( (string)filePath);
            b.WriteInt( (int)treeItemIndex);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult StoreActive(int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(25);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult StoreActiveToTime(int sequenceId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(26);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetMediaFrameBlendingById(int dmxFolderId, int dmxFileId, bool frameBlended)
        {
            var b = new ByteUtil();
            b.WriteShort(27);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)frameBlended);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetMediaDeinterlacingById(int dmxFolderId, int dmxFileId, int deinterlacer)
        {
            var b = new ByteUtil();
            b.WriteShort(28);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteInt( (int)deinterlacer);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetMediaAnisotropicFilteringById(int dmxFolderId, int dmxFileId, bool useFiltering)
        {
            var b = new ByteUtil();
            b.WriteShort(29);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)useFiltering);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetMediaUnderscanById(int dmxFolderId, int dmxFileId, bool useUnderscan)
        {
            var b = new ByteUtil();
            b.WriteShort(30);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)useUnderscan);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetMediaMpegColourSpaceById(int dmxFolderId, int dmxFileId, bool useMpegColorSpace)
        {
            var b = new ByteUtil();
            b.WriteShort(31);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)useMpegColorSpace);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetMediaAlphaChannelById(int dmxFolderId, int dmxFileId, bool useAlphaChannel)
        {
            var b = new ByteUtil();
            b.WriteShort(32);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)useAlphaChannel);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreateTextInput(int dmxFolderId, int dmxFileId, string text)
        {
            var b = new ByteUtil();
            b.WriteShort(52);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringNarrow( (string)text);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetText(int dmxFolderId, int dmxFileId, string text)
        {
            var b = new ByteUtil();
            b.WriteShort(33);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringNarrow( (string)text);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult LoadProject(string folderPathToProject, string projectXmlFileName, byte saveExisting)
        {
            var b = new ByteUtil();
            b.WriteShort(46);
            b.WriteStringNarrow( (string)folderPathToProject);
            b.WriteStringNarrow( (string)projectXmlFileName);
            b.WriteByte( (byte)saveExisting);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CloseProject(byte save)
        {
            var b = new ByteUtil();
            b.WriteShort(47);
            b.WriteByte( (byte)save);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearSelection()
        {
            var b = new ByteUtil();
            b.WriteShort(48);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetDeviceAcceptDmxById(int siteId, int deviceId, byte acceptDmx)
        {
            var b = new ByteUtil();
            b.WriteShort(49);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteByte( (byte)acceptDmx);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSiteAcceptDmxById(int siteId, byte acceptDmx)
        {
            var b = new ByteUtil();
            b.WriteShort(50);
            b.WriteInt( (int)siteId);
            b.WriteByte( (byte)acceptDmx);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetDeviceDmxAddressById(int siteId, int deviceId, int index, int id1, int id2)
        {
            var b = new ByteUtil();
            b.WriteShort(51);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)id1);
            b.WriteInt( (int)id2);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSiteDmxAddressById(int siteId, int index, int id1, int id2)
        {
            var b = new ByteUtil();
            b.WriteShort(235);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)id1);
            b.WriteInt( (int)id2);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetCuePlayMode(int sequenceId, int cueId, int playMode)
        {
            var b = new ByteUtil();
            b.WriteShort(53);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b.WriteInt( (int)playMode);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetNextCuePlayMode(int sequenceId, int playMode)
        {
            var b = new ByteUtil();
            b.WriteShort(54);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)playMode);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetIgnoreNextCue(int sequenceId, byte doIgnore)
        {
            var b = new ByteUtil();
            b.WriteShort(55);
            b.WriteInt( (int)sequenceId);
            b.WriteByte( (byte)doIgnore);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SaveProject()
        {
            var b = new ByteUtil();
            b.WriteShort(62);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetIsSiteFullscreen(int siteId, byte isFullscreen)
        {
            var b = new ByteUtil();
            b.WriteShort(64);
            b.WriteInt( (int)siteId);
            b.WriteByte( (byte)isFullscreen);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetIsSiteFullscreenByIp(string ipAddress, byte isFullscreen)
        {
            var b = new ByteUtil();
            b.WriteShort(65);
            b.WriteStringNarrow( (string)ipAddress);
            b.WriteByte( (byte)isFullscreen);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetTextTextureSize(int dmxFolderId, int dmxFileId, int width, int height)
        {
            var b = new ByteUtil();
            b.WriteShort(66);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteInt( (int)width);
            b.WriteInt( (int)height);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetTextProperties(int dmxFolderId, int dmxFileId, string fontFamily, int size, byte style, byte alignment, byte colorRed, byte colorGreen, byte colorBlue)
        {
            var b = new ByteUtil();
            b.WriteShort(67);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringNarrow( (string)fontFamily);
            b.WriteInt( (int)size);
            b.WriteByte( (byte)style);
            b.WriteByte( (byte)alignment);
            b.WriteByte( (byte)colorRed);
            b.WriteByte( (byte)colorGreen);
            b.WriteByte( (byte)colorBlue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetTextCenterOnTexture(int dmxFolderId, int dmxFileId, byte centerOnTexture)
        {
            var b = new ByteUtil();
            b.WriteShort(68);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteByte( (byte)centerOnTexture);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreateTextInputWide(int dmxFolderId, int dmxFileId, string text)
        {
            var b = new ByteUtil();
            b.WriteShort(69);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringWide( (string)text);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetTextWide(int dmxFolderId, int dmxFileId, string text)
        {
            var b = new ByteUtil();
            b.WriteShort(70);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringWide( (string)text);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSiteIpById(int siteId, string ip)
        {
            var b = new ByteUtil();
            b.WriteShort(71);
            b.WriteInt( (int)siteId);
            b.WriteStringNarrow( (string)ip);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetClipRemainingTimeResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Hours;
            public int Minutes;
            public int Seconds;
            public int Frames;
        }
        public GetClipRemainingTimeResult GetClipRemainingTime(int siteId, int deviceId, int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(77);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, true);
            var r = new GetClipRemainingTimeResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Hours = b.ReadInt();
                r.Minutes = b.ReadInt();
                r.Seconds = b.ReadInt();
                r.Frames = b.ReadInt();
            }
            return r;
        }

        public struct GetRemainingTimeUntilNextCueResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Hours;
            public int Minutes;
            public int Seconds;
            public int Frames;
        }
        public GetRemainingTimeUntilNextCueResult GetRemainingTimeUntilNextCue(int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(78);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, true);
            var r = new GetRemainingTimeUntilNextCueResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Hours = b.ReadInt();
                r.Minutes = b.ReadInt();
                r.Seconds = b.ReadInt();
                r.Frames = b.ReadInt();
            }
            return r;
        }

        public struct GetResourceCountResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int MediaCount;
        }
        public GetResourceCountResult GetResourceCount()
        {
            var b = new ByteUtil();
            b.WriteShort(82);
            b = connector.Send(b, true);
            var r = new GetResourceCountResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.MediaCount = b.ReadInt();
            }
            return r;
        }

        public struct GetTreeItemCountResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int TreeItemCount;
        }
        public GetTreeItemCountResult GetTreeItemCount()
        {
            var b = new ByteUtil();
            b.WriteShort(150);
            b = connector.Send(b, true);
            var r = new GetTreeItemCountResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TreeItemCount = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult CreateProjectFolder(string folderName)
        {
            var b = new ByteUtil();
            b.WriteShort(83);
            b.WriteStringWide( (string)folderName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreateProjectFolderInPath(string folderName, string projectPath)
        {
            var b = new ByteUtil();
            b.WriteShort(122);
            b.WriteStringWide( (string)folderName);
            b.WriteStringWide( (string)projectPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreateProjectFolderInTreeItem(string folderName, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(157);
            b.WriteStringWide( (string)folderName);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveFolderFromProject(string projectPath)
        {
            var b = new ByteUtil();
            b.WriteShort(123);
            b.WriteStringWide( (string)projectPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetDeviceSelection(int siteId, int deviceId, int selectionMode)
        {
            var b = new ByteUtil();
            b.WriteShort(86);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)selectionMode);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetClxControllerFaderMapping(int faderId, int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(90);
            b.WriteInt( (int)faderId);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetClxControllerCueMapping(int cueBtnId, int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.WriteShort(92);
            b.WriteInt( (int)cueBtnId);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreateCue(int sequenceId, int cueId, int hours, int minutes, int seconds, int frames, string cueName, int cueKindId)
        {
            var b = new ByteUtil();
            b.WriteShort(93);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b.WriteStringWide( (string)cueName);
            b.WriteInt( (int)cueKindId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveCueById(int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.WriteShort(94);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemoveAllCues(int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(95);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct CreateVideoLayerGetIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int LayerId;
        }
        public CreateVideoLayerGetIdResult CreateVideoLayerGetId(int siteId, bool isGraphicLayer)
        {
            var b = new ByteUtil();
            b.WriteShort(110);
            b.WriteInt( (int)siteId);
            b.WriteBool( (bool)isGraphicLayer);
            b = connector.Send(b, true);
            var r = new CreateVideoLayerGetIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.LayerId = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult RemoveLayer(int siteId, int layerId, bool isGraphicLayer)
        {
            var b = new ByteUtil();
            b.WriteShort(97);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)layerId);
            b.WriteBool( (bool)isGraphicLayer);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetIsBackup(bool enable)
        {
            var b = new ByteUtil();
            b.WriteShort(102);
            b.WriteBool( (bool)enable);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ApplyView(int viewId)
        {
            var b = new ByteUtil();
            b.WriteShort(103);
            b.WriteInt( (int)viewId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSpareFromSpread(int siteId, bool spareFromSpread)
        {
            var b = new ByteUtil();
            b.WriteShort(104);
            b.WriteInt( (int)siteId);
            b.WriteBool( (bool)spareFromSpread);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetParamResourceResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int DmxFolderId;
            public int DmxFileId;
            public string FilePath;
            public string ResourcePath;
        }
        public GetParamResourceResult GetParamResource(int siteId, int deviceId, bool isMedia, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(105);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteBool( (bool)isMedia);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, true);
            var r = new GetParamResourceResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.DmxFolderId = b.ReadInt();
                r.DmxFileId = b.ReadInt();
                r.FilePath = b.ReadStringNarrow();
                r.ResourcePath = b.ReadStringNarrow();
            }
            return r;
        }

        public struct GetMediaTransportModeResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int TransportMode;
        }
        public GetMediaTransportModeResult GetMediaTransportMode(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(108);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, true);
            var r = new GetMediaTransportModeResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TransportMode = b.ReadInt();
            }
            return r;
        }

        public struct GetIsSiteConnectedResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public bool IsConnected;
        }
        public GetIsSiteConnectedResult GetIsSiteConnected(int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(109);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, true);
            var r = new GetIsSiteConnectedResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsConnected = b.ReadBool();
            }
            return r;
        }

        public PbAutoResult MoveLayerUp(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(111);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult MoveLayerDown(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(112);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult MoveLayerToFirstPosition(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(113);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult MoveLayerToLastPosition(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(114);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetEnableClxController(byte forJogShuttle, bool enable)
        {
            var b = new ByteUtil();
            b.WriteShort(117);
            b.WriteByte( (byte)forJogShuttle);
            b.WriteBool( (bool)enable);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetEnableClxControllerResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public bool IsEnabled;
        }
        public GetEnableClxControllerResult GetEnableClxController(byte forJogShuttle)
        {
            var b = new ByteUtil();
            b.WriteShort(116);
            b.WriteByte( (byte)forJogShuttle);
            b = connector.Send(b, true);
            var r = new GetEnableClxControllerResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsEnabled = b.ReadBool();
            }
            return r;
        }

        public PbAutoResult SetSequenceCueWaitTime(int sequenceId, int cueId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(118);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSequenceCueJumpTargetTime(int sequenceId, int cueId, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(119);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetCueJumpCount(int sequenceId, int cueId, int jumpCount)
        {
            var b = new ByteUtil();
            b.WriteShort(120);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b.WriteInt( (int)jumpCount);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ResetCueTriggerCount(int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.WriteShort(121);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetIsContentConsistentResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int IsContentInconsistent;
        }
        public GetIsContentConsistentResult GetIsContentConsistent(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(127);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, true);
            var r = new GetIsContentConsistentResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsContentInconsistent = b.ReadInt();
            }
            return r;
        }

        public struct GetIsContentConsistentByNameResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int IsContentInconsistent;
        }
        public GetIsContentConsistentByNameResult GetIsContentConsistentByName(string resourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(128);
            b.WriteStringWide( (string)resourcePath);
            b = connector.Send(b, true);
            var r = new GetIsContentConsistentByNameResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsContentInconsistent = b.ReadInt();
            }
            return r;
        }

        public struct CreateSequenceGetIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int SequenceId;
        }
        public CreateSequenceGetIdResult CreateSequenceGetId()
        {
            var b = new ByteUtil();
            b.WriteShort(130);
            b = connector.Send(b, true);
            var r = new CreateSequenceGetIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.SequenceId = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult RemoveSequence(int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(131);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SendMouseInput(int siteId, int deviceId, int mouseEventType, int screenPosX, int screenPosY, bool firstPass)
        {
            var b = new ByteUtil();
            b.WriteShort(136);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)mouseEventType);
            b.WriteInt( (int)screenPosX);
            b.WriteInt( (int)screenPosY);
            b.WriteBool( (bool)firstPass);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SendMouseScroll(int siteId, int deviceId, int scrollValue)
        {
            var b = new ByteUtil();
            b.WriteShort(233);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)scrollValue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SendTouchInput(int siteId, int deviceId, int touchId, int touchType, int screenPosX, int screenPosY, bool firstPass)
        {
            var b = new ByteUtil();
            b.WriteShort(146);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)touchId);
            b.WriteInt( (int)touchType);
            b.WriteInt( (int)screenPosX);
            b.WriteInt( (int)screenPosY);
            b.WriteBool( (bool)firstPass);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SendKeyboardInput(int siteId, int keyboardEventType, int keyCode)
        {
            var b = new ByteUtil();
            b.WriteShort(137);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)keyboardEventType);
            b.WriteInt( (int)keyCode);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetShowCursorInFullscreen(int siteId, bool showCursor)
        {
            var b = new ByteUtil();
            b.WriteShort(138);
            b.WriteInt( (int)siteId);
            b.WriteBool( (bool)showCursor);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetNodeOfSiteIsAudioClockMaster(int siteId, bool isMaster)
        {
            var b = new ByteUtil();
            b.WriteShort(145);
            b.WriteInt( (int)siteId);
            b.WriteBool( (bool)isMaster);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct AddEncryptionKeyGetIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public bool IsKeyAdded;
        }
        public AddEncryptionKeyGetIdResult AddEncryptionKeyGetId(string encryptionKey)
        {
            var b = new ByteUtil();
            b.WriteShort(164);
            b.WriteStringWide( (string)encryptionKey);
            b = connector.Send(b, true);
            var r = new AddEncryptionKeyGetIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsKeyAdded = b.ReadBool();
            }
            return r;
        }

        public struct AddEncryptionPolicyGetIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public bool IsKeyAdded;
        }
        public AddEncryptionPolicyGetIdResult AddEncryptionPolicyGetId(string encryptionPolicy)
        {
            var b = new ByteUtil();
            b.WriteShort(165);
            b.WriteStringWide( (string)encryptionPolicy);
            b = connector.Send(b, true);
            var r = new AddEncryptionPolicyGetIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsKeyAdded = b.ReadBool();
            }
            return r;
        }

        public PbAutoResult SetRouteInputToLayer(int siteId, bool enableInputRouting)
        {
            var b = new ByteUtil();
            b.WriteShort(166);
            b.WriteInt( (int)siteId);
            b.WriteBool( (bool)enableInputRouting);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetRouteInputToAutomation(int siteId, bool enableInputAutomation)
        {
            var b = new ByteUtil();
            b.WriteShort(167);
            b.WriteInt( (int)siteId);
            b.WriteBool( (bool)enableInputAutomation);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetEnableOutputForPicking(int siteId, int outputId, bool enableInputPicking)
        {
            var b = new ByteUtil();
            b.WriteShort(168);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)outputId);
            b.WriteBool( (bool)enableInputPicking);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetASIOMasterVolume(int siteId, double asioVolume)
        {
            var b = new ByteUtil();
            b.WriteShort(169);
            b.WriteInt( (int)siteId);
            b.WriteDouble( (double)asioVolume);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetThumbnailByPathResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int ThumbnailWidth;
            public int ThumbnailHeight;
            public byte[] ThumbnailData;
        }
        public GetThumbnailByPathResult GetThumbnailByPath(string resourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(162);
            b.WriteStringWide( (string)resourcePath);
            b = connector.Send(b, true);
            var r = new GetThumbnailByPathResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.ThumbnailWidth = b.ReadInt();
                r.ThumbnailHeight = b.ReadInt();
                r.ThumbnailData = b.ReadByteBuffer();
            }
            return r;
        }

        public struct GetThumbnailByItemIndexResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int ThumbnailWidth;
            public int ThumbnailHeight;
            public byte[] ThumbnailData;
        }
        public GetThumbnailByItemIndexResult GetThumbnailByItemIndex(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(163);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, true);
            var r = new GetThumbnailByItemIndexResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.ThumbnailWidth = b.ReadInt();
                r.ThumbnailHeight = b.ReadInt();
                r.ThumbnailData = b.ReadByteBuffer();
            }
            return r;
        }

        public PbAutoResult CreatePlaylist(bool doSetDmxIds, int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(176);
            b.WriteBool( (bool)doSetDmxIds);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreatePlaylistInPath(string projectPath, bool doSetDmxIds, int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(177);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteBool( (bool)doSetDmxIds);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreatePlaylistInItemId(int treeItemIndex, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(178);
            b.WriteInt( (int)treeItemIndex);
            b.WriteBool( (bool)setdmxFileIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreatePlaylistInPathFromFolder(string projectPath, string sourceProjectPath, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(179);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteStringNarrow( (string)sourceProjectPath);
            b.WriteBool( (bool)setdmxFileIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreatePlaylistInTreeItemFromFolder(int treeItemIndex, int sourceFolderItemId, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(180);
            b.WriteInt( (int)treeItemIndex);
            b.WriteInt( (int)sourceFolderItemId);
            b.WriteBool( (bool)setdmxFileIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult PushBackPlaylistEntryByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int resourceDmxFolderId, int resourceDmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(181);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)resourceDmxFolderId);
            b.WriteInt( (int)resourceDmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult PushBackPlaylistEntryByPath(string playlistPath, string resourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(182);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteStringNarrow( (string)resourcePath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult PushBackPlaylistEntryByItemId(int playlistItemIndex, int resourceItemId)
        {
            var b = new ByteUtil();
            b.WriteShort(183);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)resourceItemId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult InsertPlaylistEntryByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int resourceDmxFolderId, int resourceDmxFileId, int index)
        {
            var b = new ByteUtil();
            b.WriteShort(184);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)resourceDmxFolderId);
            b.WriteInt( (int)resourceDmxFileId);
            b.WriteInt( (int)index);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult InsertPlaylistEntryByPath(string playlistPath, string resourcePath, int index)
        {
            var b = new ByteUtil();
            b.WriteShort(185);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteStringNarrow( (string)resourcePath);
            b.WriteInt( (int)index);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult InsertPlaylistEntryByItemId(int playlistItemIndex, int resourceItemId, int index)
        {
            var b = new ByteUtil();
            b.WriteShort(186);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)resourceItemId);
            b.WriteInt( (int)index);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemovePlaylistEntryByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index)
        {
            var b = new ByteUtil();
            b.WriteShort(187);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)index);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemovePlaylistEntryByPath(string playlistPath, int index)
        {
            var b = new ByteUtil();
            b.WriteShort(188);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)index);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RemovePlaylistEntryByItemId(int playlistItemIndex, int index)
        {
            var b = new ByteUtil();
            b.WriteShort(189);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)index);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetPlaylistSizeByDmxIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int PlaylistSize;
        }
        public GetPlaylistSizeByDmxIdResult GetPlaylistSizeByDmxId(int playlistDmxFolderId, int playlistdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(190);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b = connector.Send(b, true);
            var r = new GetPlaylistSizeByDmxIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.PlaylistSize = b.ReadInt();
            }
            return r;
        }

        public struct GetPlaylistSizeByPathResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int PlaylistSize;
        }
        public GetPlaylistSizeByPathResult GetPlaylistSizeByPath(string playlistPath)
        {
            var b = new ByteUtil();
            b.WriteShort(191);
            b.WriteStringNarrow( (string)playlistPath);
            b = connector.Send(b, true);
            var r = new GetPlaylistSizeByPathResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.PlaylistSize = b.ReadInt();
            }
            return r;
        }

        public struct GetPlaylistSizeByItemIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int PlaylistSize;
        }
        public GetPlaylistSizeByItemIdResult GetPlaylistSizeByItemId(int playlistItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(192);
            b.WriteInt( (int)playlistItemIndex);
            b = connector.Send(b, true);
            var r = new GetPlaylistSizeByItemIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.PlaylistSize = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult SetPlaylistEntryIndexByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int newIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(199);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)newIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryIndexByPath(string playlistPath, int index, int newIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(200);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)index);
            b.WriteInt( (int)newIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryIndexByItemId(int playlistItemIndex, int index, int newIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(201);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)index);
            b.WriteInt( (int)newIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryDurationByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(202);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryDurationByPath(string playlistPath, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(203);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryDurationByItemId(int playlistItemIndex, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(204);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryFadeOutTimeByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(205);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryFadeOutTimeByPath(string playlistPath, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(206);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryFadeOutTimeByItemId(int playlistItemIndex, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(207);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryInPointByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(208);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryInPointByPath(string playlistPath, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(210);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryInPointByItemId(int playlistItemIndex, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(211);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryOutPointByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(212);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryOutPointByPath(string playlistPath, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(213);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryOutPointByItemId(int playlistItemIndex, int index, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(214);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)index);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryTransitionByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, int fadeFxId)
        {
            var b = new ByteUtil();
            b.WriteShort(215);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)fadeFxId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryTransitionByPath(string playlistPath, int index, int fadeFxId)
        {
            var b = new ByteUtil();
            b.WriteShort(216);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)index);
            b.WriteInt( (int)fadeFxId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryTransitionByItemId(int playlistItemIndex, int index, int fadeFxId)
        {
            var b = new ByteUtil();
            b.WriteShort(217);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)index);
            b.WriteInt( (int)fadeFxId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryNoteByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int index, string pNote)
        {
            var b = new ByteUtil();
            b.WriteShort(218);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)index);
            b.WriteStringNarrow( (string)pNote);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryNoteByPath(string playlistPath, int index, string pNote)
        {
            var b = new ByteUtil();
            b.WriteShort(219);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)index);
            b.WriteStringNarrow( (string)pNote);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetPlaylistEntryNoteByItemId(int playlistItemIndex, int index, string pNote)
        {
            var b = new ByteUtil();
            b.WriteShort(220);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)index);
            b.WriteStringNarrow( (string)pNote);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RecordLiveInputByDmxId(int folderID, int fileID, string pFilename, string encodingPresetName, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(222);
            b.WriteInt( (int)folderID);
            b.WriteInt( (int)fileID);
            b.WriteStringNarrow( (string)pFilename);
            b.WriteStringNarrow( (string)encodingPresetName);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RecordLiveInputStartByDmxId(int folderID, int fileID, string pFilename, string encodingPresetName)
        {
            var b = new ByteUtil();
            b.WriteShort(223);
            b.WriteInt( (int)folderID);
            b.WriteInt( (int)fileID);
            b.WriteStringNarrow( (string)pFilename);
            b.WriteStringNarrow( (string)encodingPresetName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RecordLiveInputByName(string liveInputResourcePath, string pFilename, string encodingPresetName, int hours, int minutes, int seconds, int frames)
        {
            var b = new ByteUtil();
            b.WriteShort(225);
            b.WriteStringNarrow( (string)liveInputResourcePath);
            b.WriteStringNarrow( (string)pFilename);
            b.WriteStringNarrow( (string)encodingPresetName);
            b.WriteInt( (int)hours);
            b.WriteInt( (int)minutes);
            b.WriteInt( (int)seconds);
            b.WriteInt( (int)frames);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RecordLiveInputStartByName(string liveInputResourcePath, string pFilename, string encodingPresetName)
        {
            var b = new ByteUtil();
            b.WriteShort(226);
            b.WriteStringNarrow( (string)liveInputResourcePath);
            b.WriteStringNarrow( (string)pFilename);
            b.WriteStringNarrow( (string)encodingPresetName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ExportVideo(string pFilename, string encodingPresetName, int sequenceId, int startHour, int startMinute, int startSecond, int startFrame, int endHour, int endMinute, int endSecond, int endFrame)
        {
            var b = new ByteUtil();
            b.WriteShort(227);
            b.WriteStringNarrow( (string)pFilename);
            b.WriteStringNarrow( (string)encodingPresetName);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)startHour);
            b.WriteInt( (int)startMinute);
            b.WriteInt( (int)startSecond);
            b.WriteInt( (int)startFrame);
            b.WriteInt( (int)endHour);
            b.WriteInt( (int)endMinute);
            b.WriteInt( (int)endSecond);
            b.WriteInt( (int)endFrame);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult EncodeFileByName(string resourcePath, string encodingPreset)
        {
            var b = new ByteUtil();
            b.WriteShort(228);
            b.WriteStringNarrow( (string)resourcePath);
            b.WriteStringNarrow( (string)encodingPreset);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult EncodeFileByDmxId(int folderID, int fileID, string encodingPreset)
        {
            var b = new ByteUtil();
            b.WriteShort(230);
            b.WriteInt( (int)folderID);
            b.WriteInt( (int)fileID);
            b.WriteStringNarrow( (string)encodingPreset);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult EncodeFileToPath(string resourcePath, string projectPath, bool overwriteExisting, string encodingPreset)
        {
            var b = new ByteUtil();
            b.WriteShort(229);
            b.WriteStringNarrow( (string)resourcePath);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteBool( (bool)overwriteExisting);
            b.WriteStringNarrow( (string)encodingPreset);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult EncodeFileByDmxId(int folderID, int fileID, string projectPath, bool overwriteExisting, string encodingPreset)
        {
            var b = new ByteUtil();
            b.WriteShort(231);
            b.WriteInt( (int)folderID);
            b.WriteInt( (int)fileID);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteBool( (bool)overwriteExisting);
            b.WriteStringNarrow( (string)encodingPreset);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetCanvasResolutionByDmxId(int canvasDmxFolderId, int canvasDmxFileId, int width, int height)
        {
            var b = new ByteUtil();
            b.WriteShort(239);
            b.WriteInt( (int)canvasDmxFolderId);
            b.WriteInt( (int)canvasDmxFileId);
            b.WriteInt( (int)width);
            b.WriteInt( (int)height);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetCanvasResolutionByPath(string canvasResourcePath, int width, int height)
        {
            var b = new ByteUtil();
            b.WriteShort(240);
            b.WriteStringNarrow( (string)canvasResourcePath);
            b.WriteInt( (int)width);
            b.WriteInt( (int)height);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetCanvasResolutionByItemId(int canvasItemIndex, int width, int height)
        {
            var b = new ByteUtil();
            b.WriteShort(241);
            b.WriteInt( (int)canvasItemIndex);
            b.WriteInt( (int)width);
            b.WriteInt( (int)height);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearCanvasByDmxId(int canvasDmxFolderId, int canvasDmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(242);
            b.WriteInt( (int)canvasDmxFolderId);
            b.WriteInt( (int)canvasDmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearCanvasByPath(string canvasResourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(243);
            b.WriteStringNarrow( (string)canvasResourcePath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearCanvasByItemId(int canvasItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(244);
            b.WriteInt( (int)canvasItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ExecuteCanvasCmdByDmxId(int canvasDmxFolderId, int canvasDmxFileId, string cmd, bool cmdContainsResourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(245);
            b.WriteInt( (int)canvasDmxFolderId);
            b.WriteInt( (int)canvasDmxFileId);
            b.WriteStringNarrow( (string)cmd);
            b.WriteBool( (bool)cmdContainsResourcePath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ExecuteCanvasCmdByPath(string canvasResourcePath, string cmd, bool cmdContainsResourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(246);
            b.WriteStringNarrow( (string)canvasResourcePath);
            b.WriteStringNarrow( (string)cmd);
            b.WriteBool( (bool)cmdContainsResourcePath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ExecuteCanvasCmdByItemId(int canvasItemIndex, string cmd, bool cmdContainsResourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(247);
            b.WriteInt( (int)canvasItemIndex);
            b.WriteStringNarrow( (string)cmd);
            b.WriteBool( (bool)cmdContainsResourcePath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetCanvasDrawCommandsByDmxIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public string Commands;
        }
        public GetCanvasDrawCommandsByDmxIdResult GetCanvasDrawCommandsByDmxId(int canvasDmxFolderId, int canvasDmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(248);
            b.WriteInt( (int)canvasDmxFolderId);
            b.WriteInt( (int)canvasDmxFileId);
            b = connector.Send(b, true);
            var r = new GetCanvasDrawCommandsByDmxIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Commands = b.ReadStringNarrow();
            }
            return r;
        }

        public struct GetCanvasDrawCommandsByPathResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public string Commands;
        }
        public GetCanvasDrawCommandsByPathResult GetCanvasDrawCommandsByPath(string canvasResourcePath)
        {
            var b = new ByteUtil();
            b.WriteShort(249);
            b.WriteStringNarrow( (string)canvasResourcePath);
            b = connector.Send(b, true);
            var r = new GetCanvasDrawCommandsByPathResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Commands = b.ReadStringNarrow();
            }
            return r;
        }

        public struct GetCanvasDrawCommandsByItemIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public string Commands;
        }
        public GetCanvasDrawCommandsByItemIdResult GetCanvasDrawCommandsByItemId(int canvasItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(250);
            b.WriteInt( (int)canvasItemIndex);
            b = connector.Send(b, true);
            var r = new GetCanvasDrawCommandsByItemIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Commands = b.ReadStringNarrow();
            }
            return r;
        }

        public struct GetMediaWidthByDmxIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Width;
        }
        public GetMediaWidthByDmxIdResult GetMediaWidthByDmxId(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(251);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, true);
            var r = new GetMediaWidthByDmxIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Width = b.ReadInt();
            }
            return r;
        }

        public struct GetMediaWidthByPathResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Width;
        }
        public GetMediaWidthByPathResult GetMediaWidthByPath(string folderPathToProject)
        {
            var b = new ByteUtil();
            b.WriteShort(252);
            b.WriteStringNarrow( (string)folderPathToProject);
            b = connector.Send(b, true);
            var r = new GetMediaWidthByPathResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Width = b.ReadInt();
            }
            return r;
        }

        public struct GetMediaWidthByItemIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Width;
        }
        public GetMediaWidthByItemIdResult GetMediaWidthByItemId(int itemId)
        {
            var b = new ByteUtil();
            b.WriteShort(253);
            b.WriteInt( (int)itemId);
            b = connector.Send(b, true);
            var r = new GetMediaWidthByItemIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Width = b.ReadInt();
            }
            return r;
        }

        public struct GetMediaHeightByDmxIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Height;
        }
        public GetMediaHeightByDmxIdResult GetMediaHeightByDmxId(int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(254);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, true);
            var r = new GetMediaHeightByDmxIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Height = b.ReadInt();
            }
            return r;
        }

        public struct GetMediaHeightByPathResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Height;
        }
        public GetMediaHeightByPathResult GetMediaHeightByPath(string folderPathToProject)
        {
            var b = new ByteUtil();
            b.WriteShort(255);
            b.WriteStringNarrow( (string)folderPathToProject);
            b = connector.Send(b, true);
            var r = new GetMediaHeightByPathResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Height = b.ReadInt();
            }
            return r;
        }

        public struct GetMediaHeightByItemIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Height;
        }
        public GetMediaHeightByItemIdResult GetMediaHeightByItemId(int itemId)
        {
            var b = new ByteUtil();
            b.WriteShort(256);
            b.WriteInt( (int)itemId);
            b = connector.Send(b, true);
            var r = new GetMediaHeightByItemIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Height = b.ReadInt();
            }
            return r;
        }

        public struct GetProjectPathOnDiscResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public string Commands;
        }
        public GetProjectPathOnDiscResult GetProjectPathOnDisc()
        {
            var b = new ByteUtil();
            b.WriteShort(257);
            b = connector.Send(b, true);
            var r = new GetProjectPathOnDiscResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Commands = b.ReadStringNarrow();
            }
            return r;
        }

        public PbAutoResult SaveProjectAs(string folderPathToProject, string projectXmlFileName)
        {
            var b = new ByteUtil();
            b.WriteShort(258);
            b.WriteStringNarrow( (string)folderPathToProject);
            b.WriteStringNarrow( (string)projectXmlFileName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SaveProjectCopy(string folderPathToProject, string projectXmlFileName)
        {
            var b = new ByteUtil();
            b.WriteShort(259);
            b.WriteStringNarrow( (string)folderPathToProject);
            b.WriteStringNarrow( (string)projectXmlFileName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult BundleProject(string bundlePath, string bundleName)
        {
            var b = new ByteUtil();
            b.WriteShort(260);
            b.WriteStringNarrow( (string)bundlePath);
            b.WriteStringNarrow( (string)bundleName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetResourceNameByPath(string resourcePath, string newResourceName)
        {
            var b = new ByteUtil();
            b.WriteShort(261);
            b.WriteStringNarrow( (string)resourcePath);
            b.WriteStringNarrow( (string)newResourceName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetResourceNameByItemIndex(int treeItemIndex, string newResourceName)
        {
            var b = new ByteUtil();
            b.WriteShort(263);
            b.WriteInt( (int)treeItemIndex);
            b.WriteStringNarrow( (string)newResourceName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetResourceNameByDmxId(int dmxFolderId, int dmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.WriteShort(262);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringNarrow( (string)newResourceName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SendCanvasCmdsToStackByDmxId(int canvasDmxFolderId, int canvasDmxFileId, bool doAddToStack)
        {
            var b = new ByteUtil();
            b.WriteShort(265);
            b.WriteInt( (int)canvasDmxFolderId);
            b.WriteInt( (int)canvasDmxFileId);
            b.WriteBool( (bool)doAddToStack);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetAddCanvasCmdsToStackByPath(string canvasResourcePath, bool doAddToStack)
        {
            var b = new ByteUtil();
            b.WriteShort(266);
            b.WriteStringNarrow( (string)canvasResourcePath);
            b.WriteBool( (bool)doAddToStack);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetAddCanvasCmdsToStackByItemId(int canvasItemIndex, bool doAddToStack)
        {
            var b = new ByteUtil();
            b.WriteShort(267);
            b.WriteInt( (int)canvasItemIndex);
            b.WriteBool( (bool)doAddToStack);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearEmptyPlaylistEntriesByDmxId(int playlistDmxFolderId, int playlistdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(268);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearEmptyPlaylistEntriesByPath(string playlistPath)
        {
            var b = new ByteUtil();
            b.WriteShort(269);
            b.WriteStringNarrow( (string)playlistPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearEmptyPlaylistEntriesByItemId(int playlistItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(270);
            b.WriteInt( (int)playlistItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearAllPlaylistEntriesByDmxId(int playlistDmxFolderId, int playlistdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(271);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearAllPlaylistEntriesByPath(string playlistPath)
        {
            var b = new ByteUtil();
            b.WriteShort(272);
            b.WriteStringNarrow( (string)playlistPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearAllPlaylistEntriesByItemIndex(int playlistItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(273);
            b.WriteInt( (int)playlistItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSublayerParamOfKindDouble(int siteId, int deviceId, int sublayerId, int parameterKindId, double parameterValue, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.WriteShort(274);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)sublayerId);
            b.WriteInt( (int)parameterKindId);
            b.WriteDouble( (double)parameterValue);
            b.WriteBool( (bool)doSilent);
            b.WriteBool( (bool)doDirect);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult HandleSublayer(int siteId, int deviceId, int action, int data)
        {
            var b = new ByteUtil();
            b.WriteShort(275);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)action);
            b.WriteInt( (int)data);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetCueName(int sequenceId, int cueId, string cueName)
        {
            var b = new ByteUtil();
            b.WriteShort(276);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b.WriteStringNarrow( (string)cueName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetCueNameResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public string CueName;
        }
        public GetCueNameResult GetCueName(int sequenceId, int cueId)
        {
            var b = new ByteUtil();
            b.WriteShort(277);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)cueId);
            b = connector.Send(b, true);
            var r = new GetCueNameResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.CueName = b.ReadStringNarrow();
            }
            return r;
        }

        public PbAutoResult StoreActiveSite(int sequenceId, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(278);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult StoreActiveDevice(int sequenceId, int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(279);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult StoreActiveParam(int sequenceId, int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(280);
            b.WriteInt( (int)sequenceId);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AssignDeviceByName(int siteId, int deviceId, int sourceDeviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(282);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)sourceDeviceId);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AssignResourceToParam(int siteId, int deviceId, int dmxFolderId, int dmxFileId, bool forMesh, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(283);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)forMesh);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddImageSequence(string folderPath, int siteId, int dmxFolderId, int dmxFileId, int fps)
        {
            var b = new ByteUtil();
            b.WriteShort(284);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteInt( (int)fps);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddImageSequenceToFolder(string folderPath, int siteId, int dmxFolderId, int dmxFileId, int fps, string projectPath)
        {
            var b = new ByteUtil();
            b.WriteShort(285);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteInt( (int)fps);
            b.WriteStringNarrow( (string)projectPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddImageSequenceToTreeItem(string folderPath, int siteId, int dmxFolderId, int dmxFileId, int fps, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(286);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteInt( (int)fps);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddImageSequenceFromLocalNode(string folderPath, int fps)
        {
            var b = new ByteUtil();
            b.WriteShort(287);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)fps);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddImageSequenceFromLocalNodeId(string folderPath, int fps, int dmxFolderId, int dmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(288);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)fps);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddImageSequenceFromLocalNodeToFolder(string folderPath, int fps, string projectPath)
        {
            var b = new ByteUtil();
            b.WriteShort(289);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)fps);
            b.WriteStringNarrow( (string)projectPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddImageSequenceFromLocalNodeToFolderId(string folderPath, int fps, int dmxFolderId, int dmxFileId, string projectPath)
        {
            var b = new ByteUtil();
            b.WriteShort(290);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)fps);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringNarrow( (string)projectPath);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddImageSequenceFromLocalNodeToTreeItem(string folderPath, int fps, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(291);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)fps);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddImageSequenceFromLocalNodeToTreeItemId(string folderPath, int fps, int dmxFolderId, int dmxFileId, int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(292);
            b.WriteStringNarrow( (string)folderPath);
            b.WriteInt( (int)fps);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetTextFormatted(int dmxFolderId, int dmxFileId, string text, bool isFormatted)
        {
            var b = new ByteUtil();
            b.WriteShort(293);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringNarrow( (string)text);
            b.WriteBool( (bool)isFormatted);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetTextFormattedWide(int dmxFolderId, int dmxFileId, string text, bool isFormatted)
        {
            var b = new ByteUtil();
            b.WriteShort(294);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringWide( (string)text);
            b.WriteBool( (bool)isFormatted);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetCurrentTimeCueInfoResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Hours;
            public int Minutes;
            public int Seconds;
            public int Frames;
            public int PreviousCueId;
            public string PreviousCueName;
            public int HoursPreviousCue;
            public int MinutesPreviousCue;
            public int SecondsPreviousCue;
            public int FramesPreviousCue;
            public int PreviousCueMode;
            public int NextCueId;
            public string NextCueName;
            public int HoursNextCue;
            public int MinutesNextCue;
            public int SecondsNextCue;
            public int FramesNextCue;
            public int NextCueMode;
        }
        public GetCurrentTimeCueInfoResult GetCurrentTimeCueInfo(int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(295);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, true);
            var r = new GetCurrentTimeCueInfoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Hours = b.ReadInt();
                r.Minutes = b.ReadInt();
                r.Seconds = b.ReadInt();
                r.Frames = b.ReadInt();
                r.PreviousCueId = b.ReadInt();
                r.PreviousCueName = b.ReadStringNarrow();
                r.HoursPreviousCue = b.ReadInt();
                r.MinutesPreviousCue = b.ReadInt();
                r.SecondsPreviousCue = b.ReadInt();
                r.FramesPreviousCue = b.ReadInt();
                r.PreviousCueMode = b.ReadInt();
                r.NextCueId = b.ReadInt();
                r.NextCueName = b.ReadStringNarrow();
                r.HoursNextCue = b.ReadInt();
                r.MinutesNextCue = b.ReadInt();
                r.SecondsNextCue = b.ReadInt();
                r.FramesNextCue = b.ReadInt();
                r.NextCueMode = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult GetContentIsConsistentByTreeItem(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(296);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SpreadToSite(string resourcePath, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(297);
            b.WriteStringNarrow( (string)resourcePath);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetGroupSelection(int groupIndex, int selectionMode)
        {
            var b = new ByteUtil();
            b.WriteShort(298);
            b.WriteInt( (int)groupIndex);
            b.WriteInt( (int)selectionMode);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSequenceSelection(int sequenceId)
        {
            var b = new ByteUtil();
            b.WriteShort(299);
            b.WriteInt( (int)sequenceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreatePlaylistWithName(bool doSetDmxIds, int dmxFolderId, int dmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.WriteShort(300);
            b.WriteBool( (bool)doSetDmxIds);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringNarrow( (string)newResourceName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreatePlaylistInPathWithName(string projectPath, bool doSetDmxIds, int dmxFolderId, int dmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.WriteShort(301);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteBool( (bool)doSetDmxIds);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteStringNarrow( (string)newResourceName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreatePlaylistInItemIdWithName(int treeItemIndex, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.WriteShort(302);
            b.WriteInt( (int)treeItemIndex);
            b.WriteBool( (bool)setdmxFileIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b.WriteStringNarrow( (string)newResourceName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreatePlaylistInPathFromFolderWithName(string projectPath, string sourceProjectPath, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.WriteShort(303);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteStringNarrow( (string)sourceProjectPath);
            b.WriteBool( (bool)setdmxFileIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b.WriteStringNarrow( (string)newResourceName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreatePlaylistInTreeItemFromFolderWithName(int treeItemIndex, int sourceFolderItemId, bool setdmxFileIds, int newDmxFolderId, int newdmxFileId, string newResourceName)
        {
            var b = new ByteUtil();
            b.WriteShort(304);
            b.WriteInt( (int)treeItemIndex);
            b.WriteInt( (int)sourceFolderItemId);
            b.WriteBool( (bool)setdmxFileIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b.WriteStringNarrow( (string)newResourceName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetWatchedFolderProperty(string projectPath, int watchFolderProperty, bool enable)
        {
            var b = new ByteUtil();
            b.WriteShort(305);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteInt( (int)watchFolderProperty);
            b.WriteBool( (bool)enable);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetWatchedFolderPropertyByItemId(int treeItemIndex, int watchFolderProperty, bool enable)
        {
            var b = new ByteUtil();
            b.WriteShort(306);
            b.WriteInt( (int)treeItemIndex);
            b.WriteInt( (int)watchFolderProperty);
            b.WriteBool( (bool)enable);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetFolderSpreadToSite(string projectPath, int siteId, bool enable)
        {
            var b = new ByteUtil();
            b.WriteShort(307);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteInt( (int)siteId);
            b.WriteBool( (bool)enable);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetFolderSpreadToSiteByItemId(int treeItemIndex, int siteId, bool enable)
        {
            var b = new ByteUtil();
            b.WriteShort(308);
            b.WriteInt( (int)treeItemIndex);
            b.WriteInt( (int)siteId);
            b.WriteBool( (bool)enable);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ClearStreamingText(int dmxFolderId, int dmxFileId, bool pendingOnly)
        {
            var b = new ByteUtil();
            b.WriteShort(309);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)pendingOnly);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetWatchedFolderPropertyResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public bool IsEnabled;
        }
        public GetWatchedFolderPropertyResult GetWatchedFolderProperty(string projectPath, int watchFolderProperty)
        {
            var b = new ByteUtil();
            b.WriteShort(310);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteInt( (int)watchFolderProperty);
            b = connector.Send(b, true);
            var r = new GetWatchedFolderPropertyResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsEnabled = b.ReadBool();
            }
            return r;
        }

        public struct GetWatchedFolderPropertyByItemIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public bool IsEnabled;
        }
        public GetWatchedFolderPropertyByItemIdResult GetWatchedFolderPropertyByItemId(int treeItemIndex, int watchFolderProperty)
        {
            var b = new ByteUtil();
            b.WriteShort(311);
            b.WriteInt( (int)treeItemIndex);
            b.WriteInt( (int)watchFolderProperty);
            b = connector.Send(b, true);
            var r = new GetWatchedFolderPropertyByItemIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsEnabled = b.ReadBool();
            }
            return r;
        }

        public struct GetFolderSpreadToSiteResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public bool IsEnabled;
        }
        public GetFolderSpreadToSiteResult GetFolderSpreadToSite(string projectPath, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(312);
            b.WriteStringNarrow( (string)projectPath);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, true);
            var r = new GetFolderSpreadToSiteResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsEnabled = b.ReadBool();
            }
            return r;
        }

        public struct GetFolderSpreadToSiteByItemIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public bool IsEnabled;
        }
        public GetFolderSpreadToSiteByItemIdResult GetFolderSpreadToSiteByItemId(int treeItemIndex, int siteId)
        {
            var b = new ByteUtil();
            b.WriteShort(313);
            b.WriteInt( (int)treeItemIndex);
            b.WriteInt( (int)siteId);
            b = connector.Send(b, true);
            var r = new GetFolderSpreadToSiteByItemIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.IsEnabled = b.ReadBool();
            }
            return r;
        }

        public PbAutoResult InsertPlaylistEntryWithParametersByDmxId()
        {
            var b = new ByteUtil();
            b.WriteShort(314);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult InsertPlaylistEntryWithParametersByPath()
        {
            var b = new ByteUtil();
            b.WriteShort(315);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult InsertPlaylistEntryWithParametersByTreeItem()
        {
            var b = new ByteUtil();
            b.WriteShort(316);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetParamRelative(int siteId, int deviceId, string parameterName, int parameterValue)
        {
            var b = new ByteUtil();
            b.WriteShort(18);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b.WriteInt( (int)parameterValue);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AddContent(string filePath, int siteId, int dmxFolderId, int dmxFileId, bool autoIncrementDmxId)
        {
            var b = new ByteUtil();
            b.WriteShort(19);
            b.WriteStringNarrow( (string)filePath);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)autoIncrementDmxId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetMediaInfoResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int DmxFolderId;
            public int DmxFileId;
            public string ResourceName;
            public string ResourcePath;
            public string ProjectPath;
            public int Width;
            public int Height;
            public int Fps;
            public int Hours;
            public int Minutes;
            public int Seconds;
            public int Frames;
            public int Options;
        }
        public GetMediaInfoResult GetMediaInfo(int treeItemsMediaIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(76);
            b.WriteInt( (int)treeItemsMediaIndex);
            b = connector.Send(b, true);
            var r = new GetMediaInfoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.DmxFolderId = b.ReadInt();
                r.DmxFileId = b.ReadInt();
                r.ResourceName = b.ReadStringNarrow();
                r.ResourcePath = b.ReadStringNarrow();
                r.ProjectPath = b.ReadStringNarrow();
                r.Width = b.ReadInt();
                r.Height = b.ReadInt();
                r.Fps = b.ReadInt();
                r.Hours = b.ReadInt();
                r.Minutes = b.ReadInt();
                r.Seconds = b.ReadInt();
                r.Frames = b.ReadInt();
                r.Options = b.ReadInt();
            }
            return r;
        }

        public PbAutoResult InsertPlaylistEntryWithParametersByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int resourceDmxFolderId, int resourceDmxFileId, int index, int durationHours, int durationMinutes, int durationSeconds, int durationFrames, int fadeOutHour, int fadeOutMinute, int fadeOutSecond, int fadeOutFrame, int startHour, int startMinute, int startSecond, int startFrame, int endHour, int endMinute, int endSecond, int endFrame, int fadeFxId)
        {
            var b = new ByteUtil();
            b.WriteShort(314);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)resourceDmxFolderId);
            b.WriteInt( (int)resourceDmxFileId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)durationHours);
            b.WriteInt( (int)durationMinutes);
            b.WriteInt( (int)durationSeconds);
            b.WriteInt( (int)durationFrames);
            b.WriteInt( (int)fadeOutHour);
            b.WriteInt( (int)fadeOutMinute);
            b.WriteInt( (int)fadeOutSecond);
            b.WriteInt( (int)fadeOutFrame);
            b.WriteInt( (int)startHour);
            b.WriteInt( (int)startMinute);
            b.WriteInt( (int)startSecond);
            b.WriteInt( (int)startFrame);
            b.WriteInt( (int)endHour);
            b.WriteInt( (int)endMinute);
            b.WriteInt( (int)endSecond);
            b.WriteInt( (int)endFrame);
            b.WriteInt( (int)fadeFxId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult InsertPlaylistEntryWithParametersByPath(string playlistPath, string resourcePath, int index, int durationHours, int durationMinutes, int durationSeconds, int durationFrames, int fadeOutHour, int fadeOutMinute, int fadeOutSecond, int fadeOutFrame, int startHour, int startMinute, int startSecond, int startFrame, int endHour, int endMinute, int endSecond, int endFrame, int fadeFxId)
        {
            var b = new ByteUtil();
            b.WriteShort(315);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteStringNarrow( (string)resourcePath);
            b.WriteInt( (int)index);
            b.WriteInt( (int)durationHours);
            b.WriteInt( (int)durationMinutes);
            b.WriteInt( (int)durationSeconds);
            b.WriteInt( (int)durationFrames);
            b.WriteInt( (int)fadeOutHour);
            b.WriteInt( (int)fadeOutMinute);
            b.WriteInt( (int)fadeOutSecond);
            b.WriteInt( (int)fadeOutFrame);
            b.WriteInt( (int)startHour);
            b.WriteInt( (int)startMinute);
            b.WriteInt( (int)startSecond);
            b.WriteInt( (int)startFrame);
            b.WriteInt( (int)endHour);
            b.WriteInt( (int)endMinute);
            b.WriteInt( (int)endSecond);
            b.WriteInt( (int)endFrame);
            b.WriteInt( (int)fadeFxId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult InsertPlaylistEntryWithParametersByTreeItem(int playlistItemIndex, int resourceItemId, int index, int durationHours, int durationMinutes, int durationSeconds, int durationFrames, int fadeOutHour, int fadeOutMinute, int fadeOutSecond, int fadeOutFrame, int startHour, int startMinute, int startSecond, int startFrame, int endHour, int endMinute, int endSecond, int endFrame, int fadeFxId)
        {
            var b = new ByteUtil();
            b.WriteShort(316);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)resourceItemId);
            b.WriteInt( (int)index);
            b.WriteInt( (int)durationHours);
            b.WriteInt( (int)durationMinutes);
            b.WriteInt( (int)durationSeconds);
            b.WriteInt( (int)durationFrames);
            b.WriteInt( (int)fadeOutHour);
            b.WriteInt( (int)fadeOutMinute);
            b.WriteInt( (int)fadeOutSecond);
            b.WriteInt( (int)fadeOutFrame);
            b.WriteInt( (int)startHour);
            b.WriteInt( (int)startMinute);
            b.WriteInt( (int)startSecond);
            b.WriteInt( (int)startFrame);
            b.WriteInt( (int)endHour);
            b.WriteInt( (int)endMinute);
            b.WriteInt( (int)endSecond);
            b.WriteInt( (int)endFrame);
            b.WriteInt( (int)fadeFxId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetYawPitchRoll(int siteId, int deviceId, bool inRadians, double yaw, double pitch, double roll, bool doSilent, bool doDirect)
        {
            var b = new ByteUtil();
            b.WriteShort(323);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteBool( (bool)inRadians);
            b.WriteDouble( (double)yaw);
            b.WriteDouble( (double)pitch);
            b.WriteDouble( (double)roll);
            b.WriteBool( (bool)doSilent);
            b.WriteBool( (bool)doDirect);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetYawPitchRollResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public double Yaw;
            public double Pitch;
            public double Roll;
        }
        public GetYawPitchRollResult GetYawPitchRoll(int siteId, int deviceId, bool inRadians)
        {
            var b = new ByteUtil();
            b.WriteShort(324);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteBool( (bool)inRadians);
            b = connector.Send(b, true);
            var r = new GetYawPitchRollResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Yaw = b.ReadDouble();
                r.Pitch = b.ReadDouble();
                r.Roll = b.ReadDouble();
            }
            return r;
        }

        public struct GetSiteIdsResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int[] SiteIds;
        }
        public GetSiteIdsResult GetSiteIds()
        {
            var b = new ByteUtil();
            b.WriteShort(317);
            b = connector.Send(b, true);
            var r = new GetSiteIdsResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.SiteIds = b.ReadIntBuffer();
            }
            return r;
        }

        public PbAutoResult SetCompositingPassRenderTargetSize(int siteId, int deviceId, int width, int height)
        {
            var b = new ByteUtil();
            b.WriteShort(341);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)width);
            b.WriteInt( (int)height);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetSoftedgeIsWarped(int siteId, int deviceId, bool isWarped)
        {
            var b = new ByteUtil();
            b.WriteShort(342);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteBool( (bool)isWarped);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetCanvasTextureFormatByDmxId(int canvasDmxFolderId, int canvasDmxFileId, int textureFormat)
        {
            var b = new ByteUtil();
            b.WriteShort(338);
            b.WriteInt( (int)canvasDmxFolderId);
            b.WriteInt( (int)canvasDmxFileId);
            b.WriteInt( (int)textureFormat);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetCanvasTextureFormatByPath(string canvasResourcePath, int textureFormat)
        {
            var b = new ByteUtil();
            b.WriteShort(339);
            b.WriteStringNarrow( (string)canvasResourcePath);
            b.WriteInt( (int)textureFormat);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetCanvasTextureFormatByItemId(int canvasItemIndex, int textureFormat)
        {
            var b = new ByteUtil();
            b.WriteShort(340);
            b.WriteInt( (int)canvasItemIndex);
            b.WriteInt( (int)textureFormat);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ResetSockets()
        {
            var b = new ByteUtil();
            b.WriteShort(354);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult ResetSerialLink(int siteId, int deviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(355);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AssignResourceToParamBlocked(int siteId, int deviceId, int dmxFolderId, int dmxFileId, bool forMesh, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(352);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)forMesh);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AssignResourceBlocked(int siteId, int deviceId, int dmxFolderId, int dmxFileId, bool forMesh)
        {
            var b = new ByteUtil();
            b.WriteShort(353);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)dmxFolderId);
            b.WriteInt( (int)dmxFileId);
            b.WriteBool( (bool)forMesh);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult JumpToPlayListEntryByDmxId(bool forward, int playlistDmxFolderId, int playlistdmxFileId, int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(356);
            b.WriteBool( (bool)forward);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult JumpToPlayListEntryByPath(bool forward, string playlistPath, int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(357);
            b.WriteBool( (bool)forward);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult JumpToPlayListEntryByItemId(bool forward, int playlistItemIndex, int siteId, int deviceId, string parameterName)
        {
            var b = new ByteUtil();
            b.WriteShort(358);
            b.WriteBool( (bool)forward);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteStringNarrow( (string)parameterName);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult SetMediaTransportMode(int siteId, int deviceId, int transportMode)
        {
            var b = new ByteUtil();
            b.WriteShort(359);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)transportMode);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult AssignDevice(int siteId, int deviceId, int sourceDeviceId)
        {
            var b = new ByteUtil();
            b.WriteShort(281);
            b.WriteInt( (int)siteId);
            b.WriteInt( (int)deviceId);
            b.WriteInt( (int)sourceDeviceId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreateCanvas(bool doSetDmxIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(236);
            b.WriteBool( (bool)doSetDmxIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreateCanvasByPath(string canvasResourcePath, bool doSetDmxIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(237);
            b.WriteStringNarrow( (string)canvasResourcePath);
            b.WriteBool( (bool)doSetDmxIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreateCanvasByItemId(int folderItemIndex, bool doSetDmxIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(238);
            b.WriteInt( (int)folderItemIndex);
            b.WriteBool( (bool)doSetDmxIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult RecordLiveInputStop()
        {
            var b = new ByteUtil();
            b.WriteShort(224);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public PbAutoResult CreateCanvasByPathFromTemplate(string canvasResourcePath, string newResourceName, string cmd, bool setDims, int width, int height, bool doSetDmxIds, int newDmxFolderId, int newdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(264);
            b.WriteStringNarrow( (string)canvasResourcePath);
            b.WriteStringNarrow( (string)newResourceName);
            b.WriteStringNarrow( (string)cmd);
            b.WriteBool( (bool)setDims);
            b.WriteInt( (int)width);
            b.WriteInt( (int)height);
            b.WriteBool( (bool)doSetDmxIds);
            b.WriteInt( (int)newDmxFolderId);
            b.WriteInt( (int)newdmxFileId);
            b = connector.Send(b, false);
            var r = new PbAutoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else r.Error = 0;
            return r;
        }

        public struct GetHostRevisionNumberResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int Revision;
        }
        public GetHostRevisionNumberResult GetHostRevisionNumber()
        {
            var b = new ByteUtil();
            b.WriteShort(334);
            b = connector.Send(b, true);
            var r = new GetHostRevisionNumberResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.Revision = b.ReadInt();
            }
            return r;
        }

        public struct GetTreeItemInfoResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int ResourceType;
            public string ResourcePath;
            public string FolderPath;
        }
        public GetTreeItemInfoResult GetTreeItemInfo(int treeItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(151);
            b.WriteInt( (int)treeItemIndex);
            b = connector.Send(b, true);
            var r = new GetTreeItemInfoResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.ResourceType = b.ReadInt();
                r.ResourcePath = b.ReadStringWide();
                r.FolderPath = b.ReadStringWide();
            }
            return r;
        }

        public struct GetMediaInfoByTreeItemIndexResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int DmxFolderId;
            public int DmxFileId;
            public string ResourceName;
            public string ResourcePath;
            public string ProjectPath;
            public int Width;
            public int Height;
            public int Fps;
            public int Hours;
            public int Minutes;
            public int Seconds;
            public int Frames;
            public int Options;
        }
        public GetMediaInfoByTreeItemIndexResult GetMediaInfoByTreeItemIndex(int index)
        {
            var b = new ByteUtil();
            b.WriteShort(152);
            b.WriteInt( (int)index);
            b = connector.Send(b, true);
            var r = new GetMediaInfoByTreeItemIndexResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.DmxFolderId = b.ReadInt();
                r.DmxFileId = b.ReadInt();
                r.ResourceName = b.ReadStringNarrow();
                r.ResourcePath = b.ReadStringNarrow();
                r.ProjectPath = b.ReadStringNarrow();
                r.Width = b.ReadInt();
                r.Height = b.ReadInt();
                r.Fps = b.ReadInt();
                r.Hours = b.ReadInt();
                r.Minutes = b.ReadInt();
                r.Seconds = b.ReadInt();
                r.Frames = b.ReadInt();
                r.Options = b.ReadInt();
            }
            return r;
        }

        public struct GetPlaylistEntryByDmxIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int TreeItemIndex;
            public string ResourceName;
            public string ResourcePath;
            public int DurationHours;
            public int DurationMinutes;
            public int DurationSeconds;
            public int DurationFrames;
            public int FadeOutHour;
            public int FadeOutMinute;
            public int FadeOutSecond;
            public int FadeOutFrame;
            public int StartHour;
            public int StartMinute;
            public int StartSecond;
            public int StartFrame;
            public int EndHour;
            public int EndMinute;
            public int EndSecond;
            public int EndFrame;
            public int FadeFxId;
        }
        public GetPlaylistEntryByDmxIdResult GetPlaylistEntryByDmxId(int playlistDmxFolderId, int playlistdmxFileId, int playlistEntryIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(193);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b.WriteInt( (int)playlistEntryIndex);
            b = connector.Send(b, true);
            var r = new GetPlaylistEntryByDmxIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TreeItemIndex = b.ReadInt();
                r.ResourceName = b.ReadStringNarrow();
                r.ResourcePath = b.ReadStringNarrow();
                r.DurationHours = b.ReadInt();
                r.DurationMinutes = b.ReadInt();
                r.DurationSeconds = b.ReadInt();
                r.DurationFrames = b.ReadInt();
                r.FadeOutHour = b.ReadInt();
                r.FadeOutMinute = b.ReadInt();
                r.FadeOutSecond = b.ReadInt();
                r.FadeOutFrame = b.ReadInt();
                r.StartHour = b.ReadInt();
                r.StartMinute = b.ReadInt();
                r.StartSecond = b.ReadInt();
                r.StartFrame = b.ReadInt();
                r.EndHour = b.ReadInt();
                r.EndMinute = b.ReadInt();
                r.EndSecond = b.ReadInt();
                r.EndFrame = b.ReadInt();
                r.FadeFxId = b.ReadInt();
            }
            return r;
        }

        public struct GetPlaylistEntryByPathResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int TreeItemIndex;
            public string ResourceName;
            public string ResourcePath;
            public int DurationHours;
            public int DurationMinutes;
            public int DurationSeconds;
            public int DurationFrames;
            public int FadeOutHour;
            public int FadeOutMinute;
            public int FadeOutSecond;
            public int FadeOutFrame;
            public int StartHour;
            public int StartMinute;
            public int StartSecond;
            public int StartFrame;
            public int EndHour;
            public int EndMinute;
            public int EndSecond;
            public int EndFrame;
            public int FadeFxId;
        }
        public GetPlaylistEntryByPathResult GetPlaylistEntryByPath(string playlistPath, int playlistEntryIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(194);
            b.WriteStringNarrow( (string)playlistPath);
            b.WriteInt( (int)playlistEntryIndex);
            b = connector.Send(b, true);
            var r = new GetPlaylistEntryByPathResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TreeItemIndex = b.ReadInt();
                r.ResourceName = b.ReadStringNarrow();
                r.ResourcePath = b.ReadStringNarrow();
                r.DurationHours = b.ReadInt();
                r.DurationMinutes = b.ReadInt();
                r.DurationSeconds = b.ReadInt();
                r.DurationFrames = b.ReadInt();
                r.FadeOutHour = b.ReadInt();
                r.FadeOutMinute = b.ReadInt();
                r.FadeOutSecond = b.ReadInt();
                r.FadeOutFrame = b.ReadInt();
                r.StartHour = b.ReadInt();
                r.StartMinute = b.ReadInt();
                r.StartSecond = b.ReadInt();
                r.StartFrame = b.ReadInt();
                r.EndHour = b.ReadInt();
                r.EndMinute = b.ReadInt();
                r.EndSecond = b.ReadInt();
                r.EndFrame = b.ReadInt();
                r.FadeFxId = b.ReadInt();
            }
            return r;
        }

        public struct GetPlaylistEntryByItemIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int TreeItemIndex;
            public string ResourceName;
            public string ResourcePath;
            public int DurationHours;
            public int DurationMinutes;
            public int DurationSeconds;
            public int DurationFrames;
            public int FadeOutHour;
            public int FadeOutMinute;
            public int FadeOutSecond;
            public int FadeOutFrame;
            public int StartHour;
            public int StartMinute;
            public int StartSecond;
            public int StartFrame;
            public int EndHour;
            public int EndMinute;
            public int EndSecond;
            public int EndFrame;
            public int FadeFxId;
        }
        public GetPlaylistEntryByItemIdResult GetPlaylistEntryByItemId(int playlistItemIndex, int playlistEntryIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(195);
            b.WriteInt( (int)playlistItemIndex);
            b.WriteInt( (int)playlistEntryIndex);
            b = connector.Send(b, true);
            var r = new GetPlaylistEntryByItemIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TreeItemIndex = b.ReadInt();
                r.ResourceName = b.ReadStringNarrow();
                r.ResourcePath = b.ReadStringNarrow();
                r.DurationHours = b.ReadInt();
                r.DurationMinutes = b.ReadInt();
                r.DurationSeconds = b.ReadInt();
                r.DurationFrames = b.ReadInt();
                r.FadeOutHour = b.ReadInt();
                r.FadeOutMinute = b.ReadInt();
                r.FadeOutSecond = b.ReadInt();
                r.FadeOutFrame = b.ReadInt();
                r.StartHour = b.ReadInt();
                r.StartMinute = b.ReadInt();
                r.StartSecond = b.ReadInt();
                r.StartFrame = b.ReadInt();
                r.EndHour = b.ReadInt();
                r.EndMinute = b.ReadInt();
                r.EndSecond = b.ReadInt();
                r.EndFrame = b.ReadInt();
                r.FadeFxId = b.ReadInt();
            }
            return r;
        }

        public struct GetPlaylistEntryIndicesByDmxIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int[] TreeItemIds;
        }
        public GetPlaylistEntryIndicesByDmxIdResult GetPlaylistEntryIndicesByDmxId(int playlistDmxFolderId, int playlistdmxFileId)
        {
            var b = new ByteUtil();
            b.WriteShort(196);
            b.WriteInt( (int)playlistDmxFolderId);
            b.WriteInt( (int)playlistdmxFileId);
            b = connector.Send(b, true);
            var r = new GetPlaylistEntryIndicesByDmxIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TreeItemIds = b.ReadIntBuffer();
            }
            return r;
        }

        public struct GetPlaylistEntryIndicesByPathResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int[] TreeItemIds;
        }
        public GetPlaylistEntryIndicesByPathResult GetPlaylistEntryIndicesByPath(string playlistPath)
        {
            var b = new ByteUtil();
            b.WriteShort(197);
            b.WriteStringNarrow( (string)playlistPath);
            b = connector.Send(b, true);
            var r = new GetPlaylistEntryIndicesByPathResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TreeItemIds = b.ReadIntBuffer();
            }
            return r;
        }

        public struct GetPlaylistEntryIndicesByItemIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int[] TreeItemIds;
        }
        public GetPlaylistEntryIndicesByItemIdResult GetPlaylistEntryIndicesByItemId(int playlistItemIndex)
        {
            var b = new ByteUtil();
            b.WriteShort(198);
            b.WriteInt( (int)playlistItemIndex);
            b = connector.Send(b, true);
            var r = new GetPlaylistEntryIndicesByItemIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.TreeItemIds = b.ReadIntBuffer();
            }
            return r;
        }

        public struct CreateGraphicLayerGetIdResult
        {
            public bool Ok { get { return Error == 0; } }
            public short Code; public int Error;
            public int LayerId;
        }
        public CreateGraphicLayerGetIdResult CreateGraphicLayerGetId(int siteId, bool isGraphicLayer)
        {
            var b = new ByteUtil();
            b.WriteShort(96);
            b.WriteInt( (int)siteId);
            b.WriteBool( (bool)isGraphicLayer);
            b = connector.Send(b, true);
            var r = new CreateGraphicLayerGetIdResult();
            r.Code = b.ReadShort();
            if (r.Code < 0) r.Error = b.ReadInt(); else
            {
                r.Error = 0;
                r.LayerId = b.ReadInt();
            }
            return r;
        }
    }


    enum ErrorCode
    {
        None = 0,
        NoConnection = 1,
        WrongParam = 2,
        AddressTranslation = 3,
        CouldNotConnectToSocket = 4,
        HandshakeFailed = 5,
        RequestTimedOut = 6,
        WrongMessageReturned = 7,
        ParamPointer = 8,
        WrongClient = 9,
        HostInvalidLayer = 10,
        HostInvalidSequence = 11,
        HostInvalidPointer = 12,
        HostInvalidParameterName = 13,
        HostInvalidParam = 14,
        InvalidPort = 15,
        WrongNetworkProtocol = 16,
        AlreadyConnected = 17,
        InvalidCueId = 18,
        InvalidCueButtonId = 19,
        InvalidDomainNr = 20,
        GraphicLayerNotCreated = 21,
        InvalidSiteId = 22,
        InvalidViewId = 23,
        InvalidCast = 24,
        AddingVideoLayerNotAllowed = 25,
        InvalidLayerMoveTarget = 26,
        InvalidFolderPath = 27,
        DmxResourceNotFound = 28,
        NoAdditionalSequenceAllowed = 29,
        InvalidContentPath = 30,
        HandshakeTimeout = 31,
        FunctionNotSupportedByOs = 32,
        TreeItemIndexNoMediaFile = 33,
        TreeItemNotFound = 34,
        InvalidTreeItemIndex = 35,
        NoThumbnailAvailable = 36,
        EncryptionKeyNotValid = 37,
        EncryptionPolicyNotValid = 38,
        NoEncryptionManager = 39,
        InvalidMessageId = 40,
        WatchedFolderUnkownProperty = 41,
        FolderNotWatched = 42,
        Unknown = 43,
        NoProject = 44,
        HostInvalidAttributeName = 45
    }

    enum ParamKind
    {
        No = 0,
        Opacity = 1,
        Mesh = 2,
        Media = 3,
        Inpoint = 4,
        Outpoint = 5,
        Transport = 6,
        XPos = 8,
        YPos = 9,
        ZPos = 10,
        XAngle = 11,
        YAngle = 12,
        ZAngle = 13,
        XScale = 14,
        YScale = 15,
        ZScale = 16,
        XAxis = 25,
        YAxis = 26,
        ZAxis = 27,
        XOffset = 29,
        YOffset = 30,
        KSL = 32,
        Kslr = 33,
        Ksr = 34,
        Ksrr = 35,
        Kst = 36,
        Kstr = 37,
        Ksb = 38,
        Ksbr = 39,
        LinX = 40,
        LinY = 41,
        Sel = 42,
        Selc = 43,
        Ser = 44,
        Serc = 45,
        Set = 46,
        Setc = 47,
        Seb = 48,
        Sebc = 49,
        Volume = 50,
        X = 51,
        Z = 52,
        RoomSize = 53,
        Ambience = 54,
        Diffusion = 55,
        BlendMode = 56,
        FxHue = 57,
        FxSaturation = 58,
        FxBrightness = 59,
        MultiFxList = 60,
        VideoSpeed = 61,
        AudioPan = 62,
        RotPivotXPos = 63,
        RotPivotYPos = 64,
        RotPivotZPos = 65,
        ScalePivotXPos = 66,
        ScalePivotYPos = 67,
        ScalePivotZPos = 68,
        XRotSpeed = 69,
        YRotSpeed = 70,
        ZRotSpeed = 71,
        CamTargetXPos = 72,
        CamTargetYPos = 73,
        CamTargetZPos = 74,
        CamFov = 75,
        CamNearPlane = 76,
        CamFarPlane = 77,
        CamAspect = 78,
        CamZRoll = 79,
        CamPostBypass = 80,
        CamProjMode = 81,
        ParticleGravity = 82,
        ParticleSpawnRate = 83,
        ParticleSpeed = 84,
        ParticleTimeToLive = 85,
        ParticleWind = 86,
        ParticleWindPosX = 87,
        ParticleWindPosY = 88,
        ParticleWindPosZ = 89,
        ParticleWindRotX = 90,
        ParticleWindRotY = 91,
        ParticleWindRotZ = 92,
        ParticleEmitterType = 93,
        ParticleEmitterRadius = 94,
        ParticleEmitterRadiusOption = 95,
        ParticleMass = 96,
        ParticleEmissionAngle = 97,
        ParticleAlignment = 98,
        ParticleDrag = 99,
        ParticleEmissionRange = 100,
        CamState = 101,
        AudioVolume = 102,
        ParticleColor = 103,
        ParticleOpacity = 104,
        Selm = 105,
        Selmw = 106,
        Serm = 107,
        Sermw = 108,
        Setm = 109,
        Setmw = 110,
        Sebm = 111,
        Sebmw = 112,
        ParticleXScale = 113,
        ParticleYScale = 114,
        ParticleZScale = 115,
        PsOpacity = 116,
        ParticleRotationX = 117,
        ParticleRotationY = 118,
        ParticleRotationZ = 119,
        XRotMode = 120,
        YRotMode = 121,
        ZRotMode = 122,
        LightXPos = 123,
        LightYPos = 124,
        LightZPos = 125,
        LightTargetXPos = 126,
        LightTargetYPos = 127,
        LightTargetZPos = 128,
        LightAngle = 129,
        LightMedia = 130,
        LightIntensity = 131,
        LightColorRed = 132,
        LightColorGreen = 133,
        LightColorBlue = 134,
        LightAspect = 135,
        LightZRoll = 136,
        LightTolerance = 137,
        ShadowSoftness = 138,
        WidgetValue1 = 140,
        WidgetValue2 = 141,
        WidgetValue3 = 142,
        WidgetValue4 = 143,
        WidgetValue5 = 144,
        WidgetValue6 = 145,
        WidgetValue7 = 146,
        WidgetValue8 = 147,
        WidgetValue9 = 148,
        WidgetValue10 = 149,
        WidgetValue11 = 150,
        WidgetValue12 = 151,
        MatrixMix = 152,
        MatrixTexture = 153,
        MatrixPatch = 154,
        PointerLoopInPoint = 155,
        PointerOutDelay = 156,
        PointerOffsetX = 157,
        PointerOffsetY = 158,
        RtClearColorRed = 159,
        RtClearColorGreen = 160,
        RtClearColorBlue = 161,
        RtClearColorAlpha = 162,
        GenPerspTargetPt1X = 163,
        GenPerspTargetPt1Y = 164,
        GenPerspTargetPt1Z = 165,
        GenPerspTargetPt2X = 166,
        GenPerspTargetPt2Y = 167,
        GenPerspTargetPt2Z = 168,
        GenPerspTargetPt3X = 169,
        GenPerspTargetPt3Y = 170,
        GenPerspTargetPt3Z = 171,
        EngineGlobalParam = 172,
        BrowserUrl = 173,
        CameraPre = 174,
        LightProjMode = 175,
        DefaultMeshShadingWireRed = 176,
        DefaultMeshShadingWireGreen = 177,
        DefaultMeshShadingWireBlue = 178,
        DefaultMeshShadingWireAlpha = 179,
        DefaultMeshShadingFillRed = 180,
        DefaultMeshShadingFillGreen = 181,
        DefaultMeshShadingFillBlue = 182,
        DefaultMeshShadingFillAlpha = 183,
        DefaultMeshShadingWireWidth = 184,
        DefaultMeshShadingAmbient = 185,
        DefaultMeshShadingDiffuse = 186,
        DefaultMeshShadingSpecular = 187,
        DefaultMeshShadingShininess = 188,
        DefaultMeshShadingWireBrightnessFactor = 189
    }

    enum ClxHardware
    {
        FaderExtension = 0,
        JogShuttle = 1
    }

    enum Consistency
    {
        Inconsistent = 1,
        Consistent = 0
    }

    enum SelectionMode
    {
        SetSelection = 0,
        AddSelection = 1,
        Unselect = 2,
        UnselectAll = 3
    }

    enum WatchFolderProperty
    {
        IncludeSubdirectories = 1,
        DeleteInProject = 2,
        DeleteInClients = 3
    }

    enum TransportMode
    {
        Play = 1,
        Pause = 3,
        Stop = 2
    }

    enum SequenceSmpteMode
    {
        No = 0,
        Send = 1,
        Receive = 2
    }

    enum SequenceSmpteStopMode
    {
        No = 0,
        Stop = 1,
        Pause = 2,
        Continue = 3
    }

    enum MediaOption
    {
        AnisotropicFiltering = 1,
        IgnoreThumbnail = 2,
        VideoAlphaChannel = 4,
        FluidFrame = 8,
        OptimizeMpegColorspace = 16,
        Underscan = 32,
        OptimizeLooping = 64,
        MuteSound = 128
    }


    /// <summary>
    /// Contains extension methods for conversion between native format and byte arrays
    /// </summary>
    public static class PBUtil
    {
        public static byte PbAutoChecksum(this byte[] message)
        {
            if (message.Length < 17) throw new ArgumentException("Byte array is not a PbAuto header! Length != 17");
            var checksum = 0;
            for(int i=4;i<16;i++)
            {
                checksum += message[i];
            }
            return (byte)(checksum % 255);
        }
        public static long GetInt64(this byte[] bytes, int offset = 0)
        {
            byte[] valueBytes = new byte[8];
            Array.Copy(bytes, offset, valueBytes, 0, 8);
            if (BitConverter.IsLittleEndian) { Array.Reverse(valueBytes); }
            return BitConverter.ToInt64(valueBytes, 0);
        }

        public static int GetInt32(this byte[] bytes, int offset = 0)
        {
            byte[] valueBytes = new byte[4];
            Array.Copy(bytes, offset, valueBytes, 0, 4);
            if (BitConverter.IsLittleEndian) { Array.Reverse(valueBytes); }
            return BitConverter.ToInt32(valueBytes, 0);
        }

        public static short GetInt16(this byte[] bytes, int offset = 0)
        {
            byte[] valueBytes = new byte[2];
            Array.Copy(bytes, offset, valueBytes, 0, 2);
            if (BitConverter.IsLittleEndian) { Array.Reverse(valueBytes); }
            return BitConverter.ToInt16(valueBytes, 0);
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
        private List<byte> listBytes;
        private byte[] readBytes;

        // Position for Reading
        private int position = 0;

        // Constructors
        public ByteUtil()
        {
            listBytes = new List<byte>();
        }
        public ByteUtil(byte[] data)
        {
            readBytes = data;
        }

        // default responses
        public static ByteUtil ErrorNotConnected()
        {
            ByteUtil b = new ByteUtil();
            b.WriteShort(-1);
            b.WriteInt((int)ErrorCode.NoConnection);
            return b;
        }
        public static ByteUtil ErrorWrongMessageReturned()
        {
            ByteUtil b = new ByteUtil();
            b.WriteShort(-1);
            b.WriteInt((int)ErrorCode.WrongMessageReturned);
            return b;
        }
        public static ByteUtil ResponseOk()
        {
            ByteUtil b = new ByteUtil();
            b.WriteShort(0);
            return b;
        }     

        public void CopyTo(byte[] bytes, int offset) { listBytes.CopyTo(bytes, offset); }
        public int Length { get { return listBytes.Count; } }

        // Writing
        public void WriteBool(bool value) { listBytes.Add((byte)(value ? 1 : 0)); }
        public void WriteByte(byte value) { listBytes.Add(value); }
        public void WriteShort(short value) { listBytes.AddRange(value.GetBytesNetworkOrder() ); }
        public void WriteInt(int value) { listBytes.AddRange(value.GetBytesNetworkOrder()); }
        public void WriteInt64(long value) { listBytes.AddRange(value.GetBytesNetworkOrder()); }
        public void WriteDouble(double value) { listBytes.AddRange(BitConverter.GetBytes(value)); }
        public void WriteStringNarrow(string value) { WriteShort((short)value.Length); listBytes.AddRange(Encoding.UTF8.GetBytes(value)); }
        public void WriteStringWide(string value) { WriteShort((short)value.Length); listBytes.AddRange(Encoding.BigEndianUnicode.GetBytes(value)); }
        public void WriteByteBuffer(byte[] value) { WriteInt(value.Length); listBytes.AddRange(value); }
        public void WriteIntBuffer(int[] value) { WriteInt(value.Length); foreach (var i in value) { listBytes.AddRange(i.GetBytesNetworkOrder()); } }

        // Reading
        private byte[] _readBlock(int length) { var ret = new byte[length]; Array.Copy(readBytes, position, ret, 0, length);position += length;return ret; }
        public bool ReadBool() { var result = readBytes[position];position++;return result == 1; }
        public byte ReadByte() { var result = readBytes[position];position++;return result; }
        public short ReadShort() { return _readBlock(2).GetInt16(); }
        public int ReadInt() { return _readBlock(4).GetInt32(); }
        public long ReadInt64() { return _readBlock(8).GetInt64(); }
        public double ReadDouble() { return BitConverter.ToDouble(_readBlock(8), 0); }
        public string ReadStringNarrow() { int length = ReadShort(); return Encoding.UTF8.GetString(_readBlock(length)); }
        public string ReadStringWide() { int length = ReadShort(); return Encoding.BigEndianUnicode.GetString(_readBlock(length)); }
        public byte[] ReadByteBuffer() { int length = ReadInt(); return _readBlock(length); }
        public int[] ReadIntBuffer() { int length = ReadInt(); int[] result = new int[length]; for (int i = 0;i < length; i++) { result[i] = _readBlock(4).GetInt32(); }; return result; }
    }

    /// <summary>
    /// Interface that allows PbAuto to transmit messages
    /// </summary>
    public interface IConnector : IDisposable
    {
        // Returns a ByteUtil instance
        ByteUtil Send(ByteUtil data, bool hasResponse);

        // returns false if the connection is known to be broken, may return false positives
        bool IsConnected();
    }

    /// <summary>
    /// Implements the Connector interface using TCP as the underlying protocol
    /// </summary>
    public class TcpConnector : IConnector
    {
        private bool disposed = false;
        private string ip;
        private int domain;
        private TcpClient tcpClient;
        private const int PORT = 6211;

        public TcpConnector(string ip, int domain = 0)
        {
            this.ip = ip;
            this.domain = domain;
            tcpClient = new TcpClient();
            tcpClient.NoDelay = true;

            System.Net.IPAddress ipAddress;
            try
            {
                ipAddress = System.Net.IPAddress.Parse(ip);
            }
            catch(FormatException)
            {
                return;
            }

            tcpClient.Connect(ipAddress, PORT);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if(disposed)
            {
                return;
            }

            if(disposing)
            {
                tcpClient.Close();
            }

            disposed = true;
        }

        ~TcpConnector()
        {
            Dispose(false);
        }

        /// If successful returns ByteUtil
        public ByteUtil Send(ByteUtil data, bool hasResponse)
        {
            // quick check to see if client connection is known to be broken
            if(!tcpClient.Client.Connected)
            {
                return ByteUtil.ErrorNotConnected();
            }

            byte[] header = new byte[17] {
                (byte)'P', (byte)'B', (byte)'A', (byte)'U', //# header consists of magic "PBAU" sequence
                1,                                          //# + protocol version (byte, currently 1)
                0, 0, 0, 0,                                 //# + domain id (integer)
                0, 0,                                       //# + message size (short)
                0, 0, 0, 0,                                 //# + connection id (int, user definable, defaults to 0)
                0,                                          //# + protocol flag (byte, 0 for TCP)
                0,                                          //# + checksum
            };

            // Write domain id to header
            domain.GetBytesNetworkOrder().CopyTo(header, 5);
            // Write message length
            ((short)data.Length).GetBytesNetworkOrder().CopyTo(header, 9);
            // calculate checksum and Write
            header[16] = header.PbAutoChecksum();

            var message = new byte[17 + data.Length];
            header.CopyTo(message, 0);
            data.CopyTo(message, 17);

            var stream = tcpClient.GetStream();
            stream.Write(message, 0, message.Length);
            stream.Flush();

            if( !hasResponse )
            {
                return ByteUtil.ResponseOk();
            }

            int bytesRead = 0;
            while(bytesRead < 17)
            {
                bytesRead += stream.Read(header, bytesRead, 17 - bytesRead);
            }

            if(header[0] != 0x50 || header[1] != 0x42 || header[2] != 0x41 || header[3] != 0x55 || header.PbAutoChecksum() != header[16])
            {
                return ByteUtil.ErrorWrongMessageReturned();
            }

            int messageLength = header.GetInt16(9);
            message = new byte[messageLength];

            bytesRead = 0;
            while (bytesRead < messageLength)
            {
                bytesRead += stream.Read(message, bytesRead, messageLength - bytesRead);
            }

            return new ByteUtil(message);
        }

        public bool IsConnected()
        {
            return tcpClient.Client.Connected;
        }
    }
}