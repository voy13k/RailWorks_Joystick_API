<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVigilanceSystem
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
Me.tmUpdateInfo = New System.Windows.Forms.Timer(Me.components)
Me.lstStatus = New System.Windows.Forms.ListBox()
Me.chkUseVigilanceSystem = New System.Windows.Forms.CheckBox()
Me.txtMinDelay = New System.Windows.Forms.TextBox()
Me.lblMinDelay = New System.Windows.Forms.Label()
Me.lblMaxDelay = New System.Windows.Forms.Label()
Me.txtMaxDelay = New System.Windows.Forms.TextBox()
Me.lblEarlyTriggerChance = New System.Windows.Forms.Label()
Me.txtEarlyTriggerChance = New System.Windows.Forms.TextBox()
Me.lblTrainSpeedFactor = New System.Windows.Forms.Label()
Me.txtTrainSpeedFactor = New System.Windows.Forms.TextBox()
Me.lblMinTrainSpeed = New System.Windows.Forms.Label()
Me.txtMinTrainSpeed = New System.Windows.Forms.TextBox()
Me.lblTimeout = New System.Windows.Forms.Label()
Me.txtTimeout = New System.Windows.Forms.TextBox()
Me.SuspendLayout()
'
'tmUpdateInfo
'
Me.tmUpdateInfo.Enabled = True
'
'lstStatus
'
Me.lstStatus.FormattingEnabled = True
Me.lstStatus.Location = New System.Drawing.Point(12, 12)
Me.lstStatus.Name = "lstStatus"
Me.lstStatus.Size = New System.Drawing.Size(256, 199)
Me.lstStatus.TabIndex = 0
'
'chkUseVigilanceSystem
'
Me.chkUseVigilanceSystem.AutoSize = True
Me.chkUseVigilanceSystem.Location = New System.Drawing.Point(275, 13)
Me.chkUseVigilanceSystem.Name = "chkUseVigilanceSystem"
Me.chkUseVigilanceSystem.Size = New System.Drawing.Size(149, 17)
Me.chkUseVigilanceSystem.TabIndex = 1
Me.chkUseVigilanceSystem.Text = "Use the vigilance system?"
Me.chkUseVigilanceSystem.UseVisualStyleBackColor = True
'
'txtMinDelay
'
Me.txtMinDelay.Location = New System.Drawing.Point(444, 36)
Me.txtMinDelay.Name = "txtMinDelay"
Me.txtMinDelay.Size = New System.Drawing.Size(112, 20)
Me.txtMinDelay.TabIndex = 2
'
'lblMinDelay
'
Me.lblMinDelay.AutoSize = True
Me.lblMinDelay.Location = New System.Drawing.Point(274, 39)
Me.lblMinDelay.Name = "lblMinDelay"
Me.lblMinDelay.Size = New System.Drawing.Size(142, 13)
Me.lblMinDelay.TabIndex = 3
Me.lblMinDelay.Text = "Minimum delay (Milliseconds)"
'
'lblMaxDelay
'
Me.lblMaxDelay.AutoSize = True
Me.lblMaxDelay.Location = New System.Drawing.Point(274, 65)
Me.lblMaxDelay.Name = "lblMaxDelay"
Me.lblMaxDelay.Size = New System.Drawing.Size(145, 13)
Me.lblMaxDelay.TabIndex = 5
Me.lblMaxDelay.Text = "Maximum delay (Milliseconds)"
'
'txtMaxDelay
'
Me.txtMaxDelay.Location = New System.Drawing.Point(444, 62)
Me.txtMaxDelay.Name = "txtMaxDelay"
Me.txtMaxDelay.Size = New System.Drawing.Size(112, 20)
Me.txtMaxDelay.TabIndex = 4
'
'lblEarlyTriggerChance
'
Me.lblEarlyTriggerChance.AutoSize = True
Me.lblEarlyTriggerChance.Location = New System.Drawing.Point(274, 91)
Me.lblEarlyTriggerChance.Name = "lblEarlyTriggerChance"
Me.lblEarlyTriggerChance.Size = New System.Drawing.Size(147, 13)
Me.lblEarlyTriggerChance.TabIndex = 7
Me.lblEarlyTriggerChance.Text = "Early trigger chance (Percent)"
'
'txtEarlyTriggerChance
'
Me.txtEarlyTriggerChance.Location = New System.Drawing.Point(444, 88)
Me.txtEarlyTriggerChance.Name = "txtEarlyTriggerChance"
Me.txtEarlyTriggerChance.Size = New System.Drawing.Size(112, 20)
Me.txtEarlyTriggerChance.TabIndex = 6
'
'lblTrainSpeedFactor
'
Me.lblTrainSpeedFactor.AutoSize = True
Me.lblTrainSpeedFactor.Location = New System.Drawing.Point(274, 117)
Me.lblTrainSpeedFactor.Name = "lblTrainSpeedFactor"
Me.lblTrainSpeedFactor.Size = New System.Drawing.Size(93, 13)
Me.lblTrainSpeedFactor.TabIndex = 10
Me.lblTrainSpeedFactor.Text = "Train speed factor"
'
'txtTrainSpeedFactor
'
Me.txtTrainSpeedFactor.Location = New System.Drawing.Point(444, 114)
Me.txtTrainSpeedFactor.Name = "txtTrainSpeedFactor"
Me.txtTrainSpeedFactor.Size = New System.Drawing.Size(112, 20)
Me.txtTrainSpeedFactor.TabIndex = 9
'
'lblMinTrainSpeed
'
Me.lblMinTrainSpeed.AutoSize = True
Me.lblMinTrainSpeed.Location = New System.Drawing.Point(274, 143)
Me.lblMinTrainSpeed.Name = "lblMinTrainSpeed"
Me.lblMinTrainSpeed.Size = New System.Drawing.Size(103, 13)
Me.lblMinTrainSpeed.TabIndex = 12
Me.lblMinTrainSpeed.Text = "Minimum train speed"
'
'txtMinTrainSpeed
'
Me.txtMinTrainSpeed.Location = New System.Drawing.Point(444, 140)
Me.txtMinTrainSpeed.Name = "txtMinTrainSpeed"
Me.txtMinTrainSpeed.Size = New System.Drawing.Size(112, 20)
Me.txtMinTrainSpeed.TabIndex = 11
'
'lblTimeout
'
Me.lblTimeout.AutoSize = True
Me.lblTimeout.Location = New System.Drawing.Point(274, 169)
Me.lblTimeout.Name = "lblTimeout"
Me.lblTimeout.Size = New System.Drawing.Size(167, 13)
Me.lblTimeout.TabIndex = 14
Me.lblTimeout.Text = "Time before EBrake (Milliseconds)"
'
'txtTimeout
'
Me.txtTimeout.Location = New System.Drawing.Point(444, 166)
Me.txtTimeout.Name = "txtTimeout"
Me.txtTimeout.Size = New System.Drawing.Size(112, 20)
Me.txtTimeout.TabIndex = 13
'
'frmVigilanceSystem
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(570, 218)
Me.Controls.Add(Me.lblTimeout)
Me.Controls.Add(Me.txtTimeout)
Me.Controls.Add(Me.lblMinTrainSpeed)
Me.Controls.Add(Me.txtMinTrainSpeed)
Me.Controls.Add(Me.lblTrainSpeedFactor)
Me.Controls.Add(Me.txtTrainSpeedFactor)
Me.Controls.Add(Me.lblEarlyTriggerChance)
Me.Controls.Add(Me.txtEarlyTriggerChance)
Me.Controls.Add(Me.lblMaxDelay)
Me.Controls.Add(Me.txtMaxDelay)
Me.Controls.Add(Me.lblMinDelay)
Me.Controls.Add(Me.txtMinDelay)
Me.Controls.Add(Me.chkUseVigilanceSystem)
Me.Controls.Add(Me.lstStatus)
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
Me.MaximizeBox = False
Me.Name = "frmVigilanceSystem"
Me.Text = "Vigilance System Settings"
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Friend WithEvents tmUpdateInfo As System.Windows.Forms.Timer
    Friend WithEvents lstStatus As System.Windows.Forms.ListBox
    Friend WithEvents chkUseVigilanceSystem As System.Windows.Forms.CheckBox
    Friend WithEvents txtMinDelay As System.Windows.Forms.TextBox
    Friend WithEvents lblMinDelay As System.Windows.Forms.Label
    Friend WithEvents lblMaxDelay As System.Windows.Forms.Label
    Friend WithEvents txtMaxDelay As System.Windows.Forms.TextBox
    Friend WithEvents lblEarlyTriggerChance As System.Windows.Forms.Label
    Friend WithEvents txtEarlyTriggerChance As System.Windows.Forms.TextBox
    Friend WithEvents lblTrainSpeedFactor As System.Windows.Forms.Label
    Friend WithEvents txtTrainSpeedFactor As System.Windows.Forms.TextBox
    Friend WithEvents lblMinTrainSpeed As System.Windows.Forms.Label
    Friend WithEvents txtMinTrainSpeed As System.Windows.Forms.TextBox
    Friend WithEvents lblTimeout As System.Windows.Forms.Label
    Friend WithEvents txtTimeout As System.Windows.Forms.TextBox
End Class
