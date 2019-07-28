Imports System.IO


Namespace TextParser
    Public Class TextParser

        Private FileLocation As String
        Private DelimeterStartChar As Char
        Private DelimeterEndChar As Char

        Public Sub New(FileLocation As String)
            Me.FileLocation = FileLocation
            Me.DelimeterStartChar = "~"
            Me.DelimeterEndChar = "~"
        End Sub

        Public ReadOnly Property IsFileExisit() As Boolean
            Get
                Return File.Exists(Me.FileLocation)
            End Get
        End Property


        Public Sub CreateFile()
            If Not File.Exists(Me.FileLocation) Then
                File.Create(Me.FileLocation).Dispose()
            End If
        End Sub

        Public Sub SaveStatmenet(ByVal StatementToBeSaved As Statement)
            Dim NewWriter As New StreamWriter(Me.FileLocation, True)

            Try
                NewWriter.WriteLine(Me.DelimeterStartChar & StatementToBeSaved.Name & Me.DelimeterEndChar)
                NewWriter.WriteLine(StatementToBeSaved.Body)
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                NewWriter.Dispose()
            End Try
        End Sub

        Public Function SelectData(ByVal WhatToSelect As StringType, Optional ByVal StatementTitle As String = "") As List(Of String)
            Dim StreamReader As New StreamReader(Me.FileLocation)
            Dim Result As New List(Of String)
            Dim Line As String
            Dim bolBody As Boolean

            Try
                Line = StreamReader.ReadLine
                bolBody = False
                Do While Line <> Nothing
                    Select Case WhatToSelect
                        Case StringType.All
                            Result.Add(Line)
                        Case StringType.Titles
                            If Me.IsTitleString(Line) Then
                                Result.Add(Me.ExtractTitle(Line))
                            End If
                        Case StringType.Body
                            If bolBody Then
                                Result.Add(Line)
                            End If
                            If Me.IsTitleString(Line) And Not bolBody Then
                                If Me.ExtractTitle(Line) = StatementTitle Then
                                    bolBody = True
                                End If
                            ElseIf Me.IsTitleString(Line) And bolBody Then
                                bolBody = False
                                Result.RemoveAt(Result.Count - 1)
                                Return Result
                            End If
                    End Select
                    Line = StreamReader.ReadLine
                Loop
                Return Result
            Catch ex As Exception
                MsgBox(ex.Message)
                Return Result
            Finally
                StreamReader.Dispose()
            End Try
        End Function

        Public Sub ClearFile()
            Dim NewWriter As New StreamWriter(Me.FileLocation, False)

            Try
                NewWriter.Write("")
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                NewWriter.Dispose()
            End Try
        End Sub

        Private Function IsTitleString(Line As String) As Boolean
            Return Line.StartsWith(DelimeterStartChar)
        End Function

        Private Function ExtractTitle(Line As String) As String
            Return Line.Replace(DelimeterStartChar, "").Replace(DelimeterEndChar, "")
        End Function
    End Class

    Public Structure Statement
        Public Name As String
        Public Body As String
    End Structure

    Public Enum StringType As Integer
        All = 1
        Titles
        Body
    End Enum
End Namespace

