<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPhaseShift
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPhaseShift))
        Me.rbn0 = New System.Windows.Forms.RadioButton()
        Me.rbnPi4 = New System.Windows.Forms.RadioButton()
        Me.rbnPi2 = New System.Windows.Forms.RadioButton()
        Me.rbn3Pi4 = New System.Windows.Forms.RadioButton()
        Me.rbnPi = New System.Windows.Forms.RadioButton()
        Me.rbn5Pi4 = New System.Windows.Forms.RadioButton()
        Me.rbn3Pi2 = New System.Windows.Forms.RadioButton()
        Me.rbn7Pi4 = New System.Windows.Forms.RadioButton()
        Me.txtPhaseShift = New System.Windows.Forms.TextBox()
        Me.lblPhaseShift = New System.Windows.Forms.Label()
        Me.btnSetPhaseShift = New System.Windows.Forms.Button()
        Me.lblResultInPi = New System.Windows.Forms.Label()
        Me.pbStatus = New System.Windows.Forms.PictureBox()
        Me.ImgList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grp1 = New System.Windows.Forms.GroupBox()
        Me.rbnOnlyBeamer = New System.Windows.Forms.RadioButton()
        Me.rbnGratingBeamer = New System.Windows.Forms.RadioButton()
        CType(Me.pbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp1.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbn0
        '
        Me.rbn0.AutoSize = True
        Me.rbn0.Location = New System.Drawing.Point(12, 12)
        Me.rbn0.Name = "rbn0"
        Me.rbn0.Size = New System.Drawing.Size(31, 17)
        Me.rbn0.TabIndex = 0
        Me.rbn0.Text = "0"
        Me.rbn0.UseVisualStyleBackColor = True
        '
        'rbnPi4
        '
        Me.rbnPi4.AutoSize = True
        Me.rbnPi4.Location = New System.Drawing.Point(12, 35)
        Me.rbnPi4.Name = "rbnPi4"
        Me.rbnPi4.Size = New System.Drawing.Size(53, 17)
        Me.rbnPi4.TabIndex = 1
        Me.rbnPi4.Text = "1/4 pi"
        Me.rbnPi4.UseVisualStyleBackColor = True
        '
        'rbnPi2
        '
        Me.rbnPi2.AutoSize = True
        Me.rbnPi2.Location = New System.Drawing.Point(12, 58)
        Me.rbnPi2.Name = "rbnPi2"
        Me.rbnPi2.Size = New System.Drawing.Size(53, 17)
        Me.rbnPi2.TabIndex = 2
        Me.rbnPi2.Text = "1/2 pi"
        Me.rbnPi2.UseVisualStyleBackColor = True
        '
        'rbn3Pi4
        '
        Me.rbn3Pi4.AutoSize = True
        Me.rbn3Pi4.Location = New System.Drawing.Point(12, 81)
        Me.rbn3Pi4.Name = "rbn3Pi4"
        Me.rbn3Pi4.Size = New System.Drawing.Size(53, 17)
        Me.rbn3Pi4.TabIndex = 3
        Me.rbn3Pi4.Text = "3/4 pi"
        Me.rbn3Pi4.UseVisualStyleBackColor = True
        '
        'rbnPi
        '
        Me.rbnPi.AutoSize = True
        Me.rbnPi.Location = New System.Drawing.Point(12, 104)
        Me.rbnPi.Name = "rbnPi"
        Me.rbnPi.Size = New System.Drawing.Size(33, 17)
        Me.rbnPi.TabIndex = 4
        Me.rbnPi.Text = "pi"
        Me.rbnPi.UseVisualStyleBackColor = True
        '
        'rbn5Pi4
        '
        Me.rbn5Pi4.AutoSize = True
        Me.rbn5Pi4.Location = New System.Drawing.Point(12, 127)
        Me.rbn5Pi4.Name = "rbn5Pi4"
        Me.rbn5Pi4.Size = New System.Drawing.Size(53, 17)
        Me.rbn5Pi4.TabIndex = 5
        Me.rbn5Pi4.Text = "5/4 pi"
        Me.rbn5Pi4.UseVisualStyleBackColor = True
        '
        'rbn3Pi2
        '
        Me.rbn3Pi2.AutoSize = True
        Me.rbn3Pi2.Location = New System.Drawing.Point(12, 150)
        Me.rbn3Pi2.Name = "rbn3Pi2"
        Me.rbn3Pi2.Size = New System.Drawing.Size(53, 17)
        Me.rbn3Pi2.TabIndex = 6
        Me.rbn3Pi2.Text = "3/2 pi"
        Me.rbn3Pi2.UseVisualStyleBackColor = True
        '
        'rbn7Pi4
        '
        Me.rbn7Pi4.AutoSize = True
        Me.rbn7Pi4.Location = New System.Drawing.Point(12, 173)
        Me.rbn7Pi4.Name = "rbn7Pi4"
        Me.rbn7Pi4.Size = New System.Drawing.Size(53, 17)
        Me.rbn7Pi4.TabIndex = 7
        Me.rbn7Pi4.Text = "7/4 pi"
        Me.rbn7Pi4.UseVisualStyleBackColor = True
        '
        'txtPhaseShift
        '
        Me.txtPhaseShift.Location = New System.Drawing.Point(77, 204)
        Me.txtPhaseShift.Name = "txtPhaseShift"
        Me.txtPhaseShift.Size = New System.Drawing.Size(95, 20)
        Me.txtPhaseShift.TabIndex = 8
        '
        'lblPhaseShift
        '
        Me.lblPhaseShift.AutoSize = True
        Me.lblPhaseShift.Location = New System.Drawing.Point(9, 207)
        Me.lblPhaseShift.Name = "lblPhaseShift"
        Me.lblPhaseShift.Size = New System.Drawing.Size(67, 13)
        Me.lblPhaseShift.TabIndex = 9
        Me.lblPhaseShift.Text = "Phase Shift: "
        '
        'btnSetPhaseShift
        '
        Me.btnSetPhaseShift.Location = New System.Drawing.Point(11, 307)
        Me.btnSetPhaseShift.Name = "btnSetPhaseShift"
        Me.btnSetPhaseShift.Size = New System.Drawing.Size(199, 23)
        Me.btnSetPhaseShift.TabIndex = 10
        Me.btnSetPhaseShift.Text = "Set phase shift"
        Me.btnSetPhaseShift.UseVisualStyleBackColor = True
        '
        'lblResultInPi
        '
        Me.lblResultInPi.Location = New System.Drawing.Point(78, 227)
        Me.lblResultInPi.Name = "lblResultInPi"
        Me.lblResultInPi.Size = New System.Drawing.Size(112, 24)
        Me.lblResultInPi.TabIndex = 11
        Me.lblResultInPi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pbStatus
        '
        Me.pbStatus.Location = New System.Drawing.Point(178, 204)
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(20, 20)
        Me.pbStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbStatus.TabIndex = 12
        Me.pbStatus.TabStop = False
        '
        'ImgList1
        '
        Me.ImgList1.ImageStream = CType(resources.GetObject("ImgList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList1.Images.SetKeyName(0, "checkmark.png")
        Me.ImgList1.Images.SetKeyName(1, "error.png")
        '
        'grp1
        '
        Me.grp1.Controls.Add(Me.rbnOnlyBeamer)
        Me.grp1.Controls.Add(Me.rbnGratingBeamer)
        Me.grp1.Location = New System.Drawing.Point(11, 230)
        Me.grp1.Name = "grp1"
        Me.grp1.Size = New System.Drawing.Size(200, 71)
        Me.grp1.TabIndex = 13
        Me.grp1.TabStop = False
        '
        'rbnOnlyBeamer
        '
        Me.rbnOnlyBeamer.AutoSize = True
        Me.rbnOnlyBeamer.Checked = True
        Me.rbnOnlyBeamer.Location = New System.Drawing.Point(11, 19)
        Me.rbnOnlyBeamer.Name = "rbnOnlyBeamer"
        Me.rbnOnlyBeamer.Size = New System.Drawing.Size(84, 17)
        Me.rbnOnlyBeamer.TabIndex = 14
        Me.rbnOnlyBeamer.TabStop = True
        Me.rbnOnlyBeamer.Text = "Only beamer"
        Me.rbnOnlyBeamer.UseVisualStyleBackColor = True
        '
        'rbnGratingBeamer
        '
        Me.rbnGratingBeamer.AutoSize = True
        Me.rbnGratingBeamer.Location = New System.Drawing.Point(11, 42)
        Me.rbnGratingBeamer.Name = "rbnGratingBeamer"
        Me.rbnGratingBeamer.Size = New System.Drawing.Size(118, 17)
        Me.rbnGratingBeamer.TabIndex = 15
        Me.rbnGratingBeamer.Text = "Grating and beamer"
        Me.rbnGratingBeamer.UseVisualStyleBackColor = True
        '
        'frmPhaseShift
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(222, 342)
        Me.ControlBox = False
        Me.Controls.Add(Me.grp1)
        Me.Controls.Add(Me.pbStatus)
        Me.Controls.Add(Me.lblResultInPi)
        Me.Controls.Add(Me.btnSetPhaseShift)
        Me.Controls.Add(Me.lblPhaseShift)
        Me.Controls.Add(Me.txtPhaseShift)
        Me.Controls.Add(Me.rbn7Pi4)
        Me.Controls.Add(Me.rbn3Pi2)
        Me.Controls.Add(Me.rbn5Pi4)
        Me.Controls.Add(Me.rbnPi)
        Me.Controls.Add(Me.rbn3Pi4)
        Me.Controls.Add(Me.rbnPi2)
        Me.Controls.Add(Me.rbnPi4)
        Me.Controls.Add(Me.rbn0)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPhaseShift"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Phase Shift Settings"
        CType(Me.pbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp1.ResumeLayout(False)
        Me.grp1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rbn0 As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPi4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPi2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbn3Pi4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbnPi As System.Windows.Forms.RadioButton
    Friend WithEvents rbn5Pi4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbn3Pi2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbn7Pi4 As System.Windows.Forms.RadioButton
    Friend WithEvents txtPhaseShift As System.Windows.Forms.TextBox
    Friend WithEvents lblPhaseShift As System.Windows.Forms.Label
    Friend WithEvents btnSetPhaseShift As System.Windows.Forms.Button
    Friend WithEvents lblResultInPi As System.Windows.Forms.Label
    Friend WithEvents pbStatus As System.Windows.Forms.PictureBox
    Friend WithEvents ImgList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents grp1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbnOnlyBeamer As System.Windows.Forms.RadioButton
    Friend WithEvents rbnGratingBeamer As System.Windows.Forms.RadioButton
End Class
