namespace LibraryBlazorMvvm.ViewModels
{
	public interface IMvvmItemViewModelBase<TItem> : IMvvmViewModelBase
	{
        public bool IsNotChanged { get; }
        public bool IsChanged { get; }

    }
}
