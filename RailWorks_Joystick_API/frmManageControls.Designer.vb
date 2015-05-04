<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageControls
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
Me.gbInputList = New System.Windows.Forms.GroupBox()
Me.btnExport = New System.Windows.Forms.Button()
Me.btnImport = New System.Windows.Forms.Button()
Me.btnRemoveInput = New System.Windows.Forms.Button()
Me.lswInputList = New System.Windows.Forms.ListView()
Me.InputListHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
Me.btnAssignSelected = New System.Windows.Forms.Button()
Me.btnAddInput = New System.Windows.Forms.Button()
Me.gbTreatAs = New System.Windows.Forms.GroupBox()
Me.cbTreatAs = New System.Windows.Forms.ComboBox()
Me.gbCommandDescription = New System.Windows.Forms.GroupBox()
Me.lbCommandDescription = New System.Windows.Forms.Label()
Me.gbMode = New System.Windows.Forms.GroupBox()
Me.cbMode = New System.Windows.Forms.ComboBox()
Me.pbInputValue = New System.Windows.Forms.PictureBox()
Me.gbInputValue = New System.Windows.Forms.GroupBox()
Me.gbOutputValue = New System.Windows.Forms.GroupBox()
Me.pbOutputValue = New System.Windows.Forms.PictureBox()
Me.gbNotches = New System.Windows.Forms.GroupBox()
Me.txtNotches = New System.Windows.Forms.TextBox()
Me.cbNotchPresets = New System.Windows.Forms.ComboBox()
Me.gbNotchRanges = New System.Windows.Forms.GroupBox()
Me.txtRanges = New System.Windows.Forms.TextBox()
Me.cbRangePresets = New System.Windows.Forms.ComboBox()
Me.btnReload = New System.Windows.Forms.Button()
Me.gbAPIOutput = New System.Windows.Forms.GroupBox()
Me.pbAPIOutput = New System.Windows.Forms.PictureBox()
Me.chkIsNegativePositiveRange = New System.Windows.Forms.CheckBox()
Me.gbInputList.SuspendLayout()
Me.gbTreatAs.SuspendLayout()
Me.gbCommandDescription.SuspendLayout()
Me.gbMode.SuspendLayout()
CType(Me.pbInputValue, System.ComponentModel.ISupportInitialize).BeginInit()
Me.gbInputValue.SuspendLayout()
Me.gbOutputValue.SuspendLayout()
CType(Me.pbOutputValue, System.ComponentModel.ISupportInitialize).BeginInit()
Me.gbNotches.SuspendLayout()
Me.gbNotchRanges.SuspendLayout()
Me.gbAPIOutput.SuspendLayout()
CType(Me.pbAPIOutput, System.ComponentModel.ISupportInitialize).BeginInit()
Me.SuspendLayout()
'
'gbInputList
'
Me.gbInputList.Controls.Add(Me.btnExport)
Me.gbInputList.Controls.Add(Me.btnImport)
Me.gbInputList.Controls.Add(Me.btnRemoveInput)
Me.gbInputList.Controls.Add(Me.lswInputList)
Me.gbInputList.Controls.Add(Me.btnAssignSelected)
Me.gbInputList.Controls.Add(Me.btnAddInput)
Me.gbInputList.Location = New System.Drawing.Point(12, 80)
Me.gbInputList.Name = "gbInputList"
Me.gbInputList.Size = New System.Drawing.Size(506, 274)
Me.gbInputList.TabIndex = 0
Me.gbInputList.TabStop = False
Me.gbInputList.Text = "Inputs"
'
'btnExport
'
Me.btnExport.Location = New System.Drawing.Point(425, 245)
Me.btnExport.Name = "btnExport"
Me.btnExport.Size = New System.Drawing.Size(75, 23)
Me.btnExport.TabIndex = 1
Me.btnExport.Text = "Export"
Me.btnExport.UseVisualStyleBackColor = True
'
'btnImport
'
Me.btnImport.Location = New System.Drawing.Point(344, 245)
Me.btnImport.Name = "btnImport"
Me.btnImport.Size = New System.Drawing.Size(75, 23)
Me.btnImport.TabIndex = 1
Me.btnImport.Text = "Import"
Me.btnImport.UseVisualStyleBackColor = True
'
'btnRemoveInput
'
Me.btnRemoveInput.Location = New System.Drawing.Point(222, 245)
Me.btnRemoveInput.Name = "btnRemoveInput"
Me.btnRemoveInput.Size = New System.Drawing.Size(100, 23)
Me.btnRemoveInput.TabIndex = 1
Me.btnRemoveInput.Text = "Remove Selected"
Me.btnRemoveInput.UseVisualStyleBackColor = True
'
'lswInputList
'
Me.lswInputList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.InputListHeader})
Me.lswInputList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lswInputList.ForeColor = System.Drawing.Color.Red
Me.lswInputList.FullRowSelect = True
Me.lswInputList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
Me.lswInputList.HideSelection = False
Me.lswInputList.Location = New System.Drawing.Point(10, 19)
Me.lswInputList.MultiSelect = False
Me.lswInputList.Name = "lswInputList"
Me.lswInputList.ShowGroups = False
Me.lswInputList.Size = New System.Drawing.Size(490, 220)
Me.lswInputList.TabIndex = 1
Me.lswInputList.UseCompatibleStateImageBehavior = False
Me.lswInputList.View = System.Windows.Forms.View.Details
'
'InputListHeader
'
Me.InputListHeader.Text = "Should not see"
Me.InputListHeader.Width = 486
'
'btnAssignSelected
'
Me.btnAssignSelected.Location = New System.Drawing.Point(116, 245)
Me.btnAssignSelected.Name = "btnAssignSelected"
Me.btnAssignSelected.Size = New System.Drawing.Size(100, 23)
Me.btnAssignSelected.TabIndex = 1
Me.btnAssignSelected.Text = "Assign Selected"
Me.btnAssignSelected.UseVisualStyleBackColor = True
'
'btnAddInput
'
Me.btnAddInput.Location = New System.Drawing.Point(10, 245)
Me.btnAddInput.Name = "btnAddInput"
Me.btnAddInput.Size = New System.Drawing.Size(100, 23)
Me.btnAddInput.TabIndex = 1
Me.btnAddInput.Text = "Add Input"
Me.btnAddInput.UseVisualStyleBackColor = True
'
'gbTreatAs
'
Me.gbTreatAs.Controls.Add(Me.cbTreatAs)
Me.gbTreatAs.Location = New System.Drawing.Point(12, 360)
Me.gbTreatAs.Name = "gbTreatAs"
Me.gbTreatAs.Size = New System.Drawing.Size(250, 50)
Me.gbTreatAs.TabIndex = 0
Me.gbTreatAs.TabStop = False
Me.gbTreatAs.Text = "Treat as..."
'
'cbTreatAs
'
Me.cbTreatAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.cbTreatAs.Enabled = False
Me.cbTreatAs.FormattingEnabled = True
Me.cbTreatAs.Items.AddRange(New Object() {"Not Assigned", "Axis", "Button"})
Me.cbTreatAs.Location = New System.Drawing.Point(10, 19)
Me.cbTreatAs.Name = "cbTreatAs"
Me.cbTreatAs.Size = New System.Drawing.Size(234, 21)
Me.cbTreatAs.TabIndex = 0
'
'gbCommandDescription
'
Me.gbCommandDescription.Controls.Add(Me.lbCommandDescription)
Me.gbCommandDescription.Location = New System.Drawing.Point(12, 12)
Me.gbCommandDescription.Name = "gbCommandDescription"
Me.gbCommandDescription.Size = New System.Drawing.Size(740, 62)
Me.gbCommandDescription.TabIndex = 0
Me.gbCommandDescription.TabStop = False
Me.gbCommandDescription.Text = "Command..."
'
'lbCommandDescription
'
Me.lbCommandDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
Me.lbCommandDescription.ForeColor = System.Drawing.Color.Green
Me.lbCommandDescription.Location = New System.Drawing.Point(6, 16)
Me.lbCommandDescription.Name = "lbCommandDescription"
Me.lbCommandDescription.Size = New System.Drawing.Size(728, 43)
Me.lbCommandDescription.TabIndex = 0
Me.lbCommandDescription.Text = "NOTHING"
Me.lbCommandDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'gbMode
'
Me.gbMode.Controls.Add(Me.cbMode)
Me.gbMode.Location = New System.Drawing.Point(268, 360)
Me.gbMode.Name = "gbMode"
Me.gbMode.Size = New System.Drawing.Size(250, 50)
Me.gbMode.TabIndex = 0
Me.gbMode.TabStop = False
Me.gbMode.Text = "Mode..."
'
'cbMode
'
Me.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.cbMode.Enabled = False
Me.cbMode.FormattingEnabled = True
Me.cbMode.Items.AddRange(New Object() {"Slider", "Notched slider", "Notched slider with ranges", "Up/Down latch", "Up/Down relative"})
Me.cbMode.Location = New System.Drawing.Point(10, 19)
Me.cbMode.Name = "cbMode"
Me.cbMode.Size = New System.Drawing.Size(234, 21)
Me.cbMode.TabIndex = 0
'
'pbInputValue
'
Me.pbInputValue.Location = New System.Drawing.Point(6, 19)
Me.pbInputValue.Name = "pbInputValue"
Me.pbInputValue.Size = New System.Drawing.Size(55, 530)
Me.pbInputValue.TabIndex = 1
Me.pbInputValue.TabStop = False
'
'gbInputValue
'
Me.gbInputValue.Controls.Add(Me.pbInputValue)
Me.gbInputValue.Location = New System.Drawing.Point(527, 80)
Me.gbInputValue.Name = "gbInputValue"
Me.gbInputValue.Size = New System.Drawing.Size(69, 555)
Me.gbInputValue.TabIndex = 0
Me.gbInputValue.TabStop = False
Me.gbInputValue.Text = "Input"
'
'gbOutputValue
'
Me.gbOutputValue.Controls.Add(Me.pbOutputValue)
Me.gbOutputValue.Location = New System.Drawing.Point(602, 80)
Me.gbOutputValue.Name = "gbOutputValue"
Me.gbOutputValue.Size = New System.Drawing.Size(69, 555)
Me.gbOutputValue.TabIndex = 0
Me.gbOutputValue.TabStop = False
Me.gbOutputValue.Text = "Output"
'
'pbOutputValue
'
Me.pbOutputValue.Location = New System.Drawing.Point(6, 19)
Me.pbOutputValue.Name = "pbOutputValue"
Me.pbOutputValue.Size = New System.Drawing.Size(55, 530)
Me.pbOutputValue.TabIndex = 1
Me.pbOutputValue.TabStop = False
'
'gbNotches
'
Me.gbNotches.Controls.Add(Me.txtNotches)
Me.gbNotches.Controls.Add(Me.cbNotchPresets)
Me.gbNotches.Location = New System.Drawing.Point(12, 416)
Me.gbNotches.Name = "gbNotches"
Me.gbNotches.Size = New System.Drawing.Size(506, 50)
Me.gbNotches.TabIndex = 0
Me.gbNotches.TabStop = False
Me.gbNotches.Text = "Notches..."
Me.gbNotches.Visible = False
'
'txtNotches
'
Me.txtNotches.Location = New System.Drawing.Point(10, 20)
Me.txtNotches.Name = "txtNotches"
Me.txtNotches.Size = New System.Drawing.Size(250, 20)
Me.txtNotches.TabIndex = 1
'
'cbNotchPresets
'
Me.cbNotchPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.cbNotchPresets.FormattingEnabled = True
Me.cbNotchPresets.Items.AddRange(New Object() {"Evenly distribute...", "2 Notches", "3 Notches", "4 Notches", "5 Notches", "6 Notches", "7 Notches", "8 Notches", "9 Notches", "10 Notches", "11 Notches", "12 Notches", "13 Notches", "14 Notches", "15 Notches", "16 Notches", "17 Notches", "18 Notches", "19 Notches", "20 Notches", "21 Notches", "22 Notches", "23 Notches", "24 Notches", "25 Notches", "26 Notches", "27 Notches", "28 Notches", "29 Notches"})
Me.cbNotchPresets.Location = New System.Drawing.Point(266, 19)
Me.cbNotchPresets.Name = "cbNotchPresets"
Me.cbNotchPresets.Size = New System.Drawing.Size(234, 21)
Me.cbNotchPresets.TabIndex = 0
'
'gbNotchRanges
'
Me.gbNotchRanges.Controls.Add(Me.txtRanges)
Me.gbNotchRanges.Controls.Add(Me.cbRangePresets)
Me.gbNotchRanges.Location = New System.Drawing.Point(12, 472)
Me.gbNotchRanges.Name = "gbNotchRanges"
Me.gbNotchRanges.Size = New System.Drawing.Size(506, 50)
Me.gbNotchRanges.TabIndex = 0
Me.gbNotchRanges.TabStop = False
Me.gbNotchRanges.Text = "Ranges..."
Me.gbNotchRanges.Visible = False
'
'txtRanges
'
Me.txtRanges.Location = New System.Drawing.Point(10, 20)
Me.txtRanges.Name = "txtRanges"
Me.txtRanges.Size = New System.Drawing.Size(250, 20)
Me.txtRanges.TabIndex = 1
'
'cbRangePresets
'
Me.cbRangePresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
Me.cbRangePresets.FormattingEnabled = True
Me.cbRangePresets.Items.AddRange(New Object() {"Evenly distribute...", "2 Notches", "3 Notches", "4 Notches", "5 Notches", "6 Notches", "7 Notches", "8 Notches", "9 Notches", "10 Notches", "11 Notches", "12 Notches", "13 Notches", "14 Notches", "15 Notches", "16 Notches", "17 Notches", "18 Notches", "19 Notches", "20 Notches", "21 Notches", "22 Notches", "23 Notches", "24 Notches", "25 Notches", "26 Notches", "27 Notches", "28 Notches", "29 Notches"})
Me.cbRangePresets.Location = New System.Drawing.Point(266, 19)
Me.cbRangePresets.Name = "cbRangePresets"
Me.cbRangePresets.Size = New System.Drawing.Size(234, 21)
Me.cbRangePresets.TabIndex = 0
'
'btnReload
'
Me.btnReload.Enabled = False
Me.btnReload.Location = New System.Drawing.Point(446, 612)
Me.btnReload.Name = "btnReload"
Me.btnReload.Size = New System.Drawing.Size(75, 23)
Me.btnReload.TabIndex = 1
Me.btnReload.Text = "Reload"
Me.btnReload.UseVisualStyleBackColor = True
'
'gbAPIOutput
'
Me.gbAPIOutput.Controls.Add(Me.pbAPIOutput)
Me.gbAPIOutput.Location = New System.Drawing.Point(677, 80)
Me.gbAPIOutput.Name = "gbAPIOutput"
Me.gbAPIOutput.Size = New System.Drawing.Size(75, 555)
Me.gbAPIOutput.TabIndex = 0
Me.gbAPIOutput.TabStop = False
Me.gbAPIOutput.Text = "RW Output"
'
'pbAPIOutput
'
Me.pbAPIOutput.Location = New System.Drawing.Point(10, 19)
Me.pbAPIOutput.Name = "pbAPIOutput"
Me.pbAPIOutput.Size = New System.Drawing.Size(55, 530)
Me.pbAPIOutput.TabIndex = 1
Me.pbAPIOutput.TabStop = False
'
'chkIsNegativePositiveRange
'
Me.chkIsNegativePositiveRange.AutoSize = True
Me.chkIsNegativePositiveRange.Location = New System.Drawing.Point(22, 528)
Me.chkIsNegativePositiveRange.Name = "chkIsNegativePositiveRange"
Me.chkIsNegativePositiveRange.Size = New System.Drawing.Size(369, 17)
Me.chkIsNegativePositiveRange.TabIndex = 2
Me.chkIsNegativePositiveRange.Text = "Treat as -100% to +100% (used for certain combined throttle locomotives)"
Me.chkIsNegativePositiveRange.UseVisualStyleBackColor = True
'
'frmManageControls
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(765, 647)
Me.Controls.Add(Me.chkIsNegativePositiveRange)
Me.Controls.Add(Me.btnReload)
Me.Controls.Add(Me.gbMode)
Me.Controls.Add(Me.gbAPIOutput)
Me.Controls.Add(Me.gbOutputValue)
Me.Controls.Add(Me.gbInputValue)
Me.Controls.Add(Me.gbNotchRanges)
Me.Controls.Add(Me.gbNotches)
Me.Controls.Add(Me.gbTreatAs)
Me.Controls.Add(Me.gbCommandDescription)
Me.Controls.Add(Me.gbInputList)
Me.DoubleBuffered = True
Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
Me.Name = "frmManageControls"
Me.Text = "Control Manager"
Me.gbInputList.ResumeLayout(False)
Me.gbTreatAs.ResumeLayout(False)
Me.gbCommandDescription.ResumeLayout(False)
Me.gbMode.ResumeLayout(False)
CType(Me.pbInputValue, System.ComponentModel.ISupportInitialize).EndInit()
Me.gbInputValue.ResumeLayout(False)
Me.gbOutputValue.ResumeLayout(False)
CType(Me.pbOutputValue, System.ComponentModel.ISupportInitialize).EndInit()
Me.gbNotches.ResumeLayout(False)
Me.gbNotches.PerformLayout()
Me.gbNotchRanges.ResumeLayout(False)
Me.gbNotchRanges.PerformLayout()
Me.gbAPIOutput.ResumeLayout(False)
CType(Me.pbAPIOutput, System.ComponentModel.ISupportInitialize).EndInit()
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Friend WithEvents gbInputList As System.Windows.Forms.GroupBox
    Friend WithEvents gbTreatAs As System.Windows.Forms.GroupBox
    Friend WithEvents cbTreatAs As System.Windows.Forms.ComboBox
    Friend WithEvents gbCommandDescription As System.Windows.Forms.GroupBox
    Friend WithEvents lbCommandDescription As System.Windows.Forms.Label
    Friend WithEvents gbMode As System.Windows.Forms.GroupBox
    Friend WithEvents cbMode As System.Windows.Forms.ComboBox
    Friend WithEvents pbInputValue As System.Windows.Forms.PictureBox
    Friend WithEvents gbInputValue As System.Windows.Forms.GroupBox
    Friend WithEvents gbOutputValue As System.Windows.Forms.GroupBox
    Friend WithEvents pbOutputValue As System.Windows.Forms.PictureBox
    Friend WithEvents lswInputList As System.Windows.Forms.ListView
    Friend WithEvents InputListHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnAddInput As System.Windows.Forms.Button
    Friend WithEvents btnRemoveInput As System.Windows.Forms.Button
    Friend WithEvents btnAssignSelected As System.Windows.Forms.Button
    Friend WithEvents gbNotches As System.Windows.Forms.GroupBox
    Friend WithEvents txtNotches As System.Windows.Forms.TextBox
    Friend WithEvents cbNotchPresets As System.Windows.Forms.ComboBox
    Friend WithEvents gbNotchRanges As System.Windows.Forms.GroupBox
    Friend WithEvents txtRanges As System.Windows.Forms.TextBox
    Friend WithEvents cbRangePresets As System.Windows.Forms.ComboBox
    Friend WithEvents btnReload As System.Windows.Forms.Button
    Friend WithEvents gbAPIOutput As System.Windows.Forms.GroupBox
    Friend WithEvents pbAPIOutput As System.Windows.Forms.PictureBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents chkIsNegativePositiveRange As System.Windows.Forms.CheckBox
End Class
