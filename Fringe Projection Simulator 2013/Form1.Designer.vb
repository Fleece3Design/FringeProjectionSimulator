<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Open3DModelTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResolutionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.X480TSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.X600TSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.X768TSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeamerSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeamerLocTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeamerTargetTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeamerParTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeamerResolutionTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.BeamerShowTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.CameraSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LocationTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.TargetTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ParametersTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.GratingSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowGratingSettingsTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShaderTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetShaderTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.EnableSaveTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.MeasurementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetFrequencyTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetphaseShiftTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.TakeSingleShotTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoiréToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GratingTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetGratingParametersTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetGratingResolutionTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetGratingFrequencyTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewHelpTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TSL1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSL2 = New System.Windows.Forms.ToolStripLabel()
        Me.tss2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSL3 = New System.Windows.Forms.ToolStripLabel()
        Me.tss3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSL4 = New System.Windows.Forms.ToolStripLabel()
        Me.GlC1 = New OpenTK.GLControl()
        Me.btnStartMeasurement = New Artesis_Button001.btnArtesis001()
        Me.btnReadSettings = New Artesis_Button001.btnArtesis001()
        Me.btnOpenModel = New Artesis_Button001.btnArtesis001()
        Me.btnShader = New Artesis_Button001.btnArtesis001()
        Me.btnSingleShot = New Artesis_Button001.btnArtesis001()
        Me.btnSetPhaseShift = New Artesis_Button001.btnArtesis001()
        Me.btnSetFrequency = New Artesis_Button001.btnArtesis001()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.MeasurementToolStripMenuItem, Me.MoiréToolStripMenuItem, Me.HelpToolStripMenuItem, Me.ToolStripMenuItem7})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1019, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Open3DModelTSMI, Me.ToolStripMenuItem2, Me.ExitTSMI})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'Open3DModelTSMI
        '
        Me.Open3DModelTSMI.Name = "Open3DModelTSMI"
        Me.Open3DModelTSMI.Size = New System.Drawing.Size(159, 22)
        Me.Open3DModelTSMI.Text = "&Open 3D-model"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(156, 6)
        '
        'ExitTSMI
        '
        Me.ExitTSMI.Name = "ExitTSMI"
        Me.ExitTSMI.Size = New System.Drawing.Size(159, 22)
        Me.ExitTSMI.Text = "&Exit"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResolutionToolStripMenuItem, Me.BeamerSettingsToolStripMenuItem, Me.CameraSettingsToolStripMenuItem, Me.GratingSettingsToolStripMenuItem, Me.ShaderTSMI, Me.SetShaderTSMI, Me.EnableSaveTSMI})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "&Settings"
        '
        'ResolutionToolStripMenuItem
        '
        Me.ResolutionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.X480TSMI, Me.X600TSMI, Me.X768TSMI})
        Me.ResolutionToolStripMenuItem.Name = "ResolutionToolStripMenuItem"
        Me.ResolutionToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.ResolutionToolStripMenuItem.Text = "&Resolution"
        '
        'X480TSMI
        '
        Me.X480TSMI.Checked = True
        Me.X480TSMI.CheckOnClick = True
        Me.X480TSMI.CheckState = System.Windows.Forms.CheckState.Checked
        Me.X480TSMI.Name = "X480TSMI"
        Me.X480TSMI.Size = New System.Drawing.Size(121, 22)
        Me.X480TSMI.Text = "&640x480"
        '
        'X600TSMI
        '
        Me.X600TSMI.CheckOnClick = True
        Me.X600TSMI.Name = "X600TSMI"
        Me.X600TSMI.Size = New System.Drawing.Size(121, 22)
        Me.X600TSMI.Text = "&800x600"
        '
        'X768TSMI
        '
        Me.X768TSMI.CheckOnClick = True
        Me.X768TSMI.Name = "X768TSMI"
        Me.X768TSMI.Size = New System.Drawing.Size(121, 22)
        Me.X768TSMI.Text = "&1024x768"
        '
        'BeamerSettingsToolStripMenuItem
        '
        Me.BeamerSettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BeamerLocTSMI, Me.BeamerTargetTSMI, Me.BeamerParTSMI, Me.BeamerResolutionTSMI, Me.ToolStripMenuItem5, Me.BeamerShowTSMI})
        Me.BeamerSettingsToolStripMenuItem.Name = "BeamerSettingsToolStripMenuItem"
        Me.BeamerSettingsToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.BeamerSettingsToolStripMenuItem.Text = "Beamer settin&gs"
        '
        'BeamerLocTSMI
        '
        Me.BeamerLocTSMI.Name = "BeamerLocTSMI"
        Me.BeamerLocTSMI.Size = New System.Drawing.Size(152, 22)
        Me.BeamerLocTSMI.Text = "Set &Location"
        '
        'BeamerTargetTSMI
        '
        Me.BeamerTargetTSMI.Name = "BeamerTargetTSMI"
        Me.BeamerTargetTSMI.Size = New System.Drawing.Size(152, 22)
        Me.BeamerTargetTSMI.Text = "Set Target"
        '
        'BeamerParTSMI
        '
        Me.BeamerParTSMI.Name = "BeamerParTSMI"
        Me.BeamerParTSMI.Size = New System.Drawing.Size(152, 22)
        Me.BeamerParTSMI.Text = "Set &Parameters"
        '
        'BeamerResolutionTSMI
        '
        Me.BeamerResolutionTSMI.Name = "BeamerResolutionTSMI"
        Me.BeamerResolutionTSMI.Size = New System.Drawing.Size(152, 22)
        Me.BeamerResolutionTSMI.Text = "Set Resolution"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(149, 6)
        '
        'BeamerShowTSMI
        '
        Me.BeamerShowTSMI.Name = "BeamerShowTSMI"
        Me.BeamerShowTSMI.Size = New System.Drawing.Size(152, 22)
        Me.BeamerShowTSMI.Text = "Sh&ow"
        '
        'CameraSettingsToolStripMenuItem
        '
        Me.CameraSettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LocationTSMI, Me.TargetTSMI, Me.ParametersTSMI, Me.ToolStripMenuItem4, Me.ShowTSMI})
        Me.CameraSettingsToolStripMenuItem.Name = "CameraSettingsToolStripMenuItem"
        Me.CameraSettingsToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.CameraSettingsToolStripMenuItem.Text = "&Camera settings"
        '
        'LocationTSMI
        '
        Me.LocationTSMI.Name = "LocationTSMI"
        Me.LocationTSMI.Size = New System.Drawing.Size(152, 22)
        Me.LocationTSMI.Text = "Set &Location"
        '
        'TargetTSMI
        '
        Me.TargetTSMI.Name = "TargetTSMI"
        Me.TargetTSMI.Size = New System.Drawing.Size(152, 22)
        Me.TargetTSMI.Text = "Set &Target"
        '
        'ParametersTSMI
        '
        Me.ParametersTSMI.Name = "ParametersTSMI"
        Me.ParametersTSMI.Size = New System.Drawing.Size(152, 22)
        Me.ParametersTSMI.Text = "Set &Parameters"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(149, 6)
        '
        'ShowTSMI
        '
        Me.ShowTSMI.Name = "ShowTSMI"
        Me.ShowTSMI.Size = New System.Drawing.Size(152, 22)
        Me.ShowTSMI.Text = "Sh&ow"
        '
        'GratingSettingsToolStripMenuItem
        '
        Me.GratingSettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowGratingSettingsTSMI})
        Me.GratingSettingsToolStripMenuItem.Name = "GratingSettingsToolStripMenuItem"
        Me.GratingSettingsToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.GratingSettingsToolStripMenuItem.Text = "Grating setti&ngs"
        '
        'ShowGratingSettingsTSMI
        '
        Me.ShowGratingSettingsTSMI.Name = "ShowGratingSettingsTSMI"
        Me.ShowGratingSettingsTSMI.Size = New System.Drawing.Size(103, 22)
        Me.ShowGratingSettingsTSMI.Text = "Sh&ow"
        '
        'ShaderTSMI
        '
        Me.ShaderTSMI.CheckOnClick = True
        Me.ShaderTSMI.Name = "ShaderTSMI"
        Me.ShaderTSMI.Size = New System.Drawing.Size(159, 22)
        Me.ShaderTSMI.Text = "Enable S&hader"
        '
        'SetShaderTSMI
        '
        Me.SetShaderTSMI.Name = "SetShaderTSMI"
        Me.SetShaderTSMI.Size = New System.Drawing.Size(159, 22)
        Me.SetShaderTSMI.Text = "Set Sha&der"
        '
        'EnableSaveTSMI
        '
        Me.EnableSaveTSMI.Checked = True
        Me.EnableSaveTSMI.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EnableSaveTSMI.Name = "EnableSaveTSMI"
        Me.EnableSaveTSMI.Size = New System.Drawing.Size(159, 22)
        Me.EnableSaveTSMI.Text = "Disable Sa&ving"
        '
        'MeasurementToolStripMenuItem
        '
        Me.MeasurementToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetFrequencyTSMI, Me.SetphaseShiftTSMI, Me.TakeSingleShotTSMI})
        Me.MeasurementToolStripMenuItem.Name = "MeasurementToolStripMenuItem"
        Me.MeasurementToolStripMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.MeasurementToolStripMenuItem.Text = "&Measurement"
        '
        'SetFrequencyTSMI
        '
        Me.SetFrequencyTSMI.Name = "SetFrequencyTSMI"
        Me.SetFrequencyTSMI.Size = New System.Drawing.Size(159, 22)
        Me.SetFrequencyTSMI.Text = "S&et frequency"
        '
        'SetphaseShiftTSMI
        '
        Me.SetphaseShiftTSMI.Name = "SetphaseShiftTSMI"
        Me.SetphaseShiftTSMI.Size = New System.Drawing.Size(159, 22)
        Me.SetphaseShiftTSMI.Text = "Set &phase shift"
        '
        'TakeSingleShotTSMI
        '
        Me.TakeSingleShotTSMI.Name = "TakeSingleShotTSMI"
        Me.TakeSingleShotTSMI.Size = New System.Drawing.Size(159, 22)
        Me.TakeSingleShotTSMI.Text = "&Take single shot"
        '
        'MoiréToolStripMenuItem
        '
        Me.MoiréToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GratingTSMI, Me.SetGratingParametersTSMI, Me.SetGratingResolutionTSMI, Me.SetGratingFrequencyTSMI})
        Me.MoiréToolStripMenuItem.Name = "MoiréToolStripMenuItem"
        Me.MoiréToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.MoiréToolStripMenuItem.Text = "M&oiré"
        '
        'GratingTSMI
        '
        Me.GratingTSMI.Name = "GratingTSMI"
        Me.GratingTSMI.Size = New System.Drawing.Size(193, 22)
        Me.GratingTSMI.Text = "&Enable grating"
        '
        'SetGratingParametersTSMI
        '
        Me.SetGratingParametersTSMI.Name = "SetGratingParametersTSMI"
        Me.SetGratingParametersTSMI.Size = New System.Drawing.Size(193, 22)
        Me.SetGratingParametersTSMI.Text = "Set &grating parameters"
        '
        'SetGratingResolutionTSMI
        '
        Me.SetGratingResolutionTSMI.Name = "SetGratingResolutionTSMI"
        Me.SetGratingResolutionTSMI.Size = New System.Drawing.Size(193, 22)
        Me.SetGratingResolutionTSMI.Text = "Set grating resolution"
        '
        'SetGratingFrequencyTSMI
        '
        Me.SetGratingFrequencyTSMI.Name = "SetGratingFrequencyTSMI"
        Me.SetGratingFrequencyTSMI.Size = New System.Drawing.Size(193, 22)
        Me.SetGratingFrequencyTSMI.Text = "Set grating frequency"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewHelpTSMI, Me.ToolStripMenuItem6, Me.AboutTSMI})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'ViewHelpTSMI
        '
        Me.ViewHelpTSMI.Name = "ViewHelpTSMI"
        Me.ViewHelpTSMI.Size = New System.Drawing.Size(127, 22)
        Me.ViewHelpTSMI.Text = "&View Help"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(124, 6)
        '
        'AboutTSMI
        '
        Me.AboutTSMI.Name = "AboutTSMI"
        Me.AboutTSMI.Size = New System.Drawing.Size(127, 22)
        Me.AboutTSMI.Text = "&About"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(12, 20)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSL1, Me.ToolStripSeparator1, Me.TSL2, Me.tss2, Me.TSL3, Me.tss3, Me.TSL4})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 634)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1019, 25)
        Me.ToolStrip1.TabIndex = 7
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TSL1
        '
        Me.TSL1.Name = "TSL1"
        Me.TSL1.Size = New System.Drawing.Size(32, 22)
        Me.TSL1.Text = "TSL1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TSL2
        '
        Me.TSL2.Name = "TSL2"
        Me.TSL2.Size = New System.Drawing.Size(32, 22)
        Me.TSL2.Text = "TSL2"
        '
        'tss2
        '
        Me.tss2.Name = "tss2"
        Me.tss2.Size = New System.Drawing.Size(6, 25)
        '
        'TSL3
        '
        Me.TSL3.Name = "TSL3"
        Me.TSL3.Size = New System.Drawing.Size(32, 22)
        Me.TSL3.Text = "TSL3"
        '
        'tss3
        '
        Me.tss3.Name = "tss3"
        Me.tss3.Size = New System.Drawing.Size(6, 25)
        '
        'TSL4
        '
        Me.TSL4.Name = "TSL4"
        Me.TSL4.Size = New System.Drawing.Size(32, 22)
        Me.TSL4.Text = "TSL4"
        '
        'GlC1
        '
        Me.GlC1.BackColor = System.Drawing.Color.DimGray
        Me.GlC1.Location = New System.Drawing.Point(6, 29)
        Me.GlC1.Name = "GlC1"
        Me.GlC1.Size = New System.Drawing.Size(800, 600)
        Me.GlC1.TabIndex = 8
        Me.GlC1.VSync = False
        '
        'btnStartMeasurement
        '
        Me.btnStartMeasurement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStartMeasurement.BackColor = System.Drawing.Color.Transparent
        Me.btnStartMeasurement.ForeColor = System.Drawing.Color.White
        Me.btnStartMeasurement.Location = New System.Drawing.Point(819, 200)
        Me.btnStartMeasurement.Name = "btnStartMeasurement"
        Me.btnStartMeasurement.Size = New System.Drawing.Size(188, 20)
        Me.btnStartMeasurement.TabIndex = 27
        Me.btnStartMeasurement.TextValue = "Start Measurement"
        '
        'btnReadSettings
        '
        Me.btnReadSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReadSettings.BackColor = System.Drawing.Color.Transparent
        Me.btnReadSettings.ForeColor = System.Drawing.Color.White
        Me.btnReadSettings.Location = New System.Drawing.Point(819, 174)
        Me.btnReadSettings.Name = "btnReadSettings"
        Me.btnReadSettings.Size = New System.Drawing.Size(188, 20)
        Me.btnReadSettings.TabIndex = 26
        Me.btnReadSettings.TextValue = "Read Settings"
        '
        'btnOpenModel
        '
        Me.btnOpenModel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenModel.BackColor = System.Drawing.Color.Transparent
        Me.btnOpenModel.ForeColor = System.Drawing.Color.White
        Me.btnOpenModel.Location = New System.Drawing.Point(819, 29)
        Me.btnOpenModel.Name = "btnOpenModel"
        Me.btnOpenModel.Size = New System.Drawing.Size(188, 20)
        Me.btnOpenModel.TabIndex = 25
        Me.btnOpenModel.TextValue = "Open 3D Model"
        '
        'btnShader
        '
        Me.btnShader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShader.BackColor = System.Drawing.Color.Transparent
        Me.btnShader.ForeColor = System.Drawing.Color.White
        Me.btnShader.Location = New System.Drawing.Point(819, 60)
        Me.btnShader.Name = "btnShader"
        Me.btnShader.Size = New System.Drawing.Size(188, 20)
        Me.btnShader.TabIndex = 24
        Me.btnShader.TextValue = "Activate Shader"
        '
        'btnSingleShot
        '
        Me.btnSingleShot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSingleShot.BackColor = System.Drawing.Color.Transparent
        Me.btnSingleShot.ForeColor = System.Drawing.Color.White
        Me.btnSingleShot.Location = New System.Drawing.Point(819, 143)
        Me.btnSingleShot.Name = "btnSingleShot"
        Me.btnSingleShot.Size = New System.Drawing.Size(188, 20)
        Me.btnSingleShot.TabIndex = 23
        Me.btnSingleShot.TextValue = "Take Single Shot"
        '
        'btnSetPhaseShift
        '
        Me.btnSetPhaseShift.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSetPhaseShift.BackColor = System.Drawing.Color.Transparent
        Me.btnSetPhaseShift.ForeColor = System.Drawing.Color.White
        Me.btnSetPhaseShift.Location = New System.Drawing.Point(819, 117)
        Me.btnSetPhaseShift.Name = "btnSetPhaseShift"
        Me.btnSetPhaseShift.Size = New System.Drawing.Size(188, 20)
        Me.btnSetPhaseShift.TabIndex = 22
        Me.btnSetPhaseShift.TextValue = "Set Phase Shift"
        '
        'btnSetFrequency
        '
        Me.btnSetFrequency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSetFrequency.BackColor = System.Drawing.Color.Transparent
        Me.btnSetFrequency.ForeColor = System.Drawing.Color.White
        Me.btnSetFrequency.Location = New System.Drawing.Point(819, 91)
        Me.btnSetFrequency.Name = "btnSetFrequency"
        Me.btnSetFrequency.Size = New System.Drawing.Size(188, 20)
        Me.btnSetFrequency.TabIndex = 21
        Me.btnSetFrequency.TextValue = "Set Frequency"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1019, 659)
        Me.Controls.Add(Me.btnStartMeasurement)
        Me.Controls.Add(Me.btnReadSettings)
        Me.Controls.Add(Me.btnOpenModel)
        Me.Controls.Add(Me.btnShader)
        Me.Controls.Add(Me.btnSingleShot)
        Me.Controls.Add(Me.btnSetPhaseShift)
        Me.Controls.Add(Me.btnSetFrequency)
        Me.Controls.Add(Me.GlC1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fringe Projection Simulator 2013"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Open3DModelTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResolutionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents X480TSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents X600TSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents X768TSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeamerSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeamerLocTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeamerTargetTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeamerParTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeamerResolutionTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BeamerShowTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CameraSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LocationTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TargetTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ParametersTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ShowTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GratingSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowGratingSettingsTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShaderTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetShaderTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MeasurementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetFrequencyTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetphaseShiftTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TakeSingleShotTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoiréToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GratingTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetGratingParametersTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetGratingResolutionTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetGratingFrequencyTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewHelpTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutTSMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TSL1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSL2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tss2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSL3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tss3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSL4 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents GlC1 As OpenTK.GLControl
    Friend WithEvents btnStartMeasurement As Artesis_Button001.btnArtesis001
    Friend WithEvents btnReadSettings As Artesis_Button001.btnArtesis001
    Friend WithEvents btnOpenModel As Artesis_Button001.btnArtesis001
    Friend WithEvents btnShader As Artesis_Button001.btnArtesis001
    Friend WithEvents btnSingleShot As Artesis_Button001.btnArtesis001
    Friend WithEvents btnSetPhaseShift As Artesis_Button001.btnArtesis001
    Friend WithEvents btnSetFrequency As Artesis_Button001.btnArtesis001
    Friend WithEvents EnableSaveTSMI As System.Windows.Forms.ToolStripMenuItem

End Class
