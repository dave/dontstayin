using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Net;

using Exortech.NetReflector;
using ThoughtWorks.CruiseControl.Core.Util;

using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.Client;


// Placed this is the same namespace that CCNET uses as hopefully it might end up being distrbuted with CCNET.
// TODO:  Check that this does not cause any naming conflicts
namespace ThoughtWorks.CruiseControl.Core.Sourcecontrol
{
    /// <summary>
    ///   Source Control Plugin for CruiseControl.NET that talks to VSTS Team Foundation Server.
    /// </summary>
    [ReflectorType("vsts")]
    public class Vsts : ISourceControl
    {
        #region Constants

        private const string DEFAULT_WORKSPACE_NAME = "CCNET";
        private const string DEFAULT_WORKSPACE_COMMENT = "Temporary CruiseControl.NET Workspace";

        #endregion Constants

        #region NetReflectored Properties

        /// <summary>
        ///   The name or URL of the team foundation server.  For example http://vstsb2:8080 or vstsb2 if it
        ///   has already been registered on the machine.
        /// </summary>
        [ReflectorProperty("server")]
        public string Server;

        /// <summary>
        ///   The path to the project in source control, for example $\VSTSPlugins
        /// </summary>
        [ReflectorProperty("project")]
        public string ProjectPath;

        /// <summary>
        /// Gets or sets whether this repository should be labeled.
        /// </summary>
        [ReflectorProperty("applyLabel", Required = false)]
        public bool ApplyLabel = false;

        [ReflectorProperty("autoGetSource", Required = false)]
        public bool AutoGetSource = false;

        /// <summary>
        ///   Username that should be used.  Domain cannot be placed here, rather in domain property.
        /// </summary>
        [ReflectorProperty("username", Required = false)]
        public string Username;

        /// <summary>
        ///   The password in clear test of the domain user to be used.
        /// </summary>
        [ReflectorProperty("password", Required = false)]
        public string Password;

        /// <summary>
        ///  The domain of the user to be used.
        /// </summary>
        [ReflectorProperty("domain", Required = false)]
        public string Domain;

        [ReflectorProperty("workingDirectory", Required = false)]
        public string WorkingDirectory;

        [ReflectorProperty("cleanCopy", Required = false)]
        public bool CleanCopy = false;


        [ReflectorProperty("force", Required = false)]
        public bool Force = false;

        private string workspaceName;
        /// <summary>
        ///   Name of the workspace to create.  This will revert to the DEFAULT_WORKSPACE_NAME if not passed.
        /// </summary>
        [ReflectorProperty("workspace", Required = false)]
        public string Workspace
        {
            get
            {
                if (workspaceName == null)
                {
                    workspaceName = DEFAULT_WORKSPACE_NAME;
                }
                return workspaceName;
            }
            set
            {
                workspaceName = value;
            }
        }

        [ReflectorProperty("deleteWorkspace", Required = false)]

        /// <summary>
        ///   Flag indicating if workspace should be deleted every time or if it should be 
        ///   left (the default).  Leaving the workspace will mean that subsequent gets 
        ///   will only need to transfer the modified files, improving performance considerably.
        /// </summary>
        public bool DeleteWorkspace = false;

        #endregion NetReflectored Properties

        #region ISourceControl Implementation

