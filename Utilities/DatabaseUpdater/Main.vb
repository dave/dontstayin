Option Strict On

Imports DatabaseUpdater
Imports System.Collections.Generic

Public Class Main


	Dim scriptSource As ScriptSource
	'Dim objectScripts As Hashtable = New Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default)
	'Dim updatesScripts As New List(Of UpdateScript)
	'Dim dataScripts As New List(Of TableDataScript)
	Dim encryptScripts As Boolean
	Dim applyDataDirectory As Boolean
	Dim log As Int16
	Dim [global] As New DatabaseUpdater.Global



	Sub New(ByVal scriptSource As ScriptSource, ByVal connectionstring As String, ByVal encryptScripts As Boolean, Optional ByVal log As Int16 = 0, Optional ByVal applyDataDirectory As Boolean = False)
		Me.scriptSource = scriptSource
		DatabaseUpdater.Global.ConnectionString = connectionstring
		Me.encryptScripts = encryptScripts
		Me.log = log
		Me.applyDataDirectory = applyDataDirectory
	End Sub



	Sub SynchroniseDatabase()



		ApplyUpdateScripts()

		If Me.applyDataDirectory Then
			ApplyDataScripts()
		End If


		ApplyObjectScripts(ScriptType.View)
		ApplyObjectScripts(ScriptType.UserDefinedFunction)
		ApplyObjectScripts(ScriptType.StoredProcedure)
		ApplyObjectScripts(ScriptType.Trigger)





	End Sub
#Region "ApplyScripts methods - these are practically all the same and should be refactored into one"
	Private Sub ApplyUpdateScripts()
		scriptSource.UpdateScripts.Sort()
		'Array.Sort(sortedScripts)
		Dim enumerator As IEnumerator = scriptSource.UpdateScripts.GetEnumerator
		Dim total As Integer = scriptSource.UpdateScripts.Count

		Dim appliedScripts As ArrayList = New ArrayList
		Dim done As Integer = 0, skipped As Integer = 0, run As Integer = 0
		While enumerator.MoveNext
			Dim script As Script = CType(enumerator.Current, Script)
			If script.ShouldBeApplied Then
				script.ApplyToDatabase(encryptScripts)
				run += 1
				appliedScripts.Add(script.GetFileName)
			Else
				skipped += 1
			End If
			done += 1
		End While

		Console.WriteLine(String.Format("{0} update scripts applied, {1} skipped", run, skipped))
		If log > 0 Then
			Dim i As Integer
			For i = 0 To appliedScripts.Count - 1
				Console.WriteLine(String.Format("{0} applied", appliedScripts.Item(i)))
			Next
		End If
	End Sub
	Private Sub ApplyDataScripts()

		Dim enumerator As IEnumerator = Me.scriptSource.TableDataScripts.GetEnumerator
		Dim total As Integer = Me.scriptSource.TableDataScripts.Count
		'If total = 0 Then Exit Sub
		Dim appliedScripts As New List(Of String)
		Dim done As Integer = 0, skipped As Integer = 0, run As Integer = 0
		While enumerator.MoveNext
			Dim script As Script = CType(enumerator.Current, Script)
			'Console.WriteLine(script.GetFileName)
			If script.ShouldBeApplied Then
				script.ApplyToDatabase(encryptScripts)
				run += 1
				appliedScripts.Add(script.GetFileName)

			Else
				skipped += 1
			End If
			done += 1
		End While

		Console.WriteLine(String.Format("{0} data scripts applied, {1} skipped", run, skipped))
		If log > 0 Then
			Dim i As Integer
			For i = 0 To appliedScripts.Count - 1
				Console.WriteLine(String.Format("{0} applied", appliedScripts.Item(i)))
			Next
		End If
	End Sub

	Private Sub ApplyObjectScripts(ByVal currentScriptType As String)
		Dim objectScripts As List(Of ObjectScript) = Nothing
		Select Case currentScriptType
			Case ScriptType.StoredProcedure
				objectScripts = Me.scriptSource.ProcedureScripts
			Case ScriptType.Trigger
				objectScripts = Me.scriptSource.TriggerScripts
			Case ScriptType.View
				objectScripts = Me.scriptSource.ViewScripts
			Case ScriptType.UserDefinedFunction
				objectScripts = Me.scriptSource.FunctionScripts
		End Select

		Dim total As Integer = objectScripts.Count
		Dim checked As Integer = 0, run As Integer = 0, skipped As Integer = 0


		Dim appliedScripts As ArrayList = New ArrayList
		Dim enumerator As IEnumerator(Of ObjectScript) = objectScripts.GetEnumerator
		While enumerator.MoveNext
			Dim script As ObjectScript = enumerator.Current
			If script.ShouldBeApplied Then
				RunScript(script)
				run += 1
				appliedScripts.Add(script.GetFileName)
			Else
				skipped += 1
			End If
			objectScripts.Remove(script)
			checked += 1
			enumerator = objectScripts.GetEnumerator
		End While
		Console.WriteLine(String.Format("{0} {1} scripts applied, {2} skipped", run, currentScriptType, skipped))
		If log > 0 Then
			Dim i As Integer
			For i = 0 To appliedScripts.Count - 1
				Console.WriteLine(String.Format("{0} applied", appliedScripts.Item(i)))
			Next
		End If
	End Sub
