﻿<!DOCTYPE html>
<html>
<head>
    @ModelType WCMApp.BasicForm
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    @Scripts.Render("~/Scripts/js/AJAX.js")
    @Scripts.Render("~/Scripts/js/misc.js")
    @Scripts.Render("~/Scripts/js/wcm.js")
    @Scripts.Render("~/Scripts/js/SEWO.js")
    @Scripts.Render("~/Scripts/js/Media.js")
    @Styles.Render("~/Content/normalize.css")
    @Styles.Render("~/Content/FormClasses.css")
    @Styles.Render("~/Content/h3normalize.css")
    @Styles.Render("~/Content/Media.css")
    <title>UCAN</title>
</head>

<body onload="mobileView()">
    <form action="@ViewData("Action")" method="post" enctype="multipart/form-data" id="UCAN">
        <table>
            <tr>
                <td colspan="3" class="dkgrey whitetext"><h1>Unsafe Conditions And Acts Form</h1></td>
                <td class="ltgrey"><h2>UCAN Number @Model.ID</h2><input id="ID" name="ID" type="hidden" value="@Model.ID" /></td>
            </tr>
            <tr>
                <td class="ltgrey"><h3>Type</h3></td>
                <td class="ltgrey"><h3>Plant</h3></td>
                <td class="ltgrey"><h3>Department</h3></td>
                <td class="ltgrey"><h3>Description of Incident</h3></td>
            </tr>
            <tr>
                <td>
                    <select name="UCANType" required>
                        <option value="">UCAN Type</option>
                        @Html.Raw(ViewData("UCANTypeOptions"))
                    </select>
                </td>
                <td>
                    <select name="Plant" id="plant" onchange="getDepartments(getSelectText(this))">
                        @Html.Raw(ViewData("PlantOptions"))
                    </select>
                </td>
                <td>
                    <select name="UCANDept" id="dept" onchange="getZones(getSelectText(this))" required>
                    </select>
                </td>
                <td rowspan="7"><input type="text" name="UCANDescription" placeholder="Description" value="@Model.Fields("UCANDescription")" required /></td>
            </tr>
            <tr>
                <td class="ltgrey"><h3>Reported By</h3></td>
                <td class="ltgrey"><h3>Location/Persons</h3></td>
                <td class="ltgrey"><h3>Zone</h3></td>
            </tr>
            <tr>
                <td><input type="text" name="UCANReportedBy" placeholder="Reported By" value="@Model.Fields("UCANReportedBy")" required /></td>
                <td rowspan="3"><input type="text" name="UCANPersons" placeholder="Location/Persons" value="@Model.Fields("UCANPersons")" required /></td>
                <td>
                    <select name="UCANZone" id="zone" onchange="getMachines(getSelectText(this))" required></select>
                </td>
            </tr>
            <tr>
                <td class="ltgrey"><h3>Safety Tag</h3></td>
                <td class="ltgrey"><h3>Machine</h3></td>
            </tr>
            <tr>
                <td>
                    <select name="IsSafetyTag" required>
                        <option value="">Is Safety Tag</option>
                        @Html.Raw(ViewData("SafetyTagOptions"))
                    </select>
                </td>
                <td>
                    <select name="UCANEquip" id="mach" required></select>
                </td>
            </tr>
            <tr>
                <td class="ltgrey"><h3>Completed</h3></td>
                <td class="ltgrey"><h3>Date</h3></td>
                <td class="ltgrey"><h3>Notes/Tag No.</h3></td>
            </tr>
            <tr>
                <td>
                    <select name="UCANCompleted" required>
                        <option value="">Completed</option>
                        @Html.Raw(ViewData("CompletedOptions"))
                    </select>
                </td>
                <td><input type="date" name="UCANDate" placeholder="Date Submitted" value="@Model.Fields("UCANDate")" required /></td>
                <td><input type="text" name="Notes" placeholder="Notes" value="@Model.Fields("Notes")" /></td>
            </tr>
            <tr>
                <td colspan="4" class="ltgrey"><h3>Action Plan</h3></td>
            </tr>
            <tr>
                <td colspan="4" height="100px"><input type="text" name="UCANActionPlan" placeholder="Action plan" value="@Model.Fields("UCANActionPlan")" required /></td>
            </tr>
            <tr>
                <td colspan="4">
                    <button type="submit" value="@ViewData("Action")">@ViewData("Action")</button>
                    <button type="button" onclick="location.href='Open'">Clear</button>
                    <button type="button" onclick="openForm()">Open</button>
                </td>
            </tr>
        </table>
    </form>
    <script>
        getDepartments("@Model.Fields("Plant")");
        getZones("@Model.Fields("UCANDept")");
        getMachines("@Model.Fields("UCANZone")");

        setValue("UCANDept", "@Model.Fields("UCANDept")");
        setValue("UCANZone", "@Model.Fields("UCANZone")");
        setValue("UCANEquip", "@Model.Fields("UCANEquip")");
    </script>
</body>
</html>