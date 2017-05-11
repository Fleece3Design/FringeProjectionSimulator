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
Imports System.IO
Imports System.Xml
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports DR = System.Drawing
Imports M = System.Math
Imports OpenTK
Imports GAL = OpenTK.Graphics.All
Imports GL = OpenTK.Graphics.OpenGL.GL
Imports OpGL = OpenTK.Graphics.OpenGL
Imports GraphTK = OpenTK.Graphics
Imports GLUT = Tao.FreeGlut.Glut
Public Class OpenGLRev01
    Partial Class OpenGLMain
        Private Shared ModelView As Matrix4 = Matrix4.Identity
        Private Shared ModelView1 As Matrix4 = Matrix4.Identity
        Private Shared ModelView2 As Matrix4 = Matrix4.Identity
        Private Shared ModelZoomMatrix As Matrix4 = Matrix4.Identity
        Private Shared sglTotalZoomFactor As Single = 1
        Private Shared ModelTranslationMatrix As Matrix4 = Matrix4.Identity
        Private Shared TotalTranslation As Vector3 = Vector3.Zero
        Private Shared ModelRotationMatrix As Matrix4 = Matrix4.Identity
        Private Shared Projection As Matrix4 = Matrix4.Identity
        Private Shared vEye, vCenter, vUp As Vector3
        Private Shared MySetupType As SetupType = SetupType.TypeOne
        Private Shared RotAngle As Single
        Private Shared blnRunning As Boolean = False
        Private Shared intFPS As Integer
        Private Shared RunStatus As LoopStatus = LoopStatus.IDLE
        Private Shared intSleepTime As Integer
        Private Shared intScreenWidth As Integer
        Private Shared intScreenHeight As Integer
        Enum LoopStatus
            IDLE = 0
            FREERUN = 1
            MEASUREMENT = 2
            FINISHED = 3
        End Enum
        Public Enum SetupType
            TypeOne = 1
            TypeTwo = 2
            TypeThree = 3
        End Enum
        Public Shared Sub oSetRunning(ByVal Status As Boolean)
            blnRunning = Status
        End Sub
        Public Shared Function oGetRunning() As Boolean
            Return blnRunning
        End Function
        Public Shared Sub oSetFPS(ByVal FPS As Integer)
            intFPS = FPS
        End Sub
        Public Shared Function oGetFPS() As Integer
            Return intFPS
        End Function
        Public Shared Sub oSetRunStatus(ByVal Status As LoopStatus)
            RunStatus = Status
        End Sub
        Public Shared Function oGetRunStatus() As LoopStatus
            Return RunStatus
        End Function
        Public Shared Sub oSetSleepTime(ByVal ms As Integer)
            intSleepTime = ms
        End Sub
        Public Shared Function oGetSleepTime() As Integer
            Return intSleepTime
        End Function
        Public Shared Sub oRenderFrame(ByVal glctrl As GLControl, ByVal ArrayLists() As Integer)
            intScreenWidth = glctrl.Width
            intScreenHeight = glctrl.Height

            ''>>>>> Phase one <<<<<<
            'glctrl.Visible = False
            Dim vEye As New Vector3(OpenGLColor.oGetBeamerLocationX, OpenGLColor.oGetBeamerLocationY, OpenGLColor.oGetBeamerLocationZ)
            Dim vTarget As New Vector3(OpenGLColor.oGetBeamerTargetX, OpenGLColor.oGetBeamerTargetY, OpenGLColor.ogetBeamerTargetZ)
            Dim vUp As New Vector3(0.0, 1.0, 0.0)
            'Dim frustmat As Matrix4

            OpenGLColor.oCalculateEyePlanePoints()
            OpenGLColor.oCalculateBeamerGratingDistance()

            OpenGLColor.oUpdateShader()

            GL.Clear(OpGL.ClearBufferMask.ColorBufferBit Or OpGL.ClearBufferMask.DepthBufferBit)
            'GL.LoadIdentity()

            glctrl.Width = OpenGLColor.oGetShadowMapSize
            glctrl.Height = OpenGLColor.oGetShadowMapSize
            GL.Viewport(0, 0, OpenGLColor.oGetShadowMapSize, OpenGLColor.oGetShadowMapSize)

            'GL.Ortho(-GratingWidth / 2, GratingWidth / 2, -GratingHeight / 2, GratingHeight / 2, -vEye.Z, (vEye.Z - vTarget.Z) * 0.2)
            'GL.Ortho(-GratingWidth / 2, GratingWidth / 2, -GratingHeight / 2, GratingHeight / 2, -85, 85 * 0.2)
            'OrthoBoxLength = vEye.Z + (vEye.Z - vTarget.Z) * 0.2
            'OrthoBoxLength = 85 * 1.2
            'oUpdateShader()

            GL.ShadeModel(OpGL.ShadingModel.Flat)

            GL.PushMatrix()
            GL.MatrixMode(OpGL.MatrixMode.Modelview)

            OpenGLColor.oSetNear(OpenGLColor.oGetBeamerGratingDistance)
            OpenGLColor.oSetFar(OpenGLColor.oGetEyeTargetDistance * 1.15)

            CameraSettings.oSetFOVCameraPerspectiveDepth(vEye, vTarget, vUp, OpenGLColor.oGetBeamerGratingX1, OpenGLColor.oGetBeamerGratingX2, OpenGLColor.oGetBeamerGratingY1, OpenGLColor.oGetBeamerGratingY2, OpenGLColor.oGetNear, OpenGLColor.oGetFar)

            GL.CallList(1)
            GL.PopMatrix()

            'Read the depth buffer into the shadow map texture
            GL.BindTexture(OpGL.TextureTarget.Texture2D, OpenGLColor.oGetShadowMapTexture)
            GL.CopyTexSubImage2D(OpGL.TextureTarget.Texture2D, 0, 0, 0, 0, 0, OpenGLColor.oGetShadowMapSize, OpenGLColor.oGetShadowMapSize)
            'oTakeScreenshot("C:/depthMap.png", glctrl)
            '' Printscreen? => 32 bit
            'glctrl.SwapBuffers()
            'glctrl.Visible = True

            '>>>>> Phase two <<<<<<
            GL.Clear(Graphics.OpenGL.ClearBufferMask.ColorBufferBit Or Graphics.OpenGL.ClearBufferMask.DepthBufferBit) ' Or Graphics.OpenGL.ClearBufferMask.StencilBufferBit)

            'LOAD HERE CORRECT VIEW!!!!!!!!!!!!!!!!!!
            '<<<<< ERROR >>>>>

            'NEW
            glctrl.Width = intScreenWidth
            glctrl.Height = intScreenHeight
            GL.Viewport(0, 0, glctrl.Width, glctrl.Height)
            'END NEW

            'PushMatrix()

            'OpenTK.Graphics.OpenGL.GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview)
            GL.MatrixMode(OpGL.MatrixMode.Modelview)
            GL.LoadIdentity()

            'OpenTK.Graphics.OpenGL.GL.Translate(1, 5, 10)
            'OpenTK.Graphics.OpenGL.GL.Rotate(PI / 2, 1, 1, 0)
            'glutSolidTeapot(5)

            ModelView1 = Matrix4.Mult(ModelZoomMatrix, Matrix4.Identity)
            ModelView2 = Matrix4.Mult(ModelTranslationMatrix, ModelView1)
            ModelView = Matrix4.Mult(ModelRotationMatrix, ModelView2)

            GL.LoadMatrix(ModelView)

            'PopMatrix()
            For i As Integer = 0 To UBound(ArrayLists)
                If ArrayLists(i) = 1 Then
                    OpenGLColor.oSetModelColor(Drawing.Color.Green, Drawing.Color.Green, Drawing.Color.Black, Drawing.Color.Black, Drawing.Color.Black)
                    GL.CallList(1)
                ElseIf ArrayLists(i) = 2 Then
                    OpenGLColor.oSetModelColor(Drawing.Color.Red, Drawing.Color.Red, Drawing.Color.Black, Drawing.Color.Black, Drawing.Color.Black)
                    GL.CallList(2)
                End If
            Next

            'LoadIdentity()
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)

            'LoadIdentity()
            'GL.Rotate(PI / 100, 0, 1, 0)

            ''wrong !!!
            ' LoadIdentity()
            'RotAngle += 0.1
            'LoadMatrix(Matrix4.Mult(Matrix4.CreateOrthographicOffCenter(-50, 50, -50, 50, -1000, 1000), Matrix4.CreateRotationY(RotAngle)))


            Try
                glctrl.SwapBuffers()
                'glctrl.Invalidate()
            Catch ex As Exception
                MsgBox("Swap error")
            End Try
        End Sub
        Public Shared Sub oRenderFrame(ByVal glctrl As GLControl, ByVal ArrayLists() As Integer, strScreenShotPath As String)
            intScreenWidth = glctrl.Width
            intScreenHeight = glctrl.Height

            ''>>>>> Phase one <<<<<<
            'glctrl.Visible = False
            Dim vEye As New Vector3(OpenGLColor.oGetBeamerLocationX, OpenGLColor.oGetBeamerLocationY, OpenGLColor.oGetBeamerLocationZ)
            Dim vTarget As New Vector3(OpenGLColor.oGetBeamerTargetX, OpenGLColor.oGetBeamerTargetY, OpenGLColor.ogetBeamerTargetZ)
            Dim vUp As New Vector3(0.0, 1.0, 0.0)
            'Dim frustmat As Matrix4

            OpenGLColor.oCalculateEyePlanePoints()
            OpenGLColor.oCalculateBeamerGratingDistance()

            OpenGLColor.oUpdateShader()

            GL.Clear(OpGL.ClearBufferMask.ColorBufferBit Or OpGL.ClearBufferMask.DepthBufferBit)
            'GL.LoadIdentity()

            glctrl.Width = OpenGLColor.oGetShadowMapSize
            glctrl.Height = OpenGLColor.oGetShadowMapSize
            GL.Viewport(0, 0, OpenGLColor.oGetShadowMapSize, OpenGLColor.oGetShadowMapSize)

            'GL.Ortho(-GratingWidth / 2, GratingWidth / 2, -GratingHeight / 2, GratingHeight / 2, -vEye.Z, (vEye.Z - vTarget.Z) * 0.2)
            'GL.Ortho(-GratingWidth / 2, GratingWidth / 2, -GratingHeight / 2, GratingHeight / 2, -85, 85 * 0.2)
            'OrthoBoxLength = vEye.Z + (vEye.Z - vTarget.Z) * 0.2
            'OrthoBoxLength = 85 * 1.2
            'oUpdateShader()

            GL.ShadeModel(OpGL.ShadingModel.Flat)

            GL.PushMatrix()
            GL.MatrixMode(OpGL.MatrixMode.Modelview)

            OpenGLColor.oSetNear(OpenGLColor.oGetBeamerGratingDistance)
            OpenGLColor.oSetFar(OpenGLColor.oGetEyeTargetDistance * 1.15)

            CameraSettings.oSetFOVCameraPerspectiveDepth(vEye, vTarget, vUp, OpenGLColor.oGetBeamerGratingX1, OpenGLColor.oGetBeamerGratingX2, OpenGLColor.oGetBeamerGratingY1, OpenGLColor.oGetBeamerGratingY2, OpenGLColor.oGetNear, OpenGLColor.oGetFar)
            GL.CallList(1)
            GL.PopMatrix()

            'Read the depth buffer into the shadow map texture
            GL.BindTexture(OpGL.TextureTarget.Texture2D, OpenGLColor.oGetShadowMapTexture)
            GL.CopyTexSubImage2D(OpGL.TextureTarget.Texture2D, 0, 0, 0, 0, 0, OpenGLColor.oGetShadowMapSize, OpenGLColor.oGetShadowMapSize)
            'oTakeScreenshot("C:/depthMap.png", glctrl)
            '' Printscreen? => 32 bit
            'glctrl.SwapBuffers()
            'glctrl.Visible = True

            '>>>>> Phase two <<<<<<
            GL.Clear(Graphics.OpenGL.ClearBufferMask.ColorBufferBit Or Graphics.OpenGL.ClearBufferMask.DepthBufferBit Or Graphics.OpenGL.ClearBufferMask.StencilBufferBit)

            'LOAD HERE CORRECT VIEW!!!!!!!!!!!!!!!!!!
            '<<<<< ERROR >>>>>

            'NEW
            glctrl.Width = intScreenWidth
            glctrl.Height = intScreenHeight
            GL.Viewport(0, 0, glctrl.Width, glctrl.Height)
            'END NEW

            'PushMatrix()

            'OpenTK.Graphics.OpenGL.GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview)
            GL.MatrixMode(OpGL.MatrixMode.Modelview)
            GL.LoadIdentity()

            'OpenTK.Graphics.OpenGL.GL.Translate(1, 5, 10)
            'OpenTK.Graphics.OpenGL.GL.Rotate(PI / 2, 1, 1, 0)
            'glutSolidTeapot(5)

            ModelView1 = Matrix4.Mult(ModelZoomMatrix, Matrix4.Identity)
            ModelView2 = Matrix4.Mult(ModelTranslationMatrix, ModelView1)
            ModelView = Matrix4.Mult(ModelRotationMatrix, ModelView2)

            GL.LoadMatrix(ModelView)

            'PopMatrix()
            For i As Integer = 0 To UBound(ArrayLists)
                If ArrayLists(i) = 1 Then
                    OpenGLColor.oSetModelColor(Drawing.Color.Green, Drawing.Color.Green, Drawing.Color.Black, Drawing.Color.Black, Drawing.Color.Black)
                    GL.CallList(1)
                ElseIf ArrayLists(i) = 2 Then
                    OpenGLColor.oSetModelColor(Drawing.Color.Red, Drawing.Color.Red, Drawing.Color.Black, Drawing.Color.Black, Drawing.Color.Black)
                    GL.CallList(2)
                End If
            Next

            'LoadIdentity()
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)

            'LoadIdentity()
            'GL.Rotate(PI / 100, 0, 1, 0)

            ''wrong !!!
            ' LoadIdentity()
            'RotAngle += 0.1
            'LoadMatrix(Matrix4.Mult(Matrix4.CreateOrthographicOffCenter(-50, 50, -50, 50, -1000, 1000), Matrix4.CreateRotationY(RotAngle)))

            Try
                oTakeScreenshot(strScreenShotPath, glctrl)
                glctrl.SwapBuffers()
                'glctrl.Invalidate()
            Catch ex As Exception
                MsgBox("Swap error")
            End Try
        End Sub
        Public Shared Sub oRenderFrame(ByVal glctrl As GLControl, ByVal ArrayLists() As Integer, ByVal BackColor As Drawing.Color)
            OpenGLColor.oSetBackgroundColor(BackColor)
            OpenGLColor.oSetModelColoring()
            GL.Clear(Graphics.OpenGL.ClearBufferMask.ColorBufferBit Or Graphics.OpenGL.ClearBufferMask.DepthBufferBit Or Graphics.OpenGL.ClearBufferMask.StencilBufferBit)

            'PushMatrix()

            OpenTK.Graphics.OpenGL.GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview)
            GL.LoadIdentity()

            'OpenTK.Graphics.OpenGL.GL.Translate(1, 5, 10)
            'OpenTK.Graphics.OpenGL.GL.Rotate(PI / 2, 1, 1, 0)
            'glutSolidTeapot(5)

            ModelView1 = Matrix4.Mult(ModelZoomMatrix, Matrix4.Identity)
            ModelView2 = Matrix4.Mult(ModelTranslationMatrix, ModelView1)
            ModelView = Matrix4.Mult(ModelRotationMatrix, ModelView2)

            GL.LoadMatrix(ModelView)

            'PopMatrix()
            For i As Integer = 0 To UBound(ArrayLists)
                If ArrayLists(i) = 1 Then
                    OpenGLColor.oSetModelColor(Drawing.Color.Green, Drawing.Color.Green, Drawing.Color.Black, Drawing.Color.Black, Drawing.Color.Black)
                    GL.CallList(1)
                ElseIf ArrayLists(i) = 2 Then
                    OpenGLColor.oSetModelColor(Drawing.Color.Red, Drawing.Color.Red, Drawing.Color.Black, Drawing.Color.Black, Drawing.Color.Black)
                    GL.CallList(2)
                End If
            Next



            'LoadIdentity()
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)

            'LoadIdentity()
            'GL.Rotate(PI / 100, 0, 1, 0)

            ''wrong !!!
            ' LoadIdentity()
            'RotAngle += 0.1
            'LoadMatrix(Matrix4.Mult(Matrix4.CreateOrthographicOffCenter(-50, 50, -50, 50, -1000, 1000), Matrix4.CreateRotationY(RotAngle)))



            Try
                glctrl.SwapBuffers()
                'glctrl.Invalidate()
            Catch ex As Exception

            End Try
        End Sub
        Public Shared Sub oRenderFrame(ByVal glctrl As GLControl, ByVal Frequency As Double, ByVal Color As Drawing.Color)
            GL.Clear(Graphics.OpenGL.ClearBufferMask.ColorBufferBit Or Graphics.OpenGL.ClearBufferMask.DepthBufferBit Or Graphics.OpenGL.ClearBufferMask.StencilBufferBit)

            OpenTK.Graphics.OpenGL.GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview)
            GL.LoadIdentity()
            GL.LoadMatrix(ModelView)
            Draw.oDrawingVerticalFringes(glctrl, Frequency, Color)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)

            Try
                glctrl.SwapBuffers()
            Catch ex As Exception

            End Try
        End Sub
        Public Shared Sub oRenderFrame(ByVal glctrl As GLControl, ByVal Color As Drawing.Color)
            OpenGLColor.oSetBackgroundColor(Color)
            OpenGLColor.oSetModelColoring()
            GL.Clear(Graphics.OpenGL.ClearBufferMask.ColorBufferBit Or Graphics.OpenGL.ClearBufferMask.DepthBufferBit Or Graphics.OpenGL.ClearBufferMask.StencilBufferBit)

            'OpenTK.Graphics.OpenGL.GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview)
            'GL.LoadIdentity()
            'GL.LoadMatrix(ModelView)
            'Draw.oDrawingVerticalFringes(glctrl, Frequency)
            'GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)

            Try
                glctrl.SwapBuffers()
            Catch ex As Exception

            End Try
        End Sub
        Public Shared Sub oRenderFrame(ByVal glctrl As GLControl, ByVal Color As Drawing.Color, strScreenShotPath As String)
            OpenGLColor.oSetBackgroundColor(Color)
            OpenGLColor.oSetModelColoring()
            GL.Clear(Graphics.OpenGL.ClearBufferMask.ColorBufferBit Or Graphics.OpenGL.ClearBufferMask.DepthBufferBit Or Graphics.OpenGL.ClearBufferMask.StencilBufferBit)

            'OpenTK.Graphics.OpenGL.GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Modelview)
            'GL.LoadIdentity()
            'GL.LoadMatrix(ModelView)
            'Draw.oDrawingVerticalFringes(glctrl, Frequency)
            'GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)

            Try
                oTakeScreenshot(strScreenShotPath, glctrl)
                glctrl.SwapBuffers()
            Catch ex As Exception

            End Try
        End Sub
        Public Shared Sub oGLInitialize(ByVal glctrl As GLControl, ByVal clrClearColor As Drawing.Color, ByVal blnStandardColoring As Boolean)
            GLUT.glutInit()

            OpenGLColor.oSetBackgroundColor(clrClearColor)
            'Enable(Graphics.OpenGL.EnableCap.DepthTest)

            GL.Viewport(0, 0, glctrl.Width, glctrl.Height)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)
            GL.LoadIdentity()
            GL.Ortho(-20, 20, -15, 15, -1000, 1000)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Modelview)

            If blnStandardColoring = True Then
                OpenGLColor.oSetModelColoring()
            Else
                'nothing
            End If
        End Sub
        Public Shared Sub oGLViewportUpdate(ByVal glctrl As GLControl)
            GL.Viewport(0, 0, glctrl.Width, glctrl.Height)
        End Sub
        Public Shared Sub oWait(ByVal intTimeMilliseconds As Integer)
            System.Threading.Thread.Sleep(intTimeMilliseconds)
        End Sub
        Public Shared Sub oZoom(ByVal sglZoomFactor As Single)
            sglTotalZoomFactor += sglZoomFactor
            ModelZoomMatrix = Matrix4.Scale(sglTotalZoomFactor)
        End Sub
        Public Shared Sub oTranslate(ByVal sglTranslateX As Single, ByVal sglTranslateY As Single, ByVal sglTranslateZ As Single)
            oResetModelTranslation()
            TotalTranslation.X += sglTranslateX
            TotalTranslation.Y += sglTranslateY
            TotalTranslation.Z += sglTranslateZ
            ModelTranslationMatrix = Matrix4.CreateTranslation(TotalTranslation)
        End Sub
        Public Shared Sub oRotateY(ByVal Angle As Single)
            oResetModelRotation()
            Dim RotationVector As Vector3 = Vector3.UnitY
            Dim RotMat As Matrix4

            RotMat = Matrix4.CreateFromAxisAngle(RotationVector, Angle)
            ModelRotationMatrix = Matrix4.Mult(RotMat, ModelRotationMatrix)
        End Sub
        Public Shared Sub oRotateYShifted(ByVal Angle As Single, ByVal Translation As Vector3)
            oResetModelRotation()

            Dim RotationAxis As Vector3
            RotationAxis = Vector3.UnitY

            Dim T As Matrix4
            Dim M As Matrix4
            Dim Tn As Matrix4
            Dim tmp As Matrix4

            T = Matrix4.CreateTranslation(Translation)
            M = Matrix4.CreateFromAxisAngle(RotationAxis, Angle)
            Tn = Matrix4.CreateTranslation(-Translation)

            tmp = Matrix4.Mult(Tn, M)
            tmp = Matrix4.Mult(tmp, T)

            'ModelRotationMatrix = Matrix4.Transpose(tmp)
            ModelRotationMatrix = tmp
        End Sub
        Public Shared Sub oRotateXShifted(ByVal Angle As Single, ByVal Translation As Vector3)
            oResetModelRotation()

            Dim RotationAxis As Vector3
            RotationAxis = Vector3.UnitX

            Dim T As Matrix4
            Dim M As Matrix4
            Dim Tn As Matrix4
            Dim tmp As Matrix4

            T = Matrix4.CreateTranslation(Translation)
            M = Matrix4.CreateFromAxisAngle(RotationAxis, Angle)
            Tn = Matrix4.CreateTranslation(-Translation)

            tmp = Matrix4.Mult(Tn, M)
            tmp = Matrix4.Mult(tmp, T)

            'ModelRotationMatrix = Matrix4.Transpose(tmp)
            ModelRotationMatrix = tmp
        End Sub
        Public Shared Sub oLoop(ByVal GLC1 As GLControl, ByVal ArrayLists() As Integer)
            Dim intSec As Integer
            Dim intFPS As Integer = 0
            Dim intBlackoutIndex As Integer = 0
            Dim intMeasurementIndex As Integer = 0

            If RunStatus = LoopStatus.MEASUREMENT Then
                MsgBox("Please specify 'measurement' argument in function call 'oLoop'")
                Exit Sub
            Else
                While blnRunning = True
                    OpenGLMain.oRenderFrame(GLC1, ArrayLists)
                    System.Threading.Thread.Sleep(oGetSleepTime)

                    Application.DoEvents()
                    If intSec <> CStr(Date.Now.Second) Then
                        oSetFPS(intFPS)
                        intFPS = 0
                        intSec = CInt(Date.Now.Second)
                    Else
                        intFPS += 1
                    End If
                End While
            End If

            'While (blnRunning = True)
            '    If RunStatus = LoopStatus.MEASUREMENT Then
            '        If intBlackoutIndex < intBlackNumberImages Then
            '            OpenGLColor.sglBlackout = 1
            '            OpenGLColor.sglFrequency = OpenGLColor.sglStartFrequency
            '            OpenGLColor.UpdateShader()
            '            OpenGLMain.RenderFrame(GLC1, intListArray)
            '            OpenGLMain.TakeScreenshot(strMeasurementLocation & "\" & Format(intImageNumber, "0000") & ".png", GLC1)
            '        ElseIf OpenGLColor.sglFrequency <= OpenGLColor.sglStopFrequency Then
            '            OpenGLColor.sglBlackout = 0
            '            OpenGLColor.UpdateShader()
            '            OpenGLMain.RenderFrame(GLC1, intListArray)
            '            OpenGLMain.TakeScreenshot(strMeasurementLocation & "\" & Format(intImageNumber, "0000") & ".png", GLC1)
            '            OpenGLColor.sglFrequency += OpenGLColor.sglFrequencyDifference
            '        Else
            '            If intBlackoutIndex < intBlackNumberImages Then
            '                OpenGLColor.sglBlackout = 1
            '                OpenGLColor.UpdateShader()
            '                OpenGLMain.RenderFrame(GLC1, intListArray)
            '                OpenGLMain.TakeScreenshot(strMeasurementLocation & "\" & Format(intImageNumber, "0000") & ".png", GLC1)
            '                intBlackoutIndex += 1
            '                'Me.Text = intBlackoutIndex
            '            Else
            '                blnMeasurement = False
            '                OpenGLColor.sglBlackout = 0
            '                OpenGLColor.UpdateShader()
            '                'TSL2.Text = "Measurement stopped"
            '            End If
            '        End If
            '        intImageNumber += 1
            '    ElseIf blnPhaseMeasurement = True Then
            '        If intImageNumber < intBlackNumberImages Then
            '            OpenGLColor.sglBlackout = 1
            '            If blnPhaseBothGratings = True Then
            '                OpenGLColor.sglPhaseShift = OpenGLColor.sglStartPhase
            '                OpenGLColor.sglGratingShift = OpenGLColor.sglStartPhase
            '            Else
            '                OpenGLColor.sglPhaseShift = OpenGLColor.sglStartPhase
            '            End If
            '            OpenGLColor.UpdateShader()
            '            OpenGLMain.RenderFrame(GLC1, intListArray)
            '            OpenGLMain.TakeScreenshot(strMeasurementLocation & "\" & Format(intImageNumber, "0000") & ".png", GLC1)
            '        ElseIf OpenGLColor.sglPhaseShift <= OpenGLColor.sglStopPhase Then
            '            OpenGLColor.sglBlackout = 0
            '            OpenGLColor.UpdateShader()
            '            OpenGLMain.RenderFrame(GLC1, intListArray)
            '            OpenGLMain.TakeScreenshot(strMeasurementLocation & "\" & Format(intImageNumber, "0000") & ".png", GLC1)
            '            If blnPhaseBothGratings = True Then
            '                OpenGLColor.sglPhaseShift += OpenGLColor.sglPhaseDifference
            '                OpenGLColor.sglGratingShift += OpenGLColor.sglPhaseDifference
            '            Else
            '                OpenGLColor.sglPhaseShift += OpenGLColor.sglPhaseDifference
            '            End If
            '        Else
            '            If intBlackoutIndex < intBlackNumberImages Then
            '                OpenGLColor.sglBlackout = 1
            '                OpenGLColor.UpdateShader()
            '                OpenGLMain.RenderFrame(GLC1, intListArray)
            '                OpenGLMain.TakeScreenshot(strMeasurementLocation & "\" & Format(intImageNumber, "0000") & ".png", GLC1)
            '                intBlackoutIndex += 1
            '                'Me.Text = intBlackoutIndex
            '            Else
            '                blnPhaseMeasurement = False
            '                OpenGLColor.sglBlackout = 0
            '                OpenGLColor.UpdateShader()
            '                'TSL2.Text = "Measurement stopped"
            '            End If
            '        End If
            '        intImageNumber += 1
            '    Else
            '        OpenGLMain.RenderFrame(GLC1, intListArray)
            '        System.Threading.Thread.Sleep(intSleepTimems)
            '    End If




        End Sub
        Public Shared Sub oLoop(ByVal GLC1 As GLControl, ByVal ArrayLists() As Integer, ByVal BackColor As Drawing.Color)
            Dim intSec As Integer
            Dim intFPS As Integer = 0
            Dim intBlackoutIndex As Integer = 0
            Dim intMeasurementIndex As Integer = 0

            If RunStatus = LoopStatus.MEASUREMENT Then
                MsgBox("Please specify 'measurement' argument in function call 'oLoop'")
                Exit Sub
            Else
                While blnRunning = True
                    OpenGLMain.oRenderFrame(GLC1, ArrayLists, BackColor)
                    System.Threading.Thread.Sleep(oGetSleepTime)

                    Application.DoEvents()
                    If intSec <> CStr(Date.Now.Second) Then
                        oSetFPS(intFPS)
                        intFPS = 0
                        intSec = CInt(Date.Now.Second)
                    Else
                        intFPS += 1
                    End If
                End While
            End If
        End Sub
        Public Shared Sub oLoop(ByVal GLC1 As GLControl, ByVal ArrayLists() As Integer, ByVal Measurement() As ReadSettings.Measurement, ByVal BlackoutColor As Drawing.Color)
            Dim intSec As Integer
            Dim intFPS As Integer = 0
            Dim intBlackoutIndex As Integer = 0
            Dim intMeasurementIndex As Integer = 0
            Dim sglFrequency As Single = 0
            Dim sglPhase As Single = 0
            Dim sglFreqDif As Single = 0
            Dim sglPhaseDif As Single = 0
            Dim sglCamPhase As Single = 0
            Dim sglCamFreq As Single = 0
            Dim sglCamPhaseDif As Single = 0
            Dim sglCamFreqDif As Single = 0

            If RunStatus = LoopStatus.FREERUN Then
                While blnRunning = True
                    OpenGLMain.oRenderFrame(GLC1, ArrayLists)
                    System.Threading.Thread.Sleep(oGetSleepTime)

                    Application.DoEvents()
                    If intSec <> CStr(Date.Now.Second) Then
                        oSetFPS(intFPS)
                        intFPS = 0
                        intSec = CInt(Date.Now.Second)
                    Else
                        intFPS += 1
                    End If
                End While
            ElseIf RunStatus = LoopStatus.MEASUREMENT Then
                If Measurement Is Nothing Then
                    MsgBox("No measurement settings available")
                    Exit Sub
                End If
                For i As Integer = 0 To 100
                    oRenderFrame(GLC1, Drawing.Color.PowderBlue)
                    oWait(oGetSleepTime)
                Next
                For i As Integer = 0 To UBound(Measurement)
                    'Beamer parameters
                    sglPhase = Measurement(i).BeamerGratingStartPhase
                    sglFrequency = Measurement(i).BeamerGratingStartFrequency
                    sglPhaseDif = (Measurement(i).BeamerGratingStopPhase - Measurement(i).BeamerGratingStartPhase) / Measurement(i).NumberSteps
                    sglFreqDif = (Measurement(i).BeamerGratingStopFrequency - Measurement(i).BeamerGratingStartFrequency) / Measurement(i).NumberSteps
                    OpenGLColor.oSetBeamerFrequency(sglFrequency)
                    OpenGLColor.oSetBeamerPhaseShift(sglPhase)

                    'Camera parameters
                    sglCamPhase = Measurement(i).CameraGratingStartPhase
                    sglCamFreq = Measurement(i).CameraGratingStartFrequency
                    sglCamPhaseDif = (Measurement(i).CameraGratingStopPhase - Measurement(i).CameraGratingStartPhase) / Measurement(i).NumberSteps
                    sglCamFreqDif = (Measurement(i).CameraGratingStopFrequency - Measurement(i).CameraGratingStartFrequency) / Measurement(i).NumberSteps
                    OpenGLColor.oSetCameraGratingFrequency(sglCamFreq)
                    OpenGLColor.oSetCameraGratingPhaseShift(sglCamPhase)

                    While intBlackoutIndex < Measurement(i).NumberBlackoutImages

                        If oGetRunning() = False Then
                            Exit Sub
                        End If

                        oRenderFrame(GLC1, BlackoutColor)
                        intBlackoutIndex += 1

                        Application.DoEvents()
                        oWait(oGetSleepTime)
                        If intSec <> CStr(Date.Now.Second) Then
                            oSetFPS(intFPS)
                            intFPS = 0
                            intSec = CInt(Date.Now.Second)
                        Else
                            intFPS += 1
                        End If
                    End While
                    intBlackoutIndex = 0

                    While intMeasurementIndex < Measurement(i).NumberSteps

                        If oGetRunning() = False Then
                            Exit Sub
                        End If

                        'oRenderFrame(GLC1, Drawing.Color.Purple)
                        OpenGLColor.oSetBeamerFrequency(OpenGLColor.oGetBeamerFrequency + sglFreqDif)
                        OpenGLColor.oSetBeamerPhaseShift(OpenGLColor.oGetBeamerPhaseShift + sglPhaseDif)
                        OpenGLColor.oSetCameraGratingFrequency(OpenGLColor.oGetCameraFrequency + sglCamFreqDif)
                        OpenGLColor.oSetCameraGratingPhaseShift(OpenGLColor.oGetCameraPhaseShift + sglCamPhaseDif)

                        oRenderFrame(GLC1, Measurement(i).intListArray)
                        intMeasurementIndex += 1

                        Application.DoEvents()
                        oWait(oGetSleepTime)
                        If intSec <> CStr(Date.Now.Second) Then
                            oSetFPS(intFPS)
                            intFPS = 0
                            intSec = CInt(Date.Now.Second)
                        Else
                            intFPS += 1
                        End If
                    End While
                    intMeasurementIndex = 0

                    While intBlackoutIndex < Measurement(i).NumberBlackoutImages

                        If oGetRunning() = False Then
                            Exit Sub
                        End If

                        oRenderFrame(GLC1, BlackoutColor)
                        intBlackoutIndex += 1

                        Application.DoEvents()
                        oWait(oGetSleepTime)
                        If intSec <> CStr(Date.Now.Second) Then
                            oSetFPS(intFPS)
                            intFPS = 0
                            intSec = CInt(Date.Now.Second)
                        Else
                            intFPS += 1
                        End If
                    End While
                    intBlackoutIndex = 0
                Next
                oRenderFrame(GLC1, Drawing.Color.Green)
                RunStatus = LoopStatus.FINISHED
            End If
        End Sub
        Public Shared Sub oLoop(ByVal GLC1 As GLControl, ByVal ArrayLists() As Integer, ByVal Measurement() As ReadSettings.Measurement, ByVal BlackoutColor As Drawing.Color, ByVal blnFileSaving As Boolean)
            Dim intSec As Integer
            Dim intFPS As Integer = 0
            Dim intBlackoutIndex As Integer = 0
            Dim intMeasurementIndex As Integer = 0
            Dim intFileIndex As Integer = 0
            Dim sglFrequency As Single = 0
            Dim sglPhase As Single = 0
            Dim sglFreqDif As Single = 0
            Dim sglPhaseDif As Single = 0
            Dim sglCamPhase As Single = 0
            Dim sglCamFreq As Single = 0
            Dim sglCamPhaseDif As Single = 0
            Dim sglCamFreqDif As Single = 0
            Dim MeasurementTimer As New Stopwatch

            If RunStatus = LoopStatus.FREERUN Then
                While blnRunning = True
                    OpenGLMain.oRenderFrame(GLC1, ArrayLists)
                    System.Threading.Thread.Sleep(oGetSleepTime)

                    Application.DoEvents()
                    If intSec <> CStr(Date.Now.Second) Then
                        oSetFPS(intFPS)
                        intFPS = 0
                        intSec = CInt(Date.Now.Second)
                    Else
                        intFPS += 1
                    End If
                End While
            ElseIf RunStatus = LoopStatus.MEASUREMENT Then
                If Measurement Is Nothing Then
                    MsgBox("No measurement settings available")
                    Exit Sub
                End If
                For i As Integer = 0 To 100
                    oRenderFrame(GLC1, Drawing.Color.PowderBlue)
                    oWait(oGetSleepTime)
                Next
                MeasurementTimer.Start()
                For i As Integer = 0 To UBound(Measurement)
                    Try
                        'MsgBox(Measurement(i).MapPath & "  " & CStr(i))
                        If Directory.Exists(Measurement(i).MapPath) = False Then
                            Directory.CreateDirectory(Measurement(i).MapPath)
                        Else
                            Directory.Delete(Measurement(i).MapPath, True)
                            Directory.CreateDirectory(Measurement(i).MapPath)
                        End If
                    Catch ex As Exception
                        MsgBox("Error 002 - " & ex.Message)
                    End Try

                    intFileIndex = 0

                    'Beamer parameters
                    sglPhase = Measurement(i).BeamerGratingStartPhase
                    sglFrequency = Measurement(i).BeamerGratingStartFrequency
                    sglPhaseDif = (Measurement(i).BeamerGratingStopPhase - Measurement(i).BeamerGratingStartPhase) / Measurement(i).NumberSteps
                    sglFreqDif = (Measurement(i).BeamerGratingStopFrequency - Measurement(i).BeamerGratingStartFrequency) / Measurement(i).NumberSteps
                    OpenGLColor.oSetBeamerFrequency(sglFrequency)
                    OpenGLColor.oSetBeamerPhaseShift(sglPhase)
                    'MsgBox("Beamer Phase Dif: " & sglPhaseDif)
                    'MsgBox("Beamer Freq Dif: " & sglFreqDif)

                    'Camera parameters
                    sglCamPhase = Measurement(i).CameraGratingStartPhase
                    sglCamFreq = Measurement(i).CameraGratingStartFrequency
                    sglCamPhaseDif = (Measurement(i).CameraGratingStopPhase - Measurement(i).CameraGratingStartPhase) / Measurement(i).NumberSteps
                    sglCamFreqDif = (Measurement(i).CameraGratingStopFrequency - Measurement(i).CameraGratingStartFrequency) / Measurement(i).NumberSteps
                    OpenGLColor.oSetCameraGratingFrequency(sglCamFreq)
                    OpenGLColor.oSetCameraGratingPhaseShift(sglCamPhase)
                    'MsgBox("Camera Phase Dif: " & sglCamPhaseDif)
                    'MsgBox("Camera Freq Dif: " & sglCamFreqDif)

                    If Measurement(i).Type = ReadSettings.MeasurementType.MOIRE Then
                        OpenGLColor.oActivateCameraGrating()
                        OpenGLSettings.oSetBlending(True)
                        Draw.oDrawGrating(2)
                        OpenGLColor.oUpdateShader()
                    Else
                        OpenGLColor.oDeactivateCameraGrating()
                        OpenGLSettings.oSetBlending(False)
                        OpenGLColor.oUpdateShader()
                    End If

                    While intBlackoutIndex < Measurement(i).NumberBlackoutImages

                        If oGetRunning() = False Then
                            Exit Sub
                        End If

                        'oRenderFrame(GLC1, BlackoutColor)
                        If blnFileSaving = True Then
                            oRenderFrame(GLC1, BlackoutColor, Measurement(i).MapPath & Measurement(i).FileName & oGetFixedNumberFileName(intFileIndex) & ".png")
                            'oTakeScreenshot(Measurement(i).MapPath & Measurement(i).FileName & oGetFixedNumberFileName(intFileIndex) & ".png", GLC1)
                            '  MsgBox(Measurement(i).MapPath & Measurement(i).FileName & intFileIndex & ".png")
                            intFileIndex += 1
                        Else
                            oRenderFrame(GLC1, BlackoutColor)
                        End If

                        intBlackoutIndex += 1

                        Application.DoEvents()
                        oWait(oGetSleepTime)
                        If intSec <> CStr(Date.Now.Second) Then
                            oSetFPS(intFPS)
                            intFPS = 0
                            intSec = CInt(Date.Now.Second)
                        Else
                            intFPS += 1
                        End If
                    End While
                    intBlackoutIndex = 0

                    While intMeasurementIndex < Measurement(i).NumberSteps

                        If oGetRunning() = False Then
                            Exit Sub
                        End If

                        'oRenderFrame(GLC1, Drawing.Color.Purple)
                        OpenGLColor.oSetBeamerFrequency(OpenGLColor.oGetBeamerFrequency + sglFreqDif)
                        OpenGLColor.oSetBeamerPhaseShift(OpenGLColor.oGetBeamerPhaseShift + sglPhaseDif)
                        OpenGLColor.oSetCameraGratingFrequency(OpenGLColor.oGetCameraFrequency + sglCamFreqDif)
                        OpenGLColor.oSetCameraGratingPhaseShift(OpenGLColor.oGetCameraPhaseShift + sglCamPhaseDif)

                        ''Original version

                        If blnFileSaving = True Then
                            oRenderFrame(GLC1, Measurement(i).intListArray, Measurement(i).MapPath & Measurement(i).FileName & oGetFixedNumberFileName(intFileIndex) & ".png")
                            'oTakeScreenshot(Measurement(i).MapPath & Measurement(i).FileName & oGetFixedNumberFileName(intFileIndex) & ".png", GLC1)
                            intFileIndex += 1
                        Else
                            oRenderFrame(GLC1, Measurement(i).intListArray)
                        End If
                        'Test version
                        'If blnFileSaving = True Then
                        '    oRenderFrame(GLC1, Measurement(i).intListArray, Measurement(i).MapPath & Measurement(i).FileName & oGetFixedNumberFileName(intFileIndex) & ".png")
                        '    intFileIndex += 1
                        'End If

                        intMeasurementIndex += 1

                        Application.DoEvents()
                        oWait(oGetSleepTime)
                        If intSec <> CStr(Date.Now.Second) Then
                            oSetFPS(intFPS)
                            intFPS = 0
                            intSec = CInt(Date.Now.Second)
                        Else
                            intFPS += 1
                        End If
                    End While
                    intMeasurementIndex = 0

                    While intBlackoutIndex < Measurement(i).NumberBlackoutImages

                        If oGetRunning() = False Then
                            Exit Sub
                        End If

                        If blnFileSaving = True Then
                            oRenderFrame(GLC1, BlackoutColor, Measurement(i).MapPath & Measurement(i).FileName & oGetFixedNumberFileName(intFileIndex) & ".png")
                            'oTakeScreenshot(Measurement(i).MapPath & Measurement(i).FileName & oGetFixedNumberFileName(intFileIndex) & ".png", GLC1)
                            intFileIndex += 1
                        Else
                            oRenderFrame(GLC1, BlackoutColor)
                        End If

                        intBlackoutIndex += 1

                        Application.DoEvents()
                        oWait(oGetSleepTime)
                        If intSec <> CStr(Date.Now.Second) Then
                            oSetFPS(intFPS)
                            intFPS = 0
                            intSec = CInt(Date.Now.Second)
                        Else
                            intFPS += 1
                        End If
                    End While
                    intBlackoutIndex = 0
                Next
                MeasurementTimer.Stop()
                MsgBox(MeasurementTimer.ElapsedMilliseconds)
                oRenderFrame(GLC1, Drawing.Color.Green)
                RunStatus = LoopStatus.FINISHED
            End If
        End Sub
        Private Shared Function oGetFixedNumberFileName(ByVal ImageNumber As Integer) As String
            Dim FileNrStr(3) As String
            Dim Result As String
            Dim Rest As Integer

            Rest = ImageNumber
            FileNrStr(3) = Rest Mod 10
            Rest = (Rest - Rest Mod 10) / 10
            FileNrStr(2) = Rest Mod 10
            Rest = (Rest - Rest Mod 10) / 10
            FileNrStr(1) = Rest Mod 10
            Rest = (Rest - Rest Mod 10) / 10
            FileNrStr(0) = Rest Mod 10
            Result = FileNrStr(0) & FileNrStr(1) & FileNrStr(2) & FileNrStr(3)

            Return Result
        End Function
        Public Shared Sub oResetModelZoom()
            ModelZoomMatrix = Matrix4.Identity
        End Sub
        Public Shared Sub oResetModelTranslation()
            ModelTranslationMatrix = Matrix4.Identity
            TotalTranslation = Vector3.Zero
        End Sub
        Public Shared Sub oResetModelRotation()
            ModelRotationMatrix = Matrix4.Identity
        End Sub
        Public Shared Sub oResetModel()
            ModelZoomMatrix = Matrix4.Identity
            ModelRotationMatrix = Matrix4.Identity
            ModelTranslationMatrix = Matrix4.Identity
        End Sub
        Public Shared Sub oOrthogonalView(ByVal sglTranslationX As Single, ByVal sglTranslationY As Single, ByVal sglTranslationZ As Single)
            Dim TranslationMatrix As Matrix4 = Matrix4.CreateTranslation(sglTranslationX, sglTranslationY, sglTranslationZ)
            CameraSettings.oSetFOVBeamerOrthogonal(TranslationMatrix)
        End Sub
        Public Shared Sub oPerspectiveView(ByVal sglTranslationX As Single, ByVal sglTranslationY As Single, ByVal sglTranslationZ As Single)
            Dim TranslationMatrix As Matrix4 = Matrix4.CreateTranslation(sglTranslationX, sglTranslationY, sglTranslationZ) 'Z: -100 ideal
            CameraSettings.oSetFOVBeamerPerspective(TranslationMatrix, 0.5, 4 / 3, 20, 200)
        End Sub
        Public Shared Sub oCameraView(ByVal sglCameraLocationX As Single, ByVal sglCameraLocationY As Single, ByVal sglCameraLocationZ As Single, ByVal sglCameraTargetX As Single, ByVal sglCameraTargetY As Single, ByVal sglCameraTargetZ As Single, ByVal sglCameraFOV As Single, ByVal sglCameraAspectRatio As Single, ByVal sglCameraNear As Single, ByVal sglCameraFar As Single)
            'Dim TranslationMatrix As Matrix4 = Matrix4.CreateTranslation(0, 0, -100)
            Dim vEye, vTarget As Vector3
            vEye.X = sglCameraLocationX
            vEye.Y = sglCameraLocationY
            vEye.Z = sglCameraLocationZ
            vTarget.X = sglCameraTargetX
            vTarget.Y = sglCameraTargetY
            vTarget.Z = sglCameraTargetZ
            Dim vUp As Vector3 = Vector3.UnitY
            CameraSettings.oSetFOVCameraPerspective(vEye, vTarget, vUp, sglCameraFOV, sglCameraAspectRatio, sglCameraNear, sglCameraFar)
        End Sub
        Public Shared Function oTakeScreenshot(ByVal GLC As GLControl) As Boolean
            Try
                Dim SFD As New SaveFileDialog
                Dim strFilePath As String = ""
                SFD.DefaultExt = "png"
                SFD.Filter = "PNG images (*.png)|*.png"
                SFD.AddExtension = True
                If SFD.ShowDialog = DialogResult.OK Then
                    strFilePath = SFD.InitialDirectory & SFD.FileName
                Else
                    MsgBox("No valid path selected")
                    Return False
                    Exit Function
                End If
                Dim Image As Drawing.Bitmap = CameraSettings.oTakeScreenshotFunction(GLC)
                Image.Save(strFilePath, Drawing.Imaging.ImageFormat.Png)
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oTakeScreenshot(ByVal strFilePath As String, ByVal GLC As GLControl) As Boolean
            Try
                Dim Image As Drawing.Bitmap = CameraSettings.oTakeScreenshotFunction(GLC)
                Image.Save(strFilePath, Drawing.Imaging.ImageFormat.Png)
                Return True
            Catch ex As Exception
                Return False
                MsgBox("Error 001 - " & ex.Message)
            End Try
        End Function
        Public Shared Sub oSetOrthoParameters(ByVal Xmin As Single, ByVal Xmax As Single, ByVal Ymin As Single, ByVal Ymax As Single, ByVal Zmin As Single, ByVal Zmax As Single, ByVal glctrl As GLControl)
            oGLViewportUpdate(glctrl)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)
            GL.LoadIdentity()
            GL.Ortho(Xmin, Xmax, Ymin, Ymax, Zmin, Zmax)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Modelview)
        End Sub
        Public Shared Sub oSetOrthoParameters(ByVal Xmin As Single, ByVal Xmax As Single, ByVal Ymin As Single, ByVal Ymax As Single, ByVal Zmin As Single, ByVal Zmax As Single)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)
            GL.LoadIdentity()
            GL.Ortho(Xmin, Xmax, Ymin, Ymax, Zmin, Zmax)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Modelview)
        End Sub
        Public Shared Sub oSetDepthTest(ByVal blnActive As Boolean)
            If blnActive = True Then
                GL.Enable(GraphTK.EnableCap.DepthTest)
            Else
                GL.Disable(GraphTK.EnableCap.DepthTest)
            End If
        End Sub
    End Class
    Partial Class ReadSTL
        Private Shared sglMinX, sglMinY, sglMinZ, sglMaxX, sglMaxY, sglMaxZ As Single
        Private Shared sglDifX, sglDifY, sglDifZ As Single
        Private Shared sglMaxDif As Single
        Public Shared Function oOpenSTLFile(ByRef FilePath As String) As Boolean
            Dim oRead As StreamReader
            Dim OFD1 As New OpenFileDialog
            Try
                OFD1.InitialDirectory = ReadSettings.oGetModelPath()
                OFD1.Title = "Open STL File"
                OFD1.Filter = "STL File|*.stl"
                If OFD1.ShowDialog = DialogResult.OK Then
                    FilePath = OFD1.FileName
                    oRead = File.OpenText(FilePath)
                    ReadSettings.oSetModelPath(ReadSettings.oGetMapPath(OFD1.FileName))
                    'VolledigeInhoud = oRead.ReadToEnd
                    Return True
                Else
                    MsgBox("Open STL-file was cancelled")
                    Return False
                    GL.End()
                End If
            Catch ex As Exception
                MessageBox.Show("Fout bij het lezen van de file")
                oOpenSTLFile(FilePath)
            End Try
            Return False
        End Function
        Public Shared Function oFormatCheck(ByVal FilePath As String) As Boolean
            Dim oRead As StreamReader = File.OpenText(FilePath)
            oRead.ReadLine()
            Dim blnASCII As Boolean = False
            Dim strLineTwo As String = oRead.ReadLine
            If Len(strLineTwo) = 0 Then
                ' MsgBox("This is a binary STL")
                blnASCII = False
            Else
                Dim StringParts() As String = strLineTwo.Split(" ")
                For i = 0 To UBound(StringParts)
                    If StringParts(i) = "facet" Then
                        '        MsgBox("This is an ASCII STL file")
                        blnASCII = True
                        Exit For
                    End If
                Next
                If blnASCII = False Then
                    'MsgBox("This is a binary STL file")
                End If
            End If

            Return blnASCII
        End Function
        Public Shared Function oSTLtoList(ByVal Filepath As String, ByVal intListNumber As Integer, ByVal blnASCCI As Boolean, ByVal Vis As OpenGLSettings.VisualisationType, ByVal intSize As Integer, ByVal blnCulling As Boolean) As Integer
            Dim blnFirstVertex As Boolean = True
            Dim oRead As StreamReader = File.OpenText(Filepath)
            Dim strLine As String = Nothing
            Dim strSplittedLine() As String = Nothing
            Dim sglX, sglY, sglZ, sglNormalX, sglNormalY, sglNormalZ As Single
            Dim provider As NumberFormatInfo = New NumberFormatInfo
            Dim intNumberVertices As Integer = 0
            Dim intVertexCounter As Integer = 0

            provider.NumberDecimalSeparator = "."
            GL.DeleteLists(0, 1)
            If blnASCCI = True Then

                GL.NewList(intListNumber, OpenTK.Graphics.OpenGL.ListMode.Compile)

                'oSetVisualizationMode(Vis, intSize)
                OpenGLSettings.oSetCulling(blnCulling)

                GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Triangles)

                Dim frmSTLProgress As New frmProgress
                frmSTLProgress.Text = "STL loading progress"
                frmSTLProgress.Show()

                'Inlezen van ASCCI STL PER LIJN !!!
                Do Until oRead.EndOfStream = True

                    If frmSTLProgress.btnCancel.ForeColor = Drawing.Color.Red Then
                        MsgBox("Loading cancelled")
                        frmSTLProgress.Close()
                        Return False
                        Exit Function
                    End If
                    If frmSTLProgress.pb1.Value >= 10000 Then
                        frmSTLProgress.pb1.Value = 0
                    End If
                    frmSTLProgress.pb1.Maximum = 10000
                    frmSTLProgress.pb1.Value += 1
                    frmSTLProgress.lblCommand.Text = "Ascii STL loading ..." & vbCrLf & CStr(intNumberVertices) & " triangles processed"
                    Application.DoEvents()

                    strLine = oRead.ReadLine
                    strSplittedLine = strLine.Split(" "c)
                    For i As Integer = 0 To UBound(strSplittedLine)
                        If strSplittedLine(i) = "normal" Then
                            Do Until strSplittedLine(i + 1) <> ""
                                i += 1
                            Loop
                            sglNormalX = Convert.ToSingle(strSplittedLine(i + 1), provider)
                            sglNormalY = Convert.ToSingle(strSplittedLine(i + 2), provider)
                            sglNormalZ = Convert.ToSingle(strSplittedLine(i + 3), provider)
                            GL.Normal3(sglNormalX, sglNormalY, sglNormalZ)
                            intNumberVertices += 1
                        ElseIf strSplittedLine(i) = "vertex" Then
                            Do Until strSplittedLine(i + 1) <> ""
                                i += 1
                            Loop
                            sglX = Convert.ToSingle(strSplittedLine(i + 1), provider)
                            sglY = Convert.ToSingle(strSplittedLine(i + 2), provider)
                            sglZ = Convert.ToSingle(strSplittedLine(i + 3), provider)
                            intVertexCounter += 1
                            GL.Vertex3(sglX, sglY, sglZ)

                            If blnFirstVertex = True Then
                                blnFirstVertex = False
                                sglMinX = sglX
                                sglMaxX = sglX
                                sglMinY = sglY
                                sglMaxY = sglY
                                sglMinZ = sglZ
                                sglMaxZ = sglZ
                            Else
                                If sglX < sglMinX Then
                                    sglMinX = sglX
                                ElseIf sglX > sglMaxX Then
                                    sglMaxX = sglX
                                End If
                                If sglY < sglMinY Then
                                    sglMinY = sglY
                                ElseIf sglY > sglMaxY Then
                                    sglMaxY = sglY
                                End If
                                If sglZ < sglMinZ Then
                                    sglMinZ = sglZ
                                ElseIf sglZ > sglMaxZ Then
                                    sglMaxZ = sglZ
                                End If
                            End If

                        End If
                    Next
                Loop
                GL.End()
                GL.EndList()
                frmSTLProgress.Close()
            Else
                Dim frmSTLProgress As New frmProgress
                frmSTLProgress.Text = "STL loading progress"
                frmSTLProgress.lblCommand.Text = "Binary STL loading"
                frmSTLProgress.pb1.Maximum = 100
                frmSTLProgress.Show()

                'Binary STL
                ' UINT8[80]         -   Header
                ' UINT32            -   Number of triangles
                '
                ' foreach(triangle)
                ' REAL32[3]       -    Normal vector
                ' REAL32[3]       -    Vertex 1
                ' REAL32[3]       -    Vertex 2
                ' REAL32[3]       -    Vertex 3
                ' UINT16          -    Attribute byte count
                ' End

                'Read header
                Dim fi As New FileInfo(Filepath)
                Dim fs As FileStream = fi.OpenRead()
                Dim nBytesRead As Integer = 4

                Dim strHeader As String = ""
                For i As Integer = 0 To 79
                    strHeader += CStr(Convert.ToChar(fs.ReadByte))
                Next

                'Number of vertices
                Dim TempByteArray(3) As Byte
                For i As Integer = 0 To 3
                    TempByteArray(i) = fs.ReadByte
                Next
                intNumberVertices = BitConverter.ToUInt32(TempByteArray, 0)

                GL.NewList(intListNumber, OpenTK.Graphics.OpenGL.ListMode.Compile)

                ' oSetVisualizationMode(Vis, intSize)
                OpenGLSettings.oSetCulling(blnCulling)

                GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Triangles)

                For Index As Integer = 0 To intNumberVertices - 1
                    frmSTLProgress.pb1.Value = Index / (intNumberVertices - 1) * 100
                    Application.DoEvents()

                    If frmSTLProgress.btnCancel.ForeColor = Drawing.Color.Red Then
                        MsgBox("Loading cancelled")
                        frmSTLProgress.Close()
                        Return False
                        Exit Function
                    End If

                    'Normal
                    Try
                        For j As Integer = 1 To 3
                            For i As Integer = 0 To 3
                                TempByteArray(i) = fs.ReadByte
                            Next
                            Select Case j
                                Case 1
                                    sglNormalX = BitConverter.ToSingle(TempByteArray, 0)
                                Case 2
                                    sglNormalY = BitConverter.ToSingle(TempByteArray, 0)
                                Case 3
                                    sglNormalZ = BitConverter.ToSingle(TempByteArray, 0)
                            End Select
                        Next
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    GL.Normal3(sglNormalX, sglNormalY, sglNormalZ)

                    'Vertex coordinates
                    For k As Integer = 1 To 3
                        Try
                            For j As Integer = 1 To 3
                                For i As Integer = 0 To 3
                                    TempByteArray(i) = fs.ReadByte
                                Next
                                Select Case j
                                    Case 1
                                        sglX = BitConverter.ToSingle(TempByteArray, 0)
                                    Case 2
                                        sglY = BitConverter.ToSingle(TempByteArray, 0)
                                    Case 3
                                        sglZ = BitConverter.ToSingle(TempByteArray, 0)
                                End Select
                            Next
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                        GL.Vertex3(sglX, sglY, sglZ)

                        If blnFirstVertex = True Then
                            blnFirstVertex = False
                            sglMinX = sglX
                            sglMaxX = sglX
                            sglMinY = sglY
                            sglMaxY = sglY
                            sglMinZ = sglZ
                            sglMaxZ = sglZ
                        Else
                            If sglX < sglMinX Then
                                sglMinX = sglX
                            ElseIf sglX > sglMaxX Then
                                sglMaxX = sglX
                            End If
                            If sglY < sglMinY Then
                                sglMinY = sglY
                            ElseIf sglY > sglMaxY Then
                                sglMaxY = sglY
                            End If
                            If sglZ < sglMinZ Then
                                sglMinZ = sglZ
                            ElseIf sglZ > sglMaxZ Then
                                sglMaxZ = sglZ
                            End If
                        End If
                    Next

                    fs.ReadByte()
                    fs.ReadByte()


                Next
                GL.End()
                GL.EndList()
                frmSTLProgress.Close()
            End If

            sglDifX = sglMaxX - sglMinX
            sglDifY = sglMaxY - sglMinY
            sglDifZ = sglMaxZ - sglMinZ
            If sglDifX > sglDifY Then
                If sglDifX > sglDifZ Then
                    sglMaxDif = sglDifX
                Else
                    sglMaxDif = sglDifZ
                End If
            Else
                If sglDifY > sglDifZ Then
                    sglMaxDif = sglDifY
                Else
                    sglMaxDif = sglDifZ
                End If
            End If

            Return intNumberVertices
        End Function
        Public Shared Function oGetMinX() As Single
            Return sglMinX
        End Function
        Public Shared Function oGetMinY() As Single
            Return sglMinY
        End Function
        Public Shared Function oGetMinZ() As Single
            Return sglMinZ
        End Function
        Public Shared Function oGetMaxX() As Single
            Return sglMaxX
        End Function
        Public Shared Function oGetMaxY() As Single
            Return sglMaxY
        End Function
        Public Shared Function oGetMaxZ() As Single
            Return sglMaxZ
        End Function
        Public Shared Function oGetDifX() As Single
            Return sglDifX
        End Function
        Public Shared Function oGetDifY() As Single
            Return sglDifY
        End Function
        Public Shared Function oGetDifZ() As Single
            Return sglDifZ
        End Function
        Public Shared Function oGetMaxDif() As Single
            Return sglMaxDif
        End Function
    End Class
    Partial Class OpenGLColor
