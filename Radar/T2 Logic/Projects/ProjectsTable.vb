


Namespace Radar
    Public Class ProjectsTable
        Inherits TableHandler


        Public Sub New()
            MyBase.New("Projects_Table")

        End Sub

        Public Function CreateTableFromScratch() As FunR
            Me.FillTable()
            Me.ValidateTableInfo()
            If Me.GetColumnsCount <> 0 Then Return FunR.Exception
            Me.AddColumn(Project.Project_ID_Name, SqlGenerator.SQLDataTypesEnum.SQL_INTEGER, True)
            Me.AddColumn(Project.Project_Opp_ID_Name, SqlGenerator.SQLDataTypesEnum.SQL_INTEGER)
            Me.AddColumn(Project.Project_SAP_Def_Name, SqlGenerator.SQLDataTypesEnum.VARCHAR_50)
            Me.AddColumn(Project.Project_Plant_ID_Name, SqlGenerator.SQLDataTypesEnum.SQL_INTEGER)
            Me.AddColumn(Project.Project_Team_ID_Name, SqlGenerator.SQLDataTypesEnum.SQL_INTEGER)
            Me.AddColumn(Project.Project_Status_Name, SqlGenerator.SQLDataTypesEnum.VARCHAR_50)
            Me.AddColumn(Project.Project_Title_Name, SqlGenerator.SQLDataTypesEnum.VARCHAR_100)
            Me.AddColumn(Project.Project_EPC_Name, SqlGenerator.SQLDataTypesEnum.VARCHAR_50)
            Me.AddColumn(Project.Project_SubCon_Name, SqlGenerator.SQLDataTypesEnum.VARCHAR_50)
            Return FunR.Succeded
        End Function
    End Class
End Namespace


