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
Public Class frmGratingParameters
    Private Sub frmGratingParameters_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtBeamerGratingLocX1.Text = OpenGLRev01.OpenGLColor.oGetBeamerGratingX1()
        txtBeamerGratingLocY1.Text = OpenGLRev01.OpenGLColor.oGetBeamerGratingY1()
        txtBeamerGratingLocZ1.Text = OpenGLRev01.OpenGLColor.oGetBeamerGratingZ1()
        txtBeamerGratingLocX2.Text = OpenGLRev01.OpenGLColor.oGetBeamerGratingX2()
        txtBeamerGratingLocY2.Text = OpenGLRev01.OpenGLColor.oGetBeamerGratingY2()
        txtBeamerGratingLocZ2.Text = OpenGLRev01.OpenGLColor.oGetBeamerGratingZ2()
        txtCameraGratingLocX1.Text = OpenGLRev01.OpenGLColor.oGetCameraGratingX1()
        txtCameraGratingLocY1.Text = OpenGLRev01.OpenGLColor.oGetCameraGratingY1()
        txtCameraGratingLocZ1.Text = OpenGLRev01.OpenGLColor.oGetCameraGratingZ1()
        txtCameraGratingLocX2.Text = OpenGLRev01.OpenGLColor.oGetCameraGratingX2()
        txtCameraGratingLocY2.Text = OpenGLRev01.OpenGLColor.oGetCameraGratingY2()
        txtCameraGratingLocZ2.Text = OpenGLRev01.OpenGLColor.oGetCameraGratingZ2()

        txtBeamerResX.Text = OpenGLRev01.OpenGLColor.oGetBeamerPixelWidth()
        txtBeamerResY.Text = OpenGLRev01.OpenGLColor.oGetBeamerPixelHeight()
        txtCameraResX.Text = OpenGLRev01.OpenGLColor.oGetCameraGratingPixelWidth()
        txtCameraResY.Text = OpenGLRev01.OpenGLColor.oGetCameraGratingPixelHeight()

        txtBeamerGratingFrequency.Text = OpenGLRev01.OpenGLColor.oGetBeamerFrequency()
        txtBeamerGratingPhaseShift.Text = OpenGLRev01.OpenGLColor.oGetBeamerPhaseShift()
        txtCameraGratingFrequency.Text = OpenGLRev01.OpenGLColor.oGetCameraFrequency()
        txtCameraGratingPhaseShift.Text = OpenGLRev01.OpenGLColor.oGetCameraPhaseShift()
    End Sub
End Class