
Public Class DependencyException
	Inherits Exception
	Private _dependencies As New System.Collections.Generic.List(Of String)
	Private ex As ScriptExecutionException
	Sub New(ByVal ex As ScriptExecutionException)
		Me.ex = ex
	End Sub
	Sub addDependency(ByVal objectName As String)
		Me._dependencies.Add(objectName)
	End Sub
	Function dependencies() As String()
		Return Me._dependencies.ToArray()
	End Function
	Public Overrides ReadOnly Property Message() As String
		Get
			Dim sb As New System.Text.StringBuilder
			sb.AppendLine("Missing dependencies:")
			For Each dependency As String In Me._dependencies
				sb.AppendLine("  " + dependency)
			Next
			sb.AppendLine(ex.Message)
			Return sb.ToString
		End Get
	End Property
End Class