#End Region
	Private Sub RunScript(ByVal script As Script)
		Try
			script.ApplyToDatabase(encryptScripts)
		Catch depEx As DependencyException
			For Each objectName As String In depEx.dependencies
				If Left(objectName, 4) = "dbo." Then objectName = Right(objectName, Len(objectName) - 4) 'strip dbo. from object name
				Dim requiredScript As ObjectScript = Nothing
				If requiredScript Is Nothing Then requiredScript = FindScript(objectName, Me.scriptSource.FunctionScripts)
				If requiredScript Is Nothing Then requiredScript = FindScript(objectName, Me.scriptSource.ProcedureScripts)
				If requiredScript Is Nothing Then requiredScript = FindScript(objectName, Me.scriptSource.ViewScripts)
				If requiredScript Is Nothing Then
					Throw New ObjectDoesNotExistException(objectName)
				Else
					RunScript(requiredScript)
				End If

			Next
			RunScript(script)	'reapply original script after dependencies have been created
		Catch ex As Exception
			Throw New Exception(String.Format("There was an error running script {0} : {1}", script.GetFileName, ex.Message), ex)
		End Try
	End Sub

	Private Shared Function FindScript(ByVal missingObjectName As String, ByVal collection As List(Of ObjectScript)) As ObjectScript
		For Each script As ObjectScript In collection
			If script.ObjectName = missingObjectName Then
				Return script
			End If
		Next
		Return Nothing
	End Function


 

	Public Shared Sub Main(ByVal args() As String)
		Dim log As Int16 = 0
		Try
			Dim connectionString As String
			Try
				connectionString = GetArgValue("/cs:", args, Nothing)
			Catch ex As ArgumentNotProvidedException
				Dim server As String = GetArgValue("/s:", args, Nothing)
				Dim database As String = GetArgValue("/d:", args, Nothing)
				connectionString = String.Format("Server={0};Database={1};Trusted_Connection=True", server, database)
			End Try

			Dim path As String = GetArgValue("/p:", args, ".")
			Dim scriptSource As ScriptSource
			Dim fi As New IO.FileInfo(path)
			If fi.Exists AndAlso fi.Extension = "dbp" Then
                scriptSource = New DatabaseProjectScriptSource(path)
            ElseIf fi.Exists AndAlso fi.Extension = "csproj" Then
                scriptSource = New CSharpProjectScriptSource(path)
            Else
                Dim di As New IO.DirectoryInfo(path)
                Dim projectFiles As IO.FileInfo() = di.GetFiles("*.dbp")
                If projectFiles.Length = 1 Then
                    scriptSource = New DatabaseProjectScriptSource(projectFiles(0).FullName)
                Else
                    projectFiles = di.GetFiles("*.csproj")
                    If projectFiles.Length = 1 Then
                        scriptSource = New CSharpProjectScriptSource(projectFiles(0).FullName)
                    Else
                        scriptSource = New FolderStructureScriptSource(path)

                    End If
                End If

			End If
			scriptSource.FillTables()


			Dim applyDataDirectory As Boolean = Convert.ToBoolean(GetArgValue("/data:", args, "False"))


			Dim encryptScripts As Boolean = Convert.ToBoolean(GetArgValue("/e:", args, "False"))
			log = Convert.ToInt16(GetArgValue("/l:", args, "0"))
			Dim dbSync As New Main(scriptSource, connectionString, encryptScripts, log, applyDataDirectory)
			Console.WriteLine("Conn: " + connectionString)
			Console.WriteLine("Path: " + path)

			dbSync.SynchroniseDatabase()
			Console.WriteLine("Success")
			Environment.Exit(0)
		Catch ex As Exception
			If log > 1 Then
				Console.Error.WriteLine("Failed: " & ex.ToString)
			Else
				Console.Error.WriteLine("Failed: " & ex.Message)
			End If

			Environment.Exit(1)
		End Try
	End Sub
	Shared Function IsAFolder(ByVal path As String) As Boolean
		Return IO.Directory.Exists(path)
	End Function
	Shared Function IsAVisualStudioSQLProject(ByVal path As String) As Boolean
		Dim fi As New IO.FileInfo(path)
		Return fi.Exists AndAlso fi.Extension = "dbp"
	End Function

	Private Shared Function GetArgValue(ByVal prefix As String, ByVal args() As String, ByVal defaultValue As String) As String
		For Each arg As String In args
			If arg.Length > prefix.Length AndAlso arg.Substring(0, prefix.Length).ToLower = prefix.ToLower Then
				Return arg.Substring(prefix.Length)
			End If
		Next
		If Not defaultValue Is Nothing Then Return defaultValue
		Throw New ArgumentNotProvidedException(prefix)
	End Function
	Class ArgumentNotProvidedException
		Inherits System.Exception
		Dim argument As String
		Sub New(ByVal argument As String)
			Me.argument = argument
		End Sub
		Public Overrides ReadOnly Property Message() As String
			Get
				Return "Argument " & argument & " was not provided"
			End Get
		End Property
	End Class
End Class



Class ObjectDoesNotExistException
    Inherits Exception
    Private _objectName As String
    Function objectName() As String
        Return Me._objectName
    End Function
    Sub New(ByVal objectName As String)
        MyBase.New(objectName & " does not exist within the script folders but is required by one of the other objectScripts")
        Me._objectName = objectName
    End Sub
End Class
