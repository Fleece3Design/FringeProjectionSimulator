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
Imports System.Deployment.Application
Imports Fringe_Projection_Simulator_2013.Form1
Public NotInheritable Class frmAbout
    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = GetVersion()
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        'Me.TextBoxDescription.Text = My.Application.Info.Description
        Me.TextBoxDescription.Text = "License information" & vbCrLf & "-------------------------------" & vbCrLf
    End Sub
    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub
    Public Shared Function GetVersion() As String
        Dim ourVersion As String = String.Empty
        'if running the deployed application, you can get the version
        '  from the ApplicationDeployment information. If you try
        '  to access this when you are running in Visual Studio, it will not work.
        If System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed Then
            ourVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
        Else
            Dim assemblyInfo As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            If assemblyInfo IsNot Nothing Then
                ourVersion = assemblyInfo.GetName().Version.ToString()
            End If
        End If
        Return ourVersion
    End Function
End Class
