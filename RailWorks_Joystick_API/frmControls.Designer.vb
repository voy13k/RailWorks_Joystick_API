﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmControls
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
Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
Me.ControlsGUI = New RailWorks_Joystick_API.ucControls()
Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
Me.SuspendLayout()
'
'ElementHost1
'
Me.ElementHost1.Location = New System.Drawing.Point(0, 0)
Me.ElementHost1.Name = "ElementHost1"
Me.ElementHost1.Size = New System.Drawing.Size(674, 500)
Me.ElementHost1.TabIndex = 0
Me.ElementHost1.Text = "ElementHost1"
Me.ElementHost1.Child = Me.ControlsGUI
'
'Timer1
'
'
'frmControls
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(673, 500)
Me.Controls.Add(Me.ElementHost1)
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
Me.Name = "frmControls"
Me.Text = "Controls"
Me.ResumeLayout(False)

End Sub
    Friend WithEvents ElementHost1 As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents ControlsGUI As RailWorks_Joystick_API.ucControls
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
