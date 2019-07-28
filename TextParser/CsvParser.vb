



Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics



''' <summary>
''' Determines how empty lines are interpreted when reading CSV files.
''' These values do not affect empty lines that occur within quoted fields
''' or empty lines that appear at the end of the input file.
''' </summary>
Public Enum EmptyLineBehavior
    NoColumns
    EmptyColumn
    Ignore
    EndOfFile
End Enum

    ''' <summary>
    ''' Common base class for CSV reader and writer classes.
    ''' </summary>
    Public MustInherit Class CsvFileCommon

        ''' <summary>
        ''' These are special characters in CSV files. If a column contains any
        ''' of these characters, the entire column is wrapped in double quotes.
        ''' </summary>
        Protected SpecialChars() As Char = New Char() {Microsoft.VisualBasic.ChrW(44), Microsoft.VisualBasic.ChrW(34), vbCr, vbLf}

        ' Indexes into SpecialChars for characters with specific meaning
        Private Const DelimiterIndex As Integer = 0

        Private Const QuoteIndex As Integer = 1

        ''' <summary>
        ''' Gets/sets the character used for column delimiters.
        ''' </summary>
        Public Property Delimiter As Char
            Get
                Return Me.SpecialChars(DelimiterIndex)
            End Get
            Set
                Me.SpecialChars(DelimiterIndex) = Value
            End Set
        End Property

        ''' <summary>
        ''' Gets/sets the character used for column quotes.
        ''' </summary>
        Public Property Quote As Char
            Get
                Return Me.SpecialChars(QuoteIndex)
            End Get
            Set
                Me.SpecialChars(QuoteIndex) = Value
            End Set
        End Property
    End Class

    ''' <summary>
    ''' Class for reading from comma-separated-value (CSV) files
    ''' </summary>
    Public Class CsvFileReader
        Inherits CsvFileCommon
    'Implements IDisposable

    ' Private members
    Private Reader As StreamReader

        Private CurrLine As String

        Private CurrPos As Integer

        Private EmptyLineBehavior As EmptyLineBehavior

    ''' <summary>
    ''' Initializes a new instance of the CsvFileReader class for the
    ''' specified stream.
    ''' </summary>
    ''' <param name="stream">The stream to read from</param>
    ''' <param name="emptyLineBehavior">Determines how empty lines are handled</param>
    Public Sub New(ByVal reader As StreamReader, Optional ByVal emptyLineBehavior As EmptyLineBehavior = EmptyLineBehavior.NoColumns)
        MyBase.New
        Me.Reader = reader
        Me.EmptyLineBehavior = emptyLineBehavior
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the CsvFileReader class for the
    ''' specified stream.
    ''' </summary>
    ''' <param name="stream">The stream to read from</param>
    ''' <param name="emptyLineBehavior">Determines how empty lines are handled</param>
    Public Sub New(ByVal stream As Stream, Optional ByVal emptyLineBehavior As EmptyLineBehavior = EmptyLineBehavior.NoColumns)
        MyBase.New
        Me.Reader = New StreamReader(stream)
        Me.EmptyLineBehavior = emptyLineBehavior
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the CsvFileReader class for the
    ''' specified file path.
    ''' </summary>
    ''' <param name="path">The name of the CSV file to read from</param>
    ''' <param name="emptyLineBehavior">Determines how empty lines are handled</param>
    Public Sub New(ByVal path As String, Optional ByVal emptyLineBehavior As EmptyLineBehavior = EmptyLineBehavior.NoColumns)
        MyBase.New
        Me.Reader = New StreamReader(path)
        Me.EmptyLineBehavior = emptyLineBehavior
    End Sub

    Public Overloads Shared Function ReadAll(ByVal path As String, ByVal encoding As Encoding) As List(Of List)
            Dim sr = New StreamReader(path, encoding)
            Dim cfr = New CsvFileReader(sr)
            Dim dataGrid As List(Of List) = New List(Of List)
            If cfr.ReadAll(dataGrid) Then
                Return dataGrid
            End If

            Return Nothing
        End Function

        Public Overloads Function ReadAll(ByVal dataGrid As List(Of List)) As Boolean
            ' Verify required argument
            If (dataGrid Is Nothing) Then
                Throw New ArgumentNullException("dataGrid")
            End If

            Dim row As List(Of String) = New List(Of String)

            While Me.ReadRow(row)
                dataGrid.Add(New List(Of String)(row))

            End While

            Return True
        End Function

        ''' <summary>
        ''' Reads a row of columns from the current CSV file. Returns false if no
        ''' more data could be read because the end of the file was reached.
        ''' </summary>
        ''' <param name="columns">Collection to hold the columns read</param>
        Public Function ReadRow(ByVal columns As List(Of String)) As Boolean
            ' Verify required argument
            If (columns Is Nothing) Then
                Throw New ArgumentNullException("columns")
            End If

