<!doctype html>
<html>
<head>
    @ModelType WCMApp.BasicForm
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>E-SEWO</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    @Scripts.Render("~/Scripts/js/jscolor/jscolor.js")
    @Scripts.Render("~/Scripts/js/AJAX.js")
    @Scripts.Render("~/Scripts/js/misc.js")
    @Scripts.Render("~/Scripts/js/wcm.js")
    @Scripts.Render("~/Scripts/js/SEWO.js")
    @Styles.Render("~/Content/normalize.css")
    @Styles.Render("~/Content/FormClasses.css")
    @Styles.Render("~/Content/Media.css")
</head>

<body>
    <form action="@ViewData("Action")" method="post" enctype="multipart/form-data" id="SEWO">
        <table>
            <tr>
                <td colspan="4" class="dkgrey whitetext"><h1>E-SEWO Report</h1></td>
                <td colspan="3" class="ltgrey"><h2>SEWO Number @Model.ID<input id="ID" type="hidden" name="SEWONumber" value="@Model.ID"></h2></td>
            </tr>
            <tr>
                <td rowspan="22" class="vert-text green whitetext">P<br>L<br>A<br>N</td>
                <td class="ltgrey">Accident Type</td>
                <td>
                    <select name="AccidentType" required>
                        <option value=""></option>
                        @Html.Raw(ViewData("AccidentTypeOptions"))
                    </select>
                </td>
                <td class="ltgrey">Sex</td>
                <td class="ltgrey">Position Type</td>
                <td>
                    <select name="Department" onchange="getZones(getSelectText(this));" required>
                        <option value="">Department</option>
                        @Html.Raw(ViewData("DepartmentOptions"))
                    </select>
                </td>
                <td>
                    <select name="ZZone" id="zone" onchange="getMachines(getSelectText(this))" required>
                        <option value="">Zone</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2"><input type="text" name="InjuredName" placeholder="Name of the injured" value="@Model.Fields("InjuredName")" required /></td>
                <td>
                    <select name="Sex">
                        <option value=""></option>
                        @Html.Raw(ViewData("SexOptions"))
                    </select>
                </td>
                <td>
                    <select name="TypeOfPosition">
                        <option value=""></option>
                        @Html.Raw(ViewData("PositionTypeOptions"))
                    </select>
                </td>
                <td colspan="2">
                    <select name="Machine" id="mach">
                        <option value="">Machine</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="text" name="InjuredJob" placeholder="Job of the injured" value="@Model.Fields("InjuredJob")"/>
                </td>
                <td>
                    <input type="text" name="ReportedBy" placeholder="Report made by" value="@Model.Fields("ReportedBy")" />
                </td>
                <td>
                    <select name="Shift" required>
                        <option value="">Shift</option>
                        @Html.Raw(ViewData("ShiftOptions"))
                    </select>
                </td>
                <td><input type="date" name="InjuryDate" value="@Model.Fields("InjuryDate")" required /></td>
                <td><input type="time" name="InjuryTime" placeholder="Time of incident" value="@Model.Fields("InjuryTime")" /></td>
            </tr>
            <tr>
                <td class="dkgreen whitetext">SW+1H Analysis(Event Description)</td>
                <td class="ltgrey">INJURY TYPE</td>
                <td colspan="2" class="dkgreen whitetext">BODY CHART</td>
                <td class="dkgreen whitetext">SKETCH</td>
                <td class="dkgreen whitetext">CORRECTIVE ACTION</td>
            </tr>
            <tr>
                <td><input type="text" name="WWhat" placeholder="WHAT(Nature and body part)" value="@Model.Fields("WWhat")" /></td>
                <td>
                    <select name="InjuryType" required>
                        <option value=""></option>
                        @Html.Raw(ViewData("InjuryTypeOptions"))
                    </select>
                </td>
                <td colspan="2" rowspan="6" height="200">
                    <img src="~/res/BodyChart.png" />
                    <select name="BodyPart" required>
                        <option value=""></option>
                        @Html.Raw(ViewData("BodyPartOptions"))
                    </select>
                </td>
                <td rowspan="6">
                    <button type="button" onclick="clearSketch()">Clear</button>
                    <button type="button">
                        <label for="colorPicker">Choose(Color)</label>
                    </button>
                    <input type="text" id="colorPicker" class="color" />
                    <label for="brushSize">Brush Size:</label>
                    <input type="text" id="brushSize" placeholder="Brush Size" value="2" />
                    <canvas id="sketchArea" onmousemove="drawSketch()" onmouseout="saveSketch()">Your browser does not support this sketch box.</canvas>
                    <input type="hidden" name="SketchURL" id="sketchUrl" value="@Model.Fields("SketchURL")" />
                </td>
                <td rowspan="6"><textarea name="ActionDescription">@Model.Fields("ActionDescription")</textarea><!-- corrective action --></td>
            </tr>
            <tr>
                <td><input type="text" name="WWhen" placeholder="WHEN(when did it happen)" value="@Model.Fields("WWhen")" /></td>
                <td>
                    <select name="PPESupplied">
                        <option value="">PPE Supplied?</option>
                        @Html.Raw(ViewData("PPESuppliedOptions"))
                    </select>
                </td>
            </tr>
            <tr>
                <td><input type="text" name="WWhere" placeholder="WHERE(Where is the job?)" value="@Model.Fields("WWhere")" /></td>
                <td>
                    <select name="PPEInUse">
                        <option value="">PPE in use?</option>
                        @Html.Raw(ViewData("PPEInUseOptions"))
                    </select>
                </td>
            </tr>
            <tr>
                <td><input type="text" name="WWho" placeholder="WHO(Who is doing the job?)" value="@Model.Fields("WWho")" /></td>
                <td>
                    <select name="UsualWork">
                        <option value="">Usual work?</option>
                        @Html.Raw(ViewData("UsualWorkOptions"))
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2"><input type="text" name="WWhich" placeholder="WHICH(Which kind of job?)" value="@Model.Fields("WWhich")" /></td>
            <tr>
                <td colspan="2"><input type="text" name="HHow" placeholder="HOW(How did this injury occur?)" value="@Model.Fields("HHow")" /></td>
            </tr>
            <tr>
                <td class="ltgreen whitetext" colspan="7">Analysis Root Cause: Write "5 Why's" for the most probable cause</td>
            </tr>
            <tr>
                <td colspan="7">
                    <input type="text" name="FiveWhy1" placeholder="Why?" value="@Model.Fields("FiveWhy1")" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <input type="text" name="FiveWhy2" placeholder="Why?" value="@Model.Fields("FiveWhy2")" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <input type="text" name="FiveWhy3" placeholder="Why?" value="@Model.Fields("FiveWhy3")" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <input type="text" name="FiveWhy4" placeholder="Why?" value="@Model.Fields("FiveWhy4")" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <input type="text" name="FiveWhy5" placeholder="Why?" value="@Model.Fields("FiveWhy5")" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <input type="text" name="FiveWhyRootCause" placeholder="Root cause" value="@Model.Fields("FiveWhyRootCause")" />
                </td>
            </tr>
            <tr>
                <td class="ltgreen whitetext" colspan="7">Categorize Root Cause</td>
            </tr>
            <tr>
                <td colspan="7">
                    <select name="RootCause" onchange="getSecondaryRootCauses(getSelectText(this))" required>
                        <option value="">Root Cause Type</option>
                        @Html.Raw(ViewData("RootCauseOptions"))
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <select name="SecondaryCause" id="secondary" onchange="getActionsAndMicros(getSelectText(this))" required>
                        <option value="">Secondary Root Cause</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <select name="MicroCause" id="micro" required>
                        <option value="">Micro Root Cause</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <select name="Action" id="actions" required>
                        <option value="">Action</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td rowspan="2" class="vert-text ltblue whitetext">D<br>O</td>
                <td colspan="2" class="dkblue whitetext">Action Plan</td>
                <td class="dkblue whitetext">RESPONSIBLE</td>
                <td class="dkblue whitetext">DUE DATE</td>
                <td class="dkblue whitetext">CLOSE DATE</td>
                <td class="dkblue whitetext">NOTES</td>
            </tr>
            <tr>
                <td colspan="2" height="100px"><textarea name="ActionPlan">@Model.Fields("ActionPlan")</textarea><!-- action plan --></td>
                <td><textarea name="Responsible">@Model.Fields("Responsible")</textarea><!-- responsible --></td>
                <td><input type="text" name="DueDate" value="@Model.Fields("DueDate")" /><!-- due date --></td>
                <td><input type="text" name="CloseDate" value="@Model.Fields("CloseDate")" /><!-- close date --></td>
                <td><textarea name="Notes">@Model.Fields("Notes")</textarea><!-- notes --></td>
            </tr>
            <tr>
                <td colspan="7">
                    <table>
                        <tr>
                            <td rowspan="9" class="vert-text red whitetext">C<br>H<br>E<br>C<br>K</td>
                            <td colspan="3" height="150">
                                RESULTS ACHEIVED WEEKLY
                                <!-- an embedded excel chart maybe, or move to dashboard -->
                            </td>
                            <td class="vert-text yellow">A<br>C<br>T</td>
                            <td valign="top">
                                <table height="150">
                                    <tr>
                                        <td>
                                            <select name="ExpansionPlan">
                                                <option value="">Expansion Plan</option>
                                                @Html.Raw(ViewData("ExpansionPlanOptions"))
                                            </select>
                                        </td>
                                        <td><input type="text" name="Location" placeholder="Location" value="@Model.Fields("Location")" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"><textarea name="Act" placeholder="Act">@Model.Fields("Act")</textarea></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <table>
                        <tr class="ltgrey">
                            <td></td>
                            <td>Employee</td>
                            <td>Team Leader</td>
                            <td>Supervisor</td>
                            <td>Mgr Depart.</td>
                            <td>Safety Mgr.</td>
                            <td>Plant Mgr.</td>
                        </tr>
                        <tr>
                            <td class="ltgrey">Name</td>
                            <td><input type="text" name="EmployeeName" value="@Model.Fields("EmployeeName")" /></td>
                            <td><input type="text" name="TeamLeadName" value="@Model.Fields("TeamLeadName")" /></td>
                            <td><input type="text" name="SupervisorName" value="@Model.Fields("SupervisorName")" /></td>
                            <td><input type="text" name="DeptManagerName" value="@Model.Fields("DeptManagerName")" /></td>
                            <td><input type="text" name="SafetyMgrName" value="@Model.Fields("SafetyMgrName")" /></td>
                            <td><input type="text" name="PlantMgrName" value="@Model.Fields("PlantMgrName")" /></td>
                        </tr>
                        <tr>
                            <td class="ltgrey">Date</td>
                            <td><input type="date" name="EmployeeDate" value="@Model.Fields("EmployeeDate")" /></td>
                            <td><input type="date" name="TeamLeadDate" value="@Model.Fields("TeamLeadDate")" /></td>
                            <td><input type="date" name="SupervisorDate" value="@Model.Fields("SupervisorDate")" /></td>
                            <td><input type="date" name="DeptManagerDate" value="@Model.Fields("DeptManagerDate")" /></td>
                            <td><input type="date" name="SafetyMgrDate" value="@Model.Fields("SafetyMgrDate")" /></td>
                            <td><input type="date" name="PlantMgrDate" value="@Model.Fields("PlantMgrDate")" /></td>
                        </tr>
                        <tr>
                            <td class="ltgrey">Signature</td>
                            <td><input type="text" name="EmployeeSignature" value="@Model.Fields("EmployeeSignature")" /></td>
                            <td><input type="text" name="TeamLeadSignature" value="@Model.Fields("TeamLeadSignature")" /></td>
                            <td><input type="text" name="SupervisorSignature" value="@Model.Fields("SupervisorSignature")" /></td>
                            <td><input type="text" name="DeptManagerSignature" value="@Model.Fields("DeptManagerSignature")" /></td>
                            <td><input type="text" name="SafetyMgrSignature" value="@Model.Fields("SafetyMgrSignature")" /></td>
                            <td><input type="text" name="PlantMgrSignature" value="@Model.Fields("PlantMgrSignature")" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="5"><h3>Injured Statement</h3></td>
                <td colspan="2"><h3>Witness Statement</h3></td>
            </tr>
            <tr>
                <td colspan="5"><textarea name="InjuredStatement" placeholder="Statement">@Model.Fields("InjuredStatement")</textarea></td>
                <td colspan="2"><textarea name="WitnessStatement" placeholder="Statement">@Model.Fields("WitnessStatement")</textarea></td>
            </tr>
            <tr>
                <td colspan="3"><input type="text" name="InjuredSignature" placeholder="Signature" value="@Model.Fields("InjuredSignature")"></td>
                <td colspan="2"><input type="date" name="InjuredDate" value="@Model.Fields("InjuredDate")"></td>
                <td><input type="text" name="WitnessSignature" placeholder="Signature" value="@Model.Fields("WitnessSignature")"></td>
                <td><input type="date" name="WitnessDate" value="@Model.Fields("WitnessDate")"></td>
            </tr>
            <tr>
                <td colspan="7">
                    <input id="submitButton" class="button" type="submit" value="@ViewData("Action")"> <!-- If a form is opened, the update button is displayed, if the default page is opened, a submit button is displayed -->
                    <button class="button" type="button" onclick="location.href='Open'">Clear</button>
                    <button class="button" type="button" onclick="openForm()">Open</button>
                </td>
            </tr>
        </table>
    </form>
    <script>
        getZones("@Model.Fields("Department")");
        getMachines("@Model.Fields("ZZone")");
        getSecondaryRootCauses("@Model.Fields("RootCause")");
        getActionsAndMicros("@Model.Fields("SecondaryCause")");

        setValue("ZZone", "@Model.Fields("ZZone")");
        setValue("Machine", "@Model.Fields("Machine")");
        setValue("SecondaryCause", "@Model.Fields("SecondaryCause")");
        setValue("MicroCause", "@Model.Fields("MicroCause")");
        setValue("Action", "@Model.Fields("Action")");
        showSketch("@Model.Fields("SketchURL")")
    </script>
</body>
</html>