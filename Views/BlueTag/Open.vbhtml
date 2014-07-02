<!DOCTYPE html>
<html>
<head>
    @ModelType WCMApp.BasicForm
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    @Scripts.Render("~/Scripts/js/AJAX.js")
    @Scripts.Render("~/Scripts/js/misc.js")
    @Scripts.Render("~/Scripts/js/wcm.js")
    @Scripts.Render("~/Scripts/js/Media.js")
    @Styles.Render("~/Content/normalize.css")
    @Styles.Render("~/Content/FormClasses.css")
    @Styles.Render("~/Content/h3normalize.css")
    @Styles.Render("~/Content/Media.css")
    <title>Blue Tag</title>
</head>

<body onload="mobileView()">
    <form action="@ViewData("Action")" method="post" enctype="multipart/form-data" id="UCAN">
        <table>
            <tr>
                <td colspan="5" class="ltblue whitetext"><h1>Blue Tag Number @Model.ID</h1><input id="ID" name="ID" type="hidden" value="@Model.ID" /></td>
            </tr>
            <tr>
                <td class="ltgrey">Tag#</td>
                <td class="ltgrey">WO#</td>
                <td class="ltgrey">Tag Type</td>
                <td class="ltgrey">Name</td>
            </tr>
            <tr>
                <td><input type="text" name="TagNo" placeholder="Tag Number" value="@Model.Fields("TagNo")" required/></td>
                <td><input type="text" name="WONo" value="@Model.Fields("WONo")" /></td>
                <td>
                    <select name="TagType">
                        <option value="">Tag Type</option>
                        @Html.Raw(ViewData("TagTypeOptions"))
                    </select>
                </td>
                <td><input type="text" name="Name" placeholder="Name" value="@Model.Fields("Name")" /></td>
            </tr>
            <tr>
                <td class="ltgrey">Date Opened</td>
                <td class="ltgrey">Shift</td>
                <td class="ltgrey">Department</td>
                <td class="ltgrey">Zone</td>
            </tr>
            <tr>
                <td><input type="date" name="OpenDate" placeholder="Open Date" value="@Model.Fields("OpenDate")" required /></td>
                <td>
                    <select name="Shift">
                        <option value="">Shift</option>
                        @Html.Raw(ViewData("ShiftOptions"))
                    </select>
                </td>
                <td>
                    <select name="Department" id="dept" onchange="getZones(getSelectText(this))">
                        <option value="">Department</option>
                        @Html.Raw(ViewData("DepartmentOptions"))
                    </select>
                </td>
                <td>
                    <select name="Zone" id="zone" onchange="getMachines(getSelectText(this))">
                        <option value="">Zone</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="ltgrey">Machine</td>
                <td class="ltgrey">Problem Type</td>
                <td class="ltgrey">Date Completed</td>
                <td class="ltgrey">Completed By</td>
            </tr>
            <tr>
                <td>
                    <select name="Machine" id="mach">
                        <option value="">Machine</option>
                    </select>
                </td>
                <td>
                    <select name="ProblemType">
                        <option value="">Problem Type</option>
                        @Html.Raw(ViewData("ProblemTypeOptions"))
                    </select>
                </td>
                <td><input type="date" name="CompletedDate" placeholder="Completed Date" value="@Model.Fields("CompletedDate")" /></td>
                <td><input type="text" name="CompletedBy" placeholder="Completed By" value="@Model.Fields("CompletedBy")" /></td>
            </tr>
            <tr>
                <td colspan="4" class="ltgrey">Details</td>
            </tr>
            <tr>
                <td colspan="4" height="100"><input type="text" name="Details" placeholder="Details" value="@Model.Fields("Details")" /></td>
            </tr>
            <tr>
                <td colspan="4">
                    <input id="submitButton" class="button" type="submit" value="@ViewData("Action")"> <!-- If a form is opened, the update button is displayed, if the default page is opened, a submit button is displayed -->
                    <button class="button" type="button" onclick="location.href='Open'">Clear</button>
                    <button class="button" type="button" onclick="openForm()">Open</button>
                </td>
            </tr>
        </table>
    </form>
    <script>
        getZones("@Model.Fields("Department")");
        getMachines("@Model.Fields("Zone")");

        setValue("Zone", "@Model.Fields("Zone")");
        setValue("Machine", "@Model.Fields("Machine")");
    </script>
</body>
</html>