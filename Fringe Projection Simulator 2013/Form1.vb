'This file is part of Fringe Projection Simulator.
'
'    Fringe Projection Simulator is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    Fringe Projection Simulator is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with Fringe Projection Simulator.  If not, see <http://www.gnu.org/licenses/>.
Public Class Form1
#Region "Declarations"
    Dim Width640 As Integer = 875
    Dim Height480 As Integer = 577
    Dim strVShader As String = FileIO.FileSystem.CurrentDirectory & "/" & "Shaders/VS_all.glsl"
    'Dim strFShader As String = FileIO.FileSystem.CurrentDirectory & "/" & "Shaders/FS_all.glsl"
    Dim strFShader As String = FileIO.FileSystem.CurrentDirectory & "/" & "Shaders/FS_all_v26-11-2012.glsl"
    'Dim strVShader As String = "C:\Users\Bart\Desktop\VS_all.glsl"
    ''Dim strFShader As String = "C:\Users\Bart\Desktop\shadowF_rev03.glsl"
    'Dim strFShader As String = "C:\Users\Bart\Desktop\FS_all_v26-11-2012.glsl"
    Dim BgColor As Drawing.Color = Color.FromArgb(12, 68, 71)
#End Region
#Region "TSM functionalities"
#Region "File"
    Private Sub ExitTSMI_Click(sender As System.Object, e As System.EventArgs) Handles ExitTSMI.Click
        End
    End Sub
    Private Sub Open3DModelTSMI_Click(sender As System.Object, e As System.EventArgs) Handles Open3DModelTSMI.Click
        If Load3DModel(1) = True Then
            'Model loaded
        End If
    End Sub
