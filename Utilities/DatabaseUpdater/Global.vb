Public Class [Global]
	Private Shared _globalLock As New Object
	Private Shared _connectionString As String
	Shared Property ConnectionString() As String
		Get
			Return _connectionString
		End Get
		Set(ByVal value As String)
			SyncLock (_globalLock)
				_connectionString = value
			End SyncLock
		End Set
	End Property
End Class