#Region "<> Declaraties Coloring model with light"
        Private Shared light_position() As Single
        'OpenGL parameters mbt Light and Material
        Private Shared gsglAlphaLightDif As Single = 0.5
        Private Shared gsglAlphaLightSpec As Single = 0.5
        Private Shared gsglAlphaLightAmb As Single = 0.5
        Private Shared gsglAlphaMatAmb As Single = 0.5
        Private Shared gsglAlphaMatDif As Single = 0.5
        Private Shared gsglAlphaMatSpec As Single = 0.5
        Private Shared gsglAlphaMatShin As Single = 0.5
        Private Shared gsglalphaMatEmi As Single = 0.5

        Private Shared gclrBackground As Drawing.Color = Drawing.Color.Green
        Private Shared gclrLightDiffuse As Drawing.Color = Drawing.Color.Sienna
        Private Shared gclrLightSpecular As Drawing.Color = Drawing.Color.Black
        Private Shared gclrLightAmbient As Drawing.Color = Drawing.Color.Sienna
        Private Shared gclrMaterialAmbient As Drawing.Color = Drawing.Color.Sienna
        Private Shared gclrMaterialDiffuse As Drawing.Color = Drawing.Color.Sienna
        Private Shared gclrMaterialSpecular As Drawing.Color = Drawing.Color.Sienna
        Private Shared gclrMaterialShininess As Drawing.Color = Drawing.Color.Sienna
        Private Shared gclrMaterialEmission As Drawing.Color = Drawing.Color.Black

        Private Shared gsglLightPosX As Single = 75
        Private Shared gsglLightPosY As Single = -0.000001
        Private Shared gsglLightPosZ As Single = 250
        Private Shared gsglLightDirection As Single = 1.0 '1 = point light, 0 = directional light

        Public Shared Function oGetAlphaLightDiffuse() As Single
            Return gsglAlphaLightDif
        End Function
        Public Shared Function oGetAlphaLightSpecular() As Single
            Return gsglAlphaLightSpec
        End Function
        Public Shared Function oGetAlphaLightAmbient() As Single
            Return gsglAlphaLightAmb
        End Function
        Public Shared Function oGetAlphaMaterialAmbient() As Single
            Return gsglAlphaMatAmb
        End Function
        Public Shared Function oGetAlphaMaterialDiffuse() As Single
            Return gsglAlphaMatDif
        End Function
        Public Shared Function oGetAlphaMaterialSpecular() As Single
            Return gsglAlphaMatSpec
        End Function
        Public Shared Function oGetAlphaMaterialShininess() As Single
            Return gsglAlphaMatShin
        End Function
        Public Shared Function oGetAlphaMaterialEmission() As Single
            Return gsglalphaMatEmi
        End Function

        Public Shared Function oGetColorBackground() As Drawing.Color
            Return gclrBackground
        End Function
        Public Shared Function oGetColorLightDiffuse() As Drawing.Color
            Return gclrLightDiffuse
        End Function
        Public Shared Function oGetColorLightSpecular() As Drawing.Color
            Return gclrLightSpecular
        End Function
        Public Shared Function oGetColorLightAmbient() As Drawing.Color
            Return gclrLightAmbient
        End Function
        Public Shared Function oGetColorMaterialAmbient() As Drawing.Color
            Return gclrMaterialAmbient
        End Function
        Public Shared Function oGetColorMaterialDiffuse() As Drawing.Color
            Return gclrMaterialDiffuse
        End Function
        Public Shared Function oGetColorMaterialSpecular() As Drawing.Color
            Return gclrMaterialSpecular
        End Function
        Public Shared Function oGetMaterialShininess() As Drawing.Color
            Return gclrMaterialShininess
        End Function
        Public Shared Function oGetMaterialEmission() As Drawing.Color
            Return gclrMaterialEmission
        End Function

        Public Shared Sub oGetLightPosition(ByRef X As Single, ByRef Y As Single, ByRef Z As Single)
            X = gsglLightPosX
            Y = gsglLightPosY
            Z = gsglLightPosZ
        End Sub
        Public Shared Function oGetLightPositionX() As Single
            Return gsglLightPosX
        End Function
        Public Shared Function oGetLightPositionY() As Single
            Return gsglLightPosY
        End Function
        Public Shared Function oGetLightPositionZ() As Single
            Return gsglLightPosZ
        End Function
        Public Shared Function oGetLightDirection() As Single
            Return gsglLightDirection
        End Function
