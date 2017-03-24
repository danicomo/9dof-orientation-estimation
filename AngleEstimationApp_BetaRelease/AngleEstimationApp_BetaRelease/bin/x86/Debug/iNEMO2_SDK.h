/******************** (C) COPYRIGHT 2010 STMicroelectronics ********************
* File Name          : iNEMO2_SDK.h
* Author             : IMS Systems Lab 
* Date First Issued  : 01 June 2010
* Description        : Header file for the iNEMO2 sdk dll
********************************************************************************
* History:
* 2009-05-01 ver. 1.0.0.0 created
* 2010-06-01 ver. 1.2.0.0 Optimization
* 2010-09-09 ver. 1.4.0.0 Add APIs
********************************************************************************
* THE PRESENT SOFTWARE WHICH IS FOR GUIDANCE ONLY AIMS AT PROVIDING CUSTOMERS
* WITH CODING INFORMATION REGARDING THEIR PRODUCTS IN ORDER FOR THEM TO SAVE TIME.
* AS A RESULT, STMICROELECTRONICS SHALL NOT BE HELD LIABLE FOR ANY DIRECT,
* INDIRECT OR CONSEQUENTIAL DAMAGES WITH RESPECT TO ANY CLAIMS ARISING FROM THE
* CONTENT OF SUCH SOFTWARE AND/OR THE USE MADE BY CUSTOMERS OF THE CODING
* INFORMATION CONTAINED HEREIN IN CONNECTION WITH THEIR PRODUCTS.
*
* THIS SOURCE CODE IS PROTECTED BY A LICENSE.
* FOR MORE INFORMATION PLEASE CAREFULLY READ THE LICENSE AGREEMENT FILE LOCATED
* IN THE ROOT DIRECTORY OF THIS FIRMWARE PACKAGE.
*******************************************************************************/


#ifndef __INEMO2_SDK_H_
#define __INEMO2_SDK_H_

// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the INEMO2_SDK_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// INEMO2_SDK_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef INEMO2_SDK_EXPORTS
#define INEMO2_SDK_API __declspec(dllexport) 
#else
#define INEMO2_SDK_API __declspec(dllimport) 
#endif

/*******************************************************************************
* The INEMO2_DEVICE_HANDLE Handle device
*******************************************************************************/
typedef unsigned int INEMO2_DEVICE_HANDLE;
#define INEMO2_DEVICE_HANDLE_INVALID			-1

/*******************************************************************************
* The INEMO2_ERROR List of available error
*******************************************************************************/
typedef unsigned int INEMO2_ERROR;
#define INEMO2_ERROR_NONE						0x00000000
#define INEMO2_ERROR_INVALID_HANDLE				0x00000001
#define INEMO2_ERROR_HANDLE_NOT_OPENED			0x00000002
#define INEMO2_ERROR_INVALID_PARAMETER			0x00000003
#define INEMO2_ERROR_DEVICE_ALREADY_CONNECT		0x00000004
#define INEMO2_ERROR_DEVICE_FAIL_TO_OPEN        0x00000005
#define INEMO2_ERROR_ON_SET_DEVICE_FUNCTION     0x00000006
#define INEMO2_ERROR_ON_SEND_DATA_FRAME         0x00000007
#define INEMO2_ERROR_ON_READ_DATA_FRAME         0x00000008
#define INEMO2_ERROR_ON_READ_INVALID_MEX_ID     0x00000009        
#define INEMO2_ERROR_ON_READ_DATA_MORE_BUFFER   0x0000000A        
#define INEMO2_ERROR_CMD_UNKWOUN                0x0000000B   
#define INEMO2_ERROR_GET_INFO_UNKWOUN           0x0000000C
#define INEMO2_ERROR_NO_TRACE_DATA_RECEIVED     0x0000000D
#define INEMO2_ERROR_NO_FRAME_DATA_RECEIVED     0x0000000E
#define INEMO2_ERROR_INVALID_FRAME_RECEIVED     0x0000000F
#define INEMO2_ERROR_ON_DATA_RECEIVED           0x00000010
#define INEMO2_ERROR_NO_PROCESS_ADDRESS         0x000000FF
#define INEMO2_ERROR_NACK_DEVICE_FORBIDDEN      0x00000100
#define INEMO2_ERROR_NACK_DEVICE_CMD_UNSUP      0x00000101
#define INEMO2_ERROR_NACK_DEVICE_OUT_OF_RANGE   0x00000102
#define INEMO2_ERROR_NACK_DEVICE_CMD_NOT_EXEC   0x00000103
#define INEMO2_ERROR_NACK_DEVICE_WRONG_SYNTAX   0x00000104
#define INEMO2_ERROR_NACK_DEVICE_NOT_CONNECT    0x00000105



