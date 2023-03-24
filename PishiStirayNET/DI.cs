using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PishiStirayNET.Data;
using PishiStirayNET.Services;
using PishiStirayNET.VeiwModels;
using System.Configuration;

namespace PishiStirayNET
{
    internal class DI
    {
        public static ServiceProvider? _provider;

        public static void Init()
        {
            ServiceCollection services = new ServiceCollection();

            #region Services

            services.AddSingleton<UserService>();
            services.AddScoped<PageService>();
            services.AddTransient<ProductService>();
            services.AddSingleton<OrderService>();
            services.AddTransient<SaveFileDialogService>();
            services.AddTransient<ManufacturersService>();
            services.AddTransient<DeliveriesService>();

            #endregion


            #region ViewModels

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<SignInPageViewModel>();
            services.AddTransient<ProductsPageViewModel>();
            services.AddTransient<AuthorizedUserUserControlViewModel>();
            services.AddTransient<CartPageViewModel>();
            services.AddTransient<AddProductPageViewModel>();
            services.AddTransient<CangeProductPageViewModel>();
            services.AddTransient<SignUpPageViewModel>();
            services.AddTransient<ManufacturersPageViewModel>();
            services.AddTransient<DeliveriesPageViewModel>();

            #endregion





            #region Database Contexts
            services.AddDbContext<TradeContext>(options =>
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TradeDatabase"].ConnectionString;
                options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            }, ServiceLifetime.Transient);





            #endregion

            _provider = services.BuildServiceProvider();

            foreach (var service in services)
            {
                _provider.GetRequiredService(service.ServiceType);
            }
        }

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();
        public SignInPageViewModel SignInPageViewModel => _provider.GetRequiredService<SignInPageViewModel>();
        public ProductsPageViewModel ProductsPageViewModel => _provider.GetRequiredService<ProductsPageViewModel>();
        public AuthorizedUserUserControlViewModel AuthorizedUserUserControlViewModel => _provider.GetRequiredService<AuthorizedUserUserControlViewModel>();
        public CartPageViewModel CartPageViewModel => _provider?.GetRequiredService<CartPageViewModel>();
        public AddProductPageViewModel AddProductPageViewModel => _provider?.GetRequiredService<AddProductPageViewModel>();
        public CangeProductPageViewModel CangeProductPageViewModel => _provider?.GetRequiredService<CangeProductPageViewModel>();
        public SignUpPageViewModel SignUpPageViewModel => _provider?.GetRequiredService<SignUpPageViewModel>();
        public ManufacturersPageViewModel ManufacturersPageViewModel => _provider?.GetRequiredService<ManufacturersPageViewModel>();
        public DeliveriesPageViewModel DeliveriesPageViewModel => _provider?.GetRequiredService<DeliveriesPageViewModel>();
    }
}