#End Region
#Region "Settings"
    Private Sub X480TSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles X480TSMI.Click
        Me.Visible = False
        Me.Height = Height480
        Me.Width = Width640
        GlC1.Height = 480
        GlC1.Width = 640
        X600TSMI.Checked = False
        X768TSMI.Checked = False
        OpenGLRev01.OpenGLMain.oGLViewportUpdate(GlC1)
        Me.Visible = True
    End Sub
    Private Sub X600TSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles X600TSMI.Click
        Me.Visible = False
        Me.Height = Height480 + 120
        Me.Width = Width640 + 160
        GlC1.Height = 600
        GlC1.Width = 800
        X480TSMI.Checked = False
        X768TSMI.Checked = False
        OpenGLRev01.OpenGLMain.oGLViewportUpdate(GlC1)
        Me.Visible = True
    End Sub
    Private Sub X768TSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles X768TSMI.Click
        Me.Visible = False
        Me.Height = Height480 + 288
        Me.Width = Width640 + 384
        GlC1.Height = 768
        GlC1.Width = 1024
        X480TSMI.Checked = False
        X600TSMI.Checked = False
        OpenGLRev01.OpenGLMain.oGLViewportUpdate(GlC1)
        Me.Visible = True
    End Sub
    Private Sub BeamerLocTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeamerLocTSMI.Click
        OpenGLRev01.OpenGLColor.oSetBeamerLocation()
    End Sub
    Private Sub BeamerParTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeamerParTSMI.Click
        OpenGLRev01.OpenGLColor.oSetBeamerParameters()
    End Sub
    Private Sub BeamerShowTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeamerShowTSMI.Click
        frmBeamerParameters.ShowDialog()
    End Sub
    Private Sub BeamerResolutionTSMI_Click(sender As System.Object, e As System.EventArgs) Handles BeamerResolutionTSMI.Click
        OpenGLRev01.OpenGLColor.oSetBeamerResolution()
    End Sub
    Private Sub LocationTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocationTSMI.Click
        OpenGLRev01.CameraSettings.oSetCameraLocation()
    End Sub
    Private Sub TargetTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TargetTSMI.Click
        OpenGLRev01.CameraSettings.oSetCameraTarget()
    End Sub
    Private Sub ParametersTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ParametersTSMI.Click
        OpenGLRev01.CameraSettings.oSetCameraParameters()
    End Sub
    Private Sub ShowGratingSettingsTSMI_Click(sender As System.Object, e As System.EventArgs) Handles ShowGratingSettingsTSMI.Click
        frmGratingParameters.ShowDialog()
    End Sub
    Private Sub ShowTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowTSMI.Click
        frmCameraParameters.ShowDialog()
    End Sub
    Private Sub ShaderTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShaderTSMI.Click
        If ShaderTSMI.Checked = True Then
            If OpenGLRev01.OpenGLColor.oSetShader(strVShader, strFShader) <> True Then
                ShaderTSMI.Checked = False
            Else
                ShaderTSMI.Text = "Disable &Shader"
            End If
        Else
            OpenGLRev01.OpenGLColor.oDeactivateShaders()
            ShaderTSMI.Text = "Enable &Shader"
        End If
    End Sub
    Private Sub SetShaderTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetShaderTSMI.Click
        Dim OFD1 As New OpenFileDialog
        Dim FilePath As String
        Try
            OFD1.Title = "Open vertex shader"
            OFD1.Filter = "OpenGL Shading Language|*.glsl"
            If OFD1.ShowDialog = DialogResult.OK Then
                FilePath = OFD1.InitialDirectory & OFD1.FileName
                strVShader = FilePath
            Else
                MsgBox("Open shader was cancelled")
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("Fout bij het lezen van de file")
        End Try
        Try
            OFD1.Title = "Open fragment shader"
            OFD1.Filter = "OpenGL Shading Language|*.glsl"
            If OFD1.ShowDialog = DialogResult.OK Then
                FilePath = OFD1.InitialDirectory & OFD1.FileName
                strFShader = FilePath
            Else
                MsgBox("Open shader was cancelled")
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("Fout bij het lezen van de file")
        End Try
        If ShaderTSMI.Checked = True Then
            OpenGLRev01.OpenGLColor.oSetShader(strVShader, strFShader)
        Else
            OpenGLRev01.OpenGLColor.oSetShader(strVShader, strFShader)
            OpenGLRev01.OpenGLColor.oDeactivateShaders()
        End If
    End Sub
    Private Sub EnableSaveTSMI_Click(sender As System.Object, e As System.EventArgs) Handles EnableSaveTSMI.Click
        EnableSaveTSMI.Checked = Not EnableSaveTSMI.Checked
        If EnableSaveTSMI.Checked = True Then
            EnableSaveTSMI.Text = "Disable Sa&ving"
        Else
            EnableSaveTSMI.Text = "Enable Sa&ving"
        End If
    End Sub
#End Region
#Region "Measurement"
    Private Sub SetphaseShiftTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetphaseShiftTSMI.Click
        frmPhaseShift.ShowDialog()
    End Sub
    Private Sub SetFrequencyTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetFrequencyTSMI.Click
        OpenGLRev01.OpenGLColor.oSetBeamerFrequency()
    End Sub
    Private Sub TakeSingleShotTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TakeSingleShotTSMI.Click
        OpenGLRev01.OpenGLMain.oTakeScreenshot(GlC1)
    End Sub
