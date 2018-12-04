
Imports TextParser
Imports TextParser.TextParser

Public Class Test_Form


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim file As String
        Dim txtParser As TextParser.TextParser
        Dim Statement As TextParser.Statement

        file = My.Application.Info.DirectoryPath & "\TextParser.txp"
        txtParser = New TextParser.TextParser(file)
        Statement.Name = "Test Test Statement"
        Statement.Body = "SELECT * 
FROM table_Name 
Where x = y;"
        'Statement.Body += "Line 2" & vbCrLf
        'Statement.Body += "Line 3" & vbCrLf
        'Statement.Body += "Line 4"
        txtParser.SaveStatmenet(Statement)
        'TextBox1.Text = txtParser.GetAllData()
        TextBox1.Text = ""
        For Each Title As String In txtParser.SelectData(StringType.Body, "Test Test Statement")
            TextBox1.Text += Title
            TextBox1.Text += vbCrLf
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim txtParser As TextParser.TextParser
        Dim file As String
        file = My.Application.Info.DirectoryPath & "\TextParser.txp"
        txtParser = New TextParser.TextParser(file)
        txtParser.ClearFile()
        TextBox1.Text = ""
        For Each Title As String In txtParser.SelectData(StringType.All, "Test Test Statement")
            TextBox1.Text += Title
            TextBox1.Text += vbCrLf
        Next
    End Sub
End Class
