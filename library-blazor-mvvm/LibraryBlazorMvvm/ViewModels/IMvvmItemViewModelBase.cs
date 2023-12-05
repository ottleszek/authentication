namespace LibraryBlazorMvvm.ViewModels
{
	public interface IMvvmItemViewModelBase<TItem> : IMvvmViewModelBase
	{
		public TItem GetByAsync<TItem>(TItem item);
	}
}