#End Region
#Region "Moiré"
    Private Sub GratingTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GratingTSMI.Click
        If OpenGLRev01.OpenGLColor.oIsCameraGratingEnabled = True Then
            OpenGLRev01.OpenGLColor.oDeactivateCameraGrating()
            OpenGLRev01.OpenGLSettings.oSetBlending(False)
            intListArray = {1}

            GratingTSMI.Text = "&Enable grating"
            GratingTSMI.Checked = False
            OpenGLRev01.OpenGLMain.oSetRunStatus(OpenGLRev01.OpenGLMain.LoopStatus.FREERUN)
            OpenGLRev01.OpenGLMain.oLoop(GlC1, intListArray, BgColor)
            'OpenGLRev01.OpenGLMain.RenderFrame(GLC1, intListArray, Drawing.Color.Orange)
        Else
            OpenGLRev01.OpenGLColor.oActivateCameraGrating()
            OpenGLRev01.OpenGLSettings.oSetBlending(True)
            GratingTSMI.Checked = True
            OpenGLRev01.Draw.oDrawGrating(2)
            intListArray = {1, 2}

            GratingTSMI.Text = "&Disable grating"
            OpenGLRev01.OpenGLColor.oUpdateShader()
            OpenGLRev01.OpenGLMain.oSetRunStatus(OpenGLRev01.OpenGLMain.LoopStatus.FREERUN)
            OpenGLRev01.OpenGLMain.oLoop(GlC1, intListArray, BgColor)
            'FD.OpenGLMain.RenderFrame(GLC1, intListArray, Drawing.Color.Orange)
        End If
    End Sub
    Private Sub SetGratingParametersTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetGratingParametersTSMI.Click
        OpenGLRev01.OpenGLColor.oSetCameraGratingParameters()
        If OpenGLRev01.OpenGLColor.oIsCameraGratingEnabled = True Then
            OpenGLRev01.Draw.oDrawGrating(2)
            intListArray = {1, 2}
        End If
    End Sub
    Private Sub SetGratingResolutionTSMI_Click(sender As System.Object, e As System.EventArgs) Handles SetGratingResolutionTSMI.Click
        OpenGLRev01.OpenGLColor.oSetCameraGratingResolution()
    End Sub
    Private Sub SetGratingFrequencyTSMI_Click(sender As System.Object, e As System.EventArgs) Handles SetGratingFrequencyTSMI.Click
        OpenGLRev01.OpenGLColor.oSetCameraGratingFrequency()
    End Sub
#End Region
#Region "About"
    Private Sub ViewHelpTSMI_Click(sender As System.Object, e As System.EventArgs) Handles ViewHelpTSMI.Click
        'Open Help window
        Process.Start("http://www.fringesimulator.com/request.html")
    End Sub
    Private Sub AboutTSMI_Click(sender As System.Object, e As System.EventArgs) Handles AboutTSMI.Click
        frmAbout.Show()
    End Sub
#End Region
#End Region
#Region "Core functions"
    Dim intListArray() As Integer = {1}
    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Initialize()
        Main()
    End Sub

    Function Load3DModel(ByVal intListNr As Integer) As Boolean
        Try
            Dim FilePath As String = Nothing
            Dim blnASCII As Boolean
            Dim intNumberVertices As Integer = 0
            If OpenGLRev01.ReadSTL.oOpenSTLFile(FilePath) = True Then
                blnASCII = OpenGLRev01.ReadSTL.oFormatCheck(FilePath)
                intNumberVertices = OpenGLRev01.ReadSTL.oSTLtoList(FilePath, intListNr, blnASCII, OpenGLRev01.OpenGLSettings.VisualisationType.SHADED, 1, True)
                OpenGLRev01.OpenGLSettings.oSetVisualizationMode(OpenGLRev01.OpenGLSettings.VisualisationType.SHADED, 1)
                TSL2.Text = " This model contains " & CStr(intNumberVertices) & " vertices"
                'OpenGLRev01.Draw.GenerateShadowFBO(5)
                Return True
                Exit Function
            Else
                Return False
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Sub Initialize()
        TSL1.Text = "©by Bart Ribbens 2013"
        TSL2.Text = "Version 2-05-2017" '"No file location selected"
        TSL3.Text = "No measurement map selected"
        tss2.Visible = False
        TSL3.Visible = False
        tss3.Visible = False
        TSL4.Visible = False
        Dim MyBackgroundColor As Color = Color.FromArgb(12, 68, 71)
        OpenGLRev01.OpenGLMain.oGLInitialize(GlC1, MyBackgroundColor, True)
        Me.Height = Height480 + 120
        Me.Width = Width640 + 160
        GlC1.Height = 600
        GlC1.Width = 800
        X480TSMI.Checked = False
        X600TSMI.Checked = True
        X768TSMI.Checked = False
        'Me.Height = Height640
        'Me.Width = Width640
        'GlC1.Height = 480
        'GlC1.Width = 640
        'OpenGLRev01.Draw.AttachTexture("C:\Users\Public\Pictures\Sample Pictures\Koala.bmp")
        OpenGLRev01.OpenGLColor.oSetShadowMapSize(2048)
        Me.Show()
        Me.Activate()
    End Sub
    Sub Main()
        OpenGLRev01.OpenGLMain.oSetRunStatus(OpenGLRev01.OpenGLMain.LoopStatus.FREERUN)
        OpenGLRev01.OpenGLMain.oSetRunning(True)
        OpenGLRev01.OpenGLMain.oSetSleepTime(10)
        OpenGLRev01.OpenGLMain.oLoop(GlC1, {1})
    End Sub
