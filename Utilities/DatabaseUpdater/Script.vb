Public MustInherit Class Script
	Friend _objectName As String

    Private filename As String
    Protected content As String
    Protected path As String
    Friend modified As DateTime
	Protected isReadOnlyFile As Boolean
	Friend objectType As String
	Public Sub New(ByVal path As String)
		Me.path = path
		Dim file As New IO.FileInfo(path)
		Me.filename = file.Name
		Me.modified = DateTime.Parse(file.LastWriteTime.ToString)
		Me.isReadOnlyFile = file.IsReadOnly

	End Sub

	Function GetContent() As String
		Dim reader As New IO.StreamReader(path, System.Text.Encoding.Default, True)
		Dim builder As New Text.StringBuilder
		builder.Append(reader.ReadToEnd)
		reader.Close()
		Return builder.ToString
	End Function
	Function GetModified() As DateTime
		Return modified
	End Function

	Function GetPath() As String
		Return path
	End Function

	Public MustOverride Function ShouldBeApplied() As Boolean

	Function GetFileName() As String
		Return Me.filename
	End Function

	Public Sub ApplyToDatabase(ByVal withEncryption As Boolean)
		Dim scripts() As String = SQLScriptParser.Parse(Me.GetContent, withEncryption)
		ExecuteProcs(scripts)
		LogUpdateInDatabase()
	End Sub
	Public MustOverride Sub LogUpdateInDatabase()


	Friend Function IsCheckedOut() As Boolean
		Return Not Me.isReadOnlyFile
	End Function




	Sub ExecuteProcs(ByVal scripts() As String)
		Dim trans As SqlClient.SqlTransaction = Nothing
		Dim commandText As String = Nothing
		Dim conn As New SqlClient.SqlConnection(DatabaseUpdater.Global.ConnectionString)
		Try
			conn.Open()
			trans = conn.BeginTransaction
			For Each commandText In scripts
				If commandText.Trim <> "" Then
					Dim cmd As New SqlClient.SqlCommand(commandText, conn, trans)
					cmd.CommandTimeout = 0
					cmd.ExecuteNonQuery()
				End If
			Next
			trans.Commit()
		Catch ex As System.Data.SqlClient.SqlException
			trans.Rollback()
			Dim scriptEx As New ScriptExecutionException(Me.filename, commandText, ex)
			Dim missingObjectNames As ArrayList = MissingObjectExceptionMessage.GetMissingObjectNames(ex.Message)
			If missingObjectNames.Count = 0 Then
				Throw scriptEx
			Else
				Dim depEx As New DependencyException(scriptEx)
				For Each missingObjectName As String In missingObjectNames
					depEx.addDependency(missingObjectName)
				Next
				Throw depEx
			End If

		Finally
			conn.Close()
		End Try
	End Sub
	Class NotAMissingObjectException
		Inherits Exception
	End Class
	Class MissingObjectExceptionMessage
		'If indexOfInvalidObjectNameMessage = -1 Then
		'    indexofinvalidobjectnamemessage = ex.Message.IndexOf ("the user-defined function or aggregate """
		'End If
		Shared Function GetMissingObjectNames(ByVal message As String) As ArrayList
			Try
				Dim missingObjectNames As New ArrayList
				Dim someMissingObjectNames As ArrayList = GetMissingObjectNames(message, "Invalid object name ", "'"c)
				For Each objectName As String In someMissingObjectNames
					If Not missingObjectNames.Contains(objectName) Then missingObjectNames.Add(objectName)
				Next
				someMissingObjectNames.Clear()
				someMissingObjectNames = GetMissingObjectNames(message, "the user-defined function or aggregate ", """"c)
				For Each objectName As String In someMissingObjectNames
					If Not missingObjectNames.Contains(objectName) Then missingObjectNames.Add(objectName)
				Next
				Return missingObjectNames

			Catch ex As Exception
				Throw ex
			End Try

		End Function
		Private Shared Function GetMissingObjectNames(ByVal message As String, ByVal searchString As String, ByVal delimiter As Char) As ArrayList
			Dim missingObjectNames As New ArrayList
			Dim pos As Int32 = 0
			Dim indexOfInvalidObjectNameMessage As Integer = message.IndexOf(searchString, pos)
			While indexOfInvalidObjectNameMessage > -1
				pos = message.IndexOf(delimiter, indexOfInvalidObjectNameMessage + searchString.Length + 1)
				Dim objectName As String = message.Substring(indexOfInvalidObjectNameMessage + searchString.Length + 1, pos - indexOfInvalidObjectNameMessage - (searchString.Length + 1))
				If objectName.Substring(0, 4).ToLower <> "dbo." Then objectName = "dbo." & objectName
				If Not missingObjectNames.Contains(objectName) Then missingObjectNames.Add(objectName)
				indexOfInvalidObjectNameMessage = message.IndexOf("Invalid object name", pos)
			End While
			Return missingObjectNames

		End Function

	End Class

	Protected Overrides Sub Finalize()
		MyBase.Finalize()
	End Sub


End Class


