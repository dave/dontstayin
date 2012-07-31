Imports System.Collections.Generic
Public MustInherit Class ScriptSource
	Friend _functionScripts As New List(Of ObjectScript)
	Friend _procedureScripts As New List(Of ObjectScript)
	Friend _viewScripts As New List(Of ObjectScript)
	Friend _triggerScripts As New List(Of ObjectScript)
	Friend _updateScripts As New List(Of UpdateScript)
	Friend _tableDataScripts As New List(Of TableDataScript)

	Public MustOverride Sub FillTables()




	Public ReadOnly Property FunctionScripts() As System.Collections.Generic.List(Of ObjectScript)
		Get
			Return Me._functionScripts
		End Get
	End Property

	Public ReadOnly Property ProcedureScripts() As System.Collections.Generic.List(Of ObjectScript)
		Get
			Return Me._procedureScripts
		End Get
	End Property

	Public ReadOnly Property TableDataScripts() As System.Collections.Generic.List(Of TableDataScript)
		Get
			Return Me._tableDataScripts
		End Get
	End Property

	Public ReadOnly Property TriggerScripts() As System.Collections.Generic.List(Of ObjectScript)
		Get
			Return Me._triggerScripts
		End Get
	End Property

	Public ReadOnly Property UpdateScripts() As System.Collections.Generic.List(Of UpdateScript)
		Get
			Return Me._updateScripts
		End Get
	End Property

	Public ReadOnly Property ViewScripts() As System.Collections.Generic.List(Of ObjectScript)
		Get
			Return Me._viewScripts
		End Get
	End Property

	Sub AddScript(ByVal type As String, ByVal path As String)
		Select Case type
			Case ScriptType.TableData
				Me._tableDataScripts.Add(New TableDataScript(path))
			Case ScriptType.UpdateScript
				Me._updateScripts.Add(New UpdateScript(path))
			Case ScriptType.Trigger
				Me._triggerScripts.Add(New ObjectScript(path, type))
			Case ScriptType.StoredProcedure
				Me._procedureScripts.Add(New ObjectScript(path, type))
			Case ScriptType.UserDefinedFunction
				Me._functionScripts.Add(New ObjectScript(path, type))
			Case ScriptType.View
				Me._viewScripts.Add(New ObjectScript(path, type))

		End Select

	End Sub
End Class

