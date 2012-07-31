using System;
using System.Collections.Generic;
using System.Text;
using Caching;
using Bobs.ChildInterfaces;
using Bobs.CachedDataAccess;

namespace Bobs
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region Tag
	/// <summary>
	/// Tag definitions
	/// </summary>
	[Serializable]
	public partial class Tag : ICacheKeyProvider, ILinkable, IHasChildPhotos
	{

		#region Simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Tag.Columns.K] as int? ?? 0; }
			set { this[Tag.Columns.K] = value; }
		}
		/// <summary>
		/// The actual tag itself
		/// </summary>
		public override string TagText
		{
			get { return (string)this[Tag.Columns.TagText]; }
			set { this[Tag.Columns.TagText] = value; }
		}
		/// <summary>
		/// Used to block offensive terms
		/// </summary>
		public override bool Blocked
		{
			get { return (bool)this[Tag.Columns.Blocked]; }
			set { this[Tag.Columns.Blocked] = value; }
		}
		/// <summary>
		/// Usr who blocked it
		/// </summary>
		public override int BlockedByUsrK
		{
			get { return (int)this[Tag.Columns.BlockedByUsrK]; }
			set { this[Tag.Columns.BlockedByUsrK] = value; }
		}
		/// <summary>
		/// When it was blocked
		/// </summary>
		public override DateTime BlockedDateTime
		{
			get { return (DateTime)this[Tag.Columns.BlockedDateTime]; }
			set { this[Tag.Columns.BlockedDateTime] = value; }
		}
				/// <summary>
		/// should this be shown in the tag cloud
		/// </summary>
		public override bool ShowInTagCloud
		{
			get { return (bool)this[Tag.Columns.ShowInTagCloud]; }
			set { this[Tag.Columns.ShowInTagCloud] = value; }
		}
		#endregion

		public static Tag AddTag(string tagText, Tagging.ITaggable objectToTag, Usr usrAddingTag)
		{
			try
			{
				Transaction t = new Transaction();
				try
				{
					Tag tag = Tag.GetTag(tagText);
					if (tag.Blocked)
					{
						throw new InvalidTagException();
					}
					TagPhoto tagPhoto = TagPhoto.GetTagPhoto(tag.K, objectToTag.K);
					TagPhotoHistory.TagPhotoHistoryAction action = TagPhotoHistory.TagPhotoHistoryAction.Created;
					if (tagPhoto == null)
					{
						tagPhoto = new TagPhoto()
						{
							TagK = tag.K,
							PhotoK = objectToTag.K,
							Disabled = false
						};
						tagPhoto.Update(t);
						action = TagPhotoHistory.TagPhotoHistoryAction.Created;
					}
					if (tagPhoto.Disabled)
					{
						if (!usrAddingTag.IsJunior)
						{
							throw new Exception("You do not have rights to re-enable that tag");
						}
						tagPhoto.Disabled = false;
						tagPhoto.Update(t);
						action = TagPhotoHistory.TagPhotoHistoryAction.Enabled;
					}
					TagPhotoHistory history = new TagPhotoHistory()
					{
						DateTime = DateTime.Now,
						Action = action,
						UsrK = usrAddingTag.K,
						TagPhotoK = tagPhoto.K
					};
					history.Update(t);
					t.Commit();

					return tag;
				}
				catch (Exception ex)
				{
					t.Rollback();
					throw ex;
				}
			}
			catch (InvalidTagException)
			{
				return null;
			}
		}

		#region LinkedTables
		public CachedSqlSelect<Photo> ChildPhotos()
		{
			return this.ChildPhotos(null, null);
		}
		public CachedSqlSelect<Photo> ChildPhotos(Q where)
		{
			return this.ChildPhotos(where, null);
		}
		public CachedSqlSelect<Photo> ChildPhotos(params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			return this.ChildPhotos(null, orderBy);
		}
		public CachedSqlSelect<Photo> ChildPhotos(Q where, params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			PhotoTableDef def = new PhotoTableDef();
			TagPhotoTableDef def2 = new TagPhotoTableDef();
			return new CachedSqlSelect<Photo>(
				new LinkedChildren<Photo>
				(
					TablesEnum.Tag,
					this.K,
					TablesEnum.Photo,
					def.TableCacheKey,
					dr =>
					{
						Photo newPhoto = new Photo();
						newPhoto.Initialise(dr);
						return newPhoto;
					},
					where,
					orderBy,
					TablesEnum.TagPhoto,
					def2.TableCacheKey
				)
			);
		}
		#endregion
		const string ValidChars = "abcdefghijklmnopqrstuvwxyz ";
		private static string RemoveAnyInvalidCharactersAndStuff(string tag)
		{
			StringBuilder sb = new StringBuilder();
			char[] tagChars = tag.ToLower().ToCharArray();
			for (int i = 0; i < tagChars.Length; i++)
			{
				char c = tagChars[i];
				if (ValidChars.IndexOf(c) > -1)
				{
					sb.Append(c);
				}
			}
			string filteredTag = sb.ToString().Trim();
			while (filteredTag.IndexOf("  ") > -1)
			{
				filteredTag.Replace("  ", "");
			}
			if (filteredTag.Length == 0)
			{
				throw new InvalidTagException();
			}
			if (filteredTag.Length > 50) { filteredTag = filteredTag.Substring(0, 50); }
			return filteredTag;
		}
		public static Tag GetTag(string tagText)
		{
			tagText = RemoveAnyInvalidCharactersAndStuff(tagText);
			Query query = new Query(new Q(Tag.Columns.TagText, tagText));
			TagSet tagSet = new TagSet(query);
			if (tagSet.Count == 1)
			{
				return tagSet[0];
			}
			else
			{
				Tag tag = new Tag() { TagText = tagText };
				tag.Update();
				return tag;
			}
		}

 
		#region ICacheKeyProvider Members

		public string GetCacheKey()
		{
			return new CacheKey(CacheKeyPrefix.TagVersion, K.ToString());
		}

		#endregion

		public string Link(params string[] par)
		{
			return ILinkableExtentions.Link(this, par);
		}

		public string LinkNewWindow(params string[] par)
		{
			return ILinkableExtentions.LinkNewWindow(this, par);
		}

		public string Url(params string[] par)
		{
			return UrlInfo.MakeUrl("/tags/" + Cambro.Web.Helpers.UrlTextSerialize(TagText.ToLower()), null, par);
			//return @"/tags/photos/" + ReadableReference.Replace(' ', '-');
		}

		public string ReadableReference
		{
			get { return TagText; }
		}

		#region BlockedByUsr
		private Usr blockedByUsr;
		public Usr BlockedByUsr
		{
			get
			{
				if (blockedByUsr == null)
				{
					blockedByUsr = new Usr(this.BlockedByUsrK);
				}
				return blockedByUsr;
			}
		}
		#endregion

		
		#region GetBlockedTags
		public static TagSet GetBlockedTags()
		{
			Query q = new Query();
			q.QueryCondition = new Q(Tag.Columns.Blocked, true);
			q.OrderBy = new OrderBy(Tag.Columns.TagText);
			return new TagSet(q);
		}
		#endregion

		


		public void SetBlockedAndUpdate(bool blocked)
		{
			if (!Usr.Current.IsAdmin)
			{
				throw new Exception("You need to be an admin to block or unblock a tag");
			}
			Transaction t = new Transaction();


			Query q = new Query(new Q(TagPhoto.Columns.TagK, this.K));
			List<int> tagPhotoKs = (new TagPhotoSet(q)).ToList().ConvertAll(tagPhoto => tagPhoto.K);

			List<Assign> changes = new List<Assign>()
				{
					new Assign(TagPhoto.Columns.Disabled, blocked)
				};

			Q condition = new And(
				new Q(TagPhoto.Columns.TagK, this.K),
				new Q(TagPhoto.Columns.Disabled, !blocked)
			);
			Update u = new Update(TablesEnum.TagPhoto, changes, condition);
			u.Run(t);
			foreach (int tagPhotoK in tagPhotoKs)
			{
				TagPhotoHistory historyItem = new TagPhotoHistory()
				{
					Action = blocked ? TagPhotoHistory.TagPhotoHistoryAction.Blocked : TagPhotoHistory.TagPhotoHistoryAction.Unblocked,
					DateTime = DateTime.Now,
					TagPhotoK = tagPhotoK,
					UsrK = Usr.Current.K
				};
				historyItem.Update(t);
			}



			this.Blocked = blocked;
			this.BlockedDateTime = DateTime.Now;
			this.BlockedByUsrK = Usr.Current.K;
			this.Update(t);

			t.Commit();
			(new Caching.CacheKeys.NamespaceCacheKey(CacheKeyPrefix.TagCloudVersion)).Invalidate();
		}

	 
	}
	#endregion
	public class InvalidTagException : Exception { }
	public class BlockedTagException : Exception { }
}
