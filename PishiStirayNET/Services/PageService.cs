namespace PishiStirayNET.Services
{
    class PageService
    {
        //public event Action<Page> OnPageChanged;

        //public void ChangePage(Page page)
        //{
        //    OnPageChanged?.Invoke(page);
        //}

        public Page? Page { get; set; }
        public void ChangePage(Page page)
        {
          Page = page;
        }
    }
}
