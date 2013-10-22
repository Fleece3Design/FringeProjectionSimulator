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
Public Class frmCameraParameters
    Private Sub frmCameraParameters_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtLocX.Text = OpenGLRev01.CameraSettings.oGetCameraLocationX
        txtLocY.Text = OpenGLRev01.CameraSettings.oGetCameraLocationY
        txtLocZ.Text = OpenGLRev01.CameraSettings.oGetCameraLocationZ

        txtTarX.Text = OpenGLRev01.CameraSettings.oGetCameraTargetX
        txtTarY.Text = OpenGLRev01.CameraSettings.oGetCameraTargetY
        txtTarZ.Text = OpenGLRev01.CameraSettings.oGetCameraTargetZ

        txtPOV.Text = OpenGLRev01.CameraSettings.oGetCameraFOV
        txtAspectRatio.Text = OpenGLRev01.CameraSettings.oGetCameraAspectRatio
        txtNear.Text = OpenGLRev01.CameraSettings.oGetCameraNear
        txtFar.Text = OpenGLRev01.CameraSettings.oGetCameraFar
    End Sub
End Class