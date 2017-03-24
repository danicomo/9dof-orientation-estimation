using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace ControlLibrary.MKI062V2
{
    public struct FrameComponents
    {
        public Int16 X;
        public Int16 Y;
        public Int16 Z;
    }
    public struct Rotation
    {
        public float Roll;
        public float Pitch;
        public float Yaw;
    }
    //public struct Quaternion
    //{
    //    public float Q0;
    //    public float Q1;
    //    public float Q2;
    //    public float Q3;
    //}

    [StructLayout(LayoutKind.Sequential, Pack=2)]
    public struct INEMO2_FrameData
    {
        public UInt32 Size;
        public INEMO2_FRAME_VALID_FIELD ValidFields;
        public UInt32 TimeStamp;
        public FrameComponents Accelometer;
        public FrameComponents Gyroscope;
        public FrameComponents Magnetometer;
        public UInt16 Pressure;
        public Int16 Temperature;
        public Rotation Rot;
        //public Quaternion Quat;

        public double PressureInMbar
        {
            get { return ((double)((double)Pressure) / 10); }
        }
        public double TemperatureInC
        {
            get { return ((double)((double)Temperature) / 10); }
        }
        public double TimeStampInMillis
        {
            get { return (double)TimeStamp / 1000; }
        }

        public object[] ToObjArray()
        {
            object[] objRet = null;

            if (this.ValidFields == INEMO2_FRAME_VALID_FIELD.FRAME_DATA_VALID_FIELD_ALL )
            {
                objRet = new object[19];
                objRet[0] = TimeStampInMillis;
                objRet[1] = Accelometer.X;
                objRet[2] = Accelometer.Y;
                objRet[3] = Accelometer.Z;
                objRet[4] = Gyroscope.X;
                objRet[5] = Gyroscope.Y;
                objRet[6] = Gyroscope.Z;
                objRet[7] = Magnetometer.X;
                objRet[8] = Magnetometer.Y;
                objRet[9] = Magnetometer.Z;
                objRet[10] = PressureInMbar;
                objRet[11] = TemperatureInC;
                objRet[12] = Rot.Roll;
                objRet[13] = Rot.Pitch;
                objRet[14] = Rot.Yaw;
                //objRet[15] = Quat.Q0;
                //objRet[16] = Quat.Q1;
                //objRet[17] = Quat.Q2;
                //objRet[18] = Quat.Q3;
            }
            else if (this.ValidFields == INEMO2_FRAME_VALID_FIELD.FRAME_DATA_VALID_FIELD_ALL_SENS)
            {
                objRet = new object[12];
                objRet[0] = TimeStampInMillis;
                objRet[1] = Accelometer.X;
                objRet[2] = Accelometer.Y;
                objRet[3] = Accelometer.Z;
                objRet[4] = Gyroscope.X;
                objRet[5] = Gyroscope.Y;
                objRet[6] = Gyroscope.Z;
                objRet[7] = Magnetometer.X;
                objRet[8] = Magnetometer.Y;
                objRet[9] = Magnetometer.Z;
                objRet[10] = PressureInMbar;
                objRet[11] = TemperatureInC;
            }
            return objRet;
        }
    }
    public enum INEMO2_FRAME_VALID_FIELD : uint
    {
        FRAME_DATA_VALID_FIELD_NONE        = 0x00000000,
        FRAME_DATA_VALID_FIELD_ACC         = 0x00000001,
        FRAME_DATA_VALID_FIELD_GYRO        = 0x00000002,
        FRAME_DATA_VALID_FIELD_MAG         = 0x00000004,
        FRAME_DATA_VALID_FIELD_PRES        = 0x00000008,
        FRAME_DATA_VALID_FIELD_TEMP        = 0x00000010,
        FRAME_DATA_VALID_FIELD_ALL_SENS    = 0x0000001F,
        FRAME_DATA_VALID_FIELD_ROTAT       = 0x00000020,
        FRAME_DATA_VALID_FIELD_QUAT        = 0x00000040,
        FRAME_DATA_VALID_FIELD_ALL         = 0x0000007F
    }

    public struct INEMO2_DeviceHandle
    {
        Int32 hHandle;

        public Int32 Handle
        {
            get { return hHandle; }
        }

        public const Int32 INEMO2_DEVICE_HANDLE_INVALID = -1;
        public void FreeHandle()
        {
            hHandle = INEMO2_DEVICE_HANDLE_INVALID;
        }
        
    }

    public enum INEMO2_DeviceError : uint
    {
         INEMO2_ERROR_NONE                       = 0x00000000,
         INEMO2_ERROR_INVALID_HANDLE             = 0x00000001,
         INEMO2_ERROR_HANDLE_NOT_OPENED			 = 0x00000002,
         INEMO2_ERROR_INVALID_PARAMETER			 = 0x00000003,
         INEMO2_ERROR_DEVICE_ALREADY_CONNECT	 = 0x00000004,
         INEMO2_ERROR_DEVICE_FAIL_TO_OPEN        = 0x00000005,
         INEMO2_ERROR_ON_SET_DEVICE_FUNCTION     = 0x00000006,
         INEMO2_ERROR_ON_SEND_DATA_FRAME         = 0x00000007,
         INEMO2_ERROR_ON_READ_DATA_FRAME         = 0x00000008,
         INEMO2_ERROR_ON_READ_INVALID_MEX_ID     = 0x00000009,
         INEMO2_ERROR_ON_READ_DATA_MORE_BUFFER   = 0x0000000A,
         INEMO2_ERROR_CMD_UNKWOUN                = 0x0000000B,
         INEMO2_ERROR_GET_INFO_UNKWOUN           = 0x0000000C,
         INEMO2_ERROR_NO_TRACE_DATA_RECEIVED     = 0x0000000D,
         INEMO2_ERROR_NO_FRAME_DATA_RECEIVED     = 0x0000000E,
         INEMO2_ERROR_INVALID_FRAME_RECEIVED     = 0x0000000F,
         INEMO2_ERROR_NO_PROCESS_ADDRESS         = 0x000000FF,
         INEMO2_ERROR_NACK_DEVICE_FORBIDDEN      = 0x00000100,
         INEMO2_ERROR_NACK_DEVICE_CMD_UNSUP      = 0x00000101,
         INEMO2_ERROR_NACK_DEVICE_OUT_OF_RANGE   = 0x00000102,
         INEMO2_ERROR_NACK_DEVICE_CMD_NOT_EXEC   = 0x00000103,
         INEMO2_ERROR_NACK_DEVICE_WRONG_SYNTAX   = 0x00000104,
         INEMO2_ERROR_NACK_DEVICE_NOT_CONNECT    = 0x00000105
    }
    public enum INEMO2_DeviceCommand : uint
    {
        INEMO2_CMD_START_ACQUISTION = 0x01,
        INEMO2_CMD_STOP_ACQUISTION,
        INEMO2_CMD_TRACE_ON,
        INEMO2_CMD_TRACE_OFF,
        INEMO2_CMD_RESET_BOARD,
        INEMO2_CMD_ENTER_DFU_MODE,
        INEMO2_CMD_LED_ON,
        INEMO2_CMD_LED_OFF
    }

    public enum INEMO2_DeviceInfoText
    {
        INEMO2_INFOTEXT_MCUID = 0x01,
        INEMO2_INFOTEXT_FW_VERSION,
        INEMO2_INFOTEXT_HW_VERSION,
        INEMO2_INFOTEXT_AHRS_LIBRARY
    }

    public enum INEMO2_DeviceBuffers
    {
        INEMO2_BUFFER_DATA_SAMPLE = 0x00,
        INEMO2_BUFFER_TRACE = 0x01
    }
    public enum INEMO2_OUTPUT_MODE
    {
        INEMO2_OUTPUT_MODE_DEFAULT     = 0x00,
        INEMO2_OUTPUT_MODE_RAW         = 0x01,
        INEMO2_OUTPUT_MODE_AHRS        = 0x04
    }
    public enum INEMO2_OUTPUT_DATA
    {
        INEMO2_OUTPUT_DATA_ACC    = 0x01,
        INEMO2_OUTPUT_DATA_GYRO   = 0x02,
        INEMO2_OUTPUT_DATA_MAG    = 0x04,
        INEMO2_OUTPUT_DATA_PRESS  = 0x08,
        INEMO2_OUTPUT_DATA_TEMP   = 0x10,
        INEMO2_OUTPUT_DATA_ALL    = 0x1F
    }
    public enum INEMO2_OUTPUT_TYPE
    {
        INEMO2_OUTPUT_TYPE_USB = 0x00
    }

    public struct INEMO2_Output
    {
       public INEMO2_OUTPUT_MODE Mode;
	   public INEMO2_OUTPUT_DATA Data;
	   public uint Frequency;
	   public INEMO2_OUTPUT_TYPE Type;
	   public uint Samples;
    }
    public enum INEMO2_MODULES
    {
	    INEMO2_MODULES_NONE      = 0x00000000,
	    INEMO2_MODULES_AHRS      = 0x00000001,
	    INEMO2_MODULES_COMPASS   = 0x00000002,
	    INEMO2_MODULES_ALTIMETER = 0x00000004,
	    INEMO2_MODULES_TRACE     = 0x00000008,
	    INEMO2_MODULES_FAT       = 0x00000010,
    }
    public enum INEMO2_SENSORS
    {
	    INEMO2_SENSORS_ACC      = 0x00,
	    INEMO2_SENSORS_MAG      = 0x01,
	    INEMO2_SENSORS_GYRO_RP  = 0x02,
	    INEMO2_SENSORS_GYRO_Y   = 0x03,
	    INEMO2_SENSORS_PRESS    = 0x04,
	    INEMO2_SENSORS_TEMP     = 0x05,
    }

    public class INEMO2_Device
    {
        
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceHandle INEMO2_Connect(string lpszConnectionString);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_Disconnect(INEMO2_DeviceHandle hHandle);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern UInt32 INEMO2_IsConnect(INEMO2_DeviceHandle hHandle);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_Command(INEMO2_DeviceHandle hHandle, INEMO2_DeviceCommand cmd);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_GetInfoText(INEMO2_DeviceHandle hHandle, INEMO2_DeviceInfoText info, byte[] data, ref Int32 lpnSize);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_SetOutput(INEMO2_DeviceHandle hHandle, ref INEMO2_Output output);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_GetOutput(INEMO2_DeviceHandle hHandle, ref INEMO2_Output output);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_GetDataSample(INEMO2_DeviceHandle hHandle, ref INEMO2_FrameData pFrame);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_GetTrace(INEMO2_DeviceHandle hHandle, byte[] TraceData, ref Int32 lpnSize);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_GetBufferUsage(INEMO2_DeviceHandle hHandle, INEMO2_DeviceBuffers typeBuffer, ref double pPerc);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_GetModules(INEMO2_DeviceHandle hDevHandle,  ref INEMO2_MODULES pModules);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_SetReg(INEMO2_DeviceHandle hDevHandle, INEMO2_SENSORS sensor, UInt32 reg, ref UInt32 pRegValue, UInt32 sizeRegValue);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_GetReg(INEMO2_DeviceHandle hDevHandle, INEMO2_SENSORS sensor, UInt32 reg, ref UInt32 pRegValue, ref UInt32 pSizeRegValue);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_RestoreDefault(INEMO2_DeviceHandle hDevHandle, INEMO2_SENSORS sensor, UInt32 reg);
        [DllImport("iNEMO2_SDK.dll")]
        private static extern INEMO2_DeviceError INEMO2_Identify(INEMO2_DeviceHandle hDevHandle);

        private INEMO2_DeviceHandle m_DeviceHandle;
        private bool m_AHRS_Enabled;

        
        public INEMO2_Device()
        {
            m_DeviceHandle.FreeHandle();
            AHRS_Enabled = false;
        }

        public INEMO2_DeviceError Connect(string strConnection)
        {
            m_DeviceHandle = INEMO2_Connect(strConnection);
            
            if (m_DeviceHandle.Handle == INEMO2_DeviceHandle.INEMO2_DEVICE_HANDLE_INVALID)
                return INEMO2_DeviceError.INEMO2_ERROR_INVALID_HANDLE;

            return INEMO2_DeviceError.INEMO2_ERROR_NONE;
        }

        public INEMO2_DeviceError Disconnect()
        {
            INEMO2_DeviceError nRet;
            nRet = INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_LED_OFF);
            nRet = INEMO2_Disconnect(m_DeviceHandle);
            m_DeviceHandle.FreeHandle();
            return nRet;
        }

        public bool IsConnected()
        {
            return (bool)(INEMO2_IsConnect(m_DeviceHandle) != 0);
        }
        
        public bool AHRS_Enabled
        {
            set
            {
                m_AHRS_Enabled = value;
            }
            get 
            {
                return m_AHRS_Enabled; 
            }
        }

        public INEMO2_DeviceError GetInfo(INEMO2_DeviceInfoText info, out string strVersion)
        {
            INEMO2_DeviceError nRet;
            int strSize = 512;
            byte[] strTemp = new byte[strSize];

            nRet = INEMO2_GetInfoText(m_DeviceHandle, info, strTemp,  ref strSize);
            
            strVersion = "";
            if (nRet == INEMO2_DeviceError.INEMO2_ERROR_NONE)
            {
                for (int i = 0; i < strSize && strTemp[i] > 0; i++)
                    strVersion += (Char)strTemp[i];
            }

            return nRet;
        }
        
        public INEMO2_DeviceError Start(int mode, int nFreq, int nSamples)
        {
            INEMO2_Output OutMode;
            INEMO2_DeviceError nRet;

            OutMode.Frequency = (uint)nFreq;
            OutMode.Samples = (uint)nSamples;
            OutMode.Type = INEMO2_OUTPUT_TYPE.INEMO2_OUTPUT_TYPE_USB;
            OutMode.Data = INEMO2_OUTPUT_DATA.INEMO2_OUTPUT_DATA_ALL;
            OutMode.Mode = INEMO2_OUTPUT_MODE.INEMO2_OUTPUT_MODE_DEFAULT;
            if (mode == 1)
                OutMode.Mode = INEMO2_OUTPUT_MODE.INEMO2_OUTPUT_MODE_AHRS;

            INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_LED_ON);

            nRet = INEMO2_SetOutput(m_DeviceHandle, ref OutMode);
            if (nRet == INEMO2_DeviceError.INEMO2_ERROR_NONE)
                nRet = INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_START_ACQUISTION);

            

            return nRet;
        }

        public INEMO2_DeviceError Led_ON()
        {
            return INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_LED_ON);
        }


        public INEMO2_DeviceError Trace(bool bTrace)
        {
            if (bTrace)
                return INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_TRACE_ON);
            else
                return INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_TRACE_OFF);
        }

        public INEMO2_DeviceError EnterDFUMode()
        {
            INEMO2_DeviceError nRet;
            nRet = INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_ENTER_DFU_MODE);
            return nRet;
        }

        public INEMO2_DeviceError ResetDevice()
        {
            return INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_RESET_BOARD); 
        }

        public INEMO2_DeviceError Stop()
        {
            INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_LED_OFF);
            return INEMO2_Command(m_DeviceHandle, INEMO2_DeviceCommand.INEMO2_CMD_STOP_ACQUISTION);
        }

        public INEMO2_DeviceError GetSample(ref INEMO2_FrameData data)
        {
            return INEMO2_GetDataSample(m_DeviceHandle, ref data);
        }

        public INEMO2_DeviceError GetTrace(out string strVersion)
        {
            INEMO2_DeviceError nRet;
            int strSize = 512;
            byte[] strTemp = new byte[strSize];

            nRet = INEMO2_GetTrace(m_DeviceHandle, strTemp, ref strSize);

            strVersion = "";
            if (nRet == INEMO2_DeviceError.INEMO2_ERROR_NONE)
            {
                for (int i = 0; i < strSize && strTemp[i] > 0; i++)
                    strVersion += (Char)strTemp[i];
            }

            return nRet;
        }

        public double GetBufferUsage(INEMO2_DeviceBuffers typeBuffer)
        {
            double dbPerc = -1;

            INEMO2_GetBufferUsage(m_DeviceHandle, typeBuffer, ref dbPerc);

            return dbPerc;
        }

        public INEMO2_DeviceError GetModules(ref INEMO2_MODULES modules)
        {
            return INEMO2_GetModules(m_DeviceHandle, ref modules);
        }
        public INEMO2_DeviceError Identify()
        {
            return INEMO2_Identify(m_DeviceHandle);
        }

        public INEMO2_DeviceError SetReg(INEMO2_SENSORS sensor, UInt32 reg, ref UInt32 pRegValue, UInt32 sizeRegValue)
        {
            return INEMO2_SetReg(m_DeviceHandle, sensor, reg, ref pRegValue, sizeRegValue);
        }

        public INEMO2_DeviceError GetReg(INEMO2_SENSORS sensor, UInt32 reg, ref UInt32 pRegValue, ref UInt32 pSizeRegValue)
        {
            return INEMO2_GetReg(m_DeviceHandle,  sensor,  reg, ref pRegValue, ref pSizeRegValue);
        }
        public INEMO2_DeviceError RestoreDefault(INEMO2_SENSORS sensor, UInt32 reg)
        {
            return INEMO2_RestoreDefault(m_DeviceHandle, sensor, reg);
        }
    }
}
