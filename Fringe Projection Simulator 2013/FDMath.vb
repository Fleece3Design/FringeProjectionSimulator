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
Public Class FDMath
#Region "Declarations"
    Public NullVector As Vector
    Public Const SMALL_NUM = 0.00000001
    Public Const SMALL_TRESH = 0.000005
    Public Structure Point
        Private _X As Single
        Private _Y As Single
        Private _Z As Single
        Public Sub New(ByVal X As Single, ByVal Y As Single, ByVal Z As Single)
            _X = X
            _Y = Y
            _Z = Z
        End Sub
        Public Sub New(ByVal P() As Single)
            If UBound(P) <> 2 Then
                MsgBox("The array must agree the P(2) dimensions!")
                Exit Sub
            Else
                _X = P(0)
                _Y = P(1)
                _Z = P(2)
            End If
        End Sub
        Public Overrides Function ToString() As String
            Return [String].Format("X={0}    Y={1}    Z={2}", _X, _Y, _Z)
        End Function
        Public Property X As Single
            Get
                Return _X
            End Get
            Set(value As Single)
                _X = value
            End Set
        End Property
        Public Property Y As Single
            Get
                Return _Y
            End Get
            Set(value As Single)
                _Y = value
            End Set
        End Property
        Public Property Z As Single
            Get
                Return _Z
            End Get
            Set(value As Single)
                _Z = value
            End Set
        End Property
        Public Shared Operator +(ByVal P1 As Point, ByVal P2 As Point) As Point
            Dim Result As Point
            Result.X = P1.X + P2.X
            Result.Y = P1.Y + P2.Y
            Result.Z = P1.Z + P2.Z
            Return Result
        End Operator
        Public Shared Operator +(ByVal P As Point, ByVal V As Vector) As Point
            Dim result As Point
            result.X = P.X + V.X
            result.Y = P.Y + V.Y
            result.Z = P.Z + V.Z
            Return result
        End Operator
        Public Shared Operator -(ByVal P1 As Point, ByVal P2 As Point) As Vector
            Dim Result As Vector
            Result.X = P1.X - P2.X
            Result.Y = P1.Y - P2.Y
            Result.Z = P1.Z - P2.Z
            Return Result
        End Operator
        Public Shared Operator -(ByVal P As Point, ByVal V As Vector) As Point
            Dim result As Point
            result.X = P.X - V.X
            result.Y = P.Y - V.Y
            result.Z = P.Z - V.Z
            Return result
        End Operator
    End Structure
    Public Structure Point2
        Private _X As Single
        Private _Y As Single
        Public Sub New(ByVal X As Single, ByVal Y As Single)
            _X = X
            _Y = Y
        End Sub
        Public Sub New(ByVal P() As Single)
            If UBound(P) <> 1 Then
                MsgBox("The array must agree the P(1) dimensions!")
                Exit Sub
            Else
                _X = P(0)
                _Y = P(1)
            End If
        End Sub
        Public Overrides Function ToString() As String
            Return [String].Format("X={0}    Y={1}", _X, _Y)
        End Function
        Public Property X As Single
            Get
                Return _X
            End Get
            Set(value As Single)
                _X = value
            End Set
        End Property
        Public Property Y As Single
            Get
                Return _Y
            End Get
            Set(value As Single)
                _Y = value
            End Set
        End Property
        Public Shared Operator +(ByVal P1 As Point2, ByVal P2 As Point2) As Point2
            Dim Result As Point2
            Result.X = P1.X + P2.X
            Result.Y = P1.Y + P2.Y
            Return Result
        End Operator
        Public Shared Operator +(ByVal P As Point2, ByVal V As Vector2) As Point2
            Dim result As Point2
            result.X = P.X + V.X
            result.Y = P.Y + V.Y
            Return result
        End Operator
        Public Shared Operator -(ByVal P1 As Point2, ByVal P2 As Point2) As Vector2
            Dim Result As Vector2
            Result.X = P1.X - P2.X
            Result.Y = P1.Y - P2.Y
            Return Result
        End Operator
    End Structure
    Public Structure Vector
        Private _X As Single
        Private _Y As Single
        Private _Z As Single
        Public Sub New(ByVal X As Single, ByVal Y As Single, ByVal Z As Single)
            _X = X
            _Y = Y
            _Z = Z
        End Sub
        Public Sub New(ByVal V() As Single)
            If UBound(V) <> 2 Then
                MsgBox("The array must agree the P(2) dimensions!")
                Exit Sub
            Else
                _X = V(0)
                _Y = V(1)
                _Z = V(2)
            End If
        End Sub
        Public Overrides Function ToString() As String
            Return [String].Format("X={0}    Y={1}", _X, _Y)
        End Function
        Public Property X As Single
            Get
                Return _X
            End Get
            Set(value As Single)
                _X = value
            End Set
        End Property
        Public Property Y As Single
            Get
                Return _Y
            End Get
            Set(value As Single)
                _Y = value
            End Set
        End Property
        Public Property Z As Single
            Get
                Return _Z
            End Get
            Set(value As Single)
                _Z = value
            End Set
        End Property
        Public Function Length() As Single
            Dim Len As Single
            Len = Math.Sqrt(_X ^ 2 + _Y ^ 2 + _Z ^ 2)
            Return Len
        End Function
        Public Shared Operator +(ByVal V1 As Vector, ByVal V2 As Vector) As Vector
            Dim Result As Vector
            Result.X = V1.X + V2.X
            Result.Y = V1.Y + V2.Y
            Result.Z = V1.Z + V2.Z
            Return Result
        End Operator
        Public Shared Operator -(ByVal V1 As Vector, ByVal V2 As Vector) As Vector
            Dim Result As Vector
            Result.X = V1.X - V2.X
            Result.Y = V1.Y - V2.Y
            Result.Z = V1.Z - V2.Z
            Return Result
        End Operator
        Public Shared Operator *(ByVal V As Vector, ByVal Value As Single) As Vector
            Dim result As Vector
            result.X = V.X * Value
            result.Y = V.Y * Value
            result.Z = V.Z * Value
            Return result
        End Operator
        Public Function Normalize() As Vector
            Dim a As Single
            Dim NormalizedVector As Vector
            a = Math.Sqrt(_X ^ 2 + _Y ^ 2 + _Z ^ 2)
            NormalizedVector.X = _X / a
            NormalizedVector.Y = _Y / a
            NormalizedVector.Z = _Z / a
            Return NormalizedVector
        End Function
    End Structure
    Public Structure Vector2
        Private _X As Single
        Private _Y As Single
        Public Sub New(ByVal X As Single, ByVal Y As Single)
            _X = X
            _Y = Y
        End Sub
        Public Sub New(ByVal V() As Single)
            If UBound(V) <> 1 Then
                MsgBox("The array must agree the P(1) dimensions!")
                Exit Sub
            Else
                _X = V(0)
                _Y = V(1)
            End If
        End Sub
        Public Overrides Function ToString() As String
            Return [String].Format("X={0}    Y={1}", _X, _Y)
        End Function
        Public Property X As Single
            Get
                Return _X
            End Get
            Set(value As Single)
                _X = value
            End Set
        End Property
        Public Property Y As Single
            Get
                Return _Y
            End Get
            Set(value As Single)
                _Y = value
            End Set
        End Property
        Public Shared Operator +(ByVal V1 As Vector2, ByVal V2 As Vector2) As Vector2
            Dim Result As Vector2
            Result.X = V1.X + V2.X
            Result.Y = V1.Y + V2.Y
            Return Result
        End Operator
        Public Shared Operator -(ByVal V1 As Vector2, ByVal V2 As Vector2) As Vector2
            Dim Result As Vector2
            Result.X = V1.X - V2.X
            Result.Y = V1.Y - V2.Y
            Return Result
        End Operator
        Public Shared Operator *(ByVal V As Vector2, ByVal Value As Single) As Vector2
            Dim result As Vector2
            result.X = V.X * Value
            result.Y = V.Y * Value
            Return result
        End Operator
        Public Function Normalize() As Vector
            Dim a As Single
            Dim NormalizedVector As Vector
            a = Math.Sqrt(_X ^ 2 + _Y ^ 2)
            NormalizedVector.X = _X / a
            NormalizedVector.Y = _Y / a
            Return NormalizedVector
        End Function
    End Structure
    Public Structure Matrix2
        Private _m11 As Single
        Private _m12 As Single
        Private _m21 As Single
        Private _m22 As Single
        Public Sub New(ByVal m11 As Single, ByVal m12 As Single, ByVal m21 As Single, ByVal m22 As Single)
            _m11 = m11
            _m12 = m12
            _m21 = m21
            _m22 = m22
        End Sub
        Public Sub New(ByVal m(,) As Single)
            If UBound(m, 1) <> 1 And UBound(m, 2) <> 1 Then
                MsgBox("Error: Matrix dimensions must agree 2x2", MsgBoxStyle.Critical)
                Exit Sub
            End If
            _m11 = m(0, 0)
            _m12 = m(0, 1)
            _m21 = m(1, 0)
            _m22 = m(1, 1)
        End Sub
        Public Overrides Function ToString() As String
            Dim Result As String

            Result = "| " & _m11 & " , " & _m12 & " |" & vbCrLf
            Result &= "| " & _m21 & " , " & _m22 & " |"

            Return Result
        End Function
        Public Property m11 As Single
            Get
                Return _m11
            End Get
            Set(value As Single)
                _m11 = value
            End Set
        End Property
        Public Property m12 As Single
            Get
                Return _m12
            End Get
            Set(value As Single)
                _m12 = value
            End Set
        End Property
        Public Property m21 As Single
            Get
                Return _m21
            End Get
            Set(value As Single)
                _m21 = value
            End Set
        End Property
        Public Property m22 As Single
            Get
                Return _m22
            End Get
            Set(value As Single)
                _m22 = value
            End Set
        End Property
        Public Shared Operator +(ByVal M1 As Matrix2, ByVal M2 As Matrix2) As Matrix2
            Dim Result As Matrix2
            Result.m11 = M1.m11 + M2.m11
            Result.m12 = M1.m12 + M2.m12
            Result.m21 = M1.m21 + M2.m21
            Result.m22 = M1.m22 + M2.m22
            Return Result
        End Operator
        Public Shared Operator -(ByVal M1 As Matrix2, ByVal M2 As Matrix2) As Matrix2
            Dim Result As Matrix2
            Result.m11 = M1.m11 - M2.m11
            Result.m12 = M1.m12 - M2.m12
            Result.m21 = M1.m21 - M2.m21
            Result.m22 = M1.m22 - M2.m22
            Return Result
        End Operator
        Public Shared Operator *(ByVal M1 As Matrix2, ByVal M2 As Matrix2) As Matrix2
            Dim Result As Matrix2
            Result.m11 = M1.m11 * M2.m11 + M1.m12 * M2.m21
            Result.m12 = M1.m11 * M2.m12 + M1.m12 * M2.m22

            Result.m21 = M1.m21 * M2.m11 + M1.m22 * M2.m21
            Result.m22 = M1.m21 * M2.m12 + M1.m22 * M2.m22
            Return Result
        End Operator
        Public Shared Operator *(ByVal M As Matrix2, ByVal P As Point2) As Point2
            Dim Result As Point2

            Result.X = M.m11 * P.X + M.m12 * P.Y
            Result.Y = M.m21 * P.X + M.m22 * P.Y

            Return Result
        End Operator
        Public Function Determinant() As Single
            Dim Det As Single

            Det = (_m11 * _m22) - (_m12 * _m21)

            Return Det
        End Function
        Public Function Inverse() As Matrix2
            Dim Inv As Matrix2
            Dim tmp As Single
            Dim M As New Matrix2(_m11, _m12, _m21, _m22)

            Try
                tmp = M.Determinant
                Inv.m11 = _m22 / tmp
                Inv.m12 = -_m12 / tmp
                Inv.m21 = -_m21 / tmp
                Inv.m22 = _m11 / tmp
            Catch ex As Exception
                MsgBox("Matrix could not be inverted")
            End Try

            Return Inv
        End Function
    End Structure
    Public Structure Matrix3
        Private _m11 As Single
        Private _m12 As Single
        Private _m13 As Single
        Private _m21 As Single
        Private _m22 As Single
        Private _m23 As Single
        Private _m31 As Single
        Private _m32 As Single
        Private _m33 As Single
        Public Sub New(ByVal m11 As Single, ByVal m12 As Single, ByVal m13 As Single, ByVal m21 As Single, ByVal m22 As Single, ByVal m23 As Single, ByVal m31 As Single, ByVal m32 As Single, ByVal m33 As Single)
            _m11 = m11
            _m12 = m12
            _m13 = m13
            _m21 = m21
            _m22 = m22
            _m23 = m23
            _m31 = m31
            _m32 = m32
            _m33 = m33
        End Sub
        Public Sub New(ByVal m(,) As Single)
            If UBound(m, 1) <> 2 And UBound(m, 2) <> 2 Then
                MsgBox("Error: Matrix dimensions must agree 3x3", MsgBoxStyle.Critical)
                Exit Sub
            End If
            _m11 = m(0, 0)
            _m12 = m(0, 1)
            _m13 = m(0, 2)
            _m21 = m(1, 0)
            _m22 = m(1, 1)
            _m23 = m(1, 2)
            _m31 = m(2, 0)
            _m32 = m(2, 1)
            _m33 = m(2, 2)
        End Sub
        Public Overrides Function ToString() As String
            Dim Result As String

            Result = "| " & _m11 & " , " & _m12 & " , " & _m13 & " |" & vbCrLf
            Result &= "| " & _m21 & " , " & _m22 & " , " & _m23 & " |" & vbCrLf
            Result &= "| " & _m31 & " , " & _m32 & " , " & _m33 & " |"

            Return Result
        End Function
        Public Property m11 As Single
            Get
                Return _m11
            End Get
            Set(value As Single)
                _m11 = value
            End Set
        End Property
        Public Property m12 As Single
            Get
                Return _m12
            End Get
            Set(value As Single)
                _m12 = value
            End Set
        End Property
        Public Property m13 As Single
            Get
                Return _m13
            End Get
            Set(value As Single)
                _m13 = value
            End Set
        End Property
        Public Property m21 As Single
            Get
                Return _m21
            End Get
            Set(value As Single)
                _m21 = value
            End Set
        End Property
        Public Property m22 As Single
            Get
                Return _m22
            End Get
            Set(value As Single)
                _m22 = value
            End Set
        End Property
        Public Property m23 As Single
            Get
                Return _m23
            End Get
            Set(value As Single)
                _m23 = value
            End Set
        End Property
        Public Property m31 As Single
            Get
                Return _m31
            End Get
            Set(value As Single)
                _m31 = value
            End Set
        End Property
        Public Property m32 As Single
            Get
                Return _m32
            End Get
            Set(value As Single)
                _m32 = value
            End Set
        End Property
        Public Property m33 As Single
            Get
                Return _m33
            End Get
            Set(value As Single)
                _m33 = value
            End Set
        End Property
        Public Shared Operator +(ByVal M1 As Matrix3, ByVal M2 As Matrix3) As Matrix3
            Dim Result As Matrix3
            Result.m11 = M1.m11 + M2.m11
            Result.m12 = M1.m12 + M2.m12
            Result.m13 = M1.m13 + M2.m13
            Result.m21 = M1.m21 + M2.m21
            Result.m22 = M1.m22 + M2.m22
            Result.m23 = M1.m23 + M2.m23
            Result.m31 = M1.m31 + M2.m31
            Result.m32 = M1.m32 + M2.m32
            Result.m33 = M1.m33 + M2.m33
            Return Result
        End Operator
        Public Shared Operator -(ByVal M1 As Matrix3, ByVal M2 As Matrix3) As Matrix3
            Dim Result As Matrix3
            Result.m11 = M1.m11 - M2.m11
            Result.m12 = M1.m12 - M2.m12
            Result.m13 = M1.m13 - M2.m13
            Result.m21 = M1.m21 - M2.m21
            Result.m22 = M1.m22 - M2.m22
            Result.m23 = M1.m23 - M2.m23
            Result.m31 = M1.m31 - M2.m31
            Result.m32 = M1.m32 - M2.m32
            Result.m33 = M1.m33 - M2.m33
            Return Result
        End Operator
        Public Shared Operator *(ByVal M1 As Matrix3, ByVal M2 As Matrix3) As Matrix3
            Dim Result As Matrix3

            Result.m11 = M1.m11 * M2.m11 + M1.m12 * M2.m21 + M1.m13 * M2.m31
            Result.m12 = M1.m11 * M2.m12 + M1.m12 * M2.m22 + M1.m13 * M2.m32
            Result.m13 = M1.m11 * M2.m13 + M1.m12 * M2.m23 + M1.m13 * M2.m33

            Result.m21 = M1.m21 * M2.m11 + M1.m22 * M2.m21 + M1.m23 * M2.m31
            Result.m22 = M1.m21 * M2.m12 + M1.m22 * M2.m22 + M1.m23 * M2.m32
            Result.m23 = M1.m21 * M2.m13 + M1.m22 * M2.m23 + M1.m23 * M2.m33

            Result.m31 = M1.m31 * M2.m11 + M1.m32 * M2.m21 + M1.m33 * M2.m31
            Result.m32 = M1.m31 * M2.m12 + M1.m32 * M2.m22 + M1.m33 * M2.m32
            Result.m33 = M1.m31 * M2.m13 + M1.m32 * M2.m23 + M1.m33 * M2.m33

            Return Result
        End Operator
        Public Shared Operator *(ByVal M As Matrix3, ByVal V As Vector) As Vector
            Dim Result As Vector

            Result.X = M.m11 * V.X + M.m12 * V.Y + M.m13 * V.Z
            Result.Y = M.m21 * V.X + M.m22 * V.Y + M.m23 * V.Z
            Result.Z = M.m31 * V.X + M.m32 * V.Y + M.m33 * V.Z

            Return Result
        End Operator
        Public Shared Operator *(ByVal M As Matrix3, ByVal P As Point) As Point
            Dim Result As Point

            Result.X = M.m11 * P.X + M.m12 * P.Y + M.m13 * P.Z
            Result.Y = M.m21 * P.X + M.m22 * P.Y + M.m23 * P.Z
            Result.Z = M.m31 * P.X + M.m32 * P.Y + M.m33 * P.Z

            Return Result
        End Operator
        Public Shared Operator /(ByVal M As Matrix3, ByVal Value As Single) As Matrix3
            Dim Result As Matrix3
            Result.m11 = M.m11 / Value
            Result.m12 = M.m12 / Value
            Result.m13 = M.m13 / Value
            Result.m21 = M.m21 / Value
            Result.m22 = M.m22 / Value
            Result.m23 = M.m23 / Value
            Result.m31 = M.m31 / Value
            Result.m32 = M.m32 / Value
            Result.m33 = M.m33 / Value
            Return Result
        End Operator
        Public Function Determinant() As Single
            Dim Det As Single
            Det = _m11 * _m22 * _m33 + _m12 * _m23 * _m31 + _m13 * _m21 * _m32 - _m13 * _m22 * _m31 - _m12 * _m21 * _m33 - _m11 * _m23 * _m32
            Return Det
        End Function
        Public Function Transpose() As Matrix3
            Dim ResultMatrix As Matrix3

            ResultMatrix.m11 = _m11
            ResultMatrix.m12 = _m21
            ResultMatrix.m13 = _m31
            ResultMatrix.m21 = _m12
            ResultMatrix.m22 = _m22
            ResultMatrix.m23 = _m32
            ResultMatrix.m31 = _m13
            ResultMatrix.m32 = _m23
            ResultMatrix.m33 = _m33

            Return ResultMatrix
        End Function
        Public Function Inverse() As Matrix3
            Dim tmp As Matrix3
            Dim M As New Matrix3(_m11, _m12, _m13, _m21, _m22, _m23, _m31, _m32, _m33)

            tmp.m11 = _m22 * _m33 - _m23 * _m32
            tmp.m12 = _m23 * _m31 - _m21 * _m33
            tmp.m13 = _m21 * _m32 - _m22 * _m31
            tmp.m21 = _m13 * _m32 - _m12 * _m33
            tmp.m22 = _m11 * _m33 - _m13 * _m31
            tmp.m23 = _m31 * _m12 - _m11 * _m32
            tmp.m31 = _m12 * _m23 - _m13 * _m22
            tmp.m32 = _m13 * _m21 - _m11 * _m23
            tmp.m33 = _m11 * _m22 - _m12 * _m21

            Dim res As Matrix3

            'res = oDivision(oTranspose(tmp), oDeterminant(M))
            res = tmp.Transpose / M.Determinant
            Return res
        End Function
    End Structure
    Public Structure Matrix4
        Private _m11 As Single
        Private _m12 As Single
        Private _m13 As Single
        Private _m14 As Single
        Private _m21 As Single
        Private _m22 As Single
        Private _m23 As Single
        Private _m24 As Single
        Private _m31 As Single
        Private _m32 As Single
        Private _m33 As Single
        Private _m34 As Single
        Private _m41 As Single
        Private _m42 As Single
        Private _m43 As Single
        Private _m44 As Single
        Public Sub New(ByVal m11 As Single, ByVal m12 As Single, ByVal m13 As Single, ByVal m14 As Single, ByVal m21 As Single, ByVal m22 As Single, ByVal m23 As Single, ByVal m24 As Single, ByVal m31 As Single, ByVal m32 As Single, ByVal m33 As Single, ByVal m34 As Single, ByVal m41 As Single, ByVal m42 As Single, ByVal m43 As Single, ByVal m44 As Single)
            _m11 = m11
            _m12 = m12
            _m13 = m13
            _m14 = m14
            _m21 = m21
            _m22 = m22
            _m23 = m23
            _m24 = m24
            _m31 = m31
            _m32 = m32
            _m33 = m33
            _m34 = m34
            _m41 = m41
            _m42 = m42
            _m43 = m43
            _m44 = m44
        End Sub
        Public Sub New(ByVal m(,) As Single)
            If UBound(m, 1) <> 3 And UBound(m, 2) <> 3 Then
                MsgBox("Error: Matrix dimensions must agree 4x4", MsgBoxStyle.Critical)
                Exit Sub
            End If
            _m11 = m(0, 0)
            _m12 = m(0, 1)
            _m13 = m(0, 2)
            _m14 = m(0, 3)
            _m21 = m(1, 0)
            _m22 = m(1, 1)
            _m23 = m(1, 2)
            _m24 = m(1, 3)
            _m31 = m(2, 0)
            _m32 = m(2, 1)
            _m33 = m(2, 2)
            _m34 = m(2, 3)
            _m41 = m(3, 0)
            _m42 = m(3, 1)
            _m43 = m(3, 2)
            _m44 = m(3, 3)
        End Sub
        Public Overrides Function ToString() As String
            Dim Result As String

            Result = "| " & _m11 & " , " & _m12 & " , " & _m13 & " , " & _m14 & " |" & vbCrLf
            Result &= "| " & _m21 & " , " & _m22 & " , " & _m23 & " , " & _m24 & " |" & vbCrLf
            Result &= "| " & _m31 & " , " & _m32 & " , " & _m33 & " , " & _m34 & " |" & vbCrLf
            Result &= "| " & _m41 & " , " & _m42 & " , " & _m43 & " , " & _m44 & " |"

            Return Result
        End Function
        Public Property m11 As Single
            Get
                Return _m11
            End Get
            Set(value As Single)
                _m11 = value
            End Set
        End Property
        Public Property m12 As Single
            Get
                Return _m12
            End Get
            Set(value As Single)
                _m12 = value
            End Set
        End Property
        Public Property m13 As Single
            Get
                Return _m13
            End Get
            Set(value As Single)
                _m13 = value
            End Set
        End Property
        Public Property m14 As Single
            Get
                Return _m14
            End Get
            Set(value As Single)
                _m14 = value
            End Set
        End Property
        Public Property m21 As Single
            Get
                Return _m21
            End Get
            Set(value As Single)
                _m21 = value
            End Set
        End Property
        Public Property m22 As Single
            Get
                Return _m22
            End Get
            Set(value As Single)
                _m22 = value
            End Set
        End Property
        Public Property m23 As Single
            Get
                Return _m23
            End Get
            Set(value As Single)
                _m23 = value
            End Set
        End Property
        Public Property m24 As Single
            Get
                Return _m24
            End Get
            Set(value As Single)
                _m24 = value
            End Set
        End Property
        Public Property m31 As Single
            Get
                Return _m31
            End Get
            Set(value As Single)
                _m31 = value
            End Set
        End Property
        Public Property m32 As Single
            Get
                Return _m32
            End Get
            Set(value As Single)
                _m32 = value
            End Set
        End Property
        Public Property m33 As Single
            Get
                Return _m33
            End Get
            Set(value As Single)
                _m33 = value
            End Set
        End Property
        Public Property m34 As Single
            Get
                Return _m34
            End Get
            Set(value As Single)
                _m34 = value
            End Set
        End Property
        Public Property m41 As Single
            Get
                Return _m41
            End Get
            Set(value As Single)
                _m41 = value
            End Set
        End Property
        Public Property m42 As Single
            Get
                Return _m42
            End Get
            Set(value As Single)
                _m42 = value
            End Set
        End Property
        Public Property m43 As Single
            Get
                Return _m43
            End Get
            Set(value As Single)
                _m43 = value
            End Set
        End Property
        Public Property m44 As Single
            Get
                Return _m44
            End Get
            Set(value As Single)
                _m44 = value
            End Set
        End Property
        Public Shared Operator +(ByVal M1 As Matrix4, ByVal M2 As Matrix4) As Matrix4
            Dim Result As Matrix4
            Result.m11 = M1.m11 + M2.m11
            Result.m12 = M1.m12 + M2.m12
            Result.m13 = M1.m13 + M2.m13
            Result.m14 = M1.m14 + M2.m14
            Result.m21 = M1.m21 + M2.m21
            Result.m22 = M1.m22 + M2.m22
            Result.m23 = M1.m23 + M2.m23
            Result.m24 = M1.m24 + M2.m24
            Result.m31 = M1.m31 + M2.m31
            Result.m32 = M1.m32 + M2.m32
            Result.m33 = M1.m33 + M2.m33
            Result.m34 = M1.m34 + M2.m34
            Result.m41 = M1.m41 + M2.m41
            Result.m42 = M1.m42 + M2.m42
            Result.m43 = M1.m43 + M2.m43
            Result.m44 = M1.m44 + M2.m44
            Return Result
        End Operator
        Public Shared Operator -(ByVal M1 As Matrix4, ByVal M2 As Matrix4) As Matrix4
            Dim Result As Matrix4
            Result.m11 = M1.m11 - M2.m11
            Result.m12 = M1.m12 - M2.m12
            Result.m13 = M1.m13 - M2.m13
            Result.m14 = M1.m14 - M2.m14
            Result.m21 = M1.m21 - M2.m21
            Result.m22 = M1.m22 - M2.m22
            Result.m23 = M1.m23 - M2.m23
            Result.m24 = M1.m24 - M2.m24
            Result.m31 = M1.m31 - M2.m31
            Result.m32 = M1.m32 - M2.m32
            Result.m33 = M1.m33 - M2.m33
            Result.m34 = M1.m34 - M2.m34
            Result.m41 = M1.m41 - M2.m41
            Result.m42 = M1.m42 - M2.m42
            Result.m43 = M1.m43 - M2.m43
            Result.m44 = M1.m44 - M2.m44
            Return Result
        End Operator
        Public Shared Operator *(ByVal M1 As Matrix4, ByVal M2 As Matrix4) As Matrix4
            Dim Result As Matrix4

            Result.m11 = M1.m11 * M2.m11 + M1.m12 * M2.m21 + M1.m13 * M2.m31 + M1.m14 * M2.m41
            Result.m12 = M1.m11 * M2.m12 + M1.m12 * M2.m22 + M1.m13 * M2.m32 + M1.m14 * M2.m42
            Result.m13 = M1.m11 * M2.m13 + M1.m12 * M2.m23 + M1.m13 * M2.m33 + M1.m14 * M2.m43
            Result.m14 = M1.m11 * M2.m14 + M1.m12 * M2.m24 + M1.m13 * M2.m34 + M1.m14 * M2.m44

            Result.m21 = M1.m21 * M2.m11 + M1.m22 * M2.m21 + M1.m23 * M2.m31 + M1.m24 * M2.m41
            Result.m22 = M1.m21 * M2.m12 + M1.m22 * M2.m22 + M1.m23 * M2.m32 + M1.m24 * M2.m42
            Result.m23 = M1.m21 * M2.m13 + M1.m22 * M2.m23 + M1.m23 * M2.m33 + M1.m24 * M2.m43
            Result.m24 = M1.m21 * M2.m14 + M1.m22 * M2.m24 + M1.m23 * M2.m34 + M1.m24 * M2.m44

            Result.m31 = M1.m31 * M2.m11 + M1.m32 * M2.m21 + M1.m33 * M2.m31 + M1.m34 * M2.m41
            Result.m32 = M1.m31 * M2.m12 + M1.m32 * M2.m22 + M1.m33 * M2.m32 + M1.m34 * M2.m42
            Result.m33 = M1.m31 * M2.m13 + M1.m32 * M2.m23 + M1.m33 * M2.m33 + M1.m34 * M2.m43
            Result.m34 = M1.m31 * M2.m14 + M1.m32 * M2.m24 + M1.m33 * M2.m34 + M1.m34 * M2.m44

            Result.m41 = M1.m41 * M2.m11 + M1.m42 * M2.m21 + M1.m43 * M2.m31 + M1.m44 * M2.m41
            Result.m42 = M1.m41 * M2.m12 + M1.m42 * M2.m22 + M1.m43 * M2.m32 + M1.m44 * M2.m42
            Result.m43 = M1.m41 * M2.m13 + M1.m42 * M2.m23 + M1.m43 * M2.m33 + M1.m44 * M2.m43
            Result.m44 = M1.m41 * M2.m14 + M1.m42 * M2.m24 + M1.m43 * M2.m34 + M1.m44 * M2.m44
            Return Result
        End Operator

    End Structure
