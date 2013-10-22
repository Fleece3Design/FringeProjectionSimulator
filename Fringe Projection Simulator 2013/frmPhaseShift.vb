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
Imports System.Math
Public Class frmPhaseShift
    Private tt As ToolTip = New ToolTip()
    Private Sub frmPhaseShift_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtPhaseShift.Text = OpenGLRev01.OpenGLColor.oGetBeamerPhaseShift
        Select Case OpenGLRev01.OpenGLColor.oGetBeamerPhaseShift
            Case 0
                rbn0.Checked = True
            Case PI / 4
                rbnPi4.Checked = True
            Case PI / 2
                rbnPi2.Checked = True
            Case 3 * PI / 4
                rbn3Pi4.Checked = True
            Case PI
                rbnPi.Checked = True
            Case 5 * PI / 4
                rbn5Pi4.Checked = True
            Case 3 * PI / 2
                rbn3Pi2.Checked = True
            Case 7 * PI / 4
                rbn7Pi4.Checked = True
        End Select

        tt.UseFading = True
    End Sub
    Private Sub btnSetPhaseShift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPhaseShift.Click
        Dim sglPhase As Single
        Dim ErrorDescription As String = Nothing
        If FDMath.oGetResult(txtPhaseShift.Text, sglPhase, ErrorDescription) = True Then
            If rbnOnlyBeamer.Checked = True Then
                OpenGLRev01.OpenGLColor.oSetBeamerPhaseShift(sglPhase)
                OpenGLRev01.OpenGLColor.oUpdateShader()
                Me.Close()
            Else
                OpenGLRev01.OpenGLColor.oSetBeamerPhaseShift(sglPhase)
                OpenGLRev01.OpenGLColor.oSetCameraGratingPhaseShift(sglPhase)
                OpenGLRev01.OpenGLColor.oUpdateShader()
                Me.Close()
            End If

        Else
            MsgBox("Please enter a valid phase value!" & vbCrLf & ErrorDescription)
        End If
    End Sub
    Private Function ReadRadioButtons(ByRef PhaseShift As Single) As Boolean
        Select Case True
            Case rbn0.Checked
                PhaseShift = 0
                Return True
            Case rbnPi4.Checked
                PhaseShift = PI / 4
                Return True
            Case rbnPi2.Checked
                PhaseShift = PI / 2
                Return True
            Case rbn3Pi4.Checked
                PhaseShift = 3 * PI / 4
                Return True
            Case rbnPi.Checked
                PhaseShift = PI
                Return True
            Case rbn5Pi4.Checked
                PhaseShift = 5 * PI / 4
                Return True
            Case rbn3Pi2.Checked
                PhaseShift = 3 * PI / 2
                Return True
            Case rbn7Pi4.Checked
                PhaseShift = 7 * PI / 4
                Return True
            Case Else
                Return False
        End Select
    End Function
    Private Sub txtPhaseShift_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhaseShift.GotFocus
        rbn0.Checked = False
        rbnPi4.Checked = False
        rbnPi2.Checked = False
        rbn3Pi4.Checked = False
        rbnPi.Checked = False
        rbn5Pi4.Checked = False
        rbn3Pi2.Checked = False
        rbn7Pi4.Checked = False
    End Sub
    Private Sub txtPhaseShift_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPhaseShift.TextChanged
        UpdatePhaseShiftInPi()
    End Sub
    Dim ErrorDescription As String = Nothing
    Sub UpdatePhaseShiftInPi()
        Dim Phase As Single
        If ReadRadioButtons(Phase) = True Then
            txtPhaseShift.Text = Phase
            If FDMath.oGetResult_in_Pi(CStr(Phase), lblResultInPi.Text, ErrorDescription) = False Then
                pbStatus.Image = ImgList1.Images(1)
                pbStatus.Tag = "error"
            Else
                pbStatus.Image = ImgList1.Images(0)
                pbStatus.Tag = "ok"
            End If
        Else
            If FDMath.oGetResult(txtPhaseShift.Text, Phase, ErrorDescription) = False Then
                pbStatus.Image = ImgList1.Images(1)
                pbStatus.Tag = "error"
                lblResultInPi.Text = ""
            Else
                pbStatus.Image = ImgList1.Images(0)
                pbStatus.Tag = "ok"
                Dim MyResult As String = Nothing
                If FDMath.oGetResult_in_Pi(Phase, MyResult, ErrorDescription) = True Then
                    lblResultInPi.Text = MyResult
                Else
                    lblResultInPi.Text = ""
                End If
            End If
        End If
    End Sub
    Private Sub pbStatus_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStatus.MouseEnter
        If pbStatus.Tag = "error" Then
            tt.Show(ErrorDescription, Me, 0, 175, 3000)
        End If
    End Sub
#Region "RadioButton changes"
    Private Sub rbn0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbn0.CheckedChanged
        UpdatePhaseShiftInPi()
    End Sub
    Private Sub rbnPi4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnPi4.CheckedChanged
        UpdatePhaseShiftInPi()
    End Sub
    Private Sub rbnPi2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnPi2.CheckedChanged
        UpdatePhaseShiftInPi()
    End Sub
    Private Sub rbn3Pi4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbn3Pi4.CheckedChanged
        UpdatePhaseShiftInPi()
    End Sub
    Private Sub rbnPi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnPi.CheckedChanged
        UpdatePhaseShiftInPi()
    End Sub
    Private Sub rbn5Pi4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbn5Pi4.CheckedChanged
        UpdatePhaseShiftInPi()
    End Sub
    Private Sub rbn3Pi2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbn3Pi2.CheckedChanged
        UpdatePhaseShiftInPi()
    End Sub
    Private Sub rbn7Pi4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbn7Pi4.CheckedChanged
        UpdatePhaseShiftInPi()
    End Sub
#End Region
End Class