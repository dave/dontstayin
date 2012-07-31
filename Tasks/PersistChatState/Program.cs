using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bobs;
using SpottedScript.Controls.ChatClient.Shared;

namespace PersistChatState
{
	class Program
	{
		static void Main(string[] args)
		{
			Query onlineUsrsQuery = new Query();
			onlineUsrsQuery.Columns = new ColumnSet(Usr.Columns.K);
			onlineUsrsQuery.QueryCondition = new Q(Usr.Columns.DateTimeLastPageRequest, QueryOperator.GreaterThan, DateTime.Now.AddMinutes(-5));
			UsrSet us = new UsrSet(onlineUsrsQuery);


			foreach (Usr u in us)
			{
				try
				{
					
					string ticks = Caching.Instances.Main.Get(Chat.GetStateDirtyKey(u.K).ToString()) as string;
					if (ticks != null && ticks.Length > 1)
					{
						DateTime dtLastDirty = new DateTime(long.Parse(ticks));
						if (dtLastDirty > DateTime.Now.AddDays(-1) && dtLastDirty < DateTime.Now.AddSeconds(Vars.DevEnv ? -5 : -30))
						{
							Query q = new Query();
							q.QueryCondition = new Q(RoomPin.Columns.UsrK, u.K);
							q.Columns = new ColumnSet(RoomPin.Columns.RoomGuid, RoomPin.Columns.StateStub, RoomPin.Columns.UsrK);
							RoomPinSet rps = new RoomPinSet(q);
							StateStub[] statesArray = Chat.GetStateFromCache(rps.ToList().ConvertAll(rp => rp.RoomGuid).ToArray(), u.K, Guid.Empty);
							Dictionary<Guid, StateStub> states = new Dictionary<Guid, StateStub>();
							
							if (statesArray != null)
							{
								foreach (StateStub state in statesArray)
								{
									Guid g = state.guid.UnPackGuid();
									if (!states.ContainsKey(g))
										states.Add(g, state);
								}
							}

							foreach (RoomPin rp in rps)
							{
								if (states.ContainsKey(rp.RoomGuid))
								{
									rp.StateStub = Chat.SerializeStateStub(states[rp.RoomGuid]);
									rp.Update();
								}
							}

							Caching.Instances.Main.Set(Chat.GetStateDirtyKey(u.K).ToString(), "0");

							Console.Write(".");
						}
						else
							Console.Write("-");
					}
					else
						Console.Write("/");
				}
				catch(Exception ex)
				{
					Console.Write("X");
				}
			}
		}
	}
}
