
Imports TextParser
Imports TextParser.TextParser

Public Class Test_Form
    Private Lines_Counter As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddLine("Test Line")
    End Sub

    Private Sub AddLine(NewLine As String)
        Lines_Counter += 1
        With TextBox1
            .Text += Lines_Counter.ToString & ":" & vbTab & NewLine & vbCrLf
        End With

    End Sub

    Private Sub Test_Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        Lines_Counter = 0
    End Sub
End Class
