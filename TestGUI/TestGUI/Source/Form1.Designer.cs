namespace ZGWUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripPortSettings = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonMessageViewClear = new System.Windows.Forms.Button();
            this.buttonClearRaw = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBoxMessageView = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxCommandResponse = new System.Windows.Forms.RichTextBox();
            this.openOtaFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTipGeneralTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabPagePollControl = new System.Windows.Forms.TabPage();
            this.textBoxPollSetShortIntervalDstEndPointID = new System.Windows.Forms.TextBox();
            this.textBoxPollSetShortIntervalSrcEndPointID = new System.Windows.Forms.TextBox();
            this.textBoxPollSetLongIntervalDstEndPointID = new System.Windows.Forms.TextBox();
            this.textBoxPollSetLongIntervalSrcEndPointID = new System.Windows.Forms.TextBox();
            this.textBoxCheckInDstEndPointID = new System.Windows.Forms.TextBox();
            this.textBoxPollCheckInSrcEndPointID = new System.Windows.Forms.TextBox();
            this.textBoxPollSetShortIntervalAddress = new System.Windows.Forms.TextBox();
            this.textBoxPollSetLongIntervalAddress = new System.Windows.Forms.TextBox();
            this.textBoxPollCheckInAddress = new System.Windows.Forms.TextBox();
            this.textBoxShortPollInterval = new System.Windows.Forms.TextBox();
            this.textBoxPollLongPollInterval = new System.Windows.Forms.TextBox();
            this.buttonPollSetShortPollInterval = new System.Windows.Forms.Button();
            this.buttonPollSetLongPollInterval = new System.Windows.Forms.Button();
            this.comboBoxFastPollEnable = new System.Windows.Forms.ComboBox();
            this.textBoxFastPollExpiryTime = new System.Windows.Forms.TextBox();
            this.buttonSetCheckinRspData = new System.Windows.Forms.Button();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.textBoxOtaFileStackVer = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileHeaderVer = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileHeaderLen = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileHeaderFCTL = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileID = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileHeaderStr = new System.Windows.Forms.TextBox();
            this.textBoxOTASetWaitForDataParamsRequestBlockDelay = new System.Windows.Forms.TextBox();
            this.textBoxOTASetWaitForDataParamsRequestTime = new System.Windows.Forms.TextBox();
            this.textBoxOTASetWaitForDataParamsCurrentTime = new System.Windows.Forms.TextBox();
            this.textBoxOTASetWaitForDataParamsSrcEP = new System.Windows.Forms.TextBox();
            this.textBoxOTASetWaitForDataParamsTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileOffset = new System.Windows.Forms.TextBox();
            this.textBoxOtaDownloadStatus = new System.Windows.Forms.TextBox();
            this.textBoxOTAImageNotifyJitter = new System.Windows.Forms.TextBox();
            this.textBoxOTAImageNotifyManuID = new System.Windows.Forms.TextBox();
            this.textBoxOTAImageNotifyImageType = new System.Windows.Forms.TextBox();
            this.textBoxOTAImageNotifyFileVersion = new System.Windows.Forms.TextBox();
            this.textBoxOTAImageNotifyDstEP = new System.Windows.Forms.TextBox();
            this.textBoxOTAImageNotifySrcEP = new System.Windows.Forms.TextBox();
            this.textBoxOTAImageNotifyTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileSize = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileVersion = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileImageType = new System.Windows.Forms.TextBox();
            this.textBoxOtaFileManuCode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonOTASetWaitForDataParams = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBarOtaDownloadProgress = new System.Windows.Forms.ProgressBar();
            this.comboBoxOTAImageNotifyType = new System.Windows.Forms.ComboBox();
            this.comboBoxOTAImageNotifyAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonOTAImageNotify = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOTALoadNewImage = new System.Windows.Forms.Button();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.textBoxZllMoveToHueHue = new System.Windows.Forms.TextBox();
            this.textBoxZllMoveToHueTransTime = new System.Windows.Forms.TextBox();
            this.textBoxZllMoveToHueDirection = new System.Windows.Forms.TextBox();
            this.textBoxZllMoveToHueDstEp = new System.Windows.Forms.TextBox();
            this.textBoxZllMoveToHueSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxZllMoveToHueAddr = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonZllMoveToHue = new System.Windows.Forms.Button();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.comboBoxZllOnOffEffectID = new System.Windows.Forms.ComboBox();
            this.textBoxZllOnOffEffectsGradient = new System.Windows.Forms.TextBox();
            this.textBoxZllOnOffEffectsDstEp = new System.Windows.Forms.TextBox();
            this.textBoxZllOnOffEffectsSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxZllOnOffEffectsAddr = new System.Windows.Forms.TextBox();
            this.buttonZllOnOffEffects = new System.Windows.Forms.Button();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.buttonZllTouchlinkFactoryReset = new System.Windows.Forms.Button();
            this.buttonZllTouchlinkInitiate = new System.Windows.Forms.Button();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.comboBoxEnrollRspCode = new System.Windows.Forms.ComboBox();
            this.textBoxEnrollRspZone = new System.Windows.Forms.TextBox();
            this.textBoxEnrollRspDstEp = new System.Windows.Forms.TextBox();
            this.textBoxEnrollRspSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxEnrollRspAddr = new System.Windows.Forms.TextBox();
            this.comboBoxEnrollRspAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonEnrollResponse = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBoxLockUnlock = new System.Windows.Forms.ComboBox();
            this.textBoxLockUnlockDstEp = new System.Windows.Forms.TextBox();
            this.textBoxLockUnlockSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxLockUnlockAddr = new System.Windows.Forms.TextBox();
            this.buttonLockUnlock = new System.Windows.Forms.Button();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.textBoxMoveToSatTime = new System.Windows.Forms.TextBox();
            this.textBoxMoveToSatSat = new System.Windows.Forms.TextBox();
            this.textBoxMoveToSatDstEp = new System.Windows.Forms.TextBox();
            this.textBoxMoveToSatSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxMoveToSatAddr = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorTempRate = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorTempTemp = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorTempDstEp = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorTempSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorTempAddr = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorTime = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorY = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorX = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorDstEp = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxMoveToColorAddr = new System.Windows.Forms.TextBox();
            this.textBoxMoveToHueTime = new System.Windows.Forms.TextBox();
            this.textBoxMoveToHueDir = new System.Windows.Forms.TextBox();
            this.textBoxMoveToHueHue = new System.Windows.Forms.TextBox();
            this.textBoxMoveToHueDstEp = new System.Windows.Forms.TextBox();
            this.textBoxMoveToHueSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxMoveToHueAddr = new System.Windows.Forms.TextBox();
            this.buttonMoveToSat = new System.Windows.Forms.Button();
            this.buttonMoveToColorTemp = new System.Windows.Forms.Button();
            this.buttonMoveToColor = new System.Windows.Forms.Button();
            this.buttonMoveToHue = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.checkBoxShowExtension = new System.Windows.Forms.CheckBox();
            this.textBoxAddSceneData = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneExtLen = new System.Windows.Forms.TextBox();
            this.textBoxRemoveSceneSceneID = new System.Windows.Forms.TextBox();
            this.textBoxRemoveSceneGroupID = new System.Windows.Forms.TextBox();
            this.textBoxRemoveSceneDstEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxRemoveSceneSrcEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxRemoveSceneAddr = new System.Windows.Forms.TextBox();
            this.textBoxRemoveAllScenesGroupID = new System.Windows.Forms.TextBox();
            this.textBoxRemoveAllScenesDstEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxRemoveAllScenesSrcEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxRemoveAllScenesAddr = new System.Windows.Forms.TextBox();
            this.textBoxGetSceneMembershipGroupID = new System.Windows.Forms.TextBox();
            this.textBoxGetSceneMembershipDstEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxGetSceneMembershipSrcEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxGetSceneMembershipAddr = new System.Windows.Forms.TextBox();
            this.textBoxRecallSceneSceneId = new System.Windows.Forms.TextBox();
            this.textBoxRecallSceneGroupId = new System.Windows.Forms.TextBox();
            this.textBoxRecallSceneDstEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxRecallSceneSrcEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxRecallSceneAddr = new System.Windows.Forms.TextBox();
            this.textBoxStoreSceneSceneId = new System.Windows.Forms.TextBox();
            this.textBoxStoreSceneGroupId = new System.Windows.Forms.TextBox();
            this.textBoxStoreSceneDstEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxStoreSceneSrcEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxStoreSceneAddr = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneMaxNameLen = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneNameLen = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneName = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneTransTime = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneSceneId = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneGroupId = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneDstEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneSrcEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxAddSceneAddr = new System.Windows.Forms.TextBox();
            this.textBoxViewSceneSceneId = new System.Windows.Forms.TextBox();
            this.textBoxViewSceneGroupId = new System.Windows.Forms.TextBox();
            this.textBoxViewSceneDstEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxViewSceneSrcEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxViewSceneAddr = new System.Windows.Forms.TextBox();
            this.comboBoxRemoveSceneAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonRemoveScene = new System.Windows.Forms.Button();
            this.comboBoxRemoveAllScenesAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonRemoveAllScenes = new System.Windows.Forms.Button();
            this.comboBoxGetSceneMembershipAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonGetSceneMembership = new System.Windows.Forms.Button();
            this.comboBoxRecallSceneAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonRecallScene = new System.Windows.Forms.Button();
            this.comboBoxStoreSceneAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonStoreScene = new System.Windows.Forms.Button();
            this.comboBoxAddSceneAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonAddScene = new System.Windows.Forms.Button();
            this.comboBoxViewSceneAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonViewScene = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.comboBoxOnOffAddrMode = new System.Windows.Forms.ComboBox();
            this.comboBoxOnOffCommand = new System.Windows.Forms.ComboBox();
            this.textBoxOnOffDstEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxOnOffSrcEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxOnOffAddr = new System.Windows.Forms.TextBox();
            this.buttonOnOff = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.comboBoxMoveToLevelOnOff = new System.Windows.Forms.ComboBox();
            this.comboBoxMoveToLevelAddrMode = new System.Windows.Forms.ComboBox();
            this.textBoxMoveToLevelTransTime = new System.Windows.Forms.TextBox();
            this.textBoxMoveToLevelLevel = new System.Windows.Forms.TextBox();
            this.textBoxMoveToLevelDstEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxMoveToLevelSrcEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxMoveToLevelAddr = new System.Windows.Forms.TextBox();
            this.buttonMoveToLevel = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.textBoxIdQueryDstEp = new System.Windows.Forms.TextBox();
            this.textBoxIdQuerySrcEp = new System.Windows.Forms.TextBox();
            this.textBoxIdQueryAddr = new System.Windows.Forms.TextBox();
            this.textBoxIdSendTime = new System.Windows.Forms.TextBox();
            this.textBoxIdSendDstEp = new System.Windows.Forms.TextBox();
            this.textBoxSendIdSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxSendIdAddr = new System.Windows.Forms.TextBox();
            this.buttonIdQuery = new System.Windows.Forms.Button();
            this.buttonIdSend = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBoxGroupName = new System.Windows.Forms.TextBox();
            this.textBoxGroupNameMaxLength = new System.Windows.Forms.TextBox();
            this.textBoxGroupNameLength = new System.Windows.Forms.TextBox();
            this.textBoxGroupAddIfIdentifyGroupID = new System.Windows.Forms.TextBox();
            this.textBoxGroupAddIfIdentifySrcEp = new System.Windows.Forms.TextBox();
            this.textBoxGroupAddIfIdentifyDstEp = new System.Windows.Forms.TextBox();
            this.textBoxGroupAddIfIndentifyingTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxRemoveAllGroupDstEp = new System.Windows.Forms.TextBox();
            this.textBoxRemoveAllGroupSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxRemoveAllGroupTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxRemoveGroupGroupAddr = new System.Windows.Forms.TextBox();
            this.textBoxRemoveGroupDstEp = new System.Windows.Forms.TextBox();
            this.textBoxRemoveGroupSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxRemoveGroupTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxGetGroupCount = new System.Windows.Forms.TextBox();
            this.textBoxGetGroupDstEp = new System.Windows.Forms.TextBox();
            this.textBoxGetGroupSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxGetGroupTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxViewGroupGroupAddr = new System.Windows.Forms.TextBox();
            this.textBoxViewGroupDstEp = new System.Windows.Forms.TextBox();
            this.textBoxViewGroupSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxViewGroupAddr = new System.Windows.Forms.TextBox();
            this.textBoxAddGroupGroupAddr = new System.Windows.Forms.TextBox();
            this.textBoxAddGroupDstEp = new System.Windows.Forms.TextBox();
            this.textBoxAddGroupSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxAddGroupAddr = new System.Windows.Forms.TextBox();
            this.buttonAddToList = new System.Windows.Forms.Button();
            this.buttonGroupAddIfIdentifying = new System.Windows.Forms.Button();
            this.buttonGroupRemoveAll = new System.Windows.Forms.Button();
            this.buttonRemoveGroup = new System.Windows.Forms.Button();
            this.buttonGetGroup = new System.Windows.Forms.Button();
            this.buttonViewGroup = new System.Windows.Forms.Button();
            this.buttonAddGroup = new System.Windows.Forms.Button();
            this.BasicClusterTab = new System.Windows.Forms.TabPage();
            this.textBoxBasicResetDstEP = new System.Windows.Forms.TextBox();
            this.textBoxBasicResetSrcEP = new System.Windows.Forms.TextBox();
            this.textBoxBasicResetTargetAddr = new System.Windows.Forms.TextBox();
            this.comboBoxBasicResetTargetAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonBasicReset = new System.Windows.Forms.Button();
            this.AHIControl = new System.Windows.Forms.TabPage();
            this.textBoxAHITxPower = new System.Windows.Forms.TextBox();
            this.textBoxIPNConfigDioTxConfInDioMask = new System.Windows.Forms.TextBox();
            this.textBoxDioSetOutputOffPinMask = new System.Windows.Forms.TextBox();
            this.textBoxDioSetOutputOnPinMask = new System.Windows.Forms.TextBox();
            this.textBoxDioSetDirectionOutputPinMask = new System.Windows.Forms.TextBox();
            this.textBoxDioSetDirectionInputPinMask = new System.Windows.Forms.TextBox();
            this.textBoxIPNConfigPollPeriod = new System.Windows.Forms.TextBox();
            this.textBoxIPNConfigDioStatusOutDioMask = new System.Windows.Forms.TextBox();
            this.textBoxIPNConfigDioRfActiveOutDioMask = new System.Windows.Forms.TextBox();
            this.buttonAHISetTxPower = new System.Windows.Forms.Button();
            this.labelUnimplemented = new System.Windows.Forms.Label();
            this.comboBoxIPNConfigTimerId = new System.Windows.Forms.ComboBox();
            this.buttonDioSetOutput = new System.Windows.Forms.Button();
            this.buttonDioSetDirection = new System.Windows.Forms.Button();
            this.comboBoxIPNConfigRegisterCallback = new System.Windows.Forms.ComboBox();
            this.comboBoxIPNConfigEnable = new System.Windows.Forms.ComboBox();
            this.buttonInPacketNotification = new System.Windows.Forms.Button();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.buttonGeneralPrintExistInstallCode = new System.Windows.Forms.Button();
            this.textBoxGeneralInstallCodeCode = new System.Windows.Forms.TextBox();
            this.textBoxGeneralInstallCodeMACaddress = new System.Windows.Forms.TextBox();
            this.textBoxOOBDataKey = new System.Windows.Forms.TextBox();
            this.textBoxOOBDataAddr = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverAttributesStartAttrId = new System.Windows.Forms.TextBox();
            this.textBoxRawDataCommandsProfileID = new System.Windows.Forms.TextBox();
            this.textBoxRawDataCommandsSecurityMode = new System.Windows.Forms.TextBox();
            this.textBoxRawDataCommandsRadius = new System.Windows.Forms.TextBox();
            this.textBoxRawDataCommandsData = new System.Windows.Forms.TextBox();
            this.textBoxRawDataCommandsClusterID = new System.Windows.Forms.TextBox();
            this.textBoxRawDataCommandsDstEP = new System.Windows.Forms.TextBox();
            this.textBoxRawDataCommandsSrcEP = new System.Windows.Forms.TextBox();
            this.textBoxRawDataCommandsTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxMgmtNwkUpdateNwkManagerAddr = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverCommandsMaxCommands = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverCommandsManuID = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverCommandsCommandID = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverCommandsClusterID = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverCommandsDstEP = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverCommandsSrcEP = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverCommandsTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxMgmtNwkUpdateScanCount = new System.Windows.Forms.TextBox();
            this.textBoxMgmtNwkUpdateScanDuration = new System.Windows.Forms.TextBox();
            this.textBoxMgmtNwkUpdateChannelMask = new System.Windows.Forms.TextBox();
            this.textBoxMgmtNwkUpdateTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxManyToOneRouteRequesRadius = new System.Windows.Forms.TextBox();
            this.textBoxReadReportConfigAttribID = new System.Windows.Forms.TextBox();
            this.textBoxReadReportConfigClusterID = new System.Windows.Forms.TextBox();
            this.textBoxReadReportConfigDstEP = new System.Windows.Forms.TextBox();
            this.textBoxReadReportConfigSrcEP = new System.Windows.Forms.TextBox();
            this.textBoxReadReportConfigTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxWriteAttribManuID = new System.Windows.Forms.TextBox();
            this.textBoxWriteAttribDataType = new System.Windows.Forms.TextBox();
            this.textBoxReadAttribManuID = new System.Windows.Forms.TextBox();
            this.textBoxWriteAttribData = new System.Windows.Forms.TextBox();
            this.textBoxWriteAttribID = new System.Windows.Forms.TextBox();
            this.textBoxWriteAttribClusterID = new System.Windows.Forms.TextBox();
            this.textBoxWriteAttribDstEP = new System.Windows.Forms.TextBox();
            this.textBoxWriteAttribSrcEP = new System.Windows.Forms.TextBox();
            this.textBoxWriteAttribTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportChange = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportTimeOut = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportMaxInterval = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverAttributesMaxIDs = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverAttributesClusterID = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverAttributesDstEp = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverAttributesSrcEp = new System.Windows.Forms.TextBox();
            this.textBoxDiscoverAttributesAddr = new System.Windows.Forms.TextBox();
            this.textBoxReadAllAttribClusterID = new System.Windows.Forms.TextBox();
            this.textBoxReadAllAttribDstEP = new System.Windows.Forms.TextBox();
            this.textBoxReadAllAttribSrcEP = new System.Windows.Forms.TextBox();
            this.textBoxReadAllAttribAddr = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportAttribType = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportMinInterval = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportAttribID = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportClusterID = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportDstEP = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportSrcEP = new System.Windows.Forms.TextBox();
            this.textBoxConfigReportTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxReadAttribCount = new System.Windows.Forms.TextBox();
            this.textBoxReadAttribID1 = new System.Windows.Forms.TextBox();
            this.textBoxReadAttribClusterID = new System.Windows.Forms.TextBox();
            this.textBoxReadAttribDstEP = new System.Windows.Forms.TextBox();
            this.textBoxReadAttribSrcEP = new System.Windows.Forms.TextBox();
            this.textBoxReadAttribTargetAddr = new System.Windows.Forms.TextBox();
            this.buttonGeneralSendInstallCode = new System.Windows.Forms.Button();
            this.buttonOOBCommissioningData = new System.Windows.Forms.Button();
            this.comboBoxRawDataCommandsAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonRawDataSend = new System.Windows.Forms.Button();
            this.comboBoxDiscoverCommandsRxGen = new System.Windows.Forms.ComboBox();
            this.comboBoxDiscoverAttributesExtended = new System.Windows.Forms.ComboBox();
            this.comboBoxDiscoverCommandsManuSpecific = new System.Windows.Forms.ComboBox();
            this.comboBoxDiscoverCommandsDirection = new System.Windows.Forms.ComboBox();
            this.comboBoxDiscoverCommandsAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonDiscoverCommands = new System.Windows.Forms.Button();
            this.comboBoxMgmtNwkUpdateAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonMgmtNwkUpdate = new System.Windows.Forms.Button();
            this.comboBoxManyToOneRouteRequestCacheRoute = new System.Windows.Forms.ComboBox();
            this.buttonManyToOneRouteRequest = new System.Windows.Forms.Button();
            this.comboBoxReadReportConfigDirection = new System.Windows.Forms.ComboBox();
            this.comboBoxReadReportConfigDirIsRx = new System.Windows.Forms.ComboBox();
            this.comboBoxReadReportConfigAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonReadReportConfig = new System.Windows.Forms.Button();
            this.comboBoxWriteAttribManuSpecific = new System.Windows.Forms.ComboBox();
            this.comboBoxReadAttribManuSpecific = new System.Windows.Forms.ComboBox();
            this.comboBoxConfigReportAddrMode = new System.Windows.Forms.ComboBox();
            this.comboBoxWriteAttribDirection = new System.Windows.Forms.ComboBox();
            this.comboBoxDiscoverAttributesDirection = new System.Windows.Forms.ComboBox();
            this.buttonDiscoverAttributes = new System.Windows.Forms.Button();
            this.comboBoxReadAllAttribDirection = new System.Windows.Forms.ComboBox();
            this.buttonReadAllAttrib = new System.Windows.Forms.Button();
            this.comboBoxConfigReportAttribDirection = new System.Windows.Forms.ComboBox();
            this.comboBoxConfigReportDirection = new System.Windows.Forms.ComboBox();
            this.buttonConfigReport = new System.Windows.Forms.Button();
            this.buttonWriteAttrib = new System.Windows.Forms.Button();
            this.comboBoxReadAttribDirection = new System.Windows.Forms.ComboBox();
            this.buttonReadAttrib = new System.Windows.Forms.Button();
            this.tabPageDevice = new System.Windows.Forms.TabPage();
            this.textBoxSECADDNETKEYSEQ = new System.Windows.Forms.TextBox();
            this.textBoxSECNETKEYSEQ = new System.Windows.Forms.TextBox();
            this.buttonSECSWITCHNETKEY = new System.Windows.Forms.Button();
            this.textBoxSECNEWNETKEY = new System.Windows.Forms.TextBox();
            this.buttonSECADDNEWNETKEY = new System.Windows.Forms.Button();
            this.buttonCopyAddr = new System.Windows.Forms.Button();
            this.buttonDiscoverDevices = new System.Windows.Forms.Button();
            this.textBoxExtAddr = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxAddressList = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboBoxNciCmd = new System.Windows.Forms.ComboBox();
            this.buttonNciCmd = new System.Windows.Forms.Button();
            this.textBoxPollInterval = new System.Windows.Forms.TextBox();
            this.textBoxBindTargetExtAddr = new System.Windows.Forms.TextBox();
            this.textBoxUserSetReqDescription = new System.Windows.Forms.TextBox();
            this.textBoxUserSetReqAddr = new System.Windows.Forms.TextBox();
            this.textBoxUserReqAddr = new System.Windows.Forms.TextBox();
            this.textBoxRestoreNwkFrameCounter = new System.Windows.Forms.TextBox();
            this.textBoxLeaveAddr = new System.Windows.Forms.TextBox();
            this.textBoxRemoveChildAddr = new System.Windows.Forms.TextBox();
            this.textBoxRemoveParentAddr = new System.Windows.Forms.TextBox();
            this.textBoxMgmtLeaveExtAddr = new System.Windows.Forms.TextBox();
            this.textBoxMgmtLeaveAddr = new System.Windows.Forms.TextBox();
            this.textBoxUnBindDestEP = new System.Windows.Forms.TextBox();
            this.textBoxUnBindDestAddr = new System.Windows.Forms.TextBox();
            this.textBoxUnBindClusterID = new System.Windows.Forms.TextBox();
            this.textBoxUnBindTargetEP = new System.Windows.Forms.TextBox();
            this.textBoxUnBindTargetExtAddr = new System.Windows.Forms.TextBox();
            this.textBoxBindDestEP = new System.Windows.Forms.TextBox();
            this.textBoxBindDestAddr = new System.Windows.Forms.TextBox();
            this.textBoxBindClusterID = new System.Windows.Forms.TextBox();
            this.textBoxBindTargetEP = new System.Windows.Forms.TextBox();
            this.textBoxLqiReqStartIndex = new System.Windows.Forms.TextBox();
            this.textBoxLqiReqTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxNwkAddrReqStartIndex = new System.Windows.Forms.TextBox();
            this.textBoxNwkAddrReqExtAddr = new System.Windows.Forms.TextBox();
            this.textBoxNwkAddrReqTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxIeeeReqStartIndex = new System.Windows.Forms.TextBox();
            this.textBoxIeeeReqAddr = new System.Windows.Forms.TextBox();
            this.textBoxIeeeReqTargetAddr = new System.Windows.Forms.TextBox();
            this.textBoxComplexReqAddr = new System.Windows.Forms.TextBox();
            this.textBoxMatchReqOutputClusters = new System.Windows.Forms.TextBox();
            this.textBoxMatchReqInputClusters = new System.Windows.Forms.TextBox();
            this.textBoxMatchReqProfileID = new System.Windows.Forms.TextBox();
            this.textBoxMatchReqNbrOutputClusters = new System.Windows.Forms.TextBox();
            this.textBoxMatchReqNbrInputClusters = new System.Windows.Forms.TextBox();
            this.textBoxMatchReqAddr = new System.Windows.Forms.TextBox();
            this.textBoxActiveEpAddr = new System.Windows.Forms.TextBox();
            this.textBoxPowerReqAddr = new System.Windows.Forms.TextBox();
            this.textBoxSimpleReqEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxSimpleReqAddr = new System.Windows.Forms.TextBox();
            this.textBoxNodeDescReq = new System.Windows.Forms.TextBox();
            this.textBoxPermitJoinInterval = new System.Windows.Forms.TextBox();
            this.textBoxPermitJoinAddr = new System.Windows.Forms.TextBox();
            this.textBoxSetSecurityKeySeqNbr = new System.Windows.Forms.TextBox();
            this.textBoxSetEPID = new System.Windows.Forms.TextBox();
            this.textBoxSetCMSK = new System.Windows.Forms.TextBox();
            this.buttonPollInterval = new System.Windows.Forms.Button();
            this.buttonNWKState = new System.Windows.Forms.Button();
            this.buttonDiscoveryOnly = new System.Windows.Forms.Button();
            this.buttonUserSetReq = new System.Windows.Forms.Button();
            this.buttonUserReq = new System.Windows.Forms.Button();
            this.comboBoxLeaveChildren = new System.Windows.Forms.ComboBox();
            this.comboBoxLeaveReJoin = new System.Windows.Forms.ComboBox();
            this.buttonLeave = new System.Windows.Forms.Button();
            this.buttonRemoveDevice = new System.Windows.Forms.Button();
            this.buttonPermitJoinState = new System.Windows.Forms.Button();
            this.buttonRestoreNwk = new System.Windows.Forms.Button();
            this.buttonRecoverNwk = new System.Windows.Forms.Button();
            this.comboBoxMgmtLeaveChildren = new System.Windows.Forms.ComboBox();
            this.comboBoxMgmtLeaveReJoin = new System.Windows.Forms.ComboBox();
            this.buttonMgmtLeave = new System.Windows.Forms.Button();
            this.comboBoxUnBindAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonUnBind = new System.Windows.Forms.Button();
            this.comboBoxBindAddrMode = new System.Windows.Forms.ComboBox();
            this.buttonMgmtLqiReq = new System.Windows.Forms.Button();
            this.buttonStartScan = new System.Windows.Forms.Button();
            this.comboBoxNwkAddrReqType = new System.Windows.Forms.ComboBox();
            this.comboBoxIeeeReqType = new System.Windows.Forms.ComboBox();
            this.buttonComplexReq = new System.Windows.Forms.Button();
            this.buttonMatchReq = new System.Windows.Forms.Button();
            this.buttonActiveEpReq = new System.Windows.Forms.Button();
            this.buttonPowerDescReq = new System.Windows.Forms.Button();
            this.buttonSimpleDescReq = new System.Windows.Forms.Button();
            this.buttonNodeDescReq = new System.Windows.Forms.Button();
            this.buttonIeeeAddrReq = new System.Windows.Forms.Button();
            this.buttonNwkAddrReq = new System.Windows.Forms.Button();
            this.comboBoxSecurityKey = new System.Windows.Forms.ComboBox();
            this.comboBoxPermitJoinTCsignificance = new System.Windows.Forms.ComboBox();
            this.buttonPermitJoin = new System.Windows.Forms.Button();
            this.comboBoxSetKeyType = new System.Windows.Forms.ComboBox();
            this.comboBoxSetKeyState = new System.Windows.Forms.ComboBox();
            this.comboBoxSetType = new System.Windows.Forms.ComboBox();
            this.buttonStartNWK = new System.Windows.Forms.Button();
            this.buttonBind = new System.Windows.Forms.Button();
            this.buttonErasePD = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonSetDeviceType = new System.Windows.Forms.Button();
            this.buttonSetSecurity = new System.Windows.Forms.Button();
            this.buttonSetCMSK = new System.Windows.Forms.Button();
            this.buttonSetEPID = new System.Windows.Forms.Button();
            this.buttonGetVersion = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.buttonEZLNTSTOPONOFFLOOP = new System.Windows.Forms.Button();
            this.buttonEZLNTONOFFLOOP = new System.Windows.Forms.Button();
            this.labelEZLNTLOOPREMAIN = new System.Windows.Forms.Label();
            this.textBoxEZLNTLOOPREMAIN = new System.Windows.Forms.TextBox();
            this.buttonSOCKETSEVERTEST = new System.Windows.Forms.Button();
            this.buttonSOCKETCLIENTTEST = new System.Windows.Forms.Button();
            this.textBoxEZLNTSOCKETSEVERIP = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTSOCKETCLIENTIP = new System.Windows.Forms.TextBox();
            this.buttonEZLNTSOCKETSEVER = new System.Windows.Forms.Button();
            this.buttonEZLNTSOCKETCLIENT = new System.Windows.Forms.Button();
            this.comboBoxEZLNTUNICAST = new System.Windows.Forms.ComboBox();
            this.labelEZLNTBROADCAST = new System.Windows.Forms.Label();
            this.labelEZLNTUNICAST = new System.Windows.Forms.Label();
            this.buttonEZLNTBROADSTOPTONGGLE = new System.Windows.Forms.Button();
            this.buttonEZLNTBROADTONGGLE = new System.Windows.Forms.Button();
            this.buttonEZLNTBROADOFF = new System.Windows.Forms.Button();
            this.buttonEZLNTBROADON = new System.Windows.Forms.Button();
            this.buttonEZLNTSAVELOCAL = new System.Windows.Forms.Button();
            this.buttonEZLNTLOADREMOTE = new System.Windows.Forms.Button();
            this.buttonWZLNTPROFRAMCMD = new System.Windows.Forms.Button();
            this.buttonEZLNTDISABLEPERMIT = new System.Windows.Forms.Button();
            this.buttonEZLNTPERMIT = new System.Windows.Forms.Button();
            this.checkBoxEZLNTGROUPALL = new System.Windows.Forms.CheckBox();
            this.textBoxEZLNTSETINTERVALMAX = new System.Windows.Forms.TextBox();
            this.buttonTEMPLOOPSTOP = new System.Windows.Forms.Button();
            this.buttonEZLNTSATLOOPSTOP = new System.Windows.Forms.Button();
            this.buttonEZLNTCOLORLOOPSTOP = new System.Windows.Forms.Button();
            this.buttonEZLNTHUESTOP = new System.Windows.Forms.Button();
            this.textBoxEZLNTSETDIR = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTSETSTEP = new System.Windows.Forms.TextBox();
            this.comboBoxEZLNTLEVELWITHONOFF = new System.Windows.Forms.ComboBox();
            this.buttonEZLNTLEVLELSTOP = new System.Windows.Forms.Button();
            this.buttonEZLNTIDENTIFYSTOP = new System.Windows.Forms.Button();
            this.textBoxEZLNTTEMPTIME = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTTEMP = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTSATTIME = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTSAT = new System.Windows.Forms.TextBox();
            this.textBoxCOLORTIME = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTCOLORY = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTCOLORX = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTHUETIME = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTHUEDIR = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTHUE = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTLEVELTIME = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTLEVEL = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTIDENTIFYTIME = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTREADRPRTATTRIBUTEID = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTREADRPRTCLUSTERID = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTCONFIGRPRTCHANGE = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTCONFIGRPRTTIMEOUT = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTCONFIGRPRTMAXINTERVAL = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTCONFIGRPRTMININTERVAL = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTCONFIGRPRTATTRIBID = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTCONFIGRPRTTYPE = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTCONFIGRPRTCLUSTERID = new System.Windows.Forms.TextBox();
            this.comboBoxEZLNTLEAVECHILDREN = new System.Windows.Forms.ComboBox();
            this.comboBoxEZLNTLEAVEREJOIN = new System.Windows.Forms.ComboBox();
            this.textBoxEZLNTWRITEATTRIBUTEDATA = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTWRITEATTRIBUTECLUSTERID = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTATTRIBUTEID = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTREADCLUSTERID = new System.Windows.Forms.TextBox();
            this.buttonEZLNTMOVETOTEMP = new System.Windows.Forms.Button();
            this.buttonEZLNTMOVETOSAT = new System.Windows.Forms.Button();
            this.buttonEZLNTMOVETOCOLOR = new System.Windows.Forms.Button();
            this.buttonEZLNTMOVETOHUE = new System.Windows.Forms.Button();
            this.buttonEZLNTMOVETOLEVLEL = new System.Windows.Forms.Button();
            this.buttonEZLNTIDENTIFY = new System.Windows.Forms.Button();
            this.buttonEZLNTRESET = new System.Windows.Forms.Button();
            this.buttonEZLNTREADRPRT = new System.Windows.Forms.Button();
            this.buttonEZLNTCONFIGRPRT = new System.Windows.Forms.Button();
            this.buttonEZLNTLEAVE = new System.Windows.Forms.Button();
            this.buttonEZLNTWRITEATTRIBUTE = new System.Windows.Forms.Button();
            this.textBoxEZLNTCOMMAND = new System.Windows.Forms.TextBox();
            this.buttonEZLNTSENDCOMMAND = new System.Windows.Forms.Button();
            this.textBoxEZLNTUNBINDCLUSTERID = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTBINDCLUSTERID = new System.Windows.Forms.TextBox();
            this.buttonEZLNTUNBIND = new System.Windows.Forms.Button();
            this.buttonEZLNTBIND = new System.Windows.Forms.Button();
            this.buttonEZLNTTONGGLESTOP = new System.Windows.Forms.Button();
            this.buttonEZLNTTONGGLE = new System.Windows.Forms.Button();
            this.buttonEZLNTOFF = new System.Windows.Forms.Button();
            this.buttonEZLNTON = new System.Windows.Forms.Button();
            this.textBoxEZLNTSETLOOP = new System.Windows.Forms.TextBox();
            this.buttonEZLNTSETTIMER = new System.Windows.Forms.Button();
            this.buttonEZLNTSTOPREAD = new System.Windows.Forms.Button();
            this.textBoxEZLNTTIMERINTERVAL = new System.Windows.Forms.TextBox();
            this.buttonEZLNTREADATTRIBUTE = new System.Windows.Forms.Button();
            this.textBoxEZLNTVIEW = new System.Windows.Forms.TextBox();
            this.listViewEZLNTGROUP = new System.Windows.Forms.ListView();
            this.nwkAddrJoined = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Loca = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonREMOVEGROUPALL = new System.Windows.Forms.Button();
            this.buttonEZLNTVIEWGROUP = new System.Windows.Forms.Button();
            this.textBoxREMOVEGROUP = new System.Windows.Forms.TextBox();
            this.textBoxEZLNTADDGROUP = new System.Windows.Forms.TextBox();
            this.buttonEZLNTREMOVEGROUP = new System.Windows.Forms.Button();
            this.buttonEZLNTADDGROUP = new System.Windows.Forms.Button();
            this.buttonREFRESHCOM = new System.Windows.Forms.Button();
            this.buttonPort = new System.Windows.Forms.Button();
            this.checkBoxEZLNTALL = new System.Windows.Forms.CheckBox();
            this.listViewEZLNTINFO = new System.Windows.Forms.ListView();
            this.COMIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NwkAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MACAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Channel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ver = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Loc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Chip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Profile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PANID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.buttonLNTDISABLEPERMIT = new System.Windows.Forms.Button();
            this.buttonLNTPERMIT = new System.Windows.Forms.Button();
            this.textBoxLNTSOCKETSEVERIP = new System.Windows.Forms.TextBox();
            this.textBoxLNTSOCKETCLIENTIP = new System.Windows.Forms.TextBox();
            this.buttonLNTSOCKETSEVER = new System.Windows.Forms.Button();
            this.buttonLNTSOCKETCLIENT = new System.Windows.Forms.Button();
            this.checkBoxLNTGROUPALL = new System.Windows.Forms.CheckBox();
            this.textBoxLNTSETPARAMAXINTERVAL = new System.Windows.Forms.TextBox();
            this.buttonLNTTEMPSTOP = new System.Windows.Forms.Button();
            this.buttonLNTSTOPCOLOR = new System.Windows.Forms.Button();
            this.buttonLNTCOLORSTOP = new System.Windows.Forms.Button();
            this.buttonHUESTOP = new System.Windows.Forms.Button();
            this.textBoxLNTSETPARADIR = new System.Windows.Forms.TextBox();
            this.textBoxLNTSETPARASTEP = new System.Windows.Forms.TextBox();
            this.comboBoxLEVELMOVEWITHONOFF = new System.Windows.Forms.ComboBox();
            this.buttonLNTSTOPLEVEL = new System.Windows.Forms.Button();
            this.buttonLNTSTOPIDENTIFY = new System.Windows.Forms.Button();
            this.textBoxLNTTEMPTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTTEMP = new System.Windows.Forms.TextBox();
            this.textBoxLNTSATTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTSAT = new System.Windows.Forms.TextBox();
            this.textBoxLNTCOLORTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTCOLORY = new System.Windows.Forms.TextBox();
            this.textBoxLNTCOLORX = new System.Windows.Forms.TextBox();
            this.textBoxLNTHUETIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTHUEDIR = new System.Windows.Forms.TextBox();
            this.textBoxLNTHUE = new System.Windows.Forms.TextBox();
            this.textBoxLNTLEVELTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTLEVEL = new System.Windows.Forms.TextBox();
            this.textBoxLNTIDENTIFYTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTREADRPRTATTRID = new System.Windows.Forms.TextBox();
            this.textBoxLNTREADRPRTCLUSTERID = new System.Windows.Forms.TextBox();
            this.textBoxLNTCONFIGRPRTCHANGE = new System.Windows.Forms.TextBox();
            this.textBoxLNTCONFIGRPRTTIMEOUT = new System.Windows.Forms.TextBox();
            this.textBoxCONFIGRPRTMAXRPRTINTERVAL = new System.Windows.Forms.TextBox();
            this.textBoxCONFIGRPRTMININTERVAL = new System.Windows.Forms.TextBox();
            this.textBoxCONFIGRPRTATTRID = new System.Windows.Forms.TextBox();
            this.textBoxLNTCONFIGRPRTTYPE = new System.Windows.Forms.TextBox();
            this.textBoxLNTCONFIGRPRTCLUSTERID = new System.Windows.Forms.TextBox();
            this.comboBoxLNTLEAVEWITHCHILDREN = new System.Windows.Forms.ComboBox();
            this.comboBoxLNTLEAVEREJOIN = new System.Windows.Forms.ComboBox();
            this.textBoxLNTWRITEATTRDATA = new System.Windows.Forms.TextBox();
            this.textBoxLNTWRITEATTRDATATYPE = new System.Windows.Forms.TextBox();
            this.textBoxLNTWRITEATTRATTRID = new System.Windows.Forms.TextBox();
            this.textBoxLNTWRITEATTRCLUSTERID = new System.Windows.Forms.TextBox();
            this.textBoxLNTREADATTRATTRIBUTECOUNT = new System.Windows.Forms.TextBox();
            this.textBoxLNTREADATTRATTRIBUTEID = new System.Windows.Forms.TextBox();
            this.textBoxLNTREADATTRCLUSTERID = new System.Windows.Forms.TextBox();
            this.buttonLNTTEMP = new System.Windows.Forms.Button();
            this.buttonLNTSAT = new System.Windows.Forms.Button();
            this.buttonLNTCOLOR = new System.Windows.Forms.Button();
            this.buttonLNTHUE = new System.Windows.Forms.Button();
            this.buttonLNTLEVEL = new System.Windows.Forms.Button();
            this.buttonLNTIDENTIFY = new System.Windows.Forms.Button();
            this.buttonLNTRESET = new System.Windows.Forms.Button();
            this.buttonLNTREADRPRT = new System.Windows.Forms.Button();
            this.buttonLNTCONFIGRPRT = new System.Windows.Forms.Button();
            this.buttonLNTLEAVE = new System.Windows.Forms.Button();
            this.buttonLNTWRITEATTRIBUTE = new System.Windows.Forms.Button();
            this.textBoxLNTUNBINDIEEEADDR = new System.Windows.Forms.TextBox();
            this.textBoxLNTBINDIEEEADDR = new System.Windows.Forms.TextBox();
            this.buttonLNTUNBIND = new System.Windows.Forms.Button();
            this.buttonLNTBIND = new System.Windows.Forms.Button();
            this.buttonLNTSTOPTONGGLE = new System.Windows.Forms.Button();
            this.buttonLNTTONGGLE = new System.Windows.Forms.Button();
            this.buttonLNTOFF = new System.Windows.Forms.Button();
            this.buttonLNTON = new System.Windows.Forms.Button();
            this.textBoxLNTSETLOOP = new System.Windows.Forms.TextBox();
            this.buttonLNTSETPARA = new System.Windows.Forms.Button();
            this.buttonLNTSTOPTEADATTRIBUTE = new System.Windows.Forms.Button();
            this.textBoxLNTSETPARAMININTERVAL = new System.Windows.Forms.TextBox();
            this.buttonLNTREADATTRIBUTE = new System.Windows.Forms.Button();
            this.textBoxLNTVIEWGROUPADDRESS = new System.Windows.Forms.TextBox();
            this.buttonLNTREMOVEALL = new System.Windows.Forms.Button();
            this.buttonLNTVIEWGROUP = new System.Windows.Forms.Button();
            this.textBoxLNTREMOVEGROUPADDRESS = new System.Windows.Forms.TextBox();
            this.textBoxLNTADDGROUPADDR = new System.Windows.Forms.TextBox();
            this.buttonLNTREMOVEGROUP = new System.Windows.Forms.Button();
            this.buttonLNTADDGROUP = new System.Windows.Forms.Button();
            this.textBoxLNTSENDCOMMAND = new System.Windows.Forms.TextBox();
            this.buttonLNTSENDCOMMAND = new System.Windows.Forms.Button();
            this.checkBoxLNTALL = new System.Windows.Forms.CheckBox();
            this.listViewLNTGROUPINFO = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewLNTCOMINFO = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonLNTREMOTELOAD = new System.Windows.Forms.Button();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.buttonLNTGWSTOPONOFFLOOP = new System.Windows.Forms.Button();
            this.buttonLNTGWONOFFLOOP = new System.Windows.Forms.Button();
            this.labelLNTGWLOOPREMAIN = new System.Windows.Forms.Label();
            this.textBoxLNTGWLOOPREMAIN = new System.Windows.Forms.TextBox();
            this.labelLNTGWUNICAST = new System.Windows.Forms.Label();
            this.labelLNTGWBROADCAST = new System.Windows.Forms.Label();
            this.buttonLNTGWBROADSTOPTONGGLE = new System.Windows.Forms.Button();
            this.buttonLNTGWBROADTONGGLE = new System.Windows.Forms.Button();
            this.buttonLNTGWBROADOFF = new System.Windows.Forms.Button();
            this.buttonLNTGWBROADON = new System.Windows.Forms.Button();
            this.comboBoxLNTGWUNICAST = new System.Windows.Forms.ComboBox();
            this.textBoxLNTGWSENDCMD = new System.Windows.Forms.TextBox();
            this.buttonLNTGWSENDCMD = new System.Windows.Forms.Button();
            this.listViewLNTGWINFO = new System.Windows.Forms.ListView();
            this.Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonLNTGWDBGPORT = new System.Windows.Forms.Button();
            this.buttonLNTFGWDISPERMIT = new System.Windows.Forms.Button();
            this.buttonLNTGWPERMMIT = new System.Windows.Forms.Button();
            this.checkBoxLNTGWALL = new System.Windows.Forms.CheckBox();
            this.textBoxLNTGWSETINTERVALMAX = new System.Windows.Forms.TextBox();
            this.buttonLNTGWSTOPMOVETEMP = new System.Windows.Forms.Button();
            this.buttonLNTGWSTOPMOVESAT = new System.Windows.Forms.Button();
            this.buttonLNTGWSTOPMOVECOLOR = new System.Windows.Forms.Button();
            this.buttonLNTGWSTOPMOVEHUE = new System.Windows.Forms.Button();
            this.textBoxLNTGWSETDIR = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWSETSTEP = new System.Windows.Forms.TextBox();
            this.comboBoxLNTGWLEVELWITHONOFF = new System.Windows.Forms.ComboBox();
            this.buttonLNTGWSTOPMOVELEVEL = new System.Windows.Forms.Button();
            this.buttonLNTGWSTOPIDENTIFY = new System.Windows.Forms.Button();
            this.textBoxLNTGWTEMPTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWTEMP = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWSATTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWSAT = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCOLORTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCOLORY = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCOLORX = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWHUETIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWHUEDIR = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWHUE = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWLEVELTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWLEVEL = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWIDENTIFYTIME = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWREADRPRTATTRIBUTEID = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWREADRPRTCLUSTERID = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCONFIGRPRTCHANGE = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCONFIGRPRTTIMEOUT = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCONFIGRPRTMAXINTERVAL = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCONFIGRPRTMININTERVAL = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCONFIGRPRTTYPE = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCONFIGRPRTATTRIBID = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWCONFIGRPRTCLUSTERID = new System.Windows.Forms.TextBox();
            this.comboBoxLNTGWLEAVECHILDREN = new System.Windows.Forms.ComboBox();
            this.comboBoxLNTGWLEAVEREJOIN = new System.Windows.Forms.ComboBox();
            this.textBoxLNTGWWRITEATTRIBUTEDATA = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEDATATYPE = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEID = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWWRITEATTRIBUTECLUSTERID = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWATTRIBUTEID = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWREADCLUSTERID = new System.Windows.Forms.TextBox();
            this.buttonLNTGWMOVETEMP = new System.Windows.Forms.Button();
            this.buttonLNTGWMOVESAT = new System.Windows.Forms.Button();
            this.buttonLNTGWMOVECOLOR = new System.Windows.Forms.Button();
            this.buttonLNTGWMOVEHUE = new System.Windows.Forms.Button();
            this.buttonLNTGWMOVELEVEL = new System.Windows.Forms.Button();
            this.buttonLNTGWIDENTIFY = new System.Windows.Forms.Button();
            this.buttonLNTGWRESET = new System.Windows.Forms.Button();
            this.buttonLNTGWREADRPRT = new System.Windows.Forms.Button();
            this.buttonLNTGWCONFIGRPRT = new System.Windows.Forms.Button();
            this.buttonLNTGWLEAVE = new System.Windows.Forms.Button();
            this.buttonLNTGWWRITE = new System.Windows.Forms.Button();
            this.textBoxLNTGWUNBINDCLUSTERID = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWBINDCLUSTERID = new System.Windows.Forms.TextBox();
            this.buttonLNTGWUNBIND = new System.Windows.Forms.Button();
            this.buttonLNTGWBIND = new System.Windows.Forms.Button();
            this.buttonLNTGWSTOPTONGGLE = new System.Windows.Forms.Button();
            this.buttonLNTGWTONGGLE = new System.Windows.Forms.Button();
            this.buttonLNTGWOFF = new System.Windows.Forms.Button();
            this.buttonLNTGWON = new System.Windows.Forms.Button();
            this.textBoxLNTGWSETLOOP = new System.Windows.Forms.TextBox();
            this.buttonLNTGWSET = new System.Windows.Forms.Button();
            this.buttonLNTGWSTOPREAD = new System.Windows.Forms.Button();
            this.textBoxLNTGWTIMERINTERVAL = new System.Windows.Forms.TextBox();
            this.buttonLNTGWREAD = new System.Windows.Forms.Button();
            this.textBoxLNTGWVIEW = new System.Windows.Forms.TextBox();
            this.buttonLNTGWREMOVEGROUPALL = new System.Windows.Forms.Button();
            this.buttonLNTGWVIEWGROUP = new System.Windows.Forms.Button();
            this.textBoxLNTGWREMOVEGROUP = new System.Windows.Forms.TextBox();
            this.textBoxLNTGWADDGROUP = new System.Windows.Forms.TextBox();
            this.buttonLNTGWREMOVEGROUP = new System.Windows.Forms.Button();
            this.buttonLNTGWADDGROUP = new System.Windows.Forms.Button();
            this.listViewLNTGWGROUPINFO = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timerSCANCOM = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerReadAttribute = new System.Windows.Forms.Timer(this.components);
            this.buttonEZLNTSOCKETGETIP = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPagePollControl.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.BasicClusterTab.SuspendLayout();
            this.AHIControl.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPageDevice.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.tabPage18.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.openPortToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(194, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // openPortToolStripMenuItem
            // 
            this.openPortToolStripMenuItem.Name = "openPortToolStripMenuItem";
            this.openPortToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.openPortToolStripMenuItem.Text = "Open Port";
            this.openPortToolStripMenuItem.Click += new System.EventHandler(this.openPortToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusPort,
            this.toolStripPortSettings});
            this.statusStrip.Location = new System.Drawing.Point(0, 888);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 15, 0);
            this.statusStrip.Size = new System.Drawing.Size(1866, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusPort
            // 
            this.toolStripStatusPort.Name = "toolStripStatusPort";
            this.toolStripStatusPort.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripPortSettings
            // 
            this.toolStripPortSettings.Name = "toolStripPortSettings";
            this.toolStripPortSettings.Size = new System.Drawing.Size(0, 17);
            // 
            // buttonMessageViewClear
            // 
            this.buttonMessageViewClear.Location = new System.Drawing.Point(277, 3);
            this.buttonMessageViewClear.Name = "buttonMessageViewClear";
            this.buttonMessageViewClear.Size = new System.Drawing.Size(80, 22);
            this.buttonMessageViewClear.TabIndex = 92;
            this.buttonMessageViewClear.Text = "Clear";
            this.buttonMessageViewClear.UseVisualStyleBackColor = true;
            this.buttonMessageViewClear.Click += new System.EventHandler(this.buttonMessageViewClear_Click);
            // 
            // buttonClearRaw
            // 
            this.buttonClearRaw.Location = new System.Drawing.Point(81, 6);
            this.buttonClearRaw.Name = "buttonClearRaw";
            this.buttonClearRaw.Size = new System.Drawing.Size(80, 22);
            this.buttonClearRaw.TabIndex = 91;
            this.buttonClearRaw.Text = "Clear";
            this.buttonClearRaw.UseVisualStyleBackColor = true;
            this.buttonClearRaw.Click += new System.EventHandler(this.buttonClearRaw_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Received Message View";
            // 
            // richTextBoxMessageView
            // 
            this.richTextBoxMessageView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxMessageView.Location = new System.Drawing.Point(11, 28);
            this.richTextBoxMessageView.Name = "richTextBoxMessageView";
            this.richTextBoxMessageView.Size = new System.Drawing.Size(770, 228);
            this.richTextBoxMessageView.TabIndex = 17;
            this.richTextBoxMessageView.Text = "";
            this.richTextBoxMessageView.TextChanged += new System.EventHandler(this.richTextBoxMessageView_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Raw Data";
            // 
            // richTextBoxCommandResponse
            // 
            this.richTextBoxCommandResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxCommandResponse.Location = new System.Drawing.Point(10, 26);
            this.richTextBoxCommandResponse.Name = "richTextBoxCommandResponse";
            this.richTextBoxCommandResponse.Size = new System.Drawing.Size(1030, 230);
            this.richTextBoxCommandResponse.TabIndex = 4;
            this.richTextBoxCommandResponse.Text = "";
            this.richTextBoxCommandResponse.TextChanged += new System.EventHandler(this.richTextBoxCommandResponse_TextChanged);
            // 
            // openOtaFileDialog
            // 
            this.openOtaFileDialog.FileName = "openFileDialog1";
            this.openOtaFileDialog.Filter = "OTA|*.ota";
            this.openOtaFileDialog.Title = "Select an OTA Image";
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(133, 6);
            this.checkBoxDebug.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(139, 17);
            this.checkBoxDebug.TabIndex = 93;
            this.checkBoxDebug.Text = "View Additional Debug?";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(13, 642);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.richTextBoxCommandResponse);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.buttonClearRaw);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxDebug);
            this.splitContainer1.Panel2.Controls.Add(this.buttonMessageViewClear);
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxMessageView);
            this.splitContainer1.Size = new System.Drawing.Size(1840, 447);
            this.splitContainer1.SplitterDistance = 1052;
            this.splitContainer1.TabIndex = 94;
            // 
            // tabPagePollControl
            // 
            this.tabPagePollControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPagePollControl.Controls.Add(this.textBoxPollSetShortIntervalDstEndPointID);
            this.tabPagePollControl.Controls.Add(this.textBoxPollSetShortIntervalSrcEndPointID);
            this.tabPagePollControl.Controls.Add(this.textBoxPollSetLongIntervalDstEndPointID);
            this.tabPagePollControl.Controls.Add(this.textBoxPollSetLongIntervalSrcEndPointID);
            this.tabPagePollControl.Controls.Add(this.textBoxCheckInDstEndPointID);
            this.tabPagePollControl.Controls.Add(this.textBoxPollCheckInSrcEndPointID);
            this.tabPagePollControl.Controls.Add(this.textBoxPollSetShortIntervalAddress);
            this.tabPagePollControl.Controls.Add(this.textBoxPollSetLongIntervalAddress);
            this.tabPagePollControl.Controls.Add(this.textBoxPollCheckInAddress);
            this.tabPagePollControl.Controls.Add(this.textBoxShortPollInterval);
            this.tabPagePollControl.Controls.Add(this.textBoxPollLongPollInterval);
            this.tabPagePollControl.Controls.Add(this.buttonPollSetShortPollInterval);
            this.tabPagePollControl.Controls.Add(this.buttonPollSetLongPollInterval);
            this.tabPagePollControl.Controls.Add(this.comboBoxFastPollEnable);
            this.tabPagePollControl.Controls.Add(this.textBoxFastPollExpiryTime);
            this.tabPagePollControl.Controls.Add(this.buttonSetCheckinRspData);
            this.tabPagePollControl.Location = new System.Drawing.Point(4, 22);
            this.tabPagePollControl.Name = "tabPagePollControl";
            this.tabPagePollControl.Size = new System.Drawing.Size(1833, 584);
            this.tabPagePollControl.TabIndex = 17;
            this.tabPagePollControl.Text = "Poll Control";
            this.tabPagePollControl.Click += new System.EventHandler(this.tabPagePollControl_Click);
            // 
            // textBoxPollSetShortIntervalDstEndPointID
            // 
            this.textBoxPollSetShortIntervalDstEndPointID.Location = new System.Drawing.Point(307, 68);
            this.textBoxPollSetShortIntervalDstEndPointID.Name = "textBoxPollSetShortIntervalDstEndPointID";
            this.textBoxPollSetShortIntervalDstEndPointID.Size = new System.Drawing.Size(66, 20);
            this.textBoxPollSetShortIntervalDstEndPointID.TabIndex = 15;
            // 
            // textBoxPollSetShortIntervalSrcEndPointID
            // 
            this.textBoxPollSetShortIntervalSrcEndPointID.Location = new System.Drawing.Point(241, 68);
            this.textBoxPollSetShortIntervalSrcEndPointID.Name = "textBoxPollSetShortIntervalSrcEndPointID";
            this.textBoxPollSetShortIntervalSrcEndPointID.Size = new System.Drawing.Size(59, 20);
            this.textBoxPollSetShortIntervalSrcEndPointID.TabIndex = 14;
            // 
            // textBoxPollSetLongIntervalDstEndPointID
            // 
            this.textBoxPollSetLongIntervalDstEndPointID.Location = new System.Drawing.Point(307, 37);
            this.textBoxPollSetLongIntervalDstEndPointID.Name = "textBoxPollSetLongIntervalDstEndPointID";
            this.textBoxPollSetLongIntervalDstEndPointID.Size = new System.Drawing.Size(66, 20);
            this.textBoxPollSetLongIntervalDstEndPointID.TabIndex = 13;
            // 
            // textBoxPollSetLongIntervalSrcEndPointID
            // 
            this.textBoxPollSetLongIntervalSrcEndPointID.Location = new System.Drawing.Point(242, 37);
            this.textBoxPollSetLongIntervalSrcEndPointID.Name = "textBoxPollSetLongIntervalSrcEndPointID";
            this.textBoxPollSetLongIntervalSrcEndPointID.Size = new System.Drawing.Size(59, 20);
            this.textBoxPollSetLongIntervalSrcEndPointID.TabIndex = 12;
            // 
            // textBoxCheckInDstEndPointID
            // 
            this.textBoxCheckInDstEndPointID.Location = new System.Drawing.Point(392, 6);
            this.textBoxCheckInDstEndPointID.Name = "textBoxCheckInDstEndPointID";
            this.textBoxCheckInDstEndPointID.Size = new System.Drawing.Size(58, 20);
            this.textBoxCheckInDstEndPointID.TabIndex = 11;
            // 
            // textBoxPollCheckInSrcEndPointID
            // 
            this.textBoxPollCheckInSrcEndPointID.Location = new System.Drawing.Point(321, 6);
            this.textBoxPollCheckInSrcEndPointID.Name = "textBoxPollCheckInSrcEndPointID";
            this.textBoxPollCheckInSrcEndPointID.Size = new System.Drawing.Size(65, 20);
            this.textBoxPollCheckInSrcEndPointID.TabIndex = 10;
            // 
            // textBoxPollSetShortIntervalAddress
            // 
            this.textBoxPollSetShortIntervalAddress.Location = new System.Drawing.Point(135, 67);
            this.textBoxPollSetShortIntervalAddress.Name = "textBoxPollSetShortIntervalAddress";
            this.textBoxPollSetShortIntervalAddress.Size = new System.Drawing.Size(100, 20);
            this.textBoxPollSetShortIntervalAddress.TabIndex = 9;
            // 
            // textBoxPollSetLongIntervalAddress
            // 
            this.textBoxPollSetLongIntervalAddress.Location = new System.Drawing.Point(135, 38);
            this.textBoxPollSetLongIntervalAddress.Name = "textBoxPollSetLongIntervalAddress";
            this.textBoxPollSetLongIntervalAddress.Size = new System.Drawing.Size(100, 20);
            this.textBoxPollSetLongIntervalAddress.TabIndex = 8;
            // 
            // textBoxPollCheckInAddress
            // 
            this.textBoxPollCheckInAddress.Location = new System.Drawing.Point(214, 7);
            this.textBoxPollCheckInAddress.Name = "textBoxPollCheckInAddress";
            this.textBoxPollCheckInAddress.Size = new System.Drawing.Size(100, 20);
            this.textBoxPollCheckInAddress.TabIndex = 7;
            // 
            // textBoxShortPollInterval
            // 
            this.textBoxShortPollInterval.Location = new System.Drawing.Point(379, 68);
            this.textBoxShortPollInterval.Name = "textBoxShortPollInterval";
            this.textBoxShortPollInterval.Size = new System.Drawing.Size(100, 20);
            this.textBoxShortPollInterval.TabIndex = 6;
            // 
            // textBoxPollLongPollInterval
            // 
            this.textBoxPollLongPollInterval.Location = new System.Drawing.Point(379, 36);
            this.textBoxPollLongPollInterval.Name = "textBoxPollLongPollInterval";
            this.textBoxPollLongPollInterval.Size = new System.Drawing.Size(100, 20);
            this.textBoxPollLongPollInterval.TabIndex = 5;
            // 
            // buttonPollSetShortPollInterval
            // 
            this.buttonPollSetShortPollInterval.Location = new System.Drawing.Point(4, 65);
            this.buttonPollSetShortPollInterval.Name = "buttonPollSetShortPollInterval";
            this.buttonPollSetShortPollInterval.Size = new System.Drawing.Size(124, 23);
            this.buttonPollSetShortPollInterval.TabIndex = 4;
            this.buttonPollSetShortPollInterval.Text = "Set Short Poll Interval";
            this.buttonPollSetShortPollInterval.UseVisualStyleBackColor = true;
            this.buttonPollSetShortPollInterval.Click += new System.EventHandler(this.buttonPollSetShortPollInterval_Click);
            // 
            // buttonPollSetLongPollInterval
            // 
            this.buttonPollSetLongPollInterval.Location = new System.Drawing.Point(4, 36);
            this.buttonPollSetLongPollInterval.Name = "buttonPollSetLongPollInterval";
            this.buttonPollSetLongPollInterval.Size = new System.Drawing.Size(124, 23);
            this.buttonPollSetLongPollInterval.TabIndex = 3;
            this.buttonPollSetLongPollInterval.Text = "Set Long Poll Interval";
            this.buttonPollSetLongPollInterval.UseVisualStyleBackColor = true;
            this.buttonPollSetLongPollInterval.Click += new System.EventHandler(this.buttonPollSetLongPollInterval_Click);
            // 
            // comboBoxFastPollEnable
            // 
            this.comboBoxFastPollEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFastPollEnable.FormattingEnabled = true;
            this.comboBoxFastPollEnable.Items.AddRange(new object[] {
            "DISABLED",
            "ENABLED"});
            this.comboBoxFastPollEnable.Location = new System.Drawing.Point(102, 6);
            this.comboBoxFastPollEnable.Name = "comboBoxFastPollEnable";
            this.comboBoxFastPollEnable.Size = new System.Drawing.Size(106, 21);
            this.comboBoxFastPollEnable.TabIndex = 1;
            this.comboBoxFastPollEnable.SelectedIndexChanged += new System.EventHandler(this.comboBoxFastPollEnable_SelectedIndexChanged);
            this.comboBoxFastPollEnable.MouseLeave += new System.EventHandler(this.comboBoxFastPollEnable_MouseLeave);
            this.comboBoxFastPollEnable.MouseHover += new System.EventHandler(this.comboBoxFastPollEnable_MouseHover);
            // 
            // textBoxFastPollExpiryTime
            // 
            this.textBoxFastPollExpiryTime.Location = new System.Drawing.Point(456, 7);
            this.textBoxFastPollExpiryTime.Name = "textBoxFastPollExpiryTime";
            this.textBoxFastPollExpiryTime.Size = new System.Drawing.Size(154, 20);
            this.textBoxFastPollExpiryTime.TabIndex = 2;
            this.textBoxFastPollExpiryTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxFastPollExpiryTime_MouseClick);
            this.textBoxFastPollExpiryTime.TextChanged += new System.EventHandler(this.textBoxFastPollExpiryTime_TextChanged);
            this.textBoxFastPollExpiryTime.Leave += new System.EventHandler(this.textBoxFastPollExpiryTime_Leave);
            this.textBoxFastPollExpiryTime.MouseLeave += new System.EventHandler(this.textBoxFastPollExpiryTime_MouseLeave);
            this.textBoxFastPollExpiryTime.MouseHover += new System.EventHandler(this.textBoxFastPollExpiryTime_MouseHover);
            // 
            // buttonSetCheckinRspData
            // 
            this.buttonSetCheckinRspData.Location = new System.Drawing.Point(3, 3);
            this.buttonSetCheckinRspData.Name = "buttonSetCheckinRspData";
            this.buttonSetCheckinRspData.Size = new System.Drawing.Size(92, 26);
            this.buttonSetCheckinRspData.TabIndex = 0;
            this.buttonSetCheckinRspData.Text = "Check-In Rsp";
            this.buttonSetCheckinRspData.UseVisualStyleBackColor = true;
            this.buttonSetCheckinRspData.Click += new System.EventHandler(this.buttonSetCheckinRspData_Click);
            // 
            // tabPage14
            // 
            this.tabPage14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage14.Controls.Add(this.textBoxOtaFileStackVer);
            this.tabPage14.Controls.Add(this.textBoxOtaFileHeaderVer);
            this.tabPage14.Controls.Add(this.textBoxOtaFileHeaderLen);
            this.tabPage14.Controls.Add(this.textBoxOtaFileHeaderFCTL);
            this.tabPage14.Controls.Add(this.textBoxOtaFileID);
            this.tabPage14.Controls.Add(this.textBoxOtaFileHeaderStr);
            this.tabPage14.Controls.Add(this.textBoxOTASetWaitForDataParamsRequestBlockDelay);
            this.tabPage14.Controls.Add(this.textBoxOTASetWaitForDataParamsRequestTime);
            this.tabPage14.Controls.Add(this.textBoxOTASetWaitForDataParamsCurrentTime);
            this.tabPage14.Controls.Add(this.textBoxOTASetWaitForDataParamsSrcEP);
            this.tabPage14.Controls.Add(this.textBoxOTASetWaitForDataParamsTargetAddr);
            this.tabPage14.Controls.Add(this.textBoxOtaFileOffset);
            this.tabPage14.Controls.Add(this.textBoxOtaDownloadStatus);
            this.tabPage14.Controls.Add(this.textBoxOTAImageNotifyJitter);
            this.tabPage14.Controls.Add(this.textBoxOTAImageNotifyManuID);
            this.tabPage14.Controls.Add(this.textBoxOTAImageNotifyImageType);
            this.tabPage14.Controls.Add(this.textBoxOTAImageNotifyFileVersion);
            this.tabPage14.Controls.Add(this.textBoxOTAImageNotifyDstEP);
            this.tabPage14.Controls.Add(this.textBoxOTAImageNotifySrcEP);
            this.tabPage14.Controls.Add(this.textBoxOTAImageNotifyTargetAddr);
            this.tabPage14.Controls.Add(this.textBoxOtaFileSize);
            this.tabPage14.Controls.Add(this.textBoxOtaFileVersion);
            this.tabPage14.Controls.Add(this.textBoxOtaFileImageType);
            this.tabPage14.Controls.Add(this.textBoxOtaFileManuCode);
            this.tabPage14.Controls.Add(this.label15);
            this.tabPage14.Controls.Add(this.label14);
            this.tabPage14.Controls.Add(this.label13);
            this.tabPage14.Controls.Add(this.label12);
            this.tabPage14.Controls.Add(this.label11);
            this.tabPage14.Controls.Add(this.label10);
            this.tabPage14.Controls.Add(this.buttonOTASetWaitForDataParams);
            this.tabPage14.Controls.Add(this.label9);
            this.tabPage14.Controls.Add(this.label8);
            this.tabPage14.Controls.Add(this.label7);
            this.tabPage14.Controls.Add(this.progressBarOtaDownloadProgress);
            this.tabPage14.Controls.Add(this.comboBoxOTAImageNotifyType);
            this.tabPage14.Controls.Add(this.comboBoxOTAImageNotifyAddrMode);
            this.tabPage14.Controls.Add(this.buttonOTAImageNotify);
            this.tabPage14.Controls.Add(this.label6);
            this.tabPage14.Controls.Add(this.label5);
            this.tabPage14.Controls.Add(this.label2);
            this.tabPage14.Controls.Add(this.label1);
            this.tabPage14.Controls.Add(this.buttonOTALoadNewImage);
            this.tabPage14.Location = new System.Drawing.Point(4, 22);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Size = new System.Drawing.Size(1833, 584);
            this.tabPage14.TabIndex = 14;
            this.tabPage14.Text = "OTA Cluster";
            this.tabPage14.Click += new System.EventHandler(this.tabPage14_Click);
            // 
            // textBoxOtaFileStackVer
            // 
            this.textBoxOtaFileStackVer.Location = new System.Drawing.Point(156, 33);
            this.textBoxOtaFileStackVer.Name = "textBoxOtaFileStackVer";
            this.textBoxOtaFileStackVer.ReadOnly = true;
            this.textBoxOtaFileStackVer.Size = new System.Drawing.Size(91, 20);
            this.textBoxOtaFileStackVer.TabIndex = 8;
            // 
            // textBoxOtaFileHeaderVer
            // 
            this.textBoxOtaFileHeaderVer.Location = new System.Drawing.Point(290, 7);
            this.textBoxOtaFileHeaderVer.Name = "textBoxOtaFileHeaderVer";
            this.textBoxOtaFileHeaderVer.ReadOnly = true;
            this.textBoxOtaFileHeaderVer.Size = new System.Drawing.Size(74, 20);
            this.textBoxOtaFileHeaderVer.TabIndex = 2;
            // 
            // textBoxOtaFileHeaderLen
            // 
            this.textBoxOtaFileHeaderLen.Location = new System.Drawing.Point(442, 7);
            this.textBoxOtaFileHeaderLen.Name = "textBoxOtaFileHeaderLen";
            this.textBoxOtaFileHeaderLen.ReadOnly = true;
            this.textBoxOtaFileHeaderLen.Size = new System.Drawing.Size(74, 20);
            this.textBoxOtaFileHeaderLen.TabIndex = 3;
            // 
            // textBoxOtaFileHeaderFCTL
            // 
            this.textBoxOtaFileHeaderFCTL.Location = new System.Drawing.Point(604, 7);
            this.textBoxOtaFileHeaderFCTL.Name = "textBoxOtaFileHeaderFCTL";
            this.textBoxOtaFileHeaderFCTL.ReadOnly = true;
            this.textBoxOtaFileHeaderFCTL.Size = new System.Drawing.Size(74, 20);
            this.textBoxOtaFileHeaderFCTL.TabIndex = 4;
            // 
            // textBoxOtaFileID
            // 
            this.textBoxOtaFileID.Location = new System.Drawing.Point(138, 7);
            this.textBoxOtaFileID.Name = "textBoxOtaFileID";
            this.textBoxOtaFileID.ReadOnly = true;
            this.textBoxOtaFileID.Size = new System.Drawing.Size(74, 20);
            this.textBoxOtaFileID.TabIndex = 1;
            // 
            // textBoxOtaFileHeaderStr
            // 
            this.textBoxOtaFileHeaderStr.Location = new System.Drawing.Point(438, 33);
            this.textBoxOtaFileHeaderStr.Name = "textBoxOtaFileHeaderStr";
            this.textBoxOtaFileHeaderStr.ReadOnly = true;
            this.textBoxOtaFileHeaderStr.Size = new System.Drawing.Size(222, 20);
            this.textBoxOtaFileHeaderStr.TabIndex = 10;
            // 
            // textBoxOTASetWaitForDataParamsRequestBlockDelay
            // 
            this.textBoxOTASetWaitForDataParamsRequestBlockDelay.Location = new System.Drawing.Point(579, 88);
            this.textBoxOTASetWaitForDataParamsRequestBlockDelay.Name = "textBoxOTASetWaitForDataParamsRequestBlockDelay";
            this.textBoxOTASetWaitForDataParamsRequestBlockDelay.Size = new System.Drawing.Size(122, 20);
            this.textBoxOTASetWaitForDataParamsRequestBlockDelay.TabIndex = 26;
            this.textBoxOTASetWaitForDataParamsRequestBlockDelay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTASetWaitForDataParamsRequestBlockDelay_MouseClick);
            this.textBoxOTASetWaitForDataParamsRequestBlockDelay.Leave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsRequestBlockDelay_Leave);
            this.textBoxOTASetWaitForDataParamsRequestBlockDelay.MouseLeave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsRequestBlockDelay_MouseLeave);
            this.textBoxOTASetWaitForDataParamsRequestBlockDelay.MouseHover += new System.EventHandler(this.textBoxOTASetWaitForDataParamsRequestBlockDelay_MouseHover);
            // 
            // textBoxOTASetWaitForDataParamsRequestTime
            // 
            this.textBoxOTASetWaitForDataParamsRequestTime.Location = new System.Drawing.Point(450, 88);
            this.textBoxOTASetWaitForDataParamsRequestTime.Name = "textBoxOTASetWaitForDataParamsRequestTime";
            this.textBoxOTASetWaitForDataParamsRequestTime.Size = new System.Drawing.Size(122, 20);
            this.textBoxOTASetWaitForDataParamsRequestTime.TabIndex = 25;
            this.textBoxOTASetWaitForDataParamsRequestTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTASetWaitForDataParamsRequestTime_MouseClick);
            this.textBoxOTASetWaitForDataParamsRequestTime.Leave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsRequestTime_Leave);
            this.textBoxOTASetWaitForDataParamsRequestTime.MouseLeave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsRequestTime_MouseLeave);
            this.textBoxOTASetWaitForDataParamsRequestTime.MouseHover += new System.EventHandler(this.textBoxOTASetWaitForDataParamsRequestTime_MouseHover);
            // 
            // textBoxOTASetWaitForDataParamsCurrentTime
            // 
            this.textBoxOTASetWaitForDataParamsCurrentTime.Location = new System.Drawing.Point(321, 88);
            this.textBoxOTASetWaitForDataParamsCurrentTime.Name = "textBoxOTASetWaitForDataParamsCurrentTime";
            this.textBoxOTASetWaitForDataParamsCurrentTime.Size = new System.Drawing.Size(122, 20);
            this.textBoxOTASetWaitForDataParamsCurrentTime.TabIndex = 24;
            this.textBoxOTASetWaitForDataParamsCurrentTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTASetWaitForDataParamsCurrentTime_MouseClick);
            this.textBoxOTASetWaitForDataParamsCurrentTime.Leave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsCurrentTime_Leave);
            this.textBoxOTASetWaitForDataParamsCurrentTime.MouseLeave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsCurrentTime_MouseLeave);
            this.textBoxOTASetWaitForDataParamsCurrentTime.MouseHover += new System.EventHandler(this.textBoxOTASetWaitForDataParamsCurrentTime_MouseHover);
            // 
            // textBoxOTASetWaitForDataParamsSrcEP
            // 
            this.textBoxOTASetWaitForDataParamsSrcEP.Location = new System.Drawing.Point(209, 88);
            this.textBoxOTASetWaitForDataParamsSrcEP.Name = "textBoxOTASetWaitForDataParamsSrcEP";
            this.textBoxOTASetWaitForDataParamsSrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxOTASetWaitForDataParamsSrcEP.TabIndex = 23;
            this.textBoxOTASetWaitForDataParamsSrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTASetWaitForDataParamsSrcEP_MouseClick);
            this.textBoxOTASetWaitForDataParamsSrcEP.Leave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsSrcEP_Leave);
            this.textBoxOTASetWaitForDataParamsSrcEP.MouseLeave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsSrcEP_MouseLeave);
            this.textBoxOTASetWaitForDataParamsSrcEP.MouseHover += new System.EventHandler(this.textBoxOTASetWaitForDataParamsSrcEP_MouseHover);
            // 
            // textBoxOTASetWaitForDataParamsTargetAddr
            // 
            this.textBoxOTASetWaitForDataParamsTargetAddr.Location = new System.Drawing.Point(97, 88);
            this.textBoxOTASetWaitForDataParamsTargetAddr.Name = "textBoxOTASetWaitForDataParamsTargetAddr";
            this.textBoxOTASetWaitForDataParamsTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxOTASetWaitForDataParamsTargetAddr.TabIndex = 22;
            this.textBoxOTASetWaitForDataParamsTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTASetWaitForDataParamsTargetAddr_MouseClick);
            this.textBoxOTASetWaitForDataParamsTargetAddr.Leave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsTargetAddr_Leave);
            this.textBoxOTASetWaitForDataParamsTargetAddr.MouseLeave += new System.EventHandler(this.textBoxOTASetWaitForDataParamsTargetAddr_MouseLeave);
            this.textBoxOTASetWaitForDataParamsTargetAddr.MouseHover += new System.EventHandler(this.textBoxOTASetWaitForDataParamsTargetAddr_MouseHover);
            // 
            // textBoxOtaFileOffset
            // 
            this.textBoxOtaFileOffset.Location = new System.Drawing.Point(759, 116);
            this.textBoxOtaFileOffset.Name = "textBoxOtaFileOffset";
            this.textBoxOtaFileOffset.ReadOnly = true;
            this.textBoxOtaFileOffset.Size = new System.Drawing.Size(103, 20);
            this.textBoxOtaFileOffset.TabIndex = 28;
            // 
            // textBoxOtaDownloadStatus
            // 
            this.textBoxOtaDownloadStatus.Location = new System.Drawing.Point(97, 116);
            this.textBoxOtaDownloadStatus.Name = "textBoxOtaDownloadStatus";
            this.textBoxOtaDownloadStatus.ReadOnly = true;
            this.textBoxOtaDownloadStatus.Size = new System.Drawing.Size(106, 20);
            this.textBoxOtaDownloadStatus.TabIndex = 27;
            // 
            // textBoxOTAImageNotifyJitter
            // 
            this.textBoxOTAImageNotifyJitter.Location = new System.Drawing.Point(1060, 56);
            this.textBoxOTAImageNotifyJitter.Name = "textBoxOTAImageNotifyJitter";
            this.textBoxOTAImageNotifyJitter.Size = new System.Drawing.Size(117, 20);
            this.textBoxOTAImageNotifyJitter.TabIndex = 20;
            this.textBoxOTAImageNotifyJitter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTAImageNotifyJitter_MouseClick);
            this.textBoxOTAImageNotifyJitter.Leave += new System.EventHandler(this.textBoxOTAImageNotifyJitter_Leave);
            this.textBoxOTAImageNotifyJitter.MouseLeave += new System.EventHandler(this.textBoxOTAImageNotifyJitter_MouseLeave);
            this.textBoxOTAImageNotifyJitter.MouseHover += new System.EventHandler(this.textBoxOTAImageNotifyJitter_MouseHover);
            // 
            // textBoxOTAImageNotifyManuID
            // 
            this.textBoxOTAImageNotifyManuID.Location = new System.Drawing.Point(943, 56);
            this.textBoxOTAImageNotifyManuID.Name = "textBoxOTAImageNotifyManuID";
            this.textBoxOTAImageNotifyManuID.Size = new System.Drawing.Size(112, 20);
            this.textBoxOTAImageNotifyManuID.TabIndex = 19;
            this.textBoxOTAImageNotifyManuID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTAImageNotifyManuID_MouseClick);
            this.textBoxOTAImageNotifyManuID.Leave += new System.EventHandler(this.textBoxOTAImageNotifyManuID_Leave);
            this.textBoxOTAImageNotifyManuID.MouseLeave += new System.EventHandler(this.textBoxOTAImageNotifyManuID_MouseLeave);
            this.textBoxOTAImageNotifyManuID.MouseHover += new System.EventHandler(this.textBoxOTAImageNotifyManuID_MouseHover);
            // 
            // textBoxOTAImageNotifyImageType
            // 
            this.textBoxOTAImageNotifyImageType.Location = new System.Drawing.Point(809, 56);
            this.textBoxOTAImageNotifyImageType.Name = "textBoxOTAImageNotifyImageType";
            this.textBoxOTAImageNotifyImageType.Size = new System.Drawing.Size(128, 20);
            this.textBoxOTAImageNotifyImageType.TabIndex = 18;
            this.textBoxOTAImageNotifyImageType.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTAImageNotifyImageType_MouseClick);
            this.textBoxOTAImageNotifyImageType.Leave += new System.EventHandler(this.textBoxOTAImageNotifyImageType_Leave);
            this.textBoxOTAImageNotifyImageType.MouseLeave += new System.EventHandler(this.textBoxOTAImageNotifyImageType_MouseLeave);
            this.textBoxOTAImageNotifyImageType.MouseHover += new System.EventHandler(this.textBoxOTAImageNotifyImageType_MouseHover);
            // 
            // textBoxOTAImageNotifyFileVersion
            // 
            this.textBoxOTAImageNotifyFileVersion.Location = new System.Drawing.Point(695, 56);
            this.textBoxOTAImageNotifyFileVersion.Name = "textBoxOTAImageNotifyFileVersion";
            this.textBoxOTAImageNotifyFileVersion.Size = new System.Drawing.Size(106, 20);
            this.textBoxOTAImageNotifyFileVersion.TabIndex = 17;
            this.textBoxOTAImageNotifyFileVersion.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTAImageNotifyFileVersion_MouseClick);
            this.textBoxOTAImageNotifyFileVersion.Leave += new System.EventHandler(this.textBoxOTAImageNotifyFileVersion_Leave);
            this.textBoxOTAImageNotifyFileVersion.MouseLeave += new System.EventHandler(this.textBoxOTAImageNotifyFileVersion_MouseLeave);
            this.textBoxOTAImageNotifyFileVersion.MouseHover += new System.EventHandler(this.textBoxOTAImageNotifyFileVersion_MouseHover);
            // 
            // textBoxOTAImageNotifyDstEP
            // 
            this.textBoxOTAImageNotifyDstEP.Location = new System.Drawing.Point(438, 57);
            this.textBoxOTAImageNotifyDstEP.Name = "textBoxOTAImageNotifyDstEP";
            this.textBoxOTAImageNotifyDstEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxOTAImageNotifyDstEP.TabIndex = 15;
            this.textBoxOTAImageNotifyDstEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTAImageNotifyDstEP_MouseClick);
            this.textBoxOTAImageNotifyDstEP.Leave += new System.EventHandler(this.textBoxOTAImageNotifyDstEP_Leave);
            this.textBoxOTAImageNotifyDstEP.MouseLeave += new System.EventHandler(this.textBoxOTAImageNotifyDstEP_MouseLeave);
            this.textBoxOTAImageNotifyDstEP.MouseHover += new System.EventHandler(this.textBoxOTAImageNotifyDstEP_MouseHover);
            // 
            // textBoxOTAImageNotifySrcEP
            // 
            this.textBoxOTAImageNotifySrcEP.Location = new System.Drawing.Point(323, 57);
            this.textBoxOTAImageNotifySrcEP.Name = "textBoxOTAImageNotifySrcEP";
            this.textBoxOTAImageNotifySrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxOTAImageNotifySrcEP.TabIndex = 14;
            this.textBoxOTAImageNotifySrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTAImageNotifySrcEP_MouseClick);
            this.textBoxOTAImageNotifySrcEP.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBoxOTAImageNotifySrcEP.Leave += new System.EventHandler(this.textBoxOTAImageNotifySrcEP_Leave);
            this.textBoxOTAImageNotifySrcEP.MouseLeave += new System.EventHandler(this.textBoxOTAImageNotifySrcEP_MouseLeave);
            this.textBoxOTAImageNotifySrcEP.MouseHover += new System.EventHandler(this.textBoxOTAImageNotifySrcEP_MouseHover);
            // 
            // textBoxOTAImageNotifyTargetAddr
            // 
            this.textBoxOTAImageNotifyTargetAddr.Location = new System.Drawing.Point(209, 57);
            this.textBoxOTAImageNotifyTargetAddr.Name = "textBoxOTAImageNotifyTargetAddr";
            this.textBoxOTAImageNotifyTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxOTAImageNotifyTargetAddr.TabIndex = 13;
            this.textBoxOTAImageNotifyTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOTAImageNotifyTargetAddr_MouseClick);
            this.textBoxOTAImageNotifyTargetAddr.Leave += new System.EventHandler(this.textBoxOTAImageNotifyTargetAddr_Leave);
            this.textBoxOTAImageNotifyTargetAddr.MouseLeave += new System.EventHandler(this.textBoxOTAImageNotifyTargetAddr_MouseLeave);
            this.textBoxOTAImageNotifyTargetAddr.MouseHover += new System.EventHandler(this.textBoxOTAImageNotifyTargetAddr_MouseHover);
            // 
            // textBoxOtaFileSize
            // 
            this.textBoxOtaFileSize.Location = new System.Drawing.Point(290, 33);
            this.textBoxOtaFileSize.Name = "textBoxOtaFileSize";
            this.textBoxOtaFileSize.ReadOnly = true;
            this.textBoxOtaFileSize.Size = new System.Drawing.Size(74, 20);
            this.textBoxOtaFileSize.TabIndex = 9;
            // 
            // textBoxOtaFileVersion
            // 
            this.textBoxOtaFileVersion.Location = new System.Drawing.Point(1014, 7);
            this.textBoxOtaFileVersion.Name = "textBoxOtaFileVersion";
            this.textBoxOtaFileVersion.ReadOnly = true;
            this.textBoxOtaFileVersion.Size = new System.Drawing.Size(74, 20);
            this.textBoxOtaFileVersion.TabIndex = 7;
            // 
            // textBoxOtaFileImageType
            // 
            this.textBoxOtaFileImageType.Location = new System.Drawing.Point(884, 7);
            this.textBoxOtaFileImageType.Name = "textBoxOtaFileImageType";
            this.textBoxOtaFileImageType.ReadOnly = true;
            this.textBoxOtaFileImageType.Size = new System.Drawing.Size(53, 20);
            this.textBoxOtaFileImageType.TabIndex = 6;
            this.textBoxOtaFileImageType.TextChanged += new System.EventHandler(this.textBoxOtaFileImageType_TextChanged);
            // 
            // textBoxOtaFileManuCode
            // 
            this.textBoxOtaFileManuCode.Location = new System.Drawing.Point(753, 7);
            this.textBoxOtaFileManuCode.Name = "textBoxOtaFileManuCode";
            this.textBoxOtaFileManuCode.ReadOnly = true;
            this.textBoxOtaFileManuCode.Size = new System.Drawing.Size(53, 20);
            this.textBoxOtaFileManuCode.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(94, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Stack Ver";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(218, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Header Ver";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(369, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Header Len";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(522, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Header FCTL";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(94, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "File ID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(369, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Header Str";
            // 
            // buttonOTASetWaitForDataParams
            // 
            this.buttonOTASetWaitForDataParams.Location = new System.Drawing.Point(4, 84);
            this.buttonOTASetWaitForDataParams.Name = "buttonOTASetWaitForDataParams";
            this.buttonOTASetWaitForDataParams.Size = new System.Drawing.Size(86, 26);
            this.buttonOTASetWaitForDataParams.TabIndex = 21;
            this.buttonOTASetWaitForDataParams.Text = "WaitParams";
            this.buttonOTASetWaitForDataParams.UseVisualStyleBackColor = true;
            this.buttonOTASetWaitForDataParams.Click += new System.EventHandler(this.buttonOTASetWaitForDataParams_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(695, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "File Offset";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(209, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Progress";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Download Status";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // progressBarOtaDownloadProgress
            // 
            this.progressBarOtaDownloadProgress.Location = new System.Drawing.Point(266, 114);
            this.progressBarOtaDownloadProgress.Maximum = 1000;
            this.progressBarOtaDownloadProgress.Name = "progressBarOtaDownloadProgress";
            this.progressBarOtaDownloadProgress.Size = new System.Drawing.Size(422, 22);
            this.progressBarOtaDownloadProgress.TabIndex = 0;
            // 
            // comboBoxOTAImageNotifyType
            // 
            this.comboBoxOTAImageNotifyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOTAImageNotifyType.FormattingEnabled = true;
            this.comboBoxOTAImageNotifyType.Location = new System.Drawing.Point(550, 56);
            this.comboBoxOTAImageNotifyType.Name = "comboBoxOTAImageNotifyType";
            this.comboBoxOTAImageNotifyType.Size = new System.Drawing.Size(138, 21);
            this.comboBoxOTAImageNotifyType.TabIndex = 16;
            this.comboBoxOTAImageNotifyType.MouseLeave += new System.EventHandler(this.comboBoxOTAImageNotifyType_MouseLeave);
            this.comboBoxOTAImageNotifyType.MouseHover += new System.EventHandler(this.comboBoxOTAImageNotifyType_MouseHover);
            // 
            // comboBoxOTAImageNotifyAddrMode
            // 
            this.comboBoxOTAImageNotifyAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOTAImageNotifyAddrMode.FormattingEnabled = true;
            this.comboBoxOTAImageNotifyAddrMode.Location = new System.Drawing.Point(97, 57);
            this.comboBoxOTAImageNotifyAddrMode.Name = "comboBoxOTAImageNotifyAddrMode";
            this.comboBoxOTAImageNotifyAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxOTAImageNotifyAddrMode.TabIndex = 12;
            this.comboBoxOTAImageNotifyAddrMode.MouseLeave += new System.EventHandler(this.comboBoxOTAImageNotifyAddrMode_MouseLeave);
            this.comboBoxOTAImageNotifyAddrMode.MouseHover += new System.EventHandler(this.comboBoxOTAImageNotifyAddrMode_MouseHover);
            // 
            // buttonOTAImageNotify
            // 
            this.buttonOTAImageNotify.Location = new System.Drawing.Point(4, 53);
            this.buttonOTAImageNotify.Name = "buttonOTAImageNotify";
            this.buttonOTAImageNotify.Size = new System.Drawing.Size(86, 26);
            this.buttonOTAImageNotify.TabIndex = 11;
            this.buttonOTAImageNotify.Text = "Image Notify";
            this.buttonOTAImageNotify.UseVisualStyleBackColor = true;
            this.buttonOTAImageNotify.Click += new System.EventHandler(this.buttonOTAImageNotify_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(943, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "File Version";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(812, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Image Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(682, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manu Code";
            // 
            // buttonOTALoadNewImage
            // 
            this.buttonOTALoadNewImage.Location = new System.Drawing.Point(3, 3);
            this.buttonOTALoadNewImage.Name = "buttonOTALoadNewImage";
            this.buttonOTALoadNewImage.Size = new System.Drawing.Size(86, 26);
            this.buttonOTALoadNewImage.TabIndex = 0;
            this.buttonOTALoadNewImage.Text = "Load Image";
            this.buttonOTALoadNewImage.UseVisualStyleBackColor = true;
            this.buttonOTALoadNewImage.Click += new System.EventHandler(this.buttonOTALoadNewImage_Click);
            // 
            // tabPage11
            // 
            this.tabPage11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage11.Controls.Add(this.textBoxZllMoveToHueHue);
            this.tabPage11.Controls.Add(this.textBoxZllMoveToHueTransTime);
            this.tabPage11.Controls.Add(this.textBoxZllMoveToHueDirection);
            this.tabPage11.Controls.Add(this.textBoxZllMoveToHueDstEp);
            this.tabPage11.Controls.Add(this.textBoxZllMoveToHueSrcEp);
            this.tabPage11.Controls.Add(this.textBoxZllMoveToHueAddr);
            this.tabPage11.Controls.Add(this.button8);
            this.tabPage11.Controls.Add(this.button7);
            this.tabPage11.Controls.Add(this.button6);
            this.tabPage11.Controls.Add(this.button5);
            this.tabPage11.Controls.Add(this.buttonZllMoveToHue);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(1833, 584);
            this.tabPage11.TabIndex = 11;
            this.tabPage11.Text = "ZLL Color Cluster";
            this.tabPage11.Click += new System.EventHandler(this.tabPage11_Click);
            // 
            // textBoxZllMoveToHueHue
            // 
            this.textBoxZllMoveToHueHue.Location = new System.Drawing.Point(441, 6);
            this.textBoxZllMoveToHueHue.Name = "textBoxZllMoveToHueHue";
            this.textBoxZllMoveToHueHue.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllMoveToHueHue.TabIndex = 4;
            this.textBoxZllMoveToHueHue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllMoveToHueHue_MouseClick);
            this.textBoxZllMoveToHueHue.Leave += new System.EventHandler(this.textBoxZllMoveToHueHue_Leave);
            this.textBoxZllMoveToHueHue.MouseLeave += new System.EventHandler(this.textBoxZllMoveToHueHue_MouseLeave);
            this.textBoxZllMoveToHueHue.MouseHover += new System.EventHandler(this.textBoxZllMoveToHueHue_MouseHover);
            // 
            // textBoxZllMoveToHueTransTime
            // 
            this.textBoxZllMoveToHueTransTime.Location = new System.Drawing.Point(666, 6);
            this.textBoxZllMoveToHueTransTime.Name = "textBoxZllMoveToHueTransTime";
            this.textBoxZllMoveToHueTransTime.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllMoveToHueTransTime.TabIndex = 6;
            this.textBoxZllMoveToHueTransTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllMoveToHueTransTime_MouseClick);
            this.textBoxZllMoveToHueTransTime.Leave += new System.EventHandler(this.textBoxZllMoveToHueTransTime_Leave);
            this.textBoxZllMoveToHueTransTime.MouseLeave += new System.EventHandler(this.textBoxZllMoveToHueTransTime_MouseLeave);
            this.textBoxZllMoveToHueTransTime.MouseHover += new System.EventHandler(this.textBoxZllMoveToHueTransTime_MouseHover);
            // 
            // textBoxZllMoveToHueDirection
            // 
            this.textBoxZllMoveToHueDirection.Location = new System.Drawing.Point(554, 6);
            this.textBoxZllMoveToHueDirection.Name = "textBoxZllMoveToHueDirection";
            this.textBoxZllMoveToHueDirection.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllMoveToHueDirection.TabIndex = 5;
            this.textBoxZllMoveToHueDirection.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllMoveToHueDirection_MouseClick);
            this.textBoxZllMoveToHueDirection.Leave += new System.EventHandler(this.textBoxZllMoveToHueDirection_Leave);
            this.textBoxZllMoveToHueDirection.MouseLeave += new System.EventHandler(this.textBoxZllMoveToHueDirection_MouseLeave);
            this.textBoxZllMoveToHueDirection.MouseHover += new System.EventHandler(this.textBoxZllMoveToHueDirection_MouseHover);
            // 
            // textBoxZllMoveToHueDstEp
            // 
            this.textBoxZllMoveToHueDstEp.Location = new System.Drawing.Point(327, 6);
            this.textBoxZllMoveToHueDstEp.Name = "textBoxZllMoveToHueDstEp";
            this.textBoxZllMoveToHueDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllMoveToHueDstEp.TabIndex = 3;
            this.textBoxZllMoveToHueDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllMoveToHueDstEp_MouseClick);
            this.textBoxZllMoveToHueDstEp.Leave += new System.EventHandler(this.textBoxZllMoveToHueDstEp_Leave);
            this.textBoxZllMoveToHueDstEp.MouseLeave += new System.EventHandler(this.textBoxZllMoveToHueDstEp_MouseLeave);
            this.textBoxZllMoveToHueDstEp.MouseHover += new System.EventHandler(this.textBoxZllMoveToHueDstEp_MouseHover);
            // 
            // textBoxZllMoveToHueSrcEp
            // 
            this.textBoxZllMoveToHueSrcEp.Location = new System.Drawing.Point(214, 6);
            this.textBoxZllMoveToHueSrcEp.Name = "textBoxZllMoveToHueSrcEp";
            this.textBoxZllMoveToHueSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllMoveToHueSrcEp.TabIndex = 2;
            this.textBoxZllMoveToHueSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllMoveToHueSrcEp_MouseClick);
            this.textBoxZllMoveToHueSrcEp.Leave += new System.EventHandler(this.textBoxZllMoveToHueSrcEp_Leave);
            this.textBoxZllMoveToHueSrcEp.MouseLeave += new System.EventHandler(this.textBoxZllMoveToHueSrcEp_MouseLeave);
            this.textBoxZllMoveToHueSrcEp.MouseHover += new System.EventHandler(this.textBoxZllMoveToHueSrcEp_MouseHover);
            // 
            // textBoxZllMoveToHueAddr
            // 
            this.textBoxZllMoveToHueAddr.Location = new System.Drawing.Point(102, 6);
            this.textBoxZllMoveToHueAddr.Name = "textBoxZllMoveToHueAddr";
            this.textBoxZllMoveToHueAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllMoveToHueAddr.TabIndex = 1;
            this.textBoxZllMoveToHueAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllMoveToHueAddr_MouseClick);
            this.textBoxZllMoveToHueAddr.Leave += new System.EventHandler(this.textBoxZllMoveToHueAddr_Leave);
            this.textBoxZllMoveToHueAddr.MouseLeave += new System.EventHandler(this.textBoxZllMoveToHueAddr_MouseLeave);
            this.textBoxZllMoveToHueAddr.MouseHover += new System.EventHandler(this.textBoxZllMoveToHueAddr_MouseHover);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(4, 122);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(90, 22);
            this.button8.TabIndex = 0;
            this.button8.Text = "Color Loop";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Visible = false;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(4, 93);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(90, 22);
            this.button7.TabIndex = 0;
            this.button7.Text = "Hue and Sat";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Visible = false;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(4, 63);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(90, 22);
            this.button6.TabIndex = 0;
            this.button6.Text = "Step Hue";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Visible = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(4, 34);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 22);
            this.button5.TabIndex = 0;
            this.button5.Text = "Move Hue";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            // 
            // buttonZllMoveToHue
            // 
            this.buttonZllMoveToHue.Location = new System.Drawing.Point(4, 4);
            this.buttonZllMoveToHue.Name = "buttonZllMoveToHue";
            this.buttonZllMoveToHue.Size = new System.Drawing.Size(90, 22);
            this.buttonZllMoveToHue.TabIndex = 0;
            this.buttonZllMoveToHue.Text = "Move to Hue";
            this.buttonZllMoveToHue.UseVisualStyleBackColor = true;
            this.buttonZllMoveToHue.Click += new System.EventHandler(this.buttonZllMoveToHue_Click);
            // 
            // tabPage10
            // 
            this.tabPage10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage10.Controls.Add(this.comboBoxZllOnOffEffectID);
            this.tabPage10.Controls.Add(this.textBoxZllOnOffEffectsGradient);
            this.tabPage10.Controls.Add(this.textBoxZllOnOffEffectsDstEp);
            this.tabPage10.Controls.Add(this.textBoxZllOnOffEffectsSrcEp);
            this.tabPage10.Controls.Add(this.textBoxZllOnOffEffectsAddr);
            this.tabPage10.Controls.Add(this.buttonZllOnOffEffects);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(1833, 584);
            this.tabPage10.TabIndex = 10;
            this.tabPage10.Text = "ZLL On/Off Cluster";
            this.tabPage10.Click += new System.EventHandler(this.tabPage10_Click);
            // 
            // comboBoxZllOnOffEffectID
            // 
            this.comboBoxZllOnOffEffectID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZllOnOffEffectID.FormattingEnabled = true;
            this.comboBoxZllOnOffEffectID.Location = new System.Drawing.Point(441, 6);
            this.comboBoxZllOnOffEffectID.Name = "comboBoxZllOnOffEffectID";
            this.comboBoxZllOnOffEffectID.Size = new System.Drawing.Size(129, 21);
            this.comboBoxZllOnOffEffectID.TabIndex = 4;
            this.comboBoxZllOnOffEffectID.MouseLeave += new System.EventHandler(this.comboBoxZllOnOffEffectID_MouseLeave);
            this.comboBoxZllOnOffEffectID.MouseHover += new System.EventHandler(this.comboBoxZllOnOffEffectID_MouseHover);
            // 
            // textBoxZllOnOffEffectsGradient
            // 
            this.textBoxZllOnOffEffectsGradient.Location = new System.Drawing.Point(576, 6);
            this.textBoxZllOnOffEffectsGradient.Name = "textBoxZllOnOffEffectsGradient";
            this.textBoxZllOnOffEffectsGradient.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllOnOffEffectsGradient.TabIndex = 5;
            this.textBoxZllOnOffEffectsGradient.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllOnOffEffectsGradient_MouseClick);
            this.textBoxZllOnOffEffectsGradient.Leave += new System.EventHandler(this.textBoxZllOnOffEffectsGradient_Leave);
            this.textBoxZllOnOffEffectsGradient.MouseLeave += new System.EventHandler(this.textBoxZllOnOffEffectsGradient_MouseLeave);
            this.textBoxZllOnOffEffectsGradient.MouseHover += new System.EventHandler(this.textBoxZllOnOffEffectsGradient_MouseHover);
            // 
            // textBoxZllOnOffEffectsDstEp
            // 
            this.textBoxZllOnOffEffectsDstEp.Location = new System.Drawing.Point(327, 6);
            this.textBoxZllOnOffEffectsDstEp.Name = "textBoxZllOnOffEffectsDstEp";
            this.textBoxZllOnOffEffectsDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllOnOffEffectsDstEp.TabIndex = 3;
            this.textBoxZllOnOffEffectsDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllOnOffEffectsDstEp_MouseClick);
            this.textBoxZllOnOffEffectsDstEp.Leave += new System.EventHandler(this.textBoxZllOnOffEffectsDstEp_Leave);
            this.textBoxZllOnOffEffectsDstEp.MouseHover += new System.EventHandler(this.textBoxZllOnOffEffectsDstEp_MouseHover);
            // 
            // textBoxZllOnOffEffectsSrcEp
            // 
            this.textBoxZllOnOffEffectsSrcEp.Location = new System.Drawing.Point(214, 6);
            this.textBoxZllOnOffEffectsSrcEp.Name = "textBoxZllOnOffEffectsSrcEp";
            this.textBoxZllOnOffEffectsSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllOnOffEffectsSrcEp.TabIndex = 2;
            this.textBoxZllOnOffEffectsSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllOnOffEffectsSrcEp_MouseClick);
            this.textBoxZllOnOffEffectsSrcEp.Leave += new System.EventHandler(this.textBoxZllOnOffEffectsSrcEp_Leave);
            this.textBoxZllOnOffEffectsSrcEp.MouseLeave += new System.EventHandler(this.textBoxZllOnOffEffectsSrcEp_MouseLeave);
            this.textBoxZllOnOffEffectsSrcEp.MouseHover += new System.EventHandler(this.textBoxZllOnOffEffectsSrcEp_MouseHover);
            // 
            // textBoxZllOnOffEffectsAddr
            // 
            this.textBoxZllOnOffEffectsAddr.Location = new System.Drawing.Point(102, 6);
            this.textBoxZllOnOffEffectsAddr.Name = "textBoxZllOnOffEffectsAddr";
            this.textBoxZllOnOffEffectsAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxZllOnOffEffectsAddr.TabIndex = 1;
            this.textBoxZllOnOffEffectsAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxZllOnOffEffectsAddr_MouseClick);
            this.textBoxZllOnOffEffectsAddr.Leave += new System.EventHandler(this.textBoxZllOnOffEffectsAddr_Leave);
            this.textBoxZllOnOffEffectsAddr.MouseLeave += new System.EventHandler(this.textBoxZllOnOffEffectsAddr_MouseLeave);
            this.textBoxZllOnOffEffectsAddr.MouseHover += new System.EventHandler(this.textBoxZllOnOffEffectsAddr_MouseHover);
            // 
            // buttonZllOnOffEffects
            // 
            this.buttonZllOnOffEffects.Location = new System.Drawing.Point(4, 4);
            this.buttonZllOnOffEffects.Name = "buttonZllOnOffEffects";
            this.buttonZllOnOffEffects.Size = new System.Drawing.Size(90, 22);
            this.buttonZllOnOffEffects.TabIndex = 0;
            this.buttonZllOnOffEffects.Text = "On/Off Effects";
            this.buttonZllOnOffEffects.UseVisualStyleBackColor = true;
            this.buttonZllOnOffEffects.Click += new System.EventHandler(this.buttonZllOnOffEffects_Click);
            // 
            // tabPage9
            // 
            this.tabPage9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage9.Controls.Add(this.buttonZllTouchlinkFactoryReset);
            this.tabPage9.Controls.Add(this.buttonZllTouchlinkInitiate);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(1833, 584);
            this.tabPage9.TabIndex = 9;
            this.tabPage9.Text = "ZLL Touchlink";
            this.tabPage9.Click += new System.EventHandler(this.tabPage9_Click);
            // 
            // buttonZllTouchlinkFactoryReset
            // 
            this.buttonZllTouchlinkFactoryReset.Location = new System.Drawing.Point(4, 34);
            this.buttonZllTouchlinkFactoryReset.Name = "buttonZllTouchlinkFactoryReset";
            this.buttonZllTouchlinkFactoryReset.Size = new System.Drawing.Size(80, 22);
            this.buttonZllTouchlinkFactoryReset.TabIndex = 1;
            this.buttonZllTouchlinkFactoryReset.Text = "Reset";
            this.buttonZllTouchlinkFactoryReset.UseVisualStyleBackColor = true;
            this.buttonZllTouchlinkFactoryReset.Click += new System.EventHandler(this.buttonZllTouchlinkFactoryReset_Click);
            // 
            // buttonZllTouchlinkInitiate
            // 
            this.buttonZllTouchlinkInitiate.Location = new System.Drawing.Point(4, 4);
            this.buttonZllTouchlinkInitiate.Name = "buttonZllTouchlinkInitiate";
            this.buttonZllTouchlinkInitiate.Size = new System.Drawing.Size(80, 22);
            this.buttonZllTouchlinkInitiate.TabIndex = 0;
            this.buttonZllTouchlinkInitiate.Text = "Initiate";
            this.buttonZllTouchlinkInitiate.UseVisualStyleBackColor = true;
            this.buttonZllTouchlinkInitiate.Click += new System.EventHandler(this.buttonZllTouchlinkInitiate_Click);
            // 
            // tabPage15
            // 
            this.tabPage15.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage15.Controls.Add(this.label17);
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Size = new System.Drawing.Size(1833, 584);
            this.tabPage15.TabIndex = 19;
            this.tabPage15.Text = "IAS WD Cluster";
            this.tabPage15.Click += new System.EventHandler(this.tabPage15_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(520, 186);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(311, 46);
            this.label17.TabIndex = 16;
            this.label17.Text = "Unimplemented";
            // 
            // tabPage13
            // 
            this.tabPage13.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage13.Controls.Add(this.comboBoxEnrollRspCode);
            this.tabPage13.Controls.Add(this.textBoxEnrollRspZone);
            this.tabPage13.Controls.Add(this.textBoxEnrollRspDstEp);
            this.tabPage13.Controls.Add(this.textBoxEnrollRspSrcEp);
            this.tabPage13.Controls.Add(this.textBoxEnrollRspAddr);
            this.tabPage13.Controls.Add(this.comboBoxEnrollRspAddrMode);
            this.tabPage13.Controls.Add(this.buttonEnrollResponse);
            this.tabPage13.Location = new System.Drawing.Point(4, 22);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(1833, 584);
            this.tabPage13.TabIndex = 13;
            this.tabPage13.Text = "IAS Zone Cluster";
            this.tabPage13.Click += new System.EventHandler(this.tabPage13_Click);
            // 
            // comboBoxEnrollRspCode
            // 
            this.comboBoxEnrollRspCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnrollRspCode.FormattingEnabled = true;
            this.comboBoxEnrollRspCode.Location = new System.Drawing.Point(545, 9);
            this.comboBoxEnrollRspCode.Name = "comboBoxEnrollRspCode";
            this.comboBoxEnrollRspCode.Size = new System.Drawing.Size(138, 21);
            this.comboBoxEnrollRspCode.TabIndex = 5;
            this.comboBoxEnrollRspCode.MouseLeave += new System.EventHandler(this.comboBoxEnrollRspCode_MouseLeave);
            this.comboBoxEnrollRspCode.MouseHover += new System.EventHandler(this.comboBoxEnrollRspCode_MouseHover);
            // 
            // textBoxEnrollRspZone
            // 
            this.textBoxEnrollRspZone.Location = new System.Drawing.Point(690, 8);
            this.textBoxEnrollRspZone.Name = "textBoxEnrollRspZone";
            this.textBoxEnrollRspZone.Size = new System.Drawing.Size(106, 20);
            this.textBoxEnrollRspZone.TabIndex = 6;
            this.textBoxEnrollRspZone.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEnrollRspZone_MouseClick);
            this.textBoxEnrollRspZone.Leave += new System.EventHandler(this.textBoxEnrollRspZone_Leave);
            this.textBoxEnrollRspZone.MouseLeave += new System.EventHandler(this.textBoxEnrollRspZone_MouseLeave);
            this.textBoxEnrollRspZone.MouseHover += new System.EventHandler(this.textBoxEnrollRspZone_MouseHover);
            // 
            // textBoxEnrollRspDstEp
            // 
            this.textBoxEnrollRspDstEp.Location = new System.Drawing.Point(432, 9);
            this.textBoxEnrollRspDstEp.Name = "textBoxEnrollRspDstEp";
            this.textBoxEnrollRspDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxEnrollRspDstEp.TabIndex = 4;
            this.textBoxEnrollRspDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEnrollRspDstEp_MouseClick);
            this.textBoxEnrollRspDstEp.Leave += new System.EventHandler(this.textBoxEnrollRspDstEp_Leave);
            this.textBoxEnrollRspDstEp.MouseLeave += new System.EventHandler(this.textBoxEnrollRspDstEp_MouseLeave);
            this.textBoxEnrollRspDstEp.MouseHover += new System.EventHandler(this.textBoxEnrollRspDstEp_MouseHover);
            // 
            // textBoxEnrollRspSrcEp
            // 
            this.textBoxEnrollRspSrcEp.Location = new System.Drawing.Point(319, 9);
            this.textBoxEnrollRspSrcEp.Name = "textBoxEnrollRspSrcEp";
            this.textBoxEnrollRspSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxEnrollRspSrcEp.TabIndex = 3;
            this.textBoxEnrollRspSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEnrollRspSrcEp_MouseClick);
            this.textBoxEnrollRspSrcEp.Leave += new System.EventHandler(this.textBoxEnrollRspSrcEp_Leave);
            this.textBoxEnrollRspSrcEp.MouseLeave += new System.EventHandler(this.textBoxEnrollRspSrcEp_MouseLeave);
            this.textBoxEnrollRspSrcEp.MouseHover += new System.EventHandler(this.textBoxEnrollRspSrcEp_MouseHover);
            // 
            // textBoxEnrollRspAddr
            // 
            this.textBoxEnrollRspAddr.Location = new System.Drawing.Point(206, 9);
            this.textBoxEnrollRspAddr.Name = "textBoxEnrollRspAddr";
            this.textBoxEnrollRspAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxEnrollRspAddr.TabIndex = 2;
            this.textBoxEnrollRspAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEnrollRspAddr_MouseClick);
            this.textBoxEnrollRspAddr.Leave += new System.EventHandler(this.textBoxEnrollRspAddr_Leave);
            this.textBoxEnrollRspAddr.MouseLeave += new System.EventHandler(this.textBoxEnrollRspAddr_MouseLeave);
            this.textBoxEnrollRspAddr.MouseHover += new System.EventHandler(this.textBoxEnrollRspAddr_MouseHover);
            // 
            // comboBoxEnrollRspAddrMode
            // 
            this.comboBoxEnrollRspAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnrollRspAddrMode.FormattingEnabled = true;
            this.comboBoxEnrollRspAddrMode.Location = new System.Drawing.Point(93, 8);
            this.comboBoxEnrollRspAddrMode.Name = "comboBoxEnrollRspAddrMode";
            this.comboBoxEnrollRspAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxEnrollRspAddrMode.TabIndex = 1;
            this.comboBoxEnrollRspAddrMode.MouseLeave += new System.EventHandler(this.comboBoxEnrollRspAddrMode_MouseLeave);
            this.comboBoxEnrollRspAddrMode.MouseHover += new System.EventHandler(this.comboBoxEnrollRspAddrMode_MouseHover);
            // 
            // buttonEnrollResponse
            // 
            this.buttonEnrollResponse.Location = new System.Drawing.Point(6, 6);
            this.buttonEnrollResponse.Name = "buttonEnrollResponse";
            this.buttonEnrollResponse.Size = new System.Drawing.Size(80, 22);
            this.buttonEnrollResponse.TabIndex = 0;
            this.buttonEnrollResponse.Text = "Enroll Rsp";
            this.buttonEnrollResponse.UseVisualStyleBackColor = true;
            this.buttonEnrollResponse.Click += new System.EventHandler(this.buttonEnrollResponse_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.comboBoxLockUnlock);
            this.tabPage1.Controls.Add(this.textBoxLockUnlockDstEp);
            this.tabPage1.Controls.Add(this.textBoxLockUnlockSrcEp);
            this.tabPage1.Controls.Add(this.textBoxLockUnlockAddr);
            this.tabPage1.Controls.Add(this.buttonLockUnlock);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1833, 584);
            this.tabPage1.TabIndex = 8;
            this.tabPage1.Text = "Door Lock Cluster";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // comboBoxLockUnlock
            // 
            this.comboBoxLockUnlock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLockUnlock.FormattingEnabled = true;
            this.comboBoxLockUnlock.Location = new System.Drawing.Point(430, 6);
            this.comboBoxLockUnlock.Name = "comboBoxLockUnlock";
            this.comboBoxLockUnlock.Size = new System.Drawing.Size(129, 21);
            this.comboBoxLockUnlock.TabIndex = 4;
            this.comboBoxLockUnlock.MouseLeave += new System.EventHandler(this.comboBoxLockUnlock_MouseLeave);
            this.comboBoxLockUnlock.MouseHover += new System.EventHandler(this.comboBoxLockUnlock_MouseHover);
            // 
            // textBoxLockUnlockDstEp
            // 
            this.textBoxLockUnlockDstEp.Location = new System.Drawing.Point(317, 6);
            this.textBoxLockUnlockDstEp.Name = "textBoxLockUnlockDstEp";
            this.textBoxLockUnlockDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxLockUnlockDstEp.TabIndex = 3;
            this.textBoxLockUnlockDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLockUnlockDstEp_MouseClick);
            this.textBoxLockUnlockDstEp.Leave += new System.EventHandler(this.textBoxLockUnlockDstEp_Leave);
            this.textBoxLockUnlockDstEp.MouseLeave += new System.EventHandler(this.textBoxLockUnlockDstEp_MouseLeave);
            this.textBoxLockUnlockDstEp.MouseHover += new System.EventHandler(this.textBoxLockUnlockDstEp_MouseHover);
            // 
            // textBoxLockUnlockSrcEp
            // 
            this.textBoxLockUnlockSrcEp.Location = new System.Drawing.Point(204, 6);
            this.textBoxLockUnlockSrcEp.Name = "textBoxLockUnlockSrcEp";
            this.textBoxLockUnlockSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxLockUnlockSrcEp.TabIndex = 2;
            this.textBoxLockUnlockSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLockUnlockSrcEp_MouseClick);
            this.textBoxLockUnlockSrcEp.Leave += new System.EventHandler(this.textBoxLockUnlockSrcEp_Leave);
            this.textBoxLockUnlockSrcEp.MouseLeave += new System.EventHandler(this.textBoxLockUnlockSrcEp_MouseLeave);
            this.textBoxLockUnlockSrcEp.MouseHover += new System.EventHandler(this.textBoxLockUnlockSrcEp_MouseHover);
            // 
            // textBoxLockUnlockAddr
            // 
            this.textBoxLockUnlockAddr.Location = new System.Drawing.Point(90, 6);
            this.textBoxLockUnlockAddr.Name = "textBoxLockUnlockAddr";
            this.textBoxLockUnlockAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxLockUnlockAddr.TabIndex = 1;
            this.textBoxLockUnlockAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLockUnlockAddr_MouseClick);
            this.textBoxLockUnlockAddr.Leave += new System.EventHandler(this.textBoxLockUnlockAddr_Leave);
            this.textBoxLockUnlockAddr.MouseLeave += new System.EventHandler(this.textBoxLockUnlockAddr_MouseLeave);
            this.textBoxLockUnlockAddr.MouseHover += new System.EventHandler(this.textBoxLockUnlockAddr_MouseHover);
            // 
            // buttonLockUnlock
            // 
            this.buttonLockUnlock.Location = new System.Drawing.Point(4, 4);
            this.buttonLockUnlock.Name = "buttonLockUnlock";
            this.buttonLockUnlock.Size = new System.Drawing.Size(80, 22);
            this.buttonLockUnlock.TabIndex = 0;
            this.buttonLockUnlock.Text = "LockUnlock";
            this.buttonLockUnlock.UseVisualStyleBackColor = true;
            this.buttonLockUnlock.Click += new System.EventHandler(this.buttonLockUnlock_Click);
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage8.Controls.Add(this.textBoxMoveToSatTime);
            this.tabPage8.Controls.Add(this.textBoxMoveToSatSat);
            this.tabPage8.Controls.Add(this.textBoxMoveToSatDstEp);
            this.tabPage8.Controls.Add(this.textBoxMoveToSatSrcEp);
            this.tabPage8.Controls.Add(this.textBoxMoveToSatAddr);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorTempRate);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorTempTemp);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorTempDstEp);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorTempSrcEp);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorTempAddr);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorTime);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorY);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorX);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorDstEp);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorSrcEp);
            this.tabPage8.Controls.Add(this.textBoxMoveToColorAddr);
            this.tabPage8.Controls.Add(this.textBoxMoveToHueTime);
            this.tabPage8.Controls.Add(this.textBoxMoveToHueDir);
            this.tabPage8.Controls.Add(this.textBoxMoveToHueHue);
            this.tabPage8.Controls.Add(this.textBoxMoveToHueDstEp);
            this.tabPage8.Controls.Add(this.textBoxMoveToHueSrcEp);
            this.tabPage8.Controls.Add(this.textBoxMoveToHueAddr);
            this.tabPage8.Controls.Add(this.buttonMoveToSat);
            this.tabPage8.Controls.Add(this.buttonMoveToColorTemp);
            this.tabPage8.Controls.Add(this.buttonMoveToColor);
            this.tabPage8.Controls.Add(this.buttonMoveToHue);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(1833, 584);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Color Cluster";
            this.tabPage8.Click += new System.EventHandler(this.tabPage8_Click);
            // 
            // textBoxMoveToSatTime
            // 
            this.textBoxMoveToSatTime.Location = new System.Drawing.Point(554, 63);
            this.textBoxMoveToSatTime.Name = "textBoxMoveToSatTime";
            this.textBoxMoveToSatTime.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToSatTime.TabIndex = 19;
            this.textBoxMoveToSatTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToSatTime_MouseClick);
            this.textBoxMoveToSatTime.Leave += new System.EventHandler(this.textBoxMoveToSatTime_Leave);
            this.textBoxMoveToSatTime.MouseLeave += new System.EventHandler(this.textBoxMoveToSatTime_MouseLeave);
            this.textBoxMoveToSatTime.MouseHover += new System.EventHandler(this.textBoxMoveToSatTime_MouseHover);
            // 
            // textBoxMoveToSatSat
            // 
            this.textBoxMoveToSatSat.Location = new System.Drawing.Point(442, 63);
            this.textBoxMoveToSatSat.Name = "textBoxMoveToSatSat";
            this.textBoxMoveToSatSat.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToSatSat.TabIndex = 18;
            this.textBoxMoveToSatSat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToSatSat_MouseClick);
            this.textBoxMoveToSatSat.Leave += new System.EventHandler(this.textBoxMoveToSatSat_Leave);
            this.textBoxMoveToSatSat.MouseLeave += new System.EventHandler(this.textBoxMoveToSatSat_MouseLeave);
            this.textBoxMoveToSatSat.MouseHover += new System.EventHandler(this.textBoxMoveToSatSat_MouseHover);
            // 
            // textBoxMoveToSatDstEp
            // 
            this.textBoxMoveToSatDstEp.Location = new System.Drawing.Point(329, 63);
            this.textBoxMoveToSatDstEp.Name = "textBoxMoveToSatDstEp";
            this.textBoxMoveToSatDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToSatDstEp.TabIndex = 17;
            this.textBoxMoveToSatDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToSatDstEp_MouseClick);
            this.textBoxMoveToSatDstEp.Leave += new System.EventHandler(this.textBoxMoveToSatDstEp_Leave);
            this.textBoxMoveToSatDstEp.MouseLeave += new System.EventHandler(this.textBoxMoveToSatDstEp_MouseLeave);
            this.textBoxMoveToSatDstEp.MouseHover += new System.EventHandler(this.textBoxMoveToSatDstEp_MouseHover);
            // 
            // textBoxMoveToSatSrcEp
            // 
            this.textBoxMoveToSatSrcEp.Location = new System.Drawing.Point(215, 63);
            this.textBoxMoveToSatSrcEp.Name = "textBoxMoveToSatSrcEp";
            this.textBoxMoveToSatSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToSatSrcEp.TabIndex = 16;
            this.textBoxMoveToSatSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToSatSrcEp_MouseClick);
            this.textBoxMoveToSatSrcEp.Leave += new System.EventHandler(this.textBoxMoveToSatSrcEp_Leave);
            this.textBoxMoveToSatSrcEp.MouseLeave += new System.EventHandler(this.textBoxMoveToSatSrcEp_MouseLeave);
            this.textBoxMoveToSatSrcEp.MouseHover += new System.EventHandler(this.textBoxMoveToSatSrcEp_MouseHover);
            // 
            // textBoxMoveToSatAddr
            // 
            this.textBoxMoveToSatAddr.Location = new System.Drawing.Point(102, 63);
            this.textBoxMoveToSatAddr.Name = "textBoxMoveToSatAddr";
            this.textBoxMoveToSatAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToSatAddr.TabIndex = 15;
            this.textBoxMoveToSatAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToSatAddr_MouseClick);
            this.textBoxMoveToSatAddr.Leave += new System.EventHandler(this.textBoxMoveToSatAddr_Leave);
            this.textBoxMoveToSatAddr.MouseLeave += new System.EventHandler(this.textBoxMoveToSatAddr_MouseLeave);
            this.textBoxMoveToSatAddr.MouseHover += new System.EventHandler(this.textBoxMoveToSatAddr_MouseHover);
            // 
            // textBoxMoveToColorTempRate
            // 
            this.textBoxMoveToColorTempRate.Location = new System.Drawing.Point(554, 93);
            this.textBoxMoveToColorTempRate.Name = "textBoxMoveToColorTempRate";
            this.textBoxMoveToColorTempRate.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorTempRate.TabIndex = 25;
            this.textBoxMoveToColorTempRate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorTempRate_MouseClick);
            this.textBoxMoveToColorTempRate.Leave += new System.EventHandler(this.textBoxMoveToColorTempRate_Leave);
            this.textBoxMoveToColorTempRate.MouseLeave += new System.EventHandler(this.textBoxMoveToColorTempRate_MouseLeave);
            this.textBoxMoveToColorTempRate.MouseHover += new System.EventHandler(this.textBoxMoveToColorTempRate_MouseHover);
            // 
            // textBoxMoveToColorTempTemp
            // 
            this.textBoxMoveToColorTempTemp.Location = new System.Drawing.Point(442, 93);
            this.textBoxMoveToColorTempTemp.Name = "textBoxMoveToColorTempTemp";
            this.textBoxMoveToColorTempTemp.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorTempTemp.TabIndex = 24;
            this.textBoxMoveToColorTempTemp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorTempTemp_MouseClick);
            this.textBoxMoveToColorTempTemp.Leave += new System.EventHandler(this.textBoxMoveToColorTempTemp_Leave);
            this.textBoxMoveToColorTempTemp.MouseLeave += new System.EventHandler(this.textBoxMoveToColorTempTemp_MouseLeave);
            this.textBoxMoveToColorTempTemp.MouseHover += new System.EventHandler(this.textBoxMoveToColorTempTemp_MouseHover);
            // 
            // textBoxMoveToColorTempDstEp
            // 
            this.textBoxMoveToColorTempDstEp.Location = new System.Drawing.Point(329, 93);
            this.textBoxMoveToColorTempDstEp.Name = "textBoxMoveToColorTempDstEp";
            this.textBoxMoveToColorTempDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorTempDstEp.TabIndex = 23;
            this.textBoxMoveToColorTempDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorTempDstEp_MouseClick);
            this.textBoxMoveToColorTempDstEp.Leave += new System.EventHandler(this.textBoxMoveToColorTempDstEp_Leave);
            this.textBoxMoveToColorTempDstEp.MouseLeave += new System.EventHandler(this.textBoxMoveToColorTempDstEp_MouseLeave);
            this.textBoxMoveToColorTempDstEp.MouseHover += new System.EventHandler(this.textBoxMoveToColorTempDstEp_MouseHover);
            // 
            // textBoxMoveToColorTempSrcEp
            // 
            this.textBoxMoveToColorTempSrcEp.Location = new System.Drawing.Point(215, 93);
            this.textBoxMoveToColorTempSrcEp.Name = "textBoxMoveToColorTempSrcEp";
            this.textBoxMoveToColorTempSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorTempSrcEp.TabIndex = 22;
            this.textBoxMoveToColorTempSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorTempSrcEp_MouseClick);
            this.textBoxMoveToColorTempSrcEp.Leave += new System.EventHandler(this.textBoxMoveToColorTempSrcEp_Leave);
            this.textBoxMoveToColorTempSrcEp.MouseLeave += new System.EventHandler(this.textBoxMoveToColorTempSrcEp_MouseLeave);
            this.textBoxMoveToColorTempSrcEp.MouseHover += new System.EventHandler(this.textBoxMoveToColorTempSrcEp_MouseHover);
            // 
            // textBoxMoveToColorTempAddr
            // 
            this.textBoxMoveToColorTempAddr.Location = new System.Drawing.Point(102, 93);
            this.textBoxMoveToColorTempAddr.Name = "textBoxMoveToColorTempAddr";
            this.textBoxMoveToColorTempAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorTempAddr.TabIndex = 21;
            this.textBoxMoveToColorTempAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorTempAddr_MouseClick);
            this.textBoxMoveToColorTempAddr.Leave += new System.EventHandler(this.textBoxMoveToColorTempAddr_Leave);
            this.textBoxMoveToColorTempAddr.MouseLeave += new System.EventHandler(this.textBoxMoveToColorTempAddr_MouseLeave);
            this.textBoxMoveToColorTempAddr.MouseHover += new System.EventHandler(this.textBoxMoveToColorTempAddr_MouseHover);
            // 
            // textBoxMoveToColorTime
            // 
            this.textBoxMoveToColorTime.Location = new System.Drawing.Point(668, 34);
            this.textBoxMoveToColorTime.Name = "textBoxMoveToColorTime";
            this.textBoxMoveToColorTime.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorTime.TabIndex = 13;
            this.textBoxMoveToColorTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorTime_MouseClick);
            this.textBoxMoveToColorTime.Leave += new System.EventHandler(this.textBoxMoveToColorTime_Leave);
            this.textBoxMoveToColorTime.MouseLeave += new System.EventHandler(this.textBoxMoveToColorTime_MouseLeave);
            this.textBoxMoveToColorTime.MouseHover += new System.EventHandler(this.textBoxMoveToColorTime_MouseHover);
            // 
            // textBoxMoveToColorY
            // 
            this.textBoxMoveToColorY.Location = new System.Drawing.Point(554, 34);
            this.textBoxMoveToColorY.Name = "textBoxMoveToColorY";
            this.textBoxMoveToColorY.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorY.TabIndex = 12;
            this.textBoxMoveToColorY.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorY_MouseClick);
            this.textBoxMoveToColorY.Leave += new System.EventHandler(this.textBoxMoveToColorY_Leave);
            this.textBoxMoveToColorY.MouseLeave += new System.EventHandler(this.textBoxMoveToColorY_MouseLeave);
            this.textBoxMoveToColorY.MouseHover += new System.EventHandler(this.textBoxMoveToColorY_MouseHover);
            // 
            // textBoxMoveToColorX
            // 
            this.textBoxMoveToColorX.Location = new System.Drawing.Point(442, 35);
            this.textBoxMoveToColorX.Name = "textBoxMoveToColorX";
            this.textBoxMoveToColorX.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorX.TabIndex = 11;
            this.textBoxMoveToColorX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorX_MouseClick);
            this.textBoxMoveToColorX.Leave += new System.EventHandler(this.textBoxMoveToColorX_Leave);
            this.textBoxMoveToColorX.MouseLeave += new System.EventHandler(this.textBoxMoveToColorX_MouseLeave);
            this.textBoxMoveToColorX.MouseHover += new System.EventHandler(this.textBoxMoveToColorX_MouseHover);
            // 
            // textBoxMoveToColorDstEp
            // 
            this.textBoxMoveToColorDstEp.Location = new System.Drawing.Point(329, 34);
            this.textBoxMoveToColorDstEp.Name = "textBoxMoveToColorDstEp";
            this.textBoxMoveToColorDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorDstEp.TabIndex = 10;
            this.textBoxMoveToColorDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorDstEp_MouseClick);
            this.textBoxMoveToColorDstEp.Leave += new System.EventHandler(this.textBoxMoveToColorDstEp_Leave);
            this.textBoxMoveToColorDstEp.MouseLeave += new System.EventHandler(this.textBoxMoveToColorDstEp_MouseLeave);
            this.textBoxMoveToColorDstEp.MouseHover += new System.EventHandler(this.textBoxMoveToColorDstEp_MouseHover);
            // 
            // textBoxMoveToColorSrcEp
            // 
            this.textBoxMoveToColorSrcEp.Location = new System.Drawing.Point(215, 34);
            this.textBoxMoveToColorSrcEp.Name = "textBoxMoveToColorSrcEp";
            this.textBoxMoveToColorSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorSrcEp.TabIndex = 9;
            this.textBoxMoveToColorSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorSrcEp_MouseClick);
            this.textBoxMoveToColorSrcEp.Leave += new System.EventHandler(this.textBoxMoveToColorSrcEp_Leave);
            this.textBoxMoveToColorSrcEp.MouseLeave += new System.EventHandler(this.textBoxMoveToColorSrcEp_MouseLeave);
            this.textBoxMoveToColorSrcEp.MouseHover += new System.EventHandler(this.textBoxMoveToColorSrcEp_MouseHover);
            // 
            // textBoxMoveToColorAddr
            // 
            this.textBoxMoveToColorAddr.Location = new System.Drawing.Point(102, 35);
            this.textBoxMoveToColorAddr.Name = "textBoxMoveToColorAddr";
            this.textBoxMoveToColorAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToColorAddr.TabIndex = 8;
            this.textBoxMoveToColorAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToColorAddr_MouseClick);
            this.textBoxMoveToColorAddr.Leave += new System.EventHandler(this.textBoxMoveToColorAddr_Leave);
            this.textBoxMoveToColorAddr.MouseLeave += new System.EventHandler(this.textBoxMoveToColorAddr_MouseLeave);
            this.textBoxMoveToColorAddr.MouseHover += new System.EventHandler(this.textBoxMoveToColorAddr_MouseHover);
            // 
            // textBoxMoveToHueTime
            // 
            this.textBoxMoveToHueTime.Location = new System.Drawing.Point(668, 6);
            this.textBoxMoveToHueTime.Name = "textBoxMoveToHueTime";
            this.textBoxMoveToHueTime.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToHueTime.TabIndex = 6;
            this.textBoxMoveToHueTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToHueTime_MouseClick);
            this.textBoxMoveToHueTime.Leave += new System.EventHandler(this.textBoxMoveToHueTime_Leave);
            this.textBoxMoveToHueTime.MouseLeave += new System.EventHandler(this.textBoxMoveToHueTime_MouseLeave);
            this.textBoxMoveToHueTime.MouseHover += new System.EventHandler(this.textBoxMoveToHueTime_MouseHover);
            // 
            // textBoxMoveToHueDir
            // 
            this.textBoxMoveToHueDir.Location = new System.Drawing.Point(554, 6);
            this.textBoxMoveToHueDir.Name = "textBoxMoveToHueDir";
            this.textBoxMoveToHueDir.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToHueDir.TabIndex = 5;
            this.textBoxMoveToHueDir.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToHueDir_MouseClick);
            this.textBoxMoveToHueDir.Leave += new System.EventHandler(this.textBoxMoveToHueDir_Leave);
            this.textBoxMoveToHueDir.MouseLeave += new System.EventHandler(this.textBoxMoveToHueDir_MouseLeave);
            this.textBoxMoveToHueDir.MouseHover += new System.EventHandler(this.textBoxMoveToHueDir_MouseHover);
            // 
            // textBoxMoveToHueHue
            // 
            this.textBoxMoveToHueHue.Location = new System.Drawing.Point(442, 6);
            this.textBoxMoveToHueHue.Name = "textBoxMoveToHueHue";
            this.textBoxMoveToHueHue.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToHueHue.TabIndex = 4;
            this.textBoxMoveToHueHue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToHueHue_MouseClick);
            this.textBoxMoveToHueHue.Leave += new System.EventHandler(this.textBoxMoveToHueHue_Leave);
            this.textBoxMoveToHueHue.MouseLeave += new System.EventHandler(this.textBoxMoveToHueHue_MouseLeave);
            this.textBoxMoveToHueHue.MouseHover += new System.EventHandler(this.textBoxMoveToHueHue_MouseHover);
            // 
            // textBoxMoveToHueDstEp
            // 
            this.textBoxMoveToHueDstEp.Location = new System.Drawing.Point(329, 6);
            this.textBoxMoveToHueDstEp.Name = "textBoxMoveToHueDstEp";
            this.textBoxMoveToHueDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToHueDstEp.TabIndex = 3;
            this.textBoxMoveToHueDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToHueDstEp_MouseClick);
            this.textBoxMoveToHueDstEp.Leave += new System.EventHandler(this.textBoxMoveToHueDstEp_Leave);
            this.textBoxMoveToHueDstEp.MouseLeave += new System.EventHandler(this.textBoxMoveToHueDstEp_MouseLeave);
            this.textBoxMoveToHueDstEp.MouseHover += new System.EventHandler(this.textBoxMoveToHueDstEp_MouseHover);
            // 
            // textBoxMoveToHueSrcEp
            // 
            this.textBoxMoveToHueSrcEp.Location = new System.Drawing.Point(215, 6);
            this.textBoxMoveToHueSrcEp.Name = "textBoxMoveToHueSrcEp";
            this.textBoxMoveToHueSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToHueSrcEp.TabIndex = 2;
            this.textBoxMoveToHueSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToHueSrcEp_MouseClick);
            this.textBoxMoveToHueSrcEp.Leave += new System.EventHandler(this.textBoxMoveToHueSrcEp_Leave);
            this.textBoxMoveToHueSrcEp.MouseLeave += new System.EventHandler(this.textBoxMoveToHueSrcEp_MouseLeave);
            this.textBoxMoveToHueSrcEp.MouseHover += new System.EventHandler(this.textBoxMoveToHueSrcEp_MouseHover);
            // 
            // textBoxMoveToHueAddr
            // 
            this.textBoxMoveToHueAddr.Location = new System.Drawing.Point(102, 6);
            this.textBoxMoveToHueAddr.Name = "textBoxMoveToHueAddr";
            this.textBoxMoveToHueAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToHueAddr.TabIndex = 1;
            this.textBoxMoveToHueAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToHueAddr_MouseClick);
            this.textBoxMoveToHueAddr.Leave += new System.EventHandler(this.textBoxMoveToHueAddr_Leave);
            this.textBoxMoveToHueAddr.MouseLeave += new System.EventHandler(this.textBoxMoveToHueAddr_MouseLeave);
            this.textBoxMoveToHueAddr.MouseHover += new System.EventHandler(this.textBoxMoveToHueAddr_MouseHover);
            // 
            // buttonMoveToSat
            // 
            this.buttonMoveToSat.Location = new System.Drawing.Point(4, 62);
            this.buttonMoveToSat.Name = "buttonMoveToSat";
            this.buttonMoveToSat.Size = new System.Drawing.Size(90, 22);
            this.buttonMoveToSat.TabIndex = 14;
            this.buttonMoveToSat.Text = "MoveToSat";
            this.buttonMoveToSat.UseVisualStyleBackColor = true;
            this.buttonMoveToSat.Click += new System.EventHandler(this.buttonMoveToSat_Click);
            // 
            // buttonMoveToColorTemp
            // 
            this.buttonMoveToColorTemp.Location = new System.Drawing.Point(4, 90);
            this.buttonMoveToColorTemp.Name = "buttonMoveToColorTemp";
            this.buttonMoveToColorTemp.Size = new System.Drawing.Size(90, 22);
            this.buttonMoveToColorTemp.TabIndex = 20;
            this.buttonMoveToColorTemp.Text = "MoveToTemp";
            this.buttonMoveToColorTemp.UseVisualStyleBackColor = true;
            this.buttonMoveToColorTemp.Click += new System.EventHandler(this.buttonMoveToColorTemp_Click);
            // 
            // buttonMoveToColor
            // 
            this.buttonMoveToColor.Location = new System.Drawing.Point(4, 33);
            this.buttonMoveToColor.Name = "buttonMoveToColor";
            this.buttonMoveToColor.Size = new System.Drawing.Size(90, 22);
            this.buttonMoveToColor.TabIndex = 7;
            this.buttonMoveToColor.Text = "MoveToColor";
            this.buttonMoveToColor.UseVisualStyleBackColor = true;
            this.buttonMoveToColor.Click += new System.EventHandler(this.buttonMoveToColor_Click);
            // 
            // buttonMoveToHue
            // 
            this.buttonMoveToHue.Location = new System.Drawing.Point(4, 4);
            this.buttonMoveToHue.Name = "buttonMoveToHue";
            this.buttonMoveToHue.Size = new System.Drawing.Size(90, 22);
            this.buttonMoveToHue.TabIndex = 0;
            this.buttonMoveToHue.Text = "MoveToHue";
            this.buttonMoveToHue.UseVisualStyleBackColor = true;
            this.buttonMoveToHue.Click += new System.EventHandler(this.buttonMoveToHue_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage7.Controls.Add(this.checkBoxShowExtension);
            this.tabPage7.Controls.Add(this.textBoxAddSceneData);
            this.tabPage7.Controls.Add(this.textBoxAddSceneExtLen);
            this.tabPage7.Controls.Add(this.textBoxRemoveSceneSceneID);
            this.tabPage7.Controls.Add(this.textBoxRemoveSceneGroupID);
            this.tabPage7.Controls.Add(this.textBoxRemoveSceneDstEndPoint);
            this.tabPage7.Controls.Add(this.textBoxRemoveSceneSrcEndPoint);
            this.tabPage7.Controls.Add(this.textBoxRemoveSceneAddr);
            this.tabPage7.Controls.Add(this.textBoxRemoveAllScenesGroupID);
            this.tabPage7.Controls.Add(this.textBoxRemoveAllScenesDstEndPoint);
            this.tabPage7.Controls.Add(this.textBoxRemoveAllScenesSrcEndPoint);
            this.tabPage7.Controls.Add(this.textBoxRemoveAllScenesAddr);
            this.tabPage7.Controls.Add(this.textBoxGetSceneMembershipGroupID);
            this.tabPage7.Controls.Add(this.textBoxGetSceneMembershipDstEndPoint);
            this.tabPage7.Controls.Add(this.textBoxGetSceneMembershipSrcEndPoint);
            this.tabPage7.Controls.Add(this.textBoxGetSceneMembershipAddr);
            this.tabPage7.Controls.Add(this.textBoxRecallSceneSceneId);
            this.tabPage7.Controls.Add(this.textBoxRecallSceneGroupId);
            this.tabPage7.Controls.Add(this.textBoxRecallSceneDstEndPoint);
            this.tabPage7.Controls.Add(this.textBoxRecallSceneSrcEndPoint);
            this.tabPage7.Controls.Add(this.textBoxRecallSceneAddr);
            this.tabPage7.Controls.Add(this.textBoxStoreSceneSceneId);
            this.tabPage7.Controls.Add(this.textBoxStoreSceneGroupId);
            this.tabPage7.Controls.Add(this.textBoxStoreSceneDstEndPoint);
            this.tabPage7.Controls.Add(this.textBoxStoreSceneSrcEndPoint);
            this.tabPage7.Controls.Add(this.textBoxStoreSceneAddr);
            this.tabPage7.Controls.Add(this.textBoxAddSceneMaxNameLen);
            this.tabPage7.Controls.Add(this.textBoxAddSceneNameLen);
            this.tabPage7.Controls.Add(this.textBoxAddSceneName);
            this.tabPage7.Controls.Add(this.textBoxAddSceneTransTime);
            this.tabPage7.Controls.Add(this.textBoxAddSceneSceneId);
            this.tabPage7.Controls.Add(this.textBoxAddSceneGroupId);
            this.tabPage7.Controls.Add(this.textBoxAddSceneDstEndPoint);
            this.tabPage7.Controls.Add(this.textBoxAddSceneSrcEndPoint);
            this.tabPage7.Controls.Add(this.textBoxAddSceneAddr);
            this.tabPage7.Controls.Add(this.textBoxViewSceneSceneId);
            this.tabPage7.Controls.Add(this.textBoxViewSceneGroupId);
            this.tabPage7.Controls.Add(this.textBoxViewSceneDstEndPoint);
            this.tabPage7.Controls.Add(this.textBoxViewSceneSrcEndPoint);
            this.tabPage7.Controls.Add(this.textBoxViewSceneAddr);
            this.tabPage7.Controls.Add(this.comboBoxRemoveSceneAddrMode);
            this.tabPage7.Controls.Add(this.buttonRemoveScene);
            this.tabPage7.Controls.Add(this.comboBoxRemoveAllScenesAddrMode);
            this.tabPage7.Controls.Add(this.buttonRemoveAllScenes);
            this.tabPage7.Controls.Add(this.comboBoxGetSceneMembershipAddrMode);
            this.tabPage7.Controls.Add(this.buttonGetSceneMembership);
            this.tabPage7.Controls.Add(this.comboBoxRecallSceneAddrMode);
            this.tabPage7.Controls.Add(this.buttonRecallScene);
            this.tabPage7.Controls.Add(this.comboBoxStoreSceneAddrMode);
            this.tabPage7.Controls.Add(this.buttonStoreScene);
            this.tabPage7.Controls.Add(this.comboBoxAddSceneAddrMode);
            this.tabPage7.Controls.Add(this.buttonAddScene);
            this.tabPage7.Controls.Add(this.comboBoxViewSceneAddrMode);
            this.tabPage7.Controls.Add(this.buttonViewScene);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(1833, 584);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Scenes Cluster";
            this.tabPage7.Click += new System.EventHandler(this.tabPage7_Click);
            // 
            // checkBoxShowExtension
            // 
            this.checkBoxShowExtension.AutoSize = true;
            this.checkBoxShowExtension.Location = new System.Drawing.Point(767, 34);
            this.checkBoxShowExtension.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxShowExtension.Name = "checkBoxShowExtension";
            this.checkBoxShowExtension.Size = new System.Drawing.Size(134, 17);
            this.checkBoxShowExtension.TabIndex = 53;
            this.checkBoxShowExtension.Text = "Show extension fields?";
            this.checkBoxShowExtension.UseVisualStyleBackColor = true;
            this.checkBoxShowExtension.CheckedChanged += new System.EventHandler(this.checkBoxShowExtension_CheckedChanged);
            // 
            // textBoxAddSceneData
            // 
            this.textBoxAddSceneData.Location = new System.Drawing.Point(767, 58);
            this.textBoxAddSceneData.Name = "textBoxAddSceneData";
            this.textBoxAddSceneData.Size = new System.Drawing.Size(220, 20);
            this.textBoxAddSceneData.TabIndex = 19;
            this.textBoxAddSceneData.Visible = false;
            this.textBoxAddSceneData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneData_MouseClick);
            this.textBoxAddSceneData.Leave += new System.EventHandler(this.textBoxAddSceneData_Leave);
            this.textBoxAddSceneData.MouseLeave += new System.EventHandler(this.textBoxAddSceneData_MouseLeave);
            this.textBoxAddSceneData.MouseHover += new System.EventHandler(this.textBoxAddSceneData_MouseHover);
            // 
            // textBoxAddSceneExtLen
            // 
            this.textBoxAddSceneExtLen.Location = new System.Drawing.Point(655, 58);
            this.textBoxAddSceneExtLen.Name = "textBoxAddSceneExtLen";
            this.textBoxAddSceneExtLen.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneExtLen.TabIndex = 18;
            this.textBoxAddSceneExtLen.Visible = false;
            this.textBoxAddSceneExtLen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneExtLen_MouseClick);
            this.textBoxAddSceneExtLen.Leave += new System.EventHandler(this.textBoxAddSceneExtLen_Leave);
            this.textBoxAddSceneExtLen.MouseLeave += new System.EventHandler(this.textBoxAddSceneExtLen_MouseLeave);
            this.textBoxAddSceneExtLen.MouseHover += new System.EventHandler(this.textBoxAddSceneExtLen_MouseHover);
            // 
            // textBoxRemoveSceneSceneID
            // 
            this.textBoxRemoveSceneSceneID.Location = new System.Drawing.Point(655, 201);
            this.textBoxRemoveSceneSceneID.Name = "textBoxRemoveSceneSceneID";
            this.textBoxRemoveSceneSceneID.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveSceneSceneID.TabIndex = 52;
            this.textBoxRemoveSceneSceneID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveSceneSceneID_MouseClick);
            this.textBoxRemoveSceneSceneID.Leave += new System.EventHandler(this.textBoxRemoveSceneSceneID_Leave);
            this.textBoxRemoveSceneSceneID.MouseLeave += new System.EventHandler(this.textBoxRemoveSceneSceneID_MouseLeave);
            this.textBoxRemoveSceneSceneID.MouseHover += new System.EventHandler(this.textBoxRemoveSceneSceneID_MouseHover);
            // 
            // textBoxRemoveSceneGroupID
            // 
            this.textBoxRemoveSceneGroupID.Location = new System.Drawing.Point(542, 202);
            this.textBoxRemoveSceneGroupID.Name = "textBoxRemoveSceneGroupID";
            this.textBoxRemoveSceneGroupID.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveSceneGroupID.TabIndex = 51;
            this.textBoxRemoveSceneGroupID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveSceneGroupID_MouseClick);
            this.textBoxRemoveSceneGroupID.Leave += new System.EventHandler(this.textBoxRemoveSceneGroupID_Leave);
            this.textBoxRemoveSceneGroupID.MouseLeave += new System.EventHandler(this.textBoxRemoveSceneGroupID_MouseLeave);
            this.textBoxRemoveSceneGroupID.MouseHover += new System.EventHandler(this.textBoxRemoveSceneGroupID_MouseHover);
            // 
            // textBoxRemoveSceneDstEndPoint
            // 
            this.textBoxRemoveSceneDstEndPoint.Location = new System.Drawing.Point(429, 201);
            this.textBoxRemoveSceneDstEndPoint.Name = "textBoxRemoveSceneDstEndPoint";
            this.textBoxRemoveSceneDstEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveSceneDstEndPoint.TabIndex = 50;
            this.textBoxRemoveSceneDstEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveSceneDstEndPoint_MouseClick);
            this.textBoxRemoveSceneDstEndPoint.Leave += new System.EventHandler(this.textBoxRemoveSceneDstEndPoint_Leave);
            this.textBoxRemoveSceneDstEndPoint.MouseLeave += new System.EventHandler(this.textBoxRemoveSceneDstEndPoint_MouseLeave);
            this.textBoxRemoveSceneDstEndPoint.MouseHover += new System.EventHandler(this.textBoxRemoveSceneDstEndPoint_MouseHover);
            // 
            // textBoxRemoveSceneSrcEndPoint
            // 
            this.textBoxRemoveSceneSrcEndPoint.Location = new System.Drawing.Point(316, 201);
            this.textBoxRemoveSceneSrcEndPoint.Name = "textBoxRemoveSceneSrcEndPoint";
            this.textBoxRemoveSceneSrcEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveSceneSrcEndPoint.TabIndex = 49;
            this.textBoxRemoveSceneSrcEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveSceneSrcEndPoint_MouseClick);
            this.textBoxRemoveSceneSrcEndPoint.Leave += new System.EventHandler(this.textBoxRemoveSceneSrcEndPoint_Leave);
            this.textBoxRemoveSceneSrcEndPoint.MouseLeave += new System.EventHandler(this.textBoxRemoveSceneSrcEndPoint_MouseLeave);
            this.textBoxRemoveSceneSrcEndPoint.MouseHover += new System.EventHandler(this.textBoxRemoveSceneSrcEndPoint_MouseHover);
            // 
            // textBoxRemoveSceneAddr
            // 
            this.textBoxRemoveSceneAddr.Location = new System.Drawing.Point(202, 201);
            this.textBoxRemoveSceneAddr.Name = "textBoxRemoveSceneAddr";
            this.textBoxRemoveSceneAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveSceneAddr.TabIndex = 48;
            this.textBoxRemoveSceneAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveSceneAddr_MouseClick);
            this.textBoxRemoveSceneAddr.Leave += new System.EventHandler(this.textBoxRemoveSceneAddr_Leave);
            this.textBoxRemoveSceneAddr.MouseLeave += new System.EventHandler(this.textBoxRemoveSceneAddr_MouseLeave);
            this.textBoxRemoveSceneAddr.MouseHover += new System.EventHandler(this.textBoxRemoveSceneAddr_MouseHover);
            // 
            // textBoxRemoveAllScenesGroupID
            // 
            this.textBoxRemoveAllScenesGroupID.Location = new System.Drawing.Point(542, 170);
            this.textBoxRemoveAllScenesGroupID.Name = "textBoxRemoveAllScenesGroupID";
            this.textBoxRemoveAllScenesGroupID.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveAllScenesGroupID.TabIndex = 45;
            this.textBoxRemoveAllScenesGroupID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveAllScenesGroupID_MouseClick);
            this.textBoxRemoveAllScenesGroupID.Leave += new System.EventHandler(this.textBoxRemoveAllScenesGroupID_Leave);
            this.textBoxRemoveAllScenesGroupID.MouseLeave += new System.EventHandler(this.textBoxRemoveAllScenesGroupID_MouseLeave);
            this.textBoxRemoveAllScenesGroupID.MouseHover += new System.EventHandler(this.textBoxRemoveAllScenesGroupID_MouseHover);
            // 
            // textBoxRemoveAllScenesDstEndPoint
            // 
            this.textBoxRemoveAllScenesDstEndPoint.Location = new System.Drawing.Point(429, 170);
            this.textBoxRemoveAllScenesDstEndPoint.Name = "textBoxRemoveAllScenesDstEndPoint";
            this.textBoxRemoveAllScenesDstEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveAllScenesDstEndPoint.TabIndex = 44;
            this.textBoxRemoveAllScenesDstEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveAllScenesDstEndPoint_MouseClick);
            this.textBoxRemoveAllScenesDstEndPoint.Leave += new System.EventHandler(this.textBoxRemoveAllScenesDstEndPoint_Leave);
            this.textBoxRemoveAllScenesDstEndPoint.MouseLeave += new System.EventHandler(this.textBoxRemoveAllScenesDstEndPoint_MouseLeave);
            this.textBoxRemoveAllScenesDstEndPoint.MouseHover += new System.EventHandler(this.textBoxRemoveAllScenesDstEndPoint_MouseHover);
            // 
            // textBoxRemoveAllScenesSrcEndPoint
            // 
            this.textBoxRemoveAllScenesSrcEndPoint.Location = new System.Drawing.Point(316, 170);
            this.textBoxRemoveAllScenesSrcEndPoint.Name = "textBoxRemoveAllScenesSrcEndPoint";
            this.textBoxRemoveAllScenesSrcEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveAllScenesSrcEndPoint.TabIndex = 43;
            this.textBoxRemoveAllScenesSrcEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveAllScenesSrcEndPoint_MouseClick);
            this.textBoxRemoveAllScenesSrcEndPoint.Leave += new System.EventHandler(this.textBoxRemoveAllScenesSrcEndPoint_Leave);
            this.textBoxRemoveAllScenesSrcEndPoint.MouseLeave += new System.EventHandler(this.textBoxRemoveAllScenesSrcEndPoint_MouseLeave);
            this.textBoxRemoveAllScenesSrcEndPoint.MouseHover += new System.EventHandler(this.textBoxRemoveAllScenesSrcEndPoint_MouseHover);
            // 
            // textBoxRemoveAllScenesAddr
            // 
            this.textBoxRemoveAllScenesAddr.Location = new System.Drawing.Point(202, 170);
            this.textBoxRemoveAllScenesAddr.Name = "textBoxRemoveAllScenesAddr";
            this.textBoxRemoveAllScenesAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveAllScenesAddr.TabIndex = 42;
            this.textBoxRemoveAllScenesAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveAllScenesAddr_MouseClick);
            this.textBoxRemoveAllScenesAddr.Leave += new System.EventHandler(this.textBoxRemoveAllScenesAddr_Leave);
            this.textBoxRemoveAllScenesAddr.MouseLeave += new System.EventHandler(this.textBoxRemoveAllScenesAddr_MouseLeave);
            this.textBoxRemoveAllScenesAddr.MouseHover += new System.EventHandler(this.textBoxRemoveAllScenesAddr_MouseHover);
            // 
            // textBoxGetSceneMembershipGroupID
            // 
            this.textBoxGetSceneMembershipGroupID.Location = new System.Drawing.Point(542, 141);
            this.textBoxGetSceneMembershipGroupID.Name = "textBoxGetSceneMembershipGroupID";
            this.textBoxGetSceneMembershipGroupID.Size = new System.Drawing.Size(106, 20);
            this.textBoxGetSceneMembershipGroupID.TabIndex = 39;
            this.textBoxGetSceneMembershipGroupID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGetSceneMembershipGroupID_MouseClick);
            this.textBoxGetSceneMembershipGroupID.Leave += new System.EventHandler(this.textBoxGetSceneMembershipGroupID_Leave);
            this.textBoxGetSceneMembershipGroupID.MouseLeave += new System.EventHandler(this.textBoxGetSceneMembershipGroupID_MouseLeave);
            this.textBoxGetSceneMembershipGroupID.MouseHover += new System.EventHandler(this.textBoxGetSceneMembershipGroupID_MouseHover);
            // 
            // textBoxGetSceneMembershipDstEndPoint
            // 
            this.textBoxGetSceneMembershipDstEndPoint.Location = new System.Drawing.Point(429, 141);
            this.textBoxGetSceneMembershipDstEndPoint.Name = "textBoxGetSceneMembershipDstEndPoint";
            this.textBoxGetSceneMembershipDstEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxGetSceneMembershipDstEndPoint.TabIndex = 38;
            this.textBoxGetSceneMembershipDstEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGetSceneMembershipDstEndPoint_MouseClick);
            this.textBoxGetSceneMembershipDstEndPoint.Leave += new System.EventHandler(this.textBoxGetSceneMembershipDstEndPoint_Leave);
            this.textBoxGetSceneMembershipDstEndPoint.MouseLeave += new System.EventHandler(this.textBoxGetSceneMembershipDstEndPoint_MouseLeave);
            this.textBoxGetSceneMembershipDstEndPoint.MouseHover += new System.EventHandler(this.textBoxGetSceneMembershipDstEndPoint_MouseHover);
            // 
            // textBoxGetSceneMembershipSrcEndPoint
            // 
            this.textBoxGetSceneMembershipSrcEndPoint.Location = new System.Drawing.Point(316, 141);
            this.textBoxGetSceneMembershipSrcEndPoint.Name = "textBoxGetSceneMembershipSrcEndPoint";
            this.textBoxGetSceneMembershipSrcEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxGetSceneMembershipSrcEndPoint.TabIndex = 37;
            this.textBoxGetSceneMembershipSrcEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGetSceneMembershipSrcEndPoint_MouseClick);
            this.textBoxGetSceneMembershipSrcEndPoint.Leave += new System.EventHandler(this.textBoxGetSceneMembershipSrcEndPoint_Leave);
            this.textBoxGetSceneMembershipSrcEndPoint.MouseLeave += new System.EventHandler(this.textBoxGetSceneMembershipSrcEndPoint_MouseLeave);
            this.textBoxGetSceneMembershipSrcEndPoint.MouseHover += new System.EventHandler(this.textBoxGetSceneMembershipSrcEndPoint_MouseHover);
            // 
            // textBoxGetSceneMembershipAddr
            // 
            this.textBoxGetSceneMembershipAddr.Location = new System.Drawing.Point(202, 141);
            this.textBoxGetSceneMembershipAddr.Name = "textBoxGetSceneMembershipAddr";
            this.textBoxGetSceneMembershipAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxGetSceneMembershipAddr.TabIndex = 36;
            this.textBoxGetSceneMembershipAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGetSceneMembershipAddr_MouseClick);
            this.textBoxGetSceneMembershipAddr.Leave += new System.EventHandler(this.textBoxGetSceneMembershipAddr_Leave);
            this.textBoxGetSceneMembershipAddr.MouseLeave += new System.EventHandler(this.textBoxGetSceneMembershipAddr_MouseLeave);
            this.textBoxGetSceneMembershipAddr.MouseHover += new System.EventHandler(this.textBoxGetSceneMembershipAddr_MouseHover);
            // 
            // textBoxRecallSceneSceneId
            // 
            this.textBoxRecallSceneSceneId.Location = new System.Drawing.Point(655, 112);
            this.textBoxRecallSceneSceneId.Name = "textBoxRecallSceneSceneId";
            this.textBoxRecallSceneSceneId.Size = new System.Drawing.Size(106, 20);
            this.textBoxRecallSceneSceneId.TabIndex = 33;
            this.textBoxRecallSceneSceneId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRecallSceneSceneId_MouseClick);
            this.textBoxRecallSceneSceneId.Leave += new System.EventHandler(this.textBoxRecallSceneSceneId_Leave);
            this.textBoxRecallSceneSceneId.MouseLeave += new System.EventHandler(this.textBoxRecallSceneSceneId_MouseLeave);
            this.textBoxRecallSceneSceneId.MouseHover += new System.EventHandler(this.textBoxRecallSceneSceneId_MouseHover);
            // 
            // textBoxRecallSceneGroupId
            // 
            this.textBoxRecallSceneGroupId.Location = new System.Drawing.Point(542, 114);
            this.textBoxRecallSceneGroupId.Name = "textBoxRecallSceneGroupId";
            this.textBoxRecallSceneGroupId.Size = new System.Drawing.Size(106, 20);
            this.textBoxRecallSceneGroupId.TabIndex = 32;
            this.textBoxRecallSceneGroupId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRecallSceneGroupId_MouseClick);
            this.textBoxRecallSceneGroupId.Leave += new System.EventHandler(this.textBoxRecallSceneGroupId_Leave);
            this.textBoxRecallSceneGroupId.MouseLeave += new System.EventHandler(this.textBoxRecallSceneGroupId_MouseLeave);
            this.textBoxRecallSceneGroupId.MouseHover += new System.EventHandler(this.textBoxRecallSceneGroupId_MouseHover);
            // 
            // textBoxRecallSceneDstEndPoint
            // 
            this.textBoxRecallSceneDstEndPoint.Location = new System.Drawing.Point(429, 112);
            this.textBoxRecallSceneDstEndPoint.Name = "textBoxRecallSceneDstEndPoint";
            this.textBoxRecallSceneDstEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxRecallSceneDstEndPoint.TabIndex = 31;
            this.textBoxRecallSceneDstEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRecallSceneDstEndPoint_MouseClick);
            this.textBoxRecallSceneDstEndPoint.Leave += new System.EventHandler(this.textBoxRecallSceneDstEndPoint_Leave);
            this.textBoxRecallSceneDstEndPoint.MouseLeave += new System.EventHandler(this.textBoxRecallSceneDstEndPoint_MouseLeave);
            this.textBoxRecallSceneDstEndPoint.MouseHover += new System.EventHandler(this.textBoxRecallSceneDstEndPoint_MouseHover);
            // 
            // textBoxRecallSceneSrcEndPoint
            // 
            this.textBoxRecallSceneSrcEndPoint.Location = new System.Drawing.Point(316, 112);
            this.textBoxRecallSceneSrcEndPoint.Name = "textBoxRecallSceneSrcEndPoint";
            this.textBoxRecallSceneSrcEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxRecallSceneSrcEndPoint.TabIndex = 30;
            this.textBoxRecallSceneSrcEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRecallSceneSrcEndPoint_MouseClick);
            this.textBoxRecallSceneSrcEndPoint.Leave += new System.EventHandler(this.textBoxRecallSceneSrcEndPoint_Leave);
            this.textBoxRecallSceneSrcEndPoint.MouseLeave += new System.EventHandler(this.textBoxRecallSceneSrcEndPoint_MouseLeave);
            this.textBoxRecallSceneSrcEndPoint.MouseHover += new System.EventHandler(this.textBoxRecallSceneSrcEndPoint_MouseHover);
            // 
            // textBoxRecallSceneAddr
            // 
            this.textBoxRecallSceneAddr.Location = new System.Drawing.Point(202, 112);
            this.textBoxRecallSceneAddr.Name = "textBoxRecallSceneAddr";
            this.textBoxRecallSceneAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxRecallSceneAddr.TabIndex = 29;
            this.textBoxRecallSceneAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRecallSceneAddr_MouseClick);
            this.textBoxRecallSceneAddr.Leave += new System.EventHandler(this.textBoxRecallSceneAddr_Leave);
            this.textBoxRecallSceneAddr.MouseLeave += new System.EventHandler(this.textBoxRecallSceneAddr_MouseLeave);
            this.textBoxRecallSceneAddr.MouseHover += new System.EventHandler(this.textBoxRecallSceneAddr_MouseHover);
            // 
            // textBoxStoreSceneSceneId
            // 
            this.textBoxStoreSceneSceneId.Location = new System.Drawing.Point(655, 84);
            this.textBoxStoreSceneSceneId.Name = "textBoxStoreSceneSceneId";
            this.textBoxStoreSceneSceneId.Size = new System.Drawing.Size(106, 20);
            this.textBoxStoreSceneSceneId.TabIndex = 26;
            this.textBoxStoreSceneSceneId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxStoreSceneSceneId_MouseClick);
            this.textBoxStoreSceneSceneId.Leave += new System.EventHandler(this.textBoxStoreSceneSceneId_Leave);
            this.textBoxStoreSceneSceneId.MouseLeave += new System.EventHandler(this.textBoxStoreSceneSceneId_MouseLeave);
            this.textBoxStoreSceneSceneId.MouseHover += new System.EventHandler(this.textBoxStoreSceneSceneId_MouseHover);
            // 
            // textBoxStoreSceneGroupId
            // 
            this.textBoxStoreSceneGroupId.Location = new System.Drawing.Point(542, 84);
            this.textBoxStoreSceneGroupId.Name = "textBoxStoreSceneGroupId";
            this.textBoxStoreSceneGroupId.Size = new System.Drawing.Size(106, 20);
            this.textBoxStoreSceneGroupId.TabIndex = 25;
            this.textBoxStoreSceneGroupId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxStoreSceneGroupId_MouseClick);
            this.textBoxStoreSceneGroupId.Leave += new System.EventHandler(this.textBoxStoreSceneGroupId_Leave);
            this.textBoxStoreSceneGroupId.MouseLeave += new System.EventHandler(this.textBoxStoreSceneGroupId_MouseLeave);
            this.textBoxStoreSceneGroupId.MouseHover += new System.EventHandler(this.textBoxStoreSceneGroupId_MouseHover);
            // 
            // textBoxStoreSceneDstEndPoint
            // 
            this.textBoxStoreSceneDstEndPoint.Location = new System.Drawing.Point(429, 84);
            this.textBoxStoreSceneDstEndPoint.Name = "textBoxStoreSceneDstEndPoint";
            this.textBoxStoreSceneDstEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxStoreSceneDstEndPoint.TabIndex = 24;
            this.textBoxStoreSceneDstEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxStoreSceneDstEndPoint_MouseClick);
            this.textBoxStoreSceneDstEndPoint.Leave += new System.EventHandler(this.textBoxStoreSceneDstEndPoint_Leave);
            this.textBoxStoreSceneDstEndPoint.MouseLeave += new System.EventHandler(this.textBoxStoreSceneDstEndPoint_MouseLeave);
            this.textBoxStoreSceneDstEndPoint.MouseHover += new System.EventHandler(this.textBoxStoreSceneDstEndPoint_MouseHover);
            // 
            // textBoxStoreSceneSrcEndPoint
            // 
            this.textBoxStoreSceneSrcEndPoint.Location = new System.Drawing.Point(316, 84);
            this.textBoxStoreSceneSrcEndPoint.Name = "textBoxStoreSceneSrcEndPoint";
            this.textBoxStoreSceneSrcEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxStoreSceneSrcEndPoint.TabIndex = 23;
            this.textBoxStoreSceneSrcEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxStoreSceneSrcEndPoint_MouseClick);
            this.textBoxStoreSceneSrcEndPoint.Leave += new System.EventHandler(this.textBoxStoreSceneSrcEndPoint_Leave);
            this.textBoxStoreSceneSrcEndPoint.MouseLeave += new System.EventHandler(this.textBoxStoreSceneSrcEndPoint_MouseLeave);
            this.textBoxStoreSceneSrcEndPoint.MouseHover += new System.EventHandler(this.textBoxStoreSceneSrcEndPoint_MouseHover);
            // 
            // textBoxStoreSceneAddr
            // 
            this.textBoxStoreSceneAddr.Location = new System.Drawing.Point(202, 84);
            this.textBoxStoreSceneAddr.Name = "textBoxStoreSceneAddr";
            this.textBoxStoreSceneAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxStoreSceneAddr.TabIndex = 22;
            this.textBoxStoreSceneAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxStoreSceneAddr_MouseClick);
            this.textBoxStoreSceneAddr.Leave += new System.EventHandler(this.textBoxStoreSceneAddr_Leave);
            this.textBoxStoreSceneAddr.MouseLeave += new System.EventHandler(this.textBoxStoreSceneAddr_MouseLeave);
            this.textBoxStoreSceneAddr.MouseHover += new System.EventHandler(this.textBoxStoreSceneAddr_MouseHover);
            // 
            // textBoxAddSceneMaxNameLen
            // 
            this.textBoxAddSceneMaxNameLen.Location = new System.Drawing.Point(542, 58);
            this.textBoxAddSceneMaxNameLen.Name = "textBoxAddSceneMaxNameLen";
            this.textBoxAddSceneMaxNameLen.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneMaxNameLen.TabIndex = 17;
            this.textBoxAddSceneMaxNameLen.Visible = false;
            this.textBoxAddSceneMaxNameLen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneMaxNameLen_MouseClick);
            this.textBoxAddSceneMaxNameLen.Leave += new System.EventHandler(this.textBoxAddSceneMaxNameLen_Leave);
            this.textBoxAddSceneMaxNameLen.MouseLeave += new System.EventHandler(this.textBoxAddSceneMaxNameLen_MouseLeave);
            this.textBoxAddSceneMaxNameLen.MouseHover += new System.EventHandler(this.textBoxAddSceneMaxNameLen_MouseHover);
            // 
            // textBoxAddSceneNameLen
            // 
            this.textBoxAddSceneNameLen.Location = new System.Drawing.Point(429, 58);
            this.textBoxAddSceneNameLen.Name = "textBoxAddSceneNameLen";
            this.textBoxAddSceneNameLen.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneNameLen.TabIndex = 16;
            this.textBoxAddSceneNameLen.Visible = false;
            this.textBoxAddSceneNameLen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneNameLen_MouseClick);
            this.textBoxAddSceneNameLen.Leave += new System.EventHandler(this.textBoxAddSceneNameLen_Leave);
            this.textBoxAddSceneNameLen.MouseLeave += new System.EventHandler(this.textBoxAddSceneNameLen_MouseLeave);
            this.textBoxAddSceneNameLen.MouseHover += new System.EventHandler(this.textBoxAddSceneNameLen_MouseHover);
            // 
            // textBoxAddSceneName
            // 
            this.textBoxAddSceneName.Location = new System.Drawing.Point(316, 58);
            this.textBoxAddSceneName.Name = "textBoxAddSceneName";
            this.textBoxAddSceneName.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneName.TabIndex = 15;
            this.textBoxAddSceneName.Visible = false;
            this.textBoxAddSceneName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneName_MouseClick);
            this.textBoxAddSceneName.Leave += new System.EventHandler(this.textBoxAddSceneName_Leave);
            this.textBoxAddSceneName.MouseLeave += new System.EventHandler(this.textBoxAddSceneName_MouseLeave);
            this.textBoxAddSceneName.MouseHover += new System.EventHandler(this.textBoxAddSceneName_MouseHover);
            // 
            // textBoxAddSceneTransTime
            // 
            this.textBoxAddSceneTransTime.Location = new System.Drawing.Point(202, 58);
            this.textBoxAddSceneTransTime.Name = "textBoxAddSceneTransTime";
            this.textBoxAddSceneTransTime.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneTransTime.TabIndex = 14;
            this.textBoxAddSceneTransTime.Visible = false;
            this.textBoxAddSceneTransTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneTransTime_MouseClick);
            this.textBoxAddSceneTransTime.Leave += new System.EventHandler(this.textBoxAddSceneTransTime_Leave);
            this.textBoxAddSceneTransTime.MouseLeave += new System.EventHandler(this.textBoxAddSceneTransTime_MouseLeave);
            this.textBoxAddSceneTransTime.MouseHover += new System.EventHandler(this.textBoxAddSceneTransTime_MouseHover);
            // 
            // textBoxAddSceneSceneId
            // 
            this.textBoxAddSceneSceneId.Location = new System.Drawing.Point(655, 34);
            this.textBoxAddSceneSceneId.Name = "textBoxAddSceneSceneId";
            this.textBoxAddSceneSceneId.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneSceneId.TabIndex = 13;
            this.textBoxAddSceneSceneId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneSceneId_MouseClick);
            this.textBoxAddSceneSceneId.Leave += new System.EventHandler(this.textBoxAddSceneSceneId_Leave);
            this.textBoxAddSceneSceneId.MouseLeave += new System.EventHandler(this.textBoxAddSceneSceneId_MouseLeave);
            this.textBoxAddSceneSceneId.MouseHover += new System.EventHandler(this.textBoxAddSceneSceneId_MouseHover);
            // 
            // textBoxAddSceneGroupId
            // 
            this.textBoxAddSceneGroupId.Location = new System.Drawing.Point(542, 34);
            this.textBoxAddSceneGroupId.Name = "textBoxAddSceneGroupId";
            this.textBoxAddSceneGroupId.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneGroupId.TabIndex = 12;
            this.textBoxAddSceneGroupId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneGroupId_MouseClick);
            this.textBoxAddSceneGroupId.Leave += new System.EventHandler(this.textBoxAddSceneGroupId_Leave);
            this.textBoxAddSceneGroupId.MouseLeave += new System.EventHandler(this.textBoxAddSceneGroupId_MouseLeave);
            this.textBoxAddSceneGroupId.MouseHover += new System.EventHandler(this.textBoxAddSceneGroupId_MouseHover);
            // 
            // textBoxAddSceneDstEndPoint
            // 
            this.textBoxAddSceneDstEndPoint.Location = new System.Drawing.Point(429, 34);
            this.textBoxAddSceneDstEndPoint.Name = "textBoxAddSceneDstEndPoint";
            this.textBoxAddSceneDstEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneDstEndPoint.TabIndex = 11;
            this.textBoxAddSceneDstEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneDstEndPoint_MouseClick);
            this.textBoxAddSceneDstEndPoint.Leave += new System.EventHandler(this.textBoxAddSceneDstEndPoint_Leave);
            this.textBoxAddSceneDstEndPoint.MouseLeave += new System.EventHandler(this.textBoxAddSceneDstEndPoint_MouseLeave);
            this.textBoxAddSceneDstEndPoint.MouseHover += new System.EventHandler(this.textBoxAddSceneDstEndPoint_MouseHover);
            // 
            // textBoxAddSceneSrcEndPoint
            // 
            this.textBoxAddSceneSrcEndPoint.Location = new System.Drawing.Point(316, 34);
            this.textBoxAddSceneSrcEndPoint.Name = "textBoxAddSceneSrcEndPoint";
            this.textBoxAddSceneSrcEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneSrcEndPoint.TabIndex = 10;
            this.textBoxAddSceneSrcEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneSrcEndPoint_MouseClick);
            this.textBoxAddSceneSrcEndPoint.Leave += new System.EventHandler(this.textBoxAddSceneSrcEndPoint_Leave);
            this.textBoxAddSceneSrcEndPoint.MouseLeave += new System.EventHandler(this.textBoxAddSceneSrcEndPoint_MouseLeave);
            this.textBoxAddSceneSrcEndPoint.MouseHover += new System.EventHandler(this.textBoxAddSceneSrcEndPoint_MouseHover);
            // 
            // textBoxAddSceneAddr
            // 
            this.textBoxAddSceneAddr.Location = new System.Drawing.Point(202, 34);
            this.textBoxAddSceneAddr.Name = "textBoxAddSceneAddr";
            this.textBoxAddSceneAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddSceneAddr.TabIndex = 9;
            this.textBoxAddSceneAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddSceneAddr_MouseClick);
            this.textBoxAddSceneAddr.Leave += new System.EventHandler(this.textBoxAddSceneAddr_Leave);
            this.textBoxAddSceneAddr.MouseLeave += new System.EventHandler(this.textBoxAddSceneAddr_MouseLeave);
            this.textBoxAddSceneAddr.MouseHover += new System.EventHandler(this.textBoxAddSceneAddr_MouseHover);
            // 
            // textBoxViewSceneSceneId
            // 
            this.textBoxViewSceneSceneId.Location = new System.Drawing.Point(655, 5);
            this.textBoxViewSceneSceneId.Name = "textBoxViewSceneSceneId";
            this.textBoxViewSceneSceneId.Size = new System.Drawing.Size(106, 20);
            this.textBoxViewSceneSceneId.TabIndex = 6;
            this.textBoxViewSceneSceneId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxViewSceneSceneId_MouseClick);
            this.textBoxViewSceneSceneId.Leave += new System.EventHandler(this.textBoxViewSceneSceneId_Leave);
            this.textBoxViewSceneSceneId.MouseLeave += new System.EventHandler(this.textBoxViewSceneSceneId_MouseLeave);
            this.textBoxViewSceneSceneId.MouseHover += new System.EventHandler(this.textBoxViewSceneSceneId_MouseHover);
            // 
            // textBoxViewSceneGroupId
            // 
            this.textBoxViewSceneGroupId.Location = new System.Drawing.Point(542, 5);
            this.textBoxViewSceneGroupId.Name = "textBoxViewSceneGroupId";
            this.textBoxViewSceneGroupId.Size = new System.Drawing.Size(106, 20);
            this.textBoxViewSceneGroupId.TabIndex = 5;
            this.textBoxViewSceneGroupId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxViewSceneGroupId_MouseClick);
            this.textBoxViewSceneGroupId.Leave += new System.EventHandler(this.textBoxViewSceneGroupId_Leave);
            this.textBoxViewSceneGroupId.MouseLeave += new System.EventHandler(this.textBoxViewSceneGroupId_MouseLeave);
            this.textBoxViewSceneGroupId.MouseHover += new System.EventHandler(this.textBoxViewSceneGroupId_MouseHover);
            // 
            // textBoxViewSceneDstEndPoint
            // 
            this.textBoxViewSceneDstEndPoint.Location = new System.Drawing.Point(429, 5);
            this.textBoxViewSceneDstEndPoint.Name = "textBoxViewSceneDstEndPoint";
            this.textBoxViewSceneDstEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxViewSceneDstEndPoint.TabIndex = 4;
            this.textBoxViewSceneDstEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxViewSceneDstEndPoint_MouseClick);
            this.textBoxViewSceneDstEndPoint.Leave += new System.EventHandler(this.textBoxViewSceneDstEndPoint_Leave);
            this.textBoxViewSceneDstEndPoint.MouseLeave += new System.EventHandler(this.textBoxViewSceneDstEndPoint_MouseLeave);
            this.textBoxViewSceneDstEndPoint.MouseHover += new System.EventHandler(this.textBoxViewSceneDstEndPoint_MouseHover);
            // 
            // textBoxViewSceneSrcEndPoint
            // 
            this.textBoxViewSceneSrcEndPoint.Location = new System.Drawing.Point(316, 6);
            this.textBoxViewSceneSrcEndPoint.Name = "textBoxViewSceneSrcEndPoint";
            this.textBoxViewSceneSrcEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxViewSceneSrcEndPoint.TabIndex = 3;
            this.textBoxViewSceneSrcEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxViewSceneSrcEndPoint_MouseClick);
            this.textBoxViewSceneSrcEndPoint.Leave += new System.EventHandler(this.textBoxViewSceneSrcEndPoint_Leave);
            this.textBoxViewSceneSrcEndPoint.MouseLeave += new System.EventHandler(this.textBoxViewSceneSrcEndPoint_MouseLeave);
            this.textBoxViewSceneSrcEndPoint.MouseHover += new System.EventHandler(this.textBoxViewSceneSrcEndPoint_MouseHover);
            // 
            // textBoxViewSceneAddr
            // 
            this.textBoxViewSceneAddr.Location = new System.Drawing.Point(202, 5);
            this.textBoxViewSceneAddr.Name = "textBoxViewSceneAddr";
            this.textBoxViewSceneAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxViewSceneAddr.TabIndex = 2;
            this.textBoxViewSceneAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxViewSceneAddr_MouseClick);
            this.textBoxViewSceneAddr.Leave += new System.EventHandler(this.textBoxViewSceneAddr_Leave);
            this.textBoxViewSceneAddr.MouseLeave += new System.EventHandler(this.textBoxViewSceneAddr_MouseLeave);
            this.textBoxViewSceneAddr.MouseHover += new System.EventHandler(this.textBoxViewSceneAddr_MouseHover);
            // 
            // comboBoxRemoveSceneAddrMode
            // 
            this.comboBoxRemoveSceneAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRemoveSceneAddrMode.FormattingEnabled = true;
            this.comboBoxRemoveSceneAddrMode.Location = new System.Drawing.Point(90, 201);
            this.comboBoxRemoveSceneAddrMode.Name = "comboBoxRemoveSceneAddrMode";
            this.comboBoxRemoveSceneAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxRemoveSceneAddrMode.TabIndex = 47;
            this.comboBoxRemoveSceneAddrMode.MouseLeave += new System.EventHandler(this.comboBoxRemoveSceneAddrMode_MouseLeave);
            this.comboBoxRemoveSceneAddrMode.MouseHover += new System.EventHandler(this.comboBoxRemoveSceneAddrMode_MouseHover);
            // 
            // buttonRemoveScene
            // 
            this.buttonRemoveScene.Location = new System.Drawing.Point(3, 198);
            this.buttonRemoveScene.Name = "buttonRemoveScene";
            this.buttonRemoveScene.Size = new System.Drawing.Size(80, 25);
            this.buttonRemoveScene.TabIndex = 46;
            this.buttonRemoveScene.Text = "Remove";
            this.buttonRemoveScene.UseVisualStyleBackColor = true;
            this.buttonRemoveScene.Click += new System.EventHandler(this.buttonRemoveScene_Click);
            // 
            // comboBoxRemoveAllScenesAddrMode
            // 
            this.comboBoxRemoveAllScenesAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRemoveAllScenesAddrMode.FormattingEnabled = true;
            this.comboBoxRemoveAllScenesAddrMode.Location = new System.Drawing.Point(90, 170);
            this.comboBoxRemoveAllScenesAddrMode.Name = "comboBoxRemoveAllScenesAddrMode";
            this.comboBoxRemoveAllScenesAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxRemoveAllScenesAddrMode.TabIndex = 41;
            this.comboBoxRemoveAllScenesAddrMode.MouseLeave += new System.EventHandler(this.comboBoxRemoveAllScenesAddrMode_MouseLeave);
            this.comboBoxRemoveAllScenesAddrMode.MouseHover += new System.EventHandler(this.comboBoxRemoveAllScenesAddrMode_MouseHover);
            // 
            // buttonRemoveAllScenes
            // 
            this.buttonRemoveAllScenes.Location = new System.Drawing.Point(3, 167);
            this.buttonRemoveAllScenes.Name = "buttonRemoveAllScenes";
            this.buttonRemoveAllScenes.Size = new System.Drawing.Size(80, 25);
            this.buttonRemoveAllScenes.TabIndex = 40;
            this.buttonRemoveAllScenes.Text = "Remove All";
            this.buttonRemoveAllScenes.UseVisualStyleBackColor = true;
            this.buttonRemoveAllScenes.Click += new System.EventHandler(this.buttonRemoveAllScenes_Click);
            // 
            // comboBoxGetSceneMembershipAddrMode
            // 
            this.comboBoxGetSceneMembershipAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGetSceneMembershipAddrMode.FormattingEnabled = true;
            this.comboBoxGetSceneMembershipAddrMode.Location = new System.Drawing.Point(90, 141);
            this.comboBoxGetSceneMembershipAddrMode.Name = "comboBoxGetSceneMembershipAddrMode";
            this.comboBoxGetSceneMembershipAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxGetSceneMembershipAddrMode.TabIndex = 35;
            this.comboBoxGetSceneMembershipAddrMode.MouseLeave += new System.EventHandler(this.comboBoxGetSceneMembershipAddrMode_MouseLeave);
            this.comboBoxGetSceneMembershipAddrMode.MouseHover += new System.EventHandler(this.comboBoxGetSceneMembershipAddrMode_MouseHover);
            // 
            // buttonGetSceneMembership
            // 
            this.buttonGetSceneMembership.Location = new System.Drawing.Point(3, 139);
            this.buttonGetSceneMembership.Name = "buttonGetSceneMembership";
            this.buttonGetSceneMembership.Size = new System.Drawing.Size(80, 22);
            this.buttonGetSceneMembership.TabIndex = 34;
            this.buttonGetSceneMembership.Text = "Get Memb";
            this.buttonGetSceneMembership.UseVisualStyleBackColor = true;
            this.buttonGetSceneMembership.Click += new System.EventHandler(this.buttonGetSceneMembership_Click);
            // 
            // comboBoxRecallSceneAddrMode
            // 
            this.comboBoxRecallSceneAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRecallSceneAddrMode.FormattingEnabled = true;
            this.comboBoxRecallSceneAddrMode.Location = new System.Drawing.Point(90, 112);
            this.comboBoxRecallSceneAddrMode.Name = "comboBoxRecallSceneAddrMode";
            this.comboBoxRecallSceneAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxRecallSceneAddrMode.TabIndex = 28;
            this.comboBoxRecallSceneAddrMode.MouseLeave += new System.EventHandler(this.comboBoxRecallSceneAddrMode_MouseLeave);
            this.comboBoxRecallSceneAddrMode.MouseHover += new System.EventHandler(this.comboBoxRecallSceneAddrMode_MouseHover);
            // 
            // buttonRecallScene
            // 
            this.buttonRecallScene.Location = new System.Drawing.Point(3, 110);
            this.buttonRecallScene.Name = "buttonRecallScene";
            this.buttonRecallScene.Size = new System.Drawing.Size(80, 22);
            this.buttonRecallScene.TabIndex = 27;
            this.buttonRecallScene.Text = "Recall Scn";
            this.buttonRecallScene.UseVisualStyleBackColor = true;
            this.buttonRecallScene.Click += new System.EventHandler(this.buttonRecallScene_Click_1);
            // 
            // comboBoxStoreSceneAddrMode
            // 
            this.comboBoxStoreSceneAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStoreSceneAddrMode.FormattingEnabled = true;
            this.comboBoxStoreSceneAddrMode.Location = new System.Drawing.Point(90, 84);
            this.comboBoxStoreSceneAddrMode.Name = "comboBoxStoreSceneAddrMode";
            this.comboBoxStoreSceneAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxStoreSceneAddrMode.TabIndex = 21;
            this.comboBoxStoreSceneAddrMode.MouseLeave += new System.EventHandler(this.comboBoxStoreSceneAddrMode_MouseLeave);
            this.comboBoxStoreSceneAddrMode.MouseHover += new System.EventHandler(this.comboBoxStoreSceneAddrMode_MouseHover);
            // 
            // buttonStoreScene
            // 
            this.buttonStoreScene.Location = new System.Drawing.Point(3, 82);
            this.buttonStoreScene.Name = "buttonStoreScene";
            this.buttonStoreScene.Size = new System.Drawing.Size(80, 22);
            this.buttonStoreScene.TabIndex = 20;
            this.buttonStoreScene.Text = "Store Scene";
            this.buttonStoreScene.UseVisualStyleBackColor = true;
            this.buttonStoreScene.Click += new System.EventHandler(this.buttonStoreScene_Click);
            // 
            // comboBoxAddSceneAddrMode
            // 
            this.comboBoxAddSceneAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAddSceneAddrMode.FormattingEnabled = true;
            this.comboBoxAddSceneAddrMode.Location = new System.Drawing.Point(90, 34);
            this.comboBoxAddSceneAddrMode.Name = "comboBoxAddSceneAddrMode";
            this.comboBoxAddSceneAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxAddSceneAddrMode.TabIndex = 8;
            this.comboBoxAddSceneAddrMode.MouseLeave += new System.EventHandler(this.comboBoxAddSceneAddrMode_MouseLeave);
            this.comboBoxAddSceneAddrMode.MouseHover += new System.EventHandler(this.comboBoxAddSceneAddrMode_MouseHover);
            // 
            // buttonAddScene
            // 
            this.buttonAddScene.Location = new System.Drawing.Point(3, 31);
            this.buttonAddScene.Name = "buttonAddScene";
            this.buttonAddScene.Size = new System.Drawing.Size(80, 22);
            this.buttonAddScene.TabIndex = 7;
            this.buttonAddScene.Text = "Add Scene";
            this.buttonAddScene.UseVisualStyleBackColor = true;
            this.buttonAddScene.Click += new System.EventHandler(this.buttonAddScene_Click);
            // 
            // comboBoxViewSceneAddrMode
            // 
            this.comboBoxViewSceneAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViewSceneAddrMode.FormattingEnabled = true;
            this.comboBoxViewSceneAddrMode.Location = new System.Drawing.Point(90, 5);
            this.comboBoxViewSceneAddrMode.Name = "comboBoxViewSceneAddrMode";
            this.comboBoxViewSceneAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxViewSceneAddrMode.TabIndex = 1;
            this.comboBoxViewSceneAddrMode.MouseLeave += new System.EventHandler(this.comboBoxViewSceneAddrMode_MouseLeave);
            this.comboBoxViewSceneAddrMode.MouseHover += new System.EventHandler(this.comboBoxViewSceneAddrMode_MouseHover);
            // 
            // buttonViewScene
            // 
            this.buttonViewScene.Location = new System.Drawing.Point(3, 3);
            this.buttonViewScene.Name = "buttonViewScene";
            this.buttonViewScene.Size = new System.Drawing.Size(80, 22);
            this.buttonViewScene.TabIndex = 0;
            this.buttonViewScene.Text = "View Scene";
            this.buttonViewScene.UseVisualStyleBackColor = true;
            this.buttonViewScene.Click += new System.EventHandler(this.buttonViewScene_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage4.Controls.Add(this.comboBoxOnOffAddrMode);
            this.tabPage4.Controls.Add(this.comboBoxOnOffCommand);
            this.tabPage4.Controls.Add(this.textBoxOnOffDstEndPoint);
            this.tabPage4.Controls.Add(this.textBoxOnOffSrcEndPoint);
            this.tabPage4.Controls.Add(this.textBoxOnOffAddr);
            this.tabPage4.Controls.Add(this.buttonOnOff);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1833, 584);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "On/Off Cluster";
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
            // 
            // comboBoxOnOffAddrMode
            // 
            this.comboBoxOnOffAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOnOffAddrMode.FormattingEnabled = true;
            this.comboBoxOnOffAddrMode.Location = new System.Drawing.Point(90, 5);
            this.comboBoxOnOffAddrMode.Name = "comboBoxOnOffAddrMode";
            this.comboBoxOnOffAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxOnOffAddrMode.TabIndex = 1;
            this.comboBoxOnOffAddrMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxOnOffAddrMode_SelectedIndexChanged);
            this.comboBoxOnOffAddrMode.MouseLeave += new System.EventHandler(this.comboBoxOnOffAddrMode_MouseLeave);
            this.comboBoxOnOffAddrMode.MouseHover += new System.EventHandler(this.comboBoxOnOffAddrMode_MouseHover);
            // 
            // comboBoxOnOffCommand
            // 
            this.comboBoxOnOffCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOnOffCommand.FormattingEnabled = true;
            this.comboBoxOnOffCommand.Location = new System.Drawing.Point(543, 5);
            this.comboBoxOnOffCommand.Name = "comboBoxOnOffCommand";
            this.comboBoxOnOffCommand.Size = new System.Drawing.Size(129, 21);
            this.comboBoxOnOffCommand.TabIndex = 5;
            this.comboBoxOnOffCommand.MouseLeave += new System.EventHandler(this.comboBoxOnOffCommand_MouseLeave);
            this.comboBoxOnOffCommand.MouseHover += new System.EventHandler(this.comboBoxOnOffCommand_MouseHover);
            // 
            // textBoxOnOffDstEndPoint
            // 
            this.textBoxOnOffDstEndPoint.Location = new System.Drawing.Point(430, 5);
            this.textBoxOnOffDstEndPoint.Name = "textBoxOnOffDstEndPoint";
            this.textBoxOnOffDstEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxOnOffDstEndPoint.TabIndex = 4;
            this.textBoxOnOffDstEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOnOffDstEndPoint_MouseClick);
            this.textBoxOnOffDstEndPoint.Leave += new System.EventHandler(this.textBoxOnOffDstEndPoint_Leave);
            this.textBoxOnOffDstEndPoint.MouseLeave += new System.EventHandler(this.textBoxOnOffDstEndPoint_MouseLeave);
            this.textBoxOnOffDstEndPoint.MouseHover += new System.EventHandler(this.textBoxOnOffDstEndPoint_MouseHover);
            // 
            // textBoxOnOffSrcEndPoint
            // 
            this.textBoxOnOffSrcEndPoint.Location = new System.Drawing.Point(317, 6);
            this.textBoxOnOffSrcEndPoint.Name = "textBoxOnOffSrcEndPoint";
            this.textBoxOnOffSrcEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxOnOffSrcEndPoint.TabIndex = 3;
            this.textBoxOnOffSrcEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOnOffSrcEndPoint_MouseClick);
            this.textBoxOnOffSrcEndPoint.Leave += new System.EventHandler(this.textBoxOnOffSrcEndPoint_Leave);
            this.textBoxOnOffSrcEndPoint.MouseLeave += new System.EventHandler(this.textBoxOnOffSrcEndPoint_MouseLeave);
            this.textBoxOnOffSrcEndPoint.MouseHover += new System.EventHandler(this.textBoxOnOffSrcEndPoint_MouseHover);
            // 
            // textBoxOnOffAddr
            // 
            this.textBoxOnOffAddr.Location = new System.Drawing.Point(204, 6);
            this.textBoxOnOffAddr.Name = "textBoxOnOffAddr";
            this.textBoxOnOffAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxOnOffAddr.TabIndex = 2;
            this.textBoxOnOffAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOnOffAddr_MouseClick);
            this.textBoxOnOffAddr.Leave += new System.EventHandler(this.textBoxOnOffAddr_Leave);
            this.textBoxOnOffAddr.MouseLeave += new System.EventHandler(this.textBoxOnOffAddr_MouseLeave);
            this.textBoxOnOffAddr.MouseHover += new System.EventHandler(this.textBoxOnOffAddr_MouseHover);
            // 
            // buttonOnOff
            // 
            this.buttonOnOff.Location = new System.Drawing.Point(4, 4);
            this.buttonOnOff.Name = "buttonOnOff";
            this.buttonOnOff.Size = new System.Drawing.Size(80, 22);
            this.buttonOnOff.TabIndex = 0;
            this.buttonOnOff.Text = "OnOff";
            this.buttonOnOff.UseVisualStyleBackColor = true;
            this.buttonOnOff.Click += new System.EventHandler(this.buttonOnOff_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage6.Controls.Add(this.comboBoxMoveToLevelOnOff);
            this.tabPage6.Controls.Add(this.comboBoxMoveToLevelAddrMode);
            this.tabPage6.Controls.Add(this.textBoxMoveToLevelTransTime);
            this.tabPage6.Controls.Add(this.textBoxMoveToLevelLevel);
            this.tabPage6.Controls.Add(this.textBoxMoveToLevelDstEndPoint);
            this.tabPage6.Controls.Add(this.textBoxMoveToLevelSrcEndPoint);
            this.tabPage6.Controls.Add(this.textBoxMoveToLevelAddr);
            this.tabPage6.Controls.Add(this.buttonMoveToLevel);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1833, 584);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Level Cluster";
            this.tabPage6.Click += new System.EventHandler(this.tabPage6_Click);
            // 
            // comboBoxMoveToLevelOnOff
            // 
            this.comboBoxMoveToLevelOnOff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMoveToLevelOnOff.FormattingEnabled = true;
            this.comboBoxMoveToLevelOnOff.Items.AddRange(new object[] {
            "Without OnOff",
            "With OnOff"});
            this.comboBoxMoveToLevelOnOff.Location = new System.Drawing.Point(553, 7);
            this.comboBoxMoveToLevelOnOff.Name = "comboBoxMoveToLevelOnOff";
            this.comboBoxMoveToLevelOnOff.Size = new System.Drawing.Size(106, 21);
            this.comboBoxMoveToLevelOnOff.TabIndex = 5;
            this.comboBoxMoveToLevelOnOff.SelectedIndexChanged += new System.EventHandler(this.comboBoxMoveToLevelOnOff_SelectedIndexChanged);
            this.comboBoxMoveToLevelOnOff.MouseLeave += new System.EventHandler(this.comboBoxMoveToLevelOnOff_MouseLeave);
            this.comboBoxMoveToLevelOnOff.MouseHover += new System.EventHandler(this.comboBoxMoveToLevelOnOff_MouseHover);
            // 
            // comboBoxMoveToLevelAddrMode
            // 
            this.comboBoxMoveToLevelAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMoveToLevelAddrMode.FormattingEnabled = true;
            this.comboBoxMoveToLevelAddrMode.Location = new System.Drawing.Point(100, 7);
            this.comboBoxMoveToLevelAddrMode.Name = "comboBoxMoveToLevelAddrMode";
            this.comboBoxMoveToLevelAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxMoveToLevelAddrMode.TabIndex = 1;
            this.comboBoxMoveToLevelAddrMode.MouseLeave += new System.EventHandler(this.comboBoxMoveToLevelAddrMode_MouseLeave);
            this.comboBoxMoveToLevelAddrMode.MouseHover += new System.EventHandler(this.comboBoxMoveToLevelAddrMode_MouseHover);
            // 
            // textBoxMoveToLevelTransTime
            // 
            this.textBoxMoveToLevelTransTime.Location = new System.Drawing.Point(778, 7);
            this.textBoxMoveToLevelTransTime.Name = "textBoxMoveToLevelTransTime";
            this.textBoxMoveToLevelTransTime.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToLevelTransTime.TabIndex = 7;
            this.textBoxMoveToLevelTransTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToLevelTransTime_MouseClick);
            this.textBoxMoveToLevelTransTime.Leave += new System.EventHandler(this.textBoxMoveToLevelTransTime_Leave);
            this.textBoxMoveToLevelTransTime.MouseLeave += new System.EventHandler(this.textBoxMoveToLevelTransTime_MouseLeave);
            this.textBoxMoveToLevelTransTime.MouseHover += new System.EventHandler(this.textBoxMoveToLevelTransTime_MouseHover);
            // 
            // textBoxMoveToLevelLevel
            // 
            this.textBoxMoveToLevelLevel.Location = new System.Drawing.Point(666, 7);
            this.textBoxMoveToLevelLevel.Name = "textBoxMoveToLevelLevel";
            this.textBoxMoveToLevelLevel.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToLevelLevel.TabIndex = 6;
            this.textBoxMoveToLevelLevel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToLevelLevel_MouseClick);
            this.textBoxMoveToLevelLevel.Leave += new System.EventHandler(this.textBoxMoveToLevelLevel_Leave);
            this.textBoxMoveToLevelLevel.MouseLeave += new System.EventHandler(this.textBoxMoveToLevelLevel_MouseLeave);
            this.textBoxMoveToLevelLevel.MouseHover += new System.EventHandler(this.textBoxMoveToLevelLevel_MouseHover);
            // 
            // textBoxMoveToLevelDstEndPoint
            // 
            this.textBoxMoveToLevelDstEndPoint.Location = new System.Drawing.Point(439, 7);
            this.textBoxMoveToLevelDstEndPoint.Name = "textBoxMoveToLevelDstEndPoint";
            this.textBoxMoveToLevelDstEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToLevelDstEndPoint.TabIndex = 4;
            this.textBoxMoveToLevelDstEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToLevelDstEndPoint_MouseClick);
            this.textBoxMoveToLevelDstEndPoint.Leave += new System.EventHandler(this.textBoxMoveToLevelDstEndPoint_Leave);
            this.textBoxMoveToLevelDstEndPoint.MouseLeave += new System.EventHandler(this.textBoxMoveToLevelDstEndPoint_MouseLeave);
            this.textBoxMoveToLevelDstEndPoint.MouseHover += new System.EventHandler(this.textBoxMoveToLevelDstEndPoint_MouseHover);
            // 
            // textBoxMoveToLevelSrcEndPoint
            // 
            this.textBoxMoveToLevelSrcEndPoint.Location = new System.Drawing.Point(326, 7);
            this.textBoxMoveToLevelSrcEndPoint.Name = "textBoxMoveToLevelSrcEndPoint";
            this.textBoxMoveToLevelSrcEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToLevelSrcEndPoint.TabIndex = 3;
            this.textBoxMoveToLevelSrcEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToLevelSrcEndPoint_MouseClick);
            this.textBoxMoveToLevelSrcEndPoint.Leave += new System.EventHandler(this.textBoxMoveToLevelSrcEndPoint_Leave);
            this.textBoxMoveToLevelSrcEndPoint.MouseLeave += new System.EventHandler(this.textBoxMoveToLevelSrcEndPoint_MouseLeave);
            this.textBoxMoveToLevelSrcEndPoint.MouseHover += new System.EventHandler(this.textBoxMoveToLevelSrcEndPoint_MouseHover);
            // 
            // textBoxMoveToLevelAddr
            // 
            this.textBoxMoveToLevelAddr.Location = new System.Drawing.Point(214, 7);
            this.textBoxMoveToLevelAddr.Name = "textBoxMoveToLevelAddr";
            this.textBoxMoveToLevelAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxMoveToLevelAddr.TabIndex = 2;
            this.textBoxMoveToLevelAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMoveToLevelAddr_MouseClick);
            this.textBoxMoveToLevelAddr.TextChanged += new System.EventHandler(this.textBoxMoveToLevelAddr_TextChanged);
            this.textBoxMoveToLevelAddr.Leave += new System.EventHandler(this.textBoxMoveToLevelAddr_Leave);
            this.textBoxMoveToLevelAddr.MouseLeave += new System.EventHandler(this.textBoxMoveToLevelAddr_MouseLeave);
            this.textBoxMoveToLevelAddr.MouseHover += new System.EventHandler(this.textBoxMoveToLevelAddr_MouseHover);
            // 
            // buttonMoveToLevel
            // 
            this.buttonMoveToLevel.Location = new System.Drawing.Point(3, 5);
            this.buttonMoveToLevel.Name = "buttonMoveToLevel";
            this.buttonMoveToLevel.Size = new System.Drawing.Size(90, 22);
            this.buttonMoveToLevel.TabIndex = 0;
            this.buttonMoveToLevel.Text = "MoveToLevel";
            this.buttonMoveToLevel.UseVisualStyleBackColor = true;
            this.buttonMoveToLevel.Click += new System.EventHandler(this.buttonMoveToLevel_Click_1);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage5.Controls.Add(this.textBoxIdQueryDstEp);
            this.tabPage5.Controls.Add(this.textBoxIdQuerySrcEp);
            this.tabPage5.Controls.Add(this.textBoxIdQueryAddr);
            this.tabPage5.Controls.Add(this.textBoxIdSendTime);
            this.tabPage5.Controls.Add(this.textBoxIdSendDstEp);
            this.tabPage5.Controls.Add(this.textBoxSendIdSrcEp);
            this.tabPage5.Controls.Add(this.textBoxSendIdAddr);
            this.tabPage5.Controls.Add(this.buttonIdQuery);
            this.tabPage5.Controls.Add(this.buttonIdSend);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1833, 584);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Identify Cluster";
            this.tabPage5.Click += new System.EventHandler(this.tabPage5_Click);
            // 
            // textBoxIdQueryDstEp
            // 
            this.textBoxIdQueryDstEp.Location = new System.Drawing.Point(317, 35);
            this.textBoxIdQueryDstEp.Name = "textBoxIdQueryDstEp";
            this.textBoxIdQueryDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxIdQueryDstEp.TabIndex = 8;
            this.textBoxIdQueryDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIdQueryDstEp_MouseClick);
            this.textBoxIdQueryDstEp.Leave += new System.EventHandler(this.textBoxIdQueryDstEp_Leave);
            this.textBoxIdQueryDstEp.MouseLeave += new System.EventHandler(this.textBoxIdQueryDstEp_MouseLeave);
            this.textBoxIdQueryDstEp.MouseHover += new System.EventHandler(this.textBoxIdQueryDstEp_MouseHover);
            // 
            // textBoxIdQuerySrcEp
            // 
            this.textBoxIdQuerySrcEp.Location = new System.Drawing.Point(204, 35);
            this.textBoxIdQuerySrcEp.Name = "textBoxIdQuerySrcEp";
            this.textBoxIdQuerySrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxIdQuerySrcEp.TabIndex = 7;
            this.textBoxIdQuerySrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIdQuerySrcEp_MouseClick);
            this.textBoxIdQuerySrcEp.Leave += new System.EventHandler(this.textBoxIdQuerySrcEp_Leave);
            this.textBoxIdQuerySrcEp.MouseLeave += new System.EventHandler(this.textBoxIdQuerySrcEp_MouseLeave);
            this.textBoxIdQuerySrcEp.MouseHover += new System.EventHandler(this.textBoxIdQuerySrcEp_MouseHover);
            // 
            // textBoxIdQueryAddr
            // 
            this.textBoxIdQueryAddr.Location = new System.Drawing.Point(90, 35);
            this.textBoxIdQueryAddr.Name = "textBoxIdQueryAddr";
            this.textBoxIdQueryAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxIdQueryAddr.TabIndex = 6;
            this.textBoxIdQueryAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIdQueryAddr_MouseClick);
            this.textBoxIdQueryAddr.Leave += new System.EventHandler(this.textBoxIdQueryAddr_Leave);
            this.textBoxIdQueryAddr.MouseLeave += new System.EventHandler(this.textBoxIdQueryAddr_MouseLeave);
            this.textBoxIdQueryAddr.MouseHover += new System.EventHandler(this.textBoxIdQueryAddr_MouseHover);
            // 
            // textBoxIdSendTime
            // 
            this.textBoxIdSendTime.Location = new System.Drawing.Point(430, 6);
            this.textBoxIdSendTime.Name = "textBoxIdSendTime";
            this.textBoxIdSendTime.Size = new System.Drawing.Size(106, 20);
            this.textBoxIdSendTime.TabIndex = 4;
            this.textBoxIdSendTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIdSendTime_MouseClick);
            this.textBoxIdSendTime.Leave += new System.EventHandler(this.textBoxIdSendTime_Leave);
            this.textBoxIdSendTime.MouseLeave += new System.EventHandler(this.textBoxIdSendTime_MouseLeave);
            this.textBoxIdSendTime.MouseHover += new System.EventHandler(this.textBoxIdSendTime_MouseHover);
            // 
            // textBoxIdSendDstEp
            // 
            this.textBoxIdSendDstEp.Location = new System.Drawing.Point(317, 6);
            this.textBoxIdSendDstEp.Name = "textBoxIdSendDstEp";
            this.textBoxIdSendDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxIdSendDstEp.TabIndex = 3;
            this.textBoxIdSendDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIdSendDstEp_MouseClick);
            this.textBoxIdSendDstEp.Leave += new System.EventHandler(this.textBoxIdSendDstEp_Leave);
            this.textBoxIdSendDstEp.MouseLeave += new System.EventHandler(this.textBoxIdSendDstEp_MouseLeave);
            this.textBoxIdSendDstEp.MouseHover += new System.EventHandler(this.textBoxIdSendDstEp_MouseHover);
            // 
            // textBoxSendIdSrcEp
            // 
            this.textBoxSendIdSrcEp.Location = new System.Drawing.Point(204, 6);
            this.textBoxSendIdSrcEp.Name = "textBoxSendIdSrcEp";
            this.textBoxSendIdSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxSendIdSrcEp.TabIndex = 2;
            this.textBoxSendIdSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSendIdSrcEp_MouseClick);
            this.textBoxSendIdSrcEp.Leave += new System.EventHandler(this.textBoxSendIdSrcEp_Leave);
            this.textBoxSendIdSrcEp.MouseLeave += new System.EventHandler(this.textBoxSendIdSrcEp_MouseLeave);
            this.textBoxSendIdSrcEp.MouseHover += new System.EventHandler(this.textBoxSendIdSrcEp_MouseHover);
            // 
            // textBoxSendIdAddr
            // 
            this.textBoxSendIdAddr.Location = new System.Drawing.Point(90, 6);
            this.textBoxSendIdAddr.Name = "textBoxSendIdAddr";
            this.textBoxSendIdAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxSendIdAddr.TabIndex = 1;
            this.textBoxSendIdAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSendIdAddr_MouseClick);
            this.textBoxSendIdAddr.Leave += new System.EventHandler(this.textBoxSendIdAddr_Leave);
            this.textBoxSendIdAddr.MouseLeave += new System.EventHandler(this.textBoxSendIdAddr_MouseLeave);
            this.textBoxSendIdAddr.MouseHover += new System.EventHandler(this.textBoxSendIdAddr_MouseHover);
            // 
            // buttonIdQuery
            // 
            this.buttonIdQuery.Location = new System.Drawing.Point(4, 34);
            this.buttonIdQuery.Name = "buttonIdQuery";
            this.buttonIdQuery.Size = new System.Drawing.Size(80, 22);
            this.buttonIdQuery.TabIndex = 5;
            this.buttonIdQuery.Text = "ID Query";
            this.buttonIdQuery.UseVisualStyleBackColor = true;
            this.buttonIdQuery.Click += new System.EventHandler(this.buttonIdQuery_Click);
            // 
            // buttonIdSend
            // 
            this.buttonIdSend.Location = new System.Drawing.Point(4, 4);
            this.buttonIdSend.Name = "buttonIdSend";
            this.buttonIdSend.Size = new System.Drawing.Size(80, 22);
            this.buttonIdSend.TabIndex = 0;
            this.buttonIdSend.Text = "ID Send";
            this.buttonIdSend.UseVisualStyleBackColor = true;
            this.buttonIdSend.Click += new System.EventHandler(this.buttonIdSend_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.textBoxGroupName);
            this.tabPage3.Controls.Add(this.textBoxGroupNameMaxLength);
            this.tabPage3.Controls.Add(this.textBoxGroupNameLength);
            this.tabPage3.Controls.Add(this.textBoxGroupAddIfIdentifyGroupID);
            this.tabPage3.Controls.Add(this.textBoxGroupAddIfIdentifySrcEp);
            this.tabPage3.Controls.Add(this.textBoxGroupAddIfIdentifyDstEp);
            this.tabPage3.Controls.Add(this.textBoxGroupAddIfIndentifyingTargetAddr);
            this.tabPage3.Controls.Add(this.textBoxRemoveAllGroupDstEp);
            this.tabPage3.Controls.Add(this.textBoxRemoveAllGroupSrcEp);
            this.tabPage3.Controls.Add(this.textBoxRemoveAllGroupTargetAddr);
            this.tabPage3.Controls.Add(this.textBoxRemoveGroupGroupAddr);
            this.tabPage3.Controls.Add(this.textBoxRemoveGroupDstEp);
            this.tabPage3.Controls.Add(this.textBoxRemoveGroupSrcEp);
            this.tabPage3.Controls.Add(this.textBoxRemoveGroupTargetAddr);
            this.tabPage3.Controls.Add(this.textBoxGetGroupCount);
            this.tabPage3.Controls.Add(this.textBoxGetGroupDstEp);
            this.tabPage3.Controls.Add(this.textBoxGetGroupSrcEp);
            this.tabPage3.Controls.Add(this.textBoxGetGroupTargetAddr);
            this.tabPage3.Controls.Add(this.textBoxViewGroupGroupAddr);
            this.tabPage3.Controls.Add(this.textBoxViewGroupDstEp);
            this.tabPage3.Controls.Add(this.textBoxViewGroupSrcEp);
            this.tabPage3.Controls.Add(this.textBoxViewGroupAddr);
            this.tabPage3.Controls.Add(this.textBoxAddGroupGroupAddr);
            this.tabPage3.Controls.Add(this.textBoxAddGroupDstEp);
            this.tabPage3.Controls.Add(this.textBoxAddGroupSrcEp);
            this.tabPage3.Controls.Add(this.textBoxAddGroupAddr);
            this.tabPage3.Controls.Add(this.buttonAddToList);
            this.tabPage3.Controls.Add(this.buttonGroupAddIfIdentifying);
            this.tabPage3.Controls.Add(this.buttonGroupRemoveAll);
            this.tabPage3.Controls.Add(this.buttonRemoveGroup);
            this.tabPage3.Controls.Add(this.buttonGetGroup);
            this.tabPage3.Controls.Add(this.buttonViewGroup);
            this.tabPage3.Controls.Add(this.buttonAddGroup);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1833, 584);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Group Cluster";
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // textBoxGroupName
            // 
            this.textBoxGroupName.Location = new System.Drawing.Point(785, 6);
            this.textBoxGroupName.Name = "textBoxGroupName";
            this.textBoxGroupName.Size = new System.Drawing.Size(113, 20);
            this.textBoxGroupName.TabIndex = 7;
            this.textBoxGroupName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGroupName_MouseClick);
            this.textBoxGroupName.Leave += new System.EventHandler(this.textBoxGroupName_Leave);
            this.textBoxGroupName.MouseLeave += new System.EventHandler(this.textBoxGroupName_MouseLeave);
            this.textBoxGroupName.MouseHover += new System.EventHandler(this.textBoxGroupName_MouseHover);
            // 
            // textBoxGroupNameMaxLength
            // 
            this.textBoxGroupNameMaxLength.Location = new System.Drawing.Point(666, 6);
            this.textBoxGroupNameMaxLength.Name = "textBoxGroupNameMaxLength";
            this.textBoxGroupNameMaxLength.Size = new System.Drawing.Size(113, 20);
            this.textBoxGroupNameMaxLength.TabIndex = 6;
            this.textBoxGroupNameMaxLength.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGroupNameMaxLength_MouseClick);
            this.textBoxGroupNameMaxLength.Leave += new System.EventHandler(this.textBoxGroupNameMaxLength_Leave);
            this.textBoxGroupNameMaxLength.MouseLeave += new System.EventHandler(this.textBoxGroupNameMaxLength_MouseLeave);
            this.textBoxGroupNameMaxLength.MouseHover += new System.EventHandler(this.textBoxGroupNameMaxLength_MouseHover);
            // 
            // textBoxGroupNameLength
            // 
            this.textBoxGroupNameLength.Location = new System.Drawing.Point(548, 6);
            this.textBoxGroupNameLength.Name = "textBoxGroupNameLength";
            this.textBoxGroupNameLength.Size = new System.Drawing.Size(113, 20);
            this.textBoxGroupNameLength.TabIndex = 5;
            this.textBoxGroupNameLength.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGroupNameLength_MouseClick);
            this.textBoxGroupNameLength.Leave += new System.EventHandler(this.textBoxGroupNameLength_Leave);
            this.textBoxGroupNameLength.MouseLeave += new System.EventHandler(this.textBoxGroupNameLength_MouseLeave);
            this.textBoxGroupNameLength.MouseHover += new System.EventHandler(this.textBoxGroupNameLength_MouseHover);
            // 
            // textBoxGroupAddIfIdentifyGroupID
            // 
            this.textBoxGroupAddIfIdentifyGroupID.Location = new System.Drawing.Point(429, 149);
            this.textBoxGroupAddIfIdentifyGroupID.Name = "textBoxGroupAddIfIdentifyGroupID";
            this.textBoxGroupAddIfIdentifyGroupID.Size = new System.Drawing.Size(113, 20);
            this.textBoxGroupAddIfIdentifyGroupID.TabIndex = 32;
            this.textBoxGroupAddIfIdentifyGroupID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGroupAddIfIdentifyGroupID_MouseClick);
            this.textBoxGroupAddIfIdentifyGroupID.Leave += new System.EventHandler(this.textBoxGroupAddIfIdentifyGroupID_Leave);
            this.textBoxGroupAddIfIdentifyGroupID.MouseLeave += new System.EventHandler(this.textBoxGroupAddIfIdentifyGroupID_MouseLeave);
            this.textBoxGroupAddIfIdentifyGroupID.MouseHover += new System.EventHandler(this.textBoxGroupAddIfIdentifyGroupID_MouseHover);
            // 
            // textBoxGroupAddIfIdentifySrcEp
            // 
            this.textBoxGroupAddIfIdentifySrcEp.Location = new System.Drawing.Point(204, 149);
            this.textBoxGroupAddIfIdentifySrcEp.Name = "textBoxGroupAddIfIdentifySrcEp";
            this.textBoxGroupAddIfIdentifySrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxGroupAddIfIdentifySrcEp.TabIndex = 30;
            this.textBoxGroupAddIfIdentifySrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGroupAddIfIdentifySrcEp_MouseClick);
            this.textBoxGroupAddIfIdentifySrcEp.Leave += new System.EventHandler(this.textBoxGroupAddIfIdentifySrcEp_Leave);
            this.textBoxGroupAddIfIdentifySrcEp.MouseLeave += new System.EventHandler(this.textBoxGroupAddIfIdentifySrcEp_MouseLeave);
            this.textBoxGroupAddIfIdentifySrcEp.MouseHover += new System.EventHandler(this.textBoxGroupAddIfIdentifySrcEp_MouseHover);
            // 
            // textBoxGroupAddIfIdentifyDstEp
            // 
            this.textBoxGroupAddIfIdentifyDstEp.Location = new System.Drawing.Point(317, 149);
            this.textBoxGroupAddIfIdentifyDstEp.Name = "textBoxGroupAddIfIdentifyDstEp";
            this.textBoxGroupAddIfIdentifyDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxGroupAddIfIdentifyDstEp.TabIndex = 31;
            this.textBoxGroupAddIfIdentifyDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGroupAddIfIdentifyDstEp_MouseClick);
            this.textBoxGroupAddIfIdentifyDstEp.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBoxGroupAddIfIdentifyDstEp.Leave += new System.EventHandler(this.textBoxGroupAddIfIdentifyDstEp_Leave);
            this.textBoxGroupAddIfIdentifyDstEp.MouseLeave += new System.EventHandler(this.textBoxGroupAddIfIdentifyDstEp_MouseLeave);
            this.textBoxGroupAddIfIdentifyDstEp.MouseHover += new System.EventHandler(this.textBoxGroupAddIfIdentifyDstEp_MouseHover);
            // 
            // textBoxGroupAddIfIndentifyingTargetAddr
            // 
            this.textBoxGroupAddIfIndentifyingTargetAddr.Location = new System.Drawing.Point(90, 149);
            this.textBoxGroupAddIfIndentifyingTargetAddr.Name = "textBoxGroupAddIfIndentifyingTargetAddr";
            this.textBoxGroupAddIfIndentifyingTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxGroupAddIfIndentifyingTargetAddr.TabIndex = 29;
            this.textBoxGroupAddIfIndentifyingTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGroupAddIfIndentifyingTargetAddr_MouseClick);
            this.textBoxGroupAddIfIndentifyingTargetAddr.Leave += new System.EventHandler(this.textBoxGroupAddIfIndentifyingTargetAddr_Leave);
            this.textBoxGroupAddIfIndentifyingTargetAddr.MouseLeave += new System.EventHandler(this.textBoxGroupAddIfIndentifyingTargetAddr_MouseLeave);
            this.textBoxGroupAddIfIndentifyingTargetAddr.MouseHover += new System.EventHandler(this.textBoxGroupAddIfIndentifyingTargetAddr_MouseHover);
            // 
            // textBoxRemoveAllGroupDstEp
            // 
            this.textBoxRemoveAllGroupDstEp.Location = new System.Drawing.Point(317, 120);
            this.textBoxRemoveAllGroupDstEp.Name = "textBoxRemoveAllGroupDstEp";
            this.textBoxRemoveAllGroupDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveAllGroupDstEp.TabIndex = 27;
            this.textBoxRemoveAllGroupDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveAllGroupDstEp_MouseClick);
            this.textBoxRemoveAllGroupDstEp.Leave += new System.EventHandler(this.textBoxRemoveAllGroupDstEp_Leave);
            this.textBoxRemoveAllGroupDstEp.MouseLeave += new System.EventHandler(this.textBoxRemoveAllGroupDstEp_MouseLeave);
            this.textBoxRemoveAllGroupDstEp.MouseHover += new System.EventHandler(this.textBoxRemoveAllGroupDstEp_MouseHover);
            // 
            // textBoxRemoveAllGroupSrcEp
            // 
            this.textBoxRemoveAllGroupSrcEp.Location = new System.Drawing.Point(204, 121);
            this.textBoxRemoveAllGroupSrcEp.Name = "textBoxRemoveAllGroupSrcEp";
            this.textBoxRemoveAllGroupSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveAllGroupSrcEp.TabIndex = 26;
            this.textBoxRemoveAllGroupSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveAllGroupSrcEp_MouseClick);
            this.textBoxRemoveAllGroupSrcEp.Leave += new System.EventHandler(this.textBoxRemoveAllGroupSrcEp_Leave);
            this.textBoxRemoveAllGroupSrcEp.MouseLeave += new System.EventHandler(this.textBoxRemoveAllGroupSrcEp_MouseLeave);
            this.textBoxRemoveAllGroupSrcEp.MouseHover += new System.EventHandler(this.textBoxRemoveAllGroupSrcEp_MouseHover);
            // 
            // textBoxRemoveAllGroupTargetAddr
            // 
            this.textBoxRemoveAllGroupTargetAddr.Location = new System.Drawing.Point(90, 120);
            this.textBoxRemoveAllGroupTargetAddr.Name = "textBoxRemoveAllGroupTargetAddr";
            this.textBoxRemoveAllGroupTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveAllGroupTargetAddr.TabIndex = 25;
            this.textBoxRemoveAllGroupTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveAllGroupTargetAddr_MouseClick);
            this.textBoxRemoveAllGroupTargetAddr.Leave += new System.EventHandler(this.textBoxRemoveAllGroupTargetAddr_Leave);
            this.textBoxRemoveAllGroupTargetAddr.MouseLeave += new System.EventHandler(this.textBoxRemoveAllGroupTargetAddr_MouseLeave);
            this.textBoxRemoveAllGroupTargetAddr.MouseHover += new System.EventHandler(this.textBoxRemoveAllGroupTargetAddr_MouseHover);
            // 
            // textBoxRemoveGroupGroupAddr
            // 
            this.textBoxRemoveGroupGroupAddr.Location = new System.Drawing.Point(429, 91);
            this.textBoxRemoveGroupGroupAddr.Name = "textBoxRemoveGroupGroupAddr";
            this.textBoxRemoveGroupGroupAddr.Size = new System.Drawing.Size(114, 20);
            this.textBoxRemoveGroupGroupAddr.TabIndex = 23;
            this.textBoxRemoveGroupGroupAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveGroupGroupAddr_MouseClick);
            this.textBoxRemoveGroupGroupAddr.Leave += new System.EventHandler(this.textBoxRemoveGroupGroupAddr_Leave);
            this.textBoxRemoveGroupGroupAddr.MouseLeave += new System.EventHandler(this.textBoxRemoveGroupGroupAddr_MouseLeave);
            this.textBoxRemoveGroupGroupAddr.MouseHover += new System.EventHandler(this.textBoxRemoveGroupGroupAddr_MouseHover);
            // 
            // textBoxRemoveGroupDstEp
            // 
            this.textBoxRemoveGroupDstEp.Location = new System.Drawing.Point(317, 91);
            this.textBoxRemoveGroupDstEp.Name = "textBoxRemoveGroupDstEp";
            this.textBoxRemoveGroupDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveGroupDstEp.TabIndex = 22;
            this.textBoxRemoveGroupDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveGroupDstEp_MouseClick);
            this.textBoxRemoveGroupDstEp.Leave += new System.EventHandler(this.textBoxRemoveGroupDstEp_Leave);
            this.textBoxRemoveGroupDstEp.MouseLeave += new System.EventHandler(this.textBoxRemoveGroupDstEp_MouseLeave);
            this.textBoxRemoveGroupDstEp.MouseHover += new System.EventHandler(this.textBoxRemoveGroupDstEp_MouseHover);
            // 
            // textBoxRemoveGroupSrcEp
            // 
            this.textBoxRemoveGroupSrcEp.Location = new System.Drawing.Point(204, 91);
            this.textBoxRemoveGroupSrcEp.Name = "textBoxRemoveGroupSrcEp";
            this.textBoxRemoveGroupSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveGroupSrcEp.TabIndex = 21;
            this.textBoxRemoveGroupSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveGroupSrcEp_MouseClick);
            this.textBoxRemoveGroupSrcEp.Leave += new System.EventHandler(this.textBoxRemoveGroupSrcEp_Leave);
            this.textBoxRemoveGroupSrcEp.MouseLeave += new System.EventHandler(this.textBoxRemoveGroupSrcEp_MouseLeave);
            this.textBoxRemoveGroupSrcEp.MouseHover += new System.EventHandler(this.textBoxRemoveGroupSrcEp_MouseHover);
            // 
            // textBoxRemoveGroupTargetAddr
            // 
            this.textBoxRemoveGroupTargetAddr.Location = new System.Drawing.Point(90, 91);
            this.textBoxRemoveGroupTargetAddr.Name = "textBoxRemoveGroupTargetAddr";
            this.textBoxRemoveGroupTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxRemoveGroupTargetAddr.TabIndex = 20;
            this.textBoxRemoveGroupTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveGroupTargetAddr_MouseClick);
            this.textBoxRemoveGroupTargetAddr.Leave += new System.EventHandler(this.textBoxRemoveGroupTargetAddr_Leave);
            this.textBoxRemoveGroupTargetAddr.MouseLeave += new System.EventHandler(this.textBoxRemoveGroupTargetAddr_MouseLeave);
            this.textBoxRemoveGroupTargetAddr.MouseHover += new System.EventHandler(this.textBoxRemoveGroupTargetAddr_MouseHover);
            // 
            // textBoxGetGroupCount
            // 
            this.textBoxGetGroupCount.Location = new System.Drawing.Point(430, 63);
            this.textBoxGetGroupCount.Name = "textBoxGetGroupCount";
            this.textBoxGetGroupCount.Size = new System.Drawing.Size(113, 20);
            this.textBoxGetGroupCount.TabIndex = 17;
            this.textBoxGetGroupCount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGetGroupCount_MouseClick);
            this.textBoxGetGroupCount.Leave += new System.EventHandler(this.textBoxGetGroupCount_Leave);
            this.textBoxGetGroupCount.MouseLeave += new System.EventHandler(this.textBoxGetGroupCount_MouseLeave);
            this.textBoxGetGroupCount.MouseHover += new System.EventHandler(this.textBoxGetGroupCount_MouseHover);
            // 
            // textBoxGetGroupDstEp
            // 
            this.textBoxGetGroupDstEp.Location = new System.Drawing.Point(317, 63);
            this.textBoxGetGroupDstEp.Name = "textBoxGetGroupDstEp";
            this.textBoxGetGroupDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxGetGroupDstEp.TabIndex = 16;
            this.textBoxGetGroupDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGetGroupDstEp_MouseClick);
            this.textBoxGetGroupDstEp.Leave += new System.EventHandler(this.textBoxGetGroupDstEp_Leave);
            this.textBoxGetGroupDstEp.MouseLeave += new System.EventHandler(this.textBoxGetGroupDstEp_MouseLeave);
            this.textBoxGetGroupDstEp.MouseHover += new System.EventHandler(this.textBoxGetGroupDstEp_MouseHover);
            // 
            // textBoxGetGroupSrcEp
            // 
            this.textBoxGetGroupSrcEp.Location = new System.Drawing.Point(204, 63);
            this.textBoxGetGroupSrcEp.Name = "textBoxGetGroupSrcEp";
            this.textBoxGetGroupSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxGetGroupSrcEp.TabIndex = 15;
            this.textBoxGetGroupSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGetGroupSrcEp_MouseClick);
            this.textBoxGetGroupSrcEp.Leave += new System.EventHandler(this.textBoxGetGroupSrcEp_Leave);
            this.textBoxGetGroupSrcEp.MouseLeave += new System.EventHandler(this.textBoxGetGroupSrcEp_MouseLeave);
            this.textBoxGetGroupSrcEp.MouseHover += new System.EventHandler(this.textBoxGetGroupSrcEp_MouseHover);
            // 
            // textBoxGetGroupTargetAddr
            // 
            this.textBoxGetGroupTargetAddr.Location = new System.Drawing.Point(90, 63);
            this.textBoxGetGroupTargetAddr.Name = "textBoxGetGroupTargetAddr";
            this.textBoxGetGroupTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxGetGroupTargetAddr.TabIndex = 14;
            this.textBoxGetGroupTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxGetGroupTargetAddr_MouseClick);
            this.textBoxGetGroupTargetAddr.Leave += new System.EventHandler(this.textBoxGetGroupTargetAddr_Leave);
            this.textBoxGetGroupTargetAddr.MouseLeave += new System.EventHandler(this.textBoxGetGroupTargetAddr_MouseLeave);
            this.textBoxGetGroupTargetAddr.MouseHover += new System.EventHandler(this.textBoxGetGroupTargetAddr_MouseHover);
            // 
            // textBoxViewGroupGroupAddr
            // 
            this.textBoxViewGroupGroupAddr.Location = new System.Drawing.Point(430, 34);
            this.textBoxViewGroupGroupAddr.Name = "textBoxViewGroupGroupAddr";
            this.textBoxViewGroupGroupAddr.Size = new System.Drawing.Size(113, 20);
            this.textBoxViewGroupGroupAddr.TabIndex = 12;
            this.textBoxViewGroupGroupAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxViewGroupGroupAddr_MouseClick);
            this.textBoxViewGroupGroupAddr.Leave += new System.EventHandler(this.textBoxViewGroupGroupAddr_Leave);
            this.textBoxViewGroupGroupAddr.MouseLeave += new System.EventHandler(this.textBoxViewGroupGroupAddr_MouseLeave);
            this.textBoxViewGroupGroupAddr.MouseHover += new System.EventHandler(this.textBoxViewGroupGroupAddr_MouseHover);
            // 
            // textBoxViewGroupDstEp
            // 
            this.textBoxViewGroupDstEp.Location = new System.Drawing.Point(317, 34);
            this.textBoxViewGroupDstEp.Name = "textBoxViewGroupDstEp";
            this.textBoxViewGroupDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxViewGroupDstEp.TabIndex = 11;
            this.textBoxViewGroupDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxViewGroupDstEp_MouseClick);
            this.textBoxViewGroupDstEp.Leave += new System.EventHandler(this.textBoxViewGroupDstEp_Leave);
            this.textBoxViewGroupDstEp.MouseLeave += new System.EventHandler(this.textBoxViewGroupDstEp_MouseLeave);
            this.textBoxViewGroupDstEp.MouseHover += new System.EventHandler(this.textBoxViewGroupDstEp_MouseHover);
            // 
            // textBoxViewGroupSrcEp
            // 
            this.textBoxViewGroupSrcEp.Location = new System.Drawing.Point(204, 34);
            this.textBoxViewGroupSrcEp.Name = "textBoxViewGroupSrcEp";
            this.textBoxViewGroupSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxViewGroupSrcEp.TabIndex = 10;
            this.textBoxViewGroupSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxViewGroupSrcEp_MouseClick);
            this.textBoxViewGroupSrcEp.Leave += new System.EventHandler(this.textBoxViewGroupSrcEp_Leave);
            this.textBoxViewGroupSrcEp.MouseLeave += new System.EventHandler(this.textBoxViewGroupSrcEp_MouseLeave);
            this.textBoxViewGroupSrcEp.MouseHover += new System.EventHandler(this.textBoxViewGroupSrcEp_MouseHover);
            // 
            // textBoxViewGroupAddr
            // 
            this.textBoxViewGroupAddr.Location = new System.Drawing.Point(90, 34);
            this.textBoxViewGroupAddr.Name = "textBoxViewGroupAddr";
            this.textBoxViewGroupAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxViewGroupAddr.TabIndex = 9;
            this.textBoxViewGroupAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxViewGroupAddr_MouseClick);
            this.textBoxViewGroupAddr.Leave += new System.EventHandler(this.textBoxViewGroupAddr_Leave);
            this.textBoxViewGroupAddr.MouseLeave += new System.EventHandler(this.textBoxViewGroupAddr_MouseLeave);
            this.textBoxViewGroupAddr.MouseHover += new System.EventHandler(this.textBoxViewGroupAddr_MouseHover);
            // 
            // textBoxAddGroupGroupAddr
            // 
            this.textBoxAddGroupGroupAddr.Location = new System.Drawing.Point(430, 6);
            this.textBoxAddGroupGroupAddr.Name = "textBoxAddGroupGroupAddr";
            this.textBoxAddGroupGroupAddr.Size = new System.Drawing.Size(113, 20);
            this.textBoxAddGroupGroupAddr.TabIndex = 4;
            this.textBoxAddGroupGroupAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddGroupGroupAddr_MouseClick);
            this.textBoxAddGroupGroupAddr.Leave += new System.EventHandler(this.textBoxAddGroupGroupAddr_Leave);
            this.textBoxAddGroupGroupAddr.MouseLeave += new System.EventHandler(this.textBoxAddGroupGroupAddr_MouseLeave);
            this.textBoxAddGroupGroupAddr.MouseHover += new System.EventHandler(this.textBoxAddGroupGroupAddr_MouseHover);
            // 
            // textBoxAddGroupDstEp
            // 
            this.textBoxAddGroupDstEp.Location = new System.Drawing.Point(317, 6);
            this.textBoxAddGroupDstEp.Name = "textBoxAddGroupDstEp";
            this.textBoxAddGroupDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddGroupDstEp.TabIndex = 3;
            this.textBoxAddGroupDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddGroupDstEp_MouseClick);
            this.textBoxAddGroupDstEp.Leave += new System.EventHandler(this.textBoxAddGroupDstEp_Leave);
            this.textBoxAddGroupDstEp.MouseLeave += new System.EventHandler(this.textBoxAddGroupDstEp_MouseLeave);
            this.textBoxAddGroupDstEp.MouseHover += new System.EventHandler(this.textBoxAddGroupDstEp_MouseHover);
            // 
            // textBoxAddGroupSrcEp
            // 
            this.textBoxAddGroupSrcEp.Location = new System.Drawing.Point(204, 6);
            this.textBoxAddGroupSrcEp.Name = "textBoxAddGroupSrcEp";
            this.textBoxAddGroupSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddGroupSrcEp.TabIndex = 2;
            this.textBoxAddGroupSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddGroupSrcEp_MouseClick);
            this.textBoxAddGroupSrcEp.Leave += new System.EventHandler(this.textBoxAddGroupSrcEp_Leave);
            this.textBoxAddGroupSrcEp.MouseLeave += new System.EventHandler(this.textBoxAddGroupSrcEp_MouseLeave);
            this.textBoxAddGroupSrcEp.MouseHover += new System.EventHandler(this.textBoxAddGroupSrcEp_MouseHover);
            // 
            // textBoxAddGroupAddr
            // 
            this.textBoxAddGroupAddr.Location = new System.Drawing.Point(90, 6);
            this.textBoxAddGroupAddr.Name = "textBoxAddGroupAddr";
            this.textBoxAddGroupAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxAddGroupAddr.TabIndex = 1;
            this.textBoxAddGroupAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAddGroupAddr_MouseClick);
            this.textBoxAddGroupAddr.Leave += new System.EventHandler(this.textBoxAddGroupAddr_Leave);
            this.textBoxAddGroupAddr.MouseLeave += new System.EventHandler(this.textBoxAddGroupAddr_MouseLeave);
            this.textBoxAddGroupAddr.MouseHover += new System.EventHandler(this.textBoxAddGroupAddr_MouseHover);
            // 
            // buttonAddToList
            // 
            this.buttonAddToList.Location = new System.Drawing.Point(550, 61);
            this.buttonAddToList.Name = "buttonAddToList";
            this.buttonAddToList.Size = new System.Drawing.Size(105, 22);
            this.buttonAddToList.TabIndex = 18;
            this.buttonAddToList.Text = "Add Group List";
            this.buttonAddToList.UseVisualStyleBackColor = true;
            this.buttonAddToList.Click += new System.EventHandler(this.buttonAddToList_Click);
            // 
            // buttonGroupAddIfIdentifying
            // 
            this.buttonGroupAddIfIdentifying.Location = new System.Drawing.Point(4, 146);
            this.buttonGroupAddIfIdentifying.Name = "buttonGroupAddIfIdentifying";
            this.buttonGroupAddIfIdentifying.Size = new System.Drawing.Size(80, 22);
            this.buttonGroupAddIfIdentifying.TabIndex = 28;
            this.buttonGroupAddIfIdentifying.Text = "Add If Ident";
            this.buttonGroupAddIfIdentifying.UseVisualStyleBackColor = true;
            this.buttonGroupAddIfIdentifying.Click += new System.EventHandler(this.buttonGroupAddIfIdentifying_Click);
            // 
            // buttonGroupRemoveAll
            // 
            this.buttonGroupRemoveAll.Location = new System.Drawing.Point(4, 118);
            this.buttonGroupRemoveAll.Name = "buttonGroupRemoveAll";
            this.buttonGroupRemoveAll.Size = new System.Drawing.Size(80, 22);
            this.buttonGroupRemoveAll.TabIndex = 24;
            this.buttonGroupRemoveAll.Text = "Remove All";
            this.buttonGroupRemoveAll.UseVisualStyleBackColor = true;
            this.buttonGroupRemoveAll.Click += new System.EventHandler(this.buttonGroupRemoveAll_Click);
            // 
            // buttonRemoveGroup
            // 
            this.buttonRemoveGroup.Location = new System.Drawing.Point(4, 90);
            this.buttonRemoveGroup.Name = "buttonRemoveGroup";
            this.buttonRemoveGroup.Size = new System.Drawing.Size(80, 22);
            this.buttonRemoveGroup.TabIndex = 19;
            this.buttonRemoveGroup.Text = "Remove Grp";
            this.buttonRemoveGroup.UseVisualStyleBackColor = true;
            this.buttonRemoveGroup.Click += new System.EventHandler(this.buttonRemoveGroup_Click);
            // 
            // buttonGetGroup
            // 
            this.buttonGetGroup.Location = new System.Drawing.Point(4, 61);
            this.buttonGetGroup.Name = "buttonGetGroup";
            this.buttonGetGroup.Size = new System.Drawing.Size(80, 22);
            this.buttonGetGroup.TabIndex = 13;
            this.buttonGetGroup.Text = "Get Group";
            this.buttonGetGroup.UseVisualStyleBackColor = true;
            this.buttonGetGroup.Click += new System.EventHandler(this.buttonGetGroup_Click);
            // 
            // buttonViewGroup
            // 
            this.buttonViewGroup.Location = new System.Drawing.Point(4, 33);
            this.buttonViewGroup.Name = "buttonViewGroup";
            this.buttonViewGroup.Size = new System.Drawing.Size(80, 22);
            this.buttonViewGroup.TabIndex = 8;
            this.buttonViewGroup.Text = "View Group";
            this.buttonViewGroup.UseVisualStyleBackColor = true;
            this.buttonViewGroup.Click += new System.EventHandler(this.buttonViewGroup_Click);
            // 
            // buttonAddGroup
            // 
            this.buttonAddGroup.Location = new System.Drawing.Point(4, 4);
            this.buttonAddGroup.Name = "buttonAddGroup";
            this.buttonAddGroup.Size = new System.Drawing.Size(80, 22);
            this.buttonAddGroup.TabIndex = 0;
            this.buttonAddGroup.Text = "Add Group";
            this.buttonAddGroup.UseVisualStyleBackColor = true;
            this.buttonAddGroup.Click += new System.EventHandler(this.buttonAddGroup_Click);
            // 
            // BasicClusterTab
            // 
            this.BasicClusterTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BasicClusterTab.Controls.Add(this.textBoxBasicResetDstEP);
            this.BasicClusterTab.Controls.Add(this.textBoxBasicResetSrcEP);
            this.BasicClusterTab.Controls.Add(this.textBoxBasicResetTargetAddr);
            this.BasicClusterTab.Controls.Add(this.comboBoxBasicResetTargetAddrMode);
            this.BasicClusterTab.Controls.Add(this.buttonBasicReset);
            this.BasicClusterTab.Location = new System.Drawing.Point(4, 22);
            this.BasicClusterTab.Name = "BasicClusterTab";
            this.BasicClusterTab.Size = new System.Drawing.Size(1833, 584);
            this.BasicClusterTab.TabIndex = 15;
            this.BasicClusterTab.Text = "Basic Cluster";
            this.BasicClusterTab.Click += new System.EventHandler(this.BasicClusterTab_Click);
            // 
            // textBoxBasicResetDstEP
            // 
            this.textBoxBasicResetDstEP.Location = new System.Drawing.Point(442, 6);
            this.textBoxBasicResetDstEP.Name = "textBoxBasicResetDstEP";
            this.textBoxBasicResetDstEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxBasicResetDstEP.TabIndex = 4;
            this.textBoxBasicResetDstEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxBasicResetDstEP_MouseClick);
            this.textBoxBasicResetDstEP.Leave += new System.EventHandler(this.textBoxBasicResetDstEP_Leave);
            this.textBoxBasicResetDstEP.MouseLeave += new System.EventHandler(this.textBoxBasicResetDstEP_MouseLeave);
            this.textBoxBasicResetDstEP.MouseHover += new System.EventHandler(this.textBoxBasicResetDstEP_MouseHover);
            // 
            // textBoxBasicResetSrcEP
            // 
            this.textBoxBasicResetSrcEP.Location = new System.Drawing.Point(329, 6);
            this.textBoxBasicResetSrcEP.Name = "textBoxBasicResetSrcEP";
            this.textBoxBasicResetSrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxBasicResetSrcEP.TabIndex = 3;
            this.textBoxBasicResetSrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxBasicResetSrcEP_MouseClick);
            this.textBoxBasicResetSrcEP.Leave += new System.EventHandler(this.textBoxBasicResetSrcEP_Leave);
            this.textBoxBasicResetSrcEP.MouseLeave += new System.EventHandler(this.textBoxBasicResetSrcEP_MouseLeave);
            this.textBoxBasicResetSrcEP.MouseHover += new System.EventHandler(this.textBoxBasicResetSrcEP_MouseHover);
            // 
            // textBoxBasicResetTargetAddr
            // 
            this.textBoxBasicResetTargetAddr.Location = new System.Drawing.Point(215, 6);
            this.textBoxBasicResetTargetAddr.Name = "textBoxBasicResetTargetAddr";
            this.textBoxBasicResetTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxBasicResetTargetAddr.TabIndex = 2;
            this.textBoxBasicResetTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxBasicResetTargetAddr_MouseClick);
            this.textBoxBasicResetTargetAddr.TextChanged += new System.EventHandler(this.textBoxBasicResetTargetAddr_TextChanged);
            this.textBoxBasicResetTargetAddr.Leave += new System.EventHandler(this.textBoxBasicResetTargetAddr_Leave);
            this.textBoxBasicResetTargetAddr.MouseLeave += new System.EventHandler(this.textBoxBasicResetTargetAddr_MouseLeave);
            this.textBoxBasicResetTargetAddr.MouseHover += new System.EventHandler(this.textBoxBasicResetTargetAddr_MouseHover);
            // 
            // comboBoxBasicResetTargetAddrMode
            // 
            this.comboBoxBasicResetTargetAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBasicResetTargetAddrMode.FormattingEnabled = true;
            this.comboBoxBasicResetTargetAddrMode.Location = new System.Drawing.Point(102, 6);
            this.comboBoxBasicResetTargetAddrMode.Name = "comboBoxBasicResetTargetAddrMode";
            this.comboBoxBasicResetTargetAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxBasicResetTargetAddrMode.TabIndex = 1;
            this.comboBoxBasicResetTargetAddrMode.MouseLeave += new System.EventHandler(this.comboBoxBasicResetTargetAddrMode_MouseLeave);
            this.comboBoxBasicResetTargetAddrMode.MouseHover += new System.EventHandler(this.comboBoxBasicResetTargetAddrMode_MouseHover);
            // 
            // buttonBasicReset
            // 
            this.buttonBasicReset.Location = new System.Drawing.Point(3, 3);
            this.buttonBasicReset.Name = "buttonBasicReset";
            this.buttonBasicReset.Size = new System.Drawing.Size(93, 25);
            this.buttonBasicReset.TabIndex = 0;
            this.buttonBasicReset.Text = "Reset To FD";
            this.buttonBasicReset.UseVisualStyleBackColor = true;
            this.buttonBasicReset.Click += new System.EventHandler(this.buttonBasicReset_Click);
            // 
            // AHIControl
            // 
            this.AHIControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.AHIControl.Controls.Add(this.textBoxAHITxPower);
            this.AHIControl.Controls.Add(this.textBoxIPNConfigDioTxConfInDioMask);
            this.AHIControl.Controls.Add(this.textBoxDioSetOutputOffPinMask);
            this.AHIControl.Controls.Add(this.textBoxDioSetOutputOnPinMask);
            this.AHIControl.Controls.Add(this.textBoxDioSetDirectionOutputPinMask);
            this.AHIControl.Controls.Add(this.textBoxDioSetDirectionInputPinMask);
            this.AHIControl.Controls.Add(this.textBoxIPNConfigPollPeriod);
            this.AHIControl.Controls.Add(this.textBoxIPNConfigDioStatusOutDioMask);
            this.AHIControl.Controls.Add(this.textBoxIPNConfigDioRfActiveOutDioMask);
            this.AHIControl.Controls.Add(this.buttonAHISetTxPower);
            this.AHIControl.Controls.Add(this.labelUnimplemented);
            this.AHIControl.Controls.Add(this.comboBoxIPNConfigTimerId);
            this.AHIControl.Controls.Add(this.buttonDioSetOutput);
            this.AHIControl.Controls.Add(this.buttonDioSetDirection);
            this.AHIControl.Controls.Add(this.comboBoxIPNConfigRegisterCallback);
            this.AHIControl.Controls.Add(this.comboBoxIPNConfigEnable);
            this.AHIControl.Controls.Add(this.buttonInPacketNotification);
            this.AHIControl.Location = new System.Drawing.Point(4, 22);
            this.AHIControl.Name = "AHIControl";
            this.AHIControl.Padding = new System.Windows.Forms.Padding(3);
            this.AHIControl.Size = new System.Drawing.Size(1833, 584);
            this.AHIControl.TabIndex = 16;
            this.AHIControl.Text = "AHI Control";
            this.AHIControl.Click += new System.EventHandler(this.AHIControl_Click);
            // 
            // textBoxAHITxPower
            // 
            this.textBoxAHITxPower.Location = new System.Drawing.Point(94, 94);
            this.textBoxAHITxPower.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAHITxPower.Name = "textBoxAHITxPower";
            this.textBoxAHITxPower.Size = new System.Drawing.Size(106, 20);
            this.textBoxAHITxPower.TabIndex = 17;
            this.textBoxAHITxPower.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxAHITxPower_MouseClick);
            this.textBoxAHITxPower.Leave += new System.EventHandler(this.textBoxAHITxPower_Leave);
            this.textBoxAHITxPower.MouseLeave += new System.EventHandler(this.textBoxAHITxPower_MouseLeave);
            this.textBoxAHITxPower.MouseHover += new System.EventHandler(this.textBoxAHITxPower_MouseHover);
            // 
            // textBoxIPNConfigDioTxConfInDioMask
            // 
            this.textBoxIPNConfigDioTxConfInDioMask.Location = new System.Drawing.Point(413, 68);
            this.textBoxIPNConfigDioTxConfInDioMask.Name = "textBoxIPNConfigDioTxConfInDioMask";
            this.textBoxIPNConfigDioTxConfInDioMask.Size = new System.Drawing.Size(106, 20);
            this.textBoxIPNConfigDioTxConfInDioMask.TabIndex = 10;
            this.textBoxIPNConfigDioTxConfInDioMask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIPNConfigDioTxConfInDioMask_MouseClick);
            this.textBoxIPNConfigDioTxConfInDioMask.Leave += new System.EventHandler(this.textBoxIPNConfigDioTxConfInDioMask_Leave);
            this.textBoxIPNConfigDioTxConfInDioMask.MouseLeave += new System.EventHandler(this.textBoxIPNConfigDioTxConfInDioMask_MouseLeave);
            this.textBoxIPNConfigDioTxConfInDioMask.MouseHover += new System.EventHandler(this.textBoxIPNConfigDioTxConfInDioMask_MouseHover);
            // 
            // textBoxDioSetOutputOffPinMask
            // 
            this.textBoxDioSetOutputOffPinMask.Location = new System.Drawing.Point(207, 38);
            this.textBoxDioSetOutputOffPinMask.Name = "textBoxDioSetOutputOffPinMask";
            this.textBoxDioSetOutputOffPinMask.Size = new System.Drawing.Size(106, 20);
            this.textBoxDioSetOutputOffPinMask.TabIndex = 5;
            this.textBoxDioSetOutputOffPinMask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDioSetOutputOffPinMask_MouseClick);
            this.textBoxDioSetOutputOffPinMask.Leave += new System.EventHandler(this.textBoxDioSetOutputOffPinMask_Leave);
            this.textBoxDioSetOutputOffPinMask.MouseLeave += new System.EventHandler(this.textBoxDioSetOutputOffPinMask_MouseLeave);
            this.textBoxDioSetOutputOffPinMask.MouseHover += new System.EventHandler(this.textBoxDioSetOutputOffPinMask_MouseHover);
            // 
            // textBoxDioSetOutputOnPinMask
            // 
            this.textBoxDioSetOutputOnPinMask.Location = new System.Drawing.Point(94, 38);
            this.textBoxDioSetOutputOnPinMask.Name = "textBoxDioSetOutputOnPinMask";
            this.textBoxDioSetOutputOnPinMask.Size = new System.Drawing.Size(106, 20);
            this.textBoxDioSetOutputOnPinMask.TabIndex = 4;
            this.textBoxDioSetOutputOnPinMask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDioSetOutputOnPinMask_MouseClick);
            this.textBoxDioSetOutputOnPinMask.Leave += new System.EventHandler(this.textBoxDioSetOutputOnPinMask_Leave);
            this.textBoxDioSetOutputOnPinMask.MouseLeave += new System.EventHandler(this.textBoxDioSetOutputOnPinMask_MouseLeave);
            this.textBoxDioSetOutputOnPinMask.MouseHover += new System.EventHandler(this.textBoxDioSetOutputOnPinMask_MouseHover);
            // 
            // textBoxDioSetDirectionOutputPinMask
            // 
            this.textBoxDioSetDirectionOutputPinMask.Location = new System.Drawing.Point(207, 10);
            this.textBoxDioSetDirectionOutputPinMask.Name = "textBoxDioSetDirectionOutputPinMask";
            this.textBoxDioSetDirectionOutputPinMask.Size = new System.Drawing.Size(106, 20);
            this.textBoxDioSetDirectionOutputPinMask.TabIndex = 2;
            this.textBoxDioSetDirectionOutputPinMask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDioSetDirectionOutputPinMask_MouseClick);
            this.textBoxDioSetDirectionOutputPinMask.Leave += new System.EventHandler(this.textBoxDioSetDirectionOutputPinMask_Leave);
            this.textBoxDioSetDirectionOutputPinMask.MouseLeave += new System.EventHandler(this.textBoxDioSetDirectionOutputPinMask_MouseLeave);
            this.textBoxDioSetDirectionOutputPinMask.MouseHover += new System.EventHandler(this.textBoxDioSetDirectionOutputPinMask_MouseHover);
            // 
            // textBoxDioSetDirectionInputPinMask
            // 
            this.textBoxDioSetDirectionInputPinMask.Location = new System.Drawing.Point(94, 10);
            this.textBoxDioSetDirectionInputPinMask.Name = "textBoxDioSetDirectionInputPinMask";
            this.textBoxDioSetDirectionInputPinMask.Size = new System.Drawing.Size(106, 20);
            this.textBoxDioSetDirectionInputPinMask.TabIndex = 1;
            this.textBoxDioSetDirectionInputPinMask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDioSetDirectionInputPinMask_MouseClick);
            this.textBoxDioSetDirectionInputPinMask.Leave += new System.EventHandler(this.textBoxDioSetDirectionInputPinMask_Leave);
            this.textBoxDioSetDirectionInputPinMask.MouseLeave += new System.EventHandler(this.textBoxDioSetDirectionInputPinMask_MouseLeave);
            this.textBoxDioSetDirectionInputPinMask.MouseHover += new System.EventHandler(this.textBoxDioSetDirectionInputPinMask_MouseHover);
            // 
            // textBoxIPNConfigPollPeriod
            // 
            this.textBoxIPNConfigPollPeriod.Location = new System.Drawing.Point(618, 68);
            this.textBoxIPNConfigPollPeriod.Name = "textBoxIPNConfigPollPeriod";
            this.textBoxIPNConfigPollPeriod.Size = new System.Drawing.Size(106, 20);
            this.textBoxIPNConfigPollPeriod.TabIndex = 12;
            this.textBoxIPNConfigPollPeriod.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIPNConfigPollPeriod_MouseClick);
            this.textBoxIPNConfigPollPeriod.Leave += new System.EventHandler(this.textBoxIPNConfigPollPeriod_Leave);
            this.textBoxIPNConfigPollPeriod.MouseLeave += new System.EventHandler(this.textBoxIPNConfigPollPeriod_MouseLeave);
            this.textBoxIPNConfigPollPeriod.MouseHover += new System.EventHandler(this.textBoxIPNConfigPollPeriod_MouseHover);
            // 
            // textBoxIPNConfigDioStatusOutDioMask
            // 
            this.textBoxIPNConfigDioStatusOutDioMask.Location = new System.Drawing.Point(300, 68);
            this.textBoxIPNConfigDioStatusOutDioMask.Name = "textBoxIPNConfigDioStatusOutDioMask";
            this.textBoxIPNConfigDioStatusOutDioMask.Size = new System.Drawing.Size(106, 20);
            this.textBoxIPNConfigDioStatusOutDioMask.TabIndex = 9;
            this.textBoxIPNConfigDioStatusOutDioMask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIPNConfigDioStatusOutDioMask_MouseClick);
            this.textBoxIPNConfigDioStatusOutDioMask.Leave += new System.EventHandler(this.textBoxIPNConfigDioStatusOutDioMask_Leave);
            this.textBoxIPNConfigDioStatusOutDioMask.MouseLeave += new System.EventHandler(this.textBoxIPNConfigDioStatusOutDioMask_MouseLeave);
            this.textBoxIPNConfigDioStatusOutDioMask.MouseHover += new System.EventHandler(this.textBoxIPNConfigDioStatusOutDioMask_MouseHover);
            // 
            // textBoxIPNConfigDioRfActiveOutDioMask
            // 
            this.textBoxIPNConfigDioRfActiveOutDioMask.Location = new System.Drawing.Point(186, 68);
            this.textBoxIPNConfigDioRfActiveOutDioMask.Name = "textBoxIPNConfigDioRfActiveOutDioMask";
            this.textBoxIPNConfigDioRfActiveOutDioMask.Size = new System.Drawing.Size(106, 20);
            this.textBoxIPNConfigDioRfActiveOutDioMask.TabIndex = 8;
            this.textBoxIPNConfigDioRfActiveOutDioMask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIPNConfigDioRfActiveOutDioMask_MouseClick);
            this.textBoxIPNConfigDioRfActiveOutDioMask.Leave += new System.EventHandler(this.textBoxIPNConfigDioRfActiveOutDioMask_Leave);
            this.textBoxIPNConfigDioRfActiveOutDioMask.MouseLeave += new System.EventHandler(this.textBoxIPNConfigDioRfActiveOutDioMask_MouseLeave);
            this.textBoxIPNConfigDioRfActiveOutDioMask.MouseHover += new System.EventHandler(this.textBoxIPNConfigDioRfActiveOutDioMask_MouseHover);
            // 
            // buttonAHISetTxPower
            // 
            this.buttonAHISetTxPower.Location = new System.Drawing.Point(7, 93);
            this.buttonAHISetTxPower.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAHISetTxPower.Name = "buttonAHISetTxPower";
            this.buttonAHISetTxPower.Size = new System.Drawing.Size(79, 22);
            this.buttonAHISetTxPower.TabIndex = 16;
            this.buttonAHISetTxPower.Text = "TX Power";
            this.buttonAHISetTxPower.UseVisualStyleBackColor = true;
            this.buttonAHISetTxPower.Click += new System.EventHandler(this.buttonAHISetTxPower_Click);
            // 
            // labelUnimplemented
            // 
            this.labelUnimplemented.AutoSize = true;
            this.labelUnimplemented.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUnimplemented.Location = new System.Drawing.Point(518, 185);
            this.labelUnimplemented.Name = "labelUnimplemented";
            this.labelUnimplemented.Size = new System.Drawing.Size(311, 46);
            this.labelUnimplemented.TabIndex = 15;
            this.labelUnimplemented.Text = "Unimplemented";
            // 
            // comboBoxIPNConfigTimerId
            // 
            this.comboBoxIPNConfigTimerId.FormattingEnabled = true;
            this.comboBoxIPNConfigTimerId.Items.AddRange(new object[] {
            "TIMER 0",
            "TIMER 1",
            "TIMER 2",
            "TIMER 3",
            "TIMER 4"});
            this.comboBoxIPNConfigTimerId.Location = new System.Drawing.Point(732, 68);
            this.comboBoxIPNConfigTimerId.Name = "comboBoxIPNConfigTimerId";
            this.comboBoxIPNConfigTimerId.Size = new System.Drawing.Size(85, 21);
            this.comboBoxIPNConfigTimerId.TabIndex = 13;
            this.comboBoxIPNConfigTimerId.MouseLeave += new System.EventHandler(this.comboBoxIPNConfigTimerId_MouseLeave);
            this.comboBoxIPNConfigTimerId.MouseHover += new System.EventHandler(this.comboBoxIPNConfigTimerId_MouseHover);
            // 
            // buttonDioSetOutput
            // 
            this.buttonDioSetOutput.Location = new System.Drawing.Point(7, 37);
            this.buttonDioSetOutput.Name = "buttonDioSetOutput";
            this.buttonDioSetOutput.Size = new System.Drawing.Size(80, 22);
            this.buttonDioSetOutput.TabIndex = 3;
            this.buttonDioSetOutput.Text = "DIO Set";
            this.buttonDioSetOutput.UseVisualStyleBackColor = true;
            this.buttonDioSetOutput.Click += new System.EventHandler(this.buttonDioSetOutput_Click);
            // 
            // buttonDioSetDirection
            // 
            this.buttonDioSetDirection.Location = new System.Drawing.Point(7, 8);
            this.buttonDioSetDirection.Name = "buttonDioSetDirection";
            this.buttonDioSetDirection.Size = new System.Drawing.Size(80, 22);
            this.buttonDioSetDirection.TabIndex = 0;
            this.buttonDioSetDirection.Text = "DIO Set Dir";
            this.buttonDioSetDirection.UseVisualStyleBackColor = true;
            this.buttonDioSetDirection.Click += new System.EventHandler(this.buttonDioSetDirection_Click);
            // 
            // comboBoxIPNConfigRegisterCallback
            // 
            this.comboBoxIPNConfigRegisterCallback.FormattingEnabled = true;
            this.comboBoxIPNConfigRegisterCallback.Items.AddRange(new object[] {
            "DISABLED",
            "ENABLED"});
            this.comboBoxIPNConfigRegisterCallback.Location = new System.Drawing.Point(526, 68);
            this.comboBoxIPNConfigRegisterCallback.Name = "comboBoxIPNConfigRegisterCallback";
            this.comboBoxIPNConfigRegisterCallback.Size = new System.Drawing.Size(85, 21);
            this.comboBoxIPNConfigRegisterCallback.TabIndex = 11;
            this.comboBoxIPNConfigRegisterCallback.MouseLeave += new System.EventHandler(this.comboBoxIPNConfigRegisterCallback_MouseLeave);
            this.comboBoxIPNConfigRegisterCallback.MouseHover += new System.EventHandler(this.comboBoxIPNConfigRegisterCallback_MouseHover);
            // 
            // comboBoxIPNConfigEnable
            // 
            this.comboBoxIPNConfigEnable.FormattingEnabled = true;
            this.comboBoxIPNConfigEnable.Items.AddRange(new object[] {
            "DISABLE",
            "ENABLE"});
            this.comboBoxIPNConfigEnable.Location = new System.Drawing.Point(94, 67);
            this.comboBoxIPNConfigEnable.Name = "comboBoxIPNConfigEnable";
            this.comboBoxIPNConfigEnable.Size = new System.Drawing.Size(85, 21);
            this.comboBoxIPNConfigEnable.TabIndex = 7;
            this.comboBoxIPNConfigEnable.MouseLeave += new System.EventHandler(this.comboBoxIPNConfigEnable_MouseLeave);
            this.comboBoxIPNConfigEnable.MouseHover += new System.EventHandler(this.comboBoxIPNConfigEnable_MouseHover);
            // 
            // buttonInPacketNotification
            // 
            this.buttonInPacketNotification.Location = new System.Drawing.Point(7, 65);
            this.buttonInPacketNotification.Name = "buttonInPacketNotification";
            this.buttonInPacketNotification.Size = new System.Drawing.Size(80, 22);
            this.buttonInPacketNotification.TabIndex = 6;
            this.buttonInPacketNotification.Text = "IPN Config";
            this.buttonInPacketNotification.UseVisualStyleBackColor = true;
            this.buttonInPacketNotification.Click += new System.EventHandler(this.buttonInPacketNotification_Click);
            // 
            // tabPage12
            // 
            this.tabPage12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage12.Controls.Add(this.buttonGeneralPrintExistInstallCode);
            this.tabPage12.Controls.Add(this.textBoxGeneralInstallCodeCode);
            this.tabPage12.Controls.Add(this.textBoxGeneralInstallCodeMACaddress);
            this.tabPage12.Controls.Add(this.textBoxOOBDataKey);
            this.tabPage12.Controls.Add(this.textBoxOOBDataAddr);
            this.tabPage12.Controls.Add(this.textBoxDiscoverAttributesStartAttrId);
            this.tabPage12.Controls.Add(this.textBoxRawDataCommandsProfileID);
            this.tabPage12.Controls.Add(this.textBoxRawDataCommandsSecurityMode);
            this.tabPage12.Controls.Add(this.textBoxRawDataCommandsRadius);
            this.tabPage12.Controls.Add(this.textBoxRawDataCommandsData);
            this.tabPage12.Controls.Add(this.textBoxRawDataCommandsClusterID);
            this.tabPage12.Controls.Add(this.textBoxRawDataCommandsDstEP);
            this.tabPage12.Controls.Add(this.textBoxRawDataCommandsSrcEP);
            this.tabPage12.Controls.Add(this.textBoxRawDataCommandsTargetAddr);
            this.tabPage12.Controls.Add(this.textBoxMgmtNwkUpdateNwkManagerAddr);
            this.tabPage12.Controls.Add(this.textBoxDiscoverCommandsMaxCommands);
            this.tabPage12.Controls.Add(this.textBoxDiscoverCommandsManuID);
            this.tabPage12.Controls.Add(this.textBoxDiscoverCommandsCommandID);
            this.tabPage12.Controls.Add(this.textBoxDiscoverCommandsClusterID);
            this.tabPage12.Controls.Add(this.textBoxDiscoverCommandsDstEP);
            this.tabPage12.Controls.Add(this.textBoxDiscoverCommandsSrcEP);
            this.tabPage12.Controls.Add(this.textBoxDiscoverCommandsTargetAddr);
            this.tabPage12.Controls.Add(this.textBoxMgmtNwkUpdateScanCount);
            this.tabPage12.Controls.Add(this.textBoxMgmtNwkUpdateScanDuration);
            this.tabPage12.Controls.Add(this.textBoxMgmtNwkUpdateChannelMask);
            this.tabPage12.Controls.Add(this.textBoxMgmtNwkUpdateTargetAddr);
            this.tabPage12.Controls.Add(this.textBoxManyToOneRouteRequesRadius);
            this.tabPage12.Controls.Add(this.textBoxReadReportConfigAttribID);
            this.tabPage12.Controls.Add(this.textBoxReadReportConfigClusterID);
            this.tabPage12.Controls.Add(this.textBoxReadReportConfigDstEP);
            this.tabPage12.Controls.Add(this.textBoxReadReportConfigSrcEP);
            this.tabPage12.Controls.Add(this.textBoxReadReportConfigTargetAddr);
            this.tabPage12.Controls.Add(this.textBoxWriteAttribManuID);
            this.tabPage12.Controls.Add(this.textBoxWriteAttribDataType);
            this.tabPage12.Controls.Add(this.textBoxReadAttribManuID);
            this.tabPage12.Controls.Add(this.textBoxWriteAttribData);
            this.tabPage12.Controls.Add(this.textBoxWriteAttribID);
            this.tabPage12.Controls.Add(this.textBoxWriteAttribClusterID);
            this.tabPage12.Controls.Add(this.textBoxWriteAttribDstEP);
            this.tabPage12.Controls.Add(this.textBoxWriteAttribSrcEP);
            this.tabPage12.Controls.Add(this.textBoxWriteAttribTargetAddr);
            this.tabPage12.Controls.Add(this.textBoxConfigReportChange);
            this.tabPage12.Controls.Add(this.textBoxConfigReportTimeOut);
            this.tabPage12.Controls.Add(this.textBoxConfigReportMaxInterval);
            this.tabPage12.Controls.Add(this.textBoxDiscoverAttributesMaxIDs);
            this.tabPage12.Controls.Add(this.textBoxDiscoverAttributesClusterID);
            this.tabPage12.Controls.Add(this.textBoxDiscoverAttributesDstEp);
            this.tabPage12.Controls.Add(this.textBoxDiscoverAttributesSrcEp);
            this.tabPage12.Controls.Add(this.textBoxDiscoverAttributesAddr);
            this.tabPage12.Controls.Add(this.textBoxReadAllAttribClusterID);
            this.tabPage12.Controls.Add(this.textBoxReadAllAttribDstEP);
            this.tabPage12.Controls.Add(this.textBoxReadAllAttribSrcEP);
            this.tabPage12.Controls.Add(this.textBoxReadAllAttribAddr);
            this.tabPage12.Controls.Add(this.textBoxConfigReportAttribType);
            this.tabPage12.Controls.Add(this.textBoxConfigReportMinInterval);
            this.tabPage12.Controls.Add(this.textBoxConfigReportAttribID);
            this.tabPage12.Controls.Add(this.textBoxConfigReportClusterID);
            this.tabPage12.Controls.Add(this.textBoxConfigReportDstEP);
            this.tabPage12.Controls.Add(this.textBoxConfigReportSrcEP);
            this.tabPage12.Controls.Add(this.textBoxConfigReportTargetAddr);
            this.tabPage12.Controls.Add(this.textBoxReadAttribCount);
            this.tabPage12.Controls.Add(this.textBoxReadAttribID1);
            this.tabPage12.Controls.Add(this.textBoxReadAttribClusterID);
            this.tabPage12.Controls.Add(this.textBoxReadAttribDstEP);
            this.tabPage12.Controls.Add(this.textBoxReadAttribSrcEP);
            this.tabPage12.Controls.Add(this.textBoxReadAttribTargetAddr);
            this.tabPage12.Controls.Add(this.buttonGeneralSendInstallCode);
            this.tabPage12.Controls.Add(this.buttonOOBCommissioningData);
            this.tabPage12.Controls.Add(this.comboBoxRawDataCommandsAddrMode);
            this.tabPage12.Controls.Add(this.buttonRawDataSend);
            this.tabPage12.Controls.Add(this.comboBoxDiscoverCommandsRxGen);
            this.tabPage12.Controls.Add(this.comboBoxDiscoverAttributesExtended);
            this.tabPage12.Controls.Add(this.comboBoxDiscoverCommandsManuSpecific);
            this.tabPage12.Controls.Add(this.comboBoxDiscoverCommandsDirection);
            this.tabPage12.Controls.Add(this.comboBoxDiscoverCommandsAddrMode);
            this.tabPage12.Controls.Add(this.buttonDiscoverCommands);
            this.tabPage12.Controls.Add(this.comboBoxMgmtNwkUpdateAddrMode);
            this.tabPage12.Controls.Add(this.buttonMgmtNwkUpdate);
            this.tabPage12.Controls.Add(this.comboBoxManyToOneRouteRequestCacheRoute);
            this.tabPage12.Controls.Add(this.buttonManyToOneRouteRequest);
            this.tabPage12.Controls.Add(this.comboBoxReadReportConfigDirection);
            this.tabPage12.Controls.Add(this.comboBoxReadReportConfigDirIsRx);
            this.tabPage12.Controls.Add(this.comboBoxReadReportConfigAddrMode);
            this.tabPage12.Controls.Add(this.buttonReadReportConfig);
            this.tabPage12.Controls.Add(this.comboBoxWriteAttribManuSpecific);
            this.tabPage12.Controls.Add(this.comboBoxReadAttribManuSpecific);
            this.tabPage12.Controls.Add(this.comboBoxConfigReportAddrMode);
            this.tabPage12.Controls.Add(this.comboBoxWriteAttribDirection);
            this.tabPage12.Controls.Add(this.comboBoxDiscoverAttributesDirection);
            this.tabPage12.Controls.Add(this.buttonDiscoverAttributes);
            this.tabPage12.Controls.Add(this.comboBoxReadAllAttribDirection);
            this.tabPage12.Controls.Add(this.buttonReadAllAttrib);
            this.tabPage12.Controls.Add(this.comboBoxConfigReportAttribDirection);
            this.tabPage12.Controls.Add(this.comboBoxConfigReportDirection);
            this.tabPage12.Controls.Add(this.buttonConfigReport);
            this.tabPage12.Controls.Add(this.buttonWriteAttrib);
            this.tabPage12.Controls.Add(this.comboBoxReadAttribDirection);
            this.tabPage12.Controls.Add(this.buttonReadAttrib);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(1833, 584);
            this.tabPage12.TabIndex = 12;
            this.tabPage12.Text = "General";
            this.tabPage12.Click += new System.EventHandler(this.tabPage12_Click);
            // 
            // buttonGeneralPrintExistInstallCode
            // 
            this.buttonGeneralPrintExistInstallCode.Location = new System.Drawing.Point(658, 380);
            this.buttonGeneralPrintExistInstallCode.Name = "buttonGeneralPrintExistInstallCode";
            this.buttonGeneralPrintExistInstallCode.Size = new System.Drawing.Size(145, 23);
            this.buttonGeneralPrintExistInstallCode.TabIndex = 103;
            this.buttonGeneralPrintExistInstallCode.Text = "All Install Code";
            this.buttonGeneralPrintExistInstallCode.UseVisualStyleBackColor = true;
            this.buttonGeneralPrintExistInstallCode.Click += new System.EventHandler(this.buttonGeneralPrintExistInstallCode_Click);
            // 
            // textBoxGeneralInstallCodeCode
            // 
            this.textBoxGeneralInstallCodeCode.Location = new System.Drawing.Point(301, 380);
            this.textBoxGeneralInstallCodeCode.Name = "textBoxGeneralInstallCodeCode";
            this.textBoxGeneralInstallCodeCode.Size = new System.Drawing.Size(211, 20);
            this.textBoxGeneralInstallCodeCode.TabIndex = 100;
            this.textBoxGeneralInstallCodeCode.Click += new System.EventHandler(this.textBoxGeneralInstallCodeCode_Click);
            this.textBoxGeneralInstallCodeCode.TextChanged += new System.EventHandler(this.textBoxGeneralInstallCodeCode_TextChanged);
            this.textBoxGeneralInstallCodeCode.Leave += new System.EventHandler(this.textBoxGeneralInstallCodeCode_Leave);
            this.textBoxGeneralInstallCodeCode.MouseLeave += new System.EventHandler(this.textBoxGeneralInstallCodeCode_MouseLeave);
            this.textBoxGeneralInstallCodeCode.MouseHover += new System.EventHandler(this.textBoxGeneralInstallCodeCode_MouseHover);
            // 
            // textBoxGeneralInstallCodeMACaddress
            // 
            this.textBoxGeneralInstallCodeMACaddress.Location = new System.Drawing.Point(158, 380);
            this.textBoxGeneralInstallCodeMACaddress.Name = "textBoxGeneralInstallCodeMACaddress";
            this.textBoxGeneralInstallCodeMACaddress.Size = new System.Drawing.Size(137, 20);
            this.textBoxGeneralInstallCodeMACaddress.TabIndex = 99;
            this.textBoxGeneralInstallCodeMACaddress.Click += new System.EventHandler(this.textBoxGeneralInstallCodeMACaddress_Click);
            this.textBoxGeneralInstallCodeMACaddress.TextChanged += new System.EventHandler(this.textBoxGeneralInstallCodeMACaddress_TextChanged);
            this.textBoxGeneralInstallCodeMACaddress.Leave += new System.EventHandler(this.textBoxGeneralInstallCodeMACaddress_Leave);
            this.textBoxGeneralInstallCodeMACaddress.MouseLeave += new System.EventHandler(this.textBoxGeneralInstallCodeMACaddress_MouseLeave);
            this.textBoxGeneralInstallCodeMACaddress.MouseHover += new System.EventHandler(this.textBoxGeneralInstallCodeMACaddress_MouseHover);
            // 
            // textBoxOOBDataKey
            // 
            this.textBoxOOBDataKey.Location = new System.Drawing.Point(270, 350);
            this.textBoxOOBDataKey.Name = "textBoxOOBDataKey";
            this.textBoxOOBDataKey.Size = new System.Drawing.Size(266, 20);
            this.textBoxOOBDataKey.TabIndex = 96;
            this.textBoxOOBDataKey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOOBDataKey_MouseClick);
            this.textBoxOOBDataKey.Leave += new System.EventHandler(this.textBoxOOBDataKey_Leave);
            this.textBoxOOBDataKey.MouseLeave += new System.EventHandler(this.textBoxOOBDataKey_MouseLeave);
            this.textBoxOOBDataKey.MouseHover += new System.EventHandler(this.textBoxOOBDataKey_MouseHover);
            // 
            // textBoxOOBDataAddr
            // 
            this.textBoxOOBDataAddr.Location = new System.Drawing.Point(158, 350);
            this.textBoxOOBDataAddr.Name = "textBoxOOBDataAddr";
            this.textBoxOOBDataAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxOOBDataAddr.TabIndex = 95;
            this.textBoxOOBDataAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOOBDataAddr_MouseClick);
            this.textBoxOOBDataAddr.Leave += new System.EventHandler(this.textBoxOOBDataAddr_Leave);
            this.textBoxOOBDataAddr.MouseLeave += new System.EventHandler(this.textBoxOOBDataAddr_MouseLeave);
            this.textBoxOOBDataAddr.MouseHover += new System.EventHandler(this.textBoxOOBDataAddr_MouseHover);
            // 
            // textBoxDiscoverAttributesStartAttrId
            // 
            this.textBoxDiscoverAttributesStartAttrId.Location = new System.Drawing.Point(541, 210);
            this.textBoxDiscoverAttributesStartAttrId.Name = "textBoxDiscoverAttributesStartAttrId";
            this.textBoxDiscoverAttributesStartAttrId.Size = new System.Drawing.Size(110, 20);
            this.textBoxDiscoverAttributesStartAttrId.TabIndex = 55;
            this.textBoxDiscoverAttributesStartAttrId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverAttributesStartAttrId_MouseClick);
            this.textBoxDiscoverAttributesStartAttrId.Leave += new System.EventHandler(this.textBoxDiscoverAttributesStartAttrId_Leave);
            this.textBoxDiscoverAttributesStartAttrId.MouseLeave += new System.EventHandler(this.textBoxDiscoverAttributesStartAttrId_MouseLeave);
            this.textBoxDiscoverAttributesStartAttrId.MouseHover += new System.EventHandler(this.textBoxDiscoverAttributesStartAttrId_MouseHover);
            // 
            // textBoxRawDataCommandsProfileID
            // 
            this.textBoxRawDataCommandsProfileID.Location = new System.Drawing.Point(541, 320);
            this.textBoxRawDataCommandsProfileID.Name = "textBoxRawDataCommandsProfileID";
            this.textBoxRawDataCommandsProfileID.Size = new System.Drawing.Size(110, 20);
            this.textBoxRawDataCommandsProfileID.TabIndex = 86;
            this.textBoxRawDataCommandsProfileID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRawDataCommandsProfileID_MouseClick);
            this.textBoxRawDataCommandsProfileID.Leave += new System.EventHandler(this.textBoxRawDataCommandsProfileID_Leave);
            this.textBoxRawDataCommandsProfileID.MouseLeave += new System.EventHandler(this.textBoxRawDataCommandsProfileID_MouseLeave);
            this.textBoxRawDataCommandsProfileID.MouseHover += new System.EventHandler(this.textBoxRawDataCommandsProfileID_MouseHover);
            // 
            // textBoxRawDataCommandsSecurityMode
            // 
            this.textBoxRawDataCommandsSecurityMode.Location = new System.Drawing.Point(884, 320);
            this.textBoxRawDataCommandsSecurityMode.Name = "textBoxRawDataCommandsSecurityMode";
            this.textBoxRawDataCommandsSecurityMode.Size = new System.Drawing.Size(208, 20);
            this.textBoxRawDataCommandsSecurityMode.TabIndex = 89;
            this.textBoxRawDataCommandsSecurityMode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRawDataCommandsSecurityMode_MouseClick);
            this.textBoxRawDataCommandsSecurityMode.Leave += new System.EventHandler(this.textBoxRawDataCommandsSecurityMode_Leave);
            this.textBoxRawDataCommandsSecurityMode.MouseLeave += new System.EventHandler(this.textBoxRawDataCommandsSecurityMode_MouseLeave);
            this.textBoxRawDataCommandsSecurityMode.MouseHover += new System.EventHandler(this.textBoxRawDataCommandsSecurityMode_MouseHover);
            // 
            // textBoxRawDataCommandsRadius
            // 
            this.textBoxRawDataCommandsRadius.Location = new System.Drawing.Point(771, 320);
            this.textBoxRawDataCommandsRadius.Name = "textBoxRawDataCommandsRadius";
            this.textBoxRawDataCommandsRadius.Size = new System.Drawing.Size(106, 20);
            this.textBoxRawDataCommandsRadius.TabIndex = 88;
            this.textBoxRawDataCommandsRadius.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRawDataCommandsRadius_MouseClick);
            this.textBoxRawDataCommandsRadius.Leave += new System.EventHandler(this.textBoxRawDataCommandsRadius_Leave);
            this.textBoxRawDataCommandsRadius.MouseLeave += new System.EventHandler(this.textBoxRawDataCommandsRadius_MouseLeave);
            this.textBoxRawDataCommandsRadius.MouseHover += new System.EventHandler(this.textBoxRawDataCommandsRadius_MouseHover);
            // 
            // textBoxRawDataCommandsData
            // 
            this.textBoxRawDataCommandsData.Location = new System.Drawing.Point(1098, 320);
            this.textBoxRawDataCommandsData.Name = "textBoxRawDataCommandsData";
            this.textBoxRawDataCommandsData.Size = new System.Drawing.Size(248, 20);
            this.textBoxRawDataCommandsData.TabIndex = 90;
            this.textBoxRawDataCommandsData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRawDataCommandsData_MouseClick);
            this.textBoxRawDataCommandsData.Leave += new System.EventHandler(this.textBoxRawDataCommandsData_Leave);
            this.textBoxRawDataCommandsData.MouseLeave += new System.EventHandler(this.textBoxRawDataCommandsData_MouseLeave);
            this.textBoxRawDataCommandsData.MouseHover += new System.EventHandler(this.textBoxRawDataCommandsData_MouseHover);
            // 
            // textBoxRawDataCommandsClusterID
            // 
            this.textBoxRawDataCommandsClusterID.Location = new System.Drawing.Point(658, 320);
            this.textBoxRawDataCommandsClusterID.Name = "textBoxRawDataCommandsClusterID";
            this.textBoxRawDataCommandsClusterID.Size = new System.Drawing.Size(109, 20);
            this.textBoxRawDataCommandsClusterID.TabIndex = 87;
            this.textBoxRawDataCommandsClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRawDataCommandsClusterID_MouseClick);
            this.textBoxRawDataCommandsClusterID.Leave += new System.EventHandler(this.textBoxRawDataCommandsClusterID_Leave);
            this.textBoxRawDataCommandsClusterID.MouseLeave += new System.EventHandler(this.textBoxRawDataCommandsClusterID_MouseLeave);
            this.textBoxRawDataCommandsClusterID.MouseHover += new System.EventHandler(this.textBoxRawDataCommandsClusterID_MouseHover);
            // 
            // textBoxRawDataCommandsDstEP
            // 
            this.textBoxRawDataCommandsDstEP.Location = new System.Drawing.Point(432, 320);
            this.textBoxRawDataCommandsDstEP.Name = "textBoxRawDataCommandsDstEP";
            this.textBoxRawDataCommandsDstEP.Size = new System.Drawing.Size(103, 20);
            this.textBoxRawDataCommandsDstEP.TabIndex = 85;
            this.textBoxRawDataCommandsDstEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRawDataCommandsDstEP_MouseClick);
            this.textBoxRawDataCommandsDstEP.Leave += new System.EventHandler(this.textBoxRawDataCommandsDstEP_Leave);
            this.textBoxRawDataCommandsDstEP.MouseLeave += new System.EventHandler(this.textBoxRawDataCommandsDstEP_MouseLeave);
            this.textBoxRawDataCommandsDstEP.MouseHover += new System.EventHandler(this.textBoxRawDataCommandsDstEP_MouseHover);
            // 
            // textBoxRawDataCommandsSrcEP
            // 
            this.textBoxRawDataCommandsSrcEP.Location = new System.Drawing.Point(319, 320);
            this.textBoxRawDataCommandsSrcEP.Name = "textBoxRawDataCommandsSrcEP";
            this.textBoxRawDataCommandsSrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxRawDataCommandsSrcEP.TabIndex = 84;
            this.textBoxRawDataCommandsSrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRawDataCommandsSrcEP_MouseClick);
            this.textBoxRawDataCommandsSrcEP.Leave += new System.EventHandler(this.textBoxRawDataCommandsSrcEP_Leave);
            this.textBoxRawDataCommandsSrcEP.MouseLeave += new System.EventHandler(this.textBoxRawDataCommandsSrcEP_MouseLeave);
            this.textBoxRawDataCommandsSrcEP.MouseHover += new System.EventHandler(this.textBoxRawDataCommandsSrcEP_MouseHover);
            // 
            // textBoxRawDataCommandsTargetAddr
            // 
            this.textBoxRawDataCommandsTargetAddr.Location = new System.Drawing.Point(208, 320);
            this.textBoxRawDataCommandsTargetAddr.Name = "textBoxRawDataCommandsTargetAddr";
            this.textBoxRawDataCommandsTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxRawDataCommandsTargetAddr.TabIndex = 83;
            this.textBoxRawDataCommandsTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRawDataCommandsTargetAddr_MouseClick);
            this.textBoxRawDataCommandsTargetAddr.Leave += new System.EventHandler(this.textBoxRawDataCommandsTargetAddr_Leave);
            this.textBoxRawDataCommandsTargetAddr.MouseLeave += new System.EventHandler(this.textBoxRawDataCommandsTargetAddr_MouseLeave);
            this.textBoxRawDataCommandsTargetAddr.MouseHover += new System.EventHandler(this.textBoxRawDataCommandsTargetAddr_MouseHover);
            // 
            // textBoxMgmtNwkUpdateNwkManagerAddr
            // 
            this.textBoxMgmtNwkUpdateNwkManagerAddr.Location = new System.Drawing.Point(658, 267);
            this.textBoxMgmtNwkUpdateNwkManagerAddr.Name = "textBoxMgmtNwkUpdateNwkManagerAddr";
            this.textBoxMgmtNwkUpdateNwkManagerAddr.Size = new System.Drawing.Size(141, 20);
            this.textBoxMgmtNwkUpdateNwkManagerAddr.TabIndex = 68;
            this.textBoxMgmtNwkUpdateNwkManagerAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMgmtNwkUpdateNwkManagerAddr_MouseClick);
            this.textBoxMgmtNwkUpdateNwkManagerAddr.Leave += new System.EventHandler(this.textBoxMgmtNwkUpdateNwkManagerAddr_Leave);
            this.textBoxMgmtNwkUpdateNwkManagerAddr.MouseLeave += new System.EventHandler(this.textBoxMgmtNwkUpdateNwkManagerAddr_MouseLeave);
            this.textBoxMgmtNwkUpdateNwkManagerAddr.MouseHover += new System.EventHandler(this.textBoxMgmtNwkUpdateNwkManagerAddr_MouseHover);
            // 
            // textBoxDiscoverCommandsMaxCommands
            // 
            this.textBoxDiscoverCommandsMaxCommands.Location = new System.Drawing.Point(1098, 294);
            this.textBoxDiscoverCommandsMaxCommands.Name = "textBoxDiscoverCommandsMaxCommands";
            this.textBoxDiscoverCommandsMaxCommands.Size = new System.Drawing.Size(112, 20);
            this.textBoxDiscoverCommandsMaxCommands.TabIndex = 79;
            this.textBoxDiscoverCommandsMaxCommands.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverCommandsMaxCommands_MouseClick);
            this.textBoxDiscoverCommandsMaxCommands.Leave += new System.EventHandler(this.textBoxDiscoverCommandsMaxCommands_Leave);
            this.textBoxDiscoverCommandsMaxCommands.MouseLeave += new System.EventHandler(this.textBoxDiscoverCommandsMaxCommands_MouseLeave);
            this.textBoxDiscoverCommandsMaxCommands.MouseHover += new System.EventHandler(this.textBoxDiscoverCommandsMaxCommands_MouseHover);
            // 
            // textBoxDiscoverCommandsManuID
            // 
            this.textBoxDiscoverCommandsManuID.Location = new System.Drawing.Point(982, 294);
            this.textBoxDiscoverCommandsManuID.Name = "textBoxDiscoverCommandsManuID";
            this.textBoxDiscoverCommandsManuID.Size = new System.Drawing.Size(112, 20);
            this.textBoxDiscoverCommandsManuID.TabIndex = 78;
            this.textBoxDiscoverCommandsManuID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverCommandsManuID_MouseClick);
            this.textBoxDiscoverCommandsManuID.Leave += new System.EventHandler(this.textBoxDiscoverCommandsManuID_Leave);
            this.textBoxDiscoverCommandsManuID.MouseLeave += new System.EventHandler(this.textBoxDiscoverCommandsManuID_MouseLeave);
            this.textBoxDiscoverCommandsManuID.MouseHover += new System.EventHandler(this.textBoxDiscoverCommandsManuID_MouseHover);
            // 
            // textBoxDiscoverCommandsCommandID
            // 
            this.textBoxDiscoverCommandsCommandID.Location = new System.Drawing.Point(770, 294);
            this.textBoxDiscoverCommandsCommandID.Name = "textBoxDiscoverCommandsCommandID";
            this.textBoxDiscoverCommandsCommandID.Size = new System.Drawing.Size(109, 20);
            this.textBoxDiscoverCommandsCommandID.TabIndex = 76;
            this.textBoxDiscoverCommandsCommandID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverCommandsCommandID_MouseClick);
            this.textBoxDiscoverCommandsCommandID.Leave += new System.EventHandler(this.textBoxDiscoverCommandsCommandID_Leave);
            this.textBoxDiscoverCommandsCommandID.MouseLeave += new System.EventHandler(this.textBoxDiscoverCommandsCommandID_MouseLeave);
            this.textBoxDiscoverCommandsCommandID.MouseHover += new System.EventHandler(this.textBoxDiscoverCommandsCommandID_MouseHover);
            // 
            // textBoxDiscoverCommandsClusterID
            // 
            this.textBoxDiscoverCommandsClusterID.Location = new System.Drawing.Point(538, 294);
            this.textBoxDiscoverCommandsClusterID.Name = "textBoxDiscoverCommandsClusterID";
            this.textBoxDiscoverCommandsClusterID.Size = new System.Drawing.Size(113, 20);
            this.textBoxDiscoverCommandsClusterID.TabIndex = 74;
            this.textBoxDiscoverCommandsClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverCommandsClusterID_MouseClick);
            this.textBoxDiscoverCommandsClusterID.Leave += new System.EventHandler(this.textBoxDiscoverCommandsClusterID_Leave);
            this.textBoxDiscoverCommandsClusterID.MouseLeave += new System.EventHandler(this.textBoxDiscoverCommandsClusterID_MouseLeave);
            this.textBoxDiscoverCommandsClusterID.MouseHover += new System.EventHandler(this.textBoxDiscoverCommandsClusterID_MouseHover);
            // 
            // textBoxDiscoverCommandsDstEP
            // 
            this.textBoxDiscoverCommandsDstEP.Location = new System.Drawing.Point(431, 294);
            this.textBoxDiscoverCommandsDstEP.Name = "textBoxDiscoverCommandsDstEP";
            this.textBoxDiscoverCommandsDstEP.Size = new System.Drawing.Size(103, 20);
            this.textBoxDiscoverCommandsDstEP.TabIndex = 73;
            this.textBoxDiscoverCommandsDstEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverCommandsDstEP_MouseClick);
            this.textBoxDiscoverCommandsDstEP.Leave += new System.EventHandler(this.textBoxDiscoverCommandsDstEP_Leave);
            this.textBoxDiscoverCommandsDstEP.MouseLeave += new System.EventHandler(this.textBoxDiscoverCommandsDstEP_MouseLeave);
            this.textBoxDiscoverCommandsDstEP.MouseHover += new System.EventHandler(this.textBoxDiscoverCommandsDstEP_MouseHover);
            // 
            // textBoxDiscoverCommandsSrcEP
            // 
            this.textBoxDiscoverCommandsSrcEP.Location = new System.Drawing.Point(319, 294);
            this.textBoxDiscoverCommandsSrcEP.Name = "textBoxDiscoverCommandsSrcEP";
            this.textBoxDiscoverCommandsSrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxDiscoverCommandsSrcEP.TabIndex = 72;
            this.textBoxDiscoverCommandsSrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverCommandsSrcEP_MouseClick);
            this.textBoxDiscoverCommandsSrcEP.Leave += new System.EventHandler(this.textBoxDiscoverCommandsSrcEP_Leave);
            this.textBoxDiscoverCommandsSrcEP.MouseLeave += new System.EventHandler(this.textBoxDiscoverCommandsSrcEP_MouseLeave);
            this.textBoxDiscoverCommandsSrcEP.MouseHover += new System.EventHandler(this.textBoxDiscoverCommandsSrcEP_MouseHover);
            // 
            // textBoxDiscoverCommandsTargetAddr
            // 
            this.textBoxDiscoverCommandsTargetAddr.Location = new System.Drawing.Point(206, 294);
            this.textBoxDiscoverCommandsTargetAddr.Name = "textBoxDiscoverCommandsTargetAddr";
            this.textBoxDiscoverCommandsTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxDiscoverCommandsTargetAddr.TabIndex = 71;
            this.textBoxDiscoverCommandsTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverCommandsTargetAddr_MouseClick);
            this.textBoxDiscoverCommandsTargetAddr.Leave += new System.EventHandler(this.textBoxDiscoverCommandsTargetAddr_Leave);
            this.textBoxDiscoverCommandsTargetAddr.MouseLeave += new System.EventHandler(this.textBoxDiscoverCommandsTargetAddr_MouseLeave);
            this.textBoxDiscoverCommandsTargetAddr.MouseHover += new System.EventHandler(this.textBoxDiscoverCommandsTargetAddr_MouseHover);
            // 
            // textBoxMgmtNwkUpdateScanCount
            // 
            this.textBoxMgmtNwkUpdateScanCount.Location = new System.Drawing.Point(538, 267);
            this.textBoxMgmtNwkUpdateScanCount.Name = "textBoxMgmtNwkUpdateScanCount";
            this.textBoxMgmtNwkUpdateScanCount.Size = new System.Drawing.Size(113, 20);
            this.textBoxMgmtNwkUpdateScanCount.TabIndex = 67;
            this.textBoxMgmtNwkUpdateScanCount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMgmtNwkUpdateScanCount_MouseClick);
            this.textBoxMgmtNwkUpdateScanCount.Leave += new System.EventHandler(this.textBoxMgmtNwkUpdateScanCount_Leave);
            this.textBoxMgmtNwkUpdateScanCount.MouseLeave += new System.EventHandler(this.textBoxMgmtNwkUpdateScanCount_MouseLeave);
            this.textBoxMgmtNwkUpdateScanCount.MouseHover += new System.EventHandler(this.textBoxMgmtNwkUpdateScanCount_MouseHover);
            // 
            // textBoxMgmtNwkUpdateScanDuration
            // 
            this.textBoxMgmtNwkUpdateScanDuration.Location = new System.Drawing.Point(431, 267);
            this.textBoxMgmtNwkUpdateScanDuration.Name = "textBoxMgmtNwkUpdateScanDuration";
            this.textBoxMgmtNwkUpdateScanDuration.Size = new System.Drawing.Size(103, 20);
            this.textBoxMgmtNwkUpdateScanDuration.TabIndex = 66;
            this.textBoxMgmtNwkUpdateScanDuration.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMgmtNwkUpdateScanDuration_MouseClick);
            this.textBoxMgmtNwkUpdateScanDuration.Leave += new System.EventHandler(this.textBoxMgmtNwkUpdateScanDuration_Leave);
            this.textBoxMgmtNwkUpdateScanDuration.MouseLeave += new System.EventHandler(this.textBoxMgmtNwkUpdateScanDuration_MouseLeave);
            this.textBoxMgmtNwkUpdateScanDuration.MouseHover += new System.EventHandler(this.textBoxMgmtNwkUpdateScanDuration_MouseHover);
            // 
            // textBoxMgmtNwkUpdateChannelMask
            // 
            this.textBoxMgmtNwkUpdateChannelMask.Location = new System.Drawing.Point(319, 267);
            this.textBoxMgmtNwkUpdateChannelMask.Name = "textBoxMgmtNwkUpdateChannelMask";
            this.textBoxMgmtNwkUpdateChannelMask.Size = new System.Drawing.Size(106, 20);
            this.textBoxMgmtNwkUpdateChannelMask.TabIndex = 65;
            this.textBoxMgmtNwkUpdateChannelMask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMgmtNwkUpdateChannelMask_MouseClick);
            this.textBoxMgmtNwkUpdateChannelMask.Leave += new System.EventHandler(this.textBoxMgmtNwkUpdateChannelMask_Leave);
            this.textBoxMgmtNwkUpdateChannelMask.MouseLeave += new System.EventHandler(this.textBoxMgmtNwkUpdateChannelMask_MouseLeave);
            this.textBoxMgmtNwkUpdateChannelMask.MouseHover += new System.EventHandler(this.textBoxMgmtNwkUpdateChannelMask_MouseHover);
            // 
            // textBoxMgmtNwkUpdateTargetAddr
            // 
            this.textBoxMgmtNwkUpdateTargetAddr.Location = new System.Drawing.Point(207, 267);
            this.textBoxMgmtNwkUpdateTargetAddr.Name = "textBoxMgmtNwkUpdateTargetAddr";
            this.textBoxMgmtNwkUpdateTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxMgmtNwkUpdateTargetAddr.TabIndex = 64;
            this.textBoxMgmtNwkUpdateTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMgmtNwkUpdateTargetAddr_MouseClick);
            this.textBoxMgmtNwkUpdateTargetAddr.Leave += new System.EventHandler(this.textBoxMgmtNwkUpdateTargetAddr_Leave);
            this.textBoxMgmtNwkUpdateTargetAddr.MouseLeave += new System.EventHandler(this.textBoxMgmtNwkUpdateTargetAddr_MouseLeave);
            this.textBoxMgmtNwkUpdateTargetAddr.MouseHover += new System.EventHandler(this.textBoxMgmtNwkUpdateTargetAddr_MouseHover);
            // 
            // textBoxManyToOneRouteRequesRadius
            // 
            this.textBoxManyToOneRouteRequesRadius.Location = new System.Drawing.Point(207, 238);
            this.textBoxManyToOneRouteRequesRadius.Name = "textBoxManyToOneRouteRequesRadius";
            this.textBoxManyToOneRouteRequesRadius.Size = new System.Drawing.Size(106, 20);
            this.textBoxManyToOneRouteRequesRadius.TabIndex = 61;
            this.textBoxManyToOneRouteRequesRadius.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxManyToOneRouteRequesRadius_MouseClick);
            this.textBoxManyToOneRouteRequesRadius.Leave += new System.EventHandler(this.textBoxManyToOneRouteRequesRadius_Leave);
            this.textBoxManyToOneRouteRequesRadius.MouseLeave += new System.EventHandler(this.textBoxManyToOneRouteRequesRadius_MouseLeave);
            this.textBoxManyToOneRouteRequesRadius.MouseHover += new System.EventHandler(this.textBoxManyToOneRouteRequesRadius_MouseHover);
            // 
            // textBoxReadReportConfigAttribID
            // 
            this.textBoxReadReportConfigAttribID.Location = new System.Drawing.Point(770, 153);
            this.textBoxReadReportConfigAttribID.Name = "textBoxReadReportConfigAttribID";
            this.textBoxReadReportConfigAttribID.Size = new System.Drawing.Size(107, 20);
            this.textBoxReadReportConfigAttribID.TabIndex = 42;
            this.textBoxReadReportConfigAttribID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadReportConfigAttribID_MouseClick);
            this.textBoxReadReportConfigAttribID.Leave += new System.EventHandler(this.textBoxReadReportConfigAttribID_Leave);
            this.textBoxReadReportConfigAttribID.MouseLeave += new System.EventHandler(this.textBoxReadReportConfigAttribID_MouseLeave);
            this.textBoxReadReportConfigAttribID.MouseHover += new System.EventHandler(this.textBoxReadReportConfigAttribID_MouseHover);
            // 
            // textBoxReadReportConfigClusterID
            // 
            this.textBoxReadReportConfigClusterID.Location = new System.Drawing.Point(545, 153);
            this.textBoxReadReportConfigClusterID.Name = "textBoxReadReportConfigClusterID";
            this.textBoxReadReportConfigClusterID.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadReportConfigClusterID.TabIndex = 40;
            this.textBoxReadReportConfigClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadReportConfigClusterID_MouseClick);
            this.textBoxReadReportConfigClusterID.Leave += new System.EventHandler(this.textBoxReadReportConfigClusterID_Leave);
            this.textBoxReadReportConfigClusterID.MouseLeave += new System.EventHandler(this.textBoxReadReportConfigClusterID_MouseLeave);
            this.textBoxReadReportConfigClusterID.MouseHover += new System.EventHandler(this.textBoxReadReportConfigClusterID_MouseHover);
            // 
            // textBoxReadReportConfigDstEP
            // 
            this.textBoxReadReportConfigDstEP.Location = new System.Drawing.Point(432, 153);
            this.textBoxReadReportConfigDstEP.Name = "textBoxReadReportConfigDstEP";
            this.textBoxReadReportConfigDstEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadReportConfigDstEP.TabIndex = 39;
            this.textBoxReadReportConfigDstEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadReportConfigDstEP_MouseClick);
            this.textBoxReadReportConfigDstEP.Leave += new System.EventHandler(this.textBoxReadReportConfigDstEP_Leave);
            this.textBoxReadReportConfigDstEP.MouseLeave += new System.EventHandler(this.textBoxReadReportConfigDstEP_MouseLeave);
            this.textBoxReadReportConfigDstEP.MouseHover += new System.EventHandler(this.textBoxReadReportConfigDstEP_MouseHover);
            // 
            // textBoxReadReportConfigSrcEP
            // 
            this.textBoxReadReportConfigSrcEP.Location = new System.Drawing.Point(318, 153);
            this.textBoxReadReportConfigSrcEP.Name = "textBoxReadReportConfigSrcEP";
            this.textBoxReadReportConfigSrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadReportConfigSrcEP.TabIndex = 38;
            this.textBoxReadReportConfigSrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadReportConfigSrcEP_MouseClick);
            this.textBoxReadReportConfigSrcEP.Leave += new System.EventHandler(this.textBoxReadReportConfigSrcEP_Leave);
            this.textBoxReadReportConfigSrcEP.MouseLeave += new System.EventHandler(this.textBoxReadReportConfigSrcEP_MouseLeave);
            this.textBoxReadReportConfigSrcEP.MouseHover += new System.EventHandler(this.textBoxReadReportConfigSrcEP_MouseHover);
            // 
            // textBoxReadReportConfigTargetAddr
            // 
            this.textBoxReadReportConfigTargetAddr.Location = new System.Drawing.Point(205, 153);
            this.textBoxReadReportConfigTargetAddr.Name = "textBoxReadReportConfigTargetAddr";
            this.textBoxReadReportConfigTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadReportConfigTargetAddr.TabIndex = 37;
            this.textBoxReadReportConfigTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadReportConfigTargetAddr_MouseClick);
            this.textBoxReadReportConfigTargetAddr.Leave += new System.EventHandler(this.textBoxReadReportConfigTargetAddr_Leave);
            this.textBoxReadReportConfigTargetAddr.MouseLeave += new System.EventHandler(this.textBoxReadReportConfigTargetAddr_MouseLeave);
            this.textBoxReadReportConfigTargetAddr.MouseHover += new System.EventHandler(this.textBoxReadReportConfigTargetAddr_MouseHover);
            // 
            // textBoxWriteAttribManuID
            // 
            this.textBoxWriteAttribManuID.Location = new System.Drawing.Point(1110, 37);
            this.textBoxWriteAttribManuID.Name = "textBoxWriteAttribManuID";
            this.textBoxWriteAttribManuID.Size = new System.Drawing.Size(106, 20);
            this.textBoxWriteAttribManuID.TabIndex = 20;
            this.textBoxWriteAttribManuID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxWriteAttribManuID_MouseClick);
            this.textBoxWriteAttribManuID.Leave += new System.EventHandler(this.textBoxWriteAttribManuID_Leave);
            this.textBoxWriteAttribManuID.MouseLeave += new System.EventHandler(this.textBoxWriteAttribManuID_MouseLeave);
            this.textBoxWriteAttribManuID.MouseHover += new System.EventHandler(this.textBoxWriteAttribManuID_MouseHover);
            // 
            // textBoxWriteAttribDataType
            // 
            this.textBoxWriteAttribDataType.Location = new System.Drawing.Point(771, 37);
            this.textBoxWriteAttribDataType.Name = "textBoxWriteAttribDataType";
            this.textBoxWriteAttribDataType.Size = new System.Drawing.Size(107, 20);
            this.textBoxWriteAttribDataType.TabIndex = 17;
            this.textBoxWriteAttribDataType.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxWriteAttribDataType_MouseClick);
            this.textBoxWriteAttribDataType.Leave += new System.EventHandler(this.textBoxWriteAttribDataType_Leave);
            this.textBoxWriteAttribDataType.MouseLeave += new System.EventHandler(this.textBoxWriteAttribDataType_MouseLeave);
            this.textBoxWriteAttribDataType.MouseHover += new System.EventHandler(this.textBoxWriteAttribDataType_MouseHover);
            // 
            // textBoxReadAttribManuID
            // 
            this.textBoxReadAttribManuID.Location = new System.Drawing.Point(998, 8);
            this.textBoxReadAttribManuID.Name = "textBoxReadAttribManuID";
            this.textBoxReadAttribManuID.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadAttribManuID.TabIndex = 9;
            this.textBoxReadAttribManuID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAttribManuID_MouseClick);
            this.textBoxReadAttribManuID.Leave += new System.EventHandler(this.textBoxReadAttribManuID_Leave);
            this.textBoxReadAttribManuID.MouseLeave += new System.EventHandler(this.textBoxReadAttribManuID_MouseLeave);
            this.textBoxReadAttribManuID.MouseHover += new System.EventHandler(this.textBoxReadAttribManuID_MouseHover);
            // 
            // textBoxWriteAttribData
            // 
            this.textBoxWriteAttribData.Location = new System.Drawing.Point(884, 37);
            this.textBoxWriteAttribData.Name = "textBoxWriteAttribData";
            this.textBoxWriteAttribData.Size = new System.Drawing.Size(106, 20);
            this.textBoxWriteAttribData.TabIndex = 18;
            this.textBoxWriteAttribData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxWriteAttribData_MouseClick);
            this.textBoxWriteAttribData.Leave += new System.EventHandler(this.textBoxWriteAttribData_Leave);
            this.textBoxWriteAttribData.MouseLeave += new System.EventHandler(this.textBoxWriteAttribData_MouseLeave);
            this.textBoxWriteAttribData.MouseHover += new System.EventHandler(this.textBoxWriteAttribData_MouseHover);
            // 
            // textBoxWriteAttribID
            // 
            this.textBoxWriteAttribID.Location = new System.Drawing.Point(658, 37);
            this.textBoxWriteAttribID.Name = "textBoxWriteAttribID";
            this.textBoxWriteAttribID.Size = new System.Drawing.Size(106, 20);
            this.textBoxWriteAttribID.TabIndex = 16;
            this.textBoxWriteAttribID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxWriteAttribID_MouseClick);
            this.textBoxWriteAttribID.Leave += new System.EventHandler(this.textBoxWriteAttribID_Leave);
            this.textBoxWriteAttribID.MouseLeave += new System.EventHandler(this.textBoxWriteAttribID_MouseLeave);
            this.textBoxWriteAttribID.MouseHover += new System.EventHandler(this.textBoxWriteAttribID_MouseHover);
            // 
            // textBoxWriteAttribClusterID
            // 
            this.textBoxWriteAttribClusterID.Location = new System.Drawing.Point(432, 37);
            this.textBoxWriteAttribClusterID.Name = "textBoxWriteAttribClusterID";
            this.textBoxWriteAttribClusterID.Size = new System.Drawing.Size(106, 20);
            this.textBoxWriteAttribClusterID.TabIndex = 14;
            this.textBoxWriteAttribClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxWriteAttribClusterID_MouseClick);
            this.textBoxWriteAttribClusterID.Leave += new System.EventHandler(this.textBoxWriteAttribClusterID_Leave);
            this.textBoxWriteAttribClusterID.MouseLeave += new System.EventHandler(this.textBoxWriteAttribClusterID_MouseLeave);
            this.textBoxWriteAttribClusterID.MouseHover += new System.EventHandler(this.textBoxWriteAttribClusterID_MouseHover);
            // 
            // textBoxWriteAttribDstEP
            // 
            this.textBoxWriteAttribDstEP.Location = new System.Drawing.Point(319, 37);
            this.textBoxWriteAttribDstEP.Name = "textBoxWriteAttribDstEP";
            this.textBoxWriteAttribDstEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxWriteAttribDstEP.TabIndex = 13;
            this.textBoxWriteAttribDstEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxWriteAttribDstEP_MouseClick);
            this.textBoxWriteAttribDstEP.Leave += new System.EventHandler(this.textBoxWriteAttribDstEP_Leave);
            this.textBoxWriteAttribDstEP.MouseLeave += new System.EventHandler(this.textBoxWriteAttribDstEP_MouseLeave);
            this.textBoxWriteAttribDstEP.MouseHover += new System.EventHandler(this.textBoxWriteAttribDstEP_MouseHover);
            // 
            // textBoxWriteAttribSrcEP
            // 
            this.textBoxWriteAttribSrcEP.Location = new System.Drawing.Point(206, 37);
            this.textBoxWriteAttribSrcEP.Name = "textBoxWriteAttribSrcEP";
            this.textBoxWriteAttribSrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxWriteAttribSrcEP.TabIndex = 12;
            this.textBoxWriteAttribSrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxWriteAttribSrcEP_MouseClick);
            this.textBoxWriteAttribSrcEP.Leave += new System.EventHandler(this.textBoxWriteAttribSrcEP_Leave);
            this.textBoxWriteAttribSrcEP.MouseLeave += new System.EventHandler(this.textBoxWriteAttribSrcEP_MouseLeave);
            this.textBoxWriteAttribSrcEP.MouseHover += new System.EventHandler(this.textBoxWriteAttribSrcEP_MouseHover);
            // 
            // textBoxWriteAttribTargetAddr
            // 
            this.textBoxWriteAttribTargetAddr.Location = new System.Drawing.Point(93, 37);
            this.textBoxWriteAttribTargetAddr.Name = "textBoxWriteAttribTargetAddr";
            this.textBoxWriteAttribTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxWriteAttribTargetAddr.TabIndex = 11;
            this.textBoxWriteAttribTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxWriteAttribTargetAddr_MouseClick);
            this.textBoxWriteAttribTargetAddr.Leave += new System.EventHandler(this.textBoxWriteAttribTargetAddr_Leave);
            this.textBoxWriteAttribTargetAddr.MouseLeave += new System.EventHandler(this.textBoxWriteAttribTargetAddr_MouseLeave);
            this.textBoxWriteAttribTargetAddr.MouseHover += new System.EventHandler(this.textBoxWriteAttribTargetAddr_MouseHover);
            // 
            // textBoxConfigReportChange
            // 
            this.textBoxConfigReportChange.Location = new System.Drawing.Point(998, 117);
            this.textBoxConfigReportChange.Name = "textBoxConfigReportChange";
            this.textBoxConfigReportChange.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportChange.TabIndex = 34;
            this.textBoxConfigReportChange.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportChange_MouseClick);
            this.textBoxConfigReportChange.Leave += new System.EventHandler(this.textBoxConfigReportChange_Leave);
            this.textBoxConfigReportChange.MouseLeave += new System.EventHandler(this.textBoxConfigReportChange_MouseLeave);
            this.textBoxConfigReportChange.MouseHover += new System.EventHandler(this.textBoxConfigReportChange_MouseHover);
            // 
            // textBoxConfigReportTimeOut
            // 
            this.textBoxConfigReportTimeOut.Location = new System.Drawing.Point(886, 117);
            this.textBoxConfigReportTimeOut.Name = "textBoxConfigReportTimeOut";
            this.textBoxConfigReportTimeOut.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportTimeOut.TabIndex = 33;
            this.textBoxConfigReportTimeOut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportTimeOut_MouseClick);
            this.textBoxConfigReportTimeOut.Leave += new System.EventHandler(this.textBoxConfigReportTimeOut_Leave);
            this.textBoxConfigReportTimeOut.MouseLeave += new System.EventHandler(this.textBoxConfigReportTimeOut_MouseLeave);
            this.textBoxConfigReportTimeOut.MouseHover += new System.EventHandler(this.textBoxConfigReportTimeOut_MouseHover);
            // 
            // textBoxConfigReportMaxInterval
            // 
            this.textBoxConfigReportMaxInterval.Location = new System.Drawing.Point(998, 91);
            this.textBoxConfigReportMaxInterval.Name = "textBoxConfigReportMaxInterval";
            this.textBoxConfigReportMaxInterval.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportMaxInterval.TabIndex = 32;
            this.textBoxConfigReportMaxInterval.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportMaxInterval_MouseClick);
            this.textBoxConfigReportMaxInterval.Leave += new System.EventHandler(this.textBoxConfigReportMaxInterval_Leave);
            this.textBoxConfigReportMaxInterval.MouseLeave += new System.EventHandler(this.textBoxConfigReportMaxInterval_MouseLeave);
            this.textBoxConfigReportMaxInterval.MouseHover += new System.EventHandler(this.textBoxConfigReportMaxInterval_MouseHover);
            // 
            // textBoxDiscoverAttributesMaxIDs
            // 
            this.textBoxDiscoverAttributesMaxIDs.Location = new System.Drawing.Point(767, 210);
            this.textBoxDiscoverAttributesMaxIDs.Name = "textBoxDiscoverAttributesMaxIDs";
            this.textBoxDiscoverAttributesMaxIDs.Size = new System.Drawing.Size(110, 20);
            this.textBoxDiscoverAttributesMaxIDs.TabIndex = 57;
            this.textBoxDiscoverAttributesMaxIDs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverAttributesMaxIDs_MouseClick);
            this.textBoxDiscoverAttributesMaxIDs.Leave += new System.EventHandler(this.textBoxDiscoverAttributesMaxIDs_Leave);
            this.textBoxDiscoverAttributesMaxIDs.MouseLeave += new System.EventHandler(this.textBoxDiscoverAttributesMaxIDs_MouseLeave);
            this.textBoxDiscoverAttributesMaxIDs.MouseHover += new System.EventHandler(this.textBoxDiscoverAttributesMaxIDs_MouseHover);
            // 
            // textBoxDiscoverAttributesClusterID
            // 
            this.textBoxDiscoverAttributesClusterID.Location = new System.Drawing.Point(431, 210);
            this.textBoxDiscoverAttributesClusterID.Name = "textBoxDiscoverAttributesClusterID";
            this.textBoxDiscoverAttributesClusterID.Size = new System.Drawing.Size(102, 20);
            this.textBoxDiscoverAttributesClusterID.TabIndex = 54;
            this.textBoxDiscoverAttributesClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverAttributesClusterID_MouseClick);
            this.textBoxDiscoverAttributesClusterID.Leave += new System.EventHandler(this.textBoxDiscoverAttributesClusterID_Leave);
            this.textBoxDiscoverAttributesClusterID.MouseLeave += new System.EventHandler(this.textBoxDiscoverAttributesClusterID_MouseLeave);
            this.textBoxDiscoverAttributesClusterID.MouseHover += new System.EventHandler(this.textBoxDiscoverAttributesClusterID_MouseHover);
            // 
            // textBoxDiscoverAttributesDstEp
            // 
            this.textBoxDiscoverAttributesDstEp.Location = new System.Drawing.Point(319, 210);
            this.textBoxDiscoverAttributesDstEp.Name = "textBoxDiscoverAttributesDstEp";
            this.textBoxDiscoverAttributesDstEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxDiscoverAttributesDstEp.TabIndex = 53;
            this.textBoxDiscoverAttributesDstEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverAttributesDstEp_MouseClick);
            this.textBoxDiscoverAttributesDstEp.Leave += new System.EventHandler(this.textBoxDiscoverAttributesDstEp_Leave);
            this.textBoxDiscoverAttributesDstEp.MouseLeave += new System.EventHandler(this.textBoxDiscoverAttributesDstEp_MouseLeave);
            this.textBoxDiscoverAttributesDstEp.MouseHover += new System.EventHandler(this.textBoxDiscoverAttributesDstEp_MouseHover);
            // 
            // textBoxDiscoverAttributesSrcEp
            // 
            this.textBoxDiscoverAttributesSrcEp.Location = new System.Drawing.Point(206, 210);
            this.textBoxDiscoverAttributesSrcEp.Name = "textBoxDiscoverAttributesSrcEp";
            this.textBoxDiscoverAttributesSrcEp.Size = new System.Drawing.Size(106, 20);
            this.textBoxDiscoverAttributesSrcEp.TabIndex = 52;
            this.textBoxDiscoverAttributesSrcEp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverAttributesSrcEp_MouseClick);
            this.textBoxDiscoverAttributesSrcEp.Leave += new System.EventHandler(this.textBoxDiscoverAttributesSrcEp_Leave);
            this.textBoxDiscoverAttributesSrcEp.MouseLeave += new System.EventHandler(this.textBoxDiscoverAttributesSrcEp_MouseLeave);
            this.textBoxDiscoverAttributesSrcEp.MouseHover += new System.EventHandler(this.textBoxDiscoverAttributesSrcEp_MouseHover);
            // 
            // textBoxDiscoverAttributesAddr
            // 
            this.textBoxDiscoverAttributesAddr.Location = new System.Drawing.Point(108, 210);
            this.textBoxDiscoverAttributesAddr.Name = "textBoxDiscoverAttributesAddr";
            this.textBoxDiscoverAttributesAddr.Size = new System.Drawing.Size(91, 20);
            this.textBoxDiscoverAttributesAddr.TabIndex = 51;
            this.textBoxDiscoverAttributesAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxDiscoverAttributesAddr_MouseClick);
            this.textBoxDiscoverAttributesAddr.Leave += new System.EventHandler(this.textBoxDiscoverAttributesAddr_Leave);
            this.textBoxDiscoverAttributesAddr.MouseLeave += new System.EventHandler(this.textBoxDiscoverAttributesAddr_MouseLeave);
            this.textBoxDiscoverAttributesAddr.MouseHover += new System.EventHandler(this.textBoxDiscoverAttributesAddr_MouseHover);
            // 
            // textBoxReadAllAttribClusterID
            // 
            this.textBoxReadAllAttribClusterID.Location = new System.Drawing.Point(431, 182);
            this.textBoxReadAllAttribClusterID.Name = "textBoxReadAllAttribClusterID";
            this.textBoxReadAllAttribClusterID.Size = new System.Drawing.Size(103, 20);
            this.textBoxReadAllAttribClusterID.TabIndex = 48;
            this.textBoxReadAllAttribClusterID.Visible = false;
            this.textBoxReadAllAttribClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAllAttribClusterID_MouseClick);
            this.textBoxReadAllAttribClusterID.Leave += new System.EventHandler(this.textBoxReadAllAttribClusterID_Leave);
            this.textBoxReadAllAttribClusterID.MouseLeave += new System.EventHandler(this.textBoxReadAllAttribClusterID_MouseLeave);
            this.textBoxReadAllAttribClusterID.MouseHover += new System.EventHandler(this.textBoxReadAllAttribClusterID_MouseHover);
            // 
            // textBoxReadAllAttribDstEP
            // 
            this.textBoxReadAllAttribDstEP.Location = new System.Drawing.Point(319, 182);
            this.textBoxReadAllAttribDstEP.Name = "textBoxReadAllAttribDstEP";
            this.textBoxReadAllAttribDstEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadAllAttribDstEP.TabIndex = 47;
            this.textBoxReadAllAttribDstEP.Visible = false;
            this.textBoxReadAllAttribDstEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAllAttribDstEP_MouseClick);
            this.textBoxReadAllAttribDstEP.Leave += new System.EventHandler(this.textBoxReadAllAttribDstEP_Leave);
            this.textBoxReadAllAttribDstEP.MouseLeave += new System.EventHandler(this.textBoxReadAllAttribDstEP_MouseLeave);
            this.textBoxReadAllAttribDstEP.MouseHover += new System.EventHandler(this.textBoxReadAllAttribDstEP_MouseHover);
            // 
            // textBoxReadAllAttribSrcEP
            // 
            this.textBoxReadAllAttribSrcEP.Location = new System.Drawing.Point(206, 183);
            this.textBoxReadAllAttribSrcEP.Name = "textBoxReadAllAttribSrcEP";
            this.textBoxReadAllAttribSrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadAllAttribSrcEP.TabIndex = 46;
            this.textBoxReadAllAttribSrcEP.Visible = false;
            this.textBoxReadAllAttribSrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAllAttribSrcEP_MouseClick);
            this.textBoxReadAllAttribSrcEP.Leave += new System.EventHandler(this.textBoxReadAllAttribSrcEP_Leave);
            this.textBoxReadAllAttribSrcEP.MouseLeave += new System.EventHandler(this.textBoxReadAllAttribSrcEP_MouseLeave);
            this.textBoxReadAllAttribSrcEP.MouseHover += new System.EventHandler(this.textBoxReadAllAttribSrcEP_MouseHover);
            // 
            // textBoxReadAllAttribAddr
            // 
            this.textBoxReadAllAttribAddr.Location = new System.Drawing.Point(108, 183);
            this.textBoxReadAllAttribAddr.Name = "textBoxReadAllAttribAddr";
            this.textBoxReadAllAttribAddr.Size = new System.Drawing.Size(91, 20);
            this.textBoxReadAllAttribAddr.TabIndex = 45;
            this.textBoxReadAllAttribAddr.Visible = false;
            this.textBoxReadAllAttribAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAllAttribAddr_MouseClick);
            this.textBoxReadAllAttribAddr.Leave += new System.EventHandler(this.textBoxReadAllAttribAddr_Leave);
            this.textBoxReadAllAttribAddr.MouseLeave += new System.EventHandler(this.textBoxReadAllAttribAddr_MouseLeave);
            this.textBoxReadAllAttribAddr.MouseHover += new System.EventHandler(this.textBoxReadAllAttribAddr_MouseHover);
            // 
            // textBoxConfigReportAttribType
            // 
            this.textBoxConfigReportAttribType.Location = new System.Drawing.Point(886, 66);
            this.textBoxConfigReportAttribType.Name = "textBoxConfigReportAttribType";
            this.textBoxConfigReportAttribType.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportAttribType.TabIndex = 29;
            this.textBoxConfigReportAttribType.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportAttribType_MouseClick);
            this.textBoxConfigReportAttribType.Leave += new System.EventHandler(this.textBoxConfigReportAttribType_Leave);
            this.textBoxConfigReportAttribType.MouseLeave += new System.EventHandler(this.textBoxConfigReportAttribType_MouseLeave);
            this.textBoxConfigReportAttribType.MouseHover += new System.EventHandler(this.textBoxConfigReportAttribType_MouseHover);
            // 
            // textBoxConfigReportMinInterval
            // 
            this.textBoxConfigReportMinInterval.Location = new System.Drawing.Point(886, 91);
            this.textBoxConfigReportMinInterval.Name = "textBoxConfigReportMinInterval";
            this.textBoxConfigReportMinInterval.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportMinInterval.TabIndex = 31;
            this.textBoxConfigReportMinInterval.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportMinInterval_MouseClick);
            this.textBoxConfigReportMinInterval.Leave += new System.EventHandler(this.textBoxConfigReportMinInterval_Leave);
            this.textBoxConfigReportMinInterval.MouseLeave += new System.EventHandler(this.textBoxConfigReportMinInterval_MouseLeave);
            this.textBoxConfigReportMinInterval.MouseHover += new System.EventHandler(this.textBoxConfigReportMinInterval_MouseHover);
            // 
            // textBoxConfigReportAttribID
            // 
            this.textBoxConfigReportAttribID.Location = new System.Drawing.Point(998, 66);
            this.textBoxConfigReportAttribID.Name = "textBoxConfigReportAttribID";
            this.textBoxConfigReportAttribID.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportAttribID.TabIndex = 30;
            this.textBoxConfigReportAttribID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportAttribID_MouseClick);
            this.textBoxConfigReportAttribID.Leave += new System.EventHandler(this.textBoxConfigReportAttribID_Leave);
            this.textBoxConfigReportAttribID.MouseLeave += new System.EventHandler(this.textBoxConfigReportAttribID_MouseLeave);
            this.textBoxConfigReportAttribID.MouseHover += new System.EventHandler(this.textBoxConfigReportAttribID_MouseHover);
            // 
            // textBoxConfigReportClusterID
            // 
            this.textBoxConfigReportClusterID.Location = new System.Drawing.Point(545, 66);
            this.textBoxConfigReportClusterID.Name = "textBoxConfigReportClusterID";
            this.textBoxConfigReportClusterID.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportClusterID.TabIndex = 26;
            this.textBoxConfigReportClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportClusterID_MouseClick);
            this.textBoxConfigReportClusterID.Leave += new System.EventHandler(this.textBoxConfigReportClusterID_Leave);
            this.textBoxConfigReportClusterID.MouseLeave += new System.EventHandler(this.textBoxConfigReportClusterID_MouseLeave);
            this.textBoxConfigReportClusterID.MouseHover += new System.EventHandler(this.textBoxConfigReportClusterID_MouseHover);
            // 
            // textBoxConfigReportDstEP
            // 
            this.textBoxConfigReportDstEP.Location = new System.Drawing.Point(432, 66);
            this.textBoxConfigReportDstEP.Name = "textBoxConfigReportDstEP";
            this.textBoxConfigReportDstEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportDstEP.TabIndex = 25;
            this.textBoxConfigReportDstEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportDstEP_MouseClick);
            this.textBoxConfigReportDstEP.Leave += new System.EventHandler(this.textBoxConfigReportDstEP_Leave);
            this.textBoxConfigReportDstEP.MouseLeave += new System.EventHandler(this.textBoxConfigReportDstEP_MouseLeave);
            this.textBoxConfigReportDstEP.MouseHover += new System.EventHandler(this.textBoxConfigReportDstEP_MouseHover);
            // 
            // textBoxConfigReportSrcEP
            // 
            this.textBoxConfigReportSrcEP.Location = new System.Drawing.Point(319, 66);
            this.textBoxConfigReportSrcEP.Name = "textBoxConfigReportSrcEP";
            this.textBoxConfigReportSrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportSrcEP.TabIndex = 24;
            this.textBoxConfigReportSrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportSrcEP_MouseClick);
            this.textBoxConfigReportSrcEP.Leave += new System.EventHandler(this.textBoxConfigReportSrcEP_Leave);
            this.textBoxConfigReportSrcEP.MouseLeave += new System.EventHandler(this.textBoxConfigReportSrcEP_MouseLeave);
            this.textBoxConfigReportSrcEP.MouseHover += new System.EventHandler(this.textBoxConfigReportSrcEP_MouseHover);
            // 
            // textBoxConfigReportTargetAddr
            // 
            this.textBoxConfigReportTargetAddr.Location = new System.Drawing.Point(206, 66);
            this.textBoxConfigReportTargetAddr.Name = "textBoxConfigReportTargetAddr";
            this.textBoxConfigReportTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxConfigReportTargetAddr.TabIndex = 23;
            this.textBoxConfigReportTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxConfigReportTargetAddr_MouseClick);
            this.textBoxConfigReportTargetAddr.Leave += new System.EventHandler(this.textBoxConfigReportTargetAddr_Leave);
            this.textBoxConfigReportTargetAddr.MouseLeave += new System.EventHandler(this.textBoxConfigReportTargetAddr_MouseLeave);
            this.textBoxConfigReportTargetAddr.MouseHover += new System.EventHandler(this.textBoxConfigReportTargetAddr_MouseHover);
            // 
            // textBoxReadAttribCount
            // 
            this.textBoxReadAttribCount.Location = new System.Drawing.Point(658, 8);
            this.textBoxReadAttribCount.Name = "textBoxReadAttribCount";
            this.textBoxReadAttribCount.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadAttribCount.TabIndex = 6;
            this.textBoxReadAttribCount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAttribCount_MouseClick);
            this.textBoxReadAttribCount.Leave += new System.EventHandler(this.textBoxReadAttribCount_Leave);
            this.textBoxReadAttribCount.MouseLeave += new System.EventHandler(this.textBoxReadAttribCount_MouseLeave);
            this.textBoxReadAttribCount.MouseHover += new System.EventHandler(this.textBoxReadAttribCount_MouseHover);
            // 
            // textBoxReadAttribID1
            // 
            this.textBoxReadAttribID1.Location = new System.Drawing.Point(771, 8);
            this.textBoxReadAttribID1.Name = "textBoxReadAttribID1";
            this.textBoxReadAttribID1.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadAttribID1.TabIndex = 7;
            this.textBoxReadAttribID1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAttribID1_MouseClick);
            this.textBoxReadAttribID1.Leave += new System.EventHandler(this.textBoxReadAttribID1_Leave);
            this.textBoxReadAttribID1.MouseLeave += new System.EventHandler(this.textBoxReadAttribID1_MouseLeave);
            this.textBoxReadAttribID1.MouseHover += new System.EventHandler(this.textBoxReadAttribID1_MouseHover);
            // 
            // textBoxReadAttribClusterID
            // 
            this.textBoxReadAttribClusterID.Location = new System.Drawing.Point(432, 8);
            this.textBoxReadAttribClusterID.Name = "textBoxReadAttribClusterID";
            this.textBoxReadAttribClusterID.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadAttribClusterID.TabIndex = 4;
            this.textBoxReadAttribClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAttribClusterID_MouseClick);
            this.textBoxReadAttribClusterID.Leave += new System.EventHandler(this.textBoxReadAttribClusterID_Leave);
            this.textBoxReadAttribClusterID.MouseLeave += new System.EventHandler(this.textBoxReadAttribClusterID_MouseLeave);
            this.textBoxReadAttribClusterID.MouseHover += new System.EventHandler(this.textBoxReadAttribClusterID_MouseHover);
            // 
            // textBoxReadAttribDstEP
            // 
            this.textBoxReadAttribDstEP.Location = new System.Drawing.Point(319, 8);
            this.textBoxReadAttribDstEP.Name = "textBoxReadAttribDstEP";
            this.textBoxReadAttribDstEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadAttribDstEP.TabIndex = 3;
            this.textBoxReadAttribDstEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAttribDstEP_MouseClick);
            this.textBoxReadAttribDstEP.Leave += new System.EventHandler(this.textBoxReadAttribDstEP_Leave);
            this.textBoxReadAttribDstEP.MouseLeave += new System.EventHandler(this.textBoxReadAttribDstEP_MouseLeave);
            this.textBoxReadAttribDstEP.MouseHover += new System.EventHandler(this.textBoxReadAttribDstEP_MouseHover);
            // 
            // textBoxReadAttribSrcEP
            // 
            this.textBoxReadAttribSrcEP.Location = new System.Drawing.Point(206, 8);
            this.textBoxReadAttribSrcEP.Name = "textBoxReadAttribSrcEP";
            this.textBoxReadAttribSrcEP.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadAttribSrcEP.TabIndex = 2;
            this.textBoxReadAttribSrcEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAttribSrcEP_MouseClick);
            this.textBoxReadAttribSrcEP.Leave += new System.EventHandler(this.textBoxReadAttribSrcEP_Leave);
            this.textBoxReadAttribSrcEP.MouseLeave += new System.EventHandler(this.textBoxReadAttribSrcEP_MouseLeave);
            this.textBoxReadAttribSrcEP.MouseHover += new System.EventHandler(this.textBoxReadAttribSrcEP_MouseHover);
            // 
            // textBoxReadAttribTargetAddr
            // 
            this.textBoxReadAttribTargetAddr.Location = new System.Drawing.Point(93, 8);
            this.textBoxReadAttribTargetAddr.Name = "textBoxReadAttribTargetAddr";
            this.textBoxReadAttribTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxReadAttribTargetAddr.TabIndex = 1;
            this.textBoxReadAttribTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxReadAttribTargetAddr_MouseClick);
            this.textBoxReadAttribTargetAddr.Leave += new System.EventHandler(this.textBoxReadAttribTargetAddr_Leave);
            this.textBoxReadAttribTargetAddr.MouseLeave += new System.EventHandler(this.textBoxReadAttribTargetAddr_MouseLeave);
            this.textBoxReadAttribTargetAddr.MouseHover += new System.EventHandler(this.textBoxReadAttribTargetAddr_MouseHover);
            // 
            // buttonGeneralSendInstallCode
            // 
            this.buttonGeneralSendInstallCode.Location = new System.Drawing.Point(6, 377);
            this.buttonGeneralSendInstallCode.Name = "buttonGeneralSendInstallCode";
            this.buttonGeneralSendInstallCode.Size = new System.Drawing.Size(146, 23);
            this.buttonGeneralSendInstallCode.TabIndex = 97;
            this.buttonGeneralSendInstallCode.Text = "Send Install Code";
            this.buttonGeneralSendInstallCode.UseVisualStyleBackColor = true;
            this.buttonGeneralSendInstallCode.Click += new System.EventHandler(this.buttonGeneralSendInstallCode_Click);
            // 
            // buttonOOBCommissioningData
            // 
            this.buttonOOBCommissioningData.Location = new System.Drawing.Point(6, 348);
            this.buttonOOBCommissioningData.Name = "buttonOOBCommissioningData";
            this.buttonOOBCommissioningData.Size = new System.Drawing.Size(146, 22);
            this.buttonOOBCommissioningData.TabIndex = 91;
            this.buttonOOBCommissioningData.Text = "OOB Commissioning Data";
            this.buttonOOBCommissioningData.UseVisualStyleBackColor = true;
            this.buttonOOBCommissioningData.Click += new System.EventHandler(this.buttonOOBCommissioningData_Click);
            // 
            // comboBoxRawDataCommandsAddrMode
            // 
            this.comboBoxRawDataCommandsAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRawDataCommandsAddrMode.FormattingEnabled = true;
            this.comboBoxRawDataCommandsAddrMode.Location = new System.Drawing.Point(108, 321);
            this.comboBoxRawDataCommandsAddrMode.Name = "comboBoxRawDataCommandsAddrMode";
            this.comboBoxRawDataCommandsAddrMode.Size = new System.Drawing.Size(94, 21);
            this.comboBoxRawDataCommandsAddrMode.TabIndex = 82;
            // 
            // buttonRawDataSend
            // 
            this.buttonRawDataSend.Location = new System.Drawing.Point(6, 320);
            this.buttonRawDataSend.Name = "buttonRawDataSend";
            this.buttonRawDataSend.Size = new System.Drawing.Size(95, 22);
            this.buttonRawDataSend.TabIndex = 81;
            this.buttonRawDataSend.Text = "Raw Data";
            this.buttonRawDataSend.UseVisualStyleBackColor = true;
            this.buttonRawDataSend.Click += new System.EventHandler(this.buttonRawDataSend_Click);
            // 
            // comboBoxDiscoverCommandsRxGen
            // 
            this.comboBoxDiscoverCommandsRxGen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscoverCommandsRxGen.FormattingEnabled = true;
            this.comboBoxDiscoverCommandsRxGen.Location = new System.Drawing.Point(1217, 294);
            this.comboBoxDiscoverCommandsRxGen.Name = "comboBoxDiscoverCommandsRxGen";
            this.comboBoxDiscoverCommandsRxGen.Size = new System.Drawing.Size(96, 21);
            this.comboBoxDiscoverCommandsRxGen.TabIndex = 80;
            this.comboBoxDiscoverCommandsRxGen.MouseLeave += new System.EventHandler(this.comboBoxDiscoverCommandsRxGen_MouseLeave);
            this.comboBoxDiscoverCommandsRxGen.MouseHover += new System.EventHandler(this.comboBoxDiscoverCommandsRxGen_MouseHover);
            // 
            // comboBoxDiscoverAttributesExtended
            // 
            this.comboBoxDiscoverAttributesExtended.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscoverAttributesExtended.FormattingEnabled = true;
            this.comboBoxDiscoverAttributesExtended.Location = new System.Drawing.Point(883, 209);
            this.comboBoxDiscoverAttributesExtended.Name = "comboBoxDiscoverAttributesExtended";
            this.comboBoxDiscoverAttributesExtended.Size = new System.Drawing.Size(106, 21);
            this.comboBoxDiscoverAttributesExtended.TabIndex = 58;
            this.comboBoxDiscoverAttributesExtended.MouseLeave += new System.EventHandler(this.comboBoxDiscoverAttributesExtended_MouseLeave);
            this.comboBoxDiscoverAttributesExtended.MouseHover += new System.EventHandler(this.comboBoxDiscoverAttributesExtended_MouseHover);
            // 
            // comboBoxDiscoverCommandsManuSpecific
            // 
            this.comboBoxDiscoverCommandsManuSpecific.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscoverCommandsManuSpecific.FormattingEnabled = true;
            this.comboBoxDiscoverCommandsManuSpecific.Location = new System.Drawing.Point(884, 294);
            this.comboBoxDiscoverCommandsManuSpecific.Name = "comboBoxDiscoverCommandsManuSpecific";
            this.comboBoxDiscoverCommandsManuSpecific.Size = new System.Drawing.Size(91, 21);
            this.comboBoxDiscoverCommandsManuSpecific.TabIndex = 77;
            this.comboBoxDiscoverCommandsManuSpecific.MouseLeave += new System.EventHandler(this.comboBoxDiscoverCommandsManuSpecific_MouseLeave);
            this.comboBoxDiscoverCommandsManuSpecific.MouseHover += new System.EventHandler(this.comboBoxDiscoverCommandsManuSpecific_MouseHover);
            // 
            // comboBoxDiscoverCommandsDirection
            // 
            this.comboBoxDiscoverCommandsDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscoverCommandsDirection.FormattingEnabled = true;
            this.comboBoxDiscoverCommandsDirection.Location = new System.Drawing.Point(658, 294);
            this.comboBoxDiscoverCommandsDirection.Name = "comboBoxDiscoverCommandsDirection";
            this.comboBoxDiscoverCommandsDirection.Size = new System.Drawing.Size(106, 21);
            this.comboBoxDiscoverCommandsDirection.TabIndex = 75;
            this.comboBoxDiscoverCommandsDirection.MouseLeave += new System.EventHandler(this.comboBoxDiscoverCommandsDirection_MouseLeave);
            this.comboBoxDiscoverCommandsDirection.MouseHover += new System.EventHandler(this.comboBoxDiscoverCommandsDirection_MouseHover);
            // 
            // comboBoxDiscoverCommandsAddrMode
            // 
            this.comboBoxDiscoverCommandsAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscoverCommandsAddrMode.FormattingEnabled = true;
            this.comboBoxDiscoverCommandsAddrMode.Location = new System.Drawing.Point(108, 294);
            this.comboBoxDiscoverCommandsAddrMode.Name = "comboBoxDiscoverCommandsAddrMode";
            this.comboBoxDiscoverCommandsAddrMode.Size = new System.Drawing.Size(94, 21);
            this.comboBoxDiscoverCommandsAddrMode.TabIndex = 70;
            this.comboBoxDiscoverCommandsAddrMode.MouseLeave += new System.EventHandler(this.comboBoxDiscoverCommandsAddrMode_MouseLeave);
            this.comboBoxDiscoverCommandsAddrMode.MouseHover += new System.EventHandler(this.comboBoxDiscoverCommandsAddrMode_MouseHover);
            // 
            // buttonDiscoverCommands
            // 
            this.buttonDiscoverCommands.Location = new System.Drawing.Point(6, 293);
            this.buttonDiscoverCommands.Name = "buttonDiscoverCommands";
            this.buttonDiscoverCommands.Size = new System.Drawing.Size(95, 22);
            this.buttonDiscoverCommands.TabIndex = 69;
            this.buttonDiscoverCommands.Text = "Disc Cmds";
            this.buttonDiscoverCommands.UseVisualStyleBackColor = true;
            this.buttonDiscoverCommands.Click += new System.EventHandler(this.buttonDiscoverCommands_Click);
            // 
            // comboBoxMgmtNwkUpdateAddrMode
            // 
            this.comboBoxMgmtNwkUpdateAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMgmtNwkUpdateAddrMode.FormattingEnabled = true;
            this.comboBoxMgmtNwkUpdateAddrMode.Location = new System.Drawing.Point(108, 267);
            this.comboBoxMgmtNwkUpdateAddrMode.Name = "comboBoxMgmtNwkUpdateAddrMode";
            this.comboBoxMgmtNwkUpdateAddrMode.Size = new System.Drawing.Size(94, 21);
            this.comboBoxMgmtNwkUpdateAddrMode.TabIndex = 63;
            this.comboBoxMgmtNwkUpdateAddrMode.MouseLeave += new System.EventHandler(this.comboBoxMgmtNwkUpdateAddrMode_MouseLeave);
            this.comboBoxMgmtNwkUpdateAddrMode.MouseHover += new System.EventHandler(this.comboBoxMgmtNwkUpdateAddrMode_MouseHover);
            // 
            // buttonMgmtNwkUpdate
            // 
            this.buttonMgmtNwkUpdate.Location = new System.Drawing.Point(6, 265);
            this.buttonMgmtNwkUpdate.Name = "buttonMgmtNwkUpdate";
            this.buttonMgmtNwkUpdate.Size = new System.Drawing.Size(95, 22);
            this.buttonMgmtNwkUpdate.TabIndex = 62;
            this.buttonMgmtNwkUpdate.Text = "NWK Update";
            this.buttonMgmtNwkUpdate.UseVisualStyleBackColor = true;
            this.buttonMgmtNwkUpdate.Click += new System.EventHandler(this.buttonMgmtNwkUpdate_Click);
            // 
            // comboBoxManyToOneRouteRequestCacheRoute
            // 
            this.comboBoxManyToOneRouteRequestCacheRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxManyToOneRouteRequestCacheRoute.FormattingEnabled = true;
            this.comboBoxManyToOneRouteRequestCacheRoute.Location = new System.Drawing.Point(108, 238);
            this.comboBoxManyToOneRouteRequestCacheRoute.Name = "comboBoxManyToOneRouteRequestCacheRoute";
            this.comboBoxManyToOneRouteRequestCacheRoute.Size = new System.Drawing.Size(91, 21);
            this.comboBoxManyToOneRouteRequestCacheRoute.TabIndex = 60;
            this.comboBoxManyToOneRouteRequestCacheRoute.MouseLeave += new System.EventHandler(this.comboBoxManyToOneRouteRequestCacheRoute_MouseLeave);
            this.comboBoxManyToOneRouteRequestCacheRoute.MouseHover += new System.EventHandler(this.comboBoxManyToOneRouteRequestCacheRoute_MouseHover);
            // 
            // buttonManyToOneRouteRequest
            // 
            this.buttonManyToOneRouteRequest.Location = new System.Drawing.Point(6, 235);
            this.buttonManyToOneRouteRequest.Name = "buttonManyToOneRouteRequest";
            this.buttonManyToOneRouteRequest.Size = new System.Drawing.Size(95, 24);
            this.buttonManyToOneRouteRequest.TabIndex = 59;
            this.buttonManyToOneRouteRequest.Text = "MTO Rt Req";
            this.buttonManyToOneRouteRequest.UseVisualStyleBackColor = true;
            this.buttonManyToOneRouteRequest.Click += new System.EventHandler(this.buttonManyToOneRouteRequest_Click);
            // 
            // comboBoxReadReportConfigDirection
            // 
            this.comboBoxReadReportConfigDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReadReportConfigDirection.FormattingEnabled = true;
            this.comboBoxReadReportConfigDirection.Location = new System.Drawing.Point(654, 152);
            this.comboBoxReadReportConfigDirection.Name = "comboBoxReadReportConfigDirection";
            this.comboBoxReadReportConfigDirection.Size = new System.Drawing.Size(110, 21);
            this.comboBoxReadReportConfigDirection.TabIndex = 41;
            this.comboBoxReadReportConfigDirection.MouseLeave += new System.EventHandler(this.comboBoxReadReportConfigDirection_MouseLeave);
            this.comboBoxReadReportConfigDirection.MouseHover += new System.EventHandler(this.comboBoxReadReportConfigDirection_MouseHover);
            // 
            // comboBoxReadReportConfigDirIsRx
            // 
            this.comboBoxReadReportConfigDirIsRx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReadReportConfigDirIsRx.FormattingEnabled = true;
            this.comboBoxReadReportConfigDirIsRx.Location = new System.Drawing.Point(884, 152);
            this.comboBoxReadReportConfigDirIsRx.Name = "comboBoxReadReportConfigDirIsRx";
            this.comboBoxReadReportConfigDirIsRx.Size = new System.Drawing.Size(106, 21);
            this.comboBoxReadReportConfigDirIsRx.TabIndex = 43;
            this.comboBoxReadReportConfigDirIsRx.MouseLeave += new System.EventHandler(this.comboBoxReadReportConfigDirIsRx_MouseLeave);
            this.comboBoxReadReportConfigDirIsRx.MouseHover += new System.EventHandler(this.comboBoxReadReportConfigDirIsRx_MouseHover);
            // 
            // comboBoxReadReportConfigAddrMode
            // 
            this.comboBoxReadReportConfigAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReadReportConfigAddrMode.FormattingEnabled = true;
            this.comboBoxReadReportConfigAddrMode.Location = new System.Drawing.Point(93, 153);
            this.comboBoxReadReportConfigAddrMode.Name = "comboBoxReadReportConfigAddrMode";
            this.comboBoxReadReportConfigAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxReadReportConfigAddrMode.TabIndex = 36;
            this.comboBoxReadReportConfigAddrMode.MouseLeave += new System.EventHandler(this.comboBoxReadReportConfigAddrMode_MouseLeave);
            this.comboBoxReadReportConfigAddrMode.MouseHover += new System.EventHandler(this.comboBoxReadReportConfigAddrMode_MouseHover);
            // 
            // buttonReadReportConfig
            // 
            this.buttonReadReportConfig.Location = new System.Drawing.Point(6, 150);
            this.buttonReadReportConfig.Name = "buttonReadReportConfig";
            this.buttonReadReportConfig.Size = new System.Drawing.Size(80, 24);
            this.buttonReadReportConfig.TabIndex = 35;
            this.buttonReadReportConfig.Text = "Read Rprt";
            this.buttonReadReportConfig.UseVisualStyleBackColor = true;
            this.buttonReadReportConfig.Click += new System.EventHandler(this.buttonReadReportConfig_Click);
            // 
            // comboBoxWriteAttribManuSpecific
            // 
            this.comboBoxWriteAttribManuSpecific.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWriteAttribManuSpecific.FormattingEnabled = true;
            this.comboBoxWriteAttribManuSpecific.Location = new System.Drawing.Point(998, 37);
            this.comboBoxWriteAttribManuSpecific.Name = "comboBoxWriteAttribManuSpecific";
            this.comboBoxWriteAttribManuSpecific.Size = new System.Drawing.Size(106, 21);
            this.comboBoxWriteAttribManuSpecific.TabIndex = 19;
            this.comboBoxWriteAttribManuSpecific.MouseLeave += new System.EventHandler(this.comboBoxWriteAttribManuSpecific_MouseLeave);
            this.comboBoxWriteAttribManuSpecific.MouseHover += new System.EventHandler(this.comboBoxWriteAttribManuSpecific_MouseHover);
            // 
            // comboBoxReadAttribManuSpecific
            // 
            this.comboBoxReadAttribManuSpecific.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReadAttribManuSpecific.FormattingEnabled = true;
            this.comboBoxReadAttribManuSpecific.Location = new System.Drawing.Point(884, 8);
            this.comboBoxReadAttribManuSpecific.Name = "comboBoxReadAttribManuSpecific";
            this.comboBoxReadAttribManuSpecific.Size = new System.Drawing.Size(106, 21);
            this.comboBoxReadAttribManuSpecific.TabIndex = 8;
            this.comboBoxReadAttribManuSpecific.MouseLeave += new System.EventHandler(this.comboBoxReadAttribManuSpecific_MouseLeave);
            this.comboBoxReadAttribManuSpecific.MouseHover += new System.EventHandler(this.comboBoxReadAttribManuSpecific_MouseHover);
            // 
            // comboBoxConfigReportAddrMode
            // 
            this.comboBoxConfigReportAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConfigReportAddrMode.FormattingEnabled = true;
            this.comboBoxConfigReportAddrMode.Location = new System.Drawing.Point(93, 66);
            this.comboBoxConfigReportAddrMode.Name = "comboBoxConfigReportAddrMode";
            this.comboBoxConfigReportAddrMode.Size = new System.Drawing.Size(106, 21);
            this.comboBoxConfigReportAddrMode.TabIndex = 22;
            this.comboBoxConfigReportAddrMode.MouseLeave += new System.EventHandler(this.comboBoxConfigReportAddrMode_MouseLeave);
            this.comboBoxConfigReportAddrMode.MouseHover += new System.EventHandler(this.comboBoxConfigReportAddrMode_MouseHover);
            // 
            // comboBoxWriteAttribDirection
            // 
            this.comboBoxWriteAttribDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWriteAttribDirection.FormattingEnabled = true;
            this.comboBoxWriteAttribDirection.Location = new System.Drawing.Point(545, 37);
            this.comboBoxWriteAttribDirection.Name = "comboBoxWriteAttribDirection";
            this.comboBoxWriteAttribDirection.Size = new System.Drawing.Size(106, 21);
            this.comboBoxWriteAttribDirection.TabIndex = 15;
            this.comboBoxWriteAttribDirection.MouseLeave += new System.EventHandler(this.comboBoxWriteAttribDirection_MouseLeave);
            this.comboBoxWriteAttribDirection.MouseHover += new System.EventHandler(this.comboBoxWriteAttribDirection_MouseHover);
            // 
            // comboBoxDiscoverAttributesDirection
            // 
            this.comboBoxDiscoverAttributesDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscoverAttributesDirection.FormattingEnabled = true;
            this.comboBoxDiscoverAttributesDirection.Location = new System.Drawing.Point(654, 210);
            this.comboBoxDiscoverAttributesDirection.Name = "comboBoxDiscoverAttributesDirection";
            this.comboBoxDiscoverAttributesDirection.Size = new System.Drawing.Size(106, 21);
            this.comboBoxDiscoverAttributesDirection.TabIndex = 56;
            this.comboBoxDiscoverAttributesDirection.MouseLeave += new System.EventHandler(this.comboBoxDiscoverAttributesDirection_MouseLeave);
            this.comboBoxDiscoverAttributesDirection.MouseHover += new System.EventHandler(this.comboBoxDiscoverAttributesDirection_MouseHover);
            // 
            // buttonDiscoverAttributes
            // 
            this.buttonDiscoverAttributes.Location = new System.Drawing.Point(6, 208);
            this.buttonDiscoverAttributes.Name = "buttonDiscoverAttributes";
            this.buttonDiscoverAttributes.Size = new System.Drawing.Size(95, 22);
            this.buttonDiscoverAttributes.TabIndex = 50;
            this.buttonDiscoverAttributes.Text = "Disc Attribs";
            this.buttonDiscoverAttributes.UseVisualStyleBackColor = true;
            this.buttonDiscoverAttributes.Click += new System.EventHandler(this.buttonDiscoverAttributes_Click);
            // 
            // comboBoxReadAllAttribDirection
            // 
            this.comboBoxReadAllAttribDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReadAllAttribDirection.FormattingEnabled = true;
            this.comboBoxReadAllAttribDirection.Location = new System.Drawing.Point(541, 181);
            this.comboBoxReadAllAttribDirection.Name = "comboBoxReadAllAttribDirection";
            this.comboBoxReadAllAttribDirection.Size = new System.Drawing.Size(106, 21);
            this.comboBoxReadAllAttribDirection.TabIndex = 49;
            this.comboBoxReadAllAttribDirection.Visible = false;
            this.comboBoxReadAllAttribDirection.MouseLeave += new System.EventHandler(this.comboBoxReadAllAttribDirection_MouseLeave);
            this.comboBoxReadAllAttribDirection.MouseHover += new System.EventHandler(this.comboBoxReadAllAttribDirection_MouseHover);
            // 
            // buttonReadAllAttrib
            // 
            this.buttonReadAllAttrib.Location = new System.Drawing.Point(6, 182);
            this.buttonReadAllAttrib.Name = "buttonReadAllAttrib";
            this.buttonReadAllAttrib.Size = new System.Drawing.Size(95, 20);
            this.buttonReadAllAttrib.TabIndex = 44;
            this.buttonReadAllAttrib.Text = "Read All Attrib";
            this.buttonReadAllAttrib.UseVisualStyleBackColor = true;
            this.buttonReadAllAttrib.Visible = false;
            // 
            // comboBoxConfigReportAttribDirection
            // 
            this.comboBoxConfigReportAttribDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConfigReportAttribDirection.FormattingEnabled = true;
            this.comboBoxConfigReportAttribDirection.Location = new System.Drawing.Point(771, 66);
            this.comboBoxConfigReportAttribDirection.Name = "comboBoxConfigReportAttribDirection";
            this.comboBoxConfigReportAttribDirection.Size = new System.Drawing.Size(107, 21);
            this.comboBoxConfigReportAttribDirection.TabIndex = 28;
            this.comboBoxConfigReportAttribDirection.SelectedIndexChanged += new System.EventHandler(this.comboBoxConfigReportAttribDirection_SelectedIndexChanged);
            this.comboBoxConfigReportAttribDirection.MouseLeave += new System.EventHandler(this.comboBoxConfigReportAttribDirection_MouseLeave);
            this.comboBoxConfigReportAttribDirection.MouseHover += new System.EventHandler(this.comboBoxConfigReportAttribDirection_MouseHover);
            // 
            // comboBoxConfigReportDirection
            // 
            this.comboBoxConfigReportDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConfigReportDirection.FormattingEnabled = true;
            this.comboBoxConfigReportDirection.Location = new System.Drawing.Point(658, 66);
            this.comboBoxConfigReportDirection.Name = "comboBoxConfigReportDirection";
            this.comboBoxConfigReportDirection.Size = new System.Drawing.Size(106, 21);
            this.comboBoxConfigReportDirection.TabIndex = 27;
            this.comboBoxConfigReportDirection.MouseLeave += new System.EventHandler(this.comboBoxConfigReportDirection_MouseLeave);
            this.comboBoxConfigReportDirection.MouseHover += new System.EventHandler(this.comboBoxConfigReportDirection_MouseHover);
            // 
            // buttonConfigReport
            // 
            this.buttonConfigReport.Location = new System.Drawing.Point(6, 63);
            this.buttonConfigReport.Name = "buttonConfigReport";
            this.buttonConfigReport.Size = new System.Drawing.Size(80, 24);
            this.buttonConfigReport.TabIndex = 21;
            this.buttonConfigReport.Text = "Config Rprt";
            this.buttonConfigReport.UseVisualStyleBackColor = true;
            this.buttonConfigReport.Click += new System.EventHandler(this.buttonConfigReport_Click_1);
            // 
            // buttonWriteAttrib
            // 
            this.buttonWriteAttrib.Location = new System.Drawing.Point(6, 34);
            this.buttonWriteAttrib.Name = "buttonWriteAttrib";
            this.buttonWriteAttrib.Size = new System.Drawing.Size(80, 22);
            this.buttonWriteAttrib.TabIndex = 10;
            this.buttonWriteAttrib.Text = "Write Attrib";
            this.buttonWriteAttrib.UseVisualStyleBackColor = true;
            this.buttonWriteAttrib.Click += new System.EventHandler(this.buttonWriteAttrib_Click_1);
            // 
            // comboBoxReadAttribDirection
            // 
            this.comboBoxReadAttribDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReadAttribDirection.FormattingEnabled = true;
            this.comboBoxReadAttribDirection.Location = new System.Drawing.Point(545, 8);
            this.comboBoxReadAttribDirection.Name = "comboBoxReadAttribDirection";
            this.comboBoxReadAttribDirection.Size = new System.Drawing.Size(106, 21);
            this.comboBoxReadAttribDirection.TabIndex = 5;
            this.comboBoxReadAttribDirection.MouseLeave += new System.EventHandler(this.comboBoxReadAttribDirection_MouseLeave);
            this.comboBoxReadAttribDirection.MouseHover += new System.EventHandler(this.comboBoxReadAttribDirection_MouseHover);
            // 
            // buttonReadAttrib
            // 
            this.buttonReadAttrib.Location = new System.Drawing.Point(6, 6);
            this.buttonReadAttrib.Name = "buttonReadAttrib";
            this.buttonReadAttrib.Size = new System.Drawing.Size(80, 22);
            this.buttonReadAttrib.TabIndex = 0;
            this.buttonReadAttrib.Text = "Read Attrib";
            this.buttonReadAttrib.UseVisualStyleBackColor = true;
            this.buttonReadAttrib.Click += new System.EventHandler(this.buttonReadAttrib_Click_1);
            // 
            // tabPageDevice
            // 
            this.tabPageDevice.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageDevice.Controls.Add(this.textBoxSECADDNETKEYSEQ);
            this.tabPageDevice.Controls.Add(this.textBoxSECNETKEYSEQ);
            this.tabPageDevice.Controls.Add(this.buttonSECSWITCHNETKEY);
            this.tabPageDevice.Controls.Add(this.textBoxSECNEWNETKEY);
            this.tabPageDevice.Controls.Add(this.buttonSECADDNEWNETKEY);
            this.tabPageDevice.Controls.Add(this.buttonCopyAddr);
            this.tabPageDevice.Controls.Add(this.buttonDiscoverDevices);
            this.tabPageDevice.Controls.Add(this.textBoxExtAddr);
            this.tabPageDevice.Controls.Add(this.label16);
            this.tabPageDevice.Controls.Add(this.comboBoxAddressList);
            this.tabPageDevice.Location = new System.Drawing.Point(4, 22);
            this.tabPageDevice.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageDevice.Name = "tabPageDevice";
            this.tabPageDevice.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageDevice.Size = new System.Drawing.Size(1833, 584);
            this.tabPageDevice.TabIndex = 18;
            this.tabPageDevice.Text = "Discover Devices";
            // 
            // textBoxSECADDNETKEYSEQ
            // 
            this.textBoxSECADDNETKEYSEQ.Location = new System.Drawing.Point(110, 146);
            this.textBoxSECADDNETKEYSEQ.Name = "textBoxSECADDNETKEYSEQ";
            this.textBoxSECADDNETKEYSEQ.Size = new System.Drawing.Size(82, 20);
            this.textBoxSECADDNETKEYSEQ.TabIndex = 11;
            // 
            // textBoxSECNETKEYSEQ
            // 
            this.textBoxSECNETKEYSEQ.Location = new System.Drawing.Point(124, 177);
            this.textBoxSECNETKEYSEQ.Name = "textBoxSECNETKEYSEQ";
            this.textBoxSECNETKEYSEQ.Size = new System.Drawing.Size(60, 20);
            this.textBoxSECNETKEYSEQ.TabIndex = 10;
            // 
            // buttonSECSWITCHNETKEY
            // 
            this.buttonSECSWITCHNETKEY.Location = new System.Drawing.Point(28, 174);
            this.buttonSECSWITCHNETKEY.Name = "buttonSECSWITCHNETKEY";
            this.buttonSECSWITCHNETKEY.Size = new System.Drawing.Size(90, 23);
            this.buttonSECSWITCHNETKEY.TabIndex = 9;
            this.buttonSECSWITCHNETKEY.Text = "Switch net key";
            this.buttonSECSWITCHNETKEY.UseVisualStyleBackColor = true;
            this.buttonSECSWITCHNETKEY.Click += new System.EventHandler(this.buttonSECSWITCHNETKEY_Click);
            // 
            // textBoxSECNEWNETKEY
            // 
            this.textBoxSECNEWNETKEY.Location = new System.Drawing.Point(198, 147);
            this.textBoxSECNEWNETKEY.Name = "textBoxSECNEWNETKEY";
            this.textBoxSECNEWNETKEY.Size = new System.Drawing.Size(168, 20);
            this.textBoxSECNEWNETKEY.TabIndex = 8;
            // 
            // buttonSECADDNEWNETKEY
            // 
            this.buttonSECADDNEWNETKEY.Location = new System.Drawing.Point(28, 144);
            this.buttonSECADDNEWNETKEY.Name = "buttonSECADDNEWNETKEY";
            this.buttonSECADDNEWNETKEY.Size = new System.Drawing.Size(75, 23);
            this.buttonSECADDNEWNETKEY.TabIndex = 7;
            this.buttonSECADDNEWNETKEY.Text = "Add net key";
            this.buttonSECADDNEWNETKEY.UseVisualStyleBackColor = true;
            this.buttonSECADDNEWNETKEY.Click += new System.EventHandler(this.buttonSECADDNEWNETKEY_Click);
            // 
            // buttonCopyAddr
            // 
            this.buttonCopyAddr.Location = new System.Drawing.Point(212, 86);
            this.buttonCopyAddr.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCopyAddr.Name = "buttonCopyAddr";
            this.buttonCopyAddr.Size = new System.Drawing.Size(97, 26);
            this.buttonCopyAddr.TabIndex = 3;
            this.buttonCopyAddr.Text = "Copy";
            this.buttonCopyAddr.UseVisualStyleBackColor = true;
            this.buttonCopyAddr.Click += new System.EventHandler(this.buttonCopyAddr_Click);
            // 
            // buttonDiscoverDevices
            // 
            this.buttonDiscoverDevices.Location = new System.Drawing.Point(27, 30);
            this.buttonDiscoverDevices.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDiscoverDevices.Name = "buttonDiscoverDevices";
            this.buttonDiscoverDevices.Size = new System.Drawing.Size(180, 26);
            this.buttonDiscoverDevices.TabIndex = 0;
            this.buttonDiscoverDevices.Text = "Discover devices";
            this.buttonDiscoverDevices.UseVisualStyleBackColor = true;
            this.buttonDiscoverDevices.Click += new System.EventHandler(this.buttonDiscoverDevices_Click);
            // 
            // textBoxExtAddr
            // 
            this.textBoxExtAddr.Location = new System.Drawing.Point(27, 94);
            this.textBoxExtAddr.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxExtAddr.Name = "textBoxExtAddr";
            this.textBoxExtAddr.ReadOnly = true;
            this.textBoxExtAddr.Size = new System.Drawing.Size(181, 20);
            this.textBoxExtAddr.TabIndex = 2;
            this.textBoxExtAddr.Text = " ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(25, 78);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "Extended Address";
            // 
            // comboBoxAddressList
            // 
            this.comboBoxAddressList.FormattingEnabled = true;
            this.comboBoxAddressList.Location = new System.Drawing.Point(212, 34);
            this.comboBoxAddressList.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxAddressList.Name = "comboBoxAddressList";
            this.comboBoxAddressList.Size = new System.Drawing.Size(98, 21);
            this.comboBoxAddressList.TabIndex = 1;
            this.comboBoxAddressList.SelectedIndexChanged += new System.EventHandler(this.comboBoxAddressList_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.comboBoxNciCmd);
            this.tabPage2.Controls.Add(this.buttonNciCmd);
            this.tabPage2.Controls.Add(this.textBoxPollInterval);
            this.tabPage2.Controls.Add(this.textBoxBindTargetExtAddr);
            this.tabPage2.Controls.Add(this.textBoxUserSetReqDescription);
            this.tabPage2.Controls.Add(this.textBoxUserSetReqAddr);
            this.tabPage2.Controls.Add(this.textBoxUserReqAddr);
            this.tabPage2.Controls.Add(this.textBoxRestoreNwkFrameCounter);
            this.tabPage2.Controls.Add(this.textBoxLeaveAddr);
            this.tabPage2.Controls.Add(this.textBoxRemoveChildAddr);
            this.tabPage2.Controls.Add(this.textBoxRemoveParentAddr);
            this.tabPage2.Controls.Add(this.textBoxMgmtLeaveExtAddr);
            this.tabPage2.Controls.Add(this.textBoxMgmtLeaveAddr);
            this.tabPage2.Controls.Add(this.textBoxUnBindDestEP);
            this.tabPage2.Controls.Add(this.textBoxUnBindDestAddr);
            this.tabPage2.Controls.Add(this.textBoxUnBindClusterID);
            this.tabPage2.Controls.Add(this.textBoxUnBindTargetEP);
            this.tabPage2.Controls.Add(this.textBoxUnBindTargetExtAddr);
            this.tabPage2.Controls.Add(this.textBoxBindDestEP);
            this.tabPage2.Controls.Add(this.textBoxBindDestAddr);
            this.tabPage2.Controls.Add(this.textBoxBindClusterID);
            this.tabPage2.Controls.Add(this.textBoxBindTargetEP);
            this.tabPage2.Controls.Add(this.textBoxLqiReqStartIndex);
            this.tabPage2.Controls.Add(this.textBoxLqiReqTargetAddr);
            this.tabPage2.Controls.Add(this.textBoxNwkAddrReqStartIndex);
            this.tabPage2.Controls.Add(this.textBoxNwkAddrReqExtAddr);
            this.tabPage2.Controls.Add(this.textBoxNwkAddrReqTargetAddr);
            this.tabPage2.Controls.Add(this.textBoxIeeeReqStartIndex);
            this.tabPage2.Controls.Add(this.textBoxIeeeReqAddr);
            this.tabPage2.Controls.Add(this.textBoxIeeeReqTargetAddr);
            this.tabPage2.Controls.Add(this.textBoxComplexReqAddr);
            this.tabPage2.Controls.Add(this.textBoxMatchReqOutputClusters);
            this.tabPage2.Controls.Add(this.textBoxMatchReqInputClusters);
            this.tabPage2.Controls.Add(this.textBoxMatchReqProfileID);
            this.tabPage2.Controls.Add(this.textBoxMatchReqNbrOutputClusters);
            this.tabPage2.Controls.Add(this.textBoxMatchReqNbrInputClusters);
            this.tabPage2.Controls.Add(this.textBoxMatchReqAddr);
            this.tabPage2.Controls.Add(this.textBoxActiveEpAddr);
            this.tabPage2.Controls.Add(this.textBoxPowerReqAddr);
            this.tabPage2.Controls.Add(this.textBoxSimpleReqEndPoint);
            this.tabPage2.Controls.Add(this.textBoxSimpleReqAddr);
            this.tabPage2.Controls.Add(this.textBoxNodeDescReq);
            this.tabPage2.Controls.Add(this.textBoxPermitJoinInterval);
            this.tabPage2.Controls.Add(this.textBoxPermitJoinAddr);
            this.tabPage2.Controls.Add(this.textBoxSetSecurityKeySeqNbr);
            this.tabPage2.Controls.Add(this.textBoxSetEPID);
            this.tabPage2.Controls.Add(this.textBoxSetCMSK);
            this.tabPage2.Controls.Add(this.buttonPollInterval);
            this.tabPage2.Controls.Add(this.buttonNWKState);
            this.tabPage2.Controls.Add(this.buttonDiscoveryOnly);
            this.tabPage2.Controls.Add(this.buttonUserSetReq);
            this.tabPage2.Controls.Add(this.buttonUserReq);
            this.tabPage2.Controls.Add(this.comboBoxLeaveChildren);
            this.tabPage2.Controls.Add(this.comboBoxLeaveReJoin);
            this.tabPage2.Controls.Add(this.buttonLeave);
            this.tabPage2.Controls.Add(this.buttonRemoveDevice);
            this.tabPage2.Controls.Add(this.buttonPermitJoinState);
            this.tabPage2.Controls.Add(this.buttonRestoreNwk);
            this.tabPage2.Controls.Add(this.buttonRecoverNwk);
            this.tabPage2.Controls.Add(this.comboBoxMgmtLeaveChildren);
            this.tabPage2.Controls.Add(this.comboBoxMgmtLeaveReJoin);
            this.tabPage2.Controls.Add(this.buttonMgmtLeave);
            this.tabPage2.Controls.Add(this.comboBoxUnBindAddrMode);
            this.tabPage2.Controls.Add(this.buttonUnBind);
            this.tabPage2.Controls.Add(this.comboBoxBindAddrMode);
            this.tabPage2.Controls.Add(this.buttonMgmtLqiReq);
            this.tabPage2.Controls.Add(this.buttonStartScan);
            this.tabPage2.Controls.Add(this.comboBoxNwkAddrReqType);
            this.tabPage2.Controls.Add(this.comboBoxIeeeReqType);
            this.tabPage2.Controls.Add(this.buttonComplexReq);
            this.tabPage2.Controls.Add(this.buttonMatchReq);
            this.tabPage2.Controls.Add(this.buttonActiveEpReq);
            this.tabPage2.Controls.Add(this.buttonPowerDescReq);
            this.tabPage2.Controls.Add(this.buttonSimpleDescReq);
            this.tabPage2.Controls.Add(this.buttonNodeDescReq);
            this.tabPage2.Controls.Add(this.buttonIeeeAddrReq);
            this.tabPage2.Controls.Add(this.buttonNwkAddrReq);
            this.tabPage2.Controls.Add(this.comboBoxSecurityKey);
            this.tabPage2.Controls.Add(this.comboBoxPermitJoinTCsignificance);
            this.tabPage2.Controls.Add(this.buttonPermitJoin);
            this.tabPage2.Controls.Add(this.comboBoxSetKeyType);
            this.tabPage2.Controls.Add(this.comboBoxSetKeyState);
            this.tabPage2.Controls.Add(this.comboBoxSetType);
            this.tabPage2.Controls.Add(this.buttonStartNWK);
            this.tabPage2.Controls.Add(this.buttonBind);
            this.tabPage2.Controls.Add(this.buttonErasePD);
            this.tabPage2.Controls.Add(this.buttonReset);
            this.tabPage2.Controls.Add(this.buttonSetDeviceType);
            this.tabPage2.Controls.Add(this.buttonSetSecurity);
            this.tabPage2.Controls.Add(this.buttonSetCMSK);
            this.tabPage2.Controls.Add(this.buttonSetEPID);
            this.tabPage2.Controls.Add(this.buttonGetVersion);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1833, 584);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Management";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // comboBoxNciCmd
            // 
            this.comboBoxNciCmd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNciCmd.FormattingEnabled = true;
            this.comboBoxNciCmd.Location = new System.Drawing.Point(838, 354);
            this.comboBoxNciCmd.Name = "comboBoxNciCmd";
            this.comboBoxNciCmd.Size = new System.Drawing.Size(129, 21);
            this.comboBoxNciCmd.TabIndex = 91;
            // 
            // buttonNciCmd
            // 
            this.buttonNciCmd.Location = new System.Drawing.Point(752, 354);
            this.buttonNciCmd.Name = "buttonNciCmd";
            this.buttonNciCmd.Size = new System.Drawing.Size(80, 22);
            this.buttonNciCmd.TabIndex = 90;
            this.buttonNciCmd.Text = "NCI Cmd";
            this.buttonNciCmd.UseVisualStyleBackColor = true;
            this.buttonNciCmd.Click += new System.EventHandler(this.buttonNciCmd_Click);
            // 
            // textBoxPollInterval
            // 
            this.textBoxPollInterval.Location = new System.Drawing.Point(95, 386);
            this.textBoxPollInterval.Name = "textBoxPollInterval";
            this.textBoxPollInterval.Size = new System.Drawing.Size(129, 20);
            this.textBoxPollInterval.TabIndex = 89;
            this.textBoxPollInterval.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPollInterval_MouseClick);
            this.textBoxPollInterval.MouseLeave += new System.EventHandler(this.textBoxPollInterval_MouseLeave);
            this.textBoxPollInterval.MouseHover += new System.EventHandler(this.textBoxPollInterval_MouseHover);
            // 
            // textBoxBindTargetExtAddr
            // 
            this.textBoxBindTargetExtAddr.Location = new System.Drawing.Point(93, 326);
            this.textBoxBindTargetExtAddr.Name = "textBoxBindTargetExtAddr";
            this.textBoxBindTargetExtAddr.Size = new System.Drawing.Size(129, 20);
            this.textBoxBindTargetExtAddr.TabIndex = 43;
            this.textBoxBindTargetExtAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxBindTargetExtAddr_MouseClick);
            this.textBoxBindTargetExtAddr.Leave += new System.EventHandler(this.textBoxBindTargetExtAddr_Leave);
            this.textBoxBindTargetExtAddr.MouseLeave += new System.EventHandler(this.textBoxBindTargetExtAddr_MouseLeave);
            this.textBoxBindTargetExtAddr.MouseHover += new System.EventHandler(this.textBoxBindTargetExtAddr_MouseHover);
            // 
            // textBoxUserSetReqDescription
            // 
            this.textBoxUserSetReqDescription.Location = new System.Drawing.Point(951, 239);
            this.textBoxUserSetReqDescription.Name = "textBoxUserSetReqDescription";
            this.textBoxUserSetReqDescription.Size = new System.Drawing.Size(160, 20);
            this.textBoxUserSetReqDescription.TabIndex = 81;
            this.textBoxUserSetReqDescription.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxUserSetReqDescription_MouseClick);
            this.textBoxUserSetReqDescription.Leave += new System.EventHandler(this.textBoxUserSetReqDescription_Leave);
            this.textBoxUserSetReqDescription.MouseLeave += new System.EventHandler(this.textBoxUserSetReqDescription_MouseLeave);
            this.textBoxUserSetReqDescription.MouseHover += new System.EventHandler(this.textBoxUserSetReqDescription_MouseHover);
            // 
            // textBoxUserSetReqAddr
            // 
            this.textBoxUserSetReqAddr.Location = new System.Drawing.Point(838, 239);
            this.textBoxUserSetReqAddr.Name = "textBoxUserSetReqAddr";
            this.textBoxUserSetReqAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxUserSetReqAddr.TabIndex = 80;
            this.textBoxUserSetReqAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxUserSetReqAddr_MouseClick);
            this.textBoxUserSetReqAddr.Leave += new System.EventHandler(this.textBoxUserSetReqAddr_Leave);
            this.textBoxUserSetReqAddr.MouseLeave += new System.EventHandler(this.textBoxUserSetReqAddr_MouseLeave);
            this.textBoxUserSetReqAddr.MouseHover += new System.EventHandler(this.textBoxUserSetReqAddr_MouseHover);
            // 
            // textBoxUserReqAddr
            // 
            this.textBoxUserReqAddr.Location = new System.Drawing.Point(838, 210);
            this.textBoxUserReqAddr.Name = "textBoxUserReqAddr";
            this.textBoxUserReqAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxUserReqAddr.TabIndex = 78;
            this.textBoxUserReqAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxUserReqAddr_MouseClick);
            this.textBoxUserReqAddr.Leave += new System.EventHandler(this.textBoxUserReqAddr_Leave);
            this.textBoxUserReqAddr.MouseLeave += new System.EventHandler(this.textBoxUserReqAddr_MouseLeave);
            this.textBoxUserReqAddr.MouseHover += new System.EventHandler(this.textBoxUserReqAddr_MouseHover);
            // 
            // textBoxRestoreNwkFrameCounter
            // 
            this.textBoxRestoreNwkFrameCounter.Location = new System.Drawing.Point(838, 327);
            this.textBoxRestoreNwkFrameCounter.Name = "textBoxRestoreNwkFrameCounter";
            this.textBoxRestoreNwkFrameCounter.Size = new System.Drawing.Size(163, 20);
            this.textBoxRestoreNwkFrameCounter.TabIndex = 87;
            this.textBoxRestoreNwkFrameCounter.Visible = false;
            this.textBoxRestoreNwkFrameCounter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRestoreNwkFrameCounter_MouseClick);
            this.textBoxRestoreNwkFrameCounter.MouseLeave += new System.EventHandler(this.textBoxRestoreNwkFrameCounter_MouseLeave);
            this.textBoxRestoreNwkFrameCounter.MouseHover += new System.EventHandler(this.textBoxRestoreNwkFrameCounter_MouseHover);
            // 
            // textBoxLeaveAddr
            // 
            this.textBoxLeaveAddr.Location = new System.Drawing.Point(93, 182);
            this.textBoxLeaveAddr.Name = "textBoxLeaveAddr";
            this.textBoxLeaveAddr.Size = new System.Drawing.Size(209, 20);
            this.textBoxLeaveAddr.TabIndex = 24;
            this.textBoxLeaveAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLeaveAddr_MouseClick);
            this.textBoxLeaveAddr.Leave += new System.EventHandler(this.textBoxLeaveAddr_Leave);
            this.textBoxLeaveAddr.MouseLeave += new System.EventHandler(this.textBoxLeaveAddr_MouseLeave);
            this.textBoxLeaveAddr.MouseHover += new System.EventHandler(this.textBoxLeaveAddr_MouseHover);
            // 
            // textBoxRemoveChildAddr
            // 
            this.textBoxRemoveChildAddr.Location = new System.Drawing.Point(321, 212);
            this.textBoxRemoveChildAddr.Name = "textBoxRemoveChildAddr";
            this.textBoxRemoveChildAddr.Size = new System.Drawing.Size(219, 20);
            this.textBoxRemoveChildAddr.TabIndex = 29;
            this.textBoxRemoveChildAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveChildAddr_MouseClick);
            this.textBoxRemoveChildAddr.Leave += new System.EventHandler(this.textBoxRemoveChildAddr_Leave);
            this.textBoxRemoveChildAddr.MouseLeave += new System.EventHandler(this.textBoxRemoveChildAddr_MouseLeave);
            this.textBoxRemoveChildAddr.MouseHover += new System.EventHandler(this.textBoxRemoveChildAddr_MouseHover);
            // 
            // textBoxRemoveParentAddr
            // 
            this.textBoxRemoveParentAddr.Location = new System.Drawing.Point(93, 212);
            this.textBoxRemoveParentAddr.Name = "textBoxRemoveParentAddr";
            this.textBoxRemoveParentAddr.Size = new System.Drawing.Size(219, 20);
            this.textBoxRemoveParentAddr.TabIndex = 28;
            this.textBoxRemoveParentAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxRemoveParentAddr_MouseClick);
            this.textBoxRemoveParentAddr.Leave += new System.EventHandler(this.textBoxRemoveParentAddr_Leave);
            this.textBoxRemoveParentAddr.MouseLeave += new System.EventHandler(this.textBoxRemoveParentAddr_MouseLeave);
            this.textBoxRemoveParentAddr.MouseHover += new System.EventHandler(this.textBoxRemoveParentAddr_MouseHover);
            // 
            // textBoxMgmtLeaveExtAddr
            // 
            this.textBoxMgmtLeaveExtAddr.Location = new System.Drawing.Point(206, 154);
            this.textBoxMgmtLeaveExtAddr.Name = "textBoxMgmtLeaveExtAddr";
            this.textBoxMgmtLeaveExtAddr.Size = new System.Drawing.Size(209, 20);
            this.textBoxMgmtLeaveExtAddr.TabIndex = 20;
            this.textBoxMgmtLeaveExtAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMgmtLeaveExtAddr_MouseClick);
            this.textBoxMgmtLeaveExtAddr.Leave += new System.EventHandler(this.textBoxMgmtLeaveExtAddr_Leave);
            this.textBoxMgmtLeaveExtAddr.MouseLeave += new System.EventHandler(this.textBoxMgmtLeaveExtAddr_MouseLeave);
            this.textBoxMgmtLeaveExtAddr.MouseHover += new System.EventHandler(this.textBoxMgmtLeaveExtAddr_MouseHover);
            // 
            // textBoxMgmtLeaveAddr
            // 
            this.textBoxMgmtLeaveAddr.Location = new System.Drawing.Point(93, 154);
            this.textBoxMgmtLeaveAddr.Name = "textBoxMgmtLeaveAddr";
            this.textBoxMgmtLeaveAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxMgmtLeaveAddr.TabIndex = 19;
            this.textBoxMgmtLeaveAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMgmtLeaveAddr_MouseClick);
            this.textBoxMgmtLeaveAddr.Leave += new System.EventHandler(this.textBoxMgmtLeaveAddr_Leave);
            this.textBoxMgmtLeaveAddr.MouseLeave += new System.EventHandler(this.textBoxMgmtLeaveAddr_MouseLeave);
            this.textBoxMgmtLeaveAddr.MouseHover += new System.EventHandler(this.textBoxMgmtLeaveAddr_MouseHover);
            // 
            // textBoxUnBindDestEP
            // 
            this.textBoxUnBindDestEP.Location = new System.Drawing.Point(652, 351);
            this.textBoxUnBindDestEP.Name = "textBoxUnBindDestEP";
            this.textBoxUnBindDestEP.Size = new System.Drawing.Size(88, 20);
            this.textBoxUnBindDestEP.TabIndex = 55;
            this.textBoxUnBindDestEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxUnBindDestEP_MouseClick);
            this.textBoxUnBindDestEP.Leave += new System.EventHandler(this.textBoxUnBindDestEP_Leave);
            this.textBoxUnBindDestEP.MouseLeave += new System.EventHandler(this.textBoxUnBindDestEP_MouseLeave);
            this.textBoxUnBindDestEP.MouseHover += new System.EventHandler(this.textBoxUnBindDestEP_MouseHover);
            // 
            // textBoxUnBindDestAddr
            // 
            this.textBoxUnBindDestAddr.Location = new System.Drawing.Point(411, 351);
            this.textBoxUnBindDestAddr.Name = "textBoxUnBindDestAddr";
            this.textBoxUnBindDestAddr.Size = new System.Drawing.Size(133, 20);
            this.textBoxUnBindDestAddr.TabIndex = 53;
            this.textBoxUnBindDestAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxUnBindDestAddr_MouseClick);
            this.textBoxUnBindDestAddr.Leave += new System.EventHandler(this.textBoxUnBindDestAddr_Leave);
            this.textBoxUnBindDestAddr.MouseLeave += new System.EventHandler(this.textBoxUnBindDestAddr_MouseLeave);
            this.textBoxUnBindDestAddr.MouseHover += new System.EventHandler(this.textBoxUnBindDestAddr_MouseHover);
            // 
            // textBoxUnBindClusterID
            // 
            this.textBoxUnBindClusterID.Location = new System.Drawing.Point(550, 351);
            this.textBoxUnBindClusterID.Name = "textBoxUnBindClusterID";
            this.textBoxUnBindClusterID.Size = new System.Drawing.Size(96, 20);
            this.textBoxUnBindClusterID.TabIndex = 54;
            this.textBoxUnBindClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxUnBindClusterID_MouseClick);
            this.textBoxUnBindClusterID.Leave += new System.EventHandler(this.textBoxUnBindClusterID_Leave);
            this.textBoxUnBindClusterID.MouseLeave += new System.EventHandler(this.textBoxUnBindClusterID_MouseLeave);
            this.textBoxUnBindClusterID.MouseHover += new System.EventHandler(this.textBoxUnBindClusterID_MouseHover);
            // 
            // textBoxUnBindTargetEP
            // 
            this.textBoxUnBindTargetEP.Location = new System.Drawing.Point(228, 351);
            this.textBoxUnBindTargetEP.Name = "textBoxUnBindTargetEP";
            this.textBoxUnBindTargetEP.Size = new System.Drawing.Size(84, 20);
            this.textBoxUnBindTargetEP.TabIndex = 51;
            this.textBoxUnBindTargetEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxUnBindTargetEP_MouseClick);
            this.textBoxUnBindTargetEP.Leave += new System.EventHandler(this.textBoxUnBindTargetEP_Leave);
            this.textBoxUnBindTargetEP.MouseLeave += new System.EventHandler(this.textBoxUnBindTargetEP_MouseLeave);
            this.textBoxUnBindTargetEP.MouseHover += new System.EventHandler(this.textBoxUnBindTargetEP_MouseHover);
            // 
            // textBoxUnBindTargetExtAddr
            // 
            this.textBoxUnBindTargetExtAddr.Location = new System.Drawing.Point(93, 353);
            this.textBoxUnBindTargetExtAddr.Name = "textBoxUnBindTargetExtAddr";
            this.textBoxUnBindTargetExtAddr.Size = new System.Drawing.Size(129, 20);
            this.textBoxUnBindTargetExtAddr.TabIndex = 50;
            this.textBoxUnBindTargetExtAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxUnBindTargetExtAddr_MouseClick);
            this.textBoxUnBindTargetExtAddr.Leave += new System.EventHandler(this.textBoxUnBindTargetExtAddr_Leave);
            this.textBoxUnBindTargetExtAddr.MouseLeave += new System.EventHandler(this.textBoxUnBindTargetExtAddr_MouseLeave);
            this.textBoxUnBindTargetExtAddr.MouseHover += new System.EventHandler(this.textBoxUnBindTargetExtAddr_MouseHover);
            // 
            // textBoxBindDestEP
            // 
            this.textBoxBindDestEP.Location = new System.Drawing.Point(652, 327);
            this.textBoxBindDestEP.Name = "textBoxBindDestEP";
            this.textBoxBindDestEP.Size = new System.Drawing.Size(88, 20);
            this.textBoxBindDestEP.TabIndex = 48;
            this.textBoxBindDestEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxBindDestEP_MouseClick);
            this.textBoxBindDestEP.Leave += new System.EventHandler(this.textBoxBindDestEP_Leave);
            this.textBoxBindDestEP.MouseLeave += new System.EventHandler(this.textBoxBindDestEP_MouseLeave);
            this.textBoxBindDestEP.MouseHover += new System.EventHandler(this.textBoxBindDestEP_MouseHover);
            // 
            // textBoxBindDestAddr
            // 
            this.textBoxBindDestAddr.Location = new System.Drawing.Point(411, 326);
            this.textBoxBindDestAddr.Name = "textBoxBindDestAddr";
            this.textBoxBindDestAddr.Size = new System.Drawing.Size(133, 20);
            this.textBoxBindDestAddr.TabIndex = 46;
            this.textBoxBindDestAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxBindDestAddr_MouseClick);
            this.textBoxBindDestAddr.Leave += new System.EventHandler(this.textBoxBindDestAddr_Leave);
            this.textBoxBindDestAddr.MouseLeave += new System.EventHandler(this.textBoxBindDestAddr_MouseLeave);
            this.textBoxBindDestAddr.MouseHover += new System.EventHandler(this.textBoxBindDestAddr_MouseHover);
            // 
            // textBoxBindClusterID
            // 
            this.textBoxBindClusterID.Location = new System.Drawing.Point(550, 327);
            this.textBoxBindClusterID.Name = "textBoxBindClusterID";
            this.textBoxBindClusterID.Size = new System.Drawing.Size(96, 20);
            this.textBoxBindClusterID.TabIndex = 47;
            this.textBoxBindClusterID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxBindClusterID_MouseClick);
            this.textBoxBindClusterID.Leave += new System.EventHandler(this.textBoxBindClusterID_Leave);
            this.textBoxBindClusterID.MouseLeave += new System.EventHandler(this.textBoxBindClusterID_MouseLeave);
            this.textBoxBindClusterID.MouseHover += new System.EventHandler(this.textBoxBindClusterID_MouseHover);
            // 
            // textBoxBindTargetEP
            // 
            this.textBoxBindTargetEP.Location = new System.Drawing.Point(228, 326);
            this.textBoxBindTargetEP.Name = "textBoxBindTargetEP";
            this.textBoxBindTargetEP.Size = new System.Drawing.Size(84, 20);
            this.textBoxBindTargetEP.TabIndex = 44;
            this.textBoxBindTargetEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxBindTargetEP_MouseClick);
            this.textBoxBindTargetEP.Leave += new System.EventHandler(this.textBoxBindTargetEP_Leave);
            this.textBoxBindTargetEP.MouseLeave += new System.EventHandler(this.textBoxBindTargetEP_MouseLeave);
            this.textBoxBindTargetEP.MouseHover += new System.EventHandler(this.textBoxBindTargetEP_MouseHover);
            // 
            // textBoxLqiReqStartIndex
            // 
            this.textBoxLqiReqStartIndex.Location = new System.Drawing.Point(951, 268);
            this.textBoxLqiReqStartIndex.Name = "textBoxLqiReqStartIndex";
            this.textBoxLqiReqStartIndex.Size = new System.Drawing.Size(106, 20);
            this.textBoxLqiReqStartIndex.TabIndex = 84;
            this.textBoxLqiReqStartIndex.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLqiReqStartIndex_MouseClick);
            this.textBoxLqiReqStartIndex.Leave += new System.EventHandler(this.textBoxLqiReqStartIndex_Leave);
            this.textBoxLqiReqStartIndex.MouseLeave += new System.EventHandler(this.textBoxLqiReqStartIndex_MouseLeave);
            this.textBoxLqiReqStartIndex.MouseHover += new System.EventHandler(this.textBoxLqiReqStartIndex_MouseHover);
            // 
            // textBoxLqiReqTargetAddr
            // 
            this.textBoxLqiReqTargetAddr.Location = new System.Drawing.Point(838, 269);
            this.textBoxLqiReqTargetAddr.Name = "textBoxLqiReqTargetAddr";
            this.textBoxLqiReqTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxLqiReqTargetAddr.TabIndex = 83;
            this.textBoxLqiReqTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLqiReqTargetAddr_MouseClick);
            this.textBoxLqiReqTargetAddr.Leave += new System.EventHandler(this.textBoxLqiReqTargetAddr_Leave);
            this.textBoxLqiReqTargetAddr.MouseLeave += new System.EventHandler(this.textBoxLqiReqTargetAddr_MouseLeave);
            this.textBoxLqiReqTargetAddr.MouseHover += new System.EventHandler(this.textBoxLqiReqTargetAddr_MouseHover);
            // 
            // textBoxNwkAddrReqStartIndex
            // 
            this.textBoxNwkAddrReqStartIndex.Location = new System.Drawing.Point(1200, 64);
            this.textBoxNwkAddrReqStartIndex.Name = "textBoxNwkAddrReqStartIndex";
            this.textBoxNwkAddrReqStartIndex.Size = new System.Drawing.Size(106, 20);
            this.textBoxNwkAddrReqStartIndex.TabIndex = 67;
            this.textBoxNwkAddrReqStartIndex.Leave += new System.EventHandler(this.textBoxNwkAddrReqStartIndex_Leave);
            this.textBoxNwkAddrReqStartIndex.MouseLeave += new System.EventHandler(this.textBoxNwkAddrReqStartIndex_MouseLeave);
            this.textBoxNwkAddrReqStartIndex.MouseHover += new System.EventHandler(this.textBoxNwkAddrReqStartIndex_MouseHover);
            // 
            // textBoxNwkAddrReqExtAddr
            // 
            this.textBoxNwkAddrReqExtAddr.Location = new System.Drawing.Point(951, 65);
            this.textBoxNwkAddrReqExtAddr.Name = "textBoxNwkAddrReqExtAddr";
            this.textBoxNwkAddrReqExtAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxNwkAddrReqExtAddr.TabIndex = 65;
            this.textBoxNwkAddrReqExtAddr.Leave += new System.EventHandler(this.textBoxNwkAddrReqExtAddr_Leave);
            this.textBoxNwkAddrReqExtAddr.MouseLeave += new System.EventHandler(this.textBoxNwkAddrReqExtAddr_MouseLeave);
            this.textBoxNwkAddrReqExtAddr.MouseHover += new System.EventHandler(this.textBoxNwkAddrReqExtAddr_MouseHover);
            // 
            // textBoxNwkAddrReqTargetAddr
            // 
            this.textBoxNwkAddrReqTargetAddr.Location = new System.Drawing.Point(838, 65);
            this.textBoxNwkAddrReqTargetAddr.Name = "textBoxNwkAddrReqTargetAddr";
            this.textBoxNwkAddrReqTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxNwkAddrReqTargetAddr.TabIndex = 64;
            this.textBoxNwkAddrReqTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxNwkAddrReqTargetAddr_MouseClick);
            this.textBoxNwkAddrReqTargetAddr.Leave += new System.EventHandler(this.textBoxNwkAddrReqTargetAddr_Leave);
            this.textBoxNwkAddrReqTargetAddr.MouseLeave += new System.EventHandler(this.textBoxNwkAddrReqTargetAddr_MouseLeave);
            this.textBoxNwkAddrReqTargetAddr.MouseHover += new System.EventHandler(this.textBoxNwkAddrReqTargetAddr_MouseHover);
            // 
            // textBoxIeeeReqStartIndex
            // 
            this.textBoxIeeeReqStartIndex.Location = new System.Drawing.Point(1200, 35);
            this.textBoxIeeeReqStartIndex.Name = "textBoxIeeeReqStartIndex";
            this.textBoxIeeeReqStartIndex.Size = new System.Drawing.Size(106, 20);
            this.textBoxIeeeReqStartIndex.TabIndex = 62;
            this.textBoxIeeeReqStartIndex.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIeeeReqStartIndex_MouseClick);
            this.textBoxIeeeReqStartIndex.Leave += new System.EventHandler(this.textBoxIeeeReqStartIndex_Leave);
            this.textBoxIeeeReqStartIndex.MouseLeave += new System.EventHandler(this.textBoxIeeeReqStartIndex_MouseLeave);
            this.textBoxIeeeReqStartIndex.MouseHover += new System.EventHandler(this.textBoxIeeeReqStartIndex_MouseHover);
            // 
            // textBoxIeeeReqAddr
            // 
            this.textBoxIeeeReqAddr.Location = new System.Drawing.Point(951, 37);
            this.textBoxIeeeReqAddr.Name = "textBoxIeeeReqAddr";
            this.textBoxIeeeReqAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxIeeeReqAddr.TabIndex = 60;
            this.textBoxIeeeReqAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIeeeReqAddr_MouseClick);
            this.textBoxIeeeReqAddr.Leave += new System.EventHandler(this.textBoxIeeeReqAddr_Leave);
            this.textBoxIeeeReqAddr.MouseLeave += new System.EventHandler(this.textBoxIeeeReqAddr_MouseLeave);
            this.textBoxIeeeReqAddr.MouseHover += new System.EventHandler(this.textBoxIeeeReqAddr_MouseHover);
            // 
            // textBoxIeeeReqTargetAddr
            // 
            this.textBoxIeeeReqTargetAddr.Location = new System.Drawing.Point(838, 37);
            this.textBoxIeeeReqTargetAddr.Name = "textBoxIeeeReqTargetAddr";
            this.textBoxIeeeReqTargetAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxIeeeReqTargetAddr.TabIndex = 59;
            this.textBoxIeeeReqTargetAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxIeeeReqTargetAddr_MouseClick);
            this.textBoxIeeeReqTargetAddr.Leave += new System.EventHandler(this.textBoxIeeeReqTargetAddr_Leave);
            this.textBoxIeeeReqTargetAddr.MouseLeave += new System.EventHandler(this.textBoxIeeeReqTargetAddr_MouseLeave);
            this.textBoxIeeeReqTargetAddr.MouseHover += new System.EventHandler(this.textBoxIeeeReqTargetAddr_MouseHover);
            // 
            // textBoxComplexReqAddr
            // 
            this.textBoxComplexReqAddr.Location = new System.Drawing.Point(838, 182);
            this.textBoxComplexReqAddr.Name = "textBoxComplexReqAddr";
            this.textBoxComplexReqAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxComplexReqAddr.TabIndex = 76;
            this.textBoxComplexReqAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxComplexReqAddr_MouseClick);
            this.textBoxComplexReqAddr.Leave += new System.EventHandler(this.textBoxComplexReqAddr_Leave);
            this.textBoxComplexReqAddr.MouseLeave += new System.EventHandler(this.textBoxComplexReqAddr_MouseLeave);
            this.textBoxComplexReqAddr.MouseHover += new System.EventHandler(this.textBoxComplexReqAddr_MouseHover);
            // 
            // textBoxMatchReqOutputClusters
            // 
            this.textBoxMatchReqOutputClusters.Location = new System.Drawing.Point(637, 298);
            this.textBoxMatchReqOutputClusters.Name = "textBoxMatchReqOutputClusters";
            this.textBoxMatchReqOutputClusters.Size = new System.Drawing.Size(103, 20);
            this.textBoxMatchReqOutputClusters.TabIndex = 41;
            this.textBoxMatchReqOutputClusters.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMatchReqOutputClusters_MouseClick);
            this.textBoxMatchReqOutputClusters.Leave += new System.EventHandler(this.textBoxMatchReqOutputClusters_Leave);
            this.textBoxMatchReqOutputClusters.MouseLeave += new System.EventHandler(this.textBoxMatchReqOutputClusters_MouseLeave);
            this.textBoxMatchReqOutputClusters.MouseHover += new System.EventHandler(this.textBoxMatchReqOutputClusters_MouseHover);
            // 
            // textBoxMatchReqInputClusters
            // 
            this.textBoxMatchReqInputClusters.Location = new System.Drawing.Point(422, 298);
            this.textBoxMatchReqInputClusters.Name = "textBoxMatchReqInputClusters";
            this.textBoxMatchReqInputClusters.Size = new System.Drawing.Size(106, 20);
            this.textBoxMatchReqInputClusters.TabIndex = 39;
            this.textBoxMatchReqInputClusters.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMatchReqInputClusters_MouseClick);
            this.textBoxMatchReqInputClusters.Leave += new System.EventHandler(this.textBoxMatchReqInputClusters_Leave);
            this.textBoxMatchReqInputClusters.MouseLeave += new System.EventHandler(this.textBoxMatchReqInputClusters_MouseLeave);
            this.textBoxMatchReqInputClusters.MouseHover += new System.EventHandler(this.textBoxMatchReqInputClusters_MouseHover);
            // 
            // textBoxMatchReqProfileID
            // 
            this.textBoxMatchReqProfileID.Location = new System.Drawing.Point(206, 298);
            this.textBoxMatchReqProfileID.Name = "textBoxMatchReqProfileID";
            this.textBoxMatchReqProfileID.Size = new System.Drawing.Size(106, 20);
            this.textBoxMatchReqProfileID.TabIndex = 37;
            this.textBoxMatchReqProfileID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMatchReqProfileID_MouseClick);
            this.textBoxMatchReqProfileID.Leave += new System.EventHandler(this.textBoxMatchReqProfileID_Leave);
            this.textBoxMatchReqProfileID.MouseLeave += new System.EventHandler(this.textBoxMatchReqProfileID_MouseLeave);
            this.textBoxMatchReqProfileID.MouseHover += new System.EventHandler(this.textBoxMatchReqProfileID_MouseHover);
            // 
            // textBoxMatchReqNbrOutputClusters
            // 
            this.textBoxMatchReqNbrOutputClusters.Location = new System.Drawing.Point(534, 298);
            this.textBoxMatchReqNbrOutputClusters.Name = "textBoxMatchReqNbrOutputClusters";
            this.textBoxMatchReqNbrOutputClusters.Size = new System.Drawing.Size(96, 20);
            this.textBoxMatchReqNbrOutputClusters.TabIndex = 40;
            this.textBoxMatchReqNbrOutputClusters.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMatchReqNbrOutputClusters_MouseClick);
            this.textBoxMatchReqNbrOutputClusters.Leave += new System.EventHandler(this.textBoxMatchReqNbrOutputClusters_Leave);
            this.textBoxMatchReqNbrOutputClusters.MouseLeave += new System.EventHandler(this.textBoxMatchReqNbrOutputClusters_MouseLeave);
            this.textBoxMatchReqNbrOutputClusters.MouseHover += new System.EventHandler(this.textBoxMatchReqNbrOutputClusters_MouseHover);
            // 
            // textBoxMatchReqNbrInputClusters
            // 
            this.textBoxMatchReqNbrInputClusters.Location = new System.Drawing.Point(319, 298);
            this.textBoxMatchReqNbrInputClusters.Name = "textBoxMatchReqNbrInputClusters";
            this.textBoxMatchReqNbrInputClusters.Size = new System.Drawing.Size(96, 20);
            this.textBoxMatchReqNbrInputClusters.TabIndex = 38;
            this.textBoxMatchReqNbrInputClusters.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMatchReqNbrInputClusters_MouseClick);
            this.textBoxMatchReqNbrInputClusters.Leave += new System.EventHandler(this.textBoxMatchReqNbrInputClusters_Leave);
            this.textBoxMatchReqNbrInputClusters.MouseLeave += new System.EventHandler(this.textBoxMatchReqNbrInputClusters_MouseLeave);
            this.textBoxMatchReqNbrInputClusters.MouseHover += new System.EventHandler(this.textBoxMatchReqNbrInputClusters_MouseHover);
            // 
            // textBoxMatchReqAddr
            // 
            this.textBoxMatchReqAddr.Location = new System.Drawing.Point(93, 298);
            this.textBoxMatchReqAddr.Name = "textBoxMatchReqAddr";
            this.textBoxMatchReqAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxMatchReqAddr.TabIndex = 36;
            this.textBoxMatchReqAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxMatchReqAddr_MouseClick);
            this.textBoxMatchReqAddr.Leave += new System.EventHandler(this.textBoxMatchReqAddr_Leave);
            this.textBoxMatchReqAddr.MouseLeave += new System.EventHandler(this.textBoxMatchReqAddr_MouseLeave);
            this.textBoxMatchReqAddr.MouseHover += new System.EventHandler(this.textBoxMatchReqAddr_MouseHover);
            // 
            // textBoxActiveEpAddr
            // 
            this.textBoxActiveEpAddr.Location = new System.Drawing.Point(838, 8);
            this.textBoxActiveEpAddr.Name = "textBoxActiveEpAddr";
            this.textBoxActiveEpAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxActiveEpAddr.TabIndex = 57;
            this.textBoxActiveEpAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxActiveEpAddr_MouseClick);
            this.textBoxActiveEpAddr.Leave += new System.EventHandler(this.textBoxActiveEpAddr_Leave);
            this.textBoxActiveEpAddr.MouseLeave += new System.EventHandler(this.textBoxActiveEpAddr_MouseLeave);
            this.textBoxActiveEpAddr.MouseHover += new System.EventHandler(this.textBoxActiveEpAddr_MouseHover);
            // 
            // textBoxPowerReqAddr
            // 
            this.textBoxPowerReqAddr.Location = new System.Drawing.Point(838, 124);
            this.textBoxPowerReqAddr.Name = "textBoxPowerReqAddr";
            this.textBoxPowerReqAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxPowerReqAddr.TabIndex = 71;
            this.textBoxPowerReqAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPowerReqAddr_MouseClick);
            this.textBoxPowerReqAddr.Leave += new System.EventHandler(this.textBoxPowerReqAddr_Leave);
            this.textBoxPowerReqAddr.MouseLeave += new System.EventHandler(this.textBoxPowerReqAddr_MouseLeave);
            this.textBoxPowerReqAddr.MouseHover += new System.EventHandler(this.textBoxPowerReqAddr_MouseHover);
            // 
            // textBoxSimpleReqEndPoint
            // 
            this.textBoxSimpleReqEndPoint.Location = new System.Drawing.Point(951, 153);
            this.textBoxSimpleReqEndPoint.Name = "textBoxSimpleReqEndPoint";
            this.textBoxSimpleReqEndPoint.Size = new System.Drawing.Size(106, 20);
            this.textBoxSimpleReqEndPoint.TabIndex = 74;
            this.textBoxSimpleReqEndPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSimpleReqEndPoint_MouseClick);
            this.textBoxSimpleReqEndPoint.Leave += new System.EventHandler(this.textBoxSimpleReqEndPoint_Leave);
            this.textBoxSimpleReqEndPoint.MouseLeave += new System.EventHandler(this.textBoxSimpleReqEndPoint_MouseLeave);
            this.textBoxSimpleReqEndPoint.MouseHover += new System.EventHandler(this.textBoxSimpleReqEndPoint_MouseHover);
            // 
            // textBoxSimpleReqAddr
            // 
            this.textBoxSimpleReqAddr.Location = new System.Drawing.Point(838, 153);
            this.textBoxSimpleReqAddr.Name = "textBoxSimpleReqAddr";
            this.textBoxSimpleReqAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxSimpleReqAddr.TabIndex = 73;
            this.textBoxSimpleReqAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSimpleReqAddr_MouseClick);
            this.textBoxSimpleReqAddr.Leave += new System.EventHandler(this.textBoxSimpleReqAddr_Leave);
            this.textBoxSimpleReqAddr.MouseLeave += new System.EventHandler(this.textBoxSimpleReqAddr_MouseLeave);
            this.textBoxSimpleReqAddr.MouseHover += new System.EventHandler(this.textBoxSimpleReqAddr_MouseHover);
            // 
            // textBoxNodeDescReq
            // 
            this.textBoxNodeDescReq.Location = new System.Drawing.Point(838, 94);
            this.textBoxNodeDescReq.Name = "textBoxNodeDescReq";
            this.textBoxNodeDescReq.Size = new System.Drawing.Size(106, 20);
            this.textBoxNodeDescReq.TabIndex = 69;
            this.textBoxNodeDescReq.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxNodeDescReq_MouseClick);
            this.textBoxNodeDescReq.Leave += new System.EventHandler(this.textBoxNodeDescReq_Leave);
            this.textBoxNodeDescReq.MouseLeave += new System.EventHandler(this.textBoxNodeDescReq_MouseLeave);
            this.textBoxNodeDescReq.MouseHover += new System.EventHandler(this.textBoxNodeDescReq_MouseHover);
            // 
            // textBoxPermitJoinInterval
            // 
            this.textBoxPermitJoinInterval.Location = new System.Drawing.Point(206, 238);
            this.textBoxPermitJoinInterval.Name = "textBoxPermitJoinInterval";
            this.textBoxPermitJoinInterval.Size = new System.Drawing.Size(106, 20);
            this.textBoxPermitJoinInterval.TabIndex = 32;
            this.textBoxPermitJoinInterval.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPermitJoinInterval_MouseClick);
            this.textBoxPermitJoinInterval.Leave += new System.EventHandler(this.textBoxPermitJoinInterval_Leave);
            this.textBoxPermitJoinInterval.MouseLeave += new System.EventHandler(this.textBoxPermitJoinInterval_MouseLeave);
            this.textBoxPermitJoinInterval.MouseHover += new System.EventHandler(this.textBoxPermitJoinInterval_MouseHover);
            // 
            // textBoxPermitJoinAddr
            // 
            this.textBoxPermitJoinAddr.Location = new System.Drawing.Point(93, 238);
            this.textBoxPermitJoinAddr.Name = "textBoxPermitJoinAddr";
            this.textBoxPermitJoinAddr.Size = new System.Drawing.Size(106, 20);
            this.textBoxPermitJoinAddr.TabIndex = 31;
            this.textBoxPermitJoinAddr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxPermitJoinAddr_MouseClick);
            this.textBoxPermitJoinAddr.Leave += new System.EventHandler(this.textBoxPermitJoinAddr_Leave);
            this.textBoxPermitJoinAddr.MouseLeave += new System.EventHandler(this.textBoxPermitJoinAddr_MouseLeave);
            this.textBoxPermitJoinAddr.MouseHover += new System.EventHandler(this.textBoxPermitJoinAddr_MouseHover);
            // 
            // textBoxSetSecurityKeySeqNbr
            // 
            this.textBoxSetSecurityKeySeqNbr.Location = new System.Drawing.Point(314, 94);
            this.textBoxSetSecurityKeySeqNbr.Name = "textBoxSetSecurityKeySeqNbr";
            this.textBoxSetSecurityKeySeqNbr.Size = new System.Drawing.Size(55, 20);
            this.textBoxSetSecurityKeySeqNbr.TabIndex = 13;
            this.textBoxSetSecurityKeySeqNbr.Click += new System.EventHandler(this.textBoxSetSecurityKeySeqNbr_Click);
            this.textBoxSetSecurityKeySeqNbr.Leave += new System.EventHandler(this.textBoxSetSecurityKeySeqNbr_Leave);
            // 
            // textBoxSetEPID
            // 
            this.textBoxSetEPID.Location = new System.Drawing.Point(95, 37);
            this.textBoxSetEPID.Name = "textBoxSetEPID";
            this.textBoxSetEPID.Size = new System.Drawing.Size(213, 20);
            this.textBoxSetEPID.TabIndex = 8;
            this.textBoxSetEPID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSetEPID_MouseClick);
            this.textBoxSetEPID.Leave += new System.EventHandler(this.textBoxSetEPID_Leave);
            this.textBoxSetEPID.MouseLeave += new System.EventHandler(this.textBoxSetEPID_MouseLeave);
            this.textBoxSetEPID.MouseHover += new System.EventHandler(this.textBoxSetEPID_MouseHover);
            // 
            // textBoxSetCMSK
            // 
            this.textBoxSetCMSK.Location = new System.Drawing.Point(95, 65);
            this.textBoxSetCMSK.Name = "textBoxSetCMSK";
            this.textBoxSetCMSK.Size = new System.Drawing.Size(213, 20);
            this.textBoxSetCMSK.TabIndex = 10;
            this.textBoxSetCMSK.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxSetCMSK_MouseClick);
            this.textBoxSetCMSK.Leave += new System.EventHandler(this.textBoxSetCMSK_Leave);
            this.textBoxSetCMSK.MouseLeave += new System.EventHandler(this.textBoxSetCMSK_MouseLeave);
            this.textBoxSetCMSK.MouseHover += new System.EventHandler(this.textBoxSetCMSK_MouseHover);
            // 
            // buttonPollInterval
            // 
            this.buttonPollInterval.Location = new System.Drawing.Point(6, 382);
            this.buttonPollInterval.Name = "buttonPollInterval";
            this.buttonPollInterval.Size = new System.Drawing.Size(80, 25);
            this.buttonPollInterval.TabIndex = 88;
            this.buttonPollInterval.Text = "PollInterval";
            this.buttonPollInterval.UseVisualStyleBackColor = true;
            this.buttonPollInterval.Click += new System.EventHandler(this.PollInterval_Click);
            // 
            // buttonNWKState
            // 
            this.buttonNWKState.Location = new System.Drawing.Point(752, 383);
            this.buttonNWKState.Name = "buttonNWKState";
            this.buttonNWKState.Size = new System.Drawing.Size(105, 22);
            this.buttonNWKState.TabIndex = 88;
            this.buttonNWKState.Text = "Network State";
            this.buttonNWKState.UseVisualStyleBackColor = true;
            this.buttonNWKState.Click += new System.EventHandler(this.buttonNWKState_Click);
            // 
            // buttonDiscoveryOnly
            // 
            this.buttonDiscoveryOnly.Location = new System.Drawing.Point(438, 6);
            this.buttonDiscoveryOnly.Name = "buttonDiscoveryOnly";
            this.buttonDiscoveryOnly.Size = new System.Drawing.Size(118, 22);
            this.buttonDiscoveryOnly.TabIndex = 6;
            this.buttonDiscoveryOnly.Text = "Scan Discovery Only";
            this.buttonDiscoveryOnly.UseVisualStyleBackColor = true;
            this.buttonDiscoveryOnly.Visible = false;
            this.buttonDiscoveryOnly.Click += new System.EventHandler(this.buttonDiscoveryOnly_Click);
            // 
            // buttonUserSetReq
            // 
            this.buttonUserSetReq.Location = new System.Drawing.Point(752, 238);
            this.buttonUserSetReq.Name = "buttonUserSetReq";
            this.buttonUserSetReq.Size = new System.Drawing.Size(80, 22);
            this.buttonUserSetReq.TabIndex = 79;
            this.buttonUserSetReq.Text = "UserSetReq";
            this.buttonUserSetReq.UseVisualStyleBackColor = true;
            this.buttonUserSetReq.Click += new System.EventHandler(this.buttonUserSetReq_Click);
            // 
            // buttonUserReq
            // 
            this.buttonUserReq.Location = new System.Drawing.Point(752, 209);
            this.buttonUserReq.Name = "buttonUserReq";
            this.buttonUserReq.Size = new System.Drawing.Size(80, 22);
            this.buttonUserReq.TabIndex = 77;
            this.buttonUserReq.Text = "UserReq";
            this.buttonUserReq.UseVisualStyleBackColor = true;
            this.buttonUserReq.Click += new System.EventHandler(this.buttonUserReq_Click);
            // 
            // comboBoxLeaveChildren
            // 
            this.comboBoxLeaveChildren.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLeaveChildren.FormattingEnabled = true;
            this.comboBoxLeaveChildren.Location = new System.Drawing.Point(434, 182);
            this.comboBoxLeaveChildren.Name = "comboBoxLeaveChildren";
            this.comboBoxLeaveChildren.Size = new System.Drawing.Size(196, 21);
            this.comboBoxLeaveChildren.TabIndex = 26;
            // 
            // comboBoxLeaveReJoin
            // 
            this.comboBoxLeaveReJoin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLeaveReJoin.FormattingEnabled = true;
            this.comboBoxLeaveReJoin.Location = new System.Drawing.Point(308, 182);
            this.comboBoxLeaveReJoin.Name = "comboBoxLeaveReJoin";
            this.comboBoxLeaveReJoin.Size = new System.Drawing.Size(119, 21);
            this.comboBoxLeaveReJoin.TabIndex = 25;
            // 
            // buttonLeave
            // 
            this.buttonLeave.Location = new System.Drawing.Point(6, 180);
            this.buttonLeave.Name = "buttonLeave";
            this.buttonLeave.Size = new System.Drawing.Size(80, 22);
            this.buttonLeave.TabIndex = 23;
            this.buttonLeave.Text = "Leave";
            this.buttonLeave.UseVisualStyleBackColor = true;
            this.buttonLeave.Click += new System.EventHandler(this.buttonLeave_Click);
            // 
            // buttonRemoveDevice
            // 
            this.buttonRemoveDevice.Location = new System.Drawing.Point(6, 210);
            this.buttonRemoveDevice.Name = "buttonRemoveDevice";
            this.buttonRemoveDevice.Size = new System.Drawing.Size(80, 22);
            this.buttonRemoveDevice.TabIndex = 27;
            this.buttonRemoveDevice.Text = "Remove";
            this.buttonRemoveDevice.UseVisualStyleBackColor = true;
            this.buttonRemoveDevice.Click += new System.EventHandler(this.buttonRemoveDevice_Click);
            // 
            // buttonPermitJoinState
            // 
            this.buttonPermitJoinState.Location = new System.Drawing.Point(6, 267);
            this.buttonPermitJoinState.Name = "buttonPermitJoinState";
            this.buttonPermitJoinState.Size = new System.Drawing.Size(105, 22);
            this.buttonPermitJoinState.TabIndex = 34;
            this.buttonPermitJoinState.Text = "Permit Join State";
            this.buttonPermitJoinState.UseVisualStyleBackColor = true;
            this.buttonPermitJoinState.Click += new System.EventHandler(this.buttonPermitJoinState_Click);
            // 
            // buttonRestoreNwk
            // 
            this.buttonRestoreNwk.Location = new System.Drawing.Point(752, 324);
            this.buttonRestoreNwk.Name = "buttonRestoreNwk";
            this.buttonRestoreNwk.Size = new System.Drawing.Size(80, 22);
            this.buttonRestoreNwk.TabIndex = 86;
            this.buttonRestoreNwk.Text = "RES NWK";
            this.buttonRestoreNwk.UseVisualStyleBackColor = true;
            this.buttonRestoreNwk.Visible = false;
            this.buttonRestoreNwk.Click += new System.EventHandler(this.buttonRestoreNwk_Click);
            // 
            // buttonRecoverNwk
            // 
            this.buttonRecoverNwk.Location = new System.Drawing.Point(752, 295);
            this.buttonRecoverNwk.Name = "buttonRecoverNwk";
            this.buttonRecoverNwk.Size = new System.Drawing.Size(80, 22);
            this.buttonRecoverNwk.TabIndex = 85;
            this.buttonRecoverNwk.Text = "REC NWK";
            this.buttonRecoverNwk.UseVisualStyleBackColor = true;
            this.buttonRecoverNwk.Visible = false;
            this.buttonRecoverNwk.Click += new System.EventHandler(this.buttonRecoverNwk_Click);
            // 
            // comboBoxMgmtLeaveChildren
            // 
            this.comboBoxMgmtLeaveChildren.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMgmtLeaveChildren.FormattingEnabled = true;
            this.comboBoxMgmtLeaveChildren.Location = new System.Drawing.Point(547, 154);
            this.comboBoxMgmtLeaveChildren.Name = "comboBoxMgmtLeaveChildren";
            this.comboBoxMgmtLeaveChildren.Size = new System.Drawing.Size(196, 21);
            this.comboBoxMgmtLeaveChildren.TabIndex = 22;
            // 
            // comboBoxMgmtLeaveReJoin
            // 
            this.comboBoxMgmtLeaveReJoin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMgmtLeaveReJoin.FormattingEnabled = true;
            this.comboBoxMgmtLeaveReJoin.Location = new System.Drawing.Point(422, 154);
            this.comboBoxMgmtLeaveReJoin.Name = "comboBoxMgmtLeaveReJoin";
            this.comboBoxMgmtLeaveReJoin.Size = new System.Drawing.Size(119, 21);
            this.comboBoxMgmtLeaveReJoin.TabIndex = 21;
            // 
            // buttonMgmtLeave
            // 
            this.buttonMgmtLeave.Location = new System.Drawing.Point(6, 152);
            this.buttonMgmtLeave.Name = "buttonMgmtLeave";
            this.buttonMgmtLeave.Size = new System.Drawing.Size(80, 22);
            this.buttonMgmtLeave.TabIndex = 18;
            this.buttonMgmtLeave.Text = "Mgmt Leave";
            this.buttonMgmtLeave.UseVisualStyleBackColor = true;
            this.buttonMgmtLeave.Click += new System.EventHandler(this.buttonMgmtLeave_Click);
            // 
            // comboBoxUnBindAddrMode
            // 
            this.comboBoxUnBindAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnBindAddrMode.FormattingEnabled = true;
            this.comboBoxUnBindAddrMode.Location = new System.Drawing.Point(322, 351);
            this.comboBoxUnBindAddrMode.Name = "comboBoxUnBindAddrMode";
            this.comboBoxUnBindAddrMode.Size = new System.Drawing.Size(84, 21);
            this.comboBoxUnBindAddrMode.TabIndex = 52;
            this.comboBoxUnBindAddrMode.MouseLeave += new System.EventHandler(this.comboBoxUnBindAddrMode_MouseLeave);
            this.comboBoxUnBindAddrMode.MouseHover += new System.EventHandler(this.comboBoxUnBindAddrMode_MouseHover);
            // 
            // buttonUnBind
            // 
            this.buttonUnBind.Location = new System.Drawing.Point(6, 351);
            this.buttonUnBind.Name = "buttonUnBind";
            this.buttonUnBind.Size = new System.Drawing.Size(80, 25);
            this.buttonUnBind.TabIndex = 49;
            this.buttonUnBind.Text = "UnBind";
            this.buttonUnBind.UseVisualStyleBackColor = true;
            this.buttonUnBind.Click += new System.EventHandler(this.buttonUnBind_Click);
            // 
            // comboBoxBindAddrMode
            // 
            this.comboBoxBindAddrMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBindAddrMode.FormattingEnabled = true;
            this.comboBoxBindAddrMode.Location = new System.Drawing.Point(322, 326);
            this.comboBoxBindAddrMode.Name = "comboBoxBindAddrMode";
            this.comboBoxBindAddrMode.Size = new System.Drawing.Size(84, 21);
            this.comboBoxBindAddrMode.TabIndex = 45;
            this.comboBoxBindAddrMode.MouseLeave += new System.EventHandler(this.comboBoxBindAddrMode_MouseLeave);
            this.comboBoxBindAddrMode.MouseHover += new System.EventHandler(this.comboBoxBindAddrMode_MouseHover);
            // 
            // buttonMgmtLqiReq
            // 
            this.buttonMgmtLqiReq.Location = new System.Drawing.Point(752, 267);
            this.buttonMgmtLqiReq.Name = "buttonMgmtLqiReq";
            this.buttonMgmtLqiReq.Size = new System.Drawing.Size(80, 22);
            this.buttonMgmtLqiReq.TabIndex = 82;
            this.buttonMgmtLqiReq.Text = "Lqi Req";
            this.buttonMgmtLqiReq.UseVisualStyleBackColor = true;
            this.buttonMgmtLqiReq.Click += new System.EventHandler(this.buttonMgmtLqiReq_Click_1);
            // 
            // buttonStartScan
            // 
            this.buttonStartScan.Location = new System.Drawing.Point(352, 6);
            this.buttonStartScan.Name = "buttonStartScan";
            this.buttonStartScan.Size = new System.Drawing.Size(80, 22);
            this.buttonStartScan.TabIndex = 5;
            this.buttonStartScan.Text = "Start Scan";
            this.buttonStartScan.UseVisualStyleBackColor = true;
            this.buttonStartScan.Click += new System.EventHandler(this.buttonStartScan_Click);
            // 
            // comboBoxNwkAddrReqType
            // 
            this.comboBoxNwkAddrReqType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNwkAddrReqType.FormattingEnabled = true;
            this.comboBoxNwkAddrReqType.Location = new System.Drawing.Point(1065, 64);
            this.comboBoxNwkAddrReqType.Name = "comboBoxNwkAddrReqType";
            this.comboBoxNwkAddrReqType.Size = new System.Drawing.Size(129, 21);
            this.comboBoxNwkAddrReqType.TabIndex = 66;
            // 
            // comboBoxIeeeReqType
            // 
            this.comboBoxIeeeReqType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIeeeReqType.FormattingEnabled = true;
            this.comboBoxIeeeReqType.Location = new System.Drawing.Point(1065, 35);
            this.comboBoxIeeeReqType.Name = "comboBoxIeeeReqType";
            this.comboBoxIeeeReqType.Size = new System.Drawing.Size(129, 21);
            this.comboBoxIeeeReqType.TabIndex = 61;
            // 
            // buttonComplexReq
            // 
            this.buttonComplexReq.Location = new System.Drawing.Point(752, 180);
            this.buttonComplexReq.Name = "buttonComplexReq";
            this.buttonComplexReq.Size = new System.Drawing.Size(80, 22);
            this.buttonComplexReq.TabIndex = 75;
            this.buttonComplexReq.Text = "ComplexReq";
            this.buttonComplexReq.UseVisualStyleBackColor = true;
            this.buttonComplexReq.Click += new System.EventHandler(this.buttonComplexReq_Click);
            // 
            // buttonMatchReq
            // 
            this.buttonMatchReq.Location = new System.Drawing.Point(6, 295);
            this.buttonMatchReq.Name = "buttonMatchReq";
            this.buttonMatchReq.Size = new System.Drawing.Size(80, 22);
            this.buttonMatchReq.TabIndex = 35;
            this.buttonMatchReq.Text = "Match Req";
            this.buttonMatchReq.UseVisualStyleBackColor = true;
            this.buttonMatchReq.Click += new System.EventHandler(this.buttonMatchReq_Click);
            // 
            // buttonActiveEpReq
            // 
            this.buttonActiveEpReq.Location = new System.Drawing.Point(752, 6);
            this.buttonActiveEpReq.Name = "buttonActiveEpReq";
            this.buttonActiveEpReq.Size = new System.Drawing.Size(80, 22);
            this.buttonActiveEpReq.TabIndex = 56;
            this.buttonActiveEpReq.Text = "Active Req";
            this.buttonActiveEpReq.UseVisualStyleBackColor = true;
            this.buttonActiveEpReq.Click += new System.EventHandler(this.buttonActiveEpReq_Click);
            // 
            // buttonPowerDescReq
            // 
            this.buttonPowerDescReq.Location = new System.Drawing.Point(752, 122);
            this.buttonPowerDescReq.Name = "buttonPowerDescReq";
            this.buttonPowerDescReq.Size = new System.Drawing.Size(80, 22);
            this.buttonPowerDescReq.TabIndex = 70;
            this.buttonPowerDescReq.Text = "Power Req";
            this.buttonPowerDescReq.UseVisualStyleBackColor = true;
            this.buttonPowerDescReq.Click += new System.EventHandler(this.buttonPowerDescReq_Click);
            // 
            // buttonSimpleDescReq
            // 
            this.buttonSimpleDescReq.Location = new System.Drawing.Point(752, 150);
            this.buttonSimpleDescReq.Name = "buttonSimpleDescReq";
            this.buttonSimpleDescReq.Size = new System.Drawing.Size(80, 22);
            this.buttonSimpleDescReq.TabIndex = 72;
            this.buttonSimpleDescReq.Text = "Simple Req";
            this.buttonSimpleDescReq.UseVisualStyleBackColor = true;
            this.buttonSimpleDescReq.Click += new System.EventHandler(this.buttonSimpleDescReq_Click);
            // 
            // buttonNodeDescReq
            // 
            this.buttonNodeDescReq.Location = new System.Drawing.Point(752, 93);
            this.buttonNodeDescReq.Name = "buttonNodeDescReq";
            this.buttonNodeDescReq.Size = new System.Drawing.Size(80, 22);
            this.buttonNodeDescReq.TabIndex = 68;
            this.buttonNodeDescReq.Text = "Node Req";
            this.buttonNodeDescReq.UseVisualStyleBackColor = true;
            this.buttonNodeDescReq.Click += new System.EventHandler(this.buttonNodeDescReq_Click);
            // 
            // buttonIeeeAddrReq
            // 
            this.buttonIeeeAddrReq.Location = new System.Drawing.Point(752, 34);
            this.buttonIeeeAddrReq.Name = "buttonIeeeAddrReq";
            this.buttonIeeeAddrReq.Size = new System.Drawing.Size(80, 22);
            this.buttonIeeeAddrReq.TabIndex = 58;
            this.buttonIeeeAddrReq.Text = "IEEE Req";
            this.buttonIeeeAddrReq.UseVisualStyleBackColor = true;
            this.buttonIeeeAddrReq.Click += new System.EventHandler(this.buttonIeeeAddrReq_Click);
            // 
            // buttonNwkAddrReq
            // 
            this.buttonNwkAddrReq.Location = new System.Drawing.Point(752, 63);
            this.buttonNwkAddrReq.Name = "buttonNwkAddrReq";
            this.buttonNwkAddrReq.Size = new System.Drawing.Size(80, 22);
            this.buttonNwkAddrReq.TabIndex = 63;
            this.buttonNwkAddrReq.Text = "Addr Req";
            this.buttonNwkAddrReq.UseVisualStyleBackColor = true;
            this.buttonNwkAddrReq.Click += new System.EventHandler(this.buttonNwkAddrReq_Click);
            // 
            // comboBoxSecurityKey
            // 
            this.comboBoxSecurityKey.FormattingEnabled = true;
            this.comboBoxSecurityKey.Location = new System.Drawing.Point(512, 94);
            this.comboBoxSecurityKey.Name = "comboBoxSecurityKey";
            this.comboBoxSecurityKey.Size = new System.Drawing.Size(237, 21);
            this.comboBoxSecurityKey.TabIndex = 15;
            // 
            // comboBoxPermitJoinTCsignificance
            // 
            this.comboBoxPermitJoinTCsignificance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPermitJoinTCsignificance.FormattingEnabled = true;
            this.comboBoxPermitJoinTCsignificance.Location = new System.Drawing.Point(319, 238);
            this.comboBoxPermitJoinTCsignificance.Name = "comboBoxPermitJoinTCsignificance";
            this.comboBoxPermitJoinTCsignificance.Size = new System.Drawing.Size(129, 21);
            this.comboBoxPermitJoinTCsignificance.TabIndex = 33;
            // 
            // buttonPermitJoin
            // 
            this.buttonPermitJoin.Location = new System.Drawing.Point(6, 238);
            this.buttonPermitJoin.Name = "buttonPermitJoin";
            this.buttonPermitJoin.Size = new System.Drawing.Size(80, 22);
            this.buttonPermitJoin.TabIndex = 30;
            this.buttonPermitJoin.Text = "Permit Join";
            this.buttonPermitJoin.UseVisualStyleBackColor = true;
            this.buttonPermitJoin.Click += new System.EventHandler(this.buttonPermitJoin_Click);
            // 
            // comboBoxSetKeyType
            // 
            this.comboBoxSetKeyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSetKeyType.FormattingEnabled = true;
            this.comboBoxSetKeyType.Location = new System.Drawing.Point(377, 93);
            this.comboBoxSetKeyType.Name = "comboBoxSetKeyType";
            this.comboBoxSetKeyType.Size = new System.Drawing.Size(129, 21);
            this.comboBoxSetKeyType.TabIndex = 14;
            // 
            // comboBoxSetKeyState
            // 
            this.comboBoxSetKeyState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSetKeyState.FormattingEnabled = true;
            this.comboBoxSetKeyState.Location = new System.Drawing.Point(95, 94);
            this.comboBoxSetKeyState.Name = "comboBoxSetKeyState";
            this.comboBoxSetKeyState.Size = new System.Drawing.Size(213, 21);
            this.comboBoxSetKeyState.TabIndex = 12;
            // 
            // comboBoxSetType
            // 
            this.comboBoxSetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSetType.FormattingEnabled = true;
            this.comboBoxSetType.Location = new System.Drawing.Point(95, 124);
            this.comboBoxSetType.Name = "comboBoxSetType";
            this.comboBoxSetType.Size = new System.Drawing.Size(213, 21);
            this.comboBoxSetType.TabIndex = 17;
            // 
            // buttonStartNWK
            // 
            this.buttonStartNWK.Location = new System.Drawing.Point(266, 6);
            this.buttonStartNWK.Name = "buttonStartNWK";
            this.buttonStartNWK.Size = new System.Drawing.Size(80, 22);
            this.buttonStartNWK.TabIndex = 4;
            this.buttonStartNWK.Text = "Start NWK";
            this.buttonStartNWK.UseVisualStyleBackColor = true;
            this.buttonStartNWK.Click += new System.EventHandler(this.buttonStartNWK_Click);
            // 
            // buttonBind
            // 
            this.buttonBind.Location = new System.Drawing.Point(6, 324);
            this.buttonBind.Name = "buttonBind";
            this.buttonBind.Size = new System.Drawing.Size(80, 22);
            this.buttonBind.TabIndex = 42;
            this.buttonBind.Text = "Bind";
            this.buttonBind.UseVisualStyleBackColor = true;
            this.buttonBind.Click += new System.EventHandler(this.buttonBind_Click);
            // 
            // buttonErasePD
            // 
            this.buttonErasePD.Location = new System.Drawing.Point(6, 6);
            this.buttonErasePD.Name = "buttonErasePD";
            this.buttonErasePD.Size = new System.Drawing.Size(80, 22);
            this.buttonErasePD.TabIndex = 1;
            this.buttonErasePD.Text = "Erase PD";
            this.buttonErasePD.UseVisualStyleBackColor = true;
            this.buttonErasePD.Click += new System.EventHandler(this.buttonErasePD_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(93, 6);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(80, 22);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonSetDeviceType
            // 
            this.buttonSetDeviceType.Location = new System.Drawing.Point(6, 122);
            this.buttonSetDeviceType.Name = "buttonSetDeviceType";
            this.buttonSetDeviceType.Size = new System.Drawing.Size(80, 22);
            this.buttonSetDeviceType.TabIndex = 16;
            this.buttonSetDeviceType.Text = "Set Type";
            this.buttonSetDeviceType.UseVisualStyleBackColor = true;
            this.buttonSetDeviceType.Click += new System.EventHandler(this.buttonSetDeviceType_Click);
            // 
            // buttonSetSecurity
            // 
            this.buttonSetSecurity.Location = new System.Drawing.Point(6, 93);
            this.buttonSetSecurity.Name = "buttonSetSecurity";
            this.buttonSetSecurity.Size = new System.Drawing.Size(80, 22);
            this.buttonSetSecurity.TabIndex = 11;
            this.buttonSetSecurity.Text = "Set Security";
            this.buttonSetSecurity.UseVisualStyleBackColor = true;
            this.buttonSetSecurity.Click += new System.EventHandler(this.buttonSetSecurity_Click);
            // 
            // buttonSetCMSK
            // 
            this.buttonSetCMSK.Location = new System.Drawing.Point(6, 63);
            this.buttonSetCMSK.Name = "buttonSetCMSK";
            this.buttonSetCMSK.Size = new System.Drawing.Size(80, 22);
            this.buttonSetCMSK.TabIndex = 9;
            this.buttonSetCMSK.Text = "Set CMSK";
            this.buttonSetCMSK.UseVisualStyleBackColor = true;
            this.buttonSetCMSK.Click += new System.EventHandler(this.buttonSetCMSK_Click);
            // 
            // buttonSetEPID
            // 
            this.buttonSetEPID.Location = new System.Drawing.Point(6, 34);
            this.buttonSetEPID.Name = "buttonSetEPID";
            this.buttonSetEPID.Size = new System.Drawing.Size(80, 22);
            this.buttonSetEPID.TabIndex = 7;
            this.buttonSetEPID.Text = "Set EPID";
            this.buttonSetEPID.UseVisualStyleBackColor = true;
            this.buttonSetEPID.Click += new System.EventHandler(this.buttonSetEPID_Click);
            // 
            // buttonGetVersion
            // 
            this.buttonGetVersion.Location = new System.Drawing.Point(179, 6);
            this.buttonGetVersion.Name = "buttonGetVersion";
            this.buttonGetVersion.Size = new System.Drawing.Size(80, 22);
            this.buttonGetVersion.TabIndex = 3;
            this.buttonGetVersion.Text = "Get Version";
            this.buttonGetVersion.UseVisualStyleBackColor = true;
            this.buttonGetVersion.Click += new System.EventHandler(this.buttonGetVersion_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPageDevice);
            this.tabControl1.Controls.Add(this.tabPage12);
            this.tabControl1.Controls.Add(this.AHIControl);
            this.tabControl1.Controls.Add(this.BasicClusterTab);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage13);
            this.tabControl1.Controls.Add(this.tabPage15);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Controls.Add(this.tabPage11);
            this.tabControl1.Controls.Add(this.tabPage14);
            this.tabControl1.Controls.Add(this.tabPagePollControl);
            this.tabControl1.Controls.Add(this.tabPage16);
            this.tabControl1.Controls.Add(this.tabPage17);
            this.tabControl1.Controls.Add(this.tabPage18);
            this.tabControl1.Location = new System.Drawing.Point(13, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1841, 610);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage16
            // 
            this.tabPage16.AutoScroll = true;
            this.tabPage16.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage16.Controls.Add(this.buttonEZLNTSOCKETGETIP);
            this.tabPage16.Controls.Add(this.buttonEZLNTSTOPONOFFLOOP);
            this.tabPage16.Controls.Add(this.buttonEZLNTONOFFLOOP);
            this.tabPage16.Controls.Add(this.labelEZLNTLOOPREMAIN);
            this.tabPage16.Controls.Add(this.textBoxEZLNTLOOPREMAIN);
            this.tabPage16.Controls.Add(this.buttonSOCKETSEVERTEST);
            this.tabPage16.Controls.Add(this.buttonSOCKETCLIENTTEST);
            this.tabPage16.Controls.Add(this.textBoxEZLNTSOCKETSEVERIP);
            this.tabPage16.Controls.Add(this.textBoxEZLNTSOCKETCLIENTIP);
            this.tabPage16.Controls.Add(this.buttonEZLNTSOCKETSEVER);
            this.tabPage16.Controls.Add(this.buttonEZLNTSOCKETCLIENT);
            this.tabPage16.Controls.Add(this.comboBoxEZLNTUNICAST);
            this.tabPage16.Controls.Add(this.labelEZLNTBROADCAST);
            this.tabPage16.Controls.Add(this.labelEZLNTUNICAST);
            this.tabPage16.Controls.Add(this.buttonEZLNTBROADSTOPTONGGLE);
            this.tabPage16.Controls.Add(this.buttonEZLNTBROADTONGGLE);
            this.tabPage16.Controls.Add(this.buttonEZLNTBROADOFF);
            this.tabPage16.Controls.Add(this.buttonEZLNTBROADON);
            this.tabPage16.Controls.Add(this.buttonEZLNTSAVELOCAL);
            this.tabPage16.Controls.Add(this.buttonEZLNTLOADREMOTE);
            this.tabPage16.Controls.Add(this.buttonWZLNTPROFRAMCMD);
            this.tabPage16.Controls.Add(this.buttonEZLNTDISABLEPERMIT);
            this.tabPage16.Controls.Add(this.buttonEZLNTPERMIT);
            this.tabPage16.Controls.Add(this.checkBoxEZLNTGROUPALL);
            this.tabPage16.Controls.Add(this.textBoxEZLNTSETINTERVALMAX);
            this.tabPage16.Controls.Add(this.buttonTEMPLOOPSTOP);
            this.tabPage16.Controls.Add(this.buttonEZLNTSATLOOPSTOP);
            this.tabPage16.Controls.Add(this.buttonEZLNTCOLORLOOPSTOP);
            this.tabPage16.Controls.Add(this.buttonEZLNTHUESTOP);
            this.tabPage16.Controls.Add(this.textBoxEZLNTSETDIR);
            this.tabPage16.Controls.Add(this.textBoxEZLNTSETSTEP);
            this.tabPage16.Controls.Add(this.comboBoxEZLNTLEVELWITHONOFF);
            this.tabPage16.Controls.Add(this.buttonEZLNTLEVLELSTOP);
            this.tabPage16.Controls.Add(this.buttonEZLNTIDENTIFYSTOP);
            this.tabPage16.Controls.Add(this.textBoxEZLNTTEMPTIME);
            this.tabPage16.Controls.Add(this.textBoxEZLNTTEMP);
            this.tabPage16.Controls.Add(this.textBoxEZLNTSATTIME);
            this.tabPage16.Controls.Add(this.textBoxEZLNTSAT);
            this.tabPage16.Controls.Add(this.textBoxCOLORTIME);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCOLORY);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCOLORX);
            this.tabPage16.Controls.Add(this.textBoxEZLNTHUETIME);
            this.tabPage16.Controls.Add(this.textBoxEZLNTHUEDIR);
            this.tabPage16.Controls.Add(this.textBoxEZLNTHUE);
            this.tabPage16.Controls.Add(this.textBoxEZLNTLEVELTIME);
            this.tabPage16.Controls.Add(this.textBoxEZLNTLEVEL);
            this.tabPage16.Controls.Add(this.textBoxEZLNTIDENTIFYTIME);
            this.tabPage16.Controls.Add(this.textBoxEZLNTREADRPRTATTRIBUTEID);
            this.tabPage16.Controls.Add(this.textBoxEZLNTREADRPRTCLUSTERID);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCONFIGRPRTCHANGE);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCONFIGRPRTTIMEOUT);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCONFIGRPRTMAXINTERVAL);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCONFIGRPRTMININTERVAL);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCONFIGRPRTATTRIBID);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCONFIGRPRTTYPE);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCONFIGRPRTCLUSTERID);
            this.tabPage16.Controls.Add(this.comboBoxEZLNTLEAVECHILDREN);
            this.tabPage16.Controls.Add(this.comboBoxEZLNTLEAVEREJOIN);
            this.tabPage16.Controls.Add(this.textBoxEZLNTWRITEATTRIBUTEDATA);
            this.tabPage16.Controls.Add(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE);
            this.tabPage16.Controls.Add(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID);
            this.tabPage16.Controls.Add(this.textBoxEZLNTWRITEATTRIBUTECLUSTERID);
            this.tabPage16.Controls.Add(this.textBoxEZLNTATTRIBUTEID);
            this.tabPage16.Controls.Add(this.textBoxEZLNTREADCLUSTERID);
            this.tabPage16.Controls.Add(this.buttonEZLNTMOVETOTEMP);
            this.tabPage16.Controls.Add(this.buttonEZLNTMOVETOSAT);
            this.tabPage16.Controls.Add(this.buttonEZLNTMOVETOCOLOR);
            this.tabPage16.Controls.Add(this.buttonEZLNTMOVETOHUE);
            this.tabPage16.Controls.Add(this.buttonEZLNTMOVETOLEVLEL);
            this.tabPage16.Controls.Add(this.buttonEZLNTIDENTIFY);
            this.tabPage16.Controls.Add(this.buttonEZLNTRESET);
            this.tabPage16.Controls.Add(this.buttonEZLNTREADRPRT);
            this.tabPage16.Controls.Add(this.buttonEZLNTCONFIGRPRT);
            this.tabPage16.Controls.Add(this.buttonEZLNTLEAVE);
            this.tabPage16.Controls.Add(this.buttonEZLNTWRITEATTRIBUTE);
            this.tabPage16.Controls.Add(this.textBoxEZLNTCOMMAND);
            this.tabPage16.Controls.Add(this.buttonEZLNTSENDCOMMAND);
            this.tabPage16.Controls.Add(this.textBoxEZLNTUNBINDCLUSTERID);
            this.tabPage16.Controls.Add(this.textBoxEZLNTBINDCLUSTERID);
            this.tabPage16.Controls.Add(this.buttonEZLNTUNBIND);
            this.tabPage16.Controls.Add(this.buttonEZLNTBIND);
            this.tabPage16.Controls.Add(this.buttonEZLNTTONGGLESTOP);
            this.tabPage16.Controls.Add(this.buttonEZLNTTONGGLE);
            this.tabPage16.Controls.Add(this.buttonEZLNTOFF);
            this.tabPage16.Controls.Add(this.buttonEZLNTON);
            this.tabPage16.Controls.Add(this.textBoxEZLNTSETLOOP);
            this.tabPage16.Controls.Add(this.buttonEZLNTSETTIMER);
            this.tabPage16.Controls.Add(this.buttonEZLNTSTOPREAD);
            this.tabPage16.Controls.Add(this.textBoxEZLNTTIMERINTERVAL);
            this.tabPage16.Controls.Add(this.buttonEZLNTREADATTRIBUTE);
            this.tabPage16.Controls.Add(this.textBoxEZLNTVIEW);
            this.tabPage16.Controls.Add(this.listViewEZLNTGROUP);
            this.tabPage16.Controls.Add(this.buttonREMOVEGROUPALL);
            this.tabPage16.Controls.Add(this.buttonEZLNTVIEWGROUP);
            this.tabPage16.Controls.Add(this.textBoxREMOVEGROUP);
            this.tabPage16.Controls.Add(this.textBoxEZLNTADDGROUP);
            this.tabPage16.Controls.Add(this.buttonEZLNTREMOVEGROUP);
            this.tabPage16.Controls.Add(this.buttonEZLNTADDGROUP);
            this.tabPage16.Controls.Add(this.buttonREFRESHCOM);
            this.tabPage16.Controls.Add(this.buttonPort);
            this.tabPage16.Controls.Add(this.checkBoxEZLNTALL);
            this.tabPage16.Controls.Add(this.listViewEZLNTINFO);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(1833, 584);
            this.tabPage16.TabIndex = 20;
            this.tabPage16.Text = "LNT Local";
            this.tabPage16.Click += new System.EventHandler(this.tabPage16_Click);
            // 
            // buttonEZLNTSTOPONOFFLOOP
            // 
            this.buttonEZLNTSTOPONOFFLOOP.Location = new System.Drawing.Point(1478, 173);
            this.buttonEZLNTSTOPONOFFLOOP.Name = "buttonEZLNTSTOPONOFFLOOP";
            this.buttonEZLNTSTOPONOFFLOOP.Size = new System.Drawing.Size(80, 23);
            this.buttonEZLNTSTOPONOFFLOOP.TabIndex = 316;
            this.buttonEZLNTSTOPONOFFLOOP.Text = "Stop On/Off";
            this.buttonEZLNTSTOPONOFFLOOP.UseVisualStyleBackColor = true;
            this.buttonEZLNTSTOPONOFFLOOP.Click += new System.EventHandler(this.buttonEZLNTSTOPONOFFLOOP_Click);
            // 
            // buttonEZLNTONOFFLOOP
            // 
            this.buttonEZLNTONOFFLOOP.Location = new System.Drawing.Point(1397, 173);
            this.buttonEZLNTONOFFLOOP.Name = "buttonEZLNTONOFFLOOP";
            this.buttonEZLNTONOFFLOOP.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTONOFFLOOP.TabIndex = 315;
            this.buttonEZLNTONOFFLOOP.Text = "On/Off";
            this.buttonEZLNTONOFFLOOP.UseVisualStyleBackColor = true;
            this.buttonEZLNTONOFFLOOP.Click += new System.EventHandler(this.buttonEZLNTONOFFLOOP_Click);
            // 
            // labelEZLNTLOOPREMAIN
            // 
            this.labelEZLNTLOOPREMAIN.AutoSize = true;
            this.labelEZLNTLOOPREMAIN.Location = new System.Drawing.Point(1174, 28);
            this.labelEZLNTLOOPREMAIN.Name = "labelEZLNTLOOPREMAIN";
            this.labelEZLNTLOOPREMAIN.Size = new System.Drawing.Size(65, 13);
            this.labelEZLNTLOOPREMAIN.TabIndex = 314;
            this.labelEZLNTLOOPREMAIN.Text = "Loop Timers";
            // 
            // textBoxEZLNTLOOPREMAIN
            // 
            this.textBoxEZLNTLOOPREMAIN.Location = new System.Drawing.Point(1273, 24);
            this.textBoxEZLNTLOOPREMAIN.Name = "textBoxEZLNTLOOPREMAIN";
            this.textBoxEZLNTLOOPREMAIN.Size = new System.Drawing.Size(61, 20);
            this.textBoxEZLNTLOOPREMAIN.TabIndex = 313;
            // 
            // buttonSOCKETSEVERTEST
            // 
            this.buttonSOCKETSEVERTEST.Location = new System.Drawing.Point(549, 46);
            this.buttonSOCKETSEVERTEST.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSOCKETSEVERTEST.Name = "buttonSOCKETSEVERTEST";
            this.buttonSOCKETSEVERTEST.Size = new System.Drawing.Size(111, 20);
            this.buttonSOCKETSEVERTEST.TabIndex = 237;
            this.buttonSOCKETSEVERTEST.Text = "Socket sever test";
            this.buttonSOCKETSEVERTEST.UseVisualStyleBackColor = true;
            this.buttonSOCKETSEVERTEST.Click += new System.EventHandler(this.buttonSOCKETSEVERTEST_Click);
            // 
            // buttonSOCKETCLIENTTEST
            // 
            this.buttonSOCKETCLIENTTEST.Location = new System.Drawing.Point(433, 46);
            this.buttonSOCKETCLIENTTEST.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSOCKETCLIENTTEST.Name = "buttonSOCKETCLIENTTEST";
            this.buttonSOCKETCLIENTTEST.Size = new System.Drawing.Size(112, 20);
            this.buttonSOCKETCLIENTTEST.TabIndex = 236;
            this.buttonSOCKETCLIENTTEST.Text = "Socket client test";
            this.buttonSOCKETCLIENTTEST.UseVisualStyleBackColor = true;
            this.buttonSOCKETCLIENTTEST.Click += new System.EventHandler(this.buttonSOCKETCLIENTTEST_Click);
            // 
            // textBoxEZLNTSOCKETSEVERIP
            // 
            this.textBoxEZLNTSOCKETSEVERIP.Location = new System.Drawing.Point(330, 47);
            this.textBoxEZLNTSOCKETSEVERIP.Name = "textBoxEZLNTSOCKETSEVERIP";
            this.textBoxEZLNTSOCKETSEVERIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTSOCKETSEVERIP.TabIndex = 235;
            // 
            // textBoxEZLNTSOCKETCLIENTIP
            // 
            this.textBoxEZLNTSOCKETCLIENTIP.Location = new System.Drawing.Point(140, 47);
            this.textBoxEZLNTSOCKETCLIENTIP.Name = "textBoxEZLNTSOCKETCLIENTIP";
            this.textBoxEZLNTSOCKETCLIENTIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTSOCKETCLIENTIP.TabIndex = 234;
            // 
            // buttonEZLNTSOCKETSEVER
            // 
            this.buttonEZLNTSOCKETSEVER.Location = new System.Drawing.Point(244, 44);
            this.buttonEZLNTSOCKETSEVER.Name = "buttonEZLNTSOCKETSEVER";
            this.buttonEZLNTSOCKETSEVER.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTSOCKETSEVER.TabIndex = 233;
            this.buttonEZLNTSOCKETSEVER.Text = "As Sever";
            this.buttonEZLNTSOCKETSEVER.UseVisualStyleBackColor = true;
            this.buttonEZLNTSOCKETSEVER.Click += new System.EventHandler(this.buttonEZLNTSOCKETSEVER_Click);
            // 
            // buttonEZLNTSOCKETCLIENT
            // 
            this.buttonEZLNTSOCKETCLIENT.Location = new System.Drawing.Point(60, 43);
            this.buttonEZLNTSOCKETCLIENT.Name = "buttonEZLNTSOCKETCLIENT";
            this.buttonEZLNTSOCKETCLIENT.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTSOCKETCLIENT.TabIndex = 232;
            this.buttonEZLNTSOCKETCLIENT.Text = "As Client";
            this.buttonEZLNTSOCKETCLIENT.UseVisualStyleBackColor = true;
            this.buttonEZLNTSOCKETCLIENT.Click += new System.EventHandler(this.buttonEZLNTSOCKETCLIENT_Click);
            // 
            // comboBoxEZLNTUNICAST
            // 
            this.comboBoxEZLNTUNICAST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEZLNTUNICAST.FormattingEnabled = true;
            this.comboBoxEZLNTUNICAST.Items.AddRange(new object[] {
            "Unicast",
            "Broadcast"});
            this.comboBoxEZLNTUNICAST.Location = new System.Drawing.Point(1275, 416);
            this.comboBoxEZLNTUNICAST.Name = "comboBoxEZLNTUNICAST";
            this.comboBoxEZLNTUNICAST.Size = new System.Drawing.Size(93, 21);
            this.comboBoxEZLNTUNICAST.TabIndex = 231;
            // 
            // labelEZLNTBROADCAST
            // 
            this.labelEZLNTBROADCAST.AutoSize = true;
            this.labelEZLNTBROADCAST.Location = new System.Drawing.Point(1175, 207);
            this.labelEZLNTBROADCAST.Name = "labelEZLNTBROADCAST";
            this.labelEZLNTBROADCAST.Size = new System.Drawing.Size(55, 13);
            this.labelEZLNTBROADCAST.TabIndex = 230;
            this.labelEZLNTBROADCAST.Text = "Broadcast";
            // 
            // labelEZLNTUNICAST
            // 
            this.labelEZLNTUNICAST.AutoSize = true;
            this.labelEZLNTUNICAST.Location = new System.Drawing.Point(1175, 178);
            this.labelEZLNTUNICAST.Name = "labelEZLNTUNICAST";
            this.labelEZLNTUNICAST.Size = new System.Drawing.Size(43, 13);
            this.labelEZLNTUNICAST.TabIndex = 229;
            this.labelEZLNTUNICAST.Text = "Unicast";
            // 
            // buttonEZLNTBROADSTOPTONGGLE
            // 
            this.buttonEZLNTBROADSTOPTONGGLE.Location = new System.Drawing.Point(1479, 202);
            this.buttonEZLNTBROADSTOPTONGGLE.Name = "buttonEZLNTBROADSTOPTONGGLE";
            this.buttonEZLNTBROADSTOPTONGGLE.Size = new System.Drawing.Size(137, 23);
            this.buttonEZLNTBROADSTOPTONGGLE.TabIndex = 228;
            this.buttonEZLNTBROADSTOPTONGGLE.Text = "Stop Tonggle Loop";
            this.buttonEZLNTBROADSTOPTONGGLE.UseVisualStyleBackColor = true;
            this.buttonEZLNTBROADSTOPTONGGLE.Click += new System.EventHandler(this.buttonEZLNTBROADSTOPTONGGLE_Click);
            // 
            // buttonEZLNTBROADTONGGLE
            // 
            this.buttonEZLNTBROADTONGGLE.Location = new System.Drawing.Point(1397, 202);
            this.buttonEZLNTBROADTONGGLE.Name = "buttonEZLNTBROADTONGGLE";
            this.buttonEZLNTBROADTONGGLE.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTBROADTONGGLE.TabIndex = 227;
            this.buttonEZLNTBROADTONGGLE.Text = "Tonggle";
            this.buttonEZLNTBROADTONGGLE.UseVisualStyleBackColor = true;
            this.buttonEZLNTBROADTONGGLE.Click += new System.EventHandler(this.buttonEZLNTBROADTONGGLE_Click);
            // 
            // buttonEZLNTBROADOFF
            // 
            this.buttonEZLNTBROADOFF.Location = new System.Drawing.Point(1317, 202);
            this.buttonEZLNTBROADOFF.Name = "buttonEZLNTBROADOFF";
            this.buttonEZLNTBROADOFF.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTBROADOFF.TabIndex = 226;
            this.buttonEZLNTBROADOFF.Text = "Off";
            this.buttonEZLNTBROADOFF.UseVisualStyleBackColor = true;
            this.buttonEZLNTBROADOFF.Click += new System.EventHandler(this.buttonEZLNTBROADOFF_Click);
            // 
            // buttonEZLNTBROADON
            // 
            this.buttonEZLNTBROADON.Location = new System.Drawing.Point(1236, 202);
            this.buttonEZLNTBROADON.Name = "buttonEZLNTBROADON";
            this.buttonEZLNTBROADON.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTBROADON.TabIndex = 225;
            this.buttonEZLNTBROADON.Text = "On";
            this.buttonEZLNTBROADON.UseVisualStyleBackColor = true;
            this.buttonEZLNTBROADON.Click += new System.EventHandler(this.buttonEZLNTBROADON_Click);
            // 
            // buttonEZLNTSAVELOCAL
            // 
            this.buttonEZLNTSAVELOCAL.Location = new System.Drawing.Point(421, 14);
            this.buttonEZLNTSAVELOCAL.Name = "buttonEZLNTSAVELOCAL";
            this.buttonEZLNTSAVELOCAL.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTSAVELOCAL.TabIndex = 224;
            this.buttonEZLNTSAVELOCAL.Text = "Save Local";
            this.buttonEZLNTSAVELOCAL.UseVisualStyleBackColor = true;
            this.buttonEZLNTSAVELOCAL.Click += new System.EventHandler(this.buttonEZLNTSAVELOCAL_Click);
            // 
            // buttonEZLNTLOADREMOTE
            // 
            this.buttonEZLNTLOADREMOTE.Location = new System.Drawing.Point(330, 14);
            this.buttonEZLNTLOADREMOTE.Name = "buttonEZLNTLOADREMOTE";
            this.buttonEZLNTLOADREMOTE.Size = new System.Drawing.Size(85, 23);
            this.buttonEZLNTLOADREMOTE.TabIndex = 223;
            this.buttonEZLNTLOADREMOTE.Text = "Load Remote";
            this.buttonEZLNTLOADREMOTE.UseVisualStyleBackColor = true;
            this.buttonEZLNTLOADREMOTE.Click += new System.EventHandler(this.buttonEZLNTLOADREMOTE_Click);
            // 
            // buttonWZLNTPROFRAMCMD
            // 
            this.buttonWZLNTPROFRAMCMD.Location = new System.Drawing.Point(238, 13);
            this.buttonWZLNTPROFRAMCMD.Name = "buttonWZLNTPROFRAMCMD";
            this.buttonWZLNTPROFRAMCMD.Size = new System.Drawing.Size(86, 23);
            this.buttonWZLNTPROFRAMCMD.TabIndex = 222;
            this.buttonWZLNTPROFRAMCMD.Text = "Program";
            this.buttonWZLNTPROFRAMCMD.UseVisualStyleBackColor = true;
            this.buttonWZLNTPROFRAMCMD.Click += new System.EventHandler(this.buttonWZLNTPROFRAMCMD_Click);
            // 
            // buttonEZLNTDISABLEPERMIT
            // 
            this.buttonEZLNTDISABLEPERMIT.Location = new System.Drawing.Point(1357, 84);
            this.buttonEZLNTDISABLEPERMIT.Name = "buttonEZLNTDISABLEPERMIT";
            this.buttonEZLNTDISABLEPERMIT.Size = new System.Drawing.Size(122, 23);
            this.buttonEZLNTDISABLEPERMIT.TabIndex = 217;
            this.buttonEZLNTDISABLEPERMIT.Text = "Disable  Join";
            this.buttonEZLNTDISABLEPERMIT.UseVisualStyleBackColor = true;
            this.buttonEZLNTDISABLEPERMIT.Click += new System.EventHandler(this.buttonEZLNTDISABLEPERMIT_Click);
            // 
            // buttonEZLNTPERMIT
            // 
            this.buttonEZLNTPERMIT.Location = new System.Drawing.Point(1277, 84);
            this.buttonEZLNTPERMIT.Name = "buttonEZLNTPERMIT";
            this.buttonEZLNTPERMIT.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTPERMIT.TabIndex = 216;
            this.buttonEZLNTPERMIT.Text = "Permit Join";
            this.buttonEZLNTPERMIT.UseVisualStyleBackColor = true;
            this.buttonEZLNTPERMIT.Click += new System.EventHandler(this.buttonEZLNTPERMIT_Click);
            // 
            // checkBoxEZLNTGROUPALL
            // 
            this.checkBoxEZLNTGROUPALL.AutoSize = true;
            this.checkBoxEZLNTGROUPALL.Location = new System.Drawing.Point(835, 29);
            this.checkBoxEZLNTGROUPALL.Name = "checkBoxEZLNTGROUPALL";
            this.checkBoxEZLNTGROUPALL.Size = new System.Drawing.Size(45, 17);
            this.checkBoxEZLNTGROUPALL.TabIndex = 215;
            this.checkBoxEZLNTGROUPALL.Text = "ALL";
            this.checkBoxEZLNTGROUPALL.UseVisualStyleBackColor = true;
            this.checkBoxEZLNTGROUPALL.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBoxEZLNTSETINTERVALMAX
            // 
            this.textBoxEZLNTSETINTERVALMAX.Location = new System.Drawing.Point(1425, 50);
            this.textBoxEZLNTSETINTERVALMAX.Name = "textBoxEZLNTSETINTERVALMAX";
            this.textBoxEZLNTSETINTERVALMAX.Size = new System.Drawing.Size(71, 20);
            this.textBoxEZLNTSETINTERVALMAX.TabIndex = 141;
            this.textBoxEZLNTSETINTERVALMAX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTSETINTERVALMAX_MouseClick);
            this.textBoxEZLNTSETINTERVALMAX.Leave += new System.EventHandler(this.textBoxEZLNTSETINTERVALMAX_Leave);
            this.textBoxEZLNTSETINTERVALMAX.MouseLeave += new System.EventHandler(this.textBoxEZLNTSETINTERVALMAX_MouseLeave);
            this.textBoxEZLNTSETINTERVALMAX.MouseHover += new System.EventHandler(this.textBoxEZLNTSETINTERVALMAX_MouseHover);
            // 
            // buttonTEMPLOOPSTOP
            // 
            this.buttonTEMPLOOPSTOP.Location = new System.Drawing.Point(1492, 529);
            this.buttonTEMPLOOPSTOP.Name = "buttonTEMPLOOPSTOP";
            this.buttonTEMPLOOPSTOP.Size = new System.Drawing.Size(75, 23);
            this.buttonTEMPLOOPSTOP.TabIndex = 140;
            this.buttonTEMPLOOPSTOP.Text = "Stop Loop";
            this.buttonTEMPLOOPSTOP.UseVisualStyleBackColor = true;
            this.buttonTEMPLOOPSTOP.Click += new System.EventHandler(this.buttonTEMPLOOPSTOP_Click);
            // 
            // buttonEZLNTSATLOOPSTOP
            // 
            this.buttonEZLNTSATLOOPSTOP.Location = new System.Drawing.Point(1492, 498);
            this.buttonEZLNTSATLOOPSTOP.Name = "buttonEZLNTSATLOOPSTOP";
            this.buttonEZLNTSATLOOPSTOP.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTSATLOOPSTOP.TabIndex = 139;
            this.buttonEZLNTSATLOOPSTOP.Text = "Stop Loop";
            this.buttonEZLNTSATLOOPSTOP.UseVisualStyleBackColor = true;
            this.buttonEZLNTSATLOOPSTOP.Click += new System.EventHandler(this.buttonEZLNTSATLOOPSTOP_Click);
            // 
            // buttonEZLNTCOLORLOOPSTOP
            // 
            this.buttonEZLNTCOLORLOOPSTOP.Location = new System.Drawing.Point(1601, 473);
            this.buttonEZLNTCOLORLOOPSTOP.Name = "buttonEZLNTCOLORLOOPSTOP";
            this.buttonEZLNTCOLORLOOPSTOP.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTCOLORLOOPSTOP.TabIndex = 138;
            this.buttonEZLNTCOLORLOOPSTOP.Text = "Stop Loop";
            this.buttonEZLNTCOLORLOOPSTOP.UseVisualStyleBackColor = true;
            this.buttonEZLNTCOLORLOOPSTOP.Click += new System.EventHandler(this.buttonEZLNTCOLORLOOPSTOP_Click);
            // 
            // buttonEZLNTHUESTOP
            // 
            this.buttonEZLNTHUESTOP.Location = new System.Drawing.Point(1601, 442);
            this.buttonEZLNTHUESTOP.Name = "buttonEZLNTHUESTOP";
            this.buttonEZLNTHUESTOP.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTHUESTOP.TabIndex = 137;
            this.buttonEZLNTHUESTOP.Text = "Stop Loop";
            this.buttonEZLNTHUESTOP.UseVisualStyleBackColor = true;
            this.buttonEZLNTHUESTOP.Click += new System.EventHandler(this.buttonEZLNTHUESTOP_Click);
            // 
            // textBoxEZLNTSETDIR
            // 
            this.textBoxEZLNTSETDIR.Location = new System.Drawing.Point(1565, 50);
            this.textBoxEZLNTSETDIR.Name = "textBoxEZLNTSETDIR";
            this.textBoxEZLNTSETDIR.Size = new System.Drawing.Size(58, 20);
            this.textBoxEZLNTSETDIR.TabIndex = 136;
            this.textBoxEZLNTSETDIR.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTSETDIR_MouseClick);
            this.textBoxEZLNTSETDIR.Leave += new System.EventHandler(this.textBoxEZLNTSETDIR_Leave);
            this.textBoxEZLNTSETDIR.MouseLeave += new System.EventHandler(this.textBoxEZLNTSETDIR_MouseLeave);
            this.textBoxEZLNTSETDIR.MouseHover += new System.EventHandler(this.textBoxEZLNTSETDIR_MouseHover);
            // 
            // textBoxEZLNTSETSTEP
            // 
            this.textBoxEZLNTSETSTEP.Location = new System.Drawing.Point(1501, 50);
            this.textBoxEZLNTSETSTEP.Name = "textBoxEZLNTSETSTEP";
            this.textBoxEZLNTSETSTEP.Size = new System.Drawing.Size(57, 20);
            this.textBoxEZLNTSETSTEP.TabIndex = 135;
            this.textBoxEZLNTSETSTEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTSETSTEP_MouseClick);
            this.textBoxEZLNTSETSTEP.Leave += new System.EventHandler(this.textBoxEZLNTSETSTEP_Leave);
            this.textBoxEZLNTSETSTEP.MouseLeave += new System.EventHandler(this.textBoxEZLNTSETSTEP_MouseLeave);
            this.textBoxEZLNTSETSTEP.MouseHover += new System.EventHandler(this.textBoxEZLNTSETSTEP_MouseHover);
            // 
            // comboBoxEZLNTLEVELWITHONOFF
            // 
            this.comboBoxEZLNTLEVELWITHONOFF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEZLNTLEVELWITHONOFF.FormattingEnabled = true;
            this.comboBoxEZLNTLEVELWITHONOFF.Items.AddRange(new object[] {
            "WITHOUT ON/OFF",
            "WITH ON/OFF"});
            this.comboBoxEZLNTLEVELWITHONOFF.Location = new System.Drawing.Point(1588, 414);
            this.comboBoxEZLNTLEVELWITHONOFF.Name = "comboBoxEZLNTLEVELWITHONOFF";
            this.comboBoxEZLNTLEVELWITHONOFF.Size = new System.Drawing.Size(106, 21);
            this.comboBoxEZLNTLEVELWITHONOFF.TabIndex = 134;
            // 
            // buttonEZLNTLEVLELSTOP
            // 
            this.buttonEZLNTLEVLELSTOP.Location = new System.Drawing.Point(1700, 413);
            this.buttonEZLNTLEVLELSTOP.Name = "buttonEZLNTLEVLELSTOP";
            this.buttonEZLNTLEVLELSTOP.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTLEVLELSTOP.TabIndex = 133;
            this.buttonEZLNTLEVLELSTOP.Text = "Stop Loop";
            this.buttonEZLNTLEVLELSTOP.UseVisualStyleBackColor = true;
            this.buttonEZLNTLEVLELSTOP.Click += new System.EventHandler(this.buttonEZLNTLEVLELSTOP_Click);
            // 
            // buttonEZLNTIDENTIFYSTOP
            // 
            this.buttonEZLNTIDENTIFYSTOP.Location = new System.Drawing.Point(1381, 359);
            this.buttonEZLNTIDENTIFYSTOP.Name = "buttonEZLNTIDENTIFYSTOP";
            this.buttonEZLNTIDENTIFYSTOP.Size = new System.Drawing.Size(99, 23);
            this.buttonEZLNTIDENTIFYSTOP.TabIndex = 132;
            this.buttonEZLNTIDENTIFYSTOP.Text = "Stop Identify Loop";
            this.buttonEZLNTIDENTIFYSTOP.UseVisualStyleBackColor = true;
            this.buttonEZLNTIDENTIFYSTOP.Click += new System.EventHandler(this.buttonEZLNTIDENTIFYSTOP_Click);
            // 
            // textBoxEZLNTTEMPTIME
            // 
            this.textBoxEZLNTTEMPTIME.Location = new System.Drawing.Point(1381, 531);
            this.textBoxEZLNTTEMPTIME.Name = "textBoxEZLNTTEMPTIME";
            this.textBoxEZLNTTEMPTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTTEMPTIME.TabIndex = 131;
            this.textBoxEZLNTTEMPTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTTEMPTIME_MouseClick);
            this.textBoxEZLNTTEMPTIME.Leave += new System.EventHandler(this.textBoxEZLNTTEMPTIME_Leave);
            this.textBoxEZLNTTEMPTIME.MouseLeave += new System.EventHandler(this.textBoxEZLNTTEMPTIME_MouseLeave);
            this.textBoxEZLNTTEMPTIME.MouseHover += new System.EventHandler(this.textBoxEZLNTTEMPTIME_MouseHover);
            // 
            // textBoxEZLNTTEMP
            // 
            this.textBoxEZLNTTEMP.Location = new System.Drawing.Point(1276, 531);
            this.textBoxEZLNTTEMP.Name = "textBoxEZLNTTEMP";
            this.textBoxEZLNTTEMP.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTTEMP.TabIndex = 130;
            this.textBoxEZLNTTEMP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTTEMP_MouseClick);
            this.textBoxEZLNTTEMP.Leave += new System.EventHandler(this.textBoxEZLNTTEMP_Leave);
            this.textBoxEZLNTTEMP.MouseLeave += new System.EventHandler(this.textBoxEZLNTTEMP_MouseLeave);
            this.textBoxEZLNTTEMP.MouseHover += new System.EventHandler(this.textBoxEZLNTTEMP_MouseHover);
            // 
            // textBoxEZLNTSATTIME
            // 
            this.textBoxEZLNTSATTIME.Location = new System.Drawing.Point(1381, 501);
            this.textBoxEZLNTSATTIME.Name = "textBoxEZLNTSATTIME";
            this.textBoxEZLNTSATTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTSATTIME.TabIndex = 129;
            this.textBoxEZLNTSATTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTSATTIME_MouseClick);
            this.textBoxEZLNTSATTIME.Leave += new System.EventHandler(this.textBoxEZLNTSATTIME_Leave);
            this.textBoxEZLNTSATTIME.MouseLeave += new System.EventHandler(this.textBoxEZLNTSATTIME_MouseLeave);
            this.textBoxEZLNTSATTIME.MouseHover += new System.EventHandler(this.textBoxEZLNTSATTIME_MouseHover);
            // 
            // textBoxEZLNTSAT
            // 
            this.textBoxEZLNTSAT.Location = new System.Drawing.Point(1276, 501);
            this.textBoxEZLNTSAT.Name = "textBoxEZLNTSAT";
            this.textBoxEZLNTSAT.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTSAT.TabIndex = 128;
            this.textBoxEZLNTSAT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTSAT_MouseClick);
            this.textBoxEZLNTSAT.Leave += new System.EventHandler(this.textBoxEZLNTSAT_Leave);
            this.textBoxEZLNTSAT.MouseLeave += new System.EventHandler(this.textBoxEZLNTSAT_MouseLeave);
            this.textBoxEZLNTSAT.MouseHover += new System.EventHandler(this.textBoxEZLNTSAT_MouseHover);
            // 
            // textBoxCOLORTIME
            // 
            this.textBoxCOLORTIME.Location = new System.Drawing.Point(1492, 473);
            this.textBoxCOLORTIME.Name = "textBoxCOLORTIME";
            this.textBoxCOLORTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxCOLORTIME.TabIndex = 127;
            this.textBoxCOLORTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxCOLORTIME_MouseClick);
            this.textBoxCOLORTIME.Leave += new System.EventHandler(this.textBoxCOLORTIME_Leave);
            this.textBoxCOLORTIME.MouseLeave += new System.EventHandler(this.textBoxCOLORTIME_MouseLeave);
            this.textBoxCOLORTIME.MouseHover += new System.EventHandler(this.textBoxCOLORTIME_MouseHover);
            // 
            // textBoxEZLNTCOLORY
            // 
            this.textBoxEZLNTCOLORY.Location = new System.Drawing.Point(1385, 471);
            this.textBoxEZLNTCOLORY.Name = "textBoxEZLNTCOLORY";
            this.textBoxEZLNTCOLORY.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTCOLORY.TabIndex = 126;
            this.textBoxEZLNTCOLORY.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTCOLORY_MouseClick);
            this.textBoxEZLNTCOLORY.Leave += new System.EventHandler(this.textBoxEZLNTCOLORY_Leave);
            this.textBoxEZLNTCOLORY.MouseLeave += new System.EventHandler(this.textBoxEZLNTCOLORY_MouseLeave);
            this.textBoxEZLNTCOLORY.MouseHover += new System.EventHandler(this.textBoxEZLNTCOLORY_MouseHover);
            // 
            // textBoxEZLNTCOLORX
            // 
            this.textBoxEZLNTCOLORX.Location = new System.Drawing.Point(1277, 473);
            this.textBoxEZLNTCOLORX.Name = "textBoxEZLNTCOLORX";
            this.textBoxEZLNTCOLORX.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTCOLORX.TabIndex = 125;
            this.textBoxEZLNTCOLORX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTCOLORX_MouseClick);
            this.textBoxEZLNTCOLORX.Leave += new System.EventHandler(this.textBoxEZLNTCOLORX_Leave);
            this.textBoxEZLNTCOLORX.MouseLeave += new System.EventHandler(this.textBoxEZLNTCOLORX_MouseLeave);
            this.textBoxEZLNTCOLORX.MouseHover += new System.EventHandler(this.textBoxEZLNTCOLORX_MouseHover);
            // 
            // textBoxEZLNTHUETIME
            // 
            this.textBoxEZLNTHUETIME.Location = new System.Drawing.Point(1492, 445);
            this.textBoxEZLNTHUETIME.Name = "textBoxEZLNTHUETIME";
            this.textBoxEZLNTHUETIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTHUETIME.TabIndex = 124;
            this.textBoxEZLNTHUETIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTHUETIME_MouseClick);
            this.textBoxEZLNTHUETIME.Leave += new System.EventHandler(this.textBoxEZLNTHUETIME_Leave);
            this.textBoxEZLNTHUETIME.MouseLeave += new System.EventHandler(this.textBoxEZLNTHUETIME_MouseLeave);
            this.textBoxEZLNTHUETIME.MouseHover += new System.EventHandler(this.textBoxEZLNTHUETIME_MouseHover);
            // 
            // textBoxEZLNTHUEDIR
            // 
            this.textBoxEZLNTHUEDIR.Location = new System.Drawing.Point(1385, 445);
            this.textBoxEZLNTHUEDIR.Name = "textBoxEZLNTHUEDIR";
            this.textBoxEZLNTHUEDIR.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTHUEDIR.TabIndex = 123;
            this.textBoxEZLNTHUEDIR.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTHUEDIR_MouseClick);
            this.textBoxEZLNTHUEDIR.Leave += new System.EventHandler(this.textBoxEZLNTHUEDIR_Leave);
            this.textBoxEZLNTHUEDIR.MouseLeave += new System.EventHandler(this.textBoxEZLNTHUEDIR_MouseLeave);
            this.textBoxEZLNTHUEDIR.MouseHover += new System.EventHandler(this.textBoxEZLNTHUEDIR_MouseHover);
            // 
            // textBoxEZLNTHUE
            // 
            this.textBoxEZLNTHUE.Location = new System.Drawing.Point(1275, 445);
            this.textBoxEZLNTHUE.Name = "textBoxEZLNTHUE";
            this.textBoxEZLNTHUE.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTHUE.TabIndex = 122;
            this.textBoxEZLNTHUE.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTHUE_MouseClick);
            this.textBoxEZLNTHUE.Leave += new System.EventHandler(this.textBoxEZLNTHUE_Leave);
            this.textBoxEZLNTHUE.MouseLeave += new System.EventHandler(this.textBoxEZLNTHUE_MouseLeave);
            this.textBoxEZLNTHUE.MouseHover += new System.EventHandler(this.textBoxEZLNTHUE_MouseHover);
            // 
            // textBoxEZLNTLEVELTIME
            // 
            this.textBoxEZLNTLEVELTIME.Location = new System.Drawing.Point(1481, 415);
            this.textBoxEZLNTLEVELTIME.Name = "textBoxEZLNTLEVELTIME";
            this.textBoxEZLNTLEVELTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTLEVELTIME.TabIndex = 120;
            this.textBoxEZLNTLEVELTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTLEVELTIME_MouseClick);
            this.textBoxEZLNTLEVELTIME.Leave += new System.EventHandler(this.textBoxEZLNTLEVELTIME_Leave);
            this.textBoxEZLNTLEVELTIME.MouseLeave += new System.EventHandler(this.textBoxEZLNTLEVELTIME_MouseLeave);
            this.textBoxEZLNTLEVELTIME.MouseHover += new System.EventHandler(this.textBoxEZLNTLEVELTIME_MouseHover);
            // 
            // textBoxEZLNTLEVEL
            // 
            this.textBoxEZLNTLEVEL.Location = new System.Drawing.Point(1376, 416);
            this.textBoxEZLNTLEVEL.Name = "textBoxEZLNTLEVEL";
            this.textBoxEZLNTLEVEL.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTLEVEL.TabIndex = 119;
            this.textBoxEZLNTLEVEL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTLEVEL_MouseClick);
            this.textBoxEZLNTLEVEL.Leave += new System.EventHandler(this.textBoxEZLNTLEVEL_Leave);
            this.textBoxEZLNTLEVEL.MouseLeave += new System.EventHandler(this.textBoxEZLNTLEVEL_MouseLeave);
            this.textBoxEZLNTLEVEL.MouseHover += new System.EventHandler(this.textBoxEZLNTLEVEL_MouseHover);
            // 
            // textBoxEZLNTIDENTIFYTIME
            // 
            this.textBoxEZLNTIDENTIFYTIME.Location = new System.Drawing.Point(1276, 359);
            this.textBoxEZLNTIDENTIFYTIME.Name = "textBoxEZLNTIDENTIFYTIME";
            this.textBoxEZLNTIDENTIFYTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTIDENTIFYTIME.TabIndex = 118;
            this.textBoxEZLNTIDENTIFYTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTIDENTIFYTIME_MouseClick);
            this.textBoxEZLNTIDENTIFYTIME.Leave += new System.EventHandler(this.textBoxEZLNTIDENTIFYTIME_Leave);
            this.textBoxEZLNTIDENTIFYTIME.MouseLeave += new System.EventHandler(this.textBoxEZLNTIDENTIFYTIME_MouseLeave);
            this.textBoxEZLNTIDENTIFYTIME.MouseHover += new System.EventHandler(this.textBoxEZLNTIDENTIFYTIME_MouseHover);
            // 
            // textBoxEZLNTREADRPRTATTRIBUTEID
            // 
            this.textBoxEZLNTREADRPRTATTRIBUTEID.Location = new System.Drawing.Point(1340, 329);
            this.textBoxEZLNTREADRPRTATTRIBUTEID.Name = "textBoxEZLNTREADRPRTATTRIBUTEID";
            this.textBoxEZLNTREADRPRTATTRIBUTEID.Size = new System.Drawing.Size(63, 20);
            this.textBoxEZLNTREADRPRTATTRIBUTEID.TabIndex = 117;
            this.textBoxEZLNTREADRPRTATTRIBUTEID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTREADRPRTATTRIBUTEID_MouseClick);
            this.textBoxEZLNTREADRPRTATTRIBUTEID.Leave += new System.EventHandler(this.textBoxEZLNTREADRPRTATTRIBUTEID_Leave);
            this.textBoxEZLNTREADRPRTATTRIBUTEID.MouseLeave += new System.EventHandler(this.textBoxEZLNTREADRPRTATTRIBUTEID_MouseLeave);
            this.textBoxEZLNTREADRPRTATTRIBUTEID.MouseHover += new System.EventHandler(this.textBoxEZLNTREADRPRTATTRIBUTEID_MouseHover);
            // 
            // textBoxEZLNTREADRPRTCLUSTERID
            // 
            this.textBoxEZLNTREADRPRTCLUSTERID.Location = new System.Drawing.Point(1264, 329);
            this.textBoxEZLNTREADRPRTCLUSTERID.Name = "textBoxEZLNTREADRPRTCLUSTERID";
            this.textBoxEZLNTREADRPRTCLUSTERID.Size = new System.Drawing.Size(70, 20);
            this.textBoxEZLNTREADRPRTCLUSTERID.TabIndex = 116;
            this.textBoxEZLNTREADRPRTCLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTREADRPRTCLUSTERID_MouseClick);
            this.textBoxEZLNTREADRPRTCLUSTERID.Leave += new System.EventHandler(this.textBoxEZLNTREADRPRTCLUSTERID_Leave);
            this.textBoxEZLNTREADRPRTCLUSTERID.MouseLeave += new System.EventHandler(this.textBoxEZLNTREADRPRTCLUSTERID_MouseLeave);
            this.textBoxEZLNTREADRPRTCLUSTERID.MouseHover += new System.EventHandler(this.textBoxEZLNTREADRPRTCLUSTERID_MouseHover);
            // 
            // textBoxEZLNTCONFIGRPRTCHANGE
            // 
            this.textBoxEZLNTCONFIGRPRTCHANGE.Location = new System.Drawing.Point(1673, 299);
            this.textBoxEZLNTCONFIGRPRTCHANGE.Name = "textBoxEZLNTCONFIGRPRTCHANGE";
            this.textBoxEZLNTCONFIGRPRTCHANGE.Size = new System.Drawing.Size(58, 20);
            this.textBoxEZLNTCONFIGRPRTCHANGE.TabIndex = 115;
            this.textBoxEZLNTCONFIGRPRTCHANGE.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTCONFIGRPRTCHANGE_MouseClick);
            this.textBoxEZLNTCONFIGRPRTCHANGE.Leave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTCHANGE_Leave);
            this.textBoxEZLNTCONFIGRPRTCHANGE.MouseLeave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTCHANGE_MouseLeave);
            this.textBoxEZLNTCONFIGRPRTCHANGE.MouseHover += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTCHANGE_MouseHover);
            // 
            // textBoxEZLNTCONFIGRPRTTIMEOUT
            // 
            this.textBoxEZLNTCONFIGRPRTTIMEOUT.Location = new System.Drawing.Point(1599, 298);
            this.textBoxEZLNTCONFIGRPRTTIMEOUT.Name = "textBoxEZLNTCONFIGRPRTTIMEOUT";
            this.textBoxEZLNTCONFIGRPRTTIMEOUT.Size = new System.Drawing.Size(69, 20);
            this.textBoxEZLNTCONFIGRPRTTIMEOUT.TabIndex = 114;
            this.textBoxEZLNTCONFIGRPRTTIMEOUT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTCONFIGRPRTTIMEOUT_MouseClick);
            this.textBoxEZLNTCONFIGRPRTTIMEOUT.Leave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTTIMEOUT_Leave);
            this.textBoxEZLNTCONFIGRPRTTIMEOUT.MouseLeave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTTIMEOUT_MouseLeave);
            this.textBoxEZLNTCONFIGRPRTTIMEOUT.MouseHover += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTTIMEOUT_MouseHover);
            // 
            // textBoxEZLNTCONFIGRPRTMAXINTERVAL
            // 
            this.textBoxEZLNTCONFIGRPRTMAXINTERVAL.Location = new System.Drawing.Point(1527, 298);
            this.textBoxEZLNTCONFIGRPRTMAXINTERVAL.Name = "textBoxEZLNTCONFIGRPRTMAXINTERVAL";
            this.textBoxEZLNTCONFIGRPRTMAXINTERVAL.Size = new System.Drawing.Size(66, 20);
            this.textBoxEZLNTCONFIGRPRTMAXINTERVAL.TabIndex = 113;
            this.textBoxEZLNTCONFIGRPRTMAXINTERVAL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTCONFIGRPRTMAXINTERVAL_MouseClick);
            this.textBoxEZLNTCONFIGRPRTMAXINTERVAL.Leave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTMAXINTERVAL_Leave);
            this.textBoxEZLNTCONFIGRPRTMAXINTERVAL.MouseLeave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTMAXINTERVAL_MouseLeave);
            this.textBoxEZLNTCONFIGRPRTMAXINTERVAL.MouseHover += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTMAXINTERVAL_MouseHover);
            // 
            // textBoxEZLNTCONFIGRPRTMININTERVAL
            // 
            this.textBoxEZLNTCONFIGRPRTMININTERVAL.Location = new System.Drawing.Point(1461, 298);
            this.textBoxEZLNTCONFIGRPRTMININTERVAL.Name = "textBoxEZLNTCONFIGRPRTMININTERVAL";
            this.textBoxEZLNTCONFIGRPRTMININTERVAL.Size = new System.Drawing.Size(58, 20);
            this.textBoxEZLNTCONFIGRPRTMININTERVAL.TabIndex = 112;
            this.textBoxEZLNTCONFIGRPRTMININTERVAL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTCONFIGRPRTMININTERVAL_MouseClick);
            this.textBoxEZLNTCONFIGRPRTMININTERVAL.Leave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTMININTERVAL_Leave);
            this.textBoxEZLNTCONFIGRPRTMININTERVAL.MouseLeave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTMININTERVAL_MouseLeave);
            this.textBoxEZLNTCONFIGRPRTMININTERVAL.MouseHover += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTMININTERVAL_MouseHover);
            // 
            // textBoxEZLNTCONFIGRPRTATTRIBID
            // 
            this.textBoxEZLNTCONFIGRPRTATTRIBID.Location = new System.Drawing.Point(1397, 299);
            this.textBoxEZLNTCONFIGRPRTATTRIBID.Name = "textBoxEZLNTCONFIGRPRTATTRIBID";
            this.textBoxEZLNTCONFIGRPRTATTRIBID.Size = new System.Drawing.Size(57, 20);
            this.textBoxEZLNTCONFIGRPRTATTRIBID.TabIndex = 111;
            this.textBoxEZLNTCONFIGRPRTATTRIBID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTCONFIGRPRTATTRIBID_MouseClick);
            this.textBoxEZLNTCONFIGRPRTATTRIBID.Leave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTATTRIBID_Leave);
            this.textBoxEZLNTCONFIGRPRTATTRIBID.MouseLeave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTATTRIBID_MouseLeave);
            this.textBoxEZLNTCONFIGRPRTATTRIBID.MouseHover += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTATTRIBID_MouseHover);
            // 
            // textBoxEZLNTCONFIGRPRTTYPE
            // 
            this.textBoxEZLNTCONFIGRPRTTYPE.Location = new System.Drawing.Point(1339, 299);
            this.textBoxEZLNTCONFIGRPRTTYPE.Name = "textBoxEZLNTCONFIGRPRTTYPE";
            this.textBoxEZLNTCONFIGRPRTTYPE.Size = new System.Drawing.Size(53, 20);
            this.textBoxEZLNTCONFIGRPRTTYPE.TabIndex = 110;
            this.textBoxEZLNTCONFIGRPRTTYPE.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTCONFIGRPRTTYPE_MouseClick);
            this.textBoxEZLNTCONFIGRPRTTYPE.Leave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTTYPE_Leave);
            this.textBoxEZLNTCONFIGRPRTTYPE.MouseLeave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTTYPE_MouseLeave);
            this.textBoxEZLNTCONFIGRPRTTYPE.MouseHover += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTTYPE_MouseHover);
            // 
            // textBoxEZLNTCONFIGRPRTCLUSTERID
            // 
            this.textBoxEZLNTCONFIGRPRTCLUSTERID.Location = new System.Drawing.Point(1265, 299);
            this.textBoxEZLNTCONFIGRPRTCLUSTERID.Name = "textBoxEZLNTCONFIGRPRTCLUSTERID";
            this.textBoxEZLNTCONFIGRPRTCLUSTERID.Size = new System.Drawing.Size(67, 20);
            this.textBoxEZLNTCONFIGRPRTCLUSTERID.TabIndex = 109;
            this.textBoxEZLNTCONFIGRPRTCLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTCONFIGRPRTCLUSTERID_MouseClick);
            this.textBoxEZLNTCONFIGRPRTCLUSTERID.Leave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTCLUSTERID_Leave);
            this.textBoxEZLNTCONFIGRPRTCLUSTERID.MouseLeave += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTCLUSTERID_MouseLeave);
            this.textBoxEZLNTCONFIGRPRTCLUSTERID.MouseHover += new System.EventHandler(this.textBoxEZLNTCONFIGRPRTCLUSTERID_MouseHover);
            // 
            // comboBoxEZLNTLEAVECHILDREN
            // 
            this.comboBoxEZLNTLEAVECHILDREN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEZLNTLEAVECHILDREN.FormattingEnabled = true;
            this.comboBoxEZLNTLEAVECHILDREN.Items.AddRange(new object[] {
            "DO NOT REMOVE CHILDREN",
            "REMOVE CHILDREN"});
            this.comboBoxEZLNTLEAVECHILDREN.Location = new System.Drawing.Point(1385, 269);
            this.comboBoxEZLNTLEAVECHILDREN.Name = "comboBoxEZLNTLEAVECHILDREN";
            this.comboBoxEZLNTLEAVECHILDREN.Size = new System.Drawing.Size(159, 21);
            this.comboBoxEZLNTLEAVECHILDREN.TabIndex = 108;
            // 
            // comboBoxEZLNTLEAVEREJOIN
            // 
            this.comboBoxEZLNTLEAVEREJOIN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEZLNTLEAVEREJOIN.FormattingEnabled = true;
            this.comboBoxEZLNTLEAVEREJOIN.Items.AddRange(new object[] {
            "DO NOT REJOIN",
            "REJOIN"});
            this.comboBoxEZLNTLEAVEREJOIN.Location = new System.Drawing.Point(1259, 269);
            this.comboBoxEZLNTLEAVEREJOIN.Name = "comboBoxEZLNTLEAVEREJOIN";
            this.comboBoxEZLNTLEAVEREJOIN.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEZLNTLEAVEREJOIN.TabIndex = 107;
            // 
            // textBoxEZLNTWRITEATTRIBUTEDATA
            // 
            this.textBoxEZLNTWRITEATTRIBUTEDATA.Location = new System.Drawing.Point(1488, 147);
            this.textBoxEZLNTWRITEATTRIBUTEDATA.Name = "textBoxEZLNTWRITEATTRIBUTEDATA";
            this.textBoxEZLNTWRITEATTRIBUTEDATA.Size = new System.Drawing.Size(137, 20);
            this.textBoxEZLNTWRITEATTRIBUTEDATA.TabIndex = 105;
            this.textBoxEZLNTWRITEATTRIBUTEDATA.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTWRITEATTRIBUTEDATA_MouseClick);
            this.textBoxEZLNTWRITEATTRIBUTEDATA.Leave += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTEDATA_Leave);
            this.textBoxEZLNTWRITEATTRIBUTEDATA.MouseLeave += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTEDATA_MouseLeave);
            this.textBoxEZLNTWRITEATTRIBUTEDATA.MouseHover += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTEDATA_MouseHover);
            // 
            // textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE
            // 
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.Location = new System.Drawing.Point(1420, 146);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.Name = "textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE";
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.Size = new System.Drawing.Size(62, 20);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.TabIndex = 104;
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE_MouseClick);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.Leave += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE_Leave);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.MouseLeave += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE_MouseLeave);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE.MouseHover += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE_MouseHover);
            // 
            // textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID
            // 
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.Location = new System.Drawing.Point(1345, 146);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.Name = "textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID";
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.Size = new System.Drawing.Size(69, 20);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.TabIndex = 103;
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID_MouseClick);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.Leave += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID_Leave);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.MouseLeave += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID_MouseLeave);
            this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID.MouseHover += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID_MouseHover);
            // 
            // textBoxEZLNTWRITEATTRIBUTECLUSTERID
            // 
            this.textBoxEZLNTWRITEATTRIBUTECLUSTERID.Location = new System.Drawing.Point(1276, 146);
            this.textBoxEZLNTWRITEATTRIBUTECLUSTERID.Name = "textBoxEZLNTWRITEATTRIBUTECLUSTERID";
            this.textBoxEZLNTWRITEATTRIBUTECLUSTERID.Size = new System.Drawing.Size(63, 20);
            this.textBoxEZLNTWRITEATTRIBUTECLUSTERID.TabIndex = 102;
            this.textBoxEZLNTWRITEATTRIBUTECLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTWRITEATTRIBUTECLUSTERID_MouseClick);
            this.textBoxEZLNTWRITEATTRIBUTECLUSTERID.Leave += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTECLUSTERID_Leave);
            this.textBoxEZLNTWRITEATTRIBUTECLUSTERID.MouseLeave += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTECLUSTERID_MouseLeave);
            this.textBoxEZLNTWRITEATTRIBUTECLUSTERID.MouseHover += new System.EventHandler(this.textBoxEZLNTWRITEATTRIBUTECLUSTERID_MouseHover);
            // 
            // textBoxEZLNTATTRIBUTEID
            // 
            this.textBoxEZLNTATTRIBUTEID.Location = new System.Drawing.Point(1345, 116);
            this.textBoxEZLNTATTRIBUTEID.Name = "textBoxEZLNTATTRIBUTEID";
            this.textBoxEZLNTATTRIBUTEID.Size = new System.Drawing.Size(69, 20);
            this.textBoxEZLNTATTRIBUTEID.TabIndex = 100;
            this.textBoxEZLNTATTRIBUTEID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTATTRIBUTEID_MouseClick);
            this.textBoxEZLNTATTRIBUTEID.Leave += new System.EventHandler(this.textBoxEZLNTATTRIBUTEID_Leave);
            this.textBoxEZLNTATTRIBUTEID.MouseLeave += new System.EventHandler(this.textBoxEZLNTATTRIBUTEID_MouseLeave);
            this.textBoxEZLNTATTRIBUTEID.MouseHover += new System.EventHandler(this.textBoxEZLNTATTRIBUTEID_MouseHover);
            // 
            // textBoxEZLNTREADCLUSTERID
            // 
            this.textBoxEZLNTREADCLUSTERID.Location = new System.Drawing.Point(1276, 116);
            this.textBoxEZLNTREADCLUSTERID.Name = "textBoxEZLNTREADCLUSTERID";
            this.textBoxEZLNTREADCLUSTERID.Size = new System.Drawing.Size(63, 20);
            this.textBoxEZLNTREADCLUSTERID.TabIndex = 99;
            this.textBoxEZLNTREADCLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTREADCLUSTERID_MouseClick);
            this.textBoxEZLNTREADCLUSTERID.Leave += new System.EventHandler(this.textBoxEZLNTREADCLUSTERID_Leave);
            this.textBoxEZLNTREADCLUSTERID.MouseLeave += new System.EventHandler(this.textBoxEZLNTREADCLUSTERID_MouseLeave);
            this.textBoxEZLNTREADCLUSTERID.MouseHover += new System.EventHandler(this.textBoxEZLNTREADCLUSTERID_MouseHover);
            // 
            // buttonEZLNTMOVETOTEMP
            // 
            this.buttonEZLNTMOVETOTEMP.Location = new System.Drawing.Point(1176, 527);
            this.buttonEZLNTMOVETOTEMP.Name = "buttonEZLNTMOVETOTEMP";
            this.buttonEZLNTMOVETOTEMP.Size = new System.Drawing.Size(90, 22);
            this.buttonEZLNTMOVETOTEMP.TabIndex = 98;
            this.buttonEZLNTMOVETOTEMP.Text = "MoveToTemp";
            this.buttonEZLNTMOVETOTEMP.UseVisualStyleBackColor = true;
            this.buttonEZLNTMOVETOTEMP.Click += new System.EventHandler(this.buttonEZLNTMOVETOTEMP_Click);
            // 
            // buttonEZLNTMOVETOSAT
            // 
            this.buttonEZLNTMOVETOSAT.Location = new System.Drawing.Point(1176, 499);
            this.buttonEZLNTMOVETOSAT.Name = "buttonEZLNTMOVETOSAT";
            this.buttonEZLNTMOVETOSAT.Size = new System.Drawing.Size(90, 22);
            this.buttonEZLNTMOVETOSAT.TabIndex = 97;
            this.buttonEZLNTMOVETOSAT.Text = "MoveToSat";
            this.buttonEZLNTMOVETOSAT.UseVisualStyleBackColor = true;
            this.buttonEZLNTMOVETOSAT.Click += new System.EventHandler(this.buttonEZLNTMOVETOSAT_Click);
            // 
            // buttonEZLNTMOVETOCOLOR
            // 
            this.buttonEZLNTMOVETOCOLOR.Location = new System.Drawing.Point(1176, 471);
            this.buttonEZLNTMOVETOCOLOR.Name = "buttonEZLNTMOVETOCOLOR";
            this.buttonEZLNTMOVETOCOLOR.Size = new System.Drawing.Size(90, 22);
            this.buttonEZLNTMOVETOCOLOR.TabIndex = 96;
            this.buttonEZLNTMOVETOCOLOR.Text = "MoveToColor";
            this.buttonEZLNTMOVETOCOLOR.UseVisualStyleBackColor = true;
            this.buttonEZLNTMOVETOCOLOR.Click += new System.EventHandler(this.buttonEZLNTMOVETOCOLOR_Click);
            // 
            // buttonEZLNTMOVETOHUE
            // 
            this.buttonEZLNTMOVETOHUE.Location = new System.Drawing.Point(1177, 443);
            this.buttonEZLNTMOVETOHUE.Name = "buttonEZLNTMOVETOHUE";
            this.buttonEZLNTMOVETOHUE.Size = new System.Drawing.Size(90, 22);
            this.buttonEZLNTMOVETOHUE.TabIndex = 95;
            this.buttonEZLNTMOVETOHUE.Text = "MoveToHue";
            this.buttonEZLNTMOVETOHUE.UseVisualStyleBackColor = true;
            this.buttonEZLNTMOVETOHUE.Click += new System.EventHandler(this.buttonEZLNTMOVETOHUE_Click);
            // 
            // buttonEZLNTMOVETOLEVLEL
            // 
            this.buttonEZLNTMOVETOLEVLEL.Location = new System.Drawing.Point(1177, 416);
            this.buttonEZLNTMOVETOLEVLEL.Name = "buttonEZLNTMOVETOLEVLEL";
            this.buttonEZLNTMOVETOLEVLEL.Size = new System.Drawing.Size(93, 23);
            this.buttonEZLNTMOVETOLEVLEL.TabIndex = 94;
            this.buttonEZLNTMOVETOLEVLEL.Text = "Move to level";
            this.buttonEZLNTMOVETOLEVLEL.UseVisualStyleBackColor = true;
            this.buttonEZLNTMOVETOLEVLEL.Click += new System.EventHandler(this.buttonEZLNTMOVETOLEVLEL_Click);
            // 
            // buttonEZLNTIDENTIFY
            // 
            this.buttonEZLNTIDENTIFY.Location = new System.Drawing.Point(1177, 356);
            this.buttonEZLNTIDENTIFY.Name = "buttonEZLNTIDENTIFY";
            this.buttonEZLNTIDENTIFY.Size = new System.Drawing.Size(93, 23);
            this.buttonEZLNTIDENTIFY.TabIndex = 50;
            this.buttonEZLNTIDENTIFY.Text = "Identify Send";
            this.buttonEZLNTIDENTIFY.UseVisualStyleBackColor = true;
            this.buttonEZLNTIDENTIFY.Click += new System.EventHandler(this.buttonEZLNTIDENTIFY_Click);
            // 
            // buttonEZLNTRESET
            // 
            this.buttonEZLNTRESET.Location = new System.Drawing.Point(1177, 83);
            this.buttonEZLNTRESET.Name = "buttonEZLNTRESET";
            this.buttonEZLNTRESET.Size = new System.Drawing.Size(93, 25);
            this.buttonEZLNTRESET.TabIndex = 49;
            this.buttonEZLNTRESET.Text = "Reset To FD";
            this.buttonEZLNTRESET.UseVisualStyleBackColor = true;
            this.buttonEZLNTRESET.Click += new System.EventHandler(this.buttonEZLNTRESET_Click);
            // 
            // buttonEZLNTREADRPRT
            // 
            this.buttonEZLNTREADRPRT.Location = new System.Drawing.Point(1177, 326);
            this.buttonEZLNTREADRPRT.Name = "buttonEZLNTREADRPRT";
            this.buttonEZLNTREADRPRT.Size = new System.Drawing.Size(80, 24);
            this.buttonEZLNTREADRPRT.TabIndex = 48;
            this.buttonEZLNTREADRPRT.Text = "Read Rprt";
            this.buttonEZLNTREADRPRT.UseVisualStyleBackColor = true;
            this.buttonEZLNTREADRPRT.Click += new System.EventHandler(this.buttonEZLNTREADRPRT_Click);
            // 
            // buttonEZLNTCONFIGRPRT
            // 
            this.buttonEZLNTCONFIGRPRT.Location = new System.Drawing.Point(1177, 296);
            this.buttonEZLNTCONFIGRPRT.Name = "buttonEZLNTCONFIGRPRT";
            this.buttonEZLNTCONFIGRPRT.Size = new System.Drawing.Size(80, 24);
            this.buttonEZLNTCONFIGRPRT.TabIndex = 47;
            this.buttonEZLNTCONFIGRPRT.Text = "Config Rprt";
            this.buttonEZLNTCONFIGRPRT.UseVisualStyleBackColor = true;
            this.buttonEZLNTCONFIGRPRT.Click += new System.EventHandler(this.buttonEZLNTCONFIGRPRT_Click);
            // 
            // buttonEZLNTLEAVE
            // 
            this.buttonEZLNTLEAVE.Location = new System.Drawing.Point(1177, 267);
            this.buttonEZLNTLEAVE.Name = "buttonEZLNTLEAVE";
            this.buttonEZLNTLEAVE.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTLEAVE.TabIndex = 46;
            this.buttonEZLNTLEAVE.Text = "Leave";
            this.buttonEZLNTLEAVE.UseVisualStyleBackColor = true;
            this.buttonEZLNTLEAVE.Click += new System.EventHandler(this.buttonEZLNTLEAVE_Click);
            // 
            // buttonEZLNTWRITEATTRIBUTE
            // 
            this.buttonEZLNTWRITEATTRIBUTE.Location = new System.Drawing.Point(1177, 144);
            this.buttonEZLNTWRITEATTRIBUTE.Name = "buttonEZLNTWRITEATTRIBUTE";
            this.buttonEZLNTWRITEATTRIBUTE.Size = new System.Drawing.Size(92, 23);
            this.buttonEZLNTWRITEATTRIBUTE.TabIndex = 45;
            this.buttonEZLNTWRITEATTRIBUTE.Text = "Write Attribute";
            this.buttonEZLNTWRITEATTRIBUTE.UseVisualStyleBackColor = true;
            this.buttonEZLNTWRITEATTRIBUTE.Click += new System.EventHandler(this.buttonEZLNTWRITEATTRIBUTE_Click);
            // 
            // textBoxEZLNTCOMMAND
            // 
            this.textBoxEZLNTCOMMAND.Location = new System.Drawing.Point(613, 15);
            this.textBoxEZLNTCOMMAND.Name = "textBoxEZLNTCOMMAND";
            this.textBoxEZLNTCOMMAND.Size = new System.Drawing.Size(73, 20);
            this.textBoxEZLNTCOMMAND.TabIndex = 44;
            // 
            // buttonEZLNTSENDCOMMAND
            // 
            this.buttonEZLNTSENDCOMMAND.Location = new System.Drawing.Point(527, 13);
            this.buttonEZLNTSENDCOMMAND.Name = "buttonEZLNTSENDCOMMAND";
            this.buttonEZLNTSENDCOMMAND.Size = new System.Drawing.Size(80, 23);
            this.buttonEZLNTSENDCOMMAND.TabIndex = 43;
            this.buttonEZLNTSENDCOMMAND.Text = "Send Cmd";
            this.buttonEZLNTSENDCOMMAND.UseVisualStyleBackColor = true;
            this.buttonEZLNTSENDCOMMAND.Click += new System.EventHandler(this.buttonEZLNTSENDCOMMAND_Click);
            // 
            // textBoxEZLNTUNBINDCLUSTERID
            // 
            this.textBoxEZLNTUNBINDCLUSTERID.Location = new System.Drawing.Point(1445, 240);
            this.textBoxEZLNTUNBINDCLUSTERID.Name = "textBoxEZLNTUNBINDCLUSTERID";
            this.textBoxEZLNTUNBINDCLUSTERID.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTUNBINDCLUSTERID.TabIndex = 41;
            this.textBoxEZLNTUNBINDCLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTUNBINDCLUSTERID_MouseClick);
            this.textBoxEZLNTUNBINDCLUSTERID.Leave += new System.EventHandler(this.textBoxEZLNTUNBINDCLUSTERID_Leave);
            this.textBoxEZLNTUNBINDCLUSTERID.MouseLeave += new System.EventHandler(this.textBoxEZLNTUNBINDCLUSTERID_MouseLeave);
            this.textBoxEZLNTUNBINDCLUSTERID.MouseHover += new System.EventHandler(this.textBoxEZLNTUNBINDCLUSTERID_MouseHover);
            // 
            // textBoxEZLNTBINDCLUSTERID
            // 
            this.textBoxEZLNTBINDCLUSTERID.Location = new System.Drawing.Point(1257, 239);
            this.textBoxEZLNTBINDCLUSTERID.Name = "textBoxEZLNTBINDCLUSTERID";
            this.textBoxEZLNTBINDCLUSTERID.Size = new System.Drawing.Size(100, 20);
            this.textBoxEZLNTBINDCLUSTERID.TabIndex = 40;
            this.textBoxEZLNTBINDCLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTBINDCLUSTERID_MouseClick);
            this.textBoxEZLNTBINDCLUSTERID.Leave += new System.EventHandler(this.textBoxEZLNTBINDCLUSTERID_Leave);
            this.textBoxEZLNTBINDCLUSTERID.MouseLeave += new System.EventHandler(this.textBoxEZLNTBINDCLUSTERID_MouseLeave);
            this.textBoxEZLNTBINDCLUSTERID.MouseHover += new System.EventHandler(this.textBoxEZLNTBINDCLUSTERID_MouseHover);
            // 
            // buttonEZLNTUNBIND
            // 
            this.buttonEZLNTUNBIND.Location = new System.Drawing.Point(1364, 238);
            this.buttonEZLNTUNBIND.Name = "buttonEZLNTUNBIND";
            this.buttonEZLNTUNBIND.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTUNBIND.TabIndex = 38;
            this.buttonEZLNTUNBIND.Text = "Unbind";
            this.buttonEZLNTUNBIND.UseVisualStyleBackColor = true;
            this.buttonEZLNTUNBIND.Click += new System.EventHandler(this.buttonEZLNTUNBIND_Click);
            // 
            // buttonEZLNTBIND
            // 
            this.buttonEZLNTBIND.Location = new System.Drawing.Point(1177, 237);
            this.buttonEZLNTBIND.Name = "buttonEZLNTBIND";
            this.buttonEZLNTBIND.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTBIND.TabIndex = 37;
            this.buttonEZLNTBIND.Text = "Bind";
            this.buttonEZLNTBIND.UseVisualStyleBackColor = true;
            this.buttonEZLNTBIND.Click += new System.EventHandler(this.buttonEZLNTBIND_Click);
            // 
            // buttonEZLNTTONGGLESTOP
            // 
            this.buttonEZLNTTONGGLESTOP.Location = new System.Drawing.Point(1634, 173);
            this.buttonEZLNTTONGGLESTOP.Name = "buttonEZLNTTONGGLESTOP";
            this.buttonEZLNTTONGGLESTOP.Size = new System.Drawing.Size(113, 23);
            this.buttonEZLNTTONGGLESTOP.TabIndex = 35;
            this.buttonEZLNTTONGGLESTOP.Text = "Stop Tonggle Loop";
            this.buttonEZLNTTONGGLESTOP.UseVisualStyleBackColor = true;
            this.buttonEZLNTTONGGLESTOP.Click += new System.EventHandler(this.buttonEZLNTTONGGLESTOP_Click);
            // 
            // buttonEZLNTTONGGLE
            // 
            this.buttonEZLNTTONGGLE.Location = new System.Drawing.Point(1564, 173);
            this.buttonEZLNTTONGGLE.Name = "buttonEZLNTTONGGLE";
            this.buttonEZLNTTONGGLE.Size = new System.Drawing.Size(64, 23);
            this.buttonEZLNTTONGGLE.TabIndex = 34;
            this.buttonEZLNTTONGGLE.Text = "Tonggle";
            this.buttonEZLNTTONGGLE.UseVisualStyleBackColor = true;
            this.buttonEZLNTTONGGLE.Click += new System.EventHandler(this.buttonEZLNTTONGGLE_Click);
            // 
            // buttonEZLNTOFF
            // 
            this.buttonEZLNTOFF.Location = new System.Drawing.Point(1317, 173);
            this.buttonEZLNTOFF.Name = "buttonEZLNTOFF";
            this.buttonEZLNTOFF.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTOFF.TabIndex = 33;
            this.buttonEZLNTOFF.Text = "Off";
            this.buttonEZLNTOFF.UseVisualStyleBackColor = true;
            this.buttonEZLNTOFF.Click += new System.EventHandler(this.buttonEZLNTOFF_Click);
            // 
            // buttonEZLNTON
            // 
            this.buttonEZLNTON.Location = new System.Drawing.Point(1236, 173);
            this.buttonEZLNTON.Name = "buttonEZLNTON";
            this.buttonEZLNTON.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTON.TabIndex = 32;
            this.buttonEZLNTON.Text = "On";
            this.buttonEZLNTON.UseVisualStyleBackColor = true;
            this.buttonEZLNTON.Click += new System.EventHandler(this.buttonEZLNTON_Click);
            // 
            // textBoxEZLNTSETLOOP
            // 
            this.textBoxEZLNTSETLOOP.Location = new System.Drawing.Point(1273, 50);
            this.textBoxEZLNTSETLOOP.Name = "textBoxEZLNTSETLOOP";
            this.textBoxEZLNTSETLOOP.Size = new System.Drawing.Size(66, 20);
            this.textBoxEZLNTSETLOOP.TabIndex = 27;
            this.textBoxEZLNTSETLOOP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTSETLOOP_MouseClick);
            this.textBoxEZLNTSETLOOP.Leave += new System.EventHandler(this.textBoxEZLNTSETLOOP_Leave);
            this.textBoxEZLNTSETLOOP.MouseLeave += new System.EventHandler(this.textBoxEZLNTSETLOOP_MouseLeave);
            this.textBoxEZLNTSETLOOP.MouseHover += new System.EventHandler(this.textBoxEZLNTSETLOOP_MouseHover);
            // 
            // buttonEZLNTSETTIMER
            // 
            this.buttonEZLNTSETTIMER.Location = new System.Drawing.Point(1175, 50);
            this.buttonEZLNTSETTIMER.Name = "buttonEZLNTSETTIMER";
            this.buttonEZLNTSETTIMER.Size = new System.Drawing.Size(92, 23);
            this.buttonEZLNTSETTIMER.TabIndex = 26;
            this.buttonEZLNTSETTIMER.Text = "Set  Parameter";
            this.buttonEZLNTSETTIMER.UseVisualStyleBackColor = true;
            this.buttonEZLNTSETTIMER.Click += new System.EventHandler(this.buttonEZLNTSETLOOP_Click);
            // 
            // buttonEZLNTSTOPREAD
            // 
            this.buttonEZLNTSTOPREAD.Location = new System.Drawing.Point(1425, 114);
            this.buttonEZLNTSTOPREAD.Name = "buttonEZLNTSTOPREAD";
            this.buttonEZLNTSTOPREAD.Size = new System.Drawing.Size(115, 23);
            this.buttonEZLNTSTOPREAD.TabIndex = 24;
            this.buttonEZLNTSTOPREAD.Text = "Stop Read Loop";
            this.buttonEZLNTSTOPREAD.UseVisualStyleBackColor = true;
            this.buttonEZLNTSTOPREAD.Click += new System.EventHandler(this.buttonEZLNTSTOPREAD_Click);
            // 
            // textBoxEZLNTTIMERINTERVAL
            // 
            this.textBoxEZLNTTIMERINTERVAL.Location = new System.Drawing.Point(1345, 50);
            this.textBoxEZLNTTIMERINTERVAL.Name = "textBoxEZLNTTIMERINTERVAL";
            this.textBoxEZLNTTIMERINTERVAL.Size = new System.Drawing.Size(74, 20);
            this.textBoxEZLNTTIMERINTERVAL.TabIndex = 23;
            this.textBoxEZLNTTIMERINTERVAL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTTIMERINTERVAL_MouseClick);
            this.textBoxEZLNTTIMERINTERVAL.TextChanged += new System.EventHandler(this.textBox1_TextChanged_2);
            this.textBoxEZLNTTIMERINTERVAL.Leave += new System.EventHandler(this.textBoxEZLNTTIMERINTERVAL_Leave);
            this.textBoxEZLNTTIMERINTERVAL.MouseLeave += new System.EventHandler(this.textBoxEZLNTTIMERINTERVAL_MouseLeave);
            this.textBoxEZLNTTIMERINTERVAL.MouseHover += new System.EventHandler(this.textBoxEZLNTTIMERINTERVAL_MouseHover);
            // 
            // buttonEZLNTREADATTRIBUTE
            // 
            this.buttonEZLNTREADATTRIBUTE.Location = new System.Drawing.Point(1177, 114);
            this.buttonEZLNTREADATTRIBUTE.Name = "buttonEZLNTREADATTRIBUTE";
            this.buttonEZLNTREADATTRIBUTE.Size = new System.Drawing.Size(92, 23);
            this.buttonEZLNTREADATTRIBUTE.TabIndex = 21;
            this.buttonEZLNTREADATTRIBUTE.Text = "Read Attribute";
            this.buttonEZLNTREADATTRIBUTE.UseVisualStyleBackColor = true;
            this.buttonEZLNTREADATTRIBUTE.Click += new System.EventHandler(this.buttonEZLNTREADATTRIBUTE_Click);
            // 
            // textBoxEZLNTVIEW
            // 
            this.textBoxEZLNTVIEW.Location = new System.Drawing.Point(1621, 388);
            this.textBoxEZLNTVIEW.Name = "textBoxEZLNTVIEW";
            this.textBoxEZLNTVIEW.Size = new System.Drawing.Size(79, 20);
            this.textBoxEZLNTVIEW.TabIndex = 20;
            this.textBoxEZLNTVIEW.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTVIEW_MouseClick);
            this.textBoxEZLNTVIEW.TextChanged += new System.EventHandler(this.timerReadAttribute_Tick);
            this.textBoxEZLNTVIEW.Leave += new System.EventHandler(this.textBoxEZLNTVIEW_Leave);
            this.textBoxEZLNTVIEW.MouseLeave += new System.EventHandler(this.textBoxEZLNTVIEW_MouseLeave);
            this.textBoxEZLNTVIEW.MouseHover += new System.EventHandler(this.textBoxEZLNTVIEW_MouseHover);
            // 
            // listViewEZLNTGROUP
            // 
            this.listViewEZLNTGROUP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.listViewEZLNTGROUP.CheckBoxes = true;
            this.listViewEZLNTGROUP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nwkAddrJoined,
            this.Status,
            this.Loca});
            this.listViewEZLNTGROUP.HideSelection = false;
            this.listViewEZLNTGROUP.Location = new System.Drawing.Point(835, 51);
            this.listViewEZLNTGROUP.Name = "listViewEZLNTGROUP";
            this.listViewEZLNTGROUP.Size = new System.Drawing.Size(297, 494);
            this.listViewEZLNTGROUP.TabIndex = 19;
            this.listViewEZLNTGROUP.UseCompatibleStateImageBehavior = false;
            this.listViewEZLNTGROUP.View = System.Windows.Forms.View.Details;
            this.listViewEZLNTGROUP.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewEZLNTGROUP_ItemChecked);
            this.listViewEZLNTGROUP.SelectedIndexChanged += new System.EventHandler(this.listViewEZLNTGROUP_SelectedIndexChanged_1);
            // 
            // nwkAddrJoined
            // 
            this.nwkAddrJoined.Text = "nwkAddrJoined";
            this.nwkAddrJoined.Width = 91;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 145;
            // 
            // Loca
            // 
            this.Loca.Text = "Loca";
            // 
            // buttonREMOVEGROUPALL
            // 
            this.buttonREMOVEGROUPALL.Location = new System.Drawing.Point(1707, 386);
            this.buttonREMOVEGROUPALL.Name = "buttonREMOVEGROUPALL";
            this.buttonREMOVEGROUPALL.Size = new System.Drawing.Size(109, 23);
            this.buttonREMOVEGROUPALL.TabIndex = 18;
            this.buttonREMOVEGROUPALL.Text = "Remove Group All";
            this.buttonREMOVEGROUPALL.UseVisualStyleBackColor = true;
            this.buttonREMOVEGROUPALL.Click += new System.EventHandler(this.buttonREMOVEGROUPALL_Click);
            // 
            // buttonEZLNTVIEWGROUP
            // 
            this.buttonEZLNTVIEWGROUP.Location = new System.Drawing.Point(1541, 386);
            this.buttonEZLNTVIEWGROUP.Name = "buttonEZLNTVIEWGROUP";
            this.buttonEZLNTVIEWGROUP.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTVIEWGROUP.TabIndex = 17;
            this.buttonEZLNTVIEWGROUP.Text = "View Group";
            this.buttonEZLNTVIEWGROUP.UseVisualStyleBackColor = true;
            this.buttonEZLNTVIEWGROUP.Click += new System.EventHandler(this.buttonEZLNTVIEWGROUP_Click);
            // 
            // textBoxREMOVEGROUP
            // 
            this.textBoxREMOVEGROUP.Location = new System.Drawing.Point(1461, 388);
            this.textBoxREMOVEGROUP.Name = "textBoxREMOVEGROUP";
            this.textBoxREMOVEGROUP.Size = new System.Drawing.Size(74, 20);
            this.textBoxREMOVEGROUP.TabIndex = 16;
            this.textBoxREMOVEGROUP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxREMOVEGROUP_MouseClick);
            this.textBoxREMOVEGROUP.TextChanged += new System.EventHandler(this.textBoxREMOVEGROUP_TextChanged);
            this.textBoxREMOVEGROUP.Leave += new System.EventHandler(this.textBoxREMOVEGROUP_Leave);
            this.textBoxREMOVEGROUP.MouseLeave += new System.EventHandler(this.textBoxREMOVEGROUP_MouseLeave);
            this.textBoxREMOVEGROUP.MouseHover += new System.EventHandler(this.textBoxREMOVEGROUP_MouseHover);
            // 
            // textBoxEZLNTADDGROUP
            // 
            this.textBoxEZLNTADDGROUP.Location = new System.Drawing.Point(1259, 385);
            this.textBoxEZLNTADDGROUP.Name = "textBoxEZLNTADDGROUP";
            this.textBoxEZLNTADDGROUP.Size = new System.Drawing.Size(79, 20);
            this.textBoxEZLNTADDGROUP.TabIndex = 15;
            this.textBoxEZLNTADDGROUP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxEZLNTADDGROUP_MouseClick);
            this.textBoxEZLNTADDGROUP.Leave += new System.EventHandler(this.textBoxEZLNTADDGROUP_Leave);
            this.textBoxEZLNTADDGROUP.MouseLeave += new System.EventHandler(this.textBoxEZLNTADDGROUP_MouseLeave);
            this.textBoxEZLNTADDGROUP.MouseHover += new System.EventHandler(this.textBoxEZLNTADDGROUP_MouseHover);
            // 
            // buttonEZLNTREMOVEGROUP
            // 
            this.buttonEZLNTREMOVEGROUP.Location = new System.Drawing.Point(1344, 385);
            this.buttonEZLNTREMOVEGROUP.Name = "buttonEZLNTREMOVEGROUP";
            this.buttonEZLNTREMOVEGROUP.Size = new System.Drawing.Size(109, 23);
            this.buttonEZLNTREMOVEGROUP.TabIndex = 14;
            this.buttonEZLNTREMOVEGROUP.Text = "Remove Group";
            this.buttonEZLNTREMOVEGROUP.UseVisualStyleBackColor = true;
            this.buttonEZLNTREMOVEGROUP.Click += new System.EventHandler(this.buttonEZLNTREMOVEGROUP_Click);
            // 
            // buttonEZLNTADDGROUP
            // 
            this.buttonEZLNTADDGROUP.Location = new System.Drawing.Point(1177, 385);
            this.buttonEZLNTADDGROUP.Name = "buttonEZLNTADDGROUP";
            this.buttonEZLNTADDGROUP.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTADDGROUP.TabIndex = 13;
            this.buttonEZLNTADDGROUP.Text = "Add Group";
            this.buttonEZLNTADDGROUP.UseVisualStyleBackColor = true;
            this.buttonEZLNTADDGROUP.Click += new System.EventHandler(this.buttonEZLNTADDGROUP_Click);
            // 
            // buttonREFRESHCOM
            // 
            this.buttonREFRESHCOM.Location = new System.Drawing.Point(60, 12);
            this.buttonREFRESHCOM.Name = "buttonREFRESHCOM";
            this.buttonREFRESHCOM.Size = new System.Drawing.Size(92, 23);
            this.buttonREFRESHCOM.TabIndex = 11;
            this.buttonREFRESHCOM.Text = "Refresh COM";
            this.buttonREFRESHCOM.UseVisualStyleBackColor = true;
            this.buttonREFRESHCOM.Click += new System.EventHandler(this.buttonREFRESHCOM_Click);
            // 
            // buttonPort
            // 
            this.buttonPort.Location = new System.Drawing.Point(158, 12);
            this.buttonPort.Name = "buttonPort";
            this.buttonPort.Size = new System.Drawing.Size(74, 23);
            this.buttonPort.TabIndex = 10;
            this.buttonPort.Text = "Open COMs";
            this.buttonPort.UseVisualStyleBackColor = true;
            this.buttonPort.Click += new System.EventHandler(this.buttonPort_Click);
            // 
            // checkBoxEZLNTALL
            // 
            this.checkBoxEZLNTALL.AutoSize = true;
            this.checkBoxEZLNTALL.Location = new System.Drawing.Point(6, 16);
            this.checkBoxEZLNTALL.Name = "checkBoxEZLNTALL";
            this.checkBoxEZLNTALL.Size = new System.Drawing.Size(45, 17);
            this.checkBoxEZLNTALL.TabIndex = 7;
            this.checkBoxEZLNTALL.Text = "ALL";
            this.checkBoxEZLNTALL.UseVisualStyleBackColor = true;
            this.checkBoxEZLNTALL.CheckedChanged += new System.EventHandler(this.checkBoxEZLNTALL_CheckedChanged);
            // 
            // listViewEZLNTINFO
            // 
            this.listViewEZLNTINFO.AllowDrop = true;
            this.listViewEZLNTINFO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewEZLNTINFO.CheckBoxes = true;
            this.listViewEZLNTINFO.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.COMIndex,
            this.NwkAddr,
            this.MACAddr,
            this.Channel,
            this.Type,
            this.Ver,
            this.Loc,
            this.Chip,
            this.Profile,
            this.IP,
            this.PANID});
            this.listViewEZLNTINFO.FullRowSelect = true;
            this.listViewEZLNTINFO.HideSelection = false;
            this.listViewEZLNTINFO.Location = new System.Drawing.Point(9, 81);
            this.listViewEZLNTINFO.Name = "listViewEZLNTINFO";
            this.listViewEZLNTINFO.Size = new System.Drawing.Size(820, 464);
            this.listViewEZLNTINFO.TabIndex = 3;
            this.listViewEZLNTINFO.UseCompatibleStateImageBehavior = false;
            this.listViewEZLNTINFO.View = System.Windows.Forms.View.Details;
            this.listViewEZLNTINFO.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewEZLNTINFO_ColumnClick);
            this.listViewEZLNTINFO.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewEZLNTINFO_ItemChecked);
            this.listViewEZLNTINFO.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewEZLNTINFO_ItemDrag);
            this.listViewEZLNTINFO.SelectedIndexChanged += new System.EventHandler(this.listViewEZLNTINFO_SelectedIndexChanged);
            this.listViewEZLNTINFO.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewEZLNTINFO_DragDrop);
            this.listViewEZLNTINFO.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewEZLNTINFO_DragEnter);
            this.listViewEZLNTINFO.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewEZLNTINFO_DragOver);
            this.listViewEZLNTINFO.DragLeave += new System.EventHandler(this.listViewEZLNTINFO_DragLeave);
            // 
            // COMIndex
            // 
            this.COMIndex.Text = "COMIndex";
            this.COMIndex.Width = 86;
            // 
            // NwkAddr
            // 
            this.NwkAddr.Text = "NwkAddr";
            this.NwkAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NwkAddr.Width = 67;
            // 
            // MACAddr
            // 
            this.MACAddr.Text = "MACAddr";
            this.MACAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MACAddr.Width = 127;
            // 
            // Channel
            // 
            this.Channel.Text = "Channel";
            this.Channel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Channel.Width = 62;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Type.Width = 98;
            // 
            // Ver
            // 
            this.Ver.Text = "Ver";
            this.Ver.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Ver.Width = 57;
            // 
            // Loc
            // 
            this.Loc.Text = "Loc";
            this.Loc.Width = 47;
            // 
            // Chip
            // 
            this.Chip.Text = "Chip";
            this.Chip.Width = 65;
            // 
            // Profile
            // 
            this.Profile.Text = "Profile";
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.Width = 84;
            // 
            // PANID
            // 
            this.PANID.Text = "PANID";
            // 
            // tabPage17
            // 
            this.tabPage17.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage17.Controls.Add(this.buttonLNTDISABLEPERMIT);
            this.tabPage17.Controls.Add(this.buttonLNTPERMIT);
            this.tabPage17.Controls.Add(this.textBoxLNTSOCKETSEVERIP);
            this.tabPage17.Controls.Add(this.textBoxLNTSOCKETCLIENTIP);
            this.tabPage17.Controls.Add(this.buttonLNTSOCKETSEVER);
            this.tabPage17.Controls.Add(this.buttonLNTSOCKETCLIENT);
            this.tabPage17.Controls.Add(this.checkBoxLNTGROUPALL);
            this.tabPage17.Controls.Add(this.textBoxLNTSETPARAMAXINTERVAL);
            this.tabPage17.Controls.Add(this.buttonLNTTEMPSTOP);
            this.tabPage17.Controls.Add(this.buttonLNTSTOPCOLOR);
            this.tabPage17.Controls.Add(this.buttonLNTCOLORSTOP);
            this.tabPage17.Controls.Add(this.buttonHUESTOP);
            this.tabPage17.Controls.Add(this.textBoxLNTSETPARADIR);
            this.tabPage17.Controls.Add(this.textBoxLNTSETPARASTEP);
            this.tabPage17.Controls.Add(this.comboBoxLEVELMOVEWITHONOFF);
            this.tabPage17.Controls.Add(this.buttonLNTSTOPLEVEL);
            this.tabPage17.Controls.Add(this.buttonLNTSTOPIDENTIFY);
            this.tabPage17.Controls.Add(this.textBoxLNTTEMPTIME);
            this.tabPage17.Controls.Add(this.textBoxLNTTEMP);
            this.tabPage17.Controls.Add(this.textBoxLNTSATTIME);
            this.tabPage17.Controls.Add(this.textBoxLNTSAT);
            this.tabPage17.Controls.Add(this.textBoxLNTCOLORTIME);
            this.tabPage17.Controls.Add(this.textBoxLNTCOLORY);
            this.tabPage17.Controls.Add(this.textBoxLNTCOLORX);
            this.tabPage17.Controls.Add(this.textBoxLNTHUETIME);
            this.tabPage17.Controls.Add(this.textBoxLNTHUEDIR);
            this.tabPage17.Controls.Add(this.textBoxLNTHUE);
            this.tabPage17.Controls.Add(this.textBoxLNTLEVELTIME);
            this.tabPage17.Controls.Add(this.textBoxLNTLEVEL);
            this.tabPage17.Controls.Add(this.textBoxLNTIDENTIFYTIME);
            this.tabPage17.Controls.Add(this.textBoxLNTREADRPRTATTRID);
            this.tabPage17.Controls.Add(this.textBoxLNTREADRPRTCLUSTERID);
            this.tabPage17.Controls.Add(this.textBoxLNTCONFIGRPRTCHANGE);
            this.tabPage17.Controls.Add(this.textBoxLNTCONFIGRPRTTIMEOUT);
            this.tabPage17.Controls.Add(this.textBoxCONFIGRPRTMAXRPRTINTERVAL);
            this.tabPage17.Controls.Add(this.textBoxCONFIGRPRTMININTERVAL);
            this.tabPage17.Controls.Add(this.textBoxCONFIGRPRTATTRID);
            this.tabPage17.Controls.Add(this.textBoxLNTCONFIGRPRTTYPE);
            this.tabPage17.Controls.Add(this.textBoxLNTCONFIGRPRTCLUSTERID);
            this.tabPage17.Controls.Add(this.comboBoxLNTLEAVEWITHCHILDREN);
            this.tabPage17.Controls.Add(this.comboBoxLNTLEAVEREJOIN);
            this.tabPage17.Controls.Add(this.textBoxLNTWRITEATTRDATA);
            this.tabPage17.Controls.Add(this.textBoxLNTWRITEATTRDATATYPE);
            this.tabPage17.Controls.Add(this.textBoxLNTWRITEATTRATTRID);
            this.tabPage17.Controls.Add(this.textBoxLNTWRITEATTRCLUSTERID);
            this.tabPage17.Controls.Add(this.textBoxLNTREADATTRATTRIBUTECOUNT);
            this.tabPage17.Controls.Add(this.textBoxLNTREADATTRATTRIBUTEID);
            this.tabPage17.Controls.Add(this.textBoxLNTREADATTRCLUSTERID);
            this.tabPage17.Controls.Add(this.buttonLNTTEMP);
            this.tabPage17.Controls.Add(this.buttonLNTSAT);
            this.tabPage17.Controls.Add(this.buttonLNTCOLOR);
            this.tabPage17.Controls.Add(this.buttonLNTHUE);
            this.tabPage17.Controls.Add(this.buttonLNTLEVEL);
            this.tabPage17.Controls.Add(this.buttonLNTIDENTIFY);
            this.tabPage17.Controls.Add(this.buttonLNTRESET);
            this.tabPage17.Controls.Add(this.buttonLNTREADRPRT);
            this.tabPage17.Controls.Add(this.buttonLNTCONFIGRPRT);
            this.tabPage17.Controls.Add(this.buttonLNTLEAVE);
            this.tabPage17.Controls.Add(this.buttonLNTWRITEATTRIBUTE);
            this.tabPage17.Controls.Add(this.textBoxLNTUNBINDIEEEADDR);
            this.tabPage17.Controls.Add(this.textBoxLNTBINDIEEEADDR);
            this.tabPage17.Controls.Add(this.buttonLNTUNBIND);
            this.tabPage17.Controls.Add(this.buttonLNTBIND);
            this.tabPage17.Controls.Add(this.buttonLNTSTOPTONGGLE);
            this.tabPage17.Controls.Add(this.buttonLNTTONGGLE);
            this.tabPage17.Controls.Add(this.buttonLNTOFF);
            this.tabPage17.Controls.Add(this.buttonLNTON);
            this.tabPage17.Controls.Add(this.textBoxLNTSETLOOP);
            this.tabPage17.Controls.Add(this.buttonLNTSETPARA);
            this.tabPage17.Controls.Add(this.buttonLNTSTOPTEADATTRIBUTE);
            this.tabPage17.Controls.Add(this.textBoxLNTSETPARAMININTERVAL);
            this.tabPage17.Controls.Add(this.buttonLNTREADATTRIBUTE);
            this.tabPage17.Controls.Add(this.textBoxLNTVIEWGROUPADDRESS);
            this.tabPage17.Controls.Add(this.buttonLNTREMOVEALL);
            this.tabPage17.Controls.Add(this.buttonLNTVIEWGROUP);
            this.tabPage17.Controls.Add(this.textBoxLNTREMOVEGROUPADDRESS);
            this.tabPage17.Controls.Add(this.textBoxLNTADDGROUPADDR);
            this.tabPage17.Controls.Add(this.buttonLNTREMOVEGROUP);
            this.tabPage17.Controls.Add(this.buttonLNTADDGROUP);
            this.tabPage17.Controls.Add(this.textBoxLNTSENDCOMMAND);
            this.tabPage17.Controls.Add(this.buttonLNTSENDCOMMAND);
            this.tabPage17.Controls.Add(this.checkBoxLNTALL);
            this.tabPage17.Controls.Add(this.listViewLNTGROUPINFO);
            this.tabPage17.Controls.Add(this.listViewLNTCOMINFO);
            this.tabPage17.Controls.Add(this.buttonLNTREMOTELOAD);
            this.tabPage17.Location = new System.Drawing.Point(4, 22);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage17.Size = new System.Drawing.Size(1833, 584);
            this.tabPage17.TabIndex = 21;
            this.tabPage17.Text = "LNT Remote";
            this.tabPage17.Click += new System.EventHandler(this.tabPage17_Click);
            // 
            // buttonLNTDISABLEPERMIT
            // 
            this.buttonLNTDISABLEPERMIT.Location = new System.Drawing.Point(970, 53);
            this.buttonLNTDISABLEPERMIT.Name = "buttonLNTDISABLEPERMIT";
            this.buttonLNTDISABLEPERMIT.Size = new System.Drawing.Size(122, 23);
            this.buttonLNTDISABLEPERMIT.TabIndex = 222;
            this.buttonLNTDISABLEPERMIT.Text = "Disable Permit Join";
            this.buttonLNTDISABLEPERMIT.UseVisualStyleBackColor = true;
            this.buttonLNTDISABLEPERMIT.Click += new System.EventHandler(this.buttonLNTDISABLEPERMIT_Click);
            // 
            // buttonLNTPERMIT
            // 
            this.buttonLNTPERMIT.Location = new System.Drawing.Point(889, 53);
            this.buttonLNTPERMIT.Name = "buttonLNTPERMIT";
            this.buttonLNTPERMIT.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTPERMIT.TabIndex = 221;
            this.buttonLNTPERMIT.Text = "Permit Join";
            this.buttonLNTPERMIT.UseVisualStyleBackColor = true;
            this.buttonLNTPERMIT.Click += new System.EventHandler(this.buttonLNTPERMIT_Click);
            // 
            // textBoxLNTSOCKETSEVERIP
            // 
            this.textBoxLNTSOCKETSEVERIP.Location = new System.Drawing.Point(394, 52);
            this.textBoxLNTSOCKETSEVERIP.Name = "textBoxLNTSOCKETSEVERIP";
            this.textBoxLNTSOCKETSEVERIP.ReadOnly = true;
            this.textBoxLNTSOCKETSEVERIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTSOCKETSEVERIP.TabIndex = 220;
            // 
            // textBoxLNTSOCKETCLIENTIP
            // 
            this.textBoxLNTSOCKETCLIENTIP.Location = new System.Drawing.Point(394, 23);
            this.textBoxLNTSOCKETCLIENTIP.Name = "textBoxLNTSOCKETCLIENTIP";
            this.textBoxLNTSOCKETCLIENTIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTSOCKETCLIENTIP.TabIndex = 219;
            // 
            // buttonLNTSOCKETSEVER
            // 
            this.buttonLNTSOCKETSEVER.Location = new System.Drawing.Point(313, 50);
            this.buttonLNTSOCKETSEVER.Name = "buttonLNTSOCKETSEVER";
            this.buttonLNTSOCKETSEVER.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTSOCKETSEVER.TabIndex = 216;
            this.buttonLNTSOCKETSEVER.Text = "As Sever";
            this.buttonLNTSOCKETSEVER.UseVisualStyleBackColor = true;
            this.buttonLNTSOCKETSEVER.Click += new System.EventHandler(this.buttonLNTSOCKETSEVER_Click);
            // 
            // buttonLNTSOCKETCLIENT
            // 
            this.buttonLNTSOCKETCLIENT.Location = new System.Drawing.Point(313, 23);
            this.buttonLNTSOCKETCLIENT.Name = "buttonLNTSOCKETCLIENT";
            this.buttonLNTSOCKETCLIENT.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTSOCKETCLIENT.TabIndex = 215;
            this.buttonLNTSOCKETCLIENT.Text = "As Client";
            this.buttonLNTSOCKETCLIENT.UseVisualStyleBackColor = true;
            this.buttonLNTSOCKETCLIENT.Click += new System.EventHandler(this.buttonLNTSOCKETCLIENT_Click);
            // 
            // checkBoxLNTGROUPALL
            // 
            this.checkBoxLNTGROUPALL.AutoSize = true;
            this.checkBoxLNTGROUPALL.Location = new System.Drawing.Point(517, 53);
            this.checkBoxLNTGROUPALL.Name = "checkBoxLNTGROUPALL";
            this.checkBoxLNTGROUPALL.Size = new System.Drawing.Size(45, 17);
            this.checkBoxLNTGROUPALL.TabIndex = 214;
            this.checkBoxLNTGROUPALL.Text = "ALL";
            this.checkBoxLNTGROUPALL.UseVisualStyleBackColor = true;
            this.checkBoxLNTGROUPALL.CheckedChanged += new System.EventHandler(this.checkBoxLNTGROUPALL_CheckedChanged);
            // 
            // textBoxLNTSETPARAMAXINTERVAL
            // 
            this.textBoxLNTSETPARAMAXINTERVAL.Location = new System.Drawing.Point(1039, 26);
            this.textBoxLNTSETPARAMAXINTERVAL.Name = "textBoxLNTSETPARAMAXINTERVAL";
            this.textBoxLNTSETPARAMAXINTERVAL.Size = new System.Drawing.Size(71, 20);
            this.textBoxLNTSETPARAMAXINTERVAL.TabIndex = 213;
            this.textBoxLNTSETPARAMAXINTERVAL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTSETPARAMAXINTERVAL_MouseClick);
            this.textBoxLNTSETPARAMAXINTERVAL.Leave += new System.EventHandler(this.textBoxLNTSETPARAMAXINTERVAL_Leave);
            this.textBoxLNTSETPARAMAXINTERVAL.MouseLeave += new System.EventHandler(this.textBoxLNTSETPARAMAXINTERVAL_MouseLeave);
            this.textBoxLNTSETPARAMAXINTERVAL.MouseHover += new System.EventHandler(this.textBoxLNTSETPARAMAXINTERVAL_MouseHover);
            // 
            // buttonLNTTEMPSTOP
            // 
            this.buttonLNTTEMPSTOP.Location = new System.Drawing.Point(1106, 467);
            this.buttonLNTTEMPSTOP.Name = "buttonLNTTEMPSTOP";
            this.buttonLNTTEMPSTOP.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTTEMPSTOP.TabIndex = 212;
            this.buttonLNTTEMPSTOP.Text = "Stop Loop";
            this.buttonLNTTEMPSTOP.UseVisualStyleBackColor = true;
            this.buttonLNTTEMPSTOP.Click += new System.EventHandler(this.buttonLNTTEMPSTOP_Click);
            // 
            // buttonLNTSTOPCOLOR
            // 
            this.buttonLNTSTOPCOLOR.Location = new System.Drawing.Point(1106, 436);
            this.buttonLNTSTOPCOLOR.Name = "buttonLNTSTOPCOLOR";
            this.buttonLNTSTOPCOLOR.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTSTOPCOLOR.TabIndex = 211;
            this.buttonLNTSTOPCOLOR.Text = "Stop Loop";
            this.buttonLNTSTOPCOLOR.UseVisualStyleBackColor = true;
            this.buttonLNTSTOPCOLOR.Click += new System.EventHandler(this.buttonLNTSTOPCOLOR_Click);
            // 
            // buttonLNTCOLORSTOP
            // 
            this.buttonLNTCOLORSTOP.Location = new System.Drawing.Point(1216, 411);
            this.buttonLNTCOLORSTOP.Name = "buttonLNTCOLORSTOP";
            this.buttonLNTCOLORSTOP.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTCOLORSTOP.TabIndex = 210;
            this.buttonLNTCOLORSTOP.Text = "Stop Loop";
            this.buttonLNTCOLORSTOP.UseVisualStyleBackColor = true;
            this.buttonLNTCOLORSTOP.Click += new System.EventHandler(this.buttonLNTCOLORSTOP_Click);
            // 
            // buttonHUESTOP
            // 
            this.buttonHUESTOP.Location = new System.Drawing.Point(1216, 380);
            this.buttonHUESTOP.Name = "buttonHUESTOP";
            this.buttonHUESTOP.Size = new System.Drawing.Size(75, 23);
            this.buttonHUESTOP.TabIndex = 209;
            this.buttonHUESTOP.Text = "Stop Loop";
            this.buttonHUESTOP.UseVisualStyleBackColor = true;
            this.buttonHUESTOP.Click += new System.EventHandler(this.buttonHUESTOP_Click);
            // 
            // textBoxLNTSETPARADIR
            // 
            this.textBoxLNTSETPARADIR.Location = new System.Drawing.Point(1179, 26);
            this.textBoxLNTSETPARADIR.Name = "textBoxLNTSETPARADIR";
            this.textBoxLNTSETPARADIR.Size = new System.Drawing.Size(58, 20);
            this.textBoxLNTSETPARADIR.TabIndex = 208;
            this.textBoxLNTSETPARADIR.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTSETPARADIR_MouseClick);
            this.textBoxLNTSETPARADIR.Leave += new System.EventHandler(this.textBoxLNTSETPARADIR_Leave);
            this.textBoxLNTSETPARADIR.MouseLeave += new System.EventHandler(this.textBoxLNTSETPARADIR_MouseLeave);
            this.textBoxLNTSETPARADIR.MouseHover += new System.EventHandler(this.textBoxLNTSETPARADIR_MouseHover);
            // 
            // textBoxLNTSETPARASTEP
            // 
            this.textBoxLNTSETPARASTEP.Location = new System.Drawing.Point(1116, 26);
            this.textBoxLNTSETPARASTEP.Name = "textBoxLNTSETPARASTEP";
            this.textBoxLNTSETPARASTEP.Size = new System.Drawing.Size(57, 20);
            this.textBoxLNTSETPARASTEP.TabIndex = 207;
            this.textBoxLNTSETPARASTEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTSETPARASTEP_MouseClick);
            this.textBoxLNTSETPARASTEP.Leave += new System.EventHandler(this.textBoxLNTSETPARASTEP_Leave);
            this.textBoxLNTSETPARASTEP.MouseLeave += new System.EventHandler(this.textBoxLNTSETPARASTEP_MouseLeave);
            this.textBoxLNTSETPARASTEP.MouseHover += new System.EventHandler(this.textBoxLNTSETPARASTEP_MouseHover);
            // 
            // comboBoxLEVELMOVEWITHONOFF
            // 
            this.comboBoxLEVELMOVEWITHONOFF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLEVELMOVEWITHONOFF.FormattingEnabled = true;
            this.comboBoxLEVELMOVEWITHONOFF.Items.AddRange(new object[] {
            "WITHOUT ON/OFF",
            "WITH ON/OFF"});
            this.comboBoxLEVELMOVEWITHONOFF.Location = new System.Drawing.Point(1104, 352);
            this.comboBoxLEVELMOVEWITHONOFF.Name = "comboBoxLEVELMOVEWITHONOFF";
            this.comboBoxLEVELMOVEWITHONOFF.Size = new System.Drawing.Size(106, 21);
            this.comboBoxLEVELMOVEWITHONOFF.TabIndex = 206;
            // 
            // buttonLNTSTOPLEVEL
            // 
            this.buttonLNTSTOPLEVEL.Location = new System.Drawing.Point(1216, 350);
            this.buttonLNTSTOPLEVEL.Name = "buttonLNTSTOPLEVEL";
            this.buttonLNTSTOPLEVEL.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTSTOPLEVEL.TabIndex = 205;
            this.buttonLNTSTOPLEVEL.Text = "Stop Loop";
            this.buttonLNTSTOPLEVEL.UseVisualStyleBackColor = true;
            this.buttonLNTSTOPLEVEL.Click += new System.EventHandler(this.buttonLNTSTOPLEVEL_Click);
            // 
            // buttonLNTSTOPIDENTIFY
            // 
            this.buttonLNTSTOPIDENTIFY.Location = new System.Drawing.Point(995, 294);
            this.buttonLNTSTOPIDENTIFY.Name = "buttonLNTSTOPIDENTIFY";
            this.buttonLNTSTOPIDENTIFY.Size = new System.Drawing.Size(99, 23);
            this.buttonLNTSTOPIDENTIFY.TabIndex = 204;
            this.buttonLNTSTOPIDENTIFY.Text = "Stop Identify Loop";
            this.buttonLNTSTOPIDENTIFY.UseVisualStyleBackColor = true;
            this.buttonLNTSTOPIDENTIFY.Click += new System.EventHandler(this.buttonLNTSTOPIDENTIFY_Click);
            // 
            // textBoxLNTTEMPTIME
            // 
            this.textBoxLNTTEMPTIME.Location = new System.Drawing.Point(996, 469);
            this.textBoxLNTTEMPTIME.Name = "textBoxLNTTEMPTIME";
            this.textBoxLNTTEMPTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTTEMPTIME.TabIndex = 203;
            this.textBoxLNTTEMPTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTTEMPTIME_MouseClick);
            this.textBoxLNTTEMPTIME.Leave += new System.EventHandler(this.textBoxLNTTEMPTIME_Leave);
            this.textBoxLNTTEMPTIME.MouseLeave += new System.EventHandler(this.textBoxLNTTEMPTIME_MouseLeave);
            this.textBoxLNTTEMPTIME.MouseHover += new System.EventHandler(this.textBoxLNTTEMPTIME_MouseHover);
            // 
            // textBoxLNTTEMP
            // 
            this.textBoxLNTTEMP.Location = new System.Drawing.Point(890, 469);
            this.textBoxLNTTEMP.Name = "textBoxLNTTEMP";
            this.textBoxLNTTEMP.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTTEMP.TabIndex = 202;
            this.textBoxLNTTEMP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTTEMP_MouseClick);
            this.textBoxLNTTEMP.Leave += new System.EventHandler(this.textBoxLNTTEMP_Leave);
            this.textBoxLNTTEMP.MouseLeave += new System.EventHandler(this.textBoxLNTTEMP_MouseLeave);
            this.textBoxLNTTEMP.MouseHover += new System.EventHandler(this.textBoxLNTTEMP_MouseHover);
            // 
            // textBoxLNTSATTIME
            // 
            this.textBoxLNTSATTIME.Location = new System.Drawing.Point(996, 439);
            this.textBoxLNTSATTIME.Name = "textBoxLNTSATTIME";
            this.textBoxLNTSATTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTSATTIME.TabIndex = 201;
            this.textBoxLNTSATTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTSATTIME_MouseClick);
            this.textBoxLNTSATTIME.Leave += new System.EventHandler(this.textBoxLNTSATTIME_Leave);
            this.textBoxLNTSATTIME.MouseLeave += new System.EventHandler(this.textBoxLNTSATTIME_MouseLeave);
            this.textBoxLNTSATTIME.MouseHover += new System.EventHandler(this.textBoxLNTSATTIME_MouseHover);
            // 
            // textBoxLNTSAT
            // 
            this.textBoxLNTSAT.Location = new System.Drawing.Point(890, 439);
            this.textBoxLNTSAT.Name = "textBoxLNTSAT";
            this.textBoxLNTSAT.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTSAT.TabIndex = 200;
            this.textBoxLNTSAT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTSAT_MouseClick);
            this.textBoxLNTSAT.Leave += new System.EventHandler(this.textBoxLNTSAT_Leave);
            this.textBoxLNTSAT.MouseLeave += new System.EventHandler(this.textBoxLNTSAT_MouseLeave);
            this.textBoxLNTSAT.MouseHover += new System.EventHandler(this.textBoxLNTSAT_MouseHover);
            // 
            // textBoxLNTCOLORTIME
            // 
            this.textBoxLNTCOLORTIME.Location = new System.Drawing.Point(1106, 411);
            this.textBoxLNTCOLORTIME.Name = "textBoxLNTCOLORTIME";
            this.textBoxLNTCOLORTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTCOLORTIME.TabIndex = 199;
            this.textBoxLNTCOLORTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTCOLORTIME_MouseClick);
            this.textBoxLNTCOLORTIME.Leave += new System.EventHandler(this.textBoxLNTCOLORTIME_Leave);
            this.textBoxLNTCOLORTIME.MouseLeave += new System.EventHandler(this.textBoxLNTCOLORTIME_MouseLeave);
            this.textBoxLNTCOLORTIME.MouseHover += new System.EventHandler(this.textBoxLNTCOLORTIME_MouseHover);
            // 
            // textBoxLNTCOLORY
            // 
            this.textBoxLNTCOLORY.Location = new System.Drawing.Point(999, 409);
            this.textBoxLNTCOLORY.Name = "textBoxLNTCOLORY";
            this.textBoxLNTCOLORY.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTCOLORY.TabIndex = 198;
            this.textBoxLNTCOLORY.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTCOLORY_MouseClick);
            this.textBoxLNTCOLORY.Leave += new System.EventHandler(this.textBoxLNTCOLORY_Leave);
            this.textBoxLNTCOLORY.MouseLeave += new System.EventHandler(this.textBoxLNTCOLORY_MouseLeave);
            this.textBoxLNTCOLORY.MouseHover += new System.EventHandler(this.textBoxLNTCOLORY_MouseHover);
            // 
            // textBoxLNTCOLORX
            // 
            this.textBoxLNTCOLORX.Location = new System.Drawing.Point(891, 411);
            this.textBoxLNTCOLORX.Name = "textBoxLNTCOLORX";
            this.textBoxLNTCOLORX.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTCOLORX.TabIndex = 197;
            this.textBoxLNTCOLORX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTCOLORX_MouseClick);
            this.textBoxLNTCOLORX.Leave += new System.EventHandler(this.textBoxLNTCOLORX_Leave);
            this.textBoxLNTCOLORX.MouseLeave += new System.EventHandler(this.textBoxLNTCOLORX_MouseLeave);
            this.textBoxLNTCOLORX.MouseHover += new System.EventHandler(this.textBoxLNTCOLORX_MouseHover);
            // 
            // textBoxLNTHUETIME
            // 
            this.textBoxLNTHUETIME.Location = new System.Drawing.Point(1106, 383);
            this.textBoxLNTHUETIME.Name = "textBoxLNTHUETIME";
            this.textBoxLNTHUETIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTHUETIME.TabIndex = 196;
            this.textBoxLNTHUETIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTHUETIME_MouseClick);
            this.textBoxLNTHUETIME.Leave += new System.EventHandler(this.textBoxLNTHUETIME_Leave);
            this.textBoxLNTHUETIME.MouseLeave += new System.EventHandler(this.textBoxLNTHUETIME_MouseLeave);
            this.textBoxLNTHUETIME.MouseHover += new System.EventHandler(this.textBoxLNTHUETIME_MouseHover);
            // 
            // textBoxLNTHUEDIR
            // 
            this.textBoxLNTHUEDIR.Location = new System.Drawing.Point(999, 383);
            this.textBoxLNTHUEDIR.Name = "textBoxLNTHUEDIR";
            this.textBoxLNTHUEDIR.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTHUEDIR.TabIndex = 195;
            this.textBoxLNTHUEDIR.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTHUEDIR_MouseClick);
            this.textBoxLNTHUEDIR.Leave += new System.EventHandler(this.textBoxLNTHUEDIR_Leave);
            this.textBoxLNTHUEDIR.MouseLeave += new System.EventHandler(this.textBoxLNTHUEDIR_MouseLeave);
            this.textBoxLNTHUEDIR.MouseHover += new System.EventHandler(this.textBoxLNTHUEDIR_MouseHover);
            // 
            // textBoxLNTHUE
            // 
            this.textBoxLNTHUE.Location = new System.Drawing.Point(889, 383);
            this.textBoxLNTHUE.Name = "textBoxLNTHUE";
            this.textBoxLNTHUE.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTHUE.TabIndex = 194;
            this.textBoxLNTHUE.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTHUE_MouseClick);
            this.textBoxLNTHUE.Leave += new System.EventHandler(this.textBoxLNTHUE_Leave);
            this.textBoxLNTHUE.MouseLeave += new System.EventHandler(this.textBoxLNTHUE_MouseLeave);
            this.textBoxLNTHUE.MouseHover += new System.EventHandler(this.textBoxLNTHUE_MouseHover);
            // 
            // textBoxLNTLEVELTIME
            // 
            this.textBoxLNTLEVELTIME.Location = new System.Drawing.Point(998, 353);
            this.textBoxLNTLEVELTIME.Name = "textBoxLNTLEVELTIME";
            this.textBoxLNTLEVELTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTLEVELTIME.TabIndex = 193;
            this.textBoxLNTLEVELTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTLEVELTIME_MouseClick);
            this.textBoxLNTLEVELTIME.Leave += new System.EventHandler(this.textBoxLNTLEVELTIME_Leave);
            this.textBoxLNTLEVELTIME.MouseLeave += new System.EventHandler(this.textBoxLNTLEVELTIME_MouseLeave);
            this.textBoxLNTLEVELTIME.MouseHover += new System.EventHandler(this.textBoxLNTLEVELTIME_MouseHover);
            // 
            // textBoxLNTLEVEL
            // 
            this.textBoxLNTLEVEL.Location = new System.Drawing.Point(891, 354);
            this.textBoxLNTLEVEL.Name = "textBoxLNTLEVEL";
            this.textBoxLNTLEVEL.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTLEVEL.TabIndex = 192;
            this.textBoxLNTLEVEL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTLEVEL_MouseClick);
            this.textBoxLNTLEVEL.Leave += new System.EventHandler(this.textBoxLNTLEVEL_Leave);
            this.textBoxLNTLEVEL.MouseLeave += new System.EventHandler(this.textBoxLNTLEVEL_MouseLeave);
            this.textBoxLNTLEVEL.MouseHover += new System.EventHandler(this.textBoxLNTLEVEL_MouseHover);
            // 
            // textBoxLNTIDENTIFYTIME
            // 
            this.textBoxLNTIDENTIFYTIME.Location = new System.Drawing.Point(889, 294);
            this.textBoxLNTIDENTIFYTIME.Name = "textBoxLNTIDENTIFYTIME";
            this.textBoxLNTIDENTIFYTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTIDENTIFYTIME.TabIndex = 191;
            this.textBoxLNTIDENTIFYTIME.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTIDENTIFYTIME_MouseClick);
            this.textBoxLNTIDENTIFYTIME.Leave += new System.EventHandler(this.textBoxLNTIDENTIFYTIME_Leave);
            this.textBoxLNTIDENTIFYTIME.MouseLeave += new System.EventHandler(this.textBoxLNTIDENTIFYTIME_MouseLeave);
            this.textBoxLNTIDENTIFYTIME.MouseHover += new System.EventHandler(this.textBoxLNTIDENTIFYTIME_MouseHover);
            // 
            // textBoxLNTREADRPRTATTRID
            // 
            this.textBoxLNTREADRPRTATTRID.Location = new System.Drawing.Point(953, 264);
            this.textBoxLNTREADRPRTATTRID.Name = "textBoxLNTREADRPRTATTRID";
            this.textBoxLNTREADRPRTATTRID.Size = new System.Drawing.Size(63, 20);
            this.textBoxLNTREADRPRTATTRID.TabIndex = 190;
            this.textBoxLNTREADRPRTATTRID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTREADRPRTATTRID_MouseClick);
            this.textBoxLNTREADRPRTATTRID.Leave += new System.EventHandler(this.textBoxLNTREADRPRTATTRID_Leave);
            this.textBoxLNTREADRPRTATTRID.MouseLeave += new System.EventHandler(this.textBoxLNTREADRPRTATTRID_MouseLeave);
            this.textBoxLNTREADRPRTATTRID.MouseHover += new System.EventHandler(this.textBoxLNTREADRPRTATTRID_MouseHover);
            // 
            // textBoxLNTREADRPRTCLUSTERID
            // 
            this.textBoxLNTREADRPRTCLUSTERID.Location = new System.Drawing.Point(877, 264);
            this.textBoxLNTREADRPRTCLUSTERID.Name = "textBoxLNTREADRPRTCLUSTERID";
            this.textBoxLNTREADRPRTCLUSTERID.Size = new System.Drawing.Size(70, 20);
            this.textBoxLNTREADRPRTCLUSTERID.TabIndex = 189;
            this.textBoxLNTREADRPRTCLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTREADRPRTCLUSTERID_MouseClick);
            this.textBoxLNTREADRPRTCLUSTERID.Leave += new System.EventHandler(this.textBoxLNTREADRPRTCLUSTERID_Leave);
            this.textBoxLNTREADRPRTCLUSTERID.MouseLeave += new System.EventHandler(this.textBoxLNTREADRPRTCLUSTERID_MouseLeave);
            this.textBoxLNTREADRPRTCLUSTERID.MouseHover += new System.EventHandler(this.textBoxLNTREADRPRTCLUSTERID_MouseHover);
            // 
            // textBoxLNTCONFIGRPRTCHANGE
            // 
            this.textBoxLNTCONFIGRPRTCHANGE.Location = new System.Drawing.Point(1287, 234);
            this.textBoxLNTCONFIGRPRTCHANGE.Name = "textBoxLNTCONFIGRPRTCHANGE";
            this.textBoxLNTCONFIGRPRTCHANGE.Size = new System.Drawing.Size(58, 20);
            this.textBoxLNTCONFIGRPRTCHANGE.TabIndex = 188;
            this.textBoxLNTCONFIGRPRTCHANGE.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTCONFIGRPRTCHANGE_MouseClick);
            this.textBoxLNTCONFIGRPRTCHANGE.Leave += new System.EventHandler(this.textBoxLNTCONFIGRPRTCHANGE_Leave);
            this.textBoxLNTCONFIGRPRTCHANGE.MouseLeave += new System.EventHandler(this.textBoxLNTCONFIGRPRTCHANGE_MouseLeave);
            this.textBoxLNTCONFIGRPRTCHANGE.MouseHover += new System.EventHandler(this.textBoxLNTCONFIGRPRTCHANGE_MouseHover);
            // 
            // textBoxLNTCONFIGRPRTTIMEOUT
            // 
            this.textBoxLNTCONFIGRPRTTIMEOUT.Location = new System.Drawing.Point(1212, 233);
            this.textBoxLNTCONFIGRPRTTIMEOUT.Name = "textBoxLNTCONFIGRPRTTIMEOUT";
            this.textBoxLNTCONFIGRPRTTIMEOUT.Size = new System.Drawing.Size(69, 20);
            this.textBoxLNTCONFIGRPRTTIMEOUT.TabIndex = 187;
            this.textBoxLNTCONFIGRPRTTIMEOUT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTCONFIGRPRTTIMEOUT_MouseClick);
            this.textBoxLNTCONFIGRPRTTIMEOUT.Leave += new System.EventHandler(this.textBoxLNTCONFIGRPRTTIMEOUT_Leave);
            this.textBoxLNTCONFIGRPRTTIMEOUT.MouseLeave += new System.EventHandler(this.textBoxLNTCONFIGRPRTTIMEOUT_MouseLeave);
            this.textBoxLNTCONFIGRPRTTIMEOUT.MouseHover += new System.EventHandler(this.textBoxLNTCONFIGRPRTTIMEOUT_MouseHover);
            // 
            // textBoxCONFIGRPRTMAXRPRTINTERVAL
            // 
            this.textBoxCONFIGRPRTMAXRPRTINTERVAL.Location = new System.Drawing.Point(1140, 233);
            this.textBoxCONFIGRPRTMAXRPRTINTERVAL.Name = "textBoxCONFIGRPRTMAXRPRTINTERVAL";
            this.textBoxCONFIGRPRTMAXRPRTINTERVAL.Size = new System.Drawing.Size(66, 20);
            this.textBoxCONFIGRPRTMAXRPRTINTERVAL.TabIndex = 186;
            this.textBoxCONFIGRPRTMAXRPRTINTERVAL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxCONFIGRPRTMAXRPRTINTERVAL_MouseClick);
            this.textBoxCONFIGRPRTMAXRPRTINTERVAL.Leave += new System.EventHandler(this.textBoxCONFIGRPRTMAXRPRTINTERVAL_Leave);
            this.textBoxCONFIGRPRTMAXRPRTINTERVAL.MouseLeave += new System.EventHandler(this.textBoxCONFIGRPRTMAXRPRTINTERVAL_MouseLeave);
            this.textBoxCONFIGRPRTMAXRPRTINTERVAL.MouseHover += new System.EventHandler(this.textBoxCONFIGRPRTMAXRPRTINTERVAL_MouseHover);
            // 
            // textBoxCONFIGRPRTMININTERVAL
            // 
            this.textBoxCONFIGRPRTMININTERVAL.Location = new System.Drawing.Point(1075, 233);
            this.textBoxCONFIGRPRTMININTERVAL.Name = "textBoxCONFIGRPRTMININTERVAL";
            this.textBoxCONFIGRPRTMININTERVAL.Size = new System.Drawing.Size(58, 20);
            this.textBoxCONFIGRPRTMININTERVAL.TabIndex = 185;
            this.textBoxCONFIGRPRTMININTERVAL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxCONFIGRPRTMININTERVAL_MouseClick);
            this.textBoxCONFIGRPRTMININTERVAL.Leave += new System.EventHandler(this.textBoxCONFIGRPRTMININTERVAL_Leave);
            this.textBoxCONFIGRPRTMININTERVAL.MouseLeave += new System.EventHandler(this.textBoxCONFIGRPRTMININTERVAL_MouseLeave);
            this.textBoxCONFIGRPRTMININTERVAL.MouseHover += new System.EventHandler(this.textBoxCONFIGRPRTMININTERVAL_MouseHover);
            // 
            // textBoxCONFIGRPRTATTRID
            // 
            this.textBoxCONFIGRPRTATTRID.Location = new System.Drawing.Point(951, 235);
            this.textBoxCONFIGRPRTATTRID.Name = "textBoxCONFIGRPRTATTRID";
            this.textBoxCONFIGRPRTATTRID.Size = new System.Drawing.Size(57, 20);
            this.textBoxCONFIGRPRTATTRID.TabIndex = 184;
            this.textBoxCONFIGRPRTATTRID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxCONFIGRPRTATTRID_MouseClick);
            this.textBoxCONFIGRPRTATTRID.Leave += new System.EventHandler(this.textBoxCONFIGRPRTATTRID_Leave);
            this.textBoxCONFIGRPRTATTRID.MouseLeave += new System.EventHandler(this.textBoxCONFIGRPRTATTRID_MouseLeave);
            this.textBoxCONFIGRPRTATTRID.MouseHover += new System.EventHandler(this.textBoxCONFIGRPRTATTRID_MouseHover);
            // 
            // textBoxLNTCONFIGRPRTTYPE
            // 
            this.textBoxLNTCONFIGRPRTTYPE.Location = new System.Drawing.Point(1016, 234);
            this.textBoxLNTCONFIGRPRTTYPE.Name = "textBoxLNTCONFIGRPRTTYPE";
            this.textBoxLNTCONFIGRPRTTYPE.Size = new System.Drawing.Size(53, 20);
            this.textBoxLNTCONFIGRPRTTYPE.TabIndex = 183;
            this.textBoxLNTCONFIGRPRTTYPE.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTCONFIGRPRTTYPE_MouseClick);
            this.textBoxLNTCONFIGRPRTTYPE.Leave += new System.EventHandler(this.textBoxLNTCONFIGRPRTTYPE_Leave);
            this.textBoxLNTCONFIGRPRTTYPE.MouseLeave += new System.EventHandler(this.textBoxLNTCONFIGRPRTTYPE_MouseLeave);
            this.textBoxLNTCONFIGRPRTTYPE.MouseHover += new System.EventHandler(this.textBoxLNTCONFIGRPRTTYPE_MouseHover);
            // 
            // textBoxLNTCONFIGRPRTCLUSTERID
            // 
            this.textBoxLNTCONFIGRPRTCLUSTERID.Location = new System.Drawing.Point(878, 234);
            this.textBoxLNTCONFIGRPRTCLUSTERID.Name = "textBoxLNTCONFIGRPRTCLUSTERID";
            this.textBoxLNTCONFIGRPRTCLUSTERID.Size = new System.Drawing.Size(67, 20);
            this.textBoxLNTCONFIGRPRTCLUSTERID.TabIndex = 182;
            this.textBoxLNTCONFIGRPRTCLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTCONFIGRPRTCLUSTERID_MouseClick);
            this.textBoxLNTCONFIGRPRTCLUSTERID.Leave += new System.EventHandler(this.textBoxLNTCONFIGRPRTCLUSTERID_Leave);
            this.textBoxLNTCONFIGRPRTCLUSTERID.MouseLeave += new System.EventHandler(this.textBoxLNTCONFIGRPRTCLUSTERID_MouseLeave);
            this.textBoxLNTCONFIGRPRTCLUSTERID.MouseHover += new System.EventHandler(this.textBoxLNTCONFIGRPRTCLUSTERID_MouseHover);
            // 
            // comboBoxLNTLEAVEWITHCHILDREN
            // 
            this.comboBoxLNTLEAVEWITHCHILDREN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLNTLEAVEWITHCHILDREN.FormattingEnabled = true;
            this.comboBoxLNTLEAVEWITHCHILDREN.Items.AddRange(new object[] {
            "DO NOT REMOVE CHILDREN",
            "REMOVE CHILDREN"});
            this.comboBoxLNTLEAVEWITHCHILDREN.Location = new System.Drawing.Point(999, 204);
            this.comboBoxLNTLEAVEWITHCHILDREN.Name = "comboBoxLNTLEAVEWITHCHILDREN";
            this.comboBoxLNTLEAVEWITHCHILDREN.Size = new System.Drawing.Size(159, 21);
            this.comboBoxLNTLEAVEWITHCHILDREN.TabIndex = 181;
            // 
            // comboBoxLNTLEAVEREJOIN
            // 
            this.comboBoxLNTLEAVEREJOIN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLNTLEAVEREJOIN.FormattingEnabled = true;
            this.comboBoxLNTLEAVEREJOIN.Items.AddRange(new object[] {
            "DO NOT REJOIN",
            "REJOIN"});
            this.comboBoxLNTLEAVEREJOIN.Location = new System.Drawing.Point(872, 204);
            this.comboBoxLNTLEAVEREJOIN.Name = "comboBoxLNTLEAVEREJOIN";
            this.comboBoxLNTLEAVEREJOIN.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLNTLEAVEREJOIN.TabIndex = 180;
            // 
            // textBoxLNTWRITEATTRDATA
            // 
            this.textBoxLNTWRITEATTRDATA.Location = new System.Drawing.Point(1102, 116);
            this.textBoxLNTWRITEATTRDATA.Name = "textBoxLNTWRITEATTRDATA";
            this.textBoxLNTWRITEATTRDATA.Size = new System.Drawing.Size(137, 20);
            this.textBoxLNTWRITEATTRDATA.TabIndex = 179;
            this.textBoxLNTWRITEATTRDATA.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTWRITEATTRDATA_MouseClick);
            this.textBoxLNTWRITEATTRDATA.Leave += new System.EventHandler(this.textBoxLNTWRITEATTRDATA_Leave);
            this.textBoxLNTWRITEATTRDATA.MouseLeave += new System.EventHandler(this.textBoxLNTWRITEATTRDATA_MouseLeave);
            this.textBoxLNTWRITEATTRDATA.MouseHover += new System.EventHandler(this.textBoxLNTWRITEATTRDATA_MouseHover);
            // 
            // textBoxLNTWRITEATTRDATATYPE
            // 
            this.textBoxLNTWRITEATTRDATATYPE.Location = new System.Drawing.Point(1034, 115);
            this.textBoxLNTWRITEATTRDATATYPE.Name = "textBoxLNTWRITEATTRDATATYPE";
            this.textBoxLNTWRITEATTRDATATYPE.Size = new System.Drawing.Size(62, 20);
            this.textBoxLNTWRITEATTRDATATYPE.TabIndex = 178;
            this.textBoxLNTWRITEATTRDATATYPE.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTWRITEATTRDATATYPE_MouseClick);
            this.textBoxLNTWRITEATTRDATATYPE.Leave += new System.EventHandler(this.textBoxLNTWRITEATTRDATATYPE_Leave);
            this.textBoxLNTWRITEATTRDATATYPE.MouseLeave += new System.EventHandler(this.textBoxLNTWRITEATTRDATATYPE_MouseLeave);
            this.textBoxLNTWRITEATTRDATATYPE.MouseHover += new System.EventHandler(this.textBoxLNTWRITEATTRDATATYPE_MouseHover);
            // 
            // textBoxLNTWRITEATTRATTRID
            // 
            this.textBoxLNTWRITEATTRATTRID.Location = new System.Drawing.Point(959, 115);
            this.textBoxLNTWRITEATTRATTRID.Name = "textBoxLNTWRITEATTRATTRID";
            this.textBoxLNTWRITEATTRATTRID.Size = new System.Drawing.Size(69, 20);
            this.textBoxLNTWRITEATTRATTRID.TabIndex = 177;
            this.textBoxLNTWRITEATTRATTRID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTWRITEATTRATTRID_MouseClick);
            this.textBoxLNTWRITEATTRATTRID.Leave += new System.EventHandler(this.textBoxLNTWRITEATTRATTRID_Leave);
            this.textBoxLNTWRITEATTRATTRID.MouseLeave += new System.EventHandler(this.textBoxLNTWRITEATTRATTRID_MouseLeave);
            this.textBoxLNTWRITEATTRATTRID.MouseHover += new System.EventHandler(this.textBoxLNTWRITEATTRATTRID_MouseHover);
            // 
            // textBoxLNTWRITEATTRCLUSTERID
            // 
            this.textBoxLNTWRITEATTRCLUSTERID.Location = new System.Drawing.Point(890, 115);
            this.textBoxLNTWRITEATTRCLUSTERID.Name = "textBoxLNTWRITEATTRCLUSTERID";
            this.textBoxLNTWRITEATTRCLUSTERID.Size = new System.Drawing.Size(63, 20);
            this.textBoxLNTWRITEATTRCLUSTERID.TabIndex = 176;
            this.textBoxLNTWRITEATTRCLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTWRITEATTRCLUSTERID_MouseClick);
            this.textBoxLNTWRITEATTRCLUSTERID.Leave += new System.EventHandler(this.textBoxLNTWRITEATTRCLUSTERID_Leave);
            this.textBoxLNTWRITEATTRCLUSTERID.MouseLeave += new System.EventHandler(this.textBoxLNTWRITEATTRCLUSTERID_MouseLeave);
            this.textBoxLNTWRITEATTRCLUSTERID.MouseHover += new System.EventHandler(this.textBoxLNTWRITEATTRCLUSTERID_MouseHover);
            // 
            // textBoxLNTREADATTRATTRIBUTECOUNT
            // 
            this.textBoxLNTREADATTRATTRIBUTECOUNT.Location = new System.Drawing.Point(1034, 85);
            this.textBoxLNTREADATTRATTRIBUTECOUNT.Name = "textBoxLNTREADATTRATTRIBUTECOUNT";
            this.textBoxLNTREADATTRATTRIBUTECOUNT.Size = new System.Drawing.Size(62, 20);
            this.textBoxLNTREADATTRATTRIBUTECOUNT.TabIndex = 175;
            this.textBoxLNTREADATTRATTRIBUTECOUNT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTREADATTRATTRIBUTECOUNT_MouseClick);
            this.textBoxLNTREADATTRATTRIBUTECOUNT.Leave += new System.EventHandler(this.textBoxLNTREADATTRATTRIBUTECOUNT_Leave);
            this.textBoxLNTREADATTRATTRIBUTECOUNT.MouseLeave += new System.EventHandler(this.textBoxLNTREADATTRATTRIBUTECOUNT_MouseLeave);
            this.textBoxLNTREADATTRATTRIBUTECOUNT.MouseHover += new System.EventHandler(this.textBoxLNTREADATTRATTRIBUTECOUNT_MouseHover);
            // 
            // textBoxLNTREADATTRATTRIBUTEID
            // 
            this.textBoxLNTREADATTRATTRIBUTEID.Location = new System.Drawing.Point(959, 85);
            this.textBoxLNTREADATTRATTRIBUTEID.Name = "textBoxLNTREADATTRATTRIBUTEID";
            this.textBoxLNTREADATTRATTRIBUTEID.Size = new System.Drawing.Size(69, 20);
            this.textBoxLNTREADATTRATTRIBUTEID.TabIndex = 174;
            this.textBoxLNTREADATTRATTRIBUTEID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTREADATTRATTRIBUTEID_MouseClick);
            this.textBoxLNTREADATTRATTRIBUTEID.Leave += new System.EventHandler(this.textBoxLNTREADATTRATTRIBUTEID_Leave);
            this.textBoxLNTREADATTRATTRIBUTEID.MouseLeave += new System.EventHandler(this.textBoxLNTREADATTRATTRIBUTEID_MouseLeave);
            this.textBoxLNTREADATTRATTRIBUTEID.MouseHover += new System.EventHandler(this.textBoxLNTREADATTRATTRIBUTEID_MouseHover);
            // 
            // textBoxLNTREADATTRCLUSTERID
            // 
            this.textBoxLNTREADATTRCLUSTERID.Location = new System.Drawing.Point(890, 85);
            this.textBoxLNTREADATTRCLUSTERID.Name = "textBoxLNTREADATTRCLUSTERID";
            this.textBoxLNTREADATTRCLUSTERID.Size = new System.Drawing.Size(63, 20);
            this.textBoxLNTREADATTRCLUSTERID.TabIndex = 173;
            this.textBoxLNTREADATTRCLUSTERID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTREADATTRCLUSTERID_MouseClick);
            this.textBoxLNTREADATTRCLUSTERID.Leave += new System.EventHandler(this.textBoxLNTREADATTRCLUSTERID_Leave);
            this.textBoxLNTREADATTRCLUSTERID.MouseLeave += new System.EventHandler(this.textBoxLNTREADATTRCLUSTERID_MouseLeave);
            this.textBoxLNTREADATTRCLUSTERID.MouseHover += new System.EventHandler(this.textBoxLNTREADATTRCLUSTERID_MouseHover);
            // 
            // buttonLNTTEMP
            // 
            this.buttonLNTTEMP.Location = new System.Drawing.Point(790, 465);
            this.buttonLNTTEMP.Name = "buttonLNTTEMP";
            this.buttonLNTTEMP.Size = new System.Drawing.Size(90, 22);
            this.buttonLNTTEMP.TabIndex = 172;
            this.buttonLNTTEMP.Text = "MoveToTemp";
            this.buttonLNTTEMP.UseVisualStyleBackColor = true;
            this.buttonLNTTEMP.Click += new System.EventHandler(this.buttonLNTTEMP_Click);
            // 
            // buttonLNTSAT
            // 
            this.buttonLNTSAT.Location = new System.Drawing.Point(790, 437);
            this.buttonLNTSAT.Name = "buttonLNTSAT";
            this.buttonLNTSAT.Size = new System.Drawing.Size(90, 22);
            this.buttonLNTSAT.TabIndex = 171;
            this.buttonLNTSAT.Text = "MoveToSat";
            this.buttonLNTSAT.UseVisualStyleBackColor = true;
            this.buttonLNTSAT.Click += new System.EventHandler(this.buttonLNTSAT_Click);
            // 
            // buttonLNTCOLOR
            // 
            this.buttonLNTCOLOR.Location = new System.Drawing.Point(790, 409);
            this.buttonLNTCOLOR.Name = "buttonLNTCOLOR";
            this.buttonLNTCOLOR.Size = new System.Drawing.Size(90, 22);
            this.buttonLNTCOLOR.TabIndex = 170;
            this.buttonLNTCOLOR.Text = "MoveToColor";
            this.buttonLNTCOLOR.UseVisualStyleBackColor = true;
            this.buttonLNTCOLOR.Click += new System.EventHandler(this.buttonLNTCOLOR_Click);
            // 
            // buttonLNTHUE
            // 
            this.buttonLNTHUE.Location = new System.Drawing.Point(791, 381);
            this.buttonLNTHUE.Name = "buttonLNTHUE";
            this.buttonLNTHUE.Size = new System.Drawing.Size(90, 22);
            this.buttonLNTHUE.TabIndex = 169;
            this.buttonLNTHUE.Text = "MoveToHue";
            this.buttonLNTHUE.UseVisualStyleBackColor = true;
            this.buttonLNTHUE.Click += new System.EventHandler(this.buttonLNTHUE_Click);
            // 
            // buttonLNTLEVEL
            // 
            this.buttonLNTLEVEL.Location = new System.Drawing.Point(791, 352);
            this.buttonLNTLEVEL.Name = "buttonLNTLEVEL";
            this.buttonLNTLEVEL.Size = new System.Drawing.Size(93, 23);
            this.buttonLNTLEVEL.TabIndex = 168;
            this.buttonLNTLEVEL.Text = "Move to level";
            this.buttonLNTLEVEL.UseVisualStyleBackColor = true;
            this.buttonLNTLEVEL.Click += new System.EventHandler(this.buttonLNTLEVEL_Click);
            // 
            // buttonLNTIDENTIFY
            // 
            this.buttonLNTIDENTIFY.Location = new System.Drawing.Point(791, 291);
            this.buttonLNTIDENTIFY.Name = "buttonLNTIDENTIFY";
            this.buttonLNTIDENTIFY.Size = new System.Drawing.Size(93, 23);
            this.buttonLNTIDENTIFY.TabIndex = 167;
            this.buttonLNTIDENTIFY.Text = "Identify Send";
            this.buttonLNTIDENTIFY.UseVisualStyleBackColor = true;
            this.buttonLNTIDENTIFY.Click += new System.EventHandler(this.buttonLNTIDENTIFY_Click);
            // 
            // buttonLNTRESET
            // 
            this.buttonLNTRESET.Location = new System.Drawing.Point(791, 52);
            this.buttonLNTRESET.Name = "buttonLNTRESET";
            this.buttonLNTRESET.Size = new System.Drawing.Size(93, 25);
            this.buttonLNTRESET.TabIndex = 166;
            this.buttonLNTRESET.Text = "Reset To FD";
            this.buttonLNTRESET.UseVisualStyleBackColor = true;
            this.buttonLNTRESET.Click += new System.EventHandler(this.buttonLNTRESET_Click);
            // 
            // buttonLNTREADRPRT
            // 
            this.buttonLNTREADRPRT.Location = new System.Drawing.Point(791, 261);
            this.buttonLNTREADRPRT.Name = "buttonLNTREADRPRT";
            this.buttonLNTREADRPRT.Size = new System.Drawing.Size(80, 24);
            this.buttonLNTREADRPRT.TabIndex = 165;
            this.buttonLNTREADRPRT.Text = "Read Rprt";
            this.buttonLNTREADRPRT.UseVisualStyleBackColor = true;
            this.buttonLNTREADRPRT.Click += new System.EventHandler(this.buttonLNTREADRPRT_Click);
            // 
            // buttonLNTCONFIGRPRT
            // 
            this.buttonLNTCONFIGRPRT.Location = new System.Drawing.Point(791, 231);
            this.buttonLNTCONFIGRPRT.Name = "buttonLNTCONFIGRPRT";
            this.buttonLNTCONFIGRPRT.Size = new System.Drawing.Size(80, 24);
            this.buttonLNTCONFIGRPRT.TabIndex = 164;
            this.buttonLNTCONFIGRPRT.Text = "Config Rprt";
            this.buttonLNTCONFIGRPRT.UseVisualStyleBackColor = true;
            this.buttonLNTCONFIGRPRT.Click += new System.EventHandler(this.buttonLNTCONFIGRPRT_Click);
            // 
            // buttonLNTLEAVE
            // 
            this.buttonLNTLEAVE.Location = new System.Drawing.Point(791, 202);
            this.buttonLNTLEAVE.Name = "buttonLNTLEAVE";
            this.buttonLNTLEAVE.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTLEAVE.TabIndex = 163;
            this.buttonLNTLEAVE.Text = "Leave";
            this.buttonLNTLEAVE.UseVisualStyleBackColor = true;
            this.buttonLNTLEAVE.Click += new System.EventHandler(this.buttonLNTLEAVE_Click);
            // 
            // buttonLNTWRITEATTRIBUTE
            // 
            this.buttonLNTWRITEATTRIBUTE.Location = new System.Drawing.Point(791, 113);
            this.buttonLNTWRITEATTRIBUTE.Name = "buttonLNTWRITEATTRIBUTE";
            this.buttonLNTWRITEATTRIBUTE.Size = new System.Drawing.Size(92, 23);
            this.buttonLNTWRITEATTRIBUTE.TabIndex = 162;
            this.buttonLNTWRITEATTRIBUTE.Text = "Write Attribute";
            this.buttonLNTWRITEATTRIBUTE.UseVisualStyleBackColor = true;
            this.buttonLNTWRITEATTRIBUTE.Click += new System.EventHandler(this.buttonLNTWRITEATTRIBUTE_Click);
            // 
            // textBoxLNTUNBINDIEEEADDR
            // 
            this.textBoxLNTUNBINDIEEEADDR.Location = new System.Drawing.Point(1058, 175);
            this.textBoxLNTUNBINDIEEEADDR.Name = "textBoxLNTUNBINDIEEEADDR";
            this.textBoxLNTUNBINDIEEEADDR.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTUNBINDIEEEADDR.TabIndex = 161;
            this.textBoxLNTUNBINDIEEEADDR.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTUNBINDIEEEADDR_MouseClick);
            this.textBoxLNTUNBINDIEEEADDR.Leave += new System.EventHandler(this.textBoxLNTUNBINDIEEEADDR_Leave);
            this.textBoxLNTUNBINDIEEEADDR.MouseLeave += new System.EventHandler(this.textBoxLNTUNBINDIEEEADDR_MouseLeave);
            this.textBoxLNTUNBINDIEEEADDR.MouseHover += new System.EventHandler(this.textBoxLNTUNBINDIEEEADDR_MouseHover);
            // 
            // textBoxLNTBINDIEEEADDR
            // 
            this.textBoxLNTBINDIEEEADDR.Location = new System.Drawing.Point(871, 174);
            this.textBoxLNTBINDIEEEADDR.Name = "textBoxLNTBINDIEEEADDR";
            this.textBoxLNTBINDIEEEADDR.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTBINDIEEEADDR.TabIndex = 160;
            this.textBoxLNTBINDIEEEADDR.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTBINDIEEEADDR_MouseClick);
            this.textBoxLNTBINDIEEEADDR.Leave += new System.EventHandler(this.textBoxLNTBINDIEEEADDR_Leave);
            this.textBoxLNTBINDIEEEADDR.MouseLeave += new System.EventHandler(this.textBoxLNTBINDIEEEADDR_MouseLeave);
            this.textBoxLNTBINDIEEEADDR.MouseHover += new System.EventHandler(this.textBoxLNTBINDIEEEADDR_MouseHover);
            // 
            // buttonLNTUNBIND
            // 
            this.buttonLNTUNBIND.Location = new System.Drawing.Point(977, 173);
            this.buttonLNTUNBIND.Name = "buttonLNTUNBIND";
            this.buttonLNTUNBIND.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTUNBIND.TabIndex = 159;
            this.buttonLNTUNBIND.Text = "Unbind";
            this.buttonLNTUNBIND.UseVisualStyleBackColor = true;
            this.buttonLNTUNBIND.Click += new System.EventHandler(this.buttonLNTUNBIND_Click);
            // 
            // buttonLNTBIND
            // 
            this.buttonLNTBIND.Location = new System.Drawing.Point(790, 172);
            this.buttonLNTBIND.Name = "buttonLNTBIND";
            this.buttonLNTBIND.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTBIND.TabIndex = 158;
            this.buttonLNTBIND.Text = "Bind";
            this.buttonLNTBIND.UseVisualStyleBackColor = true;
            this.buttonLNTBIND.Click += new System.EventHandler(this.buttonLNTBIND_Click);
            // 
            // buttonLNTSTOPTONGGLE
            // 
            this.buttonLNTSTOPTONGGLE.Location = new System.Drawing.Point(1034, 143);
            this.buttonLNTSTOPTONGGLE.Name = "buttonLNTSTOPTONGGLE";
            this.buttonLNTSTOPTONGGLE.Size = new System.Drawing.Size(137, 23);
            this.buttonLNTSTOPTONGGLE.TabIndex = 157;
            this.buttonLNTSTOPTONGGLE.Text = "Stop Tonggle Loop";
            this.buttonLNTSTOPTONGGLE.UseVisualStyleBackColor = true;
            this.buttonLNTSTOPTONGGLE.Click += new System.EventHandler(this.buttonLNTSTOPTONGGLE_Click);
            // 
            // buttonLNTTONGGLE
            // 
            this.buttonLNTTONGGLE.Location = new System.Drawing.Point(953, 143);
            this.buttonLNTTONGGLE.Name = "buttonLNTTONGGLE";
            this.buttonLNTTONGGLE.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTTONGGLE.TabIndex = 156;
            this.buttonLNTTONGGLE.Text = "Tonggle";
            this.buttonLNTTONGGLE.UseVisualStyleBackColor = true;
            this.buttonLNTTONGGLE.Click += new System.EventHandler(this.buttonLNTTONGGLE_Click);
            // 
            // buttonLNTOFF
            // 
            this.buttonLNTOFF.Location = new System.Drawing.Point(872, 143);
            this.buttonLNTOFF.Name = "buttonLNTOFF";
            this.buttonLNTOFF.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTOFF.TabIndex = 155;
            this.buttonLNTOFF.Text = "Off";
            this.buttonLNTOFF.UseVisualStyleBackColor = true;
            this.buttonLNTOFF.Click += new System.EventHandler(this.buttonLNTOFF_Click);
            // 
            // buttonLNTON
            // 
            this.buttonLNTON.Location = new System.Drawing.Point(791, 143);
            this.buttonLNTON.Name = "buttonLNTON";
            this.buttonLNTON.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTON.TabIndex = 154;
            this.buttonLNTON.Text = "On";
            this.buttonLNTON.UseVisualStyleBackColor = true;
            this.buttonLNTON.Click += new System.EventHandler(this.buttonLNTON_Click);
            // 
            // textBoxLNTSETLOOP
            // 
            this.textBoxLNTSETLOOP.Location = new System.Drawing.Point(887, 26);
            this.textBoxLNTSETLOOP.Name = "textBoxLNTSETLOOP";
            this.textBoxLNTSETLOOP.Size = new System.Drawing.Size(66, 20);
            this.textBoxLNTSETLOOP.TabIndex = 153;
            this.textBoxLNTSETLOOP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTSETLOOP_MouseClick);
            this.textBoxLNTSETLOOP.Leave += new System.EventHandler(this.textBoxLNTSETLOOP_Leave);
            this.textBoxLNTSETLOOP.MouseLeave += new System.EventHandler(this.textBoxLNTSETLOOP_MouseLeave);
            this.textBoxLNTSETLOOP.MouseHover += new System.EventHandler(this.textBoxLNTSETLOOP_MouseHover);
            // 
            // buttonLNTSETPARA
            // 
            this.buttonLNTSETPARA.Location = new System.Drawing.Point(791, 24);
            this.buttonLNTSETPARA.Name = "buttonLNTSETPARA";
            this.buttonLNTSETPARA.Size = new System.Drawing.Size(92, 23);
            this.buttonLNTSETPARA.TabIndex = 152;
            this.buttonLNTSETPARA.Text = "Set  Parameter";
            this.buttonLNTSETPARA.UseVisualStyleBackColor = true;
            this.buttonLNTSETPARA.Click += new System.EventHandler(this.buttonLNTSETPARA_Click);
            // 
            // buttonLNTSTOPTEADATTRIBUTE
            // 
            this.buttonLNTSTOPTEADATTRIBUTE.Location = new System.Drawing.Point(1102, 83);
            this.buttonLNTSTOPTEADATTRIBUTE.Name = "buttonLNTSTOPTEADATTRIBUTE";
            this.buttonLNTSTOPTEADATTRIBUTE.Size = new System.Drawing.Size(115, 23);
            this.buttonLNTSTOPTEADATTRIBUTE.TabIndex = 151;
            this.buttonLNTSTOPTEADATTRIBUTE.Text = "Stop Read Loop";
            this.buttonLNTSTOPTEADATTRIBUTE.UseVisualStyleBackColor = true;
            this.buttonLNTSTOPTEADATTRIBUTE.Click += new System.EventHandler(this.buttonLNTSTOPTEADATTRIBUTE_Click);
            // 
            // textBoxLNTSETPARAMININTERVAL
            // 
            this.textBoxLNTSETPARAMININTERVAL.Location = new System.Drawing.Point(959, 26);
            this.textBoxLNTSETPARAMININTERVAL.Name = "textBoxLNTSETPARAMININTERVAL";
            this.textBoxLNTSETPARAMININTERVAL.Size = new System.Drawing.Size(74, 20);
            this.textBoxLNTSETPARAMININTERVAL.TabIndex = 150;
            this.textBoxLNTSETPARAMININTERVAL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTSETPARAMININTERVAL_MouseClick);
            this.textBoxLNTSETPARAMININTERVAL.Leave += new System.EventHandler(this.textBoxLNTSETPARAMININTERVAL_Leave);
            this.textBoxLNTSETPARAMININTERVAL.MouseLeave += new System.EventHandler(this.textBoxLNTSETPARAMININTERVAL_MouseLeave);
            this.textBoxLNTSETPARAMININTERVAL.MouseHover += new System.EventHandler(this.textBoxLNTSETPARAMININTERVAL_MouseHover);
            // 
            // buttonLNTREADATTRIBUTE
            // 
            this.buttonLNTREADATTRIBUTE.Location = new System.Drawing.Point(791, 83);
            this.buttonLNTREADATTRIBUTE.Name = "buttonLNTREADATTRIBUTE";
            this.buttonLNTREADATTRIBUTE.Size = new System.Drawing.Size(92, 23);
            this.buttonLNTREADATTRIBUTE.TabIndex = 149;
            this.buttonLNTREADATTRIBUTE.Text = "Read Attribute";
            this.buttonLNTREADATTRIBUTE.UseVisualStyleBackColor = true;
            this.buttonLNTREADATTRIBUTE.Click += new System.EventHandler(this.buttonLNTREADATTRIBUTE_Click);
            // 
            // textBoxLNTVIEWGROUPADDRESS
            // 
            this.textBoxLNTVIEWGROUPADDRESS.Location = new System.Drawing.Point(1235, 323);
            this.textBoxLNTVIEWGROUPADDRESS.Name = "textBoxLNTVIEWGROUPADDRESS";
            this.textBoxLNTVIEWGROUPADDRESS.Size = new System.Drawing.Size(79, 20);
            this.textBoxLNTVIEWGROUPADDRESS.TabIndex = 148;
            this.textBoxLNTVIEWGROUPADDRESS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTVIEWGROUPADDRESS_MouseClick);
            this.textBoxLNTVIEWGROUPADDRESS.Leave += new System.EventHandler(this.textBoxLNTVIEWGROUPADDRESS_Leave);
            this.textBoxLNTVIEWGROUPADDRESS.MouseLeave += new System.EventHandler(this.textBoxLNTVIEWGROUPADDRESS_MouseLeave);
            this.textBoxLNTVIEWGROUPADDRESS.MouseHover += new System.EventHandler(this.textBoxLNTVIEWGROUPADDRESS_MouseHover);
            // 
            // buttonLNTREMOVEALL
            // 
            this.buttonLNTREMOVEALL.Location = new System.Drawing.Point(1320, 321);
            this.buttonLNTREMOVEALL.Name = "buttonLNTREMOVEALL";
            this.buttonLNTREMOVEALL.Size = new System.Drawing.Size(109, 23);
            this.buttonLNTREMOVEALL.TabIndex = 147;
            this.buttonLNTREMOVEALL.Text = "Remove Group All";
            this.buttonLNTREMOVEALL.UseVisualStyleBackColor = true;
            this.buttonLNTREMOVEALL.Click += new System.EventHandler(this.buttonLNTREMOVEALL_Click);
            // 
            // buttonLNTVIEWGROUP
            // 
            this.buttonLNTVIEWGROUP.Location = new System.Drawing.Point(1154, 321);
            this.buttonLNTVIEWGROUP.Name = "buttonLNTVIEWGROUP";
            this.buttonLNTVIEWGROUP.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTVIEWGROUP.TabIndex = 146;
            this.buttonLNTVIEWGROUP.Text = "View Group";
            this.buttonLNTVIEWGROUP.UseVisualStyleBackColor = true;
            this.buttonLNTVIEWGROUP.Click += new System.EventHandler(this.buttonLNTVIEWGROUP_Click);
            // 
            // textBoxLNTREMOVEGROUPADDRESS
            // 
            this.textBoxLNTREMOVEGROUPADDRESS.Location = new System.Drawing.Point(1074, 323);
            this.textBoxLNTREMOVEGROUPADDRESS.Name = "textBoxLNTREMOVEGROUPADDRESS";
            this.textBoxLNTREMOVEGROUPADDRESS.Size = new System.Drawing.Size(74, 20);
            this.textBoxLNTREMOVEGROUPADDRESS.TabIndex = 145;
            this.textBoxLNTREMOVEGROUPADDRESS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTREMOVEGROUPADDRESS_MouseClick);
            this.textBoxLNTREMOVEGROUPADDRESS.Leave += new System.EventHandler(this.textBoxLNTREMOVEGROUPADDRESS_Leave);
            this.textBoxLNTREMOVEGROUPADDRESS.MouseLeave += new System.EventHandler(this.textBoxLNTREMOVEGROUPADDRESS_MouseLeave);
            this.textBoxLNTREMOVEGROUPADDRESS.MouseHover += new System.EventHandler(this.textBoxLNTREMOVEGROUPADDRESS_MouseHover);
            // 
            // textBoxLNTADDGROUPADDR
            // 
            this.textBoxLNTADDGROUPADDR.Location = new System.Drawing.Point(872, 320);
            this.textBoxLNTADDGROUPADDR.Name = "textBoxLNTADDGROUPADDR";
            this.textBoxLNTADDGROUPADDR.Size = new System.Drawing.Size(79, 20);
            this.textBoxLNTADDGROUPADDR.TabIndex = 144;
            this.textBoxLNTADDGROUPADDR.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLNTADDGROUPADDR_MouseClick);
            this.textBoxLNTADDGROUPADDR.Leave += new System.EventHandler(this.textBoxLNTADDGROUPADDR_Leave);
            this.textBoxLNTADDGROUPADDR.MouseLeave += new System.EventHandler(this.textBoxLNTADDGROUPADDR_MouseLeave);
            this.textBoxLNTADDGROUPADDR.MouseHover += new System.EventHandler(this.textBoxLNTADDGROUPADDR_MouseHover);
            // 
            // buttonLNTREMOVEGROUP
            // 
            this.buttonLNTREMOVEGROUP.Location = new System.Drawing.Point(957, 320);
            this.buttonLNTREMOVEGROUP.Name = "buttonLNTREMOVEGROUP";
            this.buttonLNTREMOVEGROUP.Size = new System.Drawing.Size(109, 23);
            this.buttonLNTREMOVEGROUP.TabIndex = 143;
            this.buttonLNTREMOVEGROUP.Text = "Remove Group";
            this.buttonLNTREMOVEGROUP.UseVisualStyleBackColor = true;
            this.buttonLNTREMOVEGROUP.Click += new System.EventHandler(this.buttonLNTREMOVEGROUP_Click);
            // 
            // buttonLNTADDGROUP
            // 
            this.buttonLNTADDGROUP.Location = new System.Drawing.Point(791, 320);
            this.buttonLNTADDGROUP.Name = "buttonLNTADDGROUP";
            this.buttonLNTADDGROUP.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTADDGROUP.TabIndex = 142;
            this.buttonLNTADDGROUP.Text = "Add Group";
            this.buttonLNTADDGROUP.UseVisualStyleBackColor = true;
            this.buttonLNTADDGROUP.Click += new System.EventHandler(this.buttonLNTADDGROUP_Click);
            // 
            // textBoxLNTSENDCOMMAND
            // 
            this.textBoxLNTSENDCOMMAND.Location = new System.Drawing.Point(175, 52);
            this.textBoxLNTSENDCOMMAND.Name = "textBoxLNTSENDCOMMAND";
            this.textBoxLNTSENDCOMMAND.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTSENDCOMMAND.TabIndex = 45;
            // 
            // buttonLNTSENDCOMMAND
            // 
            this.buttonLNTSENDCOMMAND.Location = new System.Drawing.Point(76, 52);
            this.buttonLNTSENDCOMMAND.Name = "buttonLNTSENDCOMMAND";
            this.buttonLNTSENDCOMMAND.Size = new System.Drawing.Size(93, 23);
            this.buttonLNTSENDCOMMAND.TabIndex = 44;
            this.buttonLNTSENDCOMMAND.Text = "Send Command";
            this.buttonLNTSENDCOMMAND.UseVisualStyleBackColor = true;
            this.buttonLNTSENDCOMMAND.Click += new System.EventHandler(this.buttonLNTSENDCOMMAND_Click);
            // 
            // checkBoxLNTALL
            // 
            this.checkBoxLNTALL.AutoSize = true;
            this.checkBoxLNTALL.Location = new System.Drawing.Point(15, 53);
            this.checkBoxLNTALL.Name = "checkBoxLNTALL";
            this.checkBoxLNTALL.Size = new System.Drawing.Size(45, 17);
            this.checkBoxLNTALL.TabIndex = 40;
            this.checkBoxLNTALL.Text = "ALL";
            this.checkBoxLNTALL.UseVisualStyleBackColor = true;
            this.checkBoxLNTALL.CheckedChanged += new System.EventHandler(this.checkBoxLNTALL_CheckedChanged);
            // 
            // listViewLNTGROUPINFO
            // 
            this.listViewLNTGROUPINFO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewLNTGROUPINFO.CheckBoxes = true;
            this.listViewLNTGROUPINFO.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.listViewLNTGROUPINFO.HideSelection = false;
            this.listViewLNTGROUPINFO.Location = new System.Drawing.Point(517, 83);
            this.listViewLNTGROUPINFO.Name = "listViewLNTGROUPINFO";
            this.listViewLNTGROUPINFO.Size = new System.Drawing.Size(252, 474);
            this.listViewLNTGROUPINFO.TabIndex = 39;
            this.listViewLNTGROUPINFO.UseCompatibleStateImageBehavior = false;
            this.listViewLNTGROUPINFO.View = System.Windows.Forms.View.Details;
            this.listViewLNTGROUPINFO.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewLNTGROUPINFO_ItemChecked);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "nwkAddrJoined";
            this.columnHeader7.Width = 88;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Status";
            this.columnHeader8.Width = 145;
            // 
            // listViewLNTCOMINFO
            // 
            this.listViewLNTCOMINFO.AllowDrop = true;
            this.listViewLNTCOMINFO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewLNTCOMINFO.CheckBoxes = true;
            this.listViewLNTCOMINFO.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewLNTCOMINFO.HideSelection = false;
            this.listViewLNTCOMINFO.Location = new System.Drawing.Point(15, 85);
            this.listViewLNTCOMINFO.Name = "listViewLNTCOMINFO";
            this.listViewLNTCOMINFO.Size = new System.Drawing.Size(496, 472);
            this.listViewLNTCOMINFO.TabIndex = 38;
            this.listViewLNTCOMINFO.UseCompatibleStateImageBehavior = false;
            this.listViewLNTCOMINFO.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "COMIndex";
            this.columnHeader1.Width = 86;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "NwkAddr";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 67;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "IEEEMAC";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 127;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Channel";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 62;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 98;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Ver";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 57;
            // 
            // buttonLNTREMOTELOAD
            // 
            this.buttonLNTREMOTELOAD.Location = new System.Drawing.Point(76, 20);
            this.buttonLNTREMOTELOAD.Name = "buttonLNTREMOTELOAD";
            this.buttonLNTREMOTELOAD.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTREMOTELOAD.TabIndex = 37;
            this.buttonLNTREMOTELOAD.Text = "Load Script";
            this.buttonLNTREMOTELOAD.UseVisualStyleBackColor = true;
            this.buttonLNTREMOTELOAD.Click += new System.EventHandler(this.buttonLNTREMOTELOAD_Click);
            // 
            // tabPage18
            // 
            this.tabPage18.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage18.Controls.Add(this.buttonLNTGWSTOPONOFFLOOP);
            this.tabPage18.Controls.Add(this.buttonLNTGWONOFFLOOP);
            this.tabPage18.Controls.Add(this.labelLNTGWLOOPREMAIN);
            this.tabPage18.Controls.Add(this.textBoxLNTGWLOOPREMAIN);
            this.tabPage18.Controls.Add(this.labelLNTGWUNICAST);
            this.tabPage18.Controls.Add(this.labelLNTGWBROADCAST);
            this.tabPage18.Controls.Add(this.buttonLNTGWBROADSTOPTONGGLE);
            this.tabPage18.Controls.Add(this.buttonLNTGWBROADTONGGLE);
            this.tabPage18.Controls.Add(this.buttonLNTGWBROADOFF);
            this.tabPage18.Controls.Add(this.buttonLNTGWBROADON);
            this.tabPage18.Controls.Add(this.comboBoxLNTGWUNICAST);
            this.tabPage18.Controls.Add(this.textBoxLNTGWSENDCMD);
            this.tabPage18.Controls.Add(this.buttonLNTGWSENDCMD);
            this.tabPage18.Controls.Add(this.listViewLNTGWINFO);
            this.tabPage18.Controls.Add(this.buttonLNTGWDBGPORT);
            this.tabPage18.Controls.Add(this.buttonLNTFGWDISPERMIT);
            this.tabPage18.Controls.Add(this.buttonLNTGWPERMMIT);
            this.tabPage18.Controls.Add(this.checkBoxLNTGWALL);
            this.tabPage18.Controls.Add(this.textBoxLNTGWSETINTERVALMAX);
            this.tabPage18.Controls.Add(this.buttonLNTGWSTOPMOVETEMP);
            this.tabPage18.Controls.Add(this.buttonLNTGWSTOPMOVESAT);
            this.tabPage18.Controls.Add(this.buttonLNTGWSTOPMOVECOLOR);
            this.tabPage18.Controls.Add(this.buttonLNTGWSTOPMOVEHUE);
            this.tabPage18.Controls.Add(this.textBoxLNTGWSETDIR);
            this.tabPage18.Controls.Add(this.textBoxLNTGWSETSTEP);
            this.tabPage18.Controls.Add(this.comboBoxLNTGWLEVELWITHONOFF);
            this.tabPage18.Controls.Add(this.buttonLNTGWSTOPMOVELEVEL);
            this.tabPage18.Controls.Add(this.buttonLNTGWSTOPIDENTIFY);
            this.tabPage18.Controls.Add(this.textBoxLNTGWTEMPTIME);
            this.tabPage18.Controls.Add(this.textBoxLNTGWTEMP);
            this.tabPage18.Controls.Add(this.textBoxLNTGWSATTIME);
            this.tabPage18.Controls.Add(this.textBoxLNTGWSAT);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCOLORTIME);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCOLORY);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCOLORX);
            this.tabPage18.Controls.Add(this.textBoxLNTGWHUETIME);
            this.tabPage18.Controls.Add(this.textBoxLNTGWHUEDIR);
            this.tabPage18.Controls.Add(this.textBoxLNTGWHUE);
            this.tabPage18.Controls.Add(this.textBoxLNTGWLEVELTIME);
            this.tabPage18.Controls.Add(this.textBoxLNTGWLEVEL);
            this.tabPage18.Controls.Add(this.textBoxLNTGWIDENTIFYTIME);
            this.tabPage18.Controls.Add(this.textBoxLNTGWREADRPRTATTRIBUTEID);
            this.tabPage18.Controls.Add(this.textBoxLNTGWREADRPRTCLUSTERID);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCONFIGRPRTCHANGE);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCONFIGRPRTTIMEOUT);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCONFIGRPRTMAXINTERVAL);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCONFIGRPRTMININTERVAL);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCONFIGRPRTTYPE);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCONFIGRPRTATTRIBID);
            this.tabPage18.Controls.Add(this.textBoxLNTGWCONFIGRPRTCLUSTERID);
            this.tabPage18.Controls.Add(this.comboBoxLNTGWLEAVECHILDREN);
            this.tabPage18.Controls.Add(this.comboBoxLNTGWLEAVEREJOIN);
            this.tabPage18.Controls.Add(this.textBoxLNTGWWRITEATTRIBUTEDATA);
            this.tabPage18.Controls.Add(this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEDATATYPE);
            this.tabPage18.Controls.Add(this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEID);
            this.tabPage18.Controls.Add(this.textBoxLNTGWWRITEATTRIBUTECLUSTERID);
            this.tabPage18.Controls.Add(this.textBoxLNTGWATTRIBUTEID);
            this.tabPage18.Controls.Add(this.textBoxLNTGWREADCLUSTERID);
            this.tabPage18.Controls.Add(this.buttonLNTGWMOVETEMP);
            this.tabPage18.Controls.Add(this.buttonLNTGWMOVESAT);
            this.tabPage18.Controls.Add(this.buttonLNTGWMOVECOLOR);
            this.tabPage18.Controls.Add(this.buttonLNTGWMOVEHUE);
            this.tabPage18.Controls.Add(this.buttonLNTGWMOVELEVEL);
            this.tabPage18.Controls.Add(this.buttonLNTGWIDENTIFY);
            this.tabPage18.Controls.Add(this.buttonLNTGWRESET);
            this.tabPage18.Controls.Add(this.buttonLNTGWREADRPRT);
            this.tabPage18.Controls.Add(this.buttonLNTGWCONFIGRPRT);
            this.tabPage18.Controls.Add(this.buttonLNTGWLEAVE);
            this.tabPage18.Controls.Add(this.buttonLNTGWWRITE);
            this.tabPage18.Controls.Add(this.textBoxLNTGWUNBINDCLUSTERID);
            this.tabPage18.Controls.Add(this.textBoxLNTGWBINDCLUSTERID);
            this.tabPage18.Controls.Add(this.buttonLNTGWUNBIND);
            this.tabPage18.Controls.Add(this.buttonLNTGWBIND);
            this.tabPage18.Controls.Add(this.buttonLNTGWSTOPTONGGLE);
            this.tabPage18.Controls.Add(this.buttonLNTGWTONGGLE);
            this.tabPage18.Controls.Add(this.buttonLNTGWOFF);
            this.tabPage18.Controls.Add(this.buttonLNTGWON);
            this.tabPage18.Controls.Add(this.textBoxLNTGWSETLOOP);
            this.tabPage18.Controls.Add(this.buttonLNTGWSET);
            this.tabPage18.Controls.Add(this.buttonLNTGWSTOPREAD);
            this.tabPage18.Controls.Add(this.textBoxLNTGWTIMERINTERVAL);
            this.tabPage18.Controls.Add(this.buttonLNTGWREAD);
            this.tabPage18.Controls.Add(this.textBoxLNTGWVIEW);
            this.tabPage18.Controls.Add(this.buttonLNTGWREMOVEGROUPALL);
            this.tabPage18.Controls.Add(this.buttonLNTGWVIEWGROUP);
            this.tabPage18.Controls.Add(this.textBoxLNTGWREMOVEGROUP);
            this.tabPage18.Controls.Add(this.textBoxLNTGWADDGROUP);
            this.tabPage18.Controls.Add(this.buttonLNTGWREMOVEGROUP);
            this.tabPage18.Controls.Add(this.buttonLNTGWADDGROUP);
            this.tabPage18.Controls.Add(this.listViewLNTGWGROUPINFO);
            this.tabPage18.Location = new System.Drawing.Point(4, 22);
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage18.Size = new System.Drawing.Size(1833, 584);
            this.tabPage18.TabIndex = 22;
            this.tabPage18.Text = "LNT GW";
            // 
            // buttonLNTGWSTOPONOFFLOOP
            // 
            this.buttonLNTGWSTOPONOFFLOOP.Location = new System.Drawing.Point(1398, 179);
            this.buttonLNTGWSTOPONOFFLOOP.Name = "buttonLNTGWSTOPONOFFLOOP";
            this.buttonLNTGWSTOPONOFFLOOP.Size = new System.Drawing.Size(80, 23);
            this.buttonLNTGWSTOPONOFFLOOP.TabIndex = 318;
            this.buttonLNTGWSTOPONOFFLOOP.Text = "Stop On/Off";
            this.buttonLNTGWSTOPONOFFLOOP.UseVisualStyleBackColor = true;
            this.buttonLNTGWSTOPONOFFLOOP.Click += new System.EventHandler(this.buttonLNTGWSTOPONOFFLOOP_Click);
            // 
            // buttonLNTGWONOFFLOOP
            // 
            this.buttonLNTGWONOFFLOOP.Location = new System.Drawing.Point(1317, 179);
            this.buttonLNTGWONOFFLOOP.Name = "buttonLNTGWONOFFLOOP";
            this.buttonLNTGWONOFFLOOP.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWONOFFLOOP.TabIndex = 317;
            this.buttonLNTGWONOFFLOOP.Text = "On/Off";
            this.buttonLNTGWONOFFLOOP.UseVisualStyleBackColor = true;
            this.buttonLNTGWONOFFLOOP.Click += new System.EventHandler(this.buttonLNTGWONOFFLOOP_Click);
            // 
            // labelLNTGWLOOPREMAIN
            // 
            this.labelLNTGWLOOPREMAIN.AutoSize = true;
            this.labelLNTGWLOOPREMAIN.Location = new System.Drawing.Point(1088, 36);
            this.labelLNTGWLOOPREMAIN.Name = "labelLNTGWLOOPREMAIN";
            this.labelLNTGWLOOPREMAIN.Size = new System.Drawing.Size(65, 13);
            this.labelLNTGWLOOPREMAIN.TabIndex = 312;
            this.labelLNTGWLOOPREMAIN.Text = "Loop Timers";
            // 
            // textBoxLNTGWLOOPREMAIN
            // 
            this.textBoxLNTGWLOOPREMAIN.Location = new System.Drawing.Point(1179, 33);
            this.textBoxLNTGWLOOPREMAIN.Name = "textBoxLNTGWLOOPREMAIN";
            this.textBoxLNTGWLOOPREMAIN.Size = new System.Drawing.Size(61, 20);
            this.textBoxLNTGWLOOPREMAIN.TabIndex = 311;
            // 
            // labelLNTGWUNICAST
            // 
            this.labelLNTGWUNICAST.AutoSize = true;
            this.labelLNTGWUNICAST.Location = new System.Drawing.Point(1092, 184);
            this.labelLNTGWUNICAST.Name = "labelLNTGWUNICAST";
            this.labelLNTGWUNICAST.Size = new System.Drawing.Size(43, 13);
            this.labelLNTGWUNICAST.TabIndex = 310;
            this.labelLNTGWUNICAST.Text = "Unicast";
            // 
            // labelLNTGWBROADCAST
            // 
            this.labelLNTGWBROADCAST.AutoSize = true;
            this.labelLNTGWBROADCAST.Location = new System.Drawing.Point(1092, 213);
            this.labelLNTGWBROADCAST.Name = "labelLNTGWBROADCAST";
            this.labelLNTGWBROADCAST.Size = new System.Drawing.Size(55, 13);
            this.labelLNTGWBROADCAST.TabIndex = 309;
            this.labelLNTGWBROADCAST.Text = "Broadcast";
            // 
            // buttonLNTGWBROADSTOPTONGGLE
            // 
            this.buttonLNTGWBROADSTOPTONGGLE.Location = new System.Drawing.Point(1396, 208);
            this.buttonLNTGWBROADSTOPTONGGLE.Name = "buttonLNTGWBROADSTOPTONGGLE";
            this.buttonLNTGWBROADSTOPTONGGLE.Size = new System.Drawing.Size(137, 23);
            this.buttonLNTGWBROADSTOPTONGGLE.TabIndex = 308;
            this.buttonLNTGWBROADSTOPTONGGLE.Text = "Stop Tonggle Loop";
            this.buttonLNTGWBROADSTOPTONGGLE.UseVisualStyleBackColor = true;
            this.buttonLNTGWBROADSTOPTONGGLE.Click += new System.EventHandler(this.buttonLNTGWBROADSTOPTONGGLE_Click);
            // 
            // buttonLNTGWBROADTONGGLE
            // 
            this.buttonLNTGWBROADTONGGLE.Location = new System.Drawing.Point(1314, 208);
            this.buttonLNTGWBROADTONGGLE.Name = "buttonLNTGWBROADTONGGLE";
            this.buttonLNTGWBROADTONGGLE.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWBROADTONGGLE.TabIndex = 307;
            this.buttonLNTGWBROADTONGGLE.Text = "Tonggle";
            this.buttonLNTGWBROADTONGGLE.UseVisualStyleBackColor = true;
            this.buttonLNTGWBROADTONGGLE.Click += new System.EventHandler(this.buttonLNTGWBROADTONGGLE_Click);
            // 
            // buttonLNTGWBROADOFF
            // 
            this.buttonLNTGWBROADOFF.Location = new System.Drawing.Point(1234, 208);
            this.buttonLNTGWBROADOFF.Name = "buttonLNTGWBROADOFF";
            this.buttonLNTGWBROADOFF.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWBROADOFF.TabIndex = 306;
            this.buttonLNTGWBROADOFF.Text = "Off";
            this.buttonLNTGWBROADOFF.UseVisualStyleBackColor = true;
            this.buttonLNTGWBROADOFF.Click += new System.EventHandler(this.buttonLNTGWBROADOFF_Click);
            // 
            // buttonLNTGWBROADON
            // 
            this.buttonLNTGWBROADON.Location = new System.Drawing.Point(1153, 208);
            this.buttonLNTGWBROADON.Name = "buttonLNTGWBROADON";
            this.buttonLNTGWBROADON.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWBROADON.TabIndex = 305;
            this.buttonLNTGWBROADON.Text = "On";
            this.buttonLNTGWBROADON.UseVisualStyleBackColor = true;
            this.buttonLNTGWBROADON.Click += new System.EventHandler(this.buttonLNTGWBROADON_Click);
            // 
            // comboBoxLNTGWUNICAST
            // 
            this.comboBoxLNTGWUNICAST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLNTGWUNICAST.FormattingEnabled = true;
            this.comboBoxLNTGWUNICAST.Items.AddRange(new object[] {
            "Unicast",
            "Broadcast"});
            this.comboBoxLNTGWUNICAST.Location = new System.Drawing.Point(1182, 429);
            this.comboBoxLNTGWUNICAST.Name = "comboBoxLNTGWUNICAST";
            this.comboBoxLNTGWUNICAST.Size = new System.Drawing.Size(93, 21);
            this.comboBoxLNTGWUNICAST.TabIndex = 304;
            // 
            // textBoxLNTGWSENDCMD
            // 
            this.textBoxLNTGWSENDCMD.Location = new System.Drawing.Point(482, 40);
            this.textBoxLNTGWSENDCMD.Name = "textBoxLNTGWSENDCMD";
            this.textBoxLNTGWSENDCMD.Size = new System.Drawing.Size(163, 20);
            this.textBoxLNTGWSENDCMD.TabIndex = 303;
            // 
            // buttonLNTGWSENDCMD
            // 
            this.buttonLNTGWSENDCMD.Location = new System.Drawing.Point(384, 38);
            this.buttonLNTGWSENDCMD.Name = "buttonLNTGWSENDCMD";
            this.buttonLNTGWSENDCMD.Size = new System.Drawing.Size(80, 23);
            this.buttonLNTGWSENDCMD.TabIndex = 302;
            this.buttonLNTGWSENDCMD.Text = "Send Cmd";
            this.buttonLNTGWSENDCMD.UseVisualStyleBackColor = true;
            this.buttonLNTGWSENDCMD.Click += new System.EventHandler(this.buttonLNTGWSENDCMD_Click);
            // 
            // listViewLNTGWINFO
            // 
            this.listViewLNTGWINFO.AllowDrop = true;
            this.listViewLNTGWINFO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewLNTGWINFO.CheckBoxes = true;
            this.listViewLNTGWINFO.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader11,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader21});
            this.listViewLNTGWINFO.FullRowSelect = true;
            this.listViewLNTGWINFO.HideSelection = false;
            this.listViewLNTGWINFO.Location = new System.Drawing.Point(17, 93);
            this.listViewLNTGWINFO.Name = "listViewLNTGWINFO";
            this.listViewLNTGWINFO.Size = new System.Drawing.Size(628, 472);
            this.listViewLNTGWINFO.TabIndex = 301;
            this.listViewLNTGWINFO.UseCompatibleStateImageBehavior = false;
            this.listViewLNTGWINFO.View = System.Windows.Forms.View.Details;
            this.listViewLNTGWINFO.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewLNTGWINFO_ColumnClick);
            // 
            // Index
            // 
            this.Index.Text = "Index";
            this.Index.Width = 86;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "NwkAddr";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader12.Width = 67;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "MACAddr";
            this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader13.Width = 127;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "NxtHop";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Channel";
            this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader14.Width = 62;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Type";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 98;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "PANID";
            // 
            // buttonLNTGWDBGPORT
            // 
            this.buttonLNTGWDBGPORT.Location = new System.Drawing.Point(17, 36);
            this.buttonLNTGWDBGPORT.Name = "buttonLNTGWDBGPORT";
            this.buttonLNTGWDBGPORT.Size = new System.Drawing.Size(128, 23);
            this.buttonLNTGWDBGPORT.TabIndex = 300;
            this.buttonLNTGWDBGPORT.Text = "Open GW Dbg Port";
            this.buttonLNTGWDBGPORT.UseVisualStyleBackColor = true;
            this.buttonLNTGWDBGPORT.Click += new System.EventHandler(this.buttonLNTGWDBGPORT_Click);
            // 
            // buttonLNTFGWDISPERMIT
            // 
            this.buttonLNTFGWDISPERMIT.Location = new System.Drawing.Point(1262, 87);
            this.buttonLNTFGWDISPERMIT.Name = "buttonLNTFGWDISPERMIT";
            this.buttonLNTFGWDISPERMIT.Size = new System.Drawing.Size(122, 23);
            this.buttonLNTFGWDISPERMIT.TabIndex = 298;
            this.buttonLNTFGWDISPERMIT.Text = "Disable Permit Join";
            this.buttonLNTFGWDISPERMIT.UseVisualStyleBackColor = true;
            this.buttonLNTFGWDISPERMIT.Click += new System.EventHandler(this.buttonLNTFGWDISPERMIT_Click);
            // 
            // buttonLNTGWPERMMIT
            // 
            this.buttonLNTGWPERMMIT.Location = new System.Drawing.Point(1181, 87);
            this.buttonLNTGWPERMMIT.Name = "buttonLNTGWPERMMIT";
            this.buttonLNTGWPERMMIT.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWPERMMIT.TabIndex = 297;
            this.buttonLNTGWPERMMIT.Text = "Permit Join";
            this.buttonLNTGWPERMMIT.UseVisualStyleBackColor = true;
            this.buttonLNTGWPERMMIT.Click += new System.EventHandler(this.buttonLNTGWPERMMIT_Click);
            // 
            // checkBoxLNTGWALL
            // 
            this.checkBoxLNTGWALL.AutoSize = true;
            this.checkBoxLNTGWALL.Location = new System.Drawing.Point(809, 63);
            this.checkBoxLNTGWALL.Name = "checkBoxLNTGWALL";
            this.checkBoxLNTGWALL.Size = new System.Drawing.Size(45, 17);
            this.checkBoxLNTGWALL.TabIndex = 296;
            this.checkBoxLNTGWALL.Text = "ALL";
            this.checkBoxLNTGWALL.UseVisualStyleBackColor = true;
            this.checkBoxLNTGWALL.CheckedChanged += new System.EventHandler(this.checkBoxLNTGWALL_CheckedChanged);
            // 
            // textBoxLNTGWSETINTERVALMAX
            // 
            this.textBoxLNTGWSETINTERVALMAX.Location = new System.Drawing.Point(1331, 60);
            this.textBoxLNTGWSETINTERVALMAX.Name = "textBoxLNTGWSETINTERVALMAX";
            this.textBoxLNTGWSETINTERVALMAX.Size = new System.Drawing.Size(71, 20);
            this.textBoxLNTGWSETINTERVALMAX.TabIndex = 295;
            // 
            // buttonLNTGWSTOPMOVETEMP
            // 
            this.buttonLNTGWSTOPMOVETEMP.Location = new System.Drawing.Point(1399, 544);
            this.buttonLNTGWSTOPMOVETEMP.Name = "buttonLNTGWSTOPMOVETEMP";
            this.buttonLNTGWSTOPMOVETEMP.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWSTOPMOVETEMP.TabIndex = 294;
            this.buttonLNTGWSTOPMOVETEMP.Text = "Stop Loop";
            this.buttonLNTGWSTOPMOVETEMP.UseVisualStyleBackColor = true;
            this.buttonLNTGWSTOPMOVETEMP.Click += new System.EventHandler(this.buttonLNTGWSTOPMOVETEMP_Click);
            // 
            // buttonLNTGWSTOPMOVESAT
            // 
            this.buttonLNTGWSTOPMOVESAT.Location = new System.Drawing.Point(1399, 513);
            this.buttonLNTGWSTOPMOVESAT.Name = "buttonLNTGWSTOPMOVESAT";
            this.buttonLNTGWSTOPMOVESAT.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWSTOPMOVESAT.TabIndex = 293;
            this.buttonLNTGWSTOPMOVESAT.Text = "Stop Loop";
            this.buttonLNTGWSTOPMOVESAT.UseVisualStyleBackColor = true;
            this.buttonLNTGWSTOPMOVESAT.Click += new System.EventHandler(this.buttonLNTGWSTOPMOVESAT_Click);
            // 
            // buttonLNTGWSTOPMOVECOLOR
            // 
            this.buttonLNTGWSTOPMOVECOLOR.Location = new System.Drawing.Point(1509, 488);
            this.buttonLNTGWSTOPMOVECOLOR.Name = "buttonLNTGWSTOPMOVECOLOR";
            this.buttonLNTGWSTOPMOVECOLOR.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWSTOPMOVECOLOR.TabIndex = 292;
            this.buttonLNTGWSTOPMOVECOLOR.Text = "Stop Loop";
            this.buttonLNTGWSTOPMOVECOLOR.UseVisualStyleBackColor = true;
            this.buttonLNTGWSTOPMOVECOLOR.Click += new System.EventHandler(this.buttonLNTGWSTOPMOVECOLOR_Click);
            // 
            // buttonLNTGWSTOPMOVEHUE
            // 
            this.buttonLNTGWSTOPMOVEHUE.Location = new System.Drawing.Point(1509, 457);
            this.buttonLNTGWSTOPMOVEHUE.Name = "buttonLNTGWSTOPMOVEHUE";
            this.buttonLNTGWSTOPMOVEHUE.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWSTOPMOVEHUE.TabIndex = 291;
            this.buttonLNTGWSTOPMOVEHUE.Text = "Stop Loop";
            this.buttonLNTGWSTOPMOVEHUE.UseVisualStyleBackColor = true;
            this.buttonLNTGWSTOPMOVEHUE.Click += new System.EventHandler(this.buttonLNTGWSTOPMOVEHUE_Click);
            // 
            // textBoxLNTGWSETDIR
            // 
            this.textBoxLNTGWSETDIR.Location = new System.Drawing.Point(1471, 60);
            this.textBoxLNTGWSETDIR.Name = "textBoxLNTGWSETDIR";
            this.textBoxLNTGWSETDIR.Size = new System.Drawing.Size(58, 20);
            this.textBoxLNTGWSETDIR.TabIndex = 290;
            // 
            // textBoxLNTGWSETSTEP
            // 
            this.textBoxLNTGWSETSTEP.Location = new System.Drawing.Point(1408, 60);
            this.textBoxLNTGWSETSTEP.Name = "textBoxLNTGWSETSTEP";
            this.textBoxLNTGWSETSTEP.Size = new System.Drawing.Size(57, 20);
            this.textBoxLNTGWSETSTEP.TabIndex = 289;
            // 
            // comboBoxLNTGWLEVELWITHONOFF
            // 
            this.comboBoxLNTGWLEVELWITHONOFF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLNTGWLEVELWITHONOFF.FormattingEnabled = true;
            this.comboBoxLNTGWLEVELWITHONOFF.Items.AddRange(new object[] {
            "WITHOUT ON/OFF",
            "WITH ON/OFF"});
            this.comboBoxLNTGWLEVELWITHONOFF.Location = new System.Drawing.Point(1505, 428);
            this.comboBoxLNTGWLEVELWITHONOFF.Name = "comboBoxLNTGWLEVELWITHONOFF";
            this.comboBoxLNTGWLEVELWITHONOFF.Size = new System.Drawing.Size(106, 21);
            this.comboBoxLNTGWLEVELWITHONOFF.TabIndex = 288;
            // 
            // buttonLNTGWSTOPMOVELEVEL
            // 
            this.buttonLNTGWSTOPMOVELEVEL.Location = new System.Drawing.Point(1617, 426);
            this.buttonLNTGWSTOPMOVELEVEL.Name = "buttonLNTGWSTOPMOVELEVEL";
            this.buttonLNTGWSTOPMOVELEVEL.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWSTOPMOVELEVEL.TabIndex = 287;
            this.buttonLNTGWSTOPMOVELEVEL.Text = "Stop Loop";
            this.buttonLNTGWSTOPMOVELEVEL.UseVisualStyleBackColor = true;
            this.buttonLNTGWSTOPMOVELEVEL.Click += new System.EventHandler(this.buttonLNTGWSTOPMOVELEVEL_Click);
            // 
            // buttonLNTGWSTOPIDENTIFY
            // 
            this.buttonLNTGWSTOPIDENTIFY.Location = new System.Drawing.Point(1288, 371);
            this.buttonLNTGWSTOPIDENTIFY.Name = "buttonLNTGWSTOPIDENTIFY";
            this.buttonLNTGWSTOPIDENTIFY.Size = new System.Drawing.Size(99, 23);
            this.buttonLNTGWSTOPIDENTIFY.TabIndex = 286;
            this.buttonLNTGWSTOPIDENTIFY.Text = "Stop Identify Loop";
            this.buttonLNTGWSTOPIDENTIFY.UseVisualStyleBackColor = true;
            this.buttonLNTGWSTOPIDENTIFY.Click += new System.EventHandler(this.buttonLNTGWSTOPIDENTIFY_Click);
            // 
            // textBoxLNTGWTEMPTIME
            // 
            this.textBoxLNTGWTEMPTIME.Location = new System.Drawing.Point(1289, 546);
            this.textBoxLNTGWTEMPTIME.Name = "textBoxLNTGWTEMPTIME";
            this.textBoxLNTGWTEMPTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWTEMPTIME.TabIndex = 285;
            // 
            // textBoxLNTGWTEMP
            // 
            this.textBoxLNTGWTEMP.Location = new System.Drawing.Point(1183, 546);
            this.textBoxLNTGWTEMP.Name = "textBoxLNTGWTEMP";
            this.textBoxLNTGWTEMP.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWTEMP.TabIndex = 284;
            // 
            // textBoxLNTGWSATTIME
            // 
            this.textBoxLNTGWSATTIME.Location = new System.Drawing.Point(1289, 516);
            this.textBoxLNTGWSATTIME.Name = "textBoxLNTGWSATTIME";
            this.textBoxLNTGWSATTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWSATTIME.TabIndex = 283;
            // 
            // textBoxLNTGWSAT
            // 
            this.textBoxLNTGWSAT.Location = new System.Drawing.Point(1183, 516);
            this.textBoxLNTGWSAT.Name = "textBoxLNTGWSAT";
            this.textBoxLNTGWSAT.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWSAT.TabIndex = 282;
            // 
            // textBoxLNTGWCOLORTIME
            // 
            this.textBoxLNTGWCOLORTIME.Location = new System.Drawing.Point(1399, 488);
            this.textBoxLNTGWCOLORTIME.Name = "textBoxLNTGWCOLORTIME";
            this.textBoxLNTGWCOLORTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWCOLORTIME.TabIndex = 281;
            // 
            // textBoxLNTGWCOLORY
            // 
            this.textBoxLNTGWCOLORY.Location = new System.Drawing.Point(1292, 486);
            this.textBoxLNTGWCOLORY.Name = "textBoxLNTGWCOLORY";
            this.textBoxLNTGWCOLORY.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWCOLORY.TabIndex = 280;
            // 
            // textBoxLNTGWCOLORX
            // 
            this.textBoxLNTGWCOLORX.Location = new System.Drawing.Point(1184, 488);
            this.textBoxLNTGWCOLORX.Name = "textBoxLNTGWCOLORX";
            this.textBoxLNTGWCOLORX.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWCOLORX.TabIndex = 279;
            // 
            // textBoxLNTGWHUETIME
            // 
            this.textBoxLNTGWHUETIME.Location = new System.Drawing.Point(1399, 460);
            this.textBoxLNTGWHUETIME.Name = "textBoxLNTGWHUETIME";
            this.textBoxLNTGWHUETIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWHUETIME.TabIndex = 278;
            // 
            // textBoxLNTGWHUEDIR
            // 
            this.textBoxLNTGWHUEDIR.Location = new System.Drawing.Point(1292, 460);
            this.textBoxLNTGWHUEDIR.Name = "textBoxLNTGWHUEDIR";
            this.textBoxLNTGWHUEDIR.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWHUEDIR.TabIndex = 277;
            // 
            // textBoxLNTGWHUE
            // 
            this.textBoxLNTGWHUE.Location = new System.Drawing.Point(1182, 460);
            this.textBoxLNTGWHUE.Name = "textBoxLNTGWHUE";
            this.textBoxLNTGWHUE.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWHUE.TabIndex = 276;
            // 
            // textBoxLNTGWLEVELTIME
            // 
            this.textBoxLNTGWLEVELTIME.Location = new System.Drawing.Point(1399, 429);
            this.textBoxLNTGWLEVELTIME.Name = "textBoxLNTGWLEVELTIME";
            this.textBoxLNTGWLEVELTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWLEVELTIME.TabIndex = 275;
            // 
            // textBoxLNTGWLEVEL
            // 
            this.textBoxLNTGWLEVEL.Location = new System.Drawing.Point(1292, 430);
            this.textBoxLNTGWLEVEL.Name = "textBoxLNTGWLEVEL";
            this.textBoxLNTGWLEVEL.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWLEVEL.TabIndex = 274;
            // 
            // textBoxLNTGWIDENTIFYTIME
            // 
            this.textBoxLNTGWIDENTIFYTIME.Location = new System.Drawing.Point(1182, 371);
            this.textBoxLNTGWIDENTIFYTIME.Name = "textBoxLNTGWIDENTIFYTIME";
            this.textBoxLNTGWIDENTIFYTIME.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWIDENTIFYTIME.TabIndex = 273;
            // 
            // textBoxLNTGWREADRPRTATTRIBUTEID
            // 
            this.textBoxLNTGWREADRPRTATTRIBUTEID.Location = new System.Drawing.Point(1246, 341);
            this.textBoxLNTGWREADRPRTATTRIBUTEID.Name = "textBoxLNTGWREADRPRTATTRIBUTEID";
            this.textBoxLNTGWREADRPRTATTRIBUTEID.Size = new System.Drawing.Size(63, 20);
            this.textBoxLNTGWREADRPRTATTRIBUTEID.TabIndex = 272;
            // 
            // textBoxLNTGWREADRPRTCLUSTERID
            // 
            this.textBoxLNTGWREADRPRTCLUSTERID.Location = new System.Drawing.Point(1170, 341);
            this.textBoxLNTGWREADRPRTCLUSTERID.Name = "textBoxLNTGWREADRPRTCLUSTERID";
            this.textBoxLNTGWREADRPRTCLUSTERID.Size = new System.Drawing.Size(70, 20);
            this.textBoxLNTGWREADRPRTCLUSTERID.TabIndex = 271;
            // 
            // textBoxLNTGWCONFIGRPRTCHANGE
            // 
            this.textBoxLNTGWCONFIGRPRTCHANGE.Location = new System.Drawing.Point(1580, 311);
            this.textBoxLNTGWCONFIGRPRTCHANGE.Name = "textBoxLNTGWCONFIGRPRTCHANGE";
            this.textBoxLNTGWCONFIGRPRTCHANGE.Size = new System.Drawing.Size(58, 20);
            this.textBoxLNTGWCONFIGRPRTCHANGE.TabIndex = 270;
            // 
            // textBoxLNTGWCONFIGRPRTTIMEOUT
            // 
            this.textBoxLNTGWCONFIGRPRTTIMEOUT.Location = new System.Drawing.Point(1505, 310);
            this.textBoxLNTGWCONFIGRPRTTIMEOUT.Name = "textBoxLNTGWCONFIGRPRTTIMEOUT";
            this.textBoxLNTGWCONFIGRPRTTIMEOUT.Size = new System.Drawing.Size(69, 20);
            this.textBoxLNTGWCONFIGRPRTTIMEOUT.TabIndex = 269;
            // 
            // textBoxLNTGWCONFIGRPRTMAXINTERVAL
            // 
            this.textBoxLNTGWCONFIGRPRTMAXINTERVAL.Location = new System.Drawing.Point(1433, 310);
            this.textBoxLNTGWCONFIGRPRTMAXINTERVAL.Name = "textBoxLNTGWCONFIGRPRTMAXINTERVAL";
            this.textBoxLNTGWCONFIGRPRTMAXINTERVAL.Size = new System.Drawing.Size(66, 20);
            this.textBoxLNTGWCONFIGRPRTMAXINTERVAL.TabIndex = 268;
            // 
            // textBoxLNTGWCONFIGRPRTMININTERVAL
            // 
            this.textBoxLNTGWCONFIGRPRTMININTERVAL.Location = new System.Drawing.Point(1368, 310);
            this.textBoxLNTGWCONFIGRPRTMININTERVAL.Name = "textBoxLNTGWCONFIGRPRTMININTERVAL";
            this.textBoxLNTGWCONFIGRPRTMININTERVAL.Size = new System.Drawing.Size(58, 20);
            this.textBoxLNTGWCONFIGRPRTMININTERVAL.TabIndex = 267;
            // 
            // textBoxLNTGWCONFIGRPRTTYPE
            // 
            this.textBoxLNTGWCONFIGRPRTTYPE.Location = new System.Drawing.Point(1244, 312);
            this.textBoxLNTGWCONFIGRPRTTYPE.Name = "textBoxLNTGWCONFIGRPRTTYPE";
            this.textBoxLNTGWCONFIGRPRTTYPE.Size = new System.Drawing.Size(57, 20);
            this.textBoxLNTGWCONFIGRPRTTYPE.TabIndex = 266;
            // 
            // textBoxLNTGWCONFIGRPRTATTRIBID
            // 
            this.textBoxLNTGWCONFIGRPRTATTRIBID.Location = new System.Drawing.Point(1309, 311);
            this.textBoxLNTGWCONFIGRPRTATTRIBID.Name = "textBoxLNTGWCONFIGRPRTATTRIBID";
            this.textBoxLNTGWCONFIGRPRTATTRIBID.Size = new System.Drawing.Size(53, 20);
            this.textBoxLNTGWCONFIGRPRTATTRIBID.TabIndex = 265;
            // 
            // textBoxLNTGWCONFIGRPRTCLUSTERID
            // 
            this.textBoxLNTGWCONFIGRPRTCLUSTERID.Location = new System.Drawing.Point(1171, 311);
            this.textBoxLNTGWCONFIGRPRTCLUSTERID.Name = "textBoxLNTGWCONFIGRPRTCLUSTERID";
            this.textBoxLNTGWCONFIGRPRTCLUSTERID.Size = new System.Drawing.Size(67, 20);
            this.textBoxLNTGWCONFIGRPRTCLUSTERID.TabIndex = 264;
            // 
            // comboBoxLNTGWLEAVECHILDREN
            // 
            this.comboBoxLNTGWLEAVECHILDREN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLNTGWLEAVECHILDREN.FormattingEnabled = true;
            this.comboBoxLNTGWLEAVECHILDREN.Items.AddRange(new object[] {
            "DO NOT REMOVE CHILDREN",
            "REMOVE CHILDREN"});
            this.comboBoxLNTGWLEAVECHILDREN.Location = new System.Drawing.Point(1292, 281);
            this.comboBoxLNTGWLEAVECHILDREN.Name = "comboBoxLNTGWLEAVECHILDREN";
            this.comboBoxLNTGWLEAVECHILDREN.Size = new System.Drawing.Size(159, 21);
            this.comboBoxLNTGWLEAVECHILDREN.TabIndex = 263;
            // 
            // comboBoxLNTGWLEAVEREJOIN
            // 
            this.comboBoxLNTGWLEAVEREJOIN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLNTGWLEAVEREJOIN.FormattingEnabled = true;
            this.comboBoxLNTGWLEAVEREJOIN.Items.AddRange(new object[] {
            "DO NOT REJOIN",
            "REJOIN"});
            this.comboBoxLNTGWLEAVEREJOIN.Location = new System.Drawing.Point(1165, 281);
            this.comboBoxLNTGWLEAVEREJOIN.Name = "comboBoxLNTGWLEAVEREJOIN";
            this.comboBoxLNTGWLEAVEREJOIN.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLNTGWLEAVEREJOIN.TabIndex = 262;
            // 
            // textBoxLNTGWWRITEATTRIBUTEDATA
            // 
            this.textBoxLNTGWWRITEATTRIBUTEDATA.Location = new System.Drawing.Point(1394, 150);
            this.textBoxLNTGWWRITEATTRIBUTEDATA.Name = "textBoxLNTGWWRITEATTRIBUTEDATA";
            this.textBoxLNTGWWRITEATTRIBUTEDATA.Size = new System.Drawing.Size(137, 20);
            this.textBoxLNTGWWRITEATTRIBUTEDATA.TabIndex = 261;
            // 
            // textBoxLNTGWWRITEATTRIBUTEATTRIBUTEDATATYPE
            // 
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEDATATYPE.Location = new System.Drawing.Point(1326, 149);
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEDATATYPE.Name = "textBoxLNTGWWRITEATTRIBUTEATTRIBUTEDATATYPE";
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEDATATYPE.Size = new System.Drawing.Size(62, 20);
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEDATATYPE.TabIndex = 260;
            // 
            // textBoxLNTGWWRITEATTRIBUTEATTRIBUTEID
            // 
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEID.Location = new System.Drawing.Point(1251, 149);
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEID.Name = "textBoxLNTGWWRITEATTRIBUTEATTRIBUTEID";
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEID.Size = new System.Drawing.Size(69, 20);
            this.textBoxLNTGWWRITEATTRIBUTEATTRIBUTEID.TabIndex = 259;
            // 
            // textBoxLNTGWWRITEATTRIBUTECLUSTERID
            // 
            this.textBoxLNTGWWRITEATTRIBUTECLUSTERID.Location = new System.Drawing.Point(1182, 149);
            this.textBoxLNTGWWRITEATTRIBUTECLUSTERID.Name = "textBoxLNTGWWRITEATTRIBUTECLUSTERID";
            this.textBoxLNTGWWRITEATTRIBUTECLUSTERID.Size = new System.Drawing.Size(63, 20);
            this.textBoxLNTGWWRITEATTRIBUTECLUSTERID.TabIndex = 258;
            // 
            // textBoxLNTGWATTRIBUTEID
            // 
            this.textBoxLNTGWATTRIBUTEID.Location = new System.Drawing.Point(1251, 119);
            this.textBoxLNTGWATTRIBUTEID.Name = "textBoxLNTGWATTRIBUTEID";
            this.textBoxLNTGWATTRIBUTEID.Size = new System.Drawing.Size(69, 20);
            this.textBoxLNTGWATTRIBUTEID.TabIndex = 256;
            // 
            // textBoxLNTGWREADCLUSTERID
            // 
            this.textBoxLNTGWREADCLUSTERID.Location = new System.Drawing.Point(1182, 119);
            this.textBoxLNTGWREADCLUSTERID.Name = "textBoxLNTGWREADCLUSTERID";
            this.textBoxLNTGWREADCLUSTERID.Size = new System.Drawing.Size(63, 20);
            this.textBoxLNTGWREADCLUSTERID.TabIndex = 255;
            // 
            // buttonLNTGWMOVETEMP
            // 
            this.buttonLNTGWMOVETEMP.Location = new System.Drawing.Point(1079, 542);
            this.buttonLNTGWMOVETEMP.Name = "buttonLNTGWMOVETEMP";
            this.buttonLNTGWMOVETEMP.Size = new System.Drawing.Size(90, 22);
            this.buttonLNTGWMOVETEMP.TabIndex = 254;
            this.buttonLNTGWMOVETEMP.Text = "MoveToTemp";
            this.buttonLNTGWMOVETEMP.UseVisualStyleBackColor = true;
            this.buttonLNTGWMOVETEMP.Click += new System.EventHandler(this.buttonLNTGWMOVETEMP_Click);
            // 
            // buttonLNTGWMOVESAT
            // 
            this.buttonLNTGWMOVESAT.Location = new System.Drawing.Point(1083, 514);
            this.buttonLNTGWMOVESAT.Name = "buttonLNTGWMOVESAT";
            this.buttonLNTGWMOVESAT.Size = new System.Drawing.Size(90, 22);
            this.buttonLNTGWMOVESAT.TabIndex = 253;
            this.buttonLNTGWMOVESAT.Text = "MoveToSat";
            this.buttonLNTGWMOVESAT.UseVisualStyleBackColor = true;
            this.buttonLNTGWMOVESAT.Click += new System.EventHandler(this.buttonLNTGWMOVESAT_Click);
            // 
            // buttonLNTGWMOVECOLOR
            // 
            this.buttonLNTGWMOVECOLOR.Location = new System.Drawing.Point(1083, 486);
            this.buttonLNTGWMOVECOLOR.Name = "buttonLNTGWMOVECOLOR";
            this.buttonLNTGWMOVECOLOR.Size = new System.Drawing.Size(90, 22);
            this.buttonLNTGWMOVECOLOR.TabIndex = 252;
            this.buttonLNTGWMOVECOLOR.Text = "MoveToColor";
            this.buttonLNTGWMOVECOLOR.UseVisualStyleBackColor = true;
            this.buttonLNTGWMOVECOLOR.Click += new System.EventHandler(this.buttonLNTGWMOVECOLOR_Click);
            // 
            // buttonLNTGWMOVEHUE
            // 
            this.buttonLNTGWMOVEHUE.Location = new System.Drawing.Point(1084, 458);
            this.buttonLNTGWMOVEHUE.Name = "buttonLNTGWMOVEHUE";
            this.buttonLNTGWMOVEHUE.Size = new System.Drawing.Size(90, 22);
            this.buttonLNTGWMOVEHUE.TabIndex = 251;
            this.buttonLNTGWMOVEHUE.Text = "MoveToHue";
            this.buttonLNTGWMOVEHUE.UseVisualStyleBackColor = true;
            this.buttonLNTGWMOVEHUE.Click += new System.EventHandler(this.buttonLNTGWMOVEHUE_Click);
            // 
            // buttonLNTGWMOVELEVEL
            // 
            this.buttonLNTGWMOVELEVEL.Location = new System.Drawing.Point(1084, 429);
            this.buttonLNTGWMOVELEVEL.Name = "buttonLNTGWMOVELEVEL";
            this.buttonLNTGWMOVELEVEL.Size = new System.Drawing.Size(93, 23);
            this.buttonLNTGWMOVELEVEL.TabIndex = 250;
            this.buttonLNTGWMOVELEVEL.Text = "Move to level";
            this.buttonLNTGWMOVELEVEL.UseVisualStyleBackColor = true;
            this.buttonLNTGWMOVELEVEL.Click += new System.EventHandler(this.buttonLNTGWMOVELEVEL_Click);
            // 
            // buttonLNTGWIDENTIFY
            // 
            this.buttonLNTGWIDENTIFY.Location = new System.Drawing.Point(1084, 368);
            this.buttonLNTGWIDENTIFY.Name = "buttonLNTGWIDENTIFY";
            this.buttonLNTGWIDENTIFY.Size = new System.Drawing.Size(93, 23);
            this.buttonLNTGWIDENTIFY.TabIndex = 249;
            this.buttonLNTGWIDENTIFY.Text = "Identify Send";
            this.buttonLNTGWIDENTIFY.UseVisualStyleBackColor = true;
            this.buttonLNTGWIDENTIFY.Click += new System.EventHandler(this.buttonLNTGWIDENTIFY_Click);
            // 
            // buttonLNTGWRESET
            // 
            this.buttonLNTGWRESET.Location = new System.Drawing.Point(1083, 86);
            this.buttonLNTGWRESET.Name = "buttonLNTGWRESET";
            this.buttonLNTGWRESET.Size = new System.Drawing.Size(93, 25);
            this.buttonLNTGWRESET.TabIndex = 248;
            this.buttonLNTGWRESET.Text = "Reset To FD";
            this.buttonLNTGWRESET.UseVisualStyleBackColor = true;
            this.buttonLNTGWRESET.Click += new System.EventHandler(this.buttonLNTGWRESET_Click);
            // 
            // buttonLNTGWREADRPRT
            // 
            this.buttonLNTGWREADRPRT.Location = new System.Drawing.Point(1084, 338);
            this.buttonLNTGWREADRPRT.Name = "buttonLNTGWREADRPRT";
            this.buttonLNTGWREADRPRT.Size = new System.Drawing.Size(80, 24);
            this.buttonLNTGWREADRPRT.TabIndex = 247;
            this.buttonLNTGWREADRPRT.Text = "Read Rprt";
            this.buttonLNTGWREADRPRT.UseVisualStyleBackColor = true;
            this.buttonLNTGWREADRPRT.Click += new System.EventHandler(this.buttonLNTGWREADRPRT_Click);
            // 
            // buttonLNTGWCONFIGRPRT
            // 
            this.buttonLNTGWCONFIGRPRT.Location = new System.Drawing.Point(1084, 308);
            this.buttonLNTGWCONFIGRPRT.Name = "buttonLNTGWCONFIGRPRT";
            this.buttonLNTGWCONFIGRPRT.Size = new System.Drawing.Size(80, 24);
            this.buttonLNTGWCONFIGRPRT.TabIndex = 246;
            this.buttonLNTGWCONFIGRPRT.Text = "Config Rprt";
            this.buttonLNTGWCONFIGRPRT.UseVisualStyleBackColor = true;
            this.buttonLNTGWCONFIGRPRT.Click += new System.EventHandler(this.buttonLNTGWCONFIGRPRT_Click);
            // 
            // buttonLNTGWLEAVE
            // 
            this.buttonLNTGWLEAVE.Location = new System.Drawing.Point(1084, 279);
            this.buttonLNTGWLEAVE.Name = "buttonLNTGWLEAVE";
            this.buttonLNTGWLEAVE.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWLEAVE.TabIndex = 245;
            this.buttonLNTGWLEAVE.Text = "Leave";
            this.buttonLNTGWLEAVE.UseVisualStyleBackColor = true;
            this.buttonLNTGWLEAVE.Click += new System.EventHandler(this.buttonLNTGWLEAVE_Click);
            // 
            // buttonLNTGWWRITE
            // 
            this.buttonLNTGWWRITE.Location = new System.Drawing.Point(1083, 147);
            this.buttonLNTGWWRITE.Name = "buttonLNTGWWRITE";
            this.buttonLNTGWWRITE.Size = new System.Drawing.Size(92, 23);
            this.buttonLNTGWWRITE.TabIndex = 244;
            this.buttonLNTGWWRITE.Text = "Write Attribute";
            this.buttonLNTGWWRITE.UseVisualStyleBackColor = true;
            this.buttonLNTGWWRITE.Click += new System.EventHandler(this.buttonLNTGWWRITE_Click);
            // 
            // textBoxLNTGWUNBINDCLUSTERID
            // 
            this.textBoxLNTGWUNBINDCLUSTERID.Location = new System.Drawing.Point(1351, 252);
            this.textBoxLNTGWUNBINDCLUSTERID.Name = "textBoxLNTGWUNBINDCLUSTERID";
            this.textBoxLNTGWUNBINDCLUSTERID.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWUNBINDCLUSTERID.TabIndex = 243;
            // 
            // textBoxLNTGWBINDCLUSTERID
            // 
            this.textBoxLNTGWBINDCLUSTERID.Location = new System.Drawing.Point(1164, 251);
            this.textBoxLNTGWBINDCLUSTERID.Name = "textBoxLNTGWBINDCLUSTERID";
            this.textBoxLNTGWBINDCLUSTERID.Size = new System.Drawing.Size(100, 20);
            this.textBoxLNTGWBINDCLUSTERID.TabIndex = 242;
            // 
            // buttonLNTGWUNBIND
            // 
            this.buttonLNTGWUNBIND.Location = new System.Drawing.Point(1270, 250);
            this.buttonLNTGWUNBIND.Name = "buttonLNTGWUNBIND";
            this.buttonLNTGWUNBIND.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWUNBIND.TabIndex = 241;
            this.buttonLNTGWUNBIND.Text = "Unbind";
            this.buttonLNTGWUNBIND.UseVisualStyleBackColor = true;
            this.buttonLNTGWUNBIND.Click += new System.EventHandler(this.buttonLNTGWUNBIND_Click);
            // 
            // buttonLNTGWBIND
            // 
            this.buttonLNTGWBIND.Location = new System.Drawing.Point(1083, 249);
            this.buttonLNTGWBIND.Name = "buttonLNTGWBIND";
            this.buttonLNTGWBIND.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWBIND.TabIndex = 240;
            this.buttonLNTGWBIND.Text = "Bind";
            this.buttonLNTGWBIND.UseVisualStyleBackColor = true;
            this.buttonLNTGWBIND.Click += new System.EventHandler(this.buttonLNTGWBIND_Click);
            // 
            // buttonLNTGWSTOPTONGGLE
            // 
            this.buttonLNTGWSTOPTONGGLE.Location = new System.Drawing.Point(1565, 179);
            this.buttonLNTGWSTOPTONGGLE.Name = "buttonLNTGWSTOPTONGGLE";
            this.buttonLNTGWSTOPTONGGLE.Size = new System.Drawing.Size(137, 23);
            this.buttonLNTGWSTOPTONGGLE.TabIndex = 239;
            this.buttonLNTGWSTOPTONGGLE.Text = "Stop Tonggle Loop";
            this.buttonLNTGWSTOPTONGGLE.UseVisualStyleBackColor = true;
            this.buttonLNTGWSTOPTONGGLE.Click += new System.EventHandler(this.buttonLNTGWSTOPTONGGLE_Click);
            // 
            // buttonLNTGWTONGGLE
            // 
            this.buttonLNTGWTONGGLE.Location = new System.Drawing.Point(1484, 179);
            this.buttonLNTGWTONGGLE.Name = "buttonLNTGWTONGGLE";
            this.buttonLNTGWTONGGLE.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWTONGGLE.TabIndex = 238;
            this.buttonLNTGWTONGGLE.Text = "Tonggle";
            this.buttonLNTGWTONGGLE.UseVisualStyleBackColor = true;
            this.buttonLNTGWTONGGLE.Click += new System.EventHandler(this.buttonLNTGWTONGGLE_Click);
            // 
            // buttonLNTGWOFF
            // 
            this.buttonLNTGWOFF.Location = new System.Drawing.Point(1234, 179);
            this.buttonLNTGWOFF.Name = "buttonLNTGWOFF";
            this.buttonLNTGWOFF.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWOFF.TabIndex = 237;
            this.buttonLNTGWOFF.Text = "Off";
            this.buttonLNTGWOFF.UseVisualStyleBackColor = true;
            this.buttonLNTGWOFF.Click += new System.EventHandler(this.buttonLNTGWOFF_Click);
            // 
            // buttonLNTGWON
            // 
            this.buttonLNTGWON.Location = new System.Drawing.Point(1153, 179);
            this.buttonLNTGWON.Name = "buttonLNTGWON";
            this.buttonLNTGWON.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWON.TabIndex = 236;
            this.buttonLNTGWON.Text = "On";
            this.buttonLNTGWON.UseVisualStyleBackColor = true;
            this.buttonLNTGWON.Click += new System.EventHandler(this.buttonLNTGWON_Click);
            // 
            // textBoxLNTGWSETLOOP
            // 
            this.textBoxLNTGWSETLOOP.Location = new System.Drawing.Point(1179, 60);
            this.textBoxLNTGWSETLOOP.Name = "textBoxLNTGWSETLOOP";
            this.textBoxLNTGWSETLOOP.Size = new System.Drawing.Size(66, 20);
            this.textBoxLNTGWSETLOOP.TabIndex = 235;
            // 
            // buttonLNTGWSET
            // 
            this.buttonLNTGWSET.Location = new System.Drawing.Point(1083, 58);
            this.buttonLNTGWSET.Name = "buttonLNTGWSET";
            this.buttonLNTGWSET.Size = new System.Drawing.Size(92, 23);
            this.buttonLNTGWSET.TabIndex = 234;
            this.buttonLNTGWSET.Text = "Set  Parameter";
            this.buttonLNTGWSET.UseVisualStyleBackColor = true;
            this.buttonLNTGWSET.Click += new System.EventHandler(this.buttonLNTGWSET_Click);
            // 
            // buttonLNTGWSTOPREAD
            // 
            this.buttonLNTGWSTOPREAD.Location = new System.Drawing.Point(1326, 119);
            this.buttonLNTGWSTOPREAD.Name = "buttonLNTGWSTOPREAD";
            this.buttonLNTGWSTOPREAD.Size = new System.Drawing.Size(115, 23);
            this.buttonLNTGWSTOPREAD.TabIndex = 233;
            this.buttonLNTGWSTOPREAD.Text = "Stop Read Loop";
            this.buttonLNTGWSTOPREAD.UseVisualStyleBackColor = true;
            this.buttonLNTGWSTOPREAD.Click += new System.EventHandler(this.buttonLNTGWSTOPREAD_Click);
            // 
            // textBoxLNTGWTIMERINTERVAL
            // 
            this.textBoxLNTGWTIMERINTERVAL.Location = new System.Drawing.Point(1251, 60);
            this.textBoxLNTGWTIMERINTERVAL.Name = "textBoxLNTGWTIMERINTERVAL";
            this.textBoxLNTGWTIMERINTERVAL.Size = new System.Drawing.Size(74, 20);
            this.textBoxLNTGWTIMERINTERVAL.TabIndex = 232;
            // 
            // buttonLNTGWREAD
            // 
            this.buttonLNTGWREAD.Location = new System.Drawing.Point(1083, 117);
            this.buttonLNTGWREAD.Name = "buttonLNTGWREAD";
            this.buttonLNTGWREAD.Size = new System.Drawing.Size(92, 23);
            this.buttonLNTGWREAD.TabIndex = 231;
            this.buttonLNTGWREAD.Text = "Read Attribute";
            this.buttonLNTGWREAD.UseVisualStyleBackColor = true;
            this.buttonLNTGWREAD.Click += new System.EventHandler(this.buttonLNTGWREAD_Click);
            // 
            // textBoxLNTGWVIEW
            // 
            this.textBoxLNTGWVIEW.Location = new System.Drawing.Point(1528, 400);
            this.textBoxLNTGWVIEW.Name = "textBoxLNTGWVIEW";
            this.textBoxLNTGWVIEW.Size = new System.Drawing.Size(79, 20);
            this.textBoxLNTGWVIEW.TabIndex = 230;
            // 
            // buttonLNTGWREMOVEGROUPALL
            // 
            this.buttonLNTGWREMOVEGROUPALL.Location = new System.Drawing.Point(1613, 398);
            this.buttonLNTGWREMOVEGROUPALL.Name = "buttonLNTGWREMOVEGROUPALL";
            this.buttonLNTGWREMOVEGROUPALL.Size = new System.Drawing.Size(109, 23);
            this.buttonLNTGWREMOVEGROUPALL.TabIndex = 229;
            this.buttonLNTGWREMOVEGROUPALL.Text = "Remove Group All";
            this.buttonLNTGWREMOVEGROUPALL.UseVisualStyleBackColor = true;
            this.buttonLNTGWREMOVEGROUPALL.Click += new System.EventHandler(this.buttonLNTGWREMOVEGROUPALL_Click);
            // 
            // buttonLNTGWVIEWGROUP
            // 
            this.buttonLNTGWVIEWGROUP.Location = new System.Drawing.Point(1447, 398);
            this.buttonLNTGWVIEWGROUP.Name = "buttonLNTGWVIEWGROUP";
            this.buttonLNTGWVIEWGROUP.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWVIEWGROUP.TabIndex = 228;
            this.buttonLNTGWVIEWGROUP.Text = "View Group";
            this.buttonLNTGWVIEWGROUP.UseVisualStyleBackColor = true;
            this.buttonLNTGWVIEWGROUP.Click += new System.EventHandler(this.buttonLNTGWVIEWGROUP_Click);
            // 
            // textBoxLNTGWREMOVEGROUP
            // 
            this.textBoxLNTGWREMOVEGROUP.Location = new System.Drawing.Point(1367, 400);
            this.textBoxLNTGWREMOVEGROUP.Name = "textBoxLNTGWREMOVEGROUP";
            this.textBoxLNTGWREMOVEGROUP.Size = new System.Drawing.Size(74, 20);
            this.textBoxLNTGWREMOVEGROUP.TabIndex = 227;
            // 
            // textBoxLNTGWADDGROUP
            // 
            this.textBoxLNTGWADDGROUP.Location = new System.Drawing.Point(1165, 397);
            this.textBoxLNTGWADDGROUP.Name = "textBoxLNTGWADDGROUP";
            this.textBoxLNTGWADDGROUP.Size = new System.Drawing.Size(79, 20);
            this.textBoxLNTGWADDGROUP.TabIndex = 226;
            // 
            // buttonLNTGWREMOVEGROUP
            // 
            this.buttonLNTGWREMOVEGROUP.Location = new System.Drawing.Point(1250, 397);
            this.buttonLNTGWREMOVEGROUP.Name = "buttonLNTGWREMOVEGROUP";
            this.buttonLNTGWREMOVEGROUP.Size = new System.Drawing.Size(109, 23);
            this.buttonLNTGWREMOVEGROUP.TabIndex = 225;
            this.buttonLNTGWREMOVEGROUP.Text = "Remove Group";
            this.buttonLNTGWREMOVEGROUP.UseVisualStyleBackColor = true;
            this.buttonLNTGWREMOVEGROUP.Click += new System.EventHandler(this.buttonLNTGWREMOVEGROUP_Click);
            // 
            // buttonLNTGWADDGROUP
            // 
            this.buttonLNTGWADDGROUP.Location = new System.Drawing.Point(1084, 397);
            this.buttonLNTGWADDGROUP.Name = "buttonLNTGWADDGROUP";
            this.buttonLNTGWADDGROUP.Size = new System.Drawing.Size(75, 23);
            this.buttonLNTGWADDGROUP.TabIndex = 224;
            this.buttonLNTGWADDGROUP.Text = "Add Group";
            this.buttonLNTGWADDGROUP.UseVisualStyleBackColor = true;
            this.buttonLNTGWADDGROUP.Click += new System.EventHandler(this.buttonLNTGWADDGROUP_Click);
            // 
            // listViewLNTGWGROUPINFO
            // 
            this.listViewLNTGWGROUPINFO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewLNTGWGROUPINFO.CheckBoxes = true;
            this.listViewLNTGWGROUPINFO.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.listViewLNTGWGROUPINFO.HideSelection = false;
            this.listViewLNTGWGROUPINFO.Location = new System.Drawing.Point(809, 93);
            this.listViewLNTGWGROUPINFO.Name = "listViewLNTGWGROUPINFO";
            this.listViewLNTGWGROUPINFO.Size = new System.Drawing.Size(252, 474);
            this.listViewLNTGWGROUPINFO.TabIndex = 223;
            this.listViewLNTGWGROUPINFO.UseCompatibleStateImageBehavior = false;
            this.listViewLNTGWGROUPINFO.View = System.Windows.Forms.View.Details;
            this.listViewLNTGWGROUPINFO.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewLNTGWGROUPINFO_ItemChecked);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "nwkAddrJoined";
            this.columnHeader9.Width = 88;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Status";
            this.columnHeader10.Width = 145;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // timerSCANCOM
            // 
            this.timerSCANCOM.Interval = 1000;
            // 
            // timerReadAttribute
            // 
            this.timerReadAttribute.Interval = 60;
            this.timerReadAttribute.Tick += new System.EventHandler(this.timerReadAttribute_Tick);
            // 
            // buttonEZLNTSOCKETGETIP
            // 
            this.buttonEZLNTSOCKETGETIP.Location = new System.Drawing.Point(665, 44);
            this.buttonEZLNTSOCKETGETIP.Name = "buttonEZLNTSOCKETGETIP";
            this.buttonEZLNTSOCKETGETIP.Size = new System.Drawing.Size(75, 23);
            this.buttonEZLNTSOCKETGETIP.TabIndex = 317;
            this.buttonEZLNTSOCKETGETIP.Text = "Get IP";
            this.buttonEZLNTSOCKETGETIP.UseVisualStyleBackColor = true;
            this.buttonEZLNTSOCKETGETIP.Click += new System.EventHandler(this.buttonEZLNTSOCKETGETIP_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1866, 910);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ZigBee Gateway User Interface";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPagePollControl.ResumeLayout(false);
            this.tabPagePollControl.PerformLayout();
            this.tabPage14.ResumeLayout(false);
            this.tabPage14.PerformLayout();
            this.tabPage11.ResumeLayout(false);
            this.tabPage11.PerformLayout();
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tabPage15.PerformLayout();
            this.tabPage13.ResumeLayout(false);
            this.tabPage13.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.BasicClusterTab.ResumeLayout(false);
            this.BasicClusterTab.PerformLayout();
            this.AHIControl.ResumeLayout(false);
            this.AHIControl.PerformLayout();
            this.tabPage12.ResumeLayout(false);
            this.tabPage12.PerformLayout();
            this.tabPageDevice.ResumeLayout(false);
            this.tabPageDevice.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this.tabPage16.PerformLayout();
            this.tabPage17.ResumeLayout(false);
            this.tabPage17.PerformLayout();
            this.tabPage18.ResumeLayout(false);
            this.tabPage18.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPortToolStripMenuItem;
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusPort;
        private System.Windows.Forms.ToolStripStatusLabel toolStripPortSettings;
        private System.Windows.Forms.RichTextBox richTextBoxCommandResponse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBoxMessageView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClearRaw;
        private System.Windows.Forms.Button buttonMessageViewClear;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openOtaFileDialog;
        private System.Windows.Forms.ToolTip toolTipGeneralTooltip;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPagePollControl;
        private System.Windows.Forms.ComboBox comboBoxFastPollEnable;
        private System.Windows.Forms.TextBox textBoxFastPollExpiryTime;
        private System.Windows.Forms.Button buttonSetCheckinRspData;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.TextBox textBoxOtaFileStackVer;
        private System.Windows.Forms.TextBox textBoxOtaFileHeaderVer;
        private System.Windows.Forms.TextBox textBoxOtaFileHeaderLen;
        private System.Windows.Forms.TextBox textBoxOtaFileHeaderFCTL;
        private System.Windows.Forms.TextBox textBoxOtaFileID;
        private System.Windows.Forms.TextBox textBoxOtaFileHeaderStr;
        private System.Windows.Forms.TextBox textBoxOTASetWaitForDataParamsRequestBlockDelay;
        private System.Windows.Forms.TextBox textBoxOTASetWaitForDataParamsRequestTime;
        private System.Windows.Forms.TextBox textBoxOTASetWaitForDataParamsCurrentTime;
        private System.Windows.Forms.TextBox textBoxOTASetWaitForDataParamsSrcEP;
        private System.Windows.Forms.TextBox textBoxOTASetWaitForDataParamsTargetAddr;
        private System.Windows.Forms.TextBox textBoxOtaFileOffset;
        private System.Windows.Forms.TextBox textBoxOtaDownloadStatus;
        private System.Windows.Forms.TextBox textBoxOTAImageNotifyJitter;
        private System.Windows.Forms.TextBox textBoxOTAImageNotifyManuID;
        private System.Windows.Forms.TextBox textBoxOTAImageNotifyImageType;
        private System.Windows.Forms.TextBox textBoxOTAImageNotifyFileVersion;
        private System.Windows.Forms.TextBox textBoxOTAImageNotifyDstEP;
        private System.Windows.Forms.TextBox textBoxOTAImageNotifySrcEP;
        private System.Windows.Forms.TextBox textBoxOTAImageNotifyTargetAddr;
        private System.Windows.Forms.TextBox textBoxOtaFileSize;
        private System.Windows.Forms.TextBox textBoxOtaFileVersion;
        private System.Windows.Forms.TextBox textBoxOtaFileImageType;
        private System.Windows.Forms.TextBox textBoxOtaFileManuCode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonOTASetWaitForDataParams;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBarOtaDownloadProgress;
        private System.Windows.Forms.ComboBox comboBoxOTAImageNotifyType;
        private System.Windows.Forms.ComboBox comboBoxOTAImageNotifyAddrMode;
        private System.Windows.Forms.Button buttonOTAImageNotify;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOTALoadNewImage;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TextBox textBoxZllMoveToHueHue;
        private System.Windows.Forms.TextBox textBoxZllMoveToHueTransTime;
        private System.Windows.Forms.TextBox textBoxZllMoveToHueDirection;
        private System.Windows.Forms.TextBox textBoxZllMoveToHueDstEp;
        private System.Windows.Forms.TextBox textBoxZllMoveToHueSrcEp;
        private System.Windows.Forms.TextBox textBoxZllMoveToHueAddr;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonZllMoveToHue;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.ComboBox comboBoxZllOnOffEffectID;
        private System.Windows.Forms.TextBox textBoxZllOnOffEffectsGradient;
        private System.Windows.Forms.TextBox textBoxZllOnOffEffectsDstEp;
        private System.Windows.Forms.TextBox textBoxZllOnOffEffectsSrcEp;
        private System.Windows.Forms.TextBox textBoxZllOnOffEffectsAddr;
        private System.Windows.Forms.Button buttonZllOnOffEffects;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.Button buttonZllTouchlinkFactoryReset;
        private System.Windows.Forms.Button buttonZllTouchlinkInitiate;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.ComboBox comboBoxEnrollRspCode;
        private System.Windows.Forms.TextBox textBoxEnrollRspZone;
        private System.Windows.Forms.TextBox textBoxEnrollRspDstEp;
        private System.Windows.Forms.TextBox textBoxEnrollRspSrcEp;
        private System.Windows.Forms.TextBox textBoxEnrollRspAddr;
        private System.Windows.Forms.ComboBox comboBoxEnrollRspAddrMode;
        private System.Windows.Forms.Button buttonEnrollResponse;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox comboBoxLockUnlock;
        private System.Windows.Forms.TextBox textBoxLockUnlockDstEp;
        private System.Windows.Forms.TextBox textBoxLockUnlockSrcEp;
        private System.Windows.Forms.TextBox textBoxLockUnlockAddr;
        private System.Windows.Forms.Button buttonLockUnlock;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TextBox textBoxMoveToSatTime;
        private System.Windows.Forms.TextBox textBoxMoveToSatSat;
        private System.Windows.Forms.TextBox textBoxMoveToSatDstEp;
        private System.Windows.Forms.TextBox textBoxMoveToSatSrcEp;
        private System.Windows.Forms.TextBox textBoxMoveToSatAddr;
        private System.Windows.Forms.TextBox textBoxMoveToColorTempRate;
        private System.Windows.Forms.TextBox textBoxMoveToColorTempTemp;
        private System.Windows.Forms.TextBox textBoxMoveToColorTempDstEp;
        private System.Windows.Forms.TextBox textBoxMoveToColorTempSrcEp;
        private System.Windows.Forms.TextBox textBoxMoveToColorTempAddr;
        private System.Windows.Forms.TextBox textBoxMoveToColorTime;
        private System.Windows.Forms.TextBox textBoxMoveToColorY;
        private System.Windows.Forms.TextBox textBoxMoveToColorX;
        private System.Windows.Forms.TextBox textBoxMoveToColorDstEp;
        private System.Windows.Forms.TextBox textBoxMoveToColorSrcEp;
        private System.Windows.Forms.TextBox textBoxMoveToColorAddr;
        private System.Windows.Forms.TextBox textBoxMoveToHueTime;
        private System.Windows.Forms.TextBox textBoxMoveToHueDir;
        private System.Windows.Forms.TextBox textBoxMoveToHueHue;
        private System.Windows.Forms.TextBox textBoxMoveToHueDstEp;
        private System.Windows.Forms.TextBox textBoxMoveToHueSrcEp;
        private System.Windows.Forms.TextBox textBoxMoveToHueAddr;
        private System.Windows.Forms.Button buttonMoveToSat;
        private System.Windows.Forms.Button buttonMoveToColorTemp;
        private System.Windows.Forms.Button buttonMoveToColor;
        private System.Windows.Forms.Button buttonMoveToHue;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.CheckBox checkBoxShowExtension;
        private System.Windows.Forms.TextBox textBoxAddSceneData;
        private System.Windows.Forms.TextBox textBoxAddSceneExtLen;
        private System.Windows.Forms.TextBox textBoxRemoveSceneSceneID;
        private System.Windows.Forms.TextBox textBoxRemoveSceneGroupID;
        private System.Windows.Forms.TextBox textBoxRemoveSceneDstEndPoint;
        private System.Windows.Forms.TextBox textBoxRemoveSceneSrcEndPoint;
        private System.Windows.Forms.TextBox textBoxRemoveSceneAddr;
        private System.Windows.Forms.TextBox textBoxRemoveAllScenesGroupID;
        private System.Windows.Forms.TextBox textBoxRemoveAllScenesDstEndPoint;
        private System.Windows.Forms.TextBox textBoxRemoveAllScenesSrcEndPoint;
        private System.Windows.Forms.TextBox textBoxRemoveAllScenesAddr;
        private System.Windows.Forms.TextBox textBoxGetSceneMembershipGroupID;
        private System.Windows.Forms.TextBox textBoxGetSceneMembershipDstEndPoint;
        private System.Windows.Forms.TextBox textBoxGetSceneMembershipSrcEndPoint;
        private System.Windows.Forms.TextBox textBoxGetSceneMembershipAddr;
        private System.Windows.Forms.TextBox textBoxRecallSceneSceneId;
        private System.Windows.Forms.TextBox textBoxRecallSceneGroupId;
        private System.Windows.Forms.TextBox textBoxRecallSceneDstEndPoint;
        private System.Windows.Forms.TextBox textBoxRecallSceneSrcEndPoint;
        private System.Windows.Forms.TextBox textBoxRecallSceneAddr;
        private System.Windows.Forms.TextBox textBoxStoreSceneSceneId;
        private System.Windows.Forms.TextBox textBoxStoreSceneGroupId;
        private System.Windows.Forms.TextBox textBoxStoreSceneDstEndPoint;
        private System.Windows.Forms.TextBox textBoxStoreSceneSrcEndPoint;
        private System.Windows.Forms.TextBox textBoxStoreSceneAddr;
        private System.Windows.Forms.TextBox textBoxAddSceneMaxNameLen;
        private System.Windows.Forms.TextBox textBoxAddSceneNameLen;
        private System.Windows.Forms.TextBox textBoxAddSceneName;
        private System.Windows.Forms.TextBox textBoxAddSceneTransTime;
        private System.Windows.Forms.TextBox textBoxAddSceneSceneId;
        private System.Windows.Forms.TextBox textBoxAddSceneGroupId;
        private System.Windows.Forms.TextBox textBoxAddSceneDstEndPoint;
        private System.Windows.Forms.TextBox textBoxAddSceneSrcEndPoint;
        private System.Windows.Forms.TextBox textBoxAddSceneAddr;
        private System.Windows.Forms.TextBox textBoxViewSceneSceneId;
        private System.Windows.Forms.TextBox textBoxViewSceneGroupId;
        private System.Windows.Forms.TextBox textBoxViewSceneDstEndPoint;
        private System.Windows.Forms.TextBox textBoxViewSceneSrcEndPoint;
        private System.Windows.Forms.TextBox textBoxViewSceneAddr;
        private System.Windows.Forms.ComboBox comboBoxRemoveSceneAddrMode;
        private System.Windows.Forms.Button buttonRemoveScene;
        private System.Windows.Forms.ComboBox comboBoxRemoveAllScenesAddrMode;
        private System.Windows.Forms.Button buttonRemoveAllScenes;
        private System.Windows.Forms.ComboBox comboBoxGetSceneMembershipAddrMode;
        private System.Windows.Forms.Button buttonGetSceneMembership;
        private System.Windows.Forms.ComboBox comboBoxRecallSceneAddrMode;
        private System.Windows.Forms.Button buttonRecallScene;
        private System.Windows.Forms.ComboBox comboBoxStoreSceneAddrMode;
        private System.Windows.Forms.Button buttonStoreScene;
        private System.Windows.Forms.ComboBox comboBoxAddSceneAddrMode;
        private System.Windows.Forms.Button buttonAddScene;
        private System.Windows.Forms.ComboBox comboBoxViewSceneAddrMode;
        private System.Windows.Forms.Button buttonViewScene;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox comboBoxOnOffAddrMode;
        private System.Windows.Forms.ComboBox comboBoxOnOffCommand;
        private System.Windows.Forms.TextBox textBoxOnOffDstEndPoint;
        private System.Windows.Forms.TextBox textBoxOnOffSrcEndPoint;
        private System.Windows.Forms.TextBox textBoxOnOffAddr;
        private System.Windows.Forms.Button buttonOnOff;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ComboBox comboBoxMoveToLevelOnOff;
        private System.Windows.Forms.ComboBox comboBoxMoveToLevelAddrMode;
        private System.Windows.Forms.TextBox textBoxMoveToLevelTransTime;
        private System.Windows.Forms.TextBox textBoxMoveToLevelLevel;
        private System.Windows.Forms.TextBox textBoxMoveToLevelDstEndPoint;
        private System.Windows.Forms.TextBox textBoxMoveToLevelSrcEndPoint;
        private System.Windows.Forms.TextBox textBoxMoveToLevelAddr;
        private System.Windows.Forms.Button buttonMoveToLevel;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox textBoxIdQueryDstEp;
        private System.Windows.Forms.TextBox textBoxIdQuerySrcEp;
        private System.Windows.Forms.TextBox textBoxIdQueryAddr;
        private System.Windows.Forms.TextBox textBoxIdSendTime;
        private System.Windows.Forms.TextBox textBoxIdSendDstEp;
        private System.Windows.Forms.TextBox textBoxSendIdSrcEp;
        private System.Windows.Forms.TextBox textBoxSendIdAddr;
        private System.Windows.Forms.Button buttonIdQuery;
        private System.Windows.Forms.Button buttonIdSend;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBoxGroupName;
        private System.Windows.Forms.TextBox textBoxGroupNameMaxLength;
        private System.Windows.Forms.TextBox textBoxGroupNameLength;
        private System.Windows.Forms.TextBox textBoxGroupAddIfIdentifyGroupID;
        private System.Windows.Forms.TextBox textBoxGroupAddIfIdentifySrcEp;
        private System.Windows.Forms.TextBox textBoxGroupAddIfIdentifyDstEp;
        private System.Windows.Forms.TextBox textBoxGroupAddIfIndentifyingTargetAddr;
        private System.Windows.Forms.TextBox textBoxRemoveAllGroupDstEp;
        private System.Windows.Forms.TextBox textBoxRemoveAllGroupSrcEp;
        private System.Windows.Forms.TextBox textBoxRemoveAllGroupTargetAddr;
        private System.Windows.Forms.TextBox textBoxRemoveGroupGroupAddr;
        private System.Windows.Forms.TextBox textBoxRemoveGroupDstEp;
        private System.Windows.Forms.TextBox textBoxRemoveGroupSrcEp;
        private System.Windows.Forms.TextBox textBoxRemoveGroupTargetAddr;
        private System.Windows.Forms.TextBox textBoxGetGroupCount;
        private System.Windows.Forms.TextBox textBoxGetGroupDstEp;
        private System.Windows.Forms.TextBox textBoxGetGroupSrcEp;
        private System.Windows.Forms.TextBox textBoxGetGroupTargetAddr;
        private System.Windows.Forms.TextBox textBoxViewGroupGroupAddr;
        private System.Windows.Forms.TextBox textBoxViewGroupDstEp;
        private System.Windows.Forms.TextBox textBoxViewGroupSrcEp;
        private System.Windows.Forms.TextBox textBoxViewGroupAddr;
        private System.Windows.Forms.TextBox textBoxAddGroupGroupAddr;
        private System.Windows.Forms.TextBox textBoxAddGroupDstEp;
        private System.Windows.Forms.TextBox textBoxAddGroupSrcEp;
        private System.Windows.Forms.TextBox textBoxAddGroupAddr;
        private System.Windows.Forms.Button buttonAddToList;
        private System.Windows.Forms.Button buttonGroupAddIfIdentifying;
        private System.Windows.Forms.Button buttonGroupRemoveAll;
        private System.Windows.Forms.Button buttonRemoveGroup;
        private System.Windows.Forms.Button buttonGetGroup;
        private System.Windows.Forms.Button buttonViewGroup;
        private System.Windows.Forms.Button buttonAddGroup;
        private System.Windows.Forms.TabPage BasicClusterTab;
        private System.Windows.Forms.TextBox textBoxBasicResetDstEP;
        private System.Windows.Forms.TextBox textBoxBasicResetSrcEP;
        private System.Windows.Forms.TextBox textBoxBasicResetTargetAddr;
        private System.Windows.Forms.ComboBox comboBoxBasicResetTargetAddrMode;
        private System.Windows.Forms.Button buttonBasicReset;
        private System.Windows.Forms.TabPage AHIControl;
        private System.Windows.Forms.TextBox textBoxAHITxPower;
        private System.Windows.Forms.TextBox textBoxIPNConfigDioTxConfInDioMask;
        private System.Windows.Forms.TextBox textBoxDioSetOutputOffPinMask;
        private System.Windows.Forms.TextBox textBoxDioSetOutputOnPinMask;
        private System.Windows.Forms.TextBox textBoxDioSetDirectionOutputPinMask;
        private System.Windows.Forms.TextBox textBoxDioSetDirectionInputPinMask;
        private System.Windows.Forms.TextBox textBoxIPNConfigPollPeriod;
        private System.Windows.Forms.TextBox textBoxIPNConfigDioStatusOutDioMask;
        private System.Windows.Forms.TextBox textBoxIPNConfigDioRfActiveOutDioMask;
        private System.Windows.Forms.Button buttonAHISetTxPower;
        private System.Windows.Forms.Label labelUnimplemented;
        private System.Windows.Forms.ComboBox comboBoxIPNConfigTimerId;
        private System.Windows.Forms.Button buttonDioSetOutput;
        private System.Windows.Forms.Button buttonDioSetDirection;
        private System.Windows.Forms.ComboBox comboBoxIPNConfigRegisterCallback;
        private System.Windows.Forms.ComboBox comboBoxIPNConfigEnable;
        private System.Windows.Forms.Button buttonInPacketNotification;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.TextBox textBoxGeneralInstallCodeCode;
        private System.Windows.Forms.TextBox textBoxGeneralInstallCodeMACaddress;
        private System.Windows.Forms.TextBox textBoxOOBDataKey;
        private System.Windows.Forms.TextBox textBoxOOBDataAddr;
        private System.Windows.Forms.TextBox textBoxDiscoverAttributesStartAttrId;
        private System.Windows.Forms.TextBox textBoxRawDataCommandsProfileID;
        private System.Windows.Forms.TextBox textBoxRawDataCommandsSecurityMode;
        private System.Windows.Forms.TextBox textBoxRawDataCommandsRadius;
        private System.Windows.Forms.TextBox textBoxRawDataCommandsData;
        private System.Windows.Forms.TextBox textBoxRawDataCommandsClusterID;
        private System.Windows.Forms.TextBox textBoxRawDataCommandsDstEP;
        private System.Windows.Forms.TextBox textBoxRawDataCommandsSrcEP;
        private System.Windows.Forms.TextBox textBoxRawDataCommandsTargetAddr;
        private System.Windows.Forms.TextBox textBoxMgmtNwkUpdateNwkManagerAddr;
        private System.Windows.Forms.TextBox textBoxDiscoverCommandsMaxCommands;
        private System.Windows.Forms.TextBox textBoxDiscoverCommandsManuID;
        private System.Windows.Forms.TextBox textBoxDiscoverCommandsCommandID;
        private System.Windows.Forms.TextBox textBoxDiscoverCommandsClusterID;
        private System.Windows.Forms.TextBox textBoxDiscoverCommandsDstEP;
        private System.Windows.Forms.TextBox textBoxDiscoverCommandsSrcEP;
        private System.Windows.Forms.TextBox textBoxDiscoverCommandsTargetAddr;
        private System.Windows.Forms.TextBox textBoxMgmtNwkUpdateScanCount;
        private System.Windows.Forms.TextBox textBoxMgmtNwkUpdateScanDuration;
        private System.Windows.Forms.TextBox textBoxMgmtNwkUpdateChannelMask;
        private System.Windows.Forms.TextBox textBoxMgmtNwkUpdateTargetAddr;
        private System.Windows.Forms.TextBox textBoxManyToOneRouteRequesRadius;
        private System.Windows.Forms.TextBox textBoxReadReportConfigAttribID;
        private System.Windows.Forms.TextBox textBoxReadReportConfigClusterID;
        private System.Windows.Forms.TextBox textBoxReadReportConfigDstEP;
        private System.Windows.Forms.TextBox textBoxReadReportConfigSrcEP;
        private System.Windows.Forms.TextBox textBoxReadReportConfigTargetAddr;
        private System.Windows.Forms.TextBox textBoxWriteAttribManuID;
        private System.Windows.Forms.TextBox textBoxWriteAttribDataType;
        private System.Windows.Forms.TextBox textBoxReadAttribManuID;
        private System.Windows.Forms.TextBox textBoxWriteAttribData;
        private System.Windows.Forms.TextBox textBoxWriteAttribID;
        private System.Windows.Forms.TextBox textBoxWriteAttribClusterID;
        private System.Windows.Forms.TextBox textBoxWriteAttribDstEP;
        private System.Windows.Forms.TextBox textBoxWriteAttribSrcEP;
        private System.Windows.Forms.TextBox textBoxWriteAttribTargetAddr;
        private System.Windows.Forms.TextBox textBoxConfigReportChange;
        private System.Windows.Forms.TextBox textBoxConfigReportTimeOut;
        private System.Windows.Forms.TextBox textBoxConfigReportMaxInterval;
        private System.Windows.Forms.TextBox textBoxDiscoverAttributesMaxIDs;
        private System.Windows.Forms.TextBox textBoxDiscoverAttributesClusterID;
        private System.Windows.Forms.TextBox textBoxDiscoverAttributesDstEp;
        private System.Windows.Forms.TextBox textBoxDiscoverAttributesSrcEp;
        private System.Windows.Forms.TextBox textBoxDiscoverAttributesAddr;
        private System.Windows.Forms.TextBox textBoxReadAllAttribClusterID;
        private System.Windows.Forms.TextBox textBoxReadAllAttribDstEP;
        private System.Windows.Forms.TextBox textBoxReadAllAttribSrcEP;
        private System.Windows.Forms.TextBox textBoxReadAllAttribAddr;
        private System.Windows.Forms.TextBox textBoxConfigReportAttribType;
        private System.Windows.Forms.TextBox textBoxConfigReportMinInterval;
        private System.Windows.Forms.TextBox textBoxConfigReportAttribID;
        private System.Windows.Forms.TextBox textBoxConfigReportClusterID;
        private System.Windows.Forms.TextBox textBoxConfigReportDstEP;
        private System.Windows.Forms.TextBox textBoxConfigReportSrcEP;
        private System.Windows.Forms.TextBox textBoxConfigReportTargetAddr;
        private System.Windows.Forms.TextBox textBoxReadAttribCount;
        private System.Windows.Forms.TextBox textBoxReadAttribID1;
        private System.Windows.Forms.TextBox textBoxReadAttribClusterID;
        private System.Windows.Forms.TextBox textBoxReadAttribDstEP;
        private System.Windows.Forms.TextBox textBoxReadAttribSrcEP;
        private System.Windows.Forms.TextBox textBoxReadAttribTargetAddr;
        private System.Windows.Forms.Button buttonGeneralSendInstallCode;
        private System.Windows.Forms.Button buttonOOBCommissioningData;
        private System.Windows.Forms.ComboBox comboBoxRawDataCommandsAddrMode;
        private System.Windows.Forms.Button buttonRawDataSend;
        private System.Windows.Forms.ComboBox comboBoxDiscoverCommandsRxGen;
        private System.Windows.Forms.ComboBox comboBoxDiscoverAttributesExtended;
        private System.Windows.Forms.ComboBox comboBoxDiscoverCommandsManuSpecific;
        private System.Windows.Forms.ComboBox comboBoxDiscoverCommandsDirection;
        private System.Windows.Forms.ComboBox comboBoxDiscoverCommandsAddrMode;
        private System.Windows.Forms.Button buttonDiscoverCommands;
        private System.Windows.Forms.ComboBox comboBoxMgmtNwkUpdateAddrMode;
        private System.Windows.Forms.Button buttonMgmtNwkUpdate;
        private System.Windows.Forms.ComboBox comboBoxManyToOneRouteRequestCacheRoute;
        private System.Windows.Forms.Button buttonManyToOneRouteRequest;
        private System.Windows.Forms.ComboBox comboBoxReadReportConfigDirection;
        private System.Windows.Forms.ComboBox comboBoxReadReportConfigDirIsRx;
        private System.Windows.Forms.ComboBox comboBoxReadReportConfigAddrMode;
        private System.Windows.Forms.Button buttonReadReportConfig;
        private System.Windows.Forms.ComboBox comboBoxWriteAttribManuSpecific;
        private System.Windows.Forms.ComboBox comboBoxReadAttribManuSpecific;
        private System.Windows.Forms.ComboBox comboBoxConfigReportAddrMode;
        private System.Windows.Forms.ComboBox comboBoxWriteAttribDirection;
        private System.Windows.Forms.ComboBox comboBoxDiscoverAttributesDirection;
        private System.Windows.Forms.Button buttonDiscoverAttributes;
        private System.Windows.Forms.ComboBox comboBoxReadAllAttribDirection;
        private System.Windows.Forms.Button buttonReadAllAttrib;
        private System.Windows.Forms.ComboBox comboBoxConfigReportAttribDirection;
        private System.Windows.Forms.ComboBox comboBoxConfigReportDirection;
        private System.Windows.Forms.Button buttonConfigReport;
        private System.Windows.Forms.Button buttonWriteAttrib;
        private System.Windows.Forms.ComboBox comboBoxReadAttribDirection;
        private System.Windows.Forms.Button buttonReadAttrib;
        private System.Windows.Forms.TabPage tabPageDevice;
        private System.Windows.Forms.Button buttonCopyAddr;
        private System.Windows.Forms.Button buttonDiscoverDevices;
        private System.Windows.Forms.TextBox textBoxExtAddr;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBoxAddressList;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox comboBoxNciCmd;
        private System.Windows.Forms.Button buttonNciCmd;
        private System.Windows.Forms.TextBox textBoxPollInterval;
        private System.Windows.Forms.TextBox textBoxBindTargetExtAddr;
        private System.Windows.Forms.TextBox textBoxUserSetReqDescription;
        private System.Windows.Forms.TextBox textBoxUserSetReqAddr;
        private System.Windows.Forms.TextBox textBoxUserReqAddr;
        private System.Windows.Forms.TextBox textBoxRestoreNwkFrameCounter;
        private System.Windows.Forms.TextBox textBoxLeaveAddr;
        private System.Windows.Forms.TextBox textBoxRemoveChildAddr;
        private System.Windows.Forms.TextBox textBoxRemoveParentAddr;
        private System.Windows.Forms.TextBox textBoxMgmtLeaveExtAddr;
        private System.Windows.Forms.TextBox textBoxMgmtLeaveAddr;
        private System.Windows.Forms.TextBox textBoxUnBindDestEP;
        private System.Windows.Forms.TextBox textBoxUnBindDestAddr;
        private System.Windows.Forms.TextBox textBoxUnBindClusterID;
        private System.Windows.Forms.TextBox textBoxUnBindTargetEP;
        private System.Windows.Forms.TextBox textBoxUnBindTargetExtAddr;
        private System.Windows.Forms.TextBox textBoxBindDestEP;
        private System.Windows.Forms.TextBox textBoxBindDestAddr;
        private System.Windows.Forms.TextBox textBoxBindClusterID;
        private System.Windows.Forms.TextBox textBoxBindTargetEP;
        private System.Windows.Forms.TextBox textBoxLqiReqStartIndex;
        private System.Windows.Forms.TextBox textBoxLqiReqTargetAddr;
        private System.Windows.Forms.TextBox textBoxNwkAddrReqStartIndex;
        private System.Windows.Forms.TextBox textBoxNwkAddrReqExtAddr;
        private System.Windows.Forms.TextBox textBoxNwkAddrReqTargetAddr;
        private System.Windows.Forms.TextBox textBoxIeeeReqStartIndex;
        private System.Windows.Forms.TextBox textBoxIeeeReqAddr;
        private System.Windows.Forms.TextBox textBoxIeeeReqTargetAddr;
        private System.Windows.Forms.TextBox textBoxComplexReqAddr;
        private System.Windows.Forms.TextBox textBoxMatchReqOutputClusters;
        private System.Windows.Forms.TextBox textBoxMatchReqInputClusters;
        private System.Windows.Forms.TextBox textBoxMatchReqProfileID;
        private System.Windows.Forms.TextBox textBoxMatchReqNbrOutputClusters;
        private System.Windows.Forms.TextBox textBoxMatchReqNbrInputClusters;
        private System.Windows.Forms.TextBox textBoxMatchReqAddr;
        private System.Windows.Forms.TextBox textBoxActiveEpAddr;
        private System.Windows.Forms.TextBox textBoxPowerReqAddr;
        private System.Windows.Forms.TextBox textBoxSimpleReqEndPoint;
        private System.Windows.Forms.TextBox textBoxSimpleReqAddr;
        private System.Windows.Forms.TextBox textBoxNodeDescReq;
        private System.Windows.Forms.TextBox textBoxPermitJoinInterval;
        private System.Windows.Forms.TextBox textBoxPermitJoinAddr;
        private System.Windows.Forms.TextBox textBoxSetSecurityKeySeqNbr;
        private System.Windows.Forms.TextBox textBoxSetEPID;
        private System.Windows.Forms.TextBox textBoxSetCMSK;
        private System.Windows.Forms.Button buttonPollInterval;
        private System.Windows.Forms.Button buttonNWKState;
        private System.Windows.Forms.Button buttonDiscoveryOnly;
        private System.Windows.Forms.Button buttonUserSetReq;
        private System.Windows.Forms.Button buttonUserReq;
        private System.Windows.Forms.ComboBox comboBoxLeaveChildren;
        private System.Windows.Forms.ComboBox comboBoxLeaveReJoin;
        private System.Windows.Forms.Button buttonLeave;
        private System.Windows.Forms.Button buttonRemoveDevice;
        private System.Windows.Forms.Button buttonPermitJoinState;
        private System.Windows.Forms.Button buttonRestoreNwk;
        private System.Windows.Forms.Button buttonRecoverNwk;
        private System.Windows.Forms.ComboBox comboBoxMgmtLeaveChildren;
        private System.Windows.Forms.ComboBox comboBoxMgmtLeaveReJoin;
        private System.Windows.Forms.Button buttonMgmtLeave;
        private System.Windows.Forms.ComboBox comboBoxUnBindAddrMode;
        private System.Windows.Forms.Button buttonUnBind;
        private System.Windows.Forms.ComboBox comboBoxBindAddrMode;
        private System.Windows.Forms.Button buttonMgmtLqiReq;
        private System.Windows.Forms.Button buttonStartScan;
        private System.Windows.Forms.ComboBox comboBoxNwkAddrReqType;
        private System.Windows.Forms.ComboBox comboBoxIeeeReqType;
        private System.Windows.Forms.Button buttonComplexReq;
        private System.Windows.Forms.Button buttonMatchReq;
        private System.Windows.Forms.Button buttonActiveEpReq;
        private System.Windows.Forms.Button buttonPowerDescReq;
        private System.Windows.Forms.Button buttonSimpleDescReq;
        private System.Windows.Forms.Button buttonNodeDescReq;
        private System.Windows.Forms.Button buttonIeeeAddrReq;
        private System.Windows.Forms.Button buttonNwkAddrReq;
        private System.Windows.Forms.ComboBox comboBoxSecurityKey;
        private System.Windows.Forms.ComboBox comboBoxPermitJoinTCsignificance;
        private System.Windows.Forms.Button buttonPermitJoin;
        private System.Windows.Forms.ComboBox comboBoxSetKeyType;
        private System.Windows.Forms.ComboBox comboBoxSetKeyState;
        private System.Windows.Forms.ComboBox comboBoxSetType;
        private System.Windows.Forms.Button buttonStartNWK;
        private System.Windows.Forms.Button buttonBind;
        private System.Windows.Forms.Button buttonErasePD;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonSetDeviceType;
        private System.Windows.Forms.Button buttonSetSecurity;
        private System.Windows.Forms.Button buttonSetCMSK;
        private System.Windows.Forms.Button buttonSetEPID;
        private System.Windows.Forms.Button buttonGetVersion;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button buttonGeneralPrintExistInstallCode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.TabPage tabPage16;
        private System.Windows.Forms.ListView listViewEZLNTINFO;
        private System.Windows.Forms.ColumnHeader NwkAddr;
        private System.Windows.Forms.ColumnHeader MACAddr;
        private System.Windows.Forms.ColumnHeader Channel;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Ver;
        private System.Windows.Forms.CheckBox checkBoxEZLNTALL;
        private System.Windows.Forms.ColumnHeader COMIndex;
        private System.Windows.Forms.Button buttonPort;
        private System.Windows.Forms.Timer timerSCANCOM;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button buttonREFRESHCOM;
        private System.Windows.Forms.Button buttonEZLNTREMOVEGROUP;
        private System.Windows.Forms.Button buttonEZLNTADDGROUP;
        private System.Windows.Forms.TextBox textBoxREMOVEGROUP;
        private System.Windows.Forms.TextBox textBoxEZLNTADDGROUP;
        private System.Windows.Forms.Button buttonEZLNTVIEWGROUP;
        private System.Windows.Forms.Button buttonREMOVEGROUPALL;
        private System.Windows.Forms.ListView listViewEZLNTGROUP;
        private System.Windows.Forms.ColumnHeader nwkAddrJoined;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.TextBox textBoxEZLNTVIEW;
        private System.Windows.Forms.Button buttonEZLNTREADATTRIBUTE;
        private System.Windows.Forms.TextBox textBoxEZLNTTIMERINTERVAL;
        private System.Windows.Forms.Button buttonEZLNTSTOPREAD;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerReadAttribute;
        private System.Windows.Forms.TextBox textBoxEZLNTSETLOOP;
        private System.Windows.Forms.Button buttonEZLNTSETTIMER;
        private System.Windows.Forms.Button buttonPollSetLongPollInterval;
        private System.Windows.Forms.Button buttonPollSetShortPollInterval;
        private System.Windows.Forms.TextBox textBoxShortPollInterval;
        private System.Windows.Forms.TextBox textBoxPollLongPollInterval;
        private System.Windows.Forms.TextBox textBoxPollSetShortIntervalDstEndPointID;
        private System.Windows.Forms.TextBox textBoxPollSetShortIntervalSrcEndPointID;
        private System.Windows.Forms.TextBox textBoxPollSetLongIntervalDstEndPointID;
        private System.Windows.Forms.TextBox textBoxPollSetLongIntervalSrcEndPointID;
        private System.Windows.Forms.TextBox textBoxCheckInDstEndPointID;
        private System.Windows.Forms.TextBox textBoxPollCheckInSrcEndPointID;
        private System.Windows.Forms.TextBox textBoxPollSetShortIntervalAddress;
        private System.Windows.Forms.TextBox textBoxPollSetLongIntervalAddress;
        private System.Windows.Forms.TextBox textBoxPollCheckInAddress;
        private System.Windows.Forms.Button buttonEZLNTTONGGLE;
        private System.Windows.Forms.Button buttonEZLNTOFF;
        private System.Windows.Forms.Button buttonEZLNTON;
        private System.Windows.Forms.Button buttonEZLNTTONGGLESTOP;
        private System.Windows.Forms.Button buttonSECADDNEWNETKEY;
        private System.Windows.Forms.TextBox textBoxSECNEWNETKEY;
        private System.Windows.Forms.Button buttonSECSWITCHNETKEY;
        private System.Windows.Forms.TextBox textBoxSECNETKEYSEQ;
        private System.Windows.Forms.TextBox textBoxSECADDNETKEYSEQ;
        private System.Windows.Forms.Button buttonEZLNTBIND;
        private System.Windows.Forms.Button buttonEZLNTUNBIND;
        private System.Windows.Forms.TextBox textBoxEZLNTUNBINDCLUSTERID;
        private System.Windows.Forms.TextBox textBoxEZLNTBINDCLUSTERID;
        private System.Windows.Forms.ColumnHeader Command;
        private System.Windows.Forms.Button buttonEZLNTSENDCOMMAND;
        private System.Windows.Forms.TextBox textBoxEZLNTCOMMAND;
        private System.Windows.Forms.TabPage tabPage17;
        private System.Windows.Forms.Button buttonLNTREMOTELOAD;
        private System.Windows.Forms.Button buttonEZLNTWRITEATTRIBUTE;
        private System.Windows.Forms.Button buttonEZLNTREADRPRT;
        private System.Windows.Forms.Button buttonEZLNTCONFIGRPRT;
        private System.Windows.Forms.Button buttonEZLNTLEAVE;
        private System.Windows.Forms.Button buttonEZLNTRESET;
        private System.Windows.Forms.Button buttonEZLNTIDENTIFY;
        private System.Windows.Forms.Button buttonEZLNTMOVETOLEVLEL;
        private System.Windows.Forms.Button buttonEZLNTMOVETOTEMP;
        private System.Windows.Forms.Button buttonEZLNTMOVETOSAT;
        private System.Windows.Forms.Button buttonEZLNTMOVETOCOLOR;
        private System.Windows.Forms.Button buttonEZLNTMOVETOHUE;
        private System.Windows.Forms.TextBox textBoxEZLNTATTRIBUTEID;
        private System.Windows.Forms.TextBox textBoxEZLNTREADCLUSTERID;
        private System.Windows.Forms.TextBox textBoxEZLNTWRITEATTRIBUTEATTRIBUTEID;
        private System.Windows.Forms.TextBox textBoxEZLNTWRITEATTRIBUTECLUSTERID;
        private System.Windows.Forms.TextBox textBoxEZLNTWRITEATTRIBUTEATTRIBUTEDATATYPE;
        private System.Windows.Forms.TextBox textBoxEZLNTWRITEATTRIBUTEDATA;
        private System.Windows.Forms.ComboBox comboBoxEZLNTLEAVECHILDREN;
        private System.Windows.Forms.ComboBox comboBoxEZLNTLEAVEREJOIN;
        private System.Windows.Forms.TextBox textBoxEZLNTCONFIGRPRTCLUSTERID;
        private System.Windows.Forms.TextBox textBoxEZLNTCONFIGRPRTTYPE;
        private System.Windows.Forms.TextBox textBoxEZLNTCONFIGRPRTATTRIBID;
        private System.Windows.Forms.TextBox textBoxEZLNTCONFIGRPRTMININTERVAL;
        private System.Windows.Forms.TextBox textBoxEZLNTCONFIGRPRTMAXINTERVAL;
        private System.Windows.Forms.TextBox textBoxEZLNTCONFIGRPRTTIMEOUT;
        private System.Windows.Forms.TextBox textBoxEZLNTCONFIGRPRTCHANGE;
        private System.Windows.Forms.TextBox textBoxEZLNTREADRPRTATTRIBUTEID;
        private System.Windows.Forms.TextBox textBoxEZLNTREADRPRTCLUSTERID;
        private System.Windows.Forms.TextBox textBoxEZLNTIDENTIFYTIME;
        private System.Windows.Forms.TextBox textBoxEZLNTTEMPTIME;
        private System.Windows.Forms.TextBox textBoxEZLNTTEMP;
        private System.Windows.Forms.TextBox textBoxEZLNTSATTIME;
        private System.Windows.Forms.TextBox textBoxEZLNTSAT;
        private System.Windows.Forms.TextBox textBoxCOLORTIME;
        private System.Windows.Forms.TextBox textBoxEZLNTCOLORY;
        private System.Windows.Forms.TextBox textBoxEZLNTCOLORX;
        private System.Windows.Forms.TextBox textBoxEZLNTHUEDIR;
        private System.Windows.Forms.TextBox textBoxEZLNTHUE;
        private System.Windows.Forms.TextBox textBoxEZLNTLEVELTIME;
        private System.Windows.Forms.TextBox textBoxEZLNTLEVEL;
        private System.Windows.Forms.Button buttonEZLNTIDENTIFYSTOP;
        private System.Windows.Forms.Button buttonEZLNTLEVLELSTOP;
        private System.Windows.Forms.ComboBox comboBoxEZLNTLEVELWITHONOFF;
        private System.Windows.Forms.TextBox textBoxEZLNTSETSTEP;
        private System.Windows.Forms.TextBox textBoxEZLNTSETDIR;
        private System.Windows.Forms.Button buttonEZLNTHUESTOP;
        private System.Windows.Forms.Button buttonTEMPLOOPSTOP;
        private System.Windows.Forms.Button buttonEZLNTSATLOOPSTOP;
        private System.Windows.Forms.Button buttonEZLNTCOLORLOOPSTOP;
        private System.Windows.Forms.TextBox textBoxEZLNTSETINTERVALMAX;
        private System.Windows.Forms.TextBox textBoxEZLNTHUETIME;
        private System.Windows.Forms.TextBox textBoxLNTSETPARAMAXINTERVAL;
        private System.Windows.Forms.Button buttonLNTTEMPSTOP;
        private System.Windows.Forms.Button buttonLNTSTOPCOLOR;
        private System.Windows.Forms.Button buttonLNTCOLORSTOP;
        private System.Windows.Forms.Button buttonHUESTOP;
        private System.Windows.Forms.TextBox textBoxLNTSETPARADIR;
        private System.Windows.Forms.TextBox textBoxLNTSETPARASTEP;
        private System.Windows.Forms.ComboBox comboBoxLEVELMOVEWITHONOFF;
        private System.Windows.Forms.Button buttonLNTSTOPLEVEL;
        private System.Windows.Forms.Button buttonLNTSTOPIDENTIFY;
        private System.Windows.Forms.TextBox textBoxLNTTEMPTIME;
        private System.Windows.Forms.TextBox textBoxLNTTEMP;
        private System.Windows.Forms.TextBox textBoxLNTSATTIME;
        private System.Windows.Forms.TextBox textBoxLNTSAT;
        private System.Windows.Forms.TextBox textBoxLNTCOLORTIME;
        private System.Windows.Forms.TextBox textBoxLNTCOLORY;
        private System.Windows.Forms.TextBox textBoxLNTCOLORX;
        private System.Windows.Forms.TextBox textBoxLNTHUETIME;
        private System.Windows.Forms.TextBox textBoxLNTHUEDIR;
        private System.Windows.Forms.TextBox textBoxLNTHUE;
        private System.Windows.Forms.TextBox textBoxLNTLEVELTIME;
        private System.Windows.Forms.TextBox textBoxLNTLEVEL;
        private System.Windows.Forms.TextBox textBoxLNTIDENTIFYTIME;
        private System.Windows.Forms.TextBox textBoxLNTREADRPRTATTRID;
        private System.Windows.Forms.TextBox textBoxLNTREADRPRTCLUSTERID;
        private System.Windows.Forms.TextBox textBoxLNTCONFIGRPRTCHANGE;
        private System.Windows.Forms.TextBox textBoxLNTCONFIGRPRTTIMEOUT;
        private System.Windows.Forms.TextBox textBoxCONFIGRPRTMAXRPRTINTERVAL;
        private System.Windows.Forms.TextBox textBoxCONFIGRPRTMININTERVAL;
        private System.Windows.Forms.TextBox textBoxCONFIGRPRTATTRID;
        private System.Windows.Forms.TextBox textBoxLNTCONFIGRPRTTYPE;
        private System.Windows.Forms.TextBox textBoxLNTCONFIGRPRTCLUSTERID;
        private System.Windows.Forms.ComboBox comboBoxLNTLEAVEWITHCHILDREN;
        private System.Windows.Forms.ComboBox comboBoxLNTLEAVEREJOIN;
        private System.Windows.Forms.TextBox textBoxLNTWRITEATTRDATA;
        private System.Windows.Forms.TextBox textBoxLNTWRITEATTRDATATYPE;
        private System.Windows.Forms.TextBox textBoxLNTWRITEATTRATTRID;
        private System.Windows.Forms.TextBox textBoxLNTREADATTRATTRIBUTECOUNT;
        private System.Windows.Forms.TextBox textBoxLNTREADATTRATTRIBUTEID;
        private System.Windows.Forms.TextBox textBoxLNTREADATTRCLUSTERID;
        private System.Windows.Forms.Button buttonLNTTEMP;
        private System.Windows.Forms.Button buttonLNTSAT;
        private System.Windows.Forms.Button buttonLNTCOLOR;
        private System.Windows.Forms.Button buttonLNTHUE;
        private System.Windows.Forms.Button buttonLNTLEVEL;
        private System.Windows.Forms.Button buttonLNTIDENTIFY;
        private System.Windows.Forms.Button buttonLNTRESET;
        private System.Windows.Forms.Button buttonLNTREADRPRT;
        private System.Windows.Forms.Button buttonLNTCONFIGRPRT;
        private System.Windows.Forms.Button buttonLNTLEAVE;
        private System.Windows.Forms.Button buttonLNTWRITEATTRIBUTE;
        private System.Windows.Forms.TextBox textBoxLNTUNBINDIEEEADDR;
        private System.Windows.Forms.TextBox textBoxLNTBINDIEEEADDR;
        private System.Windows.Forms.Button buttonLNTUNBIND;
        private System.Windows.Forms.Button buttonLNTBIND;
        private System.Windows.Forms.Button buttonLNTSTOPTONGGLE;
        private System.Windows.Forms.Button buttonLNTTONGGLE;
        private System.Windows.Forms.Button buttonLNTOFF;
        private System.Windows.Forms.Button buttonLNTON;
        private System.Windows.Forms.TextBox textBoxLNTSETLOOP;
        private System.Windows.Forms.Button buttonLNTSETPARA;
        private System.Windows.Forms.Button buttonLNTSTOPTEADATTRIBUTE;
        private System.Windows.Forms.TextBox textBoxLNTSETPARAMININTERVAL;
        private System.Windows.Forms.Button buttonLNTREADATTRIBUTE;
        private System.Windows.Forms.TextBox textBoxLNTVIEWGROUPADDRESS;
        private System.Windows.Forms.Button buttonLNTREMOVEALL;
        private System.Windows.Forms.Button buttonLNTVIEWGROUP;
        private System.Windows.Forms.TextBox textBoxLNTREMOVEGROUPADDRESS;
        private System.Windows.Forms.TextBox textBoxLNTADDGROUPADDR;
        private System.Windows.Forms.Button buttonLNTREMOVEGROUP;
        private System.Windows.Forms.Button buttonLNTADDGROUP;
        private System.Windows.Forms.TextBox textBoxLNTSENDCOMMAND;
        private System.Windows.Forms.Button buttonLNTSENDCOMMAND;
        private System.Windows.Forms.CheckBox checkBoxLNTALL;
        private System.Windows.Forms.ListView listViewLNTGROUPINFO;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ListView listViewLNTCOMINFO;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.CheckBox checkBoxLNTGROUPALL;
        private System.Windows.Forms.CheckBox checkBoxEZLNTGROUPALL;
        private System.Windows.Forms.TextBox textBoxLNTWRITEATTRCLUSTERID;
        private System.Windows.Forms.Button buttonLNTSOCKETCLIENT;
        private System.Windows.Forms.Button buttonLNTSOCKETSEVER;
        private System.Windows.Forms.TextBox textBoxLNTSOCKETSEVERIP;
        private System.Windows.Forms.TextBox textBoxLNTSOCKETCLIENTIP;
        private System.Windows.Forms.Button buttonEZLNTPERMIT;
        private System.Windows.Forms.Button buttonEZLNTDISABLEPERMIT;
        private System.Windows.Forms.Button buttonLNTDISABLEPERMIT;
        private System.Windows.Forms.Button buttonLNTPERMIT;
        private System.Windows.Forms.Button buttonWZLNTPROFRAMCMD;
        private System.Windows.Forms.Button buttonEZLNTLOADREMOTE;
        private System.Windows.Forms.ColumnHeader Loc;
        private System.Windows.Forms.ColumnHeader Chip;
        private System.Windows.Forms.Button buttonEZLNTSAVELOCAL;
        private System.Windows.Forms.ColumnHeader Profile;
        private System.Windows.Forms.ColumnHeader Loca;
        private System.Windows.Forms.Label labelEZLNTUNICAST;
        private System.Windows.Forms.Button buttonEZLNTBROADSTOPTONGGLE;
        private System.Windows.Forms.Button buttonEZLNTBROADTONGGLE;
        private System.Windows.Forms.Button buttonEZLNTBROADOFF;
        private System.Windows.Forms.Button buttonEZLNTBROADON;
        private System.Windows.Forms.Label labelEZLNTBROADCAST;
        private System.Windows.Forms.ComboBox comboBoxEZLNTUNICAST;
        private System.Windows.Forms.TextBox textBoxEZLNTSOCKETSEVERIP;
        private System.Windows.Forms.TextBox textBoxEZLNTSOCKETCLIENTIP;
        private System.Windows.Forms.Button buttonEZLNTSOCKETSEVER;
        private System.Windows.Forms.Button buttonEZLNTSOCKETCLIENT;
        private System.Windows.Forms.Button buttonSOCKETCLIENTTEST;
        private System.Windows.Forms.Button buttonSOCKETSEVERTEST;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader PANID;
        private System.Windows.Forms.TabPage tabPage18;
        private System.Windows.Forms.ListView listViewLNTGWINFO;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.Button buttonLNTGWDBGPORT;
        private System.Windows.Forms.Button buttonLNTFGWDISPERMIT;
        private System.Windows.Forms.Button buttonLNTGWPERMMIT;
        private System.Windows.Forms.CheckBox checkBoxLNTGWALL;
        private System.Windows.Forms.TextBox textBoxLNTGWSETINTERVALMAX;
        private System.Windows.Forms.Button buttonLNTGWSTOPMOVETEMP;
        private System.Windows.Forms.Button buttonLNTGWSTOPMOVESAT;
        private System.Windows.Forms.Button buttonLNTGWSTOPMOVECOLOR;
        private System.Windows.Forms.Button buttonLNTGWSTOPMOVEHUE;
        private System.Windows.Forms.TextBox textBoxLNTGWSETDIR;
        private System.Windows.Forms.TextBox textBoxLNTGWSETSTEP;
        private System.Windows.Forms.ComboBox comboBoxLNTGWLEVELWITHONOFF;
        private System.Windows.Forms.Button buttonLNTGWSTOPMOVELEVEL;
        private System.Windows.Forms.Button buttonLNTGWSTOPIDENTIFY;
        private System.Windows.Forms.TextBox textBoxLNTGWTEMPTIME;
        private System.Windows.Forms.TextBox textBoxLNTGWTEMP;
        private System.Windows.Forms.TextBox textBoxLNTGWSATTIME;
        private System.Windows.Forms.TextBox textBoxLNTGWSAT;
        private System.Windows.Forms.TextBox textBoxLNTGWCOLORTIME;
        private System.Windows.Forms.TextBox textBoxLNTGWCOLORY;
        private System.Windows.Forms.TextBox textBoxLNTGWCOLORX;
        private System.Windows.Forms.TextBox textBoxLNTGWHUETIME;
        private System.Windows.Forms.TextBox textBoxLNTGWHUEDIR;
        private System.Windows.Forms.TextBox textBoxLNTGWHUE;
        private System.Windows.Forms.TextBox textBoxLNTGWLEVELTIME;
        private System.Windows.Forms.TextBox textBoxLNTGWLEVEL;
        private System.Windows.Forms.TextBox textBoxLNTGWIDENTIFYTIME;
        private System.Windows.Forms.TextBox textBoxLNTGWREADRPRTATTRIBUTEID;
        private System.Windows.Forms.TextBox textBoxLNTGWREADRPRTCLUSTERID;
        private System.Windows.Forms.TextBox textBoxLNTGWCONFIGRPRTCHANGE;
        private System.Windows.Forms.TextBox textBoxLNTGWCONFIGRPRTTIMEOUT;
        private System.Windows.Forms.TextBox textBoxLNTGWCONFIGRPRTMAXINTERVAL;
        private System.Windows.Forms.TextBox textBoxLNTGWCONFIGRPRTMININTERVAL;
        private System.Windows.Forms.TextBox textBoxLNTGWCONFIGRPRTTYPE;
        private System.Windows.Forms.TextBox textBoxLNTGWCONFIGRPRTATTRIBID;
        private System.Windows.Forms.TextBox textBoxLNTGWCONFIGRPRTCLUSTERID;
        private System.Windows.Forms.ComboBox comboBoxLNTGWLEAVECHILDREN;
        private System.Windows.Forms.ComboBox comboBoxLNTGWLEAVEREJOIN;
        private System.Windows.Forms.TextBox textBoxLNTGWWRITEATTRIBUTEDATA;
        private System.Windows.Forms.TextBox textBoxLNTGWWRITEATTRIBUTEATTRIBUTEDATATYPE;
        private System.Windows.Forms.TextBox textBoxLNTGWWRITEATTRIBUTEATTRIBUTEID;
        private System.Windows.Forms.TextBox textBoxLNTGWWRITEATTRIBUTECLUSTERID;
        private System.Windows.Forms.TextBox textBoxLNTGWATTRIBUTEID;
        private System.Windows.Forms.TextBox textBoxLNTGWREADCLUSTERID;
        private System.Windows.Forms.Button buttonLNTGWMOVETEMP;
        private System.Windows.Forms.Button buttonLNTGWMOVESAT;
        private System.Windows.Forms.Button buttonLNTGWMOVECOLOR;
        private System.Windows.Forms.Button buttonLNTGWMOVEHUE;
        private System.Windows.Forms.Button buttonLNTGWMOVELEVEL;
        private System.Windows.Forms.Button buttonLNTGWIDENTIFY;
        private System.Windows.Forms.Button buttonLNTGWRESET;
        private System.Windows.Forms.Button buttonLNTGWREADRPRT;
        private System.Windows.Forms.Button buttonLNTGWCONFIGRPRT;
        private System.Windows.Forms.Button buttonLNTGWLEAVE;
        private System.Windows.Forms.Button buttonLNTGWWRITE;
        private System.Windows.Forms.TextBox textBoxLNTGWUNBINDCLUSTERID;
        private System.Windows.Forms.TextBox textBoxLNTGWBINDCLUSTERID;
        private System.Windows.Forms.Button buttonLNTGWUNBIND;
        private System.Windows.Forms.Button buttonLNTGWBIND;
        private System.Windows.Forms.Button buttonLNTGWSTOPTONGGLE;
        private System.Windows.Forms.Button buttonLNTGWTONGGLE;
        private System.Windows.Forms.Button buttonLNTGWOFF;
        private System.Windows.Forms.Button buttonLNTGWON;
        private System.Windows.Forms.TextBox textBoxLNTGWSETLOOP;
        private System.Windows.Forms.Button buttonLNTGWSET;
        private System.Windows.Forms.Button buttonLNTGWSTOPREAD;
        private System.Windows.Forms.TextBox textBoxLNTGWTIMERINTERVAL;
        private System.Windows.Forms.Button buttonLNTGWREAD;
        private System.Windows.Forms.TextBox textBoxLNTGWVIEW;
        private System.Windows.Forms.Button buttonLNTGWREMOVEGROUPALL;
        private System.Windows.Forms.Button buttonLNTGWVIEWGROUP;
        private System.Windows.Forms.TextBox textBoxLNTGWREMOVEGROUP;
        private System.Windows.Forms.TextBox textBoxLNTGWADDGROUP;
        private System.Windows.Forms.Button buttonLNTGWREMOVEGROUP;
        private System.Windows.Forms.Button buttonLNTGWADDGROUP;
        private System.Windows.Forms.ListView listViewLNTGWGROUPINFO;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.TextBox textBoxLNTGWSENDCMD;
        private System.Windows.Forms.Button buttonLNTGWSENDCMD;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ComboBox comboBoxLNTGWUNICAST;
        private System.Windows.Forms.Label labelLNTGWUNICAST;
        private System.Windows.Forms.Label labelLNTGWBROADCAST;
        private System.Windows.Forms.Button buttonLNTGWBROADSTOPTONGGLE;
        private System.Windows.Forms.Button buttonLNTGWBROADTONGGLE;
        private System.Windows.Forms.Button buttonLNTGWBROADOFF;
        private System.Windows.Forms.Button buttonLNTGWBROADON;
        private System.Windows.Forms.Label labelLNTGWLOOPREMAIN;
        private System.Windows.Forms.TextBox textBoxLNTGWLOOPREMAIN;
        private System.Windows.Forms.Label labelEZLNTLOOPREMAIN;
        private System.Windows.Forms.TextBox textBoxEZLNTLOOPREMAIN;
        private System.Windows.Forms.Button buttonEZLNTSTOPONOFFLOOP;
        private System.Windows.Forms.Button buttonEZLNTONOFFLOOP;
        private System.Windows.Forms.Button buttonLNTGWSTOPONOFFLOOP;
        private System.Windows.Forms.Button buttonLNTGWONOFFLOOP;
        private System.Windows.Forms.Button buttonEZLNTSOCKETGETIP;
    }
}