#End Region
#Region "Functions"
    Public Shared Function oDot(ByVal V1 As Vector, ByVal V2 As Vector) As Single
        Dim DP As Single
        DP = V1.X * V2.X + V1.Y * V2.Y + V1.Z * V2.Z
        Return DP
    End Function
    Public Shared Function oCross(ByVal V1 As Vector, ByVal V2 As Vector) As Vector
        Dim CP As Vector
        CP.X = (V1.Y * V2.Z) - (V1.Z * V2.Y)
        CP.Y = -((V1.X * V2.Z) - (V1.Z * V2.X))
        CP.Z = (V1.X * V2.Y) - (V1.Y * V2.X)
        Return CP
    End Function
    Public Shared Function oAngle(ByVal V1 As Vector, ByVal V2 As Vector) As Single
        V1 = V1.Normalize
        V2 = V2.Normalize
        Return Math.Acos(oDot(V1, V2))
    End Function
    Public Shared Function oAngleXZ(ByVal V1 As Vector, ByVal V2 As Vector) As Single
        V1.Y = 0
        V2.Y = 0
        Return oAngle(V1, V2)
    End Function
    Public Shared Function oSetRotationMatrix2Z(ByVal Angle As Single) As Matrix2
        Dim RotMat As Matrix2

        RotMat.m11 = Math.Cos(Angle)
        RotMat.m12 = -Math.Sin(Angle)
        RotMat.m21 = Math.Sin(Angle)
        RotMat.m22 = Math.Cos(Angle)

        Return RotMat
    End Function
    Public Shared Function oSetRotationMatrix3Y(ByVal Angle As Single) As Matrix3
        Dim RotMat As Matrix3

        RotMat.m11 = Math.Cos(Angle)
        RotMat.m12 = 0
        RotMat.m13 = Math.Sin(Angle)
        RotMat.m21 = 0
        RotMat.m22 = 1
        RotMat.m23 = 0
        RotMat.m31 = -Math.Sin(Angle)
        RotMat.m32 = 0
        RotMat.m33 = Math.Cos(Angle)

        Return RotMat
    End Function
    Public Shared Function oMatrixMultiplication(ByVal L As Matrix3, ByVal R As Matrix3) As Matrix3
        Dim Result As Matrix3

        Result.m11 = L.m11 * R.m11 + L.m12 * R.m21 + L.m13 * R.m31
        Result.m12 = L.m11 * R.m12 + L.m12 * R.m22 + L.m13 * R.m32
        Result.m13 = L.m11 * R.m13 + L.m12 * R.m23 + L.m13 * R.m33

        Result.m21 = L.m21 * R.m11 + L.m22 * R.m21 + L.m23 * R.m31
        Result.m22 = L.m21 * R.m12 + L.m22 * R.m22 + L.m23 * R.m32
        Result.m23 = L.m21 * R.m13 + L.m22 * R.m23 + L.m23 * R.m33

        Result.m31 = L.m31 * R.m11 + L.m32 * R.m21 + L.m33 * R.m31
        Result.m32 = L.m31 * R.m12 + L.m32 * R.m22 + L.m33 * R.m32
        Result.m33 = L.m31 * R.m13 + L.m32 * R.m23 + L.m33 * R.m33

        Return Result
    End Function
    Public Shared Function oMatrixMultiplication(ByVal L As Matrix4, ByVal R As Matrix4) As Matrix4
        Dim Result As Matrix4

        Result.m11 = L.m11 * R.m11 + L.m12 * R.m21 + L.m13 * R.m31 + L.m14 * R.m41
        Result.m12 = L.m11 * R.m12 + L.m12 * R.m22 + L.m13 * R.m32 + L.m14 * R.m42
        Result.m13 = L.m11 * R.m13 + L.m12 * R.m23 + L.m13 * R.m33 + L.m14 * R.m43
        Result.m14 = L.m11 * R.m14 + L.m12 * R.m24 + L.m13 * R.m34 + L.m14 * R.m44

        Result.m21 = L.m21 * R.m11 + L.m22 * R.m21 + L.m23 * R.m31 + L.m24 * R.m41
        Result.m22 = L.m21 * R.m12 + L.m22 * R.m22 + L.m23 * R.m32 + L.m24 * R.m42
        Result.m23 = L.m21 * R.m13 + L.m22 * R.m23 + L.m23 * R.m33 + L.m24 * R.m43
        Result.m24 = L.m21 * R.m14 + L.m22 * R.m24 + L.m23 * R.m34 + L.m24 * R.m44

        Result.m31 = L.m31 * R.m11 + L.m32 * R.m21 + L.m33 * R.m31 + L.m34 * R.m41
        Result.m32 = L.m31 * R.m12 + L.m32 * R.m22 + L.m33 * R.m32 + L.m34 * R.m42
        Result.m33 = L.m31 * R.m13 + L.m32 * R.m23 + L.m33 * R.m33 + L.m34 * R.m43
        Result.m34 = L.m31 * R.m14 + L.m32 * R.m24 + L.m33 * R.m34 + L.m34 * R.m44

        Result.m41 = L.m41 * R.m11 + L.m42 * R.m21 + L.m43 * R.m31 + L.m44 * R.m41
        Result.m42 = L.m41 * R.m12 + L.m42 * R.m22 + L.m43 * R.m32 + L.m44 * R.m42
        Result.m43 = L.m41 * R.m13 + L.m42 * R.m23 + L.m43 * R.m33 + L.m44 * R.m43
        Result.m44 = L.m41 * R.m14 + L.m42 * R.m24 + L.m43 * R.m34 + L.m44 * R.m44

        Return Result
    End Function
    Public Shared Function oMatrixElementMultiplication(ByVal L As Matrix3, ByVal R As Matrix3) As Matrix3
        Dim Result As Matrix3

        Result.m11 = L.m11 * R.m11
        Result.m12 = L.m12 * R.m12
        Result.m13 = L.m13 * R.m13

        Result.m21 = L.m21 * R.m21
        Result.m22 = L.m22 * R.m22
        Result.m23 = L.m23 * R.m23

        Result.m31 = L.m31 * R.m31
        Result.m32 = L.m32 * R.m32
        Result.m33 = L.m33 * R.m33

        Return Result
    End Function
    Public Shared Function oMatrixElementMultiplication(ByVal L As Matrix4, ByVal R As Matrix4) As Matrix4
        Dim Result As Matrix4

        Result.m11 = L.m11 * R.m11
        Result.m12 = L.m12 * R.m12
        Result.m13 = L.m13 * R.m13
        Result.m14 = L.m14 * R.m14

        Result.m21 = L.m21 * R.m21
        Result.m22 = L.m22 * R.m22
        Result.m23 = L.m23 * R.m23
        Result.m24 = L.m24 * R.m24

        Result.m31 = L.m31 * R.m31
        Result.m32 = L.m32 * R.m32
        Result.m33 = L.m33 * R.m33
        Result.m34 = L.m34 * R.m34

        Result.m41 = L.m41 * R.m41
        Result.m42 = L.m42 * R.m42
        Result.m43 = L.m43 * R.m43
        Result.m44 = L.m44 * R.m44

        Return Result
    End Function
    Public Shared Function oIntersectRayTriangle(ByVal V0 As Point, ByVal V1 As Point, ByVal V2 As Point, ByVal RayP0 As Point, ByRef RayP1 As Point, ByRef Result As Point) As Integer
        Dim u, v, n As Vector
        Dim dir, w0, w As Vector
        Dim a, b, r As Single

        u = V1 - V0
        v = V2 - V0
        n = oCross(u, v)

        If oIsNullVector(n) Then
            Return -1
            Exit Function
        End If

        dir = RayP1 - RayP0
        w0 = RayP0 - V0
        a = -oDot(n, w0)
        b = oDot(n, dir)

        If Math.Abs(b) < SMALL_NUM Then
            If a = 0 Then
                Return 2
                Exit Function
            Else
                Return 0
                Exit Function
            End If
        End If

        r = a / b

        If r < 0.0 Then
            Return 0
            Exit Function
        End If

        Result = RayP0 + (dir * r)

        Dim uu, uv, vv, wu, wv, D As Single

        uu = oDot(u, u)
        uv = oDot(u, v)
        vv = oDot(v, v)
        w = Result - V0
        wu = oDot(w, u)
        wv = oDot(w, v)
        D = uv * uv - uu * vv

        Dim s, t As Single
        s = (uv * wv - vv * wu) / D
        If (s < 0.0 Or s > 1.0) Then
            Return 0
            Exit Function
        End If
        t = (uv * wu - uu * wv) / D
        If (t < 0.0 Or (s + t) > 1.0) Then
            Return 0
            Exit Function
        End If

        Return 1
    End Function
    Public Shared Function oIsNullVector(ByVal V As Vector) As Boolean
        If V.X = 0 And V.Y And V.Z = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function oPlaneLineIntersection(ByVal P1 As Point, ByVal P2 As Point, ByVal P3 As Point, ByVal L1 As Point, ByVal L2 As Point) As Point
        Dim P As Point
        Dim M, Inv As Matrix3
        Dim V As Vector

        M.m11 = L1.X - L2.X
        M.m12 = P2.X - P1.X
        M.m13 = P3.X - P1.X
        M.m21 = L1.Y - L2.Y
        M.m22 = P2.Y - P1.Y
        M.m23 = P3.Y - P1.Y
        M.m31 = L1.Z - L2.Z
        M.m32 = P2.Z - P1.Z
        M.m33 = P3.Z - P1.Z

        Inv = M.Inverse

        V.X = L1.X - P1.X
        V.Y = L1.Y - P1.Y
        V.Z = L1.Z - P1.Z

        Dim tuv As Vector
        tuv = Inv * V

        'P.X = L2.X + tuv.X * (L1.X - L2.X)
        'P.Y = L2.Y + tuv.X * (L1.Y - L2.Y)
        'P.Z = L2.Z + tuv.X * (L1.Z - L2.Z)

        P.X = L1.X + tuv.X * (L2.X - L1.X)
        P.Y = L1.Y + tuv.X * (L2.Y - L1.Y)
        P.Z = L1.Z + tuv.X * (L2.Z - L1.Z)

        If System.Math.Abs(P.X) < SMALL_TRESH Then
            P.X = 0
        End If
        If System.Math.Abs(P.Y) < SMALL_TRESH Then
            P.Y = 0
        End If
        If System.Math.Abs(P.Z) < SMALL_TRESH Then
            P.Z = 0
        End If

        Return P
    End Function
    Public Shared Function oFindRotationMatrixBetween2Vectors(ByVal V1 As Vector, ByVal V2 As Vector) As Matrix3
        V1 = V1.Normalize
        V2 = V2.Normalize

        Dim u As Vector
        u = oCross(V1, V2).Normalize

        Dim theta As Single
        theta = -Math.Acos(oDot(V1, V2) / (V1.Length * V2.Length))

        Dim c, s, t As Single
        c = Math.Cos(theta)
        s = Math.Sin(theta)
        t = 1 - Math.Cos(theta)

        Dim x, y, z As Single
        x = u.X
        y = u.Y
        z = u.Z

        Dim R As Matrix3

        R.m11 = t * x ^ 2 + c
        R.m12 = t * x * y + s * z
        R.m13 = t * x * z - s * y
        R.m21 = t * x * y - s * z
        R.m22 = t * y ^ 2 + c
        R.m23 = t * y * z + s * x
        R.m31 = t * x * z + s * y
        R.m32 = t * y * z - s * x
        R.m33 = t * z ^ 2 + c

        Dim Res As Vector
        Res = R * V1

        'If (V2.X - Res.X) < (1 / 100000) And (V2.Y - Res.Y) < (1 / 100000) And (V2.Z - Res.Z) < (1 / 100000) Then
        '    MsgBox("Correct")
        'Else
        '    MsgBox("Incorrect")
        'End If

        Return R
    End Function
    Public Shared Function oRound(ByVal p As Point, ByVal decimals As Integer) As Point
        Dim RoundedPoint As Point
        RoundedPoint.X = Math.Round(p.X, decimals)
        RoundedPoint.Y = Math.Round(p.Y, decimals)
        RoundedPoint.Z = Math.Round(p.Z, decimals)
        Return RoundedPoint
    End Function
