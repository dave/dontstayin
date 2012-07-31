Public Class SQLExtendedPropertyMethods
#Region "Delete methods"

	Private Shared Sub Delete(ByVal propertyName As String)
		Delete(propertyName, "", ScriptType.UpdateScript)
	End Sub



	Private Shared Sub Delete(ByVal propertyName As String, ByVal objectName As String, ByVal objectType As String)
		Dim conn As New SqlClient.SqlConnection(DatabaseUpdater.Global.ConnectionString)
		Try

			Dim cmd As New SqlClient.SqlCommand("sp_dropextendedproperty", conn)
			cmd.CommandType = CommandType.StoredProcedure
			If objectType = ScriptType.UpdateScript Then
				cmd.Parameters.Add("@name", SqlDbType.VarChar, 128).Value = propertyName
				cmd.Parameters.AddWithValue("@level0type", DBNull.Value)
				cmd.Parameters.AddWithValue("@level0name", DBNull.Value)
				cmd.Parameters.AddWithValue("@level1type", DBNull.Value)
				cmd.Parameters.AddWithValue("@level1name", DBNull.Value)
				cmd.Parameters.AddWithValue("@level2type", DBNull.Value)
				cmd.Parameters.AddWithValue("@level2name", DBNull.Value)
			Else
				cmd.Parameters.Add("@name", SqlDbType.VarChar, 128).Value = "ScriptDate"
				cmd.Parameters.Add("@level0type", SqlDbType.VarChar, 128).Value = "user"
				cmd.Parameters.Add("@level0name", SqlDbType.VarChar, 128).Value = "dbo"
				If objectType = DatabaseUpdater.ScriptType.Trigger Then
					cmd.Parameters.Add("@level1type", SqlDbType.VarChar, 128).Value = "table"
					cmd.Parameters.Add("@level1name", SqlDbType.VarChar, 128).Value = objectName.Split("_"c)(0)
					cmd.Parameters.Add("@level2type", SqlDbType.VarChar, 128).Value = "trigger"
					cmd.Parameters.Add("@level2name", SqlDbType.VarChar, 128).Value = objectName
				Else
					cmd.Parameters.Add("@level1type", SqlDbType.VarChar, 128).Value = objectType
					cmd.Parameters.Add("@level1name", SqlDbType.VarChar, 128).Value = objectName
					cmd.Parameters.Add("@level2type", SqlDbType.VarChar, 128).Value = DBNull.Value
					cmd.Parameters.Add("@level2name", SqlDbType.VarChar, 128).Value = DBNull.Value
				End If
			End If




			conn.Open()
			cmd.ExecuteNonQuery()
		Catch ex As Exception
			Throw ex
		Finally
			conn.Close()
		End Try
	End Sub

#End Region
#Region "Adds"
	Private Shared Sub Add(ByVal propertyName As String, ByVal value As String)
		Add(propertyName, value, "", ScriptType.UpdateScript)
	End Sub
	Private Shared Sub Add(ByVal propertyName As String, ByVal value As String, ByVal objectName As String, ByVal objectType As String)
		Dim conn As New System.Data.SqlClient.SqlConnection(DatabaseUpdater.Global.ConnectionString)
		Try
			Dim cmd As New SqlClient.SqlCommand("sp_addextendedproperty", conn)
			cmd.CommandType = CommandType.StoredProcedure

			If objectType = ScriptType.UpdateScript Then
				cmd.Parameters.Add("@name", SqlDbType.VarChar, 128).Value = propertyName
				cmd.Parameters.AddWithValue("@value", SqlDbType.DateTime).Value = value
				cmd.Parameters.AddWithValue("@level0type", DBNull.Value)
				cmd.Parameters.AddWithValue("@level0name", DBNull.Value)
				cmd.Parameters.AddWithValue("@level1type", DBNull.Value)
				cmd.Parameters.AddWithValue("@level1name", DBNull.Value)
				cmd.Parameters.AddWithValue("@level2type", DBNull.Value)
				cmd.Parameters.AddWithValue("@level2name", DBNull.Value)
			Else

				cmd.Parameters.Add("@name", SqlDbType.VarChar, 128).Value = "ScriptDate"
				cmd.Parameters.AddWithValue("@value", SqlDbType.DateTime).Value = value
				cmd.Parameters.Add("@level0type", SqlDbType.VarChar, 128).Value = "user"
				cmd.Parameters.Add("@level0name", SqlDbType.VarChar, 128).Value = "dbo"

				If objectType = DatabaseUpdater.ScriptType.Trigger Then
					cmd.Parameters.Add("@level1type", SqlDbType.VarChar, 128).Value = "table"
					cmd.Parameters.Add("@level1name", SqlDbType.VarChar, 128).Value = objectName.Split("_"c)(0)
					cmd.Parameters.Add("@level2type", SqlDbType.VarChar, 128).Value = "trigger"
					cmd.Parameters.Add("@level2name", SqlDbType.VarChar, 128).Value = objectName
				Else
					cmd.Parameters.Add("@level1type", SqlDbType.VarChar, 128).Value = objectType
					cmd.Parameters.Add("@level1name", SqlDbType.VarChar, 128).Value = objectName
					cmd.Parameters.Add("@level2type", SqlDbType.VarChar, 128).Value = DBNull.Value
					cmd.Parameters.Add("@level2name", SqlDbType.VarChar, 128).Value = DBNull.Value
				End If
			End If
			conn.Open()
			cmd.ExecuteNonQuery()
		Catch ex As Exception

			Throw ex
		Finally
			conn.Close()
		End Try
	End Sub
