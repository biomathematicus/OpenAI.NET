Imports System.Text
Imports System.Data
Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Diagnostics

Public Class Connection
    Private m_oConn As SqlConnection
    Private DB_CONN_STRING As String = System.Configuration.ConfigurationManager.AppSettings("DB_CONN_STRING")

    '-----------------------------------------------------
    Public Function formatTextInput(ByVal sTxtToClean)
        If sTxtToClean Is Nothing Then
            Return ""
        End If
        sTxtToClean = sTxtToClean.ToString.Replace("'", "''")
        sTxtToClean = sTxtToClean.ToString.Replace("select", "sel&#101;ct")
        sTxtToClean = sTxtToClean.ToString.Replace("join", "jo&#105;n")
        sTxtToClean = sTxtToClean.ToString.Replace("union", "un&#105;on")
        sTxtToClean = sTxtToClean.ToString.Replace("where", "wh&#101;re")
        sTxtToClean = sTxtToClean.ToString.Replace("insert", "ins&#101;rt")
        sTxtToClean = sTxtToClean.ToString.Replace("delete", "del&#101;te")
        sTxtToClean = sTxtToClean.ToString.Replace("update", "up&#100;ate")
        sTxtToClean = sTxtToClean.ToString.Replace("like", "lik&#101;")
        sTxtToClean = sTxtToClean.ToString.Replace("drop", "dro&#112;")
        sTxtToClean = sTxtToClean.ToString.Replace("create", "cr&#101;ate")
        sTxtToClean = sTxtToClean.ToString.Replace("modify", "mod&#105;fy")
        sTxtToClean = sTxtToClean.ToString.Replace("rename", "ren&#097;me")
        sTxtToClean = sTxtToClean.ToString.Replace("alter", "alt&#101;r")
        sTxtToClean = sTxtToClean.ToString.Replace("cast", "ca&#115;t")
        sTxtToClean = sTxtToClean.ToString.Replace("pass", "pa&#115;s")
        sTxtToClean = sTxtToClean.ToString.Replace("code", "co&#100;e")
        'sTxtToClean = sTxtToClean.ToString.Replace("author", "aut&#104;or")
        sTxtToClean = sTxtToClean.ToString.Replace("username", "usernam&#101;")
        Return sTxtToClean
    End Function

    '-----------------------------------------------------
    Public Function formatSQLInput(ByVal sTxtToClean)
        If sTxtToClean Is Nothing Then
            Return ""
        End If
        sTxtToClean = formatTextInput(sTxtToClean)
        sTxtToClean = sTxtToClean.ToString.Replace("<", "&lt;")
        sTxtToClean = sTxtToClean.ToString.Replace(">", "&gt;")
        sTxtToClean = sTxtToClean.ToString.Replace("""", "")
        sTxtToClean = sTxtToClean.ToString.Replace("=", "&#061;")
        Return sTxtToClean
    End Function

    '-----------------------------------------------------
    Public Function DataReaderOpen(ByVal sSQL As String) As SqlDataReader
        'Open the data connection
        m_oConn = New SqlConnection(DB_CONN_STRING)
        m_oConn.Open()
        'Create a command object
        Dim oCommand As SqlCommand
        'Load the command object
        oCommand = New SqlCommand(sSQL, m_oConn)
        oCommand.CommandTimeout = 0
        'Load the datareader
        DataReaderOpen = oCommand.ExecuteReader()
    End Function

    '-----------------------------------------------------
    Public Function DataConnClose() As Boolean
        'Closes the connection
        Try
            m_oConn.Close()
            DataConnClose = True
        Catch ex As Exception
            DataConnClose = False
        End Try
    End Function

    '-----------------------------------------------------
    Public Function DBXML(ByVal sSQL As String) As String
        Dim sXML As New StringBuilder
        'Create a datareader
        Dim oDataReader As SqlDataReader
        'Load the datareader
        oDataReader = DataReaderOpen(sSQL)
        While oDataReader.Read()
            sXML.Append(oDataReader("lines"))
        End While
        'Close the datareader/db connection
        oDataReader.Close()
        DataConnClose()
        Return sXML.ToString
    End Function

    '-----------------------------------------------------
    Public Sub DataSetFromDataReader(ByVal ds As DataSet, ByVal table As String, _
        ByVal dr As IDataReader)
        ' Create an DataAdapter of the same type as the DataReader
        Dim drType As Type = CObj(dr).GetType
        Dim typename As String = drType.FullName.Replace("DataReader", "DataAdapter")
        Dim daType As Type = drType.Assembly.GetType(typename)
        Dim da As Object = Activator.CreateInstance(daType)
        ' invoke the protected Fill method that takes an IDataReader object
        Dim args() As Object = {ds, table, dr, 0, 999999}
        daType.InvokeMember("Fill", BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or _
           BindingFlags.Instance, Nothing, da, args)
        ' close the DataReader
        dr.Close()
    End Sub

End Class

