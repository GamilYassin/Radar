Imports System
Imports System.Data
Imports System.Data.OleDb

Namespace Radar
    Public Class DBOperations
        Private DBConnector As DBConnector


        Public Sub New()
            DBConnector = New DBConnector

        End Sub

        Public Function CreateTable(sqlTable As String) As FunR
            'ToDo: Implement Create Table logic
            Return FunR.Exception
        End Function

        Public Function DBBackup(Location As String) As FunR
            'ToDo: Implement DB Backup logic
            Return FunR.Exception
        End Function

        Public Function DBCopy(Location As String) As FunR
            Return DBBackup(Location)
        End Function

        Public Function DBExport(Location As String) As FunR
            'ToDo: Implement DB Export logic
            Return FunR.Exception
        End Function

        Public Function IntegrateDB() As FunR
            'ToDo: Implement DB Integration logic
            Return FunR.Exception
        End Function

    End Class




End Namespace