#End Region
#Region "Gets"
	Public Shared Function [Get](ByVal propertyName As String) As String
		Return [Get](propertyName, "", DatabaseUpdater.ScriptType.UpdateScript)
	End Function
	Public Shared Function [Get](ByVal propertyName As String, ByVal objectName As String, ByVal objectType As String) As String
		Dim conn As New SqlClient.SqlConnection(DatabaseUpdater.Global.ConnectionString)
		Try


			conn.Open()
			Dim cmd As New SqlClient.SqlCommand("SELECT value FROM ::fn_listextendedproperty (@name,@level0type,@level0name,@level1type,@level1name,@level2type,@level2name) WHERE NAME = @name", conn)
			If objectType = DatabaseUpdater.ScriptType.UpdateScript Then
				cmd.Parameters.Add("@name", SqlDbType.VarChar, 128).Value = propertyName
				cmd.Parameters.AddWithValue("@level0type", DBNull.Value)
				cmd.Parameters.AddWithValue("@level0name", DBNull.Value)
				cmd.Parameters.AddWithValue("@level1type", DBNull.Value)
				cmd.Parameters.AddWithValue("@level1name", DBNull.Value)
				cmd.Parameters.AddWithValue("@level2type", DBNull.Value)
				cmd.Parameters.AddWithValue("@level2name", DBNull.Value)
			Else

				cmd.Parameters.Add("@name", SqlDbType.VarChar, 128).Value = "ScriptDate"
				cmd.Parameters.Add("@level0type", SqlDbType.VarChar, 128).Value = "user"
				cmd.Parameters.Add("@level0name", SqlDbType.VarChar, 128).Value = "dbo"
				If objectType = DatabaseUpdater.ScriptType.Trigger Then
					cmd.Parameters.Add("@level1type", SqlDbType.VarChar, 128).Value = "table"
					cmd.Parameters.Add("@level1name", SqlDbType.VarChar, 128).Value = objectName.Split("_"c)(0)
					cmd.Parameters.Add("@level2type", SqlDbType.VarChar, 128).Value = "trigger"
					cmd.Parameters.Add("@level2name", SqlDbType.VarChar, 128).Value = objectName
				Else
					cmd.Parameters.Add("@level1type", SqlDbType.VarChar, 128).Value = objectType
					cmd.Parameters.Add("@level1name", SqlDbType.VarChar, 128).Value = objectName
					cmd.Parameters.Add("@level2type", SqlDbType.VarChar, 128).Value = DBNull.Value
					cmd.Parameters.Add("@level2name", SqlDbType.VarChar, 128).Value = DBNull.Value
				End If

			End If
			Dim value As Object = cmd.ExecuteScalar
			If value Is Nothing Then Throw New NoDatabaseObjectWithThatNameException
			Return value.ToString
		Catch ex0 As NoDatabaseObjectWithThatNameException
			Throw ex0
		Catch ex1 As Exception
			Throw ex1

		Finally
			conn.Close()
		End Try
	End Function
#End Region
#Region "Updates"
	Public Shared Sub Update(ByVal propertyName As String, ByVal value As String)

		Update(propertyName, value, "", ScriptType.UpdateScript)
	End Sub
	Public Shared Sub Update(ByVal propertyName As String, ByVal value As String, ByVal objectName As String, ByVal objectType As String)
		Try
			[Get](propertyName, objectName, objectType)
			Delete(propertyName, objectName, objectType)
		Catch ex As NoDatabaseObjectWithThatNameException
			'this will get thrown by GetTimeStampOfDatabaseObject 
		End Try
		Add(propertyName, value, objectName, objectType)
	End Sub
#End Region
End Class
