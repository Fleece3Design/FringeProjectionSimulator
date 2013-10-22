<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCameraParameters
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
        Me.grpLocation = New System.Windows.Forms.GroupBox()
        Me.txtLocZ = New System.Windows.Forms.TextBox()
        Me.lblLocZ = New System.Windows.Forms.Label()
        Me.txtLocY = New System.Windows.Forms.TextBox()
        Me.lblLocY = New System.Windows.Forms.Label()
        Me.txtLocX = New System.Windows.Forms.TextBox()
        Me.lblLocX = New System.Windows.Forms.Label()
        Me.grpTarget = New System.Windows.Forms.GroupBox()
        Me.txtTarZ = New System.Windows.Forms.TextBox()
        Me.lblTarZ = New System.Windows.Forms.Label()
        Me.txtTarY = New System.Windows.Forms.TextBox()
        Me.lblTarY = New System.Windows.Forms.Label()
        Me.txtTarX = New System.Windows.Forms.TextBox()
        Me.lblTarX = New System.Windows.Forms.Label()
        Me.grpParameters = New System.Windows.Forms.GroupBox()
        Me.txtFar = New System.Windows.Forms.TextBox()
        Me.lblFar = New System.Windows.Forms.Label()
        Me.txtNear = New System.Windows.Forms.TextBox()
        Me.lblNear = New System.Windows.Forms.Label()
        Me.txtAspectRatio = New System.Windows.Forms.TextBox()
        Me.lblAspectRatio = New System.Windows.Forms.Label()
        Me.txtPOV = New System.Windows.Forms.TextBox()
        Me.lblPOV = New System.Windows.Forms.Label()
        Me.grpLocation.SuspendLayout()
        Me.grpTarget.SuspendLayout()
        Me.grpParameters.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpLocation
        '
        Me.grpLocation.Controls.Add(Me.txtLocZ)
        Me.grpLocation.Controls.Add(Me.lblLocZ)
        Me.grpLocation.Controls.Add(Me.txtLocY)
        Me.grpLocation.Controls.Add(Me.lblLocY)
        Me.grpLocation.Controls.Add(Me.txtLocX)
        Me.grpLocation.Controls.Add(Me.lblLocX)
        Me.grpLocation.Location = New System.Drawing.Point(12, 12)
        Me.grpLocation.Name = "grpLocation"
        Me.grpLocation.Size = New System.Drawing.Size(184, 100)
        Me.grpLocation.TabIndex = 0
        Me.grpLocation.TabStop = False
        Me.grpLocation.Text = "Location"
        '
        'txtLocZ
        '
        Me.txtLocZ.Enabled = False
        Me.txtLocZ.Location = New System.Drawing.Point(32, 71)
        Me.txtLocZ.Name = "txtLocZ"
        Me.txtLocZ.Size = New System.Drawing.Size(138, 20)
        Me.txtLocZ.TabIndex = 7
        '
        'lblLocZ
        '
        Me.lblLocZ.AutoSize = True
        Me.lblLocZ.Location = New System.Drawing.Point(6, 74)
        Me.lblLocZ.Name = "lblLocZ"
        Me.lblLocZ.Size = New System.Drawing.Size(20, 13)
        Me.lblLocZ.TabIndex = 6
        Me.lblLocZ.Text = "Z: "
        '
        'txtLocY
        '
        Me.txtLocY.Enabled = False
        Me.txtLocY.Location = New System.Drawing.Point(32, 45)
        Me.txtLocY.Name = "txtLocY"
        Me.txtLocY.Size = New System.Drawing.Size(138, 20)
        Me.txtLocY.TabIndex = 5
        '
        'lblLocY
        '
        Me.lblLocY.AutoSize = True
        Me.lblLocY.Location = New System.Drawing.Point(6, 48)
        Me.lblLocY.Name = "lblLocY"
        Me.lblLocY.Size = New System.Drawing.Size(20, 13)
        Me.lblLocY.TabIndex = 4
        Me.lblLocY.Text = "Y: "
        '
        'txtLocX
        '
        Me.txtLocX.Enabled = False
        Me.txtLocX.Location = New System.Drawing.Point(32, 19)
        Me.txtLocX.Name = "txtLocX"
        Me.txtLocX.Size = New System.Drawing.Size(138, 20)
        Me.txtLocX.TabIndex = 3
        '
        'lblLocX
        '
        Me.lblLocX.AutoSize = True
        Me.lblLocX.Location = New System.Drawing.Point(6, 22)
        Me.lblLocX.Name = "lblLocX"
        Me.lblLocX.Size = New System.Drawing.Size(20, 13)
        Me.lblLocX.TabIndex = 0
        Me.lblLocX.Text = "X: "
        '
        'grpTarget
        '
        Me.grpTarget.Controls.Add(Me.txtTarZ)
        Me.grpTarget.Controls.Add(Me.lblTarZ)
        Me.grpTarget.Controls.Add(Me.txtTarY)
        Me.grpTarget.Controls.Add(Me.lblTarY)
        Me.grpTarget.Controls.Add(Me.txtTarX)
        Me.grpTarget.Controls.Add(Me.lblTarX)
        Me.grpTarget.Location = New System.Drawing.Point(12, 118)
        Me.grpTarget.Name = "grpTarget"
        Me.grpTarget.Size = New System.Drawing.Size(184, 100)
        Me.grpTarget.TabIndex = 8
        Me.grpTarget.TabStop = False
        Me.grpTarget.Text = "Target"
        '
        'txtTarZ
        '
        Me.txtTarZ.Enabled = False
        Me.txtTarZ.Location = New System.Drawing.Point(32, 71)
        Me.txtTarZ.Name = "txtTarZ"
        Me.txtTarZ.Size = New System.Drawing.Size(138, 20)
        Me.txtTarZ.TabIndex = 7
        '
        'lblTarZ
        '
        Me.lblTarZ.AutoSize = True
        Me.lblTarZ.Location = New System.Drawing.Point(6, 74)
        Me.lblTarZ.Name = "lblTarZ"
        Me.lblTarZ.Size = New System.Drawing.Size(20, 13)
        Me.lblTarZ.TabIndex = 6
        Me.lblTarZ.Text = "Z: "
        '
        'txtTarY
        '
        Me.txtTarY.Enabled = False
        Me.txtTarY.Location = New System.Drawing.Point(32, 45)
        Me.txtTarY.Name = "txtTarY"
        Me.txtTarY.Size = New System.Drawing.Size(138, 20)
        Me.txtTarY.TabIndex = 5
        '
        'lblTarY
        '
        Me.lblTarY.AutoSize = True
        Me.lblTarY.Location = New System.Drawing.Point(6, 48)
        Me.lblTarY.Name = "lblTarY"
        Me.lblTarY.Size = New System.Drawing.Size(20, 13)
        Me.lblTarY.TabIndex = 4
        Me.lblTarY.Text = "Y: "
        '
        'txtTarX
        '
        Me.txtTarX.Enabled = False
        Me.txtTarX.Location = New System.Drawing.Point(32, 19)
        Me.txtTarX.Name = "txtTarX"
        Me.txtTarX.Size = New System.Drawing.Size(138, 20)
        Me.txtTarX.TabIndex = 3
        '
        'lblTarX
        '
        Me.lblTarX.AutoSize = True
        Me.lblTarX.Location = New System.Drawing.Point(6, 22)
        Me.lblTarX.Name = "lblTarX"
        Me.lblTarX.Size = New System.Drawing.Size(20, 13)
        Me.lblTarX.TabIndex = 0
        Me.lblTarX.Text = "X: "
        '
        'grpParameters
        '
        Me.grpParameters.Controls.Add(Me.txtFar)
        Me.grpParameters.Controls.Add(Me.lblFar)
        Me.grpParameters.Controls.Add(Me.txtNear)
        Me.grpParameters.Controls.Add(Me.lblNear)
        Me.grpParameters.Controls.Add(Me.txtAspectRatio)
        Me.grpParameters.Controls.Add(Me.lblAspectRatio)
        Me.grpParameters.Controls.Add(Me.txtPOV)
        Me.grpParameters.Controls.Add(Me.lblPOV)
        Me.grpParameters.Location = New System.Drawing.Point(213, 48)
        Me.grpParameters.Name = "grpParameters"
        Me.grpParameters.Size = New System.Drawing.Size(227, 131)
        Me.grpParameters.TabIndex = 9
        Me.grpParameters.TabStop = False
        Me.grpParameters.Text = "Parameters"
        '
        'txtFar
        '
        Me.txtFar.Enabled = False
        Me.txtFar.Location = New System.Drawing.Point(83, 97)
        Me.txtFar.Name = "txtFar"
        Me.txtFar.Size = New System.Drawing.Size(138, 20)
        Me.txtFar.TabIndex = 15
        '
        'lblFar
        '
        Me.lblFar.AutoSize = True
        Me.lblFar.Location = New System.Drawing.Point(6, 100)
        Me.lblFar.Name = "lblFar"
        Me.lblFar.Size = New System.Drawing.Size(28, 13)
        Me.lblFar.TabIndex = 14
        Me.lblFar.Text = "Far: "
        '
        'txtNear
        '
        Me.txtNear.Enabled = False
        Me.txtNear.Location = New System.Drawing.Point(83, 71)
        Me.txtNear.Name = "txtNear"
        Me.txtNear.Size = New System.Drawing.Size(138, 20)
        Me.txtNear.TabIndex = 13
        '
        'lblNear
        '
        Me.lblNear.AutoSize = True
        Me.lblNear.Location = New System.Drawing.Point(6, 74)
        Me.lblNear.Name = "lblNear"
        Me.lblNear.Size = New System.Drawing.Size(36, 13)
        Me.lblNear.TabIndex = 12
        Me.lblNear.Text = "Near: "
        '
        'txtAspectRatio
        '
        Me.txtAspectRatio.Enabled = False
        Me.txtAspectRatio.Location = New System.Drawing.Point(83, 45)
        Me.txtAspectRatio.Name = "txtAspectRatio"
        Me.txtAspectRatio.Size = New System.Drawing.Size(138, 20)
        Me.txtAspectRatio.TabIndex = 11
        '
        'lblAspectRatio
        '
        Me.lblAspectRatio.AutoSize = True
        Me.lblAspectRatio.Location = New System.Drawing.Point(6, 48)
        Me.lblAspectRatio.Name = "lblAspectRatio"
        Me.lblAspectRatio.Size = New System.Drawing.Size(69, 13)
        Me.lblAspectRatio.TabIndex = 10
        Me.lblAspectRatio.Text = "Aspect ratio: "
        '
        'txtPOV
        '
        Me.txtPOV.Enabled = False
        Me.txtPOV.Location = New System.Drawing.Point(83, 19)
        Me.txtPOV.Name = "txtPOV"
        Me.txtPOV.Size = New System.Drawing.Size(138, 20)
        Me.txtPOV.TabIndex = 9
        '
        'lblPOV
        '
        Me.lblPOV.AutoSize = True
        Me.lblPOV.Location = New System.Drawing.Point(6, 22)
        Me.lblPOV.Name = "lblPOV"
        Me.lblPOV.Size = New System.Drawing.Size(34, 13)
        Me.lblPOV.TabIndex = 8
        Me.lblPOV.Text = "FOV: "
        '
        'frmCameraParameters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 229)
        Me.Controls.Add(Me.grpParameters)
        Me.Controls.Add(Me.grpTarget)
        Me.Controls.Add(Me.grpLocation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCameraParameters"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Camera Parameters"
        Me.grpLocation.ResumeLayout(False)
        Me.grpLocation.PerformLayout()
        Me.grpTarget.ResumeLayout(False)
        Me.grpTarget.PerformLayout()
        Me.grpParameters.ResumeLayout(False)
        Me.grpParameters.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpLocation As System.Windows.Forms.GroupBox
    Friend WithEvents txtLocZ As System.Windows.Forms.TextBox
    Friend WithEvents lblLocZ As System.Windows.Forms.Label
    Friend WithEvents txtLocY As System.Windows.Forms.TextBox
    Friend WithEvents lblLocY As System.Windows.Forms.Label
    Friend WithEvents txtLocX As System.Windows.Forms.TextBox
    Friend WithEvents lblLocX As System.Windows.Forms.Label
    Friend WithEvents grpTarget As System.Windows.Forms.GroupBox
    Friend WithEvents txtTarZ As System.Windows.Forms.TextBox
    Friend WithEvents lblTarZ As System.Windows.Forms.Label
    Friend WithEvents txtTarY As System.Windows.Forms.TextBox
    Friend WithEvents lblTarY As System.Windows.Forms.Label
    Friend WithEvents txtTarX As System.Windows.Forms.TextBox
    Friend WithEvents lblTarX As System.Windows.Forms.Label
    Friend WithEvents grpParameters As System.Windows.Forms.GroupBox
    Friend WithEvents txtFar As System.Windows.Forms.TextBox
    Friend WithEvents lblFar As System.Windows.Forms.Label
    Friend WithEvents txtNear As System.Windows.Forms.TextBox
    Friend WithEvents lblNear As System.Windows.Forms.Label
    Friend WithEvents txtAspectRatio As System.Windows.Forms.TextBox
    Friend WithEvents lblAspectRatio As System.Windows.Forms.Label
    Friend WithEvents txtPOV As System.Windows.Forms.TextBox
    Friend WithEvents lblPOV As System.Windows.Forms.Label
End Class