#End Region
#Region "<> Model Light/Material Settings"
        Public Shared Sub oStandardLight()
            'Declaraties voor light
            Dim light_ambient() As Single
            Dim light_diffuse() As Single
            Dim light_specular() As Single

            'Instelling light
            light_ambient = New Single() {gclrLightAmbient.R / 255, gclrLightAmbient.G / 255, gclrLightAmbient.B / 255, gsglAlphaLightAmb}
            light_diffuse = New Single() {gclrLightDiffuse.R / 255, gclrLightDiffuse.G / 255, gclrLightDiffuse.B / 255, gsglAlphaLightDif}
            light_position = New Single() {gsglLightPosX, gsglLightPosY, gsglLightPosZ, gsglLightDirection}
            light_specular = New Single() {gclrLightSpecular.R / 255, gclrLightSpecular.G / 255, gclrLightSpecular.B / 255, gsglAlphaLightSpec}

            'Set Light
            GL.Light(GraphTK.LightName.Light0, GraphTK.LightParameter.Ambient, light_ambient)
            GL.Light(GraphTK.LightName.Light0, GraphTK.LightParameter.Diffuse, light_diffuse)
            GL.Light(GraphTK.LightName.Light0, GraphTK.LightParameter.Position, light_position)
            GL.Light(GraphTK.LightName.Light0, GraphTK.LightParameter.Specular, light_specular)
        End Sub
        Public Shared Sub oStandardMaterial()
            'Declaraties voor material
            Dim material_ambient() As Single
            Dim material_diffuse() As Single
            Dim material_specular() As Single
            Dim material_shininess() As Single
            Dim material_emission() As Single

            'Instelling material
            material_ambient = New Single() {gclrMaterialAmbient.R / 255, gclrMaterialAmbient.G / 255, gclrMaterialAmbient.B / 255, gsglAlphaMatAmb}
            material_diffuse = New Single() {gclrMaterialDiffuse.R / 255, gclrMaterialDiffuse.G / 255, gclrMaterialDiffuse.B / 255, gsglAlphaMatDif}
            material_specular = New Single() {gclrMaterialSpecular.R / 255, gclrMaterialSpecular.G / 255, gclrMaterialSpecular.B / 255, gsglAlphaMatSpec}
            material_shininess = New Single() {gclrMaterialShininess.R / 255, gclrMaterialShininess.G / 255, gclrMaterialShininess.B / 255, gsglAlphaMatShin}
            material_emission = New Single() {gclrMaterialEmission.R / 255, gclrMaterialEmission.G / 255, gclrMaterialEmission.B / 255, gsglalphaMatEmi}

            GL.Material(GraphTK.MaterialFace.FrontAndBack, GraphTK.MaterialParameter.Diffuse, material_diffuse)
            GL.Material(GraphTK.MaterialFace.FrontAndBack, GraphTK.MaterialParameter.Specular, material_specular)
            GL.Material(GraphTK.MaterialFace.FrontAndBack, GraphTK.MaterialParameter.Shininess, material_shininess)
            GL.Material(GraphTK.MaterialFace.FrontAndBack, GraphTK.MaterialParameter.Ambient, material_ambient)
            GL.Material(GraphTK.MaterialFace.FrontAndBack, GraphTK.MaterialParameter.Emission, material_emission)
        End Sub
        Public Shared Sub oSetModelColoring()
            'Backgroundcolor
            GL.ClearColor(gclrBackground.R / 255, gclrBackground.G / 255, gclrBackground.B / 255, 0.5)

            'Enables Smooth Color Shading
            GL.ShadeModel(GraphTK.ShadingModel.Smooth)
            GL.Enable(GraphTK.EnableCap.ColorMaterial)

            'Set Light & Material
            oStandardLight()
            oStandardMaterial()

            'Enable Lighting
            GL.Enable(GraphTK.EnableCap.Lighting)
            GL.Enable(GraphTK.EnableCap.Light0)

            'Diepteregeling met verschil in kleur tot gevolg
            GL.Enable(GraphTK.EnableCap.DepthTest)
            oInitializeDepthMapping()
        End Sub
        Private Shared Sub oInitializeDepthMapping()
            GL.Hint(OpGL.HintTarget.PerspectiveCorrectionHint, OpGL.HintMode.Nicest)
            GL.ClearDepth(1.0F)
            GL.DepthFunc(OpGL.DepthFunction.Less)
            GL.Enable(OpGL.EnableCap.DepthTest)

            GL.Enable(OpGL.EnableCap.CullFace)
            GL.Enable(OpGL.EnableCap.Normalize)

            '  Create the shadow map texture
            Dim ShadowMapTexture As Integer
            GL.GenTextures(1, ShadowMapTexture)
            GL.BindTexture(OpGL.TextureTarget.Texture2D, ShadowMapTexture)
            oSetShadowMapTexture(ShadowMapTexture)

            'Correct settings
            GL.TexImage2D(OpGL.TextureTarget.Texture2D, 0, DirectCast(OpGL.All.DepthComponent32, OpGL.PixelInternalFormat), oGetShadowMapSize, oGetShadowMapSize, 0, OpGL.PixelFormat.DepthComponent, OpGL.PixelType.UnsignedInt, Nothing)

            ''Debugging settings
            'GL.TexImage2D(OpGL.TextureTarget.Texture2D, 0, DirectCast(OpGL.All.DepthComponent, OpGL.PixelInternalFormat), oGetShadowMapSize, oGetShadowMapSize, 0, OpGL.PixelFormat.DepthComponent, OpGL.PixelType.UnsignedByte, Nothing)

            GL.TexParameterI(OpGL.TextureTarget.Texture2D, OpGL.TextureParameterName.TextureWrapS, OpGL.All.Linear)
            GL.TexParameterI(OpGL.TextureTarget.Texture2D, OpGL.TextureParameterName.TextureWrapT, OpGL.All.Linear)

            GL.TexParameter(OpGL.TextureTarget.Texture2D, OpGL.TextureParameterName.TextureMinFilter, OpGL.TextureMinFilter.Linear)
            GL.TexParameter(OpGL.TextureTarget.Texture2D, OpGL.TextureParameterName.TextureMagFilter, OpGL.TextureMagFilter.Linear)
        End Sub
        Public Shared Sub oSetModelColoring(ByVal DepthTest As Boolean)
            'Backgroundcolor
            GL.ClearColor(gclrBackground.R / 255, gclrBackground.G / 255, gclrBackground.B / 255, 0.5)

            'Enables Smooth Color Shading
            GL.ShadeModel(GraphTK.ShadingModel.Smooth)
            GL.Enable(GraphTK.EnableCap.ColorMaterial)

            'Set Light & Material
            oStandardLight()
            oStandardMaterial()

            'Enable Lighting
            GL.Enable(GraphTK.EnableCap.Lighting)
            GL.Enable(GraphTK.EnableCap.Light0)

            'Diepteregeling met verschil in kleur tot gevolg
            If DepthTest = True Then
                GL.Enable(GraphTK.EnableCap.DepthTest)
            Else
                GL.Disable(Graphics.OpenGL.EnableCap.DepthTest)
            End If
        End Sub
        Public Shared Sub oSetModelColor(ByVal Diffuse As Drawing.Color, ByVal Ambient As Drawing.Color, ByVal Specular As Drawing.Color, ByVal Shininess As Drawing.Color, ByVal Emission As Drawing.Color)
            gclrMaterialAmbient = Ambient
            gclrMaterialDiffuse = Diffuse
            gclrMaterialEmission = Emission
            gclrMaterialShininess = Shininess
            gclrMaterialSpecular = Specular
            oStandardMaterial()
        End Sub
        Public Shared Sub oSetLightColor(ByVal Diffuse As Drawing.Color, ByVal Ambient As Drawing.Color, ByVal Specular As Drawing.Color)
            gclrLightAmbient = Ambient
            gclrLightDiffuse = Diffuse
            gclrLightSpecular = Specular
        End Sub
        Public Shared Sub oSetBackgroundColor(ByVal bgColor As Drawing.Color)
            gclrBackground = bgColor
        End Sub
        Public Shared Sub oSetColorMaterial(ByVal blnActive As Boolean)
            If blnActive = True Then
                GL.Enable(GraphTK.EnableCap.ColorMaterial)
            Else
                GL.Disable(GraphTK.EnableCap.ColorMaterial)
            End If
        End Sub
        Public Shared Sub oSetLightDirection(ByVal blnDirectional As Boolean)
            If blnDirectional = True Then
                gsglLightDirection = 0
                gsglLightPosX = (sglBeamerLocationX - sglBeamerTargetX) / M.Sqrt((sglBeamerTargetX - sglBeamerLocationX) ^ 2 + (sglBeamerTargetY - sglBeamerLocationY) ^ 2 + (sglBeamerTargetZ - sglBeamerLocationZ) ^ 2)
                gsglLightPosY = (sglBeamerLocationY - sglBeamerTargetY) / M.Sqrt((sglBeamerTargetX - sglBeamerLocationX) ^ 2 + (sglBeamerTargetY - sglBeamerLocationY) ^ 2 + (sglBeamerTargetZ - sglBeamerLocationZ) ^ 2)
                gsglLightPosZ = (sglBeamerLocationZ - sglBeamerTargetZ) / M.Sqrt((sglBeamerTargetX - sglBeamerLocationX) ^ 2 + (sglBeamerTargetY - sglBeamerLocationY) ^ 2 + (sglBeamerTargetZ - sglBeamerLocationZ) ^ 2)
            Else
                gsglLightPosX = sglBeamerLocationX
                gsglLightPosY = sglBeamerLocationY
                gsglLightPosZ = sglBeamerLocationZ
                gsglLightDirection = 1
            End If
            oSetModelColoring()
            CameraSettings.oUpdateCameraSettings()
            oUpdateShader()
        End Sub
