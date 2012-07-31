using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmazonS3;
using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using NUnit.Framework;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web;
using System.Drawing;

namespace Bobs
{
 
	public class Storage
	{
		#region getAmazonConnection()
		private static AWSAuthConnection getAmazonConnection()
		{
			return new AWSAuthConnection("1RW4W98SK2J70PCSVX02", "YWFZDd4OpQubx4qkzaRma8fFgexLsgHMedMdwqJQ", false);
		}
		#endregion
		#region Stores
		public enum Stores
		{
			Pix = 1,
			Master = 2,
			Temporary = 3
		}
		#endregion
		#region Locations
		public enum Locations
		{
			Pix,
			PixNew,
			Static,
			Master,
			MasterNew,
			PixAmazonUS,
			PixAmazonEU,
			MasterAmazonUS,
			MasterAmazonEU,
			Temporary
		}
		#endregion
		#region LocationTypes
		public enum LocationTypes
		{
			Amazon,
			FileSystem
		}
		#endregion

		#region AddToStore
		public static void AddToStore(byte[] data, Stores store, Guid guid, string extention, IBob parent, string dataType)
		{
			SortedList metaData = null;
			if (parent != null && parent is IHasObjectType && parent is IHasSinglePrimaryKey)
			{
				metaData = new SortedList();
				metaData.Add("ObjectType", ((IHasObjectType)parent).ObjectType);
				metaData.Add("ObjectK", ((IHasSinglePrimaryKey)parent).K);
				if (dataType != null && dataType.Length > 0)
					metaData.Add("ObjectData", dataType);
			}

			if (store == Stores.Master)
			{
				try
				{
					if (!Vars.UseOnlyAmazonForWrite)
					{
						Put(data, guid, extention, Locations.Master, metaData);
						if (Photo.HasNewPixMasterFileSystemLocation)
							Put(data, guid, extention, Locations.MasterNew, metaData);
					}
					Put(data, guid, extention, Locations.MasterAmazonEU, metaData);
				}
				catch (Exception ex)
				{
					RemoveFromStore(store, guid, extention);
					throw ex;
				}
			}
			else if (store == Stores.Pix)
			{
				try
				{
					if (!Vars.UseOnlyAmazonForWrite)
					{
						Put(data, guid, extention, Locations.Pix, metaData);
						Put(data, guid, extention, Locations.Static, metaData);
						if (Photo.HasNewPixFileSystemLocation)
							Put(data, guid, extention, Locations.PixNew, metaData);
					}
					Put(data, guid, extention, Locations.PixAmazonEU, metaData);
				}
				catch (Exception ex)
				{
					RemoveFromStore(store, guid, extention);
					throw ex;
				}
			}
			else if (store == Stores.Temporary)
			{
				Put(data, guid, extention, Locations.Temporary, metaData);
			}
			else
				throw new NotImplementedException();
		}
		#endregion
		#region RemoveFromStore
		public static void RemoveFromStore(Stores store, Guid guid, string extention)
		{
			if (store == Stores.Pix)
			{

				if (!Vars.UseOnlyAmazonForWrite)
				{
					try
					{
						Storage.Delete(guid, extention, Locations.Pix);
					}
					catch { }

					if (Photo.HasNewPixFileSystemLocation)
					{
						try
						{
							Storage.Delete(guid, extention, Locations.PixNew);
						}
						catch { }
					}
				
					try
					{
						Storage.Delete(guid, extention, Locations.Static);
					}
					catch { }
				}

				try
				{
					Storage.Delete(guid, extention, Locations.PixAmazonEU);
				}
				catch { }

			}
			else if (store == Stores.Master)
			{
				if (!Vars.UseOnlyAmazonForWrite)
				{

					try
					{
						Storage.Delete(guid, extention, Locations.Master);
					}
					catch { }

					if (Photo.HasNewPixMasterFileSystemLocation)
					{
						try
						{
							Storage.Delete(guid, extention, Locations.MasterNew);
						}
						catch { }
					}
				}

				try
				{
					Storage.Delete(guid, extention, Locations.MasterAmazonEU);
				}
				catch { }
			}
			else if (store == Stores.Temporary)
			{
				try
				{
					Storage.Delete(guid, extention, Locations.Temporary);
				}
				catch { }

			}
			else
			{
				throw new NotImplementedException();
			}
		}
		#endregion
		#region GetFromStore
		public static byte[] GetFromStore(Stores store, Guid guid, string extention)
		{
			if (store == Stores.Pix)
			{
				if (Vars.UseOnlyAmazonForRead)
				{
					return Get(guid, extention, Locations.PixAmazonEU);
				}
				else
				{
					return Get(guid, extention, Locations.Pix);
				}
			}
			else if (store == Stores.Master)
			{
				if (Vars.UseOnlyAmazonForRead)
				{
					return Get(guid, extention, Locations.MasterAmazonEU);
				}
				else
				{
					return Get(guid, extention, Locations.Master);
				}
			}
			else if (store == Stores.Temporary)
			{

				return Get(guid, extention, Locations.Temporary);
			}
			else
				throw new NotImplementedException();
		}
		#endregion
		#region ExistsInStore
		public static bool ExistsInStore(Stores store, Guid guid, string extention)
		{
			if (store == Stores.Pix)
			{
				if (Vars.UseOnlyAmazonForRead)
				{
					return Exists(guid, extention, Locations.PixAmazonEU);
				}
				else
				{
					return Exists(guid, extention, Locations.Pix);
				}
			}
			else if (store == Stores.Master)
			{
				if (Vars.UseOnlyAmazonForRead)
				{
					return Exists(guid, extention, Locations.MasterAmazonEU);
				}
				else
				{
					return Exists(guid, extention, Locations.Master);
				}
			}
			else if (store == Stores.Temporary)
			{
				return Exists(guid, extention, Locations.Temporary);
			}
			else
				throw new NotImplementedException();
		}
		#endregion

