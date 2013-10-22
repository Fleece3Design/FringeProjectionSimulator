<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProgress
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProgress))
        Me.lblCommand = New System.Windows.Forms.Label()
        Me.pb1 = New System.Windows.Forms.ProgressBar()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblCommand
        '
        Me.lblCommand.Location = New System.Drawing.Point(12, 9)
        Me.lblCommand.Name = "lblCommand"
        Me.lblCommand.Size = New System.Drawing.Size(260, 43)
        Me.lblCommand.TabIndex = 0
        Me.lblCommand.Text = "lblCommand"
        Me.lblCommand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pb1
        '
        Me.pb1.Location = New System.Drawing.Point(12, 55)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(260, 23)
        Me.pb1.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.ForeColor = System.Drawing.Color.Green
        Me.btnCancel.Location = New System.Drawing.Point(12, 84)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(260, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 117)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.pb1)
        Me.Controls.Add(Me.lblCommand)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProgress"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmProgress"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCommand As System.Windows.Forms.Label
    Friend WithEvents pb1 As System.Windows.Forms.ProgressBar
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