#End Region
#Region "<> Shaders"
        Private Shared vertex_shader_object As Integer
        Private Shared fragment_shader_object As Integer
        Private Shared Shader_Program As Integer = GL.CreateProgram()
        Private Shared intShadowMapSize As Integer = 2048
        Private Shared intShadowMapTexture As Integer
#Region "Beamer declarations"
        Private Shared sglBeamerFrequency As Single = 50
        Private Shared sglBeamerStartFrequency As Single = 1
        Private Shared sglBeamerStopFrequency As Single = 50
        Private Shared sglBeamerFrequencyDifference As Single = 1
        Private Shared sglBeamerStartPhase As Single = 0
        Private Shared sglBeamerStopPhase As Single = 2 * M.PI
        Private Shared sglBeamerPhaseDifference As Single = 2 * M.PI / 8
        Private Shared sglBeamerLocationX As Single = 112.382 '121.2079 '112.382
        Private Shared sglBeamerLocationY As Single = 0
        Private Shared sglBeamerLocationZ As Single = 300
        Private Shared sglBeamerTargetX As Single = 0
        Private Shared sglBeamerTargetY As Single = 0
        Private Shared sglBeamerTargetZ As Single = 0
        Private Shared sglBeamerGratingX1 As Single = 90.562 '121.2079 - 6.6648 '90.562
        Private Shared sglBeamerGratingY1 As Single
        Private Shared sglBeamerGratingZ1 As Single = 233.044 '290 '233.044
        Private Shared sglBeamerGratingX2 As Single = 96.741 '121.2079  '96.741
        Private Shared sglBeamerGratingY2 As Single
        Private Shared sglBeamerGratingZ2 As Single = 230.548 '290 '230.548
        Private Shared sglBeamerGratingDistance As Single
        Private Shared sglBeamerNear As Single = 40
        Private Shared sglBeamerFar As Single = 120
        Private Shared OrthoBoxLength As Single
        Private Shared E1 As FDMath.Point
        Private Shared E2 As FDMath.Point
        Private Shared E3 As FDMath.Point
        Private Shared EI As FDMath.Vector 'vector from Intersection point between target-eye and grating to Location=Eye
        Private Shared TE As FDMath.Vector
        Private Shared TEn As FDMath.Vector
        Private Shared IntersectionPoint As FDMath.Point
        Private Shared sglBeamerWidth As Single = 40
        Private Shared sglBeamerDistance As Single = sglBeamerLocationZ
        Private Shared sglBeamerAngle As Single = 0.1331
        Private Shared sglBeamerRatio As Single = 4 / 3
        Private Shared intBeamerPixelWidth As Integer = 1024
        Private Shared intBeamerPixelHeight As Integer = 768
        Private Shared sglBeamerPhaseShift As Single = 0
        Private Shared sglBeamerBlackout As Single = 0 '0=no blackout      >0=blackout
        Private Shared sglBeamerCalibration As Single = 0 '0=no calibration    >0=calibration
        Private Shared intBeamerLensType As Integer = False '0=diverging, 1=telecentric
        Enum LensType
            DIVERGING = 0
            TELECENTRIC = 1
        End Enum
        Public Shared Function oGetBeamerFrequency() As Single
            Return sglBeamerFrequency
        End Function
        Public Shared Function oGetBeamerStartFrequency() As Single
            Return sglBeamerStartFrequency
        End Function
        Public Shared Function oGetBeamerStopFrequency() As Single
            Return sglBeamerStopFrequency
        End Function
        Public Shared Function oGetBeamerFrequencyDifference() As Single
            Return sglBeamerFrequencyDifference
        End Function
        Public Shared Function oGetBeamerStartPhase() As Single
            Return sglBeamerStartPhase
        End Function
        Public Shared Function oGetBeamerStopPhase() As Single
            Return sglBeamerStopPhase
        End Function
        Public Shared Function oGetBeamerPhaseDifference() As Single
            Return sglBeamerPhaseDifference
        End Function
        Public Shared Sub oGetBeamerLocation(ByRef X As Single, ByRef Y As Single, ByRef Z As Single)
            X = sglBeamerLocationX
            Y = sglBeamerLocationY
            Z = sglBeamerLocationZ
        End Sub
        Public Shared Function oGetBeamerLocationX() As Single
            Return sglBeamerLocationX
        End Function
        Public Shared Function oGetBeamerLocationY() As Single
            Return sglBeamerLocationY
        End Function
        Public Shared Function oGetBeamerLocationZ() As Single
            Return sglBeamerLocationZ
        End Function
        Public Shared Sub oGetBeamerTarget(ByRef X As Single, ByRef Y As Single, ByRef Z As Single)
            X = sglBeamerTargetX
            Y = sglBeamerTargetY
            Z = sglBeamerTargetZ
        End Sub
        Public Shared Function oGetBeamerTargetX() As Single
            Return sglBeamerTargetX
        End Function
        Public Shared Function oGetBeamerTargetY() As Single
            Return sglBeamerTargetY
        End Function
        Public Shared Function ogetBeamerTargetZ() As Single
            Return sglBeamerTargetZ
        End Function
        Public Shared Sub oGetBeamerGrating(ByRef X1 As Single, Y1 As Single, Z1 As Single, X2 As Single, Y2 As Single, Z2 As Single)
            X1 = sglBeamerGratingX1
            Y1 = sglBeamerGratingY1
            Z1 = sglBeamerGratingZ1
            X2 = sglBeamerGratingX2
            Y2 = sglBeamerGratingY2
            Z2 = sglBeamerGratingZ2
        End Sub
        Public Shared Function oGetBeamerGratingX1() As Single
            Return sglBeamerGratingX1
        End Function
        Public Shared Function oGetBeamerGratingY1() As Single
            Return sglBeamerGratingY1
        End Function
        Public Shared Function oGetBeamerGratingZ1() As Single
            Return sglBeamerGratingZ1
        End Function
        Public Shared Function oGetBeamerGratingX2() As Single
            Return sglBeamerGratingX2
        End Function
        Public Shared Function oGetBeamerGratingY2() As Single
            Return sglBeamerGratingY2
        End Function
        Public Shared Function oGetBeamerGratingZ2() As Single
            Return sglBeamerGratingZ2
        End Function
        Public Shared Function oGetBeamerWidth() As Single
            Return sglBeamerWidth
        End Function
        Public Shared Function oGetBeamerDistance() As Single
            Return sglBeamerDistance
        End Function
        Public Shared Function oGetBeamerAngle() As Single
            Return sglBeamerAngle
        End Function
        Public Shared Function oGetBeamerRatio() As Single
            Return sglBeamerRatio
        End Function
        Public Shared Function oGetBeamerPixelWidth() As Integer
            Return intBeamerPixelWidth
        End Function
        Public Shared Function oGetBeamerPixelHeight() As Integer
            Return intBeamerPixelHeight
        End Function
        Public Shared Function oGetBeamerPhaseShift() As Single
            Return sglBeamerPhaseShift
        End Function
        Public Shared Function oGetBeamerBlackout() As Single
            Return sglBeamerBlackout
        End Function
        Public Shared Function oGetBeamerCalibration() As Single
            Return sglBeamerCalibration
        End Function
        Public Shared Function oGetBemaerLensType() As Integer
            Return intBeamerLensType
        End Function
        Public Shared Function oGetShaderHandle()
            Return Shader_Program
        End Function
        Public Shared Function oGetBeamerGratingDistance()
            Return sglBeamerGratingDistance
        End Function
        Public Shared Function oGetIntersectionPoint() As FDMath.Point
            Return IntersectionPoint
        End Function
        Public Shared Function oGetEyeTargetDistance() As Single
            Return TE.Length
        End Function
        Public Shared Function oGetNear() As Single
            Return sglBeamerNear
        End Function
        Public Shared Function oGetFar() As Single
            Return sglBeamerFar
        End Function
        Public Shared Sub oSetNear(ByVal value As Single)
            sglBeamerNear = value
        End Sub
        Public Shared Sub oSetFar(ByVal value As Single)
            sglBeamerFar = value
        End Sub
#End Region
#Region "Grating declarations"
        Private Shared blnCameraGratingEnabled As Boolean = False
        Private Shared sglCameraGratingX1 As Single = -3.3324
        Private Shared sglCameraGratingY1 As Single = -3.3324
        Private Shared sglCameraGratingZ1 As Single = 250 '290 '250
        Private Shared sglCameraGratingX2 As Single = 3.3324
        Private Shared sglCameraGratingY2 As Single = 3.3324
        Private Shared sglCameraGratingZ2 As Single = 250 '290 '250
        Private Shared sglCameraFrequency As Single = 50
        Private Shared sglCameraPhaseShift As Single = 0
        Private Shared intCameraGratingPixelWidth As Integer = 1024
        Private Shared intCameraGratingPixelHeight As Integer = 768
        Public Shared Function oIsCameraGratingEnabled() As Boolean
            Return blnCameraGratingEnabled
        End Function
        Public Shared Sub oActivateCameraGrating()
            blnCameraGratingEnabled = True
        End Sub
        Public Shared Sub oDeactivateCameraGrating()
            blnCameraGratingEnabled = False
        End Sub
        Public Shared Sub oGetCameraGrating(ByRef X1 As Single, ByRef Y1 As Single, ByRef Z1 As Single, ByRef X2 As Single, ByRef Y2 As Single, ByRef Z2 As Single)
            X1 = sglCameraGratingX1
            Y1 = sglCameraGratingY1
            Z1 = sglCameraGratingZ1
            X2 = sglCameraGratingX2
            Y2 = sglCameraGratingY2
            Z2 = sglCameraGratingZ2
        End Sub
        Public Shared Function oGetCameraGratingX1() As Single
            Return sglCameraGratingX1
        End Function
        Public Shared Function oGetCameraGratingY1() As Single
            Return sglCameraGratingY1
        End Function
        Public Shared Function oGetCameraGratingZ1() As Single
            Return sglCameraGratingZ1
        End Function
        Public Shared Function oGetCameraGratingX2() As Single
            Return sglCameraGratingX2
        End Function
        Public Shared Function oGetCameraGratingY2() As Single
            Return sglCameraGratingY2
        End Function
        Public Shared Function oGetCameraGratingZ2() As Single
            Return sglCameraGratingZ2
        End Function
        Public Shared Function oGetCameraFrequency() As Single
            Return sglCameraFrequency
        End Function
        Public Shared Function oGetCameraPhaseShift() As Single
            Return sglCameraPhaseShift
        End Function
        Public Shared Function oGetCameraGratingPixelWidth() As Integer
            Return intCameraGratingPixelWidth
        End Function
        Public Shared Function oGetCameraGratingPixelHeight() As Integer
            Return intCameraGratingPixelHeight
        End Function