        public Modification[] GetModifications(IIntegrationResult from, IIntegrationResult to)
        {
            Log.Debug("Checking Team Foundation Server for Modifications");
            Log.Debug("From: " + from.StartTime + " - To: " + to.StartTime);

            VersionSpec fromVersion = new DateVersionSpec(from.StartTime);
            VersionSpec toVersion = new DateVersionSpec(to.StartTime);

            IEnumerable changesets = this.SourceControl.QueryHistory(this.ProjectPath, VersionSpec.Latest, 0, RecursionType.Full, null, fromVersion, toVersion, int.MaxValue, true, false);

            List<Modification> modifications = new List<Modification>();
            int latestChangeSetId = 0;

            // Each changeset contains multiple file modifications.  
            // Build up array of all CCNET modifications from all changesets.
            foreach (Changeset changeset in changesets)
            {
				
				if (changeset.CreationDate > from.StartTime || changeset.CreationDate > to.StartTime) //check as 2008 beta1/beta2 always return the most recent changeset even if outsite from and to range
				{
					string userName = changeset.Committer;
					string comment = changeset.Comment;
					int changeNumber = changeset.ChangesetId;

					if (latestChangeSetId < changeNumber)
						latestChangeSetId = changeNumber;

					// In VSTS, the version of the file is the same as the changeset number it was checked in with.
					string version = Convert.ToString(changeNumber);

					DateTime modifedTime = this.TFS.TimeZone.ToLocalTime(changeset.CreationDate);

					foreach (Change change in changeset.Changes)
					{
						Modification modification = this.ConvertToModification(userName, comment, changeNumber, version, modifedTime, change);

						modifications.Add(modification);
					}

				}
                
            }

            if (latestChangeSetId == 0)
            {
                changesets = this.SourceControl.QueryHistory(this.ProjectPath, VersionSpec.Latest, 0, RecursionType.Full, null, null, null, 1, false, false);
                foreach (Changeset changeset in changesets)
                    latestChangeSetId = changeset.ChangesetId;
            }
            this.WorkingVersion = new ChangesetVersionSpec(latestChangeSetId);

            Log.Debug(string.Format("Found {0} modifications", modifications.Count));

            return modifications.ToArray();
        }

        public void LabelSourceControl(IIntegrationResult result)
        {
            if (ApplyLabel && result.Succeeded)
            {
                Log.Debug(String.Format("Applying label \"{0}\"", result.Label));
                VersionControlLabel label = new VersionControlLabel(this.SourceControl, result.Label, sourceControl.AuthenticatedUser, this.ProjectPath, "Labeled by CruiseControl.NET");

                // Create Label Item Spec.
                ItemSpec itemSpec = new ItemSpec(this.ProjectPath, RecursionType.Full);
                LabelItemSpec[] labelItemSpec = new LabelItemSpec[] {  
                    new LabelItemSpec(itemSpec, this.WorkingVersion, false)
                };

                this.SourceControl.CreateLabel(label, labelItemSpec, LabelChildOption.Replace);
            }
        }

        public void GetSource(IIntegrationResult result)
        {
            if (AutoGetSource)
            {
                this.WorkingDirectory = result.BaseFromWorkingDirectory(this.WorkingDirectory);

                if (CleanCopy)
                {
                    // If we have said we want a clean copy, then delete old copy before getting.
                    Log.Debug("Deleting " + this.WorkingDirectory);
                    this.deleteDirectory(this.WorkingDirectory);
                }

                Workspace[] workspaces = this.SourceControl.QueryWorkspaces(Workspace, this.SourceControl.AuthenticatedUser, Workstation.Current.Name);
                Workspace workspace = null;

                if (workspaces.Length > 0)
                {
                    // The workspace exists.  
                    if (DeleteWorkspace)
                    {
                        // We have asked for a new workspace every time, therefore delete the existing one.
                        Log.Debug("Removing existing workspace " + Workspace);
                        this.SourceControl.DeleteWorkspace(Workspace, this.SourceControl.AuthenticatedUser);
                        workspaces = new Workspace[0];
                    }
                    else
                    {
                        Log.Debug("Existing workspace detected - reusing");
                        workspace = workspaces[0];
                    }
                }
                if (workspaces.Length == 0)
                {
                    Log.Debug("Creating new workspace name: " + Workspace);
                    workspace = this.SourceControl.CreateWorkspace(Workspace, this.SourceControl.AuthenticatedUser, DEFAULT_WORKSPACE_COMMENT);
                }

                try
                {
                    workspace.Map(ProjectPath, WorkingDirectory);

                    Log.Debug(String.Format("Getting {0} to {1}", ProjectPath, WorkingDirectory));
                    GetRequest getReq = new GetRequest(new ItemSpec(ProjectPath, RecursionType.Full), this.WorkingVersion);
                    if (CleanCopy || Force)
                    {
                        Log.Debug("Forcing a Get Specific with the options \"get all files\" and \"overwrite read/write files\"");
                        workspace.Get(getReq, GetOptions.GetAll | GetOptions.Overwrite);
                    }
                    else
                    {
                        Log.Debug("Performing a Get Latest");
                        workspace.Get(getReq, GetOptions.None);
                    }
                }
                finally
                {
                    if (workspace != null && DeleteWorkspace)
                    {
                        Log.Debug("Deleting the workspace");
                        workspace.Delete();
                    }
                }

            }

        }

