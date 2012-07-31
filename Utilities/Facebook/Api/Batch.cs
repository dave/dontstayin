using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook.Json;

namespace Facebook.Api
{
    /// <summary>Provides access to the <a href="http://wiki.developers.facebook.com/index.php/Using_batching_API">Batching API</a>, which
    /// allows multiple API calls to be executing in a single request.</summary>
    [Serializable]
    public class Batch : IDisposable
    {
        /// <summary>Represents the maximum number of operations that can be executed in a single batch.</summary>
        /// <remarks>The value of this constant is 20, which is the hard limit set by the Facebook API.
        /// See Facebook's documentation on the <a href="http://wiki.developers.facebook.com/index.php/Using_batching_API">Batching API</a> for more information.</remarks>
        public const Int32 MaxBatchedOperations = 20;

        /// <summary>Initializes an instance of <see cref="Batch" />.</summary>
        /// <param name="context">A reference to the <see cref="IFacebookContext" /> object that will own the batch.</param>
        internal Batch(IFacebookContext context)
        {
            this.Items = new List<IBatchItem>();
            this.Context = context;
        }

        /// <summary>Adds a new item to the batch.</summary>
        /// <typeparam name="TReturnType">The return type of the API method.</typeparam>
        /// <param name="request">A <see cref="FacebookRequest" /> object containing information about the request such as the method and any parameters to be passed in.</param>
        /// <param name="response">An <see cref="IFacebookResponse" /> object representing the unpopulated response of the request.</param>
        /// <exception cref="InvalidOperationException">The batch has exceeded the limit of 20 individual operations.</exception>
        internal static void Add<TReturnType>(FacebookRequest request, IFacebookResponse response)
        {
            var current = request.Context.Batch;
            if (current.Items.Count < Batch.MaxBatchedOperations) current.Items.Add(new BatchItem<TReturnType>(request, response));
            else throw new InvalidOperationException(String.Format("Only {0} operations may be batched.", Batch.MaxBatchedOperations));
        }

        /// <summary>Activates a batch, which will route all requests to be queued until <see cref="Batch.Complete" /> is called.</summary>
        /// <param name="context">A reference to the <see cref="IFacebookContext" /> object that will own the batch.</param>
        /// <returns>A <see cref="Batch" /> object used to control when responses for batched requests will be retrieved.</returns>
        public static Batch Start(IFacebookContext context)
        {
            if (context.Batch == null)
            {
                context.Batch = new Batch(context);
                return context.Batch;
            }
            else if (context.Batch.IsCompleting) throw new InvalidOperationException("A batch is currently completing. You must wait for the current batch to complete its request before starting a new one.");
            else throw new InvalidOperationException("A batch is already in progress. You must either complete or discard the batch before starting a new one.");
        }

        /// <summary>Gets a value representing whether the <see cref="Batch" /> is current in the process of receiving data from the Facebook API.</summary>
        internal Boolean IsCompleting { get; private set; }

        /// <summary>Causes all batch operations to completed and assigns the responses to their respective <see cref="IFacebookResponse" /> objects.</summary>
        public void Complete()
        {
            this.CompleteInternal();
        }

        /// <summary>Causes all batch operations to completed and assigns the responses to their respective <see cref="IFacebookResponse" /> objects.</summary>
        /// <returns>A <see cref="String" /> containing the responses for each request, delimited by newlines.</returns>
        public String CompleteDebug()
        {
            return this.CompleteInternal();
        }

        /// <summary>Causes all batch operations to completed and assigns the responses to their respective <see cref="IFacebookResponse" /> objects.</summary>
        private String CompleteInternal()
        {
            var feedQuery =
                from batchItem in this.Context.Batch.Items
                select batchItem.Request.GenerateRequestContent();

            var feed = feedQuery.ToJson();
            var args = new Dictionary<String, Object>();
            args.Add("method_feed", feed);

            this.Context.Batch.IsCompleting = true;
            var response = this.Context.ExecuteRequest<String[]>("Batch.run", args);
            if (response.IsError) throw response.ResponseException;
            else
            {
                String[] batchResponses = response.Value;

                for (var responseIndex = 0; responseIndex < batchResponses.Length; responseIndex++)
                {
                    var responseItem = batchResponses[responseIndex];
                    var batchItem = this.Items[responseIndex];
                    batchItem.Response.Init(responseItem);
                }

                return batchResponses.ToDelimitedString(Environment.NewLine);
            }
        }

        /// <summary>Gets the current number of batched requests.</summary>
        public Int32 Count { get { return this.Items.Count; } }

        /// <summary>Gets the internal list of batch items.</summary>
        internal List<IBatchItem> Items { get; private set; }

        /// <summary>Gets a reference to the <see cref="IFacebookContext" /> object that owns the batch.</summary>
        internal IFacebookContext Context { get; private set; }

        /// <summary>Cancels the batch, causing any batch items and the batch itself to be discarded.</summary>
        public void Discard()
        {
            ((IDisposable)this).Dispose();
        }

        #region [ IDisposable Members ]

        void IDisposable.Dispose()
        {
            this.Context.Batch = null;
        }

        #endregion
    }
}
