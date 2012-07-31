using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Web;

namespace Bobs
{
	public abstract class BobSet : ICollection, IEnumerator
	{
		protected int CurrentResultIndex;

		public TableDef Table { get; set; }
		public Query Query { get; set; }

		public abstract void InitTable();

		//TODO: Add Filter functionality, preferably by using a Q object.  To be applied to Bobset.Dataset.DefaultView.RowFilter

		#region Constructor used to fill with data
		public BobSet()
		{
			InitTable();
		}
		public BobSet(Query query) : this()
		{
			this.Query = query;

			Query.MainTable = Table;

			if (Query.TableElement == null)
				Query.TableElement = new TableElement(Table);

			CurrentResultIndex = -1;
			
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				conn.Open();
				SqlCommand command = new SqlCommand(Query.Sql.ToString(), conn);

				foreach (string c in Query.Sql.Parameters.Keys)
					command.Parameters.Add(Query.Sql.Parameters[c]);
				foreach (var p in Query.ExtraParameters)
					command.Parameters.Add(p);

				if (Vars.TraceQueries && HttpContext.Current != null)
					HttpContext.Current.Trace.Write(command.CommandText);
				
				try
				{
					this.RunNow(command, conn, query.CacheDuration);
				}
				catch (System.Data.SqlClient.SqlException exc)
				{
					if (exc.Message.IndexOf("was deadlocked on lock resources") > -1)
					{
						Random r = new Random();
						int rand = r.Next(250) + 250;
						System.Threading.Thread.Sleep(rand);

						try
						{
							this.RunNow(command, conn, query.CacheDuration);
						}
						catch (System.Data.SqlClient.SqlException exc1)
						{
							throw new Exception("Caught error after timeout", exc1);
						}
					}
					else
					{
						if (Vars.DevEnv)
							throw new Exception("Failed running query: " + command.CommandText, exc);
						else
							throw exc;
					}
				}

				
			}
			finally
			{
				conn.Close();
			}
		}
		#region RunNow()
		/// <summary>
		/// Just used by constructor
		/// </summary>
		/// <param name="command"></param>
		private void RunNow(SqlCommand command, SqlConnection conn, TimeSpan? cacheDuration)
		{

			int selectCount = 0;

			try
			{
				if (Query.ReturnCountOnly)
				{
					if (cacheDuration != null)
					{
						string cacheKey = Query.GetCacheKey() + cacheDuration.Value.ToString();

						int? cachedResult = Caching.Instances.Main.Get(cacheKey) as int?;
						if (cachedResult != null)
						{
							Global.IncrementRequestCounter(Global.RequestCounter.CacheHit);
							ReturnCountOnlyCount = cachedResult.Value;
						}
						else
						{
							Global.IncrementRequestCounter(Global.RequestCounter.CacheMiss);
							selectCount++;
							ReturnCountOnlyCount = (int)command.ExecuteScalar();
							try
							{
								Caching.Instances.Main.Store(cacheKey, ReturnCountOnlyCount, cacheDuration.Value);
							}
							catch (Exception ex)
							{
								SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "BobSetStore2", "", "", 0, null);
							}
							Global.IncrementRequestCounter(Global.RequestCounter.CacheStore);
						}
					}
					else
					{
						selectCount++;
						ReturnCountOnlyCount = (int)command.ExecuteScalar();
					}
				}
				else
				{
					SqlDataAdapter Adapter = new SqlDataAdapter();
					Adapter.SelectCommand = command;
					if (Query.Paging.IsEnabled)
					{
						if (Query.Paging.RequestedPage > 5)
						{
							#region Page>5 - we are using ROW_NUMBER()
							selectCount++;



							Adapter.Fill(DataSet);
							if (DataSet.Tables[0].Rows.Count == 0 && Query.Paging.RequestedPageIndex > 0)
							{
								//We have returned no rows
								//Calculate the number of records
								Query.ReturnCountOnly = true;
								Query.Sql = null;
								SqlCommand countCommand = new SqlCommand(Query.Sql.ToString(), conn);
								foreach (string c in Query.Sql.Parameters.Keys)
									countCommand.Parameters.Add(Query.Sql.Parameters[c]);
								selectCount++;
								int totalItems = (int)countCommand.ExecuteScalar();

								if (totalItems == 0)
								{
									Paging.ReturnedPageIndex = 0;
									Paging.ShowNextPageLink = false;
								}
								else
								{
									//Get the last page of results
									int lastPageIndex = (int)Math.Ceiling((double)totalItems / (double)Query.Paging.RecordsPerPage) - 1;

									Query.ReturnCountOnly = false;
									Query.Sql = null;
									Query.Paging.RequestedPageIndex = lastPageIndex;
									Query.Paging.ForceUseRowNumber = true;
									command.CommandText = Query.Sql.ToString();

									selectCount++;
									Adapter.Fill(DataSet);

									Paging.ReturnedPageIndex = lastPageIndex;
									Paging.ShowNextPageLink = false;
								}

							}
							else if (DataSet.Tables[0].Rows.Count == 0)
							{
								Paging.ReturnedPageIndex = 0;
								Paging.ShowNextPageLink = false;
							}
							else
							{
								Paging.ReturnedPageIndex = Query.Paging.RequestedPageIndex;
								if (DataSet.Tables[0].DefaultView.Count == Query.Paging.RecordsPerPage + 1)
								{
									Paging.ShowNextPageLink = true;
									DataSet.Tables[0].Rows[Query.Paging.RecordsPerPage].Delete();
								}
								else
									Paging.ShowNextPageLink = false;
							}
							#endregion
						}
						else
						{
							try
							{
								selectCount++;
								Adapter.Fill(DataSet, Query.Paging.RecordsPerPage * Query.Paging.RequestedPageIndex, Query.Paging.RecordsPerPage + 1, "Table");
								Paging.ReturnedPageIndex = Query.Paging.RequestedPageIndex;
							}
							catch (System.IndexOutOfRangeException)
							{
								selectCount++;
								Adapter.Fill(DataSet, 0, 0, "Table");
								Paging.ReturnedPageIndex = (int)Math.Ceiling((double)DataSet.Tables[0].DefaultView.Count / (double)Query.Paging.RecordsPerPage) - 1;
								DataSet.Tables[0].Clear();
								selectCount++;
								Adapter.Fill(DataSet, Query.Paging.RecordsPerPage * Paging.ReturnedPageIndex, Query.Paging.RecordsPerPage + 1, "Table");
							}
							if (Paging.ReturnedPageIndex > 0 && DataSet.Tables[0].DefaultView.Count == 0)
							{
								selectCount++;
								Adapter.Fill(DataSet, 0, 0, "Table");
								Paging.ReturnedPageIndex = (int)Math.Ceiling((double)DataSet.Tables[0].DefaultView.Count / (double)Query.Paging.RecordsPerPage) - 1;
								DataSet.Tables[0].Clear();
								selectCount++;
								Adapter.Fill(DataSet, Query.Paging.RecordsPerPage * Paging.ReturnedPageIndex, Query.Paging.RecordsPerPage + 1, "Table");
							}
							if (DataSet.Tables[0].DefaultView.Count == Query.Paging.RecordsPerPage + 1)
							{
								Paging.ShowNextPageLink = true;
								DataSet.Tables[0].Rows[Query.Paging.RecordsPerPage].Delete();
							}
							else
								Paging.ShowNextPageLink = false;
						}
					}
					else
					{
						if (cacheDuration != null)
						{
							string cacheKey = Query.GetCacheKey() + cacheDuration.Value.ToString();
							DataSet cachedDataSet = Caching.Instances.Main.Get(cacheKey) as DataSet;
							if (cachedDataSet != null)
							{
								this.DataSet = cachedDataSet; 
								Global.IncrementRequestCounter(Global.RequestCounter.CacheHit);
							}
							else
							{
								selectCount++;
								Adapter.Fill(DataSet, Query.FillStartingAt, Query.FillMaxRecords, "Table");
								try
								{
									Caching.Instances.Main.Store(cacheKey, DataSet, cacheDuration.Value);
								}
								catch (Exception ex)
								{
									SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "BobSetStore", "", "", 0, null);
								}
								Global.IncrementRequestCounter(Global.RequestCounter.CacheStore);
							}
						}
						else
						{
							selectCount++;
							Adapter.Fill(DataSet, Query.FillStartingAt, Query.FillMaxRecords, "Table");
						}
					}
				}
			}
			finally
			{
				Global.LogSqlQuery(Bobs.Global.QueryTypes.Select, selectCount);
			}
		}
		#endregion
		#endregion

		#region DataSet
		public DataSet DataSet
		{
			get
			{
				if (dataSet == null)
					dataSet = new DataSet();
				return dataSet;
			}
			set
			{
				dataSet = value;
			}
		}
		private DataSet dataSet;
		#endregion

		#region Kill(int i)
		public void Kill(int i)
		{
			BobCache[i] = null;
		}
		#endregion

		#region KillAll()
		public void KillAll()
		{
			BobCache = new Hashtable();
		}
		#endregion

		#region BobCache
		protected Hashtable BobCache
		{
			get
			{
				if (bobCache == null)
					bobCache = new Hashtable();
				return bobCache;
			}
			set
			{
				bobCache = value;
			}
		}
		private Hashtable bobCache;
		#endregion

		#region PagingDescriptor
		public class PagingDescriptor
		{
			public BobSet Parent {get;set;}
			#region PagingDescriptor
			public PagingDescriptor(BobSet parent)
			{
				this.Parent = parent;
			}
			#endregion
			#region IsEnabled
			bool IsEnabled
			{
				get
				{
					return Parent.Query.Paging.IsEnabled;
				}
			}
			#endregion
			#region ReturnedPage
			public int ReturnedPage
			{
				get
				{
					return ReturnedPageIndex + 1;
				}
			}
			#endregion
			public int ReturnedPageIndex { get; set; }
			public bool ShowNextPageLink { get; set; }
			#region ShowPrevPageLink
			public bool ShowPrevPageLink
			{
				get
				{
					return ReturnedPageIndex > 0;
				}
			}
			#endregion
			#region ShowNoLinks
			public bool ShowNoLinks
			{
				get
				{
					return !this.ShowNextPageLink && !this.ShowPrevPageLink;
				}
			}
			#endregion

		}
		#endregion
		#region Paging
		public PagingDescriptor Paging
		{
			get
			{
				if (paging == null)
					paging = new PagingDescriptor(this);
				return paging;
			}
			set
			{
				paging = value;
			}
		}
		private PagingDescriptor paging;
		#endregion

		#region TableNames
		public ArrayList TableNames
		{
			get
			{
				if (tableNames == null)
				{
					tableNames = new ArrayList();
					if (this.Query.Distinct && this.Query.DataTableElement!=null)
						this.Query.DataTableElement.GetTableNames(tableNames);
					else
						this.Query.TableElement.GetTableNames(tableNames);
				}
				return tableNames;
			}
			set
			{
				tableNames = value;
			}
		}
		private ArrayList tableNames;
		#endregion

		#region Collection methods - Count, IEnumerator, IEnumerable, ICollection
		
		#region Count
		public int Count
		{
			get
			{
				if (Query.ReturnCountOnly)
					return ReturnCountOnlyCount;
				else
					return DataSet.Tables[0].DefaultView.Count;
			}
		}
		private int ReturnCountOnlyCount;
		#endregion

		#region Implementation of IEnumerator
		#region Reset()
		public void Reset()
		{
			CurrentResultIndex = -1;
		}
		#endregion
		#region MoveNext()
		public bool MoveNext()
		{
			if (CurrentResultIndex < Count - 1)
			{
				CurrentResultIndex++;
				return true;
			}
			else
			{
				Reset();
				return false;
			}
		}
		#endregion
		#region Current and GetFromIndex are defined in the derived class because we can't accesss this[] here.
		public abstract object Current { get; }
		public abstract object GetFromIndex(int index);
		#endregion
		#endregion
		#region Implementation of IEnumerable
		public System.Collections.IEnumerator GetEnumerator()
		{
			return (IEnumerator)this;
		}
		#endregion
		#region Implementation of ICollection
		#region IsReadOnly
		public bool IsReadOnly
		{
			get
			{
				// As our goal is something with add/remove capabilities,
				// it makes sense to return that Collection is R/W.
				//
				return true;
			}
		}
		#endregion
		#region CopyTo
		public void CopyTo(System.Array destArray, int startIndex)
		{
			for (int localIndex = startIndex; localIndex < this.Count; ++localIndex)
			{
				destArray.SetValue(GetFromIndex(localIndex), localIndex - startIndex);
			}
		}
		#endregion
		#region IsSynchronized
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}
		#endregion
		#region SyncRoot
		public object SyncRoot
		{
			get
			{
				return null;
			}
		}
		#endregion
		#endregion

		#endregion
	}


}
