Imports System
Imports System.Data
Imports System.Data.OleDb


Namespace Radar
    Public MustInherit Class TableHandler

        Private Name As String
        'Private ColumnsCount As Integer
        Private RowsCount As Integer
        Private ColumnsHeaders As List(Of String)
        Private ColumnsTypes As List(Of SqlGenerator.SQLDataTypesEnum)
        Private dbConnector As DBConnector
        Private PK_ColumnName As String
        Private MyTable As DataTable


        Public Sub New()
            RowsCount = 0
            ColumnsHeaders = New List(Of String)
            ColumnsTypes = New List(Of SqlGenerator.SQLDataTypesEnum)
            dbConnector = New DBConnector()
            MyTable = New DataTable()
        End Sub

        Public Sub New(myName As String)
            Me.New
            Name = myName
            MyTable.TableName = myName
        End Sub

        Public Sub New(myName As String, PK_Col As String)
            Me.New(myName)
            Me.PK_ColumnName = PK_Col
        End Sub

        Public ReadOnly Property GetColumnsCount() As Integer
            Get
                Return Me.ColumnsHeaders.Count
            End Get
        End Property

        Public ReadOnly Property GetRowsCount() As Integer
            Get
                Return Me.RowsCount
            End Get
        End Property

        Public ReadOnly Property GetName() As Integer
            Get
                Return Me.Name
            End Get
        End Property

        Public ReadOnly Property GetColumnHeader(Index As Integer) As String
            Get
                If Index < ColumnsHeaders.Count Then
                    Return Me.GetColumnHeader(Index)
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property GetPKColumnName() As String
            Get
                Return Me.PK_ColumnName
            End Get
        End Property

        Public Function SetPKColumnName(PK_Col As String) As FunR
            Me.PK_ColumnName = PK_Col
            Return FunR.Succeded
        End Function

        Public Function AddColumn(ColName As String, ColType As SqlGenerator.SQLDataTypesEnum, Optional IsPKCol As Boolean = False) As FunR
            Me.ColumnsHeaders.Add(ColName)
            Me.ColumnsTypes.Add(ColType)
            Dim cmdString As String = SqlGenerator.InsertNewColum(Me.Name, ColName, SqlGenerator.GetSqlDataType(ColType))
            Try
                If IsPKCol Then Me.SetPKColumnName(ColName)
                Return dbConnector.ExecuteCommands(cmdString)
            Catch ex As Exception
                MsgBox(ex.Message)
                Return FunR.Exception
            End Try
        End Function

        Public Function DeleteColumn(ColName As String) As FunR
            Dim ColIndex As Integer
            Dim cmdString As String

            ColIndex = Me.ColumnsHeaders.IndexOf(ColName)
            Me.ColumnsHeaders.RemoveAt(ColIndex)
            Me.ColumnsTypes.RemoveAt(ColIndex)
            cmdString = SqlGenerator.DropColumn(Me.Name, ColName)

            Try
                Return dbConnector.ExecuteCommands(cmdString)
            Catch ex As Exception
                MsgBox(ex.Message)
                Return FunR.Exception
            End Try
        End Function

        Public Function DeleteColumn(ColIndex As Integer) As FunR
            Dim cmdString As String
            Dim ColName As String

            ColName = Me.ColumnsHeaders(ColIndex)
            Me.ColumnsHeaders.RemoveAt(ColIndex)
            Me.ColumnsTypes.RemoveAt(ColIndex)
            cmdString = SqlGenerator.DropColumn(Me.Name, ColName)

            Try
                Return dbConnector.ExecuteCommands(cmdString)
            Catch ex As Exception
                MsgBox(ex.Message)
                Return FunR.Exception
            End Try
        End Function

        Public Function AddNewRow(Values() As String) As FunR
            ' Validate
            If Values.Count <> Me.ColumnsHeaders.Count Then
                MsgBox("Error 1: Passed Values not equal Columns at Table: " & Me.Name)
                Return FunR.Exception
            End If

            ' Get insert statment
            Dim cmdString As String
            Dim Counter As Integer

            Try
                For Counter = 0 To Me.ColumnsHeaders.Count - 1
                    cmdString = SqlGenerator.InsertIntoStatment(Me.Name, Me.ColumnsHeaders(Counter), Values(Counter))
                    dbConnector.ExecuteCommands(cmdString)
                Next
                Return FunR.Succeded
            Catch ex As Exception
                MsgBox(ex.Message)
                Return FunR.Exception
            End Try
        End Function

        Public Function UpdateRow(Values() As String, PK_Value As String) As FunR
            ' Validate
            If Values.Count <> Me.ColumnsHeaders.Count Then
                MsgBox("Error 1: Passed Values not equal Columns at Table")
                Return FunR.Exception
            End If

            ' Get insert statment
            Dim cmdString As String
            Try
                cmdString = SqlGenerator.UpdateStatment(Me.Name, Me.ColumnsHeaders, Values, Me.PK_ColumnName, PK_Value)
                Return dbConnector.ExecuteCommands(cmdString)
            Catch ex As Exception
                MsgBox(ex.Message)
                Return FunR.Exception
            End Try
        End Function

        Public Function DeleteRow(PK_Value As String) As FunR
            ' Get insert statment
            Dim cmdString As String
            Try
                cmdString = SqlGenerator.DeleteStatment(Me.Name, Me.PK_ColumnName, PK_Value)
                Return dbConnector.ExecuteCommands(cmdString)
            Catch ex As Exception
                MsgBox(ex.Message)
                Return FunR.Exception
            End Try
        End Function

        Public Function FillTable() As FunR
            Dim sqlString As String = SqlGenerator.SelectStatment(Me.Name)
            Me.ClearTable()
            Return dbConnector.ExecuteSelectCommand(sqlString, Me.MyTable)
        End Function

        Private Sub ClearTable()
            Me.MyTable.Reset()
        End Sub

        Public Function BoundDGV(ByRef dgvDisplay As DataGridView) As FunR
            'ToDo: Set DataGrid view parameters
            Me.FillTable()
            With dgvDisplay
                .Columns.Clear()
                .Rows.Clear()
                .AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader)
            End With

            For Each DataColumn As DataColumn In MyTable.Columns()
                dgvDisplay.Columns.Add(DataColumn.ColumnName, DataColumn.ColumnName)
            Next
            For Each DataRow As DataRow In MyTable.Rows
                dgvDisplay.Rows.Add(DataRow)
            Next
            Return FunR.Exception
        End Function

        Public Function DataSourceBinding(ByRef DataSource As BindingSource) As FunR
            'ToDo: Set DataGrid view parameters
            Me.FillTable()
            DataSource.DataSource = MyTable
            Return FunR.Succeded
        End Function

        Public Function BoundTextBox(ColumnIndex As Integer, PK_Value As String, ByRef txtDisplay As TextBox) As FunR
            'ToDo: Bind Textbox to table logic
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Function used to validate Table stored info (column names, count, types, rows count) against actual
        ''' </summary>
        ''' <returns></returns>
        Public Function ValidateTableInfo() As FunR
            Me.FillTable()
            Me.RowsCount = MyTable.Rows.Count
            Me.ColumnsHeaders.Clear()
            'ToDo: Add logic for column data types verification
            'Me.ColumnsTypes.Clear()
            For Each DataColumn As DataColumn In MyTable.Columns
                Me.ColumnsHeaders.Add(DataColumn.ColumnName)
                'Me.ColumnsTypes.Add(DataColumn.DataType)
            Next
            Return FunR.Exception
        End Function
    End Class

End Namespace


