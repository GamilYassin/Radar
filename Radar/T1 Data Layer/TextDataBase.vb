Imports System.IO


''' <summary>
''' This class implements Low level Text File database operations
''' </summary>

Public Class TextDataBase

    Private _TextFileLocation As String


    Public Sub New()

    End Sub

    Public Sub New(TextFileLocation As String)
        Me.New
        Me._TextFileLocation = TextFileLocation

    End Sub


    Public Property FileLocation() As String
        Get
            Return Me._TextFileLocation
        End Get
        Set(TextFileLocation As String)
            Me._TextFileLocation = TextFileLocation
        End Set
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









End Class