#pragma pack(1)

#define INEMO2_OUTPUT_MODE_DEFAULT      0x00
#define INEMO2_OUTPUT_MODE_RAW          0x01
#define INEMO2_OUTPUT_MODE_AHRS         0x04

#define INEMO2_OUTPUT_DATA_ACC          0x01
#define INEMO2_OUTPUT_DATA_GYRO         0x02
#define INEMO2_OUTPUT_DATA_MAG          0x04
#define INEMO2_OUTPUT_DATA_PRESS        0x08
#define INEMO2_OUTPUT_DATA_TEMP         0x10

#define INEMO2_OUTPUT_DATA_ALL          0x1F

#define INEMO2_OUTPUT_TYPE_USB          0x00

#define INEMO2_OUTPUT_FREQ_1               1
#define INEMO2_OUTPUT_FREQ_10             10
#define INEMO2_OUTPUT_FREQ_25             25
#define INEMO2_OUTPUT_FREQ_50             50   /*Default*/

#define INEMO2_OUTPUT_SAMPLES_CONTINOUS    0


/*******************************************************************************
* The INEMO2_OUTPUT_s describes the configuration output data for the iNEMO board V2
*    Mode        : output mode one of the above defines INEMO2_OUTPUT_MODE_xxxxx  
*    Data        : sensors data enabled for receiving INEMO2_OUTPUT_DATA_xxxxxx
*    Frequency   : frequency value of data sample output allowed value INEMO2_OUTPUT_FREQ_xxxx
*    Type        : the physical layer of communication available only INEMO2_OUTPUT_TYPE_USB
*    Samples     : the number of samples to get, 0 for continous mode
*******************************************************************************/
typedef struct INEMO2_OUTPUT_s
{
	unsigned int Mode;
	unsigned int Data;
	unsigned int Frequency;
	unsigned int Type;
	unsigned int Samples;
}INEMO2_OUTPUT_t, *PINEMO2_OUTPUT_t;


typedef struct Component_s
{
	short X;
	short Y;
	short Z;
} Component_t;

typedef struct Rotation_s
{
	float Roll;
	float Pitch;
	float Yaw;
}Rotation_t;

typedef struct Quaternion_s
{
	float Q1;
	float Q2;
	float Q3;
	float Q4;
}Quaternion_t;


/*******************************************************************************
* The FrameData_t describes the data received from the iNEMO board V2
*    Size            : Size in byte of this structure  
*    SampleTimeStamp : timestamp (milliseconds from start)
*    ValidFields     : maskbit field to define the valid field on the structure 
*    Accelerometer   : Accelerometer data component (3 axes vaules X, Y, Z) in mg unit
*    Gyroscope       : Gyroscope data component (3 axes vaules X, Y, Z) in dps unit
*    Magnetometer    : Magnetometer data component (3 axes vaules X, Y, Z) mGa unit
*    Pressure        : Pressure value in dmbar unit
*    Temperature     : Temperature value in dºC unit
*    Rotation        : R/P/Y information
*    Quad            : (Q1 Q2 Q3 Q4) - Quadernion information
*******************************************************************************/
typedef struct FrameData_s
{
	size_t Size;
	unsigned int ValidFields;
	unsigned int SampleTimeStamp;
	Component_t Accelerometer;
	Component_t Gyroscope;
	Component_t Magnetometer;
	unsigned short Pressure;
	short Temperature;
	Rotation_t Rotation;
	Quaternion_t Quaternion;
}FrameData_t, *PFrameData_t;

/*******************************************************************************
* The ValidField Flags
*******************************************************************************/
#define FRAME_DATA_VALID_FIELD_NONE        0x00000000

#define FRAME_DATA_VALID_FIELD_ACC         0x00000001
#define FRAME_DATA_VALID_FIELD_GYRO        0x00000002
#define FRAME_DATA_VALID_FIELD_MAG         0x00000004
#define FRAME_DATA_VALID_FIELD_PRES        0x00000008
#define FRAME_DATA_VALID_FIELD_TEMP        0x00000010
#define FRAME_DATA_VALID_FIELD_ROTAT       0x00000020
#define FRAME_DATA_VALID_FIELD_QUAT        0x00000040

#define FRAME_DATA_VALID_FIELD_ALL         0x0000007F

#pragma pack()

/*******************************************************************************
* The INEMO2_CMD_t are the implemented command for the device iNEMO
*******************************************************************************/
typedef enum INEMO2_CMD_e
{
	INEMO2_CMD_START_ACQUISTION = 0x01,  
	INEMO2_CMD_STOP_ACQUISTION,
	INEMO2_CMD_TRACE_ON,
	INEMO2_CMD_TRACE_OFF,
	INEMO2_CMD_RESET_BOARD,
	INEMO2_CMD_ENTER_DFU_MODE,
	INEMO2_CMD_LED_ON,
	INEMO2_CMD_LED_OFF
}INEMO2_CMD_t;

