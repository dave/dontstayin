namespace ServiceLocator
{
	public interface IKernel
	{
		T Get<T>();
	}
}