ReadNextLine:
            ' Read next line from the file
            Me.CurrLine = Me.Reader.ReadLine
            Me.CurrPos = 0
            ' Test for end of file
            If (Me.CurrLine Is Nothing) Then
                Return False
            End If

            ' Test for empty line
            If (Me.CurrLine.Length = 0) Then
                Select Case (Me.EmptyLineBehavior)
                    Case Me.EmptyLineBehavior.NoColumns
                        columns.Clear()
                        Return True
                    Case Me.EmptyLineBehavior.Ignore
                        GoTo ReadNextLine
                    Case Me.EmptyLineBehavior.EndOfFile
                        Return False
                End Select

            End If

            ' Parse line
            Dim column As String
            Dim numColumns As Integer = 0

            While True
                ' Read next column
                If ((Me.CurrPos < Me.CurrLine.Length) _
                            AndAlso (Me.CurrLine(Me.CurrPos) = Quote)) Then
                    column = Me.ReadQuotedColumn
                Else
                    column = Me.ReadUnquotedColumn
                End If

                ' Add column to list
                If (numColumns < columns.Count) Then
                    columns(numColumns) = column
                Else
                    columns.Add(column)
                End If

                numColumns = (numColumns + 1)
                ' Break if we reached the end of the line
                If ((Me.CurrLine Is Nothing) _
                            OrElse (Me.CurrPos = Me.CurrLine.Length)) Then
                    Exit While
                End If

                ' Otherwise skip delimiter
                Debug.Assert((Me.CurrLine(Me.CurrPos) = Delimiter))
                Me.CurrPos = (Me.CurrPos + 1)

            End While

            ' Remove any unused columns from collection
            If (numColumns < columns.Count) Then
                columns.RemoveRange(numColumns, (columns.Count - numColumns))
            End If

            ' Indicate success
            Return True
        End Function

        ''' <summary>
        ''' Reads a quoted column by reading from the current line until a
        ''' closing quote is found or the end of the file is reached. On return,
        ''' the current position points to the delimiter or the end of the last
        ''' line in the file. Note: CurrLine may be set to null on return.
        ''' </summary>
        Private Function ReadQuotedColumn() As String
            ' Skip opening quote character
            Debug.Assert(((Me.CurrPos < Me.CurrLine.Length) _
                            AndAlso (Me.CurrLine(Me.CurrPos) = Quote)))
            Me.CurrPos = (Me.CurrPos + 1)
            ' Parse column
            Dim builder As StringBuilder = New StringBuilder

            While True

                While (Me.CurrPos = Me.CurrLine.Length)
                    ' End of line so attempt to read the next line
                    Me.CurrLine = Me.Reader.ReadLine
                    Me.CurrPos = 0
                    ' Done if we reached the end of the file
                    If (Me.CurrLine Is Nothing) Then
                        Return builder.ToString
                    End If

                    ' Otherwise, treat as a multi-line field
                    builder.Append(Environment.NewLine)

                End While

                ' Test for quote character
                If (Me.CurrLine(Me.CurrPos) = Quote) Then
                    ' If two quotes, skip first and treat second as literal
                    Dim nextPos As Integer = (Me.CurrPos + 1)
                    If ((nextPos < Me.CurrLine.Length) _
                                AndAlso (Me.CurrLine(nextPos) = Quote)) Then
                        Me.CurrPos = (Me.CurrPos + 1)
                    Else
                        Exit While
                    End If

                    ' Single quote ends quoted sequence
                End If

            ' Add current character to the column

            builder.Append(Me.CurrLine(CurrPos))  'builder.Append(Me.CurrLine(CurrPos + +))
        End While

            If (Me.CurrPos < Me.CurrLine.Length) Then
                ' Consume closing quote
                Debug.Assert((Me.CurrLine(Me.CurrPos) = Quote))
                Me.CurrPos = (Me.CurrPos + 1)
                ' Append any additional characters appearing before next delimiter
                builder.Append(Me.ReadUnquotedColumn)
            End If

            ' Return column value
            Return builder.ToString
        End Function

        ''' <summary>
        ''' Reads an unquoted column by reading from the current line until a
        ''' delimiter is found or the end of the line is reached. On return, the
        ''' current position points to the delimiter or the end of the current
        ''' line.
        ''' </summary>
        Private Function ReadUnquotedColumn() As String
            Dim startPos As Integer = Me.CurrPos
            Me.CurrPos = Me.CurrLine.IndexOf(Delimiter, Me.CurrPos)
            If (Me.CurrPos = -1) Then
                Me.CurrPos = Me.CurrLine.Length
            End If

            If (Me.CurrPos > startPos) Then
                Return Me.CurrLine.Substring(startPos, (Me.CurrPos - startPos))
            End If

            Return String.Empty
        End Function

        ' Propagate Dispose to StreamReader
        Public Sub Dispose()
            Me.Reader.Dispose()
        End Sub
    End Class

    ''' <summary>
    ''' Class for writing to comma-separated-value (CSV) files.
    ''' </summary>
    Public Class CsvFileWriter
        Inherits CsvFileCommon
        Implements IDisposable

        ' Private members
        Private Writer As StreamWriter

        Private OneQuote As String = Nothing

        Private TwoQuotes As String = Nothing

        Private QuotedFormat As String = Nothing

        ''' <summary>
        ''' Initializes a new instance of the CsvFileWriter class for the
        ''' specified stream.
        ''' </summary>
        ''' <param name="stream">The stream to write to</param>
        Public Sub New(ByVal writer As StreamWriter)
            MyBase.New
            Me.Writer = writer
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the CsvFileWriter class for the
        ''' specified stream.
        ''' </summary>
        ''' <param name="stream">The stream to write to</param>
        Public Sub New(ByVal stream As Stream)
            MyBase.New
            Me.Writer = New StreamWriter(stream)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the CsvFileWriter class for the
        ''' specified file path.
        ''' </summary>
        ''' <param name="path">The name of the CSV file to write to</param>
        Public Sub New(ByVal path As String)
            MyBase.New
            Me.Writer = New StreamWriter(path)
        End Sub

        Public Overloads Shared Sub WriteAll(ByVal dataGrid As List(Of List), ByVal path As String, ByVal encoding As Encoding)
            Dim sw = New StreamWriter(path, False, encoding)
            Dim cfw = New CsvFileWriter(sw)
            For Each row In dataGrid
                cfw.WriteRow(row)
            Next
        End Sub

        Public Overloads Sub WriteAll(ByVal dataGrid As List(Of List))
            For Each row As List(Of String) In dataGrid
                Me.WriteRow(row)
            Next
        End Sub

        ''' <summary>
        ''' Writes a row of columns to the current CSV file.
        ''' </summary>
        ''' <param name="columns">The list of columns to write</param>
        Public Sub WriteRow(ByVal columns As List(Of String))
            ' Verify required argument
            If (columns Is Nothing) Then
                Throw New ArgumentNullException("columns")
            End If

            If ((Me.OneQuote Is Nothing) _
                        OrElse (Me.OneQuote(0) <> Quote)) Then
                Me.OneQuote = String.Format("{0}", Quote)
                Me.TwoQuotes = String.Format("{0}{0}", Quote)
                Me.QuotedFormat = String.Format("{0}{{0}}{0}", Quote)
            End If

            ' Write each column
            Dim i As Integer = 0
            Do While (i < columns.Count)
                ' Add delimiter if this isn't the first column
                If (i > 0) Then
                    Me.Writer.Write(Delimiter)
                End If

                ' Write this column
                If (columns(i).IndexOfAny(SpecialChars) = -1) Then
                    Me.Writer.Write(columns(i))
                Else
                    Me.Writer.Write(Me.QuotedFormat, columns(i).Replace(Me.OneQuote, Me.TwoQuotes))
                End If

                i = (i + 1)
            Loop

            Me.Writer.Write("" & vbCrLf)
        End Sub

    ' Propagate Dispose to StreamWriter
    Public Sub Dispose() Implements IDisposable.Dispose
        Me.Writer.Dispose()
    End Sub
End Class

