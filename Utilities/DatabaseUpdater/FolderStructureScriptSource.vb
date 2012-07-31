Imports System.Collections.Generic
Public Class FolderStructureScriptSource
	Inherits ScriptSource


	Dim path As String

	Sub New(ByVal path As String)
		Me.path = path
	End Sub

	Public Overrides Sub FillTables()
		Dim dir As New IO.DirectoryInfo(path)
		AddScriptsFromFolder(dir.FullName & "\Change Scripts", "*.sql", True, ScriptType.UpdateScript)
		AddScriptsFromFolder(dir.FullName & "\Data Scripts", "*.sql", True, ScriptType.TableData)
		AddScriptsFromFolder(dir.FullName & "\Create scripts\viw", "*.sql", True, ScriptType.View)
		AddScriptsFromFolder(dir.FullName & "\Create scripts\udf", "*.sql", True, ScriptType.UserDefinedFunction)
		AddScriptsFromFolder(dir.FullName & "\Create scripts\prc", "*.sql", True, ScriptType.StoredProcedure)
		AddScriptsFromFolder(dir.FullName & "\Create scripts\trg", "*.sql", True, ScriptType.Trigger)

	End Sub
	Private Sub AddScriptsFromFolder(ByVal path As String, ByVal extensionMask As String, ByVal recurse As Boolean, ByVal type As String)
		Dim dir As New IO.DirectoryInfo(path)
		If Not dir.Exists Then Exit Sub
		For Each file As IO.FileInfo In dir.GetFiles(extensionMask)
			AddScript(type, file.FullName)
		Next
		If recurse Then
			For Each directory As IO.DirectoryInfo In dir.GetDirectories
				AddScriptsFromFolder(directory.FullName, extensionMask, recurse, type)
			Next
		End If

	End Sub

End Class
