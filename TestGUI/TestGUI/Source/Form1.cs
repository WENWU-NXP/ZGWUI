using FTD2XX_NET;
using ListManagement;
using PortSet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;



namespace ZGWUI
{

    public partial class Form1 : Form
    {
        NetworkRecovery nwkRecovery = new NetworkRecovery();
        ListManager listManager;

        UInt64[] au64ExtAddr = new UInt64[16];

        byte[] au8OTAFile = new byte[524288]; // 512k max file size
        byte u8OtaInProgress = 0;
        byte u8OTAWaitForDataParamsPending = 0;
        UInt16 u16OTAWaitForDataParamsTargetAddr;
        byte u8OTAWaitForDataParamsSrcEndPoint;
        UInt32 u32OTAWaitForDataParamsCurrentTime;
        UInt32 u32OTAWaitForDataParamsRequestTime;
        UInt16 u16OTAWaitForDataParamsBlockDelay;
        UInt32 u32OtaFileIdentifier;
        UInt16 u16OtaFileHeaderVersion;
        UInt16 u16OtaFileHeaderLength;
        UInt16 u16OtaFileHeaderControlField;
        UInt16 u16OtaFileManufacturerCode;
        UInt16 u16OtaFileImageType;
        UInt32 u32OtaFileVersion;
        UInt16 u16OtaFileStackVersion;
        UInt32 u32OtaFileTotalImage;
        byte u8OtaFileSecurityCredVersion;
        UInt64 u64OtaFileUpgradeFileDest;
        UInt16 u16OtaFileMinimumHwVersion;
        UInt16 u16OtaFileMaxHwVersion;



        //time
        [DllImport("winmm")]
        static extern uint timeGetTime();
        [DllImport("winmm")]
        static extern void timeBeginPeriod(int t);
        [DllImport("winmm")]
        static extern uint timeEndPeriod(int t);
        ManualResetEvent syncEvent = new ManualResetEvent(false);
        bool syncEventPort1 = false;

        string installCodePath = Application.StartupPath + @"\installcode.txt";
        string comPath;
        //UInt16 u16PollControlFastPollExpiry;
        //byte bPollControlStartFastPolling;

        //UInt32 u32CurrentOffset = 0;

        public Form1()
        {
            InitializeComponent();
            GUIinitialize();

            listManager = new ListManager();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoScroll = true;
        }

        #region GUI initialization functions

        private void GUIinitialize()
        {
            comboBoxSetKeyState.Items.Add("NO NETWORK KEY");
            comboBoxSetKeyState.Items.Add("PRECONFIGURED NETWORK KEY");
            comboBoxSetKeyState.Items.Add("DEFAULT NETWORK KEY");
            comboBoxSetKeyState.Items.Add("PRECONFIGURED LINK KEY");
            comboBoxSetKeyState.Items.Add("ZLL LINK KEY");
            comboBoxSetKeyState.SelectedIndex = 3;

            comboBoxSetKeyType.Items.Add("UNIQUE LINK KEY");
            comboBoxSetKeyType.Items.Add("GLOBAL LINK KEY");
            comboBoxSetKeyType.SelectedIndex = 1;

            comboBoxSetType.Items.Add("COORDINATOR");
            comboBoxSetType.Items.Add("ROUTER");
            comboBoxSetType.Items.Add("END DEVICE");
            comboBoxSetType.SelectedIndex = 0;

            comboBoxPermitJoinTCsignificance.Items.Add("NO CHANGE");
            comboBoxPermitJoinTCsignificance.Items.Add("POLICY AS SPEC");
            comboBoxPermitJoinTCsignificance.SelectedIndex = 0;

            comboBoxSecurityKey.Items.Add("5A6967426565416C6C69616E63653039");
            comboBoxSecurityKey.Items.Add("D0D1D2D3D4D5D6D7D8D9DADBDCDDDEDF");
            comboBoxSecurityKey.SelectedIndex = 0;

            textBoxSetEPID.ForeColor = System.Drawing.Color.Gray;
            textBoxSetEPID.Text = "Extended PAN ID (64-bit Hex)";

            textBoxSetCMSK.ForeColor = System.Drawing.Color.Gray;
            textBoxSetCMSK.Text = "Single Channel or Mask (32-bit Hex)";

            textBoxPermitJoinInterval.ForeColor = System.Drawing.Color.Gray;
            textBoxPermitJoinInterval.Text = "Interval (8-bit Hex)";

            textBoxSetSecurityKeySeqNbr.ForeColor = System.Drawing.Color.Gray;
            textBoxSetSecurityKeySeqNbr.Text = "SQN";

            textBoxMatchReqNbrInputClusters.ForeColor = System.Drawing.Color.Gray;
            textBoxMatchReqNbrInputClusters.Text = "Inputs (8-bit Hex)";

            textBoxMatchReqNbrOutputClusters.ForeColor = System.Drawing.Color.Gray;
            textBoxMatchReqNbrOutputClusters.Text = "Outputs (8-bit Hex)";

            textBoxMatchReqInputClusters.ForeColor = System.Drawing.Color.Gray;
            textBoxMatchReqInputClusters.Text = "Clusters (16-bit Hex)";

            textBoxMatchReqOutputClusters.ForeColor = System.Drawing.Color.Gray;
            textBoxMatchReqOutputClusters.Text = "Clusters (16-bit Hex)";

            textBoxUserSetReqDescription.ForeColor = System.Drawing.Color.Gray;
            textBoxUserSetReqDescription.Text = "User Description (String)";

            addrModeComboBoxInit(ref comboBoxBindAddrMode);
            destShortIeeeAddrTextBoxInit(ref textBoxBindDestAddr);
            dstEndPointTextBoxInit(ref textBoxBindDestEP);

            addrModeComboBoxInit(ref comboBoxUnBindAddrMode);
            destShortIeeeAddrTextBoxInit(ref textBoxUnBindDestAddr);
            dstEndPointTextBoxInit(ref textBoxUnBindDestEP);

            // Management tab text box initialization
            targetExtendedAddrTextBoxInit(ref textBoxBindTargetExtAddr);
            targetEndPointTextBoxInit(ref textBoxBindTargetEP);
            clusterIdTextBoxInit(ref textBoxBindClusterID);

            targetExtendedAddrTextBoxInit(ref textBoxUnBindTargetExtAddr);
            targetEndPointTextBoxInit(ref textBoxUnBindTargetEP);
            clusterIdTextBoxInit(ref textBoxUnBindClusterID);

            shortAddrTextBoxInit(ref textBoxPermitJoinAddr);
            shortAddrTextBoxInit(ref textBoxNodeDescReq);
            shortAddrTextBoxInit(ref textBoxSimpleReqAddr);
            shortAddrTextBoxInit(ref textBoxPowerReqAddr);
            shortAddrTextBoxInit(ref textBoxActiveEpAddr);
            shortAddrTextBoxInit(ref textBoxMatchReqAddr);
            shortAddrTextBoxInit(ref textBoxComplexReqAddr);
            shortAddrTextBoxInit(ref textBoxUserReqAddr);
            shortAddrTextBoxInit(ref textBoxUserSetReqAddr);
            shortAddrTextBoxInit(ref textBoxLqiReqTargetAddr);
            profileIdTextBoxInit(ref textBoxMatchReqProfileID);
            dstEndPointTextBoxInit(ref textBoxSimpleReqEndPoint);
            targetShortAddrTextBoxInit(ref textBoxIeeeReqTargetAddr);
            shortAddrTextBoxInit(ref textBoxIeeeReqAddr);
            startIndexTextBoxInit(ref textBoxIeeeReqStartIndex);
            startIndexTextBoxInit(ref textBoxLqiReqStartIndex);
            comboBoxIeeeReqType.Items.Add("SINGLE");
            comboBoxIeeeReqType.Items.Add("EXTENDED");
            comboBoxIeeeReqType.SelectedIndex = 0;
            targetShortAddrTextBoxInit(ref textBoxNwkAddrReqTargetAddr);
            extendedAddrTextBoxInit(ref textBoxNwkAddrReqExtAddr);
            startIndexTextBoxInit(ref textBoxNwkAddrReqStartIndex);
            comboBoxNwkAddrReqType.Items.Add("SINGLE");
            comboBoxNwkAddrReqType.Items.Add("EXTENDED");
            comboBoxNwkAddrReqType.SelectedIndex = 0;

            // Restore Network Recovery
            textBoxRestoreNwkFrameCounter.ForeColor = System.Drawing.Color.Gray;
            textBoxRestoreNwkFrameCounter.Text = "Out Frame Counter (32-bit Hex)";

            // Remove Device Request UI
            extendedAddrTextBoxInit(ref textBoxRemoveParentAddr);
            extendedAddrTextBoxInit(ref textBoxRemoveChildAddr);

            // Management Leave Request UI
            extendedAddrTextBoxInit(ref textBoxMgmtLeaveExtAddr);
            targetShortAddrTextBoxInit(ref textBoxMgmtLeaveAddr);
            comboBoxMgmtLeaveReJoin.Items.Add("DO NOT REJOIN");
            comboBoxMgmtLeaveReJoin.Items.Add("REJOIN");
            comboBoxMgmtLeaveReJoin.SelectedIndex = 1;
            comboBoxMgmtLeaveChildren.Items.Add("DO NOT REMOVE CHILDREN");
            comboBoxMgmtLeaveChildren.Items.Add("REMOVE CHILDREN");
            comboBoxMgmtLeaveChildren.SelectedIndex = 1;

            // Leave Request UI
            extendedAddrTextBoxInit(ref textBoxLeaveAddr);
            comboBoxLeaveReJoin.Items.Add("DO NOT REJOIN");
            comboBoxLeaveReJoin.Items.Add("REJOIN");
            comboBoxLeaveReJoin.SelectedIndex = 1;
            comboBoxLeaveChildren.Items.Add("DO NOT REMOVE CHILDREN");
            comboBoxLeaveChildren.Items.Add("REMOVE CHILDREN");
            comboBoxLeaveChildren.SelectedIndex = 1;

            // NCI UI
            comboBoxNciCmd.Items.Add("COMMISSION");
            comboBoxNciCmd.Items.Add("DECOMMISSION");
            comboBoxNciCmd.Items.Add("DISABLE");
            comboBoxNciCmd.SelectedIndex = 0;

            // General tab initialization
            addrModeComboBoxInit(ref comboBoxMgmtNwkUpdateAddrMode);
            targetShortAddrTextBoxInit(ref textBoxMgmtNwkUpdateTargetAddr);
            textBoxMgmtNwkUpdateChannelMask.ForeColor = System.Drawing.Color.Gray;
            textBoxMgmtNwkUpdateChannelMask.Text = "ChanMask (32-bit Hex)";
            scanDurationTextBoxInit(ref textBoxMgmtNwkUpdateScanDuration);
            textBoxMgmtNwkUpdateScanCount.ForeColor = System.Drawing.Color.Gray;
            textBoxMgmtNwkUpdateScanCount.Text = "Count (8-bit Hex)";

            textBoxMgmtNwkUpdateNwkManagerAddr.ForeColor = System.Drawing.Color.Gray;
            textBoxMgmtNwkUpdateNwkManagerAddr.Text = "NwkMan Addr (16-bit Hex)";

            targetShortAddrTextBoxInit(ref textBoxReadAttribTargetAddr);
            srcEndPointTextBoxInit(ref textBoxReadAttribSrcEP);
            dstEndPointTextBoxInit(ref textBoxReadAttribDstEP);
            clusterIdTextBoxInit(ref textBoxReadAttribClusterID);
            comboBoxReadAttribDirection.Items.Add("TO SERVER");
            comboBoxReadAttribDirection.Items.Add("TO CLIENT");
            comboBoxReadAttribDirection.SelectedIndex = 0;
            attributeIdTextBoxInit(ref textBoxReadAttribID1);
            attributeCountTextBoxInit(ref textBoxReadAttribCount);
            manufacturerSpecificComboBoxInit(ref comboBoxReadAttribManuSpecific);
            manufacturerIdTextBoxInit(ref textBoxReadAttribManuID);

            targetShortAddrTextBoxInit(ref textBoxWriteAttribTargetAddr);
            srcEndPointTextBoxInit(ref textBoxWriteAttribSrcEP);
            dstEndPointTextBoxInit(ref textBoxWriteAttribDstEP);
            clusterIdTextBoxInit(ref textBoxWriteAttribClusterID);
            comboBoxWriteAttribDirection.Items.Add("TO SERVER");
            comboBoxWriteAttribDirection.Items.Add("TO CLIENT");
            comboBoxWriteAttribDirection.SelectedIndex = 0;
            attributeIdTextBoxInit(ref textBoxWriteAttribID);
            attributeDataTypeTextBoxInit(ref textBoxWriteAttribDataType);
            attribDataTextBoxInit(ref textBoxWriteAttribData);
            manufacturerSpecificComboBoxInit(ref comboBoxWriteAttribManuSpecific);
            manufacturerIdTextBoxInit(ref textBoxWriteAttribManuID);

            targetShortAddrTextBoxInit(ref textBoxConfigReportTargetAddr);
            addrModeComboBoxZCLInit(ref comboBoxConfigReportAddrMode);
            srcEndPointTextBoxInit(ref textBoxConfigReportSrcEP);
            dstEndPointTextBoxInit(ref textBoxConfigReportDstEP);
            clusterIdTextBoxInit(ref textBoxConfigReportClusterID);
            comboBoxConfigReportDirection.Items.Add("TO SERVER");
            comboBoxConfigReportDirection.Items.Add("TO CLIENT");
            comboBoxConfigReportDirection.SelectedIndex = 0;
            comboBoxConfigReportAttribDirection.Items.Add("TX SERVER");
            comboBoxConfigReportAttribDirection.Items.Add("Rx CLIENT");
            comboBoxConfigReportAttribDirection.SelectedIndex = 0;
            attributeTypeTextBoxInit(ref textBoxConfigReportAttribType);
            attributeIdTextBoxInit(ref textBoxConfigReportAttribID);
            minIntervalTextBoxInit(ref textBoxConfigReportMinInterval);
            maxIntervalTextBoxInit(ref textBoxConfigReportMaxInterval);
            timeOutPeriodTextBoxInit(ref textBoxConfigReportTimeOut);
            reportChangeTextBoxInit(ref textBoxConfigReportChange);

            targetShortAddrTextBoxInit(ref textBoxReadAllAttribAddr);
            srcEndPointTextBoxInit(ref textBoxReadAllAttribSrcEP);
            dstEndPointTextBoxInit(ref textBoxReadAllAttribDstEP);
            clusterIdTextBoxInit(ref textBoxReadAllAttribClusterID);
            comboBoxReadAllAttribDirection.Items.Add("TO SERVER");
            comboBoxReadAllAttribDirection.Items.Add("TO CLIENT");
            comboBoxReadAllAttribDirection.SelectedIndex = 0;

            targetShortAddrTextBoxInit(ref textBoxDiscoverAttributesAddr);
            srcEndPointTextBoxInit(ref textBoxDiscoverAttributesSrcEp);
            dstEndPointTextBoxInit(ref textBoxDiscoverAttributesDstEp);
            clusterIdTextBoxInit(ref textBoxDiscoverAttributesClusterID);
            attributeIdTextBoxInit(ref textBoxDiscoverAttributesStartAttrId);


            comboBoxDiscoverAttributesDirection.Items.Add("TO SERVER");
            comboBoxDiscoverAttributesDirection.Items.Add("TO CLIENT");
            comboBoxDiscoverAttributesDirection.SelectedIndex = 0;
            maxIDsTextBoxInit(ref textBoxDiscoverAttributesMaxIDs);
            comboBoxDiscoverAttributesExtended.Items.Add("STANDARD");
            comboBoxDiscoverAttributesExtended.Items.Add("EXTENDED");
            comboBoxDiscoverAttributesExtended.SelectedIndex = 0;

            addrModeComboBoxZCLInit(ref comboBoxDiscoverCommandsAddrMode);
            targetShortAddrTextBoxInit(ref textBoxDiscoverCommandsTargetAddr);
            srcEndPointTextBoxInit(ref textBoxDiscoverCommandsSrcEP);
            dstEndPointTextBoxInit(ref textBoxDiscoverCommandsDstEP);
            clusterIdTextBoxInit(ref textBoxDiscoverCommandsClusterID);

            addrModeComboBoxZCLInit(ref comboBoxRawDataCommandsAddrMode);
            targetShortAddrTextBoxInit(ref textBoxRawDataCommandsTargetAddr);
            srcEndPointTextBoxInit(ref textBoxRawDataCommandsSrcEP);
            dstEndPointTextBoxInit(ref textBoxRawDataCommandsDstEP);
            profileIdTextBoxInit(ref textBoxRawDataCommandsProfileID);
            clusterIdTextBoxInit(ref textBoxRawDataCommandsClusterID);

            textBoxRawDataCommandsSecurityMode.ForeColor = System.Drawing.Color.Gray;
            textBoxRawDataCommandsSecurityMode.Text = "Security Mode (8-bit Hex)";

            textBoxRawDataCommandsRadius.ForeColor = System.Drawing.Color.Gray;
            textBoxRawDataCommandsRadius.Text = "Radius (8-bit Hex)";

            textBoxRawDataCommandsData.ForeColor = System.Drawing.Color.Gray;
            textBoxRawDataCommandsData.Text = "Raw Data (Format: Byte:Byte:Byte)";
            textBoxRawDataCommandsData.TextChanged += new EventHandler(textBoxRawDataCommandsData_TextChanged);

            comboBoxDiscoverCommandsDirection.Items.Add("TO SERVER");
            comboBoxDiscoverCommandsDirection.Items.Add("TO CLIENT");
            comboBoxDiscoverCommandsDirection.SelectedIndex = 0;
            commandIDTextBoxInit(ref textBoxDiscoverCommandsCommandID);
            manufacturerSpecificComboBoxInit(ref comboBoxDiscoverCommandsManuSpecific);
            manufacturerIdTextBoxInit(ref textBoxDiscoverCommandsManuID);
            maxCommandsTextBoxInit(ref textBoxDiscoverCommandsMaxCommands);
            comboBoxDiscoverCommandsRxGen.Items.Add("RECEIVED");
            comboBoxDiscoverCommandsRxGen.Items.Add("GENERATED");
            comboBoxDiscoverCommandsRxGen.SelectedIndex = 0;

            addrModeComboBoxZCLInit(ref comboBoxReadReportConfigAddrMode);
            targetShortAddrTextBoxInit(ref textBoxReadReportConfigTargetAddr);
            srcEndPointTextBoxInit(ref textBoxReadReportConfigSrcEP);
            dstEndPointTextBoxInit(ref textBoxReadReportConfigDstEP);
            clusterIdTextBoxInit(ref textBoxReadReportConfigClusterID);
            comboBoxReadReportConfigDirection.Items.Add("TO SERVER");
            comboBoxReadReportConfigDirection.Items.Add("TO CLIENT");
            comboBoxReadReportConfigDirection.SelectedIndex = 0;
            attributeIdTextBoxInit(ref textBoxReadReportConfigAttribID);
            comboBoxReadReportConfigDirIsRx.Items.Add("DIR TX");
            comboBoxReadReportConfigDirIsRx.Items.Add("DIR RX");
            comboBoxReadReportConfigDirIsRx.SelectedIndex = 1;

            comboBoxManyToOneRouteRequestCacheRoute.Items.Add("NO CACHE");
            comboBoxManyToOneRouteRequestCacheRoute.Items.Add("CACHE");
            comboBoxManyToOneRouteRequestCacheRoute.SelectedIndex = 1;
            radiusTextBoxInit(ref textBoxManyToOneRouteRequesRadius);

            OOBAddrTextBoxInit(ref textBoxOOBDataAddr);
            OOBKeyTextBoxInit(ref textBoxOOBDataKey);

            // General Install Code initialization
            MacAddressTextBoxInit(ref textBoxGeneralInstallCodeMACaddress);
            InstallCodeTextBoxInit(ref textBoxGeneralInstallCodeCode);


            // Basic cluster tab initialization
            addrModeComboBoxZCLInit(ref comboBoxBasicResetTargetAddrMode);
            shortAddrTextBoxInit(ref textBoxBasicResetTargetAddr);
            srcEndPointTextBoxInit(ref textBoxBasicResetSrcEP);
            dstEndPointTextBoxInit(ref textBoxBasicResetDstEP);

            // Scenes cluster tab initialization
            addrModeComboBoxZCLInit(ref comboBoxViewSceneAddrMode);
            shortAddrTextBoxInit(ref textBoxViewSceneAddr);
            srcEndPointTextBoxInit(ref textBoxViewSceneSrcEndPoint);
            dstEndPointTextBoxInit(ref textBoxViewSceneDstEndPoint);
            groupIdTextBoxInit(ref textBoxViewSceneGroupId);
            sceneIdTextBoxInit(ref textBoxViewSceneSceneId);

            addrModeComboBoxZCLInit(ref comboBoxAddSceneAddrMode);
            shortAddrTextBoxInit(ref textBoxAddSceneAddr);
            srcEndPointTextBoxInit(ref textBoxAddSceneSrcEndPoint);
            dstEndPointTextBoxInit(ref textBoxAddSceneDstEndPoint);
            groupIdTextBoxInit(ref textBoxAddSceneGroupId);
            sceneIdTextBoxInit(ref textBoxAddSceneSceneId);
            time16bitTextBoxInit(ref textBoxAddSceneTransTime);
            nameStringTextBoxInit(ref textBoxAddSceneName);
            stringLenTextBoxInit(ref textBoxAddSceneNameLen);
            stringMaxLenTextBoxInit(ref textBoxAddSceneMaxNameLen);
            extLenTextBoxInit(ref textBoxAddSceneExtLen);
            sceneDataTextBoxInit(ref textBoxAddSceneData);

            addrModeComboBoxZCLInit(ref comboBoxStoreSceneAddrMode);
            shortAddrTextBoxInit(ref textBoxStoreSceneAddr);
            srcEndPointTextBoxInit(ref textBoxStoreSceneSrcEndPoint);
            dstEndPointTextBoxInit(ref textBoxStoreSceneDstEndPoint);
            groupIdTextBoxInit(ref textBoxStoreSceneGroupId);
            sceneIdTextBoxInit(ref textBoxStoreSceneSceneId);

            addrModeComboBoxZCLInit(ref comboBoxRecallSceneAddrMode);
            shortAddrTextBoxInit(ref textBoxRecallSceneAddr);
            srcEndPointTextBoxInit(ref textBoxRecallSceneSrcEndPoint);
            dstEndPointTextBoxInit(ref textBoxRecallSceneDstEndPoint);
            groupIdTextBoxInit(ref textBoxRecallSceneGroupId);
            sceneIdTextBoxInit(ref textBoxRecallSceneSceneId);

            addrModeComboBoxZCLInit(ref comboBoxGetSceneMembershipAddrMode);
            shortAddrTextBoxInit(ref textBoxGetSceneMembershipAddr);
            srcEndPointTextBoxInit(ref textBoxGetSceneMembershipSrcEndPoint);
            dstEndPointTextBoxInit(ref textBoxGetSceneMembershipDstEndPoint);
            groupIdTextBoxInit(ref textBoxGetSceneMembershipGroupID);

            addrModeComboBoxZCLInit(ref comboBoxRemoveAllScenesAddrMode);
            shortAddrTextBoxInit(ref textBoxRemoveAllScenesAddr);
            srcEndPointTextBoxInit(ref textBoxRemoveAllScenesSrcEndPoint);
            dstEndPointTextBoxInit(ref textBoxRemoveAllScenesDstEndPoint);
            groupIdTextBoxInit(ref textBoxRemoveAllScenesGroupID);

            addrModeComboBoxZCLInit(ref comboBoxRemoveSceneAddrMode);
            shortAddrTextBoxInit(ref textBoxRemoveSceneAddr);
            srcEndPointTextBoxInit(ref textBoxRemoveSceneSrcEndPoint);
            dstEndPointTextBoxInit(ref textBoxRemoveSceneDstEndPoint);
            groupIdTextBoxInit(ref textBoxRemoveSceneGroupID);
            sceneIdTextBoxInit(ref textBoxRemoveSceneSceneID);

            // Group cluster tab initialization
            shortAddrTextBoxInit(ref textBoxAddGroupAddr);
            dstEndPointTextBoxInit(ref textBoxAddGroupDstEp);
            srcEndPointTextBoxInit(ref textBoxAddGroupSrcEp);
            groupIdTextBoxInit(ref textBoxAddGroupGroupAddr);
            groupNameLengthTextBoxInit(ref textBoxGroupNameLength);
            groupNameMaxLengthTextBoxInit(ref textBoxGroupNameMaxLength);
            groupNameTextBoxInit(ref textBoxGroupName);
            shortAddrTextBoxInit(ref textBoxViewGroupAddr);
            dstEndPointTextBoxInit(ref textBoxViewGroupDstEp);
            srcEndPointTextBoxInit(ref textBoxViewGroupSrcEp);
            groupIdTextBoxInit(ref textBoxViewGroupGroupAddr);
            shortAddrTextBoxInit(ref textBoxGetGroupTargetAddr);
            dstEndPointTextBoxInit(ref textBoxGetGroupDstEp);
            srcEndPointTextBoxInit(ref textBoxGetGroupSrcEp);
            groupCountTextBoxInit(ref textBoxGetGroupCount);
            shortAddrTextBoxInit(ref textBoxRemoveGroupTargetAddr);
            srcEndPointTextBoxInit(ref textBoxRemoveGroupSrcEp);
            dstEndPointTextBoxInit(ref textBoxRemoveGroupDstEp);
            groupIdTextBoxInit(ref textBoxRemoveGroupGroupAddr);
            shortAddrTextBoxInit(ref textBoxRemoveAllGroupTargetAddr);
            srcEndPointTextBoxInit(ref textBoxRemoveAllGroupSrcEp);
            dstEndPointTextBoxInit(ref textBoxRemoveAllGroupDstEp);

            shortAddrTextBoxInit(ref textBoxGroupAddIfIndentifyingTargetAddr);
            dstEndPointTextBoxInit(ref textBoxGroupAddIfIdentifyDstEp);
            srcEndPointTextBoxInit(ref textBoxGroupAddIfIdentifySrcEp);
            groupIdTextBoxInit(ref textBoxGroupAddIfIdentifyGroupID);

            // On/off cluster tab initialization
            addrModeComboBoxZCLInit(ref comboBoxOnOffAddrMode);
            shortAddrTextBoxInit(ref textBoxOnOffAddr);
            dstEndPointTextBoxInit(ref textBoxOnOffDstEndPoint);
            srcEndPointTextBoxInit(ref textBoxOnOffSrcEndPoint);
            comboBoxOnOffCommand.Items.Add("Off");
            comboBoxOnOffCommand.Items.Add("On");
            comboBoxOnOffCommand.Items.Add("Toggle");
            comboBoxOnOffCommand.SelectedIndex = 0;

            // Level cluster tab initialization
            addrModeComboBoxZCLInit(ref comboBoxMoveToLevelAddrMode);
            shortAddrTextBoxInit(ref textBoxMoveToLevelAddr);
            srcEndPointTextBoxInit(ref textBoxMoveToLevelSrcEndPoint);
            dstEndPointTextBoxInit(ref textBoxMoveToLevelDstEndPoint);
            withOnOffComboBoxInit(ref comboBoxMoveToLevelOnOff);
            levelTextBoxInit(ref textBoxMoveToLevelLevel);
            time16bitTextBoxInit(ref textBoxMoveToLevelTransTime);

            // Identify cluster initialization
            shortAddrTextBoxInit(ref textBoxSendIdAddr);
            shortAddrTextBoxInit(ref textBoxIdQueryAddr);
            srcEndPointTextBoxInit(ref textBoxSendIdSrcEp);
            srcEndPointTextBoxInit(ref textBoxIdQuerySrcEp);
            dstEndPointTextBoxInit(ref textBoxIdSendDstEp);
            dstEndPointTextBoxInit(ref textBoxIdQueryDstEp);
            time16bitTextBoxInit(ref textBoxIdSendTime);

            // Color cluster initialization
            shortAddrTextBoxInit(ref textBoxMoveToHueAddr);
            srcEndPointTextBoxInit(ref textBoxMoveToHueSrcEp);
            dstEndPointTextBoxInit(ref textBoxMoveToHueDstEp);
            hueTextBoxInit(ref textBoxMoveToHueHue);
            directionTextBoxInit(ref textBoxMoveToHueDir);
            time16bitTextBoxInit(ref textBoxMoveToHueTime);

            shortAddrTextBoxInit(ref textBoxMoveToColorAddr);
            srcEndPointTextBoxInit(ref textBoxMoveToColorSrcEp);
            dstEndPointTextBoxInit(ref textBoxMoveToColorDstEp);
            time16bitTextBoxInit(ref textBoxMoveToColorTime);
            xCoordTextBoxInit(ref textBoxMoveToColorX);
            yCoordTextBoxInit(ref textBoxMoveToColorY);

            shortAddrTextBoxInit(ref textBoxMoveToSatAddr);
            srcEndPointTextBoxInit(ref textBoxMoveToSatSrcEp);
            dstEndPointTextBoxInit(ref textBoxMoveToSatDstEp);
            satTextBoxInit(ref textBoxMoveToSatSat);
            time16bitTextBoxInit(ref textBoxMoveToSatTime);

            shortAddrTextBoxInit(ref textBoxMoveToColorTempAddr);
            srcEndPointTextBoxInit(ref textBoxMoveToColorTempSrcEp);
            dstEndPointTextBoxInit(ref textBoxMoveToColorTempDstEp);
            colorTempTextBoxInit(ref textBoxMoveToColorTempTemp);
            time16bitTextBoxInit(ref textBoxMoveToColorTempRate);

            // Lock cluster initialization
            comboBoxLockUnlock.Items.Add("LOCK");
            comboBoxLockUnlock.Items.Add("UNLOCK");
            comboBoxLockUnlock.SelectedIndex = 0;
            shortAddrTextBoxInit(ref textBoxLockUnlockAddr);
            srcEndPointTextBoxInit(ref textBoxLockUnlockSrcEp);
            dstEndPointTextBoxInit(ref textBoxLockUnlockDstEp);

            // IAS cluster tab initialization
            addrModeComboBoxZCLInit(ref comboBoxEnrollRspAddrMode);
            shortAddrTextBoxInit(ref textBoxEnrollRspAddr);
            dstEndPointTextBoxInit(ref textBoxEnrollRspDstEp);
            srcEndPointTextBoxInit(ref textBoxEnrollRspSrcEp);
            comboBoxEnrollRspCode.Items.Add("SUCCESS");
            comboBoxEnrollRspCode.Items.Add("NOT SUPPORTED");
            comboBoxEnrollRspCode.Items.Add("NO ENROLL PERMIT");
            comboBoxEnrollRspCode.Items.Add("TOO MANY ZONES");
            comboBoxEnrollRspCode.SelectedIndex = 0;
            zoneIdTextBoxInit(ref textBoxEnrollRspZone);

            // ZLL On/Off cluster tab initialization
            shortAddrTextBoxInit(ref textBoxZllOnOffEffectsAddr);
            srcEndPointTextBoxInit(ref textBoxZllOnOffEffectsSrcEp);
            dstEndPointTextBoxInit(ref textBoxZllOnOffEffectsDstEp);
            gradientTextBoxInit(ref textBoxZllOnOffEffectsGradient);
            comboBoxZllOnOffEffectID.Items.Add("OFF");
            comboBoxZllOnOffEffectID.Items.Add("ON");
            comboBoxZllOnOffEffectID.Items.Add("TOGGLE");
            comboBoxZllOnOffEffectID.SelectedIndex = 0;

            // ZLL color cluster tab initialization
            shortAddrTextBoxInit(ref textBoxZllMoveToHueAddr);
            srcEndPointTextBoxInit(ref textBoxZllMoveToHueSrcEp);
            dstEndPointTextBoxInit(ref textBoxZllMoveToHueDstEp);
            zllhueTextBoxInit(ref textBoxZllMoveToHueHue);
            directionTextBoxInit(ref textBoxZllMoveToHueDirection);
            time16bitTextBoxInit(ref textBoxZllMoveToHueTransTime);

            // OTA cluster tab initialization
            addrModeComboBoxZCLInit(ref comboBoxOTAImageNotifyAddrMode);
            shortAddrTextBoxInit(ref textBoxOTAImageNotifyTargetAddr);
            srcEndPointTextBoxInit(ref textBoxOTAImageNotifySrcEP);
            dstEndPointTextBoxInit(ref textBoxOTAImageNotifyDstEP);
            comboBoxOTAImageNotifyType.Items.Add("JITTER");
            comboBoxOTAImageNotifyType.Items.Add("MDID JITTER");
            comboBoxOTAImageNotifyType.Items.Add("ITYPE MDID JITTER");
            comboBoxOTAImageNotifyType.Items.Add("ITYPE MDID VER JITTER");
            comboBoxOTAImageNotifyType.SelectedIndex = 0;
            fileVersionTextBoxInit(ref textBoxOTAImageNotifyFileVersion);
            imageTypeTextBoxInit(ref textBoxOTAImageNotifyImageType);
            manufacturerIdTextBoxInit(ref textBoxOTAImageNotifyManuID);
            queryJitterTextBoxInit(ref textBoxOTAImageNotifyJitter);

            shortAddrTextBoxInit(ref textBoxOTASetWaitForDataParamsTargetAddr);
            srcEndPointTextBoxInit(ref textBoxOTASetWaitForDataParamsSrcEP);
            currentTme32bitTextBoxInit(ref textBoxOTASetWaitForDataParamsCurrentTime);
            requestTme32bitTextBoxInit(ref textBoxOTASetWaitForDataParamsRequestTime);
            blockDelay16bitTextBoxInit(ref textBoxOTASetWaitForDataParamsRequestBlockDelay);
            time16bitTextBoxInit(ref textBoxIPNConfigPollPeriod);

            DIOMaskInit(ref textBoxDioSetDirectionOutputPinMask);
            DIOMaskInit(ref textBoxDioSetDirectionInputPinMask);

            DIOMaskInit(ref textBoxDioSetOutputOnPinMask);
            DIOMaskInit(ref textBoxDioSetOutputOffPinMask);

            DIOMaskInit(ref textBoxIPNConfigDioRfActiveOutDioMask);
            DIOMaskInit(ref textBoxIPNConfigDioStatusOutDioMask);
            DIOMaskInit(ref textBoxIPNConfigDioTxConfInDioMask);
            comboBoxIPNConfigTimerId.SelectedIndex = 0;
            comboBoxIPNConfigEnable.SelectedIndex = 0;
            comboBoxIPNConfigRegisterCallback.SelectedIndex = 0;

            txPowerTextBoxInit(ref textBoxAHITxPower);

            //EZ LNT tab initialization
            //comboBoxEZLNTPROGRAMEEPROM.SelectedIndex = 0;
            //comboBoxEZLNTPROGRAMERRASE.SelectedIndex = 0;

            EZLNTGroupInit(ref textBoxEZLNTADDGROUP);
            EZLNTGroupInit(ref textBoxREMOVEGROUP);
            EZLNTGroupInit(ref textBoxEZLNTVIEW);

            EZLNTGroupInit(ref textBoxLNTADDGROUPADDR);
            EZLNTGroupInit(ref textBoxLNTREMOVEGROUPADDRESS);
            EZLNTGroupInit(ref textBoxLNTVIEWGROUPADDRESS);


            EZLNTTimerLoopInit(ref textBoxEZLNTSETLOOP);
            EZLNTTimerIntervalInit(ref textBoxEZLNTTIMERINTERVAL);
            EZLNTTimerIntervalInit(ref textBoxEZLNTCONFIGRPRTMININTERVAL);
            EZLNTTimerIntervalMaxInit(ref textBoxEZLNTSETINTERVALMAX);
            EZLNTTimerIntervalMaxInit(ref textBoxEZLNTCONFIGRPRTMAXINTERVAL);

            EZLNTTimerLoopInit(ref textBoxLNTSETLOOP);
            EZLNTTimerIntervalInit(ref textBoxLNTSETPARAMININTERVAL);
            EZLNTTimerIntervalInit(ref textBoxCONFIGRPRTMININTERVAL);
            EZLNTTimerIntervalMaxInit(ref textBoxLNTSETPARAMAXINTERVAL);
            EZLNTTimerIntervalMaxInit(ref textBoxCONFIGRPRTMAXRPRTINTERVAL);


            EZLNTTimeOutInit(ref textBoxEZLNTCONFIGRPRTTIMEOUT);
            EZLNTDataChangeInit(ref textBoxEZLNTCONFIGRPRTCHANGE);
            EZLNTTimeOutInit(ref textBoxLNTCONFIGRPRTTIMEOUT);
            EZLNTDataChangeInit(ref textBoxLNTCONFIGRPRTCHANGE);

            EZLNTStepInit(ref textBoxEZLNTSETSTEP);
            EZLNTDIRInit(ref textBoxEZLNTSETDIR);
            EZLNTStepInit(ref textBoxLNTSETPARASTEP);
            EZLNTDIRInit(ref textBoxLNTSETPARADIR);


            EZLNTClusterIDInit(ref textBoxEZLNTBINDCLUSTERID);
            EZLNTClusterIDInit(ref textBoxEZLNTUNBINDCLUSTERID);
            EZLNTClusterIDInit(ref textBoxEZLNTREADCLUSTERID);
            EZLNTClusterIDInit(ref textBoxEZLNTWRITEATTRIBUTECLUSTERID);
            EZLNTClusterIDInit(ref textBoxEZLNTCONFIGRPRTCLUSTERID);
            EZLNTClusterIDInit(ref textBoxEZLNTREADRPRTCLUSTERID);
            EZLNTClusterIDInit(ref textBoxLNTBINDIEEEADDR);
            EZLNTClusterIDInit(ref textBoxLNTUNBINDIEEEADDR);
            EZLNTClusterIDInit(ref textBoxLNTREADATTRCLUSTERID);
            EZLNTClusterIDInit(ref textBoxLNTWRITEATTRCLUSTERID);
            EZLNTClusterIDInit(ref textBoxLNTCONFIGRPRTCLUSTERID);
            EZLNTClusterIDInit(ref textBoxLNTREADRPRTCLUSTERID);


            EZLNTAttributeIDInit(ref textBoxEZLNTATTRIBUTEID);
            EZLNTAttributeIDInit(ref textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID);
            EZLNTAttributeIDInit(ref textBoxEZLNTCONFIGRPRTATTRIBID);
            EZLNTAttributeIDInit(ref textBoxEZLNTREADRPRTATTRIBUTEID);
            EZLNTAttributeIDInit(ref textBoxLNTREADATTRATTRIBUTEID);
            EZLNTAttributeIDInit(ref textBoxLNTWRITEATTRATTRID);
            EZLNTAttributeIDInit(ref textBoxCONFIGRPRTATTRID);
            EZLNTAttributeIDInit(ref textBoxLNTREADRPRTATTRID);


            EZLNTTimeInit(ref textBoxEZLNTIDENTIFYTIME);
            EZLNTTimeInit(ref textBoxEZLNTLEVELTIME);
            EZLNTTimeInit(ref textBoxEZLNTHUETIME);
            EZLNTTimeInit(ref textBoxCOLORTIME);
            EZLNTTimeInit(ref textBoxEZLNTSATTIME);
            EZLNTTimeInit(ref textBoxEZLNTTEMPTIME);
            EZLNTTimeInit(ref textBoxLNTIDENTIFYTIME);
            EZLNTTimeInit(ref textBoxLNTLEVELTIME);
            EZLNTTimeInit(ref textBoxLNTHUETIME);
            EZLNTTimeInit(ref textBoxLNTCOLORTIME);
            EZLNTTimeInit(ref textBoxLNTSATTIME);
            EZLNTTimeInit(ref textBoxLNTTEMPTIME);



            //EZLNTCountInit(ref textBoxEZLNTATTRIBUTECOUNT);
            EZLNTCountInit(ref textBoxLNTREADATTRATTRIBUTECOUNT);


            EZLNTdatatypeInit(ref textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE);
            EZLNTdatatypeInit(ref textBoxEZLNTCONFIGRPRTTYPE);
            EZLNTdatatypeInit(ref textBoxLNTWRITEATTRDATATYPE);
            EZLNTdatatypeInit(ref textBoxLNTCONFIGRPRTTYPE);

            EZLNTdataInit(ref textBoxEZLNTWRITEATTRIBUTEDATA);
            EZLNTdataInit(ref textBoxLNTWRITEATTRDATA);

            EZLNTlevelInit(ref textBoxEZLNTLEVEL);
            EZLNTHuelInit(ref textBoxEZLNTHUE);
            EZLNTcolorXlInit(ref textBoxEZLNTCOLORX);
            EZLNTcolorYlInit(ref textBoxEZLNTCOLORY);
            EZLNTsatlInit(ref textBoxEZLNTSAT);
            EZLNTtemplInit(ref textBoxEZLNTTEMP);
            EZLNTHueDirlInit(ref textBoxEZLNTHUEDIR);
            EZLNTlevelInit(ref textBoxLNTLEVEL);
            EZLNTHuelInit(ref textBoxLNTHUE);
            EZLNTcolorXlInit(ref textBoxLNTCOLORX);
            EZLNTcolorYlInit(ref textBoxLNTCOLORY);
            EZLNTsatlInit(ref textBoxLNTSAT);
            EZLNTtemplInit(ref textBoxLNTTEMP);
            EZLNTHueDirlInit(ref textBoxLNTHUEDIR);

            comboBoxEZLNTUNICAST.SelectedIndex = 0;


            listViewEZLNTINFO.InsertionMark.Color = Color.Red;

            #region PollControlInit

            PollControlFastPollingExpiryInit(ref textBoxFastPollExpiryTime);

            #endregion
        }

        private void DIOMaskInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "DIO Mask (32-bit Hex)";
        }

        private void txPowerTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Tx Power (6-bit Hex)";
        }

        private void queryJitterTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Query Jitter (8-bit Hex)";
        }

        private void imageTypeTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Image Type (16-bit Hex)";
        }

        private void fileVersionTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Version (32-bit Hex)";
        }

        private void scanDurationTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Duration (8-bit Hex)";
        }

        private void radiusTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Radius (8-bit Hex)";
        }

        private void stringLenTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Len (8-bit Hex)";
        }

        private void extLenTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Ext Len (16-bit Hex)";
        }

        private void sceneDataTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Data (Format: Byte:Byte:Byte)";
            textBox.TextChanged += new EventHandler(textBoxAddSceneData_TextChanged);
        }

        private void colorTempTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "TempK (16-bit Dec)";
        }

        private void withOnOffComboBoxInit(ref ComboBox comboBox)
        {
            comboBox.Items.Add("Without OnOff");
            comboBox.Items.Add("With OnOff");
            comboBox.SelectedIndex = 0;
        }

        private void attribDataTypComboBoxInit(ref ComboBox comboBox)
        {
            comboBox.Items.Add("Bound Addr");
            comboBox.Items.Add("Group Addr");
            comboBox.Items.Add("Short Addr");
            comboBox.Items.Add("IEEE Addr");
            comboBox.SelectedIndex = 0;
        }

        private void manufacturerSpecificComboBoxInit(ref ComboBox comboBox)
        {
            comboBox.Items.Add("STANDARD");
            comboBox.Items.Add("CUSTOM");
            comboBox.SelectedIndex = 0;
        }

        private void testInstallCodeComboBoxInit(ref ComboBox comboBox)
        {
            comboBox.Items.Add("TEST");
            comboBox.Items.Add("CUSTOM");
            comboBox.SelectedIndex = 0;
        }


        private void attribDataTypeTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Type (8-bit Hex)";
        }

        private void manufacturerIdTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Manu ID (16-bit Hex)";
        }

        private void attribDataTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Data";
        }

        private void reportChangeTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Change (8-bit Hex)";
        }

        private void timeOutPeriodTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "TimeOut (16-bit Hex)";
        }

        private void minIntervalTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Min Intv (16-bit Hex)";
        }

        private void maxIntervalTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Max Intv (16-bit Hex)";
        }

        private void nameStringTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Name (String)";
        }

        private void stringMaxLenTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Max Len (8-bit Hex)";
        }

        private void maxIDsTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Max ID's (8-Bit Hex)";
        }

        private void zoneIdTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Zone ID (8-Bit Hex)";
        }

        private void addrModeComboBoxInit(ref ComboBox comboBox)
        {
            comboBox.Items.Add("Bound");
            comboBox.Items.Add("Group");
            comboBox.Items.Add("Short");
            comboBox.Items.Add("IEEE");
            comboBox.SelectedIndex = 0;
        }

        private void addrModeComboBoxZCLInit(ref ComboBox comboBox)
        {
            comboBox.Items.Add("Bound");
            comboBox.Items.Add("Group");
            comboBox.Items.Add("Short");
            comboBox.Items.Add("IEEE");
            comboBox.Items.Add("Broadcast");
            comboBox.Items.Add("No Transmit");
            comboBox.Items.Add("Bound No Ack");
            comboBox.Items.Add("Short No Ack");
            comboBox.Items.Add("IEEE No Ack");
            comboBox.SelectedIndex = 0;
        }

        private void startIndexTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Start Idx (8-bit Hex)";
        }

        private void effectIdTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Effect ID (8-bit Hex)";
        }

        private void gradientTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Gradient (8-bit Hex)";
        }

        private void directionTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Dir (8-bit Hex)";
        }

        private void hueTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Hue (8-bit Hex)";
        }

        private void zllhueTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Hue (16-bit Hex)";
        }

        private void satTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Saturation (8-bit Hex)";
        }

        private void xCoordTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "X (16-bit Hex)";
        }

        private void yCoordTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Y (16-bit Hex)";
        }

        private void currentTme32bitTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Current Time (32-bit Hex)";
        }

        private void requestTme32bitTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Request Time (32-bit Hex)";
        }

        private void blockDelay16bitTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Block Delay (16-bit Hex)";
        }


        private void time8bitTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Time (8-bit Hex)";
        }

        private void time16bitTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Time (16-bit Hex)";
        }

        private void levelTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Level (8-bit Hex)";
        }

        private void profileIdTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Profile (16-bit Hex)";
        }

        private void clusterIdTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Cluster (16-bit Hex)";
        }

        private void attributeIdTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Attrib (16-bit Hex)";
        }

        private void attributeCountTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Attrib Count";
        }

        private void attributeTypeTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Attrib Type";
        }

        private void attributeDataTypeTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Type (8-bit Hex)";
        }

        private void targetShortAddrTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Target (16-bit Hex)";
        }

        private void commandIDTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Cmd ID (8-bit Hex)";
        }

        private void maxCommandsTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Max Cmds (8-bit Hex)";
        }

        private void shortAddrTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Address (16-bit Hex)";
        }

        private void destShortAddrTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Dst Addr (16-bit Hex)";
        }

        private void destShortIeeeAddrTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Dst Addr (16-bit or 64-bit Hex)";
        }

        private void extendedAddrTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Address (64-bit Hex)";
        }

        private void AddrTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Address";
        }

        private void targetExtendedAddrTextBoxInit(ref ComboBox comboBox)
        {
            comboBox.ForeColor = System.Drawing.Color.Gray;
            comboBox.Text = "Target Address (64-bit Hex)";
        }

        private void targetExtendedAddrTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Target Address (64-bit Hex)";
        }

        private void dstEndPointTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Dst EP (8-bit Hex)";
        }

        private void targetEndPointTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Target EP (8-bit Hex)";
        }

        private void srcEndPointTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Src EP (8-bit Hex)";
        }

        private void groupCountTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Group Count";
        }

        private void sceneIdTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Scene ID (8-bit Hex)";
        }

        private void groupIdTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Group ID (16-bit Hex)";
        }

        private void groupNameLengthTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Name Len (8-bit Hex)";
        }

        private void groupNameMaxLengthTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Max Len (8-bit Hex)";
        }

        private void groupNameTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Group Name (String)";
        }

        private void PollControlFastPollingExpiryInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Fast Poll Expiry (16-bit Hex)";
        }

        private void OOBAddrTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Address (64-bit Hex)";
        }

        private void OOBKeyTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Key (Format: Byte:Byte:Byte)";
            textBox.TextChanged += new EventHandler(textBoxOOBDataKey_TextChanged);
        }

        private void MacAddressTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "MACaddress (64-bit Hex)";
        }

        private void InstallCodeTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Install Code (64-bit Hex)";
        }

        private void InstallCodeLinkKeyTextBoxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Link Key (128-bit Hex)";
        }

        private void EZLNTTextBoxSendCommandInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "command (char)";
        }

        private void EZLNTGroupInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "group address (hex)";
        }

        private void EZLNTOnOffAddressInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "address (hex)";
        }

        private void EZLNTTimerLoopInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "loop count ";
        }

        private void EZLNTTimerIntervalInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Timer interval min ";
        }

        private void EZLNTTimerIntervalMaxInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Timer interval max ";
        }

        private void EZLNTTimeOutInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "Time out ";
        }

        private void EZLNTDataChangeInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "data change ";
        }

        private void EZLNTStepInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "step ";
        }

        private void EZLNTDIRInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "dir ";
        }

        private void EZLNTClusterIDInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "cluster ID (hex)";
        }

        private void EZLNTAttributeIDInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "attribute ID (hex)";
        }

        private void EZLNTTimeInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "time (hex)";
        }

        private void EZLNTCountInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "attribute count (hex)";
        }

        private void EZLNTdatatypeInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "data type (hex)";
        }

        private void EZLNTdataInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "data(hex)";
        }

        private void EZLNTlevelInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "level(hex)";
        }

        private void EZLNTHuelInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "hue(hex)";
        }

        private void EZLNTcolorXlInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "color X(hex)";
        }

        private void EZLNTcolorYlInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "color Y(hex)";
        }

        private void EZLNTsatlInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "sat(hex)";
        }

        private void EZLNTtemplInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "temp(hex)";
        }

        private void EZLNTHueDirlInit(ref TextBox textBox)
        {
            textBox.ForeColor = System.Drawing.Color.Gray;
            textBox.Text = "direction";
        }


        #endregion

        #region ToolTip

        private void showToolTipWindow(string s)
        {
            Point p = this.PointToClient(Cursor.Position);
            toolTipGeneralTooltip.Show(s, this, p.X - 25, p.Y - 5);
        }

        private void hideToolTipWindow()
        {
            toolTipGeneralTooltip.Hide(this);
        }

        #endregion

        #region menu strip functions

        private bool bPortConfigured = false;
        private bool bDBGPortConfigured = false;
        private bool bMultiPortConfigured = false;
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                MessageBox.Show("The port must be closed before changing the settings");
                return;
            }
            else
            {
                PortSettings settings = new PortSettings();

                if (settings.ShowDialog() == DialogResult.OK)
                {
                    serialPort1.PortName = settings.selectedPort;
                    serialPort1.BaudRate = settings.selectedBaudRate;
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceivedHandler);

                    displayPortSettings(serialPort1);

                    bPortConfigured = true;
                }
            }
        }

        private void multiUSBNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (multiPortOpen)
            {
                MessageBox.Show("The port must be closed before changing the settings");
                return;
            }
            else
            {
                PortSettings settings = new PortSettings();

                if (settings.ShowDialog() == DialogResult.OK)
                {
                    portCount = settings.PortList.Count;


                    for (int i = 0; i < portCount; i++)
                    {
                        SerialPort serialPorttemp = new SerialPort();
                        serialPorttemp.PortName = settings.PortList[i] as string;
                        serialPorttemp.BaudRate = settings.selectedBaudRate;
                        serialPorttemp.DataBits = 8;
                        serialPorttemp.Parity = Parity.None;
                        serialPorttemp.StopBits = StopBits.One;
                        serialPorttemp.DataReceived += new SerialDataReceivedEventHandler(serialPortMulti_DataReceivedHandler);
                        multiUSBport.Add(serialPorttemp);

                        //                      displayPortSettings();

                    }

                    bMultiPortConfigured = true;

                }
            }

        }



        private void openPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bPortConfigured == true)
            {
                try
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Close();
                        openPortToolStripMenuItem.Text = "Open Port";
                    }
                    else
                    {
                        serialPort1.Open();
                        sendGetIEEEAddress();
                        openPortToolStripMenuItem.Text = "Close Port";
                    }
                    displayPortSettings(serialPort1);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error - openPortToolStripMenuItem_Click Exception: " + ex);
                }
            }
            else
            {
                MessageBox.Show("Error - No Port Selected");
            }
        }

        private void displayPortSettings(SerialPort serialPorttemp)
        {
            toolStripPortSettings.Text = serialPorttemp.PortName;
            toolStripPortSettings.Text += " ";
            toolStripPortSettings.Text += serialPorttemp.BaudRate.ToString();
            toolStripPortSettings.Text += "-";
            toolStripPortSettings.Text += serialPorttemp.DataBits.ToString();
            toolStripPortSettings.Text += "-";
            toolStripPortSettings.Text += (serialPorttemp.Parity.ToString())[0];
            toolStripPortSettings.Text += "-";

            if (serialPorttemp.StopBits.ToString() == "One")
            {
                toolStripPortSettings.Text += "1";
            }
            else
            {
                toolStripPortSettings.Text += "2";
            }

            toolStripPortSettings.Text += " ";

            if (serialPorttemp.IsOpen)
            {
                toolStripPortSettings.Text += "Open";
            }
            else
            {
                toolStripPortSettings.Text += "Closed";
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                String version = System.IO.File.ReadAllText(path + "\\..\\..\\VERSION.txt");

                MessageBox.Show("NXP ZigBee Gateway User Interface - Version " + version);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Version File not found\nPlease run ZGWUI in Directory: \n\nJN-AN-1223-ZigBee-IoT-Gateway-Control-Bridge");
            }
        }

        #endregion 

        #region management command button handlers

        private void buttonMgmtLeave_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt64 u64ExtAddr;

            if (bStringToUint16(textBoxMgmtLeaveAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint64(textBoxMgmtLeaveExtAddr.Text, out u64ExtAddr) == true)
                {
                    sendMgmtLeaveRequest(u16ShortAddr, u64ExtAddr, (byte)comboBoxMgmtLeaveReJoin.SelectedIndex, (byte)comboBoxMgmtLeaveChildren.SelectedIndex);
                }
            }

        }

        private void buttonUnBind_Click(object sender, EventArgs e)
        {
            UInt64 u64TargetExtAddr;
            UInt16 u16ClusterID;
            UInt64 u64DstAddr;
            byte u8TargetEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint64(textBoxUnBindTargetExtAddr.Text, out u64TargetExtAddr) == true)
            {
                if (bStringToUint8(textBoxUnBindTargetEP.Text, out u8TargetEndPoint) == true)
                {
                    if (bStringToUint16(textBoxUnBindClusterID.Text, out u16ClusterID) == true)
                    {
                        if (bStringToUint64(textBoxUnBindDestAddr.Text, out u64DstAddr) == true)
                        {
                            if (bStringToUint8(textBoxUnBindDestEP.Text, out u8DstEndPoint) == true)
                            {
                                sendUnBindRequest(u64TargetExtAddr, u8TargetEndPoint, u16ClusterID, (byte)comboBoxUnBindAddrMode.SelectedIndex, u64DstAddr, u8DstEndPoint);
                            }
                        }
                    }
                }
            }
        }

        private void buttonBind_Click(object sender, EventArgs e)
        {
            UInt64 u64TargetExtAddr;
            UInt16 u16ClusterID;
            UInt64 u64DstAddr;
            byte u8TargetEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint64(textBoxBindTargetExtAddr.Text, out u64TargetExtAddr) == true)
            {
                if (bStringToUint8(textBoxBindTargetEP.Text, out u8TargetEndPoint) == true)
                {
                    if (bStringToUint16(textBoxBindClusterID.Text, out u16ClusterID) == true)
                    {
                        if (bStringToUint64(textBoxBindDestAddr.Text, out u64DstAddr) == true)
                        {
                            if (bStringToUint8(textBoxBindDestEP.Text, out u8DstEndPoint) == true)
                            {
                                sendBindRequest(u64TargetExtAddr, u8TargetEndPoint, u16ClusterID, (byte)comboBoxBindAddrMode.SelectedIndex, u64DstAddr, u8DstEndPoint);
                            }
                        }
                    }
                }
            }
        }

        private void buttonSetCMSK_Click(object sender, EventArgs e)
        {
            UInt32 u32ChannelMask;

            // First check if user is entering a single channel or a 32-bit mask..
            if (UInt32.TryParse(textBoxSetCMSK.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out u32ChannelMask) == true)
            {
                if (u32ChannelMask >= 11 && u32ChannelMask <= 26)
                {
                    // User is specifying a single channel, we must create the 32-bit mask from this                                        
                    UInt32 u32ChannelMaskTemp = 1;

                    for (int i = 0; i < u32ChannelMask; i++)
                    {
                        u32ChannelMaskTemp <<= 1;
                    }
                    u32ChannelMask = u32ChannelMaskTemp;

                    // Set channel mask
                    setChannelMask(u32ChannelMask);
                }
                else
                {
                    // User has entered a channel bit mask
                    if (bStringToUint32(textBoxSetCMSK.Text, out u32ChannelMask) == true)
                    {
                        // Set channel mask
                        setChannelMask(u32ChannelMask);
                    }
                }
            }
        }

        private void buttonMgmtLqiReq_Click_1(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8StartIndex;

            if (bStringToUint16(textBoxLqiReqTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxLqiReqStartIndex.Text, out u8StartIndex) == true)
                {
                    sendMgmtLqiRequest(u16TargetAddr, u8StartIndex);
                }
            }
        }

        private void buttonNwkAddrReq_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt64 u64ExtAddr;
            byte u8StartIndex;

            if (bStringToUint16(textBoxNwkAddrReqTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint64(textBoxNwkAddrReqExtAddr.Text, out u64ExtAddr) == true)
                {
                    if (bStringToUint8(textBoxNwkAddrReqStartIndex.Text, out u8StartIndex) == true)
                    {
                        sendNwkAddrRequest(u16TargetAddr, u64ExtAddr, (byte)comboBoxNwkAddrReqType.SelectedIndex, u8StartIndex);
                    }
                }
            }
        }

        private void buttonIeeeAddrReq_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ShortAddr;
            byte u8StartIndex;

            if (bStringToUint16(textBoxIeeeReqTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint16(textBoxIeeeReqAddr.Text, out u16ShortAddr) == true)
                {
                    if (bStringToUint8(textBoxIeeeReqStartIndex.Text, out u8StartIndex) == true)
                    {
                        sendIeeeAddrRequest(u16TargetAddr, u16ShortAddr, (byte)comboBoxIeeeReqType.SelectedIndex, u8StartIndex);
                    }
                }
            }
        }

        private void buttonComplexReq_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;

            if (bStringToUint16(textBoxComplexReqAddr.Text, out u16ShortAddr) == true)
            {
                complexDescriptorRequest(u16ShortAddr);
            }
        }

        private void buttonUserReq_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;

            if (bStringToUint16(textBoxUserReqAddr.Text, out u16ShortAddr) == true)
            {
                userDescriptorRequest(u16ShortAddr);
            }
        }

        private void buttonUserSetReq_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;

            if (bStringToUint16(textBoxUserSetReqAddr.Text, out u16ShortAddr) == true)
            {
                if (textBoxUserSetReqDescription.Text != "")
                {
                    userDescriptorSetRequest(u16ShortAddr, textBoxUserSetReqDescription.Text);
                }
            }
        }

        private void buttonMatchReq_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16ProfileID;
            byte u8NbrInputClusters;
            byte u8NbrOutputClusters;
            UInt16[] au16InputClusters = new UInt16[8];
            UInt16[] au16OutputClusters = new UInt16[8];

            if (bStringToUint16(textBoxMatchReqAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint16(textBoxMatchReqProfileID.Text, out u16ProfileID) == true)
                {
                    if (bStringToUint8(textBoxMatchReqNbrInputClusters.Text, out u8NbrInputClusters) == true)
                    {
                        if ((u8NbrInputClusters == 0) ||
                            ((u8NbrInputClusters > 0) &&
                             (bStringToUint16Array(textBoxMatchReqInputClusters.Text, out au16InputClusters) == true)))
                        {
                            if (bStringToUint8(textBoxMatchReqNbrOutputClusters.Text, out u8NbrOutputClusters) == true)
                            {
                                if ((u8NbrOutputClusters == 0) ||
                                    ((u8NbrOutputClusters > 0) &&
                                    (bStringToUint16Array(textBoxMatchReqOutputClusters.Text, out au16OutputClusters) == true)))
                                {
                                    matchDescriptorRequest(u16ShortAddr, u16ProfileID, u8NbrInputClusters, au16InputClusters, u8NbrOutputClusters, au16OutputClusters);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonActiveEpReq_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;

            if (bStringToUint16(textBoxActiveEpAddr.Text, out u16ShortAddr) == true)
            {
                activeEndpointDescriptorRequest(u16ShortAddr);
            }
        }

        private void buttonSimpleDescReq_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8EndPoint;

            if (bStringToUint16(textBoxSimpleReqAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxSimpleReqEndPoint.Text, out u8EndPoint) == true)
                {
                    simpleDescriptorRequest(u16ShortAddr, u8EndPoint);
                }
            }
        }

        private void buttonPowerDescReq_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;

            if (bStringToUint16(textBoxPowerReqAddr.Text, out u16ShortAddr) == true)
            {
                powerDescriptorRequest(u16ShortAddr);
            }
        }

        private void buttonPermitJoin_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8Interval;

            if (bStringToUint16(textBoxPermitJoinAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxPermitJoinInterval.Text, out u8Interval) == true)
                {
                    setPermitJoin((UInt16)u16ShortAddr, u8Interval, (byte)comboBoxPermitJoinTCsignificance.SelectedIndex);
                }
            }
        }

        private void buttonSetDeviceType_Click(object sender, EventArgs e)
        {
            // Set device type
            setDeviceType((byte)comboBoxSetType.SelectedIndex);
        }

        private void buttonSetEPID_Click(object sender, EventArgs e)
        {
            UInt64 u64ExtendedPanID;

            if (bStringToUint64(textBoxSetEPID.Text, out u64ExtendedPanID) == true)
            {
                // Set channel mask
                setExtendedPanID(u64ExtendedPanID);
            }
        }

        private void buttonGetVersion_Click(object sender, EventArgs e)
        {
            transmitCommand(0x0010, 0, null);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            // Transmit command
            transmitCommand(0x0011, 0, null);
        }

        private void buttonErasePD_Click(object sender, EventArgs e)
        {
            // Transmit command
            System.IO.File.WriteAllText(installCodePath, string.Empty);
            transmitCommand(0x0012, 0, null);
        }

        private void buttonStartNWK_Click(object sender, EventArgs e)
        {
            // Transmit command
            transmitCommand(0x0024, 0, null);
        }

        private void buttonStartScan_Click(object sender, EventArgs e)
        {
            transmitCommand(0x0025, 0, null);
        }

        private void buttonDiscoveryOnly_Click(object sender, EventArgs e)
        {
            transmitCommand(0x0015, 0, null);
        }

        private void buttonSetSecurity_Click(object sender, EventArgs e)
        {
            byte u8SeqNbr;
            byte[] au8keyData;

            if (bStringToUint8(textBoxSetSecurityKeySeqNbr.Text, out u8SeqNbr) == true)
            {
                if (bStringToUint128(comboBoxSecurityKey.Text, out au8keyData) == true)
                {
                    // Set key state information
                    setSecurityKeyState((byte)comboBoxSetKeyState.SelectedIndex, u8SeqNbr, (byte)comboBoxSetKeyType.SelectedIndex, au8keyData);
                }
            }
        }

        private void buttonNodeDescReq_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;

            if (bStringToUint16(textBoxNodeDescReq.Text, out u16ShortAddr) == true)
            {
                nodeDescriptorRequest(u16ShortAddr);
            }
        }

        #endregion

        #region general command button handlers

        /* Unsupported*/
        private void buttonRecoverNwk_Click(object sender, EventArgs e)
        {
            transmitCommand(0x0600, 0, null);
        }

        /* Unsupported */
        private void buttonRestoreNwk_Click(object sender, EventArgs e)
        {
            UInt32 u32OutFrameCounter = 0;
            byte[] baBuff = new byte[nwkRecovery.iGetSize()];

            if (textBoxRestoreNwkFrameCounter.TextLength < 10 && textBoxRestoreNwkFrameCounter.TextLength != 0)
            {
                bStringToUint32(textBoxRestoreNwkFrameCounter.Text, out u32OutFrameCounter);
                nwkRecovery.NetworkRecoverySetOutFrameCounter(u32OutFrameCounter);
            }

            nwkRecovery.NetworkRecoveryConstructBuffer(ref baBuff);
            transmitCommand(0x0601, nwkRecovery.iGetSize(), baBuff);
        }
        private void buttonDiscoverCommands_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            UInt16 u16ManuID;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8CommandId;
            byte u8MaxCommands;

            if (bStringToUint16(textBoxDiscoverCommandsTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxDiscoverCommandsSrcEP.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxDiscoverCommandsDstEP.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxDiscoverCommandsClusterID.Text, out u16ClusterID) == true)
                        {
                            if (bStringToUint8(textBoxDiscoverCommandsCommandID.Text, out u8CommandId) == true)
                            {
                                if (bStringToUint16(textBoxDiscoverCommandsManuID.Text, out u16ManuID) == true)
                                {
                                    if (bStringToUint8(textBoxDiscoverCommandsMaxCommands.Text, out u8MaxCommands) == true)
                                    {
                                        sendDiscoverCommandsRequest((byte)comboBoxDiscoverCommandsAddrMode.SelectedIndex, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, (byte)comboBoxDiscoverCommandsDirection.SelectedIndex, u8CommandId, (byte)comboBoxDiscoverCommandsManuSpecific.SelectedIndex, u16ManuID, u8MaxCommands, (byte)comboBoxDiscoverCommandsRxGen.SelectedIndex);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonMgmtNwkUpdate_Click(object sender, EventArgs e)
        {
            UInt32 u32ChannelMask;
            UInt16 u16TargetAddr;
            UInt16 u16NwkManangerAddr;
            byte u8ScanDuration;
            byte u8ScanCount;

            if (bStringToUint16(textBoxMgmtNwkUpdateTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint32(textBoxMgmtNwkUpdateChannelMask.Text, out u32ChannelMask) == true)
                {
                    if (bStringToUint8(textBoxMgmtNwkUpdateScanDuration.Text, out u8ScanDuration) == true)
                    {
                        if (bStringToUint8(textBoxMgmtNwkUpdateScanCount.Text, out u8ScanCount) == true)
                        {
                            if (bStringToUint16(textBoxMgmtNwkUpdateNwkManagerAddr.Text, out u16NwkManangerAddr) == true)
                            {
                                sendMgmtNwkUpdateRequest((byte)comboBoxMgmtNwkUpdateAddrMode.SelectedIndex, u16TargetAddr, u32ChannelMask, u8ScanDuration, u8ScanCount, u16NwkManangerAddr);
                            }
                        }
                    }
                }
            }
        }

        private void buttonManyToOneRouteRequest_Click(object sender, EventArgs e)
        {
            byte u8Radius;

            if (bStringToUint8(textBoxManyToOneRouteRequesRadius.Text, out u8Radius) == true)
            {
                sendOneToManyRouteRequest((byte)comboBoxManyToOneRouteRequestCacheRoute.SelectedIndex, u8Radius);
            }
        }

        private void buttonWriteAttrib_Click_1(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            UInt16 u16AttribID;
            UInt16 u16ManuID;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8AttribType;
            byte[] au8Data = new byte[64];
            byte u8DataLen = 0;

            if (bStringToUint16(textBoxWriteAttribTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxWriteAttribSrcEP.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxWriteAttribDstEP.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxWriteAttribClusterID.Text, out u16ClusterID) == true)
                        {
                            if (bStringToUint16(textBoxWriteAttribID.Text, out u16AttribID) == true)
                            {
                                if (bStringToUint16(textBoxWriteAttribManuID.Text, out u16ManuID) == true)
                                {
                                    if (bStringToUint8(textBoxWriteAttribDataType.Text, out u8AttribType) == true)
                                    {
                                        if (u8AttribType == 0x42)
                                        {
                                            // if the data is a character string get the length make make this is the first byte                                         
                                            au8Data[0] = (byte)System.Text.Encoding.ASCII.GetBytes(textBoxWriteAttribData.Text, 0, textBoxWriteAttribData.TextLength, au8Data, 1);
                                            u8DataLen = au8Data[0];
                                            u8DataLen++;
                                        }
                                        else if (u8AttribType == 0x21)
                                        {
                                            UInt16 u16Data;

                                            /* Data is a uint16 */
                                            if (bStringToUint16(textBoxWriteAttribData.Text, out u16Data) == true)
                                            {
                                                u8DataLen = 2;
                                                au8Data[1] = (byte)u16Data;
                                                au8Data[0] = (byte)(u16Data >> 8);
                                            }
                                        }
                                        else if (u8AttribType == 0xf0)
                                        {
                                            /* Data is a uint64 */
                                            UInt64 u64MACaddress;
                                            if (bStringToUint64(textBoxWriteAttribData.Text, out u64MACaddress) == true)
                                            {
                                                u8DataLen = 8;
                                                au8Data[7] = (byte)u64MACaddress;
                                                au8Data[6] = (byte)(u64MACaddress >> 8);
                                                au8Data[5] = (byte)(u64MACaddress >> 16);
                                                au8Data[4] = (byte)(u64MACaddress >> 24);
                                                au8Data[3] = (byte)(u64MACaddress >> 32);
                                                au8Data[2] = (byte)(u64MACaddress >> 40);
                                                au8Data[1] = (byte)(u64MACaddress >> 48);
                                                au8Data[0] = (byte)(u64MACaddress >> 56);
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < textBoxWriteAttribData.TextLength; i += 2)
                                            {
                                                byte u8Data = 0;
                                                if (bStringToUint8(textBoxWriteAttribData.Text, out u8Data) == true)
                                                {
                                                    au8Data[i] = u8Data;
                                                }
                                                else
                                                {
                                                    return;
                                                }
                                                u8DataLen++;
                                            }
                                        }
                                        sendWriteAttribRequest(u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, (byte)comboBoxReadAttribDirection.SelectedIndex, (byte)comboBoxWriteAttribManuSpecific.SelectedIndex, u16ManuID, 1, u16AttribID, u8AttribType, au8Data, u8DataLen);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonReadAttrib_Click_1(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            UInt16 u16AttribID1;
            UInt16 u16ManuID;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8AttribCount;

            if (bStringToUint16(textBoxReadAttribTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxReadAttribSrcEP.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxReadAttribDstEP.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxReadAttribClusterID.Text, out u16ClusterID) == true)
                        {
                            if (bStringToUint8(textBoxReadAttribCount.Text, out u8AttribCount) == true)
                            {
                                if (bStringToUint16(textBoxReadAttribID1.Text, out u16AttribID1) == true)
                                {
                                    if (bStringToUint16(textBoxReadAttribManuID.Text, out u16ManuID) == true)
                                    {
                                        sendReadAttribRequest(u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, (byte)comboBoxReadAttribDirection.SelectedIndex, (byte)comboBoxReadAttribManuSpecific.SelectedIndex, u16ManuID, u8AttribCount, u16AttribID1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonReadReportConfig_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            UInt16 u16ClusterID;
            UInt16 u16AttribID;

            if (bStringToUint16(textBoxReadReportConfigTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxReadReportConfigSrcEP.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxReadReportConfigDstEP.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxReadReportConfigClusterID.Text, out u16ClusterID) == true)
                        {
                            if (bStringToUint16(textBoxReadReportConfigAttribID.Text, out u16AttribID) == true)
                            {
                                sendReadReportConfigRequest((byte)comboBoxReadReportConfigAddrMode.SelectedIndex, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, (byte)comboBoxReadReportConfigDirection.SelectedIndex, 1, 0, 0, (byte)comboBoxReadReportConfigDirIsRx.SelectedIndex, u16AttribID);
                            }
                        }
                    }
                }
            }
        }

        private void buttonConfigReport_Click_1(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            UInt16 u16AttribId;
            UInt16 u16MinInterval;
            UInt16 u16MaxInterval;
            UInt16 u16TimeOut;
            UInt64 u64Change;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8AttribType;

            if (bStringToUint16(textBoxConfigReportTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxConfigReportSrcEP.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxConfigReportDstEP.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxConfigReportClusterID.Text, out u16ClusterID) == true)
                        {
                            if (bStringToUint8(textBoxConfigReportAttribType.Text, out u8AttribType) == true)
                            {
                                if (bStringToUint16(textBoxConfigReportAttribID.Text, out u16AttribId) == true)
                                {
                                    if (bStringToUint16(textBoxConfigReportMinInterval.Text, out u16MinInterval) == true)
                                    {
                                        if (bStringToUint16(textBoxConfigReportMaxInterval.Text, out u16MaxInterval) == true)
                                        {
                                            if (bStringToUint16(textBoxConfigReportTimeOut.Text, out u16TimeOut) == true)
                                            {
                                                if (bStringToUint64(textBoxConfigReportChange.Text, out u64Change) == true)
                                                {
                                                    sendConfigReportRequest((byte)comboBoxConfigReportAddrMode.SelectedIndex, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, (byte)comboBoxConfigReportDirection.SelectedIndex, (byte)comboBoxConfigReportAttribDirection.SelectedIndex, u8AttribType, u16AttribId, u16MinInterval, u16MaxInterval, u16TimeOut, u64Change);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonDiscoverAttributes_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8AttribsMax;
            byte u8AttribOffset;

            if (bStringToUint16(textBoxDiscoverAttributesAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxDiscoverAttributesSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxDiscoverAttributesDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxDiscoverAttributesClusterID.Text, out u16ClusterID) == true)
                        {
                            if (bStringToUint8(textBoxDiscoverAttributesMaxIDs.Text, out u8AttribsMax) == true)
                            {
                                if (bStringToUint8(textBoxDiscoverAttributesStartAttrId.Text, out u8AttribOffset) == true)
                                {
                                    sendDiscoverAttributesRequest(u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, u8AttribOffset, (byte)comboBoxDiscoverAttributesDirection.SelectedIndex, 0, 0, u8AttribsMax, (byte)comboBoxDiscoverAttributesExtended.SelectedIndex);
                                }
                            }
                        }
                    }
                }
            }
        }

        /*private void buttonReadAllAttrib_Click(object sender, EventArgs e)
        *{
        *    UInt16 u16DstAddr;
        *    UInt16 u16ClusterID;
        *    byte u8SrcEndPoint;
        *    byte u8DstEndPoint;
        *
        *    if (bStringToUint16(textBoxReadAllAttribAddr.Text, out u16DstAddr) == true)
        *    {
        *        if (bStringToUint8(textBoxReadAllAttribSrcEP.Text, out u8SrcEndPoint) == true)
        *        {
        *            if (bStringToUint8(textBoxReadAllAttribDstEP.Text, out u8DstEndPoint) == true)
        *            {
        *                if (bStringToUint16(textBoxReadAllAttribClusterID.Text, out u16ClusterID) == true)
        *                {
        *                    sendReadAllAttribRequest(u16DstAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, (byte)comboBoxReadAttribDirection.SelectedIndex, 0, 0);
        *                }
        *            }
        *        }
        *    }
        }*/

        private void buttonOOBCommissioningData_Click(object sender, EventArgs e)
        {
            string stringkeydata;
            UInt64 u64AddrData;

            if (bStringToUint64(textBoxOOBDataAddr.Text, out u64AddrData) == true)
            {
                if (1 == (textBoxOOBDataKey.TextLength % 2))
                {
                    stringkeydata = textBoxOOBDataKey.Text;
                    sendOOBCommissioningData(u64AddrData, stringkeydata);
                }
            }
        }

        private void buttonPermitJoinState_Click(object sender, EventArgs e)
        {
            vSendPermitRejoinStateRequest();
        }

        private void buttonNWKState_Click(object sender, EventArgs e)
        {
            vSendNetworkStateRequest();
        }

        private void buttonGeneralSendInstallCode_Click(object sender, EventArgs e)
        {
            UInt64 u64ExtAddr;
            byte[] InstallCode;
            if (bStringToUint64(textBoxGeneralInstallCodeMACaddress.Text, out u64ExtAddr) == true)
            {
                if (bStringToUint128(textBoxGeneralInstallCodeCode.Text, out InstallCode) == true)
                {
                    Write(installCodePath, textBoxGeneralInstallCodeMACaddress.Text + " " + ":" + textBoxGeneralInstallCodeCode.Text, textBoxGeneralInstallCodeMACaddress.Text);
                    sendInstallCodeCustom(u64ExtAddr, (byte)1, ref InstallCode);
                }
            }
        }



        private void buttonGeneralPrintExistInstallCode_Click(object sender, EventArgs e)
        {
            Read(installCodePath);
        }

        #endregion

        #region basic cluster command button handlers

        private void buttonBasicReset_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxBasicResetTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxBasicResetSrcEP.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxBasicResetDstEP.Text, out u8DstEndPoint) == true)
                    {
                        sendBasicResetFactoryDefaultCommand((byte)comboBoxBasicResetTargetAddrMode.SelectedIndex, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint);
                    }
                }
            }
        }

        #endregion

        #region OTA cluster button handlers


        private void buttonOTASetWaitForDataParams_Click(object sender, EventArgs e)
        {
            if (bStringToUint16(textBoxOTASetWaitForDataParamsTargetAddr.Text, out u16OTAWaitForDataParamsTargetAddr) == true)
            {
                if (bStringToUint8(textBoxOTASetWaitForDataParamsSrcEP.Text, out u8OTAWaitForDataParamsSrcEndPoint) == true)
                {
                    if (bStringToUint32(textBoxOTASetWaitForDataParamsCurrentTime.Text, out u32OTAWaitForDataParamsCurrentTime) == true)
                    {
                        if (bStringToUint32(textBoxOTASetWaitForDataParamsRequestTime.Text, out u32OTAWaitForDataParamsRequestTime) == true)
                        {
                            if (bStringToUint16(textBoxOTASetWaitForDataParamsRequestBlockDelay.Text, out u16OTAWaitForDataParamsBlockDelay) == true)
                            {
                                // Set flag indicating that next time we get a block request we should reply with a wait for data message
                                u8OTAWaitForDataParamsPending = 1;
                            }
                        }
                    }
                }
            }
        }

        private void buttonOTAImageNotify_Click(object sender, EventArgs e)
        {
            UInt32 u32FileVersion;
            UInt16 u16TargetAddr;
            UInt16 u16ImageType;
            UInt16 u16ManuCode;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8Jitter;

            if (bStringToUint16(textBoxOTAImageNotifyTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxOTAImageNotifySrcEP.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxOTAImageNotifyDstEP.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint32(textBoxOTAImageNotifyFileVersion.Text, out u32FileVersion) == true)
                        {
                            if (bStringToUint16(textBoxOTAImageNotifyImageType.Text, out u16ImageType) == true)
                            {
                                if (bStringToUint16(textBoxOTAImageNotifyManuID.Text, out u16ManuCode) == true)
                                {
                                    if (bStringToUint8(textBoxOTAImageNotifyJitter.Text, out u8Jitter) == true)
                                    {
                                        sendOtaImageNotify((byte)comboBoxOTAImageNotifyAddrMode.SelectedIndex, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, (byte)comboBoxOTAImageNotifyType.SelectedIndex, u32FileVersion, u16ImageType, u16ManuCode, u8Jitter);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonOTALoadNewImage_Click(object sender, EventArgs e)
        {
            if (openOtaFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                FileStream otaFileStream = null;

                try
                {
                    otaFileStream = File.OpenRead(openOtaFileDialog.FileName);
                    otaFileStream.Read(au8OTAFile, 0, Convert.ToInt32(otaFileStream.Length));

                    byte[] au8OtaFileHeaderString = null;
                    au8OtaFileHeaderString = new byte[32];
                    byte i;

                    for (i = 0; i < 32; i++)
                    {
                        au8OtaFileHeaderString[i] = au8OTAFile[20 + i];
                    }

                    u32OtaFileIdentifier = BitConverter.ToUInt32(au8OTAFile, 0);
                    u16OtaFileHeaderVersion = BitConverter.ToUInt16(au8OTAFile, 4);
                    u16OtaFileHeaderLength = BitConverter.ToUInt16(au8OTAFile, 6);
                    u16OtaFileHeaderControlField = BitConverter.ToUInt16(au8OTAFile, 8);
                    u16OtaFileManufacturerCode = BitConverter.ToUInt16(au8OTAFile, 10);
                    u16OtaFileImageType = BitConverter.ToUInt16(au8OTAFile, 12);
                    u32OtaFileVersion = BitConverter.ToUInt32(au8OTAFile, 14);
                    u16OtaFileStackVersion = BitConverter.ToUInt16(au8OTAFile, 18);
                    u32OtaFileTotalImage = BitConverter.ToUInt32(au8OTAFile, 52);
                    u8OtaFileSecurityCredVersion = au8OTAFile[56];
                    u64OtaFileUpgradeFileDest = BitConverter.ToUInt64(au8OTAFile, 57);
                    u16OtaFileMinimumHwVersion = BitConverter.ToUInt16(au8OTAFile, 65);
                    u16OtaFileMaxHwVersion = BitConverter.ToUInt16(au8OTAFile, 67);

                    textBoxOtaFileID.Text = u32OtaFileIdentifier.ToString("X4");
                    textBoxOtaFileHeaderVer.Text = u16OtaFileHeaderVersion.ToString("X2");
                    textBoxOtaFileHeaderLen.Text = u16OtaFileHeaderLength.ToString("X2");
                    textBoxOtaFileHeaderFCTL.Text = u16OtaFileHeaderControlField.ToString("X2");
                    textBoxOtaFileManuCode.Text = u16OtaFileManufacturerCode.ToString("X4");
                    textBoxOtaFileImageType.Text = u16OtaFileImageType.ToString("X4");
                    textBoxOtaFileVersion.Text = u32OtaFileVersion.ToString("X8");
                    textBoxOtaFileStackVer.Text = u16OtaFileStackVersion.ToString("X2");
                    textBoxOtaFileSize.Text = u32OtaFileTotalImage.ToString();
                    textBoxOtaFileHeaderStr.Text = System.Text.Encoding.Default.GetString(au8OtaFileHeaderString);

                    sendOtaLoadNewImage(0x02, 0x0000, u32OtaFileIdentifier, u16OtaFileHeaderVersion, u16OtaFileHeaderLength, u16OtaFileHeaderControlField, u16OtaFileManufacturerCode, u16OtaFileImageType, u32OtaFileVersion, u16OtaFileStackVersion, au8OtaFileHeaderString, u32OtaFileTotalImage, u8OtaFileSecurityCredVersion, u64OtaFileUpgradeFileDest, u16OtaFileMinimumHwVersion, u16OtaFileMaxHwVersion);
                }
                finally
                {
                    if (otaFileStream != null)
                    {
                        otaFileStream.Close();
                        otaFileStream.Dispose();
                    }
                }
            }
        }

        #endregion

        #region group cluster button handlers

        private void buttonGroupAddIfIdentifying_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16GroupAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxGroupAddIfIndentifyingTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxGroupAddIfIdentifySrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxGroupAddIfIdentifyDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxGroupAddIfIdentifyGroupID.Text, out u16GroupAddr) == true)
                        {
                            sendGroupAddIfIdentifying(u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupAddr);
                        }
                    }
                }
            }
        }

        private void buttonGroupRemoveAll_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxRemoveAllGroupTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxRemoveAllGroupSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxRemoveAllGroupDstEp.Text, out u8DstEndPoint) == true)
                    {
                        sendGroupRemoveAll(u16TargetAddr, u8SrcEndPoint, u8DstEndPoint);
                    }
                }
            }
        }

        private void buttonRemoveGroup_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16GroupAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxRemoveGroupTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxRemoveGroupSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxRemoveGroupDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxRemoveGroupGroupAddr.Text, out u16GroupAddr) == true)
                        {
                            sendGroupRemove(u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupAddr);
                        }
                    }
                }
            }
        }

        private void buttonGetGroup_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8GroupCount;
            UInt16[] au16GroupList = new UInt16[8];

            if (bStringToUint16(textBoxGetGroupTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxGetGroupSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxGetGroupDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint8(textBoxGetGroupCount.Text, out u8GroupCount) == true)
                        {
                            if (listManager.getListCount() >= u8GroupCount)
                            {
                                if (bStringToUint16Array(listManager.getListAsString(), out au16GroupList) == true)
                                {
                                    sendGroupGet(u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u8GroupCount, au16GroupList);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid Parameter");
                            }
                        }
                    }
                }
            }
        }

        private void buttonViewGroup_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16GroupAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxViewGroupAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxViewGroupSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxViewGroupDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxViewGroupGroupAddr.Text, out u16GroupAddr) == true)
                        {
                            sendViewGroup(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupAddr);
                        }
                    }
                }
            }
        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16GroupAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8GroupNameLength;
            byte u8GroupNameMaxLength;

            if (bStringToUint16(textBoxAddGroupAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxAddGroupSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxAddGroupDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxAddGroupGroupAddr.Text, out u16GroupAddr) == true)
                        {
                            if (bStringToUint8(textBoxGroupNameLength.Text, out u8GroupNameLength) == true)
                            {
                                if (bStringToUint8(textBoxGroupNameMaxLength.Text, out u8GroupNameMaxLength) == true)
                                {
                                    sendGroupAdd(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupAddr, u8GroupNameLength, u8GroupNameMaxLength, textBoxGroupName.Text);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region on/off cluster button handlers

        private void buttonOnOff_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxOnOffAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxOnOffSrcEndPoint.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxOnOffDstEndPoint.Text, out u8DstEndPoint) == true)
                    {
                        sendClusterOnOff((byte)comboBoxOnOffAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, (byte)comboBoxOnOffCommand.SelectedIndex);
                    }
                }
            }
        }

        private void buttonOnOffTimed_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region level cluster button handlers

        private void buttonMoveToLevel_Click_1(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16TransTime;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8Level;

            if (bStringToUint16(textBoxMoveToLevelAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxMoveToLevelSrcEndPoint.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxMoveToLevelDstEndPoint.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint8(textBoxMoveToLevelLevel.Text, out u8Level) == true)
                        {
                            if (bStringToUint16(textBoxMoveToLevelTransTime.Text, out u16TransTime) == true)
                            {
                                sendClusterMoveToLevel((byte)comboBoxMoveToLevelAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, (byte)comboBoxMoveToLevelOnOff.SelectedIndex, u8Level, u16TransTime);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region identify cluster button handlers

        private void buttonIdSend_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            UInt16 u16Time;

            if (bStringToUint16(textBoxSendIdAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxSendIdSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxIdSendDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxIdSendTime.Text, out u16Time) == true)
                        {
                            sendIdentify(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16Time);
                        }
                    }
                }
            }
        }

        private void buttonIdQuery_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxIdQueryAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxIdQuerySrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxIdQueryDstEp.Text, out u8DstEndPoint) == true)
                    {
                        sendIdentifyQuery(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint);
                    }
                }
            }
        }

        #endregion

        #region scene cluster button handlers

        private void buttonRemoveScene_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16GroupId;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8SceneId;

            if (bStringToUint16(textBoxRemoveSceneAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxRemoveSceneSrcEndPoint.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxRemoveSceneDstEndPoint.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxRemoveSceneGroupID.Text, out u16GroupId) == true)
                        {
                            if (bStringToUint8(textBoxRemoveSceneSceneID.Text, out u8SceneId) == true)
                            {
                                sendRemoveScene((byte)comboBoxRemoveSceneAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupId, u8SceneId);
                            }
                        }
                    }
                }
            }
        }

        private void buttonRemoveAllScenes_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16GroupId;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxRemoveAllScenesAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxRemoveAllScenesSrcEndPoint.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxRemoveAllScenesDstEndPoint.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxRemoveAllScenesGroupID.Text, out u16GroupId) == true)
                        {
                            sendRemoveAllScenes((byte)comboBoxRemoveAllScenesAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupId);
                        }
                    }
                }
            }
        }

        private void buttonGetSceneMembership_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16GroupId;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxGetSceneMembershipAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxGetSceneMembershipSrcEndPoint.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxGetSceneMembershipDstEndPoint.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxGetSceneMembershipGroupID.Text, out u16GroupId) == true)
                        {
                            sendGetSceneMembership((byte)comboBoxGetSceneMembershipAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupId);
                        }
                    }
                }
            }
        }

        private void buttonAddScene_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16GroupId;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8SceneId;
            UInt16 u16TransTime;
            byte u8NameLen;
            byte u8NameMaxLen;
            UInt16 u16SceneLength;
            string stringSceneData;

            if (bStringToUint16(textBoxAddSceneAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxAddSceneSrcEndPoint.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxAddSceneDstEndPoint.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxAddSceneGroupId.Text, out u16GroupId) == true)
                        {
                            if (bStringToUint8(textBoxAddSceneSceneId.Text, out u8SceneId) == true)
                            {
                                if (checkBoxShowExtension.Checked == true)
                                {
                                    if (bStringToUint16(textBoxAddSceneTransTime.Text, out u16TransTime) == true)
                                    {
                                        if (bStringToUint8(textBoxAddSceneNameLen.Text, out u8NameLen) == true)
                                        {
                                            if (bStringToUint8(textBoxAddSceneMaxNameLen.Text, out u8NameMaxLen) == true)
                                            {
                                                if (bStringToUint16(textBoxAddSceneExtLen.Text, out u16SceneLength) == true)
                                                {
                                                    stringSceneData = textBoxAddSceneData.Text;
                                                    sendAddSceneExtData((byte)comboBoxAddSceneAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupId, u8SceneId, u16TransTime, textBoxAddSceneName.Text, u8NameLen, u8NameMaxLen, u16SceneLength, stringSceneData);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    sendAddScene((byte)comboBoxAddSceneAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupId, u8SceneId);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonViewScene_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16GroupId;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8SceneId;

            if (bStringToUint16(textBoxViewSceneAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxViewSceneSrcEndPoint.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxViewSceneDstEndPoint.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxViewSceneGroupId.Text, out u16GroupId) == true)
                        {
                            if (bStringToUint8(textBoxViewSceneSceneId.Text, out u8SceneId) == true)
                            {
                                sendViewScene((byte)comboBoxViewSceneAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupId, u8SceneId);
                            }
                        }
                    }
                }
            }
        }

        private void buttonStoreScene_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16GroupId;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8SceneId;

            if (bStringToUint16(textBoxStoreSceneAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxStoreSceneSrcEndPoint.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxStoreSceneDstEndPoint.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxStoreSceneGroupId.Text, out u16GroupId) == true)
                        {
                            if (bStringToUint8(textBoxStoreSceneSceneId.Text, out u8SceneId) == true)
                            {
                                sendStoreScene((byte)comboBoxStoreSceneAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupId, u8SceneId);
                            }
                        }
                    }
                }
            }
        }

        private void buttonRecallScene_Click_1(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            UInt16 u16GroupId;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8SceneId;

            if (bStringToUint16(textBoxRecallSceneAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxRecallSceneSrcEndPoint.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxRecallSceneDstEndPoint.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxRecallSceneGroupId.Text, out u16GroupId) == true)
                        {
                            if (bStringToUint8(textBoxRecallSceneSceneId.Text, out u8SceneId) == true)
                            {
                                sendRecallScene((byte)comboBoxRecallSceneAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16GroupId, u8SceneId);
                            }
                        }
                    }
                }
            }
        }

        private void buttonDiscoverDevices_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr = 0x0000;
            byte u8StartIndex = 0;

            comboBoxAddressList.Items.Clear();
            sendMgmtLqiRequest(u16ShortAddr, u8StartIndex);
        }

        private void buttonCopyAddr_Click(object sender, EventArgs e)
        {
            textBoxExtAddr.SelectAll();
            textBoxExtAddr.Copy();
        }

        #endregion

        #region color cluster button handlers

        private void buttonMoveToColorTemp_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            UInt16 u16ColorTemp;
            UInt16 u16TransTime;

            if (bStringToUint16(textBoxMoveToColorTempAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxMoveToColorTempSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxMoveToColorTempDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16Decimal(textBoxMoveToColorTempTemp.Text, out u16ColorTemp) == true)
                        {
                            u16ColorTemp = (UInt16)((UInt32)1000000 / (UInt32)u16ColorTemp);

                            if (bStringToUint16(textBoxMoveToColorTempRate.Text, out u16TransTime) == true)
                            {
                                sendMoveToColorTemp(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16ColorTemp, u16TransTime);
                            }
                        }
                    }
                }
            }
        }

        private void buttonMoveToColor_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            UInt16 u16X;
            UInt16 u16Y;
            UInt16 u16Time;

            if (bStringToUint16(textBoxMoveToColorAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxMoveToColorSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxMoveToColorDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxMoveToColorX.Text, out u16X) == true)
                        {
                            if (bStringToUint16(textBoxMoveToColorY.Text, out u16Y) == true)
                            {
                                if (bStringToUint16(textBoxMoveToColorTime.Text, out u16Time) == true)
                                {
                                    sendMoveToColor(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16X, u16Y, u16Time);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonMoveToHue_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8Hue;
            byte u8Direction;
            UInt16 u16Time;

            if (bStringToUint16(textBoxMoveToHueAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxMoveToHueSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxMoveToHueDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint8(textBoxMoveToHueHue.Text, out u8Hue) == true)
                        {
                            if (bStringToUint8(textBoxMoveToHueDir.Text, out u8Direction) == true)
                            {
                                if (bStringToUint16(textBoxMoveToHueTime.Text, out u16Time) == true)
                                {
                                    sendMoveToHue(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u8Hue, u8Direction, u16Time);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonMoveToSat_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8Sat;
            UInt16 u16Time;

            if (bStringToUint16(textBoxMoveToSatAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxMoveToSatSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxMoveToSatDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint8(textBoxMoveToSatSat.Text, out u8Sat) == true)
                        {
                            if (bStringToUint16(textBoxMoveToSatTime.Text, out u16Time) == true)
                            {
                                sendMoveToSat(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u8Sat, u16Time);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region lock cluster button handlers

        private void buttonLockUnlock_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;

            if (bStringToUint16(textBoxLockUnlockAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxLockUnlockSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxLockUnlockDstEp.Text, out u8DstEndPoint) == true)
                    {
                        sendLockUnlock(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, (byte)comboBoxLockUnlock.SelectedIndex);
                    }
                }
            }
        }

        #endregion

        #region IAS cluster button handlers

        private void buttonEnrollResponse_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8ZoneId;

            if (bStringToUint16(textBoxEnrollRspAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxEnrollRspSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxEnrollRspDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint8(textBoxEnrollRspZone.Text, out u8ZoneId) == true)
                        {
                            sendIASEnrollResponse((byte)comboBoxEnrollRspAddrMode.SelectedIndex, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, (byte)comboBoxEnrollRspCode.SelectedIndex, u8ZoneId);
                        }
                    }
                }
            }
        }
        #endregion

        #region touchlink command button handlers

        private void buttonZllTouchlinkFactoryReset_Click(object sender, EventArgs e)
        {
            sendTouchlinkFactoryReset();
        }

        private void buttonZllTouchlinkInitiate_Click(object sender, EventArgs e)
        {
            sendTouchlinkInitiate();
        }

        #endregion

        #region ZLL on/off command button handlers

        private void buttonZllOnOffEffects_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            byte u8EffectGrad;

            if (bStringToUint16(textBoxZllOnOffEffectsAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxZllOnOffEffectsSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxZllOnOffEffectsDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint8(textBoxZllOnOffEffectsGradient.Text, out u8EffectGrad) == true)
                        {
                            sendZllClusterOnOff(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, (byte)comboBoxZllOnOffEffectID.SelectedIndex, u8EffectGrad);
                        }
                    }
                }
            }
        }

        #endregion

        #region ZLL color cluster button handlers

        private void buttonZllMoveToHue_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            UInt16 u16Hue;
            byte u8Direction;
            UInt16 u16Time;

            if (bStringToUint16(textBoxZllMoveToHueAddr.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxZllMoveToHueSrcEp.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxZllMoveToHueDstEp.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxZllMoveToHueHue.Text, out u16Hue) == true)
                        {
                            if (bStringToUint8(textBoxZllMoveToHueDirection.Text, out u8Direction) == true)
                            {
                                if (bStringToUint16(textBoxZllMoveToHueTransTime.Text, out u16Time) == true)
                                {
                                    sendEnhancedMoveToHue(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16Hue, u8Direction, u16Time);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Poll Control Cluster Button Handlers

        private void buttonSetCheckinRspData_Click(object sender, EventArgs e)
        {
            UInt16 u16PollControlFastPollExpiry = 0;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            UInt16 u16ShortAddr;
            if (bStringToUint16(textBoxPollCheckInAddress.Text, out u16ShortAddr) == true)
            {
                if (bStringToUint8(textBoxPollCheckInSrcEndPointID.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxCheckInDstEndPointID.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxFastPollExpiryTime.Text, out u16PollControlFastPollExpiry) == true)
                        {
                            sendPollControlCheckInResponseValues(u16PollControlFastPollExpiry, u16ShortAddr, (byte)comboBoxFastPollEnable.SelectedIndex, u8SrcEndPoint, u8DstEndPoint);
                        }
                    }
                }
            }
        }

        private void buttonPollSetLongPollInterval_Click(object sender, EventArgs e)
        {
            UInt16 u16PollLongPollInterval = 0;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            if (bStringToUint16(textBoxPollLongPollInterval.Text, out u16PollLongPollInterval) == true)
            {
                sendPollControlLongPollInterval(u16PollLongPollInterval);
            }
        }

        private void buttonPollSetShortPollInterval_Click(object sender, EventArgs e)
        {
            UInt16 u16PollShortPollInterval = 0;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            if (bStringToUint16(textBoxShortPollInterval.Text, out u16PollShortPollInterval) == true)
            {
                sendPollControlShortPollInterval(u16PollShortPollInterval);
            }
        }

        #endregion

        #region EZ LNT Button Handlers

        private static string[] GetPorts()
        {
            string[] strArray = DevInfo.DeviceInfo.ParsePorts();//.parseFriendlyPorts();

            return strArray;
        }
        private void buttonREFRESHCOM_Click(object sender, EventArgs e)
        {
            //close port first
            try
            {
                for (int j = 0; j < multiUSBport.Count; j++)
                {
                    if (multiUSBport[j].IsOpen)
                    {
                        multiUSBport[j].Close();
                        multiPortOpen = false;

                    }
                }

                if (multiPortOpen == false)
                {
                    buttonPort.Text = "Open Multi Port";
                    bMultiPortConfigured = false;
                    multiUSBport.Clear();
                   // listViewEZLNTGROUP.Clear();
                    indexComInMultiPortHashTable.Clear();
                    {   //clear previous info

                        // listViewEZLNTINFO.Items.Clear();
                        // comHashTable.Clear();

                        comIndex.Clear();
                        nwkAddr.Clear();
                        IEEEAddr.Clear();
                        channel.Clear();
                        chip.Clear();
                        profile.Clear();
                        panID.Clear();
                        type.Clear();
                        ver.Clear();
                        indexList.Clear();

                    }
                }
            }

            catch (System.Exception ex)
            {
                MessageBox.Show("Error - openPortToolStripMenuItem_Click Exception: " + ex);
            }


            // Get a list of the Serial port names
            string[] ports = GetPorts();
            nodeResetCount = 0;
            readComStringList.Clear();
            checkBoxEZLNTALL.Checked = false;
            multiUSBport.Clear();
            indexComInMultiPortHashTable.Clear();
            listViewEZLNTGROUP.Items.Clear();
            listViewEZLNTINFO.Items.Clear();
            comHashTable.Clear();
            indexComHashTable.Clear();
            comGroupHashTable.Clear();
            indexMacAddrHashTable.Clear();
            groupIndex = 0;
            newCOMIntem = 0;
            ffffCount = 0;
            sameCount = 0;
            comIndex.Clear();
            nwkAddr.Clear();
            IEEEAddr.Clear();
            channel.Clear();
            chip.Clear();
            profile.Clear();
            panID.Clear();
            type.Clear();
            ver.Clear();
            indexList.Clear();

            int i = 0;
            foreach (string s in ports)
            {
                if (s != "")
                {
                    SerialPort serialPorttemp = new SerialPort();
                    serialPorttemp.PortName = s;
                    if (!serialPorttemp.IsOpen)
                    {
                        comHashTable.Add(s, i);
                        indexComHashTable.Add(i, s);
                        ListViewItem item = new ListViewItem(i.ToString() + ". " + s);//com
                        item.SubItems.Add("");  //NwkAddr
                        item.SubItems.Add("");  // MACAddr 
                        item.SubItems.Add("");  //Channel
                        item.SubItems.Add("");   //Type
                        item.SubItems.Add(""); //Ver
                        item.SubItems.Add("Local"); //Loc
                        item.SubItems.Add(""); //Chip
                        item.SubItems.Add(""); //profile
                        item.SubItems.Add(""); //ip
                        item.SubItems.Add(""); //panid

                        listViewEZLNTINFO.Items.Insert(i++, item);
                    }
                }
            };

            for (int j = 0; j < listViewEZLNTINFO.Items.Count; j++)
            {

                listViewEZLNTINFO.Items[j].Checked = true;

            }

            //  queryForInfo();

        }

        void queryForInfo()
        {
            if (multiPortOpen)
            {
                // MessageBox.Show("The port must be closed before changing the settings");

                try
                {
                    for (int i = 0; i < multiUSBport.Count; i++)
                    {
                        if (multiUSBport[i].IsOpen)
                        {
                            multiUSBport[i].Close();
                            multiPortOpen = false;

                        }
                    }

                    if (multiPortOpen == false)
                    {
                        buttonPort.Text = "Open Multi Port";
                        bMultiPortConfigured = false;
                        multiUSBport.Clear();

                        listViewEZLNTGROUP.Items.Clear();
                        groupIndex = 0;
                        comGroupHashTable.Clear();


                        indexComInMultiPortHashTable.Clear();
                        //clear previous info

                        // listViewEZLNTINFO.Items.Clear();
                        // comHashTable.Clear();

                        comIndex.Clear();
                        nwkAddr.Clear();
                        IEEEAddr.Clear();
                        channel.Clear();
                        chip.Clear();
                        profile.Clear();
                        panID.Clear();
                        type.Clear();
                        ver.Clear();
                        indexList.Clear();

                        
                    }
                }

                catch (System.Exception ex)
                {
                    MessageBox.Show("Error - openPortToolStripMenuItem_Click Exception: " + ex);
                }

            }
            else
            {
                if (bselected)
                {
                    portCount = listViewEZLNTINFO.CheckedItems.Count;

                    multiUSBport.Clear();
                    indexComInMultiPortHashTable.Clear();
                    if (multiUSBport.Count() != portCount)
                    {
                        for (int i = 0; i < portCount; i++)
                        {
                            int index = listViewEZLNTINFO.CheckedIndices[i];
                            indexList.Add(index);
                            if ((string)indexComHashTable[index] != "")
                            {
                                SerialPort serialPorttemp = new SerialPort();
                                serialPorttemp.PortName = (string)indexComHashTable[index];
                                serialPorttemp.BaudRate = 115200;
                                serialPorttemp.DataBits = 8;
                                serialPorttemp.Parity = Parity.None;
                                serialPorttemp.StopBits = StopBits.One;
                                serialPorttemp.DataReceived += new SerialDataReceivedEventHandler(serialPortMulti_DataReceivedHandler);
                                if (!serialPorttemp.IsOpen)
                                {
                                    multiUSBport.Add(serialPorttemp);
                                    indexComInMultiPortHashTable.Add(serialPorttemp.PortName, multiUSBport.Count - 1);
                                }
                                else
                                {
                                    MessageBox.Show("some port is open\n open port failed");
                                    listViewEZLNTINFO.Items[index].Checked = false;
                                    return;
                                }
                            }
                        }
                    }

                    richTextBoxMessageView.Text += "\r\n";
                    richTextBoxMessageView.Text += "Selected COM: \r\n";
                    foreach (int i in indexList)
                    {
                        richTextBoxMessageView.Text += i;
                        richTextBoxMessageView.Text += ", ";
                    }
                    richTextBoxMessageView.Text += "\r\n";

                    bMultiPortConfigured = true;
                }
                else
                {
                    MessageBox.Show("None port has been selected!");
                    return;
                }

                if (bMultiPortConfigured == true)
                {
                    try
                    {
                        for (int i = 0; i < multiUSBport.Count; i++)
                        {
                            if (!multiUSBport[i].IsOpen)
                            {
                                multiUSBport[i].Open();
                                multiPortOpen = true;

                            }
                            //                  displayPortSettings();
                        }

                        if (multiPortOpen)
                        {
                            buttonPort.Text = "Close Multi Port";

                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Error - openPortToolStripMenuItem_Click Exception: " + ex);
                    }
                }
                else
                {
                    MessageBox.Show("Error - No Port Selected");
                }

                if (multiPortOpen)
                {
                    byte u8Command;
                    u8Command = (byte)0x69;
                    comIndex.Clear();
                    nwkAddr.Clear();
                    IEEEAddr.Clear();
                    channel.Clear();
                    chip.Clear();
                    profile.Clear();
                    panID.Clear();
                    type.Clear();
                    ver.Clear();
                    {
                        byte[] dataArray = null;
                        dataArray = new byte[1];
                        dataArray[0] = u8Command;
                        sendCommand = dataArray[0];
                        for (int i = 0; i < multiUSBport.Count; i++)
                        {

                            if (multiUSBport[i].IsOpen)
                            {
                                multiUSBport[i].Write(dataArray, 0, 1);
                                //   Console.WriteLine("Write success");
                            }
                        }
                    }

                }

            }
        }
        private void buttonPort_Click(object sender, EventArgs e)
        {
            queryForInfo();
        }

        #region Socket

        private void buttonSOCKETSEVERTEST_Click(object sender, EventArgs e)
        {
            if (asClient ==1)
            {
                string i = "sever test ";
                Dictionary<string, Socket>.ValueCollection valCol = clientConnectionItems.Values;
                foreach (Socket s in valCol)
                    Send(i, s);
            }
        }

        private void buttonSOCKETCLIENTTEST_Click(object sender, EventArgs e)
        {
            if (asClient ==2)
            {
                string i = "client test ";
                {
                    Send(i, clientSocket);
                }
            }
        }

        private void buttonEZLNTSOCKETSEVER_Click(object sender, EventArgs e)
        {
            if (asClient!=1)
            {
                asClient = 1;  //sever
                if (textBoxEZLNTSOCKETSEVERIP.Text == "")
                {
                    string resultIP = string.Empty;
                    System.Net.IPAddress[] ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
                    foreach (System.Net.IPAddress ip in ips)
                    {
                        if (IsCorrentIP(ip.ToString()))
                        {
                            resultIP = ip.ToString();
                            break;
                        }
                    }
                    textBoxEZLNTSOCKETSEVERIP.Text = resultIP;
                }

                if (textBoxEZLNTSOCKETSEVERIP.Text != "")
                {
                    string ipStr = textBoxEZLNTSOCKETSEVERIP.Text;
                    IPAddress ip = IPAddress.Parse(ipStr);

                    serverSocket.Bind(new IPEndPoint(ip, myProt));
                    serverSocket.Listen(20);
                    string s = serverSocket.LocalEndPoint.ToString();
                    richTextBoxMessageView.Text += "listen:";
                    richTextBoxMessageView.Text += s;
                    richTextBoxMessageView.Text += "\n";

                    Thread myWatchThread = new Thread(ListenClientConnect);
                    myWatchThread.IsBackground = true;
                    myWatchThread.Start();
                }
            }

        }

        public void Connect(IPAddress ip, int port)
        {
            this.clientSocket.BeginConnect(ip, port, new AsyncCallback(ConnectCallback), this.clientSocket);
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                handler.EndConnect(ar);
            }
            catch (SocketException ex)
            { }
        }


        private void Send(string data,Socket tempSocket)
        {
            
            try
            {
                tempSocket.Send(Encoding.ASCII.GetBytes(data));
                Thread.Sleep(200);
            }
            catch (SocketException ex)
            {

            }
        }

      
        public void ReceiveData()    //client
        {

            clientSocket.BeginReceive(resultClient, 0, resultClient.Length, 0, new AsyncCallback(ReceiveCallback), null);
        }

        string command = string.Empty;
        private void ReceiveCallback(IAsyncResult ar)
        {
            multiMessageParser = new MultiMessageParser(myMultiMessageParser);
            lntSendCommand = new LNTSendCommand(sendCommandFun);
            socketRefreshCOMList = new SocketRefreshCOMList(mysocketRefreshCOMList);
            socketClearCOMCheckedList = new SocketClearCOMCheckedList(mysocketClearCOMCheckedList);
            try
            {
                int REnd = clientSocket.EndReceive(ar);
                if (REnd > 0)
                {
                    byte[] data = new byte[REnd];
                    Array.Copy(resultClient, 0, data, 0, REnd);

                    //deal data
                    string s = Encoding.ASCII.GetString(resultClient, 0, REnd);

                  
                    
                    if (s.Contains("sever test"))
                    {
                        clientSocket.Send(Encoding.ASCII.GetBytes("client connetion OK"));
                    }

                    if (s.Contains("new command"))
                    {
                        this.Invoke(socketClearCOMCheckedList);
                    }

                    

                    if (s.Contains("command done"))
                    {
                        this.Invoke(lntSendCommand, command);
                    }
                    string IPAddress = GetIP();
                    if (s.Contains(IPAddress))   //has IPaddess
                    {
                        //get command
                        string[] str = Regex.Split(s, " ", RegexOptions.IgnoreCase);
                        // invoke the delegate in the MainForm thread   
                        command = str[2];
                        this.Invoke(socketRefreshCOMList, str[1], str[0]);
                    }
                    else
                    {
                        command = string.Empty;
                    }
                    s += "\n";
                    char[] cc = s.ToCharArray();
                    this.Invoke(multiMessageParser, cc);
                    clientSocket.BeginReceive(resultClient, 0, resultClient.Length, 0, new AsyncCallback(ReceiveCallback), null);
                    
                }
                else
                {
                    dispose();
                }

            }
            catch (SocketException ex)
            {

            }
        }

        private void dispose()
        {
            try
            {
                this.clientSocket.Shutdown(SocketShutdown.Both);
                this.clientSocket.Close();
            }
            catch (Exception ex)
            { }
        }

        private void buttonEZLNTSOCKETCLIENT_Click(object sender, EventArgs e)
        {
            if (asClient != 2)
            {
                asClient = 2;

                for (int i=0;i< listViewEZLNTINFO.Items.Count;i++)
                {
                    listViewEZLNTINFO.Items[i].Checked = false;

                }

               // listViewEZLNTINFO.CheckBoxes = false;

                string str = textBoxEZLNTSOCKETCLIENTIP.Text;
                if (IsCorrentIP(str))
                {
                    IPAddress ip = IPAddress.Parse(str);
                    try
                    {
                        // clientSocket.Connect(new IPEndPoint(ip, 8885));
                        Connect(ip, 8885);
                        richTextBoxMessageView.Text += "connect success";
                        richTextBoxMessageView.Text += "\n";
                    }
                    catch
                    {
                        richTextBoxMessageView.Text += "connect failed";
                        richTextBoxMessageView.Text += "\n";
                        return;
                    }
                    Thread clientReceiveThread = new Thread(ReceiveData);
                    clientReceiveThread.IsBackground = true;
                    clientReceiveThread.Start();
                }
                else
                {
                    MessageBox.Show("Wrong IPAddress");
                }
            }
        }

        Socket searchSocket(string IPAddress)
        {
            Socket sSocket=null;
            IPAddress clientIP;
            Dictionary<string, Socket>.ValueCollection valCol = clientConnectionItems.Values;
            foreach (Socket s in valCol)
            {
                clientIP = (s.RemoteEndPoint as IPEndPoint).Address;
                if (clientIP.ToString() == IPAddress)
                {
                    sSocket = s;
                    break;
                }
            }
            
            return sSocket;
        }

        #endregion

        bool newcommand = true;
        void sendCommandFun(string command)
        {
            if (command != string.Empty)
            {
                nodeResetCount = 0;
                checkedItems();
                newcommand = true;
                byte u8command;
                byte[] array = System.Text.Encoding.ASCII.GetBytes(command);
                u8command = array[0];

                {
                    comIndex.Clear();
                    nwkAddr.Clear();
                    IEEEAddr.Clear();
                    channel.Clear();
                    chip.Clear();
                    profile.Clear();
                    panID.Clear();
                    type.Clear();
                    ver.Clear();

                    {

                        byte[] dataArray = null;
                        dataArray = new byte[1];
                        dataArray[0] = u8command;
                        sendCommand = dataArray[0];

                        if (sendCommand == 0x66)
                        {

                            if (checkedAddress.Count > 0)
                            {
                                deletedAddressFun();
                            }
                        }

                        for (int i = 0; i < checkedCOM.Count; i++)
                        {
                            string COM = (string)checkedCOM[i];
                            if ((string)checkedCOMLocation[i] == "Local")
                            {
                                if (multiPortOpen)
                                {
                                    if (indexComInMultiPortHashTable.Contains(COM))
                                    {
                                        if (multiUSBport[((int)indexComInMultiPortHashTable[COM])].IsOpen)
                                        {

                                            multiUSBport[((int)indexComInMultiPortHashTable[COM])].Write(dataArray, 0, 1);
                                            //   Console.WriteLine("Write success");
                                        }
                                    }
                                    else
                                    {
                                        richTextBoxMessageView.Text += "\nThe port ";
                                        richTextBoxMessageView.Text += COM;
                                        richTextBoxMessageView.Text += " has not opened!\n";
                                    }
                                }
                            }
                            else if ((string)checkedCOMLocation[i] == "Remote")
                            {
                                if (asClient == 1)  //sever
                                {
                                    string MACAddress = listViewEZLNTINFO.Items[(int)checkedIndex[i]].SubItems[2].Text;
                                    string IPAddress = listViewEZLNTINFO.Items[(int)checkedIndex[i]].SubItems[9].Text;
                                    Socket sSocket = searchSocket(IPAddress);
                                    if (asClient == 1 && newcommand)
                                    {
                                        newcommand = false;
                                        Send("new command", sSocket);

                                    }
                                    string socketSendCommandStr = IPAddress + " " + MACAddress + " " + textBoxEZLNTCOMMAND.Text;
                                    if (sSocket != null)
                                    {
                                        Send(socketSendCommandStr, sSocket);

                                        // Thread.Sleep(200);
                                    }
                                }
                            }
                        }
                        if (asClient == 1)  //sever
                        {
                            string i = "command done";
                            Dictionary<string, Socket>.ValueCollection valCol = clientConnectionItems.Values;
                            foreach (Socket s in valCol)
                                Send(i, s);
                        }

                    }
                }
            }

        }

        private void buttonEZLNTSENDCOMMAND_Click(object sender, EventArgs e)
        {
                       
            if (listViewEZLNTINFO.CheckedItems.Count != 0)
            {
               
                {
                    sendCommandFun(textBoxEZLNTCOMMAND.Text);
                }
            }
            else
            {
                MessageBox.Show("None port selected");

            }
        }

        public void comRead(string path)
        {

            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {

                readComStringList.Add(line);

            }
            sr.Close();

        }



        private void buttonEZLNTLOADSCRIPT_Click(object sender, EventArgs e)
        {
            readComStringList.Clear();
            var f = new OpenFileDialog();
            //f.Multiselect = true; 
            if (f.ShowDialog() == DialogResult.OK)
            {
                String filepath = f.FileName;
                String filename = f.SafeFileName;
                comPath = filepath;

                // Get a list of the Serial port names

                comRead(comPath);
                checkBoxEZLNTALL.Checked = false;
                multiUSBport.Clear();
                indexComInMultiPortHashTable.Clear();
                comHashTable.Clear();
                indexComHashTable.Clear();
                comGroupHashTable.Clear();
                listViewEZLNTGROUP.Items.Clear();
                listViewEZLNTINFO.Items.Clear();
                groupIndex = 0;
                comIndex.Clear();
                nwkAddr.Clear();
                IEEEAddr.Clear();
                channel.Clear();
                chip.Clear();
                profile.Clear();
                panID.Clear();
                type.Clear();
                ver.Clear();
                indexList.Clear();

                int i = 0;
                foreach (string s in readComStringList)
                {
                    if (s != "")
                    {
                        string[] sArray = Regex.Split(s, ",", RegexOptions.IgnoreCase);

                        //  comHashTable.Add(sArray, i);
                        //  indexComHashTable.Add(i, s);
                        ListViewItem item = new ListViewItem(i.ToString() + ". ");


                        string[] Nwkrray = Regex.Split(sArray[2], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(Nwkrray[1]);

                        ListViewItem itemGroup = new ListViewItem(i.ToString() + ". " + Nwkrray[1]);
                        itemGroup.SubItems.Add("");
                        string str = Nwkrray[1].Remove(0, Nwkrray[1].Length - 4);
                        comGroupHashTable.Add(str, groupIndex++);

                        string[] IEEEArray = Regex.Split(sArray[3], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(IEEEArray[1]);

                        string[] ChaArray = Regex.Split(sArray[1], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(ChaArray[1]);

                        string[] TypeArray = Regex.Split(sArray[5], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(TypeArray[1]);

                        string[] VerArray = Regex.Split(sArray[4], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(VerArray[1]);

                        listViewEZLNTINFO.Items.Insert(i, item);
                        listViewEZLNTGROUP.Items.Insert(i++, itemGroup);

                    }
                }

                for (int j = 0; j < listViewEZLNTGROUP.Items.Count; j++)
                {
                    listViewEZLNTGROUP.Items[j].Checked = true;
                }
            }

        }


        private void buttonEZLNTVIEWGROUP_Click(object sender, EventArgs e)
        {
            UInt16 u16GroupAddr;
            UInt16 u16ShortAddr;
            removeAllGroup = false;
            if (bStringToUint16(textBoxEZLNTVIEW.Text, out u16GroupAddr) == true)
            {
                for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
                {
                    int index = listViewEZLNTGROUP.CheckedIndices[i];
                    string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                    if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                    {
                        sendViewGroup(u16ShortAddr, 1, 1, u16GroupAddr);
                    }
                }
            }
        }

        private void buttonREMOVEGROUPALL_Click(object sender, EventArgs e)
        {
            removeAllGroup = true;
            UInt16 u16ShortAddr;
            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    sendGroupRemoveAll(u16ShortAddr, 1, 1);
                }
            }
        }

        private void buttonEZLNTREMOVEGROUP_Click(object sender, EventArgs e)
        {
            UInt16 u16GroupAddr;
            UInt16 u16ShortAddr;
            removeAllGroup = false;
            if (bStringToUint16(textBoxREMOVEGROUP.Text, out u16GroupAddr) == true)
            {
                for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
                {
                    int index = listViewEZLNTGROUP.CheckedIndices[i];
                    string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                    if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                    {
                        sendGroupRemove(u16ShortAddr, 1, 1, u16GroupAddr);
                    }
                }
            }
        }

        private void buttonEZLNTADDGROUP_Click(object sender, EventArgs e)
        {
            UInt16 u16GroupAddr;
            UInt16 u16ShortAddr;
            removeAllGroup = false;
            if (bStringToUint16(textBoxEZLNTADDGROUP.Text, out u16GroupAddr) == true)
            {
                for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
                {
                    int index = listViewEZLNTGROUP.CheckedIndices[i];
                    string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                    if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                    {
                        sendGroupAdd(u16ShortAddr, 1, 1, u16GroupAddr, 0, 0, "");
                    }
                }
            }
        }

        private void buttonEZLNTWRITEATTRIBUTE_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            UInt16 u16AttribID;
            UInt16 u16ManuID = 0;
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            byte u8AttribType;
            byte[] au8Data = new byte[64];
            byte u8DataLen = 0;

            for (int j = 0; j < listViewEZLNTGROUP.CheckedItems.Count; j++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[j];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {

                    if (bStringToUint16(textBoxEZLNTWRITEATTRIBUTECLUSTERID.Text, out u16ClusterID) == true)
                    {
                        if (bStringToUint16(textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.Text, out u16AttribID) == true)
                        {

                            {
                                if (bStringToUint8(textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.Text, out u8AttribType) == true)
                                {
                                    if (u8AttribType == 0x42)
                                    {
                                        // if the data is a character string get the length make make this is the first byte                                         
                                        au8Data[0] = (byte)System.Text.Encoding.ASCII.GetBytes(textBoxEZLNTWRITEATTRIBUTEDATA.Text, 0, textBoxEZLNTWRITEATTRIBUTEDATA.TextLength, au8Data, 1);
                                        u8DataLen = au8Data[0];
                                        u8DataLen++;
                                    }
                                    else if (u8AttribType == 0x21)
                                    {
                                        UInt16 u16Data;

                                        /* Data is a uint16 */
                                        if (bStringToUint16(textBoxEZLNTWRITEATTRIBUTEDATA.Text, out u16Data) == true)
                                        {
                                            u8DataLen = 2;
                                            au8Data[1] = (byte)u16Data;
                                            au8Data[0] = (byte)(u16Data >> 8);
                                        }
                                    }
                                    else if (u8AttribType == 0xf0)
                                    {
                                        /* Data is a uint64 */
                                        UInt64 u64MACaddress;
                                        if (bStringToUint64(textBoxEZLNTWRITEATTRIBUTEDATA.Text, out u64MACaddress) == true)
                                        {
                                            u8DataLen = 8;
                                            au8Data[7] = (byte)u64MACaddress;
                                            au8Data[6] = (byte)(u64MACaddress >> 8);
                                            au8Data[5] = (byte)(u64MACaddress >> 16);
                                            au8Data[4] = (byte)(u64MACaddress >> 24);
                                            au8Data[3] = (byte)(u64MACaddress >> 32);
                                            au8Data[2] = (byte)(u64MACaddress >> 40);
                                            au8Data[1] = (byte)(u64MACaddress >> 48);
                                            au8Data[0] = (byte)(u64MACaddress >> 56);
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < textBoxEZLNTWRITEATTRIBUTEDATA.TextLength; i += 2)
                                        {
                                            byte u8Data = 0;
                                            if (bStringToUint8(textBoxEZLNTWRITEATTRIBUTEDATA.Text, out u8Data) == true)
                                            {
                                                au8Data[i] = u8Data;
                                            }
                                            else
                                            {
                                                return;
                                            }
                                            u8DataLen++;
                                        }
                                    }
                                    sendWriteAttribRequest(u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, 0, 0, u16ManuID, 1, u16AttribID, u8AttribType, au8Data, u8DataLen);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonEZLNTREADATTRIBUTE_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    LNTReadFlag = false;
                    Thread timerReadAttributeThread = new Thread(customertimer);
                    readAttributeThreadStop = false;
                    timerReadAttributeThread.Priority = ThreadPriority.Normal;

                    syncEventPort1 = true;
                    syncEvent.Reset();
                    timeBeginPeriod(1);
                    timerReadAttributeThread.Start();

                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonEZLNTSTOPREAD_Click(object sender, EventArgs e)
        {
            readAttributeThreadStop = true;
            timeEndPeriod(1);
        }

        byte[] fileToBuffer(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    byte[] buffer = new byte[fs.Length - 4];
                    fs.Seek(4, 0);
                    fs.Read(buffer, 0, (int)(fs.Length - 4));
                    return buffer;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }

        }

        enum teBL_MessageType
        {
            E_BL_MSG_TYPE_SET_CS_REQUEST = 0x05,
            E_BL_MSG_TYPE_SET_CS_RESPONSE = 0x06,
            E_BL_MSG_TYPE_FLASH_ERASE_REQUEST = 0x07,
            E_BL_MSG_TYPE_FLASH_ERASE_RESPONSE = 0x08,
            E_BL_MSG_TYPE_FLASH_PROGRAM_REQUEST = 0x09,
            E_BL_MSG_TYPE_FLASH_PROGRAM_RESPONSE = 0x0a,
            E_BL_MSG_TYPE_FLASH_READ_REQUEST = 0x0b,
            E_BL_MSG_TYPE_FLASH_READ_RESPONSE = 0x0c,
            E_BL_MSG_TYPE_FLASH_SECTOR_ERASE_REQUEST = 0x0d,
            E_BL_MSG_TYPE_FLASH_SECTOR_ERASE_RESPONSE = 0x0e,
            E_BL_MSG_TYPE_FLASH_WRITE_PRG_REGISTER_REQUEST = 0x0f,
            E_BL_MSG_TYPE_FLASH_WRITE_PRG_REGISTER_RESPONSE = 0x10,
            E_BL_MSG_TYPE_RESET_REQUEST = 0x14,
            E_BL_MSG_TYPE_RESET_RESPONSE = 0x15,
            E_BL_MSG_TYPE_RAM_WRITE_REQUEST = 0x1d,
            E_BL_MSG_TYPE_RAM_WRITE_RESPONSE = 0x1e,
            E_BL_MSG_TYPE_RAM_READ_REQUEST = 0x1f,
            E_BL_MSG_TYPE_RAM_READ_RESPONSE = 0x20,
            E_BL_MSG_TYPE_RAM_RUN_REQUEST = 0x21,
            E_BL_MSG_TYPE_RAM_RUN_RESPONSE = 0x22,
            E_BL_MSG_TYPE_FLASH_READ_ID_REQUEST = 0x25,
            E_BL_MSG_TYPE_FLASH_READ_ID_RESPONSE = 0x26,
            E_BL_MSG_TYPE_SET_BAUD_REQUEST = 0x27,
            E_BL_MSG_TYPE_SET_BAUD_RESPONSE = 0x28,
            E_BL_MSG_TYPE_FLASH_SELECT_TYPE_REQUEST = 0x2c,
            E_BL_MSG_TYPE_FLASH_SELECT_TYPE_RESPONSE = 0x2d,

            E_BL_MSG_TYPE_GET_CHIPID_REQUEST = 0x32,
            E_BL_MSG_TYPE_GET_CHIPID_RESPONSE = 0x33,

            /* Flash programmer extension commands */
            E_BL_MSG_TYPE_PDM_ERASE_REQUEST = 0x36,
            E_BL_MSG_TYPE_PDM_ERASE_RESPONSE = 0x37,
            E_BL_MSG_TYPE_PROGRAM_INDEX_SECTOR_REQUEST = 0x38,
            E_BL_MSG_TYPE_PROGRAM_INDEX_SECTOR_RESPONSE = 0x39,
            E_BL_MSG_TYPE_EEPROM_READ_REQUEST = 0x3A,
            E_BL_MSG_TYPE_EEPROM_READ_RESPONSE = 0x3B,
            E_BL_MSG_TYPE_EEPROM_WRITE_REQUEST = 0x3C,
            E_BL_MSG_TYPE_EEPROM_WRITE_RESPONSE = 0x3D,

        }

        enum teStatus
        {
            E_PRG_OK,
            E_PRG_ERROR,
            E_PRG_OUT_OF_MEMORY,
            E_PRG_ERROR_WRITING,
            E_PRG_ERROR_READING,
            E_PRG_FAILED_TO_OPEN_FILE,
            E_PRG_BAD_PARAMETER,
            E_PRG_NULL_PARAMETER,
            E_PRG_INCOMPATIBLE,
            E_PRG_INVALID_FILE,
            E_PRG_UNSUPPORED_CHIP,
            E_PRG_ABORTED,
            E_PRG_VERIFICATION_FAILED,
            E_PRG_INVALID_TRANSPORT,
            E_PRG_COMMS_FAILED,
            E_PRG_UNSUPPORTED_OPERATION,
            E_PRG_FLASH_DEVICE_UNAVAILABLE,
            E_PRG_CRP_SET,
        }

        enum teBLResponse
        {
            E_BL_RESPONSE_OK = 0x00,
            E_BL_RESPONSE_NOT_SUPPORTED = 0xff,
            E_BL_RESPONSE_WRITE_FAIL = 0xfe,
            E_BL_RESPONSE_INVALID_RESPONSE = 0xfd,
            E_BL_RESPONSE_CRC_ERROR = 0xfc,
            E_BL_RESPONSE_ASSERT_FAIL = 0xfb,
            E_BL_RESPONSE_USER_INTERRUPT = 0xfa,
            E_BL_RESPONSE_READ_FAIL = 0xf9,
            E_BL_RESPONSE_TST_ERROR = 0xf8,
            E_BL_RESPONSE_AUTH_ERROR = 0xf7,
            E_BL_RESPONSE_NO_RESPONSE = 0xf6,
            E_BL_RESPONSE_ERROR = 0xf0,
        }


        FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
        UInt32 ftdiDeviceCount = 0;
        // Create new instance of the FTDI device class
        FTDI myFtdiDevice = new FTDI();
        // Allocate storage for device info list
        FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[1024];
        Hashtable FTDICOMIndex = new Hashtable();
        private static string CmdPath = @"C:\Windows\System32\cmd.exe";

        byte chipVersion = 0;
        byte whetherErase = 0;

        void PRG_init()
        {
            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
            // Populate our device list
            ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);
            string str;
            for (uint i = 0; i < ftdiDeviceCount; i++)
            {
                //myFtdiDevice.OpenByIndex(i);
                myFtdiDevice.GetCOMPort(out str);
                FTDICOMIndex.Add(str, i);
                myFtdiDevice.Close();
            }
        }


        void ePRG_FTDI_ModeProgramming()
        {
            FTDI.FT_STATUS status = 0;
            byte[,] aaiBitModes =
            {
                { 0xC0, 0x20 }, /* Drive reset and program low */
                { 0xC4, 0x20 }, /* Drive reset high */
                { 0xCC, 0x20 }, /* Drive program high */
                { 0x0C, 0x20 }, /* Release reset and program */
                { 0x00, 0x00 }, /* Normal bit mode */
            };

            ftStatus = myFtdiDevice.ResetPort();
            status = myFtdiDevice.Purge(FTDI.FT_PURGE.FT_PURGE_TX | FTDI.FT_PURGE.FT_PURGE_RX);
            for (int i = 0; i < 5; i++)
            {
                status = myFtdiDevice.SetBitMode(aaiBitModes[i, 0], aaiBitModes[i, 1]);
                Thread.Sleep(10);
            }
            Console.WriteLine("Bootloader mode active");
            Thread.Sleep(200);
            status = myFtdiDevice.SetLatency(2);
            status = myFtdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_NONE);
            status = myFtdiDevice.Purge(FTDI.FT_PURGE.FT_PURGE_TX | FTDI.FT_PURGE.FT_PURGE_RX);
            status = myFtdiDevice.SetBaudRate(38400);
        }

        FTDI.FT_STATUS eBL_Request(teBL_MessageType eTxType, byte u8HeaderLen, byte[] pu8Header, byte u8TxLength, byte[] pu8TxData)
        {
            byte u8CheckSum = 0;
            uint bytesWritten = 0;
            byte[] au8Msg = new byte[256];
            au8Msg[0] = (byte)(u8HeaderLen + u8TxLength + 2);
            au8Msg[1] = (byte)eTxType;
            if (pu8Header != null)
            {
                Array.Copy(pu8Header, 0, au8Msg, 2, u8HeaderLen);
            }

            if (pu8TxData != null)
            {
                Array.Copy(pu8TxData, 0, au8Msg, 2 + u8HeaderLen, u8TxLength);
            }
            for (int i = 0; i < u8HeaderLen + u8TxLength + 2; i++)
            {
                u8CheckSum ^= au8Msg[i];
            }
            au8Msg[u8HeaderLen + u8TxLength + 2] = u8CheckSum;
            return myFtdiDevice.Write(au8Msg, u8HeaderLen + u8TxLength + 3, ref bytesWritten);
        }

        FTDI.FT_STATUS eBL_SetBaudrate(UInt32 u32Baudrate)
        {
            FTDI.FT_STATUS status;
            byte[] au8Buffer = new byte[6];
            UInt32 u32Divisor;

            u32Divisor = 1000000 / u32Baudrate;
            au8Buffer[0] = (byte)u32Divisor;
            au8Buffer[1] = 0;
            au8Buffer[2] = 0;
            au8Buffer[3] = 0;
            au8Buffer[4] = 0;

            status = eBL_Request(teBL_MessageType.E_BL_MSG_TYPE_SET_BAUD_REQUEST, 1, au8Buffer, 0, null);
            Thread.Sleep(2);
            return status;

        }

        void ePRG_ConnectionUpdate(UInt32 u32Baudrate)
        {
            FTDI.FT_STATUS status;
            /* Change bootloader to new speed */
            status = eBL_SetBaudrate(u32Baudrate);
            if (status != FTDI.FT_STATUS.FT_OK)
            {
                Console.WriteLine("eBL_SetBaudrate failed");
            }
            /* change local port to new speed */
            status = myFtdiDevice.SetBaudRate(u32Baudrate);
            if (status != FTDI.FT_STATUS.FT_OK)
            {
                Console.WriteLine("SetBaudRate failed");
            }
        }

        void ePRG_FlashErase()
        {
            FTDI.FT_STATUS status;
            status = eBL_Request(teBL_MessageType.E_BL_MSG_TYPE_FLASH_ERASE_REQUEST, 0, null, 0, null);
            if (status != FTDI.FT_STATUS.FT_OK)
            {
                Console.WriteLine("ePRG_FlashErase failed");
            }
            Thread.Sleep(100);
        }

        void ePRG_FlashProgram(byte[] buffer)
        {
            byte[] au8Msg = new byte[4];
            byte[] payload = new byte[128];
            byte payLoadSize = 0;
            int bufferLength = buffer.Length;
            int u32FlashOffset = 0;
            for (int n = 0; n < bufferLength; n += payLoadSize)
            {
                if (bufferLength - n > 128)
                {
                    payLoadSize = 128;
                }
                else
                {
                    payLoadSize = (byte)(bufferLength - n);
                }
                u32FlashOffset = n;
                au8Msg[0] = (byte)((u32FlashOffset >> 0) & 0xff);
                au8Msg[1] = (byte)((u32FlashOffset >> 8) & 0xff);
                au8Msg[2] = (byte)((u32FlashOffset >> 16) & 0xff);
                au8Msg[3] = (byte)((u32FlashOffset >> 24) & 0xff);
                Array.Copy(buffer, n, payload, 0, payLoadSize);
                eBL_Request(teBL_MessageType.E_BL_MSG_TYPE_FLASH_PROGRAM_REQUEST, 4, au8Msg, payLoadSize, payload);
                //progressBarEZLNTDOWNLOAD.Value =(100*(n+ payLoadSize))/ bufferLength;
                //labelEZLNTPER.Text = progressBarEZLNTDOWNLOAD.Value.ToString()+"%";
                Thread.Sleep(4);
            }
        }

        void eBL_EEPROMErase(byte eraseFlag)
        {
            byte[] au8CmdBuffer = new byte[1];
            au8CmdBuffer[0] = eraseFlag;
            eBL_Request(teBL_MessageType.E_BL_MSG_TYPE_PDM_ERASE_REQUEST, 0, null, 1, au8CmdBuffer);
        }

        void flashProgram(string[] COM, string fileName, byte chipVersion, byte erraseFlag)
        {
            string erraseFlagString = "";
            if (chipVersion == 1)
            {

                if (erraseFlag == 1)
                {
                    erraseFlagString = " --eraseeeprom full";
                }
                else
                {
                    erraseFlagString = "";
                }
            }
            else
            {
                if (erraseFlag == 1)
                {
                    erraseFlagString = " -e FLASH";
                }
                else
                {
                    erraseFlagString = "";
                }

            }
            string COMSerial = "";
            for (int i = 0; i < COM.Length; i++)
            {
                COMSerial += " -s " + COM[i];
            }
            string cmd = @"JN51xxProgrammer.exe" + COMSerial + " -f " + fileName + erraseFlagString + "\r\n";



            using (Process p = new Process())
            {
                p.StartInfo.FileName = CmdPath;
                if (chipVersion == 1)
                {
                    p.StartInfo.WorkingDirectory = Application.StartupPath + "\\v1365\\CLI\\Build";
                }

                if (chipVersion == 2)
                {
                    p.StartInfo.WorkingDirectory = Application.StartupPath + "\\JN518x_v2056\\CLI\\Build";
                    //MessageBox.Show("JN5189 haven't implemented");
                    //  return;
                    cmd = @"JN518xProgrammer.exe" + COMSerial + erraseFlagString + " -p " + fileName + "\r\n";
                }
                cmd = cmd.Trim().TrimEnd('&') + "&&exit";
                p.StartInfo.Arguments = "/k " + cmd;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.RedirectStandardInput = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.RedirectStandardError = false;
                p.StartInfo.CreateNoWindow = true;

                richTextBoxMessageView.Text += "\n program start \n";
                p.Start();
                richTextBoxMessageView.Text += "\n" + cmd + "\n";
                richTextBoxMessageView.Text += "\n" + p.StartInfo.WorkingDirectory + "\n";
                //p.StandardInput.WriteLine(cmd);
                p.WaitForExit();
                p.Close();
                richTextBoxMessageView.Text += "\n program has done \n";
            }
        }

        private void buttonEZLNTLOADREMOTE_Click(object sender, EventArgs e)
        {
            readComStringList.Clear();
            var f = new OpenFileDialog();
            //f.Multiselect = true; 
            if (f.ShowDialog() == DialogResult.OK)
            {
                String filepath = f.FileName;
                String filename = f.SafeFileName;
                comPath = filepath;
                string IPAddress;
                Match m = Regex.Match(filename, @"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}");
                IPAddress = m.Value;

                // Get a list of the Serial port names

                comRead(comPath);
                checkBoxLNTALL.Checked = false;
                multiUSBport2.Clear();
                comHashTable2.Clear();
                indexComHashTable2.Clear();
                comGroupHashTable2.Clear();
                listViewLNTGROUPINFO.Items.Clear();
                listViewLNTCOMINFO.Items.Clear();
                groupIndex2 = 0;
                comIndex2.Clear();
                nwkAddr2.Clear();
                IEEEAddr2.Clear();
                channel2.Clear();
                type2.Clear();
                ver2.Clear();
                indexList2.Clear();

                int i = listViewEZLNTINFO.Items.Count;

                foreach (string s in readComStringList)
                {
                    if (s != "")
                    {
                        string[] nArray = Regex.Split(s, ". ", RegexOptions.IgnoreCase);

                        string[] sArray = Regex.Split(nArray[1], ",", RegexOptions.IgnoreCase);

                       
                        ListViewItem item = new ListViewItem(i.ToString() + ". " + sArray[0]);

                        item.SubItems.Add(sArray[1]);  //nwkAddr

                        ListViewItem itemGroup = new ListViewItem(groupIndex.ToString() + ". " + sArray[1]);
                        itemGroup.SubItems.Add("");
                        itemGroup.SubItems.Add("Remote");
                        string str = sArray[1].Remove(0, sArray[1].Length - 4);




                        item.SubItems.Add(sArray[2]);    //MACAddr      

                        item.SubItems.Add(sArray[3]);    //channel                 
                        item.SubItems.Add(sArray[4]);    //type         
                        item.SubItems.Add(sArray[5]);    //version
                        item.SubItems.Add("Remote");     //Location
                        item.SubItems.Add(sArray[6]);    //chip
                        item.SubItems.Add(sArray[7]);    //profile
                        item.SubItems.Add(IPAddress);    //IPAddress
                        item.SubItems.Add(sArray[8]);    //panID


                        if (!indexMacAddrHashTable.Contains(sArray[2]))
                        {
                            listViewEZLNTINFO.Items.Insert(i, item);
                            indexMacAddrHashTable.Add(sArray[2], i++);
                            //comHashTable.Add(sArray, i);
                            //indexComHashTable.Add(i, s);

                            if (!comGroupHashTable.Contains(str))
                            {
                                listViewEZLNTGROUP.Items.Insert(groupIndex, itemGroup);
                                comGroupHashTable.Add(str, groupIndex++);

                            }
                            else
                            {
                                richTextBoxMessageView.Text += "\nThe nwkAddress ";
                                richTextBoxMessageView.Text += str;
                                richTextBoxMessageView.Text += " has in list!\n";

                            }

                        }
                        else
                        {
                            richTextBoxMessageView.Text += "\nThe macAddress ";
                            richTextBoxMessageView.Text += sArray[2];
                            richTextBoxMessageView.Text += " has in list!\n";

                        }

                    }
                }

                for (int j = 0; j < listViewEZLNTGROUP.Items.Count; j++)
                {
                    listViewEZLNTGROUP.Items[j].Checked = true;
                }
                for (int j = 0; j < listViewEZLNTINFO.Items.Count; j++)
                {
                    if (listViewEZLNTINFO.Items[j].SubItems[1].Text != "")
                    {
                        listViewEZLNTINFO.Items[j].Checked = true;
                    }
                }

            }
        }

        public static string GetMacByNetworkInterface()
        {
            try
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    return BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes());
                }
            }
            catch (Exception)
            {
            }
            return "00-00-00-00-00-00";
        }

        string GetIP()
        {
            string resultIP = string.Empty;
            System.Net.IPAddress[] ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
            foreach (System.Net.IPAddress ip in ips)
            {
                if (IsCorrentIP(ip.ToString()))
                {
                    resultIP = ip.ToString();
                    break;
                }
            }
            return resultIP;
        }

        public void Write(ArrayList writeData)
        {
            string IPAddress = GetIP();
            string path = this.GetType().Assembly.Location;
            string fileName = "RemoteNodesInfo" + "_" + IPAddress + ".txt";
            if (!File.Exists(fileName))
            {
                StreamWriter sr = File.CreateText(fileName);
                sr.Close();
            }
            StreamWriter fs = new StreamWriter(fileName);

            for (int i = 0; i < writeData.Count; i++)
            {

                fs.WriteLine((string)writeData[i]);
            }

            fs.Close();

            MessageBox.Show(fileName + " has been saved");

        }

        private void buttonEZLNTSAVELOCAL_Click(object sender, EventArgs e)
        {
            ArrayList writeData = new ArrayList();
            for (int i = 0; i < listViewEZLNTINFO.Items.Count; i++)
            {
                if (listViewEZLNTINFO.Items[i].SubItems[6].Text == "Local")
                {
                    if (listViewEZLNTINFO.Items[i].SubItems[1].Text != "" && listViewEZLNTINFO.Items[i].SubItems[1].Text != "0xffff")
                    {
                        string s = "";
                        for (int j = 0; j < listViewEZLNTINFO.Items[i].SubItems.Count; j++)
                        {
                            if (j != 6) //skip location
                            {
                                if (j == 0)
                                {
                                    string str = listViewEZLNTINFO.Items[i].SubItems[j].Text;
                                    String[] strCOM = Regex.Split(str, ". ", RegexOptions.IgnoreCase);

                                    s += writeData.Count.ToString() + ". " + strCOM[1] + ",";
                                }
                                else
                                {
                                    s += listViewEZLNTINFO.Items[i].SubItems[j].Text + ",";
                                }
                            }
                        }
                        writeData.Add(s);
                    }
                }
            }
            if (listViewEZLNTINFO.Items.Count > 0)
            {
                Write(writeData);
            }
        }

        private void buttonWZLNTPROFRAMCMD_Click(object sender, EventArgs e)
        {
            if (listViewEZLNTINFO.CheckedItems.Count != 0)
            {
                FlashProgram program = new FlashProgram();
                if (program.ShowDialog() == DialogResult.OK)
                {
                    chipVersion = program.selectedChip;
                    whetherErase = program.whetherErase;

                    if (chipVersion == 0)
                    {
                        MessageBox.Show("please choose chip version");
                        return;
                    }

                    var f = new OpenFileDialog();
                    if (f.ShowDialog() == DialogResult.OK)
                    {

                        string[] COM = new string[listViewEZLNTINFO.CheckedItems.Count];

                        for (int i = 0; i < listViewEZLNTINFO.CheckedItems.Count; i++)
                        {
                            string s = listViewEZLNTINFO.CheckedItems[i].SubItems[0].Text;
                            if (s != "")
                            {
                                String[] str = Regex.Split(s, ". ", RegexOptions.IgnoreCase);

                                COM[i] = str[1];
                            }

                        }
                        flashProgram(COM, f.FileName, chipVersion, whetherErase);

                    }

                }

            }
            else
            {
                MessageBox.Show("None port selected");

            }
        }

        private void buttonEZLNTPROGRAM_Click(object sender, EventArgs e)
        {
            var f = new OpenFileDialog();
            byte[] buffer;

            if (f.ShowDialog() == DialogResult.OK)
            {
                PRG_init();
                String filepath = f.FileName;
                buffer = fileToBuffer(f.FileName);


                ePRG_FTDI_ModeProgramming();

                ePRG_ConnectionUpdate(1000000);

                ePRG_FlashErase();
                ePRG_FlashProgram(buffer);

                //eBL_EEPROMErase((byte)comboBoxEZLNTPROGRAMEEPROM.SelectedIndex);
                myFtdiDevice.ResetPort();
                myFtdiDevice.Close();

                richTextBoxMessageView.Text += "\n";
                richTextBoxMessageView.Text += "program has done\n";

            }
        }

        private void buttonEZLNTSETLOOP_Click(object sender, EventArgs e)
        {
            UInt16 u16TimerLoop;
            UInt16 u16TimerInterval;
            UInt16 u16TimerIntervalMax;


            if (bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out u16TimerLoop) == true)
            {
                if (bStringToUint16Decimal(textBoxEZLNTTIMERINTERVAL.Text, out u16TimerInterval) == true)
                {

                    if (bStringToUint16Decimal(textBoxEZLNTSETINTERVALMAX.Text, out u16TimerIntervalMax) == true)
                    {

                        msCountMax = u16TimerIntervalMax;
                        readAttributeLoop = u16TimerLoop;
                        msCountMin = u16TimerInterval;
                        if (msCountMax < msCountMin)
                        {
                            MessageBox.Show("Loop parameter is error");
                        }
                        else
                        {
                            MessageBox.Show("Loop parameter has set");
                        }
                    }
                }
            }
        }

        private void buttonEZLNTBROADON_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr = 0xfffc;


            sendClusterOnOff(4, u16ShortAddr, 1, 1, 1);

        }

        private void buttonEZLNTBROADOFF_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr = 0xfffc;



            sendClusterOnOff(4, u16ShortAddr, 1, 1, 0);


        }

        private void buttonEZLNTBROADTONGGLE_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                // if (listViewEZLNTGROUP.Items.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    Thread timerBoardTonggleThread = new Thread(customertimerBoardTonggle);
                    boardTonggleThreadStop = false;
                    timerBoardTonggleThread.Priority = ThreadPriority.Normal;

                    syncEventPort1 = true;
                    syncEvent.Reset();
                    timeBeginPeriod(1);
                    timerBoardTonggleThread.Start();
                }
                //else
                //{
                //    MessageBox.Show("None joined node in the list");
                //}
            }
        }

        private void buttonEZLNTBROADSTOPTONGGLE_Click(object sender, EventArgs e)
        {
            boardTonggleThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonEZLNTON_Click(object sender, EventArgs e)
        {

            UInt16 u16ShortAddr;

            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    sendClusterOnOff(2, u16ShortAddr, 1, 1, 1);
                }
            }

        }

        private void buttonEZLNTOFF_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;

            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    sendClusterOnOff(2, u16ShortAddr, 1, 1, 0);
                }
            }
        }

        private void buttonEZLNTTONGGLE_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {
                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    Thread timerTonggleThread = new Thread(customertimerTonggle);
                    tonggleThreadStop = false;
                    timerTonggleThread.Priority = ThreadPriority.Normal;

                    syncEventPort1 = true;
                    syncEvent.Reset();
                    timeBeginPeriod(1);
                    timerTonggleThread.Start();
                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonEZLNTBIND_Click(object sender, EventArgs e)
        {
            UInt64 u64TargetExtAddr;
            UInt16 u16ClusterID;

            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                string str = s.Remove(0, s.Length - 6);
                for (int j = 0; j < listViewEZLNTINFO.Items.Count; j++)
                {
                    if (listViewEZLNTINFO.Items[j].SubItems[1].Text == str)
                    {
                        if (bStringToUint64(listViewEZLNTINFO.Items[j].SubItems[2].Text.Remove(0, 2), out u64TargetExtAddr) == true)
                        {

                            if (bStringToUint16(textBoxEZLNTBINDCLUSTERID.Text, out u16ClusterID) == true)
                            {
                                sendBindRequest(u64TargetExtAddr, 1, u16ClusterID, 3, localIEEEAddress, 1);

                            }

                            break;
                        }
                    }
                }

            }
        }

        private void buttonEZLNTUNBIND_Click(object sender, EventArgs e)
        {
            UInt64 u64TargetExtAddr;
            UInt16 u16ClusterID;


            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                string str = s.Remove(0, s.Length - 6);
                for (int j = 0; j < listViewEZLNTINFO.Items.Count; j++)
                {
                    if (listViewEZLNTINFO.Items[j].SubItems[1].Text == str)
                    {
                        if (bStringToUint64(listViewEZLNTINFO.Items[j].SubItems[2].Text.Remove(0, 2), out u64TargetExtAddr) == true)
                        {

                            if (bStringToUint16(textBoxEZLNTUNBINDCLUSTERID.Text, out u16ClusterID) == true)
                            {
                                sendUnBindRequest(u64TargetExtAddr, 1, u16ClusterID, 3, localIEEEAddress, 1);

                            }

                            break;
                        }
                    }
                }

            }
        }

        private void buttonEZLNTLEAVE_Click(object sender, EventArgs e)
        {
            UInt64 u64TargetExtAddr;

            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                string str = s.Remove(0, s.Length - 6);
                for (int j = 0; j < listViewEZLNTINFO.Items.Count; j++)
                {
                    if (listViewEZLNTINFO.Items[j].SubItems[1].Text == str)
                    {
                        if (bStringToUint64(listViewEZLNTINFO.Items[j].SubItems[2].Text.Remove(0, 2), out u64TargetExtAddr) == true)
                        {

                            sendLeaveRequest(u64TargetExtAddr, (byte)comboBoxEZLNTLEAVEREJOIN.SelectedIndex, (byte)comboBoxEZLNTLEAVECHILDREN.SelectedIndex);

                        }
                    }
                }
            }

        }

        private void buttonEZLNTCONFIGRPRT_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            UInt16 u16AttribId;
            UInt16 u16MinInterval;
            UInt16 u16MaxInterval;
            UInt16 u16TimeOut;
            UInt64 u64Change;
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            byte u8AttribType;

            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {

                    {
                        if (bStringToUint16(textBoxEZLNTCONFIGRPRTCLUSTERID.Text, out u16ClusterID) == true)
                        {
                            if (bStringToUint8(textBoxEZLNTCONFIGRPRTTYPE.Text, out u8AttribType) == true)
                            {
                                if (bStringToUint16(textBoxEZLNTCONFIGRPRTATTRIBID.Text, out u16AttribId) == true)
                                {
                                    if (bStringToUint16(textBoxEZLNTCONFIGRPRTMININTERVAL.Text, out u16MinInterval) == true)
                                    {
                                        if (bStringToUint16(textBoxEZLNTCONFIGRPRTMAXINTERVAL.Text, out u16MaxInterval) == true)
                                        {
                                            if (bStringToUint16(textBoxEZLNTCONFIGRPRTTIMEOUT.Text, out u16TimeOut) == true)
                                            {
                                                if (bStringToUint64(textBoxEZLNTCONFIGRPRTCHANGE.Text, out u64Change) == true)
                                                {
                                                    sendConfigReportRequest(2, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, 0, 0, u8AttribType, u16AttribId, u16MinInterval, u16MaxInterval, u16TimeOut, u64Change);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonEZLNTREADRPRT_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            UInt16 u16ClusterID;
            UInt16 u16AttribID;
            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {

                    if (bStringToUint16(textBoxEZLNTREADRPRTCLUSTERID.Text, out u16ClusterID) == true)
                    {
                        if (bStringToUint16(textBoxEZLNTREADRPRTATTRIBUTEID.Text, out u16AttribID) == true)
                        {
                            sendReadReportConfigRequest(2, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, 0, 1, 0, 0, 0, u16AttribID);
                        }
                    }
                }

            }
        }

        private void buttonEZLNTRESET_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {

                    sendBasicResetFactoryDefaultCommand(2, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint);
                }
            }
        }

        public bool permitIsZED(string type)
        {
            if (type.Contains("ZED"))
                return true;
            else if (type.Contains("switch"))
                return true;
            else if (type.Contains("sensor"))
                return true;
            else
                return false;
        }

        private void buttonEZLNTDISABLEPERMIT_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8Interval;
            u8Interval = 0x00;
            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {
                    string str = s.Remove(0, s.Length - 6);
                    for (int j = 0; j < listViewEZLNTINFO.Items.Count; j++)
                    {
                        if (listViewEZLNTINFO.Items[j].SubItems[1].Text == str)
                        {
                            if (!permitIsZED(listViewEZLNTINFO.Items[j].SubItems[4].Text))
                            {
                                setPermitJoin((UInt16)u16TargetAddr, u8Interval, 0);
                            }
                            break;
                        }
                    }
                }
            }
        }


        private void buttonEZLNTPERMIT_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8Interval;
            u8Interval = 0xff;
            for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
            {
                int index = listViewEZLNTGROUP.CheckedIndices[i];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {
                    string str = s.Remove(0, s.Length - 6);
                    for (int j = 0; j < listViewEZLNTINFO.Items.Count; j++)
                    {
                        if (listViewEZLNTINFO.Items[j].SubItems[1].Text == str)
                        {
                            if (!permitIsZED(listViewEZLNTINFO.Items[j].SubItems[4].Text))
                            {
                                setPermitJoin((UInt16)u16TargetAddr, u8Interval, 0);
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void buttonEZLNTIDENTIFY_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {
                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    LNTIdentifyFlag = false;
                    Thread timerIdentifyThread = new Thread(customertimerIdentify);
                    identifyThreadStop = false;
                    timerIdentifyThread.Priority = ThreadPriority.Highest;

                    syncEventPort1 = true;
                    syncEvent.Reset();
                    timeBeginPeriod(1);
                    timerIdentifyThread.Start();
                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonEZLNTIDENTIFYSTOP_Click(object sender, EventArgs e)
        {
            identifyThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonEZLNTLEVLELSTOP_Click(object sender, EventArgs e)
        {
            levelThreadStop = true;
            timeEndPeriod(1);
        }


        private void buttonEZLNTCOLORLOOPSTOP_Click(object sender, EventArgs e)
        {
            colorThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonEZLNTSATLOOPSTOP_Click(object sender, EventArgs e)
        {
            satThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonTEMPLOOPSTOP_Click(object sender, EventArgs e)
        {
            tempThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonEZLNTHUESTOP_Click(object sender, EventArgs e)
        {
            hueThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonEZLNTMOVETOLEVLEL_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {

                if (textBoxEZLNTSETLOOP.Text != "")
                {
                    bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                }
                if (bStringToUint16Decimal(textBoxEZLNTSETSTEP.Text, out u16Step) == true)
                {
                    step = u16Step;
                    if (bStringToUint16Decimal(textBoxEZLNTSETDIR.Text, out u16Dir) == true)
                    {
                        setDir = u16Dir;
                        if (bStringToUint8(textBoxEZLNTLEVEL.Text, out currentLevel) == true)
                        {
                            if (comboBoxEZLNTUNICAST.SelectedIndex == 0)
                            {
                                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                                {
                                    LNTLevelFlag = false;
                                    Thread timerLevelThread = new Thread(customertimerLevel);
                                    levelThreadStop = false;
                                    timerLevelThread.Priority = ThreadPriority.Normal;

                                    syncEventPort1 = true;
                                    syncEvent.Reset();
                                    timeBeginPeriod(1);
                                    timerLevelThread.Start();
                                }
                                else
                                {
                                    MessageBox.Show("None joined node has been choosen");
                                }
                            }
                            else
                            {
                                LNTLevelFlag = false;
                                Thread timerBoardLevelThread = new Thread(customertimerBoardLevel);
                                boardLevelThreadStop = false;
                                timerBoardLevelThread.Priority = ThreadPriority.Highest;

                                syncEventPort1 = true;
                                syncEvent.Reset();
                                timeBeginPeriod(1);
                                timerBoardLevelThread.Start();

                            }
                        }
                    }

                }

            }
        }

        private void buttonEZLNTMOVETOHUE_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {
                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    if (bStringToUint16Decimal(textBoxEZLNTSETSTEP.Text, out u16Step) == true)
                    {
                        step = u16Step;
                        if (bStringToUint16Decimal(textBoxEZLNTSETDIR.Text, out u16Dir) == true)
                        {
                            setDir = u16Dir;
                            if (bStringToUint8(textBoxEZLNTHUE.Text, out currentHue) == true)
                            {
                                LNTHueFlag = false;
                                Thread timerHueThread = new Thread(customertimerHue);
                                hueThreadStop = false;
                                timerHueThread.Priority = ThreadPriority.Normal;

                                syncEventPort1 = true;
                                syncEvent.Reset();
                                timeBeginPeriod(1);
                                timerHueThread.Start();
                            }
                        }


                    }


                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonEZLNTMOVETOCOLOR_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {
                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    if (bStringToUint16Decimal(textBoxEZLNTSETSTEP.Text, out u16Step) == true)
                    {
                        step = u16Step;
                        if (bStringToUint16Decimal(textBoxEZLNTSETDIR.Text, out u16Dir) == true)
                        {
                            setDir = u16Dir;
                            if (bStringToUint16(textBoxEZLNTCOLORX.Text, out currentX) == true)
                            {
                                if (bStringToUint16(textBoxEZLNTCOLORY.Text, out currentY) == true)
                                {
                                    LNTColorFlag = false;
                                    Thread timerColorThread = new Thread(customertimerColor);
                                    colorThreadStop = false;
                                    timerColorThread.Priority = ThreadPriority.Normal;

                                    syncEventPort1 = true;
                                    syncEvent.Reset();
                                    timeBeginPeriod(1);
                                    timerColorThread.Start();
                                }
                            }
                        }


                    }


                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonEZLNTMOVETOSAT_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {
                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    if (bStringToUint16Decimal(textBoxEZLNTSETSTEP.Text, out u16Step) == true)
                    {
                        step = u16Step;
                        if (bStringToUint16Decimal(textBoxEZLNTSETDIR.Text, out u16Dir) == true)
                        {
                            setDir = u16Dir;
                            if (bStringToUint8(textBoxEZLNTSAT.Text, out currentSat) == true)
                            {
                                LNTSatFlag = false;
                                Thread timerSatThread = new Thread(customertimerSat);
                                satThreadStop = false;
                                timerSatThread.Priority = ThreadPriority.Normal;

                                syncEventPort1 = true;
                                syncEvent.Reset();
                                timeBeginPeriod(1);
                                timerSatThread.Start();
                            }
                        }


                    }

                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonEZLNTMOVETOTEMP_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {
                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    if (bStringToUint16Decimal(textBoxEZLNTSETSTEP.Text, out u16Step) == true)
                    {
                        step = u16Step;
                        if (bStringToUint16Decimal(textBoxEZLNTSETDIR.Text, out u16Dir) == true)
                        {
                            setDir = u16Dir;
                            if (bStringToUint16(textBoxEZLNTTEMP.Text, out currentTemp) == true)
                            {
                                LNTTempFlag = false;
                                Thread timerTempThread = new Thread(customertimerTemp);
                                tempThreadStop = false;
                                timerTempThread.Priority = ThreadPriority.Normal;

                                syncEventPort1 = true;
                                syncEvent.Reset();
                                timeBeginPeriod(1);
                                timerTempThread.Start();
                            }
                        }


                    }

                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }


        #endregion

        #region LNT Remote
        private void buttonLNTREMOTELOAD_Click(object sender, EventArgs e)
        {
            readComStringList.Clear();
            var f = new OpenFileDialog();
            //f.Multiselect = true; 
            if (f.ShowDialog() == DialogResult.OK)
            {
                String filepath = f.FileName;
                String filename = f.SafeFileName;
                comPath = filepath;

                // Get a list of the Serial port names

                comRead(comPath);
                checkBoxLNTALL.Checked = false;
                multiUSBport2.Clear();
                comHashTable2.Clear();
                indexComHashTable2.Clear();
                comGroupHashTable2.Clear();
                listViewLNTGROUPINFO.Items.Clear();
                listViewLNTCOMINFO.Items.Clear();
                groupIndex2 = 0;
                comIndex2.Clear();
                nwkAddr2.Clear();
                IEEEAddr2.Clear();
                channel2.Clear();
                type2.Clear();
                ver2.Clear();
                indexList2.Clear();

                int i = 0;
                foreach (string s in readComStringList)
                {
                    if (s != "")
                    {
                        string[] sArray = Regex.Split(s, ",", RegexOptions.IgnoreCase);

                        //  comHashTable.Add(sArray, i);
                        //  indexComHashTable.Add(i, s);
                        ListViewItem item = new ListViewItem(i.ToString() + ". ");


                        string[] Nwkrray = Regex.Split(sArray[2], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(Nwkrray[1]);

                        ListViewItem itemGroup = new ListViewItem(i.ToString() + ". " + Nwkrray[1]);
                        itemGroup.SubItems.Add("");
                        string str = Nwkrray[1].Remove(0, Nwkrray[1].Length - 4);
                        comGroupHashTable2.Add(str, groupIndex2++);

                        string[] IEEEArray = Regex.Split(sArray[3], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(IEEEArray[1]);

                        string[] ChaArray = Regex.Split(sArray[1], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(ChaArray[1]);

                        string[] TypeArray = Regex.Split(sArray[5], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(TypeArray[1]);

                        string[] VerArray = Regex.Split(sArray[4], "=", RegexOptions.IgnoreCase);
                        item.SubItems.Add(VerArray[1]);

                        listViewLNTCOMINFO.Items.Insert(i, item);
                        listViewLNTGROUPINFO.Items.Insert(i++, itemGroup);

                    }
                }

                for (int j = 0; j < listViewLNTGROUPINFO.Items.Count; j++)
                {
                    listViewLNTGROUPINFO.Items[j].Checked = true;
                    listViewLNTCOMINFO.Items[j].Checked = true;
                }
            }
        }

        private void buttonLNTSENDCOMMAND_Click(object sender, EventArgs e)
        {
            if (asClient == 1)   //sever
            {
                byte u8command;
                if (multiPortOpen)
                {
                    byte[] array = System.Text.Encoding.ASCII.GetBytes(textBoxLNTSENDCOMMAND.Text);
                    u8command = array[0];
                    {
                        comIndex.Clear();
                        nwkAddr.Clear();
                        IEEEAddr.Clear();
                        channel.Clear();
                        chip.Clear();
                        profile.Clear();
                        panID.Clear();
                        type.Clear();
                        ver.Clear();
                        {
                            byte[] dataArray = null;
                            dataArray = new byte[1];
                            dataArray[0] = u8command;
                            sendCommand = dataArray[0];
                            for (int i = 0; i < multiUSBport.Count; i++)
                            {

                                if (multiUSBport[i].IsOpen)
                                {
                                    multiUSBport[i].Write(dataArray, 0, 1);
                                    //   Console.WriteLine("Write success");
                                }
                            }
                        }
                    }
                }
            }
            else if(asClient==2) //client
            {
                if (listViewLNTCOMINFO.CheckedItems.Count != 0)
                {
                    try
                    {

                        for (int i = 0; i < listViewLNTCOMINFO.CheckedItems.Count; i++)
                        {
                            string nwkAddr = listViewLNTCOMINFO.CheckedItems[i].SubItems[1].Text;


                            clientSocket.Send(Encoding.ASCII.GetBytes(nwkAddr + "," + textBoxLNTSENDCOMMAND.Text));

                            richTextBoxMessageView.Text += nwkAddr + "," + textBoxLNTSENDCOMMAND.Text;
                            richTextBoxMessageView.Text += "\n";

                        }

                    }
                    catch
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    }
                }
            }

        }

        private void buttonLNTSOCKETSEVERIP_Click(object sender, EventArgs e)
        {

        }

        private bool IsCorrentIP(string ip)
        {
            string pattrn = @"(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])";
            if (System.Text.RegularExpressions.Regex.IsMatch(ip, pattrn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void buttonLNTSOCKETCLIENTIP_Click(object sender, EventArgs e)
        {

            string resultIP = string.Empty;
            System.Net.IPAddress[] ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
            foreach (System.Net.IPAddress ip in ips)
            {
                if (IsCorrentIP(ip.ToString()))
                {
                    resultIP = ip.ToString();
                    break;
                }
            }
            textBoxLNTSOCKETSEVERIP.Text = resultIP;
        }

        private static byte[] resultClient = new byte[1024*1024];

        private void buttonLNTSOCKETCLIENT_Click(object sender, EventArgs e)
        {
            asClient = 2;
            string str = textBoxLNTSOCKETCLIENTIP.Text;
            if (IsCorrentIP(str))
            {

                IPAddress ip = IPAddress.Parse(str);

                try
                {
                    clientSocket.Connect(new IPEndPoint(ip, 8885));
                    richTextBoxMessageView.Text += "connect success";
                    richTextBoxMessageView.Text += "\n";
                }
                catch
                {
                    richTextBoxMessageView.Text += "connect failed";
                    richTextBoxMessageView.Text += "\n";
                    return;
                }

                int receiveLength = clientSocket.Receive(resultClient);

                string s = Encoding.ASCII.GetString(resultClient, 0, receiveLength);
                richTextBoxMessageView.Text += s;
                richTextBoxMessageView.Text += "\n";

            }
            else
            {
                MessageBox.Show("Wrong IPAddress");
            }
        }

        private static byte[] resultSever = new byte[1024];
        private static int myProt = 8885;
       
        public void ListenClientConnect()
        {
            Socket sSocket = null;
            while (true)
            {
                try
                {
                    sSocket = serverSocket.Accept();
                }
                catch (Exception ex)
                {
                      
                    Console.WriteLine(ex.Message);
                    break;
                }
                IPAddress clientIP = (sSocket.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (sSocket.RemoteEndPoint as IPEndPoint).Port;
                sSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));

                string remoteEndPoint = sSocket.RemoteEndPoint.ToString();
                clientConnectionItems.Add(remoteEndPoint, sSocket);

                IPEndPoint netpoint = sSocket.RemoteEndPoint as IPEndPoint;

                //build a signal thread
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveMessage);
                Thread receiveThread = new Thread(pts);
                receiveThread.IsBackground = true;
                receiveThread.Start(sSocket);

            }
        }

        public void ReceiveMessage(object sSocket)   //sever
        {
            Socket mysSocket = (Socket)sSocket;
            socketSeverReceiveMessageCommand = new SocketSeverReceiveMessageCommand(mySocketSeverReceiveMessageCommand);
            socketSeverDealData = new SocketSeverDealData(mysocketSeverDealData);
            while (true)
            {
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                try
                {
                    int receiveNumber = mysSocket.Receive(arrServerRecMsg);
                    string s = mysSocket.RemoteEndPoint.ToString();
                    string str = Encoding.ASCII.GetString(arrServerRecMsg, 0, receiveNumber);
                    // invoke the delegate in the MainForm thread
                    if (str.Contains("client test"))
                    {
                        mysSocket.Send(Encoding.ASCII.GetBytes("sever connection OK"));
                    }
                              
                    this.Invoke(socketSeverReceiveMessageCommand, "from:" + s + " message:" + str);
                    
                    this.Invoke(socketSeverDealData, str);

                }
                catch (Exception ex)
                {
                    this.Invoke(socketSeverReceiveMessageCommand, ex.Message);
                    clientConnectionItems.Remove(mysSocket.RemoteEndPoint.ToString());
                    this.Invoke(socketSeverReceiveMessageCommand,"connnect with " +mysSocket.RemoteEndPoint.ToString()+ " has closed");
                    mysSocket.Shutdown(SocketShutdown.Both);
                    mysSocket.Close();
                    break;
                }
            }
        }

        private void buttonLNTSOCKETSEVER_Click(object sender, EventArgs e)
        {
            asClient = 1;
            if (textBoxLNTSOCKETSEVERIP.Text == "")
            {
                string resultIP = string.Empty;
                System.Net.IPAddress[] ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
                foreach (System.Net.IPAddress ip in ips)
                {
                    if (IsCorrentIP(ip.ToString()))
                    {
                        resultIP = ip.ToString();
                        break;
                    }
                }
                textBoxLNTSOCKETSEVERIP.Text = resultIP;
            }

            if (textBoxLNTSOCKETSEVERIP.Text != "")
            {
                string ipStr = textBoxLNTSOCKETSEVERIP.Text;
                IPAddress ip = IPAddress.Parse(ipStr);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(ip, myProt));
                serverSocket.Listen(10);
                string s = serverSocket.LocalEndPoint.ToString();
                richTextBoxMessageView.Text += "listen:";
                richTextBoxMessageView.Text += s;
                richTextBoxMessageView.Text += "\n";

                Thread myThread = new Thread(ListenClientConnect);
                myThread.Start();
            }

        }

        private void buttonLNTSETPARA_Click(object sender, EventArgs e)
        {
            UInt16 u16TimerLoop;
            UInt16 u16TimerInterval;
            UInt16 u16TimerIntervalMax;


            if (bStringToUint16Decimal(textBoxLNTSETLOOP.Text, out u16TimerLoop) == true)
            {
                if (bStringToUint16Decimal(textBoxLNTSETPARAMININTERVAL.Text, out u16TimerInterval) == true)
                {
                    if (bStringToUint16Decimal(textBoxLNTSETPARAMAXINTERVAL.Text, out u16TimerIntervalMax) == true)
                    {
                        msCountMax = u16TimerIntervalMax;
                    }
                    readAttributeLoop = u16TimerLoop;
                    msCountMin = u16TimerInterval;


                }
            }
        }

        private void buttonLNTRESET_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {

                    sendBasicResetFactoryDefaultCommand(2, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint);
                }
            }
        }

        private void buttonLNTPERMIT_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8Interval;
            u8Interval = 0xff;
            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {
                    string str = s.Remove(0, s.Length - 6);
                    for (int j = 0; j < listViewLNTCOMINFO.Items.Count; j++)
                    {
                        if (listViewLNTCOMINFO.Items[j].SubItems[1].Text == str)
                        {
                            if (!permitIsZED(listViewLNTCOMINFO.Items[j].SubItems[4].Text))
                            {
                                setPermitJoin((UInt16)u16TargetAddr, u8Interval, 0);
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void buttonLNTDISABLEPERMIT_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8Interval;
            u8Interval = 0x00;
            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {
                    string str = s.Remove(0, s.Length - 6);
                    for (int j = 0; j < listViewLNTCOMINFO.Items.Count; j++)
                    {
                        if (listViewLNTCOMINFO.Items[j].SubItems[1].Text == str)
                        {
                            if (!permitIsZED(listViewLNTCOMINFO.Items[j].SubItems[4].Text))
                            {
                                setPermitJoin((UInt16)u16TargetAddr, u8Interval, 0);
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void buttonLNTREADATTRIBUTE_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (listViewLNTGROUPINFO.CheckedItems.Count > 0)
                {
                    if (textBoxLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    LNTReadFlag = true;
                    Thread timerReadAttributeThread = new Thread(customertimer);
                    readAttributeThreadStop = false;
                    timerReadAttributeThread.Priority = ThreadPriority.Highest;

                    syncEventPort1 = true;
                    syncEvent.Reset();
                    timeBeginPeriod(1);
                    timerReadAttributeThread.Start();

                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonLNTSTOPTEADATTRIBUTE_Click(object sender, EventArgs e)
        {
            readAttributeThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonLNTWRITEATTRIBUTE_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            UInt16 u16AttribID;
            UInt16 u16ManuID = 0;
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            byte u8AttribType;
            byte[] au8Data = new byte[64];
            byte u8DataLen = 0;

            for (int j = 0; j < listViewLNTGROUPINFO.CheckedItems.Count; j++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[j];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {

                    if (bStringToUint16(textBoxLNTWRITEATTRCLUSTERID.Text, out u16ClusterID) == true)
                    {
                        if (bStringToUint16(textBoxLNTWRITEATTRATTRID.Text, out u16AttribID) == true)
                        {

                            {
                                if (bStringToUint8(textBoxLNTWRITEATTRDATATYPE.Text, out u8AttribType) == true)
                                {
                                    if (u8AttribType == 0x42)
                                    {
                                        // if the data is a character string get the length make make this is the first byte                                         
                                        au8Data[0] = (byte)System.Text.Encoding.ASCII.GetBytes(textBoxLNTWRITEATTRDATA.Text, 0, textBoxLNTWRITEATTRDATA.TextLength, au8Data, 1);
                                        u8DataLen = au8Data[0];
                                        u8DataLen++;
                                    }
                                    else if (u8AttribType == 0x21)
                                    {
                                        UInt16 u16Data;

                                        /* Data is a uint16 */
                                        if (bStringToUint16(textBoxLNTWRITEATTRDATA.Text, out u16Data) == true)
                                        {
                                            u8DataLen = 2;
                                            au8Data[1] = (byte)u16Data;
                                            au8Data[0] = (byte)(u16Data >> 8);
                                        }
                                    }
                                    else if (u8AttribType == 0xf0)
                                    {
                                        /* Data is a uint64 */
                                        UInt64 u64MACaddress;
                                        if (bStringToUint64(textBoxLNTWRITEATTRDATA.Text, out u64MACaddress) == true)
                                        {
                                            u8DataLen = 8;
                                            au8Data[7] = (byte)u64MACaddress;
                                            au8Data[6] = (byte)(u64MACaddress >> 8);
                                            au8Data[5] = (byte)(u64MACaddress >> 16);
                                            au8Data[4] = (byte)(u64MACaddress >> 24);
                                            au8Data[3] = (byte)(u64MACaddress >> 32);
                                            au8Data[2] = (byte)(u64MACaddress >> 40);
                                            au8Data[1] = (byte)(u64MACaddress >> 48);
                                            au8Data[0] = (byte)(u64MACaddress >> 56);
                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < textBoxLNTWRITEATTRDATA.TextLength; i += 2)
                                        {
                                            byte u8Data = 0;
                                            if (bStringToUint8(textBoxLNTWRITEATTRDATA.Text, out u8Data) == true)
                                            {
                                                au8Data[i] = u8Data;
                                            }
                                            else
                                            {
                                                return;
                                            }
                                            u8DataLen++;
                                        }
                                    }
                                    sendWriteAttribRequest(u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, 0, 0, u16ManuID, 1, u16AttribID, u8AttribType, au8Data, u8DataLen);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonLNTON_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;

            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    sendClusterOnOff(2, u16ShortAddr, 1, 1, 1);
                }
            }
        }

        private void buttonLNTOFF_Click(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;

            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    sendClusterOnOff(2, u16ShortAddr, 1, 1, 0);
                }
            }
        }

        private void buttonLNTTONGGLE_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (listViewLNTGROUPINFO.CheckedItems.Count > 0)
                {
                    if (textBoxLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    LNTTonggleFlag = true;
                    Thread timerTonggleThread = new Thread(customertimerTonggle);
                    tonggleThreadStop = false;
                    timerTonggleThread.Priority = ThreadPriority.Highest;

                    syncEventPort1 = true;
                    syncEvent.Reset();
                    timeBeginPeriod(1);
                    timerTonggleThread.Start();
                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonLNTSTOPTONGGLE_Click(object sender, EventArgs e)
        {
            tonggleThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonLNTBIND_Click(object sender, EventArgs e)
        {
            UInt64 u64TargetExtAddr;
            UInt16 u16ClusterID;

            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                string str = s.Remove(0, s.Length - 6);
                for (int j = 0; j < listViewLNTCOMINFO.Items.Count; j++)
                {
                    if (listViewLNTCOMINFO.Items[j].SubItems[1].Text == str)
                    {
                        if (bStringToUint64(listViewLNTCOMINFO.Items[j].SubItems[2].Text.Remove(0, 2), out u64TargetExtAddr) == true)
                        {

                            if (bStringToUint16(textBoxLNTBINDIEEEADDR.Text, out u16ClusterID) == true)
                            {
                                sendBindRequest(u64TargetExtAddr, 1, u16ClusterID, 3, localIEEEAddress, 1);

                            }

                            break;
                        }
                    }
                }

            }
        }

        private void buttonLNTUNBIND_Click(object sender, EventArgs e)
        {
            UInt64 u64TargetExtAddr;
            UInt16 u16ClusterID;


            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                string str = s.Remove(0, s.Length - 6);
                for (int j = 0; j < listViewLNTCOMINFO.Items.Count; j++)
                {
                    if (listViewLNTCOMINFO.Items[j].SubItems[1].Text == str)
                    {
                        if (bStringToUint64(listViewLNTCOMINFO.Items[j].SubItems[2].Text.Remove(0, 2), out u64TargetExtAddr) == true)
                        {

                            if (bStringToUint16(textBoxLNTUNBINDIEEEADDR.Text, out u16ClusterID) == true)
                            {
                                sendUnBindRequest(u64TargetExtAddr, 1, u16ClusterID, 3, localIEEEAddress, 1);
                            }

                            break;
                        }
                    }
                }

            }
        }

        private void buttonLNTLEAVE_Click(object sender, EventArgs e)
        {
            UInt64 u64TargetExtAddr;

            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                string str = s.Remove(0, s.Length - 6);
                for (int j = 0; j < listViewLNTCOMINFO.Items.Count; j++)
                {
                    if (listViewLNTCOMINFO.Items[j].SubItems[1].Text == str)
                    {
                        if (bStringToUint64(listViewLNTCOMINFO.Items[j].SubItems[2].Text.Remove(0, 2), out u64TargetExtAddr) == true)
                        {

                            sendLeaveRequest(u64TargetExtAddr, (byte)comboBoxLNTLEAVEREJOIN.SelectedIndex, (byte)comboBoxLNTLEAVEWITHCHILDREN.SelectedIndex);

                        }
                    }
                }
            }

        }

        private void buttonLNTCONFIGRPRT_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            UInt16 u16AttribId;
            UInt16 u16MinInterval;
            UInt16 u16MaxInterval;
            UInt16 u16TimeOut;
            UInt64 u64Change;
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            byte u8AttribType;

            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {

                    {
                        if (bStringToUint16(textBoxLNTCONFIGRPRTCLUSTERID.Text, out u16ClusterID) == true)
                        {
                            if (bStringToUint8(textBoxLNTCONFIGRPRTTYPE.Text, out u8AttribType) == true)
                            {
                                if (bStringToUint16(textBoxCONFIGRPRTATTRID.Text, out u16AttribId) == true)
                                {
                                    if (bStringToUint16(textBoxCONFIGRPRTMININTERVAL.Text, out u16MinInterval) == true)
                                    {
                                        if (bStringToUint16(textBoxCONFIGRPRTMAXRPRTINTERVAL.Text, out u16MaxInterval) == true)
                                        {
                                            if (bStringToUint16(textBoxLNTCONFIGRPRTTIMEOUT.Text, out u16TimeOut) == true)
                                            {
                                                if (bStringToUint64(textBoxLNTCONFIGRPRTCHANGE.Text, out u64Change) == true)
                                                {
                                                    sendConfigReportRequest(2, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, 0, 0, u8AttribType, u16AttribId, u16MinInterval, u16MaxInterval, u16TimeOut, u64Change);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonLNTREADRPRT_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            UInt16 u16ClusterID;
            UInt16 u16AttribID;
            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16TargetAddr) == true)
                {

                    if (bStringToUint16(textBoxLNTREADRPRTCLUSTERID.Text, out u16ClusterID) == true)
                    {
                        if (bStringToUint16(textBoxLNTREADRPRTATTRID.Text, out u16AttribID) == true)
                        {
                            sendReadReportConfigRequest(2, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ClusterID, 0, 1, 0, 0, 0, u16AttribID);
                        }
                    }
                }

            }
        }

        private void buttonLNTIDENTIFY_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (listViewLNTGROUPINFO.CheckedItems.Count > 0)
                {
                    if (textBoxLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    LNTIdentifyFlag = true;
                    Thread timerIdentifyThread = new Thread(customertimerIdentify);
                    identifyThreadStop = false;
                    timerIdentifyThread.Priority = ThreadPriority.Highest;

                    syncEventPort1 = true;
                    syncEvent.Reset();
                    timeBeginPeriod(1);
                    timerIdentifyThread.Start();
                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonLNTSTOPIDENTIFY_Click(object sender, EventArgs e)
        {
            identifyThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonLNTADDGROUP_Click(object sender, EventArgs e)
        {
            UInt16 u16GroupAddr;
            UInt16 u16ShortAddr;
            removeAllGroup = false;
            if (bStringToUint16(textBoxLNTADDGROUPADDR.Text, out u16GroupAddr) == true)
            {
                for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
                {
                    int index = listViewLNTGROUPINFO.CheckedIndices[i];
                    string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                    if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                    {
                        sendGroupAdd(u16ShortAddr, 1, 1, u16GroupAddr, 0, 0, "");
                    }
                }
            }
        }

        private void buttonLNTREMOVEGROUP_Click(object sender, EventArgs e)
        {
            UInt16 u16GroupAddr;
            UInt16 u16ShortAddr;
            removeAllGroup = false;
            if (bStringToUint16(textBoxLNTREMOVEGROUPADDRESS.Text, out u16GroupAddr) == true)
            {
                for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
                {
                    int index = listViewLNTGROUPINFO.CheckedIndices[i];
                    string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                    if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                    {
                        sendGroupRemove(u16ShortAddr, 1, 1, u16GroupAddr);
                    }
                }
            }
        }

        private void buttonLNTVIEWGROUP_Click(object sender, EventArgs e)
        {
            UInt16 u16GroupAddr;
            UInt16 u16ShortAddr;
            removeAllGroup = false;
            if (bStringToUint16(textBoxLNTVIEWGROUPADDRESS.Text, out u16GroupAddr) == true)
            {
                for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
                {
                    int index = listViewLNTGROUPINFO.CheckedIndices[i];
                    string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                    if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                    {
                        sendViewGroup(u16ShortAddr, 1, 1, u16GroupAddr);
                    }
                }
            }
        }

        private void buttonLNTREMOVEALL_Click(object sender, EventArgs e)
        {
            removeAllGroup = true;
            UInt16 u16ShortAddr;
            for (int i = 0; i < listViewLNTGROUPINFO.CheckedItems.Count; i++)
            {
                int index = listViewLNTGROUPINFO.CheckedIndices[i];
                string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    sendGroupRemoveAll(u16ShortAddr, 1, 1);
                }
            }
        }

        private void buttonLNTLEVEL_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {
                if (listViewLNTGROUPINFO.CheckedItems.Count > 0)
                {
                    if (textBoxLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxLNTSETLOOP.Text, out readAttributeLoop);
                    }

                    if (bStringToUint16Decimal(textBoxLNTSETPARASTEP.Text, out u16Step) == true)
                    {
                        step = u16Step;
                        if (bStringToUint16Decimal(textBoxLNTSETPARADIR.Text, out u16Dir) == true)
                        {
                            setDir = u16Dir;
                            if (bStringToUint8(textBoxLNTLEVEL.Text, out currentLevel) == true)
                            {
                                LNTLevelFlag = true;
                                Thread timerLevelThread = new Thread(customertimerLevel);
                                levelThreadStop = false;
                                timerLevelThread.Priority = ThreadPriority.Highest;

                                syncEventPort1 = true;
                                syncEvent.Reset();
                                timeBeginPeriod(1);
                                timerLevelThread.Start();
                            }
                        }


                    }


                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonLNTSTOPLEVEL_Click(object sender, EventArgs e)
        {
            levelThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonLNTHUE_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {
                if (listViewLNTGROUPINFO.CheckedItems.Count > 0)
                {
                    if (textBoxLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    if (bStringToUint16Decimal(textBoxLNTSETPARASTEP.Text, out u16Step) == true)
                    {
                        step = u16Step;
                        if (bStringToUint16Decimal(textBoxLNTSETPARADIR.Text, out u16Dir) == true)
                        {
                            setDir = u16Dir;
                            if (bStringToUint8(textBoxLNTHUE.Text, out currentHue) == true)
                            {
                                LNTHueFlag = true;
                                Thread timerHueThread = new Thread(customertimerHue);
                                hueThreadStop = false;
                                timerHueThread.Priority = ThreadPriority.Highest;

                                syncEventPort1 = true;
                                syncEvent.Reset();
                                timeBeginPeriod(1);
                                timerHueThread.Start();
                            }
                        }


                    }


                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonHUESTOP_Click(object sender, EventArgs e)
        {
            hueThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonLNTCOLOR_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {
                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    if (bStringToUint16Decimal(textBoxLNTSETPARASTEP.Text, out u16Step) == true)
                    {
                        step = u16Step;
                        if (bStringToUint16Decimal(textBoxLNTSETPARADIR.Text, out u16Dir) == true)
                        {
                            setDir = u16Dir;
                            if (bStringToUint16(textBoxEZLNTCOLORX.Text, out currentX) == true)
                            {
                                if (bStringToUint16(textBoxEZLNTCOLORY.Text, out currentY) == true)
                                {
                                    LNTColorFlag = false;
                                    Thread timerColorThread = new Thread(customertimerColor);
                                    colorThreadStop = false;
                                    timerColorThread.Priority = ThreadPriority.Highest;

                                    syncEventPort1 = true;
                                    syncEvent.Reset();
                                    timeBeginPeriod(1);
                                    timerColorThread.Start();
                                }
                            }
                        }

                    }

                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonLNTCOLORSTOP_Click(object sender, EventArgs e)
        {
            colorThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonLNTSAT_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {
                if (listViewEZLNTGROUP.CheckedItems.Count > 0)
                {
                    if (textBoxEZLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxEZLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    if (bStringToUint16Decimal(textBoxLNTSETPARASTEP.Text, out u16Step) == true)
                    {
                        step = u16Step;
                        if (bStringToUint16Decimal(textBoxLNTSETPARADIR.Text, out u16Dir) == true)
                        {
                            setDir = u16Dir;
                            if (bStringToUint8(textBoxEZLNTSAT.Text, out currentSat) == true)
                            {
                                LNTSatFlag = false;
                                Thread timerSatThread = new Thread(customertimerSat);
                                satThreadStop = false;
                                timerSatThread.Priority = ThreadPriority.Highest;

                                syncEventPort1 = true;
                                syncEvent.Reset();
                                timeBeginPeriod(1);
                                timerSatThread.Start();
                            }
                        }


                    }


                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonLNTSTOPCOLOR_Click(object sender, EventArgs e)
        {
            colorThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonLNTTEMP_Click(object sender, EventArgs e)
        {
            UInt16 u16Dir;
            UInt16 u16Step;
            if (serialPort1.IsOpen)
            {
                if (listViewLNTGROUPINFO.CheckedItems.Count > 0)
                {
                    if (textBoxLNTSETLOOP.Text != "")
                    {
                        bStringToUint16Decimal(textBoxLNTSETLOOP.Text, out readAttributeLoop);
                    }
                    if (bStringToUint16Decimal(textBoxLNTSETPARASTEP.Text, out u16Step) == true)
                    {
                        step = u16Step;
                        if (bStringToUint16Decimal(textBoxLNTSETPARADIR.Text, out u16Dir) == true)
                        {
                            setDir = u16Dir;
                            if (bStringToUint16(textBoxLNTTEMP.Text, out currentTemp))
                            {
                                LNTTempFlag = true;
                                Thread timerTempThread = new Thread(customertimerTemp);
                                tempThreadStop = false;
                                timerTempThread.Priority = ThreadPriority.Highest;

                                syncEventPort1 = true;
                                syncEvent.Reset();
                                timeBeginPeriod(1);
                                timerTempThread.Start();
                            }
                        }

                    }

                }
                else
                {
                    MessageBox.Show("None joined node has been choosen");
                }
            }
        }

        private void buttonLNTTEMPSTOP_Click(object sender, EventArgs e)
        {
            tempThreadStop = true;
            timeEndPeriod(1);
        }

        #endregion

        #region LNT GW

        private void buttonLNTGWSENDCMD_Click(object sender, EventArgs e)
        {
            if (textBoxLNTGWSENDCMD.Text != string.Empty)
            {
                if (serialPort2.IsOpen)
                {
                    byte[] array = System.Text.Encoding.ASCII.GetBytes(textBoxLNTGWSENDCMD.Text);
                    serialPort2.Write(array,0,1);
                }
                else
                {
                    MessageBox.Show("DBG port is not open");
                }

            }
            else
            {
                MessageBox.Show("cmd is empty");
            }
        }

        private void buttonLNTGWDBGPORT_Click(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                try
                {
                    serialPort2.Close();
                    listViewLNTGWINFO.Items.Clear();
                    listViewLNTGWGROUPINFO.Items.Clear();
                    buttonLNTGWDBGPORT.Text = "Open GW Dbg Port";
                    bDBGPortConfigured = false;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error - closeDBGPort: " + ex);
                }
            }
            else
            {
                PortSettings settings = new PortSettings();

                if (settings.ShowDialog() == DialogResult.OK)
                {
                    serialPort2.PortName = settings.selectedPort;
                    serialPort2.BaudRate = settings.selectedBaudRate;
                    serialPort2.DataBits = 8;
                    serialPort2.Parity = Parity.None;
                    serialPort2.StopBits = StopBits.One;
                    serialPort2.DataReceived += new SerialDataReceivedEventHandler(serialPort2_DataReceivedHandler);

                    displayPortSettings(serialPort2);

                    bDBGPortConfigured = true;
                }
            }

            if (bDBGPortConfigured == true)
            {
                try
                {
                    
                    serialPort2.Open();
                    sendGetIEEEAddress();
                    buttonLNTGWDBGPORT.Text = "Close GW Dbg Port";
                    
                    displayPortSettings(serialPort2);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error - openDBGPort: " + ex);
                }
            }

        }

        #endregion

        #region general input handling functions

        private bool bStringToUint8(string inputString, out byte u8Data)
        {
            bool bResult = true;

            if (Byte.TryParse(inputString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out u8Data) == false)
            {
                // Show error message
                MessageBox.Show("Invalid Parameter");
                bResult = false;
            }
            return bResult;
        }

        private bool bStringToUint16(string inputString, out UInt16 u16Data)
        {
            bool bResult = true;

            if (UInt16.TryParse(inputString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out u16Data) == false)
            {
                // Show error message
                MessageBox.Show("Invalid Parameter");
                bResult = false;
            }
            return bResult;
        }

        private bool bStringToUint16Decimal(string inputString, out UInt16 u16Data)
        {
            bool bResult = true;

            if (UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out u16Data) == false)
            {
                // Show error message
                MessageBox.Show("Invalid Parameter");
                bResult = false;
            }
            return bResult;
        }

        private bool bStringToUint32(string inputString, out UInt32 u32Data)
        {
            bool bResult = true;

            if (UInt32.TryParse(inputString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out u32Data) == false)
            {
                // Show error message
                MessageBox.Show("Invalid Parameter");
                bResult = false;
            }
            return bResult;
        }

        private bool bStringToUint64(string inputString, out UInt64 u64Data)
        {
            bool bResult = true;

            if (UInt64.TryParse(inputString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out u64Data) == false)
            {
                // Show error message
                MessageBox.Show("Invalid Parameter");
                bResult = false;
            }
            return bResult;
        }

        private bool bStringToUint128(string inputString, out byte[] au8Data)
        {
            bool bResult = true;
            au8Data = new byte[16];

            if (inputString.Length == 32)
            {
                for (int i = 0; i < inputString.Length; i += 2)
                {
                    if (bStringToUint8(inputString.Substring(i, 2), out au8Data[i / 2]) == false)
                    {
                        bResult = false;
                        break;
                    }

                }
            }
            else
            {
                bResult = false;
            }
            return bResult;
        }

        private bool bStringToUint16Array(string inputString, out UInt16[] au16Data)
        {
            bool bResult = true;
            au16Data = new UInt16[8];

            if ((inputString.Length % 4) == 0)
            {
                for (int i = 0; i < inputString.Length; i += 4)
                {
                    if (bStringToUint16(inputString.Substring(i, 4), out au16Data[i / 4]) == false)
                    {
                        bResult = false;
                        break;
                    }
                }
            }
            else
            {
                bResult = false;
            }
            return bResult;
        }

        private void textBoxClearSetTextBlack_MouseClick(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            textBox.ForeColor = System.Drawing.Color.Black;
            textBox.Text = "";
        }

        #endregion

        #region command transmit functions

        private void sendBasicResetFactoryDefaultCommand(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;

            commandData[u8Len++] = u8DstAddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;

            // Transmit command
            transmitCommand(0x0050, u8Len, commandData);
        }

        private void sendMgmtNwkUpdateRequest(byte u8DstAddrMode, UInt16 u16ShortAddr, UInt32 u32ChannelMask, byte u8ScanDuration, byte u8ScanCount, UInt16 u16NwkManangerAddr)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;

            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = (byte)(u32ChannelMask >> 24);
            commandData[u8Len++] = (byte)(u32ChannelMask >> 16);
            commandData[u8Len++] = (byte)(u32ChannelMask >> 8);
            commandData[u8Len++] = (byte)u32ChannelMask;
            commandData[u8Len++] = u8ScanDuration;
            commandData[u8Len++] = u8ScanCount;
            commandData[u8Len++] = (byte)(u16NwkManangerAddr >> 8);
            commandData[u8Len++] = (byte)u16NwkManangerAddr;

            // Transmit command
            transmitCommand(0x004A, u8Len, commandData);
        }

        private void sendOneToManyRouteRequest(byte u8CacheRoute, byte u8Radius)
        {
            byte[] commandData = null;
            commandData = new byte[128];
            byte u8Len = 0;

            commandData[u8Len++] = 0; // u8DstAddrMode;
            commandData[u8Len++] = 0; // (by0x8001te)(u16ShortAddr >> 8);
            commandData[u8Len++] = 0; // (byte)u16ShortAddr;
            commandData[u8Len++] = u8CacheRoute;
            commandData[u8Len++] = u8Radius;

            // Transmit command
            transmitCommand(0x004F, u8Len, commandData);
        }

        private void sendReadReportConfigRequest(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16ClusterID, byte u8DirServerClient, byte u8NbrAttribs, byte u8ManuSpecific, UInt16 u16ManuID, byte u8DirIsRx, UInt16 u16AttribId)
        {
            byte[] commandData = null;
            commandData = new byte[128];
            byte u8Len = 0;

            commandData[u8Len++] = u8DstAddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = (byte)(u16ClusterID >> 8);
            commandData[u8Len++] = (byte)u16ClusterID;
            commandData[u8Len++] = u8DirServerClient;
            commandData[u8Len++] = u8NbrAttribs;
            commandData[u8Len++] = u8ManuSpecific;
            commandData[u8Len++] = (byte)(u16ManuID >> 8);
            commandData[u8Len++] = (byte)u16ManuID;
            commandData[u8Len++] = u8DirIsRx;
            commandData[u8Len++] = (byte)(u16AttribId >> 8);
            commandData[u8Len++] = (byte)u16AttribId;

            // Transmit command
            transmitCommand(0x0122, u8Len, commandData);
        }


        private void sendOtaEndResponse(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8SeqNbr, UInt32 u32UpgradeTime, UInt32 u32CurrentTime, UInt32 u32FileVersion, UInt16 u16ImageType, UInt16 u16ManuCode)
        {
            byte[] commandData = null;
            commandData = new byte[128];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = u8SeqNbr;
            commandData[u8Len++] = (byte)(u32UpgradeTime >> 24);
            commandData[u8Len++] = (byte)(u32UpgradeTime >> 16);
            commandData[u8Len++] = (byte)(u32UpgradeTime >> 8);
            commandData[u8Len++] = (byte)u32UpgradeTime;
            commandData[u8Len++] = (byte)(u32CurrentTime >> 24);
            commandData[u8Len++] = (byte)(u32CurrentTime >> 16);
            commandData[u8Len++] = (byte)(u32CurrentTime >> 8);
            commandData[u8Len++] = (byte)u32CurrentTime;
            commandData[u8Len++] = (byte)(u32FileVersion >> 24);
            commandData[u8Len++] = (byte)(u32FileVersion >> 16);
            commandData[u8Len++] = (byte)(u32FileVersion >> 8);
            commandData[u8Len++] = (byte)u32FileVersion;
            commandData[u8Len++] = (byte)(u16ImageType >> 8);
            commandData[u8Len++] = (byte)u16ImageType;
            commandData[u8Len++] = (byte)(u16ManuCode >> 8);
            commandData[u8Len++] = (byte)u16ManuCode;

            // Transmit command
            transmitCommand(0x0504, u8Len, commandData);
        }


        private void sendOtaBlock(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8SeqNbr, byte u8Status, UInt32 u32FileOffset, UInt32 u32FileVersion, UInt16 u16ImageType, UInt16 u16ManuCode, byte u8DataSize, byte[] au8Data)
        {
            byte[] commandData = null;
            commandData = new byte[128];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = u8SeqNbr;
            commandData[u8Len++] = u8Status;
            commandData[u8Len++] = (byte)(u32FileOffset >> 24);
            commandData[u8Len++] = (byte)(u32FileOffset >> 16);
            commandData[u8Len++] = (byte)(u32FileOffset >> 8);
            commandData[u8Len++] = (byte)u32FileOffset;
            commandData[u8Len++] = (byte)(u32FileVersion >> 24);
            commandData[u8Len++] = (byte)(u32FileVersion >> 16);
            commandData[u8Len++] = (byte)(u32FileVersion >> 8);
            commandData[u8Len++] = (byte)u32FileVersion;
            commandData[u8Len++] = (byte)(u16ImageType >> 8);
            commandData[u8Len++] = (byte)u16ImageType;
            commandData[u8Len++] = (byte)(u16ManuCode >> 8);
            commandData[u8Len++] = (byte)u16ManuCode;
            commandData[u8Len++] = u8DataSize;

            byte i;
            for (i = 0; i < u8DataSize; i++)
            {
                commandData[u8Len++] = au8Data[u32FileOffset + i];
            }

            // Transmit command
            transmitCommand(0x0502, u8Len, commandData);
        }

        private void sendOtaSetWaitForDataParams(byte u8DstAddrMode, UInt16 u16TargetAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8SeqNbr, byte u8Status, UInt32 u32CurrentTime, UInt32 u32RequestTime, UInt16 u16BlockDelay)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;
            commandData[u8Len++] = (byte)(u16TargetAddr >> 8);
            commandData[u8Len++] = (byte)u16TargetAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = u8SeqNbr;
            commandData[u8Len++] = u8Status;
            commandData[u8Len++] = (byte)(u32CurrentTime >> 24);
            commandData[u8Len++] = (byte)(u32CurrentTime >> 16);
            commandData[u8Len++] = (byte)(u32CurrentTime >> 8);
            commandData[u8Len++] = (byte)u32CurrentTime;
            commandData[u8Len++] = (byte)(u32RequestTime >> 24);
            commandData[u8Len++] = (byte)(u32RequestTime >> 16);
            commandData[u8Len++] = (byte)(u32RequestTime >> 8);
            commandData[u8Len++] = (byte)u32RequestTime;
            commandData[u8Len++] = (byte)(u16BlockDelay >> 8);
            commandData[u8Len++] = (byte)u16BlockDelay;

            // Transmit command
            transmitCommand(0x0506, u8Len, commandData);
        }

        private void sendOtaImageNotify(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8NotifyType, UInt32 u32FileVersion, UInt16 u16ImageType, UInt16 u16ManuCode, byte u8Jitter)
        {
            byte[] commandData = null;
            commandData = new byte[16];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = u8NotifyType;
            commandData[u8Len++] = (byte)(u32FileVersion >> 24);
            commandData[u8Len++] = (byte)(u32FileVersion >> 16);
            commandData[u8Len++] = (byte)(u32FileVersion >> 8);
            commandData[u8Len++] = (byte)u32FileVersion;
            commandData[u8Len++] = (byte)(u16ImageType >> 8);
            commandData[u8Len++] = (byte)u16ImageType;
            commandData[u8Len++] = (byte)(u16ManuCode >> 8);
            commandData[u8Len++] = (byte)u16ManuCode;
            commandData[u8Len++] = u8Jitter;

            // Transmit command
            transmitCommand(0x0505, u8Len, commandData);
        }

        private void sendOtaLoadNewImage(byte u8DstAddrMode, UInt16 u16ShortAddr, UInt32 u32FileIdentifier, UInt16 u16HeaderVersion, UInt16 u16HeaderLength, UInt16 u16HeaderControlField, UInt16 u16ManufacturerCode, UInt16 u16ImageType, UInt32 u32FileVersion, UInt16 u16StackVersion, byte[] au8HeaderString, UInt32 u32TotalImage, byte u8SecurityCredVersion, UInt64 u64UpgradeFileDest, UInt16 u16MinimumHwVersion, UInt16 u16MaxHwVersion)
        {
            byte[] commandData = null;
            commandData = new byte[72];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = (byte)(u32FileIdentifier >> 24);
            commandData[u8Len++] = (byte)(u32FileIdentifier >> 16);
            commandData[u8Len++] = (byte)(u32FileIdentifier >> 8);
            commandData[u8Len++] = (byte)u32FileIdentifier;
            commandData[u8Len++] = (byte)(u16HeaderVersion >> 8);
            commandData[u8Len++] = (byte)u16HeaderVersion;
            commandData[u8Len++] = (byte)(u16HeaderLength >> 8);
            commandData[u8Len++] = (byte)u16HeaderLength;
            commandData[u8Len++] = (byte)(u16HeaderControlField >> 8);
            commandData[u8Len++] = (byte)u16HeaderControlField;
            commandData[u8Len++] = (byte)(u16ManufacturerCode >> 8);
            commandData[u8Len++] = (byte)u16ManufacturerCode;
            commandData[u8Len++] = (byte)(u16ImageType >> 8);
            commandData[u8Len++] = (byte)u16ImageType;
            commandData[u8Len++] = (byte)(u32FileVersion >> 24);
            commandData[u8Len++] = (byte)(u32FileVersion >> 16);
            commandData[u8Len++] = (byte)(u32FileVersion >> 8);
            commandData[u8Len++] = (byte)u32FileVersion;
            commandData[u8Len++] = (byte)(u16StackVersion >> 8);
            commandData[u8Len++] = (byte)u16StackVersion;

            if (au8HeaderString != null)
            {
                byte i;
                for (i = 0; i < 32; i++)
                {
                    commandData[u8Len++] = au8HeaderString[i];
                }
            }

            commandData[u8Len++] = (byte)(u32TotalImage >> 24);
            commandData[u8Len++] = (byte)(u32TotalImage >> 16);
            commandData[u8Len++] = (byte)(u32TotalImage >> 8);
            commandData[u8Len++] = (byte)u32TotalImage;
            commandData[u8Len++] = u8SecurityCredVersion;
            commandData[u8Len++] = (byte)(u64UpgradeFileDest >> 56);
            commandData[u8Len++] = (byte)(u64UpgradeFileDest >> 48);
            commandData[u8Len++] = (byte)(u64UpgradeFileDest >> 40);
            commandData[u8Len++] = (byte)(u64UpgradeFileDest >> 32);
            commandData[u8Len++] = (byte)(u64UpgradeFileDest >> 24);
            commandData[u8Len++] = (byte)(u64UpgradeFileDest >> 16);
            commandData[u8Len++] = (byte)(u64UpgradeFileDest >> 8);
            commandData[u8Len++] = (byte)u64UpgradeFileDest;
            commandData[u8Len++] = (byte)(u16MinimumHwVersion >> 8);
            commandData[u8Len++] = (byte)u16MinimumHwVersion;
            commandData[u8Len++] = (byte)(u16MaxHwVersion >> 8);
            commandData[u8Len++] = (byte)u16MaxHwVersion;

            // Transmit command
            transmitCommand(0x0500, u8Len, commandData);
        }

        private void sendIASEnrollResponse(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8Code, byte u8ZoneId)
        {
            byte[] commandData = null;
            commandData = new byte[7];
            byte u8Len = 7;

            // Build command payload   
            commandData[0] = u8DstAddrMode;
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = u8Code;
            commandData[6] = u8ZoneId;

            // Transmit command
            transmitCommand(0x0400, u8Len, commandData);
        }

        private void sendMoveToColorTemp(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16ColorTemp, UInt16 u16TransTime)
        {
            byte[] commandData = null;
            commandData = new byte[9];
            byte u8Len = 9;

            // Build command payload   
            commandData[0] = 0x02; // Short address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16ColorTemp >> 8);
            commandData[6] = (byte)u16ColorTemp;
            commandData[7] = (byte)(u16TransTime >> 8);
            commandData[8] = (byte)u16TransTime;

            // Transmit command
            transmitCommand(0x00C0, u8Len, commandData);
        }

        private void sendWriteAttribRequest(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16ClusterID, byte u8Direction, byte u8ManuSpecific, UInt16 u16ManuID, byte u8AttribCount, UInt16 u16AttribID, byte u8AttribType, byte[] au8Data, byte u8DataLen)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            int u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = 0x02; // Short address mode
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = (byte)(u16ClusterID >> 8);
            commandData[u8Len++] = (byte)u16ClusterID;
            commandData[u8Len++] = u8Direction;
            commandData[u8Len++] = u8ManuSpecific;
            commandData[u8Len++] = (byte)(u16ManuID >> 8);
            commandData[u8Len++] = (byte)u16ManuID;
            commandData[u8Len++] = u8AttribCount;
            commandData[u8Len++] = (byte)(u16AttribID >> 8);
            commandData[u8Len++] = (byte)u16AttribID;
            commandData[u8Len++] = u8AttribType;

            int i;
            for (i = 0; i < u8DataLen; i++)
            {
                commandData[u8Len] = au8Data[i];
                u8Len++;
            }

            /* Need to re-size the array because if we send more data, 
             * the control bridge will convert it to another write attribute */
            Array.Resize(ref commandData, u8Len);

            // Transmit command
            transmitCommand(0x0110, u8Len, commandData);
        }

        private void sendRemoveScene(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupId, byte u8SceneId)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = (byte)(u16GroupId >> 8);
            commandData[u8Len++] = (byte)u16GroupId;
            commandData[u8Len++] = u8SceneId;

            // Transmit command
            transmitCommand(0x00A2, u8Len, commandData);
        }

        private void sendRemoveAllScenes(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupId)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;               // 0 
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);   // 1
            commandData[u8Len++] = (byte)u16ShortAddr;          // 2
            commandData[u8Len++] = u8SrcEndPoint;               // 3
            commandData[u8Len++] = u8DstEndPoint;               // 4
            commandData[u8Len++] = (byte)(u16GroupId >> 8);     // 5
            commandData[u8Len++] = (byte)u16GroupId;            // 6

            // Transmit command
            transmitCommand(0x00A3, u8Len, commandData);
        }

        private void sendGetSceneMembership(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupId)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;               // 0 
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);   // 1
            commandData[u8Len++] = (byte)u16ShortAddr;          // 2
            commandData[u8Len++] = u8SrcEndPoint;               // 3
            commandData[u8Len++] = u8DstEndPoint;               // 4
            commandData[u8Len++] = (byte)(u16GroupId >> 8);     // 5
            commandData[u8Len++] = (byte)u16GroupId;            // 6

            // Transmit command
            transmitCommand(0x00A6, u8Len, commandData);
        }

        private void sendAddSceneExtData(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupId, byte u8SceneId, UInt16 u16TransTime, String sName, byte u8NameLen, byte u8NameMaxLen, UInt16 u16SceneLength, string stringSceneData)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;
            String stringData = "";

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;               // 0 
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);   // 1
            commandData[u8Len++] = (byte)u16ShortAddr;          // 2
            commandData[u8Len++] = u8SrcEndPoint;               // 3
            commandData[u8Len++] = u8DstEndPoint;               // 4
            commandData[u8Len++] = (byte)(u16GroupId >> 8);     // 5
            commandData[u8Len++] = (byte)u16GroupId;            // 6
            commandData[u8Len++] = u8SceneId;                   // 7
            commandData[u8Len++] = (byte)(u16TransTime >> 8);   // 8
            commandData[u8Len++] = (byte)u16TransTime;          // 9
            commandData[u8Len++] = u8NameLen;                   // 10
            commandData[u8Len++] = u8NameMaxLen;                // 11

            char[] u8Array = sName.ToCharArray();

            for (int i = 0; i < sName.ToCharArray().Length; i++)
            {
                commandData[u8Len + i] = (byte)u8Array[i];
            }

            u8Len += u8NameMaxLen;

            commandData[u8Len++] = (byte)(u16SceneLength >> 8); // 12+=u8NameLen
            commandData[u8Len++] = (byte)u16SceneLength;        // 13+=u8NameLen

            stringData = stringSceneData.Replace(" ", "");
            stringData = stringSceneData.Replace(":", "");

            for (int i = 0; i < stringData.Length; i += 2)
            {
                Array.Resize(ref commandData, u8Len + 1);
                commandData[u8Len++] = (byte)Convert.ToInt32(stringData.ToCharArray()[i].ToString() + stringData.ToCharArray()[i + 1].ToString(), 16);
            }

            // Transmit command
            transmitCommand(0x00A1, u8Len, commandData);
        }

        private void sendAddScene(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupId, byte u8SceneId)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;               // 0 
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);   // 1
            commandData[u8Len++] = (byte)u16ShortAddr;          // 2
            commandData[u8Len++] = u8SrcEndPoint;               // 3
            commandData[u8Len++] = u8DstEndPoint;               // 4
            commandData[u8Len++] = (byte)(u16GroupId >> 8);     // 5
            commandData[u8Len++] = (byte)u16GroupId;            // 6
            commandData[u8Len++] = u8SceneId;                   // 7

            // Transmit command
            transmitCommand(0x00A1, u8Len, commandData);
        }

        private void sendViewScene(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupId, byte u8SceneId)
        {
            byte[] commandData = null;
            commandData = new byte[8];
            byte u8Len = 8;

            // Build command payload   
            commandData[0] = u8DstAddrMode;
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16GroupId >> 8);
            commandData[6] = (byte)u16GroupId;
            commandData[7] = u8SceneId;

            // Transmit command
            transmitCommand(0x00A0, u8Len, commandData);
        }

        private void sendStoreScene(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupId, byte u8SceneId)
        {
            byte[] commandData = null;
            commandData = new byte[8];
            byte u8Len = 8;

            // Build command payload   
            commandData[0] = u8DstAddrMode;
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16GroupId >> 8);
            commandData[6] = (byte)u16GroupId;
            commandData[7] = u8SceneId;

            // Transmit command
            transmitCommand(0x00A4, u8Len, commandData);
        }

        private void sendRecallScene(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupId, byte u8SceneId)
        {
            byte[] commandData = null;
            commandData = new byte[8];
            byte u8Len = 8;

            // Build command payload   
            commandData[0] = u8DstAddrMode;
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16GroupId >> 8);
            commandData[6] = (byte)u16GroupId;
            commandData[7] = u8SceneId;

            // Transmit command
            transmitCommand(0x00A5, u8Len, commandData);
        }

        private void sendUnBindRequest(UInt64 u64TargetExtAddr, byte u8TargetEndPoint, UInt16 u16ClusterID, byte u8DstAddrMode, UInt64 u64DstAddr, byte u8DstEndPoint)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 56);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 48);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 40);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 32);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 24);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 16);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 8);
            commandData[u8Len++] = (byte)u64TargetExtAddr;
            commandData[u8Len++] = u8TargetEndPoint;
            commandData[u8Len++] = (byte)(u16ClusterID >> 8);
            commandData[u8Len++] = (byte)u16ClusterID;
            commandData[u8Len++] = u8DstAddrMode;

            if (u8DstAddrMode == 3)
            {
                commandData[u8Len++] = (byte)(u64DstAddr >> 56);
                commandData[u8Len++] = (byte)(u64DstAddr >> 48);
                commandData[u8Len++] = (byte)(u64DstAddr >> 40);
                commandData[u8Len++] = (byte)(u64DstAddr >> 32);
                commandData[u8Len++] = (byte)(u64DstAddr >> 24);
                commandData[u8Len++] = (byte)(u64DstAddr >> 16);
                commandData[u8Len++] = (byte)(u64DstAddr >> 8);
                commandData[u8Len++] = (byte)u64DstAddr;
                commandData[u8Len++] = u8DstEndPoint;
            }
            else
            {
                commandData[u8Len++] = (byte)(u64DstAddr >> 8);
                commandData[u8Len++] = (byte)u64DstAddr;
                commandData[u8Len++] = u8DstEndPoint;
            }

            // Transmit command
            transmitCommand(0x0031, u8Len, commandData);
        }

        private void sendBindRequest(UInt64 u64TargetExtAddr, byte u8TargetEndPoint, UInt16 u16ClusterID, byte u8DstAddrMode, UInt64 u64DstAddr, byte u8DstEndPoint)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 56);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 48);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 40);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 32);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 24);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 16);
            commandData[u8Len++] = (byte)(u64TargetExtAddr >> 8);
            commandData[u8Len++] = (byte)u64TargetExtAddr;
            commandData[u8Len++] = u8TargetEndPoint;
            commandData[u8Len++] = (byte)(u16ClusterID >> 8);
            commandData[u8Len++] = (byte)u16ClusterID;
            commandData[u8Len++] = u8DstAddrMode;

            if (u8DstAddrMode == 3)
            {
                commandData[u8Len++] = (byte)(u64DstAddr >> 56);
                commandData[u8Len++] = (byte)(u64DstAddr >> 48);
                commandData[u8Len++] = (byte)(u64DstAddr >> 40);
                commandData[u8Len++] = (byte)(u64DstAddr >> 32);
                commandData[u8Len++] = (byte)(u64DstAddr >> 24);
                commandData[u8Len++] = (byte)(u64DstAddr >> 16);
                commandData[u8Len++] = (byte)(u64DstAddr >> 8);
                commandData[u8Len++] = (byte)u64DstAddr;
                commandData[u8Len++] = u8DstEndPoint;
            }
            else
            {
                commandData[u8Len++] = (byte)(u64DstAddr >> 8);
                commandData[u8Len++] = (byte)u64DstAddr;
            }

            // Transmit command
            transmitCommand(0x0030, u8Len, commandData);
        }

        private void sendConfigReportRequest(byte u8DstAddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16ClusterID, byte u8ReportDirection, byte u8AttribDirection, byte u8AttribType, UInt16 u16AttribId, UInt16 u16MinInterval, UInt16 u16MaxInterval, UInt16 u16TimeOut, UInt64 u64Change)
        {
            byte[] commandData = null;
            commandData = new byte[30];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8DstAddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = (byte)(u16ClusterID >> 8);
            commandData[u8Len++] = (byte)u16ClusterID;
            commandData[u8Len++] = u8ReportDirection;
            commandData[u8Len++] = 0; // ManuSpecific
            commandData[u8Len++] = 0; // ManuID
            commandData[u8Len++] = 0; // ManuID
            commandData[u8Len++] = 1; // Number of attributes

            commandData[u8Len++] = u8AttribDirection;
            commandData[u8Len++] = u8AttribType;
            commandData[u8Len++] = (byte)(u16AttribId >> 8);
            commandData[u8Len++] = (byte)u16AttribId; ;
            commandData[u8Len++] = (byte)(u16MinInterval >> 8);
            commandData[u8Len++] = (byte)u16MinInterval;
            commandData[u8Len++] = (byte)(u16MaxInterval >> 8);
            commandData[u8Len++] = (byte)u16MaxInterval;
            commandData[u8Len++] = (byte)(u16TimeOut >> 8); ;
            commandData[u8Len++] = (byte)u16TimeOut;

            if (u8AttribType >= 0x20 &&
                u8AttribType <= 0x2f)
            {
                switch (u8AttribType)
                {
                    case 0x20:
                    case 0x28:
                        commandData[u8Len++] = (byte)(u64Change);
                        break;
                    case 0x21:
                    case 0x29:
                        commandData[u8Len++] = (byte)(u64Change >> 8);
                        commandData[u8Len++] = (byte)(u64Change);
                        break;
                    case 0x22:
                    case 0x2a:
                        commandData[u8Len++] = (byte)(u64Change >> 16);
                        commandData[u8Len++] = (byte)(u64Change >> 8);
                        commandData[u8Len++] = (byte)(u64Change);
                        break;
                    case 0x23:
                    case 0x2b:

                        commandData[u8Len++] = (byte)(u64Change >> 24);
                        commandData[u8Len++] = (byte)(u64Change >> 16);
                        commandData[u8Len++] = (byte)(u64Change >> 8);
                        commandData[u8Len++] = (byte)(u64Change);
                        break;
                    case 0x24:
                    case 0x2c:
                        commandData[u8Len++] = (byte)(u64Change >> 32);
                        commandData[u8Len++] = (byte)(u64Change >> 24);
                        commandData[u8Len++] = (byte)(u64Change >> 16);
                        commandData[u8Len++] = (byte)(u64Change >> 8);
                        commandData[u8Len++] = (byte)(u64Change);
                        break;
                    case 0x25:
                    case 0x2d:
                        commandData[u8Len++] = (byte)(u64Change >> 40);
                        commandData[u8Len++] = (byte)(u64Change >> 32);
                        commandData[u8Len++] = (byte)(u64Change >> 24);
                        commandData[u8Len++] = (byte)(u64Change >> 16);
                        commandData[u8Len++] = (byte)(u64Change >> 8);
                        commandData[u8Len++] = (byte)(u64Change);

                        break;
                    case 0x26:
                    case 0x2e:

                        commandData[u8Len++] = (byte)(u64Change >> 48);
                        commandData[u8Len++] = (byte)(u64Change >> 40);
                        commandData[u8Len++] = (byte)(u64Change >> 32);
                        commandData[u8Len++] = (byte)(u64Change >> 24);
                        commandData[u8Len++] = (byte)(u64Change >> 16);
                        commandData[u8Len++] = (byte)(u64Change >> 8);
                        commandData[u8Len++] = (byte)(u64Change);
                        break;
                    case 0x27:
                    case 0x2f:
                        commandData[u8Len++] = (byte)(u64Change >> 56);
                        commandData[u8Len++] = (byte)(u64Change >> 48);
                        commandData[u8Len++] = (byte)(u64Change >> 40);
                        commandData[u8Len++] = (byte)(u64Change >> 32);
                        commandData[u8Len++] = (byte)(u64Change >> 24);
                        commandData[u8Len++] = (byte)(u64Change >> 16);
                        commandData[u8Len++] = (byte)(u64Change >> 8);
                        commandData[u8Len++] = (byte)(u64Change);
                        break;
                    default:
                        break;
                }

            }
            else
            {
                /* WARNING : We should not be sent anything from the higher layer as there should be no reportable change field
                 * If we do get something for this record it's an error and the rest of the records will be all wrong.
                 *  */
            }
            // Transmit command
            transmitCommand(0x0120, u8Len, commandData);


        }

        private void sendDiscoverCommandsRequest(byte u8AddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16ClusterID, byte u8DirectionIsServerToClient, byte u8CommandId, byte u8ManuSpecific, UInt16 u16ManuID, byte u8MaxCommands, byte u8IsGenerated)
        {
            byte[] commandData = null;
            commandData = new byte[13];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = u8AddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = (byte)(u16ClusterID >> 8);
            commandData[u8Len++] = (byte)u16ClusterID;
            commandData[u8Len++] = u8DirectionIsServerToClient;
            commandData[u8Len++] = u8CommandId;
            commandData[u8Len++] = u8ManuSpecific;
            commandData[u8Len++] = (byte)(u16ManuID >> 8);
            commandData[u8Len++] = (byte)u16ManuID;
            commandData[u8Len++] = u8MaxCommands;

            if (u8IsGenerated == 0)
            {
                // Transmit command
                transmitCommand(0x0150, u8Len, commandData);
            }
            else
            {
                // Transmit command
                transmitCommand(0x0160, u8Len, commandData);
            }
        }

        private void sendRawDataCommandsRequest(byte u8AddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16ProfileID, UInt16 u16ClusterID, byte u8SecurityMode, byte u8Radius, String stringRawData)
        {
            byte[] commandData = null;
            commandData = new byte[12];
            byte u8Len = 0;
            String stringData = "";

            // Build command payload   
            commandData[u8Len++] = u8AddrMode;
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = (byte)(u16ClusterID >> 8);
            commandData[u8Len++] = (byte)u16ClusterID;
            commandData[u8Len++] = (byte)(u16ProfileID >> 8);
            commandData[u8Len++] = (byte)u16ProfileID;
            commandData[u8Len++] = u8SecurityMode;
            commandData[u8Len++] = u8Radius;

            stringData = stringRawData.Replace(" ", "");
            stringData = stringRawData.Replace(":", "");

            commandData[u8Len++] = (byte)(stringData.Length / 2);

            for (int i = 0; i < stringData.Length; i += 2)
            {
                Array.Resize(ref commandData, u8Len + 1);
                commandData[u8Len++] = (byte)Convert.ToInt32(stringData.ToCharArray()[i].ToString() + stringData.ToCharArray()[i + 1].ToString(), 16);
            }

            // Transmit command
            transmitCommand(0x0530, u8Len, commandData);

        }

        private void sendDiscoverAttributesRequest(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16ClusterID, UInt16 u16StartAttrib, byte u8Direction, byte u8ManuSpecific, UInt16 u16ManuID, byte u8AttribsMax, byte u8Extended)
        {
            byte[] commandData = null;
            commandData = new byte[14];
            byte u8Len = 14;

            // Build command payload   
            commandData[0] = 0x02; // Short address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16ClusterID >> 8);
            commandData[6] = (byte)u16ClusterID;
            commandData[7] = (byte)(u16StartAttrib >> 8);
            commandData[8] = (byte)u16StartAttrib;
            commandData[9] = u8Direction;
            commandData[10] = u8ManuSpecific;
            commandData[11] = (byte)(u16ManuID >> 8);
            commandData[12] = (byte)u16ManuID;
            commandData[13] = u8AttribsMax;

            // Transmit command
            if (u8Extended == 0)
            {
                transmitCommand(0x0140, u8Len, commandData);
            }
            else
            {
                transmitCommand(0x0141, u8Len, commandData);
            }
        }

        /*private void sendReadAllAttribRequest(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16ClusterID, byte u8Direction, byte u8ManuSpecific, UInt16 u16ManuID)
        *{
        *    byte[] commandData = null;
        *    commandData = new byte[11];
        *    byte u8Len = 11;
        *
        *    // Build command payload   
        *    commandData[0] = 0x02; // Short address mode
        *    commandData[1] = (byte)(u16ShortAddr >> 8);
        *    commandData[2] = (byte)u16ShortAddr;
        *    commandData[3] = u8SrcEndPoint;
        *    commandData[4] = u8DstEndPoint;
        *    commandData[5] = (byte)(u16ClusterID >> 8);
        *    commandData[6] = (byte)u16ClusterID;
        *    commandData[7] = u8Direction;
        *    commandData[8] = u8ManuSpecific;
        *    commandData[9] = (byte)(u16ManuID >> 8);
        *    commandData[10] = (byte)u16ManuID;
        *
        *    // Transmit command
        *    transmitCommand(0x0130, u8Len, commandData);
        }*/

        private void sendOOBCommissioningData(UInt64 u64AddrData, string stringkeydata)
        {
            byte[] commandData = null;
            commandData = new byte[24];
            byte u8Len = 0;
            String stringData = "";

            // Build command payload
            commandData[u8Len++] = (byte)(u64AddrData >> 56);  //0
            commandData[u8Len++] = (byte)(u64AddrData >> 48);  //1
            commandData[u8Len++] = (byte)(u64AddrData >> 40);   //2
            commandData[u8Len++] = (byte)(u64AddrData >> 32);   //3
            commandData[u8Len++] = (byte)(u64AddrData >> 24);   //4
            commandData[u8Len++] = (byte)(u64AddrData >> 16);   //5
            commandData[u8Len++] = (byte)(u64AddrData >> 8);    //6
            commandData[u8Len++] = (byte)u64AddrData;           //7

            stringData = stringkeydata.Replace(" ", "");
            stringData = stringkeydata.Replace(":", "");

            for (int i = 0; i < stringData.Length; i += 2)
            {
                commandData[u8Len++] = (byte)Convert.ToInt32(stringData.ToCharArray()[i].ToString() + stringData.ToCharArray()[i + 1].ToString(), 16);  //8-23
            }

            // Transmit command
            transmitCommand(0x0029, u8Len, commandData);

        }

        private void sendReadAttribRequest(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16ClusterID, byte u8Direction, byte u8ManuSpecific, UInt16 u16ManuID, byte u8AttribCount, UInt16 u16AttribID1)
        {
            byte[] commandData = null;
            commandData = new byte[14];
            byte u8Len = 0;

            // Build command payload   
            commandData[u8Len++] = 0x02; // Short address mode
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = u8SrcEndPoint;
            commandData[u8Len++] = u8DstEndPoint;
            commandData[u8Len++] = (byte)(u16ClusterID >> 8);
            commandData[u8Len++] = (byte)u16ClusterID;
            commandData[u8Len++] = u8Direction;
            commandData[u8Len++] = u8ManuSpecific;
            commandData[u8Len++] = (byte)(u16ManuID >> 8);
            commandData[u8Len++] = (byte)u16ManuID;
            commandData[u8Len++] = u8AttribCount;
            commandData[u8Len++] = (byte)(u16AttribID1 >> 8);
            commandData[u8Len++] = (byte)u16AttribID1;

            // Transmit command
            transmitCommand(0x0100, u8Len, commandData);
        }

        private void sendLockUnlock(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8LockUnlock)
        {
            byte[] commandData = null;
            commandData = new byte[6];

            // Build command payload   
            commandData[0] = 0x02; // Short address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = u8LockUnlock;

            // Transmit command
            transmitCommand(0x00F0, 6, commandData);
        }

        private void sendNwkAddrRequest(UInt16 u16TargetAddr, UInt64 u64ExtAddr, byte u8Type, byte u8StartIndex)
        {
            byte[] commandData = null;
            commandData = new byte[12];

            // Build command payload            
            commandData[0] = (byte)(u16TargetAddr >> 8);
            commandData[1] = (byte)u16TargetAddr;
            commandData[2] = (byte)(u64ExtAddr >> 56);
            commandData[3] = (byte)(u64ExtAddr >> 48);
            commandData[4] = (byte)(u64ExtAddr >> 40);
            commandData[5] = (byte)(u64ExtAddr >> 32);
            commandData[6] = (byte)(u64ExtAddr >> 24);
            commandData[7] = (byte)(u64ExtAddr >> 16);
            commandData[8] = (byte)(u64ExtAddr >> 8);
            commandData[9] = (byte)u64ExtAddr;
            commandData[10] = u8Type;
            commandData[11] = u8StartIndex;

            // Transmit command
            transmitCommand(0x0040, 12, commandData);
        }

        private void sendIeeeAddrRequest(UInt16 u16TargetAddr, UInt16 u16ShortAddr, byte u8Type, byte u8StartIndex)
        {
            byte[] commandData = null;
            commandData = new byte[6];

            // Build command payload            
            commandData[0] = (byte)(u16TargetAddr >> 8);
            commandData[1] = (byte)u16TargetAddr;
            commandData[2] = (byte)(u16ShortAddr >> 8);
            commandData[3] = (byte)u16ShortAddr;
            commandData[4] = u8Type;
            commandData[5] = u8StartIndex;

            // Transmit command
            transmitCommand(0x0041, 6, commandData);
        }

        private void sendGroupAdd(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupAddr, byte u8GroupNameLength, byte u8GroupNameMaxLength, string sName)
        {
            byte[] commandData = null;
            commandData = new byte[16];
            byte u8Len = 0;

            // Build command payload
            commandData[u8Len++] = 0x02; // Short address mode    // 0
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);     // 1
            commandData[u8Len++] = (byte)u16ShortAddr;            // 2
            commandData[u8Len++] = u8SrcEndPoint;                 // 3
            commandData[u8Len++] = u8DstEndPoint;                 // 4
            commandData[u8Len++] = (byte)(u16GroupAddr >> 8);     // 5
            commandData[u8Len++] = (byte)u16GroupAddr;            // 6
            commandData[u8Len++] = u8GroupNameLength;             // 7
            commandData[u8Len++] = u8GroupNameMaxLength;          // 8

            char[] u8Array = sName.ToCharArray();

            for (int i = 0; i < sName.ToCharArray().Length; i++)
            {
                commandData[u8Len + i] = (byte)u8Array[i];
            }

            u8Len += u8GroupNameMaxLength;

            // Transmit command
            transmitCommand(0x0060, u8Len, commandData);
        }

        private void sendViewGroup(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupAddr)
        {
            byte[] commandData = null;
            commandData = new byte[7];

            // Build command payload            
            commandData[0] = 0x02; // Short address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16GroupAddr >> 8);
            commandData[6] = (byte)u16GroupAddr;

            // Transmit command
            transmitCommand(0x0061, 7, commandData);
        }

        private void sendGroupRemove(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupAddr)
        {
            byte[] commandData = null;
            commandData = new byte[7];

            // Build command payload            
            commandData[0] = 0x02; // Short address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16GroupAddr >> 8);
            commandData[6] = (byte)u16GroupAddr;

            // Transmit command
            transmitCommand(0x0063, 7, commandData);
        }

        private void sendGroupRemoveAll(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint)
        {
            byte[] commandData = null;
            commandData = new byte[5];

            // Build command payload            
            commandData[0] = 0x02; // Short address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;

            // Transmit command
            transmitCommand(0x0064, 5, commandData);
        }

        private void sendGroupGet(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8GroupCount, UInt16[] au16GroupList)
        {
            byte[] commandData = null;
            commandData = new byte[6];
            byte u8Length = 0;

            // Build command payload            
            commandData[u8Length++] = 0x02; // Short address mode
            commandData[u8Length++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Length++] = (byte)u16ShortAddr;
            commandData[u8Length++] = u8SrcEndPoint;
            commandData[u8Length++] = u8DstEndPoint;
            commandData[u8Length++] = u8GroupCount;


            for (byte i = 0; i < u8GroupCount; i++)
            {
                Array.Resize(ref commandData, u8Length + 2);
                commandData[u8Length++] = (byte)(au16GroupList[i] >> 8);
                commandData[u8Length++] = (byte)(au16GroupList[i]);
            }

            // Transmit command
            transmitCommand(0x0062, u8Length, commandData);
        }

        private void sendGroupAddIfIdentifying(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16GroupID)
        {
            byte[] commandData = null;
            commandData = new byte[8];

            // Build command payload            
            commandData[0] = 0x02; // Short address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16GroupID >> 8);
            commandData[6] = (byte)u16GroupID;

            // Transmit command
            transmitCommand(0x0065, 7, commandData);
        }

        private void sendEnhancedMoveToHue(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16Hue, byte u8Direction, UInt16 u16Time)
        {
            byte[] commandData = null;
            commandData = new byte[10];

            // Build command payload            
            commandData[0] = 0x02; // Address mode - short address
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = u8Direction;
            commandData[6] = (byte)(u16Hue >> 8);
            commandData[7] = (byte)u16Hue;
            commandData[8] = (byte)(u16Time >> 8);
            commandData[9] = (byte)u16Time;

            // Transmit command
            transmitCommand(0x00BA, 10, commandData);
        }

        private void sendZllClusterOnOff(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8EffectID, byte u8EffectGradient)
        {
            byte[] commandData = null;
            commandData = new byte[6];

            // Build command payload
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;
            commandData[2] = u8SrcEndPoint;
            commandData[3] = u8DstEndPoint;
            commandData[4] = u8EffectID;
            commandData[5] = u8EffectGradient;

            // Transmit command
            transmitCommand(0x0092, 6, commandData);
        }

        private void sendTouchlinkInitiate()
        {
            // Transmit command
            transmitCommand(0x00D0, 0, null);
        }

        private void sendTouchlinkFactoryReset()
        {
            // Transmit command
            transmitCommand(0x00D2, 0, null);
        }

        private void sendMoveToHue(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8Hue, byte u8Direction, UInt16 u16Time)
        {
            byte[] commandData = null;
            commandData = new byte[9];

            // Build command payload            
            commandData[0] = 0x02; // Address mode - short address
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = u8Hue;
            commandData[6] = u8Direction;
            commandData[7] = (byte)(u16Time >> 8);
            commandData[8] = (byte)u16Time;

            // Transmit command
            transmitCommand(0x00B0, 9, commandData);
        }

        private void sendMoveToColor(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16X, UInt16 u16Y, UInt16 u16Time)
        {
            byte[] commandData = null;
            commandData = new byte[11];

            // Build command payload            
            commandData[0] = 0x02; // Address mode - short address
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16X >> 8);
            commandData[6] = (byte)u16X;
            commandData[7] = (byte)(u16Y >> 8);
            commandData[8] = (byte)u16Y;
            commandData[9] = (byte)(u16Time >> 8);
            commandData[10] = (byte)u16Time;

            // Transmit command
            transmitCommand(0x00B7, 11, commandData);
        }

        private void sendMoveToSat(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8Sat, UInt16 u16Time)
        {
            byte[] commandData = null;
            commandData = new byte[8];

            // Build command payload            
            commandData[0] = 0x02; // Address mode - short address
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = u8Sat;
            commandData[6] = (byte)(u16Time >> 8);
            commandData[7] = (byte)u16Time;

            // Transmit command
            transmitCommand(0x00B3, 8, commandData);
        }

        private void sendIdentify(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, UInt16 u16Time)
        {
            byte[] commandData = null;
            commandData = new byte[7];

            // Build command payload     
            commandData[0] = 0x02; // Short address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = (byte)(u16Time >> 8);
            commandData[6] = (byte)u16Time;

            // Transmit command
            transmitCommand(0x0070, 7, commandData);
        }

        private void sendIdentifyQuery(UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint)
        {
            byte[] commandData = null;
            commandData = new byte[5];

            // Build command payload            
            commandData[0] = 0x02; // Short address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;

            // Transmit command
            transmitCommand(0x0071, 5, commandData);
        }

        private void sendMgmtLeaveRequest(UInt16 u16ShortAddr, UInt64 u64ExtAddr, byte u8Rejoin, byte u8DoNotRemoveChildren)
        {
            byte[] commandData = null;
            commandData = new byte[12];

            // Build command payload            
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;
            commandData[2] = (byte)(u64ExtAddr >> 56);
            commandData[3] = (byte)(u64ExtAddr >> 48);
            commandData[4] = (byte)(u64ExtAddr >> 40);
            commandData[5] = (byte)(u64ExtAddr >> 32);
            commandData[6] = (byte)(u64ExtAddr >> 24);
            commandData[7] = (byte)(u64ExtAddr >> 16);
            commandData[8] = (byte)(u64ExtAddr >> 8);
            commandData[9] = (byte)u64ExtAddr;
            commandData[10] = u8Rejoin;
            commandData[11] = u8DoNotRemoveChildren;

            // Transmit command
            transmitCommand(0x0047, 12, commandData);
        }

        private void sendRemoveRequest(UInt64 u64ParentExtAddr, UInt64 u64ChildExtAddr)
        {
            byte[] commandData = null;
            commandData = new byte[16];

            // Build command payload            
            commandData[0] = (byte)(u64ParentExtAddr >> 56);
            commandData[1] = (byte)(u64ParentExtAddr >> 48);
            commandData[2] = (byte)(u64ParentExtAddr >> 40);
            commandData[3] = (byte)(u64ParentExtAddr >> 32);
            commandData[4] = (byte)(u64ParentExtAddr >> 24);
            commandData[5] = (byte)(u64ParentExtAddr >> 16);
            commandData[6] = (byte)(u64ParentExtAddr >> 8);
            commandData[7] = (byte)u64ParentExtAddr;

            commandData[8] = (byte)(u64ChildExtAddr >> 56);
            commandData[9] = (byte)(u64ChildExtAddr >> 48);
            commandData[10] = (byte)(u64ChildExtAddr >> 40);
            commandData[11] = (byte)(u64ChildExtAddr >> 32);
            commandData[12] = (byte)(u64ChildExtAddr >> 24);
            commandData[13] = (byte)(u64ChildExtAddr >> 16);
            commandData[14] = (byte)(u64ChildExtAddr >> 8);
            commandData[15] = (byte)u64ChildExtAddr;

            // Transmit command
            transmitCommand(0x0026, 16, commandData);
        }
        private void sendLeaveRequest(UInt64 u64ExtAddr, byte u8Rejoin, byte u8DoNotRemoveChildren)
        {
            byte[] commandData = null;
            commandData = new byte[10];

            // Build command payload            
            commandData[0] = (byte)(u64ExtAddr >> 56);
            commandData[1] = (byte)(u64ExtAddr >> 48);
            commandData[2] = (byte)(u64ExtAddr >> 40);
            commandData[3] = (byte)(u64ExtAddr >> 32);
            commandData[4] = (byte)(u64ExtAddr >> 24);
            commandData[5] = (byte)(u64ExtAddr >> 16);
            commandData[6] = (byte)(u64ExtAddr >> 8);
            commandData[7] = (byte)u64ExtAddr;

            commandData[8] = u8Rejoin;
            commandData[9] = u8DoNotRemoveChildren;

            // Transmit command
            transmitCommand(0x004C, 10, commandData);
        }

        private void sendClusterMoveToLevel(byte u8AddrMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8WithOnOff, byte u8Level, UInt16 u16TransTime)
        {
            byte[] commandData = null;
            commandData = new byte[9];

            // Build command payload            
            commandData[0] = u8AddrMode;
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = u8WithOnOff;
            commandData[6] = u8Level;
            commandData[7] = (byte)(u16TransTime >> 8);
            commandData[8] = (byte)u16TransTime;

            // Transmit command
            transmitCommand(0x0081, 9, commandData);
        }

        private void complexDescriptorRequest(UInt16 u16ShortAddr)
        {
            byte[] commandData = null;
            commandData = new byte[2];

            // Build command payload            
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;

            // Transmit command
            transmitCommand(0x0531, 2, commandData);
        }

        private void userDescriptorRequest(UInt16 u16ShortAddr)
        {
            byte[] commandData = null;
            commandData = new byte[2];

            // Build command payload            
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;

            // Transmit command
            transmitCommand(0x0532, 2, commandData);
        }

        private void userDescriptorSetRequest(UInt16 u16ShortAddr, String description)
        {
            byte[] commandData = null;
            char[] au8CharArry;
            byte u8Len = 0;
            commandData = new byte[3];

            au8CharArry = description.ToCharArray();

            // Build command payload            
            commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Len++] = (byte)u16ShortAddr;
            commandData[u8Len++] = (byte)(description.Length);

            Array.Resize(ref commandData, (u8Len + description.Length));

            foreach (char u8Byte in au8CharArry)
            {
                commandData[u8Len++] = (byte)u8Byte;
            }

            // Transmit command
            transmitCommand(0x0533, u8Len, commandData);
        }

        private void sendMgmtLqiRequest(UInt16 u16ShortAddr, byte u8StartIndex)
        {
            byte[] commandData = null;
            commandData = new byte[4];

            // Build command payload            
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;
            commandData[2] = u8StartIndex;

            // Transmit command
            transmitCommand(0x004E, 3, commandData);
        }

        private void matchDescriptorRequest(UInt16 u16ShortAddr, UInt16 u16ProfileId, byte u8NbrInputClusters, UInt16[] au16InputClusters, byte u8NbrOutputClusters, UInt16[] au16OutputClusters)
        {
            byte[] commandData = null;
            commandData = new byte[128];
            byte u8Length = 0;

            // Build command payload            
            commandData[u8Length++] = (byte)(u16ShortAddr >> 8);
            commandData[u8Length++] = (byte)u16ShortAddr;
            commandData[u8Length++] = (byte)(u16ProfileId >> 8);
            commandData[u8Length++] = (byte)u16ProfileId;

            commandData[u8Length++] = u8NbrInputClusters;
            for (int i = 0; i < u8NbrInputClusters; i++)
            {
                commandData[u8Length++] = (byte)(au16InputClusters[i] >> 8);
                commandData[u8Length++] = (byte)au16InputClusters[i];
            }

            commandData[u8Length++] = u8NbrOutputClusters;
            for (int i = 0; i < u8NbrOutputClusters; i++)
            {
                commandData[u8Length++] = (byte)(au16OutputClusters[i] >> 8);
                commandData[u8Length++] = (byte)au16OutputClusters[i];
            }

            // Transmit command
            transmitCommand(0x0046, u8Length, commandData);
        }

        private void activeEndpointDescriptorRequest(UInt16 u16ShortAddr)
        {
            byte[] commandData = null;
            commandData = new byte[2];

            // Build command payload            
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;

            // Transmit command
            transmitCommand(0x0045, 2, commandData);
        }

        private void simpleDescriptorRequest(UInt16 u16ShortAddr, byte u8EndPoint)
        {
            byte[] commandData = null;
            commandData = new byte[3];

            // Build command payload
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;
            commandData[2] = u8EndPoint;

            // Transmit command
            transmitCommand(0x0043, 3, commandData);
        }

        private void powerDescriptorRequest(UInt16 u16ShortAddr)
        {
            byte[] commandData = null;
            commandData = new byte[2];

            // Build command payload
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;

            // Transmit command
            transmitCommand(0x0044, 2, commandData);
        }

        private void nodeDescriptorRequest(UInt16 u16ShortAddr)
        {
            byte[] commandData = null;
            commandData = new byte[2];

            // Build command payload            
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;

            // Transmit command
            transmitCommand(0x0042, 2, commandData);
        }

        private void sendClusterOnOff(byte addressMode, UInt16 u16ShortAddr, byte u8SrcEndPoint, byte u8DstEndPoint, byte u8CommandID)
        {
            byte[] commandData = null;
            commandData = new byte[6];

            // Build command payload
            commandData[0] = addressMode;   //address mode
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = u8SrcEndPoint;
            commandData[4] = u8DstEndPoint;
            commandData[5] = u8CommandID;

            // Transmit command
            transmitCommand(0x0092, 6, commandData);
        }

        private void sendGetIEEEAddress()
        {
            byte[] commandData = null;
            commandData = new byte[1];

            // Build command payload
            commandData[0] = 0;
            // Transmit command
            transmitCommand(0x1903, 1, commandData);
        }


        private void setPermitJoin(UInt16 u16ShortAddr, byte u8Interval, byte u8TCsignificance)
        {
            byte[] commandData = null;
            commandData = new byte[4];

            // Build command payload
            commandData[0] = (byte)(u16ShortAddr >> 8);
            commandData[1] = (byte)u16ShortAddr;
            commandData[2] = u8Interval;
            commandData[3] = u8TCsignificance;

            // Transmit command
            transmitCommand(0x0049, 4, commandData);
        }

        private void setNciCmd(byte u8NciCmdIndex)
        {
            byte[] commandData = null;
            commandData = new byte[1];

            // Build command payload
            commandData[0] = 0; // Default  - disabled
            if (u8NciCmdIndex == 0) commandData[0] = 0xA1; // Commission
            if (u8NciCmdIndex == 1) commandData[0] = 0xA0; // Decommission

            // Transmit command
            transmitCommand(0x002D, 1, commandData);
        }

        private void vSendPermitRejoinStateRequest()
        {
            // Transmit command
            transmitCommand(0x0014, 0, null);
        }

        private void vSendNetworkStateRequest()
        {
            // Transmit command
            transmitCommand(0x0009, 0, null);
        }

        private void addSecurityNetKey(byte keySeqNbr, byte[] keyData)
        {
            byte[] commandData = null;
            commandData = new byte[17];

            // Build command payload

            commandData[0] = keySeqNbr;

            for (int i = 0; i < 16; i++)
            {
                commandData[1 + i] = keyData[i];
            }

            // Transmit command
            transmitCommand(0x002B, 17, commandData);
        }

        private void switchSecurityNetKey(byte keySeqNbr)
        {
            byte[] commandData = null;
            commandData = new byte[1];

            // Build command payload

            commandData[0] = keySeqNbr;

            // Transmit command
            transmitCommand(0x002C, 1, commandData);
        }

        private void setSecurityKeyState(byte keyState, byte keySeqNbr, byte keyType, byte[] keyData)
        {
            byte[] commandData = null;
            commandData = new byte[19];

            // Build command payload
            commandData[0] = keyState;
            commandData[1] = keySeqNbr;
            commandData[2] = keyType;
            for (int i = 0; i < 16; i++)
            {
                commandData[3 + i] = keyData[i];
            }

            // Transmit command
            transmitCommand(0x0022, 19, commandData);
        }

        private void setExtendedPanID(ulong ulExtPanID)
        {
            byte[] commandData = null;
            commandData = new byte[8];

            // Build command payload
            commandData[0] = (byte)(ulExtPanID >> 56);
            commandData[1] = (byte)(ulExtPanID >> 48);
            commandData[2] = (byte)(ulExtPanID >> 40);
            commandData[3] = (byte)(ulExtPanID >> 32);
            commandData[4] = (byte)(ulExtPanID >> 24);
            commandData[5] = (byte)(ulExtPanID >> 16);
            commandData[6] = (byte)(ulExtPanID >> 8);
            commandData[7] = (byte)ulExtPanID;

            // Transmit command
            transmitCommand(0x0020, 8, commandData);
        }

        private void setChannelMask(uint uiMask)
        {
            byte[] commandData = null;
            commandData = new byte[4];

            // Build command payload
            commandData[0] = (byte)(uiMask >> 24);
            commandData[1] = (byte)(uiMask >> 16);
            commandData[2] = (byte)(uiMask >> 8);
            commandData[3] = (byte)uiMask;

            // Transmit command
            transmitCommand(0x0021, 4, commandData);
        }

        private void setDeviceType(byte deviceType)
        {
            byte[] commandData = null;
            commandData = new byte[1];

            // Build command payload
            commandData[0] = deviceType;

            // Transmit command
            transmitCommand(0x0023, 1, commandData);
        }

        private void sendIPNConfigureCommand(byte bEnabled, UInt32 u32RfActiveOutDioMask, UInt32 u32StatusOutDioMask, UInt32 u32TxConfInDioMask, byte bCallbackEnabled, UInt16 u16PollPeriod, byte u8TimerId)
        {
            byte[] commandData = null;
            commandData = new byte[17];
            byte u8Len = 17;

            // Build command payload   
            commandData[0] = bEnabled;
            commandData[1] = (byte)(u32RfActiveOutDioMask >> 24);
            commandData[2] = (byte)(u32RfActiveOutDioMask >> 16);
            commandData[3] = (byte)(u32RfActiveOutDioMask >> 8);
            commandData[4] = (byte)u32RfActiveOutDioMask;
            commandData[5] = (byte)(u32StatusOutDioMask >> 24);
            commandData[6] = (byte)(u32StatusOutDioMask >> 16);
            commandData[7] = (byte)(u32StatusOutDioMask >> 8);
            commandData[8] = (byte)u32StatusOutDioMask;
            commandData[9] = (byte)(u32TxConfInDioMask >> 24);
            commandData[10] = (byte)(u32TxConfInDioMask >> 16);
            commandData[11] = (byte)(u32TxConfInDioMask >> 8);
            commandData[12] = (byte)u32TxConfInDioMask;
            commandData[13] = bCallbackEnabled;
            commandData[14] = (byte)(u16PollPeriod >> 8);
            commandData[15] = (byte)u16PollPeriod;
            commandData[16] = u8TimerId;

            // Transmit command
            transmitCommand(0x0800, u8Len, commandData);
        }

        private void sendDioSetDirectionOutputCommand(UInt16 cmdId, UInt32 u32OutputOnDIOMask, UInt32 u32OutputOffDIOMask)
        {
            byte[] commandData = null;
            commandData = new byte[8];
            byte u8Len = 8;

            // Build command payload   
            commandData[0] = (byte)(u32OutputOnDIOMask >> 24);
            commandData[1] = (byte)(u32OutputOnDIOMask >> 16);
            commandData[2] = (byte)(u32OutputOnDIOMask >> 8);
            commandData[3] = (byte)u32OutputOnDIOMask;
            commandData[4] = (byte)(u32OutputOffDIOMask >> 24);
            commandData[5] = (byte)(u32OutputOffDIOMask >> 16);
            commandData[6] = (byte)(u32OutputOffDIOMask >> 8);
            commandData[7] = (byte)u32OutputOffDIOMask;

            // Transmit command
            transmitCommand(cmdId, u8Len, commandData);
        }

        private void sendPollControlCheckInResponseValues(UInt16 u16FastPollExpiryTime, UInt16 u16ShortAddr, byte bFastPollEnableD, byte u8SrcEndPoint, byte u8DstEndPoint)
        {
            byte[] commandData = null;
            commandData = new byte[7];
            byte u8Len = 7;

            // Build command payload   
            commandData[0] = bFastPollEnableD;
            commandData[1] = (byte)(u16ShortAddr >> 8);
            commandData[2] = (byte)u16ShortAddr;
            commandData[3] = (byte)(u16FastPollExpiryTime >> 8);
            commandData[4] = (byte)u16FastPollExpiryTime;
            commandData[5] = u8SrcEndPoint;
            commandData[6] = u8DstEndPoint;

            transmitCommand(0x0900, u8Len, commandData);
        }

        private void sendPollControlLongPollInterval(UInt16 u16LongPollInterval)
        {
            byte[] commandData = null;
            commandData = new byte[2];
            byte u8Len = 2;

            // Build command payload   

            commandData[0] = (byte)(u16LongPollInterval >> 8);
            commandData[1] = (byte)u16LongPollInterval;

            transmitCommand(0x0901, u8Len, commandData);
        }

        private void sendPollControlShortPollInterval(UInt16 u16ShortPollInterval)
        {
            byte[] commandData = null;
            commandData = new byte[2];
            byte u8Len = 2;

            // Build command payload   

            commandData[0] = (byte)(u16ShortPollInterval >> 8);
            commandData[1] = (byte)u16ShortPollInterval;

            transmitCommand(0x0902, u8Len, commandData);
        }

        private void sendInstallCodeCustom(UInt64 MACaddress, byte InstallCodeTestEnabled, ref byte[] InstallCode)
        {
            byte[] commandData = null;
            commandData = new byte[25];
            byte u8Len = 25;

            // Build command payload   
            commandData[0] = (byte)(MACaddress >> 56);
            commandData[1] = (byte)(MACaddress >> 48);
            commandData[2] = (byte)(MACaddress >> 40);
            commandData[3] = (byte)(MACaddress >> 32);
            commandData[4] = (byte)(MACaddress >> 24);
            commandData[5] = (byte)(MACaddress >> 16);
            commandData[6] = (byte)(MACaddress >> 8);
            commandData[7] = (byte)MACaddress;
            commandData[8] = (byte)InstallCodeTestEnabled;

            for (int i = 0; i < 16; i++)
            {
                commandData[i + 9] = InstallCode[i];
            }



            // Transmit command
            transmitCommand(0x002F, u8Len, commandData);

        }



        private void sendInstallCodeTest(UInt64 MACaddress, byte InstallCodeTestEnabled)
        {
            byte[] commandData = null;
            commandData = new byte[9];
            byte u8Len = 9;

            // Build command payload   
            commandData[0] = (byte)(MACaddress >> 56);
            commandData[1] = (byte)(MACaddress >> 48);
            commandData[2] = (byte)(MACaddress >> 40);
            commandData[3] = (byte)(MACaddress >> 32);
            commandData[4] = (byte)(MACaddress >> 24);
            commandData[5] = (byte)(MACaddress >> 16);
            commandData[6] = (byte)(MACaddress >> 8);
            commandData[7] = (byte)MACaddress;
            commandData[8] = (byte)InstallCodeTestEnabled;

            transmitCommand(0x0401, u8Len, commandData);

        }

        private void sendInstallCodePrint(UInt64 MACaddress)
        {
            byte[] commandData = null;
            commandData = new byte[9];
            byte u8Len = 9;

            // Build command payload   
            commandData[0] = (byte)(MACaddress >> 56);
            commandData[1] = (byte)(MACaddress >> 48);
            commandData[2] = (byte)(MACaddress >> 40);
            commandData[3] = (byte)(MACaddress >> 32);
            commandData[4] = (byte)(MACaddress >> 24);
            commandData[5] = (byte)(MACaddress >> 16);
            commandData[6] = (byte)(MACaddress >> 8);
            commandData[7] = (byte)MACaddress;

            transmitCommand(0x0402, u8Len, commandData);

        }


        private void sendAHISetTxPowerCommand(byte u8TxPower)
        {
            byte[] commandData = null;
            commandData = new byte[1];
            byte u8Len = 1;

            // Build command payload   
            commandData[0] = u8TxPower;

            transmitCommand(0x0806, u8Len, commandData);
        }

        #endregion

        #region serial transmit functions

        private void transmitCommand(int iCommand, int iLength, byte[] data)
        {
            if (serialPort1.IsOpen)
            {
                int i;
                byte[] specialCharacter = null;
                specialCharacter = new byte[1];
                byte[] message = null;
                message = new byte[256];

                // Build message payload, starting with the type field                
                message[0] = (byte)(iCommand >> 8);
                message[1] = (byte)iCommand;

                // Add message length
                message[2] = (byte)(iLength >> 8);
                message[3] = (byte)iLength;

                // Calculate checksum of header
                byte csum = 0;
                csum ^= message[0];
                csum ^= message[1];
                csum ^= message[2];
                csum ^= message[3];

                // Add message data and update checksum
                if (data != null)
                {
                    for (i = 0; i < iLength; i++)
                    {
                        message[5 + i] = data[i];
                        csum ^= data[i];
                    }
                }

                // Add checksum               
                message[4] = csum;

                // Display data byte in terminal window
                if (iCommand != 0x502)
                {
                    richTextBoxCommandResponse.Text += DateTime.Now.Hour.ToString("D2");
                    richTextBoxCommandResponse.Text += ":";
                    richTextBoxCommandResponse.Text += DateTime.Now.Minute.ToString("D2");
                    richTextBoxCommandResponse.Text += ":";
                    richTextBoxCommandResponse.Text += DateTime.Now.Second.ToString("D2");
                    richTextBoxCommandResponse.Text += ".";
                    richTextBoxCommandResponse.Text += DateTime.Now.Millisecond.ToString("D3");
                    richTextBoxCommandResponse.Text += " -> ";
                }

                // Transmit the message, send start character first
                specialCharacter[0] = 1;
                if (iCommand == 0x502)
                {
                    writeByteNoRawDisplay(specialCharacter[0]);
                }
                else
                {
                    writeByte(specialCharacter[0]);
                }

                // Transmit message payload with byte stuffing as required                
                for (i = 0; i < iLength + 5; i++)
                {
                    // Check if stuffing is required
                    if (message[i] < 0x10)
                    {
                        // First send escape character then message byte XOR'd with 0x10
                        specialCharacter[0] = 2;
                        if (iCommand == 0x502)
                        {
                            writeByteNoRawDisplay(specialCharacter[0]);
                        }
                        else
                        {
                            writeByte(specialCharacter[0]);
                        }

                        int msg = message[i];
                        msg = msg ^ 0x10;
                        message[i] = (byte)msg;

                        if (iCommand == 0x502)
                        {
                            writeByteNoRawDisplay(message[i]);
                        }
                        else
                        {
                            writeByte(message[i]);
                        }
                    }
                    else
                    {
                        // Send the character with no modification
                        if (iCommand == 0x502)
                        {
                            writeByteNoRawDisplay(message[i]);
                        }
                        else
                        {
                            writeByte(message[i]);
                        }
                    }
                }

                // Send end character
                specialCharacter[0] = 3;
                if (iCommand == 0x502)
                {
                    writeByteNoRawDisplay(specialCharacter[0]);
                }
                else
                {
                    writeByte(specialCharacter[0]);
                }
                richTextBoxCommandResponse.Text += "\n";
            }
        }

        void writeByte(byte data)
        {
            byte[] dataArray = null;
            dataArray = new byte[1];
            dataArray[0] = data;

            // Display data byte in terminal window            
            richTextBoxCommandResponse.Text += Convert.ToByte(dataArray[0]).ToString("X2");
            richTextBoxCommandResponse.Text += " ";

            // Write data byte to serial port
            serialPort1.Write(dataArray, 0, 1);
        }

        void writeByteNoRawDisplay(byte data)
        {
            byte[] dataArray = null;
            dataArray = new byte[1];
            dataArray[0] = data;

            // Write data byte to serial port
            serialPort1.Write(dataArray, 0, 1);
        }

        #endregion

        #region text operation functions

        public void Write(string path, string data, string MACaddress)
        {
            ReadToDeline(path, MACaddress);
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //start write
            sw.WriteLine(data);

            sw.Flush();

            sw.Close();
            fs.Close();
        }

        public void Read(string path)
        {

            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                richTextBoxMessageView.Text += line.ToString();
                richTextBoxMessageView.Text += "\n";

            }
            sr.Close();

        }
        public void ReadToDeline(string path, string MACaddress)
        {
            List<string> lines = new List<string>(File.ReadAllLines(path));

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Split(' ')[0] == MACaddress)
                {

                    lines.RemoveAt(i);
                }
            }
            File.WriteAllLines(path, lines.ToArray());

        }

        public bool ReadToIdentify(string path, string MACaddress)
        {
            List<string> lines = new List<string>(File.ReadAllLines(path));

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Split(' ')[0] == MACaddress)
                {
                    return true;

                }
            }

            return false;
        }


        #endregion
        #region message parser functions

        // define the delegate 
        public delegate void MessageParser();
        public delegate void MultiMessageParser(char[] output);
        public delegate void MultiInfoDisplay(int index);

        public delegate void LNTSendCommand(string command);
        public delegate void LNTGWDisplayInfo(string Info);

        public delegate void ReadAttributeThreadSendCommand(UInt16 u16ShortAddr);
        public delegate void TonggleThreadSendCommand(UInt16 u16ShortAddr);
        public delegate void BoardTonggleThreadSendCommand(UInt16 u16ShortAddr);
        public delegate void IdentifyThreadSendCommand(UInt16 u16ShortAddr);
        public delegate void LevelThreadSendCommand(UInt16 u16ShortAddr);
        public delegate void HueThreadSendCommand(UInt16 u16ShortAddr);
        public delegate void ColorThreadSendCommand(UInt16 u16ShortAddr);
        public delegate void SatThreadSendCommand(UInt16 u16ShortAddr);
        public delegate void TempThreadSendCommand(UInt16 u16ShortAddr);

        public delegate void SocketSeverReceiveMessageCommand(string output);
        public delegate void SocketRefreshCOMList(string MACAddress,string IPAddress);
        public delegate void SocketSendData(string data, Socket tempSocket);
        public delegate void SocketSeverDealData(string data);
        public delegate void SocketClearCOMCheckedList();

        // define an instance of the delegate
        MessageParser messageParser;
        MultiMessageParser multiMessageParser;
        MultiInfoDisplay multiInfoDisplay;

        LNTSendCommand lntSendCommand;
        LNTGWDisplayInfo lntGWDisplayInfo;

        SocketRefreshCOMList socketRefreshCOMList;
        SocketClearCOMCheckedList socketClearCOMCheckedList;
        SocketSendData socketSendData;
        SocketSeverDealData socketSeverDealData;

        ReadAttributeThreadSendCommand readAttributeThreadSendCommand;
        TonggleThreadSendCommand tonggleThreadSendCommand;
        BoardTonggleThreadSendCommand boardTonggleThreadSendCommand;
        IdentifyThreadSendCommand identifyThreadSendCommand;
        LevelThreadSendCommand levelThreadSendCommand;
        HueThreadSendCommand hueThreadSendCommand;
        ColorThreadSendCommand colorThreadSendCommand;
        SatThreadSendCommand satThreadSendCommand;
        TempThreadSendCommand tempThreadSendCommand;

        SocketSeverReceiveMessageCommand socketSeverReceiveMessageCommand;


        public void mylntGWDisplayInfo(string DBGInfo)
        {
            if (DBGInfo.Contains("AddressMapTable"))
            {
                listViewLNTGWINFO.Items.Clear();
                listViewLNTGWGROUPINFO.Items.Clear();
                string[] sArry;
                if (!DBGInfo.Contains("Empty"))
                {
                    
                    sArry = Regex.Split(DBGInfo, "\r\n", RegexOptions.IgnoreCase);
                    for (int i = 2; i < sArry.Length; i++)
                    {
                        //1:SAddr:a51e,ExtAddr:00158d00011db4a7
                        string[] eArry = Regex.Split(sArry[i], ",", RegexOptions.IgnoreCase);

                        //1:SAddr:a51e
                        string[] SAddr = Regex.Split(eArry[0], ":", RegexOptions.IgnoreCase);

                        //ExtAddr:00158d00011db4a7
                        string[] ExtAddr = Regex.Split(eArry[1], ":", RegexOptions.IgnoreCase);
                       
                        //INFO list
                        ListViewItem item = new ListViewItem((i-1).ToString());//index
                        item.SubItems.Add(SAddr[2]);  //NwkAddr
                        item.SubItems.Add(ExtAddr[1]);  // MACAddr 
                        item.SubItems.Add("");  // NxtHop 
                        item.SubItems.Add("");  //Channel
                        item.SubItems.Add("");   //Type                    
                        item.SubItems.Add(""); //panid
                        listViewLNTGWINFO.Items.Insert(i-2, item);

                        //GROUPINFO list
                        ListViewItem item1 = new ListViewItem((i - 1).ToString() +"."+ SAddr[2]); //index+ nwkaddr
                        item1.SubItems.Add(""); //status
                        listViewLNTGWGROUPINFO.Items.Insert(i - 2, item1);
                    }
                }
                richTextBoxMessageView.Text += DBGInfo;
            }

            if (DBGInfo.Contains("NeighbourTable"))
            {
                listViewLNTGWINFO.Items.Clear();
                listViewLNTGWGROUPINFO.Items.Clear();
                string[] sArry;
                if (!DBGInfo.Contains("Empty"))
                {
                    sArry = Regex.Split(DBGInfo, "\r\n", RegexOptions.IgnoreCase);
                    for (int i = 2; i < sArry.Length; i++)
                    {
                        string[] eArry = Regex.Split(sArry[i], ",", RegexOptions.IgnoreCase);

                        //1:ZED-SAddr:6e69
                        string[] SAddr = Regex.Split(eArry[0], ":", RegexOptions.IgnoreCase);
                        //ZED - SAddr
                        string[] type = Regex.Split(SAddr[1], "-", RegexOptions.IgnoreCase);
                       
                        //ExtAddr:0x00158d00011db654
                        string[] ExtAddr = Regex.Split(eArry[1], ":", RegexOptions.IgnoreCase);

                        //INFO list
                        ListViewItem item = new ListViewItem((i - 1).ToString());//index
                        item.SubItems.Add(SAddr[2]);  //NwkAddr
                        item.SubItems.Add(ExtAddr[1]);  // MACAddr 
                        item.SubItems.Add("");  // NxtHop 
                        item.SubItems.Add("");  //Channel
                        item.SubItems.Add(type[0]);   //Type                    
                        item.SubItems.Add(""); //panid
                        listViewLNTGWINFO.Items.Insert(i - 2, item);

                        //GROUPINFO list
                        ListViewItem item1 = new ListViewItem((i - 1).ToString() + "." + SAddr[2]); //index+ nwkaddr
                        item1.SubItems.Add(""); //status
                        listViewLNTGWGROUPINFO.Items.Insert(i - 2, item1);

                    }
                }
                richTextBoxMessageView.Text += DBGInfo;
            }



            if (DBGInfo.Contains("RoutingTable"))
            {
                listViewLNTGWINFO.Items.Clear();
                listViewLNTGWGROUPINFO.Items.Clear();
                string[] sArry;
                if (!DBGInfo.Contains("Empty"))
                {
                    sArry = Regex.Split(DBGInfo, "\r\n", RegexOptions.IgnoreCase);
                    for (int i = 2; i < sArry.Length; i++)
                    {
                        string[] eArry = Regex.Split(sArry[i], ",", RegexOptions.IgnoreCase);
                        
                        //SAddr:0c70
                        string[] SAddr = Regex.Split(eArry[1], ":", RegexOptions.IgnoreCase);

                        //Next Hop:0c70
                        string[] NxtHop = Regex.Split(eArry[2], ":", RegexOptions.IgnoreCase);
                        
                        //INFO list
                        ListViewItem item = new ListViewItem((i - 1).ToString());//index
                        item.SubItems.Add(SAddr[1]);  //NwkAddr
                        item.SubItems.Add("");  // MACAddr 
                        item.SubItems.Add(NxtHop[1]);  // NxtHop 
                        item.SubItems.Add("");  //Channel
                        item.SubItems.Add("");   //Type                    
                        item.SubItems.Add(""); //panid
                        listViewLNTGWINFO.Items.Insert(i - 2, item);

                        //GROUPINFO list
                        ListViewItem item1 = new ListViewItem((i - 1).ToString() + "." + SAddr[1]); //index+ nwkaddr
                        item1.SubItems.Add(""); //status
                        listViewLNTGWGROUPINFO.Items.Insert(i - 2, item1);


                    }
                }
                richTextBoxMessageView.Text += DBGInfo;
            }
          
        }

        public void mysocketClearCOMCheckedList()
        {
            for (int i = 0; i < listViewEZLNTINFO.Items.Count; i++)
            {
                listViewEZLNTINFO.Items[i].Checked = false;
            }
        }

        public void mysocketSeverDealData(string s)
        {
            
            if (s.Contains(" Nwk Channel"))
            {
                int i = 0;
                int j = 0;
                string[] eArray;
                string data = string.Empty;
                if (s.Contains("5169"))
                {
                    
                    i = s.IndexOf(" Nwk Channel");
                    s= s.Substring(i);
                    j = s.IndexOf("\r\n");
                    data = s.Substring(0,j+2);
                    eArray = Regex.Split(data, "\r\n", RegexOptions.IgnoreCase);
                    
                }
                else
                {
                    
                    i = s.IndexOf(" Nwk Channel");
                    s = s.Substring(i);
                    j = s.IndexOf("\n");
                    data = s.Substring(0, j + 1);
                    eArray = Regex.Split(data, "\n", RegexOptions.IgnoreCase);
                   
                }
                
                string[] sArray = Regex.Split(eArray[0], ",", RegexOptions.IgnoreCase);
                string[] chaArray = Regex.Split(sArray[0], "=", RegexOptions.IgnoreCase);
                //analyze received data  

                
                string channel = chaArray[1];

                string[] nwkArray = Regex.Split(sArray[1], "=", RegexOptions.IgnoreCase);
                string nwkAddr = nwkArray[1];

                string[] IEEEArray = Regex.Split(sArray[2], "=", RegexOptions.IgnoreCase);
                string IEEEAddr = IEEEArray[1];
                int index = (int)indexMacAddrHashTable[IEEEAddr];

                string[] verArray = Regex.Split(sArray[3], "=", RegexOptions.IgnoreCase);
                string ver = verArray[1];

                string[] typeArray = Regex.Split(sArray[4], "=", RegexOptions.IgnoreCase);
                string type = typeArray[1];

                string[] chipArray = Regex.Split(sArray[5], "=", RegexOptions.IgnoreCase);
                string chip = chipArray[1];

                string[] profileArray = Regex.Split(sArray[6], "=", RegexOptions.IgnoreCase);
                string profile = profileArray[1];

                string[] panIDArray = Regex.Split(sArray[7], "=", RegexOptions.IgnoreCase);
                string panID = panIDArray[1];


                //analyze received data end

                listUpdatemut.WaitOne();
                //update list
                listViewEZLNTINFO.BeginUpdate();
                listViewEZLNTINFO.Items[index].SubItems[1].Text = nwkAddr;
                listViewEZLNTINFO.Items[index].SubItems[2].Text = IEEEAddr;
                listViewEZLNTINFO.Items[index].SubItems[3].Text = channel;
                listViewEZLNTINFO.Items[index].SubItems[4].Text = type;
                
                listViewEZLNTINFO.Items[index].SubItems[5].Text = ver;
                listViewEZLNTINFO.Items[index].SubItems[7].Text = chip;
                listViewEZLNTINFO.Items[index].SubItems[8].Text = profile;
                listViewEZLNTINFO.Items[index].SubItems[10].Text = panID;
                listViewEZLNTINFO.EndUpdate();
                Thread.Sleep(20);

                listViewEZLNTGROUP.BeginUpdate();
                if ((nwkAddr != "") & (nwkAddr != "0xffff"))
                {
                    string str = nwkAddr.Remove(0, nwkAddr.Length - 4);

                    if (!comGroupHashTable.Contains(str))
                    {

                        comGroupHashTable.Add(str, groupIndex);
                        ListViewItem item = new ListViewItem(groupIndex.ToString() + ". " + nwkAddr);

                        item.SubItems.Add("");
                        item.SubItems.Add("Remote");

                        listViewEZLNTGROUP.Items.Insert(groupIndex, item);

                        groupIndex++;

                    }
                    groupListViewRedisplayAccordingCOM();
                
                }
                listViewEZLNTGROUP.EndUpdate();
                Thread.Sleep(20);

                listUpdatemut.ReleaseMutex();
            }
        }

        public void mysocketSendData(string data,Socket tempSocket)
        {
            Send(data,tempSocket);
        }

        public void mysocketRefreshCOMList(string MACAddress,string IPAddress)
        {
            if (indexMacAddrHashTable.Count != 0)
            {
                if (indexMacAddrHashTable.Contains(MACAddress))
                {
                    int index = (int)indexMacAddrHashTable[MACAddress];
                    listViewEZLNTINFO.Items[index].Checked = true;
                }
                else
                {
                    string backstr = MACAddress+" not found"+" in "+ IPAddress;
                    Send(backstr, clientSocket);
                }
            }
        }

        //public void mylntSendCommand(byte command)    
        //{
        //    sendCommand = command;
        //    byte[] dataArray = null;
        //    dataArray = new byte[1];
        //    dataArray[0] = sendCommand;  
        //    for (int i = 0; i < checkedCOM.Count; i++)
        //    {
        //        string COM = (string)checkedCOM[i];
        //        if (multiUSBport[((int)indexComInMultiPortHashTable[COM])].IsOpen)
        //        {
        //            multiUSBport[((int)indexComInMultiPortHashTable[COM])].Write(dataArray, 0, 1);
        //            //   Console.WriteLine("Write success");
        //        }
        //    }

        //}

        //Socket Sever Receive Message Command
        public void mySocketSeverReceiveMessageCommand(string output)
        {
            richTextBoxMessageView.Text += output;
            richTextBoxMessageView.Text += "\n";
        }

        // Received message parser
        public void myMessageParser()
        {
            // Display raw message data first 
            displayRawCommandData(rxMessageType, rxMessageLength, rxMessageChecksum, rxMessageData);

            // Display decoded message
            displayDecodedCommand(rxMessageType, rxMessageLength, rxMessageData);
        }

        // Received Multi message parser
        public void myMultiMessageParser(char[] output)
        {
            // Display raw message data first 
            //displayRawCommandData(rxMessageType, rxMessageLength, rxMessageChecksum, rxMessageData);

            // Display decoded message
            displayDecodedMultiCommand(output);

        }

        // Redisplay group ListView According COM
        public void groupListViewRedisplayAccordingCOM()
        {
            int currentIndex = 0;
            for (int i = 0; i < listViewEZLNTINFO.Items.Count; i++)
            {
                string s = listViewEZLNTINFO.Items[i].SubItems[1].Text;
                if (s != "" && s != "0xffff")
                {
                    string str = s.Remove(0, s.Length - 4);
                    if (comGroupHashTable.Contains(str))
                    {
                        string scurrent = listViewEZLNTGROUP.Items[currentIndex].SubItems[0].Text;
                        string strCurrent = scurrent.Remove(0, scurrent.Length - 4);
                    
                        if ((int)comGroupHashTable[str] != currentIndex)
                        {
                            int searchIndex = (int)comGroupHashTable[str];
                            if (searchIndex> currentIndex)
                            {
                                ListViewItem itemCurrent = new ListViewItem();
                                ListViewItem itemSearch = new ListViewItem();
                                itemCurrent = listViewEZLNTGROUP.Items[currentIndex];
                                itemSearch = listViewEZLNTGROUP.Items[searchIndex];

                                listViewEZLNTGROUP.Items[currentIndex].Remove();
                                listViewEZLNTGROUP.Items[searchIndex - 1].Remove();

                                itemSearch.SubItems[0].Text = currentIndex.ToString() + ". " + s;
                                listViewEZLNTGROUP.Items.Insert(currentIndex, itemSearch);

                                itemCurrent.SubItems[0].Text = searchIndex.ToString() + ". " + scurrent.Remove(0, scurrent.Length - 6);
                                listViewEZLNTGROUP.Items.Insert(searchIndex, itemCurrent);

                                comGroupHashTable[strCurrent] = searchIndex;
                                comGroupHashTable[str] = currentIndex;
                            }
                        }
                        currentIndex++;
                    }
                }
            }
            listViewEZLNTGROUP.Refresh();
            for (int i = 0; i < listViewEZLNTGROUP.Items.Count; i++)
            {
                if (listViewEZLNTGROUP.Items[i].Checked==false)
                {
                    listViewEZLNTGROUP.Items[i].Checked = true;
                }
            }
        }


        void refreshIndex()
        {
            for (int i = 0; i < listViewEZLNTGROUP.Items.Count; i++)
            {

                string s = listViewEZLNTGROUP.Items[i].SubItems[0].Text;
                s = s.Remove(0, s.Length - 4);
                int index = (int)comGroupHashTable[s];
                listViewEZLNTGROUP.Items[i].SubItems[0].Text = index.ToString() + "." + s; ;
            }

        }
        void deletedAddressFun()
        {
            for (int i = 0; i < checkedAddress.Count; i++)
            {
                if (comGroupHashTable.Contains(checkedAddress[i]))
                {

                    listViewEZLNTGROUP.Items.RemoveAt((int)comGroupHashTable[checkedAddress[i]]);
                    foreach (System.Collections.DictionaryEntry de in comGroupHashTable)
                    {
                        if ((int)de.Value > (int)comGroupHashTable[checkedAddress[i]])
                        {
                            modifiedAddress.Add((string)de.Key);
                        }
                    }

                    for (int j = 0; j < modifiedAddress.Count; j++)
                    {
                        string s = (string)modifiedAddress[j];
                        comGroupHashTable[s] = (int)comGroupHashTable[s] - 1;

                    }
                    modifiedAddress.Clear();
                    comGroupHashTable.Remove(checkedAddress[i]);
                    groupIndex--;

                }
            }
            refreshIndex();

        }

        //Multi Info Display
        public void myMultiInfoDisplay(int index)
        {
            
            {

                if (index == 0)
                {
                    newGroupIntem = 0;
                    ffffCount = 0;
                    sameCount = 0;
                }
                newGroupIntem++;

                listUpdatemut.WaitOne();

                listViewEZLNTINFO.BeginUpdate();
                {

                    listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[1].Text = nwkAddr[index] as string;
                    listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[2].Text = IEEEAddr[index] as string;

                    if (!indexMacAddrHashTable.Contains(IEEEAddr[index]))
                    {
                        indexMacAddrHashTable.Add(IEEEAddr[index], (int)comIndex[index]);
                    }

                    listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[3].Text = channel[index] as string;
                    listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[4].Text = type[index] as string;
                    listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[5].Text = ver[index] as string;
                    listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[7].Text = chip[index] as string;
                    listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[8].Text = profile[index] as string;
                    string IPAddress = GetIP();
                    listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[9].Text = IPAddress;
                    listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[10].Text = panID[index] as string;

                }
              
                listViewEZLNTINFO.EndUpdate();
                Thread.Sleep(20);
                listViewEZLNTGROUP.BeginUpdate();

                {
                    string s = listViewEZLNTINFO.Items[(int)comIndex[index]].SubItems[1].Text;
                    string str = s.Remove(0, s.Length - 4);
                    if (s == "0xffff")
                    {
                        ffffCount++;
                    }

                    if (comGroupHashTable.Contains(str))
                    {
                        sameCount++;
                    }

                    if ((s != "") & (s != "0xffff"))
                    {

                        newCOMIntem++;
                        if (!comGroupHashTable.Contains(str))
                        {

                            if (index < groupIndex) //not first time
                            {

                                comGroupHashTable.Add(str, groupIndex);
                                ListViewItem item = new ListViewItem((groupIndex + index).ToString() + ". " + s);

                                item.SubItems.Add("");
                                item.SubItems.Add("Local");

                                listViewEZLNTGROUP.Items.Insert(groupIndex, item);

                                groupIndex++;
                            }
                            else   //first time
                            {
                                index = index - ffffCount - sameCount;
                                comGroupHashTable.Add(str, index);
                                ListViewItem item = new ListViewItem(index.ToString() + ". " + s);

                                item.SubItems.Add("");
                                item.SubItems.Add("Local");

                                listViewEZLNTGROUP.Items.Insert(index, item);
                                groupIndex = index + 1;
                            }

                            groupListViewRedisplayAccordingCOM();
                        }

                        
                    }

                }

                listViewEZLNTGROUP.EndUpdate();
                Thread.Sleep(20);

                listUpdatemut.ReleaseMutex();

            }
            
        }

        //Read Attribute Thread Send Command
        public void myReadAttributeThreadSendCommand(UInt16 u16ShortAddr)
        {
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            UInt16 u16ClusterID = 0;
            byte u8Direction = 0;
            byte u8ManuSpecific = 0;
            UInt16 u16ManuID = 0;
            byte u8AttribCount = 1;
            UInt16 u16AttribID1 = 0;
            int iCommand = 0x0100;
            int iLength = 14;
            byte csum = 0;

            if (!LNTReadFlag)
            {
                if (bStringToUint16(textBoxEZLNTREADCLUSTERID.Text, out u16ClusterID) == true)
                {

                    // if (bStringToUint8(textBoxEZLNTATTRIBUTECOUNT.Text, out u8AttribCount) == true)
                    {
                        if (bStringToUint16(textBoxEZLNTATTRIBUTEID.Text, out u16AttribID1) == true)
                        {

                            byte[] specialCharacter = null;
                            specialCharacter = new byte[1];

                            byte[] commandData = null;
                            commandData = new byte[14];

                            byte[] commandDataheader = new byte[5];

                            byte u8Len = 0;

                            if (serialPort1.IsOpen)
                            {
                                //Build command header 
                                commandDataheader[0] = (byte)(iCommand >> 8);
                                commandDataheader[1] = (byte)(iCommand);
                                commandDataheader[2] = (byte)(iLength >> 8);
                                commandDataheader[3] = (byte)(iLength);

                                csum ^= commandDataheader[0];
                                csum ^= commandDataheader[1];
                                csum ^= commandDataheader[2];
                                csum ^= commandDataheader[3];

                                // Build command payload   
                                commandData[u8Len++] = 0x02; // Short address mode
                                commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
                                commandData[u8Len++] = (byte)u16ShortAddr;
                                commandData[u8Len++] = u8SrcEndPoint;
                                commandData[u8Len++] = u8DstEndPoint;
                                commandData[u8Len++] = (byte)(u16ClusterID >> 8);
                                commandData[u8Len++] = (byte)u16ClusterID;
                                commandData[u8Len++] = u8Direction;
                                commandData[u8Len++] = u8ManuSpecific;
                                commandData[u8Len++] = (byte)(u16ManuID >> 8);
                                commandData[u8Len++] = (byte)u16ManuID;
                                commandData[u8Len++] = u8AttribCount;
                                commandData[u8Len++] = (byte)(u16AttribID1 >> 8);
                                commandData[u8Len++] = (byte)u16AttribID1;

                                for (int i = 0; i < iLength; i++)
                                {
                                    csum ^= commandData[i];
                                }
                                commandDataheader[4] = csum;

                                // Transmit the message, send start character first
                                specialCharacter[0] = 1;
                                // Write data byte to serial port
                                serialPort1.Write(specialCharacter, 0, 1);

                                for (int i = 0; i < 5; i++)
                                {
                                    if (commandDataheader[i] < 0x10)
                                    {
                                        specialCharacter[0] = 2;
                                        serialPort1.Write(specialCharacter, 0, 1);

                                        int temp = commandDataheader[i];
                                        temp = temp ^ (0x10);
                                        commandDataheader[i] = (byte)temp;
                                    }
                                    byte[] data = new byte[1];
                                    data[0] = commandDataheader[i];
                                    serialPort1.Write(data, 0, 1);
                                }


                                for (int i = 0; i < iLength; i++)
                                {
                                    if (commandData[i] < 0x10)
                                    {
                                        specialCharacter[0] = 2;
                                        serialPort1.Write(specialCharacter, 0, 1);

                                        int temp = commandData[i];
                                        temp = temp ^ (0x10);
                                        commandData[i] = (byte)temp;
                                    }
                                    byte[] data = new byte[1];
                                    data[0] = commandData[i];
                                    serialPort1.Write(data, 0, 1);
                                }


                                // Send end character
                                specialCharacter[0] = 3;
                                // Write data byte to serial port           
                                serialPort1.Write(specialCharacter, 0, 1);
                            }
                        }
                    }
                }
            }

            else
            {
                if (bStringToUint16(textBoxLNTREADATTRCLUSTERID.Text, out u16ClusterID) == true)
                {

                    if (bStringToUint8(textBoxLNTREADATTRATTRIBUTECOUNT.Text, out u8AttribCount) == true)
                    {
                        if (bStringToUint16(textBoxLNTREADATTRATTRIBUTEID.Text, out u16AttribID1) == true)
                        {

                            byte[] specialCharacter = null;
                            specialCharacter = new byte[1];

                            byte[] commandData = null;
                            commandData = new byte[14];

                            byte[] commandDataheader = new byte[5];

                            byte u8Len = 0;

                            if (serialPort1.IsOpen)
                            {
                                //Build command header 
                                commandDataheader[0] = (byte)(iCommand >> 8);
                                commandDataheader[1] = (byte)(iCommand);
                                commandDataheader[2] = (byte)(iLength >> 8);
                                commandDataheader[3] = (byte)(iLength);

                                csum ^= commandDataheader[0];
                                csum ^= commandDataheader[1];
                                csum ^= commandDataheader[2];
                                csum ^= commandDataheader[3];

                                // Build command payload   
                                commandData[u8Len++] = 0x02; // Short address mode
                                commandData[u8Len++] = (byte)(u16ShortAddr >> 8);
                                commandData[u8Len++] = (byte)u16ShortAddr;
                                commandData[u8Len++] = u8SrcEndPoint;
                                commandData[u8Len++] = u8DstEndPoint;
                                commandData[u8Len++] = (byte)(u16ClusterID >> 8);
                                commandData[u8Len++] = (byte)u16ClusterID;
                                commandData[u8Len++] = u8Direction;
                                commandData[u8Len++] = u8ManuSpecific;
                                commandData[u8Len++] = (byte)(u16ManuID >> 8);
                                commandData[u8Len++] = (byte)u16ManuID;
                                commandData[u8Len++] = u8AttribCount;
                                commandData[u8Len++] = (byte)(u16AttribID1 >> 8);
                                commandData[u8Len++] = (byte)u16AttribID1;

                                for (int i = 0; i < iLength; i++)
                                {
                                    csum ^= commandData[i];
                                }
                                commandDataheader[4] = csum;

                                // Transmit the message, send start character first
                                specialCharacter[0] = 1;
                                // Write data byte to serial port
                                serialPort1.Write(specialCharacter, 0, 1);

                                for (int i = 0; i < 5; i++)
                                {
                                    if (commandDataheader[i] < 0x10)
                                    {
                                        specialCharacter[0] = 2;
                                        serialPort1.Write(specialCharacter, 0, 1);

                                        int temp = commandDataheader[i];
                                        temp = temp ^ (0x10);
                                        commandDataheader[i] = (byte)temp;
                                    }
                                    byte[] data = new byte[1];
                                    data[0] = commandDataheader[i];
                                    serialPort1.Write(data, 0, 1);
                                }


                                for (int i = 0; i < iLength; i++)
                                {
                                    if (commandData[i] < 0x10)
                                    {
                                        specialCharacter[0] = 2;
                                        serialPort1.Write(specialCharacter, 0, 1);

                                        int temp = commandData[i];
                                        temp = temp ^ (0x10);
                                        commandData[i] = (byte)temp;
                                    }
                                    byte[] data = new byte[1];
                                    data[0] = commandData[i];
                                    serialPort1.Write(data, 0, 1);
                                }


                                // Send end character
                                specialCharacter[0] = 3;
                                // Write data byte to serial port           
                                serialPort1.Write(specialCharacter, 0, 1);
                            }
                        }
                    }
                }
            }

        }

        public void myTonggleThreadSendCommand(UInt16 u16ShortAddr)
        {
            sendClusterOnOff(2, u16ShortAddr, 1, 1, 2);
        }


        public void myBoardTonggleThreadSendCommand(UInt16 u16ShortAddr)
        {
            sendClusterOnOff(4, u16ShortAddr, 1, 1, 2);
        }

        public void myIdentifyThreadSendCommand(UInt16 u16ShortAddr)
        {

            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            UInt16 u16Time;
            if (!LNTIdentifyFlag)
            {
                if (bStringToUint16(textBoxEZLNTIDENTIFYTIME.Text, out u16Time) == true)
                {
                    sendIdentify(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16Time);
                }
            }
            else
            {
                if (bStringToUint16(textBoxLNTIDENTIFYTIME.Text, out u16Time) == true)
                {
                    sendIdentify(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, u16Time);
                }
            }
        }

        public void myLevelThreadSendCommand(UInt16 u16ShortAddr)
        {

            UInt16 u16TransTime;
            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;
            byte u8AddrMode = 2;

            if (!LNTLevelFlag)
            {
                if (bStringToUint16(textBoxEZLNTLEVELTIME.Text, out u16TransTime) == true)
                {
                    if (comboBoxEZLNTUNICAST.SelectedIndex == 0)
                    {
                        u8AddrMode = 2;
                    }
                    else
                    {
                        u8AddrMode = 4;
                    }
                    sendClusterMoveToLevel(u8AddrMode, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, (byte)comboBoxEZLNTLEVELWITHONOFF.SelectedIndex, currentLevel, u16TransTime);
                }
            }
            else
            {
                if (bStringToUint16(textBoxLNTLEVELTIME.Text, out u16TransTime) == true)
                {
                    if (comboBoxEZLNTUNICAST.SelectedIndex == 0)     //need to modify
                    {
                        u8AddrMode = 2;
                    }
                    else
                    {
                        u8AddrMode = 4;
                    }
                    sendClusterMoveToLevel(u8AddrMode, u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, (byte)comboBoxLEVELMOVEWITHONOFF.SelectedIndex, currentLevel, u16TransTime);
                }
            }

        }

        public void myHueThreadSendCommand(UInt16 u16ShortAddr)
        {

            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;

            byte u8Direction;
            UInt16 u16Time;

            if (!LNTHueFlag)
            {
                if (bStringToUint8(textBoxEZLNTHUEDIR.Text, out u8Direction) == true)
                {
                    if (bStringToUint16(textBoxEZLNTHUETIME.Text, out u16Time) == true)
                    {
                        sendMoveToHue(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, currentHue, u8Direction, u16Time);
                    }
                }
            }
            else
            {
                if (bStringToUint8(textBoxLNTHUEDIR.Text, out u8Direction) == true)
                {
                    if (bStringToUint16(textBoxLNTHUETIME.Text, out u16Time) == true)
                    {
                        sendMoveToHue(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, currentHue, u8Direction, u16Time);
                    }
                }
            }

        }

        public void mySatThreadSendCommand(UInt16 u16ShortAddr)
        {

            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;

            UInt16 u16Time;

            if (!LNTSatFlag)
            {
                if (bStringToUint16(textBoxLNTSATTIME.Text, out u16Time) == true)
                {
                    sendMoveToSat(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, currentSat, u16Time);
                }
            }
            else
            {
                if (bStringToUint16(textBoxEZLNTSATTIME.Text, out u16Time) == true)
                {
                    sendMoveToSat(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, currentSat, u16Time);
                }
            }

        }

        public void myTempThreadSendCommand(UInt16 u16ShortAddr)
        {

            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;

            UInt16 u16Time;

            if (!LNTTempFlag)
            {
                if (bStringToUint16(textBoxEZLNTTEMPTIME.Text, out u16Time) == true)
                {
                    sendMoveToColorTemp(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, currentTemp, u16Time);
                }
            }
            else
            {
                if (bStringToUint16(textBoxLNTTEMPTIME.Text, out u16Time) == true)
                {
                    sendMoveToColorTemp(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, currentTemp, u16Time);
                }
            }


        }

        public void myColorThreadSendCommand(UInt16 u16ShortAddr)
        {

            byte u8SrcEndPoint = 1;
            byte u8DstEndPoint = 1;

            UInt16 u16Time;
            if (!LNTColorFlag)
            {

                if (bStringToUint16(textBoxCOLORTIME.Text, out u16Time) == true)
                {
                    sendMoveToColor(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, currentX, currentY, u16Time);
                }
            }
            else
            {
                if (bStringToUint16(textBoxLNTCOLORTIME.Text, out u16Time) == true)
                {
                    sendMoveToColor(u16ShortAddr, u8SrcEndPoint, u8DstEndPoint, currentX, currentY, u16Time);
                }
            }
        }



        private void displayDecodedMultiCommand(char[] output)
        {
            string s = new string(output);
            richTextBoxMessageView.Text += s;
        }

        private void displayDecodedCommand(UInt16 u16Type, UInt16 u16Length, byte[] au8Data)
        {
            if ((checkBoxDebug.Checked == true) || (u16Type != 0x8011 && u16Type != 0x8012))
            {
                richTextBoxMessageView.Text += "Type: 0x";
                richTextBoxMessageView.Text += u16Type.ToString("X4");
                richTextBoxMessageView.Text += "\n";
            }

            switch (u16Type)
            {
                case 0x8000:
                    {
                        richTextBoxMessageView.Text += " (Status)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Length: " + u16Length.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[0].ToString("X2");

                        switch (au8Data[0])
                        {
                            case 0:
                                {
                                    richTextBoxMessageView.Text += " (Success)";
                                }
                                break;

                            case 1:
                                {
                                    richTextBoxMessageView.Text += " (Incorrect Parameters)";
                                }
                                break;

                            case 2:
                                {
                                    richTextBoxMessageView.Text += " (Unhandled Command)";
                                }
                                break;

                            case 3:
                                {
                                    richTextBoxMessageView.Text += " (Command Failed)";
                                }
                                break;

                            case 4:
                                {
                                    richTextBoxMessageView.Text += " (Busy)";
                                }
                                break;

                            case 5:
                                {
                                    richTextBoxMessageView.Text += " (Stack Already Started)";
                                }
                                break;

                            default:
                                {
                                    richTextBoxMessageView.Text += " (ZigBee Error Code)";
                                }
                                break;
                        }

                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[1].ToString("X2");

                        if (u16Length > 2)
                        {
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Message: ";
                            string errorMessage = System.Text.Encoding.Default.GetString(au8Data);
                            richTextBoxMessageView.Text += errorMessage.Substring(2, (u16Length - 2));
                        }
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8011:
                    {
                        if (checkBoxDebug.Checked == true)
                        {
                            UInt16 u16ProfileID = 0;
                            UInt16 u16ClusterID = 0;

                            u16ProfileID = au8Data[4];
                            u16ProfileID <<= 8;
                            u16ProfileID |= au8Data[5];

                            u16ClusterID = au8Data[6];
                            u16ClusterID <<= 8;
                            u16ClusterID |= au8Data[7];

                            richTextBoxMessageView.Text += " (APS Data ACK)";
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "   Status: 0x" + au8Data[0].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "   SQN: 0x" + au8Data[1].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "   Source EndPoint: 0x" + au8Data[2].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "   Destination EndPoint: 0x" + au8Data[3].ToString("X2");
                            richTextBoxMessageView.Text += "\n   ";
                            displayProfileId(u16ProfileID);
                            richTextBoxMessageView.Text += "\n   ";
                            displayClusterId(u16ClusterID);
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8012:
                    {
                        if (checkBoxDebug.Checked == true)
                        {
                            richTextBoxMessageView.Text += "  (APS Data Confirm)";
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "   Status: 0x" + au8Data[0].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "   SQN: 0x" + au8Data[1].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "   Source EndPoint: 0x" + au8Data[2].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "   Destination EndPoint: 0x" + au8Data[3].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8001:
                    {

                        {
                            richTextBoxMessageView.Text += " (Log)";
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Level: 0x" + au8Data[0].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Message: ";
                        }
                        string logMessage = System.Text.Encoding.Default.GetString(au8Data);
                        richTextBoxMessageView.Text += logMessage.Substring(1, (u16Length - 1));

                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8002:
                    {
                        UInt16 u16ProfileID = 0;
                        UInt16 u16ClusterID = 0;

                        u16ProfileID = au8Data[1];
                        u16ProfileID <<= 8;
                        u16ProfileID |= au8Data[2];

                        u16ClusterID = au8Data[3];
                        u16ClusterID <<= 8;
                        u16ClusterID |= au8Data[4];

                        richTextBoxMessageView.Text += " (Data Indication)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayProfileId(u16ProfileID);
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterID);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Source EndPoint: 0x" + au8Data[5].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Destination EndPoint: 0x" + au8Data[6].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Source Address Mode: 0x" + au8Data[7].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Source Address: ";

                        byte nextIndex = 0;

                        if (au8Data[9] == 0)
                        {
                            //0x00 = DstAddress and DstEndpoint not present                        
                            richTextBoxMessageView.Text += "Not Present";
                            richTextBoxMessageView.Text += "\n";

                            nextIndex = 10;
                        }
                        else if (au8Data[9] == 1)
                        {
                            UInt16 u16GroupAddr = 0;

                            u16GroupAddr = au8Data[10];
                            u16GroupAddr <<= 8;
                            u16GroupAddr |= au8Data[11];

                            //0x01 = 16-bit group address for DstAddress; DstEndpoint not present
                            richTextBoxMessageView.Text += u16GroupAddr.ToString("X4");
                            richTextBoxMessageView.Text += "\n";

                            nextIndex = 12;
                        }
                        else if (au8Data[9] == 2)
                        {
                            UInt16 u16DstAddress = 0;
                            UInt16 u16DstEndPoint1 = 0;

                            u16DstAddress = au8Data[10];
                            u16DstAddress <<= 8;
                            u16DstAddress |= au8Data[11];

                            u16DstEndPoint1 = au8Data[12];
                            u16DstEndPoint1 <<= 8;
                            u16DstEndPoint1 |= au8Data[13];

                            //0x02 = 16-bit address for DstAddress and DstEndpoint present
                            richTextBoxMessageView.Text += u16DstAddress.ToString("X4");
                            richTextBoxMessageView.Text += "  EndPoint: 0x" + u16DstEndPoint1.ToString("X4");
                            richTextBoxMessageView.Text += "\n";

                            nextIndex = 14;
                        }
                        else if (au8Data[9] == 3)
                        {
                            //0x03 = 64-bit extended address for DstAddress and DstEndpoint present
                        }
                        else
                        {
                            //0x04 - 0xff = reserved
                            nextIndex = 10;
                            richTextBoxMessageView.Text += "Not Valid";
                            richTextBoxMessageView.Text += "\n";
                        }

                        richTextBoxMessageView.Text += "  Destination Address Mode: 0x" + au8Data[nextIndex].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8003:
                    {
                        UInt16 u16Entries = 0;
                        UInt16 u16ProfileID = 0;

                        u16ProfileID = au8Data[1];
                        u16ProfileID <<= 8;
                        u16ProfileID |= au8Data[2];

                        u16Entries = (UInt16)((u16Length - 3) / 2);

                        richTextBoxMessageView.Text += " (Cluster List - Entries: ";
                        richTextBoxMessageView.Text += u16Entries.ToString();
                        richTextBoxMessageView.Text += ")\n";
                        richTextBoxMessageView.Text += "  Source EndPoint: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayProfileId(u16ProfileID);

                        for (int i = 3; i < u16Length; i += 2)
                        {
                            UInt16 u16ClusterID;

                            u16ClusterID = au8Data[i];
                            u16ClusterID <<= 8;
                            u16ClusterID |= au8Data[i + 1];

                            displayClusterId(u16ClusterID);
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8004:
                    {
                        UInt16 u16Entries = 0;
                        UInt16 u16ProfileID = 0;
                        UInt16 u16ClusterID = 0;

                        u16ProfileID = au8Data[1];
                        u16ProfileID <<= 8;
                        u16ProfileID |= au8Data[2];

                        u16ClusterID = au8Data[3];
                        u16ClusterID <<= 8;
                        u16ClusterID |= au8Data[4];

                        u16Entries = (UInt16)((u16Length - 5) / 2);

                        richTextBoxMessageView.Text += " (Cluster Attributes - Entries: ";
                        richTextBoxMessageView.Text += u16Entries.ToString();
                        richTextBoxMessageView.Text += ")\n";
                        richTextBoxMessageView.Text += " Source EndPoint: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayProfileId(u16ProfileID);
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterID);
                        richTextBoxMessageView.Text += "\n";

                        for (int i = 5; i < u16Length; i += 2)
                        {
                            UInt16 u16AttributeID = 0;

                            u16AttributeID = au8Data[i];
                            u16AttributeID <<= 8;
                            u16AttributeID |= au8Data[i + 1];

                            richTextBoxMessageView.Text += " Attribute ID: 0x" + u16AttributeID.ToString("X4");
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8005:
                    {
                        UInt16 u16Entries = 0;
                        UInt16 u16ProfileID = 0;
                        UInt16 u16ClusterID = 0;

                        u16ProfileID = au8Data[1];
                        u16ProfileID <<= 8;
                        u16ProfileID |= au8Data[2];

                        u16ClusterID = au8Data[3];
                        u16ClusterID <<= 8;
                        u16ClusterID |= au8Data[4];

                        u16Entries = (UInt16)(u16Length - 5);

                        richTextBoxMessageView.Text += " (Command IDs - Entries: ";
                        richTextBoxMessageView.Text += u16Entries.ToString();
                        richTextBoxMessageView.Text += ")\n";
                        richTextBoxMessageView.Text += " Source EndPoint: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayProfileId(u16ProfileID);
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterID);
                        richTextBoxMessageView.Text += "\n";

                        for (int i = 5; i < u16Length; i++)
                        {
                            richTextBoxMessageView.Text += " Command ID: 0x" + au8Data[i].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8009:
                    {

                        UInt16 u16PanId = 0;
                        UInt16 u16ShortAddr = 0;
                        UInt64 u64ExtendedPANID = 0;
                        UInt64 u64ExtendedAddr = 0;

                        u16ShortAddr = au8Data[0];
                        u16ShortAddr <<= 8;
                        u16ShortAddr |= au8Data[1];

                        for (int i = 0; i < 8; i++)
                        {
                            u64ExtendedAddr <<= 8;
                            u64ExtendedAddr |= au8Data[2 + i];
                        }

                        u16PanId = au8Data[10];
                        u16PanId <<= 8;
                        u16PanId |= au8Data[11];

                        for (int i = 0; i < 8; i++)
                        {
                            u64ExtendedPANID <<= 8;
                            u64ExtendedPANID |= au8Data[12 + i];
                        }

                        richTextBoxMessageView.Text += " (Network State Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: 0x" + u16ShortAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Extended Address: 0x" + u64ExtendedAddr.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  PAN ID: " + u16PanId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Ext PAN ID: 0x" + u64ExtendedPANID.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Channel: " + au8Data[20].ToString();
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8010:
                    {
                        UInt16 u16Major = 0;
                        UInt16 u16Installer = 0;

                        u16Major = au8Data[0];
                        u16Major <<= 8;
                        u16Major |= au8Data[1];

                        u16Installer = au8Data[2];
                        u16Installer <<= 8;
                        u16Installer |= au8Data[3];

                        richTextBoxMessageView.Text += " (Version)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Length: " + u16Length.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Application: " + u16Major.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SDK: " + u16Installer.ToString();
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8024:
                    {
                        UInt16 u16ShortAddr = 0;
                        UInt64 u64ExtAddr = 0;

                        u16ShortAddr = au8Data[1];
                        u16ShortAddr <<= 8;
                        u16ShortAddr |= au8Data[2];

                        for (int i = 0; i < 8; i++)
                        {
                            u64ExtAddr <<= 8;
                            u64ExtAddr |= au8Data[3 + i];
                        }

                        richTextBoxMessageView.Text += " (Network Up)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: 0x" + u16ShortAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Extended Address: 0x" + u64ExtAddr.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Channel: " + au8Data[11].ToString();
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8014:
                    {
                        richTextBoxMessageView.Text += " (Permit Join State)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "Permit Join: " + (au8Data[0] == 1 ? "TRUE" : "FALSE");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8015:
                    {
                        UInt16 u16PanId = 0;
                        UInt16 u16ShortAddr = 0;
                        UInt16 u16SuperframeSpec = 0;
                        UInt32 u32TimeStamp = 0;
                        UInt64 u64ExtendedPANID = 0;

                        u16PanId = au8Data[1];
                        u16PanId <<= 8;
                        u16PanId |= au8Data[2];

                        u16ShortAddr = au8Data[3];
                        u16ShortAddr <<= 8;
                        u16ShortAddr |= au8Data[4];

                        u16SuperframeSpec = au8Data[11];
                        u16SuperframeSpec <<= 8;
                        u16SuperframeSpec |= au8Data[12];

                        for (int i = 0; i < 4; i++)
                        {
                            u32TimeStamp <<= 8;
                            u32TimeStamp |= au8Data[13 + i];
                        }

                        for (int i = 0; i < 8; i++)
                        {
                            u64ExtendedPANID <<= 8;
                            u64ExtendedPANID |= au8Data[17 + i];
                        }

                        richTextBoxMessageView.Text += " (Discovery Only Scan Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Address Mode: " + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  PAN ID: " + u16PanId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: 0x" + u16ShortAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Channel: " + au8Data[5].ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  GTS Permit: " + au8Data[6].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Link Quality: " + au8Data[7];
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Security Use: " + au8Data[8].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  ACL Entry: " + au8Data[9].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Security Failure: " + au8Data[10].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Superframe Specification: " + u16SuperframeSpec.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Time Stamp: " + u32TimeStamp.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Ext PAN ID: 0x" + u64ExtendedPANID.ToString("X8");

                    }
                    break;

                case 0x8029:
                    {
                        UInt64 u64AddrData = 0;
                        UInt64 u64Key = 0;
                        UInt64 u64HostAddrData = 0;
                        UInt64 u64ExtPANID = 0;
                        UInt32 u32Mic = 0;
                        UInt16 u16PANID = 0;
                        UInt16 u16ShortAddr = 0;
                        UInt16 u16DeviceId = 0;

                        for (int i = 0; i < 8; i++)
                        {
                            u64AddrData <<= 8;
                            u64AddrData |= au8Data[0 + i];
                        }

                        for (int i = 0; i < 16; i++)
                        {
                            u64Key <<= 8;
                            u64Key |= au8Data[8 + i];
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            u32Mic <<= 8;
                            u32Mic |= au8Data[24 + i];
                        }

                        for (int i = 0; i < 8; i++)
                        {
                            u64HostAddrData <<= 8;
                            u64HostAddrData |= au8Data[28 + i];
                        }

                        u16PANID = au8Data[38];
                        u16PANID <<= 8;
                        u16PANID |= au8Data[39];

                        for (int i = 0; i < 8; i++)
                        {
                            u64ExtPANID <<= 8;
                            u64ExtPANID |= au8Data[40 + i];
                        }

                        u16ShortAddr = au8Data[48];
                        u16ShortAddr <<= 8;
                        u16ShortAddr |= au8Data[49];

                        u16DeviceId = au8Data[50];
                        u16DeviceId <<= 8;
                        u16DeviceId |= au8Data[51];

                        richTextBoxMessageView.Text += " (Encrypted Data)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[52].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Device Extended Address: " + u64AddrData.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Key: " + u64Key.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Mic: " + u32Mic.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Host Extended Address: " + u64HostAddrData.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Active Key Sequence Number: " + au8Data[36].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Channel: " + au8Data[37].ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  PAN ID: " + u16PANID.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Extended PAN ID: " + u64ExtPANID.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: " + u16ShortAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Device ID: " + u16DeviceId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";

                    }
                    break;

                case 0x802F:
                    {
                        UInt64 u64AddrData = 0;
                        UInt64 u64KeyFirstPart = 0;
                        UInt64 u64KeySecondPart = 0;

                        if (au8Data[0] == 0)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                u64AddrData <<= 8;
                                u64AddrData |= au8Data[1 + i];
                            }

                            for (int i = 0; i < 8; i++)
                            {
                                u64KeyFirstPart <<= 8;
                                u64KeyFirstPart |= au8Data[9 + i];
                            }

                            for (int i = 0; i < 8; i++)
                            {
                                u64KeySecondPart <<= 8;
                                u64KeySecondPart |= au8Data[17 + i];
                            }


                            richTextBoxMessageView.Text += " (Install Code Data Response)";
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Status: 0x" + au8Data[0].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Device Extended Address: " + u64AddrData.ToString("X8");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Key: " + u64KeyFirstPart.ToString("X8") + u64KeySecondPart.ToString("X8");
                            richTextBoxMessageView.Text += "\n";

                        }
                        else
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                u64AddrData <<= 8;
                                u64AddrData |= au8Data[1 + i];
                            }

                            richTextBoxMessageView.Text += " (Install Code Data Response)";
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Device Extended Address: " + u64AddrData.ToString("X8");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Status: Insufficient space";
                            richTextBoxMessageView.Text += "\n";
                        }


                    }
                    break;

                // NciCmdNotify
                case 0x802E:
                    {
                        UInt16 u16DeviceId = 0;
                        UInt64 u64ExtAddr = 0;

                        u16DeviceId = au8Data[1];
                        u16DeviceId <<= 8;
                        u16DeviceId |= au8Data[2];

                        for (int i = 0; i < 8; i++)
                        {
                            u64ExtAddr <<= 8;
                            u64ExtAddr |= au8Data[3 + i];
                        }

                        richTextBoxMessageView.Text += " (NCI Command Notify)";
                        richTextBoxMessageView.Text += "\n";
                        if (au8Data[0] == 0xA1)
                        {
                            richTextBoxMessageView.Text += "  Command: Commission";
                        }
                        else if (au8Data[0] == 0xA0)
                        {
                            richTextBoxMessageView.Text += "  Command: Decommission";
                        }
                        else
                        {
                            richTextBoxMessageView.Text += "  Command: Unknown";
                        }
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Device ID: 0x" + u16DeviceId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Extended Address: 0x" + u64ExtAddr.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8030:
                    {
                        richTextBoxMessageView.Text += " (Bind Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8031:
                    {
                        richTextBoxMessageView.Text += " (UnBind Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8041:
                    {
                        UInt64 u64ExtAddr = 0;
                        UInt16 u16ShortAddr = 0;

                        for (int i = 0; i < 8; i++)
                        {
                            u64ExtAddr <<= 8;
                            u64ExtAddr |= au8Data[2 + i];
                        }

                        u16ShortAddr = au8Data[10];
                        u16ShortAddr <<= 8;
                        u16ShortAddr |= au8Data[11];

                        richTextBoxMessageView.Text += " (IEEE Address Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Extended Address: 0x" + u64ExtAddr.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: 0x" + u16ShortAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";

                        if (u16Length > 14)
                        {
                            richTextBoxMessageView.Text += "  Associated End Devices: " + au8Data[12].ToString();
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8042:
                    {
                        UInt16 u16ShortAddr = 0;
                        UInt16 u16ManufacturerCode = 0;
                        UInt16 u16RxSize = 0;
                        UInt16 u16TxSize = 0;
                        UInt16 u16ServerMask = 0;
                        UInt16 u16BitFields = 0;
                        byte u8DescriptorCapability = 0;
                        byte u8MacCapability = 0;
                        byte u8MaxBufferSize = 0;

                        u16ShortAddr = au8Data[2];
                        u16ShortAddr <<= 8;
                        u16ShortAddr |= au8Data[3];

                        u16ManufacturerCode = au8Data[4];
                        u16ManufacturerCode <<= 8;
                        u16ManufacturerCode |= au8Data[5];

                        u16RxSize = au8Data[6];
                        u16RxSize <<= 8;
                        u16RxSize |= au8Data[7];

                        u16TxSize = au8Data[8];
                        u16TxSize <<= 8;
                        u16TxSize |= au8Data[9];

                        u16ServerMask = au8Data[10];
                        u16ServerMask <<= 8;
                        u16ServerMask |= au8Data[11];

                        u8DescriptorCapability = au8Data[12];
                        u8MacCapability = au8Data[13];
                        u8MaxBufferSize = au8Data[14];

                        u16BitFields = au8Data[15];
                        u16BitFields <<= 8;
                        u16BitFields |= au8Data[16];

                        richTextBoxMessageView.Text += " (Node Descriptor Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: 0x" + u16ShortAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Manufacturer Code: 0x" + u16ManufacturerCode.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Max Rx Size: " + u16RxSize.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Max Tx Size: " + u16TxSize.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Server Mask: 0x" + u16ServerMask.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        displayDescriptorCapability(u8DescriptorCapability);
                        displayMACcapability(u8MacCapability);
                        richTextBoxMessageView.Text += "  Max Buffer Size: " + u8MaxBufferSize.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Bit Fields: 0x" + u16BitFields.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8043:
                    {
                        UInt16 u16ShortAddr = 0;
                        byte u8Length = 0;

                        u16ShortAddr = au8Data[2];
                        u16ShortAddr <<= 8;
                        u16ShortAddr |= au8Data[3];
                        u8Length = au8Data[4];

                        richTextBoxMessageView.Text += " (Simple Descriptor Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: 0x" + u16ShortAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Length: " + u8Length.ToString("");
                        richTextBoxMessageView.Text += "\n";

                        if (u8Length > 0)
                        {
                            byte u8InputClusterCount = 0;
                            UInt16 u16ProfileId = 0;
                            UInt16 u16DeviceId = 0;

                            u16ProfileId = au8Data[6];
                            u16ProfileId <<= 8;
                            u16ProfileId |= au8Data[7];
                            u16DeviceId = au8Data[8];
                            u16DeviceId <<= 8;
                            u16DeviceId |= au8Data[9];
                            u8InputClusterCount = au8Data[11];

                            richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[5].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            displayProfileId(u16ProfileId);
                            richTextBoxMessageView.Text += "\n";
                            displayDeviceId(u16DeviceId);
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Input Cluster Count: " + u8InputClusterCount.ToString();
                            richTextBoxMessageView.Text += "\n";

                            UInt16 u16Index = 12;
                            for (int i = 0; i < u8InputClusterCount; i++)
                            {
                                UInt16 u16ClusterId = 0;

                                u16ClusterId = au8Data[(i * 2) + 12];
                                u16ClusterId <<= 8;
                                u16ClusterId |= au8Data[(i * 2) + 13];

                                richTextBoxMessageView.Text += "    Cluster " + i.ToString();
                                richTextBoxMessageView.Text += ":";
                                displayClusterId(u16ClusterId);
                                richTextBoxMessageView.Text += "\n";
                                u16Index += 2;
                            }

                            byte u8OutputClusterCount = au8Data[u16Index];
                            u16Index++;

                            richTextBoxMessageView.Text += "  Output Cluster Count: " + u8OutputClusterCount.ToString();
                            richTextBoxMessageView.Text += "\n";

                            for (int i = 0; i < u8OutputClusterCount; i++)
                            {
                                UInt16 u16ClusterId = 0;

                                u16ClusterId = au8Data[u16Index];
                                u16ClusterId <<= 8;
                                u16ClusterId |= au8Data[u16Index + 1];

                                richTextBoxMessageView.Text += "    Cluster " + i.ToString();
                                richTextBoxMessageView.Text += ":";
                                displayClusterId(u16ClusterId);
                                richTextBoxMessageView.Text += "\n";
                                u16Index += 2;
                            }
                        }
                    }
                    break;
                /*
            case 0x8044:
            {
                richTextBoxMessageView.Text += " (Power Descriptor Response)";
                richTextBoxMessageView.Text += "\n";
                richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                richTextBoxMessageView.Text += "\n";
                richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                richTextBoxMessageView.Text += "\n";
            }
            break;
                */
                case 0x8045:
                    {
                        UInt16 u16ShortAddr = 0;

                        u16ShortAddr = au8Data[2];
                        u16ShortAddr <<= 8;
                        u16ShortAddr |= au8Data[3];

                        richTextBoxMessageView.Text += " (Active Endpoints Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: 0x" + u16ShortAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Endpoint Count: " + au8Data[4].ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Endpoint List: ";
                        richTextBoxMessageView.Text += "\n";

                        for (int i = 0; i < au8Data[4]; i++)
                        {
                            richTextBoxMessageView.Text += "    Endpoint " + i.ToString();
                            richTextBoxMessageView.Text += ": ";
                            richTextBoxMessageView.Text += "0x" + au8Data[i + 5].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8047:
                    {
                        richTextBoxMessageView.Text += " (Leave Confirmation)";
                        richTextBoxMessageView.Text += "\n";

                        if (u16Length == 2)
                        {
                            richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                        }

                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";

                        if (u16Length == 9)
                        {
                            UInt64 u64ExtAddr = 0;

                            for (int i = 0; i < 8; i++)
                            {
                                u64ExtAddr <<= 8;
                                u64ExtAddr |= au8Data[1 + i];
                            }

                            richTextBoxMessageView.Text += "  Extended Address: 0x" + u64ExtAddr.ToString("X8");
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8048:
                    {
                        UInt64 u64ExtAddr = 0;

                        for (int i = 0; i < 8; i++)
                        {
                            u64ExtAddr <<= 8;
                            u64ExtAddr |= au8Data[i];
                        }

                        richTextBoxMessageView.Text += " (Leave Indication)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Extended Address: 0x" + u64ExtAddr.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Rejoin Status: 0x" + au8Data[8].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x804A:
                    {
                        byte u8ScannedChannelsListCount;
                        UInt16 u16TotalTx = 0;
                        UInt16 u16TxFailures = 0;
                        UInt32 u32ScannedChannels = 0;

                        u16TotalTx = au8Data[2];
                        u16TotalTx <<= 8;
                        u16TotalTx |= au8Data[3];

                        u16TxFailures = au8Data[4];
                        u16TxFailures <<= 8;
                        u16TxFailures |= au8Data[5];

                        u32ScannedChannels = au8Data[6];
                        u32ScannedChannels <<= 8;
                        u32ScannedChannels |= au8Data[7];
                        u32ScannedChannels <<= 8;
                        u32ScannedChannels |= au8Data[8];
                        u32ScannedChannels <<= 8;
                        u32ScannedChannels |= au8Data[9];

                        u8ScannedChannelsListCount = au8Data[10];

                        richTextBoxMessageView.Text += " (Mgmt Nwk Update Notify)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Total Tx: " + u16TotalTx.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Tx Failures: " + u16TxFailures.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Scanned Channels: 0x" + u32ScannedChannels.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Scanned Channels List Count: " + u8ScannedChannelsListCount.ToString();
                        richTextBoxMessageView.Text += "\n";

                        for (int x = 0; x < u8ScannedChannelsListCount; x++)
                        {
                            richTextBoxMessageView.Text += "  Value " + x.ToString();
                            richTextBoxMessageView.Text += ":  0x" + au8Data[11 + x].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x804E:
                    {
                        byte u8NbTableEntries = 0;
                        byte u8StartIx = 0;
                        byte u8NbTableListCount = 0;

                        UInt16[] au16NwkAddr = new UInt16[16];

                        u8NbTableEntries = au8Data[2];
                        u8NbTableListCount = au8Data[3];
                        u8StartIx = au8Data[4];

                        richTextBoxMessageView.Text += " (Mgmt LQI Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Nb Table Entries: " + u8NbTableEntries.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Start Index: " + u8StartIx.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Nb Table List Count: " + u8NbTableListCount.ToString();
                        richTextBoxMessageView.Text += "\n";

                        comboBoxAddressList.Items.Clear();

                        if (u8NbTableListCount > 0)
                        {
                            byte i;
                            UInt64 u64PanID = 0;
                            UInt64 u64ExtAddr = 0;
                            UInt16 u16NwkAddr = 0;
                            byte u8Lqi = 0;
                            byte u8Depth = 0;
                            byte u8Flags = 0;
                            byte u8PayloadIndex = 5;

                            for (i = 0; i < u8NbTableListCount; i++)
                            {
                                u16NwkAddr = 0;
                                for (int x = 0; x < 2; x++, u8PayloadIndex++)
                                {
                                    u16NwkAddr <<= 8;
                                    u16NwkAddr |= au8Data[u8PayloadIndex];
                                }

                                u64PanID = 0;
                                for (int x = 0; x < 8; x++, u8PayloadIndex++)
                                {
                                    u64PanID <<= 8;
                                    u64PanID |= au8Data[u8PayloadIndex];
                                }

                                u64ExtAddr = 0;
                                for (int x = 0; x < 8; x++, u8PayloadIndex++)
                                {
                                    u64ExtAddr <<= 8;
                                    u64ExtAddr |= au8Data[u8PayloadIndex];
                                }

                                au16NwkAddr[i] = u16NwkAddr;

                                au64ExtAddr[i] = u64ExtAddr;

                                u8Depth = au8Data[u8PayloadIndex++];
                                u8Lqi = au8Data[u8PayloadIndex++];
                                u8Flags = au8Data[u8PayloadIndex++];

                                richTextBoxMessageView.Text += "  Neighbor " + i.ToString();
                                richTextBoxMessageView.Text += ":";
                                richTextBoxMessageView.Text += "\n";
                                richTextBoxMessageView.Text += "    Extended Pan ID: 0x" + u64PanID.ToString("X8");
                                richTextBoxMessageView.Text += "\n";
                                richTextBoxMessageView.Text += "    Extended Address: 0x" + u64ExtAddr.ToString("X8");
                                richTextBoxMessageView.Text += "\n";
                                richTextBoxMessageView.Text += "    Nwk Address: 0x" + u16NwkAddr.ToString("X4");
                                richTextBoxMessageView.Text += "\n";
                                richTextBoxMessageView.Text += "    LQI: " + u8Lqi.ToString();
                                richTextBoxMessageView.Text += "\n";
                                richTextBoxMessageView.Text += "    Depth: " + u8Depth.ToString();
                                richTextBoxMessageView.Text += "\n";
                                richTextBoxMessageView.Text += "    Flags: 0x" + u8Flags.ToString("X2");
                                richTextBoxMessageView.Text += "\n";

                                byte u8DeviceType = (byte)(u8Flags & 0x03);
                                richTextBoxMessageView.Text += "    Device Type: ";

                                if (u8DeviceType == 0)
                                {
                                    richTextBoxMessageView.Text += "Coordinator";
                                }
                                else if (u8DeviceType == 1)
                                {
                                    richTextBoxMessageView.Text += "Router";
                                }
                                else if (u8DeviceType == 2)
                                {
                                    richTextBoxMessageView.Text += "End Device";
                                }
                                else
                                {
                                    richTextBoxMessageView.Text += "Unknown";
                                }
                                richTextBoxMessageView.Text += "\n";

                                byte u8PermitJoin = (byte)((u8Flags & 0x0C) >> 2);
                                richTextBoxMessageView.Text += "    Permit Joining: ";

                                if (u8PermitJoin == 0)
                                {
                                    richTextBoxMessageView.Text += "Off";
                                }
                                else if (u8PermitJoin == 1)
                                {
                                    richTextBoxMessageView.Text += "On";
                                }
                                else
                                {
                                    richTextBoxMessageView.Text += "Unknown";
                                }
                                richTextBoxMessageView.Text += "\n";

                                byte u8Relationship = (byte)((u8Flags & 0x30) >> 4);
                                richTextBoxMessageView.Text += "    Relationship: ";

                                if (u8Relationship == 0)
                                {
                                    richTextBoxMessageView.Text += "Parent";
                                }
                                else if (u8Relationship == 1)
                                {
                                    richTextBoxMessageView.Text += "Child";
                                }
                                else if (u8Relationship == 2)
                                {
                                    richTextBoxMessageView.Text += "Sibling";
                                }
                                else if (u8Relationship == 4)
                                {
                                    richTextBoxMessageView.Text += "Previous Child";
                                }
                                else
                                {
                                    richTextBoxMessageView.Text += "Unknown";
                                }
                                richTextBoxMessageView.Text += "\n";

                                byte u8RxOnWhenIdle = (byte)((u8Flags & 0xC0) >> 6);
                                richTextBoxMessageView.Text += "    RxOnWhenIdle: ";

                                if (u8RxOnWhenIdle == 0)
                                {
                                    richTextBoxMessageView.Text += "No";
                                }
                                else if (u8RxOnWhenIdle == 1)
                                {
                                    richTextBoxMessageView.Text += "Yes";
                                }
                                else
                                {
                                    richTextBoxMessageView.Text += "Unknown";
                                }
                                richTextBoxMessageView.Text += "\n";
                            }
                            for (i = 0; i < u8NbTableListCount; i++)
                            {
                                comboBoxAddressList.Items.Add(au16NwkAddr[i].ToString("X4"));
                            }
                        }
                    }
                    break;

                case 0x8050:
                    {
                        UInt16 u16TableSize;
                        UInt16 u16TableEntries;

                        u16TableSize = au8Data[1];
                        u16TableSize <<= 8;
                        u16TableSize |= au8Data[2];

                        u16TableEntries = au8Data[3];
                        u16TableEntries <<= 8;
                        u16TableEntries |= au8Data[4];

                        richTextBoxMessageView.Text += " (Addr Map Table Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Table Size: " + u16TableSize.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Entries: " + u16TableEntries.ToString();
                        richTextBoxMessageView.Text += "\n";

                        byte i;
                        for (i = 0; i < u16TableEntries; i++)
                        {
                            UInt16 u16Addr;
                            UInt64 u64Addr;

                            u16Addr = au8Data[5 + (i * 8)];
                            u16Addr <<= 8;
                            u16Addr |= au8Data[6 + (i * 8)];

                            u64Addr = au8Data[7 + (i * 8)];
                            u64Addr <<= 8;
                            u64Addr |= au8Data[8 + (i * 8)];
                            u64Addr <<= 8;
                            u64Addr |= au8Data[9 + (i * 8)];
                            u64Addr <<= 8;
                            u64Addr |= au8Data[10 + (i * 8)];
                            u64Addr <<= 8;
                            u64Addr |= au8Data[11 + (i * 8)];
                            u64Addr <<= 8;
                            u64Addr |= au8Data[12 + (i * 8)];
                            u64Addr <<= 8;
                            u64Addr |= au8Data[13 + (i * 8)];
                            u64Addr <<= 8;
                            u64Addr |= au8Data[14 + (i * 8)];

                            richTextBoxMessageView.Text += "  Entry " + i.ToString();
                            richTextBoxMessageView.Text += ": 0x" + u16Addr.ToString("X4");
                            richTextBoxMessageView.Text += " 0x" + u64Addr.ToString("X8");

                            richTextBoxMessageView.Text += "\n";
                        }

                    }
                    break;

                case 0x8060:
                    {
                        UInt16 u16ClusterId = 0;
                        UInt16 u16GroupId = 0;


                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        u16GroupId = au8Data[5];
                        u16GroupId <<= 8;
                        u16GroupId |= au8Data[6];



                        richTextBoxMessageView.Text += " (Add Group Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";

                        richTextBoxMessageView.Text += "  Group: 0x" + u16GroupId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        if (pageIndex == 19)
                        {
                            if (listViewEZLNTGROUP.CheckedItems.Count != 0)
                            {
                                UInt16 u16nwkAddr = 0;
                                u16nwkAddr = au8Data[7];
                                u16nwkAddr <<= 8;
                                u16nwkAddr |= au8Data[8];
                                string s = u16nwkAddr.ToString("x").PadLeft(4, '0');
                                int index = (int)comGroupHashTable[s];
                                if (au8Data[4] == 0)
                                {
                                    listViewEZLNTGROUP.Items[index].SubItems[1].Text = "add group success";
                                }
                                else
                                {
                                    if (au8Data[4] == 0x8A)
                                    {
                                        listViewEZLNTGROUP.Items[index].SubItems[1].Text = "add group failed status:" + au8Data[4].ToString("X2") + "(Duplicate exist)";
                                    }
                                }
                            }
                        }
                        else if (pageIndex == 20)
                        {
                            if (listViewLNTGROUPINFO.CheckedItems.Count != 0)
                            {
                                UInt16 u16nwkAddr = 0;
                                u16nwkAddr = au8Data[7];
                                u16nwkAddr <<= 8;
                                u16nwkAddr |= au8Data[8];
                                string s = u16nwkAddr.ToString("x").PadLeft(4, '0');
                                int index = (int)comGroupHashTable2[s];
                                if (au8Data[4] == 0)
                                {
                                    listViewLNTGROUPINFO.Items[index].SubItems[1].Text = "add group success";
                                }
                                else
                                {
                                    if (au8Data[4] == 0x8A)
                                    {
                                        listViewLNTGROUPINFO.Items[index].SubItems[1].Text = "add group failed status:" + au8Data[4].ToString("X2") + "(Duplicate exist)";
                                    }
                                }
                            }
                        }
                    }
                    break;

                case 0x8061:
                    {
                        UInt16 u16ClusterId = 0;
                        UInt16 u16GroupId = 0;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        u16GroupId = au8Data[5];
                        u16GroupId <<= 8;
                        u16GroupId |= au8Data[6];

                        richTextBoxMessageView.Text += " (View Group Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Group: 0x" + u16GroupId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";

                        if (pageIndex == 19)
                        {
                            if (listViewEZLNTGROUP.CheckedItems.Count != 0)
                            {
                                UInt16 u16nwkAddr = 0;
                                u16nwkAddr = au8Data[7];
                                u16nwkAddr <<= 8;
                                u16nwkAddr |= au8Data[8];
                                string s = u16nwkAddr.ToString("x").PadLeft(4, '0');
                                int index = (int)comGroupHashTable[s];
                                if (au8Data[4] == 0)
                                {
                                    listViewEZLNTGROUP.Items[index].SubItems[1].Text = "view group success";
                                }
                                else
                                {
                                    if (au8Data[4] == 0x8B)
                                    {
                                        listViewEZLNTGROUP.Items[index].SubItems[1].Text = "view group failed status:" + au8Data[4].ToString("X2") + "(Not found)";
                                    }
                                }
                            }
                        }
                        else if (pageIndex == 20)
                        {
                            if (listViewLNTGROUPINFO.CheckedItems.Count != 0)
                            {
                                UInt16 u16nwkAddr = 0;
                                u16nwkAddr = au8Data[7];
                                u16nwkAddr <<= 8;
                                u16nwkAddr |= au8Data[8];
                                string s = u16nwkAddr.ToString("x").PadLeft(4, '0');
                                int index = (int)comGroupHashTable2[s];
                                if (au8Data[4] == 0)
                                {
                                    listViewLNTGROUPINFO.Items[index].SubItems[1].Text = "view group success";
                                }
                                else
                                {
                                    if (au8Data[4] == 0x8B)
                                    {
                                        listViewLNTGROUPINFO.Items[index].SubItems[1].Text = "view group failed status:" + au8Data[4].ToString("X2") + "(Not found)";
                                    }
                                }
                            }
                        }

                    }
                    break;

                case 0x8062:
                    {
                        UInt16 u16ClusterId = 0;
                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        richTextBoxMessageView.Text += " (Get Group Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Capacity: " + au8Data[4].ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Count: " + au8Data[5].ToString();
                        richTextBoxMessageView.Text += "\n";

                        byte i;
                        for (i = 0; i < au8Data[5]; i++)
                        {
                            UInt16 u16GroupId;

                            u16GroupId = au8Data[6 + (i * 2)];
                            u16GroupId <<= 8;
                            u16GroupId |= au8Data[7 + (i * 2)];

                            richTextBoxMessageView.Text += "  Group " + i.ToString();
                            richTextBoxMessageView.Text += ": 0x" + u16GroupId.ToString("X4");
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8063:
                    {
                        UInt16 u16ClusterId = 0;
                        UInt16 u16GroupId = 0;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        u16GroupId = au8Data[5];
                        u16GroupId <<= 8;
                        u16GroupId |= au8Data[6];

                        richTextBoxMessageView.Text += " (Remove Group Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Group: 0x" + u16GroupId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";

                        if (pageIndex == 19)
                        {
                            if (listViewEZLNTGROUP.CheckedItems.Count != 0)
                            {
                                UInt16 u16nwkAddr = 0;
                                u16nwkAddr = au8Data[7];
                                u16nwkAddr <<= 8;
                                u16nwkAddr |= au8Data[8];
                                string s = u16nwkAddr.ToString("x").PadLeft(4, '0');
                                int index = (int)comGroupHashTable[s];
                                if (au8Data[4] == 0)
                                {
                                    listViewEZLNTGROUP.Items[index].SubItems[1].Text = "remove group success";
                                }
                                else
                                {
                                    if (au8Data[4] == 0x8B)
                                    {
                                        listViewEZLNTGROUP.Items[index].SubItems[1].Text = "remove group failed status:" + au8Data[4].ToString("X2") + "(Not found)";
                                    }
                                }
                            }
                        }
                        else if (pageIndex == 20)
                        {
                            if (listViewLNTGROUPINFO.CheckedItems.Count != 0)
                            {
                                UInt16 u16nwkAddr = 0;
                                u16nwkAddr = au8Data[7];
                                u16nwkAddr <<= 8;
                                u16nwkAddr |= au8Data[8];
                                string s = u16nwkAddr.ToString("x").PadLeft(4, '0');
                                int index = (int)comGroupHashTable2[s];
                                if (au8Data[4] == 0)
                                {
                                    listViewLNTGROUPINFO.Items[index].SubItems[1].Text = "remove group success";
                                }
                                else
                                {
                                    if (au8Data[4] == 0x8B)
                                    {
                                        listViewLNTGROUPINFO.Items[index].SubItems[1].Text = "remove group failed status:" + au8Data[4].ToString("X2") + "(Not found)";
                                    }
                                }
                            }
                        }
                    }
                    break;

                case 0x807A:
                    {
                        richTextBoxMessageView.Text += " (Identify Local Active)";
                        richTextBoxMessageView.Text += "\n";
                        if (au8Data[0] == 1)
                        {
                            richTextBoxMessageView.Text += "  Status: Start Identifying";
                            richTextBoxMessageView.Text += "\n";
                        }
                        else if (au8Data[0] != 1)
                        {
                            richTextBoxMessageView.Text += "  Status: Stop Identifying";
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8095:
                    {
                        UInt16 u16ClusterId = 0;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        richTextBoxMessageView.Text += " (On/Off Update)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Addr Mode: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";

                        if (au8Data[4] == 0x03)
                        {
                        }
                        else
                        {
                            UInt16 u16Addr = 0;

                            u16Addr = au8Data[5];
                            u16Addr <<= 8;
                            u16Addr |= au8Data[6];

                            richTextBoxMessageView.Text += "  Src Addr Mode: 0x" + u16Addr.ToString("X4");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Status: 0x" + au8Data[7].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x80A0:
                    {
                        UInt16 u16ClusterId = 0, u16GroupId = 0, u16TransTime = 0, u16SceneLength = 0;
                        byte u8Status;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        u8Status = au8Data[4];

                        u16GroupId = au8Data[5];
                        u16GroupId <<= 8;
                        u16GroupId |= au8Data[6];


                        richTextBoxMessageView.Text += " (View Scene)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Cluster ID: 0x" + u16ClusterId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + u8Status.ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Group ID: 0x" + u16GroupId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Scene Id: 0x" + au8Data[7].ToString("X2");

                        if (0 == u8Status)
                        {
                            u16TransTime = au8Data[8];
                            u16TransTime <<= 8;
                            u16TransTime |= au8Data[9];

                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Transition Time: 0x" + u16TransTime.ToString("X4");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Scene Name Length: 0x" + au8Data[10].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Scene Name Max Length: 0x" + au8Data[11].ToString("X2");
                            richTextBoxMessageView.Text += "\n";

                            richTextBoxMessageView.Text += "  Scene Name: ";

                            byte i = 0;
                            for (i = 0; i < au8Data[10]; i++)
                            {
                                richTextBoxMessageView.Text += Convert.ToChar(au8Data[12 + i]);
                            }

                            u16SceneLength = au8Data[12 + i];
                            u16SceneLength <<= 8;
                            u16SceneLength |= au8Data[13 + i];

                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Ext Scene Length: 0x" + u16SceneLength.ToString("X4");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Ext Max Length: 0x" + au8Data[14 + i].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Scene Data: ";
                            richTextBoxMessageView.Text += "\n      ";

                            for (byte c = 0; i < u16SceneLength; i++)
                            {
                                richTextBoxMessageView.Text += "0x" + au8Data[15 + i + c].ToString("X2") + " ";
                            }
                        }

                    }
                    break;

                case 0x80A3:
                    {
                        UInt16 u16ClusterId, u16GroupId;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        u16GroupId = au8Data[5];
                        u16GroupId <<= 8;
                        u16GroupId |= au8Data[6];

                        richTextBoxMessageView.Text += " (Remove All Scenes)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Cluster ID: 0x" + u16ClusterId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Group ID: 0x" + u16GroupId.ToString("X4");
                    }
                    break;

                case 0x80A2:
                    {
                        UInt16 u16ClusterId, u16GroupId;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        u16GroupId = au8Data[5];
                        u16GroupId <<= 8;
                        u16GroupId |= au8Data[6];

                        richTextBoxMessageView.Text += " (Remove Scene)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Cluster ID: 0x" + u16ClusterId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Group ID: 0x" + u16GroupId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Scene ID: 0x" + au8Data[7].ToString("X2");
                    }
                    break;

                case 0x8100: // Read attribute response
                    {
                        UInt16 u16ClusterId = 0;
                        UInt16 u16AttribId = 0;
                        UInt16 u16SrcAddr = 0;
                        UInt16 u16AttributeSize = 0;

                        u16SrcAddr = au8Data[1];
                        u16SrcAddr <<= 8;
                        u16SrcAddr |= au8Data[2];

                        u16ClusterId = au8Data[4];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[5];

                        u16AttribId = au8Data[6];
                        u16AttribId <<= 8;
                        u16AttribId |= au8Data[7];

                        u16AttributeSize = au8Data[10];
                        u16AttributeSize <<= 8;
                        u16AttributeSize |= au8Data[11];

                        richTextBoxMessageView.Text += " (Read Attrib Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Addr: 0x" + u16SrcAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[3].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[8].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        displayAttribute(u16AttribId, au8Data[9], au8Data, 12, u16AttributeSize);
                    }
                    break;

                case 0x8101:
                    {
                        UInt16 u16ClusterId = 0;
                        UInt16 u16DstAddr = 0;

                        u16ClusterId = au8Data[13];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[14];

                        u16DstAddr = au8Data[1];
                        u16DstAddr <<= 8;
                        u16DstAddr |= au8Data[2];

                        richTextBoxMessageView.Text += " (Default Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: 0x" + u16DstAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Source EndPoint: 0x" + au8Data[11].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Destination EndPoint: 0x" + au8Data[12].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Command: 0x" + au8Data[15].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[16].ToString("X2");
                        richTextBoxMessageView.Text += "\n";

                        if (pageIndex == 19)
                        {
                            if (listViewEZLNTGROUP.CheckedItems.Count != 0)
                            {
                                if (removeAllGroup)
                                {
                                    UInt16 u16nwkAddr = u16DstAddr;
                                    string s = u16nwkAddr.ToString("x").PadLeft(4, '0');
                                    int index = (int)comGroupHashTable[s];
                                    if (au8Data[16] == 0)
                                    {
                                        listViewEZLNTGROUP.Items[index].SubItems[1].Text = "remove all group success";
                                    }
                                    else
                                    {
                                        listViewEZLNTGROUP.Items[index].SubItems[1].Text = "remove all group failed status:" + au8Data[16].ToString("X2");
                                    }
                                    removeAllGroup = false;
                                }
                            }
                        }
                        else if (pageIndex == 20)
                        {
                            if (listViewLNTGROUPINFO.CheckedItems.Count != 0)
                            {
                                if (removeAllGroup)
                                {
                                    UInt16 u16nwkAddr = u16DstAddr;
                                    string s = u16nwkAddr.ToString("x").PadLeft(4, '0');
                                    int index = (int)comGroupHashTable2[s];
                                    if (au8Data[16] == 0)
                                    {
                                        listViewLNTGROUPINFO.Items[index].SubItems[1].Text = "remove all group success";
                                    }
                                    else
                                    {
                                        listViewLNTGROUPINFO.Items[index].SubItems[1].Text = "remove all group failed status:" + au8Data[16].ToString("X2");
                                    }
                                    removeAllGroup = false;
                                }
                            }
                        }
                    }
                    break;

                case 0x8120:
                    {
                        UInt16 u16ClusterId = 0;
                        UInt16 u16SrcAddr = 0;

                        u16ClusterId = au8Data[4];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[5];

                        u16SrcAddr = au8Data[1];
                        u16SrcAddr <<= 8;
                        u16SrcAddr |= au8Data[2];

                        richTextBoxMessageView.Text += " (Report Config Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Addr: 0x" + u16SrcAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[3].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[6].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8102:
                    {
                        UInt16 u16SrcAddr = 0;
                        UInt16 u16ClusterId = 0;
                        UInt16 u16AttribId = 0;
                        UInt16 u16AttributeSize = 0;

                        u16SrcAddr = au8Data[1];
                        u16SrcAddr <<= 8;
                        u16SrcAddr |= au8Data[2];

                        u16ClusterId = au8Data[4];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[5];

                        u16AttribId = au8Data[6];
                        u16AttribId <<= 8;
                        u16AttribId |= au8Data[7];

                        u16AttributeSize = au8Data[10];
                        u16AttributeSize <<= 8;
                        u16AttributeSize |= au8Data[11];

                        richTextBoxMessageView.Text += " (Attribute Report)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Addr: 0x" + u16SrcAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Ep: 0x" + au8Data[3].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        displayAttribute(u16AttribId, au8Data[9], au8Data, 12, u16AttributeSize);
                    }
                    break;

                case 0x8122:
                    {
                        UInt16 u16SrcAddr = 0;
                        UInt16 u16ClusterId = 0;
                        UInt16 u16AttribId = 0;
                        UInt16 u16MaxInterval = 0;
                        UInt16 u16MinInterval = 0;

                        u16SrcAddr = au8Data[1];
                        u16SrcAddr <<= 8;
                        u16SrcAddr |= au8Data[2];

                        u16ClusterId = au8Data[4];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[5];

                        u16AttribId = au8Data[8];
                        u16AttribId <<= 8;
                        u16AttribId |= au8Data[9];

                        u16MaxInterval = au8Data[10];
                        u16MaxInterval <<= 8;
                        u16MaxInterval |= au8Data[11];

                        u16MinInterval = au8Data[12];
                        u16MinInterval <<= 8;
                        u16MinInterval |= au8Data[13];

                        richTextBoxMessageView.Text += " (Attribute Config Report)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Addr: 0x" + u16SrcAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Ep: 0x" + au8Data[3].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[6].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayDataType(au8Data[7]);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Attribute: 0x" + u16AttribId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Min Interval: " + u16MinInterval.ToString();
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Max Interval: " + u16MaxInterval.ToString();
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8103: // Read local attribute response
                    {
                        UInt16 u16ClusterId = 0;
                        UInt16 u16AttribId = 0;
                        UInt16 u16SrcAddr = 0;
                        UInt16 u16AttributeSize = 0;

                        u16SrcAddr = au8Data[1];
                        u16SrcAddr <<= 8;
                        u16SrcAddr |= au8Data[2];

                        u16ClusterId = au8Data[4];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[5];

                        u16AttribId = au8Data[6];
                        u16AttribId <<= 8;
                        u16AttribId |= au8Data[7];

                        u16AttributeSize = au8Data[10];
                        u16AttributeSize <<= 8;
                        u16AttributeSize |= au8Data[11];

                        richTextBoxMessageView.Text += " (Read Local Attrib Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        //richTextBoxMessageView.Text += "  Src Addr: 0x" + u16SrcAddr.ToString("X4");
                        //richTextBoxMessageView.Text += "\n";                    
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[3].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[8].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        displayAttribute(u16AttribId, au8Data[9], au8Data, 12, u16AttributeSize);
                    }
                    break;

                case 0x8140: // Discover attribute response
                    {
                        UInt16 u16AttribId = 0;

                        u16AttribId = au8Data[2];
                        u16AttribId <<= 8;
                        u16AttribId |= au8Data[3];

                        richTextBoxMessageView.Text += " (Discover Attrib Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Complete: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayDataType(au8Data[1]);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Attribute: 0x" + u16AttribId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8141: // Discover extended attribute response
                    {
                        UInt16 u16AttribId = 0;

                        u16AttribId = au8Data[2];
                        u16AttribId <<= 8;
                        u16AttribId |= au8Data[3];

                        richTextBoxMessageView.Text += " (Discover Attrib Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Complete: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayDataType(au8Data[1]);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Attribute: 0x" + u16AttribId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Flags: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8150: // Discover command received individual response
                    {
                        richTextBoxMessageView.Text += " (Discover Command Received Individual Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Command: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Index: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8151: // Discover command received response
                    {
                        richTextBoxMessageView.Text += " (Discover Command Received Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Complete: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Commands: " + au8Data[1].ToString();
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8160: // Discover command generated individual response
                    {
                        richTextBoxMessageView.Text += " (Discover Command Generated Individual Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Command: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Index: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8161: // Discover command generated response
                    {
                        richTextBoxMessageView.Text += " (Discover Command Generated Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Complete: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Commands: " + au8Data[1].ToString();
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8401:
                    {
                        UInt16 u16ClusterId = 0;
                        UInt16 u16ZoneStatus = 0;
                        UInt16 u16Delay = 0;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        richTextBoxMessageView.Text += " (IAS Zone Status Change)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Addr Mode: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";

                        if (au8Data[4] == 0x03)
                        {
                        }
                        else
                        {
                            UInt16 u16Addr = 0;

                            u16Addr = au8Data[5];
                            u16Addr <<= 8;
                            u16Addr |= au8Data[6];

                            richTextBoxMessageView.Text += "  Src Addr Mode: 0x" + u16Addr.ToString("X4");
                            richTextBoxMessageView.Text += "\n";
                        }

                        u16ZoneStatus = au8Data[7];
                        u16ZoneStatus <<= 8;
                        u16ZoneStatus |= au8Data[8];

                        richTextBoxMessageView.Text += "  Zone Status: 0x" + u16ZoneStatus.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Ext Status: 0x" + au8Data[9].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Zone ID: 0x" + au8Data[10].ToString("X2");
                        richTextBoxMessageView.Text += "\n";

                        u16Delay = au8Data[11];
                        u16Delay <<= 8;
                        u16Delay |= au8Data[12];

                        richTextBoxMessageView.Text += "  Delay: 0x" + u16Delay.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x004D:
                    {
                        UInt16 u16ShortAddr = 0;
                        UInt64 u64ExtAddr = 0;

                        u16ShortAddr = au8Data[0];
                        u16ShortAddr <<= 8;
                        u16ShortAddr |= au8Data[1];

                        for (int i = 0; i < 8; i++)
                        {
                            u64ExtAddr <<= 8;
                            u64ExtAddr |= au8Data[2 + i];
                        }

                        richTextBoxMessageView.Text += " (End Device Announce)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Short Address: 0x" + u16ShortAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Extended Address: 0x" + u64ExtAddr.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        displayMACcapability(au8Data[10]);
                    }
                    break;

                case 0x8501:
                    {
                        byte u8Offset = 0;
                        byte u8SQN;
                        byte u8SrcEndpoint;
                        UInt16 u16ClusterId;
                        UInt16 u16SrcAddr;
                        byte u8SrcAddrMode;
                        UInt64 u64RequestNodeAddress;
                        UInt32 u32FileOffset;
                        UInt32 u32FileVersion;
                        UInt16 u16ImageType;
                        UInt16 u16ManufactureCode;
                        UInt16 u16BlockRequestDelay;
                        byte u8MaxDataSize;
                        byte u8FieldControl;

                        u8SQN = au8Data[u8Offset++];

                        u8SrcEndpoint = au8Data[u8Offset++];

                        u16ClusterId = au8Data[u8Offset++];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[u8Offset++];

                        u8SrcAddrMode = au8Data[u8Offset++];

                        u16SrcAddr = au8Data[u8Offset++];
                        u16SrcAddr <<= 8;
                        u16SrcAddr |= au8Data[u8Offset++];

                        u64RequestNodeAddress = au8Data[u8Offset++];
                        u64RequestNodeAddress <<= 8;
                        u64RequestNodeAddress |= au8Data[u8Offset++];
                        u64RequestNodeAddress <<= 8;
                        u64RequestNodeAddress |= au8Data[u8Offset++];
                        u64RequestNodeAddress <<= 8;
                        u64RequestNodeAddress |= au8Data[u8Offset++];
                        u64RequestNodeAddress <<= 8;
                        u64RequestNodeAddress |= au8Data[u8Offset++];
                        u64RequestNodeAddress <<= 8;
                        u64RequestNodeAddress |= au8Data[u8Offset++];
                        u64RequestNodeAddress <<= 8;
                        u64RequestNodeAddress |= au8Data[u8Offset++];
                        u64RequestNodeAddress <<= 8;
                        u64RequestNodeAddress |= au8Data[u8Offset++];

                        u32FileOffset = au8Data[u8Offset++];
                        u32FileOffset <<= 8;
                        u32FileOffset |= au8Data[u8Offset++];
                        u32FileOffset <<= 8;
                        u32FileOffset |= au8Data[u8Offset++];
                        u32FileOffset <<= 8;
                        u32FileOffset |= au8Data[u8Offset++];

                        u32FileVersion = au8Data[u8Offset++];
                        u32FileVersion <<= 8;
                        u32FileVersion |= au8Data[u8Offset++];
                        u32FileVersion <<= 8;
                        u32FileVersion |= au8Data[u8Offset++];
                        u32FileVersion <<= 8;
                        u32FileVersion |= au8Data[u8Offset++];

                        u16ImageType = au8Data[u8Offset++];
                        u16ImageType <<= 8;
                        u16ImageType |= au8Data[u8Offset++];

                        u16ManufactureCode = au8Data[u8Offset++];
                        u16ManufactureCode <<= 8;
                        u16ManufactureCode |= au8Data[u8Offset++];

                        u16BlockRequestDelay = au8Data[u8Offset++];
                        u16BlockRequestDelay <<= 8;
                        u16BlockRequestDelay |= au8Data[u8Offset++];

                        u8MaxDataSize = au8Data[u8Offset++];

                        u8FieldControl = au8Data[u8Offset++];

                        /*
                        richTextBoxMessageView.Text += " (OTA Block Request)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + u8SQN.ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                         */

                        richTextBoxMessageView.Text = "";
                        richTextBoxCommandResponse.Text = "";


                        /*
                        richTextBoxMessageView.Text += "  Src Addr Mode: 0x" + u8SrcAddrMode.ToString("X2");
                        richTextBoxMessageView.Text += "\n";                    
                        richTextBoxMessageView.Text += "  Src Addr: 0x" + u16SrcAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + u8SrcEndpoint.ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";

                        if ((u8FieldControl & 0x01) == 0x01)
                        {
                            richTextBoxMessageView.Text += "  Node Addr: 0x" + u64RequestNodeAddress.ToString("X16");
                            richTextBoxMessageView.Text += "\n";
                        }

                        richTextBoxMessageView.Text += "  File Offset: 0x" + u32FileOffset.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  File Version: 0x" + u32FileVersion.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Image Type: 0x" + u16ImageType.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Manu Code: 0x" + u16ManufactureCode.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Block Delay: 0x" + u16BlockRequestDelay.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Max Data Size: 0x" + u8MaxDataSize.ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Field Control: 0x" + u8FieldControl.ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        */
                        // Send response 
                        if (u8OTAWaitForDataParamsPending == 0)
                        {
                            byte u8NbrBytes = 0;

                            if ((u32FileOffset + u8MaxDataSize) > u32OtaFileTotalImage)
                            {
                                u8NbrBytes = (byte)(u32OtaFileTotalImage - u32FileOffset);
                            }
                            else
                            {
                                u8NbrBytes = u8MaxDataSize;
                            }
                            sendOtaBlock(u8SrcAddrMode, u16SrcAddr, 1, u8SrcEndpoint, u8SQN, 0, u32FileOffset, u32FileVersion, u16ImageType, u16ManufactureCode, u8NbrBytes, au8OTAFile);
                        }
                        else
                        {
                            sendOtaSetWaitForDataParams(u8SrcAddrMode, u16SrcAddr, 1, u8SrcEndpoint, u8SQN, 0x97, u32OTAWaitForDataParamsCurrentTime, u32OTAWaitForDataParamsRequestTime, u16OTAWaitForDataParamsBlockDelay);
                            u8OTAWaitForDataParamsPending = 0;
                        }

                        if (u8OtaInProgress == 0)
                        {
                            u8OtaInProgress = 1;
                            textBoxOtaDownloadStatus.Text = "In Progress";
                            progressBarOtaDownloadProgress.Value = 0;
                            textBoxOtaFileOffset.Text = "0";
                        }
                        else
                        {
                            UInt32 u32PercentComplete = (u32FileOffset * 1000) / u32OtaFileTotalImage;
                            progressBarOtaDownloadProgress.Value = (int)u32PercentComplete;
                            textBoxOtaFileOffset.Text = u32FileOffset.ToString();
                        }
                    }
                    break;
                case 0x8503:
                    {

                        byte u8Offset = 0;
                        byte u8SQN;
                        byte u8SrcEndpoint;
                        UInt16 u16ClusterId;
                        UInt16 u16SrcAddr;
                        byte u8SrcAddrMode;
                        UInt32 u32FileVersion;
                        UInt16 u16ImageType;
                        UInt16 u16ManufactureCode;
                        byte u8Status;

                        u8SQN = au8Data[u8Offset++];

                        u8SrcEndpoint = au8Data[u8Offset++];

                        u16ClusterId = au8Data[u8Offset++];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[u8Offset++];

                        u8SrcAddrMode = au8Data[u8Offset++];

                        u16SrcAddr = au8Data[u8Offset++];
                        u16SrcAddr <<= 8;
                        u16SrcAddr |= au8Data[u8Offset++];

                        u32FileVersion = au8Data[u8Offset++];
                        u32FileVersion <<= 8;
                        u32FileVersion |= au8Data[u8Offset++];
                        u32FileVersion <<= 8;
                        u32FileVersion |= au8Data[u8Offset++];
                        u32FileVersion <<= 8;
                        u32FileVersion |= au8Data[u8Offset++];

                        u16ImageType = au8Data[u8Offset++];
                        u16ImageType <<= 8;
                        u16ImageType |= au8Data[u8Offset++];

                        u16ManufactureCode = au8Data[u8Offset++];
                        u16ManufactureCode <<= 8;
                        u16ManufactureCode |= au8Data[u8Offset++];

                        u8Status = au8Data[u8Offset++];

                        richTextBoxMessageView.Text += " (OTA End Request)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + u8SQN.ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Addr Mode: 0x" + u8SrcAddrMode.ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Addr: 0x" + u16SrcAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  EndPoint: 0x" + u8SrcEndpoint.ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  File Version: 0x" + u32FileVersion.ToString("X8");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Image Type: 0x" + u16ImageType.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Manu Code: 0x" + u16ManufactureCode.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + u8Status.ToString("X2");
                        richTextBoxMessageView.Text += "\n";

                        //sendOtaEndResponse(u8SrcAddrMode, u16SrcAddr, 1, u8SrcEndpoint, u8SQN, 5, 10, u32FileVersion, u16ImageType, u16ManufactureCode);


                        textBoxOtaDownloadStatus.Text = "Complete";
                        textBoxOtaFileOffset.Text = "";
                        progressBarOtaDownloadProgress.Value = 0;
                        u8OtaInProgress = 0;
                    }
                    break;

                case 0x8110:
                    {
                        UInt16 u16SrcAddr = 0;
                        UInt16 u16ClusterId = 0;
                        UInt16 u16AttribId = 0;
                        UInt16 u16AttributeSize = 0;

                        u16SrcAddr = au8Data[1];
                        u16SrcAddr <<= 8;
                        u16SrcAddr |= au8Data[2];

                        u16ClusterId = au8Data[4];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[5];

                        u16AttribId = au8Data[6];
                        u16AttribId <<= 8;
                        u16AttribId |= au8Data[7];

                        u16AttributeSize = au8Data[10];
                        u16AttributeSize <<= 8;
                        u16AttributeSize |= au8Data[11];

                        richTextBoxMessageView.Text += " (Write Attrib Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Addr: 0x" + u16SrcAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Src Ep: 0x" + au8Data[3].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        displayClusterId(u16ClusterId);
                        richTextBoxMessageView.Text += "\n";
                        displayAttribute(u16AttribId, au8Data[9], au8Data, 12, u16AttributeSize);
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[8].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8600:
                    {
                        nwkRecovery.NetworkRecoveryParseBuffer(au8Data);
                        richTextBoxMessageView.Text += " (Retrieve Network Recovery Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += nwkRecovery.ToString();
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8601:
                    {
                        richTextBoxMessageView.Text += " (Restore Network Recovery Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Success = " + au8Data[0];
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x80A4:
                    {
                        UInt16 u16GroupId;
                        UInt16 u16ClusterId;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        u16GroupId = au8Data[5];
                        u16GroupId <<= 8;
                        u16GroupId |= au8Data[6];

                        richTextBoxMessageView.Text += " (Store Scene Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Tx Num: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Source Endpoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Cluster ID: 0x" + u16ClusterId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Group ID: 0x" + u16GroupId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Scene ID: 0x" + au8Data[7].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x80A1:
                    {
                        UInt16 u16GroupId;
                        UInt16 u16ClusterId;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        u16GroupId = au8Data[5];
                        u16GroupId <<= 8;
                        u16GroupId |= au8Data[6];

                        richTextBoxMessageView.Text += " (Add Scene Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Tx Num: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Source Endpoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Cluster ID: 0x" + u16ClusterId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Group ID: 0x" + u16GroupId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Scene ID: 0x" + au8Data[7].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x80A6:
                    {
                        UInt16 u16GroupId;
                        UInt16 u16ClusterId;

                        u16ClusterId = au8Data[2];
                        u16ClusterId <<= 8;
                        u16ClusterId |= au8Data[3];

                        u16GroupId = au8Data[6];
                        u16GroupId <<= 8;
                        u16GroupId |= au8Data[7];

                        richTextBoxMessageView.Text += " (Get Scene Membership Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Tx Num: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Source Endpoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Cluster ID: 0x" + u16ClusterId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Capacity: 0x" + au8Data[5].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Group ID: 0x" + u16GroupId.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Scene Count: 0x" + au8Data[8].ToString("X2");

                        if (au8Data[8] != 0)
                        {
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Scene List: ";
                        }

                        byte i;

                        for (i = 0; i < au8Data[8]; i++)
                        {

                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "    Scene: 0x" + au8Data[i + 9].ToString("X2");
                        }
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;
                case 0x8046:
                    {
                        UInt16 u16AddrOfInterest;

                        u16AddrOfInterest = au8Data[2];
                        u16AddrOfInterest <<= 8;
                        u16AddrOfInterest |= au8Data[3];

                        richTextBoxMessageView.Text += " (Match Descriptor Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Address Of Interest: 0x" + u16AddrOfInterest.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Match Length: " + au8Data[4];

                        if (au8Data[4] != 0)
                        {
                            richTextBoxMessageView.Text += "  Matched Endpoints: ";
                        }

                        byte i;
                        for (i = 0; i < au8Data[4]; i++)
                        {
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "    Endpoint " + au8Data[5 + i].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;
                case 0x8044:
                    {
                        richTextBoxMessageView.Text += " (Power Descriptor Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Power Source Level: " + Convert.ToString(au8Data[2] & 0x7, 2).PadLeft(4, '0');
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Current Power Source: " + Convert.ToString((au8Data[2] >> 4) & 0x7, 2).PadLeft(4, '0');
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Available Power Source: " + Convert.ToString((au8Data[3]) & 0x7, 2).PadLeft(4, '0');
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Current Power Mode: " + Convert.ToString((au8Data[3] >> 4) & 0x7, 2).PadLeft(4, '0');
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8701:
                    {
                        richTextBoxMessageView.Text += " (Route Discovery Confirm)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Network Status: 0x" + au8Data[2].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;
                case 0x8702:
                    {
                        UInt16 u16DestAddr;

                        u16DestAddr = au8Data[4];
                        u16DestAddr <<= 8;
                        u16DestAddr |= au8Data[5];

                        richTextBoxMessageView.Text += " (APS Data Confirm Fail)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Source Endpoint: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Destination Endpoint: 0x" + au8Data[2].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Destination Mode: 0x" + au8Data[3].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Destination Address: 0x" + u16DestAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[6].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8531:
                    {
                        UInt16 u16AddressOfInterest;

                        u16AddressOfInterest = au8Data[2];
                        u16AddressOfInterest <<= 8;
                        u16AddressOfInterest |= au8Data[3];

                        richTextBoxMessageView.Text += " (Complex Descriptor Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Address of Interest: 0x" + u16AddressOfInterest.ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Length: " + au8Data[4].ToString("X2");
                        richTextBoxMessageView.Text += "\n";

                        if (au8Data[1] == 0)
                        {
                            richTextBoxMessageView.Text += "        XML Tag: " + au8Data[5].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "        Field Count: " + au8Data[6].ToString("X2");
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "        Complex Description: ";
                            for (int i = 0; i < au8Data[6]; i++)
                            {
                                char c = (char)au8Data[6 + i + 1];
                                richTextBoxMessageView.Text += c.ToString();
                            }
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8532:
                    {
                        byte u8StrLen;
                        UInt16 u16NwkAddr = 0;

                        u16NwkAddr = au8Data[2];
                        u16NwkAddr <<= 8;
                        u16NwkAddr |= au8Data[3];
                        u8StrLen = au8Data[4];

                        richTextBoxMessageView.Text += " (User Descriptor Request Response)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Nwk Address: 0x" + u16NwkAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";

                        if (au8Data[1] == 0)
                        {
                            richTextBoxMessageView.Text += "  Length: " + u8StrLen.ToString();
                            richTextBoxMessageView.Text += "\n";
                            richTextBoxMessageView.Text += "  Descriptor: ";

                            for (int i = 0; i < u8StrLen; i++)
                            {
                                char c = (char)au8Data[5 + i];
                                richTextBoxMessageView.Text += c.ToString();
                            }
                            richTextBoxMessageView.Text += "\n";
                        }
                    }
                    break;

                case 0x8533:
                    {
                        byte u8StrLen;
                        UInt16 u16NwkAddr = 0;

                        u16NwkAddr = au8Data[2];
                        u16NwkAddr <<= 8;
                        u16NwkAddr |= au8Data[3];
                        u8StrLen = au8Data[4];

                        richTextBoxMessageView.Text += " (User Descriptor Set Confirm)";
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  SQN: 0x" + au8Data[0].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Status: 0x" + au8Data[1].ToString("X2");
                        richTextBoxMessageView.Text += "\n";
                        richTextBoxMessageView.Text += "  Nwk Address: 0x" + u16NwkAddr.ToString("X4");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                case 0x8903:
                    {
                        UInt64 u64IEEEAddress;
                        byte u8Offset = 0;
                        richTextBoxMessageView.Text += "Gateway IEEE Address: ";
                        u64IEEEAddress = au8Data[u8Offset++];
                        u64IEEEAddress <<= 8;
                        u64IEEEAddress |= au8Data[u8Offset++];
                        u64IEEEAddress <<= 8;
                        u64IEEEAddress |= au8Data[u8Offset++];
                        u64IEEEAddress <<= 8;
                        u64IEEEAddress |= au8Data[u8Offset++];
                        u64IEEEAddress <<= 8;
                        u64IEEEAddress |= au8Data[u8Offset++];
                        u64IEEEAddress <<= 8;
                        u64IEEEAddress |= au8Data[u8Offset++];
                        u64IEEEAddress <<= 8;
                        u64IEEEAddress |= au8Data[u8Offset++];
                        u64IEEEAddress <<= 8;
                        u64IEEEAddress |= au8Data[u8Offset++];
                        localIEEEAddress = u64IEEEAddress;
                        firstBind = false;
                        richTextBoxMessageView.Text += "0x" + u64IEEEAddress.ToString("X16");
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;

                default:
                    {
                        string str = System.Text.Encoding.ASCII.GetString(au8Data);
                        richTextBoxMessageView.Text += str;
                        richTextBoxMessageView.Text += "\n";
                    }
                    break;
            }
        }

        private void displayAttribute(UInt16 u16AttribId, byte u8AttribType, byte[] au8AttribData, byte u8AttribIndex, UInt16 u16AttrSize)
        {
            richTextBoxMessageView.Text += "  Attribute ID: 0x" + u16AttribId.ToString("X4");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "  Attribute Size: 0x" + u16AttrSize.ToString("X4");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "  Attribute Type: 0x" + u8AttribType.ToString("X2");

            switch (u8AttribType)
            {
                case 0x10:
                    richTextBoxMessageView.Text += " (Boolean)";
                    richTextBoxMessageView.Text += "\n";
                    richTextBoxMessageView.Text += "  Attribute Data: 0x" + au8AttribData[u8AttribIndex].ToString("X2");
                    richTextBoxMessageView.Text += "\n";
                    break;
                case 0x18:
                    richTextBoxMessageView.Text += " (8-bit Bitmap)";
                    richTextBoxMessageView.Text += "\n";
                    richTextBoxMessageView.Text += "  Attribute Data: 0x" + au8AttribData[u8AttribIndex].ToString("X2");
                    richTextBoxMessageView.Text += "\n";
                    break;
                case 0x20:
                    richTextBoxMessageView.Text += " (UINT8)";
                    richTextBoxMessageView.Text += "\n";
                    richTextBoxMessageView.Text += "  Attribute Data: 0x" + au8AttribData[u8AttribIndex].ToString("X2");
                    richTextBoxMessageView.Text += "\n";
                    break;
                case 0x21:
                    UInt16 u16Data;
                    u16Data = au8AttribData[u8AttribIndex];
                    u16Data <<= 8;
                    u16Data |= au8AttribData[u8AttribIndex + 1];
                    richTextBoxMessageView.Text += " (UINT16)";
                    richTextBoxMessageView.Text += "\n";
                    richTextBoxMessageView.Text += "  Attribute Data: 0x" + u16Data.ToString("X4");
                    richTextBoxMessageView.Text += "\n";
                    break;
                case 0x23:
                    UInt32 u32Data;
                    u32Data = au8AttribData[u8AttribIndex];
                    u32Data <<= 8;
                    u32Data |= au8AttribData[u8AttribIndex + 1];
                    u32Data <<= 8;
                    u32Data |= au8AttribData[u8AttribIndex + 2];
                    u32Data <<= 8;
                    u32Data |= au8AttribData[u8AttribIndex + 3];
                    richTextBoxMessageView.Text += " (UINT32)";
                    richTextBoxMessageView.Text += "\n";
                    richTextBoxMessageView.Text += "  Attribute Data: 0x" + u32Data.ToString("X8");
                    richTextBoxMessageView.Text += "\n";
                    break;
                case 0x29:
                    richTextBoxMessageView.Text += " (INT16)";
                    richTextBoxMessageView.Text += "\n";
                    break;
                case 0x30:
                    richTextBoxMessageView.Text += " (8-bit Enumeration)";
                    richTextBoxMessageView.Text += "\n";
                    richTextBoxMessageView.Text += "  Attribute Data: 0x" + au8AttribData[u8AttribIndex].ToString("X2");
                    richTextBoxMessageView.Text += "\n";
                    break;
                case 0x42:
                    richTextBoxMessageView.Text += " (Character String)";
                    richTextBoxMessageView.Text += "\n";
                    richTextBoxMessageView.Text += "  Attribute Data (Len - " + u16AttrSize.ToString() + "): ";
                    for (int i = 0; i < u16AttrSize; i++)
                    {
                        char c = (char)au8AttribData[u8AttribIndex + i];
                        richTextBoxMessageView.Text += c.ToString();
                    }
                    richTextBoxMessageView.Text += "\n";
                    break;
                case 0xF0:
                    richTextBoxMessageView.Text += " (IEEE Address)";
                    richTextBoxMessageView.Text += "\n";
                    richTextBoxMessageView.Text += "  Attribute Data: " + au8AttribData[u8AttribIndex].ToString("X2");
                    richTextBoxMessageView.Text += ":" + au8AttribData[u8AttribIndex + 1].ToString("X2");
                    richTextBoxMessageView.Text += ":" + au8AttribData[u8AttribIndex + 2].ToString("X2");
                    richTextBoxMessageView.Text += ":" + au8AttribData[u8AttribIndex + 3].ToString("X2");
                    richTextBoxMessageView.Text += ":" + au8AttribData[u8AttribIndex + 4].ToString("X2");
                    richTextBoxMessageView.Text += ":" + au8AttribData[u8AttribIndex + 5].ToString("X2");
                    richTextBoxMessageView.Text += ":" + au8AttribData[u8AttribIndex + 6].ToString("X2");
                    richTextBoxMessageView.Text += ":" + au8AttribData[u8AttribIndex + 7].ToString("X2");
                    richTextBoxMessageView.Text += "\n";
                    break;
                default:
                    richTextBoxMessageView.Text += " (Unknown)";
                    richTextBoxMessageView.Text += "\n";
                    break;
            }
        }

        private void displayMACcapability(byte u8Capability)
        {
            richTextBoxMessageView.Text += "  MAC Capability: 0x" + u8Capability.ToString("X2");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "    Alternate PAN Coordinator: " + (((u8Capability & 0x01) == 0) ? "False" : "True");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "    Device Type: " + (((u8Capability & 0x02) == 0) ? "End Device" : "Router");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "    Power Source: " + (((u8Capability & 0x04) == 0) ? "Battery" : "AC");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "    Receiver On When Idle: " + (((u8Capability & 0x08) == 0) ? "False" : "True");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "    Security Capability: " + (((u8Capability & 0x40) == 0) ? "Standard" : "High");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "    Allocate Address: " + (((u8Capability & 0x80) == 0) ? "False" : "True");
            richTextBoxMessageView.Text += "\n";
        }

        private void displayDescriptorCapability(byte u8Capability)
        {
            richTextBoxMessageView.Text += "  Descriptor Capability: 0x" + u8Capability.ToString("X2");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "    Ext Active EP List: " + (((u8Capability & 0x01) == 0) ? "No" : "Yes");
            richTextBoxMessageView.Text += "\n";
            richTextBoxMessageView.Text += "    Ext Simple Desc List: " + (((u8Capability & 0x02) == 0) ? "No" : "Yes");
            richTextBoxMessageView.Text += "\n";
        }

        private void displayDeviceId(UInt16 u16DeviceId)
        {
            Dictionary<int, string> deviceList = new Dictionary<int, string>();
            deviceList.Add(0x0000, " (Generic - On/Off Switch)");
            deviceList.Add(0x0001, " (Generic - Level Control Switch)");
            deviceList.Add(0x0002, " (Generic - On/Off Output)");
            deviceList.Add(0x0003, " (Generic - Level Controlable Output)");
            deviceList.Add(0x0004, " (Generic - Scene Selector)");
            deviceList.Add(0x0005, " (Generic - Configuration Tool)");
            deviceList.Add(0x0006, " (Generic - Remote Control)");
            deviceList.Add(0x0007, " (Generic - Combined Interface)");
            deviceList.Add(0x0008, " (Generic - Range Extender)");
            deviceList.Add(0x0009, " (Generic - Mains Power Outlet)");
            deviceList.Add(0x000C, " (Generic - Simple Sensor)");
            deviceList.Add(0x0051, " (Generic - Smart Plug)");
            deviceList.Add(0x0100, " (Lighting - On/Off Light)");
            deviceList.Add(0x0101, " (Lighting - Dimmable Light)");
            deviceList.Add(0x0102, " (Lighting - Color Dimmable Light)");
            deviceList.Add(0x0103, " (Lighting - On/Off Light Switch)");
            deviceList.Add(0x0104, " (Lighting - Dimmer Switch)");
            deviceList.Add(0x0105, " (Lighting - Color Dimmer Switch)");
            deviceList.Add(0x0106, " (Lighting - Light Sensor)");
            deviceList.Add(0x0107, " (Lighting - Occupancy Sensor)");
            deviceList.Add(0x0202, " (HVAC - Fan Control)");
            deviceList.Add(0x0301, " (HVAC - Thermostat)");
            deviceList.Add(0x0500, " (IAS - IAS Zone)");
            deviceList.Add(0x0501, " (IAS - IAS ACE)");
            deviceList.Add(0x0502, " (IAS - IAS WD)");



            richTextBoxMessageView.Text += "  Device ID: 0x" + u16DeviceId.ToString("X4");

            // The indexer throws an exception if the requested key is 
            // not in the dictionary. 
            try
            {
                richTextBoxMessageView.Text += deviceList[u16DeviceId];
            }
            catch (KeyNotFoundException)
            {
                richTextBoxMessageView.Text += " (Unknown)";
            }
        }

        private void displayProfileId(UInt16 u16ProfileId)
        {
            Dictionary<int, string> profileList = new Dictionary<int, string>();
            profileList.Add(0x0104, " (ZigBee HA)");
            profileList.Add(0xC05E, " (ZigBee LL)");

            richTextBoxMessageView.Text += "  Profile ID: 0x" + u16ProfileId.ToString("X4");

            // The indexer throws an exception if the requested key is 
            // not in the dictionary. 
            try
            {
                richTextBoxMessageView.Text += profileList[u16ProfileId];
            }
            catch (KeyNotFoundException)
            {
                richTextBoxMessageView.Text += " (Unknown)";
            }
        }

        private void displayClusterId(UInt16 u16ClusterId)
        {
            Dictionary<int, string> clusterList = new Dictionary<int, string>();
            clusterList.Add(0x0000, " (General: Basic)");
            clusterList.Add(0x0001, " (General: Power Config)");
            clusterList.Add(0x0002, " (General: Temperature Config)");
            clusterList.Add(0x0003, " (General: Identify)");
            clusterList.Add(0x0004, " (General: Groups)");
            clusterList.Add(0x0005, " (General: Scenes)");
            clusterList.Add(0x0006, " (General: On/Off)");
            clusterList.Add(0x0007, " (General: On/Off Config)");
            clusterList.Add(0x0008, " (General: Level Control)");
            clusterList.Add(0x0009, " (General: Alarms)");
            clusterList.Add(0x000A, " (General: Time)");
            clusterList.Add(0x000F, " (General: Binary Input Basic)");
            clusterList.Add(0x0020, " (General: Poll Control)");
            clusterList.Add(0x0019, " (General: OTA)");
            clusterList.Add(0x0101, " (General: Door Lock");
            clusterList.Add(0x0201, " (HVAC: Thermostat)");
            clusterList.Add(0x0202, " (HVAC: Fan Control)");
            clusterList.Add(0x0204, " (HVAC: Thermostat UI Config)");
            clusterList.Add(0x0300, " (Lighting: Color Control)");
            clusterList.Add(0x0400, " (Measurement: Illuminance)");
            clusterList.Add(0x0402, " (Measurement: Temperature)");
            clusterList.Add(0x0406, " (Measurement: Occupancy Sensing)");
            clusterList.Add(0x0500, " (Security & Safety: IAS Zone)");
            clusterList.Add(0x0502, " (Security & Safety: IAS WD)");
            clusterList.Add(0x0702, " (Smart Energy: Metering)");
            clusterList.Add(0x0B05, " (Misc: Diagnostics)");
            clusterList.Add(0x1000, " (ZLL: Commissioning)");

            richTextBoxMessageView.Text += "  Cluster ID: 0x" + u16ClusterId.ToString("X4");

            // The indexer throws an exception if the requested key is 
            // not in the dictionary. 
            try
            {
                richTextBoxMessageView.Text += clusterList[u16ClusterId];
            }
            catch (KeyNotFoundException)
            {
                richTextBoxMessageView.Text += " (Unknown)";
            }
        }

        private void displayDataType(byte u8Type)
        {
            Dictionary<byte, string> typeList = new Dictionary<byte, string>();
            typeList.Add(0x00, " (Null: No Data)");
            typeList.Add(0x10, " (Logical: Boolean)");
            typeList.Add(0x20, " (Unisgned Integer: UINT8)");
            typeList.Add(0x21, " (Unisgned Integer: UINT16)");
            typeList.Add(0x25, " (Unisgned Integer: UINT48)");
            typeList.Add(0x30, " (Enumeration: 8-bit)");
            typeList.Add(0x42, " (String: Character String)");

            richTextBoxMessageView.Text += "  Data Type: 0x" + u8Type.ToString("X2");

            // The indexer throws an exception if the requested key is 
            // not in the dictionary. 
            try
            {
                richTextBoxMessageView.Text += typeList[u8Type];
            }
            catch (KeyNotFoundException)
            {
                richTextBoxMessageView.Text += " (Unknown)";
            }
        }

        #endregion

        #region serial receive functions

        private byte[] rxMessageData = new byte[1024];
        private byte rxMessageChecksum = 0;
        private UInt16 rxMessageLength = 0;
        private uint rxMessageState = 0;
        private UInt16 rxMessageType = 0;
        private uint rxMessageCount = 0;
        private bool rxMessageInEscape = false;



        /*LNT variable*/
        UInt16 readAttributeLoop = 0;
        int readAttributeCount = 0;
        byte currentLevel = 0;
        byte currentHue = 0;
        UInt16 currentX = 0;
        UInt16 currentY = 0;
        byte currentSat = 0;
        UInt16 currentTemp = 0;


        UInt16 step = 0;
        UInt16 setDir = 1;

        bool readAttributeThreadStop = true;
        bool tonggleThreadStop = true;
        bool boardTonggleThreadStop = true;
        bool identifyThreadStop = true;
        bool levelThreadStop = true;
        bool boardLevelThreadStop = true;
        bool hueThreadStop = true;
        bool colorThreadStop = true;
        bool satThreadStop = true;
        bool tempThreadStop = true;

        bool firstLoop = true;

        int msCount = 60;     //timer ms
        UInt16 msCountMin = 60;
        UInt16 msCountMax = 60;
        UInt32 checkedNodeJoined = 0;
        UInt32 NodeJoined = 0;
        UInt64 localIEEEAddress = 0;

        bool removeAllGroup = false;
        int groupCheckedIntemsCount = 0;
        ArrayList nwkAddrJoinedNodeChecked = new ArrayList();

        byte sendCommand;
        int nodeResetCount = 0;
        bool whetherGetInfo = false;

        ArrayList checkedAddress = new ArrayList();
        ArrayList checkedCOM = new ArrayList();
        ArrayList checkedCOMLocation = new ArrayList();
        ArrayList checkedIndex = new ArrayList();
        ArrayList modifiedAddress = new ArrayList();

        List<SerialPort> multiUSBport = new List<SerialPort>();
        ArrayList comIndex = new ArrayList();
        ArrayList nwkAddr = new ArrayList();
        ArrayList IEEEAddr = new ArrayList();
        ArrayList channel = new ArrayList();
        ArrayList type = new ArrayList();
        ArrayList ver = new ArrayList();
        ArrayList chip = new ArrayList();
        ArrayList profile = new ArrayList();
        ArrayList panID = new ArrayList();

        ArrayList indexList = new ArrayList();
        ArrayList readComStringList = new ArrayList();

        Hashtable comHashTable = new Hashtable();  //[com,index in list]
        Hashtable indexComHashTable = new Hashtable(); //[index in list,com]
        Hashtable indexComInMultiPortHashTable = new Hashtable();// [com,index in multiport]
        Hashtable indexMacAddrHashTable = new Hashtable();//[macaddress,index in list]

        Hashtable comGroupHashTable = new Hashtable();

        int groupIndex = 0;
        int ffffCount = 0;
        int sameCount = 0;
        int newGroupIntem = 0;
        int newCOMIntem = 0;

        int portCount;
        bool multiPortOpen = false;
        bool bselected = false;
        bool firstBind = true;
        int previousistViewEZLNTINFOcheckedIntemsCount;
        private static Mutex mut = new Mutex();
        private static Mutex listUpdatemut = new Mutex();

        //Socket
        int asClient = 0;
        string ipAddress;
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //save client socket
        static Dictionary<string, Socket> clientConnectionItems = new Dictionary<string, Socket> { };


        /*LNT variable end*/

        /*LNT remote variable*/
        bool LNTReadFlag = false;
        bool LNTTonggleFlag = false;
        bool LNTIdentifyFlag = false;
        bool LNTLevelFlag = false;
        bool LNTHueFlag = false;
        bool LNTColorFlag = false;
        bool LNTSatFlag = false;
        bool LNTTempFlag = false;
        int pageIndex = 0;

        


        UInt16 readAttributeLoop2 = 0;
        int readAttributeCount2 = 0;
        byte currentLevel2 = 0;
        byte currentHue2 = 0;
        UInt16 currentX2 = 0;
        UInt16 currentY2 = 0;
        byte currentSat2 = 0;
        UInt16 currentTemp2 = 0;


        byte step2 = 0;
        byte setDir2 = 1;

        bool readAttributeThreadStop2 = true;
        bool tonggleThreadStop2 = true;
        bool identifyThreadStop2 = true;
        bool levelThreadStop2 = true;
        bool hueThreadStop2 = true;
        bool colorThreadStop2 = true;
        bool satThreadStop2 = true;
        bool tempThreadStop2 = true;

        int msCount2 = 60;     //timer ms
        UInt16 msCountMin2 = 60;
        UInt16 msCountMax2 = 60;
        UInt32 checkedNodeJoined2 = 0;
        UInt64 localIEEEAddress2 = 0;

        bool removeAllGroup2 = false;
        int groupCheckedIntemsCount2 = 0;
        ArrayList nwkAddrJoinedNodeChecked2 = new ArrayList();

        byte sendCommand2;

        List<SerialPort> multiUSBport2 = new List<SerialPort>();
        ArrayList comIndex2 = new ArrayList();
        ArrayList nwkAddr2 = new ArrayList();
        ArrayList IEEEAddr2 = new ArrayList();
        ArrayList channel2 = new ArrayList();
        ArrayList type2 = new ArrayList();
        ArrayList ver2 = new ArrayList();
        ArrayList indexList2 = new ArrayList();
        ArrayList readComStringList2 = new ArrayList();

        Hashtable comHashTable2 = new Hashtable();
        Hashtable indexComHashTable2 = new Hashtable();

        Hashtable comGroupHashTable2 = new Hashtable();
        int groupIndex2 = 0;
        int newGroupIntem2 = 0;
        int newCOMIntem2 = 0;
        int portCount2;
        bool multiPortOpen2 = false;
        bool bselected2 = false;
        bool firstBind2 = true;
        int previousistViewEZLNTINFOcheckedIntemsCount2;
        private static Mutex mut2 = new Mutex();
        /*LNT variable remote end*/


        public void waitTimeer()
        {
            uint dealyTime=0;
            uint timerstart = timeGetTime();
            if(!firstLoop)
            {
                while (dealyTime < msCount)     // ms
                {
                    dealyTime = timeGetTime() - timerstart;
                }
                Console.WriteLine("delta time: {0}", dealyTime);
            }
            else
            {
                firstLoop = false;
            }

        }

        //Read Attribute function
        private void timerReadAttributeFun()
        {
            UInt16 u16ShortAddr;
            uint tempNodeJoined;
            if (pageIndex == 19)
            {
                tempNodeJoined = checkedNodeJoined;

            }
            else
            {
                tempNodeJoined = checkedNodeJoined2;
            }
            if (tempNodeJoined > 0)
            {
                if (readAttributeLoop == 0)
                {
                    readAttributeThreadStop = true;
                    timeEndPeriod(1);
                    return;
                }
                string s;
                if (!LNTReadFlag)
                {
                    s = nwkAddrJoinedNodeChecked[readAttributeCount] as string;
                }
                else
                {
                    s = nwkAddrJoinedNodeChecked2[readAttributeCount] as string;
                }
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    // instantiate the delegate to be invoked by this thread
                    readAttributeThreadSendCommand = new ReadAttributeThreadSendCommand(myReadAttributeThreadSendCommand);
                    // invoke the delegate in the MainForm thread
                    this.Invoke(readAttributeThreadSendCommand, u16ShortAddr);
                }
                readAttributeCount++;
                if (readAttributeCount == tempNodeJoined)
                {
                    readAttributeLoop--;
                    readAttributeCount = 0;
                }
            }
            else
            {
                readAttributeThreadStop = true;
                timeEndPeriod(1);
                return;
            }

        }


        //ms timer
        private void customertimer()
        {
           
            
            while (!readAttributeThreadStop)
            {
                Random rd = new Random();               
                msCount = rd.Next(msCountMin, msCountMax);
               
                waitTimeer();
                Console.WriteLine("msCount: {0}",msCount);
                         
                timerReadAttributeFun();          
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }


        private void timerBoardTonggleFun()
        {
            UInt16 u16ShortAddr=0xfffc;
           
            {
                if (readAttributeLoop == 0)
                {
                    boardTonggleThreadStop = true;
                    timeEndPeriod(1);
                    return;
                }

                // instantiate the delegate to be invoked by this thread
                boardTonggleThreadSendCommand = new BoardTonggleThreadSendCommand(myBoardTonggleThreadSendCommand);
                // invoke the delegate in the MainForm thread
                this.Invoke(boardTonggleThreadSendCommand, u16ShortAddr);
                Console.WriteLine("tonggle count: {0}", readAttributeLoop);
                readAttributeLoop--;
                                
            }
           
        }

        private void customertimerBoardTonggle()
        {
            

            while (!boardTonggleThreadStop)
            {
                Random rd = new Random();
                msCount = rd.Next(msCountMin, msCountMax);
                waitTimeer();
                Console.WriteLine("msCount: {0}", msCount);
            
                timerBoardTonggleFun();
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }


        private void timerTonggleFun()
        {
            UInt16 u16ShortAddr;
            uint tempNodeJoined;
            if (pageIndex == 19)
            {
                tempNodeJoined = checkedNodeJoined;

            }
            else
            {
                tempNodeJoined = checkedNodeJoined2;
            }
            if (tempNodeJoined > 0)
            {
                if (readAttributeLoop == 0)
                {
                    tonggleThreadStop = true;
                    timeEndPeriod(1);
                    return;
                }
                string s;
                if (!LNTTonggleFlag)
                {
                    s = nwkAddrJoinedNodeChecked[readAttributeCount] as string;
                }
                else
                {
                    s = nwkAddrJoinedNodeChecked2[readAttributeCount] as string;
                }
               
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    // instantiate the delegate to be invoked by this thread
                    tonggleThreadSendCommand = new TonggleThreadSendCommand(myTonggleThreadSendCommand);
                    // invoke the delegate in the MainForm thread
                    this.Invoke(tonggleThreadSendCommand, u16ShortAddr);
                }
                readAttributeCount++;
                if (readAttributeCount == tempNodeJoined)
                {
                    readAttributeLoop--;
                    readAttributeCount = 0;
                }
            }
            else
            {
                tonggleThreadStop = true;
                timeEndPeriod(1);
                return;
            }

        }

       
        private void customertimerTonggle()
        {
        

            while (!tonggleThreadStop)
            {
                Random rd = new Random();
                msCount = rd.Next(msCountMin, msCountMax);
                waitTimeer();
                Console.WriteLine("msCount: {0}", msCount);
             
              
                timerTonggleFun();
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }


        private void timerIdentifyFun()
        {
            UInt16 u16ShortAddr;
            uint tempNodeJoined;
            if (pageIndex == 19)
            {
                tempNodeJoined = checkedNodeJoined;

            }
            else
            {
                tempNodeJoined = checkedNodeJoined2;
            }
            if (tempNodeJoined > 0)
            {
                if (readAttributeLoop == 0)
                {
                    identifyThreadStop = true;
                    timeEndPeriod(1);
                    return;
                }
                string s;
                if (!LNTIdentifyFlag)
                {
                    s = nwkAddrJoinedNodeChecked[readAttributeCount] as string;
                }
                else
                {
                    s = nwkAddrJoinedNodeChecked2[readAttributeCount] as string;
                }
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    // instantiate the delegate to be invoked by this thread
                    identifyThreadSendCommand = new IdentifyThreadSendCommand(myIdentifyThreadSendCommand);
                    // invoke the delegate in the MainForm thread
                    this.Invoke(identifyThreadSendCommand, u16ShortAddr);
                }
                readAttributeCount++;
                if (readAttributeCount == tempNodeJoined)
                {
                    readAttributeLoop--;
                    readAttributeCount = 0;
                }
            }
            else
            {
                identifyThreadStop = true;
                timeEndPeriod(1);
                return;
            }

        }


        private void customertimerIdentify()
        {
            uint timerstart = timeGetTime();

            while (!identifyThreadStop)
            {
                Random rd = new Random();
                msCount = rd.Next(msCountMin, msCountMax);
                waitTimeer();
                Console.WriteLine("msCount: {0}", msCount);
             
                timerIdentifyFun();
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }

        private void timerBoardLevelFun()
        {
            UInt16 u16ShortAddr=0xfffc;
                             
            if (readAttributeLoop == 0)
            {
                levelThreadStop = true;
                timeEndPeriod(1);
                return;
            }
                               
            // instantiate the delegate to be invoked by this thread
            levelThreadSendCommand = new LevelThreadSendCommand(myLevelThreadSendCommand);
            // invoke the delegate in the MainForm thread
            this.Invoke(levelThreadSendCommand, u16ShortAddr);
                           
            if (setDir == 1)
            {
                currentLevel += (byte)step;
            }
            else
            {
                currentLevel -= (byte)step;
            }
            readAttributeLoop--;
                    
        }

        private void customertimerBoardLevel()
        {
            uint timerstart = timeGetTime();

            while (!boardLevelThreadStop)
            {
                Random rd = new Random();
                msCount = rd.Next(msCountMin, msCountMax);
                waitTimeer();
                Console.WriteLine("msCount: {0}", msCount);
               
                timerstart = timeGetTime();
                timerBoardLevelFun();
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }


        private void timerLevelFun()
        {
            UInt16 u16ShortAddr;
            uint tempNodeJoined;
            if (pageIndex == 19)
            {
                tempNodeJoined = checkedNodeJoined;

            }
            else
            {
                tempNodeJoined = checkedNodeJoined2;
            }
            if (tempNodeJoined > 0)
            {
                if (readAttributeLoop == 0)
                {
                    levelThreadStop = true;
                    timeEndPeriod(1);
                    return;
                }
                string s;
                if (!LNTLevelFlag)
                {
                    s = nwkAddrJoinedNodeChecked[readAttributeCount] as string;
                }
                else
                {
                    s = nwkAddrJoinedNodeChecked2[readAttributeCount] as string;
                }
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    // instantiate the delegate to be invoked by this thread
                    levelThreadSendCommand = new LevelThreadSendCommand(myLevelThreadSendCommand);
                    // invoke the delegate in the MainForm thread
                    this.Invoke(levelThreadSendCommand, u16ShortAddr);
                }
                readAttributeCount++;
                if (readAttributeCount == tempNodeJoined)
                {
                    if (setDir == 1)
                    {
                        currentLevel += (byte)step;
                    }
                    else
                    {
                        currentLevel -= (byte)step;
                    }
                    readAttributeLoop--;
                    readAttributeCount = 0;
                }
            }
            else
            {
                levelThreadStop = true;
                timeEndPeriod(1);
                return;
            }
        }

        private void customertimerLevel()
        {
            uint timerstart = timeGetTime();

            while (!levelThreadStop)
            {
                Random rd = new Random();
                msCount = rd.Next(msCountMin, msCountMax);
                waitTimeer();
                Console.WriteLine("msCount: {0}", msCount);
             
                timerLevelFun();
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }

        private void timerHueFun()
        {
            UInt16 u16ShortAddr;
            uint tempNodeJoined;
            if (pageIndex == 19)
            {
                tempNodeJoined = checkedNodeJoined;

            }
            else
            {
                tempNodeJoined = checkedNodeJoined2;
            }
            if (tempNodeJoined > 0)
            {
                if (readAttributeLoop == 0)
                {
                    hueThreadStop = true;
                    timeEndPeriod(1);
                    return;
                }
                string s;
                if (!LNTHueFlag)
                {
                    s = nwkAddrJoinedNodeChecked[readAttributeCount] as string;
                }
                else
                {
                    s = nwkAddrJoinedNodeChecked2[readAttributeCount] as string;
                }
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    // instantiate the delegate to be invoked by this thread
                    hueThreadSendCommand = new HueThreadSendCommand(myHueThreadSendCommand);
                    // invoke the delegate in the MainForm thread
                    this.Invoke(hueThreadSendCommand, u16ShortAddr);
                }
                readAttributeCount++;
                if (readAttributeCount == tempNodeJoined)
                {
                    if (setDir == 1)
                    {
                        currentHue += (byte) step;
                    }
                    else
                    {
                        currentHue -= (byte)step;
                    }
                    readAttributeLoop--;
                    readAttributeCount = 0;
                }
            }
            else
            {
                hueThreadStop = true;
                timeEndPeriod(1);
                return;
            }
        }


        private void customertimerHue()
        {
            uint timerstart = timeGetTime();

            while (!hueThreadStop)
            {
                Random rd = new Random();
                msCount = rd.Next(msCountMin, msCountMax);
                waitTimeer();
                Console.WriteLine("msCount: {0}", msCount);
          
                timerHueFun();
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }

        private void timerColorFun()
        {
            UInt16 u16ShortAddr;
            uint tempNodeJoined;
            if (pageIndex == 19)
            {
                tempNodeJoined = checkedNodeJoined;

            }
            else
            {
                tempNodeJoined = checkedNodeJoined2;
            }
            if (tempNodeJoined > 0)
            {
                if (readAttributeLoop == 0)
                {
                    colorThreadStop = true;
                    timeEndPeriod(1);
                    return;
                }
                string s;
                if (!LNTColorFlag)
                {
                    s = nwkAddrJoinedNodeChecked[readAttributeCount] as string;
                }
                else
                {
                    s = nwkAddrJoinedNodeChecked2[readAttributeCount] as string;
                }
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    // instantiate the delegate to be invoked by this thread
                    colorThreadSendCommand = new ColorThreadSendCommand(myColorThreadSendCommand);
                    // invoke the delegate in the MainForm thread
                    this.Invoke(colorThreadSendCommand, u16ShortAddr);
                }
                readAttributeCount++;
                if (readAttributeCount == tempNodeJoined)
                {
                    if (setDir == 1)
                    {
                        currentX += step;
                        currentY += step;
                    }
                    else
                    {
                        currentX -= step;
                        currentY += step;
                    }
                    readAttributeLoop--;
                    readAttributeCount = 0;
                }
            }
            else
            {
                colorThreadStop = true;
                timeEndPeriod(1);
                return;
            }
        }

        private void customertimerColor()
        {
            uint timerstart = timeGetTime();

            while (!colorThreadStop)
            {
                Random rd = new Random();
                msCount = rd.Next(msCountMin, msCountMax);
                waitTimeer();
                Console.WriteLine("msCount: {0}", msCount);

                timerColorFun();
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }


        private void timerTempFun()
        {
            UInt16 u16ShortAddr;
            uint tempNodeJoined;
            if (pageIndex == 19)
            {
                tempNodeJoined = checkedNodeJoined;

            }
            else
            {
                tempNodeJoined = checkedNodeJoined2;
            }
            if (tempNodeJoined > 0)
            {
                if (readAttributeLoop == 0)
                {
                    tempThreadStop = true;
                    timeEndPeriod(1);
                    return;
                }
                string s;
                if (!LNTTempFlag)
                {
                    s = nwkAddrJoinedNodeChecked[readAttributeCount] as string;
                }
                else
                {
                    s = nwkAddrJoinedNodeChecked2[readAttributeCount] as string;
                }
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    // instantiate the delegate to be invoked by this thread
                    tempThreadSendCommand = new TempThreadSendCommand(myTempThreadSendCommand);
                    // invoke the delegate in the MainForm thread
                    this.Invoke(tempThreadSendCommand, u16ShortAddr);
                }
                readAttributeCount++;
                if (readAttributeCount == tempNodeJoined)
                {
                    if (setDir == 1)
                    {
                        currentTemp += step;
                    }
                    else
                    {
                        currentTemp -= step;
                    }
                    readAttributeLoop--;
                    readAttributeCount = 0;
                }
            }
            else
            {
                tempThreadStop = true;
                timeEndPeriod(1);
                return;
            }
        }

        private void customertimerTemp()
        {
            uint timerstart = timeGetTime();

            while (!tempThreadStop)
            {
                Random rd = new Random();
                msCount = rd.Next(msCountMin, msCountMax);
                waitTimeer();
                Console.WriteLine("msCount: {0}", msCount);

                timerTempFun();
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }

        private void timerSatFun()
        {
            UInt16 u16ShortAddr;
            uint tempNodeJoined;
            if (pageIndex == 19)
            {
                tempNodeJoined = checkedNodeJoined;

            }
            else 
            {
                tempNodeJoined = checkedNodeJoined2;
            }
            if (tempNodeJoined > 0)
            {
                if (readAttributeLoop == 0)
                {
                    satThreadStop = true;
                    timeEndPeriod(1);
                    return;
                }
                string s;
                if (!LNTSatFlag)
                {
                    s = nwkAddrJoinedNodeChecked[readAttributeCount] as string;
                }
                else
                {
                    s = nwkAddrJoinedNodeChecked2[readAttributeCount] as string;
                }
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    // instantiate the delegate to be invoked by this thread
                    satThreadSendCommand = new SatThreadSendCommand(mySatThreadSendCommand);
                    // invoke the delegate in the MainForm thread
                    this.Invoke(satThreadSendCommand, u16ShortAddr);
                }
                readAttributeCount++;
                if (readAttributeCount == tempNodeJoined)
                {
                    if (setDir == 1)
                    {
                        currentSat += (byte)step;
                    }
                    else
                    {
                        currentSat -= (byte)step;
                    }
                    readAttributeLoop--;
                    readAttributeCount = 0;
                }
            }
            else
            {
                satThreadStop = true;
                timeEndPeriod(1);
                return;
            }
        }

        private void customertimerSat()
        {
            uint timerstart = timeGetTime();

            while (!satThreadStop)
            {
                Random rd = new Random();
                msCount = rd.Next(msCountMin, msCountMax);
                waitTimeer();
                Console.WriteLine("msCount: {0}", msCount);

                timerSatFun();
            }

            syncEvent.Set();
            syncEventPort1 = false;
            firstLoop = true;
        }

        // Serial DBGport event handlder 
        private void serialPort2_DataReceivedHandler(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            lntGWDisplayInfo = new LNTGWDisplayInfo(mylntGWDisplayInfo);
            string Info = string.Empty;
            if (serialPort2.BytesToRead > 0)
            {
                Thread.Sleep(200);
                char[] output = new char[serialPort2.BytesToRead];
                // Console.WriteLine("receive data length:{0}", serialPorttemp.BytesToRead);
                serialPort2.Read(output, 0, serialPort2.BytesToRead);
                Info = new string(output);

                this.Invoke(lntGWDisplayInfo, Info);
            }
            
        }

        // Serial port event handlder 
        private void serialPort1_DataReceivedHandler(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (syncEventPort1)
            {
                syncEvent.WaitOne();
            }
            while (serialPort1.BytesToRead > 0)
            {
                byte rxByte = (byte)serialPort1.ReadByte();

                if (rxByte == 0x01)
                {
                    // Start character received
                    rxMessageChecksum = 0;
                    rxMessageLength   = 0;
                    rxMessageType     = 0;
                    rxMessageState    = 0;
                    rxMessageCount    = 0;
                    rxMessageInEscape = false;
                }
                else if (rxByte == 0x02)
                {
                    rxMessageInEscape = true;
                }
                else if (rxByte == 0x03)
                {
                    // instantiate the delegate to be invoked by this thread
                    messageParser = new MessageParser(myMessageParser);

                    // invoke the delegate in the MainForm thread
                    this.Invoke(messageParser);
                }
                else
                {
                    if (rxMessageInEscape == true)
                    {
                        rxByte ^= 0x10;
                        rxMessageInEscape = false;
                    }
                    
                    // Parse character
                    switch (rxMessageState)
                    {
                        case 0:
                        {
                            rxMessageType = rxByte;
                            rxMessageType <<= 8;
                            rxMessageState++;
                        }
                        break;

                        case 1:
                        {
                            rxMessageType |= rxByte;
                            rxMessageState++;
                        }
                        break;

                        case 2:
                        {
                            rxMessageLength = rxByte;
                            rxMessageLength <<= 8;
                            rxMessageState++;
                        }
                        break;

                        case 3:
                        {
                            rxMessageLength |= rxByte;
                            rxMessageState++;
                        }
                        break;

                        case 4:
                        {
                            rxMessageChecksum = rxByte;
                            rxMessageState++;
                        }
                        break;

                        default:
                        {
                            rxMessageData[rxMessageCount++] = rxByte;
                        }
                        break;
                    }
                }
            }
        }

        public void SendCommand()
        {          
            byte[] dataArray = null;
            dataArray = new byte[1];
            dataArray[0] = 0x69;  //send i again
            sendCommand = 0x69;
            Thread.Sleep(20);
            for (int i = 0; i < checkedCOM.Count; i++)
            {
                string COM = (string)checkedCOM[i];
                if (multiUSBport[((int)indexComInMultiPortHashTable[COM])].IsOpen)
                {
                    multiUSBport[((int)indexComInMultiPortHashTable[COM])].Write(dataArray, 0, 1);
                    Thread.Sleep(30);
                    //   Console.WriteLine("Write success");
                }
            }
        }

        private void serialPortMulti_DataReceivedHandler(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            
            //Console.WriteLine("receive data:");
            

            SerialPort serialPorttemp = new SerialPort();
            serialPorttemp = sender as SerialPort;

            
            

            Thread.Sleep(55);

            char[] output = new char[serialPorttemp.BytesToRead];
           // Console.WriteLine("receive data length:{0}", serialPorttemp.BytesToRead);
            serialPorttemp.Read(output,0,serialPorttemp.BytesToRead);
            string s = new string(output);
            multiMessageParser = new MultiMessageParser(myMultiMessageParser);
            this.Invoke(multiMessageParser, output);

            socketSendData = new SocketSendData(mysocketSendData);
            if (asClient == 2)//client
            {
                this.Invoke(socketSendData, s, clientSocket);
                
            }
            //Console.WriteLine("receive data length:{0}", serialPorttemp.BytesToRead);
            mut.WaitOne();
            
            // instantiate the delegate to be invoked by this thread
            
            multiInfoDisplay = new MultiInfoDisplay(myMultiInfoDisplay);
            
            if (sendCommand == (0x66))   //f
            {
                Regex r = new Regex("NODE RESET");
                Match m = r.Match(s);
                if (m.Success)
                {
                    nodeResetCount++;
                }
                if (nodeResetCount == checkedCOM.Count)
                {
                    sendCommand = 0x69;
                    Thread sendCommandThread = new Thread(SendCommand);
                    sendCommandThread.Start();
                   
                }
            }


            if ((sendCommand == (0x69)))  //i
            {
                string[] eArray;
                if (s.Contains("5169"))
                {
                    eArray = Regex.Split(s, "\r\n", RegexOptions.IgnoreCase);
                }
                else
                {
                    eArray = Regex.Split(s, "\n", RegexOptions.IgnoreCase);
                }
                string[] sArray = Regex.Split(eArray[1], ",", RegexOptions.IgnoreCase);
                string[] chaArray = Regex.Split(sArray[0], "=", RegexOptions.IgnoreCase);
                //analyze received data  
                if(chaArray[0] == " Nwk Channel")
                {
                    string scom = serialPorttemp.PortName as string;
                    int comInd = (int)comHashTable[scom];
                    comIndex.Add(comInd);

                    channel.Add(chaArray[1]);

                    string[] nwkArray = Regex.Split(sArray[1], "=", RegexOptions.IgnoreCase);
                    nwkAddr.Add(nwkArray[1]);

                    string[] IEEEArray = Regex.Split(sArray[2], "=", RegexOptions.IgnoreCase);
                    IEEEAddr.Add(IEEEArray[1]);

                    string[] verArray = Regex.Split(sArray[3], "=", RegexOptions.IgnoreCase);
                    ver.Add(verArray[1]);

                    string[] typeArray = Regex.Split(sArray[4], "=", RegexOptions.IgnoreCase);
                    type.Add(typeArray[1]);

                    string[] chipArray = Regex.Split(sArray[5], "=", RegexOptions.IgnoreCase);
                    chip.Add(chipArray[1]);

                    string[] profileArray = Regex.Split(sArray[6], "=", RegexOptions.IgnoreCase);
                    profile.Add(profileArray[1]);

                    string[] panIDArray = Regex.Split(sArray[7], "=", RegexOptions.IgnoreCase);
                    panID.Add(panIDArray[1]);


                    //analyze received data end
                    int index = comIndex.Count - 1;
                    this.Invoke(multiInfoDisplay, index);
                   
                }
            }
            // invoke the delegate in the MainForm thread

                   
            mut.ReleaseMutex();

        }

        private void displayRawCommandData(UInt16 u16Type, UInt16 u16Length, byte u8Checksum, byte[] au8Data)
        {
            byte tempByte;
            /* Dont display OTA block request/response as it slows down the process!! */
            if ((u8OtaInProgress == 0) || ((u16Type != 0x8000) && (u16Type != 0x8501) && (u16Type != 0x0502)))
            {
                richTextBoxCommandResponse.Text += DateTime.Now.Hour.ToString("D2");
                richTextBoxCommandResponse.Text += ":";
                richTextBoxCommandResponse.Text += DateTime.Now.Minute.ToString("D2");
                richTextBoxCommandResponse.Text += ":";
                richTextBoxCommandResponse.Text += DateTime.Now.Second.ToString("D2");
                richTextBoxCommandResponse.Text += ".";
                richTextBoxCommandResponse.Text += DateTime.Now.Millisecond.ToString("D3");
                richTextBoxCommandResponse.Text += " <- ";
                richTextBoxCommandResponse.Text += "01 ";

                if (u16Type != 0x8501)
                {
                    tempByte = (byte)(u16Type >> 8);
                    richTextBoxCommandResponse.Text += tempByte.ToString("X2");
                    richTextBoxCommandResponse.Text += " ";
                    tempByte = (byte)u16Type;
                    richTextBoxCommandResponse.Text += tempByte.ToString("X2");
                    richTextBoxCommandResponse.Text += " ";

                    tempByte = (byte)(u16Length >> 8);
                    richTextBoxCommandResponse.Text += tempByte.ToString("X2");
                    richTextBoxCommandResponse.Text += " ";
                    tempByte = (byte)u16Length;
                    richTextBoxCommandResponse.Text += tempByte.ToString("X2");
                    richTextBoxCommandResponse.Text += " ";

                    richTextBoxCommandResponse.Text += u8Checksum.ToString("X2");
                    richTextBoxCommandResponse.Text += " ";

                    for (int i = 0; i < u16Length; i++)
                    {
                        richTextBoxCommandResponse.Text += au8Data[i].ToString("X2");
                        richTextBoxCommandResponse.Text += " ";
                    }

                    richTextBoxCommandResponse.Text += "03";
                    richTextBoxCommandResponse.Text += "\n";
                }
            }
        }
#endregion

#region message display windows

        private void buttonClearRaw_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Text = "";
        }

        private void buttonMessageViewClear_Click(object sender, EventArgs e)
        {
            richTextBoxMessageView.Text = "";
        }
#endregion

        private void comboBoxConfigReportAttribDirection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxBasicResetTargetAddr_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonLeave_Click(object sender, EventArgs e)
        {
            UInt64 u64ExtAddr = 0;

            if (bStringToUint64(textBoxLeaveAddr.Text, out u64ExtAddr) == true)
            {
                sendLeaveRequest(u64ExtAddr, (byte)comboBoxLeaveReJoin.SelectedIndex, (byte)comboBoxLeaveChildren.SelectedIndex);
            }
        }

        private void buttonRemoveDevice_Click(object sender, EventArgs e)
        {
            UInt64 u64ParentExtAddr = 0;
            UInt64 u64ChildExtAddr = 0;

            if (bStringToUint64(textBoxRemoveParentAddr.Text, out u64ParentExtAddr) == true)
            {
                if (bStringToUint64(textBoxRemoveChildAddr.Text, out u64ChildExtAddr) == true)
                {
                    sendRemoveRequest(u64ParentExtAddr, u64ChildExtAddr);
                }
            }
        }

        private void buttonRawDataSend_Click(object sender, EventArgs e)
        {
            UInt16 u16TargetAddr;
            UInt16 u16ClusterID;
            UInt16 u16ProfileID;
            byte u8SecurityMode, u8Radius;
            byte u8SrcEndPoint;
            byte u8DstEndPoint;
            String stringRawData = "";

            if (bStringToUint16(textBoxRawDataCommandsTargetAddr.Text, out u16TargetAddr) == true)
            {
                if (bStringToUint8(textBoxRawDataCommandsSrcEP.Text, out u8SrcEndPoint) == true)
                {
                    if (bStringToUint8(textBoxRawDataCommandsDstEP.Text, out u8DstEndPoint) == true)
                    {
                        if (bStringToUint16(textBoxRawDataCommandsProfileID.Text, out u16ProfileID) == true)
                        {
                            if (bStringToUint16(textBoxRawDataCommandsClusterID.Text, out u16ClusterID) == true)
                            {
                                if (bStringToUint8(textBoxRawDataCommandsSecurityMode.Text, out u8SecurityMode) == true)
                                {
                                    if (bStringToUint8(textBoxRawDataCommandsRadius.Text, out u8Radius) == true)
                                    {
                                            stringRawData = textBoxRawDataCommandsData.Text;
                                            sendRawDataCommandsRequest((byte)comboBoxRawDataCommandsAddrMode.SelectedIndex, u16TargetAddr, u8SrcEndPoint, u8DstEndPoint, u16ProfileID, u16ClusterID, u8SecurityMode, u8Radius, stringRawData);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonInPacketNotification_Click(object sender, EventArgs e)
        {
            UInt32 u32RfActiveOutDioMask;
            UInt32 u32StatusOutDioMask;
            UInt32 u32TxConfInDioMask;
            UInt16 u16PollPeriod;

            if (bStringToUint32(textBoxIPNConfigDioRfActiveOutDioMask.Text, out u32RfActiveOutDioMask) == true)
            {
                if (bStringToUint32(textBoxIPNConfigDioStatusOutDioMask.Text, out u32StatusOutDioMask) == true)
                {
                    if (bStringToUint32(textBoxIPNConfigDioTxConfInDioMask.Text, out u32TxConfInDioMask) == true)
                    {
                        if (bStringToUint16(textBoxIPNConfigPollPeriod.Text, out u16PollPeriod) == true)
                        {
                            sendIPNConfigureCommand((byte)comboBoxIPNConfigEnable.SelectedIndex, u32RfActiveOutDioMask, u32StatusOutDioMask, u32TxConfInDioMask, (byte)comboBoxIPNConfigRegisterCallback.SelectedIndex, u16PollPeriod, (byte)comboBoxIPNConfigTimerId.SelectedIndex);
                        }
                    }
                }
            }
        }

        private void buttonDioSetDirection_Click(object sender, EventArgs e)
        {
            UInt32 u32InputDIOMask;
            UInt32 u32OutputDIOMask;

            if (bStringToUint32(textBoxDioSetDirectionInputPinMask.Text, out u32InputDIOMask) == true)
            {
                if (bStringToUint32(textBoxDioSetDirectionOutputPinMask.Text, out u32OutputDIOMask) == true)
                {
                    sendDioSetDirectionOutputCommand(0x0801, u32InputDIOMask, u32OutputDIOMask);
                }
            }
        }

        private void buttonDioSetOutput_Click(object sender, EventArgs e)
        {
            UInt32 u32OutputOnDIOMask;
            UInt32 u32OutputOffDIOMask;

            if (bStringToUint32(textBoxDioSetOutputOnPinMask.Text, out u32OutputOnDIOMask) == true)
            {
                if (bStringToUint32(textBoxDioSetOutputOffPinMask.Text, out u32OutputOffDIOMask) == true)
                {
                    sendDioSetDirectionOutputCommand(0x0802, u32OutputOnDIOMask, u32OutputOffDIOMask);
                }
            }
        }

        private void buttonAHISetTxPower_Click(object sender, EventArgs e)
        {
            byte u8TxPower;

            if (bStringToUint8(textBoxAHITxPower.Text, out u8TxPower) == true)
            {
                sendAHISetTxPowerCommand(u8TxPower);
            }
        }

#region Mouse Hover/Leave

#region ManagementTab

#region SetEPID

        private void textBoxSetEPID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Extended PAN ID to be used (64-bit Hex)");
        }

        private void textBoxSetEPID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region SetCMSK

        private void textBoxSetCMSK_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Channel value (32-bit). Can be either Hex of Channel Mask or Single Decimal Channel");
        }

        private void textBoxSetCMSK_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region ManagementLeave

        private void textBoxMgmtLeaveAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination address where the command will go (16-bit Hex)");
        }

        private void textBoxMgmtLeaveAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMgmtLeaveExtAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Device which is requested to leave (64-bit Hex)");
        }

        private void textBoxMgmtLeaveExtAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Leave

        private void textBoxLeaveAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Device which is requested to leave (64-bit Hex)");
        }

        private void textBoxLeaveAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region RemoveDevice

        private void textBoxRemoveParentAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Parent address of the device to be removed (64-bit Hex)");
        }

        private void textBoxRemoveParentAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveChildAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address of the device to be removed (64-bit Hex)");
        }

        private void textBoxRemoveChildAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region PermitJoin

        private void textBoxPermitJoinAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address to set permit join value (8 bit-Hex)");
        }

        private void textBoxPermitJoinAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxPermitJoinInterval_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Permit join value (8-bit Hex)");
        }

        private void textBoxPermitJoinInterval_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region MatchDescriptorRequest

        private void textBoxMatchReqAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address for the Match Descriptor Request (16-bit Hex)");
        }

        private void textBoxMatchReqAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMatchReqProfileID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Profile ID (16-bit Hex)");
        }

        private void textBoxMatchReqProfileID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMatchReqNbrInputClusters_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Number of Intput Clusters (8-bit Hex)");
        }

        private void textBoxMatchReqNbrInputClusters_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMatchReqInputClusters_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Input Cluster List (Array 16-bit Hex e.g. 00050010)");
        }

        private void textBoxMatchReqInputClusters_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMatchReqNbrOutputClusters_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Number of Output Clusters (8-bit Hex)");
        }

        private void textBoxMatchReqNbrOutputClusters_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMatchReqOutputClusters_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Output Cluster List (Array 16-bit Hex e.g. 00050010)");
        }

        private void textBoxMatchReqOutputClusters_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region BindRequest

        private void textBoxBindTargetExtAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("The Address the Request is Going to (32-bit Hex)");
        }

        private void textBoxBindTargetExtAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxBindTargetEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Endpoint Number the Request is going to (8-bit Hex)");
        }

        private void textBoxBindTargetEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxBindClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID which will be put into the Binding Table (16-bit Hex)");
        }

        private void textBoxBindClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxBindAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode (8-bit Hex)");
        }

        private void comboBoxBindAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxBindDestAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address which will be put into the binding table (32-bit Hex)");
        }

        private void textBoxBindDestAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxBindDestEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Endpoint Number which will be put into the binding table (32-bit Hex)");
        }

        private void textBoxBindDestEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region UnbindRequest

        private void textBoxUnBindTargetExtAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("The Address the Request is Going to (32-bit Hex)");
        }

        private void textBoxUnBindTargetExtAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxUnBindTargetEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Endpoint Number the Request is going to (8-bit Hex)");
        }

        private void textBoxUnBindTargetEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxUnBindClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID which will be removed from the Binding Table (16-bit Hex)");
        }

        private void textBoxUnBindClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxUnBindAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode (8-bit Hex)");
        }

        private void comboBoxUnBindAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxUnBindDestAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address which will be removed from the binding table (32-bit Hex)");
        }

        private void textBoxUnBindDestAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxUnBindDestEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Endpoint Number which will be removed from the binding table (32-bit Hex)");
        }

        private void textBoxUnBindDestEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region ActiveEndpointRequest

        private void textBoxActiveEpAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxActiveEpAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region IEEERequest

        private void textBoxIeeeReqTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address request will be sent to (16-bit Hex)");
        }

        private void textBoxIeeeReqTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIeeeReqAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("The short address associated with the requested IEEE address (16-bit Hex)");
        }

        private void textBoxIeeeReqAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIeeeReqStartIndex_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Neighbour table index of the first neighbouring node to be included in the response (8-Bit Hex)");
        }

        private void textBoxIeeeReqStartIndex_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region NetworkAddressRequest

        private void textBoxNwkAddrReqTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address the request will be sent to (16-bit Hex)");
        }

        private void textBoxNwkAddrReqTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxNwkAddrReqExtAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address associated with the short address required (32-bit Hex)");
        }

        private void textBoxNwkAddrReqExtAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxNwkAddrReqStartIndex_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Neighbour table index of the first neighbouring node to be included in the response (8-Bit Hex)");
        }

        private void textBoxNwkAddrReqStartIndex_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region NodeDescriptorRequest

        private void textBoxNodeDescReq_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxNodeDescReq_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region PowerDescriptorRequest

        private void textBoxPowerReqAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxPowerReqAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region SimpleDescriptorRequest

        private void textBoxSimpleReqAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxSimpleReqAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxSimpleReqEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Endpoint Number (8-bit Hex)");
        }

        private void textBoxSimpleReqEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region ComplexDescriptorRequest

        private void textBoxComplexReqAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxComplexReqAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLqiReqTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxLqiReqTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLqiReqStartIndex_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Starting index in the neighbour table (8-bit Hex)");
        }

        private void textBoxLqiReqStartIndex_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region UserDescriptorRequest

        private void textBoxUserReqAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxUserReqAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region UserDescriptorSetRequest

        private void textBoxUserSetReqAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxUserSetReqAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxUserSetReqDescription_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("User Description (String)");
        }

        private void textBoxUserSetReqDescription_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region NetworkRestoreRequest

        private void textBoxRestoreNwkFrameCounter_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Outgoing frame counter value to start from (16-bit Hex)");
        }

        private void textBoxRestoreNwkFrameCounter_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region GeneralTab

#region ReadAttributeRequest

        private void textBoxReadAttribTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxReadAttribTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadAttribSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxReadAttribSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadAttribDstEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxReadAttribDstEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadAttribClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID (16-bit Hex)");
        }

        private void textBoxReadAttribClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxReadAttribDirection_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Server or Client Attribute");
        }

        private void comboBoxReadAttribDirection_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadAttribCount_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Number of attributes to read (8-bit Hex)");
        }

        private void textBoxReadAttribCount_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadAttribID1_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute ID to be read (16-bit Hex)");
        }

        private void textBoxReadAttribID1_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxReadAttribManuSpecific_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Standard or Manufacturer Specific");
        }

        private void comboBoxReadAttribManuSpecific_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadAttribManuID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Manufacturer Code (16-bit Hex)");
        }

        private void textBoxReadAttribManuID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region ReadLocalAttributeRequest

        private void textBoxReadLocalSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxReadLocalSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadLocalClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID (16-bit Hex)");
        }

        private void textBoxReadLocalClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadLocalAttribID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute ID to be read (16-bit Hex)");
        }

        private void textBoxReadLocalAttribID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadLocalAttribValue_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute Value");
        }

        private void textBoxReadLocalAttribValue_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region WriteAttributeRequest

        private void textBoxWriteAttribTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxWriteAttribTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxWriteAttribSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxWriteAttribSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxWriteAttribDstEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxWriteAttribDstEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxWriteAttribClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID (16-bit Hex)");
        }

        private void textBoxWriteAttribClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxWriteAttribDirection_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Server or Client attribute");
        }

        private void comboBoxWriteAttribDirection_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxWriteAttribID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute ID (16-bit Hex)");
        }

        private void textBoxWriteAttribID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxWriteAttribDataType_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute Type (16-bit Hex)");
        }

        private void textBoxWriteAttribDataType_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxWriteAttribData_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute Data");
        }

        private void textBoxWriteAttribData_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxWriteAttribManuSpecific_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Standard or Manufacturer specific attribute");
        }

        private void comboBoxWriteAttribManuSpecific_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxWriteAttribManuID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Manufacturer Code (16-bit Hex)");
        }

        private void textBoxWriteAttribManuID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }
#endregion

#region ConfigureReportingRequest

        private void comboBoxConfigReportAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxConfigReportAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxConfigReportTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxConfigReportSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportDstEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (16-bit Hex)");
        }

        private void textBoxConfigReportDstEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID (16-bit Hex)");
        }

        private void textBoxConfigReportClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxConfigReportDirection_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Server or Client Attribute");
        }

        private void comboBoxConfigReportDirection_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxConfigReportAttribDirection_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Whether it will be sent by the server or recived by the client");
        }

        private void comboBoxConfigReportAttribDirection_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportAttribType_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute Type (8-bit Hex)");
        }

        private void textBoxConfigReportAttribType_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportAttribID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute ID (16-bit Hex)");
        }

        private void textBoxConfigReportAttribID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportMinInterval_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Minimum Interval e.g. On Change Time (16-bit Hex)");
        }

        private void textBoxConfigReportMinInterval_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportMaxInterval_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Maximum Interval e.g Periodic Report Time (16-bit Hex)");
        }

        private void textBoxConfigReportMaxInterval_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportTimeOut_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("How often a client will expect a report (16-bit Hex)");
        }

        private void textBoxConfigReportTimeOut_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxConfigReportChange_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Minimum change required before a On Change report is generated (8-bit Hex)");
        }

        private void textBoxConfigReportChange_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region ReadReportRequest

        private void comboBoxReadReportConfigAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxReadReportConfigAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadReportConfigTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxReadReportConfigTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadReportConfigSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxReadReportConfigSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadReportConfigDstEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxReadReportConfigDstEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadReportConfigClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID (16-bit Hex)");
        }

        private void textBoxReadReportConfigClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxReadReportConfigDirection_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("To Server or Client");
        }

        private void comboBoxReadReportConfigDirection_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadReportConfigAttribID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute ID (16-bit Hex)");
        }

        private void textBoxReadReportConfigAttribID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxReadReportConfigDirIsRx_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Whether it is for a device that is sending report or receiving them");
        }

        private void comboBoxReadReportConfigDirIsRx_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region ReadAllAttributeRequest

        private void textBoxReadAllAttribAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxReadAllAttribAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadAllAttribSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxReadAllAttribSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadAllAttribDstEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxReadAllAttribDstEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxReadAllAttribClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID (16-bit Hex)");
        }

        private void textBoxReadAllAttribClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxReadAllAttribDirection_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("To Server or Client");
        }

        private void comboBoxReadAllAttribDirection_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Discover Attribute Request

        private void textBoxDiscoverAttributesAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxDiscoverAttributesAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverAttributesSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxDiscoverAttributesSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverAttributesDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxDiscoverAttributesDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverAttributesStartAttrId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Start Attr ID (8-bit Hex)");
        }

        private void textBoxDiscoverAttributesStartAttrId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverAttributesClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID (16-bit Hex)");
        }

        private void textBoxDiscoverAttributesClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxDiscoverAttributesDirection_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Attribute Direction");
        }

        private void comboBoxDiscoverAttributesDirection_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverAttributesMaxIDs_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Number Of Attributes (8-bit Hex)");
        }

        private void textBoxDiscoverAttributesMaxIDs_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxDiscoverAttributesExtended_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Standard or Extended");
        }

        private void comboBoxDiscoverAttributesExtended_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region MTO Rt Req

        private void comboBoxManyToOneRouteRequestCacheRoute_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cache or No Cache");
        }

        private void comboBoxManyToOneRouteRequestCacheRoute_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxManyToOneRouteRequesRadius_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Radius (8-bit Hex)");
        }

        private void textBoxManyToOneRouteRequesRadius_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Network Update

        private void comboBoxMgmtNwkUpdateAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxMgmtNwkUpdateAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMgmtNwkUpdateTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxMgmtNwkUpdateTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMgmtNwkUpdateChannelMask_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Channel Mask (32-bit Hex)");
        }

        private void textBoxMgmtNwkUpdateChannelMask_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMgmtNwkUpdateScanDuration_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Duration (8-bit Hex)");
        }

        private void textBoxMgmtNwkUpdateScanDuration_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMgmtNwkUpdateScanCount_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Scan Count (8-bit Hex)");
        }

        private void textBoxMgmtNwkUpdateScanCount_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMgmtNwkUpdateNwkManagerAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Network Manager Address (16-bit Hex)");
        }

        private void textBoxMgmtNwkUpdateNwkManagerAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Disc Commands

        private void comboBoxDiscoverCommandsAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxDiscoverCommandsAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverCommandsTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxDiscoverCommandsTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverCommandsSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxDiscoverCommandsSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverCommandsDstEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxDiscoverCommandsDstEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverCommandsClusterID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Cluster ID (16-bit Hex)");
        }

        private void textBoxDiscoverCommandsClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxDiscoverCommandsDirection_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("To Server or Client");
        }

        private void comboBoxDiscoverCommandsDirection_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverCommandsCommandID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Command ID (8-bit Hex)");
        }

        private void textBoxDiscoverCommandsCommandID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxDiscoverCommandsManuSpecific_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Standard or Manufacturer Specific");
        }

        private void comboBoxDiscoverCommandsManuSpecific_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverCommandsManuID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Manufacturer ID (16-bit Hex)");
        }

        private void textBoxDiscoverCommandsManuID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDiscoverCommandsMaxCommands_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Maximum Commands (8-bit Hex)");
        }

        private void textBoxDiscoverCommandsMaxCommands_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxDiscoverCommandsRxGen_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Received or Generated");
        }

        private void comboBoxDiscoverCommandsRxGen_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Raw Data Send

        private void textBoxRawDataCommandsTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxRawDataCommandsTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRawDataCommandsSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxRawDataCommandsSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRawDataCommandsProfileID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Profile ID (16-bit Hex)");
        }

        private void textBoxRawDataCommandsProfileID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRawDataCommandsDstEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxRawDataCommandsDstEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRawDataCommandsClusterID_MouseHover(object sender, EventArgs e)
        {

            showToolTipWindow("Cluster ID (16-bit Hex)");
        }

        private void textBoxRawDataCommandsClusterID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRawDataCommandsRadius_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Radius/Max Number Of Hops (8-bit Hex)");
        }

        private void textBoxRawDataCommandsRadius_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRawDataCommandsSecurityMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Security Mode (8-bit Hex) - See zps_apl_af.h enum ZPS_teAplAfSecurityMode)");
        }

        private void textBoxRawDataCommandsSecurityMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRawDataCommandsData_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Raw APS data (Array of 8-Bit Hex e.g 00:11:22:33)");
        }

        private void textBoxRawDataCommandsData_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region OOB Commissioning Data

        private void textBoxOOBDataAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Extended Address Data (64-bit Hex)");
        }

        private void textBoxOOBDataAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOOBDataKey_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Key (Format: Byte:Byte:Byte)");
        }

        private void textBoxOOBDataKey_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region InstallCodeSend

        private void textBoxGeneralInstallCodeMACaddress_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("MACaddress(64bit-hex)");
        }

        private void textBoxGeneralInstallCodeMACaddress_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGeneralInstallCodeCode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("InstallCode(128bit-hex)");
        }

        private void textBoxGeneralInstallCodeCode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGeneralInstallCodeMACaddressPrint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("MACaddress(64bit-hex)");
        }

        private void textBoxGeneralInstallCodeMACaddressPrint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGeneralInstallCodeCodePrint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("LinKey(128-hex)");
        }

        private void textBoxGeneralInstallCodeCodePrint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region AHITab

#region IPNConfigure

        private void comboBoxIPNConfigEnable_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Boolean to Enable IPN");
        }

        private void comboBoxIPNConfigEnable_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIPNConfigDioRfActiveOutDioMask_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Output Pin indicating a Tx/Rx Request (32-Bit Hex)");
        }

        private void textBoxIPNConfigDioRfActiveOutDioMask_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIPNConfigDioStatusOutDioMask_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("The priority pin (32-Bit Hex)");
        }

        private void textBoxIPNConfigDioStatusOutDioMask_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIPNConfigDioTxConfInDioMask_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Input pin indicating whether the request is granted or not (32-Bit Hex)");
        }

        private void textBoxIPNConfigDioTxConfInDioMask_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxIPNConfigRegisterCallback_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Enable IPN callback on state change");
        }

        private void comboBoxIPNConfigRegisterCallback_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIPNConfigPollPeriod_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Poll period, units of 62500Hz clock (16-Bit Hex)");
        }

        private void textBoxIPNConfigPollPeriod_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxIPNConfigTimerId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("The hardware timer to be used for the 100us request/response delay");
        }

        private void comboBoxIPNConfigTimerId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAHITxPower_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("A twos compliment 6 bit value indicating the Tx Power.");
        }

        private void textBoxAHITxPower_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region DIOSetDirection

        private void textBoxDioSetDirectionInputPinMask_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("DIO Input Mask (32-Bit Hex)");
        }

        private void textBoxDioSetDirectionInputPinMask_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDioSetDirectionOutputPinMask_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("DIO Output Mask (32-Bit Hex)");
        }

        private void textBoxDioSetDirectionOutputPinMask_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region DIOSetOutput

        private void textBoxDioSetOutputOnPinMask_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("DIO Input On Mask (32-Bit Hex)");
        }

        private void textBoxDioSetOutputOnPinMask_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxDioSetOutputOffPinMask_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("DIO Output Off Mask (32-Bit Hex)");
        }

        private void textBoxDioSetOutputOffPinMask_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion


#endregion

#region BasicClusterTab

#region Reset to FD

        private void comboBoxBasicResetTargetAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxBasicResetTargetAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxBasicResetTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxBasicResetTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxBasicResetSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxBasicResetSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxBasicResetDstEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxBasicResetDstEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region PollControlTab

        private void comboBoxFastPollEnable_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Enable or Disable Fast Polling");
        }

        private void comboBoxFastPollEnable_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxFastPollExpiryTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Fast polling expiry time (1/4 second increments)");
        }

        private void textBoxFastPollExpiryTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region EZLNTTab


 
        private void textBoxEZLNTONOFFADDRESS_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("address(hex)");
        }


        private void textBoxEZLNTONOFFADDRESS_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTSendCommand_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("command(char)");
        }

        private void textBoxEZLNTSendCommand_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTSETLOOP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("loop count");
        }

        private void textBoxEZLNTSETLOOP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTTIMERINTERVAL_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("timer interval min (ms)");
        }

        private void textBoxEZLNTTIMERINTERVAL_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTVIEW_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("group address (hex)");
        }

        private void textBoxEZLNTVIEW_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTADDGROUP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("group address (hex)");
        }

        private void textBoxEZLNTADDGROUP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxREMOVEGROUP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("group address (hex)");
        }

        private void textBoxREMOVEGROUP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTBINDCLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID (hex)");
        }

        private void textBoxEZLNTBINDCLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTUNBINDCLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID (hex)");
        }

        private void textBoxEZLNTUNBINDCLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTCONFIGRPRTCLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID (hex)");
        }

        private void textBoxEZLNTCONFIGRPRTCLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTCONFIGRPRTTYPE_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("data type (hex)");
        }

        private void textBoxEZLNTCONFIGRPRTTYPE_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTCONFIGRPRTATTRIBID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute ID (hex)");
        }

        private void textBoxEZLNTCONFIGRPRTATTRIBID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTCONFIGRPRTMININTERVAL_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("min interval (hex)");
        }

        private void textBoxEZLNTCONFIGRPRTMININTERVAL_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTCONFIGRPRTMAXINTERVAL_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("max interval (hex)");
        }

        private void textBoxEZLNTCONFIGRPRTMAXINTERVAL_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTCONFIGRPRTTIMEOUT_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time out (hex)");
        }

        private void textBoxEZLNTCONFIGRPRTTIMEOUT_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTCONFIGRPRTCHANGE_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("report change (hex)");
        }

        private void textBoxEZLNTCONFIGRPRTCHANGE_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTREADRPRTCLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID (hex)");
        }

        private void textBoxEZLNTREADRPRTCLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTREADRPRTATTRIBUTEID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute ID (hex)");
        }

        private void textBoxEZLNTREADRPRTATTRIBUTEID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTREADCLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID (hex)");
        }

        private void textBoxEZLNTREADCLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTATTRIBUTEID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute ID (hex)");
        }

        private void textBoxEZLNTATTRIBUTEID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTATTRIBUTECOUNT_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute count (hex)");
        }

        private void textBoxEZLNTATTRIBUTECOUNT_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTWRITEATTRIBUTECLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID (hex)");
        }

        private void textBoxEZLNTWRITEATTRIBUTECLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute ID (hex)");
        }

        private void textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("data type (hex)");
        }

        private void textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTWRITEATTRIBUTEDATA_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("data (hex)");
        }

        private void textBoxEZLNTWRITEATTRIBUTEDATA_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTIDENTIFYTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time (hex)");
        }

        private void textBoxEZLNTIDENTIFYTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTLEVEL_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("level (hex)");
        }

        private void textBoxEZLNTLEVEL_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTLEVELTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time (hex)");
        }

        private void textBoxEZLNTLEVELTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTHUE_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("hue (hex)");
        }

        private void textBoxEZLNTHUE_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTHUEDIR_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("direction (hex)");
        }

        private void textBoxEZLNTHUEDIR_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTHUETIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time (hex)");
        }

        private void textBoxEZLNTHUETIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTCOLORX_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("x (hex)");
        }

        private void textBoxEZLNTCOLORX_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTCOLORY_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("y (hex)");
        }

        private void textBoxEZLNTCOLORY_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxCOLORTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time (hex)");
        }

        private void textBoxCOLORTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTSAT_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("saturation (hex)");
        }

        private void textBoxEZLNTSAT_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTSATTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time (hex)");
        }

        private void textBoxEZLNTSATTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTTEMP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("tempk (hex)");
        }

        private void textBoxEZLNTTEMP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTTEMPTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time (hex)");
        }

        private void textBoxEZLNTTEMPTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTSETSTEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("step");
        }

        private void textBoxEZLNTSETSTEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTSETDIR_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Dir");
        }

        private void textBoxEZLNTSETDIR_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEZLNTSETINTERVALMAX_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("interval max (ms)");
        }

        private void textBoxEZLNTSETINTERVALMAX_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTSETLOOP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("loop");
        }

        private void textBoxLNTSETLOOP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTSETPARAMININTERVAL_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("interval min(ms)");
        }

        private void textBoxLNTSETPARAMININTERVAL_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTSETPARAMAXINTERVAL_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("interval max(ms)");
        }

        private void textBoxLNTSETPARAMAXINTERVAL_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTSETPARASTEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("step");
        }

        private void textBoxLNTSETPARASTEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTSETPARADIR_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("direction");
        }

        private void textBoxLNTSETPARADIR_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTREADATTRCLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID");
        }

        private void textBoxLNTREADATTRCLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTREADATTRATTRIBUTEID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute ID");
        }

        private void textBoxLNTREADATTRATTRIBUTEID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTREADATTRATTRIBUTECOUNT_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute count");
        }

        private void textBoxLNTREADATTRATTRIBUTECOUNT_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTWRITEATTRCLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID");
        }

        private void textBoxLNTWRITEATTRCLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTWRITEATTRATTRID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute ID");

        }

        private void textBoxLNTWRITEATTRATTRID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTWRITEATTRDATATYPE_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTWRITEATTRDATATYPE_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("data type");

        }

        private void textBoxLNTWRITEATTRDATA_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("data");

        }

        private void textBoxLNTWRITEATTRDATA_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTBINDIEEEADDR_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID");

        }

        private void textBoxLNTBINDIEEEADDR_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTUNBINDIEEEADDR_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID");

        }

        private void textBoxLNTUNBINDIEEEADDR_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTCONFIGRPRTCLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID");

        }

        private void textBoxLNTCONFIGRPRTCLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTCONFIGRPRTTYPE_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("type");

        }

        private void textBoxLNTCONFIGRPRTTYPE_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxCONFIGRPRTATTRID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute ID");

        }

        private void textBoxCONFIGRPRTATTRID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxCONFIGRPRTMININTERVAL_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("interval min");

        }

        private void textBoxCONFIGRPRTMININTERVAL_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxCONFIGRPRTMAXRPRTINTERVAL_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("interval max");

        }

        private void textBoxCONFIGRPRTMAXRPRTINTERVAL_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTCONFIGRPRTTIMEOUT_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("timeout");

        }

        private void textBoxLNTCONFIGRPRTTIMEOUT_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTCONFIGRPRTCHANGE_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("data change");

        }

        private void textBoxLNTCONFIGRPRTCHANGE_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTREADRPRTCLUSTERID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("cluster ID");

        }

        private void textBoxLNTREADRPRTCLUSTERID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTREADRPRTATTRID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("attribute ID");

        }

        private void textBoxLNTREADRPRTATTRID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTIDENTIFYTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time");

        }

        private void textBoxLNTIDENTIFYTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTADDGROUPADDR_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("address");

        }

        private void textBoxLNTADDGROUPADDR_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTREMOVEGROUPADDRESS_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("address");
        }

        private void textBoxLNTREMOVEGROUPADDRESS_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTVIEWGROUPADDRESS_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("address");
        }

        private void textBoxLNTVIEWGROUPADDRESS_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTLEVEL_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("level");
        }

        private void textBoxLNTLEVEL_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTLEVELTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time");
        }

        private void textBoxLNTLEVELTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTHUE_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("hue");
        }

        private void textBoxLNTHUE_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTHUEDIR_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("direction");
        }

        private void textBoxLNTHUEDIR_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTHUETIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time");
        }

        private void textBoxLNTHUETIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTCOLORX_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("color X");
        }

        private void textBoxLNTCOLORX_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTCOLORY_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("color Y");
        }

        private void textBoxLNTCOLORY_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTCOLORTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time");
        }

        private void textBoxLNTCOLORTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTSAT_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("saturation");
        }

        private void textBoxLNTSAT_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTSATTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time");
        }

        private void textBoxLNTSATTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTTEMP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("temp");
        }

        private void textBoxLNTTEMP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLNTTEMPTIME_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("time");
        }

        private void textBoxLNTTEMPTIME_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        #endregion

        #region GroupClusterTab

        #region Add Group

        private void textBoxAddGroupAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destionation Address (16-bit Hex)");
        }

        private void textBoxAddGroupAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddGroupSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxAddGroupSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddGroupDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxAddGroupDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddGroupGroupAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxAddGroupGroupAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGroupNameLength_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group Name Length (8-bit Hex)");
        }

        private void textBoxGroupNameLength_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGroupNameMaxLength_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group Name Maximum Length (8-bit Hex)");
        }

        private void textBoxGroupNameMaxLength_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGroupName_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group Name (String)");
        }

        private void textBoxGroupName_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region View Group

        private void textBoxViewGroupAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxViewGroupAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxViewGroupSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxViewGroupSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxViewGroupDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxViewGroupDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxViewGroupGroupAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxViewGroupGroupAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Get Group

        private void textBoxGetGroupTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxGetGroupTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGetGroupSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxGetGroupSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGetGroupDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxGetGroupDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGetGroupCount_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group Count");
        }

        private void textBoxGetGroupCount_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Remove Group

        private void textBoxRemoveGroupTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxRemoveGroupTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveGroupSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxRemoveGroupSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveGroupDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxRemoveGroupDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveGroupGroupAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxRemoveGroupGroupAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Remove All

        private void textBoxRemoveAllGroupTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxRemoveAllGroupTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveAllGroupSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxRemoveAllGroupSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveAllGroupDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxRemoveAllGroupDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Add If Identified

        private void textBoxGroupAddIfIndentifyingTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxGroupAddIfIndentifyingTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGroupAddIfIdentifySrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxGroupAddIfIdentifySrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGroupAddIfIdentifyDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxGroupAddIfIdentifyDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGroupAddIfIdentifyGroupID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxGroupAddIfIdentifyGroupID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region IdentifyClusterTab

#region ID Send

        private void textBoxSendIdAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxSendIdAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxSendIdSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxSendIdSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIdSendDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxIdSendDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIdSendTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Time (16-bit Hex)");
        }

        private void textBoxIdSendTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region ID Query

        private void textBoxIdQueryAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxIdQueryAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIdQuerySrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxIdQuerySrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxIdQueryDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxIdQueryDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region LevelClusterTab

#region Move To Level

        private void comboBoxMoveToLevelAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxMoveToLevelAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToLevelAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxMoveToLevelAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToLevelSrcEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToLevelSrcEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToLevelDstEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToLevelDstEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxMoveToLevelOnOff_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("With or Without OnOff");
        }

        private void comboBoxMoveToLevelOnOff_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToLevelLevel_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Level (8-bit Hex)");
        }

        private void textBoxMoveToLevelLevel_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToLevelTransTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Time (8-bit Hex)");
        }

        private void textBoxMoveToLevelTransTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region OnOffClusterTab

#region OnOff

        private void comboBoxOnOffAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxOnOffAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOnOffAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxOnOffAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOnOffSrcEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxOnOffSrcEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOnOffDstEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxOnOffDstEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxOnOffCommand_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("On/Off/Toggle");
        }

        private void comboBoxOnOffCommand_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region ScenesClusterTab

#region View Scene

        private void comboBoxViewSceneAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxViewSceneAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxViewSceneAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxViewSceneAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxViewSceneSrcEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxViewSceneSrcEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxViewSceneDstEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxViewSceneDstEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxViewSceneGroupId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxViewSceneGroupId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxViewSceneSceneId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Scene ID (8-bit Hex)");
        }

        private void textBoxViewSceneSceneId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Add Scene

        private void comboBoxAddSceneAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxAddSceneAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxAddSceneAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneSrcEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxAddSceneSrcEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneDstEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxAddSceneDstEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneGroupId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxAddSceneGroupId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneSceneId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Scene ID (8-bit Hex)");
        }

        private void textBoxAddSceneSceneId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneTransTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Transition Time (16-bit Hex)");
        }

        private void textBoxAddSceneTransTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneName_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Scene Name (String)");
        }

        private void textBoxAddSceneName_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneNameLen_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Scene Name Length (8-bit Hex)");
        }

        private void textBoxAddSceneNameLen_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneMaxNameLen_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Scene Name Maximum Length (8-bit Hex)");
        }

        private void textBoxAddSceneMaxNameLen_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneExtLen_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Extension Field Length (16-bit Hex)");
        }

        private void textBoxAddSceneExtLen_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxAddSceneData_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Extension Field Data (8-bit Hex)");
        }

        private void textBoxAddSceneData_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Store Scene

        private void comboBoxStoreSceneAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxStoreSceneAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxStoreSceneAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxStoreSceneAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxStoreSceneSrcEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxStoreSceneSrcEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxStoreSceneDstEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxStoreSceneDstEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxStoreSceneGroupId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxStoreSceneGroupId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxStoreSceneSceneId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Scene ID (8-bit Hex)");
        }

        private void textBoxStoreSceneSceneId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Recall Scene

        private void comboBoxRecallSceneAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxRecallSceneAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRecallSceneAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxRecallSceneAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRecallSceneSrcEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxRecallSceneSrcEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRecallSceneDstEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxRecallSceneDstEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRecallSceneGroupId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxRecallSceneGroupId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRecallSceneSceneId_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Scene ID (8-bit Hex)");
        }

        private void textBoxRecallSceneSceneId_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Get Member

        private void comboBoxGetSceneMembershipAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxGetSceneMembershipAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGetSceneMembershipAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxGetSceneMembershipAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGetSceneMembershipSrcEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxGetSceneMembershipSrcEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGetSceneMembershipDstEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxGetSceneMembershipDstEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxGetSceneMembershipGroupID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxGetSceneMembershipGroupID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Remove All

        private void comboBoxRemoveAllScenesAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxRemoveAllScenesAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveAllScenesAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxRemoveAllScenesAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveAllScenesSrcEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxRemoveAllScenesSrcEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveAllScenesDstEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxRemoveAllScenesDstEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveAllScenesGroupID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxRemoveAllScenesGroupID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Remove

        private void comboBoxRemoveSceneAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxRemoveSceneAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveSceneAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxRemoveSceneAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveSceneSrcEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxRemoveSceneSrcEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveSceneDstEndPoint_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxRemoveSceneDstEndPoint_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveSceneGroupID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Group ID (16-bit Hex)");
        }

        private void textBoxRemoveSceneGroupID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxRemoveSceneSceneID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Scene ID (8-bit Hex)");
        }

        private void textBoxRemoveSceneSceneID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region ColorClusterTab

#region Move to Hue

        private void textBoxMoveToHueAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxMoveToHueAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToHueSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToHueSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToHueDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToHueDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToHueHue_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Hue (8-bit Hex)");
        }

        private void textBoxMoveToHueHue_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToHueDir_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Direction (8-bit Hex)");
        }

        private void textBoxMoveToHueDir_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToHueTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Transition Time (16-bit Hex)");
        }

        private void textBoxMoveToHueTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Move to Color

        private void textBoxMoveToColorAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxMoveToColorAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToColorSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToColorSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToColorDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToColorDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToColorX_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("X Value (16-bit Hex)");
        }

        private void textBoxMoveToColorX_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToColorY_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Y Value (16-bit Hex)");
        }

        private void textBoxMoveToColorY_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToColorTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Transition Time (16-bit Hex)");
        }

        private void textBoxMoveToColorTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Move to Saturation

        private void textBoxMoveToSatAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxMoveToSatAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToSatSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToSatSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToSatDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToSatDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToSatSat_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Saturation (8-bit Hex)");
        }

        private void textBoxMoveToSatSat_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToSatTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Transition Time (16-bit Hex)");
        }

        private void textBoxMoveToSatTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region Move to Temperature

        private void textBoxMoveToColorTempAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxMoveToColorTempAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToColorTempSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToColorTempSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToColorTempDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxMoveToColorTempDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToColorTempTemp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Temperature (16-bit Dec)");
        }

        private void textBoxMoveToColorTempTemp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxMoveToColorTempRate_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Transition Time (16-bit Hex)");
        }

        private void textBoxMoveToColorTempRate_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region DoorLockClusterTab

#region LockUnlock

        private void textBoxLockUnlockAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxLockUnlockAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLockUnlockSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxLockUnlockSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxLockUnlockDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxLockUnlockDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxLockUnlock_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Lock or Unlock");
        }

        private void comboBoxLockUnlock_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region IASClusterTab

#region Enroll Rsp

        private void comboBoxEnrollRspAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxEnrollRspAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEnrollRspAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxEnrollRspAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEnrollRspSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxEnrollRspSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEnrollRspDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxEnrollRspDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxEnrollRspCode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Response");
        }

        private void comboBoxEnrollRspCode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxEnrollRspZone_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Zone ID (8-bit Hex)");
        }

        private void textBoxEnrollRspZone_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region ZLLOnOffClusterTab

#region OnOff Effects

        private void textBoxZllOnOffEffectsAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxZllOnOffEffectsAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxZllOnOffEffectsSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxZllOnOffEffectsSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxZllOnOffEffectsDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void comboBoxZllOnOffEffectID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("On/Off/Toggle");
        }

        private void comboBoxZllOnOffEffectID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxZllOnOffEffectsGradient_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Gradient (8-bit Hex)");
        }

        private void textBoxZllOnOffEffectsGradient_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region ZLLColorClusterTab

#region Move to Hue

        private void textBoxZllMoveToHueAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxZllMoveToHueAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxZllMoveToHueSrcEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxZllMoveToHueSrcEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxZllMoveToHueDstEp_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxZllMoveToHueDstEp_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxZllMoveToHueHue_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Hue (16-bit Hex)");
        }

        private void textBoxZllMoveToHueHue_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxZllMoveToHueDirection_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Direction (16-bit Hex)");
        }

        private void textBoxZllMoveToHueDirection_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxZllMoveToHueTransTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Transition Time (16-bit Hex)");
        }

        private void textBoxZllMoveToHueTransTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region OTAClusterTab

#region Image Notify

        private void comboBoxOTAImageNotifyAddrMode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Address Mode");
        }

        private void comboBoxOTAImageNotifyAddrMode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTAImageNotifyTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxOTAImageNotifyTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTAImageNotifySrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxOTAImageNotifySrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTAImageNotifyDstEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Endpoint (8-bit Hex)");
        }

        private void textBoxOTAImageNotifyDstEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void comboBoxOTAImageNotifyType_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Jitter Options");
        }

        private void comboBoxOTAImageNotifyType_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTAImageNotifyFileVersion_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Version Number (32-bit Hex)");
        }

        private void textBoxOTAImageNotifyFileVersion_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTAImageNotifyImageType_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Image Type (16-bit Hex)");
        }

        private void textBoxOTAImageNotifyImageType_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTAImageNotifyManuID_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Manufacturer ID (16-bit Hex)");
        }

        private void textBoxOTAImageNotifyManuID_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTAImageNotifyJitter_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Query Jitter (8-bit Hex)");
        }

        private void textBoxOTAImageNotifyJitter_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#region WaitParams

        private void textBoxOTASetWaitForDataParamsTargetAddr_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Destination Address (16-bit Hex)");
        }

        private void textBoxOTASetWaitForDataParamsTargetAddr_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTASetWaitForDataParamsSrcEP_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Source Endpoint (8-bit Hex)");
        }

        private void textBoxOTASetWaitForDataParamsSrcEP_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTASetWaitForDataParamsCurrentTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Current Time (32-bit Hex)");
        }

        private void textBoxOTASetWaitForDataParamsCurrentTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTASetWaitForDataParamsRequestTime_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Request Time (32-bit Hex)");
        }

        private void textBoxOTASetWaitForDataParamsRequestTime_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxOTASetWaitForDataParamsRequestBlockDelay_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Block Delay (16-bit Hex)");
        }

        private void textBoxOTASetWaitForDataParamsRequestBlockDelay_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

#endregion

#endregion

#region InstallCodeTab

#region InstallCodeSend
       
        private void textBoxInstallCodeMACaddres_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("MACaddress(64bit-hex)");
        }

        private void textBoxInstallCodeMACaddres_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxInstallCodeCode_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("InstallCode(128bit-hex)");
        }

        private void textBoxInstallCodeCode_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }



#endregion

#endregion

#region Text Change

        private void textBoxRawDataCommandsData_TextChanged(object sender, EventArgs e)
        {
            if (textBoxRawDataCommandsData.Text != "Raw Data (Format: Byte:Byte:Byte)")
            {
                String rawData = "";
                int iRawDataSize;

                rawData = textBoxRawDataCommandsData.Text;

                rawData = rawData.Replace(" ", "");
                rawData = rawData.Replace(":", "");

                iRawDataSize = rawData.Length;
                for (int i = iRawDataSize; i > 0; i--)
                {
                    if ((i % 2) == 0)
                    {
                        if (i != iRawDataSize)
                        {
                            rawData = rawData.Insert(i, ":");
                        }
                    }
                }

                textBoxRawDataCommandsData.Text = rawData;
                textBoxRawDataCommandsData.SelectionStart = textBoxRawDataCommandsData.Text.Length;
            }
        }

        private void textBoxOOBDataKey_TextChanged(object sender, EventArgs e)
        {
            if (textBoxOOBDataKey.Text != "Key (Format: Byte:Byte:Byte)")
            {
                String rawData = "";
                int iRawDataSize;

                rawData = textBoxOOBDataKey.Text;

                rawData = rawData.Replace(" ", "");
                rawData = rawData.Replace(":", "");

                iRawDataSize = rawData.Length;
                for (int i = iRawDataSize; i > 0; i--)
                {
                    if ((i % 2) == 0)
                    {
                        if (i != iRawDataSize)
                        {
                            rawData = rawData.Insert(i, ":");
                        }
                    }
                }

                textBoxOOBDataKey.Text = rawData;
                textBoxOOBDataKey.SelectionStart = textBoxOOBDataKey.Text.Length;
            }
        }

        private void textBoxAddSceneData_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAddSceneData.Text != "Data (Format: Byte:Byte:Byte)")
            {
                String rawData = "";
                int iRawDataSize;

                rawData = textBoxAddSceneData.Text;

                rawData = rawData.Replace(" ", "");
                rawData = rawData.Replace(":", "");

                iRawDataSize = rawData.Length;
                for (int i = iRawDataSize; i > 0; i--)
                {
                    if ((i % 2) == 0)
                    {
                        if (i != iRawDataSize)
                        {
                            rawData = rawData.Insert(i, ":");
                        }
                    }
                }

                textBoxAddSceneData.Text = rawData;
                textBoxAddSceneData.SelectionStart = textBoxAddSceneData.Text.Length;
            }
        }

        private void richTextBoxMessageView_TextChanged(object sender, EventArgs e)
        {
            richTextBoxMessageView.SelectionStart = richTextBoxCommandResponse.Text.Length;
            richTextBoxMessageView.ScrollToCaret();
        }

        private void richTextBoxCommandResponse_TextChanged(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.SelectionStart = richTextBoxCommandResponse.Text.Length;
            richTextBoxCommandResponse.ScrollToCaret();
        }

#endregion

#region List Management

        private void buttonAddToList_Click(object sender, EventArgs e)
        {
            if (listManager.ShowDialog() == DialogResult.OK)
            {

            }
        }

#endregion

#endregion

#region Clear Grey Text

#region ManagementTab

#region Set EPID

        private void textBoxSetEPID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxSetEPID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxSetEPID.ForeColor = System.Drawing.Color.Black;
                textBoxSetEPID.Text = "";
            }
        }

#endregion

#region Set CMSK

        private void textBoxSetCMSK_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxSetCMSK.ForeColor != System.Drawing.Color.Black)
            {
                textBoxSetCMSK.ForeColor = System.Drawing.Color.Black;
                textBoxSetCMSK.Text = "";
            }
        }

#endregion

#region Set Security Key

        private void textBoxSetSecurityKeySeqNbr_Click(object sender, EventArgs e)
        {
            if (textBoxSetSecurityKeySeqNbr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxSetSecurityKeySeqNbr.ForeColor = System.Drawing.Color.Black;
                textBoxSetSecurityKeySeqNbr.Text = "";
            }
        }

#endregion

#region Management Leave

        private void textBoxMgmtLeaveAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMgmtLeaveAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMgmtLeaveAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMgmtLeaveAddr.Text = "";
            }
        }

        private void textBoxMgmtLeaveExtAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMgmtLeaveExtAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMgmtLeaveExtAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMgmtLeaveExtAddr.Text = "";
            }
        }

#endregion

#region Leave

        private void textBoxLeaveAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLeaveAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLeaveAddr.ForeColor = System.Drawing.Color.Black;
                textBoxLeaveAddr.Text = "";
            }
        }

#endregion

#region Remove

        private void textBoxRemoveParentAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveParentAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveParentAddr.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveParentAddr.Text = "";
            }
        }

        private void textBoxRemoveChildAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveChildAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveChildAddr.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveChildAddr.Text = "";
            }
        }

#endregion

#region Permit Join

        private void textBoxPermitJoinAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxPermitJoinAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxPermitJoinAddr.ForeColor = System.Drawing.Color.Black;
                textBoxPermitJoinAddr.Text = "";
            }
        }

        private void textBoxPermitJoinInterval_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxPermitJoinInterval.ForeColor != System.Drawing.Color.Black)
            {
                textBoxPermitJoinInterval.ForeColor = System.Drawing.Color.Black;
                textBoxPermitJoinInterval.Text = "";
            }
        }

#endregion

#region Match Req

        private void textBoxMatchReqAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMatchReqAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMatchReqAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMatchReqAddr.Text = "";
            }
        }

        private void textBoxMatchReqProfileID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMatchReqProfileID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMatchReqProfileID.ForeColor = System.Drawing.Color.Black;
                textBoxMatchReqProfileID.Text = "";
            }
        }

        private void textBoxMatchReqNbrInputClusters_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMatchReqNbrInputClusters.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMatchReqNbrInputClusters.ForeColor = System.Drawing.Color.Black;
                textBoxMatchReqNbrInputClusters.Text = "";
            }
        }

        private void textBoxMatchReqInputClusters_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMatchReqInputClusters.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMatchReqInputClusters.ForeColor = System.Drawing.Color.Black;
                textBoxMatchReqInputClusters.Text = "";
            }
        }

        private void textBoxMatchReqNbrOutputClusters_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMatchReqNbrOutputClusters.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMatchReqNbrOutputClusters.ForeColor = System.Drawing.Color.Black;
                textBoxMatchReqNbrOutputClusters.Text = "";
            }
        }

        private void textBoxMatchReqOutputClusters_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMatchReqOutputClusters.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMatchReqOutputClusters.ForeColor = System.Drawing.Color.Black;
                textBoxMatchReqOutputClusters.Text = "";
            }
        }

#endregion

#region Bind

        private void textBoxBindTargetExtAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxBindTargetExtAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxBindTargetExtAddr.ForeColor = System.Drawing.Color.Black;
                textBoxBindTargetExtAddr.Text = "";
            }
        }

        private void textBoxBindTargetEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxBindTargetEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxBindTargetEP.ForeColor = System.Drawing.Color.Black;
                textBoxBindTargetEP.Text = "";
            }
        }

        private void textBoxBindDestAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxBindDestAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxBindDestAddr.ForeColor = System.Drawing.Color.Black;
                textBoxBindDestAddr.Text = "";
            }
        }

        private void textBoxBindClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxBindClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxBindClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxBindClusterID.Text = "";
            }
        }

        private void textBoxBindDestEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxBindDestEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxBindDestEP.ForeColor = System.Drawing.Color.Black;
                textBoxBindDestEP.Text = "";
            }
        }

#endregion

#region Unbind

        private void textBoxUnBindTargetExtAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxUnBindTargetExtAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxUnBindTargetExtAddr.ForeColor = System.Drawing.Color.Black;
                textBoxUnBindTargetExtAddr.Text = "";
            }
        }

        private void textBoxUnBindTargetEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxUnBindTargetEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxUnBindTargetEP.ForeColor = System.Drawing.Color.Black;
                textBoxUnBindTargetEP.Text = "";
            }
        }

        private void textBoxUnBindDestAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxUnBindDestAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxUnBindDestAddr.ForeColor = System.Drawing.Color.Black;
                textBoxUnBindDestAddr.Text = "";
            }
        }

        private void textBoxUnBindClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxUnBindClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxUnBindClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxUnBindClusterID.Text = "";
            }
        }

        private void textBoxUnBindDestEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxUnBindDestEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxUnBindDestEP.ForeColor = System.Drawing.Color.Black;
                textBoxUnBindDestEP.Text = "";
            }
        }

#endregion

#region Active Req

        private void textBoxActiveEpAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxActiveEpAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxActiveEpAddr.ForeColor = System.Drawing.Color.Black;
                textBoxActiveEpAddr.Text = "";
            }
        }

#endregion

#region IEEE Req

        private void textBoxIeeeReqTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIeeeReqTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIeeeReqTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxIeeeReqTargetAddr.Text = "";
            }
        }

        private void textBoxIeeeReqAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIeeeReqAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIeeeReqAddr.ForeColor = System.Drawing.Color.Black;
                textBoxIeeeReqAddr.Text = "";
            }
        }

        private void textBoxIeeeReqStartIndex_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIeeeReqStartIndex.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIeeeReqStartIndex.ForeColor = System.Drawing.Color.Black;
                textBoxIeeeReqStartIndex.Text = "";
            }
        }

#endregion

#region NWK Address Req

        private void textBoxNwkAddrReqTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxNwkAddrReqTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxNwkAddrReqTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxNwkAddrReqTargetAddr.Text = "";
            }
        }

#endregion

#region Node Req

        private void textBoxNodeDescReq_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxNodeDescReq.ForeColor != System.Drawing.Color.Black)
            {
                textBoxNodeDescReq.ForeColor = System.Drawing.Color.Black;
                textBoxNodeDescReq.Text = "";
            }
        }

#endregion

#region Power Req

        private void textBoxPowerReqAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxPowerReqAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxPowerReqAddr.ForeColor = System.Drawing.Color.Black;
                textBoxPowerReqAddr.Text = "";
            }
        }

#endregion

#region Simple Req

        private void textBoxSimpleReqAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxSimpleReqAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxSimpleReqAddr.ForeColor = System.Drawing.Color.Black;
                textBoxSimpleReqAddr.Text = "";
            }
        }

        private void textBoxSimpleReqEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxSimpleReqEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxSimpleReqEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxSimpleReqEndPoint.Text = "";
            }
        }

#endregion

#region Complex Req

        private void textBoxComplexReqAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxComplexReqAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxComplexReqAddr.ForeColor = System.Drawing.Color.Black;
                textBoxComplexReqAddr.Text = "";
            }
        }

#endregion

#region User Req

        private void textBoxUserReqAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxUserReqAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxUserReqAddr.ForeColor = System.Drawing.Color.Black;
                textBoxUserReqAddr.Text = "";
            }
        }

#endregion

#region User Set Req

        private void textBoxUserSetReqAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxUserSetReqAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxUserSetReqAddr.ForeColor = System.Drawing.Color.Black;
                textBoxUserSetReqAddr.Text = "";
            }
        }

        private void textBoxUserSetReqDescription_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxUserSetReqDescription.ForeColor != System.Drawing.Color.Black)
            {
                textBoxUserSetReqDescription.ForeColor = System.Drawing.Color.Black;
                textBoxUserSetReqDescription.Text = "";
            }
        }

#endregion

#region LQI Req

        private void textBoxLqiReqTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLqiReqTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLqiReqTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxLqiReqTargetAddr.Text = "";
            }
        }

        private void textBoxLqiReqStartIndex_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLqiReqStartIndex.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLqiReqStartIndex.ForeColor = System.Drawing.Color.Black;
                textBoxLqiReqStartIndex.Text = "";
            }
        }

#endregion

#region Restore NWK Frame Counter

        private void textBoxRestoreNwkFrameCounter_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRestoreNwkFrameCounter.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRestoreNwkFrameCounter.ForeColor = System.Drawing.Color.Black;
                textBoxRestoreNwkFrameCounter.Text = "";
            }
        }

#endregion

#endregion

#region GeneralTab

#region Read Attribute

        private void textBoxReadAttribTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAttribTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAttribTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxReadAttribTargetAddr.Text = "";
            }
        }

        private void textBoxReadAttribSrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAttribSrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAttribSrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxReadAttribSrcEP.Text = "";
            }
        }

        private void textBoxReadAttribDstEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAttribDstEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAttribDstEP.ForeColor = System.Drawing.Color.Black;
                textBoxReadAttribDstEP.Text = "";
            }
        }

        private void textBoxReadAttribClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAttribClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAttribClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxReadAttribClusterID.Text = "";
            }
        }

        private void textBoxReadAttribCount_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAttribCount.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAttribCount.ForeColor = System.Drawing.Color.Black;
                textBoxReadAttribCount.Text = "";
            }
        }

        private void textBoxReadAttribID1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAttribID1.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAttribID1.ForeColor = System.Drawing.Color.Black;
                textBoxReadAttribID1.Text = "";
            }
        }

        private void textBoxReadAttribManuID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAttribManuID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAttribManuID.ForeColor = System.Drawing.Color.Black;
                textBoxReadAttribManuID.Text = "";
            }
        }

#endregion
        
#region Write Attribute

        private void textBoxWriteAttribTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxWriteAttribTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxWriteAttribTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxWriteAttribTargetAddr.Text = "";
            }
        }

        private void textBoxWriteAttribSrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxWriteAttribSrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxWriteAttribSrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxWriteAttribSrcEP.Text = "";
            }
        }

        private void textBoxWriteAttribDstEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxWriteAttribDstEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxWriteAttribDstEP.ForeColor = System.Drawing.Color.Black;
                textBoxWriteAttribDstEP.Text = "";
            }
        }

        private void textBoxWriteAttribClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxWriteAttribClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxWriteAttribClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxWriteAttribClusterID.Text = "";
            }
        }

        private void textBoxWriteAttribID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxWriteAttribID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxWriteAttribID.ForeColor = System.Drawing.Color.Black;
                textBoxWriteAttribID.Text = "";
            }
        }

        private void textBoxWriteAttribDataType_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxWriteAttribDataType.ForeColor != System.Drawing.Color.Black)
            {
                textBoxWriteAttribDataType.ForeColor = System.Drawing.Color.Black;
                textBoxWriteAttribDataType.Text = "";
            }
        }

        private void textBoxWriteAttribData_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxWriteAttribData.ForeColor != System.Drawing.Color.Black)
            {
                textBoxWriteAttribData.ForeColor = System.Drawing.Color.Black;
                textBoxWriteAttribData.Text = "";
            }
        }

        private void textBoxWriteAttribManuID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxWriteAttribManuID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxWriteAttribManuID.ForeColor = System.Drawing.Color.Black;
                textBoxWriteAttribManuID.Text = "";
            }
        }

#endregion

#region Config Report

        private void textBoxConfigReportTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportTargetAddr.Text = "";
            }
        }

        private void textBoxConfigReportSrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportSrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportSrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportSrcEP.Text = "";
            }
        }

        private void textBoxConfigReportDstEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportDstEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportDstEP.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportDstEP.Text = "";
            }
        }

        private void textBoxConfigReportClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportClusterID.Text = "";
            }
        }

        private void textBoxConfigReportAttribType_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportAttribType.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportAttribType.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportAttribType.Text = "";
            }
        }

        private void textBoxConfigReportAttribID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportAttribID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportAttribID.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportAttribID.Text = "";
            }
        }

        private void textBoxConfigReportMinInterval_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportMinInterval.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportMinInterval.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportMinInterval.Text = "";
            }
        }

        private void textBoxConfigReportMaxInterval_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportMaxInterval.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportMaxInterval.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportMaxInterval.Text = "";
            }
        }

        private void textBoxConfigReportTimeOut_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportTimeOut.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportTimeOut.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportTimeOut.Text = "";
            }
        }

        private void textBoxConfigReportChange_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxConfigReportChange.ForeColor != System.Drawing.Color.Black)
            {
                textBoxConfigReportChange.ForeColor = System.Drawing.Color.Black;
                textBoxConfigReportChange.Text = "";
            }
        }

#endregion

#region Read Report Config

        private void textBoxReadReportConfigTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadReportConfigTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadReportConfigTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxReadReportConfigTargetAddr.Text = "";
            }
        }

        private void textBoxReadReportConfigSrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadReportConfigSrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadReportConfigSrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxReadReportConfigSrcEP.Text = "";
            }
        }

        private void textBoxReadReportConfigDstEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadReportConfigDstEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadReportConfigDstEP.ForeColor = System.Drawing.Color.Black;
                textBoxReadReportConfigDstEP.Text = "";
            }
        }

        private void textBoxReadReportConfigClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadReportConfigClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadReportConfigClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxReadReportConfigClusterID.Text = "";
            }
        }

        private void textBoxReadReportConfigAttribID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadReportConfigAttribID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadReportConfigAttribID.ForeColor = System.Drawing.Color.Black;
                textBoxReadReportConfigAttribID.Text = "";
            }
        }

#endregion

#region Read All Attribute

        private void textBoxReadAllAttribAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAllAttribAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAllAttribAddr.ForeColor = System.Drawing.Color.Black;
                textBoxReadAllAttribAddr.Text = "";
            }
        }

        private void textBoxReadAllAttribSrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAllAttribSrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAllAttribSrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxReadAllAttribSrcEP.Text = "";
            }
        }

        private void textBoxReadAllAttribDstEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAllAttribDstEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAllAttribDstEP.ForeColor = System.Drawing.Color.Black;
                textBoxReadAllAttribDstEP.Text = "";
            }
        }

        private void textBoxReadAllAttribClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxReadAllAttribClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxReadAllAttribClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxReadAllAttribClusterID.Text = "";
            }
        }

#endregion

#region Discover Attributes

        private void textBoxDiscoverAttributesAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverAttributesAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverAttributesAddr.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverAttributesAddr.Text = "";
            }
        }

        private void textBoxDiscoverAttributesSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverAttributesSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverAttributesSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverAttributesSrcEp.Text = "";
            }
        }

        private void textBoxDiscoverAttributesDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverAttributesDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverAttributesDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverAttributesDstEp.Text = "";
            }
        }

        private void textBoxDiscoverAttributesClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverAttributesClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverAttributesClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverAttributesClusterID.Text = "";
            }
        }

        private void textBoxDiscoverAttributesStartAttrId_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverAttributesStartAttrId.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverAttributesStartAttrId.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverAttributesStartAttrId.Text = "";
            }
        }

        private void textBoxDiscoverAttributesMaxIDs_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverAttributesMaxIDs.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverAttributesMaxIDs.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverAttributesMaxIDs.Text = "";
            }
        }

#endregion

#region MTO Route Request

        private void textBoxManyToOneRouteRequesRadius_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxManyToOneRouteRequesRadius.ForeColor != System.Drawing.Color.Black)
            {
                textBoxManyToOneRouteRequesRadius.ForeColor = System.Drawing.Color.Black;
                textBoxManyToOneRouteRequesRadius.Text = "";
            }
        }

#endregion

#region NWK Update

        private void textBoxMgmtNwkUpdateTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMgmtNwkUpdateTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMgmtNwkUpdateTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMgmtNwkUpdateTargetAddr.Text = "";
            }
        }

        private void textBoxMgmtNwkUpdateChannelMask_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMgmtNwkUpdateChannelMask.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMgmtNwkUpdateChannelMask.ForeColor = System.Drawing.Color.Black;
                textBoxMgmtNwkUpdateChannelMask.Text = "";
            }
        }

        private void textBoxMgmtNwkUpdateScanDuration_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMgmtNwkUpdateScanDuration.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMgmtNwkUpdateScanDuration.ForeColor = System.Drawing.Color.Black;
                textBoxMgmtNwkUpdateScanDuration.Text = "";
            }
        }

        private void textBoxMgmtNwkUpdateScanCount_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMgmtNwkUpdateScanCount.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMgmtNwkUpdateScanCount.ForeColor = System.Drawing.Color.Black;
                textBoxMgmtNwkUpdateScanCount.Text = "";
            }
        }

        private void textBoxMgmtNwkUpdateNwkManagerAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMgmtNwkUpdateNwkManagerAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMgmtNwkUpdateNwkManagerAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMgmtNwkUpdateNwkManagerAddr.Text = "";
            }
        }

#endregion

#region Discover Commands

        private void textBoxDiscoverCommandsTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverCommandsTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverCommandsTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverCommandsTargetAddr.Text = "";
            }
        }

        private void textBoxDiscoverCommandsSrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverCommandsSrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverCommandsSrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverCommandsSrcEP.Text = "";
            }
        }

        private void textBoxDiscoverCommandsDstEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverCommandsDstEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverCommandsDstEP.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverCommandsDstEP.Text = "";
            }
        }

        private void textBoxDiscoverCommandsClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverCommandsClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverCommandsClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverCommandsClusterID.Text = "";
            }
        }

        private void textBoxDiscoverCommandsCommandID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverCommandsCommandID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverCommandsCommandID.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverCommandsCommandID.Text = "";
            }
        }

        private void textBoxDiscoverCommandsManuID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverCommandsManuID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverCommandsManuID.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverCommandsManuID.Text = "";
            }
        }

        private void textBoxDiscoverCommandsMaxCommands_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDiscoverCommandsMaxCommands.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDiscoverCommandsMaxCommands.ForeColor = System.Drawing.Color.Black;
                textBoxDiscoverCommandsMaxCommands.Text = "";
            }
        }

#endregion

#region Raw Data Commands

        private void textBoxRawDataCommandsTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRawDataCommandsTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRawDataCommandsTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxRawDataCommandsTargetAddr.Text = "";
            }
        }

        private void textBoxRawDataCommandsSrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRawDataCommandsSrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRawDataCommandsSrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxRawDataCommandsSrcEP.Text = "";
            }
        }

        private void textBoxRawDataCommandsDstEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRawDataCommandsDstEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRawDataCommandsDstEP.ForeColor = System.Drawing.Color.Black;
                textBoxRawDataCommandsDstEP.Text = "";
            }
        }

        private void textBoxRawDataCommandsProfileID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRawDataCommandsProfileID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRawDataCommandsProfileID.ForeColor = System.Drawing.Color.Black;
                textBoxRawDataCommandsProfileID.Text = "";
            }
        }

        private void textBoxRawDataCommandsClusterID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRawDataCommandsClusterID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRawDataCommandsClusterID.ForeColor = System.Drawing.Color.Black;
                textBoxRawDataCommandsClusterID.Text = "";
            }
        }

        private void textBoxRawDataCommandsRadius_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRawDataCommandsRadius.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRawDataCommandsRadius.ForeColor = System.Drawing.Color.Black;
                textBoxRawDataCommandsRadius.Text = "";
            }
        }

        private void textBoxRawDataCommandsSecurityMode_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRawDataCommandsSecurityMode.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRawDataCommandsSecurityMode.ForeColor = System.Drawing.Color.Black;
                textBoxRawDataCommandsSecurityMode.Text = "";
            }
        }

        private void textBoxRawDataCommandsData_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRawDataCommandsData.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRawDataCommandsData.ForeColor = System.Drawing.Color.Black;
                textBoxRawDataCommandsData.Text = "";
            }
        }

#endregion

#region OOB Data

        private void textBoxOOBDataAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOOBDataAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOOBDataAddr.ForeColor = System.Drawing.Color.Black;
                textBoxOOBDataAddr.Text = "";
            }
        }

        private void textBoxOOBDataKey_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOOBDataKey.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOOBDataKey.ForeColor = System.Drawing.Color.Black;
                textBoxOOBDataKey.Text = "";
            }
        }

#endregion

#region InstallCodeSend

        private void textBoxGeneralInstallCodeMACaddress_Click(object sender, EventArgs e)
        {
            if (textBoxGeneralInstallCodeMACaddress.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGeneralInstallCodeMACaddress.ForeColor = System.Drawing.Color.Black;
                textBoxGeneralInstallCodeMACaddress.Text = "";
            }
        }


        private void textBoxGeneralInstallCodeCode_Click(object sender, EventArgs e)
        {
            if (textBoxGeneralInstallCodeCode.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGeneralInstallCodeCode.ForeColor = System.Drawing.Color.Black;
                textBoxGeneralInstallCodeCode.Text = "";
            }
        }

 

#endregion

#endregion

#region AHIControlTab

#region DIO Set Direction

        private void textBoxDioSetDirectionInputPinMask_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDioSetDirectionInputPinMask.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDioSetDirectionInputPinMask.ForeColor = System.Drawing.Color.Black;
                textBoxDioSetDirectionInputPinMask.Text = "";
            }
        }

        private void textBoxDioSetDirectionOutputPinMask_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDioSetDirectionOutputPinMask.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDioSetDirectionOutputPinMask.ForeColor = System.Drawing.Color.Black;
                textBoxDioSetDirectionOutputPinMask.Text = "";
            }
        }

#endregion

#region DIO Set Output

        private void textBoxDioSetOutputOnPinMask_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDioSetOutputOnPinMask.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDioSetOutputOnPinMask.ForeColor = System.Drawing.Color.Black;
                textBoxDioSetOutputOnPinMask.Text = "";
            }
        }

        private void textBoxDioSetOutputOffPinMask_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxDioSetOutputOffPinMask.ForeColor != System.Drawing.Color.Black)
            {
                textBoxDioSetOutputOffPinMask.ForeColor = System.Drawing.Color.Black;
                textBoxDioSetOutputOffPinMask.Text = "";
            }
        }

#endregion

#region IPN Config

        private void textBoxIPNConfigDioRfActiveOutDioMask_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIPNConfigDioRfActiveOutDioMask.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIPNConfigDioRfActiveOutDioMask.ForeColor = System.Drawing.Color.Black;
                textBoxIPNConfigDioRfActiveOutDioMask.Text = "";
            }
        }

        private void textBoxIPNConfigDioStatusOutDioMask_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIPNConfigDioStatusOutDioMask.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIPNConfigDioStatusOutDioMask.ForeColor = System.Drawing.Color.Black;
                textBoxIPNConfigDioStatusOutDioMask.Text = "";
            }
        }

        private void textBoxIPNConfigDioTxConfInDioMask_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIPNConfigDioTxConfInDioMask.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIPNConfigDioTxConfInDioMask.ForeColor = System.Drawing.Color.Black;
                textBoxIPNConfigDioTxConfInDioMask.Text = "";
            }
        }

        private void textBoxIPNConfigPollPeriod_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIPNConfigPollPeriod.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIPNConfigPollPeriod.ForeColor = System.Drawing.Color.Black;
                textBoxIPNConfigPollPeriod.Text = "";
            }
        }

#endregion

#region AHI Tx Power

        private void textBoxAHITxPower_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAHITxPower.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAHITxPower.ForeColor = System.Drawing.Color.Black;
                textBoxAHITxPower.Text = "";
            }
        }

#endregion

#endregion

#region BasicClusterTab

#region Reset to FD

        private void textBoxBasicResetTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxBasicResetTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxBasicResetTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxBasicResetTargetAddr.Text = "";
            }
        }

        private void textBoxBasicResetSrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxBasicResetSrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxBasicResetSrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxBasicResetSrcEP.Text = "";
            }
        }

        private void textBoxBasicResetDstEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxBasicResetDstEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxBasicResetDstEP.ForeColor = System.Drawing.Color.Black;
                textBoxBasicResetDstEP.Text = "";
            }
        }

#endregion

#endregion

#region GroupClusterTab

#region Add Group

        private void textBoxAddGroupAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddGroupAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddGroupAddr.ForeColor = System.Drawing.Color.Black;
                textBoxAddGroupAddr.Text = "";
            }
        }

        private void textBoxAddGroupSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddGroupSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddGroupSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxAddGroupSrcEp.Text = "";
            }
        }

        private void textBoxAddGroupDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddGroupDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddGroupDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxAddGroupDstEp.Text = "";
            }
        }

        private void textBoxAddGroupGroupAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddGroupGroupAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddGroupGroupAddr.ForeColor = System.Drawing.Color.Black;
                textBoxAddGroupGroupAddr.Text = "";
            }
        }

        private void textBoxGroupNameLength_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGroupNameLength.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGroupNameLength.ForeColor = System.Drawing.Color.Black;
                textBoxGroupNameLength.Text = "";
            }
        }

        private void textBoxGroupNameMaxLength_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGroupNameMaxLength.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGroupNameMaxLength.ForeColor = System.Drawing.Color.Black;
                textBoxGroupNameMaxLength.Text = "";
            }
        }

        private void textBoxGroupName_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGroupName.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGroupName.ForeColor = System.Drawing.Color.Black;
                textBoxGroupName.Text = "";
            }
        }

#endregion

#region View Group

        private void textBoxViewGroupAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxViewGroupAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxViewGroupAddr.ForeColor = System.Drawing.Color.Black;
                textBoxViewGroupAddr.Text = "";
            }
        }

        private void textBoxViewGroupSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxViewGroupSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxViewGroupSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxViewGroupSrcEp.Text = "";
            }
        }

        private void textBoxViewGroupDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxViewGroupDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxViewGroupDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxViewGroupDstEp.Text = "";
            }
        }

        private void textBoxViewGroupGroupAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxViewGroupGroupAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxViewGroupGroupAddr.ForeColor = System.Drawing.Color.Black;
                textBoxViewGroupGroupAddr.Text = "";
            }
        }

#endregion

#region Get Group

        private void textBoxGetGroupTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGetGroupTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGetGroupTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxGetGroupTargetAddr.Text = "";
            }
        }

        private void textBoxGetGroupSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGetGroupSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGetGroupSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxGetGroupSrcEp.Text = "";
            }
        }

        private void textBoxGetGroupDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGetGroupDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGetGroupDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxGetGroupDstEp.Text = "";
            }
        }

        private void textBoxGetGroupCount_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGetGroupCount.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGetGroupCount.ForeColor = System.Drawing.Color.Black;
                textBoxGetGroupCount.Text = "";
            }
        }

#endregion

#region Remove Group

        private void textBoxRemoveGroupTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveGroupTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveGroupTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveGroupTargetAddr.Text = "";
            }
        }

        private void textBoxRemoveGroupSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveGroupSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveGroupSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveGroupSrcEp.Text = "";
            }
        }

        private void textBoxRemoveGroupDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveGroupDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveGroupDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveGroupDstEp.Text = "";
            }
        }

        private void textBoxRemoveGroupGroupAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveGroupGroupAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveGroupGroupAddr.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveGroupGroupAddr.Text = "";
            }
        }

#endregion

#region Remove All

        private void textBoxRemoveAllGroupTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveAllGroupTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveAllGroupTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveAllGroupTargetAddr.Text = "";
            }
        }

        private void textBoxRemoveAllGroupSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveAllGroupSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveAllGroupSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveAllGroupSrcEp.Text = "";
            }
        }

        private void textBoxRemoveAllGroupDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveAllGroupDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveAllGroupDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveAllGroupDstEp.Text = "";
            }
        }

#region Add If Identifying

        private void textBoxGroupAddIfIndentifyingTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGroupAddIfIndentifyingTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGroupAddIfIndentifyingTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxGroupAddIfIndentifyingTargetAddr.Text = "";
            }
        }

        private void textBoxGroupAddIfIdentifySrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGroupAddIfIdentifySrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGroupAddIfIdentifySrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxGroupAddIfIdentifySrcEp.Text = "";
            }
        }

        private void textBoxGroupAddIfIdentifyDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGroupAddIfIdentifyDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGroupAddIfIdentifyDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxGroupAddIfIdentifyDstEp.Text = "";
            }
        }

        private void textBoxGroupAddIfIdentifyGroupID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGroupAddIfIdentifyGroupID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGroupAddIfIdentifyGroupID.ForeColor = System.Drawing.Color.Black;
                textBoxGroupAddIfIdentifyGroupID.Text = "";
            }
        }

#endregion

#endregion

#endregion

#region IdentifyClusterTab

        private void textBoxSendIdAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxSendIdAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxSendIdAddr.ForeColor = System.Drawing.Color.Black;
                textBoxSendIdAddr.Text = "";
            }
        }

        private void textBoxSendIdSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxSendIdSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxSendIdSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxSendIdSrcEp.Text = "";
            }
        }

        private void textBoxIdSendDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIdSendDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIdSendDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxIdSendDstEp.Text = "";
            }
        }

        private void textBoxIdSendTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIdSendTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIdSendTime.ForeColor = System.Drawing.Color.Black;
                textBoxIdSendTime.Text = "";
            }
        }

        private void textBoxIdQueryAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIdQueryAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIdQueryAddr.ForeColor = System.Drawing.Color.Black;
                textBoxIdQueryAddr.Text = "";
            }
        }

        private void textBoxIdQuerySrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIdQuerySrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIdQuerySrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxIdQuerySrcEp.Text = "";
            }
        }

        private void textBoxIdQueryDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxIdQueryDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxIdQueryDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxIdQueryDstEp.Text = "";
            }
        }

#endregion

#region LevelClusterTab

        private void textBoxMoveToLevelAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToLevelAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToLevelAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToLevelAddr.Text = "";
            }
        }

        private void textBoxMoveToLevelSrcEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToLevelSrcEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToLevelSrcEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToLevelSrcEndPoint.Text = "";
            }
        }

        private void textBoxMoveToLevelDstEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToLevelDstEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToLevelDstEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToLevelDstEndPoint.Text = "";
            }
        }

        private void textBoxMoveToLevelLevel_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToLevelLevel.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToLevelLevel.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToLevelLevel.Text = "";
            }
        }

        private void textBoxMoveToLevelTransTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToLevelTransTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToLevelTransTime.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToLevelTransTime.Text = "";
            }
        }

#endregion

#region OnOffClusterTab

        private void textBoxOnOffAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOnOffAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOnOffAddr.ForeColor = System.Drawing.Color.Black;
                textBoxOnOffAddr.Text = "";
            }
        }

        private void textBoxOnOffSrcEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOnOffSrcEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOnOffSrcEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxOnOffSrcEndPoint.Text = "";
            }
        }

        private void textBoxOnOffDstEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOnOffDstEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOnOffDstEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxOnOffDstEndPoint.Text = "";
            }
        }

#endregion

#region ScenesClusterTab

        private void textBoxViewSceneAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxViewSceneAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxViewSceneAddr.ForeColor = System.Drawing.Color.Black;
                textBoxViewSceneAddr.Text = "";
            }
        }

        private void textBoxViewSceneSrcEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxViewSceneSrcEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxViewSceneSrcEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxViewSceneSrcEndPoint.Text = "";
            }
        }

        private void textBoxViewSceneDstEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxViewSceneDstEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxViewSceneDstEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxViewSceneDstEndPoint.Text = "";
            }
        }

        private void textBoxViewSceneGroupId_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxViewSceneGroupId.ForeColor != System.Drawing.Color.Black)
            {
                textBoxViewSceneGroupId.ForeColor = System.Drawing.Color.Black;
                textBoxViewSceneGroupId.Text = "";
            }
        }

        private void textBoxViewSceneSceneId_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxViewSceneSceneId.ForeColor != System.Drawing.Color.Black)
            {
                textBoxViewSceneSceneId.ForeColor = System.Drawing.Color.Black;
                textBoxViewSceneSceneId.Text = "";
            }
        }

        private void textBoxAddSceneAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneAddr.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneAddr.Text = "";
            }
        }

        private void textBoxAddSceneSrcEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneSrcEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneSrcEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneSrcEndPoint.Text = "";
            }
        }

        private void textBoxAddSceneDstEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneDstEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneDstEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneDstEndPoint.Text = "";
            }
        }

        private void textBoxAddSceneGroupId_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneGroupId.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneGroupId.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneGroupId.Text = "";
            }
        }

        private void textBoxAddSceneSceneId_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneSceneId.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneSceneId.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneSceneId.Text = "";
            }
        }

        private void textBoxAddSceneTransTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneTransTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneTransTime.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneTransTime.Text = "";
            }
        }

        private void textBoxAddSceneName_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneName.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneName.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneName.Text = "";
            }
        }

        private void textBoxAddSceneNameLen_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneNameLen.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneNameLen.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneNameLen.Text = "";
            }
        }

        private void textBoxAddSceneMaxNameLen_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneMaxNameLen.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneMaxNameLen.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneMaxNameLen.Text = "";
            }
        }

        private void textBoxAddSceneExtLen_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneExtLen.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneExtLen.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneExtLen.Text = "";
            }
        }

        private void textBoxAddSceneData_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxAddSceneData.ForeColor != System.Drawing.Color.Black)
            {
                textBoxAddSceneData.ForeColor = System.Drawing.Color.Black;
                textBoxAddSceneData.Text = "";
            }
        }

        private void textBoxStoreSceneAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxStoreSceneAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxStoreSceneAddr.ForeColor = System.Drawing.Color.Black;
                textBoxStoreSceneAddr.Text = "";
            }
        }

        private void textBoxStoreSceneSrcEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxStoreSceneSrcEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxStoreSceneSrcEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxStoreSceneSrcEndPoint.Text = "";
            }
        }

        private void textBoxStoreSceneDstEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxStoreSceneDstEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxStoreSceneDstEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxStoreSceneDstEndPoint.Text = "";
            }
        }

        private void textBoxStoreSceneGroupId_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxStoreSceneGroupId.ForeColor != System.Drawing.Color.Black)
            {
                textBoxStoreSceneGroupId.ForeColor = System.Drawing.Color.Black;
                textBoxStoreSceneGroupId.Text = "";
            }
        }

        private void textBoxStoreSceneSceneId_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxStoreSceneSceneId.ForeColor != System.Drawing.Color.Black)
            {
                textBoxStoreSceneSceneId.ForeColor = System.Drawing.Color.Black;
                textBoxStoreSceneSceneId.Text = "";
            }
        }

        private void textBoxRecallSceneAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRecallSceneAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRecallSceneAddr.ForeColor = System.Drawing.Color.Black;
                textBoxRecallSceneAddr.Text = "";
            }
        }

        private void textBoxRecallSceneSrcEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRecallSceneSrcEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRecallSceneSrcEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxRecallSceneSrcEndPoint.Text = "";
            }
        }

        private void textBoxRecallSceneDstEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRecallSceneDstEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRecallSceneDstEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxRecallSceneDstEndPoint.Text = "";
            }
        }

        private void textBoxRecallSceneGroupId_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRecallSceneGroupId.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRecallSceneGroupId.ForeColor = System.Drawing.Color.Black;
                textBoxRecallSceneGroupId.Text = "";
            }
        }

        private void textBoxRecallSceneSceneId_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRecallSceneSceneId.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRecallSceneSceneId.ForeColor = System.Drawing.Color.Black;
                textBoxRecallSceneSceneId.Text = "";
            }
        }

        private void textBoxGetSceneMembershipAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGetSceneMembershipAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGetSceneMembershipAddr.ForeColor = System.Drawing.Color.Black;
                textBoxGetSceneMembershipAddr.Text = "";
            }
        }

        private void textBoxGetSceneMembershipSrcEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGetSceneMembershipSrcEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGetSceneMembershipSrcEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxGetSceneMembershipSrcEndPoint.Text = "";
            }
        }

        private void textBoxGetSceneMembershipDstEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGetSceneMembershipDstEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGetSceneMembershipDstEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxGetSceneMembershipDstEndPoint.Text = "";
            }
        }

        private void textBoxGetSceneMembershipGroupID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxGetSceneMembershipGroupID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxGetSceneMembershipGroupID.ForeColor = System.Drawing.Color.Black;
                textBoxGetSceneMembershipGroupID.Text = "";
            }
        }

        private void textBoxRemoveAllScenesAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveAllScenesAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveAllScenesAddr.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveAllScenesAddr.Text = "";
            }
        }

        private void textBoxRemoveAllScenesSrcEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveAllScenesSrcEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveAllScenesSrcEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveAllScenesSrcEndPoint.Text = "";
            }
        }

        private void textBoxRemoveAllScenesDstEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveAllScenesDstEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveAllScenesDstEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveAllScenesDstEndPoint.Text = "";
            }
        }

        private void textBoxRemoveAllScenesGroupID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveAllScenesGroupID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveAllScenesGroupID.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveAllScenesGroupID.Text = "";
            }
        }

        private void textBoxRemoveSceneAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveSceneAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveSceneAddr.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveSceneAddr.Text = "";
            }
        }

        private void textBoxRemoveSceneSrcEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveSceneSrcEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveSceneSrcEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveSceneSrcEndPoint.Text = "";
            }
        }

        private void textBoxRemoveSceneDstEndPoint_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveSceneDstEndPoint.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveSceneDstEndPoint.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveSceneDstEndPoint.Text = "";
            }
        }

        private void textBoxRemoveSceneGroupID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveSceneGroupID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveSceneGroupID.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveSceneGroupID.Text = "";
            }
        }

        private void textBoxRemoveSceneSceneID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxRemoveSceneSceneID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxRemoveSceneSceneID.ForeColor = System.Drawing.Color.Black;
                textBoxRemoveSceneSceneID.Text = "";
            }
        }

#endregion

#region ColorClusterTab

        private void textBoxMoveToHueAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToHueAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToHueAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToHueAddr.Text = "";
            }
        }

        private void textBoxMoveToHueSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToHueSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToHueSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToHueSrcEp.Text = "";
            }
        }

        private void textBoxMoveToHueDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToHueDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToHueDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToHueDstEp.Text = "";
            }
        }

        private void textBoxMoveToHueHue_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToHueHue.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToHueHue.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToHueHue.Text = "";
            }
        }

        private void textBoxMoveToHueDir_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToHueDir.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToHueDir.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToHueDir.Text = "";
            }
        }

        private void textBoxMoveToHueTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToHueTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToHueTime.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToHueTime.Text = "";
            }
        }

        private void textBoxMoveToColorAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorAddr.Text = "";
            }
        }

        private void textBoxMoveToColorSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorSrcEp.Text = "";
            }
        }

        private void textBoxMoveToColorDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorDstEp.Text = "";
            }
        }

        private void textBoxMoveToColorX_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorX.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorX.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorX.Text = "";
            }
        }

        private void textBoxMoveToColorY_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorY.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorY.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorY.Text = "";
            }
        }

        private void textBoxMoveToColorTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorTime.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorTime.Text = "";
            }
        }

        private void textBoxMoveToSatAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToSatAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToSatAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToSatAddr.Text = "";
            }
        }

        private void textBoxMoveToSatSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToSatSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToSatSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToSatSrcEp.Text = "";
            }
        }

        private void textBoxMoveToSatDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToSatDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToSatDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToSatDstEp.Text = "";
            }
        }

        private void textBoxMoveToSatSat_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToSatSat.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToSatSat.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToSatSat.Text = "";
            }
        }

        private void textBoxMoveToSatTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToSatTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToSatTime.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToSatTime.Text = "";
            }
        }

        private void textBoxMoveToColorTempAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorTempAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorTempAddr.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorTempAddr.Text = "";
            }
        }

        private void textBoxMoveToColorTempSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorTempSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorTempSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorTempSrcEp.Text = "";
            }
        }

        private void textBoxMoveToColorTempDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorTempDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorTempDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorTempDstEp.Text = "";
            }
        }

        private void textBoxMoveToColorTempTemp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorTempTemp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorTempTemp.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorTempTemp.Text = "";
            }
        }

        private void textBoxMoveToColorTempRate_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxMoveToColorTempRate.ForeColor != System.Drawing.Color.Black)
            {
                textBoxMoveToColorTempRate.ForeColor = System.Drawing.Color.Black;
                textBoxMoveToColorTempRate.Text = "";
            }
        }

#endregion

#region DoorLockClusterTab

        private void textBoxLockUnlockAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLockUnlockAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLockUnlockAddr.ForeColor = System.Drawing.Color.Black;
                textBoxLockUnlockAddr.Text = "";
            }
        }

        private void textBoxLockUnlockSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLockUnlockSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLockUnlockSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxLockUnlockSrcEp.Text = "";
            }
        }

        private void textBoxLockUnlockDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLockUnlockDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLockUnlockDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxLockUnlockDstEp.Text = "";
            }
        }

#endregion

#region IASClusterTab

        private void textBoxEnrollRspAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEnrollRspAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEnrollRspAddr.ForeColor = System.Drawing.Color.Black;
                textBoxEnrollRspAddr.Text = "";
            }
        }

        private void textBoxEnrollRspSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEnrollRspSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEnrollRspSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxEnrollRspSrcEp.Text = "";
            }
        }

        private void textBoxEnrollRspDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEnrollRspDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEnrollRspDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxEnrollRspDstEp.Text = "";
            }
        }

        private void textBoxEnrollRspZone_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEnrollRspZone.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEnrollRspZone.ForeColor = System.Drawing.Color.Black;
                textBoxEnrollRspZone.Text = "";
            }
        }

#endregion

#region ZLLOnOffClusterTab

        private void textBoxZllOnOffEffectsAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllOnOffEffectsAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllOnOffEffectsAddr.ForeColor = System.Drawing.Color.Black;
                textBoxZllOnOffEffectsAddr.Text = "";
            }
        }

        private void textBoxZllOnOffEffectsSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllOnOffEffectsSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllOnOffEffectsSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxZllOnOffEffectsSrcEp.Text = "";
            }
        }

        private void textBoxZllOnOffEffectsDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllOnOffEffectsDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllOnOffEffectsDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxZllOnOffEffectsDstEp.Text = "";
            }
        }

        private void textBoxZllOnOffEffectsGradient_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllOnOffEffectsGradient.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllOnOffEffectsGradient.ForeColor = System.Drawing.Color.Black;
                textBoxZllOnOffEffectsGradient.Text = "";
            }
        }

#endregion

#region ZLLColorClusterTab

        private void textBoxZllMoveToHueAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllMoveToHueAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllMoveToHueAddr.ForeColor = System.Drawing.Color.Black;
                textBoxZllMoveToHueAddr.Text = "";
            }
        }

        private void textBoxZllMoveToHueSrcEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllMoveToHueSrcEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllMoveToHueSrcEp.ForeColor = System.Drawing.Color.Black;
                textBoxZllMoveToHueSrcEp.Text = "";
            }
        }

        private void textBoxZllMoveToHueDstEp_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllMoveToHueDstEp.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllMoveToHueDstEp.ForeColor = System.Drawing.Color.Black;
                textBoxZllMoveToHueDstEp.Text = "";
            }
        }

        private void textBoxZllMoveToHueHue_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllMoveToHueHue.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllMoveToHueHue.ForeColor = System.Drawing.Color.Black;
                textBoxZllMoveToHueHue.Text = "";
            }
        }

        private void textBoxZllMoveToHueDirection_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllMoveToHueDirection.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllMoveToHueDirection.ForeColor = System.Drawing.Color.Black;
                textBoxZllMoveToHueDirection.Text = "";
            }
        }

        private void textBoxZllMoveToHueTransTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxZllMoveToHueTransTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxZllMoveToHueTransTime.ForeColor = System.Drawing.Color.Black;
                textBoxZllMoveToHueTransTime.Text = "";
            }
        }

#endregion

#region OTAClusterTab

        private void textBoxOTAImageNotifyTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTAImageNotifyTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTAImageNotifyTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxOTAImageNotifyTargetAddr.Text = "";
            }
        }

        private void textBoxOTAImageNotifySrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTAImageNotifySrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTAImageNotifySrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxOTAImageNotifySrcEP.Text = "";
            }
        }

        private void textBoxOTAImageNotifyDstEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTAImageNotifyDstEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTAImageNotifyDstEP.ForeColor = System.Drawing.Color.Black;
                textBoxOTAImageNotifyDstEP.Text = "";
            }
        }

        private void textBoxOTAImageNotifyFileVersion_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTAImageNotifyFileVersion.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTAImageNotifyFileVersion.ForeColor = System.Drawing.Color.Black;
                textBoxOTAImageNotifyFileVersion.Text = "";
            }
        }

        private void textBoxOTAImageNotifyImageType_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTAImageNotifyImageType.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTAImageNotifyImageType.ForeColor = System.Drawing.Color.Black;
                textBoxOTAImageNotifyImageType.Text = "";
            }
        }

        private void textBoxOTAImageNotifyManuID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTAImageNotifyManuID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTAImageNotifyManuID.ForeColor = System.Drawing.Color.Black;
                textBoxOTAImageNotifyManuID.Text = "";
            }
        }

        private void textBoxOTAImageNotifyJitter_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTAImageNotifyJitter.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTAImageNotifyJitter.ForeColor = System.Drawing.Color.Black;
                textBoxOTAImageNotifyJitter.Text = "";
            }
        }

        private void textBoxOTASetWaitForDataParamsTargetAddr_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTASetWaitForDataParamsTargetAddr.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTASetWaitForDataParamsTargetAddr.ForeColor = System.Drawing.Color.Black;
                textBoxOTASetWaitForDataParamsTargetAddr.Text = "";
            }
        }

        private void textBoxOTASetWaitForDataParamsSrcEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTASetWaitForDataParamsSrcEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTASetWaitForDataParamsSrcEP.ForeColor = System.Drawing.Color.Black;
                textBoxOTASetWaitForDataParamsSrcEP.Text = "";
            }
        }

        private void textBoxOTASetWaitForDataParamsCurrentTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTASetWaitForDataParamsCurrentTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTASetWaitForDataParamsCurrentTime.ForeColor = System.Drawing.Color.Black;
                textBoxOTASetWaitForDataParamsCurrentTime.Text = "";
            }
        }

        private void textBoxOTASetWaitForDataParamsRequestTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTASetWaitForDataParamsRequestTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTASetWaitForDataParamsRequestTime.ForeColor = System.Drawing.Color.Black;
                textBoxOTASetWaitForDataParamsRequestTime.Text = "";
            }
        }

        private void textBoxOTASetWaitForDataParamsRequestBlockDelay_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxOTASetWaitForDataParamsRequestBlockDelay.ForeColor != System.Drawing.Color.Black)
            {
                textBoxOTASetWaitForDataParamsRequestBlockDelay.ForeColor = System.Drawing.Color.Black;
                textBoxOTASetWaitForDataParamsRequestBlockDelay.Text = "";
            }
        }

#endregion

#region PollControlTab

        private void textBoxFastPollExpiryTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxFastPollExpiryTime.ForeColor != System.Drawing.Color.Black)
            {
                textBoxFastPollExpiryTime.ForeColor = System.Drawing.Color.Black;
                textBoxFastPollExpiryTime.Text = "";
            }
        }

#endregion

#region EZLNTTab

        private void textBoxEZLNTUNBINDCLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTUNBINDCLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTUNBINDCLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTUNBINDCLUSTERID.Text = "";
            }
        }

        private void textBoxEZLNTBINDCLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTBINDCLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTBINDCLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTBINDCLUSTERID.Text = "";
            }
        }

        private void textBoxEZLNTSETLOOP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTSETLOOP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTSETLOOP.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTSETLOOP.Text = "";
            }
        }

        private void textBoxEZLNTTIMERINTERVAL_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTTIMERINTERVAL.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTTIMERINTERVAL.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTTIMERINTERVAL.Text = "";
            }
        }

        private void textBoxREMOVEGROUP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxREMOVEGROUP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxREMOVEGROUP.ForeColor = System.Drawing.Color.Black;
                textBoxREMOVEGROUP.Text = "";
            }
        }

        private void textBoxEZLNTADDGROUP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTADDGROUP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTADDGROUP.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTADDGROUP.Text = "";
            }
        }

        private void textBoxEZLNTVIEW_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTVIEW.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTVIEW.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTVIEW.Text = "";
            }
        }

        private void textBoxEZLNTSETINTERVALMAX_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTSETINTERVALMAX.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTSETINTERVALMAX.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTSETINTERVALMAX.Text = "";
            }
        }

        private void textBoxEZLNTSETSTEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTSETSTEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTSETSTEP.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTSETSTEP.Text = "";
            }
        }

        private void textBoxEZLNTSETDIR_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTSETDIR.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTSETDIR.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTSETDIR.Text = "";
            }
        }

        private void textBoxEZLNTREADCLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTREADCLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTREADCLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTREADCLUSTERID.Text = "";
            }
        }

        private void textBoxEZLNTATTRIBUTEID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTATTRIBUTEID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTATTRIBUTEID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTATTRIBUTEID.Text = "";
            }
        }

        //private void textBoxEZLNTATTRIBUTECOUNT_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (textBoxEZLNTATTRIBUTECOUNT.ForeColor != System.Drawing.Color.Black)
        //    {
        //        textBoxEZLNTATTRIBUTECOUNT.ForeColor = System.Drawing.Color.Black;
        //        textBoxEZLNTATTRIBUTECOUNT.Text = "";
        //    }
        //}

        private void textBoxEZLNTWRITEATTRIBUTECLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTWRITEATTRIBUTECLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTWRITEATTRIBUTECLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTWRITEATTRIBUTECLUSTERID.Text = "";
            }
        }


        private void textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.Text = "";
            }
        }

        private void textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.Text = "";
            }
        }

        private void textBoxEZLNTWRITEATTRIBUTEDATA_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTWRITEATTRIBUTEDATA.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTWRITEATTRIBUTEDATA.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTWRITEATTRIBUTEDATA.Text = "";
            }
        }

        private void textBoxEZLNTCONFIGRPRTCLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTCONFIGRPRTCLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTCONFIGRPRTCLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTCONFIGRPRTCLUSTERID.Text = "";
            }
        }

        private void textBoxEZLNTCONFIGRPRTTYPE_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTCONFIGRPRTTYPE.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTCONFIGRPRTTYPE.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTCONFIGRPRTTYPE.Text = "";
            }
        }

        private void textBoxEZLNTCONFIGRPRTATTRIBID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTCONFIGRPRTATTRIBID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTCONFIGRPRTATTRIBID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTCONFIGRPRTATTRIBID.Text = "";
            }
        }

        private void textBoxEZLNTCONFIGRPRTMININTERVAL_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTCONFIGRPRTMININTERVAL.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTCONFIGRPRTMININTERVAL.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTCONFIGRPRTMININTERVAL.Text = "";
            }
        }

        private void textBoxEZLNTCONFIGRPRTMAXINTERVAL_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTCONFIGRPRTMAXINTERVAL.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTCONFIGRPRTMAXINTERVAL.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTCONFIGRPRTMAXINTERVAL.Text = "";
            }
        }

        private void textBoxEZLNTCONFIGRPRTTIMEOUT_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTCONFIGRPRTTIMEOUT.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTCONFIGRPRTTIMEOUT.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTCONFIGRPRTTIMEOUT.Text = "";
            }
        }

        private void textBoxEZLNTCONFIGRPRTCHANGE_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTCONFIGRPRTCHANGE.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTCONFIGRPRTCHANGE.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTCONFIGRPRTCHANGE.Text = "";
            }
        }

        private void textBoxEZLNTREADRPRTCLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTREADRPRTCLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTREADRPRTCLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTREADRPRTCLUSTERID.Text = "";
            }
        }

        private void textBoxEZLNTREADRPRTATTRIBUTEID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTREADRPRTATTRIBUTEID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTREADRPRTATTRIBUTEID.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTREADRPRTATTRIBUTEID.Text = "";
            }
        }

        private void textBoxEZLNTIDENTIFYTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTIDENTIFYTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTIDENTIFYTIME.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTIDENTIFYTIME.Text = "";
            }
        }

        private void textBoxEZLNTLEVEL_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTLEVEL.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTLEVEL.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTLEVEL.Text = "";
            }
        }

        private void textBoxEZLNTLEVELTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTLEVELTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTLEVELTIME.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTLEVELTIME.Text = "";
            }
        }

        private void textBoxEZLNTHUE_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTHUE.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTHUE.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTHUE.Text = "";
            }
        }

        private void textBoxEZLNTHUEDIR_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTHUEDIR.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTHUEDIR.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTHUEDIR.Text = "";
            }
        }

        private void textBoxEZLNTHUETIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTHUETIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTHUETIME.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTHUETIME.Text = "";
            }
        }

        private void textBoxEZLNTCOLORX_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTCOLORX.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTCOLORX.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTCOLORX.Text = "";
            }
        }

        private void textBoxEZLNTCOLORY_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTCOLORY.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTCOLORY.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTCOLORY.Text = "";
            }
        }

        private void textBoxCOLORTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxCOLORTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxCOLORTIME.ForeColor = System.Drawing.Color.Black;
                textBoxCOLORTIME.Text = "";
            }
        }

        private void textBoxEZLNTSAT_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTSAT.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTSAT.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTSAT.Text = "";
            }
        }

        private void textBoxEZLNTSATTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTSATTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTSATTIME.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTSATTIME.Text = "";
            }
        }

        private void textBoxEZLNTTEMP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTTEMP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTTEMP.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTTEMP.Text = "";
            }
        }

        private void textBoxEZLNTTEMPTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxEZLNTTEMPTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxEZLNTTEMPTIME.ForeColor = System.Drawing.Color.Black;
                textBoxEZLNTTEMPTIME.Text = "";
            }
        }

        #endregion

        #region LNT Remote
        private void textBoxLNTSETLOOP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTSETLOOP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTSETLOOP.ForeColor = System.Drawing.Color.Black;
                textBoxLNTSETLOOP.Text = "";
            }
        }

        private void textBoxLNTSETPARAMININTERVAL_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTSETPARAMININTERVAL.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTSETPARAMININTERVAL.ForeColor = System.Drawing.Color.Black;
                textBoxLNTSETPARAMININTERVAL.Text = "";
            }
        }

        private void textBoxLNTSETPARAMAXINTERVAL_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTSETPARAMAXINTERVAL.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTSETPARAMAXINTERVAL.ForeColor = System.Drawing.Color.Black;
                textBoxLNTSETPARAMAXINTERVAL.Text = "";
            }
        }


        private void textBoxLNTSETPARASTEP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTSETPARASTEP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTSETPARASTEP.ForeColor = System.Drawing.Color.Black;
                textBoxLNTSETPARASTEP.Text = "";
            }
        }

        private void textBoxLNTSETPARADIR_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTSETPARADIR.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTSETPARADIR.ForeColor = System.Drawing.Color.Black;
                textBoxLNTSETPARADIR.Text = "";
            }
        }

        private void textBoxLNTREADATTRCLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTREADATTRCLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTREADATTRCLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxLNTREADATTRCLUSTERID.Text = "";
            }
        }

        private void textBoxLNTREADATTRATTRIBUTEID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTREADATTRATTRIBUTEID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTREADATTRATTRIBUTEID.ForeColor = System.Drawing.Color.Black;
                textBoxLNTREADATTRATTRIBUTEID.Text = "";
            }
        }

        private void textBoxLNTREADATTRATTRIBUTECOUNT_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTREADATTRATTRIBUTECOUNT.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTREADATTRATTRIBUTECOUNT.ForeColor = System.Drawing.Color.Black;
                textBoxLNTREADATTRATTRIBUTECOUNT.Text = "";
            }
        }

        private void textBoxLNTWRITEATTRCLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTWRITEATTRCLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTWRITEATTRCLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxLNTWRITEATTRCLUSTERID.Text = "";
            }
        }

        private void textBoxLNTWRITEATTRATTRID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTWRITEATTRATTRID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTWRITEATTRATTRID.ForeColor = System.Drawing.Color.Black;
                textBoxLNTWRITEATTRATTRID.Text = "";
            }
        }

        private void textBoxLNTWRITEATTRDATATYPE_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTWRITEATTRDATATYPE.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTWRITEATTRDATATYPE.ForeColor = System.Drawing.Color.Black;
                textBoxLNTWRITEATTRDATATYPE.Text = "";
            }
        }

        private void textBoxLNTWRITEATTRDATA_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTWRITEATTRDATA.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTWRITEATTRDATA.ForeColor = System.Drawing.Color.Black;
                textBoxLNTWRITEATTRDATA.Text = "";
            }
        }

        private void textBoxLNTBINDIEEEADDR_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTBINDIEEEADDR.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTBINDIEEEADDR.ForeColor = System.Drawing.Color.Black;
                textBoxLNTBINDIEEEADDR.Text = "";
            }
        }

        private void textBoxLNTUNBINDIEEEADDR_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTUNBINDIEEEADDR.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTUNBINDIEEEADDR.ForeColor = System.Drawing.Color.Black;
                textBoxLNTUNBINDIEEEADDR.Text = "";
            }
        }

        private void textBoxLNTCONFIGRPRTCLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTCONFIGRPRTCLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTCONFIGRPRTCLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxLNTCONFIGRPRTCLUSTERID.Text = "";
            }
        }

        private void textBoxCONFIGRPRTATTRID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxCONFIGRPRTATTRID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxCONFIGRPRTATTRID.ForeColor = System.Drawing.Color.Black;
                textBoxCONFIGRPRTATTRID.Text = "";
            }
        }


        private void textBoxLNTCONFIGRPRTTYPE_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTCONFIGRPRTTYPE.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTCONFIGRPRTTYPE.ForeColor = System.Drawing.Color.Black;
                textBoxLNTCONFIGRPRTTYPE.Text = "";
            }
        }

        private void textBoxCONFIGRPRTMININTERVAL_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxCONFIGRPRTMININTERVAL.ForeColor != System.Drawing.Color.Black)
            {
                textBoxCONFIGRPRTMININTERVAL.ForeColor = System.Drawing.Color.Black;
                textBoxCONFIGRPRTMININTERVAL.Text = "";
            }
        }

        private void textBoxCONFIGRPRTMAXRPRTINTERVAL_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxCONFIGRPRTMAXRPRTINTERVAL.ForeColor != System.Drawing.Color.Black)
            {
                textBoxCONFIGRPRTMAXRPRTINTERVAL.ForeColor = System.Drawing.Color.Black;
                textBoxCONFIGRPRTMAXRPRTINTERVAL.Text = "";
            }
        }


        private void textBoxLNTCONFIGRPRTTIMEOUT_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTCONFIGRPRTTIMEOUT.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTCONFIGRPRTTIMEOUT.ForeColor = System.Drawing.Color.Black;
                textBoxLNTCONFIGRPRTTIMEOUT.Text = "";
            }
        }

        private void textBoxLNTCONFIGRPRTCHANGE_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTCONFIGRPRTCHANGE.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTCONFIGRPRTCHANGE.ForeColor = System.Drawing.Color.Black;
                textBoxLNTCONFIGRPRTCHANGE.Text = "";
            }
        }

        private void textBoxLNTREADRPRTCLUSTERID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTREADRPRTCLUSTERID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTREADRPRTCLUSTERID.ForeColor = System.Drawing.Color.Black;
                textBoxLNTREADRPRTCLUSTERID.Text = "";
            }
        }

        private void textBoxLNTREADRPRTATTRID_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTREADRPRTATTRID.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTREADRPRTATTRID.ForeColor = System.Drawing.Color.Black;
                textBoxLNTREADRPRTATTRID.Text = "";
            }
        }

        private void textBoxLNTIDENTIFYTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTIDENTIFYTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTIDENTIFYTIME.ForeColor = System.Drawing.Color.Black;
                textBoxLNTIDENTIFYTIME.Text = "";
            }
        }

        private void textBoxLNTADDGROUPADDR_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTADDGROUPADDR.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTADDGROUPADDR.ForeColor = System.Drawing.Color.Black;
                textBoxLNTADDGROUPADDR.Text = "";
            }
        }

        private void textBoxLNTREMOVEGROUPADDRESS_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTREMOVEGROUPADDRESS.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTREMOVEGROUPADDRESS.ForeColor = System.Drawing.Color.Black;
                textBoxLNTREMOVEGROUPADDRESS.Text = "";
            }
        }

        private void textBoxLNTVIEWGROUPADDRESS_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTVIEWGROUPADDRESS.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTVIEWGROUPADDRESS.ForeColor = System.Drawing.Color.Black;
                textBoxLNTVIEWGROUPADDRESS.Text = "";
            }
        }

        private void textBoxLNTLEVEL_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTLEVEL.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTLEVEL.ForeColor = System.Drawing.Color.Black;
                textBoxLNTLEVEL.Text = "";
            }
        }

        private void textBoxLNTLEVELTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTLEVELTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTLEVELTIME.ForeColor = System.Drawing.Color.Black;
                textBoxLNTLEVELTIME.Text = "";
            }
        }

        private void textBoxLNTHUE_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTHUE.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTHUE.ForeColor = System.Drawing.Color.Black;
                textBoxLNTHUE.Text = "";
            }
        }

        private void textBoxLNTHUEDIR_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTHUEDIR.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTHUEDIR.ForeColor = System.Drawing.Color.Black;
                textBoxLNTHUEDIR.Text = "";
            }
        }

        private void textBoxLNTHUETIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTHUETIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTHUETIME.ForeColor = System.Drawing.Color.Black;
                textBoxLNTHUETIME.Text = "";
            }
        }

        private void textBoxLNTCOLORX_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTCOLORX.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTCOLORX.ForeColor = System.Drawing.Color.Black;
                textBoxLNTCOLORX.Text = "";
            }
        }

        private void textBoxLNTCOLORY_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTCOLORY.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTCOLORY.ForeColor = System.Drawing.Color.Black;
                textBoxLNTCOLORY.Text = "";
            }
        }

        private void textBoxLNTCOLORTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTCOLORTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTCOLORTIME.ForeColor = System.Drawing.Color.Black;
                textBoxLNTCOLORTIME.Text = "";
            }
        }

        private void textBoxLNTSAT_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTSAT.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTSAT.ForeColor = System.Drawing.Color.Black;
                textBoxLNTSAT.Text = "";
            }
        }

        private void textBoxLNTSATTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTSATTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTSATTIME.ForeColor = System.Drawing.Color.Black;
                textBoxLNTSATTIME.Text = "";
            }
        }

        private void textBoxLNTTEMP_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTTEMP.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTTEMP.ForeColor = System.Drawing.Color.Black;
                textBoxLNTTEMP.Text = "";
            }
        }

        private void textBoxLNTTEMPTIME_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxLNTTEMPTIME.ForeColor != System.Drawing.Color.Black)
            {
                textBoxLNTTEMPTIME.ForeColor = System.Drawing.Color.Black;
                textBoxLNTTEMPTIME.Text = "";
            }
        }

        #endregion
        #endregion

        #region Restore Grey Text

        #region ManagementTab

        #region Set EPID

        private void textBoxSetEPID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSetEPID.Text))
            {
                textBoxSetEPID.ForeColor = System.Drawing.Color.Gray;
                textBoxSetEPID.Text = "Extended PAN ID (64-bit Hex)";
            }
        }

#endregion

#region Set CMSK

        private void textBoxSetCMSK_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSetCMSK.Text))
            {
                textBoxSetCMSK.ForeColor = System.Drawing.Color.Gray;
                textBoxSetCMSK.Text = "Single Channel or Mask (32-bit Hex)";
            }
        }

#endregion

#region Set Security

        private void textBoxSetSecurityKeySeqNbr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSetSecurityKeySeqNbr.Text))
            {
                textBoxSetSecurityKeySeqNbr.ForeColor = System.Drawing.Color.Gray;
                textBoxSetSecurityKeySeqNbr.Text = "SQN";
            }
        }

#endregion

#region Mgmt Leave

        private void textBoxMgmtLeaveAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMgmtLeaveAddr.Text))
            {
                textBoxMgmtLeaveAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMgmtLeaveAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxMgmtLeaveExtAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMgmtLeaveExtAddr.Text))
            {
                textBoxMgmtLeaveExtAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMgmtLeaveExtAddr.Text = "Address (64-bit Hex)";
            }
        }

#endregion

#region Leave

        private void textBoxLeaveAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLeaveAddr.Text))
            {
                textBoxLeaveAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxLeaveAddr.Text = "Address (64-bit Hex)";
            }
        }

#endregion

#region Remove

        private void textBoxRemoveParentAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveParentAddr.Text))
            {
                textBoxRemoveParentAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveParentAddr.Text = "Address (64-bit Hex)";
            }
        }

        private void textBoxRemoveChildAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveChildAddr.Text))
            {
                textBoxRemoveChildAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveChildAddr.Text = "Address (64-bit Hex)";
            }
        }

#endregion

#region Permit Join

        private void textBoxPermitJoinAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPermitJoinAddr.Text))
            {
                textBoxPermitJoinAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxPermitJoinAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxPermitJoinInterval_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPermitJoinInterval.Text))
            {
                textBoxPermitJoinInterval.ForeColor = System.Drawing.Color.Gray;
                textBoxPermitJoinInterval.Text = "Interval (16-bit Hex)";
            }
        }

#endregion

#region Match Req

        private void textBoxMatchReqAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMatchReqAddr.Text))
            {
                textBoxMatchReqAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMatchReqAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxMatchReqProfileID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMatchReqProfileID.Text))
            {
                textBoxMatchReqProfileID.ForeColor = System.Drawing.Color.Gray;
                textBoxMatchReqProfileID.Text = "Profile (16-bit Hex)";
            }
        }

        private void textBoxMatchReqNbrInputClusters_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMatchReqNbrInputClusters.Text))
            {
                textBoxMatchReqNbrInputClusters.ForeColor = System.Drawing.Color.Gray;
                textBoxMatchReqNbrInputClusters.Text = "Inputs (8-bit Hex)";
            }
        }

        private void textBoxMatchReqInputClusters_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMatchReqInputClusters.Text))
            {
                textBoxMatchReqInputClusters.ForeColor = System.Drawing.Color.Gray;
                textBoxMatchReqInputClusters.Text = "Clusters (16-bit Hex)";
            }
        }

        private void textBoxMatchReqNbrOutputClusters_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMatchReqNbrOutputClusters.Text))
            {
                textBoxMatchReqNbrOutputClusters.ForeColor = System.Drawing.Color.Gray;
                textBoxMatchReqNbrOutputClusters.Text = "Outputs (8-bit Hex)";
            }
        }

        private void textBoxMatchReqOutputClusters_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMatchReqOutputClusters.Text))
            {
                textBoxMatchReqOutputClusters.ForeColor = System.Drawing.Color.Gray;
                textBoxMatchReqOutputClusters.Text = "Clusters (16-bit Hex)";
            }
        }

#endregion

#region Bind

        private void textBoxBindTargetExtAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxBindTargetExtAddr.Text))
            {
                textBoxBindTargetExtAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxBindTargetExtAddr.Text = "Target Address (64-bit Hex)";
            }
        }

        private void textBoxBindDestAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxBindDestAddr.Text))
            {
                textBoxBindDestAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxBindDestAddr.Text = "Dst Address (16 or 64-bit Hex)";
            }
        }

        private void textBoxBindTargetEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxBindTargetEP.Text))
            {
                textBoxBindTargetEP.ForeColor = System.Drawing.Color.Gray;
                textBoxBindTargetEP.Text = "Target EP (8-bit Hex)";
            }
        }

        private void textBoxBindClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxBindClusterID.Text))
            {
                textBoxBindClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxBindClusterID.Text = "Cluster (16-bit Hex)";
            }
        }

        private void textBoxBindDestEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxBindDestEP.Text))
            {
                textBoxBindDestEP.ForeColor = System.Drawing.Color.Gray;
                textBoxBindDestEP.Text = "Dst EP (8-bit Hex)";
            }
        }

#endregion

#region UnBind

        private void textBoxUnBindTargetExtAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUnBindTargetExtAddr.Text))
            {
                textBoxUnBindTargetExtAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxUnBindTargetExtAddr.Text = "Target Address (64-bit Hex)";
            }
        }

        private void textBoxUnBindDestAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUnBindDestAddr.Text))
            {
                textBoxUnBindDestAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxUnBindDestAddr.Text = "Dst Address (16 or 64-bit Hex)";
            }
        }

        private void textBoxUnBindTargetEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUnBindTargetEP.Text))
            {
                textBoxUnBindTargetEP.ForeColor = System.Drawing.Color.Gray;
                textBoxUnBindTargetEP.Text = "Target EP (8-bit Hex)";
            }
        }

        private void textBoxUnBindClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUnBindClusterID.Text))
            {
                textBoxUnBindClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxUnBindClusterID.Text = "Cluster 16-bit Hex)";
            }
        }

        private void textBoxUnBindDestEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUnBindDestEP.Text))
            {
                textBoxUnBindDestEP.ForeColor = System.Drawing.Color.Gray;
                textBoxUnBindDestEP.Text = "Dst EP (8-bit Hex)";
            }
        }

#endregion

#region Active Req

        private void textBoxActiveEpAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxActiveEpAddr.Text))
            {
                textBoxActiveEpAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxActiveEpAddr.Text = "Address (16-bit Hex)";
            }
        }

#endregion

#region IEEE Req

        private void textBoxIeeeReqTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIeeeReqTargetAddr.Text))
            {
                textBoxIeeeReqTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxIeeeReqTargetAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxIeeeReqAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIeeeReqAddr.Text))
            {
                textBoxIeeeReqAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxIeeeReqAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxIeeeReqStartIndex_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIeeeReqStartIndex.Text))
            {
                textBoxIeeeReqStartIndex.ForeColor = System.Drawing.Color.Gray;
                textBoxIeeeReqStartIndex.Text = "Start Idx (8-bit Hex)";
            }
        }

#endregion

#region Addr Req

        private void textBoxNwkAddrReqTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNwkAddrReqTargetAddr.Text))
            {
                textBoxNwkAddrReqTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxNwkAddrReqTargetAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxNwkAddrReqExtAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNwkAddrReqExtAddr.Text))
            {
                textBoxNwkAddrReqExtAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxNwkAddrReqExtAddr.Text = "Address (64-bit Hex)";
            }
        }

        private void textBoxNwkAddrReqStartIndex_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNwkAddrReqStartIndex.Text))
            {
                textBoxNwkAddrReqStartIndex.ForeColor = System.Drawing.Color.Gray;
                textBoxNwkAddrReqStartIndex.Text = "Start Idx (8-bit Hex)";
            }
        }

#endregion

#region Node Req

        private void textBoxNodeDescReq_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNodeDescReq.Text))
            {
                textBoxNodeDescReq.ForeColor = System.Drawing.Color.Gray;
                textBoxNodeDescReq.Text = "Address (16-bit Hex)";
            }
        }

#endregion

#region Power Req

        private void textBoxPowerReqAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPowerReqAddr.Text))
            {
                textBoxPowerReqAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxPowerReqAddr.Text = "Address (16-bit Hex)";
            }
        }

#endregion

#region Simple Req

        private void textBoxSimpleReqAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSimpleReqAddr.Text))
            {
                textBoxSimpleReqAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxSimpleReqAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxSimpleReqEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSimpleReqEndPoint.Text))
            {
                textBoxSimpleReqEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxSimpleReqEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

#endregion

#region ComplexReq

        private void textBoxComplexReqAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxComplexReqAddr.Text))
            {
                textBoxComplexReqAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxComplexReqAddr.Text = "Address (16-bit Hex)";
            }
        }

#endregion

#region UserReq

        private void textBoxUserReqAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserReqAddr.Text))
            {
                textBoxUserReqAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxUserReqAddr.Text = "Address (16-bit Hex)";
            }
        }

#endregion

#region UserSetReq

        private void textBoxUserSetReqAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserSetReqAddr.Text))
            {
                textBoxUserSetReqAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxUserSetReqAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxUserSetReqDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUserSetReqDescription.Text))
            {
                textBoxUserSetReqDescription.ForeColor = System.Drawing.Color.Gray;
                textBoxUserSetReqDescription.Text = "User Description (String)";
            }
        }

#endregion

#region Lqi Req

        private void textBoxLqiReqTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLqiReqTargetAddr.Text))
            {
                textBoxLqiReqTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxLqiReqTargetAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxLqiReqStartIndex_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLqiReqStartIndex.Text))
            {
                textBoxLqiReqStartIndex.ForeColor = System.Drawing.Color.Gray;
                textBoxLqiReqStartIndex.Text = "Start Idx (8-bit Hex)";
            }
        }

#endregion

#endregion

#region GeneralTab

#region Read Attribute

        private void textBoxReadAttribTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAttribTargetAddr.Text))
            {
                textBoxReadAttribTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAttribTargetAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxReadAttribSrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAttribSrcEP.Text))
            {
                textBoxReadAttribSrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAttribSrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxReadAttribDstEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAttribDstEP.Text))
            {
                textBoxReadAttribDstEP.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAttribDstEP.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxReadAttribClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAttribClusterID.Text))
            {
                textBoxReadAttribClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAttribClusterID.Text = "Cluster (16-bit Hex)";
            }
        }

        private void textBoxReadAttribCount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAttribCount.Text))
            {
                textBoxReadAttribCount.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAttribCount.Text = "Attrib Count";
            }
        }

        private void textBoxReadAttribID1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAttribID1.Text))
            {
                textBoxReadAttribID1.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAttribID1.Text = "Attrib (16-bit Hex)";
            }
        }

        private void textBoxReadAttribManuID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAttribManuID.Text))
            {
                textBoxReadAttribManuID.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAttribManuID.Text = "Manu ID (16-bit Hex)";
            }
        }

#endregion

#region Write Attribute

        private void textBoxWriteAttribTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxWriteAttribTargetAddr.Text))
            {
                textBoxWriteAttribTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxWriteAttribTargetAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxWriteAttribSrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxWriteAttribSrcEP.Text))
            {
                textBoxWriteAttribSrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxWriteAttribSrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxWriteAttribDstEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxWriteAttribDstEP.Text))
            {
                textBoxWriteAttribDstEP.ForeColor = System.Drawing.Color.Gray;
                textBoxWriteAttribDstEP.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxWriteAttribClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxWriteAttribClusterID.Text))
            {
                textBoxWriteAttribClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxWriteAttribClusterID.Text = "Cluster (16-bit Hex)";
            }
        }

        private void textBoxWriteAttribID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxWriteAttribID.Text))
            {
                textBoxWriteAttribID.ForeColor = System.Drawing.Color.Gray;
                textBoxWriteAttribID.Text = "Attrib (16-bit Hex)";
            }
        }

        private void textBoxWriteAttribDataType_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxWriteAttribDataType.Text))
            {
                textBoxWriteAttribDataType.ForeColor = System.Drawing.Color.Gray;
                textBoxWriteAttribDataType.Text = "Type (8-bit Hex)";
            }
        }

        private void textBoxWriteAttribData_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxWriteAttribData.Text))
            {
                textBoxWriteAttribData.ForeColor = System.Drawing.Color.Gray;
                textBoxWriteAttribData.Text = "Data)";
            }
        }

        private void textBoxWriteAttribManuID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxWriteAttribManuID.Text))
            {
                textBoxWriteAttribManuID.ForeColor = System.Drawing.Color.Gray;
                textBoxWriteAttribManuID.Text = "Manu ID (16-bit Hex)";
            }
        }

#endregion

#region Config Rprt

        private void textBoxConfigReportTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportTargetAddr.Text))
            {
                textBoxConfigReportTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportTargetAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxConfigReportSrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportSrcEP.Text))
            {
                textBoxConfigReportSrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportSrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxConfigReportDstEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportDstEP.Text))
            {
                textBoxConfigReportDstEP.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportDstEP.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxConfigReportClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportClusterID.Text))
            {
                textBoxConfigReportClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportClusterID.Text = "Cluster (16-bit Hex)";
            }
        }

        private void textBoxConfigReportAttribType_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportAttribType.Text))
            {
                textBoxConfigReportAttribType.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportAttribType.Text = "Attrib Type";
            }
        }

        private void textBoxConfigReportAttribID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportAttribID.Text))
            {
                textBoxConfigReportAttribID.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportAttribID.Text = "Attrib (16-bit Hex)";
            }
        }

        private void textBoxConfigReportMinInterval_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportMinInterval.Text))
            {
                textBoxConfigReportMinInterval.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportMinInterval.Text = "Min Intv (16-bit Hex)";
            }
        }

        private void textBoxConfigReportMaxInterval_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportMaxInterval.Text))
            {
                textBoxConfigReportMaxInterval.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportMaxInterval.Text = "Max Intv (16-bit Hex)";
            }
        }

        private void textBoxConfigReportTimeOut_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportTimeOut.Text))
            {
                textBoxConfigReportTimeOut.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportTimeOut.Text = "TimeOut (16-bit Hex)";
            }
        }

        private void textBoxConfigReportChange_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxConfigReportChange.Text))
            {
                textBoxConfigReportChange.ForeColor = System.Drawing.Color.Gray;
                textBoxConfigReportChange.Text = "Change (8-bit Hex)";
            }
        }

#endregion

#region Read Rprt

        private void textBoxReadReportConfigTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadReportConfigTargetAddr.Text))
            {
                textBoxReadReportConfigTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxReadReportConfigTargetAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxReadReportConfigSrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadReportConfigSrcEP.Text))
            {
                textBoxReadReportConfigSrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxReadReportConfigSrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxReadReportConfigDstEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadReportConfigDstEP.Text))
            {
                textBoxReadReportConfigDstEP.ForeColor = System.Drawing.Color.Gray;
                textBoxReadReportConfigDstEP.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxReadReportConfigClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadReportConfigClusterID.Text))
            {
                textBoxReadReportConfigClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxReadReportConfigClusterID.Text = "Cluster (16-bit Hex)";
            }
        }

        private void textBoxReadReportConfigAttribID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadReportConfigAttribID.Text))
            {
                textBoxReadReportConfigAttribID.ForeColor = System.Drawing.Color.Gray;
                textBoxReadReportConfigAttribID.Text = "Attrib (16-bit Hex)";
            }
        }

#endregion

#region Read All Attrib

        private void textBoxReadAllAttribAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAllAttribAddr.Text))
            {
                textBoxReadAllAttribAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAllAttribAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxReadAllAttribSrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAllAttribSrcEP.Text))
            {
                textBoxReadAllAttribSrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAllAttribSrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxReadAllAttribDstEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAllAttribDstEP.Text))
            {
                textBoxReadAllAttribDstEP.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAllAttribDstEP.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxReadAllAttribClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReadAllAttribClusterID.Text))
            {
                textBoxReadAllAttribClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxReadAllAttribClusterID.Text = "Cluster (16-bit Hex)";
            }
        }

#endregion

#region Disc Attribs

        private void textBoxDiscoverAttributesAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverAttributesAddr.Text))
            {
                textBoxDiscoverAttributesAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverAttributesAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxDiscoverAttributesSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverAttributesSrcEp.Text))
            {
                textBoxDiscoverAttributesSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverAttributesSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxDiscoverAttributesDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverAttributesDstEp.Text))
            {
                textBoxDiscoverAttributesDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverAttributesDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxDiscoverAttributesClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverAttributesClusterID.Text))
            {
                textBoxDiscoverAttributesClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverAttributesClusterID.Text = "Cluster (!6-bit Hex)";
            }
        }

        private void textBoxDiscoverAttributesStartAttrId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverAttributesStartAttrId.Text))
            {
                textBoxDiscoverAttributesStartAttrId.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverAttributesStartAttrId.Text = "Attrib (16-bit Hex)";
            }
        }

        private void textBoxDiscoverAttributesMaxIDs_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverAttributesMaxIDs.Text))
            {
                textBoxDiscoverAttributesMaxIDs.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverAttributesMaxIDs.Text = "Max ID's (8-bit Hex)";
            }
        }

#endregion

#region MTO Rt Req

        private void textBoxManyToOneRouteRequesRadius_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxManyToOneRouteRequesRadius.Text))
            {
                textBoxManyToOneRouteRequesRadius.ForeColor = System.Drawing.Color.Gray;
                textBoxManyToOneRouteRequesRadius.Text = "Radius (8-bit Hex)";
            }
        }

#endregion

#region NWK Update

        private void textBoxMgmtNwkUpdateTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMgmtNwkUpdateTargetAddr.Text))
            {
                textBoxMgmtNwkUpdateTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMgmtNwkUpdateTargetAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxMgmtNwkUpdateChannelMask_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMgmtNwkUpdateChannelMask.Text))
            {
                textBoxMgmtNwkUpdateChannelMask.ForeColor = System.Drawing.Color.Gray;
                textBoxMgmtNwkUpdateChannelMask.Text = "ChanMask (32-bit Hex)";
            }
        }

        private void textBoxMgmtNwkUpdateScanDuration_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMgmtNwkUpdateScanDuration.Text))
            {
                textBoxMgmtNwkUpdateScanDuration.ForeColor = System.Drawing.Color.Gray;
                textBoxMgmtNwkUpdateScanDuration.Text = "Duration (8-bit Hex)";
            }
        }

        private void textBoxMgmtNwkUpdateScanCount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMgmtNwkUpdateScanCount.Text))
            {
                textBoxMgmtNwkUpdateScanCount.ForeColor = System.Drawing.Color.Gray;
                textBoxMgmtNwkUpdateScanCount.Text = "Count (8-bit Hex)";
            }
        }

        private void textBoxMgmtNwkUpdateNwkManagerAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMgmtNwkUpdateNwkManagerAddr.Text))
            {
                textBoxMgmtNwkUpdateNwkManagerAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMgmtNwkUpdateNwkManagerAddr.Text = "NwkMan Addr (16-bit Hex)";
            }
        }

#endregion

#region Disc Cmds

        private void textBoxDiscoverCommandsTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverCommandsTargetAddr.Text))
            {
                textBoxDiscoverCommandsTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverCommandsTargetAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxDiscoverCommandsSrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverCommandsSrcEP.Text))
            {
                textBoxDiscoverCommandsSrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverCommandsSrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxDiscoverCommandsDstEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverCommandsDstEP.Text))
            {
                textBoxDiscoverCommandsDstEP.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverCommandsDstEP.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxDiscoverCommandsClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverCommandsClusterID.Text))
            {
                textBoxDiscoverCommandsClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverCommandsClusterID.Text = "Cluster (16-bit Hex)";
            }
        }

        private void textBoxDiscoverCommandsCommandID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverCommandsCommandID.Text))
            {
                textBoxDiscoverCommandsCommandID.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverCommandsCommandID.Text = "Cmd ID (8-bit Hex)";
            }
        }

        private void textBoxDiscoverCommandsManuID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverCommandsManuID.Text))
            {
                textBoxDiscoverCommandsManuID.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverCommandsManuID.Text = "Manu ID (16-bit Hex)";
            }
        }

        private void textBoxDiscoverCommandsMaxCommands_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDiscoverCommandsMaxCommands.Text))
            {
                textBoxDiscoverCommandsMaxCommands.ForeColor = System.Drawing.Color.Gray;
                textBoxDiscoverCommandsMaxCommands.Text = "Max Cmds (8-bit Hex)";
            }
        }

#endregion

#region Raw Data

        private void textBoxRawDataCommandsTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRawDataCommandsTargetAddr.Text))
            {
                textBoxRawDataCommandsTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxRawDataCommandsTargetAddr.Text = "Target (16-bit Hex)";
            }
        }

        private void textBoxRawDataCommandsSrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRawDataCommandsSrcEP.Text))
            {
                textBoxRawDataCommandsSrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxRawDataCommandsSrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxRawDataCommandsDstEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRawDataCommandsDstEP.Text))
            {
                textBoxRawDataCommandsDstEP.ForeColor = System.Drawing.Color.Gray;
                textBoxRawDataCommandsDstEP.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxRawDataCommandsProfileID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRawDataCommandsProfileID.Text))
            {
                textBoxRawDataCommandsProfileID.ForeColor = System.Drawing.Color.Gray;
                textBoxRawDataCommandsProfileID.Text = "Profile (16-bit Hex)";
            }
        }

        private void textBoxRawDataCommandsClusterID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRawDataCommandsClusterID.Text))
            {
                textBoxRawDataCommandsClusterID.ForeColor = System.Drawing.Color.Gray;
                textBoxRawDataCommandsClusterID.Text = "Cluster (16-bit Hex)";
            }
        }

        private void textBoxRawDataCommandsRadius_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRawDataCommandsRadius.Text))
            {
                textBoxRawDataCommandsRadius.ForeColor = System.Drawing.Color.Gray;
                textBoxRawDataCommandsRadius.Text = "Radius (8-bit Hex)";
            }
        }

        private void textBoxRawDataCommandsSecurityMode_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRawDataCommandsSecurityMode.Text))
            {
                textBoxRawDataCommandsSecurityMode.ForeColor = System.Drawing.Color.Gray;
                textBoxRawDataCommandsSecurityMode.Text = "Security Mode (8-bit Hex)";
            }
        }

        private void textBoxRawDataCommandsData_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRawDataCommandsData.Text))
            {
                textBoxRawDataCommandsData.ForeColor = System.Drawing.Color.Gray;
                textBoxRawDataCommandsData.Text = "Raw Data (Format: Byte:Byte:Byte)";
            }
        }

#endregion

#region OOB Commissioning Data

        private void textBoxOOBDataAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOOBDataAddr.Text))
            {
                textBoxOOBDataAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxOOBDataAddr.Text = "Address (64-bit Hex)";
            }
        }

        private void textBoxOOBDataKey_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOOBDataKey.Text))
            {
                textBoxOOBDataKey.ForeColor = System.Drawing.Color.Gray;
                textBoxOOBDataKey.Text = "Key (Format: Byte:Byte:Byte)";
            }
        }



#endregion

#region InstallCodeSend

        private void textBoxGeneralInstallCodeMACaddress_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGeneralInstallCodeMACaddress.Text))
            {
                textBoxGeneralInstallCodeMACaddress.ForeColor = System.Drawing.Color.Gray;
                textBoxGeneralInstallCodeMACaddress.Text = "MACAddress (64-bit Hex)";
            }
        }

        private void textBoxGeneralInstallCodeCode_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGeneralInstallCodeCode.Text))
            {
                textBoxGeneralInstallCodeCode.ForeColor = System.Drawing.Color.Gray;
                textBoxGeneralInstallCodeCode.Text = "InstallCode (128-bit Hex)";
            }
        }





#endregion

#endregion

#region AHIControlTab

#region DIO Set Dir

        private void textBoxDioSetDirectionInputPinMask_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDioSetDirectionInputPinMask.Text))
            {
                textBoxDioSetDirectionInputPinMask.ForeColor = System.Drawing.Color.Gray;
                textBoxDioSetDirectionInputPinMask.Text = "DIO Mask (32-bit Hex)";
            }
        }

        private void textBoxDioSetDirectionOutputPinMask_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDioSetDirectionOutputPinMask.Text))
            {
                textBoxDioSetDirectionOutputPinMask.ForeColor = System.Drawing.Color.Gray;
                textBoxDioSetDirectionOutputPinMask.Text = "DIO Mask (32-bit Hex)";
            }
        }

#endregion

#region DIO Set

        private void textBoxDioSetOutputOnPinMask_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDioSetOutputOnPinMask.Text))
            {
                textBoxDioSetOutputOnPinMask.ForeColor = System.Drawing.Color.Gray;
                textBoxDioSetOutputOnPinMask.Text = "DIO Mask (32-bit Hex)";
            }
        }

        private void textBoxDioSetOutputOffPinMask_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDioSetOutputOffPinMask.Text))
            {
                textBoxDioSetOutputOffPinMask.ForeColor = System.Drawing.Color.Gray;
                textBoxDioSetOutputOffPinMask.Text = "DIO Mask (32-bit Hex)";
            }
        }

#endregion

#region IPN Config

        private void textBoxIPNConfigDioRfActiveOutDioMask_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIPNConfigDioRfActiveOutDioMask.Text))
            {
                textBoxIPNConfigDioRfActiveOutDioMask.ForeColor = System.Drawing.Color.Gray;
                textBoxIPNConfigDioRfActiveOutDioMask.Text = "DIO Mask (32-bit Hex)";
            }
        }

        private void textBoxIPNConfigDioStatusOutDioMask_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIPNConfigDioStatusOutDioMask.Text))
            {
                textBoxIPNConfigDioStatusOutDioMask.ForeColor = System.Drawing.Color.Gray;
                textBoxIPNConfigDioStatusOutDioMask.Text = "DIO Mask (32-bit Hex)";
            }
        }

        private void textBoxIPNConfigDioTxConfInDioMask_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIPNConfigDioTxConfInDioMask.Text))
            {
                textBoxIPNConfigDioTxConfInDioMask.ForeColor = System.Drawing.Color.Gray;
                textBoxIPNConfigDioTxConfInDioMask.Text = "DIO Mask (32-bit Hex)";
            }
        }

        private void textBoxIPNConfigPollPeriod_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIPNConfigPollPeriod.Text))
            {
                textBoxIPNConfigPollPeriod.ForeColor = System.Drawing.Color.Gray;
                textBoxIPNConfigPollPeriod.Text = "Time (16-bit Hex)";
            }
        }

#endregion

#region TxPower

        private void textBoxAHITxPower_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAHITxPower.Text))
            {
                txPowerTextBoxInit(ref textBoxAHITxPower);
            }
        }

#endregion

#endregion

#region BasicClusterTab

#region Reset To FD

        private void textBoxBasicResetTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxBasicResetTargetAddr.Text))
            {
                textBoxBasicResetTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxBasicResetTargetAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxBasicResetSrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxBasicResetSrcEP.Text))
            {
                textBoxBasicResetSrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxBasicResetSrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxBasicResetDstEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxBasicResetDstEP.Text))
            {
                textBoxBasicResetDstEP.ForeColor = System.Drawing.Color.Gray;
                textBoxBasicResetDstEP.Text = "Dst EP (8-bit Hex)";
            }
        }

#endregion

#endregion

#region GroupClusterTab

#region Add Group

        private void textBoxAddGroupAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddGroupAddr.Text))
            {
                textBoxAddGroupAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxAddGroupAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxAddGroupSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddGroupSrcEp.Text))
            {
                textBoxAddGroupSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxAddGroupSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxAddGroupDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddGroupDstEp.Text))
            {
                textBoxAddGroupDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxAddGroupDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxAddGroupGroupAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddGroupGroupAddr.Text))
            {
                textBoxAddGroupGroupAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxAddGroupGroupAddr.Text = "Group ID (16-bit Hex)";
            }
        }

        private void textBoxGroupNameLength_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGroupNameLength.Text))
            {
                textBoxGroupNameLength.ForeColor = System.Drawing.Color.Gray;
                textBoxGroupNameLength.Text = "Name Len (8-bit Hex)";
            }
        }

        private void textBoxGroupNameMaxLength_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGroupNameMaxLength.Text))
            {
                textBoxGroupNameMaxLength.ForeColor = System.Drawing.Color.Gray;
                textBoxGroupNameMaxLength.Text = "Max Len (8-bit Hex)";
            }
        }

        private void textBoxGroupName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGroupName.Text))
            {
                textBoxGroupName.ForeColor = System.Drawing.Color.Gray;
                textBoxGroupName.Text = "Group Name (String)";
            }
        }

#endregion

#region View Group

        private void textBoxViewGroupAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxViewGroupAddr.Text))
            {
                textBoxViewGroupAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxViewGroupAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxViewGroupSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxViewGroupSrcEp.Text))
            {
                textBoxViewGroupSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxViewGroupSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxViewGroupDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxViewGroupDstEp.Text))
            {
                textBoxViewGroupDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxViewGroupDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxViewGroupGroupAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxViewGroupGroupAddr.Text))
            {
                textBoxViewGroupGroupAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxViewGroupGroupAddr.Text = "Group ID (16-bit Hex)";
            }
        }

#endregion

#region Get Group

        private void textBoxGetGroupTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGetGroupTargetAddr.Text))
            {
                textBoxGetGroupTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxGetGroupTargetAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxGetGroupSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGetGroupSrcEp.Text))
            {
                textBoxGetGroupSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxGetGroupSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxGetGroupDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGetGroupDstEp.Text))
            {
                textBoxGetGroupDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxGetGroupDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxGetGroupCount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGetGroupCount.Text))
            {
                textBoxGetGroupCount.ForeColor = System.Drawing.Color.Gray;
                textBoxGetGroupCount.Text = "Group ID (16-bit Hex)";
            }
        }

#endregion

#region Remove Grp

        private void textBoxRemoveGroupTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveGroupTargetAddr.Text))
            {
                textBoxRemoveGroupTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveGroupTargetAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxRemoveGroupSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveGroupSrcEp.Text))
            {
                textBoxRemoveGroupSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveGroupSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxRemoveGroupDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveGroupDstEp.Text))
            {
                textBoxRemoveGroupDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveGroupDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxRemoveGroupGroupAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveGroupGroupAddr.Text))
            {
                textBoxRemoveGroupGroupAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveGroupGroupAddr.Text = "Group ID (16-bit Hex)";
            }
        }

#endregion

#region Remove All

        private void textBoxRemoveAllGroupTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveAllGroupTargetAddr.Text))
            {
                textBoxRemoveAllGroupTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveAllGroupTargetAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxRemoveAllGroupSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveAllGroupSrcEp.Text))
            {
                textBoxRemoveAllGroupSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveAllGroupSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxRemoveAllGroupDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveAllGroupDstEp.Text))
            {
                textBoxRemoveAllGroupDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveAllGroupDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

#endregion

#region Add If Ident

        private void textBoxGroupAddIfIndentifyingTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGroupAddIfIndentifyingTargetAddr.Text))
            {
                textBoxGroupAddIfIndentifyingTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxGroupAddIfIndentifyingTargetAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxGroupAddIfIdentifySrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGroupAddIfIdentifySrcEp.Text))
            {
                textBoxGroupAddIfIdentifySrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxGroupAddIfIdentifySrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxGroupAddIfIdentifyDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGroupAddIfIdentifyDstEp.Text))
            {
                textBoxGroupAddIfIdentifyDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxGroupAddIfIdentifyDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxGroupAddIfIdentifyGroupID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGroupAddIfIdentifyGroupID.Text))
            {
                textBoxGroupAddIfIdentifyGroupID.ForeColor = System.Drawing.Color.Gray;
                textBoxGroupAddIfIdentifyGroupID.Text = "Group ID (16-bit Hex)";
            }
        }

#endregion

#endregion

#region IdentifyClusterTab

#region ID Send

        private void textBoxSendIdAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSendIdAddr.Text))
            {
                textBoxSendIdAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxSendIdAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxSendIdSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSendIdSrcEp.Text))
            {
                textBoxSendIdSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxSendIdSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxIdSendDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIdSendDstEp.Text))
            {
                textBoxIdSendDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxIdSendDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxIdSendTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIdSendTime.Text))
            {
                textBoxIdSendTime.ForeColor = System.Drawing.Color.Gray;
                textBoxIdSendTime.Text = "Time (16-bit Hex)";
            }
        }

#endregion

#region ID Query

        private void textBoxIdQueryAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIdQueryAddr.Text))
            {
                textBoxIdQueryAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxIdQueryAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxIdQuerySrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIdQuerySrcEp.Text))
            {
                textBoxIdQuerySrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxIdQuerySrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxIdQueryDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIdQueryDstEp.Text))
            {
                textBoxIdQueryDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxIdQueryDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

#endregion

#endregion

#region LevelClusterTab

#region MoveToLevel

        private void textBoxMoveToLevelAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToLevelAddr.Text))
            {
                textBoxMoveToLevelAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToLevelAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxMoveToLevelSrcEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToLevelSrcEndPoint.Text))
            {
                textBoxMoveToLevelSrcEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToLevelSrcEndPoint.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToLevelDstEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToLevelDstEndPoint.Text))
            {
                textBoxMoveToLevelDstEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToLevelDstEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToLevelLevel_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToLevelLevel.Text))
            {
                textBoxMoveToLevelLevel.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToLevelLevel.Text = "Level (8-bit Hex)";
            }
        }

        private void textBoxMoveToLevelTransTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToLevelTransTime.Text))
            {
                textBoxMoveToLevelTransTime.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToLevelTransTime.Text = "Time (16-bit Hex)";
            }
        }

#endregion

#endregion

#region OnOffClusterTab

#region OnOff

        private void textBoxOnOffAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOnOffAddr.Text))
            {
                textBoxOnOffAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxOnOffAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxOnOffSrcEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOnOffSrcEndPoint.Text))
            {
                textBoxOnOffSrcEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxOnOffSrcEndPoint.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxOnOffDstEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOnOffDstEndPoint.Text))
            {
                textBoxOnOffDstEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxOnOffDstEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

#endregion

#endregion

#region ScenesClusterTab

#region View Scene

        private void textBoxViewSceneAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxViewSceneAddr.Text))
            {
                textBoxViewSceneAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxViewSceneAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxViewSceneSrcEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxViewSceneSrcEndPoint.Text))
            {
                textBoxViewSceneSrcEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxViewSceneSrcEndPoint.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxViewSceneDstEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxViewSceneDstEndPoint.Text))
            {
                textBoxViewSceneDstEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxViewSceneDstEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxViewSceneGroupId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxViewSceneGroupId.Text))
            {
                textBoxViewSceneGroupId.ForeColor = System.Drawing.Color.Gray;
                textBoxViewSceneGroupId.Text = "Group ID (16-bit Hex)";
            }
        }

        private void textBoxViewSceneSceneId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxViewSceneSceneId.Text))
            {
                textBoxViewSceneSceneId.ForeColor = System.Drawing.Color.Gray;
                textBoxViewSceneSceneId.Text = "Scene ID (8-bit Hex)";
            }
        }

#endregion

#region Add Scene

        private void textBoxAddSceneAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneAddr.Text))
            {
                textBoxAddSceneAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxAddSceneSrcEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneSrcEndPoint.Text))
            {
                textBoxAddSceneSrcEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneSrcEndPoint.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxAddSceneDstEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneDstEndPoint.Text))
            {
                textBoxAddSceneDstEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneDstEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxAddSceneGroupId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneGroupId.Text))
            {
                textBoxAddSceneGroupId.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneGroupId.Text = "Group ID (16-bit Hex)";
            }
        }

        private void textBoxAddSceneSceneId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneSceneId.Text))
            {
                textBoxAddSceneSceneId.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneSceneId.Text = "Scene ID (8-bit Hex)";
            }
        }

        private void textBoxAddSceneTransTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneTransTime.Text))
            {
                textBoxAddSceneTransTime.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneTransTime.Text = "Time (16-bit Hex)";
            }
        }

        private void textBoxAddSceneName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneName.Text))
            {
                textBoxAddSceneName.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneName.Text = "Name (String)";
            }
        }

        private void textBoxAddSceneNameLen_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneNameLen.Text))
            {
                textBoxAddSceneNameLen.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneNameLen.Text = "Len (8-bit Hex)";
            }
        }

        private void textBoxAddSceneMaxNameLen_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneMaxNameLen.Text))
            {
                textBoxAddSceneMaxNameLen.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneMaxNameLen.Text = "Max Len (8-bit Hex)";
            }
        }

        private void textBoxAddSceneExtLen_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneExtLen.Text))
            {
                textBoxAddSceneExtLen.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneExtLen.Text = "Ext Len (16-bit Hex)";
            }
        }

        private void textBoxAddSceneData_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddSceneData.Text))
            {
                textBoxAddSceneData.ForeColor = System.Drawing.Color.Gray;
                textBoxAddSceneData.Text = "Data (Format: Byte:Byte:Byte)";
            }
        }

#endregion

#region Store Scene

        private void textBoxStoreSceneAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxStoreSceneAddr.Text))
            {
                textBoxStoreSceneAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxStoreSceneAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxStoreSceneSrcEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxStoreSceneSrcEndPoint.Text))
            {
                textBoxStoreSceneSrcEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxStoreSceneSrcEndPoint.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxStoreSceneDstEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxStoreSceneDstEndPoint.Text))
            {
                textBoxStoreSceneDstEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxStoreSceneDstEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxStoreSceneGroupId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxStoreSceneGroupId.Text))
            {
                textBoxStoreSceneGroupId.ForeColor = System.Drawing.Color.Gray;
                textBoxStoreSceneGroupId.Text = "Group ID (16-bit Hex)";
            }
        }

        private void textBoxStoreSceneSceneId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxStoreSceneSceneId.Text))
            {
                textBoxStoreSceneSceneId.ForeColor = System.Drawing.Color.Gray;
                textBoxStoreSceneSceneId.Text = "Scene ID (8-bit Hex)";
            }
        }

#endregion

#region Recall Scene

        private void textBoxRecallSceneAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRecallSceneAddr.Text))
            {
                textBoxRecallSceneAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxRecallSceneAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxRecallSceneSrcEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRecallSceneSrcEndPoint.Text))
            {
                textBoxRecallSceneSrcEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxRecallSceneSrcEndPoint.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxRecallSceneDstEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRecallSceneDstEndPoint.Text))
            {
                textBoxRecallSceneDstEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxRecallSceneDstEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxRecallSceneGroupId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRecallSceneGroupId.Text))
            {
                textBoxRecallSceneGroupId.ForeColor = System.Drawing.Color.Gray;
                textBoxRecallSceneGroupId.Text = "Group ID (16-bit Hex)";
            }
        }

        private void textBoxRecallSceneSceneId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRecallSceneSceneId.Text))
            {
                textBoxRecallSceneSceneId.ForeColor = System.Drawing.Color.Gray;
                textBoxRecallSceneSceneId.Text = "Scene ID (8-bit Hex)";
            }
        }

#endregion

#region Get Member

        private void textBoxGetSceneMembershipAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGetSceneMembershipAddr.Text))
            {
                textBoxGetSceneMembershipAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxGetSceneMembershipAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxGetSceneMembershipSrcEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGetSceneMembershipSrcEndPoint.Text))
            {
                textBoxGetSceneMembershipSrcEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxGetSceneMembershipSrcEndPoint.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxGetSceneMembershipDstEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGetSceneMembershipDstEndPoint.Text))
            {
                textBoxGetSceneMembershipDstEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxGetSceneMembershipDstEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxGetSceneMembershipGroupID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGetSceneMembershipGroupID.Text))
            {
                textBoxGetSceneMembershipGroupID.ForeColor = System.Drawing.Color.Gray;
                textBoxGetSceneMembershipGroupID.Text = "Group ID (16-bit Hex)";
            }
        }

#endregion

#region Remove All

        private void textBoxRemoveAllScenesAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveAllScenesAddr.Text))
            {
                textBoxRemoveAllScenesAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveAllScenesAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxRemoveAllScenesSrcEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveAllScenesSrcEndPoint.Text))
            {
                textBoxRemoveAllScenesSrcEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveAllScenesSrcEndPoint.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxRemoveAllScenesDstEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveAllScenesDstEndPoint.Text))
            {
                textBoxRemoveAllScenesDstEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveAllScenesDstEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxRemoveAllScenesGroupID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveAllScenesGroupID.Text))
            {
                textBoxRemoveAllScenesGroupID.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveAllScenesGroupID.Text = "Group ID (16-bit Hex)";
            }
        }

#endregion

#region Remove

        private void textBoxRemoveSceneAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveSceneAddr.Text))
            {
                textBoxRemoveSceneAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveSceneAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxRemoveSceneSrcEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveSceneSrcEndPoint.Text))
            {
                textBoxRemoveSceneSrcEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveSceneSrcEndPoint.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxRemoveSceneDstEndPoint_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveSceneDstEndPoint.Text))
            {
                textBoxRemoveSceneDstEndPoint.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveSceneDstEndPoint.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxRemoveSceneGroupID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveSceneGroupID.Text))
            {
                textBoxRemoveSceneGroupID.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveSceneGroupID.Text = "Group ID (16-bit Hex)";
            }
        }

        private void textBoxRemoveSceneSceneID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRemoveSceneSceneID.Text))
            {
                textBoxRemoveSceneSceneID.ForeColor = System.Drawing.Color.Gray;
                textBoxRemoveSceneSceneID.Text = "Scene ID (8-bit Hex)";
            }
        }

#endregion

#endregion

#region ColorClusterTab

#region MoveToHue

        private void textBoxMoveToHueAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToHueAddr.Text))
            {
                textBoxMoveToHueAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToHueAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxMoveToHueSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToHueSrcEp.Text))
            {
                textBoxMoveToHueSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToHueSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToHueDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToHueDstEp.Text))
            {
                textBoxMoveToHueDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToHueDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToHueHue_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToHueHue.Text))
            {
                textBoxMoveToHueHue.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToHueHue.Text = "Hue (8-bit Hex)";
            }
        }

        private void textBoxMoveToHueDir_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToHueDir.Text))
            {
                textBoxMoveToHueDir.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToHueDir.Text = "Dir (8-bit Hex)";
            }
        }

        private void textBoxMoveToHueTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToHueTime.Text))
            {
                textBoxMoveToHueTime.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToHueTime.Text = "Time (16-bit Hex)";
            }
        }

#endregion

#region MoveToColor

        private void textBoxMoveToColorAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorAddr.Text))
            {
                textBoxMoveToColorAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxMoveToColorSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorSrcEp.Text))
            {
                textBoxMoveToColorSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToColorDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorDstEp.Text))
            {
                textBoxMoveToColorDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToColorX_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorX.Text))
            {
                textBoxMoveToColorX.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorX.Text = "X (16-bit Hex)";
            }
        }

        private void textBoxMoveToColorY_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorY.Text))
            {
                textBoxMoveToColorY.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorY.Text = "Y (16-bit Hex)";
            }
        }

        private void textBoxMoveToColorTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorTime.Text))
            {
                textBoxMoveToColorTime.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorTime.Text = "Time (16-bit Hex)";
            }
        }

#endregion

#region MoveToSat

        private void textBoxMoveToSatAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToSatAddr.Text))
            {
                textBoxMoveToSatAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToSatAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxMoveToSatSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToSatSrcEp.Text))
            {
                textBoxMoveToSatSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToSatSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToSatDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToSatDstEp.Text))
            {
                textBoxMoveToSatDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToSatDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToSatSat_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToSatSat.Text))
            {
                textBoxMoveToSatSat.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToSatSat.Text = "Saturation (8-bit Hex)";
            }
        }

        private void textBoxMoveToSatTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToSatTime.Text))
            {
                textBoxMoveToSatTime.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToSatTime.Text = "Time (16-bit Hex)";
            }
        }

#endregion

#region MoveToTemp

        private void textBoxMoveToColorTempAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorTempAddr.Text))
            {
                textBoxMoveToColorTempAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorTempAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxMoveToColorTempSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorTempSrcEp.Text))
            {
                textBoxMoveToColorTempSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorTempSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToColorTempDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorTempDstEp.Text))
            {
                textBoxMoveToColorTempDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorTempDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxMoveToColorTempTemp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorTempTemp.Text))
            {
                textBoxMoveToColorTempTemp.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorTempTemp.Text = "TempK (16-bit Dec)";
            }
        }

        private void textBoxMoveToColorTempRate_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveToColorTempRate.Text))
            {
                textBoxMoveToColorTempRate.ForeColor = System.Drawing.Color.Gray;
                textBoxMoveToColorTempRate.Text = "Time (16-bit Hex)";
            }
        }

#endregion

#endregion

#region DoorLockClusterTab

#region LockUnlock

        private void textBoxLockUnlockAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLockUnlockAddr.Text))
            {
                textBoxLockUnlockAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxLockUnlockAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxLockUnlockSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLockUnlockSrcEp.Text))
            {
                textBoxLockUnlockSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxLockUnlockSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxLockUnlockDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLockUnlockDstEp.Text))
            {
                textBoxLockUnlockDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxLockUnlockDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

#endregion

#endregion

#region IASClusterTab

#region EnrollRsp

        private void textBoxEnrollRspAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEnrollRspAddr.Text))
            {
                textBoxEnrollRspAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxEnrollRspAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxEnrollRspSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEnrollRspSrcEp.Text))
            {
                textBoxEnrollRspSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxEnrollRspSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxEnrollRspDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEnrollRspDstEp.Text))
            {
                textBoxEnrollRspDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxEnrollRspDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxEnrollRspZone_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEnrollRspZone.Text))
            {
                textBoxEnrollRspZone.ForeColor = System.Drawing.Color.Gray;
                textBoxEnrollRspZone.Text = "Zone ID (8-bit Hex)";
            }
        }

#endregion

#endregion

#region ZLLOnOffClusterTab

#region OnOff Effects

        private void textBoxZllOnOffEffectsAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllOnOffEffectsAddr.Text))
            {
                textBoxZllOnOffEffectsAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxZllOnOffEffectsAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxZllOnOffEffectsSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllOnOffEffectsSrcEp.Text))
            {
                textBoxZllOnOffEffectsSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxZllOnOffEffectsSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxZllOnOffEffectsDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllOnOffEffectsDstEp.Text))
            {
                textBoxZllOnOffEffectsDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxZllOnOffEffectsDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxZllOnOffEffectsGradient_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllOnOffEffectsGradient.Text))
            {
                textBoxZllOnOffEffectsGradient.ForeColor = System.Drawing.Color.Gray;
                textBoxZllOnOffEffectsGradient.Text = "Gradient (8-bit Hex)";
            }
        }

#endregion

#endregion

#region ZLLColorClusterTab

#region Move To Hue

        private void textBoxZllMoveToHueAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllMoveToHueAddr.Text))
            {
                textBoxZllMoveToHueAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxZllMoveToHueAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxZllMoveToHueSrcEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllMoveToHueSrcEp.Text))
            {
                textBoxZllMoveToHueSrcEp.ForeColor = System.Drawing.Color.Gray;
                textBoxZllMoveToHueSrcEp.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxZllMoveToHueDstEp_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllMoveToHueDstEp.Text))
            {
                textBoxZllMoveToHueDstEp.ForeColor = System.Drawing.Color.Gray;
                textBoxZllMoveToHueDstEp.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxZllMoveToHueHue_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllMoveToHueHue.Text))
            {
                textBoxZllMoveToHueHue.ForeColor = System.Drawing.Color.Gray;
                textBoxZllMoveToHueHue.Text = "Hue (16-bit Hex)";
            }
        }

        private void textBoxZllMoveToHueDirection_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllMoveToHueDirection.Text))
            {
                textBoxZllMoveToHueDirection.ForeColor = System.Drawing.Color.Gray;
                textBoxZllMoveToHueDirection.Text = "Dir (8-bit Hex)";
            }
        }

        private void textBoxZllMoveToHueTransTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZllMoveToHueTransTime.Text))
            {
                textBoxZllMoveToHueTransTime.ForeColor = System.Drawing.Color.Gray;
                textBoxZllMoveToHueTransTime.Text = "Time (16-bit Hex)";
            }
        }

#endregion

#endregion

#region OTAClusterTab

#region Image Notify

        private void textBoxOTAImageNotifyTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTAImageNotifyTargetAddr.Text))
            {
                textBoxOTAImageNotifyTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxOTAImageNotifyTargetAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxOTAImageNotifySrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTAImageNotifySrcEP.Text))
            {
                textBoxOTAImageNotifySrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxOTAImageNotifySrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxOTAImageNotifyDstEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTAImageNotifyDstEP.Text))
            {
                textBoxOTAImageNotifyDstEP.ForeColor = System.Drawing.Color.Gray;
                textBoxOTAImageNotifyDstEP.Text = "Dst EP (8-bit Hex)";
            }
        }

        private void textBoxOTAImageNotifyFileVersion_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTAImageNotifyFileVersion.Text))
            {
                textBoxOTAImageNotifyFileVersion.ForeColor = System.Drawing.Color.Gray;
                textBoxOTAImageNotifyFileVersion.Text = "Version (32-bit Hex)";
            }
        }

        private void textBoxOTAImageNotifyImageType_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTAImageNotifyImageType.Text))
            {
                textBoxOTAImageNotifyImageType.ForeColor = System.Drawing.Color.Gray;
                textBoxOTAImageNotifyImageType.Text = "Image Type (16-bit Hex)";
            }
        }

        private void textBoxOTAImageNotifyManuID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTAImageNotifyManuID.Text))
            {
                textBoxOTAImageNotifyManuID.ForeColor = System.Drawing.Color.Gray;
                textBoxOTAImageNotifyManuID.Text = "Manu ID (16-bit Hex)";
            }
        }

        private void textBoxOTAImageNotifyJitter_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTAImageNotifyJitter.Text))
            {
                textBoxOTAImageNotifyJitter.ForeColor = System.Drawing.Color.Gray;
                textBoxOTAImageNotifyJitter.Text = "Query Jitter (8-bit Hex)";
            }
        }

#endregion

#region WaitParams

        private void textBoxOTASetWaitForDataParamsTargetAddr_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTASetWaitForDataParamsTargetAddr.Text))
            {
                textBoxOTASetWaitForDataParamsTargetAddr.ForeColor = System.Drawing.Color.Gray;
                textBoxOTASetWaitForDataParamsTargetAddr.Text = "Address (16-bit Hex)";
            }
        }

        private void textBoxOTASetWaitForDataParamsSrcEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTASetWaitForDataParamsSrcEP.Text))
            {
                textBoxOTASetWaitForDataParamsSrcEP.ForeColor = System.Drawing.Color.Gray;
                textBoxOTASetWaitForDataParamsSrcEP.Text = "Src EP (8-bit Hex)";
            }
        }

        private void textBoxOTASetWaitForDataParamsCurrentTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTASetWaitForDataParamsCurrentTime.Text))
            {
                textBoxOTASetWaitForDataParamsCurrentTime.ForeColor = System.Drawing.Color.Gray;
                textBoxOTASetWaitForDataParamsCurrentTime.Text = "Current Time (32-bit Hex)";
            }
        }

        private void textBoxOTASetWaitForDataParamsRequestTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTASetWaitForDataParamsRequestTime.Text))
            {
                textBoxOTASetWaitForDataParamsRequestTime.ForeColor = System.Drawing.Color.Gray;
                textBoxOTASetWaitForDataParamsRequestTime.Text = "Request Time (32-bit Hex)";
            }
        }

        private void textBoxOTASetWaitForDataParamsRequestBlockDelay_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTASetWaitForDataParamsRequestBlockDelay.Text))
            {
                textBoxOTASetWaitForDataParamsRequestBlockDelay.ForeColor = System.Drawing.Color.Gray;
                textBoxOTASetWaitForDataParamsRequestBlockDelay.Text = "Block Delay (16-bit Hex)";
            }
        }

#endregion

#endregion

#region PollControlTab

#region Check-In Rsp

        private void textBoxFastPollExpiryTime_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFastPollExpiryTime.Text))
            {
                textBoxFastPollExpiryTime.ForeColor = System.Drawing.Color.Gray;
                textBoxFastPollExpiryTime.Text = "Fast Poll Expiry (16-bit Hex)";
            }
        }

#endregion
#endregion

#region EZLNTTab

        private void textBoxEZLNTBINDCLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTBINDCLUSTERID.Text))
            {
                textBoxEZLNTBINDCLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTBINDCLUSTERID.Text = "cluster ID(hex)";
            }
        }


        private void textBoxEZLNTUNBINDCLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTUNBINDCLUSTERID.Text))
            {
                textBoxEZLNTUNBINDCLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTUNBINDCLUSTERID.Text = "cluster ID(hex)";
            }
        }


        private void textBoxEZLNTSETLOOP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTSETLOOP.Text))
            {
                textBoxEZLNTSETLOOP.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTSETLOOP.Text = "loop count";
            }
        }

        private void textBoxEZLNTTIMERINTERVAL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTTIMERINTERVAL.Text))
            {
                textBoxEZLNTTIMERINTERVAL.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTTIMERINTERVAL.Text = "timer interval";
            }
        }

        private void textBoxEZLNTVIEW_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTVIEW.Text))
            {
                textBoxEZLNTVIEW.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTVIEW.Text = "group address (hex)";
            }
        }

        private void textBoxREMOVEGROUP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxREMOVEGROUP.Text))
            {
                textBoxREMOVEGROUP.ForeColor = System.Drawing.Color.Gray;
                textBoxREMOVEGROUP.Text = "group address (hex)";
            }
        }

        private void textBoxEZLNTADDGROUP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTADDGROUP.Text))
            {
                textBoxEZLNTADDGROUP.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTADDGROUP.Text = "group address (hex)";
            }
        }

        private void textBoxEZLNTSETINTERVALMAX_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTSETINTERVALMAX.Text))
            {
                textBoxEZLNTSETINTERVALMAX.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTSETINTERVALMAX.Text = "time interval max (ms)";
            }
        }


        private void textBoxEZLNTSETSTEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTSETSTEP.Text))
            {
                textBoxEZLNTSETSTEP.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTSETSTEP.Text = "step";
            }
        }

        private void textBoxEZLNTSETDIR_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTSETDIR.Text))
            {
                textBoxEZLNTSETDIR.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTSETDIR.Text = "direction";
            }
        }


        private void textBoxEZLNTREADCLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTREADCLUSTERID.Text))
            {
                textBoxEZLNTREADCLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTREADCLUSTERID.Text = "cluster ID";
            }
        }

        private void textBoxEZLNTATTRIBUTEID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTATTRIBUTEID.Text))
            {
                textBoxEZLNTATTRIBUTEID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTATTRIBUTEID.Text = "attribute ID";
            }
        }


        //private void textBoxEZLNTATTRIBUTECOUNT_Leave(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(textBoxEZLNTATTRIBUTECOUNT.Text))
        //    {
        //        textBoxEZLNTATTRIBUTECOUNT.ForeColor = System.Drawing.Color.Gray;
        //        textBoxEZLNTATTRIBUTECOUNT.Text = "attribute count";
        //    }
        //}

        private void textBoxEZLNTWRITEATTRIBUTECLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTWRITEATTRIBUTECLUSTERID.Text))
            {
                textBoxEZLNTWRITEATTRIBUTECLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTWRITEATTRIBUTECLUSTERID.Text = "cluster ID";
            }
        }

        private void textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.Text))
            {
                textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.Text = "attribute ID";
            }
        }

        private void textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.Text))
            {
                textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.Text = "data type";
            }
        }

        private void textBoxEZLNTWRITEATTRIBUTEDATA_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTWRITEATTRIBUTEDATA.Text))
            {
                textBoxEZLNTWRITEATTRIBUTEDATA.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTWRITEATTRIBUTEDATA.Text = "data";
            }
        }

        private void textBoxEZLNTCONFIGRPRTCLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTCONFIGRPRTCLUSTERID.Text))
            {
                textBoxEZLNTCONFIGRPRTCLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTCONFIGRPRTCLUSTERID.Text = "cluster ID";
            }
        }

        private void textBoxEZLNTCONFIGRPRTTYPE_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTCONFIGRPRTTYPE.Text))
            {
                textBoxEZLNTCONFIGRPRTTYPE.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTCONFIGRPRTTYPE.Text = "data type";
            }
        }

        private void textBoxEZLNTCONFIGRPRTATTRIBID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTCONFIGRPRTATTRIBID.Text))
            {
                textBoxEZLNTCONFIGRPRTATTRIBID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTCONFIGRPRTATTRIBID.Text = "attribute ID";
            }
        }

        private void textBoxEZLNTCONFIGRPRTMININTERVAL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTCONFIGRPRTMININTERVAL.Text))
            {
                textBoxEZLNTCONFIGRPRTMININTERVAL.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTCONFIGRPRTMININTERVAL.Text = "report min interval";
            }
        }

        private void textBoxEZLNTCONFIGRPRTMAXINTERVAL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTCONFIGRPRTMAXINTERVAL.Text))
            {
                textBoxEZLNTCONFIGRPRTMAXINTERVAL.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTCONFIGRPRTMAXINTERVAL.Text = "report max interval";
            }
        }

        private void textBoxEZLNTCONFIGRPRTTIMEOUT_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTCONFIGRPRTTIMEOUT.Text))
            {
                textBoxEZLNTCONFIGRPRTTIMEOUT.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTCONFIGRPRTTIMEOUT.Text = "report timeout";
            }
        }

        private void textBoxEZLNTCONFIGRPRTCHANGE_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTCONFIGRPRTCHANGE.Text))
            {
                textBoxEZLNTCONFIGRPRTCHANGE.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTCONFIGRPRTCHANGE.Text = "report change";
            }
        }

        private void textBoxEZLNTREADRPRTCLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTREADRPRTCLUSTERID.Text))
            {
                textBoxEZLNTREADRPRTCLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTREADRPRTCLUSTERID.Text = "cluster ID";
            }
        }

        private void textBoxEZLNTREADRPRTATTRIBUTEID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTREADRPRTATTRIBUTEID.Text))
            {
                textBoxEZLNTREADRPRTATTRIBUTEID.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTREADRPRTATTRIBUTEID.Text = "attribute ID";
            }
        }

        private void textBoxEZLNTIDENTIFYTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTIDENTIFYTIME.Text))
            {
                textBoxEZLNTIDENTIFYTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTIDENTIFYTIME.Text = "identify time";
            }
        }

        private void textBoxEZLNTLEVEL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTLEVEL.Text))
            {
                textBoxEZLNTLEVEL.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTLEVEL.Text = "level";
            }
        }

        private void textBoxEZLNTLEVELTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTLEVELTIME.Text))
            {
                textBoxEZLNTLEVELTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTLEVELTIME.Text = "time";
            }
        }

        private void textBoxEZLNTHUE_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTHUE.Text))
            {
                textBoxEZLNTHUE.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTHUE.Text = "hue";
            }
        }

        private void textBoxEZLNTHUEDIR_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTHUEDIR.Text))
            {
                textBoxEZLNTHUEDIR.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTHUEDIR.Text = "hue direction";
            }
        }

        private void textBoxEZLNTHUETIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTHUETIME.Text))
            {
                textBoxEZLNTHUETIME.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTHUETIME.Text = "time";
            }
        }

        private void textBoxEZLNTCOLORX_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTCOLORX.Text))
            {
                textBoxEZLNTCOLORX.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTCOLORX.Text = "color X";
            }
        }

        private void textBoxEZLNTCOLORY_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTCOLORY.Text))
            {
                textBoxEZLNTCOLORY.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTCOLORY.Text = "color Y";
            }
        }


        private void textBoxCOLORTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCOLORTIME.Text))
            {
                textBoxCOLORTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxCOLORTIME.Text = "time";
            }
        }

        private void textBoxEZLNTSAT_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTSAT.Text))
            {
                textBoxEZLNTSAT.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTSAT.Text = "saturation";
            }
        }

        private void textBoxEZLNTSATTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTSATTIME.Text))
            {
                textBoxEZLNTSATTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTSATTIME.Text = "time";
            }
        }

        private void textBoxEZLNTTEMP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTTEMP.Text))
            {
                textBoxEZLNTTEMP.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTTEMP.Text = "temp";
            }
        }

        private void textBoxEZLNTTEMPTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEZLNTTEMPTIME.Text))
            {
                textBoxEZLNTTEMPTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxEZLNTTEMPTIME.Text = "time";
            }
        }



        #endregion


        #region LNT Remote Tab
        private void textBoxLNTSETLOOP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTSETLOOP.Text))
            {
                textBoxLNTSETLOOP.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTSETLOOP.Text = "loop";
            }
        }

        private void textBoxLNTSETPARAMININTERVAL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTSETPARAMININTERVAL.Text))
            {
                textBoxLNTSETPARAMININTERVAL.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTSETPARAMININTERVAL.Text = "min interval(ms)";
            }
        }


        private void textBoxLNTSETPARAMAXINTERVAL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTSETPARAMAXINTERVAL.Text))
            {
                textBoxLNTSETPARAMAXINTERVAL.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTSETPARAMAXINTERVAL.Text = "max interval(ms)";
            }
        }

        private void textBoxLNTSETPARASTEP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTSETPARASTEP.Text))
            {
                textBoxLNTSETPARASTEP.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTSETPARASTEP.Text = "step";
            }
        }


        private void textBoxLNTSETPARADIR_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTSETPARADIR.Text))
            {
                textBoxLNTSETPARADIR.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTSETPARADIR.Text = "direction";
            }
        }

        private void textBoxLNTREADATTRCLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTREADATTRCLUSTERID.Text))
            {
                textBoxLNTREADATTRCLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTREADATTRCLUSTERID.Text = "cluster ID";
            }
        }


        private void textBoxLNTREADATTRATTRIBUTEID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTREADATTRATTRIBUTEID.Text))
            {
                textBoxLNTREADATTRATTRIBUTEID.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTREADATTRATTRIBUTEID.Text = "attribute ID";
            }
        }


        private void textBoxLNTREADATTRATTRIBUTECOUNT_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTREADATTRATTRIBUTECOUNT.Text))
            {
                textBoxLNTREADATTRATTRIBUTECOUNT.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTREADATTRATTRIBUTECOUNT.Text = "attribute count";
            }
        }

        private void textBoxLNTWRITEATTRCLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTWRITEATTRCLUSTERID.Text))
            {
                textBoxLNTWRITEATTRCLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTWRITEATTRCLUSTERID.Text = "cluster ID";
            }
        }

        private void textBoxLNTWRITEATTRATTRID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTWRITEATTRATTRID.Text))
            {
                textBoxLNTWRITEATTRATTRID.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTWRITEATTRATTRID.Text = "attribute ID";
            }
        }


        private void textBoxLNTWRITEATTRDATATYPE_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTWRITEATTRDATATYPE.Text))
            {
                textBoxLNTWRITEATTRDATATYPE.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTWRITEATTRDATATYPE.Text = "attribute type";
            }
        }


        private void textBoxLNTWRITEATTRDATA_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTWRITEATTRDATA.Text))
            {
                textBoxLNTWRITEATTRDATA.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTWRITEATTRDATA.Text = "data";
            }
        }


        private void textBoxLNTBINDIEEEADDR_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTBINDIEEEADDR.Text))
            {
                textBoxLNTBINDIEEEADDR.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTBINDIEEEADDR.Text = "cluster ID";
            }
        }


        private void textBoxLNTUNBINDIEEEADDR_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTUNBINDIEEEADDR.Text))
            {
                textBoxLNTUNBINDIEEEADDR.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTUNBINDIEEEADDR.Text = "cluster ID";
            }
        }


        private void textBoxLNTCONFIGRPRTCLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTCONFIGRPRTCLUSTERID.Text))
            {
                textBoxLNTCONFIGRPRTCLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTCONFIGRPRTCLUSTERID.Text = "cluster ID";
            }
        }


        private void textBoxCONFIGRPRTATTRID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCONFIGRPRTATTRID.Text))
            {
                textBoxCONFIGRPRTATTRID.ForeColor = System.Drawing.Color.Gray;
                textBoxCONFIGRPRTATTRID.Text = "attribute ID";
            }
        }


        private void textBoxLNTCONFIGRPRTTYPE_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTCONFIGRPRTTYPE.Text))
            {
                textBoxLNTCONFIGRPRTTYPE.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTCONFIGRPRTTYPE.Text = "type";
            }
        }

        private void textBoxCONFIGRPRTMININTERVAL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCONFIGRPRTMININTERVAL.Text))
            {
                textBoxCONFIGRPRTMININTERVAL.ForeColor = System.Drawing.Color.Gray;
                textBoxCONFIGRPRTMININTERVAL.Text = "min interval";
            }
        }


        private void textBoxCONFIGRPRTMAXRPRTINTERVAL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCONFIGRPRTMAXRPRTINTERVAL.Text))
            {
                textBoxCONFIGRPRTMAXRPRTINTERVAL.ForeColor = System.Drawing.Color.Gray;
                textBoxCONFIGRPRTMAXRPRTINTERVAL.Text = "max interval";
            }
        }

        private void textBoxLNTCONFIGRPRTTIMEOUT_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTCONFIGRPRTTIMEOUT.Text))
            {
                textBoxLNTCONFIGRPRTTIMEOUT.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTCONFIGRPRTTIMEOUT.Text = "time out";
            }
        }


        private void textBoxLNTCONFIGRPRTCHANGE_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTCONFIGRPRTCHANGE.Text))
            {
                textBoxLNTCONFIGRPRTCHANGE.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTCONFIGRPRTCHANGE.Text = "change";
            }
        }


        private void textBoxLNTREADRPRTCLUSTERID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTREADRPRTCLUSTERID.Text))
            {
                textBoxLNTREADRPRTCLUSTERID.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTREADRPRTCLUSTERID.Text = "cluster ID";
            }
        }


        private void textBoxLNTREADRPRTATTRID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTREADRPRTATTRID.Text))
            {
                textBoxLNTREADRPRTATTRID.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTREADRPRTATTRID.Text = "attribute ID";
            }
        }


        private void textBoxLNTIDENTIFYTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTIDENTIFYTIME.Text))
            {
                textBoxLNTIDENTIFYTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTIDENTIFYTIME.Text = "time";
            }
        }


        private void textBoxLNTADDGROUPADDR_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTADDGROUPADDR.Text))
            {
                textBoxLNTADDGROUPADDR.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTADDGROUPADDR.Text = "address";
            }
        }


        private void textBoxLNTREMOVEGROUPADDRESS_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTREMOVEGROUPADDRESS.Text))
            {
                textBoxLNTREMOVEGROUPADDRESS.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTREMOVEGROUPADDRESS.Text = "address";
            }
        }


        private void textBoxLNTVIEWGROUPADDRESS_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTVIEWGROUPADDRESS.Text))
            {
                textBoxLNTVIEWGROUPADDRESS.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTVIEWGROUPADDRESS.Text = "address";
            }
        }


        private void textBoxLNTLEVEL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTLEVEL.Text))
            {
                textBoxLNTLEVEL.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTLEVEL.Text = "level";
            }
        }


        private void textBoxLNTLEVELTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTLEVELTIME.Text))
            {
                textBoxLNTLEVELTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTLEVELTIME.Text = "time";
            }
        }

        private void textBoxLNTHUE_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTHUE.Text))
            {
                textBoxLNTHUE.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTHUE.Text = "hue";
            }
        }


        private void textBoxLNTHUEDIR_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTHUEDIR.Text))
            {
                textBoxLNTHUEDIR.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTHUEDIR.Text = "hue direction";
            }
        }

        private void textBoxLNTHUETIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTHUETIME.Text))
            {
                textBoxLNTHUETIME.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTHUETIME.Text = "time";
            }
        }


        private void textBoxLNTCOLORX_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTCOLORX.Text))
            {
                textBoxLNTCOLORX.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTCOLORX.Text = "color X";
            }
        }


        private void textBoxLNTCOLORY_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTCOLORY.Text))
            {
                textBoxLNTCOLORY.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTCOLORY.Text = "color Y";
            }
        }

        private void textBoxLNTCOLORTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTCOLORTIME.Text))
            {
                textBoxLNTCOLORTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTCOLORTIME.Text = "time";
            }
        }


        private void textBoxLNTSAT_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTSAT.Text))
            {
                textBoxLNTSAT.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTSAT.Text = "saturation";
            }
        }


        private void textBoxLNTSATTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTSATTIME.Text))
            {
                textBoxLNTSATTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTSATTIME.Text = "time";
            }
        }


        private void textBoxLNTTEMP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTTEMP.Text))
            {
                textBoxLNTTEMP.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTTEMP.Text = "temp";
            }
        }

        private void textBoxLNTTEMPTIME_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLNTTEMPTIME.Text))
            {
                textBoxLNTTEMPTIME.ForeColor = System.Drawing.Color.Gray;
                textBoxLNTTEMPTIME.Text = "time";
            }
        }

        #endregion



        #region PageClick

        private void tabPage2_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage12_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void AHIControl_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void BasicClusterTab_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage13_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage15_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage9_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage10_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage11_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPage14_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

        private void tabPagePollControl_Click(object sender, EventArgs e)
        {
            richTextBoxCommandResponse.Focus();
        }

#endregion

#endregion

        private void comboBoxAddressList_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxExtAddr.Text = au64ExtAddr[comboBoxAddressList.SelectedIndex].ToString("X8");
        }

        private void checkBoxShowExtension_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowExtension.Checked == true)
            {
                textBoxAddSceneTransTime.Visible = true;
                textBoxAddSceneName.Visible = true;
                textBoxAddSceneNameLen.Visible = true;
                textBoxAddSceneMaxNameLen.Visible = true;
                textBoxAddSceneExtLen.Visible = true;
                textBoxAddSceneData.Visible = true;
            }
            else if (checkBoxShowExtension.Checked != true)
            {

                textBoxAddSceneTransTime.Visible = false;
                textBoxAddSceneName.Visible = false;
                textBoxAddSceneNameLen.Visible = false;
                textBoxAddSceneMaxNameLen.Visible = false;
                textBoxAddSceneExtLen.Visible = false;
                textBoxAddSceneData.Visible = false;
            }
        }

        private void PollInterval_Click(object sender, EventArgs e)
        {
            byte[] commandData = null;
            commandData = new byte[32];
            byte u8Len = 0;
            UInt32 u32PollInterval;
            if (bStringToUint32(textBoxPollInterval.Text, out u32PollInterval) == true)
            {
                commandData[u8Len++] = (byte)(u32PollInterval >> 24);
                commandData[u8Len++] = (byte)(u32PollInterval >> 16);
                commandData[u8Len++] = (byte)(u32PollInterval >> 8);
                commandData[u8Len++] = (byte)(u32PollInterval);
                // Transmit command
                transmitCommand(0x002D, u8Len, commandData);
            }
        }

        private void textBoxPollInterval_MouseHover(object sender, EventArgs e)
        {
            showToolTipWindow("Poll Interval to be used (32-bit Hex) milliseconds");
        }

        private void textBoxPollInterval_MouseLeave(object sender, EventArgs e)
        {
            hideToolTipWindow();
        }

        private void textBoxPollInterval_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBoxPollInterval.ForeColor != System.Drawing.Color.Black)
            {
                textBoxPollInterval.ForeColor = System.Drawing.Color.Black;
                textBoxPollInterval.Text = "";
            }
        }

        private void buttonNciCmd_Click(object sender, EventArgs e)
        {
            setNciCmd((byte)comboBoxNciCmd.SelectedIndex);
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBoxFastPollEnable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxMoveToLevelOnOff_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxOnOffAddrMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textInstallCodeCode_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBoxMoveToLevelAddr_TextChanged(object sender, EventArgs e)
        {

        }

        private void InstallCode_Click(object sender, EventArgs e)
        {

        }

        private void textBoxInstallCodeMACaddress_MouseHover(object sender, EventArgs e)
        {

        }

        private void textBoxGeneralInstallCodeCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxGeneralInstallCodeCodePrint_Click(object sender, EventArgs e)
        {

        }

        private void textBoxOtaFileImageType_TextChanged(object sender, EventArgs e)
        {

        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBoxGeneralInstallCodeMACaddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEZLNTSendCommand_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBoxFastPollExpiryTime_TextChanged(object sender, EventArgs e)
        {

        }

        void checkedItems()
        {
            checkedAddress.Clear();
            checkedCOM.Clear();
            checkedCOMLocation.Clear();
            checkedIndex.Clear();

            for (int i = 0; i < listViewEZLNTINFO.CheckedItems.Count; i++)
            {
                string s = listViewEZLNTINFO.CheckedItems[i].SubItems[1].Text;


                if (s != "0xffff" && s != "")
                {
                    s = s.Remove(0, 2);
                    checkedAddress.Add(s);
                }
                s = listViewEZLNTINFO.CheckedItems[i].SubItems[0].Text;
                if (s != "")
                {
                    String[] str = Regex.Split(s, ". ", RegexOptions.IgnoreCase);

                    checkedCOM.Add(str[1]);
                    int index = int.Parse(str[0]);
                    checkedIndex.Add(index);
                    checkedCOMLocation.Add(listViewEZLNTINFO.CheckedItems[i].SubItems[6].Text);
                }
              
            }

        }
       
        private void listViewEZLNTINFO_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            checkedItems();
            if (listViewEZLNTINFO.CheckedItems.Count == 0)
            {
                bselected = false;
            }
            else
            {
                indexList.Clear();                 
                bselected = true;
                previousistViewEZLNTINFOcheckedIntemsCount = listViewEZLNTINFO.CheckedItems.Count;
            }
              
            
        }

        private void checkBoxEZLNTALL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEZLNTALL.Checked)
            {
                for (int i = 0; i < listViewEZLNTINFO.Items.Count; i++)
                {
                    listViewEZLNTINFO.Items[i].Checked = true;
                }

            }
            else
            {
                for (int i = 0; i < listViewEZLNTINFO.Items.Count; i++)
                {
                    listViewEZLNTINFO.Items[i].Checked = false;
                }
            }
        }

        private void listViewEZLNTGROUP_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // e.Item.Selected = e.Item.Checked;
            if (listViewEZLNTGROUP.CheckedItems.Count != 0)
            {
                groupCheckedIntemsCount = listViewEZLNTGROUP.CheckedItems.Count;
                nwkAddrJoinedNodeChecked.Clear();
                checkedNodeJoined = (uint)listViewEZLNTGROUP.CheckedItems.Count;
                NodeJoined = (uint)listViewEZLNTGROUP.Items.Count;
                for (int i = 0; i < checkedNodeJoined; i++)
                {
                    int index = listViewEZLNTGROUP.CheckedIndices[i];
                    string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                    nwkAddrJoinedNodeChecked.Add(s);
                }

                //richTextBoxMessageView.Text += "\r\n";
                //richTextBoxMessageView.Text += "Group Selected COM: \r\n";
                //for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
                //{
                //    int index = listViewEZLNTGROUP.CheckedIndices[i];

                //    {
                //        richTextBoxMessageView.Text += index;
                //        richTextBoxMessageView.Text += ", ";
                //    }

                //}
                //richTextBoxMessageView.Text += "\r\n";

            }
            else
            {
              //  richTextBoxMessageView.Text += "Group None COM Selected! \r\n";

            }
        }

        private void listViewEZLNTGROUP_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void textBoxREMOVEGROUP_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void timerReadAttribute_Tick(object sender, EventArgs e)
        {
            UInt16 u16ShortAddr;
            if (listViewEZLNTGROUP.CheckedItems.Count > 0)
            {
                if (readAttributeLoop == 0)
                {
                    timerReadAttribute.Stop();
                    return;
                }

                int index = listViewEZLNTGROUP.CheckedIndices[readAttributeCount];
                string s = listViewEZLNTGROUP.Items[index].SubItems[0].Text;
                if (bStringToUint16(s.Remove(0, s.Length - 4), out u16ShortAddr) == true)
                {
                    sendReadAttribRequest(u16ShortAddr, 1, 1, 0, 0, 0, 0, 1, 0);
                }

                readAttributeCount++;

                if (readAttributeCount == listViewEZLNTGROUP.CheckedItems.Count)
                {
                    readAttributeLoop--;
                    readAttributeCount = 0;
                }
            }
            
        }

        private void tabPage16_Click(object sender, EventArgs e)
        {

        }

#region  listview drag

#region EZLNT Tab

        private void listViewEZLNTINFO_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listViewEZLNTINFO.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listViewEZLNTINFO_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void listViewEZLNTINFO_DragOver(object sender, DragEventArgs e)
        {
            
            Point point = listViewEZLNTINFO.PointToClient(new Point(e.X, e.Y));
            
            int index = listViewEZLNTINFO.InsertionMark.NearestIndex(point);
           
            if (index > -1)
            {
                Rectangle itemBounds = listViewEZLNTINFO.GetItemRect(index);
                if (point.X > itemBounds.Left + (itemBounds.Width / 2))
                {
                    listViewEZLNTINFO.InsertionMark.AppearsAfterItem = true;
                }
                else
                {
                    listViewEZLNTINFO.InsertionMark.AppearsAfterItem = false;
                }
            }
            listViewEZLNTINFO.InsertionMark.Index = index;
          
        }

        private void listViewEZLNTINFO_DragLeave(object sender, EventArgs e)
        {
            listViewEZLNTINFO.InsertionMark.Index = -1;
        }

        private void listViewEZLNTINFO_DragDrop(object sender, DragEventArgs e)
        {

            int index = listViewEZLNTINFO.InsertionMark.Index;

            if (index == -1)
            {
                return;
            }

            if (listViewEZLNTINFO.InsertionMark.AppearsAfterItem)
            {
                index++;
            }

            ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            listViewEZLNTINFO.Items.Remove(item);

            
            string str = item.SubItems[0].Text.Remove(0, 1);
            item.SubItems[0].Text = index.ToString() + str;
            listViewEZLNTINFO.Items.Insert(index, item);

            //update comHashTable & listViewEZLNTINFO 
            comHashTable.Clear();
            indexComHashTable.Clear();

            for (int i = 0; i < listViewEZLNTINFO.Items.Count; i++)
            {
                string s = listViewEZLNTINFO.Items[i].SubItems[0].Text;
                string[] sArray = Regex.Split(s,". ", RegexOptions.IgnoreCase);

                if (sArray[1] != "")
                {
                    comHashTable.Add(sArray[1], i);
                    indexComHashTable.Add(i, sArray[1]);
                }

                s = i.ToString() +". "+ sArray[1];
                listViewEZLNTINFO.Items[i].SubItems[0].Text = s;
            }

            //update listViewEZLNTGROUP
            groupListViewRedisplayAccordingCOM();

#endregion

#endregion
        }

        private void buttonEZLNTTONGGLESTOP_Click(object sender, EventArgs e)
        {
            tonggleThreadStop = true;
            timeEndPeriod(1);
        }

        private void buttonSECADDNEWNETKEY_Click(object sender, EventArgs e)
        {
            byte u8SeqNbr;
            byte[] au8keyData;

            if (bStringToUint8(textBoxSECADDNETKEYSEQ.Text, out u8SeqNbr) == true)
            {
                if (bStringToUint128(textBoxSECNEWNETKEY.Text, out au8keyData) == true)
                {
                    // Set key state information
                    addSecurityNetKey(u8SeqNbr, au8keyData);
                }
            }

        }

        private void buttonSECSWITCHNETKEY_Click(object sender, EventArgs e)
        {
            byte u8SeqNbr;
            if (bStringToUint8(textBoxSECADDNETKEYSEQ.Text, out u8SeqNbr) == true)
            {

                switchSecurityNetKey(u8SeqNbr);
            }
        }

        private void listViewEZLNTGROUP_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBoxLNTALL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLNTALL.Checked)
            {
                for (int i = 0; i < listViewLNTCOMINFO.Items.Count; i++)
                {
                    listViewLNTCOMINFO.Items[i].Checked = true;
                }

            }
            else
            {
                for (int i = 0; i < listViewLNTCOMINFO.Items.Count; i++)
                {
                    listViewLNTCOMINFO.Items[i].Checked = false;
                }
            }
        }

        private void tabPage17_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxLNTGROUPALL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLNTGROUPALL.Checked)
            {
                for (int i = 0; i < listViewLNTGROUPINFO.Items.Count; i++)
                {
                    listViewLNTGROUPINFO.Items[i].Checked = true;
                }

            }
            else
            {
                for (int i = 0; i < listViewLNTGROUPINFO.Items.Count; i++)
                {
                    listViewLNTGROUPINFO.Items[i].Checked = false;
                }
            }
        }

        private void listViewLNTGROUPINFO_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // e.Item.Selected = e.Item.Checked;
            if (listViewLNTGROUPINFO.CheckedItems.Count != 0)
            {
                groupCheckedIntemsCount = listViewLNTGROUPINFO.CheckedItems.Count;
                nwkAddrJoinedNodeChecked2.Clear();
                checkedNodeJoined2 = (uint)listViewLNTGROUPINFO.CheckedItems.Count;

                for (int i = 0; i < checkedNodeJoined2; i++)
                {
                    int index = listViewLNTGROUPINFO.CheckedIndices[i];
                    string s = listViewLNTGROUPINFO.Items[index].SubItems[0].Text;
                    nwkAddrJoinedNodeChecked2.Add(s);
                }

                //richTextBoxMessageView.Text += "\r\n";
                //richTextBoxMessageView.Text += "Group Selected COM: \r\n";
                //for (int i = 0; i < listViewEZLNTGROUP.CheckedItems.Count; i++)
                //{
                //    int index = listViewEZLNTGROUP.CheckedIndices[i];

                //    {
                //        richTextBoxMessageView.Text += index;
                //        richTextBoxMessageView.Text += ", ";
                //    }

                //}
                //richTextBoxMessageView.Text += "\r\n";

            }
            else
            {
                richTextBoxMessageView.Text += "Group None COM Selected! \r\n";

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageIndex = tabControl1.SelectedIndex;
            string inputString;
            if (pageIndex == 20)
            {
                if (textBoxLNTSETLOOP.Text != "")
                { 
                    inputString = textBoxLNTSETLOOP.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out readAttributeLoop);
                }
                if (textBoxLNTSETPARAMININTERVAL.Text != "")
                { 
                    inputString = textBoxLNTSETPARAMININTERVAL.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out msCountMin);
                }
                if (textBoxLNTSETLOOP.Text != "")
                { 
                    inputString = textBoxLNTSETPARAMAXINTERVAL.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out msCountMax);
                }
                if (textBoxLNTSETLOOP.Text != "")
                { 
                    inputString = textBoxLNTSETPARASTEP.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out step);
                }
                if (textBoxLNTSETLOOP.Text != "")
                { 
                    inputString = textBoxLNTSETPARADIR.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out setDir);
                }
            }
            else if(pageIndex == 19)
            {
                if (textBoxEZLNTSETLOOP.Text != "")
                { 
                    inputString = textBoxEZLNTSETLOOP.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out readAttributeLoop);
                }
                if (textBoxEZLNTTIMERINTERVAL.Text != "")
                { 
                    inputString = textBoxEZLNTTIMERINTERVAL.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out msCountMin);
                }
                if (textBoxEZLNTSETINTERVALMAX.Text != "")
                { 
                    inputString = textBoxEZLNTSETINTERVALMAX.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out msCountMax);
                }
                if (textBoxEZLNTSETSTEP.Text != "")
                { 
                    inputString = textBoxEZLNTSETSTEP.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out step);
                }
                if (textBoxEZLNTSETDIR.Text != "")
                {
                    inputString = textBoxEZLNTSETDIR.Text;
                    UInt16.TryParse(inputString, NumberStyles.Integer, CultureInfo.CurrentCulture, out setDir);
                }

            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEZLNTGROUPALL.Checked)
            {
                for (int i = 0; i < listViewEZLNTGROUP.Items.Count; i++)
                {
                    listViewEZLNTGROUP.Items[i].Checked = true;
                }

            }
            else
            {
                for (int i = 0; i < listViewEZLNTGROUP.Items.Count; i++)
                {
                    listViewEZLNTGROUP.Items[i].Checked = false;
                }
            }
        }

        public class ListViewItemComparer : IComparer
        {
            //private int col = 4;
            //public int Compare(object x, object y)
            //{
            //    int returnVal = -1;
            //    returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
            //    ((ListViewItem)y).SubItems[col].Text);
            //    return returnVal;
            //}
            private int[] col; //要排序的列的索引，int数组
            private bool m_Asc;//true按升序false降序排序，
            public ListViewItemComparer()
            {
            }
            public ListViewItemComparer(int[] column, bool p_Asc) //构造函数
            {
                col = new int[column.Length];
                for (int i = 0; i < column.Length; i++)
                {
                    col[i] = column[i];
                }
                m_Asc = p_Asc;
            }

            //实现compare方法
            public int Compare(object x, object y)
            {
                string compStrX = "", compStrY = "";
                for (int i = 0; i < col.Length; i++)
                {
                    //,如果你的字段值有*号，你可以换成别的分割符号
                    compStrX += ((ListViewItem)x).SubItems[col[i]].Text.Trim() + "*";
                    compStrY += ((ListViewItem)y).SubItems[col[i]].Text.Trim() + "*";
                }
                if (m_Asc)
                {
                    // 从小到大排序
                    return myStrCmp(compStrX.Trim(), compStrY.Trim());
                }
                else
                {
                    //要将x,y反过来比较
                    return myStrCmp(compStrY.Trim(), compStrX.Trim());
                }
            }

            /*
            * 逐个比较用*分开的字符串 
            */
            private int myStrCmp(string strA, string strB)
            {
                string[] SA = strA.Split(new char[] { '*' });
                string[] SB = strB.Split(new char[] { '*' });
                for (int i = 0; i < SA.Length; i++)
                {
                    if (String.Compare(SA[i], SB[i]) != 0)
                    {
                        return String.Compare(SA[i], SB[i]);
                    }

                }
                return 0;
            }

        }

        public void updateCOMListHashTable()
        {
            indexMacAddrHashTable.Clear();
            indexComHashTable.Clear();
            comHashTable.Clear();
            for (int i = 0; i < listViewEZLNTINFO.Items.Count; i++)
            {
                if (listViewEZLNTINFO.Items[i].SubItems[0].Text != "")
                {
                    string s = listViewEZLNTINFO.Items[i].SubItems[0].Text;
                    string[] sArray = Regex.Split(s, ". ", RegexOptions.IgnoreCase);

                    if (sArray[1] != "")
                    {
                        comHashTable.Add(sArray[1], i);
                        indexComHashTable.Add(i, sArray[1]);
                        listViewEZLNTINFO.Items[i].SubItems[0].Text = i.ToString() +". "+sArray[1];
                        string str = listViewEZLNTINFO.Items[i].SubItems[2].Text;
                        if (!indexMacAddrHashTable.Contains(str))
                        {
                            indexMacAddrHashTable.Add(str,i);
                        }
                    }

                }

            }
            groupListViewRedisplayAccordingCOM();
        }

        private void listViewEZLNTINFO_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            int[] cols = new int[] {10,4};
            listViewEZLNTINFO.ListViewItemSorter = new ListViewItemComparer(cols,true);
            listViewEZLNTINFO.Sort();
            updateCOMListHashTable(); ;
        }

        private void listViewEZLNTINFO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}