		#region getContentTypeFromExtention
		private static string getContentTypeFromExtention(string extention)
		{
			string ext = extention.ToLower().Trim();
			if (ext == "jpg" || ext == "jpeg" || ext == "jpe")
				return "image/jpeg";
			else if (ext == "gif")
				return "image/gif";
			else if (ext == "png")
				return "image/png";
			else if (ext == "avi")
				return "video/avi";
			else if (ext == "dv")
				return "video/x-dv";
			else if (ext == "mov" || ext == "qt")
				return "video/quicktime";
			else if (ext == "mpg" || ext == "mpeg")
				return "video/mpeg";
			else if (ext == "mp4")
				return "video/mp4";
			else if (ext == "3gp")
				return "video/3gpp";
			else if (ext == "3g2")
				return "video/3gpp2";
			else if (ext == "asf")
				return "video/x-ms-asf";
			else if (ext == "wmv")
				return "video/x-ms-wmv";
			else if (ext == "swf")
				return "application/x-shockwave-flash";
			else if (ext == "flv")
				return "video/x-flv";
			else
				throw new NotImplementedException();
		}
		#endregion
		#region getAmazonS3BucketName
		private static string getAmazonS3BucketName(Locations location, bool read)
		{
			string test = Vars.DevEnv ? "-test" : "";
			if (read)
				test = Vars.DevEnvPix ? "-test" : "";

			if (location == Locations.PixAmazonEU)
				return "pix" + test + "-eu.dontstayin.com";
			else if (location == Locations.PixAmazonUS)
				return "pix" + test + "-us.dontstayin.com";
			else if (location == Locations.MasterAmazonEU)
				return "pixmaster" + test + "-eu.dontstayin.com";
			else if (location == Locations.MasterAmazonUS)
				return "pixmaster" + test + "-us.dontstayin.com";
			else
				throw new Exception();
		}
		#endregion
		#region getAmazonS3DomainName
		private static string getAmazonS3DomainName(Locations location)
		{
			if (Vars.DevEnvPix)
			{
				return getAmazonS3BucketName(location, true);
			}
			else
			{	
				if (location == Locations.PixAmazonEU)
					return ServeFromCdn ? "pix-cdn.dontstayin.com" : "pix-eu.dontstayin.com";
				else if (location == Locations.PixAmazonUS)
					return "pix-us.dontstayin.com";
				else if (location == Locations.MasterAmazonEU)
					return "pixmaster-eu.dontstayin.com";
				else if (location == Locations.MasterAmazonUS)
					return "pixmaster-us.dontstayin.com";
				else
					throw new Exception();
			}
		}
		#endregion
		#region getLocation
		private static Locations getLocation(Stores store)
		{
			if (store == Stores.Temporary)
			{
				return Locations.Temporary;
			}
			else
			{
				if (store == Stores.Master)
					return Locations.MasterAmazonEU;
				else
					return Locations.PixAmazonEU;
			}
		}
		#endregion
		#region ServeFromCdn
		public static bool ServeFromCdn
		{
			get
			{
				return Common.Settings.ServePixFromCdn == Common.Settings.ServePixFromCdnOption.On || (Prefs.Current != null && Prefs.Current["AmazonPix"].Exists && Prefs.Current["AmazonPix"] == 4);
			}
		}
		#endregion
		#region getStore(Locations location)
		private static Stores getStore(Locations location)
		{
			if (location == Locations.Master ||
				location == Locations.MasterNew ||
				location == Locations.MasterAmazonEU ||
				location == Locations.MasterAmazonUS)
			{
				return Stores.Master;
			}
			else if (location == Locations.Pix ||
				location == Locations.PixNew ||
				location == Locations.Static ||
				location == Locations.PixAmazonUS ||
				location == Locations.PixAmazonEU)
			{
				return Stores.Pix;
			}
			else if (location == Locations.Temporary)
			{
				return Stores.Temporary;
			}
			else
				throw new NotImplementedException();
		}
		#endregion
		#region getLocationType(Locations location)
		private static LocationTypes getLocationType(Locations location)
		{
			if (location == Locations.MasterAmazonEU ||
				location == Locations.MasterAmazonUS ||
				location == Locations.PixAmazonEU ||
				location == Locations.PixAmazonUS)
			{
				return LocationTypes.Amazon;
			}
			else if (location == Locations.Master ||
				location == Locations.MasterNew ||
				location == Locations.Pix ||
				location == Locations.PixNew ||
				location == Locations.Static ||
				location == Locations.Temporary)
			{
				return LocationTypes.FileSystem;
			}
			else
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Path
		public static string Path(Guid guid)
		{
			return Path(guid, "jpg");
		}
		public static string Path(Guid guid, string extention)
		{
			return Path(guid, extention, Stores.Pix);
		}
		public static string Path(Guid guid, Stores store)
		{
			return Path(guid, "jpg", store);
		}
		public static string Path(Guid guid, string extention, Stores store)
		{
			if (store == Stores.Temporary)
				throw new NotImplementedException();
			else
				return path(guid, extention, getLocation(store));
		}
		private static string path(Guid guid, string extention, Locations location)
		{
			if (getLocationType(location) == LocationTypes.Amazon)
			{
				if (Vars.UrlScheme == "http")
					return "http://" + getAmazonS3DomainName(location) + "/" + guid.ToString() + "." + extention.ToLower();
				else
					return Vars.UrlScheme + "://" + Vars.DomainName + "/images/" + (getStore(location) == Stores.Master ? "master" : "pix") + "/" + guid.ToString() + "." + extention.ToLower();
			}
			else
			{
				if (Vars.DevEnvPix)
				{
					return "/" + (getStore(location) == Stores.Master ? "pixmaster" : "pix") + "/" + guid.ToString() + "." + extention.ToLower();
				}
				else
				{
					if (getStore(location) == Stores.Master)
						return Vars.UrlScheme + "://pixmaster.dontstayin.com/" + guid.ToString().Substring(0, 2) + "/" + guid.ToString().Substring(2, 2) + "/" + guid.ToString() + "." + extention.ToLower();
					else
						return Vars.UrlScheme + "://s" + guid.ToString()[0] + ".dontstayin.com/" + guid.ToString().Substring(0, 2) + "/" + guid.ToString().Substring(2, 2) + "/" + guid.ToString() + "." + extention.ToLower();
				}
			}
		}
		
		#endregion
		#region PathJavascriptFunction
		public static string PathJavascriptFunction()
		{
			return @"
<script>
	function StoragePath(guid, extention, store)
	{
		if (store == null)
			store = 1;
		if (extention == null)
			extention = ""jpg"";
		
		if (window.location.protocol == ""http:"")
		{
			if (guid.substring(8,9) == ""-"")
				return ""http://"" + (store == 2 ? """ + getAmazonS3DomainName(Locations.MasterAmazonEU) + @""" : """ + getAmazonS3DomainName(Locations.PixAmazonEU) + @""") + ""/"" + guid + ""."" + extention;
			else
				return ""http://graph.facebook.com/"" + guid + ""/picture"";
		}
		else
			return ""/images/"" + (store == 2 ? ""master"" : ""pix"") + ""/"" + guid + ""."" + extention;
	}
</script>
";
		}
		#endregion

		static bool isSuccessCode(HttpStatusCode code)
		{
			return code == HttpStatusCode.OK || code == HttpStatusCode.NoContent;
		}
		static bool isFailCode(HttpStatusCode code)
		{
			return !isSuccessCode(code);
		}


		#region Retry
		public static T Retry<T>(Func<T> func, int count)
		{
			for (int i = 0; i < count; i++)
			{
				try
				{
					return func();
				}
				catch (WebException ex)
				{
					System.Threading.Thread.Sleep(Common.ThreadSafeRandom.Next(50, 100));
					if (i >= count - 1)
						throw ex;
				}
			}
			throw new NotImplementedException();
		}
		public static void Retry(Action action, int count)
		{
			for (int i = 0; i < count; i++)
			{
				try
				{
					action();
					return;
				}
				catch (WebException ex)
				{
					System.Threading.Thread.Sleep(Common.ThreadSafeRandom.Next(50, 100));
					if (i >= count - 1)
						throw ex;
				}
			}
			throw new NotImplementedException();
		}
		#endregion
		#region Put
		/// <param name="metaData">Optional sorted list with keys: "ObjectType" [Model.Entities.ObjectType] , "ObjectK" [int], "ObjectData" [string]</param>
		private static void Put(byte[] data, Guid guid, string extention, Locations location, SortedList metaData)
		{
			if (getLocationType(location) == LocationTypes.Amazon)
			{
				try
				{
					string bucket = getAmazonS3BucketName(location, false);
					string name = guid.ToString() + "." + extention.ToLower().Trim();
					string contentType = getContentTypeFromExtention(extention);
					AWSAuthConnection conn = getAmazonConnection();

					SortedList headers = new SortedList();
					headers.Add("Content-Type", contentType);
					headers.Add("x-amz-acl", "public-read");
					headers.Add("Content-MD5", calculateMD5Hash(data));

					S3Object ob = new S3Object(data, metaData);

					Retry(() => { using ( Response r = conn.put(bucket, name, ob, headers) ) { if (isFailCode(r.Status)) throw new WebException("amazon put failed because response http status code not ok - status was " + r.Status.ToString()); } }, 10);
				}
				catch(Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't put the amazon object [" + location.ToString() + ", " + guid.ToString() + "." + extention + "]...", ex);
				}
				return;
			}
			else if (getLocationType(location) == LocationTypes.FileSystem)
			{
				try
				{
					File.WriteAllBytes(fileSystemPath(guid, extention, location), data);
				}
				catch (Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't write the file [" + location.ToString() + ", " + guid.ToString() + "." + extention + "]...", ex);
				}
			}
			else
				throw new NotImplementedException();
		}
		#endregion
		#region Get
		private static byte[] Get(Guid guid, string extention, Locations location)
		{
			if (getLocationType(location) == LocationTypes.Amazon)
			{
				try
				{
					string bucket = getAmazonS3BucketName(location, true);
					string name = guid.ToString() + "." + extention.ToLower().Trim();
					AWSAuthConnection conn = getAmazonConnection();

					return Retry(() => { using (GetResponse r = conn.get(bucket, name, null)) { if (isSuccessCode(r.Status)) return r.Object.Bytes; else throw new WebException("amazon get failed because response http status code not ok - status was " + r.Status.ToString()); } }, 10);

				}
				catch (Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't get the amazon object [" + location.ToString() + ", " + guid.ToString() + "." + extention + "]...", ex);
				}
			}
			else if (getLocationType(location) == LocationTypes.FileSystem)
			{
				try
				{
					Console.WriteLine(fileSystemPath(guid, extention, location));
					return File.ReadAllBytes(fileSystemPath(guid, extention, location));
				}
				catch (Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't read the file [" + location.ToString() + ", " + guid.ToString() + "." + extention + "]...", ex);
				}
			}
			else
				throw new NotImplementedException();
		}
		#endregion
		#region Delete
		private static void Delete(Guid guid, string extention, Locations location)
		{
			if (getLocationType(location) == LocationTypes.Amazon)
			{
				try
				{
					string bucket = getAmazonS3BucketName(location, false);
					string name = guid.ToString() + "." + extention.ToLower().Trim();
					AWSAuthConnection conn = getAmazonConnection();

					//Retry(() => { using (Response r = conn.delete(bucket, name, null)) { if (r.Status != HttpStatusCode.OK) throw new WebException("amazon delete failed because response http status code not ok - status was " + r.Status.ToString()); } }, 10);

					using (Response r = conn.delete(bucket, name, null)) 
					{
						if (isFailCode(r.Status))
							throw new WebException("amazon delete failed because response http status code not ok - status was " + r.Status.ToString()); 
					}
					
				}
				catch (Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't delete the amazon object [" + location.ToString() + ", " + guid.ToString() + "." + extention + "]...", ex);
				}

				return;
			}
			else if (getLocationType(location) == LocationTypes.FileSystem)
			{
				try
				{
					File.Delete(fileSystemPath(guid, extention, location));
				}
				catch (Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't delete the file [" + location.ToString() + ", " + guid.ToString() + "." + extention + "]...", ex);
				}
			}
			else
				throw new NotImplementedException();
		}
		#endregion
		#region Copy
		private static void Copy(Guid guid, string extention, Locations sourceLocation, Locations destinationLocation, SortedList destinationMetaData)
		{
			Copy(
				guid, extention, sourceLocation,
				guid, extention, destinationLocation, 
				destinationMetaData);
		}
		private static void Copy(Guid sourceGuid, string sourceExtention, Locations sourceLocation, Guid destinationGuid, string destinationExtention, Locations destinationLocation, SortedList destinationMetaData)
		{
			byte[] bytes = null;

			try
			{
				bytes = Get(sourceGuid, sourceExtention, sourceLocation);
			}
			catch (Exception ex)
			{
				throw new StorageOperationFailedException("we couldn't get the item...", ex);
			}

			try
			{
				Put(bytes, destinationGuid, destinationExtention, destinationLocation, destinationMetaData);
			}
			catch (Exception ex)
			{
				throw new StorageOperationFailedException("we couldn't put the item...", ex);
			}

		}
		#endregion
		#region Move
		private static void Move(Guid guid, string extention, Locations sourceLocation, Locations destinationLocation, SortedList destinationMetaData)
		{
			Move(
				guid, extention, sourceLocation,
				guid, extention, destinationLocation,
				destinationMetaData);
		}
		private static void Move(Guid sourceGuid, string sourceExtention, Locations sourceLocation, Guid destinationGuid, string destinationExtention, Locations destinationLocation, SortedList destinationMetaData)
		{
			try
			{
				Copy(sourceGuid, sourceExtention, sourceLocation, destinationGuid, destinationExtention, destinationLocation, destinationMetaData);
			}
			catch(Exception ex)
			{
				//we couldn't copy the file...
				throw new StorageOperationFailedException("we couldn't copy the item...", ex);
			}

			try
			{
				//we delete the source file
				Delete(sourceGuid, sourceExtention, sourceLocation);
				return;
			}
			catch(Exception ex)
			{
				try
				{
					//we can't delete the source - so we delete the destination to revert to previous state...
					Delete(destinationGuid, destinationExtention, destinationLocation);
				}
				catch (Exception ex1)
				{
					//we can't delete the destination also, so we throw an operation corrupt error because we can't restore to the previous state...
					throw new StorageStateRestoreFailedException("both the delete source and delete destination operations failed, so we can't restore to the previous state...", ex1);
				}

				//the operation failed, but we managed to restore to the previous state...
				throw new StorageOperationFailedException("the delete source operation failed, but we managed to restore to the previous state by deleting the destination...", ex);
			}
		}
		#endregion
		#region Exists
		public static bool Exists(Guid guid, string extention, Locations location)
		{
			if (getLocationType(location) == LocationTypes.Amazon)
			{
				try
				{
					string bucket = getAmazonS3BucketName(location, true);
					string name = guid.ToString() + "." + extention.ToLower().Trim();
					AWSAuthConnection conn = getAmazonConnection();

					ArrayList entries = Retry(() => { using (ListBucketResponse r = conn.listBucket(bucket, name, null, 0, null)) { if (isSuccessCode(r.Status)) return r.Entries; else throw new WebException("amazon list bucket failed because response http status code not ok - status was " + r.Status.ToString()); } }, 10);
					if (entries == null)
					{
						return false;
					}
					else
					{
						foreach (ListEntry entry in entries)
						{
							if (entry.Key == name)
								return true;
						}
						return false;
					}
				}
				catch (Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't read the amazon bucket list [" + location.ToString() + ", " + guid.ToString() + "." + extention + "]...", ex);
				}
			}
			else if (getLocationType(location) == LocationTypes.FileSystem)
			{
				try
				{
					return File.Exists(fileSystemPath(guid, extention, location));
				}
				catch (Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't tell if the file exists [" + location.ToString() + ", " + guid.ToString() + "." + extention + "]...", ex);
				}
			}
			else
				throw new NotImplementedException();
		}
		#endregion
		public static bool CheckMD5(Guid guid, string extention, Locations location1, Locations location2)
		{
			string hash1 = calculateMD5Hash(guid, extention, location1);
			string hash2 = calculateMD5Hash(guid, extention, location2);
			return hash1.Equals(hash2);

		}

		#region fileSystemPath
		private static string fileSystemPath(Guid Name, string Extention, Locations Location)
		{
			if (Common.Properties.IsDevelopmentEnvironment)
				return @"c:\dontstayin\" + (Location == Locations.Temporary ? "temporary" : (Location == Locations.Master ? "pixmaster" : "pix")) + @"\" + Name.ToString() + "." + Extention;
			else
			{
				if (Location.Equals(Locations.Pix))
					return @"\\" + Vars.PixIp + @"\C$\DontStayIn\Live\Pix\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
				else if (Location.Equals(Locations.PixNew))
					throw new Exception("PixNew is undefined!");//return @"\\" + Vars.PixServerIp + @"\C$\DontStayIn\Live\Pix\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
				else if (Location.Equals(Locations.Master))
					return @"\\" + Vars.PixMasterIp + @"\C$\DontStayIn\Live\PixMaster\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
				else if (Location.Equals(Locations.MasterNew))
					throw new Exception("PixMasterNew is undefined!");//return @"\\" + Vars.PixMasterIp + @"\C$\DontStayIn\Live\PixMasterOffice\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
				else if (Location.Equals(Locations.Temporary))
					return @"\\" + Vars.PixMasterIp + @"\C$\DontStayIn\Live\Temporary\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString() + "." + Extention;
				else if (Location.Equals(Locations.Static))
					return @"\\" + Vars.StaticContentIp(Name) + @"\C$\DontStayIn\Live\Pix" + Name.ToString().Substring(0, 1).ToUpper() + @"\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
				else
					throw new NotImplementedException();
			}
		}
		#endregion
		#region TemporaryFilesystemPath
		public static string TemporaryFilesystemPath(Guid guid, string extention)
		{
			return fileSystemPath(guid, extention, Locations.Temporary);
		}
		#endregion

		#region calculateMD5Hash
		static string calculateMD5Hash(Guid guid, string extention, Locations location)
		{
			if (getLocationType(location) == LocationTypes.Amazon)
			{
				try
				{
					string bucket = getAmazonS3BucketName(location, true);
					string name = guid.ToString() + "." + extention.ToLower().Trim();
					AWSAuthConnection conn = getAmazonConnection();

					ArrayList entries = Retry(() => { using (ListBucketResponse r = conn.listBucket(bucket, name, null, 0, null)) { if (isSuccessCode(r.Status)) return r.Entries; else throw new WebException("amazon list bucket failed because response http status code not ok - status was " + r.Status.ToString()); } }, 10);
					if (entries == null)
					{
						throw new Exception("we couldn't find the amazon object. we didn't return any objects from the bucket list...");
					}
					else
					{
						foreach (ListEntry entry in entries)
						{
							if (entry.Key == name)
								return entry.ETag;
						}
						throw new Exception("we couldn't find the amazon object. we returned objects from the bucket list, but none of them exactly matched our object name...");
					}
				}
				catch (Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't get the amazon MD5 etag...", ex);
				}
			}
			else if (getLocationType(location) == LocationTypes.FileSystem)
			{
				try
				{
					return calculateMD5Hash(fileSystemPath(guid, extention, location));
				}
				catch (Exception ex)
				{
					throw new StorageOperationFailedException("we couldn't calculate the MD5 of this file...", ex);
				}
			}
			else
				throw new NotImplementedException();
		}
		static string calculateMD5Hash(byte[] inputBytes)
		{
			MD5 md5 = MD5.Create();
			return Convert.ToBase64String(md5.ComputeHash(inputBytes));
		}
		static string calculateMD5Hash(string fileName)
		{
			using (FileStream file = new FileStream(fileName, FileMode.Open))
			{
				//MD5 md5 = new MD5CryptoServiceProvider();
				MD5 md5 = MD5.Create();
				string outStr = Convert.ToBase64String(md5.ComputeHash(file));
				file.Close();
				return outStr;
			}
		}
		#endregion

		#region _CopyToAmazon
		public static void _CopyToAmazon(Guid name, IBob metaDataObject, string metaDataObjectData)
		{
			_CopyToAmazon(name, "jpg", metaDataObject, metaDataObjectData);
		}
		public static void _CopyToAmazon(Guid name, string extention, IBob metaDataObject, string metaDataObjectData)
		{
			SortedList metaData = null;
			if (metaDataObject != null && metaDataObject is IBobType)
			{
				metaData = new SortedList();
				metaData.Add("ObjectType", ((IHasObjectType)metaDataObject).ObjectType);
				metaData.Add("ObjectK", ((IHasSinglePrimaryKey)metaDataObject).K);
				if (metaDataObjectData != null && metaDataObjectData.Length > 0)
					metaData.Add("ObjectData", metaDataObjectData);
			}


			Storage.Copy(name, extention, Locations.Pix, Locations.PixAmazonEU, metaData);
			//try
			//{
			//    Storage.Copy(name, extention, Locations.Pix, Locations.PixAmazonUS, metaData);
			//}
			//catch (Exception ex)
			//{
			//    Storage.Delete(name, extention, Locations.PixAmazonEU);
			//    throw ex;
			//}
		}

		public static void _CopyToAmazonIfMD5Mismatch(Guid name, IBob metaDataObject, string metaDataObjectData)
		{
			_CopyToAmazonIfMD5Mismatch(name, "jpg", metaDataObject, metaDataObjectData);
		}
		public static void _CopyToAmazonIfMD5Mismatch(Guid name, string extention, IBob metaDataObject, string metaDataObjectData)
		{

			string hashPix = "";
			//string hashUs = "";
			string hashEu = "";

			if (!Exists(name, extention, Locations.Pix))
				throw new Exception("can't find pix file...");

			hashPix = calculateMD5Hash(name, extention, Locations.Pix);

			//try
			//{
			//    hashUs = calculateMD5Hash(name, extention, Locations.PixAmazonUS);
			//}
			//catch { }

			try
			{
				hashEu = calculateMD5Hash(name, extention, Locations.PixAmazonEU);
			}
			catch { }

			SortedList metaData = null;
			if (metaDataObject != null && metaDataObject is IBobType)
			{
				metaData = new SortedList();
				metaData.Add("ObjectType", ((IHasObjectType)metaDataObject).ObjectType);
				metaData.Add("ObjectK", ((IHasSinglePrimaryKey)metaDataObject).K);
				if (metaDataObjectData != null && metaDataObjectData.Length > 0)
					metaData.Add("ObjectData", metaDataObjectData);
			}

			bool failed = false;

			//if (!hashPix.Equals(hashUs) || hashUs.Length == 0)
			//{
			//    try
			//    {
			//        Storage.Copy(name, extention, Locations.Pix, Locations.PixAmazonUS, metaData);
			//    }
			//    catch
			//    {
			//        failed = true;
			//    }
			//}

			if (!hashPix.Equals(hashEu) || hashEu.Length == 0)
			{
				try
				{
					Storage.Copy(name, extention, Locations.Pix, Locations.PixAmazonEU, metaData);
				}
				catch
				{
					failed = true;
				}
			}

			if (failed)
			{
				throw new Exception("failed copying one or both the files...");
			}

		}

		public static void _CopyMasterToAmazon(Guid name, IBob metaDataObject, string metaDataObjectData)
		{
			_CopyMasterToAmazon(name, "jpg", metaDataObject, metaDataObjectData);
		}
		public static void _CopyMasterToAmazon(Guid name, string extention, IBob metaDataObject, string metaDataObjectData)
		{
			SortedList metaData = null;
			if (metaDataObject != null && metaDataObject is IHasObjectType && metaDataObject is IHasSinglePrimaryKey)
			{
				metaData = new SortedList();
				metaData.Add("ObjectType", ((IHasObjectType)metaDataObject).ObjectType);
				metaData.Add("ObjectK", ((IHasSinglePrimaryKey)metaDataObject).K);
				if (metaDataObjectData != null && metaDataObjectData.Length > 0)
					metaData.Add("ObjectData", metaDataObjectData);
			}
			Storage.Copy(name, extention, Locations.Master, Locations.MasterAmazonEU, metaData);
		}

		public static void _CopyMasterToAmazonIfMD5Mismatch(Guid name, IBob metaDataObject, string metaDataObjectData)
		{
			_CopyMasterToAmazonIfMD5Mismatch(name, "jpg", metaDataObject, metaDataObjectData);
		}
		public static void _CopyMasterToAmazonIfMD5Mismatch(Guid name, string extention, IBob metaDataObject, string metaDataObjectData)
		{
			
			string hashMaster = "";
			string hashEu = "";

			if (!Exists(name, extention, Locations.Master))
				throw new Exception("can't find master file...");

			hashMaster = calculateMD5Hash(name, extention, Locations.Master);

			try
			{
				hashEu = calculateMD5Hash(name, extention, Locations.MasterAmazonEU);
			}
			catch { }


			SortedList metaData = null;
			if (metaDataObject != null && metaDataObject is IHasObjectType && metaDataObject is IHasSinglePrimaryKey)
			{
				metaData = new SortedList();
				metaData.Add("ObjectType", ((IHasObjectType)metaDataObject).ObjectType);
				metaData.Add("ObjectK", ((IHasSinglePrimaryKey)metaDataObject).K);
				if (metaDataObjectData != null && metaDataObjectData.Length > 0)
					metaData.Add("ObjectData", metaDataObjectData);
			}
			bool failed = false;
			if (!hashMaster.Equals(hashEu) || hashEu.Length == 0)
			{
				try
				{
					Storage.Copy(name, extention, Locations.Master, Locations.MasterAmazonEU, metaData);
				}
				catch
				{
					failed = true;
				}
			}

			if (failed)
			{
				throw new Exception("failed copying master file...");
			}
		}
		#endregion
		#region _CopyFromMasterToTemporary
		public static void _CopyFromMasterToTemporary(Guid guid, string extention)
		{
			//Copy(guid, extention, Locations.Master, Locations.Temporary, null);
			File.Copy(
				fileSystemPath(guid, extention, Locations.Master),
				fileSystemPath(guid, extention, Locations.Temporary).ToLower(),
				true
			);
		}
		public static void _CopyFromMasterToTemporary(Guid sourceGuid, Guid destGuid, string extention)
		{
			File.Copy(
				fileSystemPath(sourceGuid, extention, Locations.Master),
				fileSystemPath(destGuid, extention, Locations.Temporary).ToLower(),
				true
			);
		}
		#endregion
		#region _GetFileSystemPath
		public static string _GetFileSystemPath(Guid Name, string Extention, Locations Location)
		{
			return fileSystemPath(Name, Extention, Location);
		}
		#endregion

		#region WriteFileToHttpResponse
		public static void WriteFileToHttpResponse(System.Web.HttpContext context, Stores store, Guid guid, string extention)
		{
			byte[] bytes = Storage.GetFromStore(store, guid, extention);
			context.Response.ContentType = getContentTypeFromExtention(extention);
			context.Response.BinaryWrite(bytes);
		}
		#endregion

		

	}
	public class StorageOperationFailedException : Exception { public StorageOperationFailedException(string description, Exception innerException) : base(description, innerException) { } }
	public class StorageStateRestoreFailedException : Exception { public StorageStateRestoreFailedException(string description, Exception innerException) : base(description, innerException) { } }

