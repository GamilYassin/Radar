Imports System.Data
Imports System.Data.OleDb
Imports Radar

Namespace Radar
    Public Class DBConnector
        Implements IDisposable

        Private ConnectionString As String
        Private DataSource As String
        Private DataProvider As String
        Private DBConnection As OleDbConnection
        Private DBCommand As OleDbCommand

        Public Sub New()
            'TODO: Make Data Source Global Variable
            DataSource = "Data Source= " & "C:\Users\Gemi\Documents\VB Projects\Radar\Radar\T0 DataBase\Radar.mdf"
            DataProvider = "Provider=Microsoft.Jet.OLEDB.4.0;"
            ConnectionString = DataProvider & DataSource
            DBConnection = New OleDbConnection
            DBConnection.ConnectionString = ConnectionString
            With DBCommand
                .Connection = Me.DBConnection
                .CommandType = CommandType.Text
            End With
        End Sub

        Public ReadOnly Property GetConnectionString() As String
            Get
                Return Me.ConnectionString
            End Get
        End Property

        Public ReadOnly Property ConnectionStatus() As ConnectionState
            Get
                Return DBConnection.State
            End Get
        End Property

        Public ReadOnly Property GetConnection() As OleDbConnection
            Get
                Return Me.DBConnection
            End Get
        End Property

        Public Function OpenConnection() As FunR
            Try
                DBConnection.Open()
                If DBConnection.State = ConnectionState.Open Then
                    Return FunR.Succeded
                Else
                    Return FunR.Failed
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Return FunR.Exception
            Finally
                Me.CloseConnection()
            End Try
        End Function

        Public Function CloseConnection() As FunR
            Try
                DBConnection.Close()
                If DBConnection.State = ConnectionState.Closed Then
                    Return FunR.Succeded
                Else
                    Return FunR.Failed
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Return FunR.Exception
            Finally
                Me.CloseConnection()
            End Try
        End Function

        Public Function ExecuteCommands(cmdString As String) As FunR
            Try
                Me.OpenConnection()
                If DBCommand.ExecuteNonQuery() > 0 Then
                    Return FunR.Succeded
                Else
                    Return FunR.Failed
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
                Return FunR.Exception
            Finally
                Me.CloseConnection()
            End Try
        End Function

        Public Function ExecuteSelectCommand(sql_statment As String, ByRef MyTable As DataTable) As FunR
            Dim DBAdapter As New OleDbDataAdapter(sql_statment, DBConnection)

            Try
                If Me.ConnectionStatus <> ConnectionState.Open Then OpenConnection()
                DBAdapter.Fill(MyTable)
                Return FunR.Succeded
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
                Return FunR.Exception
            Finally
                CloseConnection()
                DBAdapter.Dispose()
            End Try
        End Function

        Public Function ExecuteSelectCommand(sql_statment As String, ByRef MyDataSet As DataSet) As FunR
            Dim DBAdapter As New OleDbDataAdapter(sql_statment, DBConnection)

            Try
                If Me.ConnectionStatus <> ConnectionState.Open Then OpenConnection()
                DBAdapter.Fill(MyDataSet)
                Return FunR.Succeded
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation)
                Return FunR.Exception
            Finally
                CloseConnection()
                DBAdapter.Dispose()
            End Try
        End Function


        Public Sub Dispose()
            Me.DBConnection.Dispose()
            Me.DBCommand.Dispose()
            Me.Dispose()
        End Sub

        Private Sub IDisposable_Dispose() Implements IDisposable.Dispose
            Throw New NotImplementedException()
        End Sub
    End Class

End Namespace