/*******************************************************************************
* The INEMO2_INFOTEXT_t are the information available from the iNEMO device
*******************************************************************************/
typedef enum INEMO2_INFOTEXT_e
{
	INEMO2_INFOTEXT_MCUID = 0x01,  
	INEMO2_INFOTEXT_FW_VERSION,
	INEMO2_INFOTEXT_HW_VERSION,
	INEMO2_INFOTEXT_AHRS_LIBRARY,
}INEMO2_INFOTEXT_t;

/*******************************************************************************
* The INEMO2_BUFFERS_t are fifo queue implemnted for data samples and trace
*******************************************************************************/
typedef enum INEMO2_BUFFERS_e
{
	INEMO2_BUFFER_DATA_SAMPLE = 0x00,
	INEMO2_BUFFER_TRACE = 0x01
}INEMO2_BUFFERS_t;

/*******************************************************************************
* The INEMO2_MODULES_t are modules available in the device
*******************************************************************************/
typedef enum INEMO2_MODULES_e
{
	INEMO2_MODULES_NONE      = 0x00000000,
	INEMO2_MODULES_AHRS      = 0x00000001,
	INEMO2_MODULES_COMPASS   = 0x00000002,
	INEMO2_MODULES_ALTIMETER = 0x00000004,
	INEMO2_MODULES_TRACE     = 0x00000008,
	INEMO2_MODULES_FAT       = 0x00000010,
}INEMO2_MODULES_t;

/*******************************************************************************
* The INEMO2_SENSORS_t are the sensor available in the device
*******************************************************************************/
typedef enum INEMO2_SENSORS_e
{
	INEMO2_SENSORS_ACC      = 0x00,
	INEMO2_SENSORS_MAG      = 0x01,
	INEMO2_SENSORS_GYRO_RP  = 0x02,
	INEMO2_SENSORS_GYRO_Y   = 0x03,
	INEMO2_SENSORS_PRESS    = 0x04,
	INEMO2_SENSORS_TEMP     = 0x05,
}INEMO2_SENSORS_t;


/*******************************************************************************
* Function Name  : INEMO2_Connect
* Description    : Connects the PC to the iNEMO device
* Input          : lpszConnectionString string connection. The format of string
*                :      must have the following format "DATABUFFERSIZE=1024, 
*                :      TRACEBUFFERSIZE=1024, PL=PL_001{PN=COMx, SENDMODE=B}"
*                :      where:
*                :      DATABUFFERSIZE specifies the size of samples data fifo, 
*                :      min 1(default 1024) [optional]
*                :      TRACEBUFFERSIZE specifies the size of trace data fifo, 
*                :      min 1(default 1024) [optional] 
*                :      PL_001 is the physical layer module (loaded dinamically) [mandatory]
*                :      COMx is the selected COM port to open [mandatory]
*                :      SENDMODE=B is a configuration parameter for PL_001 [mandatory]
* Return         : the handle of the device connected, if connection fail the 
*                :      function returns INEMO2_DEVICE_HANDLE_INVALID. 
*******************************************************************************/
INEMO2_SDK_API INEMO2_DEVICE_HANDLE INEMO2_Connect(const char *lpzStrConnection);

/*******************************************************************************
* Function Name  : INEMO2_Disconnect
* Description    : Disconnect the device from the PC
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_Disconnect(INEMO2_DEVICE_HANDLE hDevHandle);

/*******************************************************************************
* Function Name  : INEMO2_IsConnect
* Description    : Check if the device is connected
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
* Return         : 0 = device not connected,   0 !=  device is connected
*******************************************************************************/
INEMO2_SDK_API DWORD INEMO2_IsConnect(INEMO2_DEVICE_HANDLE hDevHandle);