	namespace StorageScriptCompatibility
	{
		public class Misc
		{
			public static string GetPicUrlFromGuid(string guid)
			{
				return Storage.Path(new Guid(guid), Storage.Stores.Pix);
			}
		}
	}

	public class Cropper : System.Web.UI.UserControl, IPostBackDataHandler
	{
		protected Panel FlashPanel, DartPanel;
		protected HtmlInputHidden cW, cH, iW, iH, xO, yO, sC;
		protected HtmlGenericControl MovieParam, MovieEmbed;
		protected PlaceHolder FlashPlaceHolder, DartPlaceHolder;
		protected HtmlInputHidden cCropWidth, cCropHeight, cXOffset, cYOffset, cZoom, cImageUrl, cImageOriginalWidth, cImageOriginalHeight, cAllowCustomHeight, cAllowCustomWidth, cMaxWidth, cMaxHeight, cMinWidth, cMinHeight;

		#region AllowCustomWidth
		public bool AllowCustomWidth
		{
			get
			{
				return allowCustomWidth;
			}
			set
			{
				allowCustomWidth = value;
			}
		}
		private bool allowCustomWidth = false;
		#endregion
		#region AllowCustomHeight
		public bool AllowCustomHeight
		{
			get
			{
				return allowCustomHeight;
			}
			set
			{
				allowCustomHeight = value;
			}
		}
		private bool allowCustomHeight = false;
		#endregion
		#region MaxHeight
		public int MaxHeight
		{
			get
			{
				return maxHeight;
			}
			set
			{
				maxHeight = value;
			}
		}
		private int maxHeight = 100;
		#endregion
		#region MinHeight
		public int MinHeight
		{
			get
			{
				return minHeight;
			}
			set
			{
				minHeight = value;
			}
		}
		private int minHeight = 100;
		#endregion
		#region MaxWidth
		public int MaxWidth
		{
			get
			{
				return maxWidth;
			}
			set
			{
				maxWidth = value;
			}
		}
		private int maxWidth = 100;
		#endregion
		#region MinWidth
		public int MinWidth
		{
			get
			{
				return minWidth;
			}
			set
			{
				minWidth = value;
			}
		}
		private int minWidth = 100;
		#endregion
		#region ShowTextHelpers
		public bool ShowTextHelpers
		{
			get
			{
				return showTextHelpers;
			}
			set
			{
				showTextHelpers = value;
			}
		}
		private bool showTextHelpers = true;
		#endregion
		#region ImageUrl
		public string ImageUrl
		{
			get
			{
				return imageUrl;
			}
			set
			{
				imageUrl = value;
			}
		}
		private string imageUrl = "";
		#endregion
		#region ImageGuid
		public Guid ImageGuid { get; set; }
		#endregion
		#region ImageStore
		public Storage.Stores ImageStore { get; set; }
		#endregion

