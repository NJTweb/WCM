Public Class SEWOController
    Inherits FormController

    Protected Const formName = "SEWO"
    Protected Const contacts As String = "hooks@njt-na.com, phelps@njt-na.com, pittam@mayco-mi.com, paul@mayco-mi.com, marshallja@jvisusallc.com; gwilloughby@mayco-mi.com"
    ' GET: /WCC/[Sub]
    Overrides Function Open(Optional ByVal ID As Long? = DEFAULT_ID) As ActionResult

        Dim tForm As New BasicForm(safetyCon, "SEWONumber", 0, formName, "ESEWOs")
        tForm.ID = Convert.ToInt64(If(ID = DEFAULT_ID, tForm.getMaxID() + 1, ID))
        tForm.setValues()

        Dim dateFields As New List(Of String) From {"InjuryDate", "EmployeeDate", "TeamLeadDate", "SupervisorDate", "DeptManagerDate", "SafetyMgrDate", "PlantMgrDate", "WitnessDate", "InjuredDate"}

        For Each dateField As String In dateFields
            tForm.Fields(dateField) = specialDateParse(Convert.ToString(tForm.Fields(dateField)))
        Next

        Dim timeFields As New List(Of String) From {"InjuryTime"}

        For Each timeField As String In timeFields
            tForm.Fields(timeField) = specialTimeParse(Convert.ToString(tForm.Fields(timeField)))
        Next

        With ViewData
            .Item("Departments") = getDepartments()
            .Item("Action") = If(ID = DEFAULT_ID, "Submit", "Update")
            .Item("RootCauses") = New List(Of String) From {"Unsafe Condition", "Unsafe Act"}
            .Item("Shifts") = New List(Of String) From {"1", "2", "3"}
            .Item("AccidentTypes") = New List(Of String) From {"Severe", "Lost Time", "Recordable", "First Aid", "Near Miss"}
            .Item("InjuryTypes") = New List(Of String) From {"Bruise,Contusion,Abrasion", "Fracture,Dislocation,Crush", "Amputation", "Laceration/Puncture", "Strain/Sprain[Non-Repetative]", "Musculoskeletal Illness", "Occupational Skin Disease", "Foreign Body", "Burn", "Noise Induced Hearing Loss", "Infection", "Injury Not Listed"}
            .Item("BodyParts") = New List(Of String) From {"Eye", "Head/ Neck", "Shoulder", "Upper Arm", "Chest", "Hand/ Finger/ Wrist", "Leg/ Knee", "Body Part Not Listed", "Elbow/ Forearm", "Lower Back", "Foot/ Ankle"}
            .Item("YesNo") = New List(Of String) From {"Yes", "No"}
            .Item("Sex") = New List(Of String) From {"Male", "Female"}
            .Item("PositionTypes") = New List(Of String) From {"Permanent", "Temporary"}
            .Item("AccidentTypeOptions") = getAccidentTypeOptions(tForm)
            .Item("DepartmentOptions") = getDepartmentOptions(tForm)
            .Item("SexOptions") = getSexOptions(tForm)
            .Item("ShiftOptions") = getShiftOptions(tForm)
            .Item("InjuryTypeOptions") = getInjuryTypeOptions(tForm)
            .Item("BodyPartOptions") = getBodyPartOptions(tForm)
            .Item("PPESuppliedOptions") = getPPESuppliedOptions(tForm)
            .Item("PPEInUseOptions") = getPPEInUseOptions(tForm)
            .Item("UsualWorkOptions") = getUsualWorkOptions(tForm)
            .Item("RootCauseOptions") = getRootCauseOptions(tForm)
            .Item("ExpansionPlanOptions") = getExpansionPlanOptions(tForm)
            .Item("PositionTypeOptions") = getPositionTypeOptions(tForm)
        End With

        Return View(tForm)
    End Function
    Overrides Function Submit() As ActionResult
        Dim tForm As New BasicForm(safetyCon, "SEWONumber", -1, formName, "ESEWOs")
        For Each input As String In Request.Form
            tForm.Fields(input) = Request.Form(input)
        Next
        For i As Integer = 0 To Request.Files.Count - 1
            tForm.HttpFiles.Add(Request.Files(i))
        Next
        tForm.submitValues()

        ViewData("Status") = "Form submitted."
        notify("hooks@njt-na.com", contacts, tForm.Name & " number " & tForm.ID & " submitted.", "A(n) " & tForm.Name & " was submitted on " & DateTime.Today.ToString("MM/dd/yyyy") & " at " & DateTime.Now.ToString("hh:mm tt"))
        Return View()
    End Function
    Overrides Function Update() As ActionResult
        Dim tForm As New BasicForm(safetyCon, "SEWONumber", 0, formName, "ESEWOs")
        For Each input As String In Request.Form
            tForm.Fields(input) = Request.Form(input)
        Next
        tForm.ID = CLng(tForm.Fields("ID"))
        For i As Integer = 0 To Request.Files.Count - 1
            tForm.HttpFiles.Add(Request.Files(i))
        Next
        tForm.updateValues()

        ViewData("Status") = "Form updated."
        notify("hooks@njt-na.com", contacts, tForm.Name & " number " & tForm.ID & " updated.", "A(n) " & tForm.Name & " was updated on " & DateTime.Today.ToString("MM/dd/yyyy") & " at " & DateTime.Now.ToString("hh:mm tt"))
        Return View()
    End Function

    Function getAccidentTypeOptions(model As BasicForm) As String
        Return loopGetSelected("AccidentTypes", Convert.ToString(model.Fields("AccidentType")))
    End Function
    Function getDepartmentOptions(model As BasicForm) As String
        Return loopGetSelected("Departments", Convert.ToString(model.Fields("Department")))
    End Function
    Function getSexOptions(model As BasicForm) As String
        Return loopGetSelected("Sex", Convert.ToString(model.Fields("Sex")))
    End Function
    Function getShiftOptions(model As BasicForm) As String
        Return loopGetSelected("Shifts", Convert.ToString(model.Fields("Shift")))
    End Function
    Function getInjuryTypeOptions(model As BasicForm) As String
        Return loopGetSelected("InjuryTypes", Convert.ToString(model.Fields("InjuryType")))
    End Function
    Function getBodyPartOptions(model As BasicForm) As String
        Return loopGetSelected("BodyParts", Convert.ToString(model.Fields("BodyPart")))
    End Function
    Function getPPESuppliedOptions(model As BasicForm) As String
        Return loopGetSelected("YesNo", Convert.ToString(model.Fields("PPESupplied")))
    End Function
    Function getPPEInUseOptions(model As BasicForm) As String
        Return loopGetSelected("YesNo", Convert.ToString(model.Fields("PPEInUse")))
    End Function
    Function getUsualWorkOptions(model As BasicForm) As String
        Return loopGetSelected("YesNo", Convert.ToString(model.Fields("UsualWork")))
    End Function
    Function getRootCauseOptions(model As BasicForm) As String
        Return loopGetSelected("RootCauses", Convert.ToString(model.Fields("RootCause")))
    End Function
    Function getExpansionPlanOptions(model As BasicForm) As String
        Return loopGetSelected("YesNo", Convert.ToString(model.Fields("ExpansionPlan")))
    End Function
    Function getPositionTypeOptions(model As BasicForm) As String
        Return loopGetSelected("PositionTypes", Convert.ToString(model.Fields("TypeOfPosition")))
    End Function
End Class