#End Region
        Public Shared Function oSetShader(ByVal strVertexShaderFileName As String, ByVal strFragmentShaderFileName As String) As Boolean
            Try
                ' Check for necessary capabilities:
                Dim Version As String = GL.GetString(GraphTK.StringName.Version)
                Dim Major As Integer = Version.Substring(0, 1)
                Dim Minor As Integer = Version.Substring(2, 1)
                If Major < 2 Then
                    MessageBox.Show("You need at least OpenGL 2.0 to run this example. Aborting.", "GLSL not supported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                Dim vs As New System.IO.StreamReader(strVertexShaderFileName)
                Dim fs As New System.IO.StreamReader(strFragmentShaderFileName)

                oCreateShaders(vs.ReadToEnd, fs.ReadToEnd, vertex_shader_object, fragment_shader_object, Shader_Program)
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetShader() As Boolean
            Dim strVertexShaderFileName As String = ""
            Dim strFragmentShaderFileName As String = ""

            Try
                Dim OFD1 As New OpenFileDialog
                Dim FilePath As String
                Try
                    OFD1.Title = "Open vertex shader"
                    OFD1.Filter = "OpenGL Shading Language|*.glsl"
                    If OFD1.ShowDialog = DialogResult.OK Then
                        FilePath = OFD1.InitialDirectory & OFD1.FileName
                        strVertexShaderFileName = FilePath
                    Else
                        MsgBox("Open shader was cancelled")
                        Return False
                        Exit Function
                    End If
                Catch ex As Exception
                    MessageBox.Show("Fout bij het lezen van de file")
                End Try
                Try
                    OFD1.Title = "Open fragment shader"
                    OFD1.Filter = "OpenGL Shading Language|*.glsl"
                    If OFD1.ShowDialog = DialogResult.OK Then
                        FilePath = OFD1.InitialDirectory & OFD1.FileName
                        strFragmentShaderFileName = FilePath
                    Else
                        MsgBox("Open shader was cancelled")
                        Return False
                        Exit Function
                    End If
                Catch ex As Exception
                    MessageBox.Show("Fout bij het lezen van de file")
                End Try

                ' Check for necessary capabilities:
                Dim Version As String = GL.GetString(GraphTK.StringName.Version)
                Dim Major As Integer = Version.Substring(0, 1)
                Dim Minor As Integer = Version.Substring(2, 1)
                If Major < 2 Then
                    MessageBox.Show("You need at least OpenGL 2.0 to run this example. Aborting.", "GLSL not supported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                Dim vs As New System.IO.StreamReader(strVertexShaderFileName)
                Dim fs As New System.IO.StreamReader(strFragmentShaderFileName)

                oCreateShaders(vs.ReadToEnd, fs.ReadToEnd, vertex_shader_object, fragment_shader_object, Shader_Program)
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Sub oCreateShaders(ByVal vs As String, ByVal fs As String, ByRef vertexObject As Integer, ByRef fragmentObject As Integer, ByRef program As Integer)
            Dim StatusCode As Integer
            Dim Info As String = Nothing
            vertexObject = GL.CreateShader(GraphTK.ShaderType.VertexShader)
            fragmentObject = GL.CreateShader(GraphTK.ShaderType.FragmentShader)

            GL.ShaderSource(vertexObject, vs)
            GL.CompileShader(vertexObject)
            GL.GetShaderInfoLog(vertexObject, Info)
            GL.GetShader(vertexObject, OpGL.ShaderParameter.CompileStatus, StatusCode)
            If StatusCode <> 1 Then
                MsgBox(Info)
            End If
            GL.ShaderSource(fragmentObject, fs)
            GL.CompileShader(fragmentObject)
            GL.GetShaderInfoLog(fragmentObject, Info)
            GL.GetShader(fragmentObject, OpGL.ShaderParameter.CompileStatus, StatusCode)
            If StatusCode <> 1 Then
                MsgBox(Info)
            End If

            program = GL.CreateProgram

            GL.AttachShader(program, fragmentObject)
            GL.AttachShader(program, vertexObject)

            GL.LinkProgram(program)
            GL.UseProgram(program)

            oUpdateShader()
        End Sub
        Public Shared Sub oUpdateShader()
            sglBeamerDistance = sglBeamerLocationZ

            Dim loc As Integer = GL.GetUniformLocation(Shader_Program, "frequency")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerFrequency)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "pi")
            If loc <> -1 Then
                GL.Uniform1(loc, Convert.ToSingle(System.Math.PI))
            End If
            loc = GL.GetUniformLocation(Shader_Program, "xoffset")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerLocationX)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "distance")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerDistance)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "phaseshift")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerPhaseShift)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "calibration")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerCalibration)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "blackout")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerBlackout)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "beamerratio")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerRatio)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "beamerlenstype")
            If loc <> -1 Then
                GL.Uniform1(loc, intBeamerLensType)
            End If

            'grating parameters 
            loc = GL.GetUniformLocation(Shader_Program, "gratingenabled")
            If loc <> -1 Then
                GL.Uniform1(loc, CInt(blnCameraGratingEnabled))
            End If
            loc = GL.GetUniformLocation(Shader_Program, "gratingfrequency")
            If loc <> -1 Then
                GL.Uniform1(loc, sglCameraFrequency)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "gratingshift")
            If loc <> -1 Then
                GL.Uniform1(loc, sglCameraPhaseShift)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "p1x")
            If loc <> -1 Then
                GL.Uniform1(loc, sglCameraGratingX1)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "p1y")
            If loc <> -1 Then
                GL.Uniform1(loc, sglCameraGratingY1)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "p1z")
            If loc <> -1 Then
                GL.Uniform1(loc, sglCameraGratingZ1)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "p2x")
            If loc <> -1 Then
                GL.Uniform1(loc, sglCameraGratingX2)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "p2y")
            If loc <> -1 Then
                GL.Uniform1(loc, sglCameraGratingY2)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "p2z")
            If loc <> -1 Then
                GL.Uniform1(loc, sglCameraGratingZ2)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "pixelgratingwidth")
            If loc <> -1 Then
                GL.Uniform1(loc, intCameraGratingPixelWidth)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "pixelgratingheight")
            If loc <> -1 Then
                GL.Uniform1(loc, intCameraGratingPixelHeight)
            End If

            'Projection parameters
            loc = GL.GetUniformLocation(Shader_Program, "beamerfov")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerAngle)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "beamerfrequency")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerFrequency)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "beamershift")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerPhaseShift)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "beamerwidth")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerWidth)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "tarx")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerTargetX)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "tary")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerTargetY)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "tarz")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerTargetZ)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "locx")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerLocationX)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "locy")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerLocationY)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "locz")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerLocationZ)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "bp1x")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerGratingX1)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "bp1y")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerGratingY1)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "bp1z")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerGratingZ1)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "bp2x")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerGratingX2)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "bp2y")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerGratingY2)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "bp2z")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerGratingZ2)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "pixelbeamerwidth")
            If loc <> -1 Then
                GL.Uniform1(loc, intBeamerPixelWidth)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "pixelbeamerheight")
            If loc <> -1 Then
                GL.Uniform1(loc, intBeamerPixelHeight)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "BoxLength")
            If loc <> -1 Then
                GL.Uniform1(loc, OrthoBoxLength)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "TEX")
            If loc <> -1 Then
                GL.Uniform1(loc, TE.X)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "TEY")
            If loc <> -1 Then
                GL.Uniform1(loc, TE.Y)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "TEZ")
            If loc <> -1 Then
                GL.Uniform1(loc, TE.Z)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "TEnX")
            If loc <> -1 Then
                GL.Uniform1(loc, TEn.X)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "TEnY")
            If loc <> -1 Then
                GL.Uniform1(loc, TEn.Y)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "TEnZ")
            If loc <> -1 Then
                GL.Uniform1(loc, TEn.Z)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "E1X")
            If loc <> -1 Then
                GL.Uniform1(loc, E1.X)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "E1Y")
            If loc <> -1 Then
                GL.Uniform1(loc, E1.Y)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "E1Z")
            If loc <> -1 Then
                GL.Uniform1(loc, E1.Z)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "E2X")
            If loc <> -1 Then
                GL.Uniform1(loc, E2.X)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "E2Y")
            If loc <> -1 Then
                GL.Uniform1(loc, E2.Y)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "E2Z")
            If loc <> -1 Then
                GL.Uniform1(loc, E2.Z)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "E3X")
            If loc <> -1 Then
                GL.Uniform1(loc, E3.X)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "E3Y")
            If loc <> -1 Then
                GL.Uniform1(loc, E3.Y)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "E3Z")
            If loc <> -1 Then
                GL.Uniform1(loc, E3.Z)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "BeamerGratingDistance")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerGratingDistance)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "znear")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerNear)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "zfar")
            If loc <> -1 Then
                GL.Uniform1(loc, sglBeamerFar)
            End If
            loc = GL.GetUniformLocation(Shader_Program, "ShadowMapSize")
            If loc <> -1 Then
                GL.Uniform1(loc, intShadowMapSize)
            End If
        End Sub
        Public Shared Sub oDeactivateShaders()
            GL.UseProgram(0)
            'GL.DetachShader(Shader_Program, vertex_shader_object)
            'GL.DetachShader(Shader_Program, fragment_shader_object)
            'GL.DeleteShader(vertex_shader_object)
            'GL.DeleteShader(fragment_shader_object)
            'GL.DeleteProgram(Shader_Program)
        End Sub
        Public Shared Sub oSetShadowMapTexture(ByVal intTextureID As Integer)
            intShadowMapTexture = intTextureID
        End Sub
        Public Shared Function oGetShadowMapTexture() As Integer
            Return intShadowMapTexture
        End Function
        Public Shared Function oSetShadowMapSize(ByVal intSize As Integer) As Boolean
            Try
                intShadowMapSize = intSize
                oUpdateShader()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oGetShadowMapSize() As Integer
            Return intShadowMapSize
        End Function
        Public Shared Function oSetBeamerPhaseShift() As Boolean
            Try
                sglBeamerPhaseShift = InputBox("Enter phase shift", "Phase shift")
                oUpdateShader()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerPhaseShift(ByVal PhaseShift As Single) As Boolean
            Try
                sglBeamerPhaseShift = PhaseShift
                oUpdateShader()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerFrequency() As Boolean
            Try
                sglBeamerFrequency = InputBox("Enter frequency", "Frequency")
                oUpdateShader()
                Return False
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerFrequency(ByVal Frequency As Single) As Boolean
            Try
                sglBeamerFrequency = Frequency
                oUpdateShader()
                Return False
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerFrequencySteps() As Boolean
            Try
                Dim sglFrequencySteps As Single = Nothing
                sglFrequencySteps = InputBox("Enter number of frequency steps", "Frequency Steps")
                sglBeamerFrequencyDifference = (sglBeamerStopFrequency - sglBeamerStartFrequency) / sglFrequencySteps
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerFrequencySteps(ByVal Steps As Single) As Boolean
            Try
                sglBeamerFrequencyDifference = (sglBeamerStopFrequency - sglBeamerStartFrequency) / Steps
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerStartFrequency() As Boolean
            Try
                sglBeamerStartFrequency = InputBox("Enter Start Frequency", "Frequency")
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerStartFrequency(ByVal Frequency As Single) As Boolean
            Try
                sglBeamerStartFrequency = Frequency
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerStopFrequency() As Boolean
            Try
                sglBeamerStopFrequency = InputBox("Enter Stop Frequency", "Frequency")
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerStopFrequency(ByVal Frequency As Single) As Boolean
            Try
                sglBeamerStopFrequency = Frequency
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerStartPhase() As Boolean
            Try
                sglBeamerStartPhase = InputBox("Enter Start Phase", "Phase")
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerStartPhase(ByVal Phase As Single) As Boolean
            Try
                sglBeamerStartPhase = Phase
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerStopPhase() As Boolean
            Try
                sglBeamerStopPhase = InputBox("Enter Stop Phase", "Phase")
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerStopPhase(ByVal Phase As Single) As Boolean
            Try
                sglBeamerStopPhase = Phase
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerPhaseSteps() As Boolean
            Try
                Dim sglPhaseSteps As Single = Nothing
                sglPhaseSteps = InputBox("Enter number of phase steps", "Phase steps")
                sglBeamerPhaseDifference = (sglBeamerStopPhase - sglBeamerStartPhase) / sglPhaseSteps
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Shared Function oSetBeamerPhaseSteps(ByVal Steps As Single) As Boolean
            Try
                Dim sglPhaseSteps As Single = Steps
                sglBeamerPhaseDifference = (sglBeamerStopPhase - sglBeamerStartPhase) / sglPhaseSteps
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Shared Sub oSetBeamerLocation()
            Try
                sglBeamerLocationX = InputBox("Enter X", "Set Beamer Location")
                sglBeamerLocationY = InputBox("Enter Y", "Set Beamer Location")
                sglBeamerLocationZ = InputBox("Enter Z", "Set Beamer Location")
                sglBeamerDistance = sglBeamerLocationZ

                gsglLightPosX = sglBeamerLocationX
                gsglLightPosY = sglBeamerLocationY
                gsglLightPosZ = sglBeamerLocationZ

                oSetModelColoring()
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerLocation(ByVal X As Single, ByVal Y As Single, ByVal Z As Single)
            Try
                sglBeamerLocationX = X
                sglBeamerLocationY = Y
                sglBeamerLocationZ = Z
                sglBeamerDistance = sglBeamerLocationZ

                gsglLightPosX = sglBeamerLocationX
                gsglLightPosY = sglBeamerLocationY
                gsglLightPosZ = sglBeamerLocationZ

                oSetModelColoring()
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerTarget()
            Try
                sglBeamerTargetX = InputBox("Enter X", "Set Beamer Target")
                sglBeamerTargetY = InputBox("Enter Y", "Set Beamer Target")
                sglBeamerTargetZ = InputBox("Enter Z", "Set Beamer Target")
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerTarget(ByVal X As Single, ByVal Y As Single, ByVal Z As Single)
            Try
                sglBeamerTargetX = X
                sglBeamerTargetY = Y
                sglBeamerTargetZ = Z
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerWidth(ByVal Width As Single)
            sglBeamerWidth = Width
        End Sub
        Public Shared Sub oSetBeamerParameters()
            Try
                OpenGLColor.sglBeamerAngle = InputBox("Enter beamer angle", "Set Beamer Parameter")
                OpenGLColor.sglBeamerRatio = InputBox("Enter Width/Height Ratio", "Set Beamer Paramater")
                OpenGLColor.sglBeamerWidth = InputBox("Enter Width (situation 1d,1e,1f,1g)", "Set Beamer Paramater")
                OpenGLColor.oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerParameters(ByVal FOV As Single, ByVal AspectRatio As Single)
            Try
                OpenGLColor.sglBeamerAngle = FOV
                OpenGLColor.sglBeamerRatio = AspectRatio
                OpenGLColor.oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerParameters(ByVal FOV As Single, ByVal AspectRatio As Single, ByVal Width As Single)
            Try
                OpenGLColor.sglBeamerAngle = FOV
                OpenGLColor.sglBeamerRatio = AspectRatio
                OpenGLColor.sglBeamerWidth = Width
                OpenGLColor.oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerResolution()
            Try
                intBeamerPixelWidth = InputBox("Enter resolution in X direction", "Set Beamer Parameter")
                intBeamerPixelHeight = InputBox("Enter resolution in Y direction", "Set Beamer Parameter")
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerResolution(ByVal X As Integer, ByVal Y As Integer)
            Try
                intBeamerPixelWidth = X
                intBeamerPixelHeight = Y
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerGratingParameters()
            Try
                sglBeamerGratingX1 = InputBox("Enter X value of point 1", "Set Grating Parameter")
                sglBeamerGratingY1 = InputBox("Enter Y value of point 1", "Set Grating Parameter")
                sglBeamerGratingZ2 = InputBox("Enter Z value of point 1", "Set Grating Parameter")
                sglBeamerGratingX2 = InputBox("Enter X value of point 2", "Set Grating Parameter")
                sglBeamerGratingY2 = InputBox("Enter Y value of point 1", "Set Grating Parameter")
                sglBeamerGratingZ2 = InputBox("Enter Z value of point 2", "Set Grating Parameter")
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerGratingParameters(ByVal X1 As Single, ByVal Y1 As Single, ByVal Z1 As Single, ByVal X2 As Single, ByVal Y2 As Single, ByVal Z2 As Single)
            Try
                sglBeamerGratingX1 = X1
                sglBeamerGratingY1 = Y1
                sglBeamerGratingZ1 = Z1
                sglBeamerGratingX2 = X2
                sglBeamerGratingY2 = Y2
                sglBeamerGratingZ2 = Z2
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraGratingParameters()
            Try
                sglCameraGratingX1 = InputBox("Enter X value of point 1", "Set Grating Parameter")
                sglCameraGratingY1 = InputBox("Enter Y value of point 1", "Set Grating Parameter")
                sglCameraGratingZ1 = InputBox("Enter Z value of point 1", "Set Grating Parameter")
                sglCameraGratingX2 = InputBox("Enter X value of point 2", "Set Grating Parameter")
                sglCameraGratingY2 = InputBox("Enter Y value of point 2", "Set Grating Parameter")
                sglCameraGratingZ2 = InputBox("Enter Z value of point 2", "Set Grating Parameter")
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraGratingParameters(ByVal X1 As Single, ByVal Y1 As Single, ByVal Z1 As Single, ByVal X2 As Single, ByVal Y2 As Single, ByVal Z2 As Single)
            Try
                sglCameraGratingX1 = X1
                sglCameraGratingY1 = Y1
                sglCameraGratingZ1 = Z1
                sglCameraGratingX2 = X2
                sglCameraGratingY2 = Y2
                sglCameraGratingZ2 = Z2
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraGratingFrequency()
            Try
                sglCameraFrequency = InputBox("Enter frequency of grating", "Set Grating Parameter")
                sglCameraPhaseShift = InputBox("Enter phase shift of grating", "Set Grating Parameter")
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraGratingFrequency(ByVal Frequency As Single, ByVal PhaseShift As Single)
            Try
                sglCameraFrequency = Frequency
                sglCameraPhaseShift = PhaseShift
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraGratingFrequency(ByVal Frequency As Single)
            Try
                sglCameraFrequency = Frequency
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraGratingPhaseShift(ByVal PhaseShift As Single)
            Try
                sglCameraPhaseShift = PhaseShift
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraGratingResolution()
            Try
                intCameraGratingPixelWidth = InputBox("Enter resolution in X direction", "Set Grating Parameter")
                intCameraGratingPixelHeight = InputBox("Enter resolution in Y direction", "Set Grating Parameter")
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraGratingResolution(ByVal X As Integer, ByVal Y As Integer)
            Try
                intCameraGratingPixelWidth = X
                intCameraGratingPixelHeight = Y
                oUpdateShader()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetBeamerLensType(ByVal Type As LensType)
            intBeamerLensType = CInt(Type)
        End Sub
        Public Shared Sub oCalculateBeamerGratingDistance()
            Dim T As New FDMath.Point(sglBeamerTargetX, sglBeamerTargetY, sglBeamerTargetZ)
            Dim E As New FDMath.Point(sglBeamerLocationX, sglBeamerLocationY, sglBeamerLocationZ)
            Dim G1 As New FDMath.Point(sglBeamerGratingX1, sglBeamerGratingY1, sglBeamerGratingZ1)
            Dim G2 As New FDMath.Point(sglBeamerGratingX1, sglBeamerGratingY2, sglBeamerGratingZ1)
            Dim G3 As New FDMath.Point(sglBeamerGratingX2, sglBeamerGratingY2, sglBeamerGratingZ2)

            TE = T - E
            TEn = TE.Normalize
            Dim IntersectionPoint As FDMath.Point
            IntersectionPoint = FDMath.oPlaneLineIntersection(G1, G2, G3, E, T)

            Dim Vtmp As FDMath.Vector
            Vtmp = IntersectionPoint - E
            EI = Vtmp

            sglBeamerGratingDistance = Vtmp.Length
        End Sub
        Public Shared Sub oCalculateEyePlanePoints()
            Dim T As New FDMath.Point(sglBeamerTargetX, sglBeamerTargetY, sglBeamerTargetZ)
            Dim E As New FDMath.Point(sglBeamerLocationX, sglBeamerLocationY, sglBeamerLocationZ)
            Dim G1 As New FDMath.Point(sglBeamerGratingX1, sglBeamerGratingY1, sglBeamerGratingZ1)
            Dim G2 As New FDMath.Point(sglBeamerGratingX1, sglBeamerGratingY2, sglBeamerGratingZ1)
            Dim G3 As New FDMath.Point(sglBeamerGratingX2, sglBeamerGratingY2, sglBeamerGratingZ2)
            Dim TE As FDMath.Vector = T - E

            IntersectionPoint = FDMath.oPlaneLineIntersection(G1, G2, G3, E, T)

            TE = T - E
            TEn = TE.Normalize
            Dim Vtmp As FDMath.Vector
            Vtmp = IntersectionPoint - E
            EI = Vtmp

            E1 = G1 - EI
            E2 = G2 - EI
            E3 = G3 - EI
        End Sub
        Public Shared Function oFindNearValue(ByVal P1 As FDMath.Point, P2 As FDMath.Point, ByVal P3 As FDMath.Point, ByVal L1 As FDMath.Point, ByVal L2 As FDMath.Point) As Single
            Dim IntersectionPoint As FDMath.Point
            IntersectionPoint = FDMath.oPlaneLineIntersection(P1, P2, P3, L1, L2)

            Dim Eye As New FDMath.Point(sglBeamerLocationX, sglBeamerLocationY, sglBeamerLocationZ)
            Dim Vec As FDMath.Vector
            Vec = IntersectionPoint - Eye

            Return Vec.Length
        End Function