		#region Zoom
		public double Zoom
		{
			get
			{
				return zoom;
			}
			set
			{
				zoom = value;
			}
		}
		private double zoom = 0.0;
		#endregion
		#region XOffset
		public double XOffset
		{
			get
			{
				return xOffset;
			}
			set
			{
				xOffset = value;
			}
		}
		private double xOffset = 0.0;
		#endregion
		#region YOffset
		public double YOffset
		{
			get
			{
				return yOffset;
			}
			set
			{
				yOffset = value;
			}
		}
		private double yOffset = 0.0;
		#endregion
		#region CropWidth
		public int CropWidth
		{
			get
			{
				return cropWidth;
			}
			set
			{
				cropWidth = value;
			}
		}
		private int cropWidth = 100;
		#endregion
		#region CropHeight
		public int CropHeight
		{
			get
			{
				return cropHeight;
			}
			set
			{
				cropHeight = value;
			}
		}
		private int cropHeight = 100;
		#endregion
		#region ZoomedImageWidth
		public double ZoomedImageWidth
		{
			get
			{
				return zoomedImageWidth;
			}
			set
			{
				zoomedImageWidth = value;
			}
		}
		private double zoomedImageWidth;
		#endregion
		#region ZoomedImageHeight
		public double ZoomedImageHeight
		{
			get
			{
				return zoomedImageHeight;
			}
			set
			{
				zoomedImageHeight = value;
			}
		}
		private double zoomedImageHeight;
		#endregion
		#region ZoomBarLength
		public int ZoomBarLength
		{
			get
			{
				return zoomBarLength;
			}
			set
			{
				zoomBarLength = value;
			}
		}
		private int zoomBarLength = 300;
		#endregion
		#region ZoomBarPosition
		public int ZoomBarPosition
		{
			get
			{
				return zoomBarPosition;
			}
			set
			{
				zoomBarPosition = value;
			}
		}
		private int zoomBarPosition = 30;
		#endregion
		#region ZoomBarThickness
		public int ZoomBarThickness
		{
			get
			{
				return zoomBarThickness;
			}
			set
			{
				zoomBarThickness = value;
			}
		}
		private int zoomBarThickness = 4;
		#endregion
		#region ControlHeight
		public int ControlHeight
		{
			get
			{
				return controlHeight;
			}
			set
			{
				controlHeight = value;
			}
		}
		private int controlHeight = 250;
		#endregion
		public bool ClientDataIsCorrupt;

