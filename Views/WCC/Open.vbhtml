<!DOCTYPE html>
<html lang="en">
<head>
    @ModelType WCMApp.BasicForm
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="http://fonts.googleapis.com/css?family=Ubuntu" rel="stylesheet" type="text/css">
    @Scripts.Render("~/Scripts/js/AJAX.js")
    @Scripts.Render("~/Scripts/js/misc.js")
    @Scripts.Render("~/Scripts/js/wcm.js")
    @Styles.Render("~/Content/Checklist.css")
    @Styles.Render("~/Content/WCC.css")
    @Styles.Render("~/Content/Media.css")
    <title>WCM Safety Checklist</title>
</head>

<body>
    <form action="@ViewData("Action")" method="post" enctype="multipart/form-data" id="WCC">
        <!-- If a form is opened, the update script is used, if the default page is opened (No ID), the submit script is used -->
        <table>
            <tr>
                <td colspan="3"><h1>Work Cell Observation Checklist No. @(Model.ID)<input id="ID" type="hidden" name="ID" value="@Model.ID"></h1></td>
            </tr>
            <tr>
                <td colspan="3">
                    <div class="inputPlusLabel">
                        <strong>Date</strong>
                        @Code
                            @<input type="date" name="WCDate" placeholder="Date created" value="@DateTime.Parse(Model.Fields("WCDate")).ToString("yyyy-MM-dd")" required>
                        End Code
                    </div>
                    <div class="inputPlusLabel">
                        <strong>Shift</strong>
                        <select name="Shift" required>
                            <option value="">Shift</option>
                            @For Each shift As String In ViewData("Shifts")
                                Dim selected As Boolean = (shift = Model.Fields("Shift"))
                                @<option value="@shift" @IIf(selected, "selected", "")>@shift</option>
                            Next
                        </select>
                    </div>
                    <div class="inputPlusLabel">
                        <strong>Auditor's Name</strong>
                        <select name="AuditorName" id="AuditorName" required>
                            <option value="">Auditor Name</option>
                            @For Each auditor As String In ViewData("Auditors")
                                Dim selected As Boolean = (auditor = Model.Fields("AuditorName"))
                                @<option value="@auditor" @IIf(selected, "selected", "")>@auditor</option>
                            Next
                        </select>
                    </div>
                    <div class="inputPlusLabel">
                        <strong>Plant</strong>
                        <select name="Plant" onchange="getDepartments(getSelectText(this));" required>
                            @Html.Raw(ViewData("PlantOptions"))
                        </select>
                    </div>
                    <div class="inputPlusLabel">
                        <strong>Department</strong>
                        <select name="Department" id="dept" onchange="getZonesAndWorkCells(getSelectText(this));" required>
                        </select>
                    </div>
                    <div class="inputPlusLabel">
                        <strong>Zone</strong>
                        <select name="Zone" id="zone" onchange="getMachines(getSelectText(this));" required></select>
                    </div>
                    <div class="inputPlusLabel">
                        <strong>Machine</strong>
                        <select name="MachID" id="mach"></select>
                    </div>
                    <div class="inputPlusLabel">
                        <strong>Work Cell</strong>
                        <select name="WorkCell" id="workcell" required>
                            <!-- code to update at end of file -->
                        </select>
                    </div>
                    <div class="inputPlusLabel">
                        <strong>Tool No.</strong>
                        <select name="PartNum" id="tool">
                            <option value="">Tool Number</option>
                            @For Each tool As String In ViewData("Tools")
                                Dim selected As Boolean = (tool = Model.Fields("PartNum"))
                                @<option value="@tool" @IIf(selected, "selected", "")>@tool</option>
                            Next
                        </select>
                    </div>
                    <div class="inputPlusLabel">
                        <strong>Supervisor</strong>
                        <select name="Supervisor" id="supervisor" required>
                            <option value="">Supervisor</option>
                            @For Each supervisor As String In ViewData("Supervisors")
                                Dim selected As Boolean = (supervisor = Model.Fields("Supervisor"))
                                @<option value="@supervisor" @IIf(selected, "selected", "")>@supervisor</option>
                            Next
                        </select>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2"><h2>Requirement</h2></td>
                <td><h2>Select</h2></td>
            </tr>
            @For i As Integer = 1 To ViewData("Labels").Count
                @If ViewBag.Headers(i - 1) <> String.Empty Then
                    @<tr id="header"><td colspan="3"><h3>@ViewData("Headers")(i - 1)</h3></td></tr>
                End If
                @<text>
                    <tr>
                        <td>
                            <span>@ViewData("Labels")(i - 1)</span>
                        </td>
                        <td>
                            <label for="file@(i)"><img id="uploadimg@(i)" src="../../res/upload.png" alt="Upload"></label>
                            <input type="file" name="file@(i)" id="file@(i)" onchange="changeImage(this, document.getElementById('uploadimg@(i)'));">
                        </td>
                        <td>
                            <select name="Compliant@(i)" required>
                                @For Each compliant As String In ViewData("compliancy")
                                Dim selected As Boolean = (compliant = Model.Fields("Compliant" & i))
                                    @<option value="@compliant" @IIf(selected, "selected", "")>@compliant</option>
                                Next
                            </select>
                            <select name="Severity@(i)">
                                @For Each severity As String In ViewData("severity")
                                Dim selected As Boolean = (severity = Model.Fields("Severity" & i))
                                    @<option value="@severity" @IIf(selected, "selected", "")>@severity</option>
                                Next
                            </select>
                        </td>
                    </tr>
                </text>
            Next
            <tr>
                <td colspan="3"><h3>Comments</h3></td>
            </tr>
            <tr></tr> <!-- makes sure the next (last) row is blue, becaue of the nth-of-type rule-->
            <tr>
                <td colspan="2">
                    <textarea maxlength="1000" name="Comments" rows="6" cols="50" form="WCC" placeholder="Comments">@Model.Fields("Comments")</textarea><br />
                </td>
                <td>
                    <input class="button" type="submit" value="@ViewData("Action")"> <!-- If a form is opened, the update button is displayed, if the default page is opened, a submit button is displayed -->
                    <button class="button" type="button" onclick="location.href='Open'">Clear</button>
                    <button class="button" type="button" onclick="openForm()">Open</button>
                </td>
            </tr>
        </table>
    </form>
    <script>
        "use strict";
        //Set dropdowns
        getDepartments("@Model.Fields("Plant")");
        getZonesAndWorkCells("@Model.Fields("Department")"); //runs synchronously
        getMachines("@Model.Fields("Zone")");
        setValue("Department", "@Model.Fields("Department")");
        setValue("Zone", "@Model.Fields("Zone")");
        setValue("WorkCell", "@Model.Fields("WorkCell")");
        setValue("MachID", "@Model.Fields("MachID")");
        //ensure correct URL
        var STR_NOT_FOUND = -1;
        if (location.href.indexOf("/WCC/Open", 0) == STR_NOT_FOUND) {
            //when a user is routed to this page by default it doesn't show the full URL
            //so I redirect to that URL, so that relative links on the page will work
            location.href = "/WCC/Open";
        }
    </script>
</body>
</html>