


Public Interface ITextDataBase

    Property FileLocation() As String

    Function IsFileExisits() As Boolean
    Function IsFileExisits(TextFileLocation As String) As Boolean

    Sub CreateFile()
    Sub CreateFile(TextFileLocation As String)

    Sub InsertLine(Line As String)
    Sub InsertLine(Line As List(Of String))







End Interface
