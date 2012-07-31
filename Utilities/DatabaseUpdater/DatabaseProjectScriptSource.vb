Imports System.Collections.Generic
Public Class DatabaseProjectScriptSource
	Inherits ScriptSource
	Friend path As String
	Friend current As Integer
	Friend fileContent As String()
	Friend currentPath As String
	Friend depth As Integer = 0


	Sub New(ByVal path As String)
		Me.path = path
	End Sub

	Public Overrides Sub FillTables()
		currentPath = New IO.FileInfo(path).Directory.FullName + "\"
		fileContent = System.IO.File.ReadAllLines(path)
		current = 0
		While current < fileContent.Length
			If IsStartOfFolderDefinition() Then
				Select Case GetBitInQuotes()
					Case "Change Scripts"
						AddScriptsFromDirectory(ScriptType.UpdateScript)
					Case "Create Scripts"
						AddCreateScripts()
					Case "Data Scripts"
						AddScriptsFromDirectory(ScriptType.TableData)
				End Select
			End If
			MoveToNextLine()
		End While
	End Sub


	Sub AddCreateScripts()
		MoveToNextLine()
		While Not IsEndOfFolderDefinition()
			If IsStartOfFolderDefinition() Then
				Select Case GetBitInQuotes()
					Case "prc"
						AddScriptsFromDirectory(ScriptType.StoredProcedure)
					Case "viw"
						AddScriptsFromDirectory(ScriptType.View)
					Case "udf"
						AddScriptsFromDirectory(ScriptType.UserDefinedFunction)
					Case "trg"
						AddScriptsFromDirectory(ScriptType.Trigger)
				End Select
			Else
				Throw New Exception("Should only be folder definitions in Create Script folder. Found : " + Line)
			End If
		End While
	End Sub
	Sub AddScriptsFromDirectory(ByVal type As String)
		MoveToNextLine()
		While Not IsEndOfFolderDefinition()
			If IsStartOfFolderDefinition() Then
				AddScriptsFromDirectory(type)
			ElseIf Me.IsScriptDefinition Then
				AddScript(type, currentPath + GetBitInQuotes())
			Else
				Throw New Exception("Should only be script or folder definitions in " + currentPath + ". Found : " + Line)
			End If
			MoveToNextLine()
		End While
	End Sub
	
	Sub AddFolderPathToCurrentPath()
		Me.currentPath += GetBitInQuotes() + "\"
		Me.depth += 1
	End Sub
	Sub MoveToNextLine()
		If Me.IsStartOfFolderDefinition Then AddFolderPathToCurrentPath()
		current += 1
		If Me.IsEndOfFolderDefinition Then GoUpALevel()
	End Sub
	Sub GoUpALevel()
		If Me.depth >= 0 Then
			Me.currentPath = Me.currentPath.Substring(0, Me.currentPath.Length - 1)	'remove trailing \
			Me.currentPath = Me.currentPath.Substring(0, Me.currentPath.LastIndexOf("\") + 1)
			Me.depth -= 1
		End If
	End Sub
	ReadOnly Property Line() As String
		Get
			If current < fileContent.Length Then
				Return Me.fileContent(current).Trim
			Else
				Return ""
			End If
		End Get
	End Property
	Function GetBitInQuotes() As String
		Return Line.Substring(Line.IndexOf("""") + 1, Line.LastIndexOf("""") - Line.IndexOf("""") - 1).Replace("""""", """")
	End Function
	Function IsEndOfFolderDefinition() As Boolean
		Return Line = "End"
	End Function
	Function IsStartOfFolderDefinition() As Boolean
		If Line.Length >= "Begin Folder".Length Then
			Return Line.Substring(0, "Begin Folder".Length) = "Begin Folder"
		Else
			Return False
		End If


	End Function
	Function IsScriptDefinition() As Boolean
		Try
			Return Line.Substring(0, "Script =".Length) = "Script ="
		Catch ex As System.ArgumentOutOfRangeException
			Return False
		End Try

	End Function
End Class