        public void Initialize(IProject project)
        {
            // Do Nothing
        }

        public void Purge(IProject project)
        {
            //never called by CCNet, cruft...
        }

        #endregion ISourceControl Implementation

        #region Private Members

        private Modification ConvertToModification(string userName, string comment, int changeNumber, string version, DateTime modifedTime, Change change)
        {
            Modification modification = new Modification();
            modification.UserName = userName;
            modification.Comment = comment;
            modification.ChangeNumber = changeNumber;
            modification.ModifiedTime = modifedTime;
            modification.Version = version;
            modification.Type = PendingChange.GetLocalizedStringForChangeType(change.ChangeType);

            // Populate fields from change item
            Item item = change.Item;
            if (item.ItemType == ItemType.File)
            {
                // split into foldername and filename
                int lastSlash = item.ServerItem.LastIndexOf('/');
                modification.FileName = item.ServerItem.Substring(lastSlash + 1);
                // patch to the following line submitted by Ralf Kretzschmar.
                modification.FolderName = item.ServerItem.Substring(0, lastSlash);
            }
            else
            {
                // TODO - what should filename be if dir??  Empty string or null?
                modification.FileName = string.Empty;
                modification.FolderName = item.ServerItem;
            }
            return modification;
        }

        /// <summary>
        ///   Delete a directory, even if it contains readonly files.
        /// </summary>
        private void deleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                this.MarkAllFilesReadWrite(path);
                Directory.Delete(path, true);
            }
        }

        private void MarkAllFilesReadWrite(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                file.IsReadOnly = false;
            }

            // Now recurse down the directories
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                this.MarkAllFilesReadWrite(dir.FullName);
            }
        }

        #endregion Private Members

        #region Private Properties

        private VersionSpec _WorkingVersion;
        private VersionSpec WorkingVersion
        {
            get
            {
                return _WorkingVersion;
            }
            set
            {
                _WorkingVersion = value;
            }
        }

        private TeamFoundationServer teamFoundationServer = null;
        /// <summary>
        ///   Cached instance of TeamFoundationServer.
        /// </summary>
        public TeamFoundationServer TFS
        {
            get
            {
                if (null == teamFoundationServer)
                {
                    teamFoundationServer = new TeamFoundationServer(this.Server, this.Credentials);
                }
                return teamFoundationServer;
            }
            set
            {
                teamFoundationServer = value;
            }
        }

        private NetworkCredential networkCredential;
        /// <summary>
        ///   Network credentials used to interact with TFS.
        /// </summary>
        public NetworkCredential Credentials
        {
            get
            {
                if (null == networkCredential)
                {
                    if (Username != null && Password != null)
                    {
                        if (Domain != null)
                            networkCredential = new NetworkCredential(Username, Password, Domain);
                        else
                            networkCredential = new NetworkCredential(Username, Password);
                    }
                    else
                    {
                        networkCredential = CredentialCache.DefaultNetworkCredentials;
                    }
                }
                return networkCredential;
            }
            set
            {
                networkCredential = value;
            }
        }

        private VersionControlServer sourceControl;
        /// <summary>
        ///   The cached instace of the SourceControl object that we are connected to.
        /// </summary>
        public VersionControlServer SourceControl
        {
            get
            {
                if (null == sourceControl)
                {
                    sourceControl = (VersionControlServer)this.TFS.GetService(typeof(VersionControlServer));
                }
                return sourceControl;
            }
            set
            {
                sourceControl = value;
            }
        }

        #endregion Private Properties
    }
}
