Imports System.IO
Imports Radar

''' <summary>
''' This class implements Low level Text File database operations
''' </summary>

Public Class TextDataBase
    Implements ITextDataBase

    Private _TextFileLocation As String


    Public Sub New()

    End Sub

    Public Sub New(TextFileLocation As String)
        Me.New
        Me._TextFileLocation = TextFileLocation

    End Sub


    Public Property FileLocation() As String Implements ITextDataBase.FileLocation
        Get
            Return Me._TextFileLocation
        End Get
        Set(TextFileLocation As String)
            Me._TextFileLocation = TextFileLocation
        End Set
    End Property

    Public Function IsFileExisits() As Boolean Implements ITextDataBase.IsFileExisits
        Return File.Exists(Me._TextFileLocation)
    End Function

    Public Function IsFileExisits(TextFileLocation As String) As Boolean Implements ITextDataBase.IsFileExisits
        Me.FileLocation = TextFileLocation
        Return Me.IsFileExisits
    End Function

    Public Sub CreateFile() Implements ITextDataBase.CreateFile
        If Not File.Exists(Me._TextFileLocation) Then
            File.Create(Me._TextFileLocation).Dispose()
        End If
    End Sub

    Public Sub CreateFile(TextFileLocation As String) Implements ITextDataBase.CreateFile
        Me.FileLocation = TextFileLocation
        Me.CreateFile()
    End Sub

    Public Sub InsertLine(Line As String) Implements ITextDataBase.InsertLine
        Throw New NotImplementedException()
    End Sub

    Public Sub InsertLine(Line As List(Of String)) Implements ITextDataBase.InsertLine
        Throw New NotImplementedException()
    End Sub
End Class