#End Region
#Region "Button functionalities"
    Private Sub btnOpenModel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenModel.Click
        If Load3DModel(1) = True Then
            'Model loaded
        End If
    End Sub
    Private Sub btnShader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShader.Click
        ShaderTSMI.Checked = Not ShaderTSMI.Checked
        If ShaderTSMI.Checked = True Then
            If OpenGLRev01.OpenGLColor.oSetShader(strVShader, strFShader) <> True Then
                ShaderTSMI.Checked = False
            Else
                ShaderTSMI.Checked = True
                ShaderTSMI.Text = "Disable &Shader"
            End If
        Else
            OpenGLRev01.OpenGLColor.oDeactivateShaders()
            ShaderTSMI.Checked = False
            ShaderTSMI.Text = "Enable &Shader"
        End If
    End Sub
    Private Sub btnSetFrequency_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFrequency.Click
        OpenGLRev01.OpenGLColor.oSetBeamerFrequency()
    End Sub
    Private Sub btnSetPhaseShift_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPhaseShift.Click
        frmPhaseShift.ShowDialog()
    End Sub
    Private Sub btnSingleShot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSingleShot.Click
        OpenGLRev01.OpenGLMain.oTakeScreenshot(GlC1)
    End Sub
    Private Sub btnReadSettings_Click(sender As Object, e As System.EventArgs) Handles btnReadSettings.Click
        OpenGLRev01.ReadSettings.Measurements = OpenGLRev01.ReadSettings.oReadSettingsFileXMl
    End Sub
    Private Sub btnStartMeasurement_Click(sender As System.Object, e As System.EventArgs) Handles btnStartMeasurement.Click
        If Me.ShaderTSMI.CheckState = False Then
            If OpenGLRev01.OpenGLColor.oSetShader(strVShader, strFShader) <> True Then

            End If
            ShaderTSMI.Checked = True
            MsgBox("The shader was activated")
        End If
        OpenGLRev01.OpenGLMain.oSetRunStatus(OpenGLRev01.OpenGLMain.LoopStatus.MEASUREMENT)
        OpenGLRev01.OpenGLMain.oSetRunning(True)
        ' MsgBox(OpenGLRev01.ReadSettings.Measurements(0).MapPath & OpenGLRev01.ReadSettings.Measurements(0).FileName & "0001" & ".png")
        If EnableSaveTSMI.Checked = True Then
            MsgBox("Saving enabled ...")
            OpenGLRev01.OpenGLMain.oLoop(GlC1, {1, 2}, OpenGLRev01.ReadSettings.Measurements, Color.Black, True)
        Else
            MsgBox("Saving disabled!")
            OpenGLRev01.OpenGLMain.oLoop(GlC1, {1, 2}, OpenGLRev01.ReadSettings.Measurements, Color.Black, False)
        End If

    End Sub
#End Region
End Class
