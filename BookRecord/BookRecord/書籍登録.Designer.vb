<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frn書籍記録
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btn登録 = New System.Windows.Forms.Button()
        Me.txt登録 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btn登録
        '
        Me.btn登録.Location = New System.Drawing.Point(142, 34)
        Me.btn登録.Name = "btn登録"
        Me.btn登録.Size = New System.Drawing.Size(72, 27)
        Me.btn登録.TabIndex = 1
        Me.btn登録.Text = "登録"
        Me.btn登録.UseVisualStyleBackColor = True
        '
        'txt登録
        '
        Me.txt登録.Location = New System.Drawing.Point(27, 38)
        Me.txt登録.Name = "txt登録"
        Me.txt登録.Size = New System.Drawing.Size(100, 19)
        Me.txt登録.TabIndex = 2
        '
        'Frn書籍記録
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(236, 100)
        Me.Controls.Add(Me.txt登録)
        Me.Controls.Add(Me.btn登録)
        Me.Name = "Frn書籍記録"
        Me.Text = "書籍記録"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn登録 As Button
    Friend WithEvents txt登録 As TextBox
End Class
