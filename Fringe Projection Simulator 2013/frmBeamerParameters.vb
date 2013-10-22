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
Public Class frmBeamerParameters
    Private Sub frmBeamerParameters_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtLocX.Text = OpenGLRev01.OpenGLColor.oGetBeamerLocationX
        txtLocY.Text = OpenGLRev01.OpenGLColor.oGetBeamerLocationY
        txtLocZ.Text = OpenGLRev01.OpenGLColor.oGetBeamerLocationZ

        txtTarX.Text = OpenGLRev01.OpenGLColor.oGetBeamerTargetX
        txtTarY.Text = OpenGLRev01.OpenGLColor.oGetBeamerTargetY
        txtTarZ.Text = OpenGLRev01.OpenGLColor.ogetBeamerTargetZ

        txtResX.Text = OpenGLRev01.OpenGLColor.oGetBeamerPixelWidth
        txtResY.Text = OpenGLRev01.OpenGLColor.oGetBeamerPixelHeight

        txtAspectRatio.Text = OpenGLRev01.OpenGLColor.oGetBeamerRatio
        txtAngle.Text = OpenGLRev01.OpenGLColor.oGetBeamerAngle
    End Sub
End Class