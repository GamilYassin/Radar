


Namespace Radar

    Public Class SqlGenerator

        ''' <summary>
        ''' Return column names seperated by ,
        ''' </summary>
        ''' <param name="ColumnsNames"></param>
        ''' <returns></returns>
        Public Shared Function ColumnNamesCombiner(ByVal ColumnsNames() As String) As String
            Return String.Join(", ", ColumnsNames)
        End Function

        ''' <summary>
        ''' Return where statment string column = value AND column = value ..
        ''' </summary>
        ''' <param name="ColumnsNames"></param>
        ''' <param name="Values"></param>
        ''' <param name="bolOperation"> If True means AND, else OR</param>
        ''' <returns></returns>
        Public Shared Function WhereConditionCombiner(ByVal ColumnsNames() As String, ByVal Values() As String, ByVal Optional bolOperation As Boolean = True) As String
            ' Validate column names and values
            Dim len As Integer = ColumnsNames.Length
            Dim Result As String = " "

            If len <> Values.Length Then Return ""
            If len > 1 Then
                For I As Integer = 0 To len - 1
                    Result += ColumnsNames(I) & " = '" & Values(I) & "'"
                    If I = len - 1 Then
                        Result += " "
                    Else
                        If bolOperation Then
                            Result += " AND "
                        Else
                            Result += " OR "
                        End If
                    End If
                Next
                Return Result
            Else
                Return ColumnsNames(0) & " = " & Values(0)
            End If
        End Function

