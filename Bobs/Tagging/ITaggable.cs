using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Caching;
using Bobs.ChildInterfaces;

namespace Bobs.Tagging
{
	public interface ITaggable : IHasChildTags
	{
		int K { get; }
		int ItemOwnerUsrK { get; }
	}

	//public static class ITaggableExtensions
	//{
	//    public static string TagsCacheKey(this ITaggable taggable)
	//    {
	//        return new CacheKey(CacheKeyPrefix.TagsForAnITaggable, taggable.K.ToString());
	//    }

	//    public static void AddTag(this ITaggable taggable, string tagText, int usrK)
	//    {
	//        try
	//        {
	//            Tag tag = Tag.GetTag(tagText);
	//            if (!tag.Blocked)
	//            {
	//                Query q = new Query(new And(new Q(TagPhoto.Columns.PhotoK, tag.K), new Q(TagPhoto.Columns.PhotoK, taggable.K)));
	//                TagPhotoSet set = new TagPhotoSet(q);
	//                if (set.Count == 0)
	//                {
	//                    TagPhoto newTag = new TagPhoto()
	//                    {
	//                        TagK = tag.K,
	//                        PhotoK = taggable.K,
	//                        UsrK = usrK,
	//                        DateTime = DateTime.Now
	//                    };
	//                    newTag.Update();
	//                }
	//                else
	//                {
	//                    TagPhoto tagPhoto = set[0];
	//                    if (tagPhoto.Disabled)
	//                    {
	//                        if (Usr.Current.IsJunior || Usr.Current.IsAdmin || Usr.Current.K == taggable.ItemOwnerUsrK)
	//                        {
	//                            tagPhoto.Disabled = false;
	//                            tagPhoto.Update();
	//                        }
	//                    }
	//                }
	//            }
	//        }
	//        catch
	//        {
	//            //if there's an error here we don't really want to do much about it
	//        }
	//    }
		
	//    public static void RemoveTag(this ITaggable taggable, string tagText, Usr usr)
	//    {
	//        if (taggable.TagsCanBeRemovedByUsr(usr))
	//        {
	//            Tag tag = Tag.GetTag(tagText);
	//            TagPhoto tagPhoto = TagPhoto.Get(tag.K, taggable.K);
	//            if (tagPhoto != null)
	//            {
	//                if (tagPhoto.ReenabledByUsrK != null)
	//                {
	//                    Usr  = new Usr(tagPhoto.ReenabledByUsrK);
	//                    if(	(!usrWhoReenabledTheTag.IsJunior && (Usr.Current.IsJunior || Usr.Current.K == taggable.ItemOwnerUsrK))
	//                    ||	(usrWhoReenabledTheTag.IsJunior && Usr.Current.IsAdmin))
	//                    {
	//                        tagPhoto.Disabled = true;

	//                    }
	//                }
	//                else
	//                {
	//                    tagPhoto.Disabled = true;
	//                    tagPhoto.DisabledByUsrK = usr.K;
	//                    tagPhoto.DisabledDateTime = DateTime.Now;
	//                    tagPhoto.Update();
	//                }
	//            }
	//        }
	//        else
	//        {
	//            throw new DsiUserFriendlyException("Usr " + usr.K + " can not remove tags on Taggable(" + taggable.K.ToString() + ")");
	//        }
	//    }
	//    public static List<Tag> GetTags(this ITaggable taggable)
	//    {
	//        Photo photo = new Photo(taggable.K);
	//        return new List<Tag>(photo.ChildTags(new And(new Q(TagPhoto.Columns.Disabled, false), new Q(Tag.Columns.Blocked, false))));
	//    }
	//    public static bool TagsCanBeRemovedByUsr(this ITaggable taggable, Bobs.Usr usr)
	//    {
	//        return usr != null;// && (usr.K == taggable.ItemOwnerUsrK || usr.IsAdmin);
	//    }
	//}
}