#End Region
    End Class
    Partial Class OpenGLSettings
        Enum VisualisationType
            POINT = 1
            WIREFRAME = 2
            SHADED = 3
        End Enum
        Public Shared Sub oSetVisualizationMode(ByVal intType As VisualisationType, ByVal intSize As Integer)
            Select Case intType
                Case VisualisationType.POINT
                    GL.PointSize(intSize)
                    Graphics.OpenGL.GL.PolygonMode(GraphTK.MaterialFace.FrontAndBack, Graphics.OpenGL.PolygonMode.Point)
                Case VisualisationType.WIREFRAME
                    GL.LineWidth(intSize)
                    Graphics.OpenGL.GL.PolygonMode(GraphTK.MaterialFace.FrontAndBack, Graphics.OpenGL.PolygonMode.Line)
                Case VisualisationType.SHADED
                    Graphics.OpenGL.GL.PolygonMode(GraphTK.MaterialFace.FrontAndBack, Graphics.OpenGL.PolygonMode.Fill)
            End Select
        End Sub
        Public Shared Sub oSetCulling(ByVal blnCulling As Boolean)
            If blnCulling = True Then
                GL.CullFace(GraphTK.CullFaceMode.Back)
                GL.Enable(GraphTK.EnableCap.CullFace)
            Else
                GL.Disable(GraphTK.EnableCap.CullFace)
            End If
        End Sub
        Public Shared Sub oSetBlending(ByVal blnBlending As Boolean)
            If blnBlending = True Then
                GL.Enable(GraphTK.EnableCap.Blend)
                GL.BlendFunc(OpGL.BlendingFactorSrc.SrcAlpha, OpGL.BlendingFactorDest.OneMinusSrcAlpha)
            Else
                GL.Disable(GraphTK.EnableCap.Blend)
            End If
        End Sub
        Public Shared Sub oSetTexture2D(ByVal blnTexture As Boolean)
            If blnTexture = True Then
                GL.Enable(GraphTK.EnableCap.Texture2D)
            Else
                GL.Disable(GraphTK.EnableCap.Texture2D)
            End If
        End Sub
    End Class
    Partial Class CameraSettings
        Enum PointOfView
            BeamerOrthogonal = 1
            BeamerPerspective = 2
            CameraPerspective = 3
        End Enum
        Private Shared sglCameraLocationX As Single = 0
        Private Shared sglCameraLocationY As Single = 0
        Private Shared sglCameraLocationZ As Single = 300
        Private Shared sglCameraTargetX As Single = 0
        Private Shared sglCameraTargetY As Single = 0
        Private Shared sglCameraTargetZ As Single = 0
        Private Shared sglCameraFOV As Single = 0.1331
        Private Shared sglCameraAspectRatio As Single = 4 / 3
        Private Shared sglCameraNear As Single = 1
        Private Shared sglCameraFar As Single = 1000
        Private Shared sglCameraAngle As Single = 0
        Public Shared Sub oGetCamerLocation(ByRef X As Single, ByRef Y As Single, ByRef Z As Single)
            X = sglCameraLocationX
            Y = sglCameraLocationY
            Z = sglCameraLocationZ
        End Sub
        Public Shared Function oGetCameraLocationX() As Single
            Return sglCameraLocationX
        End Function
        Public Shared Function oGetCameraLocationY() As Single
            Return sglCameraLocationY
        End Function
        Public Shared Function oGetCameraLocationZ() As Single
            Return sglCameraLocationZ
        End Function
        Public Shared Function oGetCameraTargetX() As Single
            Return sglCameraTargetX
        End Function
        Public Shared Function oGetCameraTargetY() As Single
            Return sglCameraTargetY
        End Function
        Public Shared Function oGetCameraTargetZ() As Single
            Return sglCameraTargetZ
        End Function
        Public Shared Function oGetCameraFOV() As Single
            Return sglCameraFOV
        End Function
        Public Shared Function oGetCameraAspectRatio() As Single
            Return sglCameraAspectRatio
        End Function
        Public Shared Function oGetCameraNear() As Single
            Return sglCameraNear
        End Function
        Public Shared Function oGetCameraFar() As Single
            Return sglCameraFar
        End Function
        Public Shared Sub oSetCameraLocation()
            Try
                sglCameraLocationX = InputBox("Enter X", "Set Camera Location")
                sglCameraLocationY = InputBox("Enter Y", "Set Camera Location")
                sglCameraLocationZ = InputBox("Enter Z", "Set Camera Location")
                oUpdateCameraSettings()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraLocation(ByVal X As Single, ByVal Y As Single, ByVal Z As Single)
            Try
                sglCameraLocationX = X
                sglCameraLocationY = Y
                sglCameraLocationZ = Z
                oUpdateCameraSettings()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraTarget()
            Try
                sglCameraTargetX = InputBox("Enter X", "Set Camera Target")
                sglCameraTargetY = InputBox("Enter Y", "Set Camera Target")
                sglCameraTargetZ = InputBox("Enter Z", "Set Camera Target")
                oUpdateCameraSettings()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub
        Public Shared Sub oSetCameraTarget(ByVal X As Single, ByVal Y As Single, ByVal Z As Single)
            Try
                sglCameraTargetX = X
                sglCameraTargetY = Y
                sglCameraTargetZ = Z
                oUpdateCameraSettings()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraParameters()
            Try
                sglCameraFOV = InputBox("Enter FOV", "Set Camera Parameters")
                sglCameraAspectRatio = InputBox("Enter Aspect Ratio", "Set Camera Parameters")
                sglCameraNear = InputBox("Enter Near Plane", "Set Camera Parameters")
                sglCameraFar = InputBox("Enter Far Plane", "Set Camera Parameters")
                oUpdateCameraSettings()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetCameraParameters(ByVal FOV As Single, ByVal AspectRatio As Single, ByVal Near As Single, ByVal Far As Single)
            Try
                sglCameraFOV = FOV
                sglCameraAspectRatio = AspectRatio
                sglCameraNear = Near
                sglCameraFar = Far
                oUpdateCameraSettings()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oUpdateCameraSettings()
            Dim vEye, vTarget As Vector3
            vEye.X = sglCameraLocationX
            vEye.Y = sglCameraLocationY
            vEye.Z = sglCameraLocationZ
            vTarget.X = sglCameraTargetX
            vTarget.Y = sglCameraTargetY
            vTarget.Z = sglCameraTargetZ
            Dim vUp As Vector3 = Vector3.UnitY
            CameraSettings.oSetFOVCameraPerspective(vEye, vTarget, vUp, sglCameraFOV, sglCameraAspectRatio, sglCameraNear, sglCameraFar)
        End Sub
        Public Shared Sub oSetFOVBeamerOrthogonal(ByVal TransformationMatrix As Matrix4)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)
            GL.LoadMatrix(TransformationMatrix)
            'Dim MyVector As Vector3
            'MyVector = New Vector3(0, 1, 0)
            'LoadMatrix(Matrix4.Mult(Matrix4.CreateOrthographic(100, 100, -1000, 1000), Matrix4.CreateRotationY(1.3)))
            'LoadMatrix(Matrix4.CreateRotationY(sglCameraAngle))

            'LoadMatrix(Matrix4.LookAt(0, 0, 200, 0, 0, 0, 0, 1, 0))
            'Ortho(-sglMaxDif * 4 / 3 / 2, sglMaxDif * 4 / 3 / 2, -sglMaxDif / 2, sglMaxDif / 2, -1000, 1000)
            GL.Ortho(-20 * 1.333333333, 20 * 1.333333333, -20, 20, -1000, 1000)
        End Sub
        Public Shared Sub oSetFOVBeamerPerspective(ByVal TranslationMatrix As Matrix4, ByVal sglFovy As Single, ByVal sglAspect As Single, ByVal sglNear As Single, ByVal sglFar As Single)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)
            Dim PerspectiveMatrix As Matrix4 = Matrix4.CreatePerspectiveFieldOfView(sglFovy, sglAspect, sglNear, sglFar)
            Try
                Dim TotalMatrix As Matrix4
                TotalMatrix = Matrix4.Mult(TranslationMatrix, PerspectiveMatrix)
                GL.LoadMatrix(TotalMatrix)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
        Public Shared Sub oSetFOVCameraPerspective(ByVal vEye As Vector3, ByVal vTarget As Vector3, ByVal vUp As Vector3, ByVal sglFovy As Single, ByVal sglAspect As Single, ByVal sglNear As Single, ByVal sglFar As Single)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)
            Dim LookAtMatrix As Matrix4 = Matrix4.LookAt(vEye, vTarget, vUp)
            Dim PerspectiveMatrix As Matrix4 = Matrix4.CreatePerspectiveFieldOfView(sglFovy, sglAspect, sglNear, sglFar)
            Dim TotalMatrix As Matrix4
            TotalMatrix = Matrix4.Mult(LookAtMatrix, PerspectiveMatrix)
            GL.LoadMatrix(TotalMatrix)
        End Sub
        Public Shared Sub oSetFOVCameraPerspectiveDepth(ByVal vEye As Vector3, ByVal vTarget As Vector3, ByVal vUp As Vector3, ByVal sglLeft As Single, ByVal sglRight As Single, ByVal sglBottom As Single, ByVal sglTop As Single, ByVal sglNear As Single, ByVal sglFar As Single)
            GL.MatrixMode(Graphics.OpenGL.MatrixMode.Projection)
            Dim LookAtMatrix As Matrix4 = Matrix4.LookAt(vEye, vTarget, vUp)
            'Dim PerspectiveMatrix As Matrix4 = Matrix4.CreatePerspectiveFieldOfView(sglFovy, sglAspect, sglNear, sglFar)

            Dim WantedVector As New FDMath.Vector(0, 0, -1)
            Dim TargetVector As New FDMath.Vector(OpenGLColor.oGetBeamerTargetX - OpenGLColor.oGetBeamerLocationX, OpenGLColor.oGetBeamerTargetY - OpenGLColor.oGetBeamerLocationY, OpenGLColor.ogetBeamerTargetZ - OpenGLColor.oGetBeamerLocationZ)
            Dim GratingPoint1 As New FDMath.Point(OpenGLColor.oGetBeamerGratingX1, OpenGLColor.oGetBeamerGratingY1, OpenGLColor.oGetBeamerGratingZ1)
            Dim GratingPoint2 As New FDMath.Point(OpenGLColor.oGetBeamerGratingX2, OpenGLColor.oGetBeamerGratingY2, OpenGLColor.oGetBeamerGratingZ2)
            Dim NewGratingPoint1 As FDMath.Point
            Dim NewGratingPoint2 As FDMath.Point
            Dim R As FDMath.Matrix3

            R = FDMath.oFindRotationMatrixBetween2Vectors(TargetVector, WantedVector)
            NewGratingPoint1 = FDMath.oRound(R * GratingPoint1, 4)
            NewGratingPoint2 = FDMath.oRound(R * GratingPoint2, 4)

            Dim PerspectiveMatrix As Matrix4 = Matrix4.CreatePerspectiveOffCenter(NewGratingPoint1.X, NewGratingPoint2.X, NewGratingPoint1.Y, NewGratingPoint2.Y, sglNear, sglFar) 'UNROTATED Grating!!!
            'Dim PerspectiveMatrix As Matrix4 = Matrix4.CreatePerspectiveOffCenter(5.3, 18.0, 1.425, 10.95, 25, 60) 'UNROTATED Grating!!!
            'Dim PerspectiveMatrix As Matrix4 = Matrix4.CreatePerspectiveOffCenter(sglLeft, sglRight, sglBottom, sglTop, sglNear, sglFar)
            Dim TotalMatrix As Matrix4
            TotalMatrix = Matrix4.Mult(LookAtMatrix, PerspectiveMatrix)
            GL.LoadMatrix(TotalMatrix)
        End Sub
        Public Shared Function oTakeScreenshotFunction(ByVal GlControlActive As GLControl) As Drawing.Bitmap
            Dim bmp As Drawing.Bitmap = New Drawing.Bitmap(GlControlActive.Width, GlControlActive.Height)
            Dim rect As Drawing.Rectangle
            rect.X = 0
            rect.Y = 0
            rect.Width = GlControlActive.Width
            rect.Height = GlControlActive.Height
            Dim data As System.Drawing.Imaging.BitmapData = bmp.LockBits(rect, Drawing.Imaging.ImageLockMode.WriteOnly, Drawing.Imaging.PixelFormat.Format24bppRgb)
            GL.ReadPixels(0, 0, GlControlActive.Width, GlControlActive.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, data.Scan0)
            bmp.UnlockBits(data)
            bmp.RotateFlip(Drawing.RotateFlipType.RotateNoneFlipY)
            Return bmp
        End Function
    End Class
    Partial Class Draw
        Public Shared Sub oDrawRectangle(ByVal X1 As Single, ByVal X2 As Single, ByVal Y1 As Single, ByVal Y2 As Single, ByVal Z1 As Single, ByVal Z2 As Single, ByVal intListNumber As Integer, ByVal R As Single, ByVal G As Single, ByVal B As Single, ByVal A As Single)
            GL.NewList(intListNumber, OpenTK.Graphics.OpenGL.ListMode.Compile)
            GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Triangles)
            GL.Vertex3(X1, Y1, Z1) : GL.Vertex3(X1, Y2, Z1) : GL.Vertex3(X2, Y2, Z2)
            GL.Vertex3(X1, Y1, Z1) : GL.Vertex3(X2, Y2, Z2) : GL.Vertex3(X2, Y1, Z2)
            GL.End()
            GL.EndList()
        End Sub
        Public Shared Sub oDrawRectangleWithTexCoordinates(ByVal X1 As Single, ByVal X2 As Single, ByVal Y1 As Single, ByVal Y2 As Single, ByVal Z1 As Single, ByVal Z2 As Single, ByVal intListNumber As Integer)
            GL.NewList(intListNumber, OpenTK.Graphics.OpenGL.ListMode.Compile)
            GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Quads)
            GL.TexCoord2(0.0, 1.0) : GL.Vertex3(X1, Y1, Z1)
            GL.TexCoord2(0.0, 0.0) : GL.Vertex3(X1, Y2, Z1)
            GL.TexCoord2(1.0, 0.0) : GL.Vertex3(X2, Y2, Z2)
            GL.TexCoord2(1.0, 1.0) : GL.Vertex3(X2, Y1, Z2)
            GL.[End]()
            GL.EndList()
        End Sub
        Public Shared Sub oDrawRectangleTexture(ByVal X1 As Single, ByVal X2 As Single, ByVal Y1 As Single, ByVal Y2 As Single, ByVal Z1 As Single, ByVal Z2 As Single, ByVal intListNumber As Integer, ByVal R As Single, ByVal G As Single, ByVal B As Single, ByVal A As Single, ByVal strTexturePath As String)
            MsgBox("oDrawRectangleTexture: Revision needed!")
            GL.Enable(OpGL.EnableCap.Texture2D)

            'Dim strTextureName As String = "artTex.png"

            'ActiveTexture(TextureUnit.Texture0)

            Dim TextureID As Integer = 15 'number 15 as test
            Dim TextureBitmap As Drawing.Bitmap = New Drawing.Bitmap(strTexturePath)
            Dim TextureData As Drawing.Imaging.BitmapData
            TextureData = TextureBitmap.LockBits(New System.Drawing.Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            GL.GenTextures(1, TextureID)
            GL.BindTexture(OpGL.TextureTarget.Texture2D, TextureID)
            'GL.Uniform1(GL.GetUniformLocation(ShaderProgramhandle, UniformName), TexUnit - OpGL.TextureUnit.Texture0)

            'the following code sets certian parameters for the texture
            GL.TexEnv(OpGL.TextureEnvTarget.TextureEnv, OpGL.TextureEnvParameter.TextureEnvMode, Convert.ToSingle(OpGL.TextureEnvMode.Modulate))
            GL.TexParameter(OpGL.TextureTarget.Texture2D, OpGL.TextureParameterName.TextureMinFilter, Convert.ToSingle(OpGL.TextureMinFilter.LinearMipmapLinear))
            GL.TexParameter(OpGL.TextureTarget.Texture2D, OpGL.TextureParameterName.TextureMagFilter, Convert.ToSingle(OpGL.TextureMagFilter.Linear))

            'TexImage2D(TextureTarget.ProxyTexture2D,0,PixelInternalFormat.Rgba,PixelFormat.rgba,

            'load the data by telling OpenGL to build mipmaps out of the bitmap datab
            ' Graphics.Glu.Build2DMipmap(Graphics.TextureTarget.Texture2D, CInt(OpGL.PixelInternalFormat.Three), TextureBitmap.Width, TextureBitmap.Height, Graphics.PixelFormat.Bgr, Graphics.PixelType.UnsignedByte, TextureData.Scan0)
            Graphics.Glu.Build2DMipmap(Graphics.TextureTarget.Texture2D, CInt(OpGL.PixelInternalFormat.Three), TextureBitmap.Width, TextureBitmap.Height, Graphics.PixelFormat.Bgr, Graphics.PixelType.UnsignedByte, TextureData.Scan0)

            TextureBitmap.UnlockBits(TextureData)

            GL.NewList(intListNumber, OpenTK.Graphics.OpenGL.ListMode.Compile)
            GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Quads)
            GL.TexCoord2(0.0, 1.0) : GL.Vertex3(X1, Y1, Z1)
            GL.TexCoord2(0.0, 0.0) : GL.Vertex3(X1, Y2, Z1)
            GL.TexCoord2(1.0, 0.0) : GL.Vertex3(X2, Y2, Z2)
            GL.TexCoord2(1.0, 1.0) : GL.Vertex3(X2, Y1, Z2)
            GL.[End]()
            GL.EndList()


            Dim ShaderProgramhandle As Integer = OpenGLColor.oGetShaderHandle
            GL.Uniform1(GL.GetUniformLocation(ShaderProgramhandle, "MyTexture0"), TextureID)
        End Sub
        Public Shared Sub oAttachTexture(ByVal strTexturePath As String)
            MsgBox("oDrawRectangleTexture: Revision needed!")
            GL.Enable(OpGL.EnableCap.Texture2D)

            'Dim strTextureName As String = "artTex.png"

            'ActiveTexture(TextureUnit.Texture0)

            Dim TextureID As Integer = 15 'number 15 as test
            Dim TextureBitmap As Drawing.Bitmap = New Drawing.Bitmap(strTexturePath)
            Dim TextureData As Drawing.Imaging.BitmapData
            TextureData = TextureBitmap.LockBits(New System.Drawing.Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            GL.GenTextures(1, TextureID)
            GL.BindTexture(OpGL.TextureTarget.Texture2D, TextureID)
            'GL.Uniform1(GL.GetUniformLocation(ShaderProgramhandle, UniformName), TexUnit - OpGL.TextureUnit.Texture0)

            'the following code sets certian parameters for the texture
            GL.TexEnv(OpGL.TextureEnvTarget.TextureEnv, OpGL.TextureEnvParameter.TextureEnvMode, Convert.ToSingle(OpGL.TextureEnvMode.Modulate))
            GL.TexParameter(OpGL.TextureTarget.Texture2D, OpGL.TextureParameterName.TextureMinFilter, Convert.ToSingle(OpGL.TextureMinFilter.LinearMipmapLinear))
            GL.TexParameter(OpGL.TextureTarget.Texture2D, OpGL.TextureParameterName.TextureMagFilter, Convert.ToSingle(OpGL.TextureMagFilter.Linear))

            'TexImage2D(TextureTarget.ProxyTexture2D,0,PixelInternalFormat.Rgba,PixelFormat.rgba,

            'load the data by telling OpenGL to build mipmaps out of the bitmap datab
            ' Graphics.Glu.Build2DMipmap(Graphics.TextureTarget.Texture2D, CInt(OpGL.PixelInternalFormat.Three), TextureBitmap.Width, TextureBitmap.Height, Graphics.PixelFormat.Bgr, Graphics.PixelType.UnsignedByte, TextureData.Scan0)
            Graphics.Glu.Build2DMipmap(Graphics.TextureTarget.Texture2D, CInt(OpGL.PixelInternalFormat.Three), TextureBitmap.Width, TextureBitmap.Height, Graphics.PixelFormat.Bgr, Graphics.PixelType.UnsignedByte, TextureData.Scan0)

            TextureBitmap.UnlockBits(TextureData)

            Dim ShaderProgramhandle As Integer = OpenGLColor.oGetShaderHandle
            GL.Uniform1(GL.GetUniformLocation(ShaderProgramhandle, "MyTexture0"), TextureID)
        End Sub
        Public Shared Sub oCreateTexture(ByRef textureID As Integer, ByVal bm As Drawing.Bitmap)
            'Load texture
            GL.GenTextures(1, textureID)

            'Still required else TexImage2D will be applied on the last bound texture
            GL.BindTexture(OpGL.TextureTarget.ProxyTexture2D, textureID)
            Dim data As Drawing.Imaging.BitmapData = bm.LockBits(New Drawing.Rectangle(0, 0, bm.Width, bm.Height), Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb)

            GL.TexImage2D(OpGL.TextureTarget.Texture2D, 0, OpGL.PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, OpGL.PixelType.UnsignedByte, data.Scan0)

            bm.UnlockBits(data)
            GL.TexParameter(OpGL.TextureTarget.Texture2D, OpGL.TextureParameterName.TextureMinFilter, Convert.ToSingle(OpGL.TextureMagFilter.Linear))
        End Sub
        Public Shared Sub oBindTexture(ByRef TextureID As Integer, ByVal TexUnit As OpGL.TextureUnit, ByVal UniformName As String)
            GL.ActiveTexture(TexUnit)
            GL.BindTexture(OpGL.TextureTarget.Texture2D, TextureID)
            Dim ShaderProgramhandle As Integer = OpenGLColor.oGetShaderHandle
            GL.Uniform1(GL.GetUniformLocation(ShaderProgramhandle, UniformName), TexUnit - OpGL.TextureUnit.Texture0)
        End Sub
        Public Shared Sub oDrawGrating(ByVal intListNumber As Integer)
            GL.NewList(intListNumber, OpenTK.Graphics.OpenGL.ListMode.Compile)
            GL.Begin(OpenTK.Graphics.OpenGL.BeginMode.Triangles)

            GL.Vertex3(OpenGLColor.oGetCameraGratingX1, OpenGLColor.oGetCameraGratingY1, OpenGLColor.oGetCameraGratingZ1) : GL.Vertex3(OpenGLColor.oGetCameraGratingX2, OpenGLColor.oGetCameraGratingY2, OpenGLColor.oGetCameraGratingZ2) : GL.Vertex3(OpenGLColor.oGetCameraGratingX1, OpenGLColor.oGetCameraGratingY2, OpenGLColor.oGetCameraGratingZ1)
            GL.Vertex3(OpenGLColor.oGetCameraGratingX1, OpenGLColor.oGetCameraGratingY1, OpenGLColor.oGetCameraGratingZ1) : GL.Vertex3(OpenGLColor.oGetCameraGratingX2, OpenGLColor.oGetCameraGratingY2, OpenGLColor.oGetCameraGratingZ2) : GL.Vertex3(OpenGLColor.oGetCameraGratingX1, OpenGLColor.oGetCameraGratingY2, OpenGLColor.oGetCameraGratingZ1)
            GL.Vertex3(OpenGLColor.oGetCameraGratingX1, OpenGLColor.oGetCameraGratingY1, OpenGLColor.oGetCameraGratingZ1) : GL.Vertex3(OpenGLColor.oGetCameraGratingX2, OpenGLColor.oGetCameraGratingY1, OpenGLColor.oGetCameraGratingZ2) : GL.Vertex3(OpenGLColor.oGetCameraGratingX2, OpenGLColor.oGetCameraGratingY2, OpenGLColor.oGetCameraGratingZ2)
            GL.End()
            GL.EndList()
        End Sub
        Public Shared Sub oDrawingVerticalFringes(ByVal GLC As GLControl, ByVal dblFrequency As Double, ByVal color As Drawing.Color)
            OpenGLColor.oSetLightColor(Drawing.Color.Black, color, Drawing.Color.Black)
            OpenGLColor.oSetModelColor(Drawing.Color.Black, Drawing.Color.DarkGray, Drawing.Color.Black, Drawing.Color.Black, Drawing.Color.Black)
            OpenGLColor.oSetModelColoring(False)

            Dim intScreenX, intScreenY As Integer
            Dim sglWidth As Single
            intScreenX = GLC.Width
            intScreenY = GLC.Height
            sglWidth = intScreenX / dblFrequency

            Dim dblPhase As Double
            Dim temp As Double

            'Left fixed
            For i As Integer = 0 To intScreenX
                temp = (i Mod sglWidth)
                dblPhase = (M.Sin(temp / sglWidth * 2 * M.PI) + 1) / 2
                GL.LineWidth(sglWidth)
                GL.Begin(OpGL.BeginMode.Lines)
                GL.Color3(dblPhase, dblPhase, dblPhase) : GL.Vertex2(i, -intScreenY)
                GL.Color3(dblPhase, dblPhase, dblPhase) : GL.Vertex2(i, intScreenY)
                GL.End()
            Next
        End Sub
        Public Shared Sub oGenerateShadowFBO(ByRef DepthTextureID As Integer)
            Dim FBOID As Integer = Nothing
            Dim ShadowMapWidth As Integer = 1024
            Dim ShadowMapHeight As Integer = 768

            OpenGLMain.oSetDepthTest(True)

            'GLenum FBOstatus;
            Try
                'Try to use a texture depth component
                GL.GenTextures(1, DepthTextureID)
                GL.BindTexture(Graphics.OpenGL.TextureTarget.Texture2D, DepthTextureID)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error001 oGenerateShadowFBO")
            End Try

            Try
                'GL_LINEAR does not make sense for depth texture. However, next tutorial shows usage of GL_LINEAR and PCF
                GL.TexParameter(Graphics.OpenGL.TextureTarget.Texture2D, Graphics.OpenGL.TextureParameterName.TextureMinFilter, GAL.Nearest)
                GL.TexParameter(Graphics.OpenGL.TextureTarget.Texture2D, Graphics.OpenGL.TextureParameterName.TextureMagFilter, GAL.Nearest)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error002 oGenerateShadowFBO")
            End Try

            Try
                'Remove artifact on the edges of the shadowmap
                GL.TexParameter(Graphics.OpenGL.TextureTarget.Texture2D, Graphics.OpenGL.TextureParameterName.TextureWrapS, GAL.Clamp)
                GL.TexParameter(Graphics.OpenGL.TextureTarget.Texture2D, Graphics.OpenGL.TextureParameterName.TextureWrapT, GAL.Clamp)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error003 oGenerateShadowFBO")
            End Try

            Try
                '    'No need to force GL_DEPTH_COMPONENT24, drivers usually give you the max precision if available
                ' GL.TexImage2D(Graphics.OpenGL.TextureTarget.Texture2D, 0, Graphics.OpenGL.PixelInternalFormat.DepthComponent, ShadowMapWidth, ShadowMapHeight, 0, Graphics.OpenGL.PixelFormat.DepthComponent, Graphics.OpenGL.PixelType.UnsignedByte, 0)
                GL.TexImage2D(Graphics.OpenGL.TextureTarget.ProxyTexture2D, 0, Graphics.OpenGL.PixelInternalFormat.DepthComponent32, ShadowMapWidth, ShadowMapHeight, 0, Graphics.OpenGL.PixelFormat.DepthComponent, Graphics.OpenGL.PixelType.UnsignedByte, 0)
                GL.BindTexture(Graphics.OpenGL.TextureTarget.Texture2D, 0)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error004 oGenerateShadowFBO")
            End Try

            Try
                'create a framebuffer object
                GL.GenFramebuffers(1, FBOID)
                GL.BindFramebuffer(Graphics.OpenGL.FramebufferTarget.FramebufferExt, FBOID)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error005 oGenerateShadowFBO")
            End Try

            Try
                'Instruct openGL that we won't bind a color texture with the currently bound FBO
                GL.DrawBuffer(GAL.None)
                GL.ReadBuffer(GAL.None)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error006 oGenerateShadowFBO")
            End Try

            Try
                'attach the texture to FBO depth attachment point
                GL.FramebufferTexture2D(Graphics.OpenGL.FramebufferTarget.FramebufferExt, Graphics.OpenGL.FramebufferAttachment.DepthAttachmentExt, Graphics.OpenGL.TextureTarget.Texture2D, DepthTextureID, 0)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error007 oGenerateShadowFBO")
            End Try

            Try
                'check FBO status
                Dim FBOStatus As GraphTK.FramebufferErrorCode
                FBOStatus = GL.CheckFramebufferStatus(Graphics.OpenGL.FramebufferTarget.FramebufferExt)
                If FBOStatus <> Graphics.FramebufferErrorCode.FramebufferCompleteExt Then
                    MsgBox("GL_FRAMEBUFFER_COMPLETE_EXT failed, CANNOT use FBO", MsgBoxStyle.Critical, "Error008 oGenerateShadowFBO")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error008 oGenerateShadowFBO")
            End Try

            Try
                'switch back to window-system-provided framebuffer
                GL.BindFramebuffer(Graphics.OpenGL.FramebufferTarget.FramebufferExt, 0)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error009 oGenerateShadowFBO")
            End Try

            Try
                Dim ShaderProgramhandle As Integer = OpenGLColor.oGetShaderHandle
                GL.Uniform1(GL.GetUniformLocation(ShaderProgramhandle, "ShadowMap"), DepthTextureID)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error010 oGenerateShadowFBO")
            End Try
        End Sub
    End Class
    Partial Class ReadSettings
        Private Shared strHeader As String
        Private Shared strDescription As String
        Private Shared datDate As Date
        Private Shared lens As LensType
        Private Shared intNumberMeasurements As Integer = -1
        Private Shared strModelPath As String = ""
        Public Shared Measurements() As Measurement
        Enum LensType
            TELECENTRIC = 1
            DIVERGING = 2
        End Enum
        Enum MeasurementType
            FRINGE = 1
            MOIRE = 2
        End Enum
        Public Structure Measurement
            Dim Type As MeasurementType
            Dim NumberBlackoutImages As Integer
            Dim NumberSteps As Integer
            Dim MapPath As String
            Dim FileName As String
            Dim intListArray() As Integer
            Dim BeamerGratingStartFrequency As Single
            Dim BeamerGratingStopFrequency As Single
            Dim BeamerGratingStartPhase As Single
            Dim BeamerGratingStopPhase As Single
            Dim CameraGratingStartFrequency As Single
            Dim CameraGratingStopFrequency As Single
            Dim CameraGratingStartPhase As Single
            Dim CameraGratingStopPhase As Single
        End Structure
        Public Shared Function oReadSettingsFileXMl() As Measurement()
            Dim OFD1 As New OpenFileDialog
            Dim FilePath As String
            Try
                OFD1.Title = "Open settings file"
                OFD1.Filter = "Fringe projectpr settings file|*.fpss"
                If OFD1.ShowDialog = DialogResult.OK Then
                    FilePath = OFD1.InitialDirectory & OFD1.FileName
                Else
                    MsgBox("Open settings file was cancelled")
                    Return Nothing
                    Exit Function
                End If
                Return oReadSettingsFileXMl(FilePath)
            Catch ex As Exception
                MessageBox.Show("Fout bij het lezen van de file")
                Return Nothing
            End Try
        End Function
        Public Shared Function oReadSettingsFileXMl(ByVal FilePath As String) As Measurement()
            Try
                Dim Measurements() As Measurement = Nothing
                Dim xmlr As New XmlTextReader(FilePath)
                xmlr.WhitespaceHandling = WhitespaceHandling.None
                xmlr.ResetState()
                intNumberMeasurements = -1

                While xmlr.Read()
                    Select Case xmlr.NodeType
                        Case XmlNodeType.Element
                            Select Case xmlr.Name.ToLower
                                Case "header"
                                    xmlr.Read()
                                    strHeader = xmlr.Value
                                Case "description"
                                    xmlr.Read()
                                    strDescription = xmlr.Value
                                Case "date"
                                    xmlr.Read()
                                    datDate = xmlr.Value
                                Case "modelpath"
                                    xmlr.Read()
                                    strModelPath = xmlr.Value
                                    Dim blnASCII As Boolean = ReadSTL.oFormatCheck(strModelPath)
                                    ReadSTL.oSTLtoList(strModelPath, 1, blnASCII, OpenGLSettings.VisualisationType.SHADED, 1, True)
                                Case "beamer"
                                    Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "beamer"
                                        xmlr.Read()
                                        Select Case xmlr.Name.ToLower
                                            Case "location"
                                                Dim x, y, z As Single
                                                Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "location"
                                                    xmlr.Read()
                                                    If xmlr.NodeType = XmlNodeType.Element Then
                                                        Select Case xmlr.Name
                                                            Case "x"
                                                                xmlr.Read()
                                                                x = xmlr.ReadContentAsFloat
                                                            Case "y"
                                                                xmlr.Read()
                                                                y = xmlr.ReadContentAsFloat
                                                            Case "z"
                                                                xmlr.Read()
                                                                z = xmlr.ReadContentAsFloat
                                                        End Select
                                                    End If
                                                Loop
                                                OpenGLColor.oSetBeamerLocation(x, y, z)
                                            Case "target"
                                                Dim x, y, z As Single
                                                Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "target"
                                                    xmlr.Read()
                                                    If xmlr.NodeType = XmlNodeType.Element Then
                                                        Select Case xmlr.Name
                                                            Case "x"
                                                                xmlr.Read()
                                                                x = xmlr.ReadContentAsFloat
                                                            Case "y"
                                                                xmlr.Read()
                                                                y = xmlr.ReadContentAsFloat
                                                            Case "z"
                                                                xmlr.Read()
                                                                z = xmlr.ReadContentAsFloat
                                                        End Select
                                                    End If
                                                Loop
                                                OpenGLColor.oSetBeamerTarget(x, y, z)
                                            Case "gratingresolution"
                                                Dim x, y As Single
                                                Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "gratingresolution"
                                                    xmlr.Read()
                                                    If xmlr.NodeType = XmlNodeType.Element Then
                                                        Select Case xmlr.Name
                                                            Case "x"
                                                                xmlr.Read()
                                                                x = xmlr.ReadContentAsFloat
                                                            Case "y"
                                                                xmlr.Read()
                                                                y = xmlr.ReadContentAsFloat
                                                        End Select
                                                    End If
                                                Loop
                                                OpenGLColor.oSetBeamerResolution(x, y)
                                            Case "lens"
                                                xmlr.Read()
                                                If xmlr.Value.ToLower = "diverging" Then

                                                    Do Until xmlr.NodeType = XmlNodeType.Element And xmlr.Name = "parameters"
                                                        xmlr.Read()
                                                    Loop
                                                    Dim fov, aspect, freq, phase As Single
                                                    Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "parameters"
                                                        xmlr.Read()
                                                        If xmlr.NodeType = XmlNodeType.Element Then
                                                            Select Case xmlr.Name.ToLower
                                                                Case "fov"
                                                                    xmlr.Read()
                                                                    fov = xmlr.ReadContentAsFloat
                                                                Case "aspectratio"
                                                                    xmlr.Read()
                                                                    aspect = xmlr.ReadContentAsFloat
                                                                Case "frequency"
                                                                    xmlr.Read()
                                                                    freq = xmlr.ReadContentAsFloat
                                                                Case "phaseshift"
                                                                    xmlr.Read()
                                                                    phase = xmlr.ReadContentAsFloat
                                                            End Select
                                                        End If
                                                    Loop
                                                    OpenGLColor.oSetBeamerParameters(fov, aspect)
                                                    OpenGLColor.oSetBeamerFrequency(freq)
                                                    OpenGLColor.oSetBeamerPhaseShift(phase)
                                                    OpenGLColor.oSetLightDirection(False)
                                                    OpenGLColor.oSetBeamerLensType(OpenGLColor.LensType.DIVERGING)
                                                ElseIf xmlr.Value.ToLower = "telecentric" Then
                                                    Do Until xmlr.NodeType = XmlNodeType.Element And xmlr.Name = "parameters"
                                                        xmlr.Read()
                                                    Loop
                                                    Dim width, freq, phase As Single
                                                    Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "parameters"
                                                        xmlr.Read()
                                                        If xmlr.NodeType = XmlNodeType.Element Then
                                                            Select Case xmlr.Name.ToLower
                                                                Case "width"
                                                                    xmlr.Read()
                                                                    width = xmlr.ReadContentAsFloat
                                                                Case "frequency"
                                                                    xmlr.Read()
                                                                    freq = xmlr.ReadContentAsFloat
                                                                Case "phaseshift"
                                                                    xmlr.Read()
                                                                    phase = xmlr.ReadContentAsFloat
                                                            End Select
                                                        End If
                                                    Loop
                                                    OpenGLColor.oSetBeamerWidth(width)
                                                    OpenGLColor.oSetBeamerFrequency(freq)
                                                    OpenGLColor.oSetBeamerPhaseShift(phase)
                                                    OpenGLColor.oSetLightDirection(True)
                                                    OpenGLColor.oSetBeamerLensType(OpenGLColor.LensType.TELECENTRIC)
                                                End If
                                            Case "grating"
                                                Dim x1, y1, z1, x2, y2, z2 As Single
                                                Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "grating"
                                                    xmlr.Read()
                                                    If xmlr.NodeType = XmlNodeType.Element Then
                                                        Select Case xmlr.Name.ToLower
                                                            Case "x1"
                                                                xmlr.Read()
                                                                x1 = xmlr.ReadContentAsFloat
                                                            Case "y1"
                                                                xmlr.Read()
                                                                y1 = xmlr.ReadContentAsFloat
                                                            Case "z1"
                                                                xmlr.Read()
                                                                z1 = xmlr.ReadContentAsFloat
                                                            Case "x2"
                                                                xmlr.Read()
                                                                x2 = xmlr.ReadContentAsFloat
                                                            Case "y2"
                                                                xmlr.Read()
                                                                y2 = xmlr.ReadContentAsFloat
                                                            Case "z2"
                                                                xmlr.Read()
                                                                z2 = xmlr.ReadContentAsFloat
                                                        End Select
                                                    End If
                                                Loop
                                                Dim tmp As Single
                                                tmp = System.Math.Sqrt((x2 - x1) ^ 2 + (z2 - z1) ^ 2)
                                                OpenGLColor.oSetBeamerWidth(tmp)
                                                OpenGLColor.oSetBeamerGratingParameters(x1, y1, z1, x2, y2, z2)
                                        End Select
                                    Loop
                                Case "camera"
                                    Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "camera"
                                        xmlr.Read()
                                        Select Case xmlr.Name.ToLower
                                            Case "location"
                                                Dim x, y, z As Single
                                                Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "location"
                                                    xmlr.Read()
                                                    If xmlr.NodeType = XmlNodeType.Element Then
                                                        Select Case xmlr.Name.ToLower
                                                            Case "x"
                                                                xmlr.Read()
                                                                x = xmlr.ReadContentAsFloat
                                                            Case "y"
                                                                xmlr.Read()
                                                                y = xmlr.ReadContentAsFloat
                                                            Case "z"
                                                                xmlr.Read()
                                                                z = xmlr.ReadContentAsFloat
                                                        End Select
                                                    End If
                                                Loop
                                                CameraSettings.oSetCameraLocation(x, y, z)
                                            Case "target"
                                                Dim x, y, z As Single
                                                Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "target"
                                                    xmlr.Read()
                                                    If xmlr.NodeType = XmlNodeType.Element Then
                                                        Select Case xmlr.Name
                                                            Case "x"
                                                                xmlr.Read()
                                                                x = xmlr.ReadContentAsFloat
                                                            Case "y"
                                                                xmlr.Read()
                                                                y = xmlr.ReadContentAsFloat
                                                            Case "z"
                                                                xmlr.Read()
                                                                z = xmlr.ReadContentAsFloat
                                                        End Select
                                                    End If
                                                Loop
                                                CameraSettings.oSetCameraTarget(x, y, z)
                                            Case "gratingresolution"
                                                Dim x, y As Single
                                                Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "gratingresolution"
                                                    xmlr.Read()
                                                    If xmlr.NodeType = XmlNodeType.Element Then
                                                        Select Case xmlr.Name
                                                            Case "x"
                                                                xmlr.Read()
                                                                x = xmlr.ReadContentAsFloat
                                                            Case "y"
                                                                xmlr.Read()
                                                                y = xmlr.ReadContentAsFloat
                                                        End Select
                                                    End If
                                                Loop
                                                OpenGLColor.oSetCameraGratingResolution(x, y)
                                            Case "lens"
                                                xmlr.Read()
                                                If xmlr.Value.ToLower = "diverging" Then
                                                    Do Until xmlr.NodeType = XmlNodeType.Element And xmlr.Name = "parameters"
                                                        xmlr.Read()
                                                    Loop
                                                    Dim fov, aspect, near, far, freq, phase As Single
                                                    Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "parameters"
                                                        xmlr.Read()
                                                        If xmlr.NodeType = XmlNodeType.Element Then
                                                            Select Case xmlr.Name
                                                                Case "fov"
                                                                    xmlr.Read()
                                                                    fov = xmlr.ReadContentAsFloat
                                                                Case "aspectratio"
                                                                    xmlr.Read()
                                                                    aspect = xmlr.ReadContentAsFloat
                                                                Case "near"
                                                                    xmlr.Read()
                                                                    near = xmlr.ReadContentAsFloat
                                                                Case "far"
                                                                    xmlr.Read()
                                                                    far = xmlr.ReadContentAsFloat
                                                                Case "frequency"
                                                                    xmlr.Read()
                                                                    freq = xmlr.ReadContentAsFloat
                                                                Case "phaseshift"
                                                                    xmlr.Read()
                                                                    phase = xmlr.ReadContentAsFloat
                                                            End Select
                                                        End If
                                                    Loop
                                                    OpenGLColor.oSetCameraGratingFrequency(freq, phase)
                                                    CameraSettings.oSetCameraParameters(fov, aspect, near, far)
                                                ElseIf xmlr.Value.ToLower = "telecentric" Then
                                                    Do Until xmlr.NodeType = XmlNodeType.Element And xmlr.Name = "parameters"
                                                        xmlr.Read()
                                                    Loop
                                                    Dim width, near, far, freq, phase As Single
                                                    Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "parameters"
                                                        xmlr.Read()
                                                        If xmlr.NodeType = XmlNodeType.Element Then
                                                            Select Case xmlr.Name
                                                                Case "width"
                                                                    xmlr.Read()
                                                                    width = xmlr.ReadContentAsFloat
                                                                Case "near"
                                                                    xmlr.Read()
                                                                    near = xmlr.ReadContentAsFloat
                                                                Case "far"
                                                                    xmlr.Read()
                                                                    far = xmlr.ReadContentAsFloat
                                                                Case "frequency"
                                                                    xmlr.Read()
                                                                    freq = xmlr.ReadContentAsFloat
                                                                Case "phaseshift"
                                                                    xmlr.Read()
                                                                    phase = xmlr.ReadContentAsFloat
                                                            End Select
                                                        End If
                                                    Loop
                                                    OpenGLColor.oSetCameraGratingFrequency(freq, phase)
                                                    OpenGLMain.oSetOrthoParameters(-width / 2, width / 2, -width / (4 / 3 * 2), width / (4 / 3 * 2), near, far)
                                                End If
                                            Case "grating"
                                                Dim x1, y1, z1, x2, y2, z2 As Single
                                                Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "grating"
                                                    xmlr.Read()
                                                    If xmlr.NodeType = XmlNodeType.Element Then
                                                        Select Case xmlr.Name
                                                            Case "x1"
                                                                xmlr.Read()
                                                                x1 = xmlr.ReadContentAsFloat
                                                            Case "y1"
                                                                xmlr.Read()
                                                                y1 = xmlr.ReadContentAsFloat
                                                            Case "z1"
                                                                xmlr.Read()
                                                                z1 = xmlr.ReadContentAsFloat
                                                            Case "x2"
                                                                xmlr.Read()
                                                                x2 = xmlr.ReadContentAsFloat
                                                            Case "y2"
                                                                xmlr.Read()
                                                                y2 = xmlr.ReadContentAsFloat
                                                            Case "z2"
                                                                xmlr.Read()
                                                                z2 = xmlr.ReadContentAsFloat
                                                        End Select
                                                    End If
                                                Loop
                                                OpenGLColor.oSetCameraGratingParameters(x1, y1, z1, x2, y2, z2)
                                        End Select
                                    Loop
                                Case "measurement"
                                    intNumberMeasurements += 1
                                    ReDim Preserve Measurements(intNumberMeasurements)
                                    Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "measurement"
                                        xmlr.Read()
                                        If xmlr.NodeType = XmlNodeType.Element Then
                                            Select Case xmlr.Name.ToLower
                                                Case "type"
                                                    xmlr.Read()
                                                    If xmlr.Value.ToLower = "moire" Then
                                                        Measurements(intNumberMeasurements).Type = MeasurementType.MOIRE

                                                        Measurements(intNumberMeasurements).intListArray = {1, 2}

                                                        Do Until xmlr.NodeType = XmlNodeType.Element
                                                            xmlr.Read()
                                                        Loop
                                                        If xmlr.NodeType = XmlNodeType.Element And xmlr.Name.ToLower = "mappath" Then
                                                            xmlr.Read()
                                                            Measurements(intNumberMeasurements).MapPath = xmlr.Value
                                                            'MsgBox(xmlr.Value)
                                                        End If

                                                        Do Until xmlr.NodeType = XmlNodeType.Element
                                                            xmlr.Read()
                                                        Loop
                                                        If xmlr.NodeType = XmlNodeType.Element And xmlr.Name.ToLower = "filename" Then
                                                            xmlr.Read()
                                                            Measurements(intNumberMeasurements).FileName = xmlr.Value
                                                        End If

                                                        Do Until xmlr.NodeType = XmlNodeType.Element And xmlr.Name = "beamer"
                                                            xmlr.Read()
                                                        Loop
                                                        Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "beamer"
                                                            xmlr.Read()
                                                            If xmlr.NodeType = XmlNodeType.Element Then
                                                                Select Case xmlr.Name.ToLower
                                                                    Case "blackout"
                                                                        'MsgBox("<blackout>")
                                                                        xmlr.Read()
                                                                        Measurements(intNumberMeasurements).NumberBlackoutImages = xmlr.ReadContentAsFloat
                                                                    Case "steps"
                                                                        'MsgBox("<steps>")
                                                                        xmlr.Read()
                                                                        Measurements(intNumberMeasurements).NumberSteps = xmlr.ReadContentAsFloat
                                                                    Case "grating"
                                                                        'MsgBox("<grating>")
                                                                        Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "grating"
                                                                            xmlr.Read()
                                                                            If xmlr.NodeType = XmlNodeType.Element Then
                                                                                Select Case xmlr.Name
                                                                                    Case "frequency"
                                                                                        'MsgBox("<frequency>")
                                                                                        Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "frequency"
                                                                                            xmlr.Read()
                                                                                            If xmlr.NodeType = XmlNodeType.Element Then
                                                                                                Select Case xmlr.Name
                                                                                                    Case "start"
                                                                                                        'MsgBox("<start>")
                                                                                                        xmlr.Read()
                                                                                                        Measurements(intNumberMeasurements).BeamerGratingStartFrequency = xmlr.ReadContentAsFloat
                                                                                                    Case "stop"
                                                                                                        'MsgBox("<stop>")
                                                                                                        xmlr.Read()
                                                                                                        Measurements(intNumberMeasurements).BeamerGratingStopFrequency = xmlr.ReadContentAsFloat
                                                                                                End Select
                                                                                            End If
                                                                                        Loop
                                                                                    Case "phase"
                                                                                        'MsgBox("<phase>")
                                                                                        Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "phase"
                                                                                            xmlr.Read()
                                                                                            If xmlr.NodeType = XmlNodeType.Element Then
                                                                                                Select Case xmlr.Name
                                                                                                    Case "start"
                                                                                                        'MsgBox("<start>")
                                                                                                        xmlr.Read()
                                                                                                        Measurements(intNumberMeasurements).BeamerGratingStartPhase = xmlr.ReadContentAsFloat
                                                                                                    Case "stop"
                                                                                                        'MsgBox("<stop>")
                                                                                                        xmlr.Read()
                                                                                                        Measurements(intNumberMeasurements).BeamerGratingStopPhase = xmlr.ReadContentAsFloat
                                                                                                End Select
                                                                                            End If
                                                                                        Loop
                                                                                End Select
                                                                            End If
                                                                        Loop
                                                                        'MsgBox("</grating>")
                                                                End Select
                                                            End If
                                                        Loop
                                                        'MsgBox("</beamer>")
                                                        Do Until xmlr.NodeType = XmlNodeType.Element And xmlr.Name = "camera"
                                                            xmlr.Read()
                                                        Loop
                                                        Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "camera"
                                                            'MsgBox("<camera>")
                                                            xmlr.Read()
                                                            Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "grating"
                                                                xmlr.Read()
                                                                If xmlr.NodeType = XmlNodeType.Element Then
                                                                    Select Case xmlr.Name.ToLower
                                                                        Case "frequency"
                                                                            'MsgBox("<frequency>")
                                                                            Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "frequency"
                                                                                xmlr.Read()
                                                                                If xmlr.NodeType = XmlNodeType.Element Then
                                                                                    Select Case xmlr.Name.ToLower
                                                                                        Case "start"
                                                                                            'MsgBox("<start>")
                                                                                            xmlr.Read()
                                                                                            Measurements(intNumberMeasurements).CameraGratingStartFrequency = xmlr.ReadContentAsFloat
                                                                                        Case "stop"
                                                                                            'MsgBox("<stop>")
                                                                                            xmlr.Read()
                                                                                            Measurements(intNumberMeasurements).CameraGratingStopFrequency = xmlr.ReadContentAsFloat
                                                                                    End Select
                                                                                End If
                                                                            Loop
                                                                        Case "phase"
                                                                            'MsgBox("<phase>")
                                                                            Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "phase"
                                                                                xmlr.Read()
                                                                                If xmlr.NodeType = XmlNodeType.Element Then
                                                                                    Select Case xmlr.Name.ToLower
                                                                                        Case "start"
                                                                                            'MsgBox("<start>")
                                                                                            xmlr.Read()
                                                                                            Measurements(intNumberMeasurements).CameraGratingStartPhase = xmlr.ReadContentAsFloat
                                                                                        Case "stop"
                                                                                            'MsgBox("<stop>")
                                                                                            xmlr.Read()
                                                                                            Measurements(intNumberMeasurements).CameraGratingStopPhase = xmlr.ReadContentAsFloat
                                                                                    End Select
                                                                                End If
                                                                            Loop
                                                                            'MsgBox("</phase>")
                                                                    End Select
                                                                End If
                                                            Loop
                                                            'MsgBox("</grating>")
                                                            xmlr.Read()
                                                            'MsgBox("</" & xmlr.Name & ">")
                                                        Loop
                                                    ElseIf xmlr.Value.ToLower = "fringe" Then
                                                        'MsgBox("fringe")
                                                        Measurements(intNumberMeasurements).Type = MeasurementType.FRINGE

                                                        OpenGLColor.oDeactivateCameraGrating()
                                                        OpenGLSettings.oSetBlending(False)
                                                        Measurements(intNumberMeasurements).intListArray = {1}

                                                        Do Until xmlr.NodeType = XmlNodeType.Element
                                                            xmlr.Read()
                                                        Loop
                                                        If xmlr.NodeType = XmlNodeType.Element And xmlr.Name.ToLower = "mappath" Then
                                                            xmlr.Read()
                                                            Measurements(intNumberMeasurements).MapPath = xmlr.Value
                                                            'MsgBox(xmlr.Value)
                                                        End If

                                                        Do Until xmlr.NodeType = XmlNodeType.Element
                                                            xmlr.Read()
                                                        Loop
                                                        If xmlr.NodeType = XmlNodeType.Element And xmlr.Name.ToLower = "filename" Then
                                                            xmlr.Read()
                                                            Measurements(intNumberMeasurements).FileName = xmlr.Value
                                                        End If

                                                        Do Until xmlr.NodeType = XmlNodeType.Element And xmlr.Name = "beamer"
                                                            xmlr.Read()
                                                        Loop
                                                        'MsgBox("<beamer>")
                                                        Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "beamer"
                                                            xmlr.Read()
                                                            If xmlr.NodeType = XmlNodeType.Element Then
                                                                Select Case xmlr.Name.ToLower
                                                                    Case "blackout"
                                                                        'MsgBox("<blackout>")
                                                                        xmlr.Read()
                                                                        Measurements(intNumberMeasurements).NumberBlackoutImages = xmlr.ReadContentAsFloat
                                                                    Case "steps"
                                                                        'MsgBox("<steps>")
                                                                        xmlr.Read()
                                                                        Measurements(intNumberMeasurements).NumberSteps = xmlr.ReadContentAsFloat
                                                                    Case "grating"
                                                                        'MsgBox("<grating>")
                                                                        Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "grating"
                                                                            xmlr.Read()
                                                                            If xmlr.NodeType = XmlNodeType.Element Then
                                                                                Select Case xmlr.Name.ToLower
                                                                                    Case "frequency"
                                                                                        'MsgBox("<frequency>")
                                                                                        Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "frequency"
                                                                                            xmlr.Read()
                                                                                            If xmlr.NodeType = XmlNodeType.Element Then
                                                                                                Select Case xmlr.Name.ToLower
                                                                                                    Case "start"
                                                                                                        'MsgBox("<start>")
                                                                                                        xmlr.Read()
                                                                                                        Measurements(intNumberMeasurements).BeamerGratingStartFrequency = xmlr.ReadContentAsFloat
                                                                                                    Case "stop"
                                                                                                        'MsgBox("<stop>")
                                                                                                        xmlr.Read()
                                                                                                        Measurements(intNumberMeasurements).BeamerGratingStopFrequency = xmlr.ReadContentAsFloat
                                                                                                End Select
                                                                                            End If
                                                                                        Loop
                                                                                    Case "phase"
                                                                                        'MsgBox("<phase>")
                                                                                        Do Until xmlr.NodeType = XmlNodeType.EndElement And xmlr.Name = "phase"
                                                                                            xmlr.Read()
                                                                                            If xmlr.NodeType = XmlNodeType.Element Then
                                                                                                Select Case xmlr.Name.ToLower
                                                                                                    Case "start"
                                                                                                        'MsgBox("<start>")
                                                                                                        xmlr.Read()
                                                                                                        Measurements(intNumberMeasurements).BeamerGratingStartPhase = xmlr.ReadContentAsFloat
                                                                                                    Case "stop"
                                                                                                        'MsgBox("<stop>")
                                                                                                        xmlr.Read()
                                                                                                        Measurements(intNumberMeasurements).BeamerGratingStopPhase = xmlr.ReadContentAsFloat
                                                                                                End Select
                                                                                            End If
                                                                                        Loop
                                                                                        'MsgBox("</phase>")
                                                                                End Select
                                                                            End If
                                                                        Loop
                                                                        'MsgBox("</grating>")
                                                                End Select
                                                            End If
                                                        Loop
                                                        'MsgBox("</beamer>")
                                                    Else
                                                        MsgBox("Wrong measurement type specified (moire/fringe)")
                                                    End If
                                            End Select
                                        End If
                                    Loop
                                    'MsgBox("</measurement>")
                            End Select
                        Case XmlNodeType.Text
                            Exit Select
                        Case XmlNodeType.EndElement
                            Exit Select
                    End Select
                End While
                Return Measurements
            Catch ex As Exception
                MsgBox(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Shared Function oGetHeader() As String
            Return strHeader
        End Function
        Public Shared Function oGetDescription() As String
            Return strDescription
        End Function
        Public Shared Function oGetDate() As Date
            Return datDate
        End Function
        Public Shared Function oGetModelPath() As String
            Dim strModelPath As String = ""
            Try
                Dim oReader As New StreamReader(IO.Directory.GetCurrentDirectory() & "\ModelPath.fdsf")
                strModelPath = oReader.ReadToEnd()
                oReader.Close()
            Catch ex As Exception
                MsgBox("Settings file does not exist", MsgBoxStyle.Information, "Error SET_001")
            End Try

            Return strModelPath
        End Function
        Public Shared Function oSetModelPath(ByVal strModelPath As String) As Boolean
            Try
                Dim oWriter As New StreamWriter(IO.Directory.GetCurrentDirectory() & "\ModelPath.fdsf")
                oWriter.Write(strModelPath)
                oWriter.Close()
                Return True
            Catch ex As Exception
                MsgBox("Settings file write error", MsgBoxStyle.Information, "Error SET_002")
                Return False
            End Try
        End Function
        Public Shared Function oGetFileName(ByVal FilePath As String) As String
            Dim lst() As String = FilePath.Split("\")
            If UBound(lst) = 0 Then
                lst = FilePath.Split("/")
            End If
            Return lst(UBound(lst))
        End Function
        Public Shared Function oGetMapPath(ByVal FilePath As String) As String
            Return FilePath.Substring(0, FilePath.Length - oGetFileName(FilePath).Length)
        End Function
    End Class
    Partial Class Forms
        Public Shared Function oSetProjectionScreenParameters(ByVal ScreenNumber As Integer, ByRef ProjectionScreen As Form) As Form
            Dim ScreenWidth As Integer
            Dim ScreenHeight As Integer
            ProjectionScreen.FormBorderStyle = FormBorderStyle.None
            Try
                If ScreenNumber = 1 Then
                    oExtractScreenData(ScreenWidth, ScreenHeight, 0)
                    If ScreenHeight = 0 And ScreenWidth = 0 Then
                        MsgBox("The screen size could not be detected!", MsgBoxStyle.Exclamation, "Error")
                    Else
                        ProjectionScreen.StartPosition = FormStartPosition.Manual
                        Dim loc As New DR.Point(Screen.AllScreens(0).WorkingArea.X, Screen.AllScreens(0).WorkingArea.Y)
                        ProjectionScreen.Location = loc
                        ProjectionScreen.WindowState = FormWindowState.Maximized
                        ' ProjectionScreen.Show()
                        Application.DoEvents()
                    End If
                    Return ProjectionScreen
                ElseIf ScreenNumber = 2 Then
                    If oCheckSecondScreenAvailability() = True Then
                        oExtractScreenData(ScreenWidth, ScreenHeight, 1)
                        If ScreenHeight = 0 And ScreenWidth = 0 Then
                            MsgBox("No second screen or projector found!", MsgBoxStyle.Exclamation, "Error")
                        Else
                            ProjectionScreen.StartPosition = FormStartPosition.Manual
                            Dim loc As New DR.Point(Screen.AllScreens(1).WorkingArea.X, Screen.AllScreens(1).WorkingArea.Y)
                            ProjectionScreen.Location = loc
                            ProjectionScreen.WindowState = FormWindowState.Maximized
                            ' ProjectionScreen.Show()
                            Application.DoEvents()
                        End If
                        Return ProjectionScreen
                    Else
                        MsgBox("No second screen or projector found!", MsgBoxStyle.Exclamation, "Error")
                        Return Nothing
                    End If
                Else
                    MsgBox("Only 2 screens are supported (1, 2)")
                    Return Nothing
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Shared Function oCheckSecondScreenAvailability() As Boolean
            Dim blnFound As Boolean = False

            Dim scherm() As Screen
            scherm = Screen.AllScreens()
            If UBound(scherm) >= 1 Then
                blnFound = True
            End If
            'Me.Location = scherm.Bounds.Location ' + New Point(100, 0)
            'Me.WindowState = FormWindowState.Maximized

            Return blnFound
        End Function
        Public Shared Function oExtractScreenData(ByRef Width As Integer, ByRef Height As Integer, ByVal ScreenNumber As Integer) As Boolean
            Dim blnSuccess As Boolean = False
            Try
                Dim Screen As Screen = Screen.AllScreens(ScreenNumber)
                Width = Screen.WorkingArea.Width
                Height = Screen.WorkingArea.Height
                Return blnSuccess
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try

            'Dim blnSuccess As Boolean = False

            'If CheckSecondScreenAvailability() = False Then
            '    MsgBox("No second screen or projector found!", MsgBoxStyle.Exclamation, "Error")
            '    Return False
            '    Exit Function
            'Else
            '    Dim Screen As Screen = Screen.AllScreens(ScreenNumber)
            '    Width = Screen.WorkingArea.Width
            '    Height = Screen.WorkingArea.Height
            'End If

            'Return blnSuccess
        End Function
    End Class
End Class