#Region "SELECT Statment"
        ' SELECT Section
        '
        'SELECT  column1, column2, ...
        'FROM table_name;

        Public Shared Function SelectStatment(ByVal TableName As String) As String
            Return SelectStatment(TableName, "*")
        End Function

        ''' <summary>
        ''' Select statment
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnsNames"></param>
        ''' <returns>SELECT col1, col2, col3   FROM tableName </returns>
        Public Shared Function SelectStatment(ByVal TableName As String, ByVal ParamArray ColumnsNames() As String) As String
            Dim colsNames As String = String.Join(", ", ColumnsNames)

            Return "SELECT " & colsNames & " FROM " & TableName
        End Function

        ''' <summary>
        ''' The WHERE clause is used to filter records.
        ''' The WHERE clause Is used To extract only those records that fulfill a specified condition.
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnsNames"></param>
        ''' <param name="WhereConditions"></param>
        ''' <returns>SELECT column1, column2, ...
        '''                 FROM table_name
        '''                 WHERE condition
        ''' </returns>
        Public Shared Function SelectStatment(ByVal TableName As String, ByVal ColumnsNames As String, ByVal WhereConditions As String) As String
            ' TODO: verify Columns names and where statment strings for correct fomat str, str, str, 

            Return "SELECT " & ColumnsNames & " FROM " & TableName & " WHERE " & WhereConditions
        End Function


        ''' <summary>
        ''' The ORDER BY keyword is used to sort the result-set in ascending or descending order
        ''' The ORDER BY keyword sorts the records in ascending order by default. To sort the records in descending order, use the DESC keyword.
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnsNames"></param>
        ''' <param name="WhereConditions"></param>
        ''' <param name="OrderBy"></param>
        ''' <returns>
        ''' SELECT column1, column2, ...
        ''' FROM table_name
        ''' ORDER BY column1, column2, ... ASC|DESC
        ''' </returns>
        Public Shared Function SelectStatment(ByVal TableName As String, ByVal ColumnsNames As String, ByVal WhereConditions As String, ByVal OrderBy As String, ByVal bolAsc As Boolean) As String
            ' TODO: verify Columns names and where statment strings for correct fomat str, str, str, 
            Dim bolOrder As String = "DESC"

            If bolAsc Then bolOrder = "ASC"
            If WhereConditions = "" Then
                If OrderBy = "" Then
                    Return "SELECT " & ColumnsNames & " FROM " & TableName
                Else
                    Return "SELECT " & ColumnsNames & " FROM " & TableName & " ORDERBY " & OrderBy & " " & bolOrder
                End If
            Else
                If OrderBy = "" Then
                    Return "SELECT " & ColumnsNames & " FROM " & TableName & " WHERE " & WhereConditions
                Else
                    Return "SELECT " & ColumnsNames & " FROM " & TableName & " WHERE " & WhereConditions & " ORDERBY " & OrderBy & " " & bolOrder
                End If
            End If
        End Function

        ' The SELECT DISTINCT statement Is used to return only distinct (different) values.
        'Inside a table, a column often contains many duplicate values; And sometimes you only want To list the different (distinct) values.
        Public Shared Function SelectStatment(ByVal TableName As String, ByVal bolDistinct As Boolean, ByVal ParamArray ColumnsNames() As String) As String
            Dim colsNames As String = String.Join(", ", ColumnsNames)

            If bolDistinct Then
                Return "SELECT DISTINCT " & colsNames & " FROM " & TableName
            Else
                Return "SELECT " & colsNames & " FROM " & TableName
            End If
        End Function

        ''' <summary>
        ''' The MIN() function returns the smallest value of the selected column
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnName"></param>
        ''' <param name="WhereCondition"></param>
        ''' <returns>
        ''' SELECT MIN(column_name)
        ''' FROM table_name
        ''' WHERE condition
        ''' </returns>
        Public Shared Function SelectMinStatment(ByVal TableName As String, ByVal ColumnName As String, ByVal WhereCondition As String) As String
            If WhereCondition = "" Then
                Return "SELECT MIN(" & ColumnName & ") FROM " & TableName
            Else
                Return "SELECT MIN(" & ColumnName & ") FROM " & TableName & " WHERE " & WhereCondition
            End If
        End Function

        ''' <summary>
        ''' The MAX() function returns the smallest value of the selected column
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnName"></param>
        ''' <param name="WhereCondition"></param>
        ''' <returns>
        ''' SELECT MIN(column_name)
        ''' FROM table_name
        ''' WHERE condition
        ''' </returns>
        Public Shared Function SelectMaxStatment(ByVal TableName As String, ByVal ColumnName As String, ByVal WhereCondition As String) As String
            If WhereCondition = "" Then
                Return "SELECT MAX(" & ColumnName & ") FROM " & TableName
            Else
                Return "SELECT MAX(" & ColumnName & ") FROM " & TableName & " WHERE " & WhereCondition
            End If
        End Function

        ''' <summary>
        ''' The COUNT() function returns the number of rows that matches a specified criteria.
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnName"></param>
        ''' <param name="WhereCondition"></param>
        ''' <returns>
        ''' SELECT COUNT(column_name)
        ''' FROM table_name
        ''' WHERE condition
        ''' </returns>
        Public Shared Function SelectCountStatment(ByVal TableName As String, ByVal ColumnName As String, ByVal WhereCondition As String) As String
            If WhereCondition = "" Then
                Return "SELECT COUNT(" & ColumnName & ") FROM " & TableName
            Else
                Return "SELECT COUNT(" & ColumnName & ") FROM " & TableName & " WHERE " & WhereCondition
            End If
        End Function

        ''' <summary>
        ''' The SUM() function returns the total sum of a numeric column
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnName"></param>
        ''' <param name="WhereCondition"></param>
        ''' <returns>
        ''' SELECT SUM(column_name)
        ''' FROM table_name
        ''' WHERE condition
        ''' </returns>
        Public Shared Function SelectSumStatment(ByVal TableName As String, ByVal ColumnName As String, ByVal WhereCondition As String) As String
            If WhereCondition = "" Then
                Return "SELECT SUM(" & ColumnName & ") FROM " & TableName
            Else
                Return "SELECT SUM(" & ColumnName & ") FROM " & TableName & " WHERE " & WhereCondition
            End If
        End Function

        ''' <summary>
        ''' The AVG() function returns the average value of a numeric column
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnName"></param>
        ''' <param name="WhereCondition"></param>
        ''' <returns>
        ''' SELECT AVG(column_name)
        ''' FROM table_name
        ''' WHERE condition
        ''' </returns>
        Public Shared Function SelectAvgStatment(ByVal TableName As String, ByVal ColumnName As String, ByVal WhereCondition As String) As String
            If WhereCondition = "" Then
                Return "SELECT AVG(" & ColumnName & ") FROM " & TableName
            Else
                Return "SELECT AVG(" & ColumnName & ") FROM " & TableName & " WHERE " & WhereCondition
            End If
        End Function
#End Region

#Region "INSERT INTO Statment"

        ''' <summary>
        ''' Return Insert statment parameter (
        ''' </summary>
        ''' <param name="ColumnsNames"></param>
        ''' <param name="Values"></param>
        ''' <returns></returns>
        Public Shared Function InsertValuesCombiner(ByVal ColumnsNames() As String, ByVal Values() As String) As String
            Return String.Empty
        End Function

        ''' <summary>
        ''' The INSERT INTO statement is used to insert new records in a table
        ''' 
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnsName"></param>
        ''' <param name="Value"></param>
        ''' <returns>
        ''' INSERT INTO table_name (column1, column2, column3, ...)
        ''' VALUES (value1, value2, value3, ...)
        ''' </returns>
        Public Shared Function InsertIntoStatment(ByVal TableName As String, ByVal ColumnsName As String, ByVal Value As String) As String
            Return "INSERT INTO " & TableName & " ( " & ColumnsName & ") VALUES (" & Value & ")"
        End Function

#End Region

#Region "UPDATE Statement"

        ''' <summary>
        ''' The UPDATE statement is used to modify the existing records in a table.
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnsNames"></param>
        ''' <param name="Values"></param>
        ''' <param name="WhereConditionLeft"></param>
        ''' <param name="WhereConditionRight"></param>
        ''' <returns>
        ''' UPDATE table_name
        ''' SET column1 = value1, column2 = value2, ...
        ''' WHERE condition
        ''' </returns>
        Public Shared Function UpdateStatment(ByVal TableName As String, ByVal ColumnsNames() As String, Values() As String, ByVal WhereConditionLeft As String, ByVal WhereConditionRight As String) As String

            Dim Result As String = "UPDATE " & TableName & " SET "
            Dim Counter As Integer

            For Counter = 0 To ColumnsNames.Count - 1
                Result += ColumnsNames(Counter) & " = " & Values(Counter)
                If Counter = ColumnsNames.Count - 1 Then
                    If WhereConditionLeft = "" Then
                        Result += ";"
                    Else
                        Result += " WHERE " & WhereConditionLeft & " = " & WhereConditionRight & ";"
                    End If
                Else
                    Result += " , "
                End If
            Next
            Return Result
        End Function

        Public Shared Function UpdateStatment(ByVal TableName As String, ByVal ColumnsNames As List(Of String), Values() As String, Optional ByVal WhereConditionLeft As String = "", Optional ByVal WhereConditionRight As String = "") As String

            Dim Result As String = "UPDATE " & TableName & " SET "
            Dim Counter As Integer

            For Counter = 0 To ColumnsNames.Count - 1
                Result += ColumnsNames(Counter) & " = " & Values(Counter)
                If Counter = ColumnsNames.Count - 1 Then
                    If WhereConditionLeft = "" Then
                        Result += ";"
                    Else
                        Result += " WHERE " & WhereConditionLeft & " = " & WhereConditionRight & ";"
                    End If
                Else
                    Result += " , "
                End If
            Next
            Return Result
        End Function
#End Region

#Region "DELETE Statement"
        ''' <summary>
        ''' The DELETE statement is used to delete existing records in a table.
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="WhereConditionLeft"></param>
        ''' <param name="WhereConditionRight"></param>
        ''' <returns>
        ''' DELETE FROM table_name WHERE condition
        ''' </returns>
        Public Shared Function DeleteStatment(ByVal TableName As String, ByVal WhereConditionLeft As String, ByVal WhereConditionRight As String) As String
            Dim Result As String = "DELETE FROM " & TableName & " WHERE "
            Result += WhereConditionLeft & " = " & WhereConditionRight & ";"
            Return Result
        End Function

        ''' <summary>
        ''' It is possible to delete all rows in a table without deleting the table. This means that the table structure, attributes, and indexes will be intact
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <returns>
        ''' DELETE FROM table_name
        ''' </returns>
        Public Shared Function DeleteAllStatment(ByVal TableName As String) As String
            Return "DELETE FROM " & TableName
        End Function
#End Region

#Region " TABLE Statements"
        ''' <summary>
        ''' The CREATE TABLE statement is used to create a new table in a database
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnsInfo"></param>
        ''' <returns>
        ''' CREATE TABLE table_name (
        '''     column1 datatype,
        '''     column2 datatype,
        '''     column3 datatype,
        '''      )
        ''' </returns>
        Public Shared Function CreateTableStatment(ByVal TableName As String, ByVal ColumnsInfo As String) As String
            Return "CREATE TABLE " & TableName & "( " & ColumnsInfo & ")"
        End Function

        ''' <summary>
        ''' The DROP TABLE statement is used to drop an existing table in a database.
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <returns>
        ''' DROP TABLE table_name
        ''' </returns>
        Public Shared Function DeleteTableStatment(ByVal TableName As String) As String
            Return "CREATE TABLE " & TableName
        End Function

        ''' <summary>
        ''' The ALTER TABLE statement is used to add, delete, or modify columns in an existing table.
        ''' The ALTER TABLE statement Is also used To add And drop various constraints On an existing table
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColumnInfo"></param>
        ''' <param name="intAddDeleteModify">
        ''' 1 ADD
        ''' 2 DROP
        ''' 3 MODIFY
        ''' </param>
        ''' <returns>
        ''' 1- When Adding Column
        ''' ALTER TABLE table_name
        ''' ADD column_name datatype
        ''' 
        ''' 2- For column delete
        ''' ALTER TABLE table_name
        ''' DROP COLUMN column_name
        ''' 
        ''' 3- Modify
        ''' ALTER TABLE table_name
        ''' ALTER COLUMN column_name datatype
        ''' </returns>
        Public Shared Function AlterTableStatment(ByVal TableName As String, ByVal ColumnInfo As String, ByVal intAddDeleteModify As Boolean) As String
            If intAddDeleteModify = 1 Then ' Add column case
                Return "ALTER TABLE " & TableName & " ADD " & ColumnInfo
            ElseIf intAddDeleteModify = 2 Then
                Return "ALTER TABLE " & TableName & " DROP COLUMN " & ColumnInfo
            ElseIf intAddDeleteModify = 3 Then
                Return "ALTER TABLE " & TableName & " ALTER COLUMN " & ColumnInfo
            Else
                Return ""
            End If
        End Function
#End Region

#Region "ALTER TABLE Statement"
        ''' <summary>
        ''' ALTER TABLE Statement
        ''' To add a column, modify a column, drop a column, rename a column or rename a table
        ''' Add column in table
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColName"></param>
        ''' <param name="ColType"></param>
        ''' <returns>
        ''' ALTER TABLE table_name
        ''' ADD column_name column_definition;
        ''' </returns>
        Public Shared Function InsertNewColum(ByVal TableName As String, ByVal ColName As String, ByVal ColType As SQLDataTypesEnum) As String
            Dim Result As String = "ALTER TABLE " & TableName & " ADD "
            Dim sqlType As String = GetSqlType(ColType)
            Result += ColName & sqlType & ";"
            Return Result
        End Function


        ''' <summary>
        '''Drop column in table
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="ColName"></param>
        ''' <returns>
        ''' ALTER TABLE table_name
        ''' DROP COLUMN column_name
        ''' </returns>
        Public Shared Function DropColumn(ByVal TableName As String, ByVal ColName As String) As String
            Dim Result As String = "ALTER TABLE " & TableName & " DROP COLUMN " & ColName & ";"
            Return Result
        End Function

        Public Shared Function GetSqlType(sqlType As SQLDataTypesEnum) As String
            Select Case sqlType
                Case SQLDataTypesEnum.SQL_BOOLEAN
                    Return " BOOLEAN"
                Case SQLDataTypesEnum.SQL_DATE
                    Return " DATE"
                Case SQLDataTypesEnum.SQL_INTEGER
                    Return " INTEGER"
                Case SQLDataTypesEnum.SQL_REAL
                    Return " REAL"
                Case SQLDataTypesEnum.VARCHAR_100
                    Return " VARCHAR(100)"
                Case SQLDataTypesEnum.VARCHAR_1000
                    Return " VARCHAR(1000)"
                Case SQLDataTypesEnum.VARCHAR_200
                    Return " VARCHAR(200)"
                Case SQLDataTypesEnum.VARCHAR_50
                    Return " VARCHAR(50)"
                Case SQLDataTypesEnum.VARCHAR_500
                    Return " VARCHAR(500)"
                Case Else
                    Return " VARCHAR(100)"
            End Select
        End Function
#End Region

#Region "SQL Types Enum"
        Public Enum SQLDataTypesEnum As Integer
            VARCHAR_50
            VARCHAR_100
            VARCHAR_200
            VARCHAR_500
            VARCHAR_1000
            SQL_BOOLEAN
            SQL_INTEGER
            SQL_REAL
            SQL_DATE
        End Enum
#End Region
    End Class


    'SELECT  -extracts data from a database
    'UPDATE -updates data in a database
    'DELETE -deletes data from a database
    'INSERT INTO - inserts New data into a database

    'CREATE DATABASE - creates a New database
    'ALTER DATABASE - modifies a database
    'CREATE TABLE - creates a New table
    'ALTER TABLE - modifies a table
    'DROP TABLE - deletes a table
    'CREATE INDEX - creates an index (search key)
    'DROP INDEX - deletes an index
End Namespace
