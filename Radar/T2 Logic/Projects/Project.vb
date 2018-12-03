

Namespace Radar
    Public Structure Project
        Public Project_ID As Integer
        Public Project_SAP_Def As String
        Public Project_Plant_ID As Integer
        Public Project_Team_ID As Integer
        Public Project_Opp_ID As Integer
        Public Project_Status As String
        Public Project_Title As String
        Public Project_EPC As String
        Public Project_SubCon As String
        Public Project_Total_Rev As Single
        Public Project_Total_Cost As Costs

        Public Shared Project_ID_Name As String = "ID"
        Public Shared Project_SAP_Def_Name As String = "SAP_Def"
        Public Shared Project_Plant_ID_Name As String = "Plant_ID"
        Public Shared Project_Team_ID_Name As String = "Team_ID"
        Public Shared Project_Opp_ID_Name As String = "Opp_ID"
        Public Shared Project_Status_Name As String = "Status"
        Public Shared Project_Title_Name As String = "Title"
        Public Shared Project_EPC_Name As String = "EPC"
        Public Shared Project_SubCon_Name As String = "SubCon"
    End Structure

    Public Structure Costs
        Public Planned_Cost As Single
        Public Actual_Cost As Single
        Public Expected_Cost As Single

        Public Shared Planned_Cost_Name As String = "Planned_Cost"
        Public Shared Actual_Cost_Name As String = "Actual_Cost"
        Public Shared Expected_Cost_Name As String = "Expected_Cost"
    End Structure
End Namespace

