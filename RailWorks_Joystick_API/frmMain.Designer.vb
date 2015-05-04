<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
Me.Closer = New System.Windows.Forms.Timer(Me.components)
Me.txtDevStatus = New System.Windows.Forms.TextBox()
Me.TabControl1 = New System.Windows.Forms.TabControl()
Me.HomeTab = New System.Windows.Forms.TabPage()
Me.gbShiftMappings = New System.Windows.Forms.GroupBox()
Me.btnShiftUp = New System.Windows.Forms.Button()
Me.btnShiftDown = New System.Windows.Forms.Button()
Me.btnRemoveSelected = New System.Windows.Forms.Button()
Me.btnDuplicateSelected = New System.Windows.Forms.Button()
Me.btnSaveSelected = New System.Windows.Forms.Button()
Me.btnNew = New System.Windows.Forms.Button()
Me.btnLoadSelected = New System.Windows.Forms.Button()
Me.lstSettings = New System.Windows.Forms.ListBox()
Me.btnShowLargeSpeedo = New System.Windows.Forms.Button()
Me.ShowControlsBtn = New System.Windows.Forms.Button()
Me.DebugTab = New System.Windows.Forms.TabPage()
Me.lblCurrentDevice = New System.Windows.Forms.Label()
Me.btnAllDevice = New System.Windows.Forms.Button()
Me.btnNextDevice = New System.Windows.Forms.Button()
Me.btnPrevDevice = New System.Windows.Forms.Button()
Me.btnVigilanceSettings = New System.Windows.Forms.Button()
Me.TabControl1.SuspendLayout()
Me.HomeTab.SuspendLayout()
Me.gbShiftMappings.SuspendLayout()
Me.DebugTab.SuspendLayout()
Me.SuspendLayout()
'
'Closer
'
Me.Closer.Interval = 200
'
'txtDevStatus
'
Me.txtDevStatus.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.txtDevStatus.Location = New System.Drawing.Point(0, 35)
Me.txtDevStatus.Multiline = True
Me.txtDevStatus.Name = "txtDevStatus"
Me.txtDevStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
Me.txtDevStatus.Size = New System.Drawing.Size(811, 560)
Me.txtDevStatus.TabIndex = 0
'
'TabControl1
'
Me.TabControl1.Controls.Add(Me.HomeTab)
Me.TabControl1.Controls.Add(Me.DebugTab)
Me.TabControl1.Location = New System.Drawing.Point(12, 12)
Me.TabControl1.Name = "TabControl1"
Me.TabControl1.SelectedIndex = 0
Me.TabControl1.Size = New System.Drawing.Size(822, 617)
Me.TabControl1.TabIndex = 1
'
'HomeTab
'
Me.HomeTab.Controls.Add(Me.btnVigilanceSettings)
Me.HomeTab.Controls.Add(Me.gbShiftMappings)
Me.HomeTab.Controls.Add(Me.btnRemoveSelected)
Me.HomeTab.Controls.Add(Me.btnDuplicateSelected)
Me.HomeTab.Controls.Add(Me.btnSaveSelected)
Me.HomeTab.Controls.Add(Me.btnNew)
Me.HomeTab.Controls.Add(Me.btnLoadSelected)
Me.HomeTab.Controls.Add(Me.lstSettings)
Me.HomeTab.Controls.Add(Me.btnShowLargeSpeedo)
Me.HomeTab.Controls.Add(Me.ShowControlsBtn)
Me.HomeTab.Location = New System.Drawing.Point(4, 22)
Me.HomeTab.Name = "HomeTab"
Me.HomeTab.Padding = New System.Windows.Forms.Padding(3)
Me.HomeTab.Size = New System.Drawing.Size(814, 591)
Me.HomeTab.TabIndex = 0
Me.HomeTab.Text = "Home"
Me.HomeTab.UseVisualStyleBackColor = True
'
'gbShiftMappings
'
Me.gbShiftMappings.Controls.Add(Me.btnShiftUp)
Me.gbShiftMappings.Controls.Add(Me.btnShiftDown)
Me.gbShiftMappings.Location = New System.Drawing.Point(6, 383)
Me.gbShiftMappings.Name = "gbShiftMappings"
Me.gbShiftMappings.Size = New System.Drawing.Size(169, 51)
Me.gbShiftMappings.TabIndex = 4
Me.gbShiftMappings.TabStop = False
Me.gbShiftMappings.Text = "Shift all mapped devices..."
'
'btnShiftUp
'
Me.btnShiftUp.Location = New System.Drawing.Point(87, 19)
Me.btnShiftUp.Name = "btnShiftUp"
Me.btnShiftUp.Size = New System.Drawing.Size(75, 22)
Me.btnShiftUp.TabIndex = 3
Me.btnShiftUp.Text = "Up"
Me.btnShiftUp.UseVisualStyleBackColor = True
'
'btnShiftDown
'
Me.btnShiftDown.Location = New System.Drawing.Point(6, 19)
Me.btnShiftDown.Name = "btnShiftDown"
Me.btnShiftDown.Size = New System.Drawing.Size(75, 22)
Me.btnShiftDown.TabIndex = 3
Me.btnShiftDown.Text = "Down"
Me.btnShiftDown.UseVisualStyleBackColor = True
'
'btnRemoveSelected
'
Me.btnRemoveSelected.Location = New System.Drawing.Point(330, 354)
Me.btnRemoveSelected.Name = "btnRemoveSelected"
Me.btnRemoveSelected.Size = New System.Drawing.Size(75, 23)
Me.btnRemoveSelected.TabIndex = 2
Me.btnRemoveSelected.Text = "Remove"
Me.btnRemoveSelected.UseVisualStyleBackColor = True
'
'btnDuplicateSelected
'
Me.btnDuplicateSelected.Location = New System.Drawing.Point(249, 354)
Me.btnDuplicateSelected.Name = "btnDuplicateSelected"
Me.btnDuplicateSelected.Size = New System.Drawing.Size(75, 23)
Me.btnDuplicateSelected.TabIndex = 2
Me.btnDuplicateSelected.Text = "Duplicate"
Me.btnDuplicateSelected.UseVisualStyleBackColor = True
'
'btnSaveSelected
'
Me.btnSaveSelected.Location = New System.Drawing.Point(168, 354)
Me.btnSaveSelected.Name = "btnSaveSelected"
Me.btnSaveSelected.Size = New System.Drawing.Size(75, 23)
Me.btnSaveSelected.TabIndex = 2
Me.btnSaveSelected.Text = "Save"
Me.btnSaveSelected.UseVisualStyleBackColor = True
'
'btnNew
'
Me.btnNew.Location = New System.Drawing.Point(6, 354)
Me.btnNew.Name = "btnNew"
Me.btnNew.Size = New System.Drawing.Size(75, 23)
Me.btnNew.TabIndex = 2
Me.btnNew.Text = "New"
Me.btnNew.UseVisualStyleBackColor = True
'
'btnLoadSelected
'
Me.btnLoadSelected.Location = New System.Drawing.Point(87, 354)
Me.btnLoadSelected.Name = "btnLoadSelected"
Me.btnLoadSelected.Size = New System.Drawing.Size(75, 23)
Me.btnLoadSelected.TabIndex = 2
Me.btnLoadSelected.Text = "Load"
Me.btnLoadSelected.UseVisualStyleBackColor = True
'
'lstSettings
'
Me.lstSettings.FormattingEnabled = True
Me.lstSettings.Location = New System.Drawing.Point(6, 6)
Me.lstSettings.Name = "lstSettings"
Me.lstSettings.Size = New System.Drawing.Size(399, 342)
Me.lstSettings.TabIndex = 1
'
'btnShowLargeSpeedo
'
Me.btnShowLargeSpeedo.Location = New System.Drawing.Point(692, 49)
Me.btnShowLargeSpeedo.Name = "btnShowLargeSpeedo"
Me.btnShowLargeSpeedo.Size = New System.Drawing.Size(116, 52)
Me.btnShowLargeSpeedo.TabIndex = 0
Me.btnShowLargeSpeedo.Text = "Show Large Speedometer"
Me.btnShowLargeSpeedo.UseVisualStyleBackColor = True
'
'ShowControlsBtn
'
Me.ShowControlsBtn.Location = New System.Drawing.Point(692, 6)
Me.ShowControlsBtn.Name = "ShowControlsBtn"
Me.ShowControlsBtn.Size = New System.Drawing.Size(116, 37)
Me.ShowControlsBtn.TabIndex = 0
Me.ShowControlsBtn.Text = "Show Controls"
Me.ShowControlsBtn.UseVisualStyleBackColor = True
'
'DebugTab
'
Me.DebugTab.BackColor = System.Drawing.SystemColors.ButtonFace
Me.DebugTab.Controls.Add(Me.lblCurrentDevice)
Me.DebugTab.Controls.Add(Me.btnAllDevice)
Me.DebugTab.Controls.Add(Me.btnNextDevice)
Me.DebugTab.Controls.Add(Me.btnPrevDevice)
Me.DebugTab.Controls.Add(Me.txtDevStatus)
Me.DebugTab.Location = New System.Drawing.Point(4, 22)
Me.DebugTab.Name = "DebugTab"
Me.DebugTab.Padding = New System.Windows.Forms.Padding(3)
Me.DebugTab.Size = New System.Drawing.Size(814, 591)
Me.DebugTab.TabIndex = 1
Me.DebugTab.Text = "Debug"
'
'lblCurrentDevice
'
Me.lblCurrentDevice.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lblCurrentDevice.Location = New System.Drawing.Point(318, 6)
Me.lblCurrentDevice.Name = "lblCurrentDevice"
Me.lblCurrentDevice.Size = New System.Drawing.Size(490, 23)
Me.lblCurrentDevice.TabIndex = 2
Me.lblCurrentDevice.Text = "Listening to: ALL Devices..."
Me.lblCurrentDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
'
'btnAllDevice
'
Me.btnAllDevice.Location = New System.Drawing.Point(110, 6)
Me.btnAllDevice.Name = "btnAllDevice"
Me.btnAllDevice.Size = New System.Drawing.Size(98, 23)
Me.btnAllDevice.TabIndex = 1
Me.btnAllDevice.Text = "Listen to all"
Me.btnAllDevice.UseVisualStyleBackColor = True
'
'btnNextDevice
'
Me.btnNextDevice.Location = New System.Drawing.Point(214, 6)
Me.btnNextDevice.Name = "btnNextDevice"
Me.btnNextDevice.Size = New System.Drawing.Size(98, 23)
Me.btnNextDevice.TabIndex = 1
Me.btnNextDevice.Text = "Next Device"
Me.btnNextDevice.UseVisualStyleBackColor = True
'
'btnPrevDevice
'
Me.btnPrevDevice.Location = New System.Drawing.Point(6, 6)
Me.btnPrevDevice.Name = "btnPrevDevice"
Me.btnPrevDevice.Size = New System.Drawing.Size(98, 23)
Me.btnPrevDevice.TabIndex = 1
Me.btnPrevDevice.Text = "Previous Device"
Me.btnPrevDevice.UseVisualStyleBackColor = True
'
'btnVigilanceSettings
'
Me.btnVigilanceSettings.Location = New System.Drawing.Point(692, 107)
Me.btnVigilanceSettings.Name = "btnVigilanceSettings"
Me.btnVigilanceSettings.Size = New System.Drawing.Size(116, 52)
Me.btnVigilanceSettings.TabIndex = 5
Me.btnVigilanceSettings.Text = "Show Vigilance System Settings"
Me.btnVigilanceSettings.UseVisualStyleBackColor = True
'
'frmMain
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(846, 641)
Me.Controls.Add(Me.TabControl1)
Me.Name = "frmMain"
Me.Text = "RailWorks Joystick API"
Me.TabControl1.ResumeLayout(False)
Me.HomeTab.ResumeLayout(False)
Me.gbShiftMappings.ResumeLayout(False)
Me.DebugTab.ResumeLayout(False)
Me.DebugTab.PerformLayout()
Me.ResumeLayout(False)

