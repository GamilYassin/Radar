
Imports TextParser
Imports TextParser.TextParser
Imports Radar.Radar

Public Class Test_Form
    Private Lines_Counter As Integer
    Private ReadOnly dbFile As String = "C:\Users\Gemi\Documents\GitHub\Radar\Test_Project\dbFile.csv"
    Private ReadOnly db As New Radar.TextDataBase(dbFile)


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim bolFileexisits As Boolean = db.IsFileExisits()

        ' Check file exists
        AddLine("Test 1 check file exists: " & bolFileexisits)
        'Create file
        If Not bolFileexisits Then
            db.CreateFile()
            AddLine("Test 1 check again if file exists: " & db.IsFileExisits())
        End If
        ' Delete file
        ' db.DeleteFile()
        'AddLine("Test 2 file delete: " & db.IsFileExisits().ToString)
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

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        TextBox1.Text = ""
        Lines_Counter = 0
    End Sub

    Private Sub BtnWriteRow_Click(sender As Object, e As EventArgs) Handles btnWriteRow.Click
        Dim Row As String = ""
        Dim I As Integer

        For I = 0 To 10
            Row += "Column " & I.ToString
            If I < 10 Then Row += ","
        Next
        db.WriteRow(Row)
        AddLine("New Row Added")
    End Sub

    Private Sub BtnRowBuild_Click(sender As Object, e As EventArgs) Handles btnRowBuild.Click
        Dim I As Integer
        Dim Columns As New List(Of String)

        For I = 0 To 10
            Columns.Add("Column " & I.ToString)
        Next
        Dim Row As String = db.BuildUpRow(Columns)
        db.WriteRow(Row)
        AddLine("Row Build up test: New Row Added")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        db.ClearContents()
        AddLine("Check file exists: " & db.IsFileExisits().ToString)
    End Sub

    Private Sub BtnReadRows_Click(sender As Object, e As EventArgs) Handles btnReadRows.Click
        Dim Rows As List(Of String) = db.ReadAllRows()
        AddLine("Rows read count " & Rows.Count)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Rows As List(Of String) = db.ReadAllRows()
        AddLine("Columns count " & db.GetColumns(Rows(0)).Count)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim SelectedRow As List(Of String) = db.SelectRowsString(5, 0)
        Dim Row As String = db.BuildUpRow(SelectedRow)
        AddLine(Row)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        AddLine("Rows Deleted " & db.DeleteRows("xx", 1).ToString)
    End Sub
End Class