#End Region
#Region "Pi Vincent"
    Public Shared Function oGetResult_in_Pi(ByVal Input As String, ByRef Output As String, ByRef ErrorDescription As String) As Boolean
        ' try to get the result as a fraction of Pi:
        Dim result As Double
        If oGetResult(Input, result, ErrorDescription) = True Then
            Dim negative As Boolean = False
            If result < 0 Then
                negative = True
            ElseIf result = 0 Then
                Output = CStr(0)
                Return True
            End If
            result = Math.Abs(result)
            Dim delta As Double = 0.000001
            Dim nominator As Integer = 0
            Dim denominator As Integer = 0
            Dim prefactor As Double = result / Math.PI
            For ii As Integer = 1 To 9999
                Dim [error] As Double = prefactor * ii - Math.Round(prefactor * ii)
                If Math.Abs([error]) < delta Then
                    nominator = CInt(Math.Round(prefactor * ii))
                    denominator = ii
                    Exit For
                End If
            Next
            If nominator <> 0 Then
                If denominator = 1 Then
                    If negative Then
                        'Return "-" & nominator.ToString() & " * Pi"
                        Output = "-" & nominator.ToString() & " * Pi"
                        Return True
                    Else
                        'Return nominator.ToString() & " * Pi"
                        Output = nominator.ToString() & " * Pi"
                        Return True
                    End If
                Else
                    If negative Then
                        'Return "-" & nominator.ToString() & " / " & denominator.ToString() & " * Pi"
                        Output = "-" & nominator.ToString() & " / " & denominator.ToString() & " * Pi"
                        Return True
                    Else
                        'Return nominator.ToString() & " / " & denominator.ToString() & " * Pi"
                        Output = nominator.ToString() & " / " & denominator.ToString() & " * Pi"
                        Return True
                    End If
                End If
            Else
                ' Return "(not writable as fraction of Pi)"
                ErrorDescription = "(not writable as fraction of Pi)"
                Return False
            End If
        Else
            Return False
        End If

    End Function
    'Public Shared Function oGetResult_in_Pi(ByVal input As String) As String
    '    ' try to get the result as a fraction of Pi:
    '    Dim result As Double = oGetResult(Input)
    '    Dim negative As Boolean = False
    '    If result < 0 Then
    '        negative = True
    '    End If
    '    result = Abs(result)
    '    Dim delta As Double = 0.000001
    '    Dim nominator As Integer = 0
    '    Dim denominator As Integer = 0
    '    Dim prefactor As Double = result / PI
    '    For ii As Integer = 1 To 9999
    '        Dim [error] As Double = prefactor * ii - Round(prefactor * ii)
    '        If Abs([error]) < delta Then
    '            nominator = CInt(Round(prefactor * ii))
    '            denominator = ii
    '            Exit For
    '        End If
    '    Next
    '    If nominator <> 0 Then
    '        If denominator = 1 Then
    '            If negative Then
    '                Return "-" & nominator.ToString() & " * Pi"
    '            Else
    '                Return nominator.ToString() & " * Pi"
    '            End If
    '        Else
    '            If negative Then
    '                Return "-" & nominator.ToString() & " / " & denominator.ToString() & " * Pi"
    '            Else
    '                Return nominator.ToString() & " / " & denominator.ToString() & " * Pi"
    '            End If
    '        End If
    '    Else
    '        Return "(not writable as fraction of Pi)"
    '    End If
    'End Function
    Public Shared Function oGetResult(ByVal input As String, ByRef output As Double, ByRef ErrorDescription As String) As Boolean
        Try
            If input = "0" Then
                output = 0
                Return True
            End If
        Catch ex As Exception

        End Try

        ' First Step: Remove spaces from string
        For ii As Integer = input.Length - 1 To 0 Step -1
            If input(ii) = " "c Then
                input = input.Remove(ii, 1)
            End If
        Next

        ' Second Step: replace Pi by its actual value
        input = oReplacePi(input)
        If input = "error" Then
            ''MsgBox("Error => Input formula in wrong format: " + Environment.NewLine & "Illegal use of Pi")
            ''Return 0.0
            ErrorDescription = "Error => Input formula in wrong format: " + Environment.NewLine & "Illegal use of Pi"
            Return False
        End If

        ' Third Step: check whether the input is valid
        Dim errorstring As String = oValidateInput(input)
        If errorstring <> "OK" Then
            ''MsgBox("Error => Input formula in wrong format: " + Environment.NewLine & errorstring)
            ''''txtExtra.Text = "NOT OK: " + errorstring;
            ''Return 0.0
            ErrorDescription = "Error => Input formula in wrong format: " + Environment.NewLine & errorstring
            Return False
        Else
            'txtExtra.Text = "OK!";
            ' Fourth Step: Calculate the result
            Dim result As Double = oCalculateResult(input)
            ''Return result
            If result = Nothing Then

                Return False
            Else
                output = result
                Return True
            End If
        End If
    End Function
    Private Shared Function oReplacePi(ByVal input As String) As String
        input = input.Replace("Pi", "pi")
        input = input.Replace("PI", "pi")
        input = input.Replace("pI", "pi")
        Dim finished As Boolean = False
        Dim lastindex As Integer = 0
        Dim index As Integer = -1
        While Not finished
            index = -1
            index = input.IndexOf("pi", lastindex)
            If index <> -1 Then
                If index <> 0 AndAlso oCharIsNumeric(input(index - 1)) Then
                    Return "error"
                End If
                ' check character begore "pi"
                If input.Length > index + 2 AndAlso oCharIsNumeric(input(index + 2)) Then
                    Return "error"
                End If
                ' check character after "pi"
                lastindex = index + 2
                ' move startingsearch point towards end
                If lastindex >= input.Length Then
                    finished = True
                End If
            Else
                finished = True
            End If
        End While
        ' replace pi:
        Return input.Replace("pi", Math.PI.ToString())
    End Function
    Private Shared Function oValidateInput(ByVal input As String) As String

        ' 0th step: check if input is not empty:
        If [String].IsNullOrEmpty(input) Then
            Return "Please give an input to calculate!"
        End If

        ' Firstly: check whether there aren't any unallowed characters:                
        '#Region "checking characters"
        For Each x As Char In input
            Select Case x
                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, _
                 "6"c, "7"c, "8"c, "9"c, "."c, ","c, _
                 "+"c, "-"c, "*"c, "/"c, "("c, ")"c


                    Exit Select
                Case Else
                    Return "Unallowed character encountered: '" & x.ToString() & "' (only use: 0123456789().,+-*/ and Pi )"
                    Exit Select
            End Select
        Next
        '#End Region

        ' Secondly: check whether the format is OK:
        '#Region "checking format"
        ' check first character:
        Dim first As Char = input(0)
        If first = "+"c OrElse first = "*"c OrElse first = "/"c OrElse first = ")"c Then
            Return "character '" & first.ToString() & "' not allowed as first character!"
        End If
        ' check other characters:
        For ii As Integer = 0 To input.Length - 2
            Dim char1 As Char = input(ii)
            Dim char2 As Char = input(ii + 1)
            Select Case char1
                Case ")"c
                    ' only +,-,*,/,) can be the next charachter:
                    If Not (char2 = "+"c OrElse char2 = "-"c OrElse char2 = "*"c OrElse char2 = "/"c OrElse char2 = ")"c) Then
                        Return "Unallowed sequence of characters: '" & char2.ToString() & "' not allowed to come after '" & char1.ToString() & "' ..."
                    End If
                    Exit Select
                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, _
                 "6"c, "7"c, "8"c, "9"c, "."c, ","c
                    ' only '(' is NOT allowed as the next character:
                    If char2 = "("c Then
                        Return "Unallowed sequence of characters: '" & char2.ToString() & "' not allowed to come after '" & char1.ToString() & "' ..."
                    End If
                    Exit Select
                Case "+"c, "-"c, "*"c, "/"c, "("c
                    ' the characters +,-,*,/,),.,, are NOT allowed as next characters:
                    If char2 = "+"c OrElse char2 = "-"c OrElse char2 = "*"c OrElse char2 = "/"c OrElse char2 = ")"c Then
                        If char1 = "("c AndAlso char2 = "-"c Then
                            Exit Select
                        End If
                        ' the only exception!!!!!
                        Return "Unallowed sequence of characters: '" & char2.ToString() & "' not allowed to come after '" & char1.ToString() & "' ..."
                    End If
                    Exit Select
                Case Else
                    Exit Select
            End Select
        Next
        '#End Region

        'Thirdly: check whether the amount of brackets is OK and whether they are in an allowable order:
        '#Region "checking brackets"
        Dim shouldNotBeNegative As Integer = 0
        For Each x As Char In input
            If x = "("c Then
                shouldNotBeNegative += 1
            ElseIf x = ")"c Then
                shouldNotBeNegative -= 1
            End If
            If shouldNotBeNegative < 0 Then
                ' brackets in bad order (more 'closed' then 'opened' at this point)...
                Return "Wrong order of brackets!"
            End If
        Next
        If shouldNotBeNegative <> 0 Then
            ' not an equal amount of '(' and ')'...
            Return "Wrong number of brackets!"
        End If
        '#End Region

        'Fourthly: check whether there are no numbers like "256,54,48":
        '#Region "checking numbers format"
        Dim index1 As Integer = 0
        Dim index2 As Integer = 0
        For ii As Integer = 0 To input.Length - 2
            If oCharIsNumeric(input(ii)) = False Then
                If oCharIsNumeric(input(ii + 1)) = True Then
                    ' the start of a number found!
                    index1 = ii + 1
                End If
            End If

            If oCharIsNumeric(input(ii)) = True Then
                If oCharIsNumeric(input(ii + 1)) = False Then
                    ' the end of a number found!
                    index2 = ii + 1
                    ' end found, so try to parse the found number!
                    Dim nr As String = input.Substring(index1, index2 - index1)
                    Dim d As Double = 0.0
                    If Not [Double].TryParse(nr, d) Then
                        Return """" & nr & """  is not a correct number format!"
                    End If
                End If
            End If
        Next
        '#End Region

        'return "OK" if we got this far:
        Return "OK"
    End Function
    Private Shared Function oCharIsNumeric(ByVal x As Char) As Boolean
        If x = "+"c OrElse x = "-"c OrElse x = "*"c OrElse x = "/"c OrElse x = "("c OrElse x = ")"c Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Shared Function oCalculateResult(ByVal inputstring As String) As Double
        Dim input As String = inputstring
        If [String].IsNullOrEmpty(input) Then
            Return 0.0
        End If

        ' 1) Remove any fully embracing brackets (which also belong together!!):
        input = oStripBrackets(input)

        ' 2.a) First search for and add up the highest level terms:
        Dim result As Double = 0.0
        ' to be returned in the end!
        Dim nrOfOpenBrackets As Integer = 0
        Dim indeces As New List(Of Integer)()
        For ii As Integer = 0 To input.Length - 1
            If input(ii) = "("c Then
                nrOfOpenBrackets += 1
            ElseIf input(ii) = ")"c Then
                nrOfOpenBrackets -= 1
            ElseIf (input(ii) = "+"c OrElse input(ii) = "-"c) AndAlso nrOfOpenBrackets = 0 Then
                ' only + and - outside brackets count (upper level terms)
                indeces.Add(ii)
            End If
        Next
        ' We have the indeces of + and - now, so now add all the terms:
        If indeces.Count <> 0 Then
            For ii As Integer = 0 To indeces.Count - 1
                If ii = 0 Then
                    ' First add term before this +/-:
                    result += oCalculateResult(input.Substring(0, indeces(0) - 0))
                    ' Then add the second term:
                    If indeces.Count = 1 Then
                        If input(indeces(0)) = "+"c Then
                            result += oCalculateResult(input.Substring(indeces(0) + 1, input.Length - indeces(0) - 1))
                        ElseIf input(indeces(0)) = "-"c Then
                            result -= oCalculateResult(input.Substring(indeces(0) + 1, input.Length - indeces(0) - 1))
                        Else
                            MsgBox("Error in calculation: no '+' of '-' detected while trying to add up!")
                        End If
                    Else
                        ' indeces.Count > 1
                        If input(indeces(0)) = "+"c Then
                            result += oCalculateResult(input.Substring(indeces(0) + 1, indeces(1) - indeces(0) - 1))
                        ElseIf input(indeces(0)) = "-"c Then
                            result -= oCalculateResult(input.Substring(indeces(0) + 1, indeces(1) - indeces(0) - 1))
                        Else
                            MsgBox("Error in calculation: no '+' of '-' detected while trying to add up!")
                        End If
                    End If
                ElseIf ii = indeces.Count - 1 Then
                    ' Last term (+ or -)
                    If input(indeces(ii)) = "+"c Then
                        result += oCalculateResult(input.Substring(indeces(ii) + 1, input.Length - indeces(ii) - 1))
                    ElseIf input(indeces(ii)) = "-"c Then
                        result -= oCalculateResult(input.Substring(indeces(ii) + 1, input.Length - indeces(ii) - 1))
                    Else
                        MsgBox("Error in calculation: no '+' of '-' detected while trying to add up!")
                    End If
                Else
                    ' not the first sign and not the last:
                    If input(indeces(ii)) = "+"c Then
                        result += oCalculateResult(input.Substring(indeces(ii) + 1, indeces(ii + 1) - indeces(ii) - 1))
                    ElseIf input(indeces(ii)) = "-"c Then
                        result -= oCalculateResult(input.Substring(indeces(ii) + 1, indeces(ii + 1) - indeces(ii) - 1))
                    Else
                        MsgBox("Error in calculation: no '+' of '-' detected while trying to add up!")
                    End If
                End If
            Next
            ' Now return the result:
            Return result
        End If

        ' 2.b) Secondly, when there is only one term: look for factors and muliply them:
        result = 1.0
        ' we'll multiply and divide now...
        nrOfOpenBrackets = 0
        indeces = New List(Of Integer)()
        For ii As Integer = 0 To input.Length - 1
            If input(ii) = "("c Then
                nrOfOpenBrackets += 1
            ElseIf input(ii) = ")"c Then
                nrOfOpenBrackets -= 1
            ElseIf (input(ii) = "*"c OrElse input(ii) = "/"c) AndAlso nrOfOpenBrackets = 0 Then
                ' only * and / outside brackets count (upper level factors)
                indeces.Add(ii)
            End If
        Next
        ' We have the indeces of * and / now, so now we'll muliply all these factors:
        If indeces.Count <> 0 Then
            For ii As Integer = 0 To indeces.Count - 1
                If ii = 0 Then
                    ' First multiply factor before the first * or /:
                    Dim firstfactor As String = input.Substring(0, indeces(0) - 0)
                    If [String].IsNullOrEmpty(firstfactor) Then
                        MsgBox("Error in multiplying first factor...")
                    Else
                        result *= oCalculateResult(firstfactor)
                    End If
                    ' Then multiply/divide by the second factor:
                    If indeces.Count = 1 Then
                        If input(indeces(0)) = "*"c Then
                            result *= oCalculateResult(input.Substring(indeces(0) + 1, input.Length - indeces(0) - 1))
                        ElseIf input(indeces(0)) = "/"c Then
                            result /= oCalculateResult(input.Substring(indeces(0) + 1, input.Length - indeces(0) - 1))
                        Else
                            MsgBox("Error in calculation: no '*' of '/' detected while trying to multiply!")
                        End If
                    Else
                        ' indeces.Count > 1
                        If input(indeces(0)) = "*"c Then
                            result *= oCalculateResult(input.Substring(indeces(0) + 1, indeces(1) - indeces(0) - 1))
                        ElseIf input(indeces(0)) = "/"c Then
                            result /= oCalculateResult(input.Substring(indeces(0) + 1, indeces(1) - indeces(0) - 1))
                        Else
                            MsgBox("Error in calculation: no '*' of '/' detected while trying to multiply!")
                        End If
                    End If
                ElseIf ii = indeces.Count - 1 Then
                    ' Last factor (* or /)
                    If input(indeces(ii)) = "*"c Then
                        result *= oCalculateResult(input.Substring(indeces(ii) + 1, input.Length - indeces(ii) - 1))
                    ElseIf input(indeces(ii)) = "/"c Then
                        result /= oCalculateResult(input.Substring(indeces(ii) + 1, input.Length - indeces(ii) - 1))
                    Else
                        MsgBox("Error in calculation: no '*' of '/' detected while trying to multiply!")
                    End If
                Else
                    ' not the first *or/ and not the last:
                    If input(indeces(ii)) = "*"c Then
                        result *= oCalculateResult(input.Substring(indeces(ii) + 1, indeces(ii + 1) - indeces(ii) - 1))
                    ElseIf input(indeces(ii)) = "/"c Then
                        result /= oCalculateResult(input.Substring(indeces(ii) + 1, indeces(ii + 1) - indeces(ii) - 1))
                    Else
                        MsgBox("Error in calculation: no '*' of '/' detected while trying to multiply!")
                    End If
                End If
            Next
            ' Now return the result:
            Return result
        End If

        ' 3) Now, there should be no signs left and the inputstring should be parsable into a double...
        result = 0.0
        If [Double].TryParse(input, result) Then
            Return result
        Else
            'MsgBox("Error: string should be parsable, but is not...")
            Return Nothing
        End If

        '------------------------------------------------
        ' backup
        'MsgBox("The value 0.0 is returned, but this is not the correct value...")
        Return Nothing
    End Function
    Private Shared Function oStripBrackets(ByVal input As String) As String
        ' Check if exterior brackets potentially have to be stripped: "(.....)"    (NOT when "(...)*(...)" !!!)
        If input(0) = "("c AndAlso input(input.Length - 1) = ")"c Then
            ' first and last are opening and closing brackets
            ' check if these exterior brackets belong together:
            Dim nrOfBracketsOpen As Integer = 0
            For ii As Integer = 0 To input.Length - 2
                If input(ii) = "("c Then
                    nrOfBracketsOpen += 1
                    ' always the case for ii=0 if we got this far!
                ElseIf input(ii) = ")"c Then
                    nrOfBracketsOpen -= 1
                End If
                ' if at any moment nrOfBracketsOpen becomes 0 again => exterior brackets do not belong together!!
                If nrOfBracketsOpen = 0 Then
                    Return input
                End If
            Next
            ' if we got this far, so without previously returning, we can call agin for possible further stripping of brackets:
            Return oStripBrackets(input.Substring(1, input.Length - 2))
        Else
            Return input
        End If
    End Function
#End Region
End Class