End Sub
    Friend WithEvents Closer As System.Windows.Forms.Timer
    Friend WithEvents txtDevStatus As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents HomeTab As System.Windows.Forms.TabPage
    Friend WithEvents DebugTab As System.Windows.Forms.TabPage
    Friend WithEvents btnPrevDevice As System.Windows.Forms.Button
    Friend WithEvents btnNextDevice As System.Windows.Forms.Button
    Friend WithEvents ShowControlsBtn As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelected As System.Windows.Forms.Button
    Friend WithEvents btnDuplicateSelected As System.Windows.Forms.Button
    Friend WithEvents btnSaveSelected As System.Windows.Forms.Button
    Friend WithEvents btnLoadSelected As System.Windows.Forms.Button
    Friend WithEvents lstSettings As System.Windows.Forms.ListBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnShowLargeSpeedo As System.Windows.Forms.Button
    Friend WithEvents btnAllDevice As System.Windows.Forms.Button
    Friend WithEvents lblCurrentDevice As System.Windows.Forms.Label
    Friend WithEvents gbShiftMappings As System.Windows.Forms.GroupBox
    Friend WithEvents btnShiftUp As System.Windows.Forms.Button
    Friend WithEvents btnShiftDown As System.Windows.Forms.Button
    Friend WithEvents btnVigilanceSettings As System.Windows.Forms.Button

End Class