		#region GetState(), SetState()
		public string GetState()
		{
			return XOffset.ToString() + "$" + YOffset.ToString() + "$" + Zoom.ToString() + "$" + CropWidth.ToString() + "$" + CropHeight.ToString();
		}
		public void SetState(string state)
		{
			try
			{
				string[] stateAry = state.Split('$');
				XOffset = double.Parse(stateAry[0]);
				YOffset = double.Parse(stateAry[1]);
				Zoom = double.Parse(stateAry[2]);
				CropWidth = int.Parse(stateAry[3]);
				CropHeight = int.Parse(stateAry[4]);
			}
			catch { }
		}
		#endregion
		#region Store
		public void Store(Guid guid, IBob parent, string parentData)
		{
			byte[] b = GetBytes();
			Storage.AddToStore(b, Storage.Stores.Pix, guid, "jpg", parent, parentData);
		}
		public byte[] GetBytes()
		{
			if (ClientDataIsCorrupt)
				throw new Exception("Corrupt client data!");

			using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(Storage.GetFromStore(ImageStore, ImageGuid, "jpg"))))
			{
				#region Change Zoom, CropHeight and CropWidth if Zoom == 0.0
				if (Zoom == 0.0)
				{
					CropWidth = 100; // why?
					CropHeight = 100; // why?
					double minXZoom = 100.0 * (double)CropWidth / (double)image.Width;
					double minYZoom = 100.0 * (double)CropHeight / (double)image.Height;
					Zoom = (double)Math.Max(minXZoom, minYZoom);
				}
				#endregion

				#region Localise our variables
				int zoomedImageWidth = (int)Math.Floor(image.Width * Zoom / 100.0);
				int zoomedImageHeight = (int)Math.Floor(image.Height * Zoom / 100.0);
				double xOff = (image.Width / 2.0) - XOffset;
				double yOff = (image.Height / 2.0) - YOffset;
				int zoomedX = (int)Math.Round((xOff * Zoom / 100.0) - CropWidth / 2.0);
				int zoomedY = (int)Math.Round((yOff * Zoom / 100.0) - CropHeight / 2.0);
				#endregion

				Photo.OperationReturn operation = Photo.Operation(
					image,
					Photo.OperationType.Crop,
					new Photo.OperationParams()
					{
						CropSize = new Size(CropWidth, CropHeight),
						CropResize = new Size(zoomedImageWidth, zoomedImageHeight),
						CropOffset = new Size(zoomedX, zoomedY),
						ReturnBytes = true
					}
				);
				return operation.Bytes;
			}
		}
		#endregion