/*******************************************************************************
* Function Name  : INEMO2_Command
* Description    : Send a command to the specified device 
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                : cmd  is the command to send to the device, valid value are from
*                :      INEMO2_CMD_t enum.
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_Command(INEMO2_DEVICE_HANDLE hDevHandle, INEMO2_CMD_t cmd);

/*******************************************************************************
* Function Name  : INEMO2_GetInfoText
* Description    : Connects the PC to the iNEMO device
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                : reqInfo the request information come form the INEMO2_INFOTEXT_t 
*                :      enum
*                : lpText the buffer where receive the data, it must be allowed for
*                :      a size greater or ugual of the information received
*                : lpnSize a pointer to an integer, in input it represent the size 
*                :      of the allowed bytes in the lpText buffer, in output it is the
*                :      number of byte copied int he lpText Buffer (NULL is not allowed)
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_GetInfoText(INEMO2_DEVICE_HANDLE hDevHandle, INEMO2_INFOTEXT_t reqInfo, char *lpText, int *lpnSize);

/*******************************************************************************
* Function Name  : INEMO2_SetOutput
* Description    : Set the device output mode
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                : output a pointer to the INEMO2_OUTPUT_t sturcutre to configure 
*                :      the device output data mode 
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_SetOutput(INEMO2_DEVICE_HANDLE hDevHandle, PINEMO2_OUTPUT_t output);

/*******************************************************************************
* Function Name  : INEMO2_GetOutput
* Description    : Get the device output mode
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                : output a pointer to the INEMO2_OUTPUT_t structure where 
*                :      store the device output data mode configuration 
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_GetOutput(INEMO2_DEVICE_HANDLE hDevHandle, PINEMO2_OUTPUT_t output);

/*******************************************************************************
* Function Name  : INEMO2_GetDataSample
* Description    : Get a sample form the device, the command start must be sent before
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                : pFrame pointer to an allowed structure FrameData_t that will contains
*                :      the data sample 
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_GetDataSample(INEMO2_DEVICE_HANDLE hDevHandle, PFrameData_t pFrame);

/*******************************************************************************
* Function Name  : INEMO2_GetTrace
* Description    : Get trace information for the board 
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                : lpText the buffer where receive the trace, it must be allowed for
*                :      a size greater or ugual of the information received
*                : lpnSize a pointer to an integer, in input it represent the size 
*                :      of the allowed bytes in the lpText buffer, in output it is the
*                :      number of byte copied int he lpText Buffer (NULL is not allowed)
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_GetTrace(INEMO2_DEVICE_HANDLE hDevHandle,  char *lpText, int *lpnSize);

/*******************************************************************************
* Function Name  : INEMO_SDK_GetBufferUsage
* Description    : Get the usage of the buffers available in the SDK
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                :  type is the fifo identified form the enum INEMO2_BUFFERS_t 
*                :  pPercent pointer to an alloewed double value where will be stored
*                :      the percent usage of buffer, the value is between 0-100
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_GetBufferUsage(INEMO2_DEVICE_HANDLE hDevHandle,  INEMO2_BUFFERS_t type, double *pPercent);

/*******************************************************************************
* Function Name  : INEMO_SDK_GetModules
* Description    : Get the available modules
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                :  pModules pointer to an alloewed INEMO2_MODULES_t value where will be stored
*                :      the available modules.
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_GetModules(INEMO2_DEVICE_HANDLE hDevHandle,  INEMO2_MODULES_t *pModules);


/*******************************************************************************
* Function Name  : INEMO2_SetReg
* Description    : To set the register of a sensor
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                :  sensor INEMO2_SENSORS_t type value the sensor to set the register
*                :  reg    Register to set
*                :  pRegValue pointer to an alloewed reg value
*                :  sizeRegValue size in bytes of the regvalue register. 
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_SetReg(INEMO2_DEVICE_HANDLE hDevHandle, INEMO2_SENSORS_t sensor, unsigned int reg, unsigned char *pRegValue, unsigned int sizeRegValue);

/*******************************************************************************
* Function Name  : INEMO2_GetReg
* Description    : To get the sensor register value
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                :  sensor INEMO2_SENSORS_t type value the sensor to get the register
*                :  reg    Register to read
*                :  pRegValue pointer to an alloewed reg value that will contain the result
*                :  pSizeRegValue size in bytes of the regvalue register returned in 
*                :             input it contains the size of the number of bytes allowed for pRegValue. 
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_GetReg(INEMO2_DEVICE_HANDLE hDevHandle, INEMO2_SENSORS_t sensor,  unsigned int reg, unsigned char *pRegValue, unsigned int *pSizeRegValue);

/*******************************************************************************
* Function Name  : INEMO2_RestoreDefault
* Description    : To restore the default value of the sensor register
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
*                :  sensor INEMO2_SENSORS_t type value the sensor to set the register
*                :  reg    Register to set.
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_RestoreDefault(INEMO2_DEVICE_HANDLE hDevHandle, INEMO2_SENSORS_t sensor,  unsigned int reg);

/*******************************************************************************
* Function Name  : INEMO2_Identify
* Description    : The iNEMO identify command coulbe used to identy an iNEMO board
*                :     the available led on board will blink for 3 times.
* Input          : hDevHandle is the device handle returned from the INEMO2_Connect
*                :      function.
* Return         : 0 no error, otherwise code error
*******************************************************************************/
INEMO2_SDK_API INEMO2_ERROR INEMO2_Identify(INEMO2_DEVICE_HANDLE hDevHandle);


#endif //__INEMO2_SDK_H_

