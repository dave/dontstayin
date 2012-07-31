Imports System.Data.SqlClient
Public Class ScriptExecutionException
	Inherits Exception
	Dim _scriptName As String
	Dim _sql As String

	Sub New(ByVal scriptName As String, ByVal sql As String, ByVal ex As Exception)
		MyBase.New(ex.Message, ex)
		_sql = sql
		_scriptName = scriptName
	End Sub

	Public Overrides ReadOnly Property Message() As String
		Get
			Return "Error running " + _scriptName + vbNewLine + Me.InnerException.Message + vbNewLine + "in code: " + _sql
		End Get
	End Property



End Class