		#region TryToGetLargerPic
		public static byte[] TryToGetLargerPic(IPic pic, double factor)
		{
			if (pic.PicMiscK > 0 && pic.PicMisc != null)
			{
				try
				{
					Cropper c = new Cropper();
					//c.ImageUrl = e.PicMisc.;
					c.ImageGuid = pic.PicMisc.Guid;
					c.ImageStore = Storage.Stores.Pix;
					c.SetState(pic.PicState);

					int minDimension = pic.PicMisc.Height > pic.PicMisc.Width ? pic.PicMisc.Width : pic.PicMisc.Height;
					if (minDimension < (int)(100 * factor))
					{
						factor = factor * ((double)minDimension / (double)(int)(100 * factor));
					}

					c.CropHeight = 179;
					c.CropWidth = 179;
					c.Zoom = c.Zoom * 1.79;

					c.ResetStateToEnsureImageIsWithinCropArea();

					return c.GetBytes();
				}
				catch
				{
					return Storage.GetFromStore(Storage.Stores.Pix, pic.Pic, "jpg");
				}
			}
			else
			{
				return Storage.GetFromStore(Storage.Stores.Pix, pic.Pic, "jpg");
			}
		}
		#endregion

		#region ResetStateToEnsureImageIsWithinCropArea()
		public void ResetStateToEnsureImageIsWithinCropArea()
		{
			using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(Storage.GetFromStore(ImageStore, ImageGuid, "jpg"))))
			{

				//work out if the dimensions of the crop area are larger than the dimentions of the image... - if so, change the zoom level
				{
					int zoomedImageWidth = (int)Math.Floor(image.Width * Zoom / 100.0);
					int zoomedImageHeight = (int)Math.Floor(image.Height * Zoom / 100.0);
					if (zoomedImageWidth < CropWidth || zoomedImageHeight < CropHeight)
					{
						double minXZoom = 100.0 * (double)CropWidth / (double)image.Width;
						double minYZoom = 100.0 * (double)CropHeight / (double)image.Height;
						Zoom = (double)Math.Max(minXZoom, minYZoom);
					}
				}

				//work out if any of the crop area is off the edge of the image... - if so, change the x and y offset
				{
					int zoomedImageWidth = (int)Math.Floor(image.Width * Zoom / 100.0);
					int zoomedImageHeight = (int)Math.Floor(image.Height * Zoom / 100.0);
					double xOff = (image.Width / 2.0) - XOffset;
					double yOff = (image.Height / 2.0) - YOffset;
					int zoomedX = (int)Math.Round((xOff * Zoom / 100.0) - CropWidth / 2.0);
					int zoomedY = (int)Math.Round((yOff * Zoom / 100.0) - CropHeight / 2.0);

					if (zoomedX < 0.0)
					{
						double newZoomedX = 0.0;
						XOffset = (image.Width / 2.0) - ((newZoomedX + (CropWidth / 2.0)) * 100.0 / Zoom);
					}
					else if (zoomedX + CropWidth > zoomedImageWidth)
					{
						double newZoomedX = zoomedImageWidth - CropWidth;
						XOffset = (image.Width / 2.0) - ((newZoomedX + (CropWidth / 2.0)) * 100.0 / Zoom);
					}

					if (zoomedY < 0.0)
					{
						double newZoomedY = 0.0;
						YOffset = (image.Height / 2.0) - ((newZoomedY + (CropHeight / 2.0)) * 100.0 / Zoom);
					}
					else if (zoomedY + CropHeight > zoomedImageHeight)
					{
						double newZoomedY = zoomedImageHeight - CropHeight;
						YOffset = (image.Height / 2.0) - ((newZoomedY + (CropHeight / 2.0)) * 100.0 / Zoom);
					}

				}
			}
		}
		#endregion

		#region IsModernBrowser
		bool IsModernBrowser
		{
			get
			{
				//if (!Vars.DevEnv)
					return false; //Disabled - it doesn't work.

				if (HttpContext.Current.Request.UserAgent.ToLower().Contains("chrome"))
					return true;
				else if (HttpContext.Current.Request.UserAgent.ToLower().Contains("safari"))
					return HttpContext.Current.Request.Browser.MajorVersion >= 5;
				else if (HttpContext.Current.Request.UserAgent.ToLower().Contains("firefox"))
					return HttpContext.Current.Request.Browser.MajorVersion >= 4;
				else
					return false;
			}
		}
		#endregion

		#region Page_Load()
		private void Page_Load(object sender, System.EventArgs e)
		{
			Page.RegisterRequiresPostBack(this);
		}
		#endregion
		#region Page_PreRender()
		public void Page_PreRender(object o, System.EventArgs e)
		{
			ScriptManager.RegisterClientScriptInclude(this, typeof(Page), "CropperJs", "/misc/cropper.js");
		}
		#endregion
		#region Render()
		protected override void Render(HtmlTextWriter writer)
		{
			this.DataBind();

			FlashPanel.Visible = !IsModernBrowser;
			DartPanel.Visible = IsModernBrowser;

			if (IsModernBrowser)
			{
				StringBuilder sb = new StringBuilder();

				sb.Append(@"<input type=""hidden"" id=""cropperControlPrefix"" value=""" + this.ClientID + @"_"" />");

				cCropWidth.Value = CropWidth.ToString();
				cCropHeight.Value = CropHeight.ToString();
				cXOffset.Value = ((int)XOffset).ToString();
				cYOffset.Value = ((int)YOffset).ToString();
				cZoom.Value = Zoom.ToString();
				cImageUrl.Value = ImageUrl;
				cAllowCustomHeight.Value = AllowCustomHeight.ToString().ToLower();
				cAllowCustomWidth.Value = AllowCustomWidth.ToString().ToLower();
				cMaxWidth.Value = AllowCustomWidth ? MaxWidth.ToString() : CropWidth.ToString();
				cMaxHeight.Value = AllowCustomHeight ? MaxHeight.ToString() : CropHeight.ToString();
				cMinWidth.Value = AllowCustomWidth ? MinWidth.ToString() : CropWidth.ToString();
				cMinHeight.Value = AllowCustomHeight ? MinHeight.ToString() : CropHeight.ToString();

				DartPlaceHolder.Controls.Clear();
				DartPlaceHolder.Controls.Add(new LiteralControl(sb.ToString()));
			}
			else
			{

				string vars = GetFlashVars();

				StringBuilder sb = new StringBuilder();
				sb.Append(@"<div class=""BackgroundWhite"" id=""");
				sb.Append(this.ClientID);
				sb.Append(@"_FlashDiv""><table height=""");
				sb.Append(ControlHeight.ToString());
				sb.Append(@""" width=""100%""><tr><td valign=""middle"" align=""center"" style=""font-size:13px; font-weight:bold; padding:10px;"">You should see our photo cropper here, but it's not working! You either have JavaScript turned off or an old version of Macromedia's Flash Player. <a href=""http://www.macromedia.com/go/getflashplayer/"" target=""_blank"">Click here</a> to get the latest flash player.</td></tr></table></div>");
				sb.Append(@"<script>");
				sb.Append(@"var " + this.ClientID + @"_so = new SWFObject(""/misc/cropper-v2.swf?randomString=" + Cambro.Misc.Utility.GenRandomText(5) + @""", """ + this.ClientID + @"_mymovie"", ""100%"", " + ControlHeight.ToString() + @", ""7"", ""#ffffff"");");
				AddParameter(sb, "align", "middle");
				AddParameter(sb, "wmode", "transparent");
				AddParameter(sb, "quality", "best");
				AddParameter(sb, "allowScriptAccess", "always");
				AddParameter(sb, "loop", "false");
				AddParameter(sb, "menu", "false");

				AddVariable(sb, "iU", ImageUrl);
				if (ShowTextHelpers)
					AddVariable(sb, "tX", "true");
				AddVariable(sb, "iZ", Zoom.ToString());
				AddVariable(sb, "iW", CropWidth.ToString());
				AddVariable(sb, "iH", CropHeight.ToString());
				AddVariable(sb, "oX", XOffset.ToString());
				AddVariable(sb, "oY", YOffset.ToString());
				if (AllowCustomHeight)
					AddVariable(sb, "aH", "true");
				if (AllowCustomWidth)
					AddVariable(sb, "aW", "true");
				AddVariable(sb, "cW", MaxWidth.ToString());
				AddVariable(sb, "cH", MaxHeight.ToString());
				AddVariable(sb, "fW", MinWidth.ToString());
				AddVariable(sb, "fH", MinHeight.ToString());
				AddVariable(sb, "sB", ZoomBarLength.ToString());
				AddVariable(sb, "sH", ZoomBarThickness.ToString());
				AddVariable(sb, "sY", ZoomBarPosition.ToString());
				AddVariable(sb, "pT", this.UniqueID);

				sb.Append(this.ClientID + @"_so.write(""" + this.ClientID + @"_FlashDiv"");");

				sb.Append(@"</script>");

				FlashPlaceHolder.Controls.Clear();
				FlashPlaceHolder.Controls.Add(new LiteralControl(sb.ToString()));

				cW.Value = CropWidth.ToString();
				cH.Value = CropHeight.ToString();
				iW.Value = ZoomedImageWidth.ToString();
				iH.Value = ZoomedImageHeight.ToString();
				xO.Value = XOffset.ToString();
				yO.Value = YOffset.ToString();
				sC.Value = Zoom.ToString();
			}


			base.Render(writer);
		}
		void AddParameter(StringBuilder sb, string name, string value)
		{
			sb.Append(this.ClientID);
			sb.Append(@"_so.addParam(""");
			sb.Append(name);
			sb.Append(@""", """);
			sb.Append(value);
			sb.Append(@""");");
		}
		void AddVariable(StringBuilder sb, string name, string value)
		{
			sb.Append(this.ClientID);
			sb.Append(@"_so.addVariable(""");
			sb.Append(name);
			sb.Append(@""", """);
			sb.Append(value);
			sb.Append(@""");");
		}
		#endregion
		#region RaisePostDataChangedEvent()
		public void RaisePostDataChangedEvent()
		{

		}
		#endregion

		#region LoadPostData()
		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			if (this.Visible)
			{
				if (sC.Value == "NaN")
					ClientDataIsCorrupt = true;

				CropWidth = int.Parse(cW.Value);
				CropHeight = int.Parse(cH.Value);
				ZoomedImageWidth = double.Parse(iW.Value);
				ZoomedImageHeight = double.Parse(iH.Value);
				XOffset = double.Parse(xO.Value);
				YOffset = double.Parse(yO.Value);
				Zoom = double.Parse(sC.Value);
			}
			return false;
		}
		#endregion
		#region SaveViewState()
		protected override object SaveViewState()
		{

			this.ViewState["aH"] = AllowCustomHeight;
			this.ViewState["aW"] = AllowCustomWidth;
			this.ViewState["cH"] = MaxHeight;
			this.ViewState["cW"] = MaxWidth;
			this.ViewState["fH"] = MinHeight;
			this.ViewState["fW"] = MinWidth;
			this.ViewState["iH"] = CropHeight;
			this.ViewState["iW"] = CropWidth;
			this.ViewState["tX"] = ShowTextHelpers;
			this.ViewState["iU"] = ImageUrl;
			this.ViewState["imageGuid"] = ImageGuid;
			this.ViewState["imageStore"] = ImageStore;
			this.ViewState["iZ"] = Zoom;
			this.ViewState["oX"] = XOffset;
			this.ViewState["oY"] = YOffset;
			this.ViewState["sB"] = ZoomBarLength;
			this.ViewState["sY"] = ZoomBarPosition;
			this.ViewState["sH"] = ZoomBarThickness;
			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["aH"] != null) AllowCustomHeight = (bool)this.ViewState["aH"];
			if (this.ViewState["aW"] != null) AllowCustomWidth = (bool)this.ViewState["aW"];
			if (this.ViewState["cH"] != null) MaxHeight = (int)this.ViewState["cH"];
			if (this.ViewState["cW"] != null) MaxWidth = (int)this.ViewState["cW"];
			if (this.ViewState["fH"] != null) MinHeight = (int)this.ViewState["fH"];
			if (this.ViewState["fW"] != null) MinWidth = (int)this.ViewState["fW"];
			if (this.ViewState["iH"] != null) CropHeight = (int)this.ViewState["iH"];
			if (this.ViewState["iW"] != null) CropWidth = (int)this.ViewState["iW"];
			if (this.ViewState["tX"] != null) ShowTextHelpers = (bool)this.ViewState["tX"];
			if (this.ViewState["iU"] != null) ImageUrl = (string)this.ViewState["iU"];
			if (this.ViewState["imageGuid"] != null) ImageGuid = (Guid)this.ViewState["imageGuid"];
			if (this.ViewState["imageStore"] != null) ImageStore = (Storage.Stores)this.ViewState["imageStore"];
			if (this.ViewState["iZ"] != null) Zoom = (double)this.ViewState["iZ"];
			if (this.ViewState["oX"] != null) XOffset = (double)this.ViewState["oX"];
			if (this.ViewState["oY"] != null) YOffset = (double)this.ViewState["oY"];
			if (this.ViewState["sB"] != null) ZoomBarLength = (int)this.ViewState["sB"];
			if (this.ViewState["sY"] != null) ZoomBarPosition = (int)this.ViewState["sY"];
			if (this.ViewState["sH"] != null) ZoomBarThickness = (int)this.ViewState["sH"];
		}
		#endregion
		#region GetFlashVars()
		public string GetFlashVars()
		{
			/*
			variables

			var = name : type : default value

			pT = _passthrough : string : "undefined"
			tX = text helpers: boolean : false
			iU = image URL : string :  "error.jpg"
			iZ = initial Zoom : number (percent) : 100
			oX = offset x : number : 0
			oY = offset y : number : 0
			aW = allow crop width change : boolean : false
			aH = allow crop height change : boolean : false
			iW = initial crop width : number : 100
			iH = initial crop Height : number : 100
			cW = maximum crop width : number : image._width/2
			cH = maximum crop height : number : image._height/2
			fW = minimum crop width : number : 20
			fH = minimum crop height : number : 20
			sB = slideBar length : number : 300
			sY = slideBar y offset from base: number : 80
			sH = slideBar height : number : 4
			cP = crop button postion: boolean : false

			*/
			StringBuilder sb = new StringBuilder();
			Append(sb, "iU", ImageUrl);
			if (ShowTextHelpers)
				Append(sb, "tX", "true");
			Append(sb, "iZ", Zoom.ToString());
			Append(sb, "iW", CropWidth.ToString());
			Append(sb, "iH", CropHeight.ToString());
			Append(sb, "oX", XOffset.ToString());
			Append(sb, "oY", YOffset.ToString());
			if (AllowCustomHeight)
				Append(sb, "aH", "true");
			if (AllowCustomWidth)
				Append(sb, "aW", "true");
			Append(sb, "cW", MaxWidth.ToString());
			Append(sb, "cH", MaxHeight.ToString());
			Append(sb, "fW", MinWidth.ToString());
			Append(sb, "fH", MinHeight.ToString());
			Append(sb, "sB", ZoomBarLength.ToString());
			Append(sb, "sH", ZoomBarThickness.ToString());
			Append(sb, "sY", ZoomBarPosition.ToString());
			Append(sb, "pT", this.UniqueID);

			return sb.ToString();
		}
		public void Append(StringBuilder Builder, string Key, string Value)
		{
			Builder.Append(Builder.Length == 0 ? "" : "&");
			Builder.Append(HttpUtility.HtmlEncode(Key));
			Builder.Append("=");
			Builder.Append(HttpUtility.HtmlEncode(Value));

		}
		#endregion

	}
}
