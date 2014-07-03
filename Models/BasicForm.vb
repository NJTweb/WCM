Imports System.Data
Imports System.Data.SqlClient
Public Class BasicForm

    '==== BasicForm Class ====
    'In order to use this class, referential integrity must be ensured between 
    'the names of the input elements in the form, and the names of the fields 
    '(column headers) in the table that you are using. This class assumes a 
    '1-to-1 relationship between these two items such that Fields('Blah') 
    'contains the value of the column 'Blah' in the table 'Table' in the 
    'database connected to by ConStr. This is the only reason the code can be
    'simplified to such a degree.

    Private Property ConStr As String       'the string used to connect to the database through ADO.NET
    Private Property PrimaryKey As String   'indentifies the name of the primary key field in the table
    Public Property Name As String          'The name of the form, used when alerting the user or sending notification emails
    Private Property Table As String        'the name of the table updated by this form
    Public Property ID As Long

    'shorthand Property actually creates a private backing store called [_Property Name] with a getter and a setter
    'the shorthand is used when you don't need to validate the assignment or check that property access is ok
    'if you want to trigger events on get or set, or need to validate the values in assignment, use the full
    'get/set syntax. You can also control access levels individually for the get and set functions this way

    Public Fields As New Dictionary(Of String, Object)
    Public Property HttpFiles As New List(Of HttpPostedFileBase)

    Public Sub New(_ConStr As String, _PrimaryKey As String, _ID As Long, _Name As String, _Table As String)
        ConStr = _ConStr
        PrimaryKey = _PrimaryKey
        ID = _ID
        Name = _Name
        Table = _Table
    End Sub
    Public Sub setValues()
        Dim sqlCon As New SqlConnection(ConStr)
        sqlCon.Open()

        Dim sqlCmd As New SqlCommand("SELECT * FROM " & Table & " WHERE " & PrimaryKey & "=" & ID, sqlCon)
        'change: add parameters, insert values into parameters with sqlCmd.Parameters.Add(@param,sqldbtype.type).value = val
        Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader()
        If sqlReader.Read() Then
            For i As Integer = 0 To sqlReader.FieldCount - 1
                Fields(sqlReader.GetName(i)) = If(TypeOf sqlReader(i) Is DBNull, "", sqlReader(i))
            Next
        Else
            'Invalid record, populate with default values
            Dim schema As DataTable = sqlReader.GetSchemaTable()
            For Each row As DataRow In schema.Rows
                Select Case CType(row("DataType"), Type) 'Convert System.RuntimeType to Type
                    Case GetType(DateTime), GetType(Date)
                        Fields(Convert.ToString(row("ColumnName"))) = Date.Now.ToString("yyyy-MM-dd")
                    Case Else
                        Fields(Convert.ToString(row("ColumnName"))) = ""
                End Select
            Next
        End If

        sqlCon.Close()
    End Sub

    Public Sub submitValues()
        Dim queryStr As String = "INSERT INTO " & Table
        Dim columns As String = "", params As String = ""

        ID = Convert.ToInt64(Fields(PrimaryKey))

        Fields.Remove(PrimaryKey)
        For Each field As String In Fields.Keys
            columns &= field & ", "
            params &= "@" & field & ", "
        Next
        columns = " (" & columns.Substring(0, columns.Length - 2) & ")"
        params = " (" & params.Substring(0, params.Length - 2) & ")"
        queryStr &= columns & " VALUES " & params

        Dim sqlCon As New SqlConnection(ConStr)
        sqlCon.Open()

        Dim sqlCmd As New SqlCommand(queryStr, sqlCon)
        With sqlCmd.Parameters
            For Each field As String In Fields.Keys
                .AddWithValue("@" & field, Fields(field))
            Next
        End With

        sqlCmd.ExecuteNonQuery()
        sqlCon.Close()

        addOpenForm()

        saveFiles()
    End Sub

    Public Sub updateValues()
        Dim queryStr As String = "UPDATE " & Table & " SET "

        Fields.Remove(PrimaryKey)
        For Each field As String In Fields.Keys
            queryStr &= field & "=@" & field & ", "
        Next

        queryStr = queryStr.Substring(0, queryStr.Length - 2)
        queryStr &= " WHERE " & PrimaryKey & "=" & ID

        Dim sqlCon As New SqlConnection(ConStr)
        sqlCon.Open()

        Dim sqlCmd As New SqlCommand(queryStr, sqlCon)
        With sqlCmd.Parameters
            For Each field As String In Fields.Keys
                .AddWithValue("@" & field, Fields(field))
            Next
        End With

        sqlCmd.ExecuteNonQuery()
        sqlCon.Close()

        saveFiles()
    End Sub

    Private Sub saveFiles()
        For i As Integer = 0 To HttpFiles.Count - 1
            If Not String.IsNullOrEmpty(HttpFiles(i).FileName) Then
                HttpFiles(i).SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploaded/") & Name & "_" & ID & "_" & (i + 1) & "_" & HttpFiles(i).FileName)
            End If
        Next
    End Sub

    Public Function getMaxID() As Long
        Dim sqlCon As New SqlConnection(ConStr)
        sqlCon.Open()

        Dim sqlCmd As New SqlCommand("SELECT MAX(" & PrimaryKey & ") FROM " & Table, sqlCon)
        Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader()

        sqlReader.Read()

        getMaxID = If(TypeOf sqlReader(0) Is DBNull, 0, Convert.ToInt64(sqlReader(0))) 'old VB return syntax, helps set return value before closing connection

        sqlCon.Close()
    End Function

    Private Sub addOpenForm()
        Dim sqlCon As New SqlConnection(ConStr)
        sqlCon.Open()

        Dim sqlCmd As New SqlCommand("INSERT INTO Forms (Name, ID, Status, OpenDate) VALUES ('" & Name & "'," & ID & ",'Open','" & DateTime.Now.ToString("s") & "')", sqlCon)
        sqlCmd.ExecuteNonQuery()

        sqlCon.Close()
    End Sub
End Class