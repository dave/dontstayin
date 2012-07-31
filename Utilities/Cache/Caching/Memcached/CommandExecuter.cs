using System;
using System.Collections.Generic;
using System.Text;
using Caching.Memcached.Commands;
using Common.Pooling;

namespace Caching.Memcached
{
	class CommandExecuter
	{
		MemcachedInstances instances;
		public CommandExecuter(MemcachedInstances instances)
		{
			this.instances = instances;
		}
		public void Execute(KeyedCommand command)
		{
			MemcachedInstance instance = instances.GetMemcachedInstance(command.Key);
			using (Pooled<MemcachedSocket> pooledSocket = instance.SocketPool.Get())
			{
				try
				{
					command.Execute(pooledSocket.Item);
					pooledSocket.ItemCanBeReturnedToPool = true;
				}
				catch (Exception ex)
				{
					throw new MemcachedClientException(command.Key, instance.IPEndPoint, ex);
				}
			}
		}
		public void ExecuteCommands(ICanBeUsedInMultiCommand[] commands)
		{

			List<ICanBeUsedInMultiCommand>[] commandBuckets = SortCommandsIntoBuckets(commands);
			for (int i = 0; i < commandBuckets.Length; i++)
			{
				List<ICanBeUsedInMultiCommand> commandBucket = commandBuckets[i];
				if (commandBucket != null)
				{
					MemcachedInstance instance = instances.GetMemcachedInstance(i);
					using (Pooled<MemcachedSocket> pooledSocket = instance.SocketPool.Get())
					{
						try
						{
							int maximumBatchSize = 30;//DAVE - 1 or 5
							while (commandBucket.Count > maximumBatchSize)
							{
								List<ICanBeUsedInMultiCommand> batch = commandBucket.GetRange(0, maximumBatchSize);
								commandBucket.RemoveRange(0, maximumBatchSize);
								RunBatch(batch, pooledSocket);
							}
							RunBatch(commandBucket, pooledSocket);
							pooledSocket.ItemCanBeReturnedToPool = true;
						}
						catch (Exception ex)
						{
							throw ex;
						}
					}
				}
			}
		}

		private List<ICanBeUsedInMultiCommand>[] SortCommandsIntoBuckets(ICanBeUsedInMultiCommand[] commands)
		{
			List<ICanBeUsedInMultiCommand>[] commandBuckets = new List<ICanBeUsedInMultiCommand>[instances.Count];
			foreach (ICanBeUsedInMultiCommand command in commands)
			{
				int instanceIndex = instances.GetMemcachedInstanceIndex(command.Key);
				if (commandBuckets[instanceIndex] == null)
				{
					commandBuckets[instanceIndex] = new List<ICanBeUsedInMultiCommand>();
				}
				commandBuckets[instanceIndex].Add(command);

			}
			return commandBuckets;
		}

		private static void RunBatch(List<ICanBeUsedInMultiCommand> batch, Pooled<MemcachedSocket> pooledSocket)
		{
			if (batch.Count > 0)
			{
				foreach (ICanBeUsedInMultiCommand command in batch)
				{
					command.BeforeFlush(pooledSocket.Item);
				}
				pooledSocket.Item.Flush();
				foreach (ICanBeUsedInMultiCommand command in batch)
				{
					command.AfterFlush(pooledSocket.Item);
				}
			}
		}
		
	}
}
