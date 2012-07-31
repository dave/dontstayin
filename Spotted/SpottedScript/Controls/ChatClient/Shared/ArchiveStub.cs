namespace SpottedScript.Controls.ChatClient.Shared
{
	public class ArchiveStub : RefreshStub
	{
		public string roomGuid;
		public string archiveItems;
		public ArchiveStub() { }
	}
	public class DeleteArchiveStub : RefreshStub
	{
		public string roomGuid;
		public DeleteArchiveStub() { }
	}
}
