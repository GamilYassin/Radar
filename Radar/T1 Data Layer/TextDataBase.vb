Option Compare Text

Imports System.IO
Imports Radar

''' <summary>
''' This class implements Low level Text File database operations
''' </summary>

Public Class TextDataBase
    'Implements ITextDataBase

    Private _TextFileLocation As String
    Private ReadOnly _Delimiter As Char


    Public Sub New()
        Me._Delimiter = "~"
    End Sub

    Public Sub New(TextFileLocation As String)
        Me.New
        Me._TextFileLocation = TextFileLocation
    End Sub

    Public Sub New(TextFileLocation As String, Delimiter As Char)
        Me.New(TextFileLocation)
        Me._Delimiter = Delimiter
    End Sub


    Public Property FileLocation() As String
        Get
            Return Me._TextFileLocation
        End Get
        Set(TextFileLocation As String)
            Me._TextFileLocation = TextFileLocation
        End Set
    End Property

    Public ReadOnly Property Delimiter() As String
        Get
            Return Me._Delimiter
        End Get
    End Property

    Public Function IsFileExisits() As Boolean
        Return File.Exists(Me._TextFileLocation)
    End Function

    Public Function IsFileExisits(TextFileLocation As String) As Boolean
        Me.FileLocation = TextFileLocation
        Return Me.IsFileExisits
    End Function

    Public Sub CreateFile()
        If Not File.Exists(Me._TextFileLocation) Then
            File.Create(Me._TextFileLocation).Dispose()
        End If
    End Sub

    Public Sub CreateFile(TextFileLocation As String)
        Me.FileLocation = TextFileLocation
        Me.CreateFile()
    End Sub


    Public Sub DeleteFile()
        If File.Exists(Me._TextFileLocation) Then
            File.Delete(Me._TextFileLocation)
        End If
    End Sub

    Public Sub DeleteFile(TextFileLocation As String)
        Me.FileLocation = TextFileLocation
        Me.DeleteFile()
    End Sub

    Public Sub ClearContents()
        Me.DeleteFile()
        Me.CreateFile()
    End Sub

    Public Sub ClearContents(TextFileLocation As String)
        Me.FileLocation = TextFileLocation
        Me.ClearContents()
    End Sub

    Public Sub WriteRow(Row As String)
        Dim myWriter As New StreamWriter(Me.FileLocation, append:=True)

        Try
            ' check if row is not empty
            If Row = "" Then
                Throw New ArgumentNullException(Row)
                Exit Sub
            End If
            ' write row
            myWriter.WriteLine(Row)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Row Write Error")
        Finally
            myWriter.Dispose()
        End Try

    End Sub


    Public Sub WriteRows(Rows As List(Of String))
        For Each Row As String In Rows
            Me.WriteRow(Row)
        Next
    End Sub

    Public Function BuildUpRow(Columns As List(Of String)) As String
        Dim Counter As Integer
        Dim Row As String = ""

        If Columns.Count <= 0 Then
            Throw New ArgumentNullException("Columns")
        End If

        Counter = Columns.Count
        For I As Integer = 0 To Counter - 1
            Row += Columns(I)
            If I < Counter - 1 Then Row += Me.Delimiter
        Next
        Return Row
    End Function

    Public Function GetColumns(Row As String) As List(Of String)
        Dim Columns As List(Of String) = Row.Split(Me.Delimiter).ToList

        Return Columns
    End Function

    Public Function ReadAllRows() As List(Of String)
        Dim myReader As New StreamReader(Me.FileLocation)
        Dim Rows As New List(Of String)

        Try
            Do While Not myReader.EndOfStream
                Rows.Add(myReader.ReadLine)
            Loop
            'Rows = myReader.ReadToEnd().Split(vbCrLf).ToList
            'Rows.RemoveAt(Rows.Count - 1)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Reader Error!")
        Finally
            myReader.Dispose()
        End Try

        Return Rows
    End Function

    Public Function SelectRow(RowId As String, FromColumnIndex As Integer) As List(Of String)
        Dim Rows As List(Of String) = Me.ReadAllRows()

        If Rows.Count <= 1 Then
            Throw New ArgumentNullException("Not Enough rows")
        End If

        For Each Row As String In Rows
            Dim Columns As List(Of String) = Me.GetColumns(Row)
            If Columns.Count <= 0 Then Exit For
            If Columns.Count < FromColumnIndex Then Exit for
            If Columns(FromColumnIndex) = RowId Then
                Return Columns
            End If
        Next

        Return New List(Of String)
    End Function

    Public Function SelectRow(RowId As Integer, FromColumnIndex As Integer) As List(Of String)
        Return Me.SelectRow(RowId.ToString, FromColumnIndex)
    End Function

    Public Function SelectRowString(RowId As String, FromColumnIndex As Integer) As String
        Dim Rows As List(Of String) = Me.ReadAllRows()

        If Rows.Count <= 1 Then
            Throw New ArgumentNullException("Not Enough rows")
        End If

        For Each Row As String In Rows
            Dim Columns As List(Of String) = Me.GetColumns(Row)
            If Columns.Count <= 0 Then Exit For
            If Columns.Count < FromColumnIndex Then Exit For
            If Columns(FromColumnIndex) = RowId Then
                Return Row
            End If
        Next

        Return ""
    End Function

    Public Function SelectRowString(RowId As Integer, FromColumnIndex As Integer) as String    
        Return Me.SelectRowString(RowId.ToString, FromColumnIndex)
    End Function

    Public Function SelectRowsString(RowId As String, FromColumnIndex As Integer) As List(Of String)
        Dim Rows As List(Of String) = Me.ReadAllRows()
        Dim Result As New List(Of String)

        If Rows.Count <= 1 Then
            Throw New ArgumentNullException("Not Enough rows")
        End If

        For Each Row As String In Rows
            Dim Columns As List(Of String) = Me.GetColumns(Row)
            If Columns.Count <= 0 Then Exit For
            If Columns.Count < FromColumnIndex Then Exit For
            If Columns(FromColumnIndex) = RowId Then
                Result.Add(Row)
            End If
        Next

        Return Result
    End Function

    Public Function SelectRowsString(RowId As Integer, FromColumnIndex As Integer) As List(Of String)
        Return Me.SelectRowsString(RowId.ToString, FromColumnIndex)
    End Function


    Public Function DeleteRow(Key1 As String, WhereCol1 As Integer) As Integer
        Dim Rows As List(Of String) = Me.ReadAllRows()
        Dim Rows_deleted As Integer = 0

        If Rows.Count <= 1 Then
            Throw New ArgumentNullException("Not Enough rows")
        End If

        For Each Row As String In Rows
            Dim Columns As List(Of String) = Me.GetColumns(Row)
            If Columns.Count <= 0 Then Exit For
            If Columns.Count < WhereCol1 Then Exit For
            If Columns(WhereCol1) = Key1 Then
                Rows.Remove(Row)
                Me.ClearContents()
                Me.WriteRows(Rows)
                Rows_deleted += 1
                Exit For
            End If
        Next

        Return Rows_deleted
    End Function

    Public Function DeleteRows(Key1 As String, WhereCol1 As Integer) As Integer
        Dim Rows As List(Of String) = Me.ReadAllRows()
        Dim NewRows As New List(Of String)
        Dim Rows_deleted As Integer

        If Rows.Count <= 1 Then
            Throw New ArgumentNullException("Not Enough rows")
        End If

        For Index As Integer = 0 To Rows.Count - 1
            Dim Columns As List(Of String) = Me.GetColumns(Rows(Index))
            If Columns.Count <= 0 Then Exit For
            If Columns.Count < WhereCol1 Then Exit For

            If Not Columns(WhereCol1) = Key1 Then
                NewRows.Add(Rows(Index))
            End If
        Next

        Rows_deleted = Rows.Count - NewRows.Count
        If Rows_deleted > 0 Then
            Me.ClearContents()
            Me.WriteRows(NewRows)
        End If

        Return Rows_deleted
    End Function


    Public Function DeleteRow(Key1 As String, WhereCol1 As Integer, Key2 As String, WhereCol2 As Integer) As Integer
        Dim Rows As List(Of String) = Me.ReadAllRows()
        Dim Rows_deleted As Integer = 0

        If Rows.Count <= 1 Then
            Throw New ArgumentNullException("Not Enough rows")
        End If

        For Each Row As String In Rows
            Dim Columns As List(Of String) = Me.GetColumns(Row)
            If Columns.Count <= 0 Then Exit For
            If Columns.Count < WhereCol1 Or Columns.Count < WhereCol2 Then Exit For
            If Columns(WhereCol1) = Key1 And Columns(WhereCol2) = Key2 Then
                Rows.Remove(Row)
                Me.ClearContents()
                Me.WriteRows(Rows)
                Rows_deleted += 1
                Exit For
            End If
        Next

        Return Rows_deleted
    End Function

    Public Function DeleteRows(Key1 As String, WhereCol1 As Integer, Key2 As String, WhereCol2 As Integer) As Integer
        Dim Rows As List(Of String) = Me.ReadAllRows()
        Dim NewRows As New List(Of String)
        Dim Rows_deleted As Integer

        If Rows.Count <= 1 Then
            Throw New ArgumentNullException("Not Enough rows")
        End If

        For Index As Integer = 0 To Rows.Count - 1
            Dim Columns As List(Of String) = Me.GetColumns(Rows(Index))
            If Columns.Count <= 0 Then Exit For
            If Columns.Count < WhereCol1 Or Columns.Count < WhereCol2 Then Exit For

            If Not (Columns(WhereCol1) = Key1 And Columns(WhereCol2) = Key2) Then
                NewRows.Add(Rows(Index))
            End If
        Next

        Rows_deleted = Rows.Count - NewRows.Count
        If Rows_deleted > 0 Then
            Me.ClearContents()
            Me.WriteRows(NewRows)
        End If

        Return Rows_deleted
    End Function

    Public Function DeleteRow(Key1 As String, WhereCol1 As Integer, Key2 As String, WhereCol2 As Integer, Key3 As String, WhereCol3 As Integer) As Integer
        Dim Rows As List(Of String) = Me.ReadAllRows()
        Dim Rows_deleted As Integer = 0

        If Rows.Count <= 1 Then
            Throw New ArgumentNullException("Not Enough rows")
        End If

        For Each Row As String In Rows
            Dim Columns As List(Of String) = Me.GetColumns(Row)
            If Columns.Count <= 0 Then Exit For
            If Columns.Count < WhereCol1 Or Columns.Count < WhereCol2 Or Columns.Count < WhereCol3 Then Exit For
            If Columns(WhereCol1) = Key1 And Columns(WhereCol2) = Key2 And Columns(WhereCol3) = Key3 Then
                Rows.Remove(Row)
                Me.ClearContents()
                Me.WriteRows(Rows)
                Rows_deleted += 1
                Exit For
            End If
        Next

        Return Rows_deleted
    End Function

    Public Function DeleteRows(Key1 As String, WhereCol1 As Integer, Key2 As String, WhereCol2 As Integer, Key3 As String, WhereCol3 As Integer) As Integer
        Dim Rows As List(Of String) = Me.ReadAllRows()
        Dim NewRows As New List(Of String)
        Dim Rows_deleted As Integer

        If Rows.Count <= 1 Then
            Throw New ArgumentNullException("Not Enough rows")
        End If

        For Index As Integer = 0 To Rows.Count - 1
            Dim Columns As List(Of String) = Me.GetColumns(Rows(Index))
            If Columns.Count <= 0 Then Exit For
            If Columns.Count < WhereCol1 Or Columns.Count < WhereCol2 Or Columns.Count < WhereCol3 Then Exit For

            If Not (Columns(WhereCol1) = Key1 And Columns(WhereCol2) = Key2 And Columns(WhereCol3) = Key3) Then
                NewRows.Add(Rows(Index))
            End If
        Next

        Rows_deleted = Rows.Count - NewRows.Count
        If Rows_deleted > 0 Then
            Me.ClearContents()
            Me.WriteRows(NewRows)
        End If

        Return Rows_deleted
    End Function

    Public Function UpdateRow(Key1 As String, WhereCol1 As Integer, NewValue As String, UpdatedCol As Integer) As Integer
        'Dim Rows As List(Of String) = Me.ReadAllRows()
        'Dim NewRows As New List(Of String)
        'Dim Rows_deleted As Integer

        'If Rows.Count <= 1 Then
        '    Throw New ArgumentNullException("Not Enough rows")
        'End If

        'For Index As Integer = 0 To Rows.Count - 1
        '    Dim Columns As List(Of String) = Me.GetColumns(Rows(Index))
        '    If Columns.Count <= 0 Then Exit For
        '    If Columns.Count < WhereCol1 Then Exit For

        '    If Not Columns(WhereCol1) = Key1 Then
        '        NewRows.Add(Rows(Index))
        '    End If
        'Next

        'Rows_deleted = Rows.Count - NewRows.Count
        'If Rows_deleted > 0 Then
        '    Me.ClearContents()
        '    Me.WriteRows(NewRows)
        'End If

        'Return Rows_deleted
    End Function




End Class
