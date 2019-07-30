<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Test_Form
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnWriteRow = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRowBuild = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnReadRows = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(23, 34)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(91, 27)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Create File"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(127, 34)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(455, 399)
        Me.TextBox1.TabIndex = 1
        '
        'btnWriteRow
        '
        Me.btnWriteRow.Location = New System.Drawing.Point(23, 67)
        Me.btnWriteRow.Name = "btnWriteRow"
        Me.btnWriteRow.Size = New System.Drawing.Size(91, 27)
        Me.btnWriteRow.TabIndex = 0
        Me.btnWriteRow.Text = "Write Row"
        Me.btnWriteRow.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(31, 406)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(83, 27)
        Me.btnClear.TabIndex = 0
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRowBuild
        '
        Me.btnRowBuild.Location = New System.Drawing.Point(23, 100)
        Me.btnRowBuild.Name = "btnRowBuild"
        Me.btnRowBuild.Size = New System.Drawing.Size(91, 27)
        Me.btnRowBuild.TabIndex = 0
        Me.btnRowBuild.Text = "Row Build up"
        Me.btnRowBuild.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(23, 133)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(91, 27)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Clear Contents"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnReadRows
        '
        Me.btnReadRows.Location = New System.Drawing.Point(23, 166)
        Me.btnReadRows.Name = "btnReadRows"
        Me.btnReadRows.Size = New System.Drawing.Size(91, 27)
        Me.btnReadRows.TabIndex = 2
        Me.btnReadRows.Text = "Read All Rows"
        Me.btnReadRows.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(23, 199)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(91, 27)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Columns Build"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(23, 232)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(91, 27)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "Select Row"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(23, 265)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(91, 27)
        Me.Button5.TabIndex = 2
        Me.Button5.Text = "Delete Row"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Test_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 450)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.btnReadRows)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnRowBuild)
        Me.Controls.Add(Me.btnWriteRow)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Test_Form"
        Me.Text = "Test Form"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents btnWriteRow As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnRowBuild As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents btnReadRows As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
End Class
