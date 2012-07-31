using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.Json;

namespace Facebook.Api.Controllers
{
    public partial class FeedController
    {
        /// <summary>Publishes a Mini-Feed story to the Facebook Page corresponding to the <code>page_actor_id</code> parameter.</summary>
        public FacebookResponse<Boolean> PublishTemplatizedAction(PageActionStory template)
        {
            Dictionary<String, Object> args = new Dictionary<String, Object>(template.GetArgs());
            var response = this.ExecuteRequest<Boolean>("Feed.publishTemplatizedAction", args);
            return response;
        }

        public FacebookResponse<Boolean> PublishUserAction(UserActionStory data)
        {
            Dictionary<String, Object> args = new Dictionary<String, Object>(data.GetArgs());
            var response = this.ExecuteRequest<Boolean>("Feed.publishUserAction", args);
            return response;
        }

        public FacebookResponse<Int64> RegisterTemplateBundle(Configuration.FeedTemplateBundleElement template)
        {
            return this.RegisterTemplateBundle(
                   new String[] { JsonConvert.SerializeObject(new String[] { template.OneLineTemplate }) },
                   new String[] { JsonConvert.SerializeObject(new Configuration.FeedStoryTemplateElement[] { template.ShortStory }) },
                   new String[] { JsonConvert.SerializeObject(new Configuration.FeedActionLinkElement[] { template.ActionLink }) });
        }

        /// <summary>Ensures both that all configured template bundles are registered and that the application knows the ids for each of them.</summary>
        public void InitFeedTemplates()
        {
            var ids = Configuration.FeedTemplateBundleIds.GetCurrent(this.FacebookContext.ApiKey);
            var configuredTemplates = Configuration.FeedTemplateBundles.Get();
            var missing = (
                from configured in configuredTemplates
                where !ids.Any(id => id.Name == configured.Name)
                select configured).ToList();

            if (missing.Count > 0)
            {
                FacebookList<TemplateBundle> registeredTemplates = this.GetRegisteredTemplateBundles();
                var registerResponses = new Dictionary<String, FacebookResponse<Int64>>();
                using(var batch = Batch.Start(this.FacebookContext))
                {
                    foreach (Configuration.FeedTemplateBundleElement configured in missing)
                    {
                        var registered = registeredTemplates.FirstOrDefault(template => configured.Equals(template));
                        if (registered == null)
                        {
                            registerResponses.Add(configured.Name, this.RegisterTemplateBundle(configured));
                        }
                        else
                        {
                            ids.Add(new Configuration.FeedTemplateBundleIdMap { Name = configured.Name, Id = registered.TemplateBundleId });
                        }
                    }

                    batch.Complete();
                }
                foreach (var key in registerResponses.Keys)
                {
                    ids.Add(new Configuration.FeedTemplateBundleIdMap { Name = key, Id = registerResponses[key].Value });
                }
            }
            ids.Save();
        }
    }
